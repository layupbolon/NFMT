/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Wf_TaskOperateLogDAL.cs
// 文件功能描述：任务操作记录表dbo.Wf_TaskOperateLog数据交互类。
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
    /// 任务操作记录表dbo.Wf_TaskOperateLog数据交互类。
    /// </summary>
    public class TaskOperateLogDAL : DataOperate, ITaskOperateLogDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskOperateLogDAL()
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
            TaskOperateLog wf_taskoperatelog = (TaskOperateLog)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@LogId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter tasknodeidpara = new SqlParameter("@TaskNodeId", SqlDbType.Int, 4);
            tasknodeidpara.Value = wf_taskoperatelog.TaskNodeId;
            paras.Add(tasknodeidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = wf_taskoperatelog.EmpId;
            paras.Add(empidpara);

            if (!string.IsNullOrEmpty(wf_taskoperatelog.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = wf_taskoperatelog.Memo;
                paras.Add(memopara);
            }

            SqlParameter logtimepara = new SqlParameter("@LogTime", SqlDbType.DateTime, 8);
            logtimepara.Value = wf_taskoperatelog.LogTime;
            paras.Add(logtimepara);

            if (!string.IsNullOrEmpty(wf_taskoperatelog.LogResult))
            {
                SqlParameter logresultpara = new SqlParameter("@LogResult", SqlDbType.VarChar, 400);
                logresultpara.Value = wf_taskoperatelog.LogResult;
                paras.Add(logresultpara);
            }


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            TaskOperateLog taskoperatelog = new TaskOperateLog();

            int indexLogId = dr.GetOrdinal("LogId");
            taskoperatelog.LogId = Convert.ToInt32(dr[indexLogId]);

            int indexTaskNodeId = dr.GetOrdinal("TaskNodeId");
            taskoperatelog.TaskNodeId = Convert.ToInt32(dr[indexTaskNodeId]);

            int indexEmpId = dr.GetOrdinal("EmpId");
            taskoperatelog.EmpId = Convert.ToInt32(dr[indexEmpId]);

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                taskoperatelog.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexLogTime = dr.GetOrdinal("LogTime");
            if (dr["LogTime"] != DBNull.Value)
            {
                taskoperatelog.LogTime = Convert.ToDateTime(dr[indexLogTime]);
            }

            int indexLogResult = dr.GetOrdinal("LogResult");
            if (dr["LogResult"] != DBNull.Value)
            {
                taskoperatelog.LogResult = Convert.ToString(dr[indexLogResult]);
            }


            return taskoperatelog;
        }

        public override string TableName
        {
            get
            {
                return "Wf_TaskOperateLog";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            TaskOperateLog wf_taskoperatelog = (TaskOperateLog)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter logidpara = new SqlParameter("@LogId", SqlDbType.Int, 4);
            logidpara.Value = wf_taskoperatelog.LogId;
            paras.Add(logidpara);

            SqlParameter tasknodeidpara = new SqlParameter("@TaskNodeId", SqlDbType.Int, 4);
            tasknodeidpara.Value = wf_taskoperatelog.TaskNodeId;
            paras.Add(tasknodeidpara);

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = wf_taskoperatelog.EmpId;
            paras.Add(empidpara);

            if (!string.IsNullOrEmpty(wf_taskoperatelog.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = wf_taskoperatelog.Memo;
                paras.Add(memopara);
            }

            SqlParameter logtimepara = new SqlParameter("@LogTime", SqlDbType.DateTime, 8);
            logtimepara.Value = wf_taskoperatelog.LogTime;
            paras.Add(logtimepara);

            if (!string.IsNullOrEmpty(wf_taskoperatelog.LogResult))
            {
                SqlParameter logresultpara = new SqlParameter("@LogResult", SqlDbType.VarChar, 400);
                logresultpara.Value = wf_taskoperatelog.LogResult;
                paras.Add(logresultpara);
            }


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetLogByTaskNodeIdAndEmpId(UserModel user, int taskNodeId, int empId)
        {
            ResultModel result = new ResultModel();
            SqlDataReader dr = null;

            try
            {
                string sql = string.Format("select top 1 * from dbo.Wf_TaskOperateLog where TaskNodeId = {0} and EmpId = {1}", taskNodeId, empId);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, sql, null);

                Model.TaskOperateLog model = null;

                if (dr.Read())
                {
                    model = CreateModel(dr) as Model.TaskOperateLog;

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = model;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        #endregion
    }
}
