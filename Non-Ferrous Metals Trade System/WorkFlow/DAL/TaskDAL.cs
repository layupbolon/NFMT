/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Wf_TaskDAL.cs
// 文件功能描述：任务表dbo.Wf_Task数据交互类。
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
    /// 任务表dbo.Wf_Task数据交互类。
    /// </summary>
    public class TaskDAL : ExecOperate, ITaskDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskDAL()
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
            Task wf_task = (Task)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@TaskId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter masteridpara = new SqlParameter("@MasterId", SqlDbType.Int, 4);
            masteridpara.Value = wf_task.MasterId;
            paras.Add(masteridpara);

            if (!string.IsNullOrEmpty(wf_task.TaskName))
            {
                SqlParameter tasknamepara = new SqlParameter("@TaskName", SqlDbType.VarChar, 200);
                tasknamepara.Value = wf_task.TaskName;
                paras.Add(tasknamepara);
            }

            if (!string.IsNullOrEmpty(wf_task.TaskConnext))
            {
                SqlParameter taskconnextpara = new SqlParameter("@TaskConnext", SqlDbType.VarChar, -1);
                taskconnextpara.Value = wf_task.TaskConnext;
                paras.Add(taskconnextpara);
            }

            SqlParameter taskstatuspara = new SqlParameter("@TaskStatus", SqlDbType.Int, 4);
            taskstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(taskstatuspara);

            SqlParameter datasourceidpara = new SqlParameter("@DataSourceId", SqlDbType.Int, 4);
            datasourceidpara.Value = wf_task.DataSourceId;
            paras.Add(datasourceidpara);

            SqlParameter tasktypepara = new SqlParameter("@TaskType", SqlDbType.Int, 4);
            tasktypepara.Value = wf_task.TaskType;
            paras.Add(tasktypepara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Task task = new Task();

            int indexTaskId = dr.GetOrdinal("TaskId");
            task.TaskId = Convert.ToInt32(dr[indexTaskId]);

            int indexMasterId = dr.GetOrdinal("MasterId");
            if (dr["MasterId"] != DBNull.Value)
            {
                task.MasterId = Convert.ToInt32(dr[indexMasterId]);
            }

            int indexTaskName = dr.GetOrdinal("TaskName");
            if (dr["TaskName"] != DBNull.Value)
            {
                task.TaskName = Convert.ToString(dr[indexTaskName]);
            }

            int indexTaskConnext = dr.GetOrdinal("TaskConnext");
            if (dr["TaskConnext"] != DBNull.Value)
            {
                task.TaskConnext = Convert.ToString(dr[indexTaskConnext]);
            }

            int indexTaskStatus = dr.GetOrdinal("TaskStatus");
            if (dr["TaskStatus"] != DBNull.Value)
            {
                task.TaskStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexTaskStatus]);
            }

            int indexDataSourceId = dr.GetOrdinal("DataSourceId");
            if (dr["DataSourceId"] != DBNull.Value)
            {
                task.DataSourceId = Convert.ToInt32(dr[indexDataSourceId]);
            }

            int indexTaskType = dr.GetOrdinal("TaskType");
            if (dr["TaskType"] != DBNull.Value)
            {
                task.TaskType = Convert.ToInt32(dr[indexTaskType]);
            }


            return task;
        }

        public override string TableName
        {
            get
            {
                return "Wf_Task";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Task wf_task = (Task)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter taskidpara = new SqlParameter("@TaskId", SqlDbType.Int, 4);
            taskidpara.Value = wf_task.TaskId;
            paras.Add(taskidpara);

            SqlParameter masteridpara = new SqlParameter("@MasterId", SqlDbType.Int, 4);
            masteridpara.Value = wf_task.MasterId;
            paras.Add(masteridpara);

            if (!string.IsNullOrEmpty(wf_task.TaskName))
            {
                SqlParameter tasknamepara = new SqlParameter("@TaskName", SqlDbType.VarChar, 200);
                tasknamepara.Value = wf_task.TaskName;
                paras.Add(tasknamepara);
            }

            if (!string.IsNullOrEmpty(wf_task.TaskConnext))
            {
                SqlParameter taskconnextpara = new SqlParameter("@TaskConnext", SqlDbType.VarChar, -1);
                taskconnextpara.Value = wf_task.TaskConnext;
                paras.Add(taskconnextpara);
            }

            SqlParameter taskstatuspara = new SqlParameter("@TaskStatus", SqlDbType.Int, 4);
            taskstatuspara.Value = wf_task.TaskStatus;
            paras.Add(taskstatuspara);

            SqlParameter datasourceidpara = new SqlParameter("@DataSourceId", SqlDbType.Int, 4);
            datasourceidpara.Value = wf_task.DataSourceId;
            paras.Add(datasourceidpara);

            SqlParameter tasktypepara = new SqlParameter("@TaskType", SqlDbType.Int, 4);
            tasktypepara.Value = wf_task.TaskType;
            paras.Add(tasktypepara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetTaskByDataSourceId(UserModel user, int dataSourceId)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                string sql = string.Format("select * from dbo.Wf_Task where DataSourceId = {0}", dataSourceId);

                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, sql, null);

                Task task = new Task();

                if (dr.Read())
                {
                    int indexTaskId = dr.GetOrdinal("TaskId");
                    task.TaskId = Convert.ToInt32(dr[indexTaskId]);

                    int indexMasterId = dr.GetOrdinal("MasterId");
                    if (dr["MasterId"] != DBNull.Value)
                    {
                        task.MasterId = Convert.ToInt32(dr[indexMasterId]);
                    }

                    int indexTaskName = dr.GetOrdinal("TaskName");
                    if (dr["TaskName"] != DBNull.Value)
                    {
                        task.TaskName = Convert.ToString(dr[indexTaskName]);
                    }

                    int indexTaskConnext = dr.GetOrdinal("TaskConnext");
                    if (dr["TaskConnext"] != DBNull.Value)
                    {
                        task.TaskConnext = Convert.ToString(dr[indexTaskConnext]);
                    }

                    int indexTaskStatus = dr.GetOrdinal("TaskStatus");
                    if (dr["TaskStatus"] != DBNull.Value)
                    {
                        task.TaskStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr[indexTaskStatus].ToString());
                    }

                    int indexDataSourceId = dr.GetOrdinal("DataSourceId");
                    if (dr["DataSourceId"] != DBNull.Value)
                    {
                        task.DataSourceId = Convert.ToInt32(dr[indexDataSourceId]);
                    }

                    int indexTaskType = dr.GetOrdinal("TaskType");
                    if (dr["TaskType"] != DBNull.Value)
                    {
                        task.TaskType = Convert.ToInt32(dr[indexTaskType]);
                    }

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = task;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }

            return result;
        }

        public override int MenuId
        {
            get
            {
                return 85;
            }
        }

        public ResultModel GetAuditProgress(UserModel user, string sql)
        {
            ResultModel result = new ResultModel();

            try
            {
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = dt;
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

        #endregion
    }
}
