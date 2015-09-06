/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockReceiptDAL.cs
// 文件功能描述：仓库库存净重确认回执，磅差回执dbo.St_StockReceipt数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月3日
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
    /// 仓库库存净重确认回执，磅差回执dbo.St_StockReceipt数据交互类。
    /// </summary>
    public class StockReceiptDAL : ExecOperate, IStockReceiptDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockReceiptDAL()
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
            StockReceipt st_stockreceipt = (StockReceipt)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ReceiptId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = st_stockreceipt.ContractId;
            paras.Add(contractidpara);

            SqlParameter contractsubidpara = new SqlParameter("@ContractSubId", SqlDbType.Int, 4);
            contractsubidpara.Value = st_stockreceipt.ContractSubId;
            paras.Add(contractsubidpara);

            SqlParameter prenetamountpara = new SqlParameter("@PreNetAmount", SqlDbType.Decimal, 9);
            prenetamountpara.Value = st_stockreceipt.PreNetAmount;
            paras.Add(prenetamountpara);

            SqlParameter receiptamountpara = new SqlParameter("@ReceiptAmount", SqlDbType.Decimal, 9);
            receiptamountpara.Value = st_stockreceipt.ReceiptAmount;
            paras.Add(receiptamountpara);

            SqlParameter unitidpara = new SqlParameter("@UnitId", SqlDbType.Int, 4);
            unitidpara.Value = st_stockreceipt.UnitId;
            paras.Add(unitidpara);

            SqlParameter qtymisspara = new SqlParameter("@QtyMiss", SqlDbType.Decimal, 9);
            qtymisspara.Value = st_stockreceipt.QtyMiss;
            paras.Add(qtymisspara);

            SqlParameter qtyratepara = new SqlParameter("@QtyRate", SqlDbType.Decimal, 9);
            qtyratepara.Value = st_stockreceipt.QtyRate;
            paras.Add(qtyratepara);

            SqlParameter receiptdatepara = new SqlParameter("@ReceiptDate", SqlDbType.DateTime, 8);
            receiptdatepara.Value = st_stockreceipt.ReceiptDate;
            paras.Add(receiptdatepara);

            SqlParameter receipterpara = new SqlParameter("@Receipter", SqlDbType.Int, 4);
            receipterpara.Value = st_stockreceipt.Receipter;
            paras.Add(receipterpara);

            if (!string.IsNullOrEmpty(st_stockreceipt.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = st_stockreceipt.Memo;
                paras.Add(memopara);
            }

            SqlParameter receipttypepara = new SqlParameter("@ReceiptType", SqlDbType.Int, 4);
            receipttypepara.Value = st_stockreceipt.ReceiptType;
            paras.Add(receipttypepara);

            SqlParameter receiptstatuspara = new SqlParameter("@ReceiptStatus", SqlDbType.Int, 4);
            receiptstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(receiptstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockReceipt stockreceipt = new StockReceipt();

            int indexReceiptId = dr.GetOrdinal("ReceiptId");
            stockreceipt.ReceiptId = Convert.ToInt32(dr[indexReceiptId]);

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                stockreceipt.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexContractSubId = dr.GetOrdinal("ContractSubId");
            if (dr["ContractSubId"] != DBNull.Value)
            {
                stockreceipt.ContractSubId = Convert.ToInt32(dr[indexContractSubId]);
            }

            int indexPreNetAmount = dr.GetOrdinal("PreNetAmount");
            if (dr["PreNetAmount"] != DBNull.Value)
            {
                stockreceipt.PreNetAmount = Convert.ToDecimal(dr[indexPreNetAmount]);
            }

            int indexReceiptAmount = dr.GetOrdinal("ReceiptAmount");
            if (dr["ReceiptAmount"] != DBNull.Value)
            {
                stockreceipt.ReceiptAmount = Convert.ToDecimal(dr[indexReceiptAmount]);
            }

            int indexUnitId = dr.GetOrdinal("UnitId");
            if (dr["UnitId"] != DBNull.Value)
            {
                stockreceipt.UnitId = Convert.ToInt32(dr[indexUnitId]);
            }

            int indexQtyMiss = dr.GetOrdinal("QtyMiss");
            if (dr["QtyMiss"] != DBNull.Value)
            {
                stockreceipt.QtyMiss = Convert.ToDecimal(dr[indexQtyMiss]);
            }

            int indexQtyRate = dr.GetOrdinal("QtyRate");
            if (dr["QtyRate"] != DBNull.Value)
            {
                stockreceipt.QtyRate = Convert.ToDecimal(dr[indexQtyRate]);
            }

            int indexReceiptDate = dr.GetOrdinal("ReceiptDate");
            if (dr["ReceiptDate"] != DBNull.Value)
            {
                stockreceipt.ReceiptDate = Convert.ToDateTime(dr[indexReceiptDate]);
            }

            int indexReceipter = dr.GetOrdinal("Receipter");
            if (dr["Receipter"] != DBNull.Value)
            {
                stockreceipt.Receipter = Convert.ToInt32(dr[indexReceipter]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                stockreceipt.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexReceiptType = dr.GetOrdinal("ReceiptType");
            if (dr["ReceiptType"] != DBNull.Value)
            {
                stockreceipt.ReceiptType = Convert.ToInt32(dr[indexReceiptType]);
            }

            int indexReceiptStatus = dr.GetOrdinal("ReceiptStatus");
            if (dr["ReceiptStatus"] != DBNull.Value)
            {
                stockreceipt.ReceiptStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexReceiptStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                stockreceipt.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                stockreceipt.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                stockreceipt.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                stockreceipt.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return stockreceipt;
        }

        public override string TableName
        {
            get
            {
                return "St_StockReceipt";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockReceipt st_stockreceipt = (StockReceipt)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter receiptidpara = new SqlParameter("@ReceiptId", SqlDbType.Int, 4);
            receiptidpara.Value = st_stockreceipt.ReceiptId;
            paras.Add(receiptidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = st_stockreceipt.ContractId;
            paras.Add(contractidpara);

            SqlParameter contractsubidpara = new SqlParameter("@ContractSubId", SqlDbType.Int, 4);
            contractsubidpara.Value = st_stockreceipt.ContractSubId;
            paras.Add(contractsubidpara);

            SqlParameter prenetamountpara = new SqlParameter("@PreNetAmount", SqlDbType.Decimal, 9);
            prenetamountpara.Value = st_stockreceipt.PreNetAmount;
            paras.Add(prenetamountpara);

            SqlParameter receiptamountpara = new SqlParameter("@ReceiptAmount", SqlDbType.Decimal, 9);
            receiptamountpara.Value = st_stockreceipt.ReceiptAmount;
            paras.Add(receiptamountpara);

            SqlParameter unitidpara = new SqlParameter("@UnitId", SqlDbType.Int, 4);
            unitidpara.Value = st_stockreceipt.UnitId;
            paras.Add(unitidpara);

            SqlParameter qtymisspara = new SqlParameter("@QtyMiss", SqlDbType.Decimal, 9);
            qtymisspara.Value = st_stockreceipt.QtyMiss;
            paras.Add(qtymisspara);

            SqlParameter qtyratepara = new SqlParameter("@QtyRate", SqlDbType.Decimal, 9);
            qtyratepara.Value = st_stockreceipt.QtyRate;
            paras.Add(qtyratepara);

            SqlParameter receiptdatepara = new SqlParameter("@ReceiptDate", SqlDbType.DateTime, 8);
            receiptdatepara.Value = st_stockreceipt.ReceiptDate;
            paras.Add(receiptdatepara);

            SqlParameter receipterpara = new SqlParameter("@Receipter", SqlDbType.Int, 4);
            receipterpara.Value = st_stockreceipt.Receipter;
            paras.Add(receipterpara);

            if (!string.IsNullOrEmpty(st_stockreceipt.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = st_stockreceipt.Memo;
                paras.Add(memopara);
            }

            SqlParameter receipttypepara = new SqlParameter("@ReceiptType", SqlDbType.Int, 4);
            receipttypepara.Value = st_stockreceipt.ReceiptType;
            paras.Add(receipttypepara);

            SqlParameter receiptstatuspara = new SqlParameter("@ReceiptStatus", SqlDbType.Int, 4);
            receiptstatuspara.Value = st_stockreceipt.ReceiptStatus;
            paras.Add(receiptstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public override int MenuId
        {
            get
            {
                return 88;
            }
        }

        public ResultModel UpdateBussinessInvDetail(UserModel user, int stockLogId, decimal receiptAmount)
        {
            ResultModel result = new ResultModel();

            try
            {
                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter stockLogIdpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
                stockLogIdpara.Value = stockLogId;
                paras.Add(stockLogIdpara);

                SqlParameter receiptAmountpara = new SqlParameter("@ReceiptAmount", SqlDbType.Decimal, 18);
                receiptAmountpara.Value = receiptAmount;
                paras.Add(receiptAmountpara);

                //由于WareHouse无法应用Invoice项目，故使用StoredProcedure  2015/7/23
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.StoredProcedure, "dbo.UpdateBussinessInvoiceNetAmount", paras.ToArray());
                result.ResultStatus = 0;
                
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        #endregion
    }
}
