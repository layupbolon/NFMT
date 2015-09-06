/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InterestDAL.cs
// 文件功能描述：利息表dbo.Pri_Interest数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年3月17日
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
    /// 利息表dbo.Pri_Interest数据交互类。
    /// </summary>
    public partial class InterestDAL : ExecOperate, IInterestDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InterestDAL()
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
            Interest pri_interest = (Interest)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@InterestId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = pri_interest.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = pri_interest.ContractId;
            paras.Add(contractidpara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = pri_interest.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter pricingunitpara = new SqlParameter("@PricingUnit", SqlDbType.Decimal, 9);
            pricingunitpara.Value = pri_interest.PricingUnit;
            paras.Add(pricingunitpara);

            SqlParameter premiumpara = new SqlParameter("@Premium", SqlDbType.Decimal, 9);
            premiumpara.Value = pri_interest.Premium;
            paras.Add(premiumpara);

            SqlParameter otherpricepara = new SqlParameter("@OtherPrice", SqlDbType.Decimal, 9);
            otherpricepara.Value = pri_interest.OtherPrice;
            paras.Add(otherpricepara);

            SqlParameter interestpricepara = new SqlParameter("@InterestPrice", SqlDbType.Decimal, 9);
            interestpricepara.Value = pri_interest.InterestPrice;
            paras.Add(interestpricepara);

            SqlParameter paycapitalpara = new SqlParameter("@PayCapital", SqlDbType.Decimal, 9);
            paycapitalpara.Value = pri_interest.PayCapital;
            paras.Add(paycapitalpara);

            SqlParameter curcapitalpara = new SqlParameter("@CurCapital", SqlDbType.Decimal, 9);
            curcapitalpara.Value = pri_interest.CurCapital;
            paras.Add(curcapitalpara);

            SqlParameter interestratepara = new SqlParameter("@InterestRate", SqlDbType.Decimal, 9);
            interestratepara.Value = pri_interest.InterestRate;
            paras.Add(interestratepara);

            SqlParameter interestbalapara = new SqlParameter("@InterestBala", SqlDbType.Decimal, 9);
            interestbalapara.Value = pri_interest.InterestBala;
            paras.Add(interestbalapara);

            SqlParameter interestamountdaypara = new SqlParameter("@InterestAmountDay", SqlDbType.Decimal, 9);
            interestamountdaypara.Value = pri_interest.InterestAmountDay;
            paras.Add(interestamountdaypara);

            SqlParameter interestamountpara = new SqlParameter("@InterestAmount", SqlDbType.Decimal, 9);
            interestamountpara.Value = pri_interest.InterestAmount;
            paras.Add(interestamountpara);

            SqlParameter unitpara = new SqlParameter("@Unit", SqlDbType.Int, 4);
            unitpara.Value = pri_interest.Unit;
            paras.Add(unitpara);

            SqlParameter intereststylepara = new SqlParameter("@InterestStyle", SqlDbType.Int, 4);
            intereststylepara.Value = pri_interest.InterestStyle;
            paras.Add(intereststylepara);

            if (!string.IsNullOrEmpty(pri_interest.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = pri_interest.Memo;
                paras.Add(memopara);
            }

            SqlParameter interestdatepara = new SqlParameter("@InterestDate", SqlDbType.DateTime, 8);
            interestdatepara.Value = pri_interest.InterestDate;
            paras.Add(interestdatepara);

            SqlParameter intereststatuspara = new SqlParameter("@InterestStatus", SqlDbType.Int, 4);
            intereststatuspara.Value = pri_interest.InterestStatus;
            paras.Add(intereststatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Interest interest = new Interest();

            int indexInterestId = dr.GetOrdinal("InterestId");
            interest.InterestId = Convert.ToInt32(dr[indexInterestId]);

            int indexSubContractId = dr.GetOrdinal("SubContractId");
            if (dr["SubContractId"] != DBNull.Value)
            {
                interest.SubContractId = Convert.ToInt32(dr[indexSubContractId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                interest.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                interest.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexPricingUnit = dr.GetOrdinal("PricingUnit");
            if (dr["PricingUnit"] != DBNull.Value)
            {
                interest.PricingUnit = Convert.ToDecimal(dr[indexPricingUnit]);
            }

            int indexPremium = dr.GetOrdinal("Premium");
            if (dr["Premium"] != DBNull.Value)
            {
                interest.Premium = Convert.ToDecimal(dr[indexPremium]);
            }

            int indexOtherPrice = dr.GetOrdinal("OtherPrice");
            if (dr["OtherPrice"] != DBNull.Value)
            {
                interest.OtherPrice = Convert.ToDecimal(dr[indexOtherPrice]);
            }

            int indexInterestPrice = dr.GetOrdinal("InterestPrice");
            if (dr["InterestPrice"] != DBNull.Value)
            {
                interest.InterestPrice = Convert.ToDecimal(dr[indexInterestPrice]);
            }

            int indexPayCapital = dr.GetOrdinal("PayCapital");
            if (dr["PayCapital"] != DBNull.Value)
            {
                interest.PayCapital = Convert.ToDecimal(dr[indexPayCapital]);
            }

            int indexCurCapital = dr.GetOrdinal("CurCapital");
            if (dr["CurCapital"] != DBNull.Value)
            {
                interest.CurCapital = Convert.ToDecimal(dr[indexCurCapital]);
            }

            int indexInterestRate = dr.GetOrdinal("InterestRate");
            if (dr["InterestRate"] != DBNull.Value)
            {
                interest.InterestRate = Convert.ToDecimal(dr[indexInterestRate]);
            }

            int indexInterestBala = dr.GetOrdinal("InterestBala");
            if (dr["InterestBala"] != DBNull.Value)
            {
                interest.InterestBala = Convert.ToDecimal(dr[indexInterestBala]);
            }

            int indexInterestAmountDay = dr.GetOrdinal("InterestAmountDay");
            if (dr["InterestAmountDay"] != DBNull.Value)
            {
                interest.InterestAmountDay = Convert.ToDecimal(dr[indexInterestAmountDay]);
            }

            int indexInterestAmount = dr.GetOrdinal("InterestAmount");
            if (dr["InterestAmount"] != DBNull.Value)
            {
                interest.InterestAmount = Convert.ToDecimal(dr[indexInterestAmount]);
            }

            int indexUnit = dr.GetOrdinal("Unit");
            if (dr["Unit"] != DBNull.Value)
            {
                interest.Unit = Convert.ToInt32(dr[indexUnit]);
            }

            int indexInterestStyle = dr.GetOrdinal("InterestStyle");
            if (dr["InterestStyle"] != DBNull.Value)
            {
                interest.InterestStyle = Convert.ToInt32(dr[indexInterestStyle]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                interest.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexInterestDate = dr.GetOrdinal("InterestDate");
            if (dr["InterestDate"] != DBNull.Value)
            {
                interest.InterestDate = Convert.ToDateTime(dr[indexInterestDate]);
            }

            int indexInterestStatus = dr.GetOrdinal("InterestStatus");
            if (dr["InterestStatus"] != DBNull.Value)
            {
                interest.InterestStatus = (StatusEnum)Convert.ToInt32(dr[indexInterestStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                interest.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                interest.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                interest.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                interest.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return interest;
        }

        public override string TableName
        {
            get
            {
                return "Pri_Interest";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Interest pri_interest = (Interest)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter interestidpara = new SqlParameter("@InterestId", SqlDbType.Int, 4);
            interestidpara.Value = pri_interest.InterestId;
            paras.Add(interestidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = pri_interest.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = pri_interest.ContractId;
            paras.Add(contractidpara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = pri_interest.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter pricingunitpara = new SqlParameter("@PricingUnit", SqlDbType.Decimal, 9);
            pricingunitpara.Value = pri_interest.PricingUnit;
            paras.Add(pricingunitpara);

            SqlParameter premiumpara = new SqlParameter("@Premium", SqlDbType.Decimal, 9);
            premiumpara.Value = pri_interest.Premium;
            paras.Add(premiumpara);

            SqlParameter otherpricepara = new SqlParameter("@OtherPrice", SqlDbType.Decimal, 9);
            otherpricepara.Value = pri_interest.OtherPrice;
            paras.Add(otherpricepara);

            SqlParameter interestpricepara = new SqlParameter("@InterestPrice", SqlDbType.Decimal, 9);
            interestpricepara.Value = pri_interest.InterestPrice;
            paras.Add(interestpricepara);

            SqlParameter paycapitalpara = new SqlParameter("@PayCapital", SqlDbType.Decimal, 9);
            paycapitalpara.Value = pri_interest.PayCapital;
            paras.Add(paycapitalpara);

            SqlParameter curcapitalpara = new SqlParameter("@CurCapital", SqlDbType.Decimal, 9);
            curcapitalpara.Value = pri_interest.CurCapital;
            paras.Add(curcapitalpara);

            SqlParameter interestratepara = new SqlParameter("@InterestRate", SqlDbType.Decimal, 9);
            interestratepara.Value = pri_interest.InterestRate;
            paras.Add(interestratepara);

            SqlParameter interestbalapara = new SqlParameter("@InterestBala", SqlDbType.Decimal, 9);
            interestbalapara.Value = pri_interest.InterestBala;
            paras.Add(interestbalapara);

            SqlParameter interestamountdaypara = new SqlParameter("@InterestAmountDay", SqlDbType.Decimal, 9);
            interestamountdaypara.Value = pri_interest.InterestAmountDay;
            paras.Add(interestamountdaypara);

            SqlParameter interestamountpara = new SqlParameter("@InterestAmount", SqlDbType.Decimal, 9);
            interestamountpara.Value = pri_interest.InterestAmount;
            paras.Add(interestamountpara);

            SqlParameter unitpara = new SqlParameter("@Unit", SqlDbType.Int, 4);
            unitpara.Value = pri_interest.Unit;
            paras.Add(unitpara);

            SqlParameter intereststylepara = new SqlParameter("@InterestStyle", SqlDbType.Int, 4);
            intereststylepara.Value = pri_interest.InterestStyle;
            paras.Add(intereststylepara);

            if (!string.IsNullOrEmpty(pri_interest.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = pri_interest.Memo;
                paras.Add(memopara);
            }

            SqlParameter interestdatepara = new SqlParameter("@InterestDate", SqlDbType.DateTime, 8);
            interestdatepara.Value = pri_interest.InterestDate;
            paras.Add(interestdatepara);

            SqlParameter intereststatuspara = new SqlParameter("@InterestStatus", SqlDbType.Int, 4);
            intereststatuspara.Value = pri_interest.InterestStatus;
            paras.Add(intereststatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
