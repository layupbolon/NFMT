/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PricingDAL.cs
// 文件功能描述：点价表dbo.Pri_Pricing数据交互类。
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
using System.Linq;

namespace NFMT.DoPrice.DAL
{
    /// <summary>
    /// 点价表dbo.Pri_Pricing数据交互类。
    /// </summary>
    public class PricingDAL : ExecOperate, IPricingDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PricingDAL()
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
            Pricing pri_pricing = (Pricing)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@PricingId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter pricingapplyidpara = new SqlParameter("@PricingApplyId", SqlDbType.Int, 4);
            pricingapplyidpara.Value = pri_pricing.PricingApplyId;
            paras.Add(pricingapplyidpara);

            SqlParameter pricingweightpara = new SqlParameter("@PricingWeight", SqlDbType.Decimal, 9);
            pricingweightpara.Value = pri_pricing.PricingWeight;
            paras.Add(pricingweightpara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = pri_pricing.MUId;
            paras.Add(muidpara);

            SqlParameter exchangeidpara = new SqlParameter("@ExchangeId", SqlDbType.Int, 4);
            exchangeidpara.Value = pri_pricing.ExchangeId;
            paras.Add(exchangeidpara);

            SqlParameter futurescodeidpara = new SqlParameter("@FuturesCodeId", SqlDbType.Int, 4);
            futurescodeidpara.Value = pri_pricing.FuturesCodeId;
            paras.Add(futurescodeidpara);

            SqlParameter futurescodeenddatepara = new SqlParameter("@FuturesCodeEndDate", SqlDbType.DateTime, 8);
            futurescodeenddatepara.Value = pri_pricing.FuturesCodeEndDate;
            paras.Add(futurescodeenddatepara);

            SqlParameter spotqppara = new SqlParameter("@SpotQP", SqlDbType.DateTime, 8);
            spotqppara.Value = pri_pricing.SpotQP;
            paras.Add(spotqppara);

            SqlParameter delayfeepara = new SqlParameter("@DelayFee", SqlDbType.Decimal, 9);
            delayfeepara.Value = pri_pricing.DelayFee;
            paras.Add(delayfeepara);

            SqlParameter spreadpara = new SqlParameter("@Spread", SqlDbType.Decimal, 9);
            spreadpara.Value = pri_pricing.Spread;
            paras.Add(spreadpara);

            SqlParameter otherfeepara = new SqlParameter("@OtherFee", SqlDbType.Decimal, 9);
            otherfeepara.Value = pri_pricing.OtherFee;
            paras.Add(otherfeepara);

            SqlParameter avgpricepara = new SqlParameter("@AvgPrice", SqlDbType.Decimal, 9);
            avgpricepara.Value = pri_pricing.AvgPrice;
            paras.Add(avgpricepara);

            SqlParameter pricingtimepara = new SqlParameter("@PricingTime", SqlDbType.DateTime, 8);
            pricingtimepara.Value = pri_pricing.PricingTime;
            paras.Add(pricingtimepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = pri_pricing.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter pricingerpara = new SqlParameter("@Pricinger", SqlDbType.Int, 4);
            pricingerpara.Value = pri_pricing.Pricinger;
            paras.Add(pricingerpara);

            SqlParameter assertidpara = new SqlParameter("@AssertId", SqlDbType.Int, 4);
            assertidpara.Value = pri_pricing.AssertId;
            paras.Add(assertidpara);

            SqlParameter pricingdirectionpara = new SqlParameter("@PricingDirection", SqlDbType.Int, 4);
            pricingdirectionpara.Value = pri_pricing.PricingDirection;
            paras.Add(pricingdirectionpara);

            SqlParameter pricingstatuspara = new SqlParameter("@PricingStatus", SqlDbType.Int, 4);
            pricingstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(pricingstatuspara);

            SqlParameter finalpricepara = new SqlParameter("@FinalPrice", SqlDbType.Decimal, 9);
            finalpricepara.Value = pri_pricing.FinalPrice;
            paras.Add(finalpricepara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            Pricing pricing = new Pricing();

            pricing.PricingId = Convert.ToInt32(dr["PricingId"]);

            if (dr["PricingApplyId"] != DBNull.Value)
            {
                pricing.PricingApplyId = Convert.ToInt32(dr["PricingApplyId"]);
            }

            if (dr["PricingWeight"] != DBNull.Value)
            {
                pricing.PricingWeight = Convert.ToDecimal(dr["PricingWeight"]);
            }

            if (dr["MUId"] != DBNull.Value)
            {
                pricing.MUId = Convert.ToInt32(dr["MUId"]);
            }

            if (dr["ExchangeId"] != DBNull.Value)
            {
                pricing.ExchangeId = Convert.ToInt32(dr["ExchangeId"]);
            }

            if (dr["FuturesCodeId"] != DBNull.Value)
            {
                pricing.FuturesCodeId = Convert.ToInt32(dr["FuturesCodeId"]);
            }

            if (dr["FuturesCodeEndDate"] != DBNull.Value)
            {
                pricing.FuturesCodeEndDate = Convert.ToDateTime(dr["FuturesCodeEndDate"]);
            }

            if (dr["SpotQP"] != DBNull.Value)
            {
                pricing.SpotQP = Convert.ToDateTime(dr["SpotQP"]);
            }

            if (dr["DelayFee"] != DBNull.Value)
            {
                pricing.DelayFee = Convert.ToDecimal(dr["DelayFee"]);
            }

            if (dr["Spread"] != DBNull.Value)
            {
                pricing.Spread = Convert.ToDecimal(dr["Spread"]);
            }

            if (dr["OtherFee"] != DBNull.Value)
            {
                pricing.OtherFee = Convert.ToDecimal(dr["OtherFee"]);
            }

            if (dr["AvgPrice"] != DBNull.Value)
            {
                pricing.AvgPrice = Convert.ToDecimal(dr["AvgPrice"]);
            }

            if (dr["PricingTime"] != DBNull.Value)
            {
                pricing.PricingTime = Convert.ToDateTime(dr["PricingTime"]);
            }

            if (dr["CurrencyId"] != DBNull.Value)
            {
                pricing.CurrencyId = Convert.ToInt32(dr["CurrencyId"]);
            }

            if (dr["Pricinger"] != DBNull.Value)
            {
                pricing.Pricinger = Convert.ToInt32(dr["Pricinger"]);
            }

            if (dr["AssertId"] != DBNull.Value)
            {
                pricing.AssertId = Convert.ToInt32(dr["AssertId"]);
            }

            if (dr["PricingDirection"] != DBNull.Value)
            {
                pricing.PricingDirection = Convert.ToInt32(dr["PricingDirection"]);
            }

            if (dr["PricingStatus"] != DBNull.Value)
            {
                pricing.PricingStatus = (Common.StatusEnum)Convert.ToInt32(dr["PricingStatus"]);
            }

            if (dr["FinalPrice"] != DBNull.Value)
            {
                pricing.FinalPrice = Convert.ToDecimal(dr["FinalPrice"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                pricing.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                pricing.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                pricing.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pricing.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return pricing;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Pricing pricing = new Pricing();

            int indexPricingId = dr.GetOrdinal("PricingId");
            pricing.PricingId = Convert.ToInt32(dr[indexPricingId]);

            int indexPricingApplyId = dr.GetOrdinal("PricingApplyId");
            if (dr["PricingApplyId"] != DBNull.Value)
            {
                pricing.PricingApplyId = Convert.ToInt32(dr[indexPricingApplyId]);
            }

            int indexPricingWeight = dr.GetOrdinal("PricingWeight");
            if (dr["PricingWeight"] != DBNull.Value)
            {
                pricing.PricingWeight = Convert.ToDecimal(dr[indexPricingWeight]);
            }

            int indexMUId = dr.GetOrdinal("MUId");
            if (dr["MUId"] != DBNull.Value)
            {
                pricing.MUId = Convert.ToInt32(dr[indexMUId]);
            }

            int indexExchangeId = dr.GetOrdinal("ExchangeId");
            if (dr["ExchangeId"] != DBNull.Value)
            {
                pricing.ExchangeId = Convert.ToInt32(dr[indexExchangeId]);
            }

            int indexFuturesCodeId = dr.GetOrdinal("FuturesCodeId");
            if (dr["FuturesCodeId"] != DBNull.Value)
            {
                pricing.FuturesCodeId = Convert.ToInt32(dr[indexFuturesCodeId]);
            }

            int indexFuturesCodeEndDate = dr.GetOrdinal("FuturesCodeEndDate");
            if (dr["FuturesCodeEndDate"] != DBNull.Value)
            {
                pricing.FuturesCodeEndDate = Convert.ToDateTime(dr[indexFuturesCodeEndDate]);
            }

            int indexSpotQP = dr.GetOrdinal("SpotQP");
            if (dr["SpotQP"] != DBNull.Value)
            {
                pricing.SpotQP = Convert.ToDateTime(dr[indexSpotQP]);
            }

            int indexDelayFee = dr.GetOrdinal("DelayFee");
            if (dr["DelayFee"] != DBNull.Value)
            {
                pricing.DelayFee = Convert.ToDecimal(dr[indexDelayFee]);
            }

            int indexSpread = dr.GetOrdinal("Spread");
            if (dr["Spread"] != DBNull.Value)
            {
                pricing.Spread = Convert.ToDecimal(dr[indexSpread]);
            }

            int indexOtherFee = dr.GetOrdinal("OtherFee");
            if (dr["OtherFee"] != DBNull.Value)
            {
                pricing.OtherFee = Convert.ToDecimal(dr[indexOtherFee]);
            }

            int indexAvgPrice = dr.GetOrdinal("AvgPrice");
            if (dr["AvgPrice"] != DBNull.Value)
            {
                pricing.AvgPrice = Convert.ToDecimal(dr[indexAvgPrice]);
            }

            int indexPricingTime = dr.GetOrdinal("PricingTime");
            if (dr["PricingTime"] != DBNull.Value)
            {
                pricing.PricingTime = Convert.ToDateTime(dr[indexPricingTime]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                pricing.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexPricinger = dr.GetOrdinal("Pricinger");
            if (dr["Pricinger"] != DBNull.Value)
            {
                pricing.Pricinger = Convert.ToInt32(dr[indexPricinger]);
            }

            int indexAssertId = dr.GetOrdinal("AssertId");
            if (dr["AssertId"] != DBNull.Value)
            {
                pricing.AssertId = Convert.ToInt32(dr[indexAssertId]);
            }

            int indexPricingDirection = dr.GetOrdinal("PricingDirection");
            if (dr["PricingDirection"] != DBNull.Value)
            {
                pricing.PricingDirection = Convert.ToInt32(dr[indexPricingDirection]);
            }

            int indexPricingStatus = dr.GetOrdinal("PricingStatus");
            if (dr["PricingStatus"] != DBNull.Value)
            {
                pricing.PricingStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexPricingStatus]);
            }

            int indexFinalPrice = dr.GetOrdinal("FinalPrice");
            if (dr["FinalPrice"] != DBNull.Value)
            {
                pricing.FinalPrice = Convert.ToDecimal(dr[indexFinalPrice]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                pricing.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                pricing.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                pricing.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pricing.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return pricing;
        }

        public override string TableName
        {
            get
            {
                return "Pri_Pricing";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Pricing pri_pricing = (Pricing)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter pricingidpara = new SqlParameter("@PricingId", SqlDbType.Int, 4);
            pricingidpara.Value = pri_pricing.PricingId;
            paras.Add(pricingidpara);

            SqlParameter pricingapplyidpara = new SqlParameter("@PricingApplyId", SqlDbType.Int, 4);
            pricingapplyidpara.Value = pri_pricing.PricingApplyId;
            paras.Add(pricingapplyidpara);

            SqlParameter pricingweightpara = new SqlParameter("@PricingWeight", SqlDbType.Decimal, 9);
            pricingweightpara.Value = pri_pricing.PricingWeight;
            paras.Add(pricingweightpara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = pri_pricing.MUId;
            paras.Add(muidpara);

            SqlParameter exchangeidpara = new SqlParameter("@ExchangeId", SqlDbType.Int, 4);
            exchangeidpara.Value = pri_pricing.ExchangeId;
            paras.Add(exchangeidpara);

            SqlParameter futurescodeidpara = new SqlParameter("@FuturesCodeId", SqlDbType.Int, 4);
            futurescodeidpara.Value = pri_pricing.FuturesCodeId;
            paras.Add(futurescodeidpara);

            SqlParameter futurescodeenddatepara = new SqlParameter("@FuturesCodeEndDate", SqlDbType.DateTime, 8);
            futurescodeenddatepara.Value = pri_pricing.FuturesCodeEndDate;
            paras.Add(futurescodeenddatepara);

            SqlParameter spotqppara = new SqlParameter("@SpotQP", SqlDbType.DateTime, 8);
            spotqppara.Value = pri_pricing.SpotQP;
            paras.Add(spotqppara);

            SqlParameter delayfeepara = new SqlParameter("@DelayFee", SqlDbType.Decimal, 9);
            delayfeepara.Value = pri_pricing.DelayFee;
            paras.Add(delayfeepara);

            SqlParameter spreadpara = new SqlParameter("@Spread", SqlDbType.Decimal, 9);
            spreadpara.Value = pri_pricing.Spread;
            paras.Add(spreadpara);

            SqlParameter otherfeepara = new SqlParameter("@OtherFee", SqlDbType.Decimal, 9);
            otherfeepara.Value = pri_pricing.OtherFee;
            paras.Add(otherfeepara);

            SqlParameter avgpricepara = new SqlParameter("@AvgPrice", SqlDbType.Decimal, 9);
            avgpricepara.Value = pri_pricing.AvgPrice;
            paras.Add(avgpricepara);

            SqlParameter pricingtimepara = new SqlParameter("@PricingTime", SqlDbType.DateTime, 8);
            pricingtimepara.Value = pri_pricing.PricingTime;
            paras.Add(pricingtimepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = pri_pricing.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter pricingerpara = new SqlParameter("@Pricinger", SqlDbType.Int, 4);
            pricingerpara.Value = pri_pricing.Pricinger;
            paras.Add(pricingerpara);

            SqlParameter assertidpara = new SqlParameter("@AssertId", SqlDbType.Int, 4);
            assertidpara.Value = pri_pricing.AssertId;
            paras.Add(assertidpara);

            SqlParameter pricingdirectionpara = new SqlParameter("@PricingDirection", SqlDbType.Int, 4);
            pricingdirectionpara.Value = pri_pricing.PricingDirection;
            paras.Add(pricingdirectionpara);

            SqlParameter pricingstatuspara = new SqlParameter("@PricingStatus", SqlDbType.Int, 4);
            pricingstatuspara.Value = pri_pricing.PricingStatus;
            paras.Add(pricingstatuspara);

            SqlParameter finalpricepara = new SqlParameter("@FinalPrice", SqlDbType.Decimal, 9);
            finalpricepara.Value = pri_pricing.FinalPrice;
            paras.Add(finalpricepara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetCanDoPriceDetailIds(UserModel user, int pricingApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select detail.DetailId from dbo.Pri_PricingApplyDetail detail where detail.PricingApplyId = {0} and detail.DetailStatus >= {1} and detail.DetailId not in (select PricingApplyDetailId from dbo.Pri_PricingDetail a where a.DetailStatus <> {2})", pricingApplyId, (int)Common.StatusEnum.已生效, (int)Common.StatusEnum.已作废);
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

        /// <summary>
        /// 验证点价重量是否超过可点价范围
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pricingApplyId">点价申请序号</param>
        /// <param name="futuresCodeId">期货合约序号</param>
        /// <param name="pricingApplyWeight">点价申请重量</param>
        /// <param name="pricingWeight">录入点价重量</param>
        /// <param name="isUpdate">是否在修改时调用</param>
        /// <param name="pricingWeightBefore">之前录入的点价重量</param>
        /// <returns></returns>
        public ResultModel IsWeightCanPricing(UserModel user, int pricingApplyId, int futuresCodeId, decimal pricingApplyWeight, decimal pricingWeight, bool isUpdate, decimal pricingWeightBefore)
        {
            ResultModel result = new ResultModel();

            if (pricingApplyWeight <= 0)
            {
                result.ResultStatus = -1;
                result.Message = "申请重量错误";
                return result;
            }

            try
            {
                string sql = string.Format("select ISNULL(sum(PricingWeight),0) as PricingWeight from dbo.Pri_Pricing where PricingApplyId = {0} and PricingStatus <> {1} ", pricingApplyId, (int)Common.StatusEnum.已作废);
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
                    resultWeight = (Convert.ToInt32(Math.Floor((pricingApplyWeight - alreadyApplyWeight) / futuresCode.CodeSize)) + 1) * futuresCode.CodeSize;
                else
                    resultWeight = (Convert.ToInt32(Math.Floor((pricingApplyWeight - alreadyApplyWeight + pricingWeightBefore) / futuresCode.CodeSize)) + 1) * futuresCode.CodeSize;

                if (pricingWeight <= resultWeight)
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
                    result.Message = string.Format("点价重量不可超过【{0}】{1}", resultWeight, muName);
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

        public ResultModel GetAlreadyStopLossWeight(UserModel user, int pricingId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select ISNULL(SUM(StopLossWeight),0) from dbo.Pri_StopLossApply sla left join dbo.Apply a on sla.ApplyId = a.ApplyId where sla.PricingId = {0} and a.ApplyStatus <> {1} ", pricingId, (int)Common.StatusEnum.已作废);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                decimal alreadyStopLossWeight = 0;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && decimal.TryParse(obj.ToString(), out alreadyStopLossWeight))
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = alreadyStopLossWeight;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                    result.ReturnValue = alreadyStopLossWeight;
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取子合约下的点价列表
        /// </summary>
        /// <param name="user"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        public ResultModel Load(UserModel user, int subId)
        {
            int entryStatus = (int)StatusEnum.已录入;

            string cmdText = string.Format("select pri.* from dbo.Pri_Pricing pri inner join dbo.Pri_PricingApply pa on pa.PricingApplyId = pri.PricingApplyId where pri.PricingStatus >={0} and pa.SubContractId = {1}", entryStatus, subId);

            return Load<Model.Pricing>(user, CommandType.Text, cmdText);
        }

        public override int MenuId
        {
            get
            {
                return 60;
            }
        }

        public override IAuthority Authority
        {
            get
            {
                return new NFMT.Authority.ContractAuth();
            }
        }

        #endregion
    }
}
