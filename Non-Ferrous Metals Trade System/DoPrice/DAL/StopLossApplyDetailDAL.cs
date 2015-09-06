/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StopLossApplyDetailDAL.cs
// 文件功能描述：止损申请明细dbo.Pri_StopLossApplyDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月23日
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
    /// 止损申请明细dbo.Pri_StopLossApplyDetail数据交互类。
    /// </summary>
    public class StopLossApplyDetailDAL : ApplyOperate, IStopLossApplyDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StopLossApplyDetailDAL()
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
            StopLossApplyDetail pri_stoplossapplydetail = (StopLossApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stoplossapplyidpara = new SqlParameter("@StopLossApplyId", SqlDbType.Int, 4);
            stoplossapplyidpara.Value = pri_stoplossapplydetail.StopLossApplyId;
            paras.Add(stoplossapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = pri_stoplossapplydetail.ApplyId;
            paras.Add(applyidpara);

            SqlParameter pricingdetailidpara = new SqlParameter("@PricingDetailId", SqlDbType.Int, 4);
            pricingdetailidpara.Value = pri_stoplossapplydetail.PricingDetailId;
            paras.Add(pricingdetailidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = pri_stoplossapplydetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = pri_stoplossapplydetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter stoplossweightpara = new SqlParameter("@StopLossWeight", SqlDbType.Decimal, 9);
            stoplossweightpara.Value = pri_stoplossapplydetail.StopLossWeight;
            paras.Add(stoplossweightpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = pri_stoplossapplydetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StopLossApplyDetail stoplossapplydetail = new StopLossApplyDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            stoplossapplydetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexStopLossApplyId = dr.GetOrdinal("StopLossApplyId");
            if (dr["StopLossApplyId"] != DBNull.Value)
            {
                stoplossapplydetail.StopLossApplyId = Convert.ToInt32(dr[indexStopLossApplyId]);
            }

            int indexApplyId = dr.GetOrdinal("ApplyId");
            if (dr["ApplyId"] != DBNull.Value)
            {
                stoplossapplydetail.ApplyId = Convert.ToInt32(dr[indexApplyId]);
            }

            int indexPricingDetailId = dr.GetOrdinal("PricingDetailId");
            if (dr["PricingDetailId"] != DBNull.Value)
            {
                stoplossapplydetail.PricingDetailId = Convert.ToInt32(dr[indexPricingDetailId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                stoplossapplydetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                stoplossapplydetail.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexStopLossWeight = dr.GetOrdinal("StopLossWeight");
            if (dr["StopLossWeight"] != DBNull.Value)
            {
                stoplossapplydetail.StopLossWeight = Convert.ToDecimal(dr[indexStopLossWeight]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                stoplossapplydetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                stoplossapplydetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                stoplossapplydetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                stoplossapplydetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                stoplossapplydetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return stoplossapplydetail;
        }

        public override string TableName
        {
            get
            {
                return "Pri_StopLossApplyDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StopLossApplyDetail pri_stoplossapplydetail = (StopLossApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = pri_stoplossapplydetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter stoplossapplyidpara = new SqlParameter("@StopLossApplyId", SqlDbType.Int, 4);
            stoplossapplyidpara.Value = pri_stoplossapplydetail.StopLossApplyId;
            paras.Add(stoplossapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = pri_stoplossapplydetail.ApplyId;
            paras.Add(applyidpara);

            SqlParameter pricingdetailidpara = new SqlParameter("@PricingDetailId", SqlDbType.Int, 4);
            pricingdetailidpara.Value = pri_stoplossapplydetail.PricingDetailId;
            paras.Add(pricingdetailidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = pri_stoplossapplydetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = pri_stoplossapplydetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter stoplossweightpara = new SqlParameter("@StopLossWeight", SqlDbType.Decimal, 9);
            stoplossweightpara.Value = pri_stoplossapplydetail.StopLossWeight;
            paras.Add(stoplossweightpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = pri_stoplossapplydetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel InvalidAll(UserModel user, int stopLossApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Pri_StopLossApplyDetail set DetailStatus = {0} where StopLossApplyId = {1}", (int)Common.StatusEnum.已作废, stopLossApplyId);
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

        public ResultModel Load(UserModel user, int stopLossApplyId, Common.StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            SqlDataReader dr = null;

            try
            {
                string cmdText = string.Format("select * from dbo.Pri_StopLossApplyDetail where StopLossApplyId={0} and DetailStatus>={1}", stopLossApplyId, (int)StatusEnum.已生效);
                dr = SqlHelper.ExecuteReader(this.ConnectString, CommandType.Text, cmdText, null);

                List<Model.StopLossApplyDetail> stopLossApplyDetails = new List<StopLossApplyDetail>();

                int i = 0;
                while (dr.Read())
                {
                    stopLossApplyDetails.Add(CreateModel(dr) as Model.StopLossApplyDetail);
                    i++;
                }

                result.AffectCount = i;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = stopLossApplyDetails;
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
