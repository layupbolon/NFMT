/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PriceConfirmDetailDAL.cs
// 文件功能描述：价格确认明细dbo.Pri_PriceConfirmDetail数据交互类。
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
    /// 价格确认明细dbo.Pri_PriceConfirmDetail数据交互类。
    /// </summary>
    public partial class PriceConfirmDetailDAL : DetailOperate, IPriceConfirmDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PriceConfirmDetailDAL()
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
            PriceConfirmDetail pri_priceconfirmdetail = (PriceConfirmDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter priceconfirmidpara = new SqlParameter("@PriceConfirmId", SqlDbType.Int, 4);
            priceconfirmidpara.Value = pri_priceconfirmdetail.PriceConfirmId;
            paras.Add(priceconfirmidpara);

            SqlParameter interestdetailidpara = new SqlParameter("@InterestDetailId", SqlDbType.Int, 4);
            interestdetailidpara.Value = pri_priceconfirmdetail.InterestDetailId;
            paras.Add(interestdetailidpara);

            SqlParameter interestidpara = new SqlParameter("@InterestId", SqlDbType.Int, 4);
            interestidpara.Value = pri_priceconfirmdetail.InterestId;
            paras.Add(interestidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = pri_priceconfirmdetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = pri_priceconfirmdetail.StockId;
            paras.Add(stockidpara);

            SqlParameter confirmamountpara = new SqlParameter("@ConfirmAmount", SqlDbType.Decimal, 9);
            confirmamountpara.Value = pri_priceconfirmdetail.ConfirmAmount;
            paras.Add(confirmamountpara);

            SqlParameter settlepricepara = new SqlParameter("@SettlePrice", SqlDbType.Decimal, 9);
            settlepricepara.Value = pri_priceconfirmdetail.SettlePrice;
            paras.Add(settlepricepara);

            SqlParameter settlebalapara = new SqlParameter("@SettleBala", SqlDbType.Decimal, 9);
            settlebalapara.Value = pri_priceconfirmdetail.SettleBala;
            paras.Add(settlebalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            PriceConfirmDetail priceconfirmdetail = new PriceConfirmDetail();

            priceconfirmdetail.DetailId = Convert.ToInt32(dr["DetailId"]);

            if (dr["PriceConfirmId"] != DBNull.Value)
            {
                priceconfirmdetail.PriceConfirmId = Convert.ToInt32(dr["PriceConfirmId"]);
            }

            if (dr["InterestDetailId"] != DBNull.Value)
            {
                priceconfirmdetail.InterestDetailId = Convert.ToInt32(dr["InterestDetailId"]);
            }

            if (dr["InterestId"] != DBNull.Value)
            {
                priceconfirmdetail.InterestId = Convert.ToInt32(dr["InterestId"]);
            }

            if (dr["StockLogId"] != DBNull.Value)
            {
                priceconfirmdetail.StockLogId = Convert.ToInt32(dr["StockLogId"]);
            }

            if (dr["StockId"] != DBNull.Value)
            {
                priceconfirmdetail.StockId = Convert.ToInt32(dr["StockId"]);
            }

            if (dr["ConfirmAmount"] != DBNull.Value)
            {
                priceconfirmdetail.ConfirmAmount = Convert.ToDecimal(dr["ConfirmAmount"]);
            }

            if (dr["SettlePrice"] != DBNull.Value)
            {
                priceconfirmdetail.SettlePrice = Convert.ToDecimal(dr["SettlePrice"]);
            }

            if (dr["SettleBala"] != DBNull.Value)
            {
                priceconfirmdetail.SettleBala = Convert.ToDecimal(dr["SettleBala"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                priceconfirmdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                priceconfirmdetail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                priceconfirmdetail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                priceconfirmdetail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                priceconfirmdetail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return priceconfirmdetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PriceConfirmDetail priceconfirmdetail = new PriceConfirmDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            priceconfirmdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexPriceConfirmId = dr.GetOrdinal("PriceConfirmId");
            if (dr["PriceConfirmId"] != DBNull.Value)
            {
                priceconfirmdetail.PriceConfirmId = Convert.ToInt32(dr[indexPriceConfirmId]);
            }

            int indexInterestDetailId = dr.GetOrdinal("InterestDetailId");
            if (dr["InterestDetailId"] != DBNull.Value)
            {
                priceconfirmdetail.InterestDetailId = Convert.ToInt32(dr[indexInterestDetailId]);
            }

            int indexInterestId = dr.GetOrdinal("InterestId");
            if (dr["InterestId"] != DBNull.Value)
            {
                priceconfirmdetail.InterestId = Convert.ToInt32(dr[indexInterestId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                priceconfirmdetail.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                priceconfirmdetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexConfirmAmount = dr.GetOrdinal("ConfirmAmount");
            if (dr["ConfirmAmount"] != DBNull.Value)
            {
                priceconfirmdetail.ConfirmAmount = Convert.ToDecimal(dr[indexConfirmAmount]);
            }

            int indexSettlePrice = dr.GetOrdinal("SettlePrice");
            if (dr["SettlePrice"] != DBNull.Value)
            {
                priceconfirmdetail.SettlePrice = Convert.ToDecimal(dr[indexSettlePrice]);
            }

            int indexSettleBala = dr.GetOrdinal("SettleBala");
            if (dr["SettleBala"] != DBNull.Value)
            {
                priceconfirmdetail.SettleBala = Convert.ToDecimal(dr[indexSettleBala]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                priceconfirmdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                priceconfirmdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                priceconfirmdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                priceconfirmdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                priceconfirmdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return priceconfirmdetail;
        }

        public override string TableName
        {
            get
            {
                return "Pri_PriceConfirmDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PriceConfirmDetail pri_priceconfirmdetail = (PriceConfirmDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = pri_priceconfirmdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter priceconfirmidpara = new SqlParameter("@PriceConfirmId", SqlDbType.Int, 4);
            priceconfirmidpara.Value = pri_priceconfirmdetail.PriceConfirmId;
            paras.Add(priceconfirmidpara);

            SqlParameter interestdetailidpara = new SqlParameter("@InterestDetailId", SqlDbType.Int, 4);
            interestdetailidpara.Value = pri_priceconfirmdetail.InterestDetailId;
            paras.Add(interestdetailidpara);

            SqlParameter interestidpara = new SqlParameter("@InterestId", SqlDbType.Int, 4);
            interestidpara.Value = pri_priceconfirmdetail.InterestId;
            paras.Add(interestidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = pri_priceconfirmdetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = pri_priceconfirmdetail.StockId;
            paras.Add(stockidpara);

            SqlParameter confirmamountpara = new SqlParameter("@ConfirmAmount", SqlDbType.Decimal, 9);
            confirmamountpara.Value = pri_priceconfirmdetail.ConfirmAmount;
            paras.Add(confirmamountpara);

            SqlParameter settlepricepara = new SqlParameter("@SettlePrice", SqlDbType.Decimal, 9);
            settlepricepara.Value = pri_priceconfirmdetail.SettlePrice;
            paras.Add(settlepricepara);

            SqlParameter settlebalapara = new SqlParameter("@SettleBala", SqlDbType.Decimal, 9);
            settlebalapara.Value = pri_priceconfirmdetail.SettleBala;
            paras.Add(settlebalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = pri_priceconfirmdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
