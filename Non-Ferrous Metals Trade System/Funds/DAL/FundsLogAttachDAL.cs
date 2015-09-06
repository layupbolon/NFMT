/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FundsLogAttachDAL.cs
// 文件功能描述：资金流水附件dbo.Fun_FundsLogAttach数据交互类。
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
    /// 资金流水附件dbo.Fun_FundsLogAttach数据交互类。
    /// </summary>
    public class FundsLogAttachDAL : DataOperate, IFundsLogAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FundsLogAttachDAL()
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
            FundsLogAttach fun_fundslogattach = (FundsLogAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@FundsLogAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = fun_fundslogattach.AttachId;
            paras.Add(attachidpara);

            SqlParameter fundslogidpara = new SqlParameter("@FundsLogId", SqlDbType.Int, 4);
            fundslogidpara.Value = fun_fundslogattach.FundsLogId;
            paras.Add(fundslogidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            FundsLogAttach fundslogattach = new FundsLogAttach();

            int indexFundsLogAttachId = dr.GetOrdinal("FundsLogAttachId");
            fundslogattach.FundsLogAttachId = Convert.ToInt32(dr[indexFundsLogAttachId]);

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                fundslogattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }

            int indexFundsLogId = dr.GetOrdinal("FundsLogId");
            if (dr["FundsLogId"] != DBNull.Value)
            {
                fundslogattach.FundsLogId = Convert.ToInt32(dr[indexFundsLogId]);
            }


            return fundslogattach;
        }

        public override string TableName
        {
            get
            {
                return "Fun_FundsLogAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            FundsLogAttach fun_fundslogattach = (FundsLogAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter fundslogattachidpara = new SqlParameter("@FundsLogAttachId", SqlDbType.Int, 4);
            fundslogattachidpara.Value = fun_fundslogattach.FundsLogAttachId;
            paras.Add(fundslogattachidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = fun_fundslogattach.AttachId;
            paras.Add(attachidpara);

            SqlParameter fundslogidpara = new SqlParameter("@FundsLogId", SqlDbType.Int, 4);
            fundslogidpara.Value = fun_fundslogattach.FundsLogId;
            paras.Add(fundslogidpara);


            return paras;
        }

        #endregion

    }
}
