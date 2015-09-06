/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：LcReceivableDAL.cs
// 文件功能描述：信用证收款登记dbo.Fun_LcReceivable_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Funds.Model;
using NFMT.DBUtility;
using NFMT.Funds.IDAL;
using NFMT.Common;

namespace NFMT.Funds.DAL
{
    /// <summary>
    /// 信用证收款登记dbo.Fun_LcReceivable_Ref数据交互类。
    /// </summary>
    public class LcReceivableDAL : DataOperate, ILcReceivableDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public LcReceivableDAL()
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
            LcReceivable fun_lcreceivable_ref = (LcReceivable)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter receidpara = new SqlParameter("@ReceId", SqlDbType.Int, 4);
            receidpara.Value = fun_lcreceivable_ref.ReceId;
            paras.Add(receidpara);

            SqlParameter lcidpara = new SqlParameter("@LcId", SqlDbType.Int, 4);
            lcidpara.Value = fun_lcreceivable_ref.LcId;
            paras.Add(lcidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            LcReceivable lcreceivable = new LcReceivable();

            int indexRefId = dr.GetOrdinal("RefId");
            lcreceivable.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexReceId = dr.GetOrdinal("ReceId");
            if (dr["ReceId"] != DBNull.Value)
            {
                lcreceivable.ReceId = Convert.ToInt32(dr[indexReceId]);
            }

            int indexLcId = dr.GetOrdinal("LcId");
            if (dr["LcId"] != DBNull.Value)
            {
                lcreceivable.LcId = Convert.ToInt32(dr[indexLcId]);
            }


            return lcreceivable;
        }

        public override string TableName
        {
            get
            {
                return "Fun_LcReceivable_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            LcReceivable fun_lcreceivable_ref = (LcReceivable)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = fun_lcreceivable_ref.RefId;
            paras.Add(refidpara);

            SqlParameter receidpara = new SqlParameter("@ReceId", SqlDbType.Int, 4);
            receidpara.Value = fun_lcreceivable_ref.ReceId;
            paras.Add(receidpara);

            SqlParameter lcidpara = new SqlParameter("@LcId", SqlDbType.Int, 4);
            lcidpara.Value = fun_lcreceivable_ref.LcId;
            paras.Add(lcidpara);


            return paras;
        }

        #endregion
    }
}
