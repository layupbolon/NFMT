/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BusinessInvoiceDetailDAL.cs
// 文件功能描述：业务发票明细dbo.Inv_BusinessInvoiceDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年3月26日
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
    /// 业务发票明细dbo.Inv_BusinessInvoiceDetail数据交互类。
    /// </summary>
    public partial class BusinessInvoiceDetailDAL : ExecOperate, IBusinessInvoiceDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BusinessInvoiceDetailDAL()
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
            BusinessInvoiceDetail inv_businessinvoicedetail = (BusinessInvoiceDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter businessinvoiceidpara = new SqlParameter("@BusinessInvoiceId", SqlDbType.Int, 4);
            businessinvoiceidpara.Value = inv_businessinvoicedetail.BusinessInvoiceId;
            paras.Add(businessinvoiceidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = inv_businessinvoicedetail.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter refdetailidpara = new SqlParameter("@RefDetailId", SqlDbType.Int, 4);
            refdetailidpara.Value = inv_businessinvoicedetail.RefDetailId;
            paras.Add(refdetailidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = inv_businessinvoicedetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = inv_businessinvoicedetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter confirmpriceidpara = new SqlParameter("@ConfirmPriceId", SqlDbType.Int, 4);
            confirmpriceidpara.Value = inv_businessinvoicedetail.ConfirmPriceId;
            paras.Add(confirmpriceidpara);

            SqlParameter confirmdetailidpara = new SqlParameter("@ConfirmDetailId", SqlDbType.Int, 4);
            confirmdetailidpara.Value = inv_businessinvoicedetail.ConfirmDetailId;
            paras.Add(confirmdetailidpara);

            SqlParameter pricingidpara = new SqlParameter("@PricingId", SqlDbType.Int, 4);
            pricingidpara.Value = inv_businessinvoicedetail.PricingId;
            paras.Add(pricingidpara);

            SqlParameter pricingdetailidpara = new SqlParameter("@PricingDetailId", SqlDbType.Int, 4);
            pricingdetailidpara.Value = inv_businessinvoicedetail.PricingDetailId;
            paras.Add(pricingdetailidpara);

            SqlParameter feetypepara = new SqlParameter("@FeeType", SqlDbType.Int, 4);
            feetypepara.Value = inv_businessinvoicedetail.FeeType;
            paras.Add(feetypepara);

            SqlParameter integeramountpara = new SqlParameter("@IntegerAmount", SqlDbType.Decimal, 9);
            integeramountpara.Value = inv_businessinvoicedetail.IntegerAmount;
            paras.Add(integeramountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = inv_businessinvoicedetail.NetAmount;
            paras.Add(netamountpara);

            SqlParameter unitpricepara = new SqlParameter("@UnitPrice", SqlDbType.Decimal, 9);
            unitpricepara.Value = inv_businessinvoicedetail.UnitPrice;
            paras.Add(unitpricepara);

            SqlParameter calculatedaypara = new SqlParameter("@CalculateDay", SqlDbType.Decimal, 9);
            calculatedaypara.Value = inv_businessinvoicedetail.CalculateDay;
            paras.Add(calculatedaypara);

            SqlParameter balapara = new SqlParameter("@Bala", SqlDbType.Decimal, 9);
            balapara.Value = inv_businessinvoicedetail.Bala;
            paras.Add(balapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = inv_businessinvoicedetail.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            BusinessInvoiceDetail businessinvoicedetail = new BusinessInvoiceDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            businessinvoicedetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexBusinessInvoiceId = dr.GetOrdinal("BusinessInvoiceId");
            if (dr["BusinessInvoiceId"] != DBNull.Value)
            {
                businessinvoicedetail.BusinessInvoiceId = Convert.ToInt32(dr[indexBusinessInvoiceId]);
            }

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            if (dr["InvoiceId"] != DBNull.Value)
            {
                businessinvoicedetail.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
            }

            int indexRefDetailId = dr.GetOrdinal("RefDetailId");
            if (dr["RefDetailId"] != DBNull.Value)
            {
                businessinvoicedetail.RefDetailId = Convert.ToInt32(dr[indexRefDetailId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                businessinvoicedetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                businessinvoicedetail.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexConfirmPriceId = dr.GetOrdinal("ConfirmPriceId");
            if (dr["ConfirmPriceId"] != DBNull.Value)
            {
                businessinvoicedetail.ConfirmPriceId = Convert.ToInt32(dr[indexConfirmPriceId]);
            }

            int indexConfirmDetailId = dr.GetOrdinal("ConfirmDetailId");
            if (dr["ConfirmDetailId"] != DBNull.Value)
            {
                businessinvoicedetail.ConfirmDetailId = Convert.ToInt32(dr[indexConfirmDetailId]);
            }

            int indexPricingId = dr.GetOrdinal("PricingId");
            if (dr["PricingId"] != DBNull.Value)
            {
                businessinvoicedetail.PricingId = Convert.ToInt32(dr[indexPricingId]);
            }

            int indexPricingDetailId = dr.GetOrdinal("PricingDetailId");
            if (dr["PricingDetailId"] != DBNull.Value)
            {
                businessinvoicedetail.PricingDetailId = Convert.ToInt32(dr[indexPricingDetailId]);
            }

            int indexFeeType = dr.GetOrdinal("FeeType");
            if (dr["FeeType"] != DBNull.Value)
            {
                businessinvoicedetail.FeeType = Convert.ToInt32(dr[indexFeeType]);
            }

            int indexIntegerAmount = dr.GetOrdinal("IntegerAmount");
            if (dr["IntegerAmount"] != DBNull.Value)
            {
                businessinvoicedetail.IntegerAmount = Convert.ToDecimal(dr[indexIntegerAmount]);
            }

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            if (dr["NetAmount"] != DBNull.Value)
            {
                businessinvoicedetail.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
            }

            int indexUnitPrice = dr.GetOrdinal("UnitPrice");
            if (dr["UnitPrice"] != DBNull.Value)
            {
                businessinvoicedetail.UnitPrice = Convert.ToDecimal(dr[indexUnitPrice]);
            }

            int indexCalculateDay = dr.GetOrdinal("CalculateDay");
            if (dr["CalculateDay"] != DBNull.Value)
            {
                businessinvoicedetail.CalculateDay = Convert.ToDecimal(dr[indexCalculateDay]);
            }

            int indexBala = dr.GetOrdinal("Bala");
            if (dr["Bala"] != DBNull.Value)
            {
                businessinvoicedetail.Bala = Convert.ToDecimal(dr[indexBala]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                businessinvoicedetail.DetailStatus = (StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }


            return businessinvoicedetail;
        }

        public override string TableName
        {
            get
            {
                return "Inv_BusinessInvoiceDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            BusinessInvoiceDetail inv_businessinvoicedetail = (BusinessInvoiceDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = inv_businessinvoicedetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter businessinvoiceidpara = new SqlParameter("@BusinessInvoiceId", SqlDbType.Int, 4);
            businessinvoiceidpara.Value = inv_businessinvoicedetail.BusinessInvoiceId;
            paras.Add(businessinvoiceidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = inv_businessinvoicedetail.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter refdetailidpara = new SqlParameter("@RefDetailId", SqlDbType.Int, 4);
            refdetailidpara.Value = inv_businessinvoicedetail.RefDetailId;
            paras.Add(refdetailidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = inv_businessinvoicedetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = inv_businessinvoicedetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter confirmpriceidpara = new SqlParameter("@ConfirmPriceId", SqlDbType.Int, 4);
            confirmpriceidpara.Value = inv_businessinvoicedetail.ConfirmPriceId;
            paras.Add(confirmpriceidpara);

            SqlParameter confirmdetailidpara = new SqlParameter("@ConfirmDetailId", SqlDbType.Int, 4);
            confirmdetailidpara.Value = inv_businessinvoicedetail.ConfirmDetailId;
            paras.Add(confirmdetailidpara);

            SqlParameter pricingidpara = new SqlParameter("@PricingId", SqlDbType.Int, 4);
            pricingidpara.Value = inv_businessinvoicedetail.PricingId;
            paras.Add(pricingidpara);

            SqlParameter pricingdetailidpara = new SqlParameter("@PricingDetailId", SqlDbType.Int, 4);
            pricingdetailidpara.Value = inv_businessinvoicedetail.PricingDetailId;
            paras.Add(pricingdetailidpara);

            SqlParameter feetypepara = new SqlParameter("@FeeType", SqlDbType.Int, 4);
            feetypepara.Value = inv_businessinvoicedetail.FeeType;
            paras.Add(feetypepara);

            SqlParameter integeramountpara = new SqlParameter("@IntegerAmount", SqlDbType.Decimal, 9);
            integeramountpara.Value = inv_businessinvoicedetail.IntegerAmount;
            paras.Add(integeramountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = inv_businessinvoicedetail.NetAmount;
            paras.Add(netamountpara);

            SqlParameter unitpricepara = new SqlParameter("@UnitPrice", SqlDbType.Decimal, 9);
            unitpricepara.Value = inv_businessinvoicedetail.UnitPrice;
            paras.Add(unitpricepara);

            SqlParameter calculatedaypara = new SqlParameter("@CalculateDay", SqlDbType.Decimal, 9);
            calculatedaypara.Value = inv_businessinvoicedetail.CalculateDay;
            paras.Add(calculatedaypara);

            SqlParameter balapara = new SqlParameter("@Bala", SqlDbType.Decimal, 9);
            balapara.Value = inv_businessinvoicedetail.Bala;
            paras.Add(balapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = inv_businessinvoicedetail.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        #endregion
    }
}
