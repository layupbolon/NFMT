/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BusinessInvoiceDetailBLL.cs
// 文件功能描述：业务发票明细dbo.Inv_BusinessInvoiceDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月25日
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
    /// 业务发票明细dbo.Inv_BusinessInvoiceDetail业务逻辑类。
    /// </summary>
    public class BusinessInvoiceDetailBLL : Common.ExecBLL
    {
        private BusinessInvoiceDetailDAL businessinvoicedetailDAL = new BusinessInvoiceDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(BusinessInvoiceDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BusinessInvoiceDetailBLL()
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
            get { return this.businessinvoicedetailDAL; }
        }

        #endregion

        #region report

        public SelectModel GetReportSelect(int pageIndex, int pageSize, string orderStr, string refNo,int brandId,string cardNo)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "st.StockId desc";
            else
                select.OrderStr = orderStr;

            NFMT.Data.Model.BDStyleDetail bdStockIn = NFMT.Data.DetailProvider.Details(Data.StyleEnum.LogType)["StockIn"];//入库
            NFMT.Data.Model.BDStyleDetail bdStockOut = NFMT.Data.DetailProvider.Details(Data.StyleEnum.LogType)["StockOut"];//出库

            int readyStatus = (int)Common.StatusEnum.已生效;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("st.StockId,sn.RefNo,ass.AssetName,st.StockDate,bra.BrandName,dp.DPName,st.CardNo,");
            sb.Append("inBala.OutCorpName as InBalaOutCorpName,inBala.InCorpName as InBalaInCorpName,inBala.InBala,");
            sb.Append("outBala.InCorpName as OutBalaInCorpName,outBala.OutCorpName as OutBalaOutCorpName,outBala.OutBala,");
            sb.Append("siBala.SIBala,");
            sb.Append("outBala.OutBala-inBala.InBala-siBala.SIBala as ProfitBala,");
            sb.Append("inBala.CurrencyName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT..St_Stock st ");
            sb.Append(" inner join NFMT..St_StockName sn on st.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic..Asset ass on ass.AssetId = st.AssetId ");
            sb.Append(" left join NFMT_Basic..Brand bra on bra.BrandId = st.BrandId ");
            sb.Append(" left join NFMT_Basic..DeliverPlace dp on dp.DPId = st.DeliverPlaceId ");
            sb.Append(" left join ( ");
            sb.Append(" select bid.StockId,SUM(ISNULL(bid.Bala,0)) as InBala,cur.CurrencyName,CONVERT(varchar,SUM(ISNULL(bid.Bala,0)))+cur.CurrencyName as InBalaName,subCorpIn.CorpName as InCorpName,subCorpOut.CorpName as OutCorpName ");
            sb.Append(" from NFMT..Inv_BusinessInvoiceDetail bid ");
            sb.Append(" left join NFMT..St_StockLog slog on bid.StockLogId = slog.StockLogId ");
            sb.Append(" inner join NFMT..Invoice inv on bid.InvoiceId = inv.InvoiceId ");
            sb.Append(" left join NFMT_Basic..Currency cur on inv.CurrencyId = cur.CurrencyId ");
            sb.Append(" inner join NFMT..Inv_BusinessInvoice bi on bi.BusinessInvoiceId = bid.BusinessInvoiceId ");
            sb.Append(" left join NFMT..Con_SubCorporationDetail subCorpIn on bi.SubContractId = subCorpIn.SubId and subCorpIn.IsInnerCorp = 1 and subCorpIn.IsDefaultCorp = 1 ");
            sb.Append(" left join NFMT..Con_SubCorporationDetail subCorpOut on bi.SubContractId = subCorpOut.SubId and subCorpOut.IsInnerCorp = 0 and subCorpOut.IsDefaultCorp = 1 ");
            sb.AppendFormat(" where bid.DetailStatus>={0} and slog.LogType={1} ", readyStatus, bdStockIn.StyleDetailId);
            sb.Append(" group by bid.StockId,cur.CurrencyName,subCorpIn.CorpName,subCorpOut.CorpName ");
            sb.Append(" ) inBala on st.StockId = inBala.StockId ");
            sb.Append(" left join ( ");
            sb.Append(" select bid.StockId,SUM(ISNULL(bid.Bala,0)) as OutBala,cur.CurrencyName,CONVERT(varchar,SUM(ISNULL(bid.Bala,0)))+cur.CurrencyName as OutBalaName,subCorpIn.CorpName as InCorpName,subCorpOut.CorpName as OutCorpName ");
            sb.Append(" from NFMT..Inv_BusinessInvoiceDetail bid ");
            sb.Append(" left join NFMT..St_StockLog slog on bid.StockLogId = slog.StockLogId ");
            sb.Append(" inner join NFMT..Invoice inv on bid.InvoiceId = inv.InvoiceId ");
            sb.Append(" left join NFMT_Basic..Currency cur on inv.CurrencyId = cur.CurrencyId ");
            sb.Append(" inner join NFMT..Inv_BusinessInvoice bi on bi.BusinessInvoiceId = bid.BusinessInvoiceId ");
            sb.Append(" left join NFMT..Con_SubCorporationDetail subCorpIn on bi.SubContractId = subCorpIn.SubId and subCorpIn.IsInnerCorp = 1 and subCorpIn.IsDefaultCorp = 1 ");
            sb.Append(" left join NFMT..Con_SubCorporationDetail subCorpOut on bi.SubContractId = subCorpOut.SubId and subCorpOut.IsInnerCorp = 0 and subCorpOut.IsDefaultCorp = 1 ");
            sb.AppendFormat(" where bid.DetailStatus>={0} and slog.LogType={1} ", readyStatus, bdStockOut.StyleDetailId);
            sb.Append(" group by bid.StockId,cur.CurrencyName,subCorpIn.CorpName,subCorpOut.CorpName ");
            sb.Append(" ) outBala on st.StockId = outBala.StockId ");
            sb.Append(" left join ( ");
            sb.Append(" select detail.StockId,SUM(ISNULL(detail.DetailBala,0)) as SIBala,cur.CurrencyName,CONVERT(varchar,SUM(ISNULL(detail.DetailBala,0)))+cur.CurrencyName as SIBalaName ");
            sb.Append(" from NFMT..Inv_SIDetail detail ");
            sb.Append(" inner join NFMT..Inv_SI si on detail.SIId = si.SIId ");
            sb.Append(" inner join NFMT..Invoice inv on si.InvoiceId = inv.InvoiceId ");
            sb.Append(" left join NFMT_Basic..Currency cur on inv.CurrencyId = cur.CurrencyId ");
            sb.AppendFormat(" where detail.DetailStatus >={0} ", readyStatus);
            sb.Append(" group by detail.StockId,cur.CurrencyName ");
            sb.Append(" ) siBala on st.StockId = siBala.StockId ");

            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(refNo))
                sb.AppendFormat(" and sn.RefNo like '%{0}%' ", refNo);
            if (!string.IsNullOrEmpty(cardNo))
                sb.AppendFormat(" and st.CardNo like '%{0}%' ", cardNo);
            if (brandId > 0)
                sb.AppendFormat(" and st.BrandId = {0} ", brandId);
            
            select.WhereStr = sb.ToString();
            return select;
        }

        //public override string[,] SetExcelRangeData(DataTable source)
        //{
        //    string[,] objData = new string[source.Rows.Count, 15];

        //    for (int i = 0; i < source.Rows.Count; i++)
        //    {
        //        System.Data.DataRow dr = source.Rows[i];

        //        objData[i, 0] = ((DateTime)dr["StockDate"]).ToString("yyyy-MM-dd");
        //        objData[i, 1] = dr["RefNo"].ToString();
        //        objData[i, 2] = dr["AssetName"].ToString();
        //        objData[i, 3] = dr["BrandName"].ToString();
        //        objData[i, 4] = dr["DPName"].ToString();
        //        objData[i, 5] = dr["CardNo"].ToString();
        //        objData[i, 6] = dr["InBalaOutCorpName"].ToString();
        //        objData[i, 7] = dr["InBalaInCorpName"].ToString();
        //        objData[i, 8] = dr["InBala"].ToString();
        //        objData[i, 9] = dr["OutBalaInCorpName"].ToString();
        //        objData[i, 10] = dr["OutBalaOutCorpName"].ToString();
        //        objData[i, 11] = dr["OutBala"].ToString();
        //        objData[i, 12] = dr["SIBala"].ToString();
        //        objData[i, 13] = dr["ProfitBala"].ToString();
        //        objData[i, 14] = dr["CurrencyName"].ToString();
        //    }

        //    return objData;
        //}

        public override DataTable SetExcelRangeData(DataTable source)
        {
            string[] strs = new string[] { "StockDate", "RefNo", "AssetName", "BrandName", "DPName", "CardNo", "InBalaOutCorpName", "InBalaInCorpName", "InBala", "OutBalaInCorpName", "OutBalaOutCorpName", "OutBala", "SIBala", "ProfitBala", "CurrencyName" };

            return source.ConvertDataTable(strs);
        }

        #endregion
    }
}
