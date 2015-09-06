/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoDAL.cs
// 文件功能描述：回购dbo.Repo数据交互类。
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
    /// 回购dbo.Repo数据交互类。
    /// </summary>
    public class RepoDAL : ExecOperate, IRepoDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepoDAL()
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
            Repo st_repo = (Repo)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RepoId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter repoapplyidpara = new SqlParameter("@RepoApplyId", SqlDbType.Int, 4);
            repoapplyidpara.Value = st_repo.RepoApplyId;
            paras.Add(repoapplyidpara);

            SqlParameter repoerpara = new SqlParameter("@Repoer", SqlDbType.Int, 4);
            repoerpara.Value = st_repo.Repoer;
            paras.Add(repoerpara);

            SqlParameter repoertimepara = new SqlParameter("@RepoerTime", SqlDbType.DateTime, 8);
            repoertimepara.Value = st_repo.RepoerTime;
            paras.Add(repoertimepara);

            SqlParameter repostatuspara = new SqlParameter("@RepoStatus", SqlDbType.Int, 4);
            repostatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(repostatuspara);

            if (!string.IsNullOrEmpty(st_repo.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = st_repo.Memo;
                paras.Add(memopara);
            }

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Repo repo = new Repo();

            int indexRepoId = dr.GetOrdinal("RepoId");
            repo.RepoId = Convert.ToInt32(dr[indexRepoId]);

            int indexRepoApplyId = dr.GetOrdinal("RepoApplyId");
            repo.RepoApplyId = Convert.ToInt32(dr[indexRepoApplyId]);

            int indexRepoer = dr.GetOrdinal("Repoer");
            if (dr["Repoer"] != DBNull.Value)
            {
                repo.Repoer = Convert.ToInt32(dr[indexRepoer]);
            }

            int indexRepoerTime = dr.GetOrdinal("RepoerTime");
            if (dr["RepoerTime"] != DBNull.Value)
            {
                repo.RepoerTime = Convert.ToDateTime(dr[indexRepoerTime]);
            }

            int indexRepoStatus = dr.GetOrdinal("RepoStatus");
            if (dr["RepoStatus"] != DBNull.Value)
            {
                repo.RepoStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRepoStatus]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                repo.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                repo.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                repo.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                repo.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                repo.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return repo;
        }

        public override string TableName
        {
            get
            {
                return "St_Repo";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Repo st_repo = (Repo)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter repoidpara = new SqlParameter("@RepoId", SqlDbType.Int, 4);
            repoidpara.Value = st_repo.RepoId;
            paras.Add(repoidpara);

            SqlParameter repoapplyidpara = new SqlParameter("@RepoApplyId", SqlDbType.Int, 4);
            repoapplyidpara.Value = st_repo.RepoApplyId;
            paras.Add(repoapplyidpara);

            SqlParameter repoerpara = new SqlParameter("@Repoer", SqlDbType.Int, 4);
            repoerpara.Value = st_repo.Repoer;
            paras.Add(repoerpara);

            SqlParameter repoertimepara = new SqlParameter("@RepoerTime", SqlDbType.DateTime, 8);
            repoertimepara.Value = st_repo.RepoerTime;
            paras.Add(repoertimepara);

            SqlParameter repostatuspara = new SqlParameter("@RepoStatus", SqlDbType.Int, 4);
            repostatuspara.Value = st_repo.RepoStatus;
            paras.Add(repostatuspara);

            if (!string.IsNullOrEmpty(st_repo.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = st_repo.Memo;
                paras.Add(memopara);
            }

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel UpdateStockDP(int stockId, int dpId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.St_Stock set DeliverPlaceId = {0} where StockId = {1}", dpId, stockId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);

                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "更新交货地成功";
                    result.AffectCount = i;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "更新交货地失败";
                    result.AffectCount = i;
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = string.Format("更新状态失败,{0}", e.Message);
            }

            return result;
        }

        public override int MenuId
        {
            get
            {
                return 50;
            }
        }

        #endregion
    }
}
