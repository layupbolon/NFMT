/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：LcAttachDAL.cs
// 文件功能描述：信用证附件dbo.Con_LcAttach数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Contract.Model;
using NFMT.DBUtility;
using NFMT.Contract.IDAL;
using NFMT.Common;

namespace NFMT.Contract.DAL
{
    /// <summary>
    /// 信用证附件dbo.Con_LcAttach数据交互类。
    /// </summary>
    public class LcAttachDAL : DataOperate, ILcAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public LcAttachDAL()
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
            LcAttach con_lcattach = (LcAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@LcAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = con_lcattach.SubId;
            paras.Add(subidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = con_lcattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            LcAttach lcattach = new LcAttach();

            int indexLcAttachId = dr.GetOrdinal("LcAttachId");
            lcattach.LcAttachId = Convert.ToInt32(dr[indexLcAttachId]);

            int indexSubId = dr.GetOrdinal("SubId");
            if (dr["SubId"] != DBNull.Value)
            {
                lcattach.SubId = Convert.ToInt32(dr[indexSubId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                lcattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return lcattach;
        }

        public override string TableName
        {
            get
            {
                return "Con_LcAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            LcAttach con_lcattach = (LcAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter lcattachidpara = new SqlParameter("@LcAttachId", SqlDbType.Int, 4);
            lcattachidpara.Value = con_lcattach.LcAttachId;
            paras.Add(lcattachidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = con_lcattach.SubId;
            paras.Add(subidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = con_lcattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion
    }
}
