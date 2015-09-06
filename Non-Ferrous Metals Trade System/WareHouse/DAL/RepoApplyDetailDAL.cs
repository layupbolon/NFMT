/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoApplyDetailDAL.cs
// 文件功能描述：回购申请库存明细dbo.RepoApplyDetail数据交互类。
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
    /// 回购申请库存明细dbo.RepoApplyDetail数据交互类。
    /// </summary>
    public class RepoApplyDetailDAL : ApplyOperate, IRepoApplyDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepoApplyDetailDAL()
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
            RepoApplyDetail st_repoapplydetail = (RepoApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter repoapplyidpara = new SqlParameter("@RepoApplyId", SqlDbType.Int, 4);
            repoapplyidpara.Value = st_repoapplydetail.RepoApplyId;
            paras.Add(repoapplyidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_repoapplydetail.StockId;
            paras.Add(stockidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            RepoApplyDetail repoapplydetail = new RepoApplyDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            repoapplydetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexRepoApplyId = dr.GetOrdinal("RepoApplyId");
            repoapplydetail.RepoApplyId = Convert.ToInt32(dr[indexRepoApplyId]);

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                repoapplydetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                repoapplydetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }


            return repoapplydetail;
        }

        public override string TableName
        {
            get
            {
                return "St_RepoApplyDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            RepoApplyDetail st_repoapplydetail = (RepoApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = st_repoapplydetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter repoapplyidpara = new SqlParameter("@RepoApplyId", SqlDbType.Int, 4);
            repoapplyidpara.Value = st_repoapplydetail.RepoApplyId;
            paras.Add(repoapplyidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_repoapplydetail.StockId;
            paras.Add(stockidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = st_repoapplydetail.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        #endregion

        #region 新增方法
        internal ResultModel Invalid(UserModel user, int repoApplyId, string sids)
        {
            ResultModel result = new ResultModel();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            try
            {
                if (string.IsNullOrEmpty(sids))
                    sb.AppendFormat("update dbo.St_RepoApplyDetail set DetailStatus = {0} where RepoApplyId = {1}", (int)NFMT.Common.StatusEnum.已作废, repoApplyId);
                else
                    sb.AppendFormat("update dbo.St_RepoApplyDetail set DetailStatus = {0} where RepoApplyId = {1} and StockId not in ({2})", (int)NFMT.Common.StatusEnum.已作废, repoApplyId, sids);

                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sb.ToString(), null);

                if (i >= 0)
                {
                    result.Message = "操作成功";
                    result.ResultStatus = 0;
                    result.AffectCount = i;
                }
                else
                {
                    result.Message = "操作失败";
                    result.ResultStatus = -1;
                }

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        internal ResultModel GetStockId(UserModel user, int repoApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                int readyStatus = (int)StatusEnum.已生效;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("declare @str varchar(200)");
                sb.Append(Environment.NewLine);
                sb.Append("set @str = ' '");
                sb.Append(Environment.NewLine);
                sb.AppendFormat("select @str = @str + CONVERT(varchar,StockId) + ',' from dbo.St_RepoApplyDetail where RepoApplyId = {0} and DetailStatus>={1} ", repoApplyId, readyStatus);
                sb.Append(Environment.NewLine);
                sb.Append("if LEN(LTRIM(@str)) > 1");
                sb.Append(Environment.NewLine);
                sb.Append("	select SUBSTRING(@str,1,LEN(@str)-1)");
                sb.Append(Environment.NewLine);
                sb.Append("else");
                sb.Append(Environment.NewLine);
                sb.Append("	select ''");

                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sb.ToString(), null);
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                {
                    result.Message = "获取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = obj.ToString().Trim();
                }
                else
                {
                    result.Message = "获取失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = string.Format("获取失败,{0}", e.Message);
                result.ResultStatus = -1;
            }

            return result;
        }

        internal ResultModel InsertOrUpdateStatus(UserModel user, int repurApplyID, int stockId)
        {
            ResultModel result = new ResultModel();

            try
            {
                List<SqlParameter> paras = new List<SqlParameter>();
                SqlParameter detailidpara = new SqlParameter();
                detailidpara.Direction = ParameterDirection.Output;
                detailidpara.SqlDbType = SqlDbType.Int;
                detailidpara.ParameterName = "@DetailId";
                detailidpara.Size = 4;
                paras.Add(detailidpara);

                SqlParameter stockmoveapplyidpara = new SqlParameter("@repurApplyID", SqlDbType.Int, 4);
                stockmoveapplyidpara.Value = repurApplyID;
                paras.Add(stockmoveapplyidpara);

                SqlParameter stockidpara = new SqlParameter("@stockId", SqlDbType.Int, 4);
                stockidpara.Value = stockId;
                paras.Add(stockidpara);

                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, "InsertOrUpdate_St_RepurApplyDetail", paras.ToArray());

                if (i == 0)
                {
                    result.Message = "操作成功";
                    result.ResultStatus = 0;
                    result.AffectCount = i;
                    result.ReturnValue = detailidpara.Value;
                }
                else
                {
                    result.Message = "操作失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = string.Format("操作失败,{0}", e.Message);
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel Load(UserModel user, int repoApplyId, Common.StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select * from dbo.St_RepoApplyDetail where RepoApplyId={0} and DetailStatus>={1}", repoApplyId, (int)StatusEnum.已生效);
                DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionStringNFMT, cmdText, null, CommandType.Text);

                List<RepoApplyDetail> RepoApplyDetails = new List<RepoApplyDetail>();

                foreach (DataRow dr in dt.Rows)
                {
                    RepoApplyDetail repoapplydetail = new RepoApplyDetail();
                    repoapplydetail.DetailId = Convert.ToInt32(dr["DetailId"]);

                    repoapplydetail.RepoApplyId = Convert.ToInt32(dr["RepoApplyId"]);

                    if (dr["StockId"] != DBNull.Value)
                    {
                        repoapplydetail.StockId = Convert.ToInt32(dr["StockId"]);
                    }
                    if (dr["DetailStatus"] != DBNull.Value)
                    {
                        repoapplydetail.DetailStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr["DetailStatus"].ToString());
                    }
                    RepoApplyDetails.Add(repoapplydetail);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = RepoApplyDetails;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel GetDetailId(UserModel user, int repoApplyId, int stockId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select DetailId from dbo.St_RepoApplyDetail where RepoApplyId = {0} and StockId = {1} and DetailStatus = {2}", repoApplyId, stockId, (int)Common.StatusEnum.已生效);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                int detailId = 0;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && int.TryParse(obj.ToString(), out detailId))
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = detailId;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        public ResultModel InvalidAll(UserModel user, int repoApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.St_RepoApplyDetail set DetailStatus = {0} where RepoApplyId = {1}", (int)Common.StatusEnum.已作废, repoApplyId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "作废失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        #endregion
    }
}
