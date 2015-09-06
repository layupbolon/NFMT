/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：TaskOperateDAL.cs
// 文件功能描述：任务操作表dbo.Wf_TaskOperate数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年4月17日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.WorkFlow.Model;
using NFMT.DBUtility;
using NFMT.WorkFlow.IDAL;
using NFMT.Common;
using System.Linq;

namespace NFMT.WorkFlow.DAL
{
    /// <summary>
    /// 任务操作表dbo.Wf_TaskOperate数据交互类。
    /// </summary>
    public partial class TaskOperateDAL : DataOperate, ITaskOperateDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskOperateDAL()
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
            TaskOperate wf_taskoperate = (TaskOperate)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@TaskOperateId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter tasknodeidpara = new SqlParameter("@TaskNodeId", SqlDbType.Int, 4);
            tasknodeidpara.Value = wf_taskoperate.TaskNodeId;
            paras.Add(tasknodeidpara);

            if (!string.IsNullOrEmpty(wf_taskoperate.OperateUrl))
            {
                SqlParameter operateurlpara = new SqlParameter("@OperateUrl", SqlDbType.VarChar, 200);
                operateurlpara.Value = wf_taskoperate.OperateUrl;
                paras.Add(operateurlpara);
            }

            SqlParameter operatestatuspara = new SqlParameter("@OperateStatus", SqlDbType.Int, 4);
            operatestatuspara.Value = wf_taskoperate.OperateStatus;
            paras.Add(operatestatuspara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            TaskOperate taskoperate = new TaskOperate();

            taskoperate.TaskOperateId = Convert.ToInt32(dr["TaskOperateId"]);

            if (dr["TaskNodeId"] != DBNull.Value)
            {
                taskoperate.TaskNodeId = Convert.ToInt32(dr["TaskNodeId"]);
            }

            if (dr["OperateUrl"] != DBNull.Value)
            {
                taskoperate.OperateUrl = Convert.ToString(dr["OperateUrl"]);
            }

            if (dr["OperateStatus"] != DBNull.Value)
            {
                taskoperate.OperateStatus = (Common.StatusEnum)Convert.ToInt32(dr["OperateStatus"]);
            }


            return taskoperate;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            TaskOperate taskoperate = new TaskOperate();

            int indexTaskOperateId = dr.GetOrdinal("TaskOperateId");
            taskoperate.TaskOperateId = Convert.ToInt32(dr[indexTaskOperateId]);

            int indexTaskNodeId = dr.GetOrdinal("TaskNodeId");
            if (dr["TaskNodeId"] != DBNull.Value)
            {
                taskoperate.TaskNodeId = Convert.ToInt32(dr[indexTaskNodeId]);
            }

            int indexOperateUrl = dr.GetOrdinal("OperateUrl");
            if (dr["OperateUrl"] != DBNull.Value)
            {
                taskoperate.OperateUrl = Convert.ToString(dr[indexOperateUrl]);
            }

            int indexOperateStatus = dr.GetOrdinal("OperateStatus");
            if (dr["OperateStatus"] != DBNull.Value)
            {
                taskoperate.OperateStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexOperateStatus]);
            }


            return taskoperate;
        }

        public override string TableName
        {
            get
            {
                return "Wf_TaskOperate";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            TaskOperate wf_taskoperate = (TaskOperate)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter taskoperateidpara = new SqlParameter("@TaskOperateId", SqlDbType.Int, 4);
            taskoperateidpara.Value = wf_taskoperate.TaskOperateId;
            paras.Add(taskoperateidpara);

            SqlParameter tasknodeidpara = new SqlParameter("@TaskNodeId", SqlDbType.Int, 4);
            tasknodeidpara.Value = wf_taskoperate.TaskNodeId;
            paras.Add(tasknodeidpara);

            if (!string.IsNullOrEmpty(wf_taskoperate.OperateUrl))
            {
                SqlParameter operateurlpara = new SqlParameter("@OperateUrl", SqlDbType.VarChar, 200);
                operateurlpara.Value = wf_taskoperate.OperateUrl;
                paras.Add(operateurlpara);
            }

            SqlParameter operatestatuspara = new SqlParameter("@OperateStatus", SqlDbType.Int, 4);
            operatestatuspara.Value = wf_taskoperate.OperateStatus;
            paras.Add(operatestatuspara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetByTaskNodeId(UserModel user, int taskNodeId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select * from dbo.Wf_TaskOperate where TaskNodeId = {0} and OperateStatus = {1}", taskNodeId, (int)Common.StatusEnum.已生效);
                result = Load<Model.TaskOperate>(user, CommandType.Text, sql);
                if (result.AffectCount > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    List<Model.TaskOperate> taskOperates = result.ReturnValue as List<Model.TaskOperate>;
                    result.ReturnValue = taskOperates.First();
                }
                else
                {
                    result.ResultStatus = -1;
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

        #endregion
    }
}
