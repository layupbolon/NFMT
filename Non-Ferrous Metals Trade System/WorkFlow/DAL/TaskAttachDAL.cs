/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：TaskAttachDAL.cs
// 文件功能描述：任务附件dbo.Wf_TaskAttach数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年12月31日
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
    /// 任务附件dbo.Wf_TaskAttach数据交互类。
    /// </summary>
    public class TaskAttachDAL : DataOperate, ITaskAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskAttachDAL()
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
            TaskAttach wf_taskattach = (TaskAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@TaskAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter taskidpara = new SqlParameter("@TaskId", SqlDbType.Int, 4);
            taskidpara.Value = wf_taskattach.TaskId;
            paras.Add(taskidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = wf_taskattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            TaskAttach taskattach = new TaskAttach();

            taskattach.TaskAttachId = Convert.ToInt32(dr["TaskAttachId"]);

            if (dr["TaskId"] != DBNull.Value)
            {
                taskattach.TaskId = Convert.ToInt32(dr["TaskId"]);
            }

            if (dr["AttachId"] != DBNull.Value)
            {
                taskattach.AttachId = Convert.ToInt32(dr["AttachId"]);
            }


            return taskattach;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            TaskAttach taskattach = new TaskAttach();

            int indexTaskAttachId = dr.GetOrdinal("TaskAttachId");
            taskattach.TaskAttachId = Convert.ToInt32(dr[indexTaskAttachId]);

            int indexTaskId = dr.GetOrdinal("TaskId");
            if (dr["TaskId"] != DBNull.Value)
            {
                taskattach.TaskId = Convert.ToInt32(dr[indexTaskId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                taskattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return taskattach;
        }

        public override string TableName
        {
            get
            {
                return "Wf_TaskAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            TaskAttach wf_taskattach = (TaskAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter taskattachidpara = new SqlParameter("@TaskAttachId", SqlDbType.Int, 4);
            taskattachidpara.Value = wf_taskattach.TaskAttachId;
            paras.Add(taskattachidpara);

            SqlParameter taskidpara = new SqlParameter("@TaskId", SqlDbType.Int, 4);
            taskidpara.Value = wf_taskattach.TaskId;
            paras.Add(taskidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = wf_taskattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetTaskAttachByTaskId(UserModel user, int taskId)
        {
            string cmdText = string.Format("select * from NFMT_WorkFlow.dbo.Wf_TaskAttach where TaskId = {0}", taskId);
            return Load<Model.TaskAttach>(user, CommandType.Text, cmdText);
        }

        public ResultModel JudgeAllAttachsAudited(UserModel user, int taskId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select * from dbo.Wf_TaskAttach ta left join dbo.Wf_TaskAttachOperateLog taol on ta.AttachId = taol.AttachId where TaskId = {0} and ISNULL(taol.OperateLogId,0) = 0", taskId);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "该审核任务下还有未审核的附件，请先审核附件";
                }
                else
                {
                    result.ResultStatus = 0;
                    result.Message = "该审核任务下的审核附件全部都被审核";
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
