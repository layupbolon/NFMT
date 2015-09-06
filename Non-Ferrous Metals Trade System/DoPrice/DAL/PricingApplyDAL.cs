/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PricingApplyDAL.cs
// 文件功能描述：点价申请dbo.Pri_PricingApply数据交互类。
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
    /// 点价申请dbo.Pri_PricingApply数据交互类。
    /// </summary>
    public partial class PricingApplyDAL : ApplyOperate, IPricingApplyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PricingApplyDAL()
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
            PricingApply pri_pricingapply = (PricingApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@PricingApplyId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = pri_pricingapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = pri_pricingapply.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = pri_pricingapply.ContractId;
            paras.Add(contractidpara);

            SqlParameter pricingdirectionpara = new SqlParameter("@PricingDirection", SqlDbType.Int, 4);
            pricingdirectionpara.Value = pri_pricingapply.PricingDirection;
            paras.Add(pricingdirectionpara);

            SqlParameter qpdatepara = new SqlParameter("@QPDate", SqlDbType.DateTime, 8);
            qpdatepara.Value = pri_pricingapply.QPDate;
            paras.Add(qpdatepara);

            SqlParameter delayamountpara = new SqlParameter("@DelayAmount", SqlDbType.Decimal, 9);
            delayamountpara.Value = pri_pricingapply.DelayAmount;
            paras.Add(delayamountpara);

            SqlParameter delayfeepara = new SqlParameter("@DelayFee", SqlDbType.Decimal, 9);
            delayfeepara.Value = pri_pricingapply.DelayFee;
            paras.Add(delayfeepara);

            SqlParameter delayqpdatepara = new SqlParameter("@DelayQPDate", SqlDbType.DateTime, 8);
            delayqpdatepara.Value = pri_pricingapply.DelayQPDate;
            paras.Add(delayqpdatepara);

            SqlParameter otherfeepara = new SqlParameter("@OtherFee", SqlDbType.Decimal, 9);
            otherfeepara.Value = pri_pricingapply.OtherFee;
            paras.Add(otherfeepara);

            if (!string.IsNullOrEmpty(pri_pricingapply.OtherDesc))
            {
                SqlParameter otherdescpara = new SqlParameter("@OtherDesc", SqlDbType.VarChar, 800);
                otherdescpara.Value = pri_pricingapply.OtherDesc;
                paras.Add(otherdescpara);
            }

            SqlParameter starttimepara = new SqlParameter("@StartTime", SqlDbType.DateTime, 8);
            starttimepara.Value = pri_pricingapply.StartTime;
            paras.Add(starttimepara);

            SqlParameter endtimepara = new SqlParameter("@EndTime", SqlDbType.DateTime, 8);
            endtimepara.Value = pri_pricingapply.EndTime;
            paras.Add(endtimepara);

            SqlParameter minpricepara = new SqlParameter("@MinPrice", SqlDbType.Decimal, 9);
            minpricepara.Value = pri_pricingapply.MinPrice;
            paras.Add(minpricepara);

            SqlParameter maxpricepara = new SqlParameter("@MaxPrice", SqlDbType.Decimal, 9);
            maxpricepara.Value = pri_pricingapply.MaxPrice;
            paras.Add(maxpricepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = pri_pricingapply.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter pricingblocidpara = new SqlParameter("@PricingBlocId", SqlDbType.Int, 4);
            pricingblocidpara.Value = pri_pricingapply.PricingBlocId;
            paras.Add(pricingblocidpara);

            SqlParameter pricingcorpidpara = new SqlParameter("@PricingCorpId", SqlDbType.Int, 4);
            pricingcorpidpara.Value = pri_pricingapply.PricingCorpId;
            paras.Add(pricingcorpidpara);

            SqlParameter pricingweightpara = new SqlParameter("@PricingWeight", SqlDbType.Decimal, 9);
            pricingweightpara.Value = pri_pricingapply.PricingWeight;
            paras.Add(pricingweightpara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = pri_pricingapply.MUId;
            paras.Add(muidpara);

            SqlParameter assertidpara = new SqlParameter("@AssertId", SqlDbType.Int, 4);
            assertidpara.Value = pri_pricingapply.AssertId;
            paras.Add(assertidpara);

            SqlParameter pricingpersoinidpara = new SqlParameter("@PricingPersoinId", SqlDbType.Int, 4);
            pricingpersoinidpara.Value = pri_pricingapply.PricingPersoinId;
            paras.Add(pricingpersoinidpara);

            SqlParameter pricingstylepara = new SqlParameter("@PricingStyle", SqlDbType.Int, 4);
            pricingstylepara.Value = pri_pricingapply.PricingStyle;
            paras.Add(pricingstylepara);

            SqlParameter declaredatepara = new SqlParameter("@DeclareDate", SqlDbType.DateTime, 8);
            declaredatepara.Value = pri_pricingapply.DeclareDate;
            paras.Add(declaredatepara);

            SqlParameter avgpricestartpara = new SqlParameter("@AvgPriceStart", SqlDbType.DateTime, 8);
            avgpricestartpara.Value = pri_pricingapply.AvgPriceStart;
            paras.Add(avgpricestartpara);

            SqlParameter avgpriceendpara = new SqlParameter("@AvgPriceEnd", SqlDbType.DateTime, 8);
            avgpriceendpara.Value = pri_pricingapply.AvgPriceEnd;
            paras.Add(avgpriceendpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            PricingApply pricingapply = new PricingApply();

            pricingapply.PricingApplyId = Convert.ToInt32(dr["PricingApplyId"]);

            if (dr["ApplyId"] != DBNull.Value)
            {
                pricingapply.ApplyId = Convert.ToInt32(dr["ApplyId"]);
            }

            if (dr["SubContractId"] != DBNull.Value)
            {
                pricingapply.SubContractId = Convert.ToInt32(dr["SubContractId"]);
            }

            if (dr["ContractId"] != DBNull.Value)
            {
                pricingapply.ContractId = Convert.ToInt32(dr["ContractId"]);
            }

            if (dr["PricingDirection"] != DBNull.Value)
            {
                pricingapply.PricingDirection = Convert.ToInt32(dr["PricingDirection"]);
            }

            if (dr["QPDate"] != DBNull.Value)
            {
                pricingapply.QPDate = Convert.ToDateTime(dr["QPDate"]);
            }

            if (dr["DelayAmount"] != DBNull.Value)
            {
                pricingapply.DelayAmount = Convert.ToDecimal(dr["DelayAmount"]);
            }

            if (dr["DelayFee"] != DBNull.Value)
            {
                pricingapply.DelayFee = Convert.ToDecimal(dr["DelayFee"]);
            }

            if (dr["DelayQPDate"] != DBNull.Value)
            {
                pricingapply.DelayQPDate = Convert.ToDateTime(dr["DelayQPDate"]);
            }

            if (dr["OtherFee"] != DBNull.Value)
            {
                pricingapply.OtherFee = Convert.ToDecimal(dr["OtherFee"]);
            }

            if (dr["OtherDesc"] != DBNull.Value)
            {
                pricingapply.OtherDesc = Convert.ToString(dr["OtherDesc"]);
            }

            if (dr["StartTime"] != DBNull.Value)
            {
                pricingapply.StartTime = Convert.ToDateTime(dr["StartTime"]);
            }

            if (dr["EndTime"] != DBNull.Value)
            {
                pricingapply.EndTime = Convert.ToDateTime(dr["EndTime"]);
            }

            if (dr["MinPrice"] != DBNull.Value)
            {
                pricingapply.MinPrice = Convert.ToDecimal(dr["MinPrice"]);
            }

            if (dr["MaxPrice"] != DBNull.Value)
            {
                pricingapply.MaxPrice = Convert.ToDecimal(dr["MaxPrice"]);
            }

            if (dr["CurrencyId"] != DBNull.Value)
            {
                pricingapply.CurrencyId = Convert.ToInt32(dr["CurrencyId"]);
            }

            if (dr["PricingBlocId"] != DBNull.Value)
            {
                pricingapply.PricingBlocId = Convert.ToInt32(dr["PricingBlocId"]);
            }

            if (dr["PricingCorpId"] != DBNull.Value)
            {
                pricingapply.PricingCorpId = Convert.ToInt32(dr["PricingCorpId"]);
            }

            if (dr["PricingWeight"] != DBNull.Value)
            {
                pricingapply.PricingWeight = Convert.ToDecimal(dr["PricingWeight"]);
            }

            if (dr["MUId"] != DBNull.Value)
            {
                pricingapply.MUId = Convert.ToInt32(dr["MUId"]);
            }

            if (dr["AssertId"] != DBNull.Value)
            {
                pricingapply.AssertId = Convert.ToInt32(dr["AssertId"]);
            }

            if (dr["PricingPersoinId"] != DBNull.Value)
            {
                pricingapply.PricingPersoinId = Convert.ToInt32(dr["PricingPersoinId"]);
            }

            if (dr["PricingStyle"] != DBNull.Value)
            {
                pricingapply.PricingStyle = Convert.ToInt32(dr["PricingStyle"]);
            }

            if (dr["DeclareDate"] != DBNull.Value)
            {
                pricingapply.DeclareDate = Convert.ToDateTime(dr["DeclareDate"]);
            }

            if (dr["AvgPriceStart"] != DBNull.Value)
            {
                pricingapply.AvgPriceStart = Convert.ToDateTime(dr["AvgPriceStart"]);
            }

            if (dr["AvgPriceEnd"] != DBNull.Value)
            {
                pricingapply.AvgPriceEnd = Convert.ToDateTime(dr["AvgPriceEnd"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                pricingapply.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                pricingapply.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                pricingapply.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pricingapply.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return pricingapply;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PricingApply pricingapply = new PricingApply();

            int indexPricingApplyId = dr.GetOrdinal("PricingApplyId");
            pricingapply.PricingApplyId = Convert.ToInt32(dr[indexPricingApplyId]);

            int indexApplyId = dr.GetOrdinal("ApplyId");
            if (dr["ApplyId"] != DBNull.Value)
            {
                pricingapply.ApplyId = Convert.ToInt32(dr[indexApplyId]);
            }

            int indexSubContractId = dr.GetOrdinal("SubContractId");
            if (dr["SubContractId"] != DBNull.Value)
            {
                pricingapply.SubContractId = Convert.ToInt32(dr[indexSubContractId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                pricingapply.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexPricingDirection = dr.GetOrdinal("PricingDirection");
            if (dr["PricingDirection"] != DBNull.Value)
            {
                pricingapply.PricingDirection = Convert.ToInt32(dr[indexPricingDirection]);
            }

            int indexQPDate = dr.GetOrdinal("QPDate");
            if (dr["QPDate"] != DBNull.Value)
            {
                pricingapply.QPDate = Convert.ToDateTime(dr[indexQPDate]);
            }

            int indexDelayAmount = dr.GetOrdinal("DelayAmount");
            if (dr["DelayAmount"] != DBNull.Value)
            {
                pricingapply.DelayAmount = Convert.ToDecimal(dr[indexDelayAmount]);
            }

            int indexDelayFee = dr.GetOrdinal("DelayFee");
            if (dr["DelayFee"] != DBNull.Value)
            {
                pricingapply.DelayFee = Convert.ToDecimal(dr[indexDelayFee]);
            }

            int indexDelayQPDate = dr.GetOrdinal("DelayQPDate");
            if (dr["DelayQPDate"] != DBNull.Value)
            {
                pricingapply.DelayQPDate = Convert.ToDateTime(dr[indexDelayQPDate]);
            }

            int indexOtherFee = dr.GetOrdinal("OtherFee");
            if (dr["OtherFee"] != DBNull.Value)
            {
                pricingapply.OtherFee = Convert.ToDecimal(dr[indexOtherFee]);
            }

            int indexOtherDesc = dr.GetOrdinal("OtherDesc");
            if (dr["OtherDesc"] != DBNull.Value)
            {
                pricingapply.OtherDesc = Convert.ToString(dr[indexOtherDesc]);
            }

            int indexStartTime = dr.GetOrdinal("StartTime");
            if (dr["StartTime"] != DBNull.Value)
            {
                pricingapply.StartTime = Convert.ToDateTime(dr[indexStartTime]);
            }

            int indexEndTime = dr.GetOrdinal("EndTime");
            if (dr["EndTime"] != DBNull.Value)
            {
                pricingapply.EndTime = Convert.ToDateTime(dr[indexEndTime]);
            }

            int indexMinPrice = dr.GetOrdinal("MinPrice");
            if (dr["MinPrice"] != DBNull.Value)
            {
                pricingapply.MinPrice = Convert.ToDecimal(dr[indexMinPrice]);
            }

            int indexMaxPrice = dr.GetOrdinal("MaxPrice");
            if (dr["MaxPrice"] != DBNull.Value)
            {
                pricingapply.MaxPrice = Convert.ToDecimal(dr[indexMaxPrice]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                pricingapply.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexPricingBlocId = dr.GetOrdinal("PricingBlocId");
            if (dr["PricingBlocId"] != DBNull.Value)
            {
                pricingapply.PricingBlocId = Convert.ToInt32(dr[indexPricingBlocId]);
            }

            int indexPricingCorpId = dr.GetOrdinal("PricingCorpId");
            if (dr["PricingCorpId"] != DBNull.Value)
            {
                pricingapply.PricingCorpId = Convert.ToInt32(dr[indexPricingCorpId]);
            }

            int indexPricingWeight = dr.GetOrdinal("PricingWeight");
            if (dr["PricingWeight"] != DBNull.Value)
            {
                pricingapply.PricingWeight = Convert.ToDecimal(dr[indexPricingWeight]);
            }

            int indexMUId = dr.GetOrdinal("MUId");
            if (dr["MUId"] != DBNull.Value)
            {
                pricingapply.MUId = Convert.ToInt32(dr[indexMUId]);
            }

            int indexAssertId = dr.GetOrdinal("AssertId");
            if (dr["AssertId"] != DBNull.Value)
            {
                pricingapply.AssertId = Convert.ToInt32(dr[indexAssertId]);
            }

            int indexPricingPersoinId = dr.GetOrdinal("PricingPersoinId");
            if (dr["PricingPersoinId"] != DBNull.Value)
            {
                pricingapply.PricingPersoinId = Convert.ToInt32(dr[indexPricingPersoinId]);
            }

            int indexPricingStyle = dr.GetOrdinal("PricingStyle");
            if (dr["PricingStyle"] != DBNull.Value)
            {
                pricingapply.PricingStyle = Convert.ToInt32(dr[indexPricingStyle]);
            }

            int indexDeclareDate = dr.GetOrdinal("DeclareDate");
            if (dr["DeclareDate"] != DBNull.Value)
            {
                pricingapply.DeclareDate = Convert.ToDateTime(dr[indexDeclareDate]);
            }

            int indexAvgPriceStart = dr.GetOrdinal("AvgPriceStart");
            if (dr["AvgPriceStart"] != DBNull.Value)
            {
                pricingapply.AvgPriceStart = Convert.ToDateTime(dr[indexAvgPriceStart]);
            }

            int indexAvgPriceEnd = dr.GetOrdinal("AvgPriceEnd");
            if (dr["AvgPriceEnd"] != DBNull.Value)
            {
                pricingapply.AvgPriceEnd = Convert.ToDateTime(dr[indexAvgPriceEnd]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                pricingapply.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                pricingapply.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                pricingapply.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pricingapply.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return pricingapply;
        }

        public override string TableName
        {
            get
            {
                return "Pri_PricingApply";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PricingApply pri_pricingapply = (PricingApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter pricingapplyidpara = new SqlParameter("@PricingApplyId", SqlDbType.Int, 4);
            pricingapplyidpara.Value = pri_pricingapply.PricingApplyId;
            paras.Add(pricingapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = pri_pricingapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = pri_pricingapply.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = pri_pricingapply.ContractId;
            paras.Add(contractidpara);

            SqlParameter pricingdirectionpara = new SqlParameter("@PricingDirection", SqlDbType.Int, 4);
            pricingdirectionpara.Value = pri_pricingapply.PricingDirection;
            paras.Add(pricingdirectionpara);

            SqlParameter qpdatepara = new SqlParameter("@QPDate", SqlDbType.DateTime, 8);
            qpdatepara.Value = pri_pricingapply.QPDate;
            paras.Add(qpdatepara);

            SqlParameter delayamountpara = new SqlParameter("@DelayAmount", SqlDbType.Decimal, 9);
            delayamountpara.Value = pri_pricingapply.DelayAmount;
            paras.Add(delayamountpara);

            SqlParameter delayfeepara = new SqlParameter("@DelayFee", SqlDbType.Decimal, 9);
            delayfeepara.Value = pri_pricingapply.DelayFee;
            paras.Add(delayfeepara);

            SqlParameter delayqpdatepara = new SqlParameter("@DelayQPDate", SqlDbType.DateTime, 8);
            delayqpdatepara.Value = pri_pricingapply.DelayQPDate;
            paras.Add(delayqpdatepara);

            SqlParameter otherfeepara = new SqlParameter("@OtherFee", SqlDbType.Decimal, 9);
            otherfeepara.Value = pri_pricingapply.OtherFee;
            paras.Add(otherfeepara);

            if (!string.IsNullOrEmpty(pri_pricingapply.OtherDesc))
            {
                SqlParameter otherdescpara = new SqlParameter("@OtherDesc", SqlDbType.VarChar, 800);
                otherdescpara.Value = pri_pricingapply.OtherDesc;
                paras.Add(otherdescpara);
            }

            SqlParameter starttimepara = new SqlParameter("@StartTime", SqlDbType.DateTime, 8);
            starttimepara.Value = pri_pricingapply.StartTime;
            paras.Add(starttimepara);

            SqlParameter endtimepara = new SqlParameter("@EndTime", SqlDbType.DateTime, 8);
            endtimepara.Value = pri_pricingapply.EndTime;
            paras.Add(endtimepara);

            SqlParameter minpricepara = new SqlParameter("@MinPrice", SqlDbType.Decimal, 9);
            minpricepara.Value = pri_pricingapply.MinPrice;
            paras.Add(minpricepara);

            SqlParameter maxpricepara = new SqlParameter("@MaxPrice", SqlDbType.Decimal, 9);
            maxpricepara.Value = pri_pricingapply.MaxPrice;
            paras.Add(maxpricepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = pri_pricingapply.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter pricingblocidpara = new SqlParameter("@PricingBlocId", SqlDbType.Int, 4);
            pricingblocidpara.Value = pri_pricingapply.PricingBlocId;
            paras.Add(pricingblocidpara);

            SqlParameter pricingcorpidpara = new SqlParameter("@PricingCorpId", SqlDbType.Int, 4);
            pricingcorpidpara.Value = pri_pricingapply.PricingCorpId;
            paras.Add(pricingcorpidpara);

            SqlParameter pricingweightpara = new SqlParameter("@PricingWeight", SqlDbType.Decimal, 9);
            pricingweightpara.Value = pri_pricingapply.PricingWeight;
            paras.Add(pricingweightpara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = pri_pricingapply.MUId;
            paras.Add(muidpara);

            SqlParameter assertidpara = new SqlParameter("@AssertId", SqlDbType.Int, 4);
            assertidpara.Value = pri_pricingapply.AssertId;
            paras.Add(assertidpara);

            SqlParameter pricingpersoinidpara = new SqlParameter("@PricingPersoinId", SqlDbType.Int, 4);
            pricingpersoinidpara.Value = pri_pricingapply.PricingPersoinId;
            paras.Add(pricingpersoinidpara);

            SqlParameter pricingstylepara = new SqlParameter("@PricingStyle", SqlDbType.Int, 4);
            pricingstylepara.Value = pri_pricingapply.PricingStyle;
            paras.Add(pricingstylepara);

            SqlParameter declaredatepara = new SqlParameter("@DeclareDate", SqlDbType.DateTime, 8);
            declaredatepara.Value = pri_pricingapply.DeclareDate;
            paras.Add(declaredatepara);

            SqlParameter avgpricestartpara = new SqlParameter("@AvgPriceStart", SqlDbType.DateTime, 8);
            avgpricestartpara.Value = pri_pricingapply.AvgPriceStart;
            paras.Add(avgpricestartpara);

            SqlParameter avgpriceendpara = new SqlParameter("@AvgPriceEnd", SqlDbType.DateTime, 8);
            avgpriceendpara.Value = pri_pricingapply.AvgPriceEnd;
            paras.Add(avgpriceendpara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        
    }
}
