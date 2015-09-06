/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeApplyDetailDAL.cs
// 文件功能描述：质押申请明细dbo.St_PledgeApplyDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
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
    /// 质押申请明细dbo.St_PledgeApplyDetail数据交互类。
    /// </summary>
    public class PledgeApplyDetailDAL : ApplyOperate, IPledgeApplyDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PledgeApplyDetailDAL()
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
            PledgeApplyDetail st_pledgeapplydetail = (PledgeApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter pledgeapplyidpara = new SqlParameter("@PledgeApplyId", SqlDbType.Int, 4);
            pledgeapplyidpara.Value = st_pledgeapplydetail.PledgeApplyId;
            paras.Add(pledgeapplyidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_pledgeapplydetail.StockId;
            paras.Add(stockidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);

            SqlParameter applyqtypara = new SqlParameter("@ApplyQty", SqlDbType.Decimal, 9);
            applyqtypara.Value = st_pledgeapplydetail.ApplyQty;
            paras.Add(applyqtypara);

            SqlParameter uintidpara = new SqlParameter("@UintId", SqlDbType.Int, 4);
            uintidpara.Value = st_pledgeapplydetail.UintId;
            paras.Add(uintidpara);

            SqlParameter pledgepricepara = new SqlParameter("@PledgePrice", SqlDbType.Decimal, 9);
            pledgepricepara.Value = st_pledgeapplydetail.PledgePrice;
            paras.Add(pledgepricepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = st_pledgeapplydetail.CurrencyId;
            paras.Add(currencyidpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            PledgeApplyDetail pledgeapplydetail = new PledgeApplyDetail();

            pledgeapplydetail.DetailId = Convert.ToInt32(dr["DetailId"]);

            pledgeapplydetail.PledgeApplyId = Convert.ToInt32(dr["PledgeApplyId"]);

            if (dr["StockId"] != DBNull.Value)
            {
                pledgeapplydetail.StockId = Convert.ToInt32(dr["StockId"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                pledgeapplydetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }

            if (dr["ApplyQty"] != DBNull.Value)
            {
                pledgeapplydetail.ApplyQty = Convert.ToDecimal(dr["ApplyQty"]);
            }

            if (dr["UintId"] != DBNull.Value)
            {
                pledgeapplydetail.UintId = Convert.ToInt32(dr["UintId"]);
            }

            if (dr["PledgePrice"] != DBNull.Value)
            {
                pledgeapplydetail.PledgePrice = Convert.ToDecimal(dr["PledgePrice"]);
            }

            if (dr["CurrencyId"] != DBNull.Value)
            {
                pledgeapplydetail.CurrencyId = Convert.ToInt32(dr["CurrencyId"]);
            }


            return pledgeapplydetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PledgeApplyDetail pledgeapplydetail = new PledgeApplyDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            pledgeapplydetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexPledgeApplyId = dr.GetOrdinal("PledgeApplyId");
            pledgeapplydetail.PledgeApplyId = Convert.ToInt32(dr[indexPledgeApplyId]);

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                pledgeapplydetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                pledgeapplydetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexApplyQty = dr.GetOrdinal("ApplyQty");
            if (dr["ApplyQty"] != DBNull.Value)
            {
                pledgeapplydetail.ApplyQty = Convert.ToDecimal(dr[indexApplyQty]);
            }

            int indexUintId = dr.GetOrdinal("UintId");
            if (dr["UintId"] != DBNull.Value)
            {
                pledgeapplydetail.UintId = Convert.ToInt32(dr[indexUintId]);
            }

            int indexPledgePrice = dr.GetOrdinal("PledgePrice");
            if (dr["PledgePrice"] != DBNull.Value)
            {
                pledgeapplydetail.PledgePrice = Convert.ToDecimal(dr[indexPledgePrice]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                pledgeapplydetail.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }


            return pledgeapplydetail;
        }

        public override string TableName
        {
            get
            {
                return "St_PledgeApplyDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PledgeApplyDetail st_pledgeapplydetail = (PledgeApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = st_pledgeapplydetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter pledgeapplyidpara = new SqlParameter("@PledgeApplyId", SqlDbType.Int, 4);
            pledgeapplyidpara.Value = st_pledgeapplydetail.PledgeApplyId;
            paras.Add(pledgeapplyidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_pledgeapplydetail.StockId;
            paras.Add(stockidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = st_pledgeapplydetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter applyqtypara = new SqlParameter("@ApplyQty", SqlDbType.Decimal, 9);
            applyqtypara.Value = st_pledgeapplydetail.ApplyQty;
            paras.Add(applyqtypara);

            SqlParameter uintidpara = new SqlParameter("@UintId", SqlDbType.Int, 4);
            uintidpara.Value = st_pledgeapplydetail.UintId;
            paras.Add(uintidpara);

            SqlParameter pledgepricepara = new SqlParameter("@PledgePrice", SqlDbType.Decimal, 9);
            pledgepricepara.Value = st_pledgeapplydetail.PledgePrice;
            paras.Add(pledgepricepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = st_pledgeapplydetail.CurrencyId;
            paras.Add(currencyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetStockId(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("declare @str varchar(200)");
                sb.Append(Environment.NewLine);
                sb.Append("set @str = ' '");
                sb.Append(Environment.NewLine);
                sb.AppendFormat("select @str = @str + CONVERT(varchar,StockId) + ',' from dbo.St_PledgeApplyDetail where PledgeApplyId = {0} and DetailStatus = {1}", pledgeApplyId, (int)Common.StatusEnum.已生效);
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

        public ResultModel InsertOrUpdateStatus(UserModel user, Model.PledgeApplyDetail pledgeApplyDetail)
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

                SqlParameter stockmoveapplyidpara = new SqlParameter("@pledgeApplyId", SqlDbType.Int, 4);
                stockmoveapplyidpara.Value = pledgeApplyDetail.PledgeApplyId;
                paras.Add(stockmoveapplyidpara);

                SqlParameter stockidpara = new SqlParameter("@stockId", SqlDbType.Int, 4);
                stockidpara.Value = pledgeApplyDetail.StockId;
                paras.Add(stockidpara);

                SqlParameter applyQtypara = new SqlParameter("@applyQty", SqlDbType.Decimal, 18);
                applyQtypara.Value = pledgeApplyDetail.ApplyQty;
                paras.Add(applyQtypara);

                SqlParameter unitIdpara = new SqlParameter("@unitId", SqlDbType.Int, 4);
                unitIdpara.Value = pledgeApplyDetail.UintId;
                paras.Add(unitIdpara);

                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, "InsertOrUpdate_St_PledgeApplyDetail", paras.ToArray());

                if (i > 0)
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

        public ResultModel Invalid(UserModel user, int pledgeApplyId, string sids)
        {
            ResultModel result = new ResultModel();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            try
            {
                if (string.IsNullOrEmpty(sids))
                    sb.AppendFormat("update dbo.St_PledgeApplyDetail set DetailStatus = {0} where PledgeApplyId = {1}", (int)NFMT.Common.StatusEnum.已作废, pledgeApplyId);
                else
                    sb.AppendFormat("update dbo.St_PledgeApplyDetail set DetailStatus = {0} where PledgeApplyId = {1} and StockId not in ({2})", (int)NFMT.Common.StatusEnum.已作废, pledgeApplyId, sids);

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

        public ResultModel GetDetailId(UserModel user, int pledgeApplyId, int stockId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select DetailId from dbo.St_PledgeApplyDetail where PledgeApplyId = {0} and StockId = {1} and DetailStatus = {2}", pledgeApplyId, stockId, (int)Common.StatusEnum.已生效);
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

        public ResultModel InvalidAll(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.St_PledgeApplyDetail set DetailStatus = {0} where PledgeApplyId = {1}", (int)Common.StatusEnum.已作废, pledgeApplyId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.AffectCount = i;
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
                result.ResultStatus = -1;
                result.Message = e.Message;
            }
            return result;
        }

        public ResultModel Load(UserModel user, int pledgeApplyId, Common.StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select * from dbo.St_PledgeApplyDetail where PledgeApplyId={0} and DetailStatus>={1}", pledgeApplyId, (int)StatusEnum.已生效);
                DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionStringNFMT, cmdText, null, CommandType.Text);

                List<PledgeApplyDetail> pledgeApplyDetails = new List<PledgeApplyDetail>();

                foreach (DataRow dr in dt.Rows)
                {
                    PledgeApplyDetail pledgeapplydetail = new PledgeApplyDetail();
                    pledgeapplydetail.DetailId = Convert.ToInt32(dr["DetailId"]);

                    pledgeapplydetail.PledgeApplyId = Convert.ToInt32(dr["PledgeApplyId"]);

                    if (dr["StockId"] != DBNull.Value)
                    {
                        pledgeapplydetail.StockId = Convert.ToInt32(dr["StockId"]);
                    }
                    if (dr["DetailStatus"] != DBNull.Value)
                    {
                        pledgeapplydetail.DetailStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr["DetailStatus"].ToString());
                    }
                    if (dr["ApplyQty"] != DBNull.Value)
                    {
                        pledgeapplydetail.ApplyQty = Convert.ToDecimal(dr["ApplyQty"]);
                    }
                    if (dr["UintId"] != DBNull.Value)
                    {
                        pledgeapplydetail.UintId = Convert.ToInt32(dr["UintId"]);
                    }
                    //if (dr["ExclusiveId"] != DBNull.Value)
                    //{
                    //    pledgeapplydetail.ExclusiveId = Convert.ToInt32(dr["ExclusiveId"]);
                    //}
                    pledgeApplyDetails.Add(pledgeapplydetail);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = pledgeApplyDetails;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        #endregion
    }
}
