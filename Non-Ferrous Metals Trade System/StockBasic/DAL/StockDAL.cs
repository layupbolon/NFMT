using NFMT.Common;
using NFMT.StockBasic.IDAL;
using NFMT.StockBasic.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.StockBasic.DAL
{
    /// <summary>
    /// 库存dbo.St_Stock数据交互类。
    /// </summary>
    public class StockDAL : ExecOperate, IStockDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockDAL()
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
            Stock st_stock = (Stock)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stocknameidpara = new SqlParameter("@StockNameId", SqlDbType.Int, 4);
            stocknameidpara.Value = st_stock.StockNameId;
            paras.Add(stocknameidpara);

            if (!string.IsNullOrEmpty(st_stock.StockNo))
            {
                SqlParameter stocknopara = new SqlParameter("@StockNo", SqlDbType.VarChar, 80);
                stocknopara.Value = st_stock.StockNo;
                paras.Add(stocknopara);
            }

            SqlParameter stockdatepara = new SqlParameter("@StockDate", SqlDbType.DateTime, 8);
            stockdatepara.Value = st_stock.StockDate;
            paras.Add(stockdatepara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = st_stock.AssetId;
            paras.Add(assetidpara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = st_stock.Bundles;
            paras.Add(bundlespara);

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = st_stock.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = st_stock.NetAmount;
            paras.Add(netamountpara);

            SqlParameter receiptingappara = new SqlParameter("@ReceiptInGap", SqlDbType.Decimal, 9);
            receiptingappara.Value = st_stock.ReceiptInGap;
            paras.Add(receiptingappara);

            SqlParameter receiptoutgappara = new SqlParameter("@ReceiptOutGap", SqlDbType.Decimal, 9);
            receiptoutgappara.Value = st_stock.ReceiptOutGap;
            paras.Add(receiptoutgappara);

            SqlParameter curgrossamountpara = new SqlParameter("@CurGrossAmount", SqlDbType.Decimal, 9);
            curgrossamountpara.Value = st_stock.CurGrossAmount;
            paras.Add(curgrossamountpara);

            SqlParameter curnetamountpara = new SqlParameter("@CurNetAmount", SqlDbType.Decimal, 9);
            curnetamountpara.Value = st_stock.CurNetAmount;
            paras.Add(curnetamountpara);

            SqlParameter uintidpara = new SqlParameter("@UintId", SqlDbType.Int, 4);
            uintidpara.Value = st_stock.UintId;
            paras.Add(uintidpara);

            SqlParameter deliverplaceidpara = new SqlParameter("@DeliverPlaceId", SqlDbType.Int, 4);
            deliverplaceidpara.Value = st_stock.DeliverPlaceId;
            paras.Add(deliverplaceidpara);

            SqlParameter brandidpara = new SqlParameter("@BrandId", SqlDbType.Int, 4);
            brandidpara.Value = st_stock.BrandId;
            paras.Add(brandidpara);

            SqlParameter customstypepara = new SqlParameter("@CustomsType", SqlDbType.Int, 4);
            customstypepara.Value = st_stock.CustomsType;
            paras.Add(customstypepara);

            SqlParameter groupidpara = new SqlParameter("@GroupId", SqlDbType.Int, 4);
            groupidpara.Value = st_stock.GroupId;
            paras.Add(groupidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = st_stock.CorpId;
            paras.Add(corpidpara);

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = st_stock.DeptId;
            paras.Add(deptidpara);

            SqlParameter produceridpara = new SqlParameter("@ProducerId", SqlDbType.Int, 4);
            produceridpara.Value = st_stock.ProducerId;
            paras.Add(produceridpara);

            if (!string.IsNullOrEmpty(st_stock.PaperNo))
            {
                SqlParameter papernopara = new SqlParameter("@PaperNo", SqlDbType.VarChar, 80);
                papernopara.Value = st_stock.PaperNo;
                paras.Add(papernopara);
            }

            SqlParameter paperholderpara = new SqlParameter("@PaperHolder", SqlDbType.Int, 4);
            paperholderpara.Value = st_stock.PaperHolder;
            paras.Add(paperholderpara);

            if (!string.IsNullOrEmpty(st_stock.Format))
            {
                SqlParameter formatpara = new SqlParameter("@Format", SqlDbType.VarChar, 200);
                formatpara.Value = st_stock.Format;
                paras.Add(formatpara);
            }

            SqlParameter originplaceidpara = new SqlParameter("@OriginPlaceId", SqlDbType.Int, 4);
            originplaceidpara.Value = st_stock.OriginPlaceId;
            paras.Add(originplaceidpara);

            if (!string.IsNullOrEmpty(st_stock.OriginPlace))
            {
                SqlParameter originplacepara = new SqlParameter("@OriginPlace", SqlDbType.VarChar, 200);
                originplacepara.Value = st_stock.OriginPlace;
                paras.Add(originplacepara);
            }

            SqlParameter prestatuspara = new SqlParameter("@PreStatus", SqlDbType.Int, 4);
            prestatuspara.Value = st_stock.PreStatus;
            paras.Add(prestatuspara);

            SqlParameter stockstatuspara = new SqlParameter("@StockStatus", SqlDbType.Int, 4);
            stockstatuspara.Value = st_stock.StockStatus;
            paras.Add(stockstatuspara);

            if (!string.IsNullOrEmpty(st_stock.CardNo))
            {
                SqlParameter cardnopara = new SqlParameter("@CardNo", SqlDbType.VarChar, 200);
                cardnopara.Value = st_stock.CardNo;
                paras.Add(cardnopara);
            }

            if (!string.IsNullOrEmpty(st_stock.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = st_stock.Memo;
                paras.Add(memopara);
            }

            SqlParameter stocktypepara = new SqlParameter("@StockType", SqlDbType.Int, 4);
            stocktypepara.Value = st_stock.StockType;
            paras.Add(stocktypepara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Stock stock = new Stock();

            int indexStockId = dr.GetOrdinal("StockId");
            stock.StockId = Convert.ToInt32(dr[indexStockId]);

            int indexStockNameId = dr.GetOrdinal("StockNameId");
            if (dr["StockNameId"] != DBNull.Value)
            {
                stock.StockNameId = Convert.ToInt32(dr[indexStockNameId]);
            }

            int indexStockNo = dr.GetOrdinal("StockNo");
            if (dr["StockNo"] != DBNull.Value)
            {
                stock.StockNo = Convert.ToString(dr[indexStockNo]);
            }

            int indexStockDate = dr.GetOrdinal("StockDate");
            if (dr["StockDate"] != DBNull.Value)
            {
                stock.StockDate = Convert.ToDateTime(dr[indexStockDate]);
            }

            int indexAssetId = dr.GetOrdinal("AssetId");
            if (dr["AssetId"] != DBNull.Value)
            {
                stock.AssetId = Convert.ToInt32(dr[indexAssetId]);
            }

            int indexBundles = dr.GetOrdinal("Bundles");
            if (dr["Bundles"] != DBNull.Value)
            {
                stock.Bundles = Convert.ToInt32(dr[indexBundles]);
            }

            int indexGrossAmount = dr.GetOrdinal("GrossAmount");
            if (dr["GrossAmount"] != DBNull.Value)
            {
                stock.GrossAmount = Convert.ToDecimal(dr[indexGrossAmount]);
            }

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            if (dr["NetAmount"] != DBNull.Value)
            {
                stock.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
            }

            int indexReceiptInGap = dr.GetOrdinal("ReceiptInGap");
            if (dr["ReceiptInGap"] != DBNull.Value)
            {
                stock.ReceiptInGap = Convert.ToDecimal(dr[indexReceiptInGap]);
            }

            int indexReceiptOutGap = dr.GetOrdinal("ReceiptOutGap");
            if (dr["ReceiptOutGap"] != DBNull.Value)
            {
                stock.ReceiptOutGap = Convert.ToDecimal(dr[indexReceiptOutGap]);
            }

            int indexCurGrossAmount = dr.GetOrdinal("CurGrossAmount");
            if (dr["CurGrossAmount"] != DBNull.Value)
            {
                stock.CurGrossAmount = Convert.ToDecimal(dr[indexCurGrossAmount]);
            }

            int indexCurNetAmount = dr.GetOrdinal("CurNetAmount");
            if (dr["CurNetAmount"] != DBNull.Value)
            {
                stock.CurNetAmount = Convert.ToDecimal(dr[indexCurNetAmount]);
            }

            int indexUintId = dr.GetOrdinal("UintId");
            if (dr["UintId"] != DBNull.Value)
            {
                stock.UintId = Convert.ToInt32(dr[indexUintId]);
            }

            int indexDeliverPlaceId = dr.GetOrdinal("DeliverPlaceId");
            if (dr["DeliverPlaceId"] != DBNull.Value)
            {
                stock.DeliverPlaceId = Convert.ToInt32(dr[indexDeliverPlaceId]);
            }

            int indexBrandId = dr.GetOrdinal("BrandId");
            if (dr["BrandId"] != DBNull.Value)
            {
                stock.BrandId = Convert.ToInt32(dr[indexBrandId]);
            }

            int indexCustomsType = dr.GetOrdinal("CustomsType");
            if (dr["CustomsType"] != DBNull.Value)
            {
                stock.CustomsType = Convert.ToInt32(dr[indexCustomsType]);
            }

            int indexGroupId = dr.GetOrdinal("GroupId");
            if (dr["GroupId"] != DBNull.Value)
            {
                stock.GroupId = Convert.ToInt32(dr[indexGroupId]);
            }

            int indexCorpId = dr.GetOrdinal("CorpId");
            if (dr["CorpId"] != DBNull.Value)
            {
                stock.CorpId = Convert.ToInt32(dr[indexCorpId]);
            }

            int indexDeptId = dr.GetOrdinal("DeptId");
            if (dr["DeptId"] != DBNull.Value)
            {
                stock.DeptId = Convert.ToInt32(dr[indexDeptId]);
            }

            int indexProducerId = dr.GetOrdinal("ProducerId");
            if (dr["ProducerId"] != DBNull.Value)
            {
                stock.ProducerId = Convert.ToInt32(dr[indexProducerId]);
            }

            int indexPaperNo = dr.GetOrdinal("PaperNo");
            if (dr["PaperNo"] != DBNull.Value)
            {
                stock.PaperNo = Convert.ToString(dr[indexPaperNo]);
            }

            int indexPaperHolder = dr.GetOrdinal("PaperHolder");
            if (dr["PaperHolder"] != DBNull.Value)
            {
                stock.PaperHolder = Convert.ToInt32(dr[indexPaperHolder]);
            }

            int indexFormat = dr.GetOrdinal("Format");
            if (dr["Format"] != DBNull.Value)
            {
                stock.Format = Convert.ToString(dr[indexFormat]);
            }

            int indexOriginPlaceId = dr.GetOrdinal("OriginPlaceId");
            if (dr["OriginPlaceId"] != DBNull.Value)
            {
                stock.OriginPlaceId = Convert.ToInt32(dr[indexOriginPlaceId]);
            }

            int indexOriginPlace = dr.GetOrdinal("OriginPlace");
            if (dr["OriginPlace"] != DBNull.Value)
            {
                stock.OriginPlace = Convert.ToString(dr[indexOriginPlace]);
            }

            int indexPreStatus = dr.GetOrdinal("PreStatus");
            if (dr["PreStatus"] != DBNull.Value)
            {
                stock.PreStatus = (StockStatusEnum)Convert.ToInt32(dr[indexPreStatus]);
            }

            int indexStockStatus = dr.GetOrdinal("StockStatus");
            if (dr["StockStatus"] != DBNull.Value)
            {
                stock.StockStatus = (StockStatusEnum)Convert.ToInt32(dr[indexStockStatus]);
            }

            int indexCardNo = dr.GetOrdinal("CardNo");
            if (dr["CardNo"] != DBNull.Value)
            {
                stock.CardNo = Convert.ToString(dr[indexCardNo]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                stock.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexStockType = dr.GetOrdinal("StockType");
            if (dr["StockType"] != DBNull.Value)
            {
                stock.StockType = Convert.ToInt32(dr[indexStockType]);
            }


            return stock;
        }

        public override string TableName
        {
            get
            {
                return "St_Stock";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Stock st_stock = (Stock)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stock.StockId;
            paras.Add(stockidpara);

            SqlParameter stocknameidpara = new SqlParameter("@StockNameId", SqlDbType.Int, 4);
            stocknameidpara.Value = st_stock.StockNameId;
            paras.Add(stocknameidpara);

            if (!string.IsNullOrEmpty(st_stock.StockNo))
            {
                SqlParameter stocknopara = new SqlParameter("@StockNo", SqlDbType.VarChar, 80);
                stocknopara.Value = st_stock.StockNo;
                paras.Add(stocknopara);
            }

            SqlParameter stockdatepara = new SqlParameter("@StockDate", SqlDbType.DateTime, 8);
            stockdatepara.Value = st_stock.StockDate;
            paras.Add(stockdatepara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = st_stock.AssetId;
            paras.Add(assetidpara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = st_stock.Bundles;
            paras.Add(bundlespara);

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = st_stock.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = st_stock.NetAmount;
            paras.Add(netamountpara);

            SqlParameter receiptingappara = new SqlParameter("@ReceiptInGap", SqlDbType.Decimal, 9);
            receiptingappara.Value = st_stock.ReceiptInGap;
            paras.Add(receiptingappara);

            SqlParameter receiptoutgappara = new SqlParameter("@ReceiptOutGap", SqlDbType.Decimal, 9);
            receiptoutgappara.Value = st_stock.ReceiptOutGap;
            paras.Add(receiptoutgappara);

            SqlParameter curgrossamountpara = new SqlParameter("@CurGrossAmount", SqlDbType.Decimal, 9);
            curgrossamountpara.Value = st_stock.CurGrossAmount;
            paras.Add(curgrossamountpara);

            SqlParameter curnetamountpara = new SqlParameter("@CurNetAmount", SqlDbType.Decimal, 9);
            curnetamountpara.Value = st_stock.CurNetAmount;
            paras.Add(curnetamountpara);

            SqlParameter uintidpara = new SqlParameter("@UintId", SqlDbType.Int, 4);
            uintidpara.Value = st_stock.UintId;
            paras.Add(uintidpara);

            SqlParameter deliverplaceidpara = new SqlParameter("@DeliverPlaceId", SqlDbType.Int, 4);
            deliverplaceidpara.Value = st_stock.DeliverPlaceId;
            paras.Add(deliverplaceidpara);

            SqlParameter brandidpara = new SqlParameter("@BrandId", SqlDbType.Int, 4);
            brandidpara.Value = st_stock.BrandId;
            paras.Add(brandidpara);

            SqlParameter customstypepara = new SqlParameter("@CustomsType", SqlDbType.Int, 4);
            customstypepara.Value = st_stock.CustomsType;
            paras.Add(customstypepara);

            SqlParameter groupidpara = new SqlParameter("@GroupId", SqlDbType.Int, 4);
            groupidpara.Value = st_stock.GroupId;
            paras.Add(groupidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = st_stock.CorpId;
            paras.Add(corpidpara);

            SqlParameter deptidpara = new SqlParameter("@DeptId", SqlDbType.Int, 4);
            deptidpara.Value = st_stock.DeptId;
            paras.Add(deptidpara);

            SqlParameter produceridpara = new SqlParameter("@ProducerId", SqlDbType.Int, 4);
            produceridpara.Value = st_stock.ProducerId;
            paras.Add(produceridpara);

            if (!string.IsNullOrEmpty(st_stock.PaperNo))
            {
                SqlParameter papernopara = new SqlParameter("@PaperNo", SqlDbType.VarChar, 80);
                papernopara.Value = st_stock.PaperNo;
                paras.Add(papernopara);
            }

            SqlParameter paperholderpara = new SqlParameter("@PaperHolder", SqlDbType.Int, 4);
            paperholderpara.Value = st_stock.PaperHolder;
            paras.Add(paperholderpara);

            if (!string.IsNullOrEmpty(st_stock.Format))
            {
                SqlParameter formatpara = new SqlParameter("@Format", SqlDbType.VarChar, 200);
                formatpara.Value = st_stock.Format;
                paras.Add(formatpara);
            }

            SqlParameter originplaceidpara = new SqlParameter("@OriginPlaceId", SqlDbType.Int, 4);
            originplaceidpara.Value = st_stock.OriginPlaceId;
            paras.Add(originplaceidpara);

            if (!string.IsNullOrEmpty(st_stock.OriginPlace))
            {
                SqlParameter originplacepara = new SqlParameter("@OriginPlace", SqlDbType.VarChar, 200);
                originplacepara.Value = st_stock.OriginPlace;
                paras.Add(originplacepara);
            }

            SqlParameter prestatuspara = new SqlParameter("@PreStatus", SqlDbType.Int, 4);
            prestatuspara.Value = st_stock.PreStatus;
            paras.Add(prestatuspara);

            SqlParameter stockstatuspara = new SqlParameter("@StockStatus", SqlDbType.Int, 4);
            stockstatuspara.Value = st_stock.StockStatus;
            paras.Add(stockstatuspara);

            if (!string.IsNullOrEmpty(st_stock.CardNo))
            {
                SqlParameter cardnopara = new SqlParameter("@CardNo", SqlDbType.VarChar, 200);
                cardnopara.Value = st_stock.CardNo;
                paras.Add(cardnopara);
            }

            if (!string.IsNullOrEmpty(st_stock.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = st_stock.Memo;
                paras.Add(memopara);
            }

            SqlParameter stocktypepara = new SqlParameter("@StockType", SqlDbType.Int, 4);
            stocktypepara.Value = st_stock.StockType;
            paras.Add(stocktypepara);


            return paras;
        }

        #endregion

        #region 新增方法

        /// <summary>
        /// 更新库存状态，并将历史状态更新在PreStatus字段上
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="stockStatus"></param>
        /// <returns></returns>
        public ResultModel UpdateStockStatus(Stock stock, StockStatusEnum stockStatus)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.St_Stock set PreStatus = StockStatus ,StockStatus = {0} where StockId = {1} ", (int)stockStatus, stock.StockId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);

                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "更新状态成功";
                    result.AffectCount = i;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "更新状态失败";
                    result.AffectCount = i;
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = string.Format("更新状态失败,{0}", e.Message);
            }

            return result;
        }

        //public ResultModel UpdateStockStatus(Stock stock, StockStatusEnum stockStatus, StockStatusEnum defaultStatus)
        //{
        //    ResultModel result = new ResultModel();

        //    try
        //    {
        //        string sql = string.Format("update dbo.St_Stock set PreStatus = StockStatus ,StockStatus = {0} where StockId = {1} ", (int)stockStatus, stock.StockId);
        //        int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);

        //        if (i > 0)
        //        {
        //            result.ResultStatus = 0;
        //            result.Message = "更新状态成功";
        //            result.AffectCount = i;
        //        }
        //        else
        //        {
        //            result.ResultStatus = -1;
        //            result.Message = "更新状态失败";
        //            result.AffectCount = i;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        result.ResultStatus = -1;
        //        result.Message = string.Format("更新状态失败,{0}", e.Message);
        //    }

        //    return result;
        //}

        /// <summary>
        /// 直接更新库存状态
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="stockStatus"></param>
        /// <returns></returns>
        public ResultModel UpdateStockStatusDirect(Stock stock, StockStatusEnum stockStatus)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.St_Stock set StockStatus = {0} where StockId = {1} ", (int)stockStatus, stock.StockId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);

                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "更新状态成功";
                    result.AffectCount = i;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "更新状态失败";
                    result.AffectCount = i;
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = string.Format("更新状态失败,{0}", e.Message);
            }

            return result;
        }

        public ResultModel UpdateStockDP(int stockId, int dpId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.St_Stock set DeliverPlaceId = {0} where StockId = {1}", dpId, stockId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);

                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "更新交货地成功";
                    result.AffectCount = i;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "更新交货地失败";
                    result.AffectCount = i;
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = string.Format("更新状态失败,{0}", e.Message);
            }

            return result;
        }

        /// <summary>
        /// 更新库存状态至前一状态
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stock"></param>
        /// <returns></returns>
        public ResultModel UpdateStockStatusToPrevious(UserModel user, Stock stock)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update NFMT.dbo.St_Stock set StockStatus = PreStatus where StockId = {0}", stock.StockId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "更新成功";
                    result.AffectCount = i;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "更新失败";
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
        /// 更新库存状态至前一状态，若前一状态为空，则更新至第三参数
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stock"></param>
        /// <param name="stockStatusEnum"></param>
        /// <returns></returns>
        public ResultModel UpdateStockStatusToPrevious(UserModel user, Stock stock, StockStatusEnum stockStatusEnum)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update NFMT.dbo.St_Stock set StockStatus = ISNULL(PreStatus,{0}) where StockId = {1}", (int)stockStatusEnum, stock.StockId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "更新成功";
                    result.AffectCount = i;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "更新失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        public ResultModel GetStockContractOutCorp(UserModel user, int stockId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select corpdetail.CorpId,c.CorpName ");
                sb.Append("from dbo.Con_ContractSub sub  ");
                sb.Append("left join dbo.Con_ContractCorporationDetail corpdetail on sub.ContractId = corpdetail.ContractId ");
                sb.Append("left join NFMT_User.dbo.Corporation c on corpdetail.CorpId = c.CorpId ");
                sb.AppendFormat("where corpdetail.IsInnerCorp = 0 and sub.SubId = (select top 1 SubContractId from dbo.St_StockLog where StockId = {0})", stockId);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.Message = "获取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                }
                else
                {
                    result.Message = "获取失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        public ResultModel GetCurrencyId(UserModel user, int stockId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select sub.SettleCurrency from St_StockLog sl  ");
                sb.Append("left join dbo.Con_ContractSub sub on sl.SubContractId = sub.SubId  ");
                sb.AppendFormat("where sl.StockId = {0}", stockId);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sb.ToString(), null);
                int i;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && int.TryParse(obj.ToString(), out i))
                {
                    result.Message = "获取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = i;
                }
                else
                {
                    result.Message = "获取失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        public override IAuthority Authority
        {
            get
            {
                NFMT.Authority.StockAuth auth = new NFMT.Authority.StockAuth();
                auth.AuthColumnNames.Add("sto.StockId");
                return auth;
            }
        }

        public override int MenuId
        {
            get
            {
                return 51;
            }
        }

        public override ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate)
        {
            if (operate == OperateEnum.修改)
            {
                ResultModel result = new ResultModel();
                result.ResultStatus = 0;
                return result;
            }

            return base.AllowOperate(user, obj, operate);
        }

        #endregion
    }
}
