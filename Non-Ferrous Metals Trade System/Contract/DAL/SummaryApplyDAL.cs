/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SummaryApplyDAL.cs
// 文件功能描述：制单指令dbo.Con_SummaryApply数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Contract.Model;
using NFMT.DBUtility;
using NFMT.Contract.IDAL;
using NFMT.Common;

namespace NFMT.Contract.DAL
{
    /// <summary>
    /// 制单指令dbo.Con_SummaryApply数据交互类。
    /// </summary>
    public class SummaryApplyDAL : DataOperate, ISummaryApplyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SummaryApplyDAL()
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
            SummaryApply con_summaryapply = (SummaryApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@SummaryApplyId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_summaryapply.ContractId;
            paras.Add(contractidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = con_summaryapply.SubId;
            paras.Add(subidpara);

            SqlParameter satypepara = new SqlParameter("@SAType", SqlDbType.Int, 4);
            satypepara.Value = con_summaryapply.SAType;
            paras.Add(satypepara);

            SqlParameter sadatepara = new SqlParameter("@SADate", SqlDbType.DateTime, 8);
            sadatepara.Value = con_summaryapply.SADate;
            paras.Add(sadatepara);

            SqlParameter applydeptpara = new SqlParameter("@ApplyDept", SqlDbType.Int, 4);
            applydeptpara.Value = con_summaryapply.ApplyDept;
            paras.Add(applydeptpara);

            SqlParameter buyercorppara = new SqlParameter("@BuyerCorp", SqlDbType.Int, 4);
            buyercorppara.Value = con_summaryapply.BuyerCorp;
            paras.Add(buyercorppara);

            if (!string.IsNullOrEmpty(con_summaryapply.BuyerAddress))
            {
                SqlParameter buyeraddresspara = new SqlParameter("@BuyerAddress", SqlDbType.VarChar, 800);
                buyeraddresspara.Value = con_summaryapply.BuyerAddress;
                paras.Add(buyeraddresspara);
            }

            SqlParameter paymentstylepara = new SqlParameter("@PaymentStyle", SqlDbType.Int, 4);
            paymentstylepara.Value = con_summaryapply.PaymentStyle;
            paras.Add(paymentstylepara);

            SqlParameter recbankidpara = new SqlParameter("@RecBankId", SqlDbType.Int, 4);
            recbankidpara.Value = con_summaryapply.RecBankId;
            paras.Add(recbankidpara);

            SqlParameter outpricoptionpara = new SqlParameter("@OutPricOption", SqlDbType.Int, 4);
            outpricoptionpara.Value = con_summaryapply.OutPricOption;
            paras.Add(outpricoptionpara);

            SqlParameter brandidpara = new SqlParameter("@BrandId", SqlDbType.Int, 4);
            brandidpara.Value = con_summaryapply.BrandId;
            paras.Add(brandidpara);

            SqlParameter areaidpara = new SqlParameter("@AreaId", SqlDbType.Int, 4);
            areaidpara.Value = con_summaryapply.AreaId;
            paras.Add(areaidpara);

            if (!string.IsNullOrEmpty(con_summaryapply.BankCode))
            {
                SqlParameter bankcodepara = new SqlParameter("@BankCode", SqlDbType.VarChar, 400);
                bankcodepara.Value = con_summaryapply.BankCode;
                paras.Add(bankcodepara);
            }

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = con_summaryapply.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = con_summaryapply.NetAmount;
            paras.Add(netamountpara);

            SqlParameter curencypara = new SqlParameter("@Curency", SqlDbType.Int, 4);
            curencypara.Value = con_summaryapply.Curency;
            paras.Add(curencypara);

            SqlParameter pricepara = new SqlParameter("@Price", SqlDbType.Decimal, 9);
            pricepara.Value = con_summaryapply.Price;
            paras.Add(pricepara);

            SqlParameter balapara = new SqlParameter("@Bala", SqlDbType.Decimal, 9);
            balapara.Value = con_summaryapply.Bala;
            paras.Add(balapara);

            if (!string.IsNullOrEmpty(con_summaryapply.Meno))
            {
                SqlParameter menopara = new SqlParameter("@Meno", SqlDbType.VarChar, 4000);
                menopara.Value = con_summaryapply.Meno;
                paras.Add(menopara);
            }

            SqlParameter sastatuspara = new SqlParameter("@SAStatus", SqlDbType.Int, 4);
            sastatuspara.Value = con_summaryapply.SAStatus;
            paras.Add(sastatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            SummaryApply summaryapply = new SummaryApply();

            int indexSummaryApplyId = dr.GetOrdinal("SummaryApplyId");
            summaryapply.SummaryApplyId = Convert.ToInt32(dr[indexSummaryApplyId]);

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                summaryapply.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexSubId = dr.GetOrdinal("SubId");
            if (dr["SubId"] != DBNull.Value)
            {
                summaryapply.SubId = Convert.ToInt32(dr[indexSubId]);
            }

            int indexSAType = dr.GetOrdinal("SAType");
            if (dr["SAType"] != DBNull.Value)
            {
                summaryapply.SAType = Convert.ToInt32(dr[indexSAType]);
            }

            int indexSADate = dr.GetOrdinal("SADate");
            if (dr["SADate"] != DBNull.Value)
            {
                summaryapply.SADate = Convert.ToDateTime(dr[indexSADate]);
            }

            int indexApplyDept = dr.GetOrdinal("ApplyDept");
            if (dr["ApplyDept"] != DBNull.Value)
            {
                summaryapply.ApplyDept = Convert.ToInt32(dr[indexApplyDept]);
            }

            int indexBuyerCorp = dr.GetOrdinal("BuyerCorp");
            if (dr["BuyerCorp"] != DBNull.Value)
            {
                summaryapply.BuyerCorp = Convert.ToInt32(dr[indexBuyerCorp]);
            }

            int indexBuyerAddress = dr.GetOrdinal("BuyerAddress");
            if (dr["BuyerAddress"] != DBNull.Value)
            {
                summaryapply.BuyerAddress = Convert.ToString(dr[indexBuyerAddress]);
            }

            int indexPaymentStyle = dr.GetOrdinal("PaymentStyle");
            if (dr["PaymentStyle"] != DBNull.Value)
            {
                summaryapply.PaymentStyle = Convert.ToInt32(dr[indexPaymentStyle]);
            }

            int indexRecBankId = dr.GetOrdinal("RecBankId");
            if (dr["RecBankId"] != DBNull.Value)
            {
                summaryapply.RecBankId = Convert.ToInt32(dr[indexRecBankId]);
            }

            int indexOutPricOption = dr.GetOrdinal("OutPricOption");
            if (dr["OutPricOption"] != DBNull.Value)
            {
                summaryapply.OutPricOption = Convert.ToInt32(dr[indexOutPricOption]);
            }

            int indexBrandId = dr.GetOrdinal("BrandId");
            if (dr["BrandId"] != DBNull.Value)
            {
                summaryapply.BrandId = Convert.ToInt32(dr[indexBrandId]);
            }

            int indexAreaId = dr.GetOrdinal("AreaId");
            if (dr["AreaId"] != DBNull.Value)
            {
                summaryapply.AreaId = Convert.ToInt32(dr[indexAreaId]);
            }

            int indexBankCode = dr.GetOrdinal("BankCode");
            if (dr["BankCode"] != DBNull.Value)
            {
                summaryapply.BankCode = Convert.ToString(dr[indexBankCode]);
            }

            int indexGrossAmount = dr.GetOrdinal("GrossAmount");
            if (dr["GrossAmount"] != DBNull.Value)
            {
                summaryapply.GrossAmount = Convert.ToDecimal(dr[indexGrossAmount]);
            }

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            if (dr["NetAmount"] != DBNull.Value)
            {
                summaryapply.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
            }

            int indexCurency = dr.GetOrdinal("Curency");
            if (dr["Curency"] != DBNull.Value)
            {
                summaryapply.Curency = Convert.ToInt32(dr[indexCurency]);
            }

            int indexPrice = dr.GetOrdinal("Price");
            if (dr["Price"] != DBNull.Value)
            {
                summaryapply.Price = Convert.ToDecimal(dr[indexPrice]);
            }

            int indexBala = dr.GetOrdinal("Bala");
            if (dr["Bala"] != DBNull.Value)
            {
                summaryapply.Bala = Convert.ToDecimal(dr[indexBala]);
            }

            int indexMeno = dr.GetOrdinal("Meno");
            if (dr["Meno"] != DBNull.Value)
            {
                summaryapply.Meno = Convert.ToString(dr[indexMeno]);
            }

            int indexSAStatus = dr.GetOrdinal("SAStatus");
            if (dr["SAStatus"] != DBNull.Value)
            {
                summaryapply.SAStatus = Convert.ToInt32(dr[indexSAStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                summaryapply.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                summaryapply.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                summaryapply.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                summaryapply.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return summaryapply;
        }

        public override string TableName
        {
            get
            {
                return "Con_SummaryApply";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SummaryApply con_summaryapply = (SummaryApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter summaryapplyidpara = new SqlParameter("@SummaryApplyId", SqlDbType.Int, 4);
            summaryapplyidpara.Value = con_summaryapply.SummaryApplyId;
            paras.Add(summaryapplyidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_summaryapply.ContractId;
            paras.Add(contractidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = con_summaryapply.SubId;
            paras.Add(subidpara);

            SqlParameter satypepara = new SqlParameter("@SAType", SqlDbType.Int, 4);
            satypepara.Value = con_summaryapply.SAType;
            paras.Add(satypepara);

            SqlParameter sadatepara = new SqlParameter("@SADate", SqlDbType.DateTime, 8);
            sadatepara.Value = con_summaryapply.SADate;
            paras.Add(sadatepara);

            SqlParameter applydeptpara = new SqlParameter("@ApplyDept", SqlDbType.Int, 4);
            applydeptpara.Value = con_summaryapply.ApplyDept;
            paras.Add(applydeptpara);

            SqlParameter buyercorppara = new SqlParameter("@BuyerCorp", SqlDbType.Int, 4);
            buyercorppara.Value = con_summaryapply.BuyerCorp;
            paras.Add(buyercorppara);

            if (!string.IsNullOrEmpty(con_summaryapply.BuyerAddress))
            {
                SqlParameter buyeraddresspara = new SqlParameter("@BuyerAddress", SqlDbType.VarChar, 800);
                buyeraddresspara.Value = con_summaryapply.BuyerAddress;
                paras.Add(buyeraddresspara);
            }

            SqlParameter paymentstylepara = new SqlParameter("@PaymentStyle", SqlDbType.Int, 4);
            paymentstylepara.Value = con_summaryapply.PaymentStyle;
            paras.Add(paymentstylepara);

            SqlParameter recbankidpara = new SqlParameter("@RecBankId", SqlDbType.Int, 4);
            recbankidpara.Value = con_summaryapply.RecBankId;
            paras.Add(recbankidpara);

            SqlParameter outpricoptionpara = new SqlParameter("@OutPricOption", SqlDbType.Int, 4);
            outpricoptionpara.Value = con_summaryapply.OutPricOption;
            paras.Add(outpricoptionpara);

            SqlParameter brandidpara = new SqlParameter("@BrandId", SqlDbType.Int, 4);
            brandidpara.Value = con_summaryapply.BrandId;
            paras.Add(brandidpara);

            SqlParameter areaidpara = new SqlParameter("@AreaId", SqlDbType.Int, 4);
            areaidpara.Value = con_summaryapply.AreaId;
            paras.Add(areaidpara);

            if (!string.IsNullOrEmpty(con_summaryapply.BankCode))
            {
                SqlParameter bankcodepara = new SqlParameter("@BankCode", SqlDbType.VarChar, 400);
                bankcodepara.Value = con_summaryapply.BankCode;
                paras.Add(bankcodepara);
            }

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = con_summaryapply.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = con_summaryapply.NetAmount;
            paras.Add(netamountpara);

            SqlParameter curencypara = new SqlParameter("@Curency", SqlDbType.Int, 4);
            curencypara.Value = con_summaryapply.Curency;
            paras.Add(curencypara);

            SqlParameter pricepara = new SqlParameter("@Price", SqlDbType.Decimal, 9);
            pricepara.Value = con_summaryapply.Price;
            paras.Add(pricepara);

            SqlParameter balapara = new SqlParameter("@Bala", SqlDbType.Decimal, 9);
            balapara.Value = con_summaryapply.Bala;
            paras.Add(balapara);

            if (!string.IsNullOrEmpty(con_summaryapply.Meno))
            {
                SqlParameter menopara = new SqlParameter("@Meno", SqlDbType.VarChar, 4000);
                menopara.Value = con_summaryapply.Meno;
                paras.Add(menopara);
            }

            SqlParameter sastatuspara = new SqlParameter("@SAStatus", SqlDbType.Int, 4);
            sastatuspara.Value = con_summaryapply.SAStatus;
            paras.Add(sastatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
