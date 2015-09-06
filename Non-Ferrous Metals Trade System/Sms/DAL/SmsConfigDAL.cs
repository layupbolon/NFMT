/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsConfigDAL.cs
// 文件功能描述：消息配置dbo.Sm_SmsConfig数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年6月27日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Sms.Model;
using NFMT.DBUtility;
using NFMT.Sms.IDAL;
using NFMT.Common;

namespace NFMT.Sms.DAL
{
    /// <summary>
    /// 消息配置dbo.Sm_SmsConfig数据交互类。
    /// </summary>
    public class SmsConfigDAL : DataOperate, ISmsConfigDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SmsConfigDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringSms;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            SmsConfig sm_smsconfig = (SmsConfig)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@SmsConfigId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(sm_smsconfig.EmpId))
            {
                SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.VarChar, 80);
                empidpara.Value = sm_smsconfig.EmpId;
                paras.Add(empidpara);
            }

            SqlParameter configstatuspara = new SqlParameter("@ConfigStatus", SqlDbType.Int, 4);
            configstatuspara.Value = sm_smsconfig.ConfigStatus;
            paras.Add(configstatuspara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            SmsConfig smsconfig = new SmsConfig();

            int indexSmsConfigId = dr.GetOrdinal("SmsConfigId");
            smsconfig.SmsConfigId = Convert.ToInt32(dr[indexSmsConfigId]);

            int indexEmpId = dr.GetOrdinal("EmpId");
            if (dr["EmpId"] != DBNull.Value)
            {
                smsconfig.EmpId = Convert.ToString(dr[indexEmpId]);
            }

            int indexConfigStatus = dr.GetOrdinal("ConfigStatus");
            if (dr["ConfigStatus"] != DBNull.Value)
            {
                smsconfig.ConfigStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexConfigStatus]);
            }


            return smsconfig;
        }

        public override string TableName
        {
            get
            {
                return "Sm_SmsConfig";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SmsConfig sm_smsconfig = (SmsConfig)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter smsconfigidpara = new SqlParameter("@SmsConfigId", SqlDbType.Int, 4);
            smsconfigidpara.Value = sm_smsconfig.SmsConfigId;
            paras.Add(smsconfigidpara);

            if (!string.IsNullOrEmpty(sm_smsconfig.EmpId))
            {
                SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.VarChar, 80);
                empidpara.Value = sm_smsconfig.EmpId;
                paras.Add(empidpara);
            }

            SqlParameter configstatuspara = new SqlParameter("@ConfigStatus", SqlDbType.Int, 4);
            configstatuspara.Value = sm_smsconfig.ConfigStatus;
            paras.Add(configstatuspara);


            return paras;
        }

        #endregion
    }
}
