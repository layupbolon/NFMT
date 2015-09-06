/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoApplyAttachDAL.cs
// 文件功能描述：回购申请附件dbo.RepoApplyAttach数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.WareHouse.Model;
using NFMT.DBUtility;
using NFMT.WareHouse.IDAL;
using NFMT.Common;

namespace NFMT.WareHouse.DAL
{
    /// <summary>
    /// 回购申请附件dbo.RepoApplyAttach数据交互类。
    /// </summary>
    public class RepoApplyAttachDAL : DataOperate, IRepoApplyAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepoApplyAttachDAL()
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
            RepoApplyAttach st_repoapplyattach = (RepoApplyAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RepoApplyAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter repoapplyidpara = new SqlParameter("@RepoApplyId", SqlDbType.Int, 4);
            repoapplyidpara.Value = st_repoapplyattach.RepoApplyId;
            paras.Add(repoapplyidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_repoapplyattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            RepoApplyAttach repoapplyattach = new RepoApplyAttach();

            int indexRepoApplyAttachId = dr.GetOrdinal("RepoApplyAttachId");
            repoapplyattach.RepoApplyAttachId = Convert.ToInt32(dr[indexRepoApplyAttachId]);

            int indexRepoApplyId = dr.GetOrdinal("RepoApplyId");
            if (dr["RepoApplyId"] != DBNull.Value)
            {
                repoapplyattach.RepoApplyId = Convert.ToInt32(dr[indexRepoApplyId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                repoapplyattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return repoapplyattach;
        }

        public override string TableName
        {
            get
            {
                return "St_RepoApplyAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            RepoApplyAttach st_repoapplyattach = (RepoApplyAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter repoapplyattachidpara = new SqlParameter("@RepoApplyAttachId", SqlDbType.Int, 4);
            repoapplyattachidpara.Value = st_repoapplyattach.RepoApplyAttachId;
            paras.Add(repoapplyattachidpara);

            SqlParameter repoapplyidpara = new SqlParameter("@RepoApplyId", SqlDbType.Int, 4);
            repoapplyidpara.Value = st_repoapplyattach.RepoApplyId;
            paras.Add(repoapplyidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_repoapplyattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion
    }
}
