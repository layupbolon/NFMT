/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeApplyBLL.cs
// 文件功能描述：融资头寸质押申请单dbo.Fin_PledgeApply业务逻辑类。
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
    /// 融资头寸质押申请单dbo.Fin_PledgeApply业务逻辑类。
    /// </summary>
    public class PledgeApplyBLL : Common.ExecBLL
    {
        private PledgeApplyDAL pledgeapplyDAL = new PledgeApplyDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PledgeApplyDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PledgeApplyBLL()
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
            get { return this.pledgeapplyDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, DateTime beginDate, DateTime endDate, int bankId, int assetId, int status, string pledgeApplyNo, string refNo)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            select.OrderStr = string.IsNullOrEmpty(orderStr) ? "pa.PledgeApplyId desc" : orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" pa.PledgeApplyId,pa.PledgeApplyNo,pa.ApplyTime,bank.BankName,bacc.AccountNo,ass.AssetName,case pa.SwitchBack when 0 then '否' when 1 then '是' end as SwitchBack,pa.SumNetAmount,pa.SumHands,mu.MUName,pa.PledgeApplyStatus,bd.StatusName,task.NodeName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fin_PledgeApply pa ");
            sb.Append(" left join NFMT_Basic..Bank bank on pa.FinancingBankId = bank.BankId ");
            sb.Append(" left join NFMT_Basic..BankAccount bacc on pa.FinancingAccountId = bacc.BankAccId ");
            sb.Append(" left join NFMT_Basic..Asset ass on pa.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic..MeasureUnit mu on ass.MUId = mu.MUId ");
            sb.Append(" left join NFMT_Basic..Exchange ex on pa.ExchangeId = ex.ExchangeId ");
            sb.AppendFormat(" left join NFMT_Basic..BDStatusDetail bd on pa.PledgeApplyStatus = bd.DetailId and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);
            sb.Append(" left join ( ");
            sb.Append(" select a.NodeName,a.RowId from ( ");
            sb.Append(" select n.NodeName,ds.RowId,row_number() OVER (partition BY ds.RowId ORDER BY t.TaskId desc,ISNULL(tol.LogTime,dateadd(d,1,getdate())) desc,tn.NodeLevel desc) as num ");
            sb.Append(" from NFMT_WorkFlow.dbo.Wf_DataSource ds ");
            sb.Append(" left join NFMT_WorkFlow.dbo.Wf_Task t on ds.SourceId = t.DataSourceId  ");
            sb.Append(" left join NFMT_WorkFlow.dbo.Wf_TaskNode tn on t.TaskId = tn.TaskId ");
            sb.Append(" left join NFMT_WorkFlow.dbo.Wf_TaskOperateLog tol on tn.TaskNodeId = tol.TaskNodeId ");
            sb.Append(" left join NFMT_WorkFlow.dbo.Wf_Node n on tn.NodeId = n.NodeId ");
            sb.Append(" where ds.BaseName = 'Financing' ");
            sb.Append(" and ds.TableCode = 'dbo.Fin_PledgeApply' ");
            sb.AppendFormat(" and (ISNULL(tol.LogId,0)<>0 or tn.NodeStatus>{0}) ", (int)Common.StatusEnum.审核拒绝);
            sb.Append("  ) a ");
            sb.Append(" where a.num = 1 ");
            sb.Append(" ) task on pa.PledgeApplyId = task.RowId ");

            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (beginDate > NFMT.Common.DefaultValue.DefaultTime && endDate > beginDate)
                sb.AppendFormat(" and pa.ApplyTime between '{0}' and '{1}' ", beginDate.ToShortDateString(), endDate.ToShortDateString());
            if (bankId > 0)
                sb.AppendFormat(" and pa.FinancingBankId = {0} ", bankId);
            if (assetId > 0)
                sb.AppendFormat(" and pa.AssetId = {0} ", assetId);
            if (status > 0)
                sb.AppendFormat(" and pa.PledgeApplyStatus = {0} ", status);
            if (!string.IsNullOrEmpty(pledgeApplyNo))
                sb.AppendFormat(" and pa.PledgeApplyNo like '%{0}%' ", pledgeApplyNo);
            if (!string.IsNullOrEmpty(refNo))
                sb.AppendFormat(" and exists (select 1 from dbo.Fin_PledgeApplyStockDetail where RefNo like '%{0}%' and PledgeApplyId = pa.PledgeApplyId and DetailStatus >={1})", refNo, (int)NFMT.Common.StatusEnum.已生效);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetSelectModelForRepoCreate(int pageIndex, int pageSize, string orderStr, DateTime beginDate, DateTime endDate, int bankId, int assetId, int status, string pledgeApplyNo, string refNo)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            select.OrderStr = string.IsNullOrEmpty(orderStr) ? "pa.PledgeApplyId desc" : orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" pa.PledgeApplyId,pa.PledgeApplyNo,pa.ApplyTime,bank.BankName,bacc.AccountNo,ass.AssetName,case pa.SwitchBack when 0 then '否' when 1 then '是' end as SwitchBack,pa.SumNetAmount,pa.SumHands,mu.MUName,pa.PledgeApplyStatus,bd.StatusName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fin_PledgeApply pa ");
            sb.Append(" left join NFMT_Basic..Bank bank on pa.FinancingBankId = bank.BankId ");
            sb.Append(" left join NFMT_Basic..BankAccount bacc on pa.FinancingAccountId = bacc.BankAccId ");
            sb.Append(" left join NFMT_Basic..Asset ass on pa.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic..MeasureUnit mu on ass.MUId = mu.MUId ");
            sb.Append(" left join NFMT_Basic..Exchange ex on pa.ExchangeId = ex.ExchangeId ");
            sb.AppendFormat(" left join NFMT_Basic..BDStatusDetail bd on pa.PledgeApplyStatus = bd.DetailId and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);
            sb.AppendFormat(" left join (select SUM(SumNetAmount) SumNetAmount,PledgeApplyId from dbo.Fin_RepoApply where RepoApplyStatus >= {0} group by PledgeApplyId ) repoInfo on pa.PledgeApplyId = repoInfo.PledgeApplyId ", (int)Common.StatusEnum.已录入);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" pa.SumNetAmount > ISNULL(repoInfo.SumNetAmount,0) ");

            if (beginDate > NFMT.Common.DefaultValue.DefaultTime && endDate > beginDate)
                sb.AppendFormat(" and pa.ApplyTime between '{0}' and '{1}' ", beginDate.ToShortDateString(), endDate.ToShortDateString());
            if (bankId > 0)
                sb.AppendFormat(" and pa.FinancingBankId = {0} ", bankId);
            if (assetId > 0)
                sb.AppendFormat(" and pa.AssetId = {0} ", assetId);
            if (status > 0)
                sb.AppendFormat(" and pa.PledgeApplyStatus = {0} ", status);
            if (!string.IsNullOrEmpty(pledgeApplyNo))
                sb.AppendFormat(" and pa.PledgeApplyNo like '%{0}%' ", pledgeApplyNo);
            if (!string.IsNullOrEmpty(refNo))
                sb.AppendFormat(" and exists (select 1 from dbo.Fin_PledgeApplyStockDetail where PledgeApplyId = pa.PledgeApplyId and RefNo like '%{0}%' and DetailStatus >= {1})", refNo, (int)NFMT.Common.StatusEnum.已生效);

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion

        #region report

        //public override string[,] SetExcelRangeData(DataTable source)
        //{
        //    string[,] objData = new string[source.Rows.Count, 9];

        //    for (int i = 0; i < source.Rows.Count; i++)
        //    {
        //        System.Data.DataRow dr = source.Rows[i];

        //        objData[i, 0] = dr["PledgeApplyNo"].ToString();
        //        objData[i, 1] = ((DateTime)dr["ApplyTime"]).ToString("yyyy-MM-dd");
        //        objData[i, 2] = dr["BankName"].ToString();
        //        objData[i, 3] = dr["AccountNo"].ToString();
        //        objData[i, 4] = dr["AssetName"].ToString();
        //        //objData[i, 5] = dr["SwitchBack"].ToString();
        //        objData[i, 5] = dr["SumNetAmount"].ToString();
        //        objData[i, 6] = dr["SumHands"].ToString();
        //        objData[i, 7] = dr["NodeName"].ToString();
        //        objData[i, 8] = dr["StatusName"].ToString();
        //    }

        //    return objData;
        //}

        public override DataTable SetExcelRangeData(DataTable source)
        {
            string[] strs = new string[] { "PledgeApplyNo", "ApplyTime", "BankName", "AccountNo", "AssetName", "SumNetAmount", "SumHands", "NodeName", "StatusName" };

            return source.ConvertDataTable(strs);
        }

        #endregion
    }
}
