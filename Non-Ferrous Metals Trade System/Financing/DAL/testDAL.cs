/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：testDAL.cs
// 文件功能描述：dbo.test数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年4月17日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Financing.Model;
using NFMT.DBUtility;
using NFMT.Financing.IDAL;
using NFMT.Common;

namespace NFMT.Financing.DAL
{
    /// <summary>
    /// dbo.test数据交互类。
    /// </summary>
    public partial class testDAL : DataOperate, ItestDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public testDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringNFMT;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            test test = (test)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@id";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(test.str1))
            {
                SqlParameter str1para = new SqlParameter("@str1", SqlDbType.VarChar, 20);
                str1para.Value = test.str1;
                paras.Add(str1para);
            }

            if (!string.IsNullOrEmpty(test.str2))
            {
                SqlParameter str2para = new SqlParameter("@str2", SqlDbType.VarChar, 20);
                str2para.Value = test.str2;
                paras.Add(str2para);
            }

            SqlParameter statuspara = new SqlParameter("@status", SqlDbType.Int, 4);
            statuspara.Value = test.status;
            paras.Add(statuspara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            test test = new test();

            test.id = Convert.ToInt32(dr["id"]);

            if (dr["str1"] != DBNull.Value)
            {
                test.str1 = Convert.ToString(dr["str1"]);
            }

            if (dr["str2"] != DBNull.Value)
            {
                test.str2 = Convert.ToString(dr["str2"]);
            }

            if (dr["status"] != DBNull.Value)
            {
                test.status = (Common.StatusEnum)Convert.ToInt32(dr["status"]);
            }


            return test;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            test test = new test();

            int indexid = dr.GetOrdinal("id");
            test.id = Convert.ToInt32(dr[indexid]);

            int indexstr1 = dr.GetOrdinal("str1");
            if (dr["str1"] != DBNull.Value)
            {
                test.str1 = Convert.ToString(dr[indexstr1]);
            }

            int indexstr2 = dr.GetOrdinal("str2");
            if (dr["str2"] != DBNull.Value)
            {
                test.str2 = Convert.ToString(dr[indexstr2]);
            }

            int indexstatus = dr.GetOrdinal("status");
            if (dr["status"] != DBNull.Value)
            {
                test.status = (Common.StatusEnum)Convert.ToInt32(dr[indexstatus]);
            }


            return test;
        }

        public override string TableName
        {
            get
            {
                return "test";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            test test = (test)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter idpara = new SqlParameter("@id", SqlDbType.Int, 4);
            idpara.Value = test.id;
            paras.Add(idpara);

            if (!string.IsNullOrEmpty(test.str1))
            {
                SqlParameter str1para = new SqlParameter("@str1", SqlDbType.VarChar, 20);
                str1para.Value = test.str1;
                paras.Add(str1para);
            }

            if (!string.IsNullOrEmpty(test.str2))
            {
                SqlParameter str2para = new SqlParameter("@str2", SqlDbType.VarChar, 20);
                str2para.Value = test.str2;
                paras.Add(str2para);
            }

            SqlParameter statuspara = new SqlParameter("@status", SqlDbType.Int, 4);
            statuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(statuspara);


            return paras;
        }

        #endregion
    }
}
