/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceApplyDetailBLL.cs
// 文件功能描述：开票申请明细dbo.Inv_InvoiceApplyDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年1月27日
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
    /// 开票申请明细dbo.Inv_InvoiceApplyDetail业务逻辑类。
    /// </summary>
    public class InvoiceApplyDetailBLL : Common.ExecBLL
    {
        private InvoiceApplyDetailDAL invoiceapplydetailDAL = new InvoiceApplyDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(InvoiceApplyDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InvoiceApplyDetailBLL()
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
            get { return this.invoiceapplydetailDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int invoiceApplyId,string detailIds)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "iad.DetailId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" iad.DetailId,iad.InvoiceApplyId,iad.ApplyId,iad.InvoiceId,iad.BussinessInvoiceId,iad.ContractId,iad.SubContractId,iad.StockLogId,inv.InvoiceNo,inv.InvoiceDate,inv.InvoiceBala,cur.CurrencyName,sub.SubNo,sub.OutContractNo,customerCorp.CorpName,ass.AssetName,bi.NetAmount,mu.MUName,iad.InvoicePrice,iad.PaymentAmount,iad.InterestAmount,iad.OtherAmount,slog.CardNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Inv_InvoiceApplyDetail iad ");
            sb.Append(" left join NFMT..Invoice inv on iad.InvoiceId = inv.InvoiceId ");
            sb.Append(" left join NFMT..Con_ContractSub sub on iad.SubContractId = sub.SubId ");
            sb.Append(" left join NFMT_User..Corporation customerCorp on customerCorp.CorpId = inv.OutCorpId ");
            sb.Append(" left join NFMT..Inv_BusinessInvoice bi on iad.BussinessInvoiceId = bi.BusinessInvoiceId ");
            sb.Append(" left join NFMT_Basic..Asset ass on ass.AssetId = bi.AssetId ");
            sb.Append(" left join NFMT_Basic..MeasureUnit mu on mu.MUId = bi.MUId ");
            sb.Append(" left join NFMT..St_StockLog slog on slog.StockLogId = iad.StockLogId ");
            sb.Append(" left join NFMT_Basic..Currency cur on cur.CurrencyId = sub.SettleCurrency ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" iad.InvoiceApplyId = {0} and iad.DetailStatus = {1} ", invoiceApplyId, (int)Common.StatusEnum.已生效);
            sb.AppendFormat(" and bi.BusinessInvoiceId not in (select BusinessInvoiceId from dbo.Inv_FinBusInvAllotDetail where DetailStatus <> {0}) ", (int)Common.StatusEnum.已作废);

            if (!string.IsNullOrEmpty(detailIds))
                sb.AppendFormat(" and iad.DetailId not in ({0}) ", detailIds);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetUpdateSelectModel(int pageIndex, int pageSize, string orderStr, int invoiceApplyId, string detailIds)
        {
            SelectModel select = this.GetSelectModel(pageIndex, pageSize, orderStr, 0, string.Empty);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat(" iad.InvoiceApplyId = {0} ", invoiceApplyId);

            if (!string.IsNullOrEmpty(detailIds))
                sb.AppendFormat(" and iad.DetailId in ({0}) ", detailIds);
            else
                sb.Append(" and 1=2 ");

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetDetailSelectModel(int pageIndex, int pageSize, string orderStr, int invoiceApplyId, string detailIds)
        {
            SelectModel select = this.GetSelectModel(pageIndex, pageSize, orderStr, 0, string.Empty);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat(" iad.InvoiceApplyId = {0} and iad.DetailStatus = {1} ", invoiceApplyId, (int)Common.StatusEnum.已生效);

            if (!string.IsNullOrEmpty(detailIds))
                sb.AppendFormat(" and iad.DetailId not in ({0}) ", detailIds);

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion
    }
}
