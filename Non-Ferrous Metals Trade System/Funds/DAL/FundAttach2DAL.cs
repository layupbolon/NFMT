/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FundAttach2DAL.cs
// 文件功能描述：资金附件dbo.Fun_FundAttach2数据交互类。
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
    /// 资金附件dbo.Fun_FundAttach2数据交互类。
    /// </summary>
    public class FundAttach2DAL : DataOperate, IFundAttach2DAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FundAttach2DAL()
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
            FundAttach2 fun_fundattach2 = (FundAttach2)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@FundsAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = fun_fundattach2.AttachId;
            paras.Add(attachidpara);

            SqlParameter fundsidpara = new SqlParameter("@FundsId", SqlDbType.Int, 4);
            fundsidpara.Value = fun_fundattach2.FundsId;
            paras.Add(fundsidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            FundAttach2 fundattach2 = new FundAttach2();

            int indexFundsAttachId = dr.GetOrdinal("FundsAttachId");
            fundattach2.FundsAttachId = Convert.ToInt32(dr[indexFundsAttachId]);

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                fundattach2.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }

            int indexFundsId = dr.GetOrdinal("FundsId");
            if (dr["FundsId"] != DBNull.Value)
            {
                fundattach2.FundsId = Convert.ToInt32(dr[indexFundsId]);
            }


            return fundattach2;
        }

        public override string TableName
        {
            get
            {
                return "Fun_FundAttach2";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            FundAttach2 fun_fundattach2 = (FundAttach2)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter fundsattachidpara = new SqlParameter("@FundsAttachId", SqlDbType.Int, 4);
            fundsattachidpara.Value = fun_fundattach2.FundsAttachId;
            paras.Add(fundsattachidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = fun_fundattach2.AttachId;
            paras.Add(attachidpara);

            SqlParameter fundsidpara = new SqlParameter("@FundsId", SqlDbType.Int, 4);
            fundsidpara.Value = fun_fundattach2.FundsId;
            paras.Add(fundsidpara);


            return paras;
        }

        #endregion

    }
}
