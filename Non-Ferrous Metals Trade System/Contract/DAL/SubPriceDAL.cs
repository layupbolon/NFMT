/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SubPriceDAL.cs
// 文件功能描述：子合约价格明细dbo.Con_SubPrice数据交互类。
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
    /// 子合约价格明细dbo.Con_SubPrice数据交互类。
    /// </summary>
    public partial class SubPriceDAL : DataOperate, ISubPriceDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SubPriceDAL()
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
            SubPrice con_subprice = (SubPrice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@SubPriceId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = con_subprice.SubId;
            paras.Add(subidpara);

            SqlParameter fixedpricepara = new SqlParameter("@FixedPrice", SqlDbType.Decimal, 9);
            fixedpricepara.Value = con_subprice.FixedPrice;
            paras.Add(fixedpricepara);

            if (!string.IsNullOrEmpty(con_subprice.FixedPriceMemo))
            {
                SqlParameter fixedpricememopara = new SqlParameter("@FixedPriceMemo", SqlDbType.VarChar, 4000);
                fixedpricememopara.Value = con_subprice.FixedPriceMemo;
                paras.Add(fixedpricememopara);
            }

            SqlParameter whodopricepara = new SqlParameter("@WhoDoPrice", SqlDbType.Int, 4);
            whodopricepara.Value = con_subprice.WhoDoPrice;
            paras.Add(whodopricepara);

            SqlParameter almostpricepara = new SqlParameter("@AlmostPrice", SqlDbType.Decimal, 9);
            almostpricepara.Value = con_subprice.AlmostPrice;
            paras.Add(almostpricepara);

            SqlParameter dopricebegindatepara = new SqlParameter("@DoPriceBeginDate", SqlDbType.DateTime, 8);
            dopricebegindatepara.Value = con_subprice.DoPriceBeginDate;
            paras.Add(dopricebegindatepara);

            SqlParameter dopriceenddatepara = new SqlParameter("@DoPriceEndDate", SqlDbType.DateTime, 8);
            dopriceenddatepara.Value = con_subprice.DoPriceEndDate;
            paras.Add(dopriceenddatepara);

            SqlParameter isqppara = new SqlParameter("@IsQP", SqlDbType.Bit, 1);
            isqppara.Value = con_subprice.IsQP;
            paras.Add(isqppara);

            SqlParameter pricefrompara = new SqlParameter("@PriceFrom", SqlDbType.Int, 4);
            pricefrompara.Value = con_subprice.PriceFrom;
            paras.Add(pricefrompara);

            SqlParameter pricestyle1para = new SqlParameter("@PriceStyle1", SqlDbType.Int, 4);
            pricestyle1para.Value = con_subprice.PriceStyle1;
            paras.Add(pricestyle1para);

            SqlParameter pricestyle2para = new SqlParameter("@PriceStyle2", SqlDbType.Int, 4);
            pricestyle2para.Value = con_subprice.PriceStyle2;
            paras.Add(pricestyle2para);

            SqlParameter marginmodepara = new SqlParameter("@MarginMode", SqlDbType.Int, 4);
            marginmodepara.Value = con_subprice.MarginMode;
            paras.Add(marginmodepara);

            SqlParameter marginamountpara = new SqlParameter("@MarginAmount", SqlDbType.Decimal, 9);
            marginamountpara.Value = con_subprice.MarginAmount;
            paras.Add(marginamountpara);

            if (!string.IsNullOrEmpty(con_subprice.MarginMemo))
            {
                SqlParameter marginmemopara = new SqlParameter("@MarginMemo", SqlDbType.VarChar, 4000);
                marginmemopara.Value = con_subprice.MarginMemo;
                paras.Add(marginmemopara);
            }

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            SubPrice subprice = new SubPrice();

            int indexSubPriceId = dr.GetOrdinal("SubPriceId");
            subprice.SubPriceId = Convert.ToInt32(dr[indexSubPriceId]);

            int indexSubId = dr.GetOrdinal("SubId");
            if (dr["SubId"] != DBNull.Value)
            {
                subprice.SubId = Convert.ToInt32(dr[indexSubId]);
            }

            int indexFixedPrice = dr.GetOrdinal("FixedPrice");
            if (dr["FixedPrice"] != DBNull.Value)
            {
                subprice.FixedPrice = Convert.ToDecimal(dr[indexFixedPrice]);
            }

            int indexFixedPriceMemo = dr.GetOrdinal("FixedPriceMemo");
            if (dr["FixedPriceMemo"] != DBNull.Value)
            {
                subprice.FixedPriceMemo = Convert.ToString(dr[indexFixedPriceMemo]);
            }

            int indexWhoDoPrice = dr.GetOrdinal("WhoDoPrice");
            if (dr["WhoDoPrice"] != DBNull.Value)
            {
                subprice.WhoDoPrice = Convert.ToInt32(dr[indexWhoDoPrice]);
            }

            int indexAlmostPrice = dr.GetOrdinal("AlmostPrice");
            if (dr["AlmostPrice"] != DBNull.Value)
            {
                subprice.AlmostPrice = Convert.ToDecimal(dr[indexAlmostPrice]);
            }

            int indexDoPriceBeginDate = dr.GetOrdinal("DoPriceBeginDate");
            if (dr["DoPriceBeginDate"] != DBNull.Value)
            {
                subprice.DoPriceBeginDate = Convert.ToDateTime(dr[indexDoPriceBeginDate]);
            }

            int indexDoPriceEndDate = dr.GetOrdinal("DoPriceEndDate");
            if (dr["DoPriceEndDate"] != DBNull.Value)
            {
                subprice.DoPriceEndDate = Convert.ToDateTime(dr[indexDoPriceEndDate]);
            }

            int indexIsQP = dr.GetOrdinal("IsQP");
            if (dr["IsQP"] != DBNull.Value)
            {
                subprice.IsQP = Convert.ToBoolean(dr[indexIsQP]);
            }

            int indexPriceFrom = dr.GetOrdinal("PriceFrom");
            if (dr["PriceFrom"] != DBNull.Value)
            {
                subprice.PriceFrom = Convert.ToInt32(dr[indexPriceFrom]);
            }

            int indexPriceStyle1 = dr.GetOrdinal("PriceStyle1");
            if (dr["PriceStyle1"] != DBNull.Value)
            {
                subprice.PriceStyle1 = Convert.ToInt32(dr[indexPriceStyle1]);
            }

            int indexPriceStyle2 = dr.GetOrdinal("PriceStyle2");
            if (dr["PriceStyle2"] != DBNull.Value)
            {
                subprice.PriceStyle2 = Convert.ToInt32(dr[indexPriceStyle2]);
            }

            int indexMarginMode = dr.GetOrdinal("MarginMode");
            if (dr["MarginMode"] != DBNull.Value)
            {
                subprice.MarginMode = Convert.ToInt32(dr[indexMarginMode]);
            }

            int indexMarginAmount = dr.GetOrdinal("MarginAmount");
            if (dr["MarginAmount"] != DBNull.Value)
            {
                subprice.MarginAmount = Convert.ToDecimal(dr[indexMarginAmount]);
            }

            int indexMarginMemo = dr.GetOrdinal("MarginMemo");
            if (dr["MarginMemo"] != DBNull.Value)
            {
                subprice.MarginMemo = Convert.ToString(dr[indexMarginMemo]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                subprice.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                subprice.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                subprice.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                subprice.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return subprice;
        }

        public override string TableName
        {
            get
            {
                return "Con_SubPrice";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SubPrice con_subprice = (SubPrice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter subpriceidpara = new SqlParameter("@SubPriceId", SqlDbType.Int, 4);
            subpriceidpara.Value = con_subprice.SubPriceId;
            paras.Add(subpriceidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = con_subprice.SubId;
            paras.Add(subidpara);

            SqlParameter fixedpricepara = new SqlParameter("@FixedPrice", SqlDbType.Decimal, 9);
            fixedpricepara.Value = con_subprice.FixedPrice;
            paras.Add(fixedpricepara);

            if (!string.IsNullOrEmpty(con_subprice.FixedPriceMemo))
            {
                SqlParameter fixedpricememopara = new SqlParameter("@FixedPriceMemo", SqlDbType.VarChar, 4000);
                fixedpricememopara.Value = con_subprice.FixedPriceMemo;
                paras.Add(fixedpricememopara);
            }

            SqlParameter whodopricepara = new SqlParameter("@WhoDoPrice", SqlDbType.Int, 4);
            whodopricepara.Value = con_subprice.WhoDoPrice;
            paras.Add(whodopricepara);

            SqlParameter almostpricepara = new SqlParameter("@AlmostPrice", SqlDbType.Decimal, 9);
            almostpricepara.Value = con_subprice.AlmostPrice;
            paras.Add(almostpricepara);

            SqlParameter dopricebegindatepara = new SqlParameter("@DoPriceBeginDate", SqlDbType.DateTime, 8);
            dopricebegindatepara.Value = con_subprice.DoPriceBeginDate;
            paras.Add(dopricebegindatepara);

            SqlParameter dopriceenddatepara = new SqlParameter("@DoPriceEndDate", SqlDbType.DateTime, 8);
            dopriceenddatepara.Value = con_subprice.DoPriceEndDate;
            paras.Add(dopriceenddatepara);

            SqlParameter isqppara = new SqlParameter("@IsQP", SqlDbType.Bit, 1);
            isqppara.Value = con_subprice.IsQP;
            paras.Add(isqppara);

            SqlParameter pricefrompara = new SqlParameter("@PriceFrom", SqlDbType.Int, 4);
            pricefrompara.Value = con_subprice.PriceFrom;
            paras.Add(pricefrompara);

            SqlParameter pricestyle1para = new SqlParameter("@PriceStyle1", SqlDbType.Int, 4);
            pricestyle1para.Value = con_subprice.PriceStyle1;
            paras.Add(pricestyle1para);

            SqlParameter pricestyle2para = new SqlParameter("@PriceStyle2", SqlDbType.Int, 4);
            pricestyle2para.Value = con_subprice.PriceStyle2;
            paras.Add(pricestyle2para);

            SqlParameter marginmodepara = new SqlParameter("@MarginMode", SqlDbType.Int, 4);
            marginmodepara.Value = con_subprice.MarginMode;
            paras.Add(marginmodepara);

            SqlParameter marginamountpara = new SqlParameter("@MarginAmount", SqlDbType.Decimal, 9);
            marginamountpara.Value = con_subprice.MarginAmount;
            paras.Add(marginamountpara);

            if (!string.IsNullOrEmpty(con_subprice.MarginMemo))
            {
                SqlParameter marginmemopara = new SqlParameter("@MarginMemo", SqlDbType.VarChar, 4000);
                marginmemopara.Value = con_subprice.MarginMemo;
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
