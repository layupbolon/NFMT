/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentOrderBLL.cs
// 文件功能描述：制单指令dbo.Doc_DocumentOrder业务逻辑类。
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

namespace NFMT.Document.BLL
{
    /// <summary>
    /// 制单指令dbo.Doc_DocumentOrder业务逻辑类。
    /// </summary>
    public class DocumentOrderBLL : Common.ExecBLL
    {
        private DocumentOrderDAL documentorderDAL = new DocumentOrderDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(DocumentOrderDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DocumentOrderBLL()
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
            get { return this.documentorderDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, DateTime beginDate, DateTime endDate, string contractNo = "", int outerCorp = 0, int status = 0,bool isReady= false)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            select.OrderStr = string.IsNullOrEmpty(orderStr) ? "do.OrderId desc" : orderStr;
            int commonStatus = (int)NFMT.Common.StatusTypeEnum.通用状态;

            int readyStatus =(int) NFMT.Common.StatusEnum.已生效;

            select.ColumnName = "do.OrderId,do.OrderNo,do.OrderDate,do.ApplyCorp,appCorp.CorpName as ApplyCorpName,do.BuyerCorp,buyCorp.CorpName as BuyCorpName,do.SubId,cs.SubNo,con.AssetId,ass.AssetName,do.NetAmount,cs.UnitId,mu.MUName,do.OrderType,sd.DetailName as OrderTypeName,do.ApplyEmpId,emp.Name as EmpName,do.OrderStatus,os.StatusName,cdo.OrderType as CommercialOrderType ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(" dbo.Doc_DocumentOrder do ");
            sb.Append(" inner join dbo.Con_ContractSub cs on cs.SubId = do.SubId ");
            sb.Append(" inner join dbo.Con_Contract con on con.ContractId = do.ContractId ");
            sb.Append(" left join NFMT_User.dbo.Corporation appCorp on appCorp.CorpId = do.ApplyCorp ");
            sb.Append(" left join NFMT_User.dbo.Corporation buyCorp on buyCorp.CorpId = do.BuyerCorp ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = con.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = cs.UnitId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail sd on sd.StyleDetailId = do.OrderType ");
            sb.Append(" left join NFMT_User.dbo.Employee emp on emp.EmpId = do.ApplyEmpId   ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail  os on os.DetailId = do.OrderStatus and os.StatusId={0} ", commonStatus);

            sb.AppendFormat(" left join dbo.Doc_DocumentOrder cdo on do.CommercialId = cdo.OrderId and cdo.OrderStatus >= {0}", readyStatus);

            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" 1=1 ");
            
            if(!isReady)
                sb.AppendFormat(" and do.OrderType != {0}",(int)OrderTypeEnum.替临制单指令);

            if (beginDate > NFMT.Common.DefaultValue.DefaultTime && endDate > beginDate)
                sb.AppendFormat(" and do.OrderDate between '{0}' and '{1}' ", beginDate.ToShortDateString(), endDate.ToShortDateString());

            if (!string.IsNullOrEmpty(contractNo))
                sb.AppendFormat(" and con.ContractNo like '%{0}%' ", contractNo);

            if (outerCorp > 0)
                sb.AppendFormat(" and do.BuyerCorp = {0} ", outerCorp);

            if (status > 0)
                sb.AppendFormat(" and do.OrderStatus = {0} ", status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetContractListSelect(int pageIndex, int pageSize, string orderStr, string subNo, int outCorpId, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "soa.StockOutApplyId asc";
            else
                select.OrderStr = orderStr;

            int status = (int)NFMT.Common.StatusEnum.已生效;
            int statusType = (int)NFMT.Common.StatusTypeEnum.通用状态;

            int tradeDirection = (int)NFMT.Contract.TradeDirectionEnum.销售;
            int tradeBorder = (int)NFMT.Contract.TradeBorderEnum.外贸;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" con.ContractId,cs.SubId,cs.ContractDate,con.ContractNo,cs.SubNo,cast (cs.SignAmount as varchar) + mu.MUName as SignWeight,cast(isnull(crr.SumBala,0) as varchar) + cur.CurrencyName as AllotBala,cast(isnull(soad.SumWeight,0) as varchar)+mu.MUName as AllotWeigth, cast((cs.SignAmount -isnull(soad.SumWeight,0)) as varchar) + mu.MUName as LaveWeight,inccd.CorpName as InCorpName,outccd.CorpName as OutCorpName,a.AssetName,cs.SubStatus,sd.StatusName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Con_ContractSub cs ");
            sb.Append(" inner join dbo.Con_Contract con on con.ContractId = cs.ContractId ");
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail inccd on inccd.ContractId = con.ContractId and inccd.IsDefaultCorp = 1 and inccd.IsInnerCorp = 1 and inccd.DetailStatus= {0} ", status);
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail outccd on outccd.ContractId = con.ContractId and outccd.IsDefaultCorp = 1 and outccd.IsInnerCorp = 0 and outccd.DetailStatus={0} ", status);
            sb.Append(" left join NFMT_Basic.dbo.Asset a on con.AssetId = a.AssetId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on cs.SubStatus = sd.DetailId and sd.StatusId = {0} ", statusType);
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = cs.SettleCurrency ");

            sb.AppendFormat(" left join (select SubContractId,SUM(IsNull(NetAmount,0)) as SumWeight from dbo.St_StockOutApplyDetail where DetailStatus >={0} group by SubContractId) as soad on soad.SubContractId = cs.SubId ", status);

            sb.AppendFormat("left join(select SubContractId,SUM(isnull(AllotBala,0)) as SumBala from dbo.Fun_CashInContract_Ref where DetailStatus >= {0} group by SubContractId) as crr on crr.SubContractId = cs.SubId", status);

            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on cs.UnitId = mu.MUId ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" con.ContractStatus = {0} and cs.SubStatus = {0} and con.TradeDirection ={1} and con.TradeBorder = {2} ", status, tradeDirection, tradeBorder);

            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and cs.SubNo like '%{0}%'", subNo);
            if (outCorpId > 0)
                sb.AppendFormat("  and outccd.CorpId ={0}", outCorpId);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and cs.CreateTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays
                    (1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 可配货库存列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <returns></returns>
        public SelectModel GetOrderSalesStockSelect(int pageIndex, int pageSize, string orderStr, string sids = "", int contractId = 0, int orderId = 0, string dids = "", string refNo = "", int ownCorpId = 0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId asc";
            else
                select.OrderStr = orderStr;

            if (string.IsNullOrEmpty(dids))
                dids = "0";

            int readyStatus = (int)Common.StatusEnum.已生效;

            select.ColumnName = "sto.StockId,sn.RefNo,sto.GrossAmount,sto.NetAmount,sto.UintId,CAST(sto.GrossAmount as varchar) + mu.MUName as StockWeight,sto.CurNetAmount,sto.CurGrossAmount,mu.MUName,sto.StockStatus,sd.StatusName,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,dos.ApplyAmount,ISNULL(sto.CurNetAmount,0) - ISNULL(dos.ApplyAmount,0) as LastAmount,ISNULL(sto.CurNetAmount,0) - ISNULL(dos.ApplyAmount,0) as ApplyWeight,0 as InvoiceBala,'' as InvoiceNo,sto.Bundles ";

            int statusId = (int)Common.StatusTypeEnum.库存状态;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_Stock sto ");
            sb.AppendFormat(" inner join NFMT.dbo.Con_Contract con on con.AssetId = sto.AssetId and con.UnitId = sto.UintId and con.ContractId = {0} ", contractId);
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = sto.StockStatus and sd.StatusId ={0} ", statusId);
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            sb.AppendFormat("left join (select sum(ApplyAmount) as ApplyAmount,StockId from NFMT.dbo.Doc_DocumentOrderStock where DetailStatus ={0} and DetailId not in ({1}) group by StockId) as dos on dos.StockId = sto.StockId", readyStatus, dids);
            select.TableName = sb.ToString();

            sb.Clear();

            int planStockInStatus = (int)NFMT.WareHouse.StockStatusEnum.预入库存;
            int planCustomsStatus = (int)NFMT.WareHouse.StockStatusEnum.预报关库存;
            sb.AppendFormat(" sto.StockStatus between {0} and {1} ", planStockInStatus, planCustomsStatus);

            sb.AppendFormat(" and ISNULL(sto.CurNetAmount,0) - ISNULL(dos.ApplyAmount,0) > 0 ");
            if (orderId > 0)
                sb.AppendFormat(" and sto.StockId not in (select StockId from dbo.Doc_DocumentOrderStock where OrderId={0} and DetailStatus>={1} ) ", orderId, readyStatus);
            
            if (!string.IsNullOrEmpty(sids))
            {
                sids = sids.Trim();
                sb.AppendFormat(" and sto.StockId not in ({0})", sids);
            }

            if (!string.IsNullOrEmpty(refNo))
                sb.AppendFormat(" and sn.RefNo like '%{0}%'", refNo);

            if (ownCorpId > 0)
                sb.AppendFormat(" and sto.CorpId={0} ", ownCorpId);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Create(UserModel user, Model.DocumentOrder order, List<NFMT.Document.Model.OrderStockInvoice> stockInvoices, NFMT.Document.Model.DocumentOrderDetail detail, bool isSubmitAudit)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.DocumentOrderDetailDAL orderDetailDAL = new DocumentOrderDetailDAL();
                DAL.DocumentOrderStockDAL orderStockDAL = new DocumentOrderStockDAL();
                DAL.DocumentOrderInvoiceDAL orderInvoiceDAL = new DocumentOrderInvoiceDAL();
                NFMT.WareHouse.DAL.StockDAL stockDAL = new WareHouse.DAL.StockDAL();
                NFMT.WareHouse.DAL.StockNameDAL stockNameDAL = new WareHouse.DAL.StockNameDAL();
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
                NFMT.Contract.DAL.ContractDAL contractDAL = new Contract.DAL.ContractDAL();
                NFMT.Contract.DAL.ContractCorporationDetailDAL corpDetailDAL = new Contract.DAL.ContractCorporationDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    if (order == null)
                    {
                        result.Message = "制单指令不包含任何数据";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //验证合约
                    result = contractDAL.Get(user, order.ContractId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                    if (contract == null || contract.ContractId <= 0)
                    {
                        result.Message = "合约不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //验证子合约
                    result = subDAL.Get(user, order.SubId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.Message = "子合约不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    if (sub.SubStatus != StatusEnum.已生效)
                    {
                        result.Message = "子合约非已生效状态，不能进行制单指令新增";
                        result.ResultStatus = -1;
                        return result;
                    }

                    if (sub.ContractId != order.ContractId)
                    {
                        result.Message = "子合约归属主合约与制单指令主合约不一致";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = corpDetailDAL.LoadCorpListByContractId(user, sub.ContractId, true);
                    if (result.ResultStatus != 0)
                        return result;

                    List<NFMT.Contract.Model.ContractCorporationDetail> corps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
                    if (corps == null || corps.Count == 0)
                    {
                        result.Message = "合约我方抬头获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    int orderId = 0;

                    //制单指令主表数据初始化
                    //付款方式默认为LC
                    order.PaymentStyle = 19;
                    //卖方默认为合约我方抬头默认公司
                    order.SellerCorp = corps[0].CorpId;
                    //临票制单或直接终票制单，临终发票差额为0
                    order.InvGap = 0;
                    //LC未加入时LC序号为0
                    order.LCId = 0;
                    order.ApplyEmpId = user.EmpId;

                    //新增主表
                    order.OrderStatus = StatusEnum.已录入;
                    result = this.documentorderDAL.Insert(user, order);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out orderId) || orderId <= 0)
                    {
                        result.Message = "制单指令新增失败";
                        result.ResultStatus = -1;
                        return result;
                    }
                    order.OrderId = orderId;

                    //新增明细表
                    detail.OrderId = orderId;
                    detail.DetailStatus = StatusEnum.已生效;
                    result = orderDetailDAL.Insert(user, detail);
                    if (result.ResultStatus != 0)
                        return result;

                    OrderTypeEnum orderType = (OrderTypeEnum)order.OrderType;

                    //新增库存明细与发票明细
                    foreach (Model.OrderStockInvoice stockInvoice in stockInvoices)
                    {
                        //添加库存明细
                        Model.DocumentOrderStock orderStock = new DocumentOrderStock();
                        orderStock.DetailStatus = StatusEnum.已生效;
                        orderStock.OrderId = orderId;
                        orderStock.ApplyAmount = stockInvoice.ApplyWeight;
                        int orderStockId = 0;

                        //库存分配制单指令校验库存信息
                        if (orderType == OrderTypeEnum.临票制单指令 || orderType == OrderTypeEnum.终票制单指令)
                        {
                            //验证库存
                            result = stockDAL.Get(user, stockInvoice.StockId);
                            if (result.ResultStatus != 0)
                                return result;
                            NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                            if (stock == null || stock.StockId <= 0)
                            {
                                result.Message = "制单指令分配库存不存在";
                                result.ResultStatus = -1;
                                return result;
                            }

                            if (stock.AssetId != contract.AssetId)
                            {
                                result.Message = "合约品种与库存不匹配";
                                result.ResultStatus = -1;
                                return result;
                            }

                            if (stock.CurNetAmount < stockInvoice.ApplyWeight)
                            {
                                result.Message = "申请重量不能大于库存剩余净重";
                                result.ResultStatus = -1;
                                return result;
                            }

                            //获取业务单号
                            result = stockNameDAL.Get(user, stock.StockNameId);
                            if (result.ResultStatus != 0)
                                return result;

                            NFMT.WareHouse.Model.StockName stockName = result.ReturnValue as NFMT.WareHouse.Model.StockName;
                            if (stockName == null || stockName.StockNameId <= 0)
                            {
                                result.Message = "业务单号不存在";
                                result.ResultStatus = -1;
                                return result;
                            }

                            orderStock.StockId = stock.StockId;
                            orderStock.StockNameId = stock.StockNameId;
                            orderStock.RefNo = stockName.RefNo;                            
                        }
                        else
                        {
                            //无库存配货权录入业务单号
                            orderStock.RefNo = stockInvoice.RefNo;
                        }

                        result = orderStockDAL.Insert(user, orderStock);
                        if (result.ResultStatus != 0)
                            return result;

                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out orderStockId) || orderStockId <= 0)
                        {
                            result.Message = "制单指令库存明细新增失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //添加发票明细
                        Model.DocumentOrderInvoice orderInvoice = new DocumentOrderInvoice();
                        orderInvoice.DetailStatus = StatusEnum.已生效;
                        orderInvoice.InvoiceBala = stockInvoice.InvoiceBala;
                        orderInvoice.InvoiceNo = stockInvoice.InvoiceNo;
                        orderInvoice.OrderId = orderId;
                        orderInvoice.StockDetailId = orderStockId;

                        result = orderInvoiceDAL.Insert(user, orderInvoice);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (isSubmitAudit)
                    {
                        NFMT.WorkFlow.AutoSubmit submit = new WorkFlow.AutoSubmit();
                        NFMT.WorkFlow.ITaskProvider taskProvider = new TaskProvider.OrderTaskProvider();
                        result = submit.Submit(user, order, taskProvider, WorkFlow.MasterEnum.制单指令审核);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = order;

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

        public SelectModel GetReplaceOrdersModel(int pageIndex, int pageSize, string orderStr, DateTime beginDate, DateTime endDate, string contractNo = "", int outerCorp = 0, int status = 0)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            select.OrderStr = string.IsNullOrEmpty(orderStr) ? "do.OrderId desc" : orderStr;
            int commonStatus = (int)NFMT.Common.StatusTypeEnum.通用状态;

            select.ColumnName = "do.OrderId,do.OrderNo,do.OrderDate,do.ApplyCorp,appCorp.CorpName as ApplyCorpName,do.BuyerCorp,buyCorp.CorpName as BuyCorpName,do.SubId,cs.SubNo,con.AssetId,ass.AssetName,do.NetAmount,cs.UnitId,mu.MUName,do.OrderType,sd.DetailName as OrderTypeName,do.ApplyEmpId,emp.Name as EmpName,do.OrderStatus,os.StatusName ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(" dbo.Doc_DocumentOrder do ");
            sb.Append(" inner join dbo.Con_ContractSub cs on cs.SubId = do.SubId ");
            sb.Append(" inner join dbo.Con_Contract con on con.ContractId = do.ContractId ");
            sb.Append(" left join NFMT_User.dbo.Corporation appCorp on appCorp.CorpId = do.ApplyCorp ");
            sb.Append(" left join NFMT_User.dbo.Corporation buyCorp on buyCorp.CorpId = do.BuyerCorp ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = con.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = cs.UnitId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail sd on sd.StyleDetailId = do.OrderType ");
            sb.Append(" left join NFMT_User.dbo.Employee emp on emp.EmpId = do.ApplyEmpId   ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail  os on os.DetailId = do.OrderStatus and os.StatusId={0} ", commonStatus);

            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" do.OrderType = {0} ",(int)OrderTypeEnum.替临制单指令);

            if (beginDate > NFMT.Common.DefaultValue.DefaultTime && endDate > beginDate)
                sb.AppendFormat(" and do.OrderDate between '{0}' and '{1}' ", beginDate.ToShortDateString(), endDate.ToShortDateString());

            if (!string.IsNullOrEmpty(contractNo))
                sb.AppendFormat(" and con.ContractNo like '%{0}%' ", contractNo);

            if (outerCorp > 0)
                sb.AppendFormat(" and do.BuyerCorp = {0} ", outerCorp);

            if (status > 0)
                sb.AppendFormat(" and do.OrderStatus = {0} ", status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetCommercialOrdersModel(int pageIndex, int pageSize, string orderStr, DateTime beginDate, DateTime endDate, string contractNo = "", int outerCorp = 0, int status = 0)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            select.OrderStr = string.IsNullOrEmpty(orderStr) ? "do.OrderId desc" : orderStr;
            int commonStatus = (int)NFMT.Common.StatusTypeEnum.通用状态;
            int readyStatus =(int)StatusEnum.已生效;
            int finnalStatus =(int)StatusEnum.已完成;

            select.ColumnName = "do.OrderId,do.OrderNo,do.OrderDate,do.ApplyCorp,appCorp.CorpName as ApplyCorpName,do.BuyerCorp,buyCorp.CorpName as BuyCorpName,do.SubId,cs.SubNo,con.AssetId,ass.AssetName,do.NetAmount,cs.UnitId,mu.MUName,do.OrderType,sd.DetailName as OrderTypeName,do.ApplyEmpId,emp.Name as EmpName,do.OrderStatus,os.StatusName ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(" dbo.Doc_DocumentOrder do ");
            sb.Append(" inner join dbo.Con_ContractSub cs on cs.SubId = do.SubId ");
            sb.Append(" inner join dbo.Con_Contract con on con.ContractId = do.ContractId ");
            sb.Append(" left join NFMT_User.dbo.Corporation appCorp on appCorp.CorpId = do.ApplyCorp ");
            sb.Append(" left join NFMT_User.dbo.Corporation buyCorp on buyCorp.CorpId = do.BuyerCorp ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = con.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = cs.UnitId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail sd on sd.StyleDetailId = do.OrderType ");
            sb.Append(" left join NFMT_User.dbo.Employee emp on emp.EmpId = do.ApplyEmpId   ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail  os on os.DetailId = do.OrderStatus and os.StatusId={0} ", commonStatus);

            sb.AppendFormat(" left join (select COUNT(*) as OrderStockCount,OrderId from dbo.Doc_DocumentOrderStock where DetailStatus ={0} group by OrderId) as dos on dos.OrderId = do.OrderId ",readyStatus);

            sb.AppendFormat(" left join (select COUNT(*) as DocStockCount,OrderId from dbo.Doc_DocumentStock where DetailStatus = {0} group by OrderId) as ds on ds.OrderId = do.OrderId ",finnalStatus);

            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" do.OrderType in ({0},{1}) ", (int)OrderTypeEnum.临票制单指令,(int)OrderTypeEnum.无配货临票制单指令);
            sb.Append(" and dos.OrderStockCount = ds.DocStockCount ");

            if (beginDate > NFMT.Common.DefaultValue.DefaultTime && endDate > beginDate)
                sb.AppendFormat(" and do.OrderDate between '{0}' and '{1}' ", beginDate.ToShortDateString(), endDate.ToShortDateString());

            if (!string.IsNullOrEmpty(contractNo))
                sb.AppendFormat(" and con.ContractNo like '%{0}%' ", contractNo);

            if (outerCorp > 0)
                sb.AppendFormat(" and do.BuyerCorp = {0} ", outerCorp);

            if (status > 0)
                sb.AppendFormat(" and do.OrderStatus = {0} ", status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetOrderSelectedSelect(int pageIndex, int pageSize, string orderStr, int orderId = 0,bool isDoc = false)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "dos.DetailId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "dos.DetailId,dos.OrderId,isnull(sto.StockId,0) as StockId,isnull(sn.RefNo,dos.RefNo) as RefNo,sto.CurNetAmount as LastAmount,sto.UintId,sto.Bundles,sto.StockStatus,sd.StatusName,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,mu.MUName,dos.ApplyAmount as ApplyWeight,doi.InvoiceNo,doi.InvoiceBala,sto.CurGrossAmount ";

            int statusId = (int)Common.StatusTypeEnum.库存状态;
            int readyStatus = (int)Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.Doc_DocumentOrderStock dos ");
            sb.AppendFormat(" inner join dbo.Doc_DocumentOrderInvoice doi on dos.DetailId = doi.StockDetailId and doi.DetailStatus>={0} ",readyStatus);
            sb.Append(" left join dbo.St_Stock sto on sto.StockId = dos.StockId ");
            sb.Append(" left join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = sto.StockStatus and sd.StatusId ={0} ",statusId);
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId "); 
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            if(isDoc)
                sb.AppendFormat(" left join NFMT.dbo.Doc_DocumentStock ds on ds.OrderStockDetailId = dos.DetailId and ds.DetailStatus >={0}",readyStatus);
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" dos.DetailStatus >={0} and dos.OrderId={1} ", readyStatus, orderId);

            if (isDoc)
                sb.Append(" and ds.OrderStockDetailId is null ");

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetNoStockOrderSelect(int pageIndex, int pageSize, string orderStr, int orderId = 0, bool isDoc = false)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "dos.DetailId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "dos.DetailId,dos.OrderId,isnull(sto.StockId,0) as StockId,dos.RefNo,sto.CurNetAmount as LastAmount,sto.UintId,sto.Bundles,sto.StockStatus,sd.StatusName,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,mu.MUName,dos.ApplyAmount,dos.ApplyAmount as ApplyWeight,doi.InvoiceNo,doi.InvoiceBala,sto.CurGrossAmount ";

            int statusId = (int)Common.StatusTypeEnum.库存状态;
            int readyStatus = (int)Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.Doc_DocumentOrderStock dos ");
            sb.AppendFormat(" inner join dbo.Doc_DocumentOrderInvoice doi on dos.DetailId = doi.StockDetailId and doi.DetailStatus>={0} ", readyStatus);
            sb.Append(" left join dbo.St_Stock sto on sto.StockId = dos.StockId ");
            sb.Append(" left join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = sto.StockStatus and sd.StatusId ={0} ", statusId);
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            if (isDoc)
                sb.AppendFormat(" left join NFMT.dbo.Doc_DocumentStock ds on ds.OrderStockDetailId = dos.DetailId and ds.DetailStatus >={0}", readyStatus);
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" dos.DetailStatus >={0} and dos.OrderId={1} ", readyStatus, orderId);

            if (isDoc)
                sb.Append(" and ds.OrderStockDetailId is null ");

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Update(UserModel user, Model.DocumentOrder order, List<NFMT.Document.Model.OrderStockInvoice> stockInvoices, NFMT.Document.Model.DocumentOrderDetail detail)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.DocumentOrderDetailDAL orderDetailDAL = new DocumentOrderDetailDAL();
                DAL.DocumentOrderStockDAL orderStockDAL = new DocumentOrderStockDAL();
                DAL.DocumentOrderInvoiceDAL orderInvoiceDAL = new DocumentOrderInvoiceDAL();
                NFMT.WareHouse.DAL.StockDAL stockDAL = new WareHouse.DAL.StockDAL();
                NFMT.WareHouse.DAL.StockNameDAL stockNameDAL = new WareHouse.DAL.StockNameDAL();
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
                NFMT.Contract.DAL.ContractDAL contractDAL = new Contract.DAL.ContractDAL();
                NFMT.Contract.DAL.ContractCorporationDetailDAL corpDetailDAL = new Contract.DAL.ContractCorporationDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    if (order == null)
                    {
                        result.Message = "制单指令不包含任何数据";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //验证制单指令
                    result = this.documentorderDAL.Get(user, order.OrderId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.DocumentOrder resultOrder = result.ReturnValue as Model.DocumentOrder;
                    if (resultOrder == null || resultOrder.OrderId <= 0)
                    {
                        result.Message = "制单指令不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    resultOrder.ContractNo = order.ContractNo;
                    resultOrder.LCNo = order.LCNo;
                    resultOrder.LCDay = order.LCDay;
                    resultOrder.OrderType = order.OrderType;
                    resultOrder.OrderDate = order.OrderDate;
                    resultOrder.ApplyCorp = order.ApplyCorp;
                    resultOrder.ApplyDept = order.ApplyDept;
                    resultOrder.BuyerCorp = order.BuyerCorp;
                    resultOrder.BuyerCorpName = order.BuyerCorpName;
                    resultOrder.BuyerAddress = order.BuyerAddress;
                    resultOrder.RecBankId = order.RecBankId;
                    resultOrder.DiscountBase = order.DiscountBase;
                    resultOrder.AssetId = order.AssetId;
                    resultOrder.BrandId = order.BrandId;
                    resultOrder.AreaName = order.AreaName;
                    resultOrder.BankCode = order.BankCode;
                    resultOrder.GrossAmount = order.GrossAmount;
                    resultOrder.NetAmount = order.NetAmount;
                    resultOrder.UnitId = order.UnitId;
                    resultOrder.Bundles = order.Bundles;
                    resultOrder.Currency = order.Currency;
                    resultOrder.UnitPrice = order.UnitPrice;
                    resultOrder.InvBala = order.InvBala;
                    resultOrder.Meno = order.Meno;

                    //更新主表
                    result = this.documentorderDAL.Update(user, resultOrder);
                    if (result.ResultStatus != 0)
                        return result;                    

                    //获取明细表
                    result = orderDetailDAL.GetByOrderId(user, order.OrderId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.DocumentOrderDetail resultDetail = result.ReturnValue as Model.DocumentOrderDetail;
                    if (resultDetail == null || resultDetail.DetailId <= 0)
                    {
                        result.Message = "制单指令明细获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    resultDetail.InvoiceCopies = detail.InvoiceCopies;
                    resultDetail.InvoiceSpecific = detail.InvoiceSpecific;
                    resultDetail.QualityCopies = detail.QualityCopies;
                    resultDetail.QualitySpecific = detail.QualitySpecific;
                    resultDetail.WeightCopies = detail.WeightCopies;
                    resultDetail.WeightSpecific = detail.WeightSpecific;
                    resultDetail.TexCopies = detail.TexCopies;
                    resultDetail.TexSpecific = detail.TexSpecific;
                    resultDetail.DeliverCopies = detail.DeliverCopies;
                    resultDetail.DeliverSpecific = detail.DeliverSpecific;
                    resultDetail.TotalInvCopies = detail.TotalInvCopies;
                    resultDetail.TotalInvSpecific = detail.TotalInvSpecific;

                    result = orderDetailDAL.Update(user, resultDetail);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废库存明细与发票明细
                    result = orderInvoiceDAL.Load(user, order.OrderId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.DocumentOrderInvoice> orderInvoices = result.ReturnValue as List<Model.DocumentOrderInvoice>;
                    if (orderInvoices == null) 
                    {
                        result.Message = "制单指令发票明细获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }
                    foreach (Model.DocumentOrderInvoice orderInvoice in orderInvoices)
                    {
                        result = orderInvoiceDAL.Invalid(user, orderInvoice);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    result = orderStockDAL.Load(user,order.OrderId);
                    if(result.ResultStatus!=0)
                        return result;
                    List<Model.DocumentOrderStock> orderStocks = result.ReturnValue as List<Model.DocumentOrderStock>;
                    if (orderStocks == null)
                    {
                        result.Message = "制单指令库存明细获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    foreach (Model.DocumentOrderStock orderStock in orderStocks)
                    {
                        result = orderStockDAL.Invalid(user, orderStock);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    OrderTypeEnum orderType = (OrderTypeEnum)order.OrderType;

                    //新增库存明细与发票明细
                    foreach (Model.OrderStockInvoice stockInvoice in stockInvoices)
                    {
                        Model.DocumentOrderStock orderStock = new DocumentOrderStock();
                        orderStock.DetailStatus = StatusEnum.已生效;
                        orderStock.OrderId = order.OrderId;                        
                        orderStock.ApplyAmount = stockInvoice.ApplyWeight;

                        //库存分配制单指令校验库存信息
                        if (orderType == OrderTypeEnum.临票制单指令 || orderType == OrderTypeEnum.替临制单指令)
                        {
                            //验证库存
                            result = stockDAL.Get(user, stockInvoice.StockId);
                            if (result.ResultStatus != 0)
                                return result;
                            NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                            if (stock == null || stock.StockId <= 0)
                            {
                                result.Message = "制单指令分配库存不存在";
                                result.ResultStatus = -1;
                                return result;
                            }

                            if (stock.AssetId != order.AssetId)
                            {
                                result.Message = "库存品种不匹配";
                                result.ResultStatus = -1;
                                return result;
                            }

                            if (stock.CurNetAmount < stockInvoice.ApplyWeight)
                            {
                                result.Message = "申请重量不能大于库存剩余净重";
                                result.ResultStatus = -1;
                                return result;
                            }

                            //获取业务单号
                            result = stockNameDAL.Get(user, stock.StockNameId);
                            if (result.ResultStatus != 0)
                                return result;

                            NFMT.WareHouse.Model.StockName stockName = result.ReturnValue as NFMT.WareHouse.Model.StockName;
                            if (stockName == null || stockName.StockNameId <= 0)
                            {
                                result.Message = "业务单号不存在";
                                result.ResultStatus = -1;
                                return result;
                            }

                            orderStock.StockId = stock.StockId;
                            orderStock.StockNameId = stock.StockNameId;
                            orderStock.RefNo = stockName.RefNo;
                        }
                        else
                        {
                            //无库存配货权录入业务单号
                            orderStock.RefNo = stockInvoice.RefNo;
                        }

                        //添加库存明细                        
                        int orderStockId = 0;

                        result = orderStockDAL.Insert(user, orderStock);
                        if (result.ResultStatus != 0)
                            return result;

                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out orderStockId) || orderStockId <= 0)
                        {
                            result.Message = "制单指令库存明细新增失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //添加发票明细
                        Model.DocumentOrderInvoice orderInvoice = new DocumentOrderInvoice();
                        orderInvoice.DetailStatus = StatusEnum.已生效;
                        orderInvoice.InvoiceBala = stockInvoice.InvoiceBala;
                        orderInvoice.InvoiceNo = stockInvoice.InvoiceNo;
                        orderInvoice.OrderId = order.OrderId;
                        orderInvoice.StockDetailId = orderStockId;

                        result = orderInvoiceDAL.Insert(user, orderInvoice);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = order;

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

        public ResultModel Invalid(UserModel user, int orderId)
        {
            ResultModel result = new ResultModel();

            try
            {
                //作废制单指令
                //作废制单指令明细
                //作废制单指令库存明细
                //作废制单指令发票明细
                //作废制单指令附件
                DAL.DocumentOrderDetailDAL detailDAL = new DocumentOrderDetailDAL();
                DAL.DocumentOrderStockDAL stockDAL = new DocumentOrderStockDAL();
                DAL.DocumentOrderInvoiceDAL invoiceDAL = new DocumentOrderInvoiceDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //验证制单指令
                    result = this.documentorderDAL.Get(user, orderId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.DocumentOrder order = result.ReturnValue as Model.DocumentOrder;
                    if (order == null || order.OrderId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单指令不存在";
                        return result;
                    }

                    //作废制单指令
                    result = this.documentorderDAL.Invalid(user, order);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取明细
                    result = detailDAL.GetByOrderId(user, order.OrderId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.DocumentOrderDetail detail = result.ReturnValue as Model.DocumentOrderDetail;
                    if (detail == null || detail.DetailId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单指令明细获取失败";
                        return result;
                    }

                    //作废明细
                    result = detailDAL.Invalid(user, detail);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取库存明细
                    result = stockDAL.Load(user, order.OrderId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.DocumentOrderStock> orderStocks = result.ReturnValue as List<Model.DocumentOrderStock>;
                    if (orderStocks == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单指令库存明细获取失败";
                        return result;
                    }

                    //作废库存明细
                    foreach (Model.DocumentOrderStock orderStock in orderStocks)
                    {
                        result = stockDAL.Invalid(user, orderStock);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取发票明细
                    result = invoiceDAL.Load(user, order.OrderId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.DocumentOrderInvoice> orderInvoices = result.ReturnValue as List<Model.DocumentOrderInvoice>;
                    if (orderInvoices == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "制单指令发票明细获取失败";
                        return result;
                    }

                    //作废发票明细
                    foreach (Model.DocumentOrderInvoice orderInvoice in orderInvoices)
                    {
                        result = invoiceDAL.Invalid(user, orderInvoice);
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

        public ResultModel GoBack(UserModel user, int orderId)
        {
            ResultModel result = new ResultModel();

            try 
            {
                //撤返制单指令
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    result = this.documentorderDAL.Get(user, orderId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.DocumentOrder order = result.ReturnValue as Model.DocumentOrder;
                    if (order == null || order.OrderId <= 0)
                    {
                        result.Message = "制单指令不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = this.documentorderDAL.Goback(user, order);
                    if (result.ResultStatus != 0)
                        return result;

                    //工作流任务关闭
                    WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, order);
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

        public ResultModel Confirm(UserModel user, int orderId)
        {
            ResultModel result = new ResultModel();

            try 
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    result = this.documentorderDAL.Get(user, orderId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.DocumentOrder order = result.ReturnValue as Model.DocumentOrder;
                    if (order == null || order.OrderId <= 0)
                    {
                        result.Message = "制单指令不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = this.documentorderDAL.Confirm(user, order);
                    if (result.ResultStatus != 0)
                        return result;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel ConfirmCancel(UserModel user, int orderId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    result = this.documentorderDAL.Get(user, orderId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.DocumentOrder order = result.ReturnValue as Model.DocumentOrder;
                    if (order == null || order.OrderId <= 0)
                    {
                        result.Message = "制单指令不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = this.documentorderDAL.ConfirmCancel(user, order);
                    if (result.ResultStatus != 0)
                        return result;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public SelectModel GetComOrderStocksSelect(int pageIndex, int pageSize, string orderStr, int orderId = 0, bool isDoc = false)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "dos.DetailId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "dos.DetailId,dos.OrderId,isnull(sto.StockId,0) as StockId,isnull(sn.RefNo,dos.RefNo) as RefNo,sto.CurNetAmount as LastAmount,sto.UintId,sto.Bundles,sto.StockStatus,sd.StatusName,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,mu.MUName,dos.ApplyAmount as ApplyWeight,doi.InvoiceNo,doi.InvoiceBala,sto.CurGrossAmount ";

            int statusId = (int)Common.StatusTypeEnum.库存状态;
            int readyStatus = (int)Common.StatusEnum.已生效;
            int entryStatus = (int)Common.StatusEnum.已录入;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.Doc_DocumentOrderStock dos ");
            sb.AppendFormat(" inner join dbo.Doc_DocumentOrderInvoice doi on dos.DetailId = doi.StockDetailId and doi.DetailStatus>={0} ", readyStatus);
            sb.Append(" left join dbo.St_Stock sto on sto.StockId = dos.StockId ");
            sb.Append(" left join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = sto.StockStatus and sd.StatusId ={0} ", statusId);
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            //if (isDoc)
                sb.AppendFormat(" left join (select dos1.ComDetailId from dbo.Doc_DocumentOrder do1 inner join dbo.Doc_DocumentOrderStock dos1 on do1.OrderId = dos1.OrderId where do1.OrderStatus>={0} and dos1.DetailStatus>={1}) as do on do.ComDetailId = dos.DetailId",entryStatus, readyStatus);
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" dos.DetailStatus >={0} and dos.OrderId={1} ", readyStatus, orderId);

            //if (isDoc)
            sb.Append(" and do.ComDetailId is null ");

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel CreateReplaceOrder(UserModel user, Model.DocumentOrder order, List<NFMT.Document.Model.OrderReplaceStock> stockInvoices, NFMT.Document.Model.DocumentOrderDetail detail, bool isSubmitAudit)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.DocumentOrderDetailDAL orderDetailDAL = new DocumentOrderDetailDAL();
                DAL.DocumentOrderStockDAL orderStockDAL = new DocumentOrderStockDAL();
                DAL.DocumentOrderInvoiceDAL orderInvoiceDAL = new DocumentOrderInvoiceDAL();
                NFMT.WareHouse.DAL.StockDAL stockDAL = new WareHouse.DAL.StockDAL();
                NFMT.WareHouse.DAL.StockNameDAL stockNameDAL = new WareHouse.DAL.StockNameDAL();
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
                NFMT.Contract.DAL.ContractDAL contractDAL = new Contract.DAL.ContractDAL();
                NFMT.Contract.DAL.ContractCorporationDetailDAL corpDetailDAL = new Contract.DAL.ContractCorporationDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    if (order == null)
                    {
                        result.Message = "制单指令不包含任何数据";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //验证临票指令
                    result = this.documentorderDAL.Get(user, order.CommercialId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.DocumentOrder commercialOrder = result.ReturnValue as Model.DocumentOrder;
                    if (commercialOrder == null || commercialOrder.OrderId <= 0)
                    {
                        result.Message = "临票制单指令不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    if (commercialOrder.OrderStatus != StatusEnum.已生效)
                    {
                        result.Message = "临票制单指令状态不正确，不能替临";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //验证合约
                    result = contractDAL.Get(user, order.ContractId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                    if (contract == null || contract.ContractId <= 0)
                    {
                        result.Message = "合约不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //验证子合约
                    result = subDAL.Get(user, order.SubId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.Message = "子合约不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    if (sub.SubStatus != StatusEnum.已生效)
                    {
                        result.Message = "子合约非已生效状态，不能进行制单指令新增";
                        result.ResultStatus = -1;
                        return result;
                    }

                    if (sub.ContractId != order.ContractId)
                    {
                        result.Message = "子合约归属主合约与制单指令主合约不一致";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = corpDetailDAL.LoadCorpListByContractId(user, sub.ContractId, true);
                    if (result.ResultStatus != 0)
                        return result;

                    List<NFMT.Contract.Model.ContractCorporationDetail> corps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
                    if (corps == null || corps.Count == 0)
                    {
                        result.Message = "合约我方抬头获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    int orderId = 0;

                    //制单指令主表数据初始化
                    //付款方式默认为LC
                    order.PaymentStyle = 19;
                    //卖方默认为合约我方抬头默认公司
                    order.SellerCorp = corps[0].CorpId;
                    ////临票制单或直接终票制单，临终发票差额为0
                    //order.InvGap = 0;
                    //LC未加入时LC序号为0
                    order.LCId = 0;
                    order.ApplyEmpId = user.EmpId;

                    //新增主表
                    order.OrderStatus = StatusEnum.已录入;
                    result = this.documentorderDAL.Insert(user, order);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out orderId) || orderId <= 0)
                    {
                        result.Message = "制单指令新增失败";
                        result.ResultStatus = -1;
                        return result;
                    }
                    order.OrderId = orderId;

                    //新增明细表
                    detail.OrderId = orderId;
                    detail.DetailStatus = StatusEnum.已生效;
                    result = orderDetailDAL.Insert(user, detail);
                    if (result.ResultStatus != 0)
                        return result;

                    //临票指令类型，区分是否分配库存
                    OrderTypeEnum orderType = (OrderTypeEnum)commercialOrder.OrderType;

                    //新增库存明细与发票明细
                    foreach (Model.OrderReplaceStock stockInvoice in stockInvoices)
                    {
                        //添加库存明细
                        Model.DocumentOrderStock orderStock = new DocumentOrderStock();
                        orderStock.DetailStatus = StatusEnum.已生效;
                        orderStock.OrderId = orderId;
                        orderStock.ApplyAmount = stockInvoice.ApplyWeight;
                        orderStock.ComDetailId = stockInvoice.DetailId;

                        //验证临票库存明细是否存在
                        result = orderStockDAL.Get(user,stockInvoice.DetailId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.DocumentOrderStock commercialOrderStock = result.ReturnValue as Model.DocumentOrderStock;
                        if (commercialOrderStock == null || commercialOrderStock.DetailId <= 0)
                        {
                            result.Message = "替换的库存明细不存在";
                            result.ResultStatus = -1;
                            return result;
                        }

                        if (commercialOrderStock.DetailStatus != StatusEnum.已生效)
                        {
                            result.Message = "替换的库存状态不正确";
                            result.ResultStatus = -1;
                            return result;
                        }

                        int orderStockId = 0;

                        orderStock.StockId = commercialOrderStock.StockId;
                        orderStock.StockNameId = commercialOrderStock.StockNameId;
                        orderStock.RefNo = commercialOrderStock.RefNo;                        

                        result = orderStockDAL.Insert(user, orderStock);
                        if (result.ResultStatus != 0)
                            return result;

                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out orderStockId) || orderStockId <= 0)
                        {
                            result.Message = "制单指令库存明细新增失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //添加发票明细
                        Model.DocumentOrderInvoice orderInvoice = new DocumentOrderInvoice();
                        orderInvoice.DetailStatus = StatusEnum.已生效;
                        orderInvoice.InvoiceBala = stockInvoice.InvoiceBala;
                        orderInvoice.InvoiceNo = stockInvoice.InvoiceNo;
                        orderInvoice.OrderId = orderId;
                        orderInvoice.StockDetailId = orderStockId;

                        result = orderInvoiceDAL.Insert(user, orderInvoice);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (isSubmitAudit)
                    {
                        NFMT.WorkFlow.AutoSubmit submit = new WorkFlow.AutoSubmit();
                        NFMT.WorkFlow.ITaskProvider taskProvider = new TaskProvider.OrderTaskProvider();
                        result = submit.Submit(user, order, taskProvider, WorkFlow.MasterEnum.制单指令审核);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = order;

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

        public SelectModel GetReplaceStocksSelect(int pageIndex, int pageSize, string orderStr, int orderId = 0, bool isCommercial = false)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "dos1.DetailId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "isnull(dos.DetailId,0) as DetailId,dos.OrderId,isnull(sto.StockId,0) as StockId,isnull(sn.RefNo,dos.RefNo) as RefNo,sto.CurNetAmount as LastAmount,sto.UintId,sto.Bundles,sto.StockStatus,sd.StatusName,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,mu.MUName,dos.ApplyAmount as ApplyWeight,doi.InvoiceNo,doi.InvoiceBala,sto.CurGrossAmount ";

            int statusId = (int)Common.StatusTypeEnum.库存状态;
            int readyStatus = (int)Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.Doc_DocumentOrderStock dos ");            
            sb.AppendFormat(" inner join dbo.Doc_DocumentOrderInvoice doi on dos.DetailId = doi.StockDetailId and doi.DetailStatus>={0} ", readyStatus);
            sb.AppendFormat(" left join dbo.Doc_DocumentOrderStock dos1 on dos.DetailId = dos1.ComDetailId and dos1.DetailStatus>={0}", readyStatus);
            sb.Append(" left join dbo.St_Stock sto on sto.StockId = dos.StockId ");
            sb.Append(" left join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = sto.StockStatus and sd.StatusId ={0} ", statusId);
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" dos.DetailStatus >={0} and dos.OrderId={1} ", readyStatus, orderId);

            if (isCommercial)
                sb.Append(" and dos1.ComDetailId is null ");
            else
                sb.Append(" and dos1.ComDetailId is not null ");

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel UpdateReplaceOrder(UserModel user, Model.DocumentOrder order, List<NFMT.Document.Model.OrderReplaceStock> stockInvoices, NFMT.Document.Model.DocumentOrderDetail detail)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.DocumentOrderDetailDAL orderDetailDAL = new DocumentOrderDetailDAL();
                DAL.DocumentOrderStockDAL orderStockDAL = new DocumentOrderStockDAL();
                DAL.DocumentOrderInvoiceDAL orderInvoiceDAL = new DocumentOrderInvoiceDAL();
                NFMT.WareHouse.DAL.StockDAL stockDAL = new WareHouse.DAL.StockDAL();
                NFMT.WareHouse.DAL.StockNameDAL stockNameDAL = new WareHouse.DAL.StockNameDAL();
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
                NFMT.Contract.DAL.ContractDAL contractDAL = new Contract.DAL.ContractDAL();
                NFMT.Contract.DAL.ContractCorporationDetailDAL corpDetailDAL = new Contract.DAL.ContractCorporationDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    if (order == null)
                    {
                        result.Message = "制单指令不包含任何数据";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //验证制单指令
                    result = this.documentorderDAL.Get(user, order.OrderId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.DocumentOrder resultOrder = result.ReturnValue as Model.DocumentOrder;
                    if (resultOrder == null || resultOrder.OrderId <= 0)
                    {
                        result.Message = "制单指令不存在";
                        result.ResultStatus = -1;
                        return result;
                    }
                    
                    resultOrder.OrderDate = order.OrderDate;
                    resultOrder.BankCode = order.BankCode;
                    resultOrder.GrossAmount = order.GrossAmount;
                    resultOrder.NetAmount = order.NetAmount;
                    resultOrder.Bundles = order.Bundles;
                    resultOrder.UnitPrice = order.UnitPrice;
                    resultOrder.InvBala = order.InvBala;
                    resultOrder.InvGap = order.InvGap;
                    resultOrder.Meno = order.Meno;

                    //更新主表
                    result = this.documentorderDAL.Update(user, resultOrder);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取明细表
                    result = orderDetailDAL.GetByOrderId(user, resultOrder.OrderId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.DocumentOrderDetail resultDetail = result.ReturnValue as Model.DocumentOrderDetail;
                    if (resultDetail == null || resultDetail.DetailId <= 0)
                    {
                        result.Message = "制单指令明细获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    resultDetail.InvoiceCopies = detail.InvoiceCopies;
                    resultDetail.InvoiceSpecific = detail.InvoiceSpecific;
                    resultDetail.QualityCopies = detail.QualityCopies;
                    resultDetail.QualitySpecific = detail.QualitySpecific;
                    resultDetail.WeightCopies = detail.WeightCopies;
                    resultDetail.WeightSpecific = detail.WeightSpecific;
                    resultDetail.TexCopies = detail.TexCopies;
                    resultDetail.TexSpecific = detail.TexSpecific;
                    resultDetail.DeliverCopies = detail.DeliverCopies;
                    resultDetail.DeliverSpecific = detail.DeliverSpecific;
                    resultDetail.TotalInvCopies = detail.TotalInvCopies;
                    resultDetail.TotalInvSpecific = detail.TotalInvSpecific;

                    result = orderDetailDAL.Update(user, resultDetail);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废库存明细与发票明细
                    result = orderInvoiceDAL.Load(user, resultOrder.OrderId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.DocumentOrderInvoice> orderInvoices = result.ReturnValue as List<Model.DocumentOrderInvoice>;
                    if (orderInvoices == null)
                    {
                        result.Message = "制单指令发票明细获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }
                    foreach (Model.DocumentOrderInvoice orderInvoice in orderInvoices)
                    {
                        result = orderInvoiceDAL.Invalid(user, orderInvoice);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    result = orderStockDAL.Load(user, resultOrder.OrderId);
                    if (result.ResultStatus != 0)
                        return result;
                    List<Model.DocumentOrderStock> orderStocks = result.ReturnValue as List<Model.DocumentOrderStock>;
                    if (orderStocks == null)
                    {
                        result.Message = "制单指令库存明细获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    foreach (Model.DocumentOrderStock orderStock in orderStocks)
                    {
                        result = orderStockDAL.Invalid(user, orderStock);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    OrderTypeEnum orderType = (OrderTypeEnum)resultOrder.OrderType;

                    //新增库存明细与发票明细
                    foreach (Model.OrderReplaceStock stockInvoice in stockInvoices)
                    {
                        //验证临票库存明细
                        result = orderStockDAL.Get(user, stockInvoice.DetailId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.DocumentOrderStock commercialOrderStock = result.ReturnValue as Model.DocumentOrderStock;
                        if (commercialOrderStock == null || commercialOrderStock.OrderId <= 0)
                        {
                            result.Message = "制单指令替临库存不存在";
                            result.ResultStatus = -1;
                            return result;
                        }

                        if (commercialOrderStock.DetailStatus != StatusEnum.已生效)
                        {
                            result.Message = "制单指令替临库存状态不正确";
                            result.ResultStatus = -1;
                            return result;
                        }

                        if (commercialOrderStock.OrderId != resultOrder.CommercialId)
                        {
                            result.Message = "制单指令替临库存不存在";
                            result.ResultStatus = -1;
                            return result;
                        }

                        Model.DocumentOrderStock orderStock = new DocumentOrderStock();
                        orderStock.DetailStatus = StatusEnum.已生效;
                        orderStock.OrderId = resultOrder.OrderId;
                        orderStock.ApplyAmount = stockInvoice.ApplyWeight;
                        orderStock.StockId = commercialOrderStock.StockId;
                        orderStock.StockNameId = commercialOrderStock.StockNameId;
                        orderStock.RefNo = commercialOrderStock.RefNo;
                        orderStock.ComDetailId = commercialOrderStock.DetailId;

                        //添加库存明细
                        int orderStockId = 0;

                        result = orderStockDAL.Insert(user, orderStock);
                        if (result.ResultStatus != 0)
                            return result;

                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out orderStockId) || orderStockId <= 0)
                        {
                            result.Message = "制单指令库存明细新增失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //添加发票明细
                        Model.DocumentOrderInvoice orderInvoice = new DocumentOrderInvoice();
                        orderInvoice.DetailStatus = StatusEnum.已生效;
                        orderInvoice.InvoiceBala = stockInvoice.InvoiceBala;
                        orderInvoice.InvoiceNo = stockInvoice.InvoiceNo;
                        orderInvoice.OrderId = resultOrder.OrderId;
                        orderInvoice.StockDetailId = orderStockId;

                        result = orderInvoiceDAL.Insert(user, orderInvoice);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = resultOrder;

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
