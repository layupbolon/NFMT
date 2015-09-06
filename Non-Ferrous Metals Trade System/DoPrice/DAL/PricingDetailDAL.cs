/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PricingDetailDAL.cs
// 文件功能描述：点价明细表dbo.Pri_PricingDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月13日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.DoPrice.Model;
using NFMT.DBUtility;
using NFMT.DoPrice.IDAL;
using NFMT.Common;

namespace NFMT.DoPrice.DAL
{
    /// <summary>
    /// 点价明细表dbo.Pri_PricingDetail数据交互类。
    /// </summary>
    public class PricingDetailDAL : ExecOperate, IPricingDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PricingDetailDAL()
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
            PricingDetail pri_pricingdetail = (PricingDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter pricingidpara = new SqlParameter("@PricingId", SqlDbType.Int, 4);
            pricingidpara.Value = pri_pricingdetail.PricingId;
            paras.Add(pricingidpara);

            SqlParameter pricingapplyidpara = new SqlParameter("@PricingApplyId", SqlDbType.Int, 4);
            pricingapplyidpara.Value = pri_pricingdetail.PricingApplyId;
            paras.Add(pricingapplyidpara);

            SqlParameter pricingapplydetailidpara = new SqlParameter("@PricingApplyDetailId", SqlDbType.Int, 4);
            pricingapplydetailidpara.Value = pri_pricingdetail.PricingApplyDetailId;
            paras.Add(pricingapplydetailidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = pri_pricingdetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = pri_pricingdetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter pricingweightpara = new SqlParameter("@PricingWeight", SqlDbType.Decimal, 9);
            pricingweightpara.Value = pri_pricingdetail.PricingWeight;
            paras.Add(pricingweightpara);

            SqlParameter avgpricepara = new SqlParameter("@AvgPrice", SqlDbType.Decimal, 9);
            avgpricepara.Value = pri_pricingdetail.AvgPrice;
            paras.Add(avgpricepara);

            SqlParameter pricingtimepara = new SqlParameter("@PricingTime", SqlDbType.DateTime, 8);
            pricingtimepara.Value = pri_pricingdetail.PricingTime;
            paras.Add(pricingtimepara);

            SqlParameter pricingerpara = new SqlParameter("@Pricinger", SqlDbType.Int, 4);
            pricingerpara.Value = pri_pricingdetail.Pricinger;
            paras.Add(pricingerpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = pri_pricingdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            PricingDetail pricingdetail = new PricingDetail();

            pricingdetail.DetailId = Convert.ToInt32(dr["DetailId"]);

            if (dr["PricingId"] != DBNull.Value)
            {
                pricingdetail.PricingId = Convert.ToInt32(dr["PricingId"]);
            }

            if (dr["PricingApplyId"] != DBNull.Value)
            {
                pricingdetail.PricingApplyId = Convert.ToInt32(dr["PricingApplyId"]);
            }

            if (dr["PricingApplyDetailId"] != DBNull.Value)
            {
                pricingdetail.PricingApplyDetailId = Convert.ToInt32(dr["PricingApplyDetailId"]);
            }

            if (dr["StockId"] != DBNull.Value)
            {
                pricingdetail.StockId = Convert.ToInt32(dr["StockId"]);
            }

            if (dr["StockLogId"] != DBNull.Value)
            {
                pricingdetail.StockLogId = Convert.ToInt32(dr["StockLogId"]);
            }

            if (dr["PricingWeight"] != DBNull.Value)
            {
                pricingdetail.PricingWeight = Convert.ToDecimal(dr["PricingWeight"]);
            }

            if (dr["AvgPrice"] != DBNull.Value)
            {
                pricingdetail.AvgPrice = Convert.ToDecimal(dr["AvgPrice"]);
            }

            if (dr["PricingTime"] != DBNull.Value)
            {
                pricingdetail.PricingTime = Convert.ToDateTime(dr["PricingTime"]);
            }

            if (dr["Pricinger"] != DBNull.Value)
            {
                pricingdetail.Pricinger = Convert.ToInt32(dr["Pricinger"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                pricingdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                pricingdetail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                pricingdetail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                pricingdetail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pricingdetail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return pricingdetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PricingDetail pricingdetail = new PricingDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            pricingdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexPricingId = dr.GetOrdinal("PricingId");
            if (dr["PricingId"] != DBNull.Value)
            {
                pricingdetail.PricingId = Convert.ToInt32(dr[indexPricingId]);
            }

            int indexPricingApplyId = dr.GetOrdinal("PricingApplyId");
            if (dr["PricingApplyId"] != DBNull.Value)
            {
                pricingdetail.PricingApplyId = Convert.ToInt32(dr[indexPricingApplyId]);
            }

            int indexPricingApplyDetailId = dr.GetOrdinal("PricingApplyDetailId");
            if (dr["PricingApplyDetailId"] != DBNull.Value)
            {
                pricingdetail.PricingApplyDetailId = Convert.ToInt32(dr[indexPricingApplyDetailId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                pricingdetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                pricingdetail.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexPricingWeight = dr.GetOrdinal("PricingWeight");
            if (dr["PricingWeight"] != DBNull.Value)
            {
                pricingdetail.PricingWeight = Convert.ToDecimal(dr[indexPricingWeight]);
            }

            int indexAvgPrice = dr.GetOrdinal("AvgPrice");
            if (dr["AvgPrice"] != DBNull.Value)
            {
                pricingdetail.AvgPrice = Convert.ToDecimal(dr[indexAvgPrice]);
            }

            int indexPricingTime = dr.GetOrdinal("PricingTime");
            if (dr["PricingTime"] != DBNull.Value)
            {
                pricingdetail.PricingTime = Convert.ToDateTime(dr[indexPricingTime]);
            }

            int indexPricinger = dr.GetOrdinal("Pricinger");
            if (dr["Pricinger"] != DBNull.Value)
            {
                pricingdetail.Pricinger = Convert.ToInt32(dr[indexPricinger]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                pricingdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                pricingdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                pricingdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                pricingdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pricingdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return pricingdetail;
        }

        public override string TableName
        {
            get
            {
                return "Pri_PricingDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PricingDetail pri_pricingdetail = (PricingDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = pri_pricingdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter pricingidpara = new SqlParameter("@PricingId", SqlDbType.Int, 4);
            pricingidpara.Value = pri_pricingdetail.PricingId;
            paras.Add(pricingidpara);

            SqlParameter pricingapplyidpara = new SqlParameter("@PricingApplyId", SqlDbType.Int, 4);
            pricingapplyidpara.Value = pri_pricingdetail.PricingApplyId;
            paras.Add(pricingapplyidpara);

            SqlParameter pricingapplydetailidpara = new SqlParameter("@PricingApplyDetailId", SqlDbType.Int, 4);
            pricingapplydetailidpara.Value = pri_pricingdetail.PricingApplyDetailId;
            paras.Add(pricingapplydetailidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = pri_pricingdetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = pri_pricingdetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter pricingweightpara = new SqlParameter("@PricingWeight", SqlDbType.Decimal, 9);
            pricingweightpara.Value = pri_pricingdetail.PricingWeight;
            paras.Add(pricingweightpara);

            SqlParameter avgpricepara = new SqlParameter("@AvgPrice", SqlDbType.Decimal, 9);
            avgpricepara.Value = pri_pricingdetail.AvgPrice;
            paras.Add(avgpricepara);

            SqlParameter pricingtimepara = new SqlParameter("@PricingTime", SqlDbType.DateTime, 8);
            pricingtimepara.Value = pri_pricingdetail.PricingTime;
            paras.Add(pricingtimepara);

            SqlParameter pricingerpara = new SqlParameter("@Pricinger", SqlDbType.Int, 4);
            pricingerpara.Value = pri_pricingdetail.Pricinger;
            paras.Add(pricingerpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = pri_pricingdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetPricingApplyDetailIds(UserModel user, int pricingId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select PricingApplyDetailId from dbo.Pri_PricingDetail where PricingId = {0} and DetailStatus >= {1}", pricingId, (int)Common.StatusEnum.已生效);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string str = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        str += dr["PricingApplyDetailId"] + ",";
                    }
                    if (!string.IsNullOrEmpty(str))
                        str = str.Substring(0, str.Length - 1);

                    result.ResultStatus = 0;
                    result.ReturnValue = str;
                    result.Message = "获取成功";
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

        public ResultModel GetCanDoPriceApplyDetailIds(UserModel user, int pricingId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select DetailId from dbo.Pri_PricingApplyDetail detail ");
                sb.AppendFormat(" where detail.PricingApplyId = (select PricingApplyId from dbo.Pri_Pricing where PricingId = {0}) and detail.DetailStatus <> {1} ", pricingId, (int)Common.StatusEnum.已作废);
                sb.AppendFormat(" and detail.DetailId not in (select PricingApplyDetailId from dbo.Pri_PricingDetail a where a.PricingId = {0} and a.DetailStatus <> {1})", pricingId, (int)Common.StatusEnum.已作废);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, CommandType.Text);
                if (dt != null && dt.Rows.Count >= 0)
                {
                    string str = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        str += dr["DetailId"] + ",";
                    }
                    if (!string.IsNullOrEmpty(str))
                        str = str.Substring(0, str.Length - 1);

                    result.ResultStatus = 0;
                    result.ReturnValue = str;
                    result.Message = "获取成功";
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

        public ResultModel InvalidAll(UserModel user, int pricingId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Pri_PricingDetail set DetailStatus = {0} where PricingId = {1}", (int)Common.StatusEnum.已作废, pricingId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i >= 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "作废成功";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        public ResultModel Load(UserModel user, int pricingId, Common.StatusEnum status = Common.StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select * from dbo.Pri_PricingDetail where PricingId ={0} and DetailStatus>={1}", pricingId, (int)status);

                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, cmdText, null, CommandType.Text);

                List<PricingDetail> pricingDetails = new List<PricingDetail>();

                foreach (DataRow dr in dt.Rows)
                {
                    PricingDetail pricingdetail = new PricingDetail();
                    pricingdetail.DetailId = Convert.ToInt32(dr["DetailId"]);

                    if (dr["PricingId"] != DBNull.Value)
                    {
                        pricingdetail.PricingId = Convert.ToInt32(dr["PricingId"]);
                    }
                    if (dr["PricingApplyId"] != DBNull.Value)
                    {
                        pricingdetail.PricingApplyId = Convert.ToInt32(dr["PricingApplyId"]);
                    }
                    if (dr["PricingApplyDetailId"] != DBNull.Value)
                    {
                        pricingdetail.PricingApplyDetailId = Convert.ToInt32(dr["PricingApplyDetailId"]);
                    }
                    if (dr["StockId"] != DBNull.Value)
                    {
                        pricingdetail.StockId = Convert.ToInt32(dr["StockId"]);
                    }
                    if (dr["PricingWeight"] != DBNull.Value)
                    {
                        pricingdetail.PricingWeight = Convert.ToDecimal(dr["PricingWeight"]);
                    }
                    if (dr["AvgPrice"] != DBNull.Value)
                    {
                        pricingdetail.AvgPrice = Convert.ToDecimal(dr["AvgPrice"]);
                    }
                    if (dr["PricingTime"] != DBNull.Value)
                    {
                        pricingdetail.PricingTime = Convert.ToDateTime(dr["PricingTime"]);
                    }
                    if (dr["Pricinger"] != DBNull.Value)
                    {
                        pricingdetail.Pricinger = Convert.ToInt32(dr["Pricinger"]);
                    }
                    if (dr["DetailStatus"] != DBNull.Value)
                    {
                        pricingdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
                    }
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        pricingdetail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
                    }
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        pricingdetail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    }
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        pricingdetail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    }
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        pricingdetail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    }
                    pricingDetails.Add(pricingdetail);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = pricingDetails;
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
