/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockInDAL.cs
// 文件功能描述：入库登记dbo.St_StockIn数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.WareHouse.Model;
using NFMT.DBUtility;
using NFMT.WareHouse.IDAL;
using NFMT.Common;

namespace NFMT.WareHouse.DAL
{
    /// <summary>
    /// 入库登记dbo.St_StockIn数据交互类。
    /// </summary>
    public partial class StockInDAL : ExecOperate, IStockInDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockInDAL()
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
            StockIn st_stockin = (StockIn)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockInId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter groupidpara = new SqlParameter("@GroupId", SqlDbType.Int, 4);
            groupidpara.Value = st_stockin.GroupId;
            paras.Add(groupidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = st_stockin.CorpId;
            paras.Add(corpidpara);

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = st_stockin.DeptId;
            paras.Add(deptidpara);

            SqlParameter stockindatepara = new SqlParameter("@StockInDate", SqlDbType.DateTime, 8);
            stockindatepara.Value = st_stockin.StockInDate;
            paras.Add(stockindatepara);

            SqlParameter customtypepara = new SqlParameter("@CustomType", SqlDbType.Int, 4);
            customtypepara.Value = st_stockin.CustomType;
            paras.Add(customtypepara);

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = st_stockin.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = st_stockin.NetAmount;
            paras.Add(netamountpara);

            SqlParameter uintidpara = new SqlParameter("@UintId", SqlDbType.Int, 4);
            uintidpara.Value = st_stockin.UintId;
            paras.Add(uintidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = st_stockin.AssetId;
            paras.Add(assetidpara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = st_stockin.Bundles;
            paras.Add(bundlespara);

            SqlParameter brandidpara = new SqlParameter("@BrandId", SqlDbType.Int, 4);
            brandidpara.Value = st_stockin.BrandId;
            paras.Add(brandidpara);

            SqlParameter deliverplaceidpara = new SqlParameter("@DeliverPlaceId", SqlDbType.Int, 4);
            deliverplaceidpara.Value = st_stockin.DeliverPlaceId;
            paras.Add(deliverplaceidpara);

            SqlParameter produceridpara = new SqlParameter("@ProducerId", SqlDbType.Int, 4);
            produceridpara.Value = st_stockin.ProducerId;
            paras.Add(produceridpara);

            SqlParameter stocktypepara = new SqlParameter("@StockType", SqlDbType.Int, 4);
            stocktypepara.Value = st_stockin.StockType;
            paras.Add(stocktypepara);

            SqlParameter stockoperatetypepara = new SqlParameter("@StockOperateType", SqlDbType.Int, 4);
            stockoperatetypepara.Value = st_stockin.StockOperateType;
            paras.Add(stockoperatetypepara);

            if (!string.IsNullOrEmpty(st_stockin.PaperNo))
            {
                SqlParameter papernopara = new SqlParameter("@PaperNo", SqlDbType.VarChar, 80);
                papernopara.Value = st_stockin.PaperNo;
                paras.Add(papernopara);
            }

            SqlParameter paperholderpara = new SqlParameter("@PaperHolder", SqlDbType.Int, 4);
            paperholderpara.Value = st_stockin.PaperHolder;
            paras.Add(paperholderpara);

            if (!string.IsNullOrEmpty(st_stockin.CardNo))
            {
                SqlParameter cardnopara = new SqlParameter("@CardNo", SqlDbType.VarChar, 200);
                cardnopara.Value = st_stockin.CardNo;
                paras.Add(cardnopara);
            }

            if (!string.IsNullOrEmpty(st_stockin.Format))
            {
                SqlParameter formatpara = new SqlParameter("@Format", SqlDbType.VarChar, 200);
                formatpara.Value = st_stockin.Format;
                paras.Add(formatpara);
            }

            SqlParameter originplaceidpara = new SqlParameter("@OriginPlaceId", SqlDbType.Int, 4);
            originplaceidpara.Value = st_stockin.OriginPlaceId;
            paras.Add(originplaceidpara);

            if (!string.IsNullOrEmpty(st_stockin.OriginPlace))
            {
                SqlParameter originplacepara = new SqlParameter("@OriginPlace", SqlDbType.VarChar, 200);
                originplacepara.Value = st_stockin.OriginPlace;
                paras.Add(originplacepara);
            }

            SqlParameter stockinstatuspara = new SqlParameter("@StockInStatus", SqlDbType.Int, 4);
            stockinstatuspara.Value = st_stockin.StockInStatus;
            paras.Add(stockinstatuspara);

            if (!string.IsNullOrEmpty(st_stockin.RefNo))
            {
                SqlParameter refnopara = new SqlParameter("@RefNo", SqlDbType.VarChar, 50);
                refnopara.Value = st_stockin.RefNo;
                paras.Add(refnopara);
            }

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockIn stockin = new StockIn();

            int indexStockInId = dr.GetOrdinal("StockInId");
            stockin.StockInId = Convert.ToInt32(dr[indexStockInId]);

            int indexGroupId = dr.GetOrdinal("GroupId");
            if (dr["GroupId"] != DBNull.Value)
            {
                stockin.GroupId = Convert.ToInt32(dr[indexGroupId]);
            }

            int indexCorpId = dr.GetOrdinal("CorpId");
            if (dr["CorpId"] != DBNull.Value)
            {
                stockin.CorpId = Convert.ToInt32(dr[indexCorpId]);
            }

            int indexDeptId = dr.GetOrdinal("DeptId");
            if (dr["DeptId"] != DBNull.Value)
            {
                stockin.DeptId = Convert.ToInt32(dr[indexDeptId]);
            }

            int indexStockInDate = dr.GetOrdinal("StockInDate");
            if (dr["StockInDate"] != DBNull.Value)
            {
                stockin.StockInDate = Convert.ToDateTime(dr[indexStockInDate]);
            }

            int indexCustomType = dr.GetOrdinal("CustomType");
            if (dr["CustomType"] != DBNull.Value)
            {
                stockin.CustomType = Convert.ToInt32(dr[indexCustomType]);
            }

            int indexGrossAmount = dr.GetOrdinal("GrossAmount");
            if (dr["GrossAmount"] != DBNull.Value)
            {
                stockin.GrossAmount = Convert.ToDecimal(dr[indexGrossAmount]);
            }

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            if (dr["NetAmount"] != DBNull.Value)
            {
                stockin.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
            }

            int indexUintId = dr.GetOrdinal("UintId");
            if (dr["UintId"] != DBNull.Value)
            {
                stockin.UintId = Convert.ToInt32(dr[indexUintId]);
            }

            int indexAssetId = dr.GetOrdinal("AssetId");
            if (dr["AssetId"] != DBNull.Value)
            {
                stockin.AssetId = Convert.ToInt32(dr[indexAssetId]);
            }

            int indexBundles = dr.GetOrdinal("Bundles");
            if (dr["Bundles"] != DBNull.Value)
            {
                stockin.Bundles = Convert.ToInt32(dr[indexBundles]);
            }

            int indexBrandId = dr.GetOrdinal("BrandId");
            if (dr["BrandId"] != DBNull.Value)
            {
                stockin.BrandId = Convert.ToInt32(dr[indexBrandId]);
            }

            int indexDeliverPlaceId = dr.GetOrdinal("DeliverPlaceId");
            if (dr["DeliverPlaceId"] != DBNull.Value)
            {
                stockin.DeliverPlaceId = Convert.ToInt32(dr[indexDeliverPlaceId]);
            }

            int indexProducerId = dr.GetOrdinal("ProducerId");
            if (dr["ProducerId"] != DBNull.Value)
            {
                stockin.ProducerId = Convert.ToInt32(dr[indexProducerId]);
            }

            int indexStockType = dr.GetOrdinal("StockType");
            if (dr["StockType"] != DBNull.Value)
            {
                stockin.StockType = Convert.ToInt32(dr[indexStockType]);
            }

            int indexStockOperateType = dr.GetOrdinal("StockOperateType");
            if (dr["StockOperateType"] != DBNull.Value)
            {
                stockin.StockOperateType = Convert.ToInt32(dr[indexStockOperateType]);
            }

            int indexPaperNo = dr.GetOrdinal("PaperNo");
            if (dr["PaperNo"] != DBNull.Value)
            {
                stockin.PaperNo = Convert.ToString(dr[indexPaperNo]);
            }

            int indexPaperHolder = dr.GetOrdinal("PaperHolder");
            if (dr["PaperHolder"] != DBNull.Value)
            {
                stockin.PaperHolder = Convert.ToInt32(dr[indexPaperHolder]);
            }

            int indexCardNo = dr.GetOrdinal("CardNo");
            if (dr["CardNo"] != DBNull.Value)
            {
                stockin.CardNo = Convert.ToString(dr[indexCardNo]);
            }

            int indexFormat = dr.GetOrdinal("Format");
            if (dr["Format"] != DBNull.Value)
            {
                stockin.Format = Convert.ToString(dr[indexFormat]);
            }

            int indexOriginPlaceId = dr.GetOrdinal("OriginPlaceId");
            if (dr["OriginPlaceId"] != DBNull.Value)
            {
                stockin.OriginPlaceId = Convert.ToInt32(dr[indexOriginPlaceId]);
            }

            int indexOriginPlace = dr.GetOrdinal("OriginPlace");
            if (dr["OriginPlace"] != DBNull.Value)
            {
                stockin.OriginPlace = Convert.ToString(dr[indexOriginPlace]);
            }

            int indexStockInStatus = dr.GetOrdinal("StockInStatus");
            if (dr["StockInStatus"] != DBNull.Value)
            {
                stockin.StockInStatus = (StatusEnum)Convert.ToInt32(dr[indexStockInStatus]);
            }

            int indexRefNo = dr.GetOrdinal("RefNo");
            if (dr["RefNo"] != DBNull.Value)
            {
                stockin.RefNo = Convert.ToString(dr[indexRefNo]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                stockin.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                stockin.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                stockin.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                stockin.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return stockin;
        }

        public override string TableName
        {
            get
            {
                return "St_StockIn";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockIn st_stockin = (StockIn)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stockinidpara = new SqlParameter("@StockInId", SqlDbType.Int, 4);
            stockinidpara.Value = st_stockin.StockInId;
            paras.Add(stockinidpara);

            SqlParameter groupidpara = new SqlParameter("@GroupId", SqlDbType.Int, 4);
            groupidpara.Value = st_stockin.GroupId;
            paras.Add(groupidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = st_stockin.CorpId;
            paras.Add(corpidpara);

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = st_stockin.DeptId;
            paras.Add(deptidpara);

            SqlParameter stockindatepara = new SqlParameter("@StockInDate", SqlDbType.DateTime, 8);
            stockindatepara.Value = st_stockin.StockInDate;
            paras.Add(stockindatepara);

            SqlParameter customtypepara = new SqlParameter("@CustomType", SqlDbType.Int, 4);
            customtypepara.Value = st_stockin.CustomType;
            paras.Add(customtypepara);

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = st_stockin.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = st_stockin.NetAmount;
            paras.Add(netamountpara);

            SqlParameter uintidpara = new SqlParameter("@UintId", SqlDbType.Int, 4);
            uintidpara.Value = st_stockin.UintId;
            paras.Add(uintidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = st_stockin.AssetId;
            paras.Add(assetidpara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = st_stockin.Bundles;
            paras.Add(bundlespara);

            SqlParameter brandidpara = new SqlParameter("@BrandId", SqlDbType.Int, 4);
            brandidpara.Value = st_stockin.BrandId;
            paras.Add(brandidpara);

            SqlParameter deliverplaceidpara = new SqlParameter("@DeliverPlaceId", SqlDbType.Int, 4);
            deliverplaceidpara.Value = st_stockin.DeliverPlaceId;
            paras.Add(deliverplaceidpara);

            SqlParameter produceridpara = new SqlParameter("@ProducerId", SqlDbType.Int, 4);
            produceridpara.Value = st_stockin.ProducerId;
            paras.Add(produceridpara);

            SqlParameter stocktypepara = new SqlParameter("@StockType", SqlDbType.Int, 4);
            stocktypepara.Value = st_stockin.StockType;
            paras.Add(stocktypepara);

            SqlParameter stockoperatetypepara = new SqlParameter("@StockOperateType", SqlDbType.Int, 4);
            stockoperatetypepara.Value = st_stockin.StockOperateType;
            paras.Add(stockoperatetypepara);

            if (!string.IsNullOrEmpty(st_stockin.PaperNo))
            {
                SqlParameter papernopara = new SqlParameter("@PaperNo", SqlDbType.VarChar, 80);
                papernopara.Value = st_stockin.PaperNo;
                paras.Add(papernopara);
            }

            SqlParameter paperholderpara = new SqlParameter("@PaperHolder", SqlDbType.Int, 4);
            paperholderpara.Value = st_stockin.PaperHolder;
            paras.Add(paperholderpara);

            if (!string.IsNullOrEmpty(st_stockin.CardNo))
            {
                SqlParameter cardnopara = new SqlParameter("@CardNo", SqlDbType.VarChar, 200);
                cardnopara.Value = st_stockin.CardNo;
                paras.Add(cardnopara);
            }

            if (!string.IsNullOrEmpty(st_stockin.Format))
            {
                SqlParameter formatpara = new SqlParameter("@Format", SqlDbType.VarChar, 200);
                formatpara.Value = st_stockin.Format;
                paras.Add(formatpara);
            }

            SqlParameter originplaceidpara = new SqlParameter("@OriginPlaceId", SqlDbType.Int, 4);
            originplaceidpara.Value = st_stockin.OriginPlaceId;
            paras.Add(originplaceidpara);

            if (!string.IsNullOrEmpty(st_stockin.OriginPlace))
            {
                SqlParameter originplacepara = new SqlParameter("@OriginPlace", SqlDbType.VarChar, 200);
                originplacepara.Value = st_stockin.OriginPlace;
                paras.Add(originplacepara);
            }

            SqlParameter stockinstatuspara = new SqlParameter("@StockInStatus", SqlDbType.Int, 4);
            stockinstatuspara.Value = st_stockin.StockInStatus;
            paras.Add(stockinstatuspara);

            if (!string.IsNullOrEmpty(st_stockin.RefNo))
            {
                SqlParameter refnopara = new SqlParameter("@RefNo", SqlDbType.VarChar, 50);
                refnopara.Value = st_stockin.RefNo;
                paras.Add(refnopara);
            }

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
