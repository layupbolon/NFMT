/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ReceivableAttachDAL.cs
// 文件功能描述：收款登记附件dbo.Fun_ReceivableAttach数据交互类。
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
    /// 收款登记附件dbo.Fun_ReceivableAttach数据交互类。
    /// </summary>
    public class ReceivableAttachDAL : DataOperate, IReceivableAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReceivableAttachDAL()
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
            ReceivableAttach fun_receivableattach = (ReceivableAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ReceivableAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = fun_receivableattach.AttachId;
            paras.Add(attachidpara);

            SqlParameter receivableidpara = new SqlParameter("@ReceivableId", SqlDbType.Int, 4);
            receivableidpara.Value = fun_receivableattach.ReceivableId;
            paras.Add(receivableidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ReceivableAttach receivableattach = new ReceivableAttach();

            int indexReceivableAttachId = dr.GetOrdinal("ReceivableAttachId");
            receivableattach.ReceivableAttachId = Convert.ToInt32(dr[indexReceivableAttachId]);

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                receivableattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }

            int indexReceivableId = dr.GetOrdinal("ReceivableId");
            if (dr["ReceivableId"] != DBNull.Value)
            {
                receivableattach.ReceivableId = Convert.ToInt32(dr[indexReceivableId]);
            }


            return receivableattach;
        }

        public override string TableName
        {
            get
            {
                return "Fun_ReceivableAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ReceivableAttach fun_receivableattach = (ReceivableAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter receivableattachidpara = new SqlParameter("@ReceivableAttachId", SqlDbType.Int, 4);
            receivableattachidpara.Value = fun_receivableattach.ReceivableAttachId;
            paras.Add(receivableattachidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = fun_receivableattach.AttachId;
            paras.Add(attachidpara);

            SqlParameter receivableidpara = new SqlParameter("@ReceivableId", SqlDbType.Int, 4);
            receivableidpara.Value = fun_receivableattach.ReceivableId;
            paras.Add(receivableidpara);


            return paras;
        }

        #endregion
    }
}
