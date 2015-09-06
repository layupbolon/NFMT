/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Wf_TaskNodeDAL.cs
// 文件功能描述：任务节点dbo.Wf_TaskNode数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月11日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.WorkFlow.Model;
using NFMT.DBUtility;
using NFMT.WorkFlow.IDAL;
using NFMT.Common;

namespace NFMT.WorkFlow.DAL
{
    /// <summary>
    /// 任务节点dbo.Wf_TaskNode数据交互类。
    /// </summary>
    public class TaskNodeDAL : DataOperate, ITaskNodeDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskNodeDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringWorkFlow;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            TaskNode wf_tasknode = (TaskNode)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@TaskNodeId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter nodeidpara = new SqlParameter("@NodeId", SqlDbType.Int, 4);
            nodeidpara.Value = wf_tasknode.NodeId;
            paras.Add(nodeidpara);

            SqlParameter taskidpara = new SqlParameter("@TaskId", SqlDbType.Int, 4);
            taskidpara.Value = wf_tasknode.TaskId;
            paras.Add(taskidpara);

            SqlParameter nodelevelpara = new SqlParameter("@NodeLevel", SqlDbType.Int, 4);
            nodelevelpara.Value = wf_tasknode.NodeLevel;
            paras.Add(nodelevelpara);

            SqlParameter nodestatuspara = new SqlParameter("@NodeStatus", SqlDbType.Int, 4);
            nodestatuspara.Value = wf_tasknode.NodeStatus;
            paras.Add(nodestatuspara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = wf_tasknode.EmpId;
            paras.Add(empidpara);

            SqlParameter audittimepara = new SqlParameter("@AuditTime", SqlDbType.DateTime, 8);
            audittimepara.Value = wf_tasknode.AuditTime;
            paras.Add(audittimepara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            TaskNode tasknode = new TaskNode();

            int indexTaskNodeId = dr.GetOrdinal("TaskNodeId");
            tasknode.TaskNodeId = Convert.ToInt32(dr[indexTaskNodeId]);

            int indexNodeId = dr.GetOrdinal("NodeId");
            tasknode.NodeId = Convert.ToInt32(dr[indexNodeId]);

            int indexTaskId = dr.GetOrdinal("TaskId");
            tasknode.TaskId = Convert.ToInt32(dr[indexTaskId]);

            int indexNodeLevel = dr.GetOrdinal("NodeLevel");
            if (dr["NodeLevel"] != DBNull.Value)
            {
                tasknode.NodeLevel = Convert.ToInt32(dr[indexNodeLevel]);
            }

            int indexNodeStatus = dr.GetOrdinal("NodeStatus");
            if (dr["NodeStatus"] != DBNull.Value)
            {
                tasknode.NodeStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexNodeStatus]);
            }

            int indexEmpId = dr.GetOrdinal("EmpId");
            if (dr["EmpId"] != DBNull.Value)
            {
                tasknode.EmpId = Convert.ToInt32(dr[indexEmpId]);
            }

            int indexAuditTime = dr.GetOrdinal("AuditTime");
            if (dr["AuditTime"] != DBNull.Value)
            {
                tasknode.AuditTime = Convert.ToDateTime(dr[indexAuditTime]);
            }


            return tasknode;
        }

        public override string TableName
        {
            get
            {
                return "Wf_TaskNode";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            TaskNode wf_tasknode = (TaskNode)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter tasknodeidpara = new SqlParameter("@TaskNodeId", SqlDbType.Int, 4);
            tasknodeidpara.Value = wf_tasknode.TaskNodeId;
            paras.Add(tasknodeidpara);

            SqlParameter nodeidpara = new SqlParameter("@NodeId", SqlDbType.Int, 4);
            nodeidpara.Value = wf_tasknode.NodeId;
            paras.Add(nodeidpara);

            SqlParameter taskidpara = new SqlParameter("@TaskId", SqlDbType.Int, 4);
            taskidpara.Value = wf_tasknode.TaskId;
            paras.Add(taskidpara);

            SqlParameter nodelevelpara = new SqlParameter("@NodeLevel", SqlDbType.Int, 4);
            nodelevelpara.Value = wf_tasknode.NodeLevel;
            paras.Add(nodelevelpara);

            SqlParameter nodestatuspara = new SqlParameter("@NodeStatus", SqlDbType.Int, 4);
            nodestatuspara.Value = wf_tasknode.NodeStatus;
            paras.Add(nodestatuspara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = wf_tasknode.EmpId;
            paras.Add(empidpara);

            SqlParameter audittimepara = new SqlParameter("@AuditTime", SqlDbType.DateTime, 8);
            audittimepara.Value = wf_tasknode.AuditTime;
            paras.Add(audittimepara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetTaskNodeByTaskId(UserModel user, int taskId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select * from dbo.Wf_TaskNode where TaskId = {0}", taskId);

                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);

                List<TaskNode> wf_TaskNodes = new List<TaskNode>();

                foreach (DataRow dr in dt.Rows)
                {
                    TaskNode wf_tasknode = new TaskNode();
                    wf_tasknode.TaskNodeId = Convert.ToInt32(dr["TaskNodeId"]);

                    wf_tasknode.NodeId = Convert.ToInt32(dr["NodeId"]);

                    wf_tasknode.TaskId = Convert.ToInt32(dr["TaskId"]);

                    if (dr["NodeLevel"] != DBNull.Value)
                    {
                        wf_tasknode.NodeLevel = Convert.ToInt32(dr["NodeLevel"]);
                    }
                    if (dr["NodeStatus"] != DBNull.Value)
                    {
                        wf_tasknode.NodeStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr["NodeStatus"].ToString());
                    }
                    if (dr["EmpId"] != DBNull.Value)
                    {
                        wf_tasknode.EmpId = Convert.ToInt32(dr["EmpId"]);
                    }
                    if (dr["AuditTime"] != DBNull.Value)
                    {
                        wf_tasknode.AuditTime = Convert.ToDateTime(dr["AuditTime"]);
                    }
                    wf_TaskNodes.Add(wf_tasknode);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = wf_TaskNodes;
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel GetTaskNodeIdByTaskId(UserModel user, int taskId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select TaskNodeId from dbo.Wf_TaskNode where TaskId = {0} and EmpId ={1} ", taskId, user.EmpId);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                int taskNodeId = 0;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && int.TryParse(obj.ToString(), out taskNodeId))
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = taskNodeId;
                    result.Message = "获取成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel GetTaskNodeIdBySameAuditEmp(UserModel user, int taskId, int empId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select distinct TaskNodeId from dbo.Wf_TaskNode where TaskId = {0} and EmpId ={1} and NodeStatus = {2} ", taskId, empId, (int)Common.StatusEnum.已生效);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                    result.Message = "存在相同审核人";
                }
                else
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = new DataTable();
                    result.Message = "不存在相同审核人";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel GetDetailSelectModel(UserModel user, int taskId, int type, bool hasAttach)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                if (!hasAttach)
                {
                    sb.Append("select n.NodeName,bd.DetailName,e.Name,tol.LogTime,tol.LogResult,tol.Memo ");
                    sb.Append(" from dbo.Wf_Task t ");
                    sb.Append(" left join dbo.Wf_TaskNode tn on t.TaskId = tn.TaskId  ");
                    sb.Append(" left join dbo.Wf_TaskOperateLog tol on tn.TaskNodeId = tol.TaskNodeId  ");
                    sb.Append(" left join dbo.Wf_Node n on tn.NodeId = n.NodeId ");
                    sb.Append(" left join NFMT_Basic..BDStyleDetail bd on bd.StyleDetailId = n.NodeType ");
                    sb.Append(" left join NFMT_User.dbo.Employee e on tn.EmpId = e.EmpId ");
                    sb.AppendFormat(" where t.TaskId = {0} and (ISNULL(tol.LogId,0)<>0 or tn.NodeStatus>{1})", taskId, (int)Common.StatusEnum.审核拒绝);
                    sb.Append(" order by ISNULL(tol.LogTime,dateadd(d,1,getdate())),tn.NodeLevel ");
                }
                else
                {

                    sb.Append("declare @s nvarchar(4000)");
                    sb.Append(Environment.NewLine);
                    sb.Append("declare @taskId varchar(50) ");
                    sb.Append(Environment.NewLine);
                    sb.Append("set @s=''");
                    sb.Append(Environment.NewLine);
                    sb.AppendFormat("set @taskId={0}", taskId);
                    sb.Append(Environment.NewLine);
                    sb.Append("select  @s=@s+','+quotename([AttachName])+'=max(case when [AttachName]='+quotename(a.AttachName,'''')+' then case when ISNULL(taol.OperateLogId,0)=0 then ''0''");
                    sb.Append("else ''1'' end else null end)'");
                    sb.Append(Environment.NewLine);
                    sb.Append("from NFMT_WorkFlow..Wf_TaskAttach ta ");
                    sb.Append(Environment.NewLine);
                    sb.Append("left join NFMT_WorkFlow..Wf_TaskAttachOperateLog taol on taol.AttachId = ta.AttachId");
                    sb.Append(Environment.NewLine);
                    sb.Append("left join NFMT..Attach a on ta.AttachId = a.AttachId");
                    sb.Append(Environment.NewLine);
                    sb.Append("where ta.TaskId = @taskId");
                    sb.Append(Environment.NewLine);

                    sb.Append("exec('select tn.TaskNodeId,e.Name,tol.LogTime,tol.LogResult,tol.Memo'+@s+'");
                    sb.Append(Environment.NewLine);
                    sb.Append("from NFMT_WorkFlow..Wf_Task t");
                    sb.Append(Environment.NewLine);
                    sb.Append("left join NFMT_WorkFlow.dbo.Wf_TaskNode tn on t.TaskId = tn.TaskId");
                    sb.Append(Environment.NewLine);
                    sb.Append("left join NFMT_WorkFlow.dbo.Wf_TaskOperateLog tol on tn.TaskNodeId = tol.TaskNodeId");
                    sb.Append(Environment.NewLine);
                    sb.Append("left join NFMT_WorkFlow..Wf_TaskAttachOperateLog taol on tol.LogId = taol.LogId");
                    sb.Append(Environment.NewLine);
                    if (hasAttach)
                    {
                        sb.Append("right join  NFMT_WorkFlow..Wf_TaskAttach ta on taol.AttachId = ta.AttachId");
                        sb.Append(Environment.NewLine);
                        sb.Append("left join NFMT..Attach a on ta.AttachId = a.AttachId");
                        sb.Append(Environment.NewLine);
                    }
                    sb.Append("left join NFMT_User.dbo.Employee e on tn.EmpId = e.EmpId");
                    sb.Append(Environment.NewLine);
                    //if (type == 1)
                    sb.Append(" where tn.TaskId = '+@taskId+'");
                    //else if (type == 0)
                    //    sb.AppendFormat(" where tn.TaskId = '+@taskId+' and tn.NodeStatus = {0}", (int)NFMT.Common.StatusEnum.已生效);
                    sb.Append(Environment.NewLine);
                    sb.Append("group by tn.TaskNodeId,e.Name,tol.LogTime,tol.LogResult,tol.Memo");
                    sb.Append("')");
                }

                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                    result.Message = "获取成功";
                }
                else
                {
                    result.ResultStatus = 0;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public override int MenuId
        {
            get
            {
                return 22;
            }
        }

        public ResultModel UpdateTaskNodeStatusByLevelId(UserModel user, int taskId, int nodeLevel,Common.StatusEnum status)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Wf_TaskNode set NodeStatus = {0} where TaskId = {1} and NodeLevel = {2}", (int)status, taskId, nodeLevel);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "修改状态成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "修改状态失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel UpdateTaskNodeStatusByLevelIdExceptSelf(UserModel user, Model.TaskNode taskNode, Common.StatusEnum status)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Wf_TaskNode set NodeStatus = {0} where TaskId = {1} and NodeLevel = {2} and TaskNodeId <> {3}", (int)status, taskNode.TaskId, taskNode.NodeLevel, taskNode.TaskNodeId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "修改状态成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "修改状态失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            return result;
        }

        #endregion
    }
}
