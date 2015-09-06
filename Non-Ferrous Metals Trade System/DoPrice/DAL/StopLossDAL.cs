/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StopLossDAL.cs
// 文件功能描述：止损表dbo.Pri_StopLoss数据交互类。
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
using System.Linq;

namespace NFMT.DoPrice.DAL
{
    /// <summary>
    /// 止损表dbo.Pri_StopLoss数据交互类。
    /// </summary>
    public class StopLossDAL : ExecOperate, IStopLossDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StopLossDAL()
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
            StopLoss pri_stoploss = (StopLoss)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StopLossId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stoplossapplyidpara = new SqlParameter("@StopLossApplyId", SqlDbType.Int, 4);
            stoplossapplyidpara.Value = pri_stoploss.StopLossApplyId;
            paras.Add(stoplossapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = pri_stoploss.ApplyId;
            paras.Add(applyidpara);

            SqlParameter stoplossweightpara = new SqlParameter("@StopLossWeight", SqlDbType.Decimal, 9);
            stoplossweightpara.Value = pri_stoploss.StopLossWeight;
            paras.Add(stoplossweightpara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = pri_stoploss.MUId;
            paras.Add(muidpara);

            SqlParameter exchangeidpara = new SqlParameter("@ExchangeId", SqlDbType.Int, 4);
            exchangeidpara.Value = pri_stoploss.ExchangeId;
            paras.Add(exchangeidpara);

            SqlParameter futurescodeidpara = new SqlParameter("@FuturesCodeId", SqlDbType.Int, 4);
            futurescodeidpara.Value = pri_stoploss.FuturesCodeId;
            paras.Add(futurescodeidpara);

            SqlParameter avgpricepara = new SqlParameter("@AvgPrice", SqlDbType.Decimal, 9);
            avgpricepara.Value = pri_stoploss.AvgPrice;
            paras.Add(avgpricepara);

            SqlParameter pricingtimepara = new SqlParameter("@PricingTime", SqlDbType.DateTime, 8);
            pricingtimepara.Value = pri_stoploss.PricingTime;
            paras.Add(pricingtimepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = pri_stoploss.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter stoplosserpara = new SqlParameter("@StopLosser", SqlDbType.Int, 4);
            stoplosserpara.Value = pri_stoploss.StopLosser;
            paras.Add(stoplosserpara);

            SqlParameter assertidpara = new SqlParameter("@AssertId", SqlDbType.Int, 4);
            assertidpara.Value = pri_stoploss.AssertId;
            paras.Add(assertidpara);

            SqlParameter pricingdirectionpara = new SqlParameter("@PricingDirection", SqlDbType.Int, 4);
            pricingdirectionpara.Value = pri_stoploss.PricingDirection;
            paras.Add(pricingdirectionpara);

            SqlParameter stoplossstatuspara = new SqlParameter("@StopLossStatus", SqlDbType.Int, 4);
            stoplossstatuspara.Value = pri_stoploss.StopLossStatus;
            paras.Add(stoplossstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StopLoss stoploss = new StopLoss();

            int indexStopLossId = dr.GetOrdinal("StopLossId");
            stoploss.StopLossId = Convert.ToInt32(dr[indexStopLossId]);

            int indexStopLossApplyId = dr.GetOrdinal("StopLossApplyId");
            if (dr["StopLossApplyId"] != DBNull.Value)
            {
                stoploss.StopLossApplyId = Convert.ToInt32(dr[indexStopLossApplyId]);
            }

            int indexApplyId = dr.GetOrdinal("ApplyId");
            if (dr["ApplyId"] != DBNull.Value)
            {
                stoploss.ApplyId = Convert.ToInt32(dr[indexApplyId]);
            }

            int indexStopLossWeight = dr.GetOrdinal("StopLossWeight");
            if (dr["StopLossWeight"] != DBNull.Value)
            {
                stoploss.StopLossWeight = Convert.ToDecimal(dr[indexStopLossWeight]);
            }

            int indexMUId = dr.GetOrdinal("MUId");
            if (dr["MUId"] != DBNull.Value)
            {
                stoploss.MUId = Convert.ToInt32(dr[indexMUId]);
            }

            int indexExchangeId = dr.GetOrdinal("ExchangeId");
            if (dr["ExchangeId"] != DBNull.Value)
            {
                stoploss.ExchangeId = Convert.ToInt32(dr[indexExchangeId]);
            }

            int indexFuturesCodeId = dr.GetOrdinal("FuturesCodeId");
            if (dr["FuturesCodeId"] != DBNull.Value)
            {
                stoploss.FuturesCodeId = Convert.ToInt32(dr[indexFuturesCodeId]);
            }

            int indexAvgPrice = dr.GetOrdinal("AvgPrice");
            if (dr["AvgPrice"] != DBNull.Value)
            {
                stoploss.AvgPrice = Convert.ToDecimal(dr[indexAvgPrice]);
            }

            int indexPricingTime = dr.GetOrdinal("PricingTime");
            if (dr["PricingTime"] != DBNull.Value)
            {
                stoploss.PricingTime = Convert.ToDateTime(dr[indexPricingTime]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                stoploss.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexStopLosser = dr.GetOrdinal("StopLosser");
            if (dr["StopLosser"] != DBNull.Value)
            {
                stoploss.StopLosser = Convert.ToInt32(dr[indexStopLosser]);
            }

            int indexAssertId = dr.GetOrdinal("AssertId");
            if (dr["AssertId"] != DBNull.Value)
            {
                stoploss.AssertId = Convert.ToInt32(dr[indexAssertId]);
            }

            int indexPricingDirection = dr.GetOrdinal("PricingDirection");
            if (dr["PricingDirection"] != DBNull.Value)
            {
                stoploss.PricingDirection = Convert.ToInt32(dr[indexPricingDirection]);
            }

            int indexStopLossStatus = dr.GetOrdinal("StopLossStatus");
            if (dr["StopLossStatus"] != DBNull.Value)
            {
                stoploss.StopLossStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexStopLossStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                stoploss.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                stoploss.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                stoploss.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                stoploss.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return stoploss;
        }

        public override string TableName
        {
            get
            {
                return "Pri_StopLoss";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StopLoss pri_stoploss = (StopLoss)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stoplossidpara = new SqlParameter("@StopLossId", SqlDbType.Int, 4);
            stoplossidpara.Value = pri_stoploss.StopLossId;
            paras.Add(stoplossidpara);

            SqlParameter stoplossapplyidpara = new SqlParameter("@StopLossApplyId", SqlDbType.Int, 4);
            stoplossapplyidpara.Value = pri_stoploss.StopLossApplyId;
            paras.Add(stoplossapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = pri_stoploss.ApplyId;
            paras.Add(applyidpara);

            SqlParameter stoplossweightpara = new SqlParameter("@StopLossWeight", SqlDbType.Decimal, 9);
            stoplossweightpara.Value = pri_stoploss.StopLossWeight;
            paras.Add(stoplossweightpara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = pri_stoploss.MUId;
            paras.Add(muidpara);

            SqlParameter exchangeidpara = new SqlParameter("@ExchangeId", SqlDbType.Int, 4);
            exchangeidpara.Value = pri_stoploss.ExchangeId;
            paras.Add(exchangeidpara);

            SqlParameter futurescodeidpara = new SqlParameter("@FuturesCodeId", SqlDbType.Int, 4);
            futurescodeidpara.Value = pri_stoploss.FuturesCodeId;
            paras.Add(futurescodeidpara);

            SqlParameter avgpricepara = new SqlParameter("@AvgPrice", SqlDbType.Decimal, 9);
            avgpricepara.Value = pri_stoploss.AvgPrice;
            paras.Add(avgpricepara);

            SqlParameter pricingtimepara = new SqlParameter("@PricingTime", SqlDbType.DateTime, 8);
            pricingtimepara.Value = pri_stoploss.PricingTime;
            paras.Add(pricingtimepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = pri_stoploss.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter stoplosserpara = new SqlParameter("@StopLosser", SqlDbType.Int, 4);
            stoplosserpara.Value = pri_stoploss.StopLosser;
            paras.Add(stoplosserpara);

            SqlParameter assertidpara = new SqlParameter("@AssertId", SqlDbType.Int, 4);
            assertidpara.Value = pri_stoploss.AssertId;
            paras.Add(assertidpara);

            SqlParameter pricingdirectionpara = new SqlParameter("@PricingDirection", SqlDbType.Int, 4);
            pricingdirectionpara.Value = pri_stoploss.PricingDirection;
            paras.Add(pricingdirectionpara);

            SqlParameter stoplossstatuspara = new SqlParameter("@StopLossStatus", SqlDbType.Int, 4);
            stoplossstatuspara.Value = pri_stoploss.StopLossStatus;
            paras.Add(stoplossstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetCanStopLossDetailIds(UserModel user, int stopLossApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select detail.DetailId from dbo.Pri_StopLossApplyDetail detail where detail.StopLossApplyId = {0} and detail.DetailStatus >= {1} and detail.DetailId not in (select StopLossApplyDetailId from dbo.Pri_StopLossDetail a where a.DetailStatus <> {2})", stopLossApplyId, (int)Common.StatusEnum.已生效, (int)Common.StatusEnum.已作废);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string str = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        str += dr["DetailId"].ToString() + ",";
                    }
                    if (!string.IsNullOrEmpty(str))
                        str = str.Substring(0, str.Length - 1);

                    result.ResultStatus = 0;
                    result.ReturnValue = str;
                    result.Message = "获取成功";
                }
                else
                {
                    result.Message = "获取失败";
                    result.ResultStatus = 0;
                    result.ReturnValue = 0;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel IsWeightCanStopLoss(UserModel user, int stopLossApplyId, int futuresCodeId, decimal stopLossApplyWeight, decimal stopLossWeight, bool isUpdate, decimal stopLossWeightBefore)
        {
            ResultModel result = new ResultModel();

            if (stopLossApplyWeight <= 0)
            {
                result.ResultStatus = -1;
                result.Message = "申请重量错误";
                return result;
            }

            try
            {
                string sql = string.Format("select ISNULL(sum(StopLossWeight),0) as StopLossWeight from dbo.Pri_StopLoss where StopLossApplyId = {0} and StopLossStatus <> {1} ", stopLossApplyId, (int)Common.StatusEnum.已作废);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                decimal alreadyApplyWeight = 0;
                if (obj == null || string.IsNullOrEmpty(obj.ToString()) || !decimal.TryParse(obj.ToString(), out alreadyApplyWeight))
                {
                    result.ResultStatus = -1;
                    result.Message = "获取已申请重量错误";
                    return result;
                }

                NFMT.Data.Model.FuturesCode futuresCode = NFMT.Data.BasicDataProvider.FuturesCodes.SingleOrDefault(a => a.FuturesCodeId == futuresCodeId);
                if (futuresCode == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取期货合约失败";
                    return result;
                }

                decimal resultWeight = 0;
                if (!isUpdate)
                    resultWeight = (Convert.ToInt32(Math.Floor((stopLossApplyWeight - alreadyApplyWeight) / futuresCode.CodeSize)) + 1) * futuresCode.CodeSize;
                else
                    resultWeight = (Convert.ToInt32(Math.Floor((stopLossApplyWeight - alreadyApplyWeight + stopLossWeightBefore) / futuresCode.CodeSize)) + 1) * futuresCode.CodeSize;

                if (stopLossWeight <= resultWeight)
                {
                    result.ResultStatus = 0;
                    result.Message = "验证通过";
                    result.ReturnValue = resultWeight;
                }
                else
                {
                    NFMT.Data.Model.MeasureUnit measureUnit = NFMT.Data.BasicDataProvider.MeasureUnits.SingleOrDefault(a => a.MUId == futuresCode.MUId);
                    string muName = string.Empty;
                    if (measureUnit != null)
                        muName = measureUnit.MUName;

                    result.ResultStatus = -1;
                    result.Message = string.Format("止损重量不可超过【{0}】{1}", resultWeight, muName);
                    result.ReturnValue = resultWeight;
                }

            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public override int MenuId
        {
            get
            {
                return 92;
            }
        }

        #endregion
    }
}
