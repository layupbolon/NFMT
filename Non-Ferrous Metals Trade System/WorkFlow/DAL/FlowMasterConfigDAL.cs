/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FlowMasterConfigDAL.cs
// 文件功能描述：流程模版配置表dbo.Wf_FlowMasterConfig数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年4月23日
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
    /// 流程模版配置表dbo.Wf_FlowMasterConfig数据交互类。
    /// </summary>
    public partial class FlowMasterConfigDAL : DataOperate, IFlowMasterConfigDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FlowMasterConfigDAL()
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
            FlowMasterConfig wf_flowmasterconfig = (FlowMasterConfig)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ConfigId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter masteridpara = new SqlParameter("@MasterId", SqlDbType.Int, 4);
            masteridpara.Value = wf_flowmasterconfig.MasterId;
            paras.Add(masteridpara);

            SqlParameter canpassauditpara = new SqlParameter("@CanPassAudit", SqlDbType.Bit, 1);
            canpassauditpara.Value = wf_flowmasterconfig.CanPassAudit;
            paras.Add(canpassauditpara);

            SqlParameter isseriespara = new SqlParameter("@IsSeries", SqlDbType.Bit, 1);
            isseriespara.Value = wf_flowmasterconfig.IsSeries;
            paras.Add(isseriespara);

            if (!string.IsNullOrEmpty(wf_flowmasterconfig.RefuseUrl))
            {
                SqlParameter refuseurlpara = new SqlParameter("@RefuseUrl", SqlDbType.VarChar, 4000);
                refuseurlpara.Value = wf_flowmasterconfig.RefuseUrl;
                paras.Add(refuseurlpara);
            }

            SqlParameter configstatuspara = new SqlParameter("@ConfigStatus", SqlDbType.Int, 4);
            configstatuspara.Value = wf_flowmasterconfig.ConfigStatus;
            paras.Add(configstatuspara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            FlowMasterConfig flowmasterconfig = new FlowMasterConfig();

            flowmasterconfig.ConfigId = Convert.ToInt32(dr["ConfigId"]);

            if (dr["MasterId"] != DBNull.Value)
            {
                flowmasterconfig.MasterId = Convert.ToInt32(dr["MasterId"]);
            }

            if (dr["CanPassAudit"] != DBNull.Value)
            {
                flowmasterconfig.CanPassAudit = Convert.ToBoolean(dr["CanPassAudit"]);
            }

            if (dr["IsSeries"] != DBNull.Value)
            {
                flowmasterconfig.IsSeries = Convert.ToBoolean(dr["IsSeries"]);
            }

            if (dr["RefuseUrl"] != DBNull.Value)
            {
                flowmasterconfig.RefuseUrl = Convert.ToString(dr["RefuseUrl"]);
            }

            if (dr["ConfigStatus"] != DBNull.Value)
            {
                flowmasterconfig.ConfigStatus = (Common.StatusEnum)Convert.ToInt32(dr["ConfigStatus"]);
            }


            return flowmasterconfig;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            FlowMasterConfig flowmasterconfig = new FlowMasterConfig();

            int indexConfigId = dr.GetOrdinal("ConfigId");
            flowmasterconfig.ConfigId = Convert.ToInt32(dr[indexConfigId]);

            int indexMasterId = dr.GetOrdinal("MasterId");
            if (dr["MasterId"] != DBNull.Value)
            {
                flowmasterconfig.MasterId = Convert.ToInt32(dr[indexMasterId]);
            }

            int indexCanPassAudit = dr.GetOrdinal("CanPassAudit");
            if (dr["CanPassAudit"] != DBNull.Value)
            {
                flowmasterconfig.CanPassAudit = Convert.ToBoolean(dr[indexCanPassAudit]);
            }

            int indexIsSeries = dr.GetOrdinal("IsSeries");
            if (dr["IsSeries"] != DBNull.Value)
            {
                flowmasterconfig.IsSeries = Convert.ToBoolean(dr[indexIsSeries]);
            }

            int indexRefuseUrl = dr.GetOrdinal("RefuseUrl");
            if (dr["RefuseUrl"] != DBNull.Value)
            {
                flowmasterconfig.RefuseUrl = Convert.ToString(dr[indexRefuseUrl]);
            }

            int indexConfigStatus = dr.GetOrdinal("ConfigStatus");
            if (dr["ConfigStatus"] != DBNull.Value)
            {
                flowmasterconfig.ConfigStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexConfigStatus]);
            }


            return flowmasterconfig;
        }

        public override string TableName
        {
            get
            {
                return "Wf_FlowMasterConfig";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            FlowMasterConfig wf_flowmasterconfig = (FlowMasterConfig)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter configidpara = new SqlParameter("@ConfigId", SqlDbType.Int, 4);
            configidpara.Value = wf_flowmasterconfig.ConfigId;
            paras.Add(configidpara);

            SqlParameter masteridpara = new SqlParameter("@MasterId", SqlDbType.Int, 4);
            masteridpara.Value = wf_flowmasterconfig.MasterId;
            paras.Add(masteridpara);

            SqlParameter canpassauditpara = new SqlParameter("@CanPassAudit", SqlDbType.Bit, 1);
            canpassauditpara.Value = wf_flowmasterconfig.CanPassAudit;
            paras.Add(canpassauditpara);

            SqlParameter isseriespara = new SqlParameter("@IsSeries", SqlDbType.Bit, 1);
            isseriespara.Value = wf_flowmasterconfig.IsSeries;
            paras.Add(isseriespara);

            if (!string.IsNullOrEmpty(wf_flowmasterconfig.RefuseUrl))
            {
                SqlParameter refuseurlpara = new SqlParameter("@RefuseUrl", SqlDbType.VarChar, 4000);
                refuseurlpara.Value = wf_flowmasterconfig.RefuseUrl;
                paras.Add(refuseurlpara);
            }

            SqlParameter configstatuspara = new SqlParameter("@ConfigStatus", SqlDbType.Int, 4);
            configstatuspara.Value = wf_flowmasterconfig.ConfigStatus;
            paras.Add(configstatuspara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetByMasterId(UserModel user, int masterId)
        {
            string sql = string.Format("select * from dbo.Wf_FlowMasterConfig with (nolock) where MasterId = {0} and ConfigStatus ={1}", masterId, (int)Common.StatusEnum.已生效);
            ResultModel result = Load<Model.FlowMasterConfig>(user, CommandType.Text, sql);
            if (result.AffectCount > 0)
            {
                List<Model.FlowMasterConfig> configs = result.ReturnValue as List<Model.FlowMasterConfig>;
                if (configs == null || !configs.Any())
                {
                    result.Message = "获取失败";
                    result.ResultStatus = -1;
                }
                else
                {
                    result.Message = "获取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = configs.First();
                }
            }
            else
            {
                result.Message = "不存在流程模版配置";
                result.ResultStatus = -1;
            }
            return result;
        }


        #endregion
    }
}
