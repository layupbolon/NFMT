/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：TaskBLL.cs
// 文件功能描述：dbo.Task业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.WorkFlow.Model;
using NFMT.WorkFlow.DAL;
using NFMT.WorkFlow.IDAL;
using NFMT.Common;

namespace NFMT.WorkFlow.BLL
{
    /// <summary>
    /// dbo.Task业务逻辑类。
    /// </summary>
    public class TaskBLL : Common.DataBLL
    {
        private TaskDAL taskDAL = new TaskDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(TaskDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskBLL()
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
            get { return this.taskDAL; }
        }
        #endregion

        #region 新增方法

        /// <summary>
        /// 获取分页查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="status"></param>
        /// <param name="key"></param>
        /// <param name="empId"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int status, string key, int empId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "tn.TaskNodeId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " tn.TaskNodeId,t.TaskId,t.TaskName,t.TaskConnext,e.Name,d.ApplyTime,bd.StatusName ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" NFMT_WorkFlow.dbo.Wf_TaskNode tn left join NFMT_WorkFlow.dbo.Wf_Task t on tn.TaskId = t.TaskId");
            sb.Append(" left join NFMT_WorkFlow.dbo.Wf_DataSource d on t.DataSourceId = d.SourceId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on e.EmpId = d.EmpId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = t.TaskStatus ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" tn.NodeStatus = {0} and tn.EmpId = {1} and t.TaskStatus <> {2}", status, empId, (int)Common.StatusEnum.已作废);

            if (!string.IsNullOrEmpty(key))
                sb.AppendFormat(" and t.TaskName like '%{0}%'", key);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetAuditedModel(int pageIndex, int pageSize, string orderStr, string key, int empId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "t.TaskId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " t.TaskId,t.TaskName,e.Name,d.ApplyTime,bd.StatusName,tlog.LogTime,t.TaskConnext ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" NFMT_WorkFlow.dbo.Wf_Task t ");
            sb.Append(" left join NFMT_WorkFlow.dbo.Wf_DataSource d on t.DataSourceId = d.SourceId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on e.EmpId = d.EmpId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = t.TaskStatus ");
            sb.Append(" left join ( ");
            sb.Append(" select * from ( ");
            sb.Append(" select row_number() over(partition by tlog.EmpId,node.TaskId order by tlog.LogTime desc) rowId,tlog.EmpId,node.TaskId,tlog.LogTime ");
            sb.Append(" from NFMT_WorkFlow.dbo.Wf_TaskOperateLog tlog ");
            sb.Append(" left join NFMT_WorkFlow.dbo.Wf_TaskNode node on tlog.TaskNodeId = node.TaskNodeId ");
            sb.AppendFormat(" where tlog.EmpId = {0}) a ", empId);
            sb.Append(" where a.rowId = 1) tlog on tlog.TaskId = t.TaskId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" t.TaskId in (select tn.TaskId from NFMT_WorkFlow.dbo.Wf_TaskNode tn where tn.EmpId = {0} and tn.NodeStatus = {1}) ", empId, (int)NFMT.Common.StatusEnum.已生效);

            if (!string.IsNullOrEmpty(key))
                sb.AppendFormat(" and t.TaskName like '%{0}%'", key);

            select.WhereStr = sb.ToString();

            return select;
        }

        //public ResultModel TaskAudit(UserModel user, int id, string memo, string logResult, bool isPass)
        //{
        //    ResultModel result = new ResultModel();

        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
        //        {
        //            NFMT.WorkFlow.DAL.TaskNodeDAL taskNodeDAL = new NFMT.WorkFlow.DAL.TaskNodeDAL();
        //            result = taskNodeDAL.Get(user, id);
        //            if (result.ResultStatus != 0)
        //                return result;

        //            NFMT.WorkFlow.Model.TaskNode taskNode = result.ReturnValue as NFMT.WorkFlow.Model.TaskNode;
        //            NFMT.WorkFlow.Model.TaskOperateLog taskOperateLog = new NFMT.WorkFlow.Model.TaskOperateLog()
        //            {
        //                TaskNodeId = id,
        //                EmpId = user.EmpId,
        //                Memo = memo,
        //                LogTime = DateTime.Now,
        //                LogResult = logResult
        //            };

        //            FlowOperate flowOperate = new FlowOperate();
        //            result = flowOperate.AuditTaskNode(user, taskNode, taskOperateLog, isPass);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = ex.Message;
        //    }
        //    finally
        //    {
        //        if (result.ResultStatus != 0)
        //            log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
        //        else if (log.IsInfoEnabled)
        //            log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
        //    }

        //    return result;
        //}

        public ResultModel GetAuditProgressSelectModel(UserModel user, string baseName, string tableCode, int sourceId)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("select n.NodeName,bd.DetailName,e.Name,tol.LogTime,tol.LogResult,tol.Memo ");
            sb.Append(" from dbo.Wf_DataSource ds ");
            sb.Append(" left join dbo.Wf_Task t on ds.SourceId = t.DataSourceId ");
            sb.Append(" left join dbo.Wf_TaskNode tn on t.TaskId = tn.TaskId  ");
            sb.Append(" left join dbo.Wf_TaskOperateLog tol on tn.TaskNodeId = tol.TaskNodeId  ");
            sb.Append(" left join dbo.Wf_Node n on tn.NodeId = n.NodeId ");
            sb.Append(" left join NFMT_Basic..BDStyleDetail bd on bd.StyleDetailId = n.NodeType ");
            sb.Append(" left join NFMT_User.dbo.Employee e on tn.EmpId = e.EmpId ");
            sb.AppendFormat(" where ds.BaseName = '{0}' and ds.TableCode = '{1}' and ds.RowId = {2} and (ISNULL(tol.LogId,0)<>0 or tn.NodeStatus>{3}) ", baseName, tableCode, sourceId, (int)Common.StatusEnum.审核拒绝);
            //sb.AppendFormat(" and ds.DataStatus >= {0} ", (int)Common.StatusEnum.待审核);
            sb.Append(" order by t.TaskId,ISNULL(tol.LogTime,dateadd(d,1,getdate())),tn.NodeLevel ");

            return taskDAL.GetAuditProgress(user, sb.ToString());

            //NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            //select.PageIndex = pageIndex;
            //select.PageSize = pageSize;
            //if (string.IsNullOrEmpty(orderStr))
            //    select.OrderStr = "tol.LogId asc";
            //else
            //    select.OrderStr = orderStr;

            //select.ColumnName = " n.NodeName,bd.DetailName,e.Name,tol.LogTime,tol.LogResult,tol.Memo ";

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append(" dbo.Wf_DataSource ds ");
            //sb.Append(" left join dbo.Wf_Task t on ds.SourceId = t.DataSourceId ");
            //sb.Append(" left join dbo.Wf_TaskNode tn on t.TaskId = tn.TaskId ");
            //sb.Append(" left join dbo.Wf_TaskOperateLog tol on tn.TaskNodeId = tol.TaskNodeId ");
            //sb.Append(" left join dbo.Wf_Node n on tn.NodeId = n.NodeId ");
            //sb.Append(" left join NFMT_Basic..BDStyleDetail bd on bd.StyleDetailId = n.NodeType ");
            //sb.Append(" left join NFMT_User.dbo.Employee e on tn.EmpId = e.EmpId ");
            //select.TableName = sb.ToString();

            //sb.Clear();
            //sb.AppendFormat(" ds.BaseName = '{0}' and ds.TableCode = '{1}' and ds.RowId = {2} and ds.DataStatus >= {3} and (ISNULL(tol.LogId,0)<>0 or tn.NodeStatus>{4})", baseName, tableCode, sourceId, (int)Common.StatusEnum.待审核, (int)Common.StatusEnum.审核拒绝);
            //sb.Append(" order by ISNULL(tol.LogTime,dateadd(d,1,getdate())),tn.NodeLevel ");

            //select.WhereStr = sb.ToString();

            //return select;
        }

        #endregion

    }
}
