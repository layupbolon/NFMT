/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FinanceInvoiceDetailDAL.cs
// 文件功能描述：财务发票明细dbo.Inv_FinanceInvoiceDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年8月25日
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
    /// 财务发票明细dbo.Inv_FinanceInvoiceDetail数据交互类。
    /// </summary>
    public class FinanceInvoiceDetailDAL : ExecOperate, IFinanceInvoiceDetailDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public FinanceInvoiceDetailDAL()
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
            FinanceInvoiceDetail inv_financeinvoicedetail = (FinanceInvoiceDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter financeinvoiceidpara = new SqlParameter("@FinanceInvoiceId", SqlDbType.Int, 4);
            financeinvoiceidpara.Value = inv_financeinvoicedetail.FinanceInvoiceId;
            paras.Add(financeinvoiceidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = inv_financeinvoicedetail.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = inv_financeinvoicedetail.AssetId;
            paras.Add(assetidpara);

            SqlParameter integeramountpara = new SqlParameter("@IntegerAmount", SqlDbType.Decimal, 9);
            integeramountpara.Value = inv_financeinvoicedetail.IntegerAmount;
            paras.Add(integeramountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = inv_financeinvoicedetail.NetAmount;
            paras.Add(netamountpara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = inv_financeinvoicedetail.MUId;
            paras.Add(muidpara);

            SqlParameter vatratiopara = new SqlParameter("@VATRatio", SqlDbType.Decimal, 9);
            vatratiopara.Value = inv_financeinvoicedetail.VATRatio;
            paras.Add(vatratiopara);

            SqlParameter vatbalapara = new SqlParameter("@VATBala", SqlDbType.Decimal, 9);
            vatbalapara.Value = inv_financeinvoicedetail.VATBala;
            paras.Add(vatbalapara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            FinanceInvoiceDetail financeinvoicedetail = new FinanceInvoiceDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            financeinvoicedetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexFinanceInvoiceId = dr.GetOrdinal("FinanceInvoiceId");
            if (dr["FinanceInvoiceId"] != DBNull.Value)
            {
                financeinvoicedetail.FinanceInvoiceId = Convert.ToInt32(dr[indexFinanceInvoiceId]);
            }

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            if (dr["InvoiceId"] != DBNull.Value)
            {
                financeinvoicedetail.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
            }

            int indexAssetId = dr.GetOrdinal("AssetId");
            if (dr["AssetId"] != DBNull.Value)
            {
                financeinvoicedetail.AssetId = Convert.ToInt32(dr[indexAssetId]);
            }

            int indexIntegerAmount = dr.GetOrdinal("IntegerAmount");
            if (dr["IntegerAmount"] != DBNull.Value)
            {
                financeinvoicedetail.IntegerAmount = Convert.ToDecimal(dr[indexIntegerAmount]);
            }

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            if (dr["NetAmount"] != DBNull.Value)
            {
                financeinvoicedetail.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
            }

            int indexMUId = dr.GetOrdinal("MUId");
            if (dr["MUId"] != DBNull.Value)
            {
                financeinvoicedetail.MUId = Convert.ToInt32(dr[indexMUId]);
            }

            int indexVATRatio = dr.GetOrdinal("VATRatio");
            if (dr["VATRatio"] != DBNull.Value)
            {
                financeinvoicedetail.VATRatio = Convert.ToDecimal(dr[indexVATRatio]);
            }

            int indexVATBala = dr.GetOrdinal("VATBala");
            if (dr["VATBala"] != DBNull.Value)
            {
                financeinvoicedetail.VATBala = Convert.ToDecimal(dr[indexVATBala]);
            }


            return financeinvoicedetail;
        }

        public override string TableName
        {
            get
            {
                return "Inv_FinanceInvoiceDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            FinanceInvoiceDetail inv_financeinvoicedetail = (FinanceInvoiceDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = inv_financeinvoicedetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter financeinvoiceidpara = new SqlParameter("@FinanceInvoiceId", SqlDbType.Int, 4);
            financeinvoiceidpara.Value = inv_financeinvoicedetail.FinanceInvoiceId;
            paras.Add(financeinvoiceidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = inv_financeinvoicedetail.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = inv_financeinvoicedetail.AssetId;
            paras.Add(assetidpara);

            SqlParameter integeramountpara = new SqlParameter("@IntegerAmount", SqlDbType.Decimal, 9);
            integeramountpara.Value = inv_financeinvoicedetail.IntegerAmount;
            paras.Add(integeramountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = inv_financeinvoicedetail.NetAmount;
            paras.Add(netamountpara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = inv_financeinvoicedetail.MUId;
            paras.Add(muidpara);

            SqlParameter vatratiopara = new SqlParameter("@VATRatio", SqlDbType.Decimal, 9);
            vatratiopara.Value = inv_financeinvoicedetail.VATRatio;
            paras.Add(vatratiopara);

            SqlParameter vatbalapara = new SqlParameter("@VATBala", SqlDbType.Decimal, 9);
            vatbalapara.Value = inv_financeinvoicedetail.VATBala;
            paras.Add(vatbalapara);


            return paras;
        }

        #endregion
    }
}
