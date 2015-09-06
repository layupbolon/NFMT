/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FlowMasterDAL.cs
// 文件功能描述：流程模板dbo.Wf_FlowMaster数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月5日
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
    /// 流程模板dbo.Wf_FlowMaster数据交互类。
    /// </summary>
    public class FlowMasterDAL : DataOperate, IFlowMasterDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FlowMasterDAL()
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
            FlowMaster wf_flowmaster = (FlowMaster)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@MasterId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter masternamepara = new SqlParameter("@MasterName", SqlDbType.VarChar, 200);
            masternamepara.Value = wf_flowmaster.MasterName;
            paras.Add(masternamepara);

            SqlParameter masterstatuspara = new SqlParameter("@MasterStatus", SqlDbType.Int, 4);
            masterstatuspara.Value = wf_flowmaster.MasterStatus;
            paras.Add(masterstatuspara);

            if (!string.IsNullOrEmpty(wf_flowmaster.ViewUrl))
            {
                SqlParameter viewurlpara = new SqlParameter("@ViewUrl", SqlDbType.VarChar, 200);
                viewurlpara.Value = wf_flowmaster.ViewUrl;
                paras.Add(viewurlpara);
            }

            if (!string.IsNullOrEmpty(wf_flowmaster.ConditionUrl))
            {
                SqlParameter conditionurlpara = new SqlParameter("@ConditionUrl", SqlDbType.VarChar, 200);
                conditionurlpara.Value = wf_flowmaster.ConditionUrl;
                paras.Add(conditionurlpara);
            }

            if (!string.IsNullOrEmpty(wf_flowmaster.SuccessUrl))
            {
                SqlParameter successurlpara = new SqlParameter("@SuccessUrl", SqlDbType.VarChar, 200);
                successurlpara.Value = wf_flowmaster.SuccessUrl;
                paras.Add(successurlpara);
            }

            if (!string.IsNullOrEmpty(wf_flowmaster.RefusalUrl))
            {
                SqlParameter refusalurlpara = new SqlParameter("@RefusalUrl", SqlDbType.VarChar, 200);
                refusalurlpara.Value = wf_flowmaster.RefusalUrl;
                paras.Add(refusalurlpara);
            }

            if (!string.IsNullOrEmpty(wf_flowmaster.ViewTitle))
            {
                SqlParameter viewtitlepara = new SqlParameter("@ViewTitle", SqlDbType.VarChar, 200);
                viewtitlepara.Value = wf_flowmaster.ViewTitle;
                paras.Add(viewtitlepara);
            }

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            FlowMaster flowmaster = new FlowMaster();

            flowmaster.MasterId = Convert.ToInt32(dr["MasterId"]);

            flowmaster.MasterName = Convert.ToString(dr["MasterName"]);

            flowmaster.MasterStatus = (Common.StatusEnum)Convert.ToInt32(dr["MasterStatus"]);

            if (dr["ViewUrl"] != DBNull.Value)
            {
                flowmaster.ViewUrl = Convert.ToString(dr["ViewUrl"]);
            }

            if (dr["ConditionUrl"] != DBNull.Value)
            {
                flowmaster.ConditionUrl = Convert.ToString(dr["ConditionUrl"]);
            }

            if (dr["SuccessUrl"] != DBNull.Value)
            {
                flowmaster.SuccessUrl = Convert.ToString(dr["SuccessUrl"]);
            }

            if (dr["RefusalUrl"] != DBNull.Value)
            {
                flowmaster.RefusalUrl = Convert.ToString(dr["RefusalUrl"]);
            }

            if (dr["ViewTitle"] != DBNull.Value)
            {
                flowmaster.ViewTitle = Convert.ToString(dr["ViewTitle"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                flowmaster.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                flowmaster.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                flowmaster.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                flowmaster.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return flowmaster;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            FlowMaster flowmaster = new FlowMaster();

            int indexMasterId = dr.GetOrdinal("MasterId");
            flowmaster.MasterId = Convert.ToInt32(dr[indexMasterId]);

            int indexMasterName = dr.GetOrdinal("MasterName");
            flowmaster.MasterName = Convert.ToString(dr[indexMasterName]);

            int indexMasterStatus = dr.GetOrdinal("MasterStatus");
            flowmaster.MasterStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexMasterStatus]);

            int indexViewUrl = dr.GetOrdinal("ViewUrl");
            if (dr["ViewUrl"] != DBNull.Value)
            {
                flowmaster.ViewUrl = Convert.ToString(dr[indexViewUrl]);
            }

            int indexConditionUrl = dr.GetOrdinal("ConditionUrl");
            if (dr["ConditionUrl"] != DBNull.Value)
            {
                flowmaster.ConditionUrl = Convert.ToString(dr[indexConditionUrl]);
            }

            int indexSuccessUrl = dr.GetOrdinal("SuccessUrl");
            if (dr["SuccessUrl"] != DBNull.Value)
            {
                flowmaster.SuccessUrl = Convert.ToString(dr[indexSuccessUrl]);
            }

            int indexRefusalUrl = dr.GetOrdinal("RefusalUrl");
            if (dr["RefusalUrl"] != DBNull.Value)
            {
                flowmaster.RefusalUrl = Convert.ToString(dr[indexRefusalUrl]);
            }

            int indexViewTitle = dr.GetOrdinal("ViewTitle");
            if (dr["ViewTitle"] != DBNull.Value)
            {
                flowmaster.ViewTitle = Convert.ToString(dr[indexViewTitle]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                flowmaster.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                flowmaster.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                flowmaster.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                flowmaster.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return flowmaster;
        }

        public override string TableName
        {
            get
            {
                return "Wf_FlowMaster";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            FlowMaster wf_flowmaster = (FlowMaster)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter masteridpara = new SqlParameter("@MasterId", SqlDbType.Int, 4);
            masteridpara.Value = wf_flowmaster.MasterId;
            paras.Add(masteridpara);

            SqlParameter masternamepara = new SqlParameter("@MasterName", SqlDbType.VarChar, 200);
            masternamepara.Value = wf_flowmaster.MasterName;
            paras.Add(masternamepara);

            SqlParameter masterstatuspara = new SqlParameter("@MasterStatus", SqlDbType.Int, 4);
            masterstatuspara.Value = wf_flowmaster.MasterStatus;
            paras.Add(masterstatuspara);

            if (!string.IsNullOrEmpty(wf_flowmaster.ViewUrl))
            {
                SqlParameter viewurlpara = new SqlParameter("@ViewUrl", SqlDbType.VarChar, 200);
                viewurlpara.Value = wf_flowmaster.ViewUrl;
                paras.Add(viewurlpara);
            }

            if (!string.IsNullOrEmpty(wf_flowmaster.ConditionUrl))
            {
                SqlParameter conditionurlpara = new SqlParameter("@ConditionUrl", SqlDbType.VarChar, 200);
                conditionurlpara.Value = wf_flowmaster.ConditionUrl;
                paras.Add(conditionurlpara);
            }

            if (!string.IsNullOrEmpty(wf_flowmaster.SuccessUrl))
            {
                SqlParameter successurlpara = new SqlParameter("@SuccessUrl", SqlDbType.VarChar, 200);
                successurlpara.Value = wf_flowmaster.SuccessUrl;
                paras.Add(successurlpara);
            }

            if (!string.IsNullOrEmpty(wf_flowmaster.RefusalUrl))
            {
                SqlParameter refusalurlpara = new SqlParameter("@RefusalUrl", SqlDbType.VarChar, 200);
                refusalurlpara.Value = wf_flowmaster.RefusalUrl;
                paras.Add(refusalurlpara);
            }

            if (!string.IsNullOrEmpty(wf_flowmaster.ViewTitle))
            {
                SqlParameter viewtitlepara = new SqlParameter("@ViewTitle", SqlDbType.VarChar, 200);
                viewtitlepara.Value = wf_flowmaster.ViewTitle;
                paras.Add(viewtitlepara);
            }

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
