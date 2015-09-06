/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoAttachDAL.cs
// 文件功能描述：回购附件dbo.RepoAttach数据交互类。
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
    /// 回购附件dbo.RepoAttach数据交互类。
    /// </summary>
    public class RepoAttachDAL : DataOperate, IRepoAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepoAttachDAL()
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
            RepoAttach st_repoattach = (RepoAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RepoAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter repoidpara = new SqlParameter("@RepoId", SqlDbType.Int, 4);
            repoidpara.Value = st_repoattach.RepoId;
            paras.Add(repoidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_repoattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            RepoAttach repoattach = new RepoAttach();

            int indexRepoAttachId = dr.GetOrdinal("RepoAttachId");
            repoattach.RepoAttachId = Convert.ToInt32(dr[indexRepoAttachId]);

            int indexRepoId = dr.GetOrdinal("RepoId");
            if (dr["RepoId"] != DBNull.Value)
            {
                repoattach.RepoId = Convert.ToInt32(dr[indexRepoId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                repoattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return repoattach;
        }

        public override string TableName
        {
            get
            {
                return "St_RepoAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            RepoAttach st_repoattach = (RepoAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter repoattachidpara = new SqlParameter("@RepoAttachId", SqlDbType.Int, 4);
            repoattachidpara.Value = st_repoattach.RepoAttachId;
            paras.Add(repoattachidpara);

            SqlParameter repoidpara = new SqlParameter("@RepoId", SqlDbType.Int, 4);
            repoidpara.Value = st_repoattach.RepoId;
            paras.Add(repoidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_repoattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion
    }
}
