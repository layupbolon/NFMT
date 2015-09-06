/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractPriceDAL.cs
// 文件功能描述：合约价格明细dbo.Con_ContractPrice数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年2月4日
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
    /// 合约价格明细dbo.Con_ContractPrice数据交互类。
    /// </summary>
    public partial class ContractPriceDAL : DataOperate, IContractPriceDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractPriceDAL()
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
            ContractPrice con_contractprice = (ContractPrice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ContractPriceId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contractprice.ContractId;
            paras.Add(contractidpara);

            SqlParameter fixedpricepara = new SqlParameter("@FixedPrice", SqlDbType.Decimal, 9);
            fixedpricepara.Value = con_contractprice.FixedPrice;
            paras.Add(fixedpricepara);

            if (!string.IsNullOrEmpty(con_contractprice.FixedPriceMemo))
            {
                SqlParameter fixedpricememopara = new SqlParameter("@FixedPriceMemo", SqlDbType.VarChar, 4000);
                fixedpricememopara.Value = con_contractprice.FixedPriceMemo;
                paras.Add(fixedpricememopara);
            }

            SqlParameter whodopricepara = new SqlParameter("@WhoDoPrice", SqlDbType.Int, 4);
            whodopricepara.Value = con_contractprice.WhoDoPrice;
            paras.Add(whodopricepara);

            SqlParameter almostpricepara = new SqlParameter("@AlmostPrice", SqlDbType.Decimal, 9);
            almostpricepara.Value = con_contractprice.AlmostPrice;
            paras.Add(almostpricepara);

            SqlParameter dopricebegindatepara = new SqlParameter("@DoPriceBeginDate", SqlDbType.DateTime, 8);
            dopricebegindatepara.Value = con_contractprice.DoPriceBeginDate;
            paras.Add(dopricebegindatepara);

            SqlParameter dopriceenddatepara = new SqlParameter("@DoPriceEndDate", SqlDbType.DateTime, 8);
            dopriceenddatepara.Value = con_contractprice.DoPriceEndDate;
            paras.Add(dopriceenddatepara);

            SqlParameter isqppara = new SqlParameter("@IsQP", SqlDbType.Bit, 1);
            isqppara.Value = con_contractprice.IsQP;
            paras.Add(isqppara);

            SqlParameter pricefrompara = new SqlParameter("@PriceFrom", SqlDbType.Int, 4);
            pricefrompara.Value = con_contractprice.PriceFrom;
            paras.Add(pricefrompara);

            SqlParameter pricestyle1para = new SqlParameter("@PriceStyle1", SqlDbType.Int, 4);
            pricestyle1para.Value = con_contractprice.PriceStyle1;
            paras.Add(pricestyle1para);

            SqlParameter pricestyle2para = new SqlParameter("@PriceStyle2", SqlDbType.Int, 4);
            pricestyle2para.Value = con_contractprice.PriceStyle2;
            paras.Add(pricestyle2para);

            SqlParameter marginmodepara = new SqlParameter("@MarginMode", SqlDbType.Int, 4);
            marginmodepara.Value = con_contractprice.MarginMode;
            paras.Add(marginmodepara);

            SqlParameter marginamountpara = new SqlParameter("@MarginAmount", SqlDbType.Decimal, 9);
            marginamountpara.Value = con_contractprice.MarginAmount;
            paras.Add(marginamountpara);

            if (!string.IsNullOrEmpty(con_contractprice.MarginMemo))
            {
                SqlParameter marginmemopara = new SqlParameter("@MarginMemo", SqlDbType.VarChar, 4000);
                marginmemopara.Value = con_contractprice.MarginMemo;
                paras.Add(marginmemopara);
            }

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractPrice contractprice = new ContractPrice();

            int indexContractPriceId = dr.GetOrdinal("ContractPriceId");
            contractprice.ContractPriceId = Convert.ToInt32(dr[indexContractPriceId]);

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                contractprice.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexFixedPrice = dr.GetOrdinal("FixedPrice");
            if (dr["FixedPrice"] != DBNull.Value)
            {
                contractprice.FixedPrice = Convert.ToDecimal(dr[indexFixedPrice]);
            }

            int indexFixedPriceMemo = dr.GetOrdinal("FixedPriceMemo");
            if (dr["FixedPriceMemo"] != DBNull.Value)
            {
                contractprice.FixedPriceMemo = Convert.ToString(dr[indexFixedPriceMemo]);
            }

            int indexWhoDoPrice = dr.GetOrdinal("WhoDoPrice");
            if (dr["WhoDoPrice"] != DBNull.Value)
            {
                contractprice.WhoDoPrice = Convert.ToInt32(dr[indexWhoDoPrice]);
            }

            int indexAlmostPrice = dr.GetOrdinal("AlmostPrice");
            if (dr["AlmostPrice"] != DBNull.Value)
            {
                contractprice.AlmostPrice = Convert.ToDecimal(dr[indexAlmostPrice]);
            }

            int indexDoPriceBeginDate = dr.GetOrdinal("DoPriceBeginDate");
            if (dr["DoPriceBeginDate"] != DBNull.Value)
            {
                contractprice.DoPriceBeginDate = Convert.ToDateTime(dr[indexDoPriceBeginDate]);
            }

            int indexDoPriceEndDate = dr.GetOrdinal("DoPriceEndDate");
            if (dr["DoPriceEndDate"] != DBNull.Value)
            {
                contractprice.DoPriceEndDate = Convert.ToDateTime(dr[indexDoPriceEndDate]);
            }

            int indexIsQP = dr.GetOrdinal("IsQP");
            if (dr["IsQP"] != DBNull.Value)
            {
                contractprice.IsQP = Convert.ToBoolean(dr[indexIsQP]);
            }

            int indexPriceFrom = dr.GetOrdinal("PriceFrom");
            if (dr["PriceFrom"] != DBNull.Value)
            {
                contractprice.PriceFrom = Convert.ToInt32(dr[indexPriceFrom]);
            }

            int indexPriceStyle1 = dr.GetOrdinal("PriceStyle1");
            if (dr["PriceStyle1"] != DBNull.Value)
            {
                contractprice.PriceStyle1 = Convert.ToInt32(dr[indexPriceStyle1]);
            }

            int indexPriceStyle2 = dr.GetOrdinal("PriceStyle2");
            if (dr["PriceStyle2"] != DBNull.Value)
            {
                contractprice.PriceStyle2 = Convert.ToInt32(dr[indexPriceStyle2]);
            }

            int indexMarginMode = dr.GetOrdinal("MarginMode");
            if (dr["MarginMode"] != DBNull.Value)
            {
                contractprice.MarginMode = Convert.ToInt32(dr[indexMarginMode]);
            }

            int indexMarginAmount = dr.GetOrdinal("MarginAmount");
            if (dr["MarginAmount"] != DBNull.Value)
            {
                contractprice.MarginAmount = Convert.ToDecimal(dr[indexMarginAmount]);
            }

            int indexMarginMemo = dr.GetOrdinal("MarginMemo");
            if (dr["MarginMemo"] != DBNull.Value)
            {
                contractprice.MarginMemo = Convert.ToString(dr[indexMarginMemo]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                contractprice.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                contractprice.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                contractprice.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                contractprice.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return contractprice;
        }

        public override string TableName
        {
            get
            {
                return "Con_ContractPrice";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractPrice con_contractprice = (ContractPrice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter contractpriceidpara = new SqlParameter("@ContractPriceId", SqlDbType.Int, 4);
            contractpriceidpara.Value = con_contractprice.ContractPriceId;
            paras.Add(contractpriceidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contractprice.ContractId;
            paras.Add(contractidpara);

            SqlParameter fixedpricepara = new SqlParameter("@FixedPrice", SqlDbType.Decimal, 9);
            fixedpricepara.Value = con_contractprice.FixedPrice;
            paras.Add(fixedpricepara);

            if (!string.IsNullOrEmpty(con_contractprice.FixedPriceMemo))
            {
                SqlParameter fixedpricememopara = new SqlParameter("@FixedPriceMemo", SqlDbType.VarChar, 4000);
                fixedpricememopara.Value = con_contractprice.FixedPriceMemo;
                paras.Add(fixedpricememopara);
            }

            SqlParameter whodopricepara = new SqlParameter("@WhoDoPrice", SqlDbType.Int, 4);
            whodopricepara.Value = con_contractprice.WhoDoPrice;
            paras.Add(whodopricepara);

            SqlParameter almostpricepara = new SqlParameter("@AlmostPrice", SqlDbType.Decimal, 9);
            almostpricepara.Value = con_contractprice.AlmostPrice;
            paras.Add(almostpricepara);

            SqlParameter dopricebegindatepara = new SqlParameter("@DoPriceBeginDate", SqlDbType.DateTime, 8);
            dopricebegindatepara.Value = con_contractprice.DoPriceBeginDate;
            paras.Add(dopricebegindatepara);

            SqlParameter dopriceenddatepara = new SqlParameter("@DoPriceEndDate", SqlDbType.DateTime, 8);
            dopriceenddatepara.Value = con_contractprice.DoPriceEndDate;
            paras.Add(dopriceenddatepara);

            SqlParameter isqppara = new SqlParameter("@IsQP", SqlDbType.Bit, 1);
            isqppara.Value = con_contractprice.IsQP;
            paras.Add(isqppara);

            SqlParameter pricefrompara = new SqlParameter("@PriceFrom", SqlDbType.Int, 4);
            pricefrompara.Value = con_contractprice.PriceFrom;
            paras.Add(pricefrompara);

            SqlParameter pricestyle1para = new SqlParameter("@PriceStyle1", SqlDbType.Int, 4);
            pricestyle1para.Value = con_contractprice.PriceStyle1;
            paras.Add(pricestyle1para);

            SqlParameter pricestyle2para = new SqlParameter("@PriceStyle2", SqlDbType.Int, 4);
            pricestyle2para.Value = con_contractprice.PriceStyle2;
            paras.Add(pricestyle2para);

            SqlParameter marginmodepara = new SqlParameter("@MarginMode", SqlDbType.Int, 4);
            marginmodepara.Value = con_contractprice.MarginMode;
            paras.Add(marginmodepara);

            SqlParameter marginamountpara = new SqlParameter("@MarginAmount", SqlDbType.Decimal, 9);
            marginamountpara.Value = con_contractprice.MarginAmount;
            paras.Add(marginamountpara);

            if (!string.IsNullOrEmpty(con_contractprice.MarginMemo))
            {
                SqlParameter marginmemopara = new SqlParameter("@MarginMemo", SqlDbType.VarChar, 4000);
                marginmemopara.Value = con_contractprice.MarginMemo;
                paras.Add(marginmemopara);
            }

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
