/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SIBLL.cs
// 文件功能描述：价外票dbo.Inv_SI业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月27日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Invoice.Model;
using NFMT.Invoice.DAL;
using NFMT.Invoice.IDAL;
using NFMT.Common;

namespace NFMT.Invoice.BLL
{
    /// <summary>
    /// 价外票dbo.Inv_SI业务逻辑类。
    /// </summary>
    public class SIBLL : Common.ExecBLL
    {
        private SIDAL siDAL = new SIDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(SIDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SIBLL()
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
            get { return this.siDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, string invName, int corpId, int corpOut, DateTime fromdate, DateTime todate)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "inv.InvoiceId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" inv.InvoiceId,inv.InvoiceNo,inv.InvoiceName,inv.InvoiceDate, CONVERT(varchar,inv.InvoiceBala) + c.CurrencyName as InvoiceBala,c1.CorpName as InnerCorp,c2.CorpName as OutCorp,bd.StatusName,inv.InvoiceStatus, ");
            sb.Append(" convert(varchar,(select SUM(ISNULL(DetailBala,0)) from dbo.Inv_SIDetail where SIId = ISNULL(si.SIId,0))) + c.CurrencyName as AllotBala ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Invoice inv  ");
            sb.Append(" left join dbo.Inv_SI si on inv.InvoiceId = si.InvoiceId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on inv.CurrencyId = c.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c1 on inv.InCorpId = c1.CorpId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c2 on inv.OutCorpId = c2.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = inv.InvoiceStatus and StatusId = {0} ", statusId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" inv.InvoiceType = {0} ", (int)InvoiceTypeEnum.价外票);

            if (!string.IsNullOrEmpty(invName.Trim()))
                sb.AppendFormat(" and inv.InvoiceName like '%{0}%' ", invName);
            if (corpId > 0)
                sb.AppendFormat(" and c1.CorpId = {0} ", corpId);
            if (corpOut > 0)
                sb.AppendFormat(" and c2.CorpId = {0} ", corpOut);
            if (fromdate > Common.DefaultValue.DefaultTime && todate > fromdate)
                sb.AppendFormat(" and inv.InvoiceDate between '{0}' and '{1}' ", fromdate.ToString(), todate.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetCanAllotStockSelectModel(int pageIndex, int pageSize, string orderStr, string refNo, string outContractNO)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" sto.StockId,slog.StockLogId,slog.ContractId,slog.SubContractId as ContractSubId,sname.RefNo,c.CorpName,a.AssetName,sub.SubNo,sto.CardNo,dp.DPName,bra.BrandName,sub.OutContractNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_Stock sto ");
            sb.Append(" left join dbo.St_StockLog slog on sto.StockId = slog.StockId ");
            sb.Append(" left join dbo.St_StockName sname on sto.StockNameId = sname.StockNameId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c on sto.CorpId = c.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on sto.AssetId = a.AssetId ");
            sb.Append(" left join dbo.Con_ContractSub sub on slog.SubContractId = sub.SubId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" LogDirection = {0} and slog.LogSource = 'dbo.St_StockIn' and slog.LogSourceBase = 'NFMT' ", Data.DetailProvider.Details(Data.StyleEnum.流水方向)["In"].StyleDetailId);
            if (!string.IsNullOrEmpty(refNo))
                sb.AppendFormat(" and sname.RefNo like '%{0}%' ", refNo);
            if (!string.IsNullOrEmpty(outContractNO))
                sb.AppendFormat(" and sub.OutContractNo like '%{0}%' ", outContractNO);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Create(UserModel user, NFMT.Operate.Model.Invoice invoice, Model.SI si, List<Model.SIDetail> siDetails)
        {
            ResultModel result = new ResultModel();
            NFMT.Operate.DAL.InvoiceDAL invoiceDAL = new Operate.DAL.InvoiceDAL();
            DAL.SIDAL sIDAL = new SIDAL();
            DAL.SIDetailDAL sIDetailDAL = new SIDetailDAL();
            NFMT.User.DAL.CorporationDAL corporationDAL = new User.DAL.CorporationDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = corporationDAL.Get(user, invoice.InCorpId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.User.Model.Corporation corp = result.ReturnValue as NFMT.User.Model.Corporation;
                    if (corp == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "公司信息不存在";
                        return result;
                    }
                    invoice.InCorpName = corp.CorpName;

                    result = corporationDAL.Get(user, invoice.OutCorpId);
                    if (result.ResultStatus != 0)
                        return result;

                    corp = result.ReturnValue as NFMT.User.Model.Corporation;
                    if (corp == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "公司信息不存在";
                        return result;
                    }
                    invoice.OutCorpName = corp.CorpName;

                    result = invoiceDAL.Insert(user, invoice);
                    if (result.ResultStatus != 0)
                        return result;

                    int invoiceId = (int)result.ReturnValue;
                    si.InvoiceId = invoiceId;

                    result = sIDAL.Insert(user, si);
                    if (result.ResultStatus != 0)
                        return result;

                    int sIId = (int)result.ReturnValue;

                    foreach (Model.SIDetail detail in siDetails)
                    {
                        detail.SIId = sIId;
                        result = sIDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    invoice.InvoiceId = invoiceId;

                    if (result.ResultStatus == 0) result.ReturnValue = invoice;

                    scope.Complete();
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel GetSIbyInvoiceId(UserModel user, int invoiceId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = siDAL.GetSIbyInvoiceId(user, invoiceId);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Update(UserModel user, NFMT.Operate.Model.Invoice invoice, Model.SI si, List<Model.SIDetail> siDetails)
        {
            ResultModel result = new ResultModel();
            NFMT.Operate.DAL.InvoiceDAL invoiceDAL = new Operate.DAL.InvoiceDAL();
            DAL.SIDAL sIDAL = new SIDAL();
            DAL.SIDetailDAL sIDetailDAL = new SIDetailDAL();
            NFMT.User.DAL.CorporationDAL corporationDAL = new User.DAL.CorporationDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取发票信息
                    result = invoiceDAL.Get(user, invoice.InvoiceId);
                    if (result.ResultStatus != 0)
                        return result;
                    NFMT.Operate.Model.Invoice resultObj = result.ReturnValue as NFMT.Operate.Model.Invoice;
                    if (resultObj == null || resultObj.InvoiceId <= 0)
                    {
                        result.Message = "获取发票信息失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //获取公司信息
                    result = corporationDAL.Get(user, invoice.InCorpId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.User.Model.Corporation corp = result.ReturnValue as NFMT.User.Model.Corporation;
                    if (corp == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "公司信息不存在";
                        return result;
                    }
                    resultObj.InCorpName = corp.CorpName;

                    result = corporationDAL.Get(user, invoice.OutCorpId);
                    if (result.ResultStatus != 0)
                        return result;

                    corp = result.ReturnValue as NFMT.User.Model.Corporation;
                    if (corp == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "公司信息不存在";
                        return result;
                    }
                    resultObj.OutCorpName = corp.CorpName;

                    resultObj.InvoiceDate = invoice.InvoiceDate;
                    resultObj.InvoiceName = invoice.InvoiceName;
                    resultObj.InvoiceType = invoice.InvoiceType;
                    resultObj.InvoiceBala = invoice.InvoiceBala;
                    resultObj.CurrencyId = invoice.CurrencyId;
                    resultObj.InvoiceDirection = invoice.InvoiceDirection;
                    resultObj.OutCorpId = invoice.OutCorpId;
                    resultObj.InCorpId = invoice.InCorpId;
                    resultObj.Memo = invoice.Memo;

                    //更新发票信息
                    result = invoiceDAL.Update(user, resultObj);
                    if (result.ResultStatus != 0)
                        return result;

                    //更新价外票信息
                    result = sIDAL.Update(user, si);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废价外票
                    result = sIDetailDAL.InvalidAll(user, si.SIId);
                    if (result.ResultStatus != 0)
                        return result;

                    foreach (Model.SIDetail detail in siDetails)
                    {
                        detail.SIId = si.SIId;
                        result = sIDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                    scope.Complete();
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel Goback(UserModel user, int sIId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.SIDAL sIDAL = new SIDAL();
                NFMT.Operate.DAL.InvoiceDAL invoiceDAL = new Operate.DAL.InvoiceDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取价外票
                    result = sIDAL.Get(user, sIId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Invoice.Model.SI sI = result.ReturnValue as NFMT.Invoice.Model.SI;
                    if (sI == null || sI.SIId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票验证失败";
                        return result;
                    }

                    //获取发票
                    result = invoiceDAL.Get(user, sI.InvoiceId);
                    if (result.ResultStatus != 0)
                        return result;
                    NFMT.Operate.Model.Invoice invoice = result.ReturnValue as NFMT.Operate.Model.Invoice;
                    if (invoice == null || invoice.InvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票获取失败";
                        return result;
                    }

                    //撤返发票
                    result = invoiceDAL.Goback(user, invoice);
                    if (result.ResultStatus != 0)
                        return result;

                    //工作流任务关闭
                    WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, invoice);
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

        public ResultModel Invalid(UserModel user, int sIId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.SIDAL sIDAL = new SIDAL();
                NFMT.Operate.DAL.InvoiceDAL invoiceDAL = new Operate.DAL.InvoiceDAL();
                DAL.SIDetailDAL detailDAL = new SIDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取财务票
                    result = sIDAL.Get(user, sIId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Invoice.Model.SI sI = result.ReturnValue as NFMT.Invoice.Model.SI;
                    if (sI == null || sI.SIId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票验证失败";
                        return result;
                    }

                    //获取发票
                    result = invoiceDAL.Get(user, sI.InvoiceId);
                    if (result.ResultStatus != 0)
                        return result;
                    NFMT.Operate.Model.Invoice invoice = result.ReturnValue as NFMT.Operate.Model.Invoice;
                    if (invoice == null || invoice.InvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票获取失败";
                        return result;
                    }

                    //作废发票
                    result = invoiceDAL.Invalid(user, invoice);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废明细
                    result = detailDAL.InvalidAll(user, sI.SIId);
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

        public ResultModel Complete(UserModel user, int sIId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.SIDAL sIDAL = new SIDAL();
                NFMT.Operate.DAL.InvoiceDAL invoiceDAL = new Operate.DAL.InvoiceDAL();
                DAL.SIDetailDAL detailDAL = new SIDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取财务票
                    result = sIDAL.Get(user, sIId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Invoice.Model.SI sI = result.ReturnValue as NFMT.Invoice.Model.SI;
                    if (sI == null || sI.SIId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票验证失败";
                        return result;
                    }

                    //获取发票
                    result = invoiceDAL.Get(user, sI.InvoiceId);
                    if (result.ResultStatus != 0)
                        return result;
                    NFMT.Operate.Model.Invoice invoice = result.ReturnValue as NFMT.Operate.Model.Invoice;
                    if (invoice == null || invoice.InvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票获取失败";
                        return result;
                    }

                    //发票完成
                    result = invoiceDAL.Complete(user, invoice);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取明细
                    result = detailDAL.Load(user, sI.SIId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.SIDetail> details = result.ReturnValue as List<Model.SIDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票明细获取失败";
                        return result;
                    }

                    foreach (Model.SIDetail detail in details)
                    {
                        result = detailDAL.Complete(user, detail);
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

        public ResultModel CompleteCancel(UserModel user, int sIId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.SIDAL sIDAL = new SIDAL();
                NFMT.Operate.DAL.InvoiceDAL invoiceDAL = new Operate.DAL.InvoiceDAL();
                DAL.SIDetailDAL detailDAL = new SIDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取财务票
                    result = sIDAL.Get(user, sIId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Invoice.Model.SI sI = result.ReturnValue as NFMT.Invoice.Model.SI;
                    if (sI == null || sI.SIId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票验证失败";
                        return result;
                    }

                    //获取发票
                    result = invoiceDAL.Get(user, sI.InvoiceId);
                    if (result.ResultStatus != 0)
                        return result;
                    NFMT.Operate.Model.Invoice invoice = result.ReturnValue as NFMT.Operate.Model.Invoice;
                    if (invoice == null || invoice.InvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票获取失败";
                        return result;
                    }

                    //发票
                    result = invoiceDAL.CompleteCancel(user, invoice);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取明细
                    result = detailDAL.Load(user, sI.SIId, StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.SIDetail> details = result.ReturnValue as List<Model.SIDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票明细获取失败";
                        return result;
                    }

                    foreach (Model.SIDetail detail in details)
                    {
                        result = detailDAL.CompleteCancel(user, detail);
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

        public ResultModel GetSIIdsByCustomCorpId(UserModel user, int customCorpId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = siDAL.GetSIIdsByCustomCorpId(user, customCorpId);
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
