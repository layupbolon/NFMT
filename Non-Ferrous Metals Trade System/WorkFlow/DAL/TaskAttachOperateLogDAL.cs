/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：TaskAttachOperateLogDAL.cs
// 文件功能描述：任务附件操作记录表dbo.Wf_TaskAttachOperateLog数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年1月4日
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
    /// 任务附件操作记录表dbo.Wf_TaskAttachOperateLog数据交互类。
    /// </summary>
    public class TaskAttachOperateLogDAL : DataOperate, ITaskAttachOperateLogDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskAttachOperateLogDAL()
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
            TaskAttachOperateLog wf_taskattachoperatelog = (TaskAttachOperateLog)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@OperateLogId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter logidpara = new SqlParameter("@LogId", SqlDbType.Int, 4);
            logidpara.Value = wf_taskattachoperatelog.LogId;
            paras.Add(logidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = wf_taskattachoperatelog.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            TaskAttachOperateLog taskattachoperatelog = new TaskAttachOperateLog();

            taskattachoperatelog.OperateLogId = Convert.ToInt32(dr["OperateLogId"]);

            taskattachoperatelog.LogId = Convert.ToInt32(dr["LogId"]);

            if (dr["AttachId"] != DBNull.Value)
            {
                taskattachoperatelog.AttachId = Convert.ToInt32(dr["AttachId"]);
            }


            return taskattachoperatelog;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            TaskAttachOperateLog taskattachoperatelog = new TaskAttachOperateLog();

            int indexOperateLogId = dr.GetOrdinal("OperateLogId");
            taskattachoperatelog.OperateLogId = Convert.ToInt32(dr[indexOperateLogId]);

            int indexLogId = dr.GetOrdinal("LogId");
            taskattachoperatelog.LogId = Convert.ToInt32(dr[indexLogId]);

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                taskattachoperatelog.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return taskattachoperatelog;
        }

        public override string TableName
        {
            get
            {
                return "Wf_TaskAttachOperateLog";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            TaskAttachOperateLog wf_taskattachoperatelog = (TaskAttachOperateLog)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter operatelogidpara = new SqlParameter("@OperateLogId", SqlDbType.Int, 4);
            operatelogidpara.Value = wf_taskattachoperatelog.OperateLogId;
            paras.Add(operatelogidpara);

            SqlParameter logidpara = new SqlParameter("@LogId", SqlDbType.Int, 4);
            logidpara.Value = wf_taskattachoperatelog.LogId;
            paras.Add(logidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = wf_taskattachoperatelog.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion
    }
}
