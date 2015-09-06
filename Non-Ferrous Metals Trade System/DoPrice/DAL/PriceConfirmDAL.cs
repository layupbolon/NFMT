/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PriceConfirmDAL.cs
// 文件功能描述：价格确认表dbo.Pri_PriceConfirm数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年3月24日
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
    /// 价格确认表dbo.Pri_PriceConfirm数据交互类。
    /// </summary>
    public partial class PriceConfirmDAL : ExecOperate, IPriceConfirmDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PriceConfirmDAL()
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
            PriceConfirm pri_priceconfirm = (PriceConfirm)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@PriceConfirmId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter outcorpidpara = new SqlParameter("@OutCorpId", SqlDbType.Int, 4);
            outcorpidpara.Value = pri_priceconfirm.OutCorpId;
            paras.Add(outcorpidpara);

            SqlParameter incorpidpara = new SqlParameter("@InCorpId", SqlDbType.Int, 4);
            incorpidpara.Value = pri_priceconfirm.InCorpId;
            paras.Add(incorpidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = pri_priceconfirm.ContractId;
            paras.Add(contractidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = pri_priceconfirm.SubId;
            paras.Add(subidpara);

            SqlParameter contractamountpara = new SqlParameter("@ContractAmount", SqlDbType.Decimal, 9);
            contractamountpara.Value = pri_priceconfirm.ContractAmount;
            paras.Add(contractamountpara);

            SqlParameter subamountpara = new SqlParameter("@SubAmount", SqlDbType.Decimal, 9);
            subamountpara.Value = pri_priceconfirm.SubAmount;
            paras.Add(subamountpara);

            SqlParameter realityamountpara = new SqlParameter("@RealityAmount", SqlDbType.Decimal, 9);
            realityamountpara.Value = pri_priceconfirm.RealityAmount;
            paras.Add(realityamountpara);

            SqlParameter pricingavgpara = new SqlParameter("@PricingAvg", SqlDbType.Decimal, 9);
            pricingavgpara.Value = pri_priceconfirm.PricingAvg;
            paras.Add(pricingavgpara);

            SqlParameter premiumavgpara = new SqlParameter("@PremiumAvg", SqlDbType.Decimal, 9);
            premiumavgpara.Value = pri_priceconfirm.PremiumAvg;
            paras.Add(premiumavgpara);

            SqlParameter interestavgpara = new SqlParameter("@InterestAvg", SqlDbType.Decimal, 9);
            interestavgpara.Value = pri_priceconfirm.InterestAvg;
            paras.Add(interestavgpara);

            SqlParameter otheravgpara = new SqlParameter("@OtherAvg", SqlDbType.Decimal, 9);
            otheravgpara.Value = pri_priceconfirm.OtherAvg;
            paras.Add(otheravgpara);

            SqlParameter interestbalapara = new SqlParameter("@InterestBala", SqlDbType.Decimal, 9);
            interestbalapara.Value = pri_priceconfirm.InterestBala;
            paras.Add(interestbalapara);

            SqlParameter settlepricepara = new SqlParameter("@SettlePrice", SqlDbType.Decimal, 9);
            settlepricepara.Value = pri_priceconfirm.SettlePrice;
            paras.Add(settlepricepara);

            SqlParameter settlebalapara = new SqlParameter("@SettleBala", SqlDbType.Decimal, 9);
            settlebalapara.Value = pri_priceconfirm.SettleBala;
            paras.Add(settlebalapara);

            SqlParameter pricingdatepara = new SqlParameter("@PricingDate", SqlDbType.DateTime, 8);
            pricingdatepara.Value = pri_priceconfirm.PricingDate;
            paras.Add(pricingdatepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = pri_priceconfirm.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter unitidpara = new SqlParameter("@UnitId", SqlDbType.Int, 4);
            unitidpara.Value = pri_priceconfirm.UnitId;
            paras.Add(unitidpara);

            SqlParameter takecorpidpara = new SqlParameter("@TakeCorpId", SqlDbType.Int, 4);
            takecorpidpara.Value = pri_priceconfirm.TakeCorpId;
            paras.Add(takecorpidpara);

            if (!string.IsNullOrEmpty(pri_priceconfirm.ContactPerson))
            {
                SqlParameter contactpersonpara = new SqlParameter("@ContactPerson", SqlDbType.VarChar, 200);
                contactpersonpara.Value = pri_priceconfirm.ContactPerson;
                paras.Add(contactpersonpara);
            }

            if (!string.IsNullOrEmpty(pri_priceconfirm.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = pri_priceconfirm.Memo;
                paras.Add(memopara);
            }

            SqlParameter priceconfirmstatuspara = new SqlParameter("@PriceConfirmStatus", SqlDbType.Int, 4);
            priceconfirmstatuspara.Value = pri_priceconfirm.PriceConfirmStatus;
            paras.Add(priceconfirmstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            PriceConfirm priceconfirm = new PriceConfirm();

            priceconfirm.PriceConfirmId = Convert.ToInt32(dr["PriceConfirmId"]);

            if (dr["OutCorpId"] != DBNull.Value)
            {
                priceconfirm.OutCorpId = Convert.ToInt32(dr["OutCorpId"]);
            }

            if (dr["InCorpId"] != DBNull.Value)
            {
                priceconfirm.InCorpId = Convert.ToInt32(dr["InCorpId"]);
            }

            if (dr["ContractId"] != DBNull.Value)
            {
                priceconfirm.ContractId = Convert.ToInt32(dr["ContractId"]);
            }

            if (dr["SubId"] != DBNull.Value)
            {
                priceconfirm.SubId = Convert.ToInt32(dr["SubId"]);
            }

            if (dr["ContractAmount"] != DBNull.Value)
            {
                priceconfirm.ContractAmount = Convert.ToDecimal(dr["ContractAmount"]);
            }

            if (dr["SubAmount"] != DBNull.Value)
            {
                priceconfirm.SubAmount = Convert.ToDecimal(dr["SubAmount"]);
            }

            if (dr["RealityAmount"] != DBNull.Value)
            {
                priceconfirm.RealityAmount = Convert.ToDecimal(dr["RealityAmount"]);
            }

            if (dr["PricingAvg"] != DBNull.Value)
            {
                priceconfirm.PricingAvg = Convert.ToDecimal(dr["PricingAvg"]);
            }

            if (dr["PremiumAvg"] != DBNull.Value)
            {
                priceconfirm.PremiumAvg = Convert.ToDecimal(dr["PremiumAvg"]);
            }

            if (dr["InterestAvg"] != DBNull.Value)
            {
                priceconfirm.InterestAvg = Convert.ToDecimal(dr["InterestAvg"]);
            }

            if (dr["OtherAvg"] != DBNull.Value)
            {
                priceconfirm.OtherAvg = Convert.ToDecimal(dr["OtherAvg"]);
            }

            if (dr["InterestBala"] != DBNull.Value)
            {
                priceconfirm.InterestBala = Convert.ToDecimal(dr["InterestBala"]);
            }

            if (dr["SettlePrice"] != DBNull.Value)
            {
                priceconfirm.SettlePrice = Convert.ToDecimal(dr["SettlePrice"]);
            }

            if (dr["SettleBala"] != DBNull.Value)
            {
                priceconfirm.SettleBala = Convert.ToDecimal(dr["SettleBala"]);
            }

            if (dr["PricingDate"] != DBNull.Value)
            {
                priceconfirm.PricingDate = Convert.ToDateTime(dr["PricingDate"]);
            }

            if (dr["CurrencyId"] != DBNull.Value)
            {
                priceconfirm.CurrencyId = Convert.ToInt32(dr["CurrencyId"]);
            }

            if (dr["UnitId"] != DBNull.Value)
            {
                priceconfirm.UnitId = Convert.ToInt32(dr["UnitId"]);
            }

            if (dr["TakeCorpId"] != DBNull.Value)
            {
                priceconfirm.TakeCorpId = Convert.ToInt32(dr["TakeCorpId"]);
            }

            if (dr["ContactPerson"] != DBNull.Value)
            {
                priceconfirm.ContactPerson = Convert.ToString(dr["ContactPerson"]);
            }

            if (dr["Memo"] != DBNull.Value)
            {
                priceconfirm.Memo = Convert.ToString(dr["Memo"]);
            }

            if (dr["PriceConfirmStatus"] != DBNull.Value)
            {
                priceconfirm.PriceConfirmStatus = (Common.StatusEnum)Convert.ToInt32(dr["PriceConfirmStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                priceconfirm.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                priceconfirm.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                priceconfirm.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                priceconfirm.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return priceconfirm;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PriceConfirm priceconfirm = new PriceConfirm();

            int indexPriceConfirmId = dr.GetOrdinal("PriceConfirmId");
            priceconfirm.PriceConfirmId = Convert.ToInt32(dr[indexPriceConfirmId]);

            int indexOutCorpId = dr.GetOrdinal("OutCorpId");
            if (dr["OutCorpId"] != DBNull.Value)
            {
                priceconfirm.OutCorpId = Convert.ToInt32(dr[indexOutCorpId]);
            }

            int indexInCorpId = dr.GetOrdinal("InCorpId");
            if (dr["InCorpId"] != DBNull.Value)
            {
                priceconfirm.InCorpId = Convert.ToInt32(dr[indexInCorpId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                priceconfirm.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexSubId = dr.GetOrdinal("SubId");
            if (dr["SubId"] != DBNull.Value)
            {
                priceconfirm.SubId = Convert.ToInt32(dr[indexSubId]);
            }

            int indexContractAmount = dr.GetOrdinal("ContractAmount");
            if (dr["ContractAmount"] != DBNull.Value)
            {
                priceconfirm.ContractAmount = Convert.ToDecimal(dr[indexContractAmount]);
            }

            int indexSubAmount = dr.GetOrdinal("SubAmount");
            if (dr["SubAmount"] != DBNull.Value)
            {
                priceconfirm.SubAmount = Convert.ToDecimal(dr[indexSubAmount]);
            }

            int indexRealityAmount = dr.GetOrdinal("RealityAmount");
            if (dr["RealityAmount"] != DBNull.Value)
            {
                priceconfirm.RealityAmount = Convert.ToDecimal(dr[indexRealityAmount]);
            }

            int indexPricingAvg = dr.GetOrdinal("PricingAvg");
            if (dr["PricingAvg"] != DBNull.Value)
            {
                priceconfirm.PricingAvg = Convert.ToDecimal(dr[indexPricingAvg]);
            }

            int indexPremiumAvg = dr.GetOrdinal("PremiumAvg");
            if (dr["PremiumAvg"] != DBNull.Value)
            {
                priceconfirm.PremiumAvg = Convert.ToDecimal(dr[indexPremiumAvg]);
            }

            int indexInterestAvg = dr.GetOrdinal("InterestAvg");
            if (dr["InterestAvg"] != DBNull.Value)
            {
                priceconfirm.InterestAvg = Convert.ToDecimal(dr[indexInterestAvg]);
            }

            int indexOtherAvg = dr.GetOrdinal("OtherAvg");
            if (dr["OtherAvg"] != DBNull.Value)
            {
                priceconfirm.OtherAvg = Convert.ToDecimal(dr[indexOtherAvg]);
            }

            int indexInterestBala = dr.GetOrdinal("InterestBala");
            if (dr["InterestBala"] != DBNull.Value)
            {
                priceconfirm.InterestBala = Convert.ToDecimal(dr[indexInterestBala]);
            }

            int indexSettlePrice = dr.GetOrdinal("SettlePrice");
            if (dr["SettlePrice"] != DBNull.Value)
            {
                priceconfirm.SettlePrice = Convert.ToDecimal(dr[indexSettlePrice]);
            }

            int indexSettleBala = dr.GetOrdinal("SettleBala");
            if (dr["SettleBala"] != DBNull.Value)
            {
                priceconfirm.SettleBala = Convert.ToDecimal(dr[indexSettleBala]);
            }

            int indexPricingDate = dr.GetOrdinal("PricingDate");
            if (dr["PricingDate"] != DBNull.Value)
            {
                priceconfirm.PricingDate = Convert.ToDateTime(dr[indexPricingDate]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                priceconfirm.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexUnitId = dr.GetOrdinal("UnitId");
            if (dr["UnitId"] != DBNull.Value)
            {
                priceconfirm.UnitId = Convert.ToInt32(dr[indexUnitId]);
            }

            int indexTakeCorpId = dr.GetOrdinal("TakeCorpId");
            if (dr["TakeCorpId"] != DBNull.Value)
            {
                priceconfirm.TakeCorpId = Convert.ToInt32(dr[indexTakeCorpId]);
            }

            int indexContactPerson = dr.GetOrdinal("ContactPerson");
            if (dr["ContactPerson"] != DBNull.Value)
            {
                priceconfirm.ContactPerson = Convert.ToString(dr[indexContactPerson]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                priceconfirm.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexPriceConfirmStatus = dr.GetOrdinal("PriceConfirmStatus");
            if (dr["PriceConfirmStatus"] != DBNull.Value)
            {
                priceconfirm.PriceConfirmStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexPriceConfirmStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                priceconfirm.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                priceconfirm.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                priceconfirm.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                priceconfirm.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return priceconfirm;
        }

        public override string TableName
        {
            get
            {
                return "Pri_PriceConfirm";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PriceConfirm pri_priceconfirm = (PriceConfirm)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter priceconfirmidpara = new SqlParameter("@PriceConfirmId", SqlDbType.Int, 4);
            priceconfirmidpara.Value = pri_priceconfirm.PriceConfirmId;
            paras.Add(priceconfirmidpara);

            SqlParameter outcorpidpara = new SqlParameter("@OutCorpId", SqlDbType.Int, 4);
            outcorpidpara.Value = pri_priceconfirm.OutCorpId;
            paras.Add(outcorpidpara);

            SqlParameter incorpidpara = new SqlParameter("@InCorpId", SqlDbType.Int, 4);
            incorpidpara.Value = pri_priceconfirm.InCorpId;
            paras.Add(incorpidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = pri_priceconfirm.ContractId;
            paras.Add(contractidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = pri_priceconfirm.SubId;
            paras.Add(subidpara);

            SqlParameter contractamountpara = new SqlParameter("@ContractAmount", SqlDbType.Decimal, 9);
            contractamountpara.Value = pri_priceconfirm.ContractAmount;
            paras.Add(contractamountpara);

            SqlParameter subamountpara = new SqlParameter("@SubAmount", SqlDbType.Decimal, 9);
            subamountpara.Value = pri_priceconfirm.SubAmount;
            paras.Add(subamountpara);

            SqlParameter realityamountpara = new SqlParameter("@RealityAmount", SqlDbType.Decimal, 9);
            realityamountpara.Value = pri_priceconfirm.RealityAmount;
            paras.Add(realityamountpara);

            SqlParameter pricingavgpara = new SqlParameter("@PricingAvg", SqlDbType.Decimal, 9);
            pricingavgpara.Value = pri_priceconfirm.PricingAvg;
            paras.Add(pricingavgpara);

            SqlParameter premiumavgpara = new SqlParameter("@PremiumAvg", SqlDbType.Decimal, 9);
            premiumavgpara.Value = pri_priceconfirm.PremiumAvg;
            paras.Add(premiumavgpara);

            SqlParameter interestavgpara = new SqlParameter("@InterestAvg", SqlDbType.Decimal, 9);
            interestavgpara.Value = pri_priceconfirm.InterestAvg;
            paras.Add(interestavgpara);

            SqlParameter otheravgpara = new SqlParameter("@OtherAvg", SqlDbType.Decimal, 9);
            otheravgpara.Value = pri_priceconfirm.OtherAvg;
            paras.Add(otheravgpara);

            SqlParameter interestbalapara = new SqlParameter("@InterestBala", SqlDbType.Decimal, 9);
            interestbalapara.Value = pri_priceconfirm.InterestBala;
            paras.Add(interestbalapara);

            SqlParameter settlepricepara = new SqlParameter("@SettlePrice", SqlDbType.Decimal, 9);
            settlepricepara.Value = pri_priceconfirm.SettlePrice;
            paras.Add(settlepricepara);

            SqlParameter settlebalapara = new SqlParameter("@SettleBala", SqlDbType.Decimal, 9);
            settlebalapara.Value = pri_priceconfirm.SettleBala;
            paras.Add(settlebalapara);

            SqlParameter pricingdatepara = new SqlParameter("@PricingDate", SqlDbType.DateTime, 8);
            pricingdatepara.Value = pri_priceconfirm.PricingDate;
            paras.Add(pricingdatepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = pri_priceconfirm.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter unitidpara = new SqlParameter("@UnitId", SqlDbType.Int, 4);
            unitidpara.Value = pri_priceconfirm.UnitId;
            paras.Add(unitidpara);

            SqlParameter takecorpidpara = new SqlParameter("@TakeCorpId", SqlDbType.Int, 4);
            takecorpidpara.Value = pri_priceconfirm.TakeCorpId;
            paras.Add(takecorpidpara);

            if (!string.IsNullOrEmpty(pri_priceconfirm.ContactPerson))
            {
                SqlParameter contactpersonpara = new SqlParameter("@ContactPerson", SqlDbType.VarChar, 200);
                contactpersonpara.Value = pri_priceconfirm.ContactPerson;
                paras.Add(contactpersonpara);
            }

            if (!string.IsNullOrEmpty(pri_priceconfirm.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = pri_priceconfirm.Memo;
                paras.Add(memopara);
            }

            SqlParameter priceconfirmstatuspara = new SqlParameter("@PriceConfirmStatus", SqlDbType.Int, 4);
            priceconfirmstatuspara.Value = pri_priceconfirm.PriceConfirmStatus;
            paras.Add(priceconfirmstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
