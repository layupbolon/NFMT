/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ParameterDAL.cs
// 文件功能描述：参数表dbo.Parameter数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年7月1日
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
    /// 参数表dbo.Parameter数据交互类。
    /// </summary>
    public partial class ParameterDAL : DataOperate, IParameterDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ParameterDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringBasic;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            Parameter parameter = (Parameter)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@Id";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(parameter.ParaName))
            {
                SqlParameter paranamepara = new SqlParameter("@ParaName", SqlDbType.VarChar, 200);
                paranamepara.Value = parameter.ParaName;
                paras.Add(paranamepara);
            }

            if (!string.IsNullOrEmpty(parameter.ParaValue))
            {
                SqlParameter paravaluepara = new SqlParameter("@ParaValue", SqlDbType.VarChar, 200);
                paravaluepara.Value = parameter.ParaValue;
                paras.Add(paravaluepara);
            }

            SqlParameter parastatuspara = new SqlParameter("@ParaStatus", SqlDbType.Int, 4);
            parastatuspara.Value = parameter.ParaStatus;
            paras.Add(parastatuspara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            Parameter parameter = new Parameter();

            parameter.Id = Convert.ToInt32(dr["Id"]);

            if (dr["ParaName"] != DBNull.Value)
            {
                parameter.ParaName = Convert.ToString(dr["ParaName"]);
            }

            if (dr["ParaValue"] != DBNull.Value)
            {
                parameter.ParaValue = Convert.ToString(dr["ParaValue"]);
            }

            if (dr["ParaStatus"] != DBNull.Value)
            {
                parameter.ParaStatus = (Common.StatusEnum)Convert.ToInt32(dr["ParaStatus"]);
            }


            return parameter;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Parameter parameter = new Parameter();

            int indexId = dr.GetOrdinal("Id");
            parameter.Id = Convert.ToInt32(dr[indexId]);

            int indexParaName = dr.GetOrdinal("ParaName");
            if (dr["ParaName"] != DBNull.Value)
            {
                parameter.ParaName = Convert.ToString(dr[indexParaName]);
            }

            int indexParaValue = dr.GetOrdinal("ParaValue");
            if (dr["ParaValue"] != DBNull.Value)
            {
                parameter.ParaValue = Convert.ToString(dr[indexParaValue]);
            }

            int indexParaStatus = dr.GetOrdinal("ParaStatus");
            if (dr["ParaStatus"] != DBNull.Value)
            {
                parameter.ParaStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexParaStatus]);
            }


            return parameter;
        }

        public override string TableName
        {
            get
            {
                return "Parameter";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Parameter parameter = (Parameter)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter idpara = new SqlParameter("@Id", SqlDbType.Int, 4);
            idpara.Value = parameter.Id;
            paras.Add(idpara);

            if (!string.IsNullOrEmpty(parameter.ParaName))
            {
                SqlParameter paranamepara = new SqlParameter("@ParaName", SqlDbType.VarChar, 200);
                paranamepara.Value = parameter.ParaName;
                paras.Add(paranamepara);
            }

            if (!string.IsNullOrEmpty(parameter.ParaValue))
            {
                SqlParameter paravaluepara = new SqlParameter("@ParaValue", SqlDbType.VarChar, 200);
                paravaluepara.Value = parameter.ParaValue;
                paras.Add(paravaluepara);
            }

            SqlParameter parastatuspara = new SqlParameter("@ParaStatus", SqlDbType.Int, 4);
            parastatuspara.Value = parameter.ParaStatus;
            paras.Add(parastatuspara);


            return paras;
        }

        #endregion
    }
}
