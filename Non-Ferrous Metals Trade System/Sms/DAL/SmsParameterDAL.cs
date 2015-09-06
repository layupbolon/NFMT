/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsParameterDAL.cs
// 文件功能描述：消息类型构造参数dbo.Sm_SmsParameter数据交互类。
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
    /// 消息类型构造参数dbo.Sm_SmsParameter数据交互类。
    /// </summary>
    public class SmsParameterDAL : DataOperate, ISmsParameterDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SmsParameterDAL()
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
            SmsParameter sm_smsparameter = (SmsParameter)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ParameterId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter smstypeidpara = new SqlParameter("@SmsTypeId", SqlDbType.Int, 4);
            smstypeidpara.Value = sm_smsparameter.SmsTypeId;
            paras.Add(smstypeidpara);

            if (!string.IsNullOrEmpty(sm_smsparameter.ParameterType))
            {
                SqlParameter parametertypepara = new SqlParameter("@ParameterType", SqlDbType.VarChar, 20);
                parametertypepara.Value = sm_smsparameter.ParameterType;
                paras.Add(parametertypepara);
            }

            if (!string.IsNullOrEmpty(sm_smsparameter.ParamterValue))
            {
                SqlParameter paramtervaluepara = new SqlParameter("@ParamterValue", SqlDbType.VarChar, 50);
                paramtervaluepara.Value = sm_smsparameter.ParamterValue;
                paras.Add(paramtervaluepara);
            }

            SqlParameter parameterstatuspara = new SqlParameter("@ParameterStatus", SqlDbType.Int, 4);
            parameterstatuspara.Value = sm_smsparameter.ParameterStatus;
            paras.Add(parameterstatuspara);

            SqlParameter istypepara = new SqlParameter("@IsType", SqlDbType.Bit, 1);
            istypepara.Value = sm_smsparameter.IsType;
            paras.Add(istypepara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            SmsParameter smsparameter = new SmsParameter();

            int indexParameterId = dr.GetOrdinal("ParameterId");
            smsparameter.ParameterId = Convert.ToInt32(dr[indexParameterId]);

            int indexSmsTypeId = dr.GetOrdinal("SmsTypeId");
            if (dr["SmsTypeId"] != DBNull.Value)
            {
                smsparameter.SmsTypeId = Convert.ToInt32(dr[indexSmsTypeId]);
            }

            int indexParameterType = dr.GetOrdinal("ParameterType");
            if (dr["ParameterType"] != DBNull.Value)
            {
                smsparameter.ParameterType = Convert.ToString(dr[indexParameterType]);
            }

            int indexParamterValue = dr.GetOrdinal("ParamterValue");
            if (dr["ParamterValue"] != DBNull.Value)
            {
                smsparameter.ParamterValue = Convert.ToString(dr[indexParamterValue]);
            }

            int indexParameterStatus = dr.GetOrdinal("ParameterStatus");
            if (dr["ParameterStatus"] != DBNull.Value)
            {
                smsparameter.ParameterStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexParameterStatus]);
            }

            int indexIsType = dr.GetOrdinal("IsType");
            if (dr["IsType"] != DBNull.Value)
            {
                smsparameter.IsType = Convert.ToBoolean(dr[indexIsType]);
            }


            return smsparameter;
        }

        public override string TableName
        {
            get
            {
                return "Sm_SmsParameter";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SmsParameter sm_smsparameter = (SmsParameter)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter parameteridpara = new SqlParameter("@ParameterId", SqlDbType.Int, 4);
            parameteridpara.Value = sm_smsparameter.ParameterId;
            paras.Add(parameteridpara);

            SqlParameter smstypeidpara = new SqlParameter("@SmsTypeId", SqlDbType.Int, 4);
            smstypeidpara.Value = sm_smsparameter.SmsTypeId;
            paras.Add(smstypeidpara);

            if (!string.IsNullOrEmpty(sm_smsparameter.ParameterType))
            {
                SqlParameter parametertypepara = new SqlParameter("@ParameterType", SqlDbType.VarChar, 20);
                parametertypepara.Value = sm_smsparameter.ParameterType;
                paras.Add(parametertypepara);
            }

            if (!string.IsNullOrEmpty(sm_smsparameter.ParamterValue))
            {
                SqlParameter paramtervaluepara = new SqlParameter("@ParamterValue", SqlDbType.VarChar, 50);
                paramtervaluepara.Value = sm_smsparameter.ParamterValue;
                paras.Add(paramtervaluepara);
            }

            SqlParameter parameterstatuspara = new SqlParameter("@ParameterStatus", SqlDbType.Int, 4);
            parameterstatuspara.Value = sm_smsparameter.ParameterStatus;
            paras.Add(parameterstatuspara);

            SqlParameter istypepara = new SqlParameter("@IsType", SqlDbType.Bit, 1);
            istypepara.Value = sm_smsparameter.IsType;
            paras.Add(istypepara);


            return paras;
        }

        #endregion
    }
}
