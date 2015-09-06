/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoApplyBLL.cs
// 文件功能描述：融资头寸赎回申请单dbo.Fin_RepoApply业务逻辑类。
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
    /// 融资头寸赎回申请单dbo.Fin_RepoApply业务逻辑类。
    /// </summary>
    public class RepoApplyBLL : Common.ExecBLL
    {
        private RepoApplyDAL repoapplyDAL = new RepoApplyDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(RepoApplyDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepoApplyBLL()
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
            get { return this.repoapplyDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int status, string pledgeApplyNo, string repoApplyNo, string refNo,DateTime beginDate, DateTime endDate)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            select.OrderStr = string.IsNullOrEmpty(orderStr) ? "ra.RepoApplyId desc" : orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" ra.RepoApplyId,ra.RepoApplyIdNo,ra.CreateTime,pa.PledgeApplyNo,pa.ApplyTime,bank.BankName,ass.AssetName,pa.SumNetAmount as PledgeSumNetAmount,pa.SumHands as PledgeSumHands,ra.SumNetAmount as RepoSumNetAmount,ra.SumHands as RepoSumHands,ra.RepoApplyStatus,bd.StatusName,task.NodeName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fin_RepoApply ra ");
            sb.Append(" left join dbo.Fin_PledgeApply pa on ra.PledgeApplyId = pa.PledgeApplyId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank bank on bank.BankId = pa.FinancingBankId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = pa.AssetId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on ra.RepoApplyStatus = bd.DetailId and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);
            sb.Append(" left join ( ");
            sb.Append(" select a.NodeName,a.RowId from ( ");
            sb.Append(" select n.NodeName,ds.RowId,row_number() OVER (partition BY ds.RowId ORDER BY t.TaskId desc,ISNULL(tol.LogTime,dateadd(d,1,getdate())) desc,tn.NodeLevel desc) as num ");
            sb.Append(" from NFMT_WorkFlow.dbo.Wf_DataSource ds ");
            sb.Append(" left join NFMT_WorkFlow.dbo.Wf_Task t on ds.SourceId = t.DataSourceId  ");
            sb.Append(" left join NFMT_WorkFlow.dbo.Wf_TaskNode tn on t.TaskId = tn.TaskId ");
            sb.Append(" left join NFMT_WorkFlow.dbo.Wf_TaskOperateLog tol on tn.TaskNodeId = tol.TaskNodeId ");
            sb.Append(" left join NFMT_WorkFlow.dbo.Wf_Node n on tn.NodeId = n.NodeId ");
            sb.Append(" where ds.BaseName = 'Financing' ");
            sb.Append(" and ds.TableCode = 'dbo.Fin_RepoApply' ");
            sb.AppendFormat(" and (ISNULL(tol.LogId,0)<>0 or tn.NodeStatus>{0}) ", (int)Common.StatusEnum.审核拒绝);
            sb.Append("  ) a ");
            sb.Append(" where a.num = 1 ");
            sb.Append(" ) task on ra.RepoApplyId = task.RowId ");

            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (beginDate > NFMT.Common.DefaultValue.DefaultTime && endDate > beginDate)
                sb.AppendFormat(" and ra.CreateTime between '{0}' and '{1}' ", beginDate.ToShortDateString(), endDate.ToShortDateString());
            if (status > 0)
                sb.AppendFormat(" and ra.RepoApplyStatus = {0} ", status);
            if (!string.IsNullOrEmpty(pledgeApplyNo))
                sb.AppendFormat(" and pa.PledgeApplyNo like '%{0}%' ", pledgeApplyNo);
            if (!string.IsNullOrEmpty(repoApplyNo))
                sb.AppendFormat(" and ra.RepoApplyIdNo like '%{0}%' ", repoApplyNo);
            if (!string.IsNullOrEmpty(refNo))
                sb.AppendFormat(" and exists (select 1 from dbo.Fin_RepoApplyDetail where RefNo like '%{0}%' and RepoApplyId = ra.RepoApplyId and DetailStatus >={1})", refNo, (int)NFMT.Common.StatusEnum.已生效);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetSelectModelRepoInfo(int pageIndex, int pageSize, string orderStr, int pledgeApplyId)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            select.OrderStr = string.IsNullOrEmpty(orderStr) ? "ra.RepoApplyId desc" : orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" ra.RepoApplyId,ra.RepoApplyIdNo,ra.SumNetAmount as RepoSumNetAmount,ra.SumHands as RepoSumHands,ra.RepoApplyStatus,bd.StatusName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fin_RepoApply ra ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on ra.RepoApplyStatus = bd.DetailId and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" ra.RepoApplyStatus >= {0} and ra.PledgeApplyId = {1}", (int)Common.StatusEnum.已录入, pledgeApplyId);

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion

        #region report

        //public override string[,] SetExcelRangeData(DataTable source)
        //{
        //    string[,] objData = new string[source.Rows.Count, 11];

        //    for (int i = 0; i < source.Rows.Count; i++)
        //    {
        //        System.Data.DataRow dr = source.Rows[i];

        //        objData[i, 0] = dr["RepoApplyIdNo"].ToString();
        //        objData[i, 1] = ((DateTime)dr["CreateTime"]).ToString("yyyy-MM-dd");
        //        objData[i, 2] = dr["RepoSumNetAmount"].ToString();                
        //        objData[i, 3] = dr["RepoSumHands"].ToString();
        //        objData[i, 4] = dr["PledgeApplyNo"].ToString();                
        //        objData[i, 5] = dr["BankName"].ToString();
        //        objData[i, 6] = dr["AssetName"].ToString();
        //        objData[i, 7] = dr["PledgeSumNetAmount"].ToString();
        //        objData[i, 8] = dr["PledgeSumHands"].ToString();
        //        objData[i, 9] = dr["NodeName"].ToString();
        //        objData[i, 10] = dr["StatusName"].ToString();
        //    }

        //    return objData;
        //}

        public override DataTable SetExcelRangeData(DataTable source)
        {
            string[] strs = new string[] { "RepoApplyIdNo", "CreateTime", "RepoSumNetAmount", "RepoSumHands", "PledgeApplyNo", "BankName", "AssetName", "PledgeSumNetAmount", "PledgeSumHands", "NodeName", "StatusName" };

            return source.ConvertDataTable(strs);
        }

        #endregion
    }
}
