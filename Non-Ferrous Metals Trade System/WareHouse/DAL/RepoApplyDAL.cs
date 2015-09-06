/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoApplyDAL.cs
// 文件功能描述：回购申请dbo.RepoApply数据交互类。
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
    /// 回购申请dbo.RepoApply数据交互类。
    /// </summary>
    public class RepoApplyDAL : ApplyOperate, IRepoApplyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepoApplyDAL()
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
            RepoApply st_repoapply = (RepoApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RepoApplyId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = st_repoapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            RepoApply repoapply = new RepoApply();

            int indexRepoApplyId = dr.GetOrdinal("RepoApplyId");
            repoapply.RepoApplyId = Convert.ToInt32(dr[indexRepoApplyId]);

            int indexApplyId = dr.GetOrdinal("ApplyId");
            if (dr["ApplyId"] != DBNull.Value)
            {
                repoapply.ApplyId = Convert.ToInt32(dr[indexApplyId]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                repoapply.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                repoapply.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                repoapply.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                repoapply.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return repoapply;
        }

        public override string TableName
        {
            get
            {
                return "St_RepoApply";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            RepoApply st_repoapply = (RepoApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter repoapplyidpara = new SqlParameter("@RepoApplyId", SqlDbType.Int, 4);
            repoapplyidpara.Value = st_repoapply.RepoApplyId;
            paras.Add(repoapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = st_repoapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetByApplyId(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            if (applyId < 1)
            {
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@applyId", SqlDbType.Int, 4);
            para.Value = applyId;
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                string cmdText = "select * from dbo.St_RepoApply where ApplyId=@applyId";
                dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringNFMT, CommandType.Text, cmdText, paras.ToArray());

                RepoApply repoapply = new RepoApply();

                if (dr.Read())
                {
                    int indexRepoApplyId = dr.GetOrdinal("RepoApplyId");
                    repoapply.RepoApplyId = Convert.ToInt32(dr[indexRepoApplyId]);

                    int indexApplyId = dr.GetOrdinal("ApplyId");
                    if (dr["ApplyId"] != DBNull.Value)
                    {
                        repoapply.ApplyId = Convert.ToInt32(dr[indexApplyId]);
                    }

                    int indexCreatorId = dr.GetOrdinal("CreatorId");
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        repoapply.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
                    }

                    int indexCreateTime = dr.GetOrdinal("CreateTime");
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        repoapply.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
                    }

                    int indexLastModifyId = dr.GetOrdinal("LastModifyId");
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        repoapply.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
                    }

                    int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        repoapply.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
                    }

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = repoapply;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        public ResultModel GetPledgeStockId(UserModel user, int repoApplyId)
        {
            ResultModel result = new ResultModel();
            string str = string.Empty;

            try
            {
                string sql = string.Format("select StockId from NFMT.dbo.St_RepoApplyDetail where RepoApplyId = {0} and DetailStatus = {1} and StockId not in (select distinct ISNULL(StockId,0) from NFMT.dbo.St_RepoDetail pd right join NFMT.dbo.St_Repo p on pd.RepoId = p.RepoId and p.RepoApplyId = {2} and pd.RepoDetailStatus <> {3})", repoApplyId, (int)Common.StatusEnum.已生效, repoApplyId, (int)Common.StatusEnum.已作废);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        str += dr["StockId"].ToString() + ",";
                    }
                    if (!string.IsNullOrEmpty(str))
                        str = str.Substring(0, str.Length - 1);

                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = str;
                }
                else
                {
                    result.ResultStatus = 0;
                    result.Message = "无数据可获取";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel CheckStockOutCanConfirm(UserModel user, int repoApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                int entryStatus = (int)Common.StatusEnum.已录入;
                int completeStatus = (int)StatusEnum.已完成;
                int readyStatus = (int)StatusEnum.已生效;
                int closeStatus = (int)StatusEnum.已关闭;

                string cmdText = string.Format("select count(*) from dbo.St_Repo so where so.RepoApplyId =@repoApplyId and so.RepoStatus >= @entryStatus and so.RepoStatus <@completeStatus");
                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter para = new SqlParameter("@repoApplyId", repoApplyId);
                paras.Add(para);

                para = new SqlParameter("@entryStatus", entryStatus);
                paras.Add(para);

                para = new SqlParameter("@completeStatus", completeStatus);
                paras.Add(para);

                object obj = SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras.ToArray());
                int otherStatusRows = 0;
                if (obj == null || !int.TryParse(obj.ToString(), out otherStatusRows) || otherStatusRows != 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "回购申请拥有待执行的回购，不能进行执行完成确认操作。";
                    return result;
                }

                cmdText = string.Format("select count(*) from dbo.St_RepoApplyDetail soad left join dbo.St_RepoDetail sod on sod.RepoApplyDetailId = soad.DetailId and sod.RepoDetailStatus = @completeStatus where soad.RepoApplyId = @repoApplyId  and soad.DetailStatus = @readyStatus and sod.DetailId is null");
                paras = new List<SqlParameter>();

                para = new SqlParameter("@repoApplyId", repoApplyId);
                paras.Add(para);

                para = new SqlParameter("@completeStatus", completeStatus);
                paras.Add(para);

                para = new SqlParameter("@readyStatus", readyStatus);
                paras.Add(para);

                obj = SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras.ToArray());
                int notCompleteRows = 0;
                if (obj == null || !int.TryParse(obj.ToString(), out notCompleteRows) || notCompleteRows != 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "回购申请拥有待执行的回购，不能进行执行完成确认操作。";
                    return result;
                }

                cmdText = string.Format("select count(*) from dbo.St_RepoApplyDetail sod where sod.DetailStatus =@closeStatus and sod.RepoApplyId=@repoApplyId");

                paras = new List<SqlParameter>();

                para = new SqlParameter("@repoApplyId", repoApplyId);
                paras.Add(para);

                para = new SqlParameter("@closeStatus", closeStatus);
                paras.Add(para);

                obj = SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras.ToArray());
                int closeRows = 0;
                if (obj == null || !int.TryParse(obj.ToString(), out closeRows))
                {
                    result.ResultStatus = -1;
                    result.Message = "回购申请拥有待执行的回购，不能进行执行完成确认操作。";
                    return result;
                }

                result.ResultStatus = 0;
                if (closeRows == 0)
                    result.ReturnValue = StatusEnum.已完成;
                else
                    result.ReturnValue = StatusEnum.部分完成;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public override int MenuId
        {
            get
            {
                return 49;
            }
        }

        #endregion

    }
}
