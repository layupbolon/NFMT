/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PaymentStockDetailDAL.cs
// 文件功能描述：库存财务付款明细dbo.Fun_PaymentStockDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年12月23日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Funds.Model;
using NFMT.DBUtility;
using NFMT.Funds.IDAL;
using NFMT.Common;

namespace NFMT.Funds.DAL
{
    /// <summary>
    /// 库存财务付款明细dbo.Fun_PaymentStockDetail数据交互类。
    /// </summary>
    public partial class PaymentStockDetailDAL : DetailOperate, IPaymentStockDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PaymentStockDetailDAL()
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
            PaymentStockDetail fun_paymentstockdetail = (PaymentStockDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter contractdetailidpara = new SqlParameter("@ContractDetailId", SqlDbType.Int, 4);
            contractdetailidpara.Value = fun_paymentstockdetail.ContractDetailId;
            paras.Add(contractdetailidpara);

            SqlParameter paymentidpara = new SqlParameter("@PaymentId", SqlDbType.Int, 4);
            paymentidpara.Value = fun_paymentstockdetail.PaymentId;
            paras.Add(paymentidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = fun_paymentstockdetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = fun_paymentstockdetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = fun_paymentstockdetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = fun_paymentstockdetail.SubId;
            paras.Add(subidpara);

            SqlParameter payapplyidpara = new SqlParameter("@PayApplyId", SqlDbType.Int, 4);
            payapplyidpara.Value = fun_paymentstockdetail.PayApplyId;
            paras.Add(payapplyidpara);

            SqlParameter payapplydetailidpara = new SqlParameter("@PayApplyDetailId", SqlDbType.Int, 4);
            payapplydetailidpara.Value = fun_paymentstockdetail.PayApplyDetailId;
            paras.Add(payapplydetailidpara);

            SqlParameter paybalapara = new SqlParameter("@PayBala", SqlDbType.Decimal, 9);
            paybalapara.Value = fun_paymentstockdetail.PayBala;
            paras.Add(paybalapara);

            SqlParameter fundsbalapara = new SqlParameter("@FundsBala", SqlDbType.Decimal, 9);
            fundsbalapara.Value = fun_paymentstockdetail.FundsBala;
            paras.Add(fundsbalapara);

            SqlParameter virtualbalapara = new SqlParameter("@VirtualBala", SqlDbType.Decimal, 9);
            virtualbalapara.Value = fun_paymentstockdetail.VirtualBala;
            paras.Add(virtualbalapara);

            SqlParameter sourcefrompara = new SqlParameter("@SourceFrom", SqlDbType.Int, 4);
            sourcefrompara.Value = fun_paymentstockdetail.SourceFrom;
            paras.Add(sourcefrompara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            PaymentStockDetail paymentstockdetail = new PaymentStockDetail();

            paymentstockdetail.DetailId = Convert.ToInt32(dr["DetailId"]);

            if (dr["ContractDetailId"] != DBNull.Value)
            {
                paymentstockdetail.ContractDetailId = Convert.ToInt32(dr["ContractDetailId"]);
            }

            paymentstockdetail.PaymentId = Convert.ToInt32(dr["PaymentId"]);

            if (dr["StockId"] != DBNull.Value)
            {
                paymentstockdetail.StockId = Convert.ToInt32(dr["StockId"]);
            }

            if (dr["StockLogId"] != DBNull.Value)
            {
                paymentstockdetail.StockLogId = Convert.ToInt32(dr["StockLogId"]);
            }

            if (dr["ContractId"] != DBNull.Value)
            {
                paymentstockdetail.ContractId = Convert.ToInt32(dr["ContractId"]);
            }

            if (dr["SubId"] != DBNull.Value)
            {
                paymentstockdetail.SubId = Convert.ToInt32(dr["SubId"]);
            }

            if (dr["PayApplyId"] != DBNull.Value)
            {
                paymentstockdetail.PayApplyId = Convert.ToInt32(dr["PayApplyId"]);
            }

            if (dr["PayApplyDetailId"] != DBNull.Value)
            {
                paymentstockdetail.PayApplyDetailId = Convert.ToInt32(dr["PayApplyDetailId"]);
            }

            if (dr["PayBala"] != DBNull.Value)
            {
                paymentstockdetail.PayBala = Convert.ToDecimal(dr["PayBala"]);
            }

            if (dr["FundsBala"] != DBNull.Value)
            {
                paymentstockdetail.FundsBala = Convert.ToDecimal(dr["FundsBala"]);
            }

            if (dr["VirtualBala"] != DBNull.Value)
            {
                paymentstockdetail.VirtualBala = Convert.ToDecimal(dr["VirtualBala"]);
            }

            if (dr["SourceFrom"] != DBNull.Value)
            {
                paymentstockdetail.SourceFrom = Convert.ToInt32(dr["SourceFrom"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                paymentstockdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }


            return paymentstockdetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PaymentStockDetail paymentstockdetail = new PaymentStockDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            paymentstockdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexContractDetailId = dr.GetOrdinal("ContractDetailId");
            if (dr["ContractDetailId"] != DBNull.Value)
            {
                paymentstockdetail.ContractDetailId = Convert.ToInt32(dr[indexContractDetailId]);
            }

            int indexPaymentId = dr.GetOrdinal("PaymentId");
            paymentstockdetail.PaymentId = Convert.ToInt32(dr[indexPaymentId]);

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                paymentstockdetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                paymentstockdetail.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                paymentstockdetail.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexSubId = dr.GetOrdinal("SubId");
            if (dr["SubId"] != DBNull.Value)
            {
                paymentstockdetail.SubId = Convert.ToInt32(dr[indexSubId]);
            }

            int indexPayApplyId = dr.GetOrdinal("PayApplyId");
            if (dr["PayApplyId"] != DBNull.Value)
            {
                paymentstockdetail.PayApplyId = Convert.ToInt32(dr[indexPayApplyId]);
            }

            int indexPayApplyDetailId = dr.GetOrdinal("PayApplyDetailId");
            if (dr["PayApplyDetailId"] != DBNull.Value)
            {
                paymentstockdetail.PayApplyDetailId = Convert.ToInt32(dr[indexPayApplyDetailId]);
            }

            int indexPayBala = dr.GetOrdinal("PayBala");
            if (dr["PayBala"] != DBNull.Value)
            {
                paymentstockdetail.PayBala = Convert.ToDecimal(dr[indexPayBala]);
            }

            int indexFundsBala = dr.GetOrdinal("FundsBala");
            if (dr["FundsBala"] != DBNull.Value)
            {
                paymentstockdetail.FundsBala = Convert.ToDecimal(dr[indexFundsBala]);
            }

            int indexVirtualBala = dr.GetOrdinal("VirtualBala");
            if (dr["VirtualBala"] != DBNull.Value)
            {
                paymentstockdetail.VirtualBala = Convert.ToDecimal(dr[indexVirtualBala]);
            }

            int indexSourceFrom = dr.GetOrdinal("SourceFrom");
            if (dr["SourceFrom"] != DBNull.Value)
            {
                paymentstockdetail.SourceFrom = Convert.ToInt32(dr[indexSourceFrom]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                paymentstockdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }


            return paymentstockdetail;
        }

        public override string TableName
        {
            get
            {
                return "Fun_PaymentStockDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PaymentStockDetail fun_paymentstockdetail = (PaymentStockDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = fun_paymentstockdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter contractdetailidpara = new SqlParameter("@ContractDetailId", SqlDbType.Int, 4);
            contractdetailidpara.Value = fun_paymentstockdetail.ContractDetailId;
            paras.Add(contractdetailidpara);

            SqlParameter paymentidpara = new SqlParameter("@PaymentId", SqlDbType.Int, 4);
            paymentidpara.Value = fun_paymentstockdetail.PaymentId;
            paras.Add(paymentidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = fun_paymentstockdetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = fun_paymentstockdetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = fun_paymentstockdetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = fun_paymentstockdetail.SubId;
            paras.Add(subidpara);

            SqlParameter payapplyidpara = new SqlParameter("@PayApplyId", SqlDbType.Int, 4);
            payapplyidpara.Value = fun_paymentstockdetail.PayApplyId;
            paras.Add(payapplyidpara);

            SqlParameter payapplydetailidpara = new SqlParameter("@PayApplyDetailId", SqlDbType.Int, 4);
            payapplydetailidpara.Value = fun_paymentstockdetail.PayApplyDetailId;
            paras.Add(payapplydetailidpara);

            SqlParameter paybalapara = new SqlParameter("@PayBala", SqlDbType.Decimal, 9);
            paybalapara.Value = fun_paymentstockdetail.PayBala;
            paras.Add(paybalapara);

            SqlParameter fundsbalapara = new SqlParameter("@FundsBala", SqlDbType.Decimal, 9);
            fundsbalapara.Value = fun_paymentstockdetail.FundsBala;
            paras.Add(fundsbalapara);

            SqlParameter virtualbalapara = new SqlParameter("@VirtualBala", SqlDbType.Decimal, 9);
            virtualbalapara.Value = fun_paymentstockdetail.VirtualBala;
            paras.Add(virtualbalapara);

            SqlParameter sourcefrompara = new SqlParameter("@SourceFrom", SqlDbType.Int, 4);
            sourcefrompara.Value = fun_paymentstockdetail.SourceFrom;
            paras.Add(sourcefrompara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = fun_paymentstockdetail.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        #endregion

        
    }
}
