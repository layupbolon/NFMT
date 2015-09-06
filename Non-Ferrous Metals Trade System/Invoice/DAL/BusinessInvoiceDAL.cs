/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BusinessInvoiceDAL.cs
// 文件功能描述：业务发票dbo.Inv_BusinessInvoice数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月10日
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
    /// 业务发票dbo.Inv_BusinessInvoice数据交互类。
    /// </summary>
    public partial class BusinessInvoiceDAL : ExecOperate, IBusinessInvoiceDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BusinessInvoiceDAL()
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
            BusinessInvoice inv_businessinvoice = (BusinessInvoice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@BusinessInvoiceId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = inv_businessinvoice.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter refinvoiceidpara = new SqlParameter("@RefInvoiceId", SqlDbType.Int, 4);
            refinvoiceidpara.Value = inv_businessinvoice.RefInvoiceId;
            paras.Add(refinvoiceidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = inv_businessinvoice.ContractId;
            paras.Add(contractidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = inv_businessinvoice.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = inv_businessinvoice.AssetId;
            paras.Add(assetidpara);

            SqlParameter integeramountpara = new SqlParameter("@IntegerAmount", SqlDbType.Decimal, 9);
            integeramountpara.Value = inv_businessinvoice.IntegerAmount;
            paras.Add(integeramountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = inv_businessinvoice.NetAmount;
            paras.Add(netamountpara);

            SqlParameter unitpricepara = new SqlParameter("@UnitPrice", SqlDbType.Decimal, 9);
            unitpricepara.Value = inv_businessinvoice.UnitPrice;
            paras.Add(unitpricepara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = inv_businessinvoice.MUId;
            paras.Add(muidpara);

            SqlParameter marginratiopara = new SqlParameter("@MarginRatio", SqlDbType.Decimal, 9);
            marginratiopara.Value = inv_businessinvoice.MarginRatio;
            paras.Add(marginratiopara);

            SqlParameter vatratiopara = new SqlParameter("@VATRatio", SqlDbType.Decimal, 9);
            vatratiopara.Value = inv_businessinvoice.VATRatio;
            paras.Add(vatratiopara);

            SqlParameter vatbalapara = new SqlParameter("@VATBala", SqlDbType.Decimal, 9);
            vatbalapara.Value = inv_businessinvoice.VATBala;
            paras.Add(vatbalapara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            BusinessInvoice businessinvoice = new BusinessInvoice();

            int indexBusinessInvoiceId = dr.GetOrdinal("BusinessInvoiceId");
            businessinvoice.BusinessInvoiceId = Convert.ToInt32(dr[indexBusinessInvoiceId]);

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            if (dr["InvoiceId"] != DBNull.Value)
            {
                businessinvoice.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
            }

            int indexRefInvoiceId = dr.GetOrdinal("RefInvoiceId");
            if (dr["RefInvoiceId"] != DBNull.Value)
            {
                businessinvoice.RefInvoiceId = Convert.ToInt32(dr[indexRefInvoiceId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                businessinvoice.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexSubContractId = dr.GetOrdinal("SubContractId");
            if (dr["SubContractId"] != DBNull.Value)
            {
                businessinvoice.SubContractId = Convert.ToInt32(dr[indexSubContractId]);
            }

            int indexAssetId = dr.GetOrdinal("AssetId");
            if (dr["AssetId"] != DBNull.Value)
            {
                businessinvoice.AssetId = Convert.ToInt32(dr[indexAssetId]);
            }

            int indexIntegerAmount = dr.GetOrdinal("IntegerAmount");
            if (dr["IntegerAmount"] != DBNull.Value)
            {
                businessinvoice.IntegerAmount = Convert.ToDecimal(dr[indexIntegerAmount]);
            }

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            if (dr["NetAmount"] != DBNull.Value)
            {
                businessinvoice.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
            }

            int indexUnitPrice = dr.GetOrdinal("UnitPrice");
            if (dr["UnitPrice"] != DBNull.Value)
            {
                businessinvoice.UnitPrice = Convert.ToDecimal(dr[indexUnitPrice]);
            }

            int indexMUId = dr.GetOrdinal("MUId");
            if (dr["MUId"] != DBNull.Value)
            {
                businessinvoice.MUId = Convert.ToInt32(dr[indexMUId]);
            }

            int indexMarginRatio = dr.GetOrdinal("MarginRatio");
            if (dr["MarginRatio"] != DBNull.Value)
            {
                businessinvoice.MarginRatio = Convert.ToDecimal(dr[indexMarginRatio]);
            }

            int indexVATRatio = dr.GetOrdinal("VATRatio");
            if (dr["VATRatio"] != DBNull.Value)
            {
                businessinvoice.VATRatio = Convert.ToDecimal(dr[indexVATRatio]);
            }

            int indexVATBala = dr.GetOrdinal("VATBala");
            if (dr["VATBala"] != DBNull.Value)
            {
                businessinvoice.VATBala = Convert.ToDecimal(dr[indexVATBala]);
            }


            return businessinvoice;
        }

        public override string TableName
        {
            get
            {
                return "Inv_BusinessInvoice";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            BusinessInvoice inv_businessinvoice = (BusinessInvoice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter businessinvoiceidpara = new SqlParameter("@BusinessInvoiceId", SqlDbType.Int, 4);
            businessinvoiceidpara.Value = inv_businessinvoice.BusinessInvoiceId;
            paras.Add(businessinvoiceidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = inv_businessinvoice.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter refinvoiceidpara = new SqlParameter("@RefInvoiceId", SqlDbType.Int, 4);
            refinvoiceidpara.Value = inv_businessinvoice.RefInvoiceId;
            paras.Add(refinvoiceidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = inv_businessinvoice.ContractId;
            paras.Add(contractidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = inv_businessinvoice.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = inv_businessinvoice.AssetId;
            paras.Add(assetidpara);

            SqlParameter integeramountpara = new SqlParameter("@IntegerAmount", SqlDbType.Decimal, 9);
            integeramountpara.Value = inv_businessinvoice.IntegerAmount;
            paras.Add(integeramountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = inv_businessinvoice.NetAmount;
            paras.Add(netamountpara);

            SqlParameter unitpricepara = new SqlParameter("@UnitPrice", SqlDbType.Decimal, 9);
            unitpricepara.Value = inv_businessinvoice.UnitPrice;
            paras.Add(unitpricepara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = inv_businessinvoice.MUId;
            paras.Add(muidpara);

            SqlParameter marginratiopara = new SqlParameter("@MarginRatio", SqlDbType.Decimal, 9);
            marginratiopara.Value = inv_businessinvoice.MarginRatio;
            paras.Add(marginratiopara);

            SqlParameter vatratiopara = new SqlParameter("@VATRatio", SqlDbType.Decimal, 9);
            vatratiopara.Value = inv_businessinvoice.VATRatio;
            paras.Add(vatratiopara);

            SqlParameter vatbalapara = new SqlParameter("@VATBala", SqlDbType.Decimal, 9);
            vatbalapara.Value = inv_businessinvoice.VATBala;
            paras.Add(vatbalapara);


            return paras;
        }

        #endregion        
    }
}
