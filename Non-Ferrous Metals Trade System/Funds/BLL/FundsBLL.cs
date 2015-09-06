/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FundsBLL.cs
// 文件功能描述：资金dbo.Fun_Funds业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Funds.Model;
using NFMT.Funds.DAL;
using NFMT.Funds.IDAL;
using NFMT.Common;

namespace NFMT.Funds.BLL
{
    /// <summary>
    /// 资金dbo.Fun_Funds业务逻辑类。
    /// </summary>
    public class FundsBLL : Common.ExecBLL
    {
        private FundsDAL fundsDAL = new FundsDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(FundsDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public FundsBLL()
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
            get { return this.fundsDAL; }
        }

        #endregion

        /// <summary>
        /// 资金处理
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="fundsLog">资金流水</param>
        /// <param name="fundsHandleType">资金流水类型</param>
        /// <returns></returns>
        public ResultModel FundsUpdate(UserModel user, Model.FundsLog fundsLog, string fundsLogType)
        {
            ResultModel result = new ResultModel();

            try
            {
                string handleType = string.Empty;

                switch (fundsLogType)
                {
                    case "收款":
                        handleType = "+";
                        break;
                    case "付款":
                        handleType = "-";
                        break;
                    //case "冲销":
                    //    if (fundsLog.FlowDirection == NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.收付方向)["收款"].StyleDetailId)
                    //        handleType = "-";
                    //    else if (fundsLog.FlowDirection == NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.收付方向)["付款"].StyleDetailId)
                    //        handleType = "+";
                    //    else
                    //    {
                    //        result.Message = "不存在收付款方向";
                    //        result.ResultStatus = -1;
                    //        return result;
                    //    }
                    //    break;
                    default:
                        result.Message = "不存在资金流水类型";
                        result.ResultStatus = -1;
                        return result;
                }


                //result = fundsDAL.FundsUpdate(user, fundsLog, handleType);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }
            return result;
        }

        #region report

        public SelectModel GetFundsCurrentReportSelect(int pageIndex, int pageSize, string orderStr, int inCorpId, int outCorpId, DateTime startDate, DateTime endDate)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "corp.CorpName asc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)Common.StatusEnum.已生效;
            NFMT.Data.Model.BDStyleDetail bdIssue = NFMT.Data.DetailProvider.Details(Data.StyleEnum.InvoiceDirection)["Issue"];
            NFMT.Data.Model.BDStyleDetail bdCollect = NFMT.Data.DetailProvider.Details(Data.StyleEnum.InvoiceDirection)["Collect"];
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendFormat("'{0}' bgDate,'{1}' endDate,corp.CorpName outCorpName,cur.CurrencyName CurrencyName,t.PreBala,t.LastIssueBala,t.LastCashInBala,t.LastCollectBala,t.LastPayBala,t.LastBala ", startDate.ToShortDateString(), endDate.ToShortDateString());
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append("(");
            sb.Append(Environment.NewLine);
            sb.Append(" select outerId,CurrencyId, SUM(PreCashInBala + PreCollectBala - PreIssueBala -PrePayBala) PreBala,SUM(LastIssueBala) LastIssueBala,SUM(LastCashInBala) LastCashInBala,SUM(LastCollectBala) LastCollectBala,SUM(LastPayBala) LastPayBala,SUM(LastCashInBala + LastCollectBala - LastIssueBala - LastPayBala) LastBala  ");
            //sb.Append(" select outerId,CurrencyId, SUM(PreIssueBala + PreCashInBala - PreCollectBala  -PrePayBala) PreBala,SUM(LastIssueBala) LastIssueBala,SUM(LastCashInBala) LastCashInBala,SUM(LastCollectBala) LastCollectBala,SUM(LastPayBala) LastPayBala,SUM(LastIssueBala + LastCashInBala - LastCollectBala - LastPayBala) LastBala  ");
            sb.Append(Environment.NewLine);
            sb.Append(" from ( ");
            sb.Append(Environment.NewLine);
            sb.Append("select ");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(ISNULL(ISNULL(issue.outerId,cashIn.outerId),collect.outerId),pay.outerId) as outerId,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(ISNULL(ISNULL(issue.CurrencyId,cashIn.CurrencyId),collect.CurrencyId),pay.CurrencyId) as CurrencyId,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(issue.PreBala,0) as PreIssueBala,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(issue.LastBala,0) as LastIssueBala,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(cashIn.PreBala,0) as PreCashInBala,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(cashIn.LastBala,0) as LastCashInBala,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(collect.PreBala,0) as PreCollectBala,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(collect.LastBala,0) as LastCollectBala,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(pay.PreBala,0) as PrePayBala,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(pay.LastBala,0) as LastPayBala");
            sb.Append(Environment.NewLine);
            sb.Append("from ");
            sb.Append(Environment.NewLine);
            sb.Append("(");
            sb.Append(Environment.NewLine);
            sb.Append("	--我方公司【开具】财务发票");
            sb.Append(Environment.NewLine);
            sb.Append("	select ");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	case inv.InvoiceDirection when {0} then inCorp.CorpId when {1} then outCorp.CorpId else '' end as outerId,", bdIssue.StyleDetailId, bdCollect.StyleDetailId);
            sb.Append(Environment.NewLine);
            sb.Append("	SUM(ISNULL(inv.InvoiceBala,0)) as LastBala,");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	SUM(case when inv.InvoiceDate <= '{0}' then ISNULL(inv.InvoiceBala,0) else 0 end) as PreBala,", startDate);
            sb.Append(Environment.NewLine);
            sb.Append("	inv.CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append("	from NFMT..Inv_FinanceInvoice fi");
            sb.Append(Environment.NewLine);
            sb.Append("	left join NFMT..Invoice inv on inv.InvoiceId = fi.InvoiceId");
            sb.Append(Environment.NewLine);
            sb.Append("	left join NFMT_User..Corporation outCorp on outCorp.CorpId = inv.OutCorpId");
            sb.Append(Environment.NewLine);
            sb.Append("	left join NFMT_User..Corporation inCorp on inCorp.CorpId = inv.InCorpId");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	where inv.InvoiceStatus >={0} and inv.InvoiceDirection = {1}", readyStatus, bdIssue.StyleDetailId);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and case inv.InvoiceDirection when {0} then outCorp.CorpId when {1} then inCorp.CorpId else '' end = {2}", bdIssue.StyleDetailId, bdCollect.StyleDetailId, inCorpId);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and case inv.InvoiceDirection when {0} then inCorp.CorpId when {1} then outCorp.CorpId else '' end = ISNULL({2},case inv.InvoiceDirection when {0} then inCorp.CorpId when {1} then outCorp.CorpId else '' end)", bdIssue.StyleDetailId, bdCollect.StyleDetailId, outCorpId == 0 ? "null" : outCorpId.ToString());
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and inv.InvoiceDate <= '{0}'", endDate);
            sb.Append(Environment.NewLine);
            sb.Append("	group by ");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	case inv.InvoiceDirection when {0} then inCorp.CorpId when {1} then outCorp.CorpId else '' end,inv.CurrencyId", bdIssue.StyleDetailId, bdCollect.StyleDetailId);
            sb.Append(Environment.NewLine);
            sb.Append(") issue ");
            sb.Append(Environment.NewLine);
            sb.Append("full outer join");
            sb.Append(Environment.NewLine);
            sb.Append("(");
            sb.Append(Environment.NewLine);
            sb.Append("	--我方公司收款");
            sb.Append(Environment.NewLine);
            sb.Append("	select ");
            sb.Append(Environment.NewLine);
            sb.Append("	ci.PayCorpId as outerId,");
            sb.Append(Environment.NewLine);
            sb.Append("	SUM(ISNULL(ci.CashInBala,0)) as LastBala,");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	SUM(case when ci.CashInDate <= '{0}' then ISNULL(ci.CashInBala,0) else 0 end) as PreBala,", startDate);
            sb.Append(Environment.NewLine);
            sb.Append("	ci.CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append("	from NFMT..Fun_CashIn ci");
            sb.Append(Environment.NewLine);
            sb.Append("	left join NFMT_User..Corporation inCorp on inCorp.CorpId = ci.CashInCorpId");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	where ci.CashInStatus >= {0}", readyStatus);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and ci.CashInCorpId = {0}", inCorpId);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and ci.PayCorpId = ISNULL({0},ci.PayCorpId)", outCorpId == 0 ? "null" : outCorpId.ToString());
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and ci.CashInDate <= '{0}'", endDate);
            sb.Append(Environment.NewLine);
            sb.Append("	group by ci.PayCorpId,ci.CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append(") cashIn on cashIn.outerId = issue.outerId and cashIn.CurrencyId = issue.CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append("full outer join");
            sb.Append(Environment.NewLine);
            sb.Append("(");
            sb.Append(Environment.NewLine);
            sb.Append("	--我方公司【收取】财务发票");
            sb.Append(Environment.NewLine);
            sb.Append("	select ");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	case inv.InvoiceDirection when {0} then inCorp.CorpId when {1} then outCorp.CorpId else '' end as outerId,", bdIssue.StyleDetailId, bdCollect.StyleDetailId);
            sb.Append(Environment.NewLine);
            sb.Append("	SUM(ISNULL(inv.InvoiceBala,0)) as LastBala,");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	SUM(case when inv.InvoiceDate <= '{0}' then ISNULL(inv.InvoiceBala,0) else 0 end) as PreBala,", startDate);
            sb.Append(Environment.NewLine);
            sb.Append("inv.CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append("	from NFMT..Inv_FinanceInvoice fi");
            sb.Append(Environment.NewLine);
            sb.Append("	left join NFMT..Invoice inv on inv.InvoiceId = fi.InvoiceId");
            sb.Append(Environment.NewLine);
            sb.Append("	left join NFMT_User..Corporation outCorp on outCorp.CorpId = inv.OutCorpId");
            sb.Append(Environment.NewLine);
            sb.Append("	left join NFMT_User..Corporation inCorp on inCorp.CorpId = inv.InCorpId");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	where inv.InvoiceStatus >= {0} and inv.InvoiceDirection = {1}", readyStatus, bdCollect.StyleDetailId);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and case inv.InvoiceDirection when {0} then outCorp.CorpId when {1} then inCorp.CorpId else '' end = {2}", bdIssue.StyleDetailId, bdCollect.StyleDetailId, inCorpId);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and case inv.InvoiceDirection when {0} then inCorp.CorpId when {1} then outCorp.CorpId else '' end = ISNULL({2},case inv.InvoiceDirection when {0} then inCorp.CorpId when {1} then outCorp.CorpId else '' end)", bdIssue.StyleDetailId, bdCollect.StyleDetailId, outCorpId == 0 ? "null" : outCorpId.ToString());
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and inv.InvoiceDate <= '{0}'", endDate);
            sb.Append(Environment.NewLine);
            sb.Append("	group by ");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	case inv.InvoiceDirection when {0} then inCorp.CorpId when {1} then outCorp.CorpId else '' end,inv.CurrencyId", bdIssue.StyleDetailId, bdCollect.StyleDetailId);
            sb.Append(Environment.NewLine);
            sb.Append(") collect on collect.outerId = issue.outerId and collect.CurrencyId = issue.CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append("full outer join ");
            sb.Append(Environment.NewLine);
            sb.Append("(");
            sb.Append(Environment.NewLine);
            sb.Append("	--我方公司付款");
            sb.Append(Environment.NewLine);
            sb.Append("	select ");
            sb.Append(Environment.NewLine);
            sb.Append("	pay.RecevableCorp as outerId,");
            sb.Append(Environment.NewLine);
            sb.Append("	SUM(ISNULL(pay.PayBala,0)) as LastBala,");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	SUM(case when pay.PayDatetime <= '{0}' then ISNULL(pay.PayBala,0) else 0 end) as PreBala,", startDate);
            sb.Append(Environment.NewLine);
            sb.Append("	pay.CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append("	from NFMT..Fun_Payment pay");
            sb.Append(Environment.NewLine);
            sb.Append("	left join NFMT_User..Corporation inCorp on inCorp.CorpId = pay.PayCorp");
            sb.Append(Environment.NewLine);
            sb.Append("	left join NFMT_User..Corporation outCorp on outCorp.CorpId = pay.RecevableCorp");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	where pay.PaymentStatus >= {0}", readyStatus);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and pay.PayCorp = {0}", inCorpId);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and pay.RecevableCorp = ISNULL({0},pay.RecevableCorp)", outCorpId == 0 ? "null" : outCorpId.ToString());
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and pay.PayDatetime <= '{0}'", endDate);
            sb.Append(Environment.NewLine);
            sb.Append("	group by pay.RecevableCorp,pay.CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append(") pay on pay.outerId = issue.outerId and pay.CurrencyId = issue.CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append(" ) a group by outerId,CurrencyId ");
            sb.Append(Environment.NewLine);
            sb.Append(") t");
            sb.Append(Environment.NewLine);
            sb.Append("left join NFMT_User..Corporation corp on t.outerId = corp.CorpId");
            sb.Append(Environment.NewLine);
            sb.Append("left join NFMT_Basic..Currency cur on t.CurrencyId = cur.CurrencyId");

            select.TableName = sb.ToString();
            select.WhereStr = string.Empty;

            return select;
        }

        public SelectModel GetFundsCurrentByStockReportSelect(int pageIndex, int pageSize, string orderStr, int inCorpId, int outCorpId, DateTime startDate, DateTime endDate)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "corp.CorpName asc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)Common.StatusEnum.已生效;
            int finishStatus = (int)Common.StatusEnum.已完成;
            NFMT.Data.Model.BDStyleDetail bdIssue = NFMT.Data.DetailProvider.Details(Data.StyleEnum.InvoiceDirection)["Issue"];
            NFMT.Data.Model.BDStyleDetail bdCollect = NFMT.Data.DetailProvider.Details(Data.StyleEnum.InvoiceDirection)["Collect"];
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendFormat("'{0}' bgDate,'{1}' endDate,corp.CorpName outCorpName,cur.CurrencyName CurrencyName,t.PreBala,t.LastIssueBala,t.LastCashInBala,t.LastCollectBala,t.LastPayBala,t.LastBala ", startDate.ToShortDateString(), endDate.ToShortDateString());
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append("(");
            sb.Append(Environment.NewLine);
            sb.Append(" select outerId,CurrencyId, SUM(PreCashInBala + PreCollectBala - PreIssueBala -PrePayBala) PreBala,SUM(LastIssueBala) LastIssueBala,SUM(LastCashInBala) LastCashInBala,SUM(LastCollectBala) LastCollectBala,SUM(LastPayBala) LastPayBala,SUM(LastCashInBala + LastCollectBala - LastIssueBala - LastPayBala) LastBala  ");
            //sb.Append(" select outerId,CurrencyId, SUM(PreIssueBala + PreCashInBala - PreCollectBala  -PrePayBala) PreBala,SUM(LastIssueBala) LastIssueBala,SUM(LastCashInBala) LastCashInBala,SUM(LastCollectBala) LastCollectBala,SUM(LastPayBala) LastPayBala,SUM(LastIssueBala + LastCashInBala - LastCollectBala - LastPayBala) LastBala  ");
            sb.Append(Environment.NewLine);
            sb.Append(" from ( ");
            sb.Append(Environment.NewLine);
            sb.Append("select ");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(ISNULL(ISNULL(stockIn.outerId,cashIn.outerId),stockOut.outerId),pay.outerId) as outerId,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(ISNULL(ISNULL(stockIn.CurrencyId,cashIn.CurrencyId),stockOut.CurrencyId),pay.CurrencyId) as CurrencyId,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(stockIn.PreBala,0) as PreIssueBala,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(stockIn.LastBala,0) as LastIssueBala,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(cashIn.PreBala,0) as PreCashInBala,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(cashIn.LastBala,0) as LastCashInBala,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(stockOut.PreBala,0) as PreCollectBala,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(stockOut.LastBala,0) as LastCollectBala,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(pay.PreBala,0) as PrePayBala,");
            sb.Append(Environment.NewLine);
            sb.Append("ISNULL(pay.LastBala,0) as LastPayBala");
            sb.Append(Environment.NewLine);
            sb.Append("from ");
            sb.Append(Environment.NewLine);
            sb.Append("(");
            sb.Append(Environment.NewLine);
            sb.Append("	--入库（采购）");
            sb.Append(Environment.NewLine);
            sb.Append("	select ");
            sb.Append(Environment.NewLine);
            sb.Append("	outCorp.CorpId as outerId,");
            sb.Append(Environment.NewLine);
            sb.Append("	SUM(ISNULL(si.GrossAmount * ISNULL(pc.SettlePrice,sp.FixedPrice),0)) as LastBala,");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	SUM(case when si.StockInDate <= '{0}' then ISNULL(si.GrossAmount * ISNULL(pc.SettlePrice,sp.FixedPrice),0) else 0 end) as PreBala,", startDate);
            sb.Append(Environment.NewLine);
            sb.Append("	cs.SettleCurrency as CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append("	from dbo.St_ContractStockIn_Ref csir");
            sb.Append(Environment.NewLine);
            sb.Append("	inner join dbo.St_StockIn si on csir.StockInId = si.StockInId");
            sb.Append(Environment.NewLine);
            sb.Append("	inner join dbo.Con_ContractSub cs on csir.ContractSubId = cs.SubId");
            sb.Append(Environment.NewLine);
            sb.Append("	inner join dbo.Con_Contract con on con.ContractId = cs.ContractId");
            sb.Append(Environment.NewLine);
            sb.Append("	left join dbo.Con_SubPrice sp on cs.SubId = sp.SubId");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	left join dbo.Pri_PriceConfirm pc on cs.SubId = pc.SubId and pc.PriceConfirmStatus >={0}", readyStatus);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	inner join dbo.Con_ContractCorporationDetail inCorp on inCorp.ContractId = con.ContractId and inCorp.IsDefaultCorp= 1 and inCorp.IsInnerCorp =1 and inCorp.DetailStatus>={0}", readyStatus);
            sb.Append(Environment.NewLine);
            sb.AppendFormat(" inner join dbo.Con_ContractCorporationDetail outCorp on outCorp.ContractId = con.ContractId and outCorp.IsDefaultCorp=1 and outCorp.IsInnerCorp = 0 and outCorp.DetailStatus>={0} ", readyStatus);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	where csir.RefStatus >={0}", finishStatus);
            sb.Append(Environment.NewLine);
            sb.AppendFormat(" and inCorp.CorpId = {0}", inCorpId);
            sb.Append(Environment.NewLine);
            sb.AppendFormat(" and si.StockInDate <= '{0}' ", endDate);
            sb.Append(Environment.NewLine);
            sb.Append("	group by outCorp.CorpId,cs.SettleCurrency ");
            sb.Append(Environment.NewLine);
            sb.Append(") stockIn ");
            sb.Append(Environment.NewLine);
            sb.Append("full outer join");
            sb.Append(Environment.NewLine);
            sb.Append("(");
            sb.Append(Environment.NewLine);
            sb.Append("	--我方公司收款");
            sb.Append(Environment.NewLine);
            sb.Append("	select ");
            sb.Append(Environment.NewLine);
            sb.Append("	ci.PayCorpId as outerId,");
            sb.Append(Environment.NewLine);
            sb.Append("	SUM(ISNULL(ci.CashInBala,0)) as LastBala,");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	SUM(case when ci.CashInDate <= '{0}' then ISNULL(ci.CashInBala,0) else 0 end) as PreBala,", startDate);
            sb.Append(Environment.NewLine);
            sb.Append("	ci.CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append("	from NFMT..Fun_CashIn ci");
            sb.Append(Environment.NewLine);
            sb.Append("	left join NFMT_User..Corporation inCorp on inCorp.CorpId = ci.CashInCorpId");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	where ci.CashInStatus >= {0}", readyStatus);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and ci.CashInCorpId = {0}", inCorpId);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and ci.PayCorpId = ISNULL({0},ci.PayCorpId)", outCorpId == 0 ? "null" : outCorpId.ToString());
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and ci.CashInDate <= '{0}'", endDate);
            sb.Append(Environment.NewLine);
            sb.Append("	group by ci.PayCorpId,ci.CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append(") cashIn on cashIn.outerId = stockIn.outerId and cashIn.CurrencyId = stockIn.CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append("full outer join");
            sb.Append(Environment.NewLine);
            sb.Append("(");
            sb.Append(Environment.NewLine);
            sb.Append("	--出库（销售）");
            sb.Append(Environment.NewLine);
            sb.Append("	select ");
            sb.Append(Environment.NewLine);
            sb.Append("	outCorp.CorpId as outerId,");
            sb.Append(Environment.NewLine);
            sb.Append("	SUM(ISNULL(sod.GrossAmount * ISNULL(pc.SettlePrice,sp.FixedPrice),0)) as LastBala,");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	SUM(case when so.StockOutTime <= '{0}' then ISNULL(sod.GrossAmount * ISNULL(pc.SettlePrice,sp.FixedPrice),0) else 0 end) as PreBala,", startDate);
            sb.Append(Environment.NewLine);
            sb.Append("sub.SettleCurrency CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append("	from dbo.St_StockOutDetail sod");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	inner join dbo.St_StockOut so on sod.StockOutId = so.StockOutId and so.StockOutStatus >={0}",finishStatus);
            sb.Append(Environment.NewLine);
            sb.Append("	left join dbo.St_StockOutApply soa on so.StockOutApplyId = soa.StockOutApplyId");
            sb.Append(Environment.NewLine);
            sb.Append("	left join dbo.Con_ContractSub sub on soa.SubContractId = sub.SubId");
            sb.Append(Environment.NewLine);
            sb.Append("	left join dbo.Con_Contract con on con.ContractId = sub.ContractId");
            sb.Append(Environment.NewLine);
            sb.Append("	left join dbo.Con_SubPrice sp on sub.SubId = sp.SubId");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	left join dbo.Pri_PriceConfirm pc on sub.SubId = pc.SubId and pc.PriceConfirmStatus >={0}", readyStatus);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	inner join dbo.Con_ContractCorporationDetail inCorp on inCorp.ContractId = con.ContractId and inCorp.IsDefaultCorp= 1 and inCorp.IsInnerCorp =1 and inCorp.DetailStatus>={0}", readyStatus);
            sb.Append(Environment.NewLine);
            sb.AppendFormat(" inner join dbo.Con_ContractCorporationDetail outCorp on outCorp.ContractId = con.ContractId and outCorp.IsDefaultCorp=1 and outCorp.IsInnerCorp = 0 and outCorp.DetailStatus>={0} ", readyStatus);
            sb.Append(Environment.NewLine);
            sb.AppendFormat(" where sod.DetailStatus >={0} ", readyStatus);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("and inCorp.CorpId = {0}", inCorpId);
            sb.Append(Environment.NewLine);
            sb.AppendFormat(" and so.StockOutTime <= '{0}' ", endDate);
            sb.Append(Environment.NewLine);
            sb.Append("	group by outCorp.CorpId,sub.SettleCurrency ");            
            sb.Append(Environment.NewLine);
            sb.Append(") stockOut on stockOut.outerId = stockIn.outerId and stockOut.CurrencyId = stockIn.CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append("full outer join ");
            sb.Append(Environment.NewLine);
            sb.Append("(");
            sb.Append(Environment.NewLine);
            sb.Append("	--我方公司付款");
            sb.Append(Environment.NewLine);
            sb.Append("	select ");
            sb.Append(Environment.NewLine);
            sb.Append("	pay.RecevableCorp as outerId,");
            sb.Append(Environment.NewLine);
            sb.Append("	SUM(ISNULL(pay.PayBala,0)) as LastBala,");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	SUM(case when pay.PayDatetime <= '{0}' then ISNULL(pay.PayBala,0) else 0 end) as PreBala,", startDate);
            sb.Append(Environment.NewLine);
            sb.Append("	pay.CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append("	from NFMT..Fun_Payment pay");
            sb.Append(Environment.NewLine);
            sb.Append("	left join NFMT_User..Corporation inCorp on inCorp.CorpId = pay.PayCorp");
            sb.Append(Environment.NewLine);
            sb.Append("	left join NFMT_User..Corporation outCorp on outCorp.CorpId = pay.RecevableCorp");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	where pay.PaymentStatus >= {0}", readyStatus);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and pay.PayCorp = {0}", inCorpId);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and pay.RecevableCorp = ISNULL({0},pay.RecevableCorp)", outCorpId == 0 ? "null" : outCorpId.ToString());
            sb.Append(Environment.NewLine);
            sb.AppendFormat("	and pay.PayDatetime <= '{0}'", endDate);
            sb.Append(Environment.NewLine);
            sb.Append("	group by pay.RecevableCorp,pay.CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append(") pay on pay.outerId = stockIn.outerId and pay.CurrencyId = stockIn.CurrencyId");
            sb.Append(Environment.NewLine);
            sb.Append(" ) a group by outerId,CurrencyId ");
            sb.Append(Environment.NewLine);
            sb.Append(") t");
            sb.Append(Environment.NewLine);
            sb.Append("left join NFMT_User..Corporation corp on t.outerId = corp.CorpId");
            sb.Append(Environment.NewLine);
            sb.Append("left join NFMT_Basic..Currency cur on t.CurrencyId = cur.CurrencyId");

            select.TableName = sb.ToString();
            select.WhereStr = string.Empty;

            return select;
        }
        //public override string[,] SetExcelRangeData(DataTable source)
        //{
        //    string[,] objData = new string[source.Rows.Count, 10];

        //    for (int i = 0; i < source.Rows.Count; i++)
        //    {
        //        System.Data.DataRow dr = source.Rows[i];

        //        objData[i, 0] = dr["bgDate"].ToString();
        //        objData[i, 1] = dr["endDate"].ToString();
        //        objData[i, 2] = dr["outCorpName"].ToString();
        //        objData[i, 3] = dr["CurrencyName"].ToString();
        //        objData[i, 4] = dr["PreBala"].ToString();
        //        objData[i, 5] = dr["LastIssueBala"].ToString();
        //        objData[i, 6] = dr["LastCashInBala"].ToString();
        //        objData[i, 7] = dr["LastCollectBala"].ToString();
        //        objData[i, 8] = dr["LastPayBala"].ToString();
        //        objData[i, 9] = dr["LastBala"].ToString();
        //    }

        //    return objData;
        //}

        public override DataTable SetExcelRangeData(DataTable source)
        {
            string[] strs = new string[] { "bgDate", "endDate", "outCorpName", "CurrencyName", "PreBala", "LastIssueBala", "LastCashInBala", "LastCollectBala", "LastPayBala", "LastBala" };

            return source.ConvertDataTable(strs);
        }

        #endregion
    }
}
