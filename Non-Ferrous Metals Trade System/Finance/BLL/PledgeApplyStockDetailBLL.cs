/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeApplyStockDetailBLL.cs
// 文件功能描述：质押申请单实货明细dbo.Fin_PledgeApplyStockDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Finance.Model;
using NFMT.Finance.DAL;
using NFMT.Finance.IDAL;
using NFMT.Common;

namespace NFMT.Finance.BLL
{
    /// <summary>
    /// 质押申请单实货明细dbo.Fin_PledgeApplyStockDetail业务逻辑类。
    /// </summary>
    public class PledgeApplyStockDetailBLL : Common.ExecBLL
    {
        private PledgeApplyStockDetailDAL pledgeapplystockdetailDAL = new PledgeApplyStockDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PledgeApplyStockDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PledgeApplyStockDetailBLL()
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
            get { return this.pledgeapplystockdetailDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int pledgeApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "detail.StockDetailId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" detail.*,bd.StatusName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fin_PledgeApplyStockDetail detail ");
            sb.Append(" left join NFMT_Basic..BDStatusDetail bd on bd.DetailId = detail.DetailStatus ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" detail.PledgeApplyId = {0} and detail.DetailStatus >= {1} ", pledgeApplyId, (int)Common.StatusEnum.已作废);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetSelectModelForHands(int pageIndex, int pageSize, string orderStr, int pledgeApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "detail.StockDetailId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" detail.StockDetailId,detail.PledgeApplyId,detail.ContractNo,detail.NetAmount,detail.StockId,detail.RefNo,detail.Deadline,ROUND(detail.NetAmount/ass.AmountPerHand,0) as Hands,detail.Memo,detail.DetailStatus,bd.StatusName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fin_PledgeApplyStockDetail detail ");
            sb.Append(" left join NFMT_Basic..BDStatusDetail bd on bd.DetailId = detail.DetailStatus ");
            sb.Append(" inner join dbo.Fin_PledgeApply pa on detail.PledgeApplyId = pa.PledgeApplyId ");
            sb.Append(" left join NFMT_Basic..Asset ass on ass.AssetId = pa.AssetId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" detail.PledgeApplyId = {0} and detail.DetailStatus >= {1} ", pledgeApplyId, (int)Common.StatusEnum.已生效);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetSelectModelForRepo(int pageIndex, int pageSize, string orderStr, int pledgeApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pasd.StockDetailId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" pasd.StockDetailId,pasd.ContractNo,pasd.StockId,pasd.RefNo,pasd.NetAmount as PledgeNetAmount,pasd.Hands as PledgeHands,pasd.Memo,ISNULL(alStock.AlreadyNetAmount,0) as AlreadyNetAmount,ISNULL(alStock.AlreadyHands,0) as AlreadyHands,pasd.NetAmount-ISNULL(alStock.AlreadyNetAmount,0) as NetAmount,pasd.Hands-ISNULL(alStock.AlreadyHands,0) as Hands,pasd.Deadline ");//,pacd.Price,pacd.ExpiringDate,pacd.AccountName
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fin_PledgeApplyStockDetail pasd ");
            //sb.AppendFormat(" left join dbo.Fin_PledgeApplyCashDetail pacd on pasd.ContractNo = pacd.StockContractNo and pasd.Deadline = pacd.Deadline and pacd.PledgeApplyId = pasd.PledgeApplyId and pacd.DetailStatus = {0} ", (int)Common.StatusEnum.已生效);
            sb.Append(" left join ( ");
            sb.Append(" select StockId,SUM(ISNULL(NetAmount,0)) as AlreadyNetAmount,SUM(ISNULL(Hands,0)) as AlreadyHands ");
            sb.Append(" from dbo.Fin_RepoApplyDetail ");
            sb.AppendFormat(" where DetailStatus in ({0},{1}) and PledgeApplyId = {2} ", (int)Common.StatusEnum.已录入, (int)Common.StatusEnum.已生效, pledgeApplyId);
            sb.Append(" group by StockId ");
            sb.Append(" ) alStock on pasd.StockId = alStock.StockId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" pasd.PledgeApplyId = {0} and pasd.DetailStatus = {1} ", pledgeApplyId, (int)Common.StatusEnum.已生效);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetSelectModelForRepoUpdate(int pageIndex, int pageSize, string orderStr, int pledgeApplyId, int repoApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pasd.StockDetailId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" pasd.StockDetailId,pasd.ContractNo,pasd.StockId,pasd.RefNo,pasd.NetAmount as PledgeNetAmount,pasd.Hands as PledgeHands,pasd.Memo,ISNULL(alStock.AlreadyNetAmount,0) as AlreadyNetAmount,ISNULL(alStock.AlreadyHands,0) as AlreadyHands,pasd.NetAmount-ISNULL(alStock.AlreadyNetAmount,0) as NetAmount,pasd.Hands-ISNULL(alStock.AlreadyHands,0) as Hands,pacd.Price,pacd.ExpiringDate,pacd.AccountName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fin_PledgeApplyStockDetail pasd ");
            sb.Append(" left join dbo.Fin_PledgeApplyCashDetail pacd on pasd.ContractNo = pacd.StockContractNo and pasd.Deadline = pacd.Deadline and pasd.PledgeApplyId = pacd.PledgeApplyId ");
            sb.Append(" left join ( ");
            sb.Append(" select StockId,SUM(ISNULL(NetAmount,0)) as AlreadyNetAmount,SUM(ISNULL(Hands,0)) as AlreadyHands ");
            sb.Append(" from dbo.Fin_RepoApplyDetail ");
            sb.AppendFormat(" where DetailStatus in ({0},{1}) and RepoApplyId <> {2} and PledgeApplyId = {3} ", (int)Common.StatusEnum.已录入, (int)Common.StatusEnum.已生效, repoApplyId, pledgeApplyId);
            sb.Append(" group by StockId ");
            sb.Append(" ) alStock on pasd.StockId = alStock.StockId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" pasd.PledgeApplyId = {0} and pasd.DetailStatus = {1} and pasd.StockDetailId not in (select StockDetailId from dbo.Fin_RepoApplyDetail where RepoApplyId = {2} and DetailStatus = {1})", pledgeApplyId, (int)Common.StatusEnum.已生效, repoApplyId);

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion

        #region report

        public SelectModel GetBankPledgeReportSelect(int pageIndex, int pageSize, string orderStr, string refNo, int bankId, DateTime startDate, DateTime endDate, int repoInfo)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "bank.BankName desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)Common.StatusEnum.已生效;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(" bank.BankName,pasd.RefNo,pasd.NetAmount,pasd.ContractNo,pa.ApplyTime,pasd.Hands,price.Price,price.ExpiringDate,pasd.NetAmount-ISNULL(repo.NetAmount,0) nowPledgeAmount ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" Financing.dbo.Fin_PledgeApply pa ");
            sb.Append(" left join NFMT_Basic.dbo.Bank bank on pa.FinancingBankId = bank.BankId ");
            sb.Append(" inner join Financing.dbo.Fin_PledgeApplyStockDetail pasd on pa.PledgeApplyId = pasd.PledgeApplyId and pasd.DetailStatus >=50 ");
            sb.Append(" left join ( ");
            sb.Append(" select ROW_NUMBER() OVER(PARTITION BY PledgeApplyId,StockContractNo,Deadline ORDER BY ExpiringDate desc) rowid, PledgeApplyId,Price,ExpiringDate,StockContractNo,Deadline ");
            sb.Append(" from Financing.dbo.Fin_PledgeApplyCashDetail ");
            sb.Append(" where DetailStatus >=50  ");
            sb.Append(" ) price on pasd.ContractNo = price.StockContractNo and pasd.Deadline = price.Deadline and pasd.PledgeApplyId = price.PledgeApplyId and price.rowid = 1 ");
            sb.Append(" left join ( ");
            sb.Append(" select rad.PledgeApplyId,rad.StockId,SUM(rad.NetAmount) NetAmount ");
            sb.Append(" from Financing.dbo.Fin_RepoApplyDetail rad ");
            sb.Append(" left join Financing.dbo.Fin_RepoApply ra on rad.RepoApplyId = ra.RepoApplyId ");
            sb.Append(" where rad.DetailStatus >=50 and ra.RepoApplyStatus >=50 ");
            sb.Append(" group by rad.PledgeApplyId,rad.StockId ");
            sb.Append(" ) repo on pasd.PledgeApplyId = repo.PledgeApplyId and pasd.StockId = repo.StockId ");

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" pa.PledgeApplyStatus >= {0} ", readyStatus);
            if (bankId > 0)
                sb.AppendFormat(" and pa.FinancingBankId = {0} ", bankId);
            if (!string.IsNullOrEmpty(refNo))
                sb.AppendFormat(" and pasd.RefNo like '%{0}%' ", refNo);
            if (startDate > Common.DefaultValue.DefaultTime && endDate > startDate)
                sb.AppendFormat(" and pa.ApplyTime between '{0}' and '{1}' ", startDate.ToString(), endDate.AddDays(1).ToString());
            if (repoInfo > 0)
            {
                if (repoInfo == 1)
                    sb.AppendFormat(" and (pasd.NetAmount-ISNULL(repo.NetAmount,0)) = 0 ");
                if(repoInfo == 2)
                    sb.AppendFormat(" and (pasd.NetAmount-ISNULL(repo.NetAmount,0)) > 0 ");
            }

            select.WhereStr = sb.ToString();

            return select;
        }

        //public override string[,] SetExcelRangeData(DataTable source)
        //{
        //    string[,] objData = new string[source.Rows.Count, 9];

        //    for (int i = 0; i < source.Rows.Count; i++)
        //    {
        //        System.Data.DataRow dr = source.Rows[i];

        //        objData[i, 0] = dr["BankName"].ToString();
        //        objData[i, 1] = dr["RefNo"].ToString();
        //        objData[i, 2] = dr["NetAmount"].ToString();
        //        objData[i, 3] = dr["ContractNo"].ToString();
        //        objData[i, 4] = ((DateTime)dr["ApplyTime"]).ToString("yyyy-MM-dd");
        //        objData[i, 5] = dr["Hands"].ToString();
        //        objData[i, 6] = dr["Price"].ToString();
        //        objData[i, 7] = ((DateTime)dr["ExpiringDate"]).ToString("yyyy-MM-dd");
        //        objData[i, 8] = dr["nowPledgeAmount"].ToString();
        //    }

        //    return objData;
        //}
        
        public override DataTable SetExcelRangeData(DataTable source)
        {
            string[] strs = new string[] { "BankName", "RefNo", "NetAmount", "ContractNo", "ApplyTime", "Hands", "Price", "ExpiringDate", "nowPledgeAmount" };

            return source.ConvertDataTable(strs);

            //dt.Columns.Add(source.Columns["BankName"]);
            //dt.Columns.Add(source.Columns["RefNo"]);
            //dt.Columns.Add(source.Columns["NetAmount"]);
            //dt.Columns.Add(source.Columns["ContractNo"]);
            //dt.Columns.Add(source.Columns["ApplyTime"]);
            //dt.Columns.Add(source.Columns["Hands"]);
            //dt.Columns.Add(source.Columns["Price"]);
            //dt.Columns.Add(source.Columns["ExpiringDate"]);
            //dt.Columns.Add(source.Columns["nowPledgeAmount"]);

            //return dt;
        }

        #endregion
    }
}