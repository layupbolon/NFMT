/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoApplyDetailBLL.cs
// 文件功能描述：赎回申请单明细dbo.Fin_RepoApplyDetail业务逻辑类。
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
    /// 赎回申请单明细dbo.Fin_RepoApplyDetail业务逻辑类。
    /// </summary>
    public class RepoApplyDetailBLL : Common.ExecBLL
    {
        private RepoApplyDetailDAL repoapplydetailDAL = new RepoApplyDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(RepoApplyDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepoApplyDetailBLL()
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
            get { return this.repoapplydetailDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModelForRepoUpdate(int pageIndex, int pageSize, string orderStr, int pledgeApplyId, int repoApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "rad.StockDetailId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat(" rad.DetailId,rad.StockDetailId,rad.RepoTime,rad.ContractNo,rad.StockId,rad.RefNo,pasd.NetAmount as PledgeNetAmount,pasd.Hands as PledgeHands,rad.Memo,ISNULL(alStock.AlreadyNetAmount,0) as AlreadyNetAmount,ISNULL(alStock.AlreadyHands,0) as AlreadyHands,rad.AccountName,rad.NetAmount,rad.Hands,rad.Price,case when rad.ExpiringDate <='{0}' then '' else rad.ExpiringDate end as ExpiringDate ", NFMT.Common.DefaultValue.DefaultTime);
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fin_RepoApplyDetail rad ");
            sb.Append(" left join dbo.Fin_PledgeApplyStockDetail pasd on rad.StockDetailId = pasd.StockDetailId ");
            sb.Append(" left join ( ");
            sb.Append(" select StockId,SUM(ISNULL(NetAmount,0)) as AlreadyNetAmount,SUM(ISNULL(Hands,0)) as AlreadyHands ");
            sb.Append(" from dbo.Fin_RepoApplyDetail ");
            sb.AppendFormat(" where DetailStatus >= {0} and RepoApplyId <> {1} ", (int)Common.StatusEnum.已录入, repoApplyId);
            sb.Append(" group by StockId ");
            sb.Append(" ) alStock on rad.StockId = alStock.StockId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" rad.RepoApplyId = {0} and rad.DetailStatus >= {1} ", repoApplyId, (int)Common.StatusEnum.已作废);

            select.WhereStr = sb.ToString();

            return select;
        }
        
        public SelectModel GetSelectModelForHand(int pageIndex, int pageSize, string orderStr, int pledgeApplyId, int repoApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "rad.StockDetailId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" rad.DetailId,rad.StockDetailId,rad.RepoTime,rad.ContractNo,rad.StockId,rad.RefNo,pasd.NetAmount as PledgeNetAmount,pasd.Hands as PledgeHands,pasd.Memo,ISNULL(alStock.AlreadyNetAmount,0) as AlreadyNetAmount,ISNULL(alStock.AlreadyHands,0) as AlreadyHands,rad.AccountName,rad.NetAmount,ROUND(rad.NetAmount/ass.AmountPerHand,0) as Hands,rad.Price,rad.ExpiringDate");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fin_RepoApplyDetail rad ");
            sb.Append(" left join dbo.Fin_PledgeApplyStockDetail pasd on rad.StockDetailId = pasd.StockDetailId ");
            sb.Append(" left join dbo.Fin_PledgeApply pa on pasd.PledgeApplyId = pa.PledgeApplyId ");
            sb.Append(" left join ( ");
            sb.Append(" select StockId,SUM(ISNULL(NetAmount,0)) as AlreadyNetAmount,SUM(ISNULL(Hands,0)) as AlreadyHands ");
            sb.Append(" from dbo.Fin_RepoApplyDetail ");
            sb.AppendFormat(" where DetailStatus >= {0} and RepoApplyId <> {1} ", (int)Common.StatusEnum.已录入, repoApplyId);
            sb.Append(" group by StockId ");
            sb.Append(" ) alStock on rad.StockId = alStock.StockId ");
            sb.Append(" left join NFMT_Basic..Asset ass on ass.AssetId = pa.AssetId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" rad.RepoApplyId = {0} and rad.DetailStatus = {1} ", repoApplyId, (int)Common.StatusEnum.已生效);

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion
    }
}
