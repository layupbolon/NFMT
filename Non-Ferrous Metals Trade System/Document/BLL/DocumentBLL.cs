/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentBLL.cs
// 文件功能描述：制单dbo.Doc_Document业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年11月20日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Document.Model;
using NFMT.Document.DAL;
using NFMT.Document.IDAL;
using NFMT.Common;
using System.Linq;

namespace NFMT.Document.BLL
{
    /// <summary>
    /// 制单dbo.Doc_Document业务逻辑类。
    /// </summary>
    public class DocumentBLL : Common.ExecBLL
    {
        private DocumentDAL documentDAL = new DocumentDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(DocumentDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public DocumentBLL()
		{
		}
        
		#endregion

        #region 数据库操作
		
        protected override log4net.ILog Log
        {
            get { return this.log; }
        }

        public override IOperate Operate
        {
            get { return this.documentDAL; }
        }
		
        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, DateTime beginDate, DateTime endDate, string orderNo = "", int outerCorp = 0, int status = 0)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            select.OrderStr = string.IsNullOrEmpty(orderStr) ? "doc.DocumentId desc" : orderStr;
            
            int readyStatus =(int)StatusEnum.已生效;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("doc.DocumentId,do.OrderId,cdo.OrderType as CommercialOrderType");
            sb.Append(",doc.AcceptanceDate,doc.Acceptancer,accEmp.Name as AccEmpName");
            sb.Append(",doc.DocEmpId,docEmp.Name as DocEmpName,doc.DocumentDate");
            sb.Append(",doc.PresentDate,doc.Presenter,preEmp.Name as PreEmpName");
            sb.Append(",doc.DocumentStatus,ds.StatusName");
            sb.Append(",do.OrderNo,do.ApplyCorp,appCorp.CorpName as ApplyCorpName");
            sb.Append(",do.BuyerCorp,buyCorp.CorpName as BuyCorpName");
            sb.Append(",do.OrderType,ot.DetailName as OrderTypeName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Doc_Document doc ");
            sb.Append(" inner join dbo.Doc_DocumentOrder do on doc.OrderId = do.OrderId ");
            sb.Append(" left join NFMT_User.dbo.Employee accEmp on accEmp.EmpId = doc.Acceptancer ");
            sb.Append(" left join NFMT_User.dbo.Employee docEmp on docEmp.EmpId = doc.DocEmpId ");
            sb.Append(" left join NFMT_User.dbo.Employee preEmp on preEmp.EmpId = doc.Presenter ");
            sb.Append(" left join NFMT_Basic.dbo.BDStatusDetail ds on doc.DocumentStatus = ds.DetailId ");
            sb.Append(" left join NFMT_User.dbo.Corporation appCorp on appCorp.CorpId = do.ApplyCorp ");
            sb.Append(" left join NFMT_User.dbo.Corporation buyCorp on buyCorp.CorpId = do.BuyerCorp ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail ot on ot.StyleDetailId = do.OrderType ");

            sb.AppendFormat(" left join dbo.Doc_DocumentOrder cdo on do.CommercialId = cdo.OrderId and cdo.OrderStatus>={0}",readyStatus);

            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (beginDate > NFMT.Common.DefaultValue.DefaultTime && endDate > beginDate)
                sb.AppendFormat(" and doc.DocumentDate between '{0}' and '{1}' ", beginDate.ToShortDateString(), endDate.ToShortDateString());

            if (!string.IsNullOrEmpty(orderNo))
                sb.AppendFormat(" and doc.OrderNo like '%{0}%' ", orderNo);
            if (outerCorp > 0)
                sb.AppendFormat(" and do.BuyerCorp = {0} ", outerCorp);
            if (status > 0)
                sb.AppendFormat(" and doc.DocumentStatus = {0} ", status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Create(UserModel user, Model.Document doc, List<Model.DocumentStock> docStocks, bool isSubmitAudit)
        {
            ResultModel result = new ResultModel();

            try 
            {
                //新增制单主表
                //新增制单库存明细表
                //新增制单发票明细表
                //新增发票
                //提交审核
                DAL.DocumentOrderDAL orderDAL = new DocumentOrderDAL();
                DAL.DocumentOrderStockDAL orderStockDAL = new DocumentOrderStockDAL();
                DAL.DocumentOrderInvoiceDAL orderInvoiceDAL = new DocumentOrderInvoiceDAL();
                DAL.DocumentStockDAL docStockDAL = new DocumentStockDAL();
                DAL.DocumentInvoiceDAL docInvoiceDAL = new DocumentInvoiceDAL();
                Invoice.DAL.BusinessInvoiceDAL invoiceDAL = new Invoice.DAL.BusinessInvoiceDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    if (doc == null || doc.OrderId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单信息错误";
                        return result;
                    }

                    if (docStocks == null || docStocks.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "未选择任何制单库存";
                        return result;
                    }

                    //获取指令
                    result = orderDAL.Get(user, doc.OrderId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.DocumentOrder order = result.ReturnValue as Model.DocumentOrder;
                    if (order == null || order.OrderId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单指令不存在";
                        return result;
                    }

                    //判断指令状态
                    if (order.OrderStatus != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单指令非已生效状态，不能进行制单";
                        return result;
                    }

                    //新增制单
                    //doc.AcceptanceDate = DefaultValue.DefaultTime;
                    //doc.PresentDate = DefaultValue.DefaultTime;
                    doc.DocEmpId = user.EmpId;
                    doc.DocumentStatus = DocumentStatusEnum.已录入;
                    result = this.documentDAL.Insert(user, doc);
                    if (result.ResultStatus != 0)
                        return result;

                    int documentId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out documentId) || documentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单新增失败";
                        return result;
                    }

                    doc.DocumentId = documentId;

                    //获取当前指令所有库存明细
                    result = orderStockDAL.Load(user, order.OrderId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.DocumentOrderStock> orderStocks = result.ReturnValue as List<Model.DocumentOrderStock>;
                    if (orderStocks == null || orderStocks.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单指令库存明细获取失败";
                        return result;
                    }

                    //明细新增
                    foreach (Model.DocumentStock docStock in docStocks)
                    {
                        //验证选择明细是否在指令中
                        Model.DocumentOrderStock orderStock = orderStocks.FirstOrDefault(temp => temp.DetailId == docStock.DetailId);
                        if (orderStock == null || orderStock.DetailId<=0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "制单指令库存不存在";
                            return result;
                        }

                        //新增制单库存明细
                        Model.DocumentStock addDocStock = new DocumentStock();
                        addDocStock.DetailStatus = StatusEnum.已生效;
                        addDocStock.DocumentId = documentId;
                        addDocStock.OrderId = order.OrderId;
                        addDocStock.OrderStockDetailId = orderStock.DetailId;
                        addDocStock.RefNo = orderStock.RefNo;
                        addDocStock.StockId = orderStock.StockId;
                        addDocStock.StockNameId = orderStock.StockNameId;

                        result = docStockDAL.Insert(user, addDocStock);
                        if (result.ResultStatus != 0)
                            return result;

                        int docStockDetailId = 0;
                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out docStockDetailId) || docStockDetailId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "制单库存明细新增失败";
                            return result;
                        }

                        addDocStock.DetailId = docStockDetailId;

                        //获取指令发票明细
                        result = orderInvoiceDAL.GetByStockDetailId(user,orderStock.DetailId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.DocumentOrderInvoice orderInvoice = result.ReturnValue as Model.DocumentOrderInvoice;
                        if (orderInvoice == null || orderInvoice.DetailId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "制单指令发票明细不存在";
                            return result;
                        }

                        Model.DocumentInvoice docInvoice = new DocumentInvoice();
                        docInvoice.DetailStatus = StatusEnum.已生效;
                        docInvoice.DocumentId = doc.DocumentId;
                        docInvoice.InvoiceNo = orderInvoice.InvoiceNo;
                        docInvoice.InvoiceBala =orderInvoice.InvoiceBala;
                        docInvoice.OrderId = order.OrderId;
                        docInvoice.OrderInvoiceDetailId = orderInvoice.DetailId;
                        docInvoice.StockDetailId = addDocStock.DetailId;

                        result = docInvoiceDAL.Insert(user, docInvoice);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (isSubmitAudit)
                    {
                        NFMT.WorkFlow.AutoSubmit submit = new WorkFlow.AutoSubmit();
                        NFMT.WorkFlow.ITaskProvider taskProvider = new TaskProvider.DocumentTaskProvider();
                        result = submit.Submit(user, doc, taskProvider, WorkFlow.MasterEnum.制单审核);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = doc;

                    scope.Complete();
                }

            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public SelectModel GetDocumnetStocksSelect(int pageIndex, int pageSize, string orderStr, int documentId = 0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ds.DetailId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "ds.DetailId as DocStockId,dos.DetailId,dos.OrderId,isnull(sto.StockId,0) as StockId,isnull(sn.RefNo,dos.RefNo) as RefNo,sto.CurNetAmount as LastAmount,sto.UintId,sto.Bundles,sto.StockStatus,sd.StatusName,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,mu.MUName,dos.ApplyAmount as ApplyWeight,di.InvoiceNo,di.InvoiceBala,sto.CurGrossAmount ";

            int statusId = (int)Common.StatusTypeEnum.库存状态;
            int readyStatus = (int)Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" NFMT.dbo.Doc_DocumentStock ds ");
            sb.AppendFormat(" inner join dbo.Doc_DocumentOrderStock dos on ds.OrderStockDetailId = dos.DetailId and dos.DetailStatus >={0} ",readyStatus);
            sb.AppendFormat(" inner join dbo.Doc_DocumentInvoice di on ds.DetailId = di.StockDetailId and di.DetailStatus>={0} ", readyStatus);
            sb.Append(" left join dbo.St_Stock sto on sto.StockId = dos.StockId ");
            sb.Append(" left join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = sto.StockStatus and sd.StatusId ={0} ", statusId);
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");            
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" ds.DetailStatus >={0} and ds.DocumentId = {1}  ", readyStatus, documentId);
          

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Update(UserModel user, Model.Document doc, List<Model.DocumentStock> docStocks)
        {
            ResultModel result = new ResultModel();

            try 
            {
                DAL.DocumentOrderDAL orderDAL = new DocumentOrderDAL();
                DAL.DocumentOrderStockDAL orderStockDAL = new DocumentOrderStockDAL();
                DAL.DocumentOrderInvoiceDAL orderInvoiceDAL = new DocumentOrderInvoiceDAL();
                DAL.DocumentStockDAL docStockDAL = new DocumentStockDAL();
                DAL.DocumentInvoiceDAL docInvoiceDAL = new DocumentInvoiceDAL();
                Invoice.DAL.BusinessInvoiceDAL invoiceDAL = new Invoice.DAL.BusinessInvoiceDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    if (doc == null || doc.DocumentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单信息错误";
                        return result;
                    }

                    if (docStocks == null || docStocks.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "未选择任何制单库存";
                        return result;
                    }

                    //获取制单
                    result = this.documentDAL.Get(user, doc.DocumentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Document resultDoc = result.ReturnValue as Model.Document;
                    if (resultDoc == null || resultDoc.DocumentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单不存在";
                        return result;
                    }

                    //获取指令
                    result = orderDAL.Get(user, resultDoc.OrderId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.DocumentOrder order = result.ReturnValue as Model.DocumentOrder;
                    if (order == null || order.OrderId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单指令不存在";
                        return result;
                    }

                    //判断指令状态
                    if (order.OrderStatus != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单指令非已生效状态，不能进行制单";
                        return result;
                    }

                    //新增制单                   
                    resultDoc.DocEmpId = user.EmpId;
                    resultDoc.DocumentDate = doc.DocumentDate;
                    resultDoc.Meno = doc.Meno;
                    result = this.documentDAL.Update(user, resultDoc);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取当前指令所有库存明细
                    result = orderStockDAL.Load(user, order.OrderId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.DocumentOrderStock> orderStocks = result.ReturnValue as List<Model.DocumentOrderStock>;
                    if (orderStocks == null || orderStocks.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单指令库存明细获取失败";
                        return result;
                    }

                    //获取当前制单所有库存明细与发票明细并作废
                    result = docStockDAL.Load(user, resultDoc.DocumentId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.DocumentStock> resultDocStocks = result.ReturnValue as List<Model.DocumentStock>;
                    if (resultDocStocks == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单库存明细获取失败";
                        return result;
                    }

                    foreach (Model.DocumentStock docStock in resultDocStocks)
                    {
                        result = docStockDAL.Invalid(user, docStock);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    result = docInvoiceDAL.Load(user,resultDoc.DocumentId);
                    if(result.ResultStatus!=0)
                        return result;

                    List<Model.DocumentInvoice> resultDocInvoices = result.ReturnValue as List<Model.DocumentInvoice>;
                    if (resultDocInvoices == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单发票明细获取失败";
                        return result;
                    }

                    foreach (Model.DocumentInvoice docInvoice in resultDocInvoices)
                    {
                        result = docInvoiceDAL.Invalid(user, docInvoice);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //明细新增
                    foreach (Model.DocumentStock docStock in docStocks)
                    {
                        //验证选择明细是否在指令中
                        Model.DocumentOrderStock orderStock = orderStocks.FirstOrDefault(temp => temp.DetailId == docStock.DetailId);
                        if (orderStock == null || orderStock.DetailId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "制单指令库存不存在";
                            return result;
                        }

                        //新增制单库存明细
                        Model.DocumentStock addDocStock = new DocumentStock();
                        addDocStock.DetailStatus = StatusEnum.已生效;                        
                        addDocStock.OrderId = resultDoc.OrderId;
                        addDocStock.OrderStockDetailId = orderStock.DetailId;
                        addDocStock.RefNo = orderStock.RefNo;
                        addDocStock.StockId = orderStock.StockId;
                        addDocStock.StockNameId = orderStock.StockNameId;
                        addDocStock.DocumentId = resultDoc.DocumentId;

                        result = docStockDAL.Insert(user, addDocStock);
                        if (result.ResultStatus != 0)
                            return result;

                        int docStockDetailId = 0;
                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out docStockDetailId) || docStockDetailId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "制单库存明细新增失败";
                            return result;
                        }

                        addDocStock.DetailId = docStockDetailId;

                        //获取指令发票明细
                        result = orderInvoiceDAL.GetByStockDetailId(user, orderStock.DetailId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.DocumentOrderInvoice orderInvoice = result.ReturnValue as Model.DocumentOrderInvoice;
                        if (orderInvoice == null || orderInvoice.DetailId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "制单指令发票明细不存在";
                            return result;
                        }

                        Model.DocumentInvoice docInvoice = new DocumentInvoice();
                        docInvoice.DetailStatus = StatusEnum.已生效;
                        docInvoice.DocumentId = resultDoc.DocumentId;
                        docInvoice.InvoiceNo = orderInvoice.InvoiceNo;
                        docInvoice.InvoiceBala = orderInvoice.InvoiceBala;
                        docInvoice.OrderId = resultDoc.OrderId;
                        docInvoice.OrderInvoiceDetailId = orderInvoice.DetailId;
                        docInvoice.StockDetailId = addDocStock.DetailId;

                        result = docInvoiceDAL.Insert(user, docInvoice);
                        if (result.ResultStatus != 0)
                            return result;

                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = resultDoc;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel Invalid(UserModel user, int documentId)
        {
            ResultModel result = new ResultModel();

            try 
            {
                //作废制单主表
                //作废制单库存明细
                //作废制单发票明细
                //作废出库执行
                //作废发票

                DAL.DocumentInvoiceDAL docInvoiceDAL = new DocumentInvoiceDAL();
                DAL.DocumentStockDAL docStockDAL = new DocumentStockDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //校验制单
                    result = this.documentDAL.Get(user, documentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Document doc = result.ReturnValue as Model.Document;
                    if (doc == null || doc.DocumentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单不存在";
                        return result;
                    }

                    //作废制单主表
                    result = this.documentDAL.Invalid(user, doc);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取所有库存明细
                    result = docStockDAL.Load(user, doc.DocumentId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.DocumentStock> docStocks = result.ReturnValue as List<Model.DocumentStock>;
                    if (docStocks == null) 
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单的库存明细获取失败";
                        return result;
                    }

                    foreach (Model.DocumentStock docStock in docStocks)
                    {
                        result = docStockDAL.Invalid(user, docStock);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取所有发票明细并作废
                    result = docInvoiceDAL.Load(user, doc.DocumentId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.DocumentInvoice> docInvoices = result.ReturnValue as List<Model.DocumentInvoice>;
                    if (docInvoices == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单的发票明细获取失败";
                        return result;
                    }

                    foreach (Model.DocumentInvoice docInvoice in docInvoices)
                    {
                        result = docInvoiceDAL.Invalid(user, docInvoice);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel GoBack(UserModel user, int documentId)
        {
            ResultModel result = new ResultModel();

            try
            {
                //撤返制单主表
                //取消工作流审核
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //校验制单
                    result = this.documentDAL.Get(user, documentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Document doc = result.ReturnValue as Model.Document;
                    if (doc == null || doc.DocumentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单不存在";
                        return result;
                    }

                    //作废制单主表
                    result = this.documentDAL.Goback(user, doc);
                    if (result.ResultStatus != 0)
                        return result;

                    //工作流任务关闭
                    WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, doc);
                    if (result.ResultStatus != 0)
                        return result;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 交单
        /// </summary>
        /// <param name="user"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public ResultModel Present(UserModel user, int documentId, DateTime presentDate)
        {
            ResultModel result = new ResultModel();

            try
            {
                //交单
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //校验制单
                    result = this.documentDAL.Get(user, documentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Document doc = result.ReturnValue as Model.Document;
                    if (doc == null || doc.DocumentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单不存在";
                        return result;
                    }

                    //更新制单
                    doc.PresentDate = presentDate;
                    doc.Presenter = user.EmpId;

                    result = this.documentDAL.Update(user, doc);
                    if (result.ResultStatus != 0)
                        return result;

                    result = this.documentDAL.Present(user, doc);
                    if (result.ResultStatus != 0)
                        return result;
                    
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel Acceptan(UserModel user, int documentId, DateTime acceptanceDate)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.DocumentStockDAL docStockDAL = new DocumentStockDAL();
                DAL.DocumentInvoiceDAL docInvoiceDAL = new DocumentInvoiceDAL();

                //承兑
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //校验制单
                    result = this.documentDAL.Get(user, documentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Document doc = result.ReturnValue as Model.Document;
                    if (doc == null || doc.DocumentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单不存在";
                        return result;
                    }

                    //更新制单
                    doc.AcceptanceDate = acceptanceDate;
                    doc.Acceptancer = user.EmpId;

                    result = this.documentDAL.Update(user, doc);
                    if (result.ResultStatus != 0)
                        return result;

                    result = this.documentDAL.Acceptan(user, doc);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取库存与发票明细，并设置状态完成
                    result = docStockDAL.Load(user, doc.DocumentId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.DocumentStock> docStocks = result.ReturnValue as List<Model.DocumentStock>;
                    if (docStocks != null)
                    {
                        foreach (Model.DocumentStock docStock in docStocks)
                        {
                            result = docStockDAL.Complete(user, docStock);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    result = docInvoiceDAL.Load(user, doc.DocumentId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.DocumentInvoice> docInvoices = result.ReturnValue as List<Model.DocumentInvoice>;
                    if (docInvoices != null)
                    {
                        foreach (Model.DocumentInvoice docInvoice in docInvoices)
                        {
                            result = docInvoiceDAL.Complete(user, docInvoice);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel BackDocument(UserModel user, int documentId)
        {
            ResultModel result = new ResultModel();

            try
            {
                //交单
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //校验制单
                    result = this.documentDAL.Get(user, documentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Document doc = result.ReturnValue as Model.Document;
                    if (doc == null || doc.DocumentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单不存在";
                        return result;
                    }

                    result = this.documentDAL.BackDocument(user, doc);
                    if (result.ResultStatus != 0)
                        return result;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        #endregion
    }
}
