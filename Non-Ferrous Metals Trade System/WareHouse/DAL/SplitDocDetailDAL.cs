/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SplitDocDetailDAL.cs
// 文件功能描述：拆单明细dbo.St_SplitDocDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月27日
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
    /// 拆单明细dbo.St_SplitDocDetail数据交互类。
    /// </summary>
    public class SplitDocDetailDAL : DetailOperate, ISplitDocDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SplitDocDetailDAL()
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
            SplitDocDetail st_splitdocdetail = (SplitDocDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter splitdocidpara = new SqlParameter("@SplitDocId", SqlDbType.Int, 4);
            splitdocidpara.Value = st_splitdocdetail.SplitDocId;
            paras.Add(splitdocidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = st_splitdocdetail.DetailStatus;
            paras.Add(detailstatuspara);

            if (!string.IsNullOrEmpty(st_splitdocdetail.NewRefNo))
            {
                SqlParameter newrefnopara = new SqlParameter("@NewRefNo", SqlDbType.VarChar, 50);
                newrefnopara.Value = st_splitdocdetail.NewRefNo;
                paras.Add(newrefnopara);
            }

            SqlParameter oldrefnoidpara = new SqlParameter("@OldRefNoId", SqlDbType.Int, 4);
            oldrefnoidpara.Value = st_splitdocdetail.OldRefNoId;
            paras.Add(oldrefnoidpara);

            SqlParameter oldstockidpara = new SqlParameter("@OldStockId", SqlDbType.Int, 4);
            oldstockidpara.Value = st_splitdocdetail.OldStockId;
            paras.Add(oldstockidpara);

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = st_splitdocdetail.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = st_splitdocdetail.NetAmount;
            paras.Add(netamountpara);

            SqlParameter unitidpara = new SqlParameter("@UnitId", SqlDbType.Int, 4);
            unitidpara.Value = st_splitdocdetail.UnitId;
            paras.Add(unitidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = st_splitdocdetail.AssetId;
            paras.Add(assetidpara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = st_splitdocdetail.Bundles;
            paras.Add(bundlespara);

            SqlParameter brandidpara = new SqlParameter("@BrandId", SqlDbType.Int, 4);
            brandidpara.Value = st_splitdocdetail.BrandId;
            paras.Add(brandidpara);

            if (!string.IsNullOrEmpty(st_splitdocdetail.PaperNo))
            {
                SqlParameter papernopara = new SqlParameter("@PaperNo", SqlDbType.VarChar, 80);
                papernopara.Value = st_splitdocdetail.PaperNo;
                paras.Add(papernopara);
            }

            SqlParameter paperholderpara = new SqlParameter("@PaperHolder", SqlDbType.Int, 4);
            paperholderpara.Value = st_splitdocdetail.PaperHolder;
            paras.Add(paperholderpara);

            if (!string.IsNullOrEmpty(st_splitdocdetail.CardNo))
            {
                SqlParameter cardnopara = new SqlParameter("@CardNo", SqlDbType.VarChar, 200);
                cardnopara.Value = st_splitdocdetail.CardNo;
                paras.Add(cardnopara);
            }

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_splitdocdetail.StockLogId;
            paras.Add(stocklogidpara);

            if (!string.IsNullOrEmpty(st_splitdocdetail.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = st_splitdocdetail.Memo;
                paras.Add(memopara);
            }

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            SplitDocDetail splitdocdetail = new SplitDocDetail();

            splitdocdetail.DetailId = Convert.ToInt32(dr["DetailId"]);

            if (dr["SplitDocId"] != DBNull.Value)
            {
                splitdocdetail.SplitDocId = Convert.ToInt32(dr["SplitDocId"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                splitdocdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }

            if (dr["NewRefNo"] != DBNull.Value)
            {
                splitdocdetail.NewRefNo = Convert.ToString(dr["NewRefNo"]);
            }

            if (dr["OldRefNoId"] != DBNull.Value)
            {
                splitdocdetail.OldRefNoId = Convert.ToInt32(dr["OldRefNoId"]);
            }

            if (dr["OldStockId"] != DBNull.Value)
            {
                splitdocdetail.OldStockId = Convert.ToInt32(dr["OldStockId"]);
            }

            if (dr["GrossAmount"] != DBNull.Value)
            {
                splitdocdetail.GrossAmount = Convert.ToDecimal(dr["GrossAmount"]);
            }

            if (dr["NetAmount"] != DBNull.Value)
            {
                splitdocdetail.NetAmount = Convert.ToDecimal(dr["NetAmount"]);
            }

            if (dr["UnitId"] != DBNull.Value)
            {
                splitdocdetail.UnitId = Convert.ToInt32(dr["UnitId"]);
            }

            if (dr["AssetId"] != DBNull.Value)
            {
                splitdocdetail.AssetId = Convert.ToInt32(dr["AssetId"]);
            }

            if (dr["Bundles"] != DBNull.Value)
            {
                splitdocdetail.Bundles = Convert.ToInt32(dr["Bundles"]);
            }

            if (dr["BrandId"] != DBNull.Value)
            {
                splitdocdetail.BrandId = Convert.ToInt32(dr["BrandId"]);
            }

            if (dr["PaperNo"] != DBNull.Value)
            {
                splitdocdetail.PaperNo = Convert.ToString(dr["PaperNo"]);
            }

            if (dr["PaperHolder"] != DBNull.Value)
            {
                splitdocdetail.PaperHolder = Convert.ToInt32(dr["PaperHolder"]);
            }

            if (dr["CardNo"] != DBNull.Value)
            {
                splitdocdetail.CardNo = Convert.ToString(dr["CardNo"]);
            }

            if (dr["StockLogId"] != DBNull.Value)
            {
                splitdocdetail.StockLogId = Convert.ToInt32(dr["StockLogId"]);
            }

            if (dr["Memo"] != DBNull.Value)
            {
                splitdocdetail.Memo = Convert.ToString(dr["Memo"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                splitdocdetail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                splitdocdetail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                splitdocdetail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                splitdocdetail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return splitdocdetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            SplitDocDetail splitdocdetail = new SplitDocDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            splitdocdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexSplitDocId = dr.GetOrdinal("SplitDocId");
            if (dr["SplitDocId"] != DBNull.Value)
            {
                splitdocdetail.SplitDocId = Convert.ToInt32(dr[indexSplitDocId]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                splitdocdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexNewRefNo = dr.GetOrdinal("NewRefNo");
            if (dr["NewRefNo"] != DBNull.Value)
            {
                splitdocdetail.NewRefNo = Convert.ToString(dr[indexNewRefNo]);
            }

            int indexOldRefNoId = dr.GetOrdinal("OldRefNoId");
            if (dr["OldRefNoId"] != DBNull.Value)
            {
                splitdocdetail.OldRefNoId = Convert.ToInt32(dr[indexOldRefNoId]);
            }

            int indexOldStockId = dr.GetOrdinal("OldStockId");
            if (dr["OldStockId"] != DBNull.Value)
            {
                splitdocdetail.OldStockId = Convert.ToInt32(dr[indexOldStockId]);
            }

            int indexGrossAmount = dr.GetOrdinal("GrossAmount");
            if (dr["GrossAmount"] != DBNull.Value)
            {
                splitdocdetail.GrossAmount = Convert.ToDecimal(dr[indexGrossAmount]);
            }

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            if (dr["NetAmount"] != DBNull.Value)
            {
                splitdocdetail.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
            }

            int indexUnitId = dr.GetOrdinal("UnitId");
            if (dr["UnitId"] != DBNull.Value)
            {
                splitdocdetail.UnitId = Convert.ToInt32(dr[indexUnitId]);
            }

            int indexAssetId = dr.GetOrdinal("AssetId");
            if (dr["AssetId"] != DBNull.Value)
            {
                splitdocdetail.AssetId = Convert.ToInt32(dr[indexAssetId]);
            }

            int indexBundles = dr.GetOrdinal("Bundles");
            if (dr["Bundles"] != DBNull.Value)
            {
                splitdocdetail.Bundles = Convert.ToInt32(dr[indexBundles]);
            }

            int indexBrandId = dr.GetOrdinal("BrandId");
            if (dr["BrandId"] != DBNull.Value)
            {
                splitdocdetail.BrandId = Convert.ToInt32(dr[indexBrandId]);
            }

            int indexPaperNo = dr.GetOrdinal("PaperNo");
            if (dr["PaperNo"] != DBNull.Value)
            {
                splitdocdetail.PaperNo = Convert.ToString(dr[indexPaperNo]);
            }

            int indexPaperHolder = dr.GetOrdinal("PaperHolder");
            if (dr["PaperHolder"] != DBNull.Value)
            {
                splitdocdetail.PaperHolder = Convert.ToInt32(dr[indexPaperHolder]);
            }

            int indexCardNo = dr.GetOrdinal("CardNo");
            if (dr["CardNo"] != DBNull.Value)
            {
                splitdocdetail.CardNo = Convert.ToString(dr[indexCardNo]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                splitdocdetail.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                splitdocdetail.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                splitdocdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                splitdocdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                splitdocdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                splitdocdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return splitdocdetail;
        }

        public override string TableName
        {
            get
            {
                return "St_SplitDocDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SplitDocDetail st_splitdocdetail = (SplitDocDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = st_splitdocdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter splitdocidpara = new SqlParameter("@SplitDocId", SqlDbType.Int, 4);
            splitdocidpara.Value = st_splitdocdetail.SplitDocId;
            paras.Add(splitdocidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = st_splitdocdetail.DetailStatus;
            paras.Add(detailstatuspara);

            if (!string.IsNullOrEmpty(st_splitdocdetail.NewRefNo))
            {
                SqlParameter newrefnopara = new SqlParameter("@NewRefNo", SqlDbType.VarChar, 50);
                newrefnopara.Value = st_splitdocdetail.NewRefNo;
                paras.Add(newrefnopara);
            }

            SqlParameter oldrefnoidpara = new SqlParameter("@OldRefNoId", SqlDbType.Int, 4);
            oldrefnoidpara.Value = st_splitdocdetail.OldRefNoId;
            paras.Add(oldrefnoidpara);

            SqlParameter oldstockidpara = new SqlParameter("@OldStockId", SqlDbType.Int, 4);
            oldstockidpara.Value = st_splitdocdetail.OldStockId;
            paras.Add(oldstockidpara);

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = st_splitdocdetail.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = st_splitdocdetail.NetAmount;
            paras.Add(netamountpara);

            SqlParameter unitidpara = new SqlParameter("@UnitId", SqlDbType.Int, 4);
            unitidpara.Value = st_splitdocdetail.UnitId;
            paras.Add(unitidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = st_splitdocdetail.AssetId;
            paras.Add(assetidpara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = st_splitdocdetail.Bundles;
            paras.Add(bundlespara);

            SqlParameter brandidpara = new SqlParameter("@BrandId", SqlDbType.Int, 4);
            brandidpara.Value = st_splitdocdetail.BrandId;
            paras.Add(brandidpara);

            if (!string.IsNullOrEmpty(st_splitdocdetail.PaperNo))
            {
                SqlParameter papernopara = new SqlParameter("@PaperNo", SqlDbType.VarChar, 80);
                papernopara.Value = st_splitdocdetail.PaperNo;
                paras.Add(papernopara);
            }

            SqlParameter paperholderpara = new SqlParameter("@PaperHolder", SqlDbType.Int, 4);
            paperholderpara.Value = st_splitdocdetail.PaperHolder;
            paras.Add(paperholderpara);

            if (!string.IsNullOrEmpty(st_splitdocdetail.CardNo))
            {
                SqlParameter cardnopara = new SqlParameter("@CardNo", SqlDbType.VarChar, 200);
                cardnopara.Value = st_splitdocdetail.CardNo;
                paras.Add(cardnopara);
            }

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_splitdocdetail.StockLogId;
            paras.Add(stocklogidpara);

            if (!string.IsNullOrEmpty(st_splitdocdetail.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = st_splitdocdetail.Memo;
                paras.Add(memopara);
            }

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int splitDocId, Common.StatusEnum status = Common.StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.St_SplitDocDetail where SplitDocId ={0} and DetailStatus>={1}", splitDocId, (int)status);
            return Load<Model.SplitDocDetail>(user, CommandType.Text, cmdText);
        }

        #endregion
    }
}
