/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoDetailDAL.cs
// 文件功能描述：回购明细dbo.RepoDetail数据交互类。
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
    /// 回购明细dbo.RepoDetail数据交互类。
    /// </summary>
    public class RepoDetailDAL : DetailOperate, IRepoDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepoDetailDAL()
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
            RepoDetail st_repodetail = (RepoDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter repoidpara = new SqlParameter("@RepoId", SqlDbType.Int, 4);
            repoidpara.Value = st_repodetail.RepoId;
            paras.Add(repoidpara);

            SqlParameter repodetailstatuspara = new SqlParameter("@RepoDetailStatus", SqlDbType.Int, 4);
            repodetailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(repodetailstatuspara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_repodetail.StockId;
            paras.Add(stockidpara);

            SqlParameter repoapplydetailidpara = new SqlParameter("@RepoApplyDetailId", SqlDbType.Int, 4);
            repoapplydetailidpara.Value = st_repodetail.RepoApplyDetailId;
            paras.Add(repoapplydetailidpara);

            SqlParameter repoweightpara = new SqlParameter("@RepoWeight", SqlDbType.Decimal, 9);
            repoweightpara.Value = st_repodetail.RepoWeight;
            paras.Add(repoweightpara);

            SqlParameter unitpara = new SqlParameter("@Unit", SqlDbType.Int, 4);
            unitpara.Value = st_repodetail.Unit;
            paras.Add(unitpara);

            SqlParameter repopricepara = new SqlParameter("@RepoPrice", SqlDbType.Decimal, 9);
            repopricepara.Value = st_repodetail.RepoPrice;
            paras.Add(repopricepara);

            SqlParameter currencypara = new SqlParameter("@Currency", SqlDbType.Int, 4);
            currencypara.Value = st_repodetail.Currency;
            paras.Add(currencypara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_repodetail.StockLogId;
            paras.Add(stocklogidpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            RepoDetail repodetail = new RepoDetail();

            repodetail.DetailId = Convert.ToInt32(dr["DetailId"]);

            if (dr["RepoId"] != DBNull.Value)
            {
                repodetail.RepoId = Convert.ToInt32(dr["RepoId"]);
            }

            if (dr["RepoDetailStatus"] != DBNull.Value)
            {
                repodetail.RepoDetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["RepoDetailStatus"]);
            }

            if (dr["StockId"] != DBNull.Value)
            {
                repodetail.StockId = Convert.ToInt32(dr["StockId"]);
            }

            if (dr["RepoApplyDetailId"] != DBNull.Value)
            {
                repodetail.RepoApplyDetailId = Convert.ToInt32(dr["RepoApplyDetailId"]);
            }

            if (dr["RepoWeight"] != DBNull.Value)
            {
                repodetail.RepoWeight = Convert.ToDecimal(dr["RepoWeight"]);
            }

            if (dr["Unit"] != DBNull.Value)
            {
                repodetail.Unit = Convert.ToInt32(dr["Unit"]);
            }

            if (dr["RepoPrice"] != DBNull.Value)
            {
                repodetail.RepoPrice = Convert.ToDecimal(dr["RepoPrice"]);
            }

            if (dr["Currency"] != DBNull.Value)
            {
                repodetail.Currency = Convert.ToInt32(dr["Currency"]);
            }

            if (dr["StockLogId"] != DBNull.Value)
            {
                repodetail.StockLogId = Convert.ToInt32(dr["StockLogId"]);
            }


            return repodetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            RepoDetail repodetail = new RepoDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            repodetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexRepoId = dr.GetOrdinal("RepoId");
            if (dr["RepoId"] != DBNull.Value)
            {
                repodetail.RepoId = Convert.ToInt32(dr[indexRepoId]);
            }

            int indexRepoDetailStatus = dr.GetOrdinal("RepoDetailStatus");
            if (dr["RepoDetailStatus"] != DBNull.Value)
            {
                repodetail.RepoDetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRepoDetailStatus]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                repodetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexRepoApplyDetailId = dr.GetOrdinal("RepoApplyDetailId");
            if (dr["RepoApplyDetailId"] != DBNull.Value)
            {
                repodetail.RepoApplyDetailId = Convert.ToInt32(dr[indexRepoApplyDetailId]);
            }

            int indexRepoWeight = dr.GetOrdinal("RepoWeight");
            if (dr["RepoWeight"] != DBNull.Value)
            {
                repodetail.RepoWeight = Convert.ToDecimal(dr[indexRepoWeight]);
            }

            int indexUnit = dr.GetOrdinal("Unit");
            if (dr["Unit"] != DBNull.Value)
            {
                repodetail.Unit = Convert.ToInt32(dr[indexUnit]);
            }

            int indexRepoPrice = dr.GetOrdinal("RepoPrice");
            if (dr["RepoPrice"] != DBNull.Value)
            {
                repodetail.RepoPrice = Convert.ToDecimal(dr[indexRepoPrice]);
            }

            int indexCurrency = dr.GetOrdinal("Currency");
            if (dr["Currency"] != DBNull.Value)
            {
                repodetail.Currency = Convert.ToInt32(dr[indexCurrency]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                repodetail.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }


            return repodetail;
        }

        public override string TableName
        {
            get
            {
                return "St_RepoDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            RepoDetail st_repodetail = (RepoDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = st_repodetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter repoidpara = new SqlParameter("@RepoId", SqlDbType.Int, 4);
            repoidpara.Value = st_repodetail.RepoId;
            paras.Add(repoidpara);

            SqlParameter repodetailstatuspara = new SqlParameter("@RepoDetailStatus", SqlDbType.Int, 4);
            repodetailstatuspara.Value = st_repodetail.RepoDetailStatus;
            paras.Add(repodetailstatuspara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_repodetail.StockId;
            paras.Add(stockidpara);

            SqlParameter repoapplydetailidpara = new SqlParameter("@RepoApplyDetailId", SqlDbType.Int, 4);
            repoapplydetailidpara.Value = st_repodetail.RepoApplyDetailId;
            paras.Add(repoapplydetailidpara);

            SqlParameter repoweightpara = new SqlParameter("@RepoWeight", SqlDbType.Decimal, 9);
            repoweightpara.Value = st_repodetail.RepoWeight;
            paras.Add(repoweightpara);

            SqlParameter unitpara = new SqlParameter("@Unit", SqlDbType.Int, 4);
            unitpara.Value = st_repodetail.Unit;
            paras.Add(unitpara);

            SqlParameter repopricepara = new SqlParameter("@RepoPrice", SqlDbType.Decimal, 9);
            repopricepara.Value = st_repodetail.RepoPrice;
            paras.Add(repopricepara);

            SqlParameter currencypara = new SqlParameter("@Currency", SqlDbType.Int, 4);
            currencypara.Value = st_repodetail.Currency;
            paras.Add(currencypara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_repodetail.StockLogId;
            paras.Add(stocklogidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetStockId(UserModel user, int repoId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("declare @str varchar(200)");
                sb.Append(Environment.NewLine);
                sb.Append("set @str = ' '");
                sb.Append(Environment.NewLine);
                sb.AppendFormat("select @str = @str + CONVERT(varchar,StockId) + ',' from dbo.St_RepoDetail where RepoId = {0}", repoId);
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
                    result.ReturnValue = obj.ToString();
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

        public ResultModel GetDetailId(UserModel user, int repoId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("declare @str varchar(200)");
                sb.Append(Environment.NewLine);
                sb.Append("set @str = ' '");
                sb.Append(Environment.NewLine);
                sb.AppendFormat("select @str = @str + CONVERT(varchar,DetailId) + ',' from dbo.St_RepoDetail where RepoId = {0}", repoId);
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
                    result.ReturnValue = obj.ToString();
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

        public ResultModel InvalidAll(UserModel user, int repoId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.St_RepoDetail set RepoDetailStatus = {0} where RepoId = {1}", (int)Common.StatusEnum.已作废, repoId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.AffectCount = i;
                    result.Message = "操作成功";
                    result.ResultStatus = 0;
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

        public ResultModel Load(UserModel user, int repoId, Common.StatusEnum status = Common.StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.St_RepoDetail where RepoId ={0} and RepoDetailStatus>={1}", repoId, (int)status);
            return Load<Model.RepoDetail>(user, CommandType.Text, cmdText);
        }

        #endregion
    }
}
