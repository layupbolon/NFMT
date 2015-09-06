/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceApplyDetailDAL.cs
// 文件功能描述：开票申请明细dbo.Inv_InvoiceApplyDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年1月28日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Invoice.Model;
using NFMT.DBUtility;
using NFMT.Invoice.IDAL;
using NFMT.Common;

namespace NFMT.Invoice.DAL
{
    /// <summary>
    /// 开票申请明细dbo.Inv_InvoiceApplyDetail数据交互类。
    /// </summary>
    public partial class InvoiceApplyDetailDAL : ApplyOperate, IInvoiceApplyDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InvoiceApplyDetailDAL()
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
            InvoiceApplyDetail inv_invoiceapplydetail = (InvoiceApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter invoiceapplyidpara = new SqlParameter("@InvoiceApplyId", SqlDbType.Int, 4);
            invoiceapplyidpara.Value = inv_invoiceapplydetail.InvoiceApplyId;
            paras.Add(invoiceapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = inv_invoiceapplydetail.ApplyId;
            paras.Add(applyidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = inv_invoiceapplydetail.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter bussinessinvoiceidpara = new SqlParameter("@BussinessInvoiceId", SqlDbType.Int, 4);
            bussinessinvoiceidpara.Value = inv_invoiceapplydetail.BussinessInvoiceId;
            paras.Add(bussinessinvoiceidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = inv_invoiceapplydetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = inv_invoiceapplydetail.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = inv_invoiceapplydetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter invoicepricepara = new SqlParameter("@InvoicePrice", SqlDbType.Decimal, 9);
            invoicepricepara.Value = inv_invoiceapplydetail.InvoicePrice;
            paras.Add(invoicepricepara);

            SqlParameter paymentamountpara = new SqlParameter("@PaymentAmount", SqlDbType.Decimal, 9);
            paymentamountpara.Value = inv_invoiceapplydetail.PaymentAmount;
            paras.Add(paymentamountpara);

            SqlParameter interestamountpara = new SqlParameter("@InterestAmount", SqlDbType.Decimal, 9);
            interestamountpara.Value = inv_invoiceapplydetail.InterestAmount;
            paras.Add(interestamountpara);

            SqlParameter otheramountpara = new SqlParameter("@OtherAmount", SqlDbType.Decimal, 9);
            otheramountpara.Value = inv_invoiceapplydetail.OtherAmount;
            paras.Add(otheramountpara);

            SqlParameter invoicebalapara = new SqlParameter("@InvoiceBala", SqlDbType.Decimal, 9);
            invoicebalapara.Value = inv_invoiceapplydetail.InvoiceBala;
            paras.Add(invoicebalapara);

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
            InvoiceApplyDetail invoiceapplydetail = new InvoiceApplyDetail();

            invoiceapplydetail.DetailId = Convert.ToInt32(dr["DetailId"]);

            if (dr["InvoiceApplyId"] != DBNull.Value)
            {
                invoiceapplydetail.InvoiceApplyId = Convert.ToInt32(dr["InvoiceApplyId"]);
            }

            if (dr["ApplyId"] != DBNull.Value)
            {
                invoiceapplydetail.ApplyId = Convert.ToInt32(dr["ApplyId"]);
            }

            if (dr["InvoiceId"] != DBNull.Value)
            {
                invoiceapplydetail.InvoiceId = Convert.ToInt32(dr["InvoiceId"]);
            }

            if (dr["BussinessInvoiceId"] != DBNull.Value)
            {
                invoiceapplydetail.BussinessInvoiceId = Convert.ToInt32(dr["BussinessInvoiceId"]);
            }

            if (dr["ContractId"] != DBNull.Value)
            {
                invoiceapplydetail.ContractId = Convert.ToInt32(dr["ContractId"]);
            }

            if (dr["SubContractId"] != DBNull.Value)
            {
                invoiceapplydetail.SubContractId = Convert.ToInt32(dr["SubContractId"]);
            }

            if (dr["StockLogId"] != DBNull.Value)
            {
                invoiceapplydetail.StockLogId = Convert.ToInt32(dr["StockLogId"]);
            }

            if (dr["InvoicePrice"] != DBNull.Value)
            {
                invoiceapplydetail.InvoicePrice = Convert.ToDecimal(dr["InvoicePrice"]);
            }

            if (dr["PaymentAmount"] != DBNull.Value)
            {
                invoiceapplydetail.PaymentAmount = Convert.ToDecimal(dr["PaymentAmount"]);
            }

            if (dr["InterestAmount"] != DBNull.Value)
            {
                invoiceapplydetail.InterestAmount = Convert.ToDecimal(dr["InterestAmount"]);
            }

            if (dr["OtherAmount"] != DBNull.Value)
            {
                invoiceapplydetail.OtherAmount = Convert.ToDecimal(dr["OtherAmount"]);
            }

            if (dr["InvoiceBala"] != DBNull.Value)
            {
                invoiceapplydetail.InvoiceBala = Convert.ToDecimal(dr["InvoiceBala"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                invoiceapplydetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                invoiceapplydetail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                invoiceapplydetail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                invoiceapplydetail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                invoiceapplydetail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return invoiceapplydetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            InvoiceApplyDetail invoiceapplydetail = new InvoiceApplyDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            invoiceapplydetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexInvoiceApplyId = dr.GetOrdinal("InvoiceApplyId");
            if (dr["InvoiceApplyId"] != DBNull.Value)
            {
                invoiceapplydetail.InvoiceApplyId = Convert.ToInt32(dr[indexInvoiceApplyId]);
            }

            int indexApplyId = dr.GetOrdinal("ApplyId");
            if (dr["ApplyId"] != DBNull.Value)
            {
                invoiceapplydetail.ApplyId = Convert.ToInt32(dr[indexApplyId]);
            }

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            if (dr["InvoiceId"] != DBNull.Value)
            {
                invoiceapplydetail.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
            }

            int indexBussinessInvoiceId = dr.GetOrdinal("BussinessInvoiceId");
            if (dr["BussinessInvoiceId"] != DBNull.Value)
            {
                invoiceapplydetail.BussinessInvoiceId = Convert.ToInt32(dr[indexBussinessInvoiceId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                invoiceapplydetail.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexSubContractId = dr.GetOrdinal("SubContractId");
            if (dr["SubContractId"] != DBNull.Value)
            {
                invoiceapplydetail.SubContractId = Convert.ToInt32(dr[indexSubContractId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                invoiceapplydetail.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexInvoicePrice = dr.GetOrdinal("InvoicePrice");
            if (dr["InvoicePrice"] != DBNull.Value)
            {
                invoiceapplydetail.InvoicePrice = Convert.ToDecimal(dr[indexInvoicePrice]);
            }

            int indexPaymentAmount = dr.GetOrdinal("PaymentAmount");
            if (dr["PaymentAmount"] != DBNull.Value)
            {
                invoiceapplydetail.PaymentAmount = Convert.ToDecimal(dr[indexPaymentAmount]);
            }

            int indexInterestAmount = dr.GetOrdinal("InterestAmount");
            if (dr["InterestAmount"] != DBNull.Value)
            {
                invoiceapplydetail.InterestAmount = Convert.ToDecimal(dr[indexInterestAmount]);
            }

            int indexOtherAmount = dr.GetOrdinal("OtherAmount");
            if (dr["OtherAmount"] != DBNull.Value)
            {
                invoiceapplydetail.OtherAmount = Convert.ToDecimal(dr[indexOtherAmount]);
            }

            int indexInvoiceBala = dr.GetOrdinal("InvoiceBala");
            if (dr["InvoiceBala"] != DBNull.Value)
            {
                invoiceapplydetail.InvoiceBala = Convert.ToDecimal(dr[indexInvoiceBala]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                invoiceapplydetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                invoiceapplydetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                invoiceapplydetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                invoiceapplydetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                invoiceapplydetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return invoiceapplydetail;
        }

        public override string TableName
        {
            get
            {
                return "Inv_InvoiceApplyDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            InvoiceApplyDetail inv_invoiceapplydetail = (InvoiceApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = inv_invoiceapplydetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter invoiceapplyidpara = new SqlParameter("@InvoiceApplyId", SqlDbType.Int, 4);
            invoiceapplyidpara.Value = inv_invoiceapplydetail.InvoiceApplyId;
            paras.Add(invoiceapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = inv_invoiceapplydetail.ApplyId;
            paras.Add(applyidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = inv_invoiceapplydetail.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter bussinessinvoiceidpara = new SqlParameter("@BussinessInvoiceId", SqlDbType.Int, 4);
            bussinessinvoiceidpara.Value = inv_invoiceapplydetail.BussinessInvoiceId;
            paras.Add(bussinessinvoiceidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = inv_invoiceapplydetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = inv_invoiceapplydetail.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = inv_invoiceapplydetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter invoicepricepara = new SqlParameter("@InvoicePrice", SqlDbType.Decimal, 9);
            invoicepricepara.Value = inv_invoiceapplydetail.InvoicePrice;
            paras.Add(invoicepricepara);

            SqlParameter paymentamountpara = new SqlParameter("@PaymentAmount", SqlDbType.Decimal, 9);
            paymentamountpara.Value = inv_invoiceapplydetail.PaymentAmount;
            paras.Add(paymentamountpara);

            SqlParameter interestamountpara = new SqlParameter("@InterestAmount", SqlDbType.Decimal, 9);
            interestamountpara.Value = inv_invoiceapplydetail.InterestAmount;
            paras.Add(interestamountpara);

            SqlParameter otheramountpara = new SqlParameter("@OtherAmount", SqlDbType.Decimal, 9);
            otheramountpara.Value = inv_invoiceapplydetail.OtherAmount;
            paras.Add(otheramountpara);

            SqlParameter invoicebalapara = new SqlParameter("@InvoiceBala", SqlDbType.Decimal, 9);
            invoicebalapara.Value = inv_invoiceapplydetail.InvoiceBala;
            paras.Add(invoicebalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = inv_invoiceapplydetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
