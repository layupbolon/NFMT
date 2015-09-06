/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentOrderDAL.cs
// 文件功能描述：制单指令dbo.Doc_DocumentOrder数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年12月2日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Document.Model;
using NFMT.DBUtility;
using NFMT.Document.IDAL;
using NFMT.Common;

namespace NFMT.Document.DAL
{
    /// <summary>
    /// 制单指令dbo.Doc_DocumentOrder数据交互类。
    /// </summary>
    public class DocumentOrderDAL : ApplyOperate, IDocumentOrderDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DocumentOrderDAL()
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
            DocumentOrder doc_documentorder = (DocumentOrder)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@OrderId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(doc_documentorder.OrderNo))
            {
                SqlParameter ordernopara = new SqlParameter("@OrderNo", SqlDbType.VarChar, 200);
                ordernopara.Value = doc_documentorder.OrderNo;
                paras.Add(ordernopara);
            }

            SqlParameter commercialidpara = new SqlParameter("@CommercialId", SqlDbType.Int, 4);
            commercialidpara.Value = doc_documentorder.CommercialId;
            paras.Add(commercialidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = doc_documentorder.ContractId;
            paras.Add(contractidpara);

            if (!string.IsNullOrEmpty(doc_documentorder.ContractNo))
            {
                SqlParameter contractnopara = new SqlParameter("@ContractNo", SqlDbType.VarChar, 200);
                contractnopara.Value = doc_documentorder.ContractNo;
                paras.Add(contractnopara);
            }

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = doc_documentorder.SubId;
            paras.Add(subidpara);

            SqlParameter lcidpara = new SqlParameter("@LCId", SqlDbType.Int, 4);
            lcidpara.Value = doc_documentorder.LCId;
            paras.Add(lcidpara);

            if (!string.IsNullOrEmpty(doc_documentorder.LCNo))
            {
                SqlParameter lcnopara = new SqlParameter("@LCNo", SqlDbType.VarChar, 200);
                lcnopara.Value = doc_documentorder.LCNo;
                paras.Add(lcnopara);
            }

            SqlParameter lcdaypara = new SqlParameter("@LCDay", SqlDbType.Int, 4);
            lcdaypara.Value = doc_documentorder.LCDay;
            paras.Add(lcdaypara);

            SqlParameter ordertypepara = new SqlParameter("@OrderType", SqlDbType.Int, 4);
            ordertypepara.Value = doc_documentorder.OrderType;
            paras.Add(ordertypepara);

            SqlParameter orderdatepara = new SqlParameter("@OrderDate", SqlDbType.DateTime, 8);
            orderdatepara.Value = doc_documentorder.OrderDate;
            paras.Add(orderdatepara);

            SqlParameter applycorppara = new SqlParameter("@ApplyCorp", SqlDbType.Int, 4);
            applycorppara.Value = doc_documentorder.ApplyCorp;
            paras.Add(applycorppara);

            SqlParameter applydeptpara = new SqlParameter("@ApplyDept", SqlDbType.Int, 4);
            applydeptpara.Value = doc_documentorder.ApplyDept;
            paras.Add(applydeptpara);

            SqlParameter sellercorppara = new SqlParameter("@SellerCorp", SqlDbType.Int, 4);
            sellercorppara.Value = doc_documentorder.SellerCorp;
            paras.Add(sellercorppara);

            SqlParameter buyercorppara = new SqlParameter("@BuyerCorp", SqlDbType.Int, 4);
            buyercorppara.Value = doc_documentorder.BuyerCorp;
            paras.Add(buyercorppara);

            if (!string.IsNullOrEmpty(doc_documentorder.BuyerCorpName))
            {
                SqlParameter buyercorpnamepara = new SqlParameter("@BuyerCorpName", SqlDbType.VarChar, 200);
                buyercorpnamepara.Value = doc_documentorder.BuyerCorpName;
                paras.Add(buyercorpnamepara);
            }

            if (!string.IsNullOrEmpty(doc_documentorder.BuyerAddress))
            {
                SqlParameter buyeraddresspara = new SqlParameter("@BuyerAddress", SqlDbType.VarChar, 800);
                buyeraddresspara.Value = doc_documentorder.BuyerAddress;
                paras.Add(buyeraddresspara);
            }

            SqlParameter paymentstylepara = new SqlParameter("@PaymentStyle", SqlDbType.Int, 4);
            paymentstylepara.Value = doc_documentorder.PaymentStyle;
            paras.Add(paymentstylepara);

            SqlParameter recbankidpara = new SqlParameter("@RecBankId", SqlDbType.Int, 4);
            recbankidpara.Value = doc_documentorder.RecBankId;
            paras.Add(recbankidpara);

            if (!string.IsNullOrEmpty(doc_documentorder.DiscountBase))
            {
                SqlParameter discountbasepara = new SqlParameter("@DiscountBase", SqlDbType.VarChar, 200);
                discountbasepara.Value = doc_documentorder.DiscountBase;
                paras.Add(discountbasepara);
            }

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = doc_documentorder.AssetId;
            paras.Add(assetidpara);

            SqlParameter brandidpara = new SqlParameter("@BrandId", SqlDbType.Int, 4);
            brandidpara.Value = doc_documentorder.BrandId;
            paras.Add(brandidpara);

            SqlParameter areaidpara = new SqlParameter("@AreaId", SqlDbType.Int, 4);
            areaidpara.Value = doc_documentorder.AreaId;
            paras.Add(areaidpara);

            if (!string.IsNullOrEmpty(doc_documentorder.AreaName))
            {
                SqlParameter areanamepara = new SqlParameter("@AreaName", SqlDbType.VarChar, 400);
                areanamepara.Value = doc_documentorder.AreaName;
                paras.Add(areanamepara);
            }

            if (!string.IsNullOrEmpty(doc_documentorder.BankCode))
            {
                SqlParameter bankcodepara = new SqlParameter("@BankCode", SqlDbType.VarChar, 400);
                bankcodepara.Value = doc_documentorder.BankCode;
                paras.Add(bankcodepara);
            }

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = doc_documentorder.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = doc_documentorder.NetAmount;
            paras.Add(netamountpara);

            SqlParameter unitidpara = new SqlParameter("@UnitId", SqlDbType.Int, 4);
            unitidpara.Value = doc_documentorder.UnitId;
            paras.Add(unitidpara);

            SqlParameter currencypara = new SqlParameter("@Currency", SqlDbType.Int, 4);
            currencypara.Value = doc_documentorder.Currency;
            paras.Add(currencypara);

            SqlParameter unitpricepara = new SqlParameter("@UnitPrice", SqlDbType.Decimal, 9);
            unitpricepara.Value = doc_documentorder.UnitPrice;
            paras.Add(unitpricepara);

            SqlParameter invbalapara = new SqlParameter("@InvBala", SqlDbType.Decimal, 9);
            invbalapara.Value = doc_documentorder.InvBala;
            paras.Add(invbalapara);

            SqlParameter invgappara = new SqlParameter("@InvGap", SqlDbType.Decimal, 9);
            invgappara.Value = doc_documentorder.InvGap;
            paras.Add(invgappara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = doc_documentorder.Bundles;
            paras.Add(bundlespara);

            if (!string.IsNullOrEmpty(doc_documentorder.Meno))
            {
                SqlParameter menopara = new SqlParameter("@Meno", SqlDbType.VarChar, 4000);
                menopara.Value = doc_documentorder.Meno;
                paras.Add(menopara);
            }

            SqlParameter orderstatuspara = new SqlParameter("@OrderStatus", SqlDbType.Int, 4);
            orderstatuspara.Value = doc_documentorder.OrderStatus;
            paras.Add(orderstatuspara);

            SqlParameter applyempidpara = new SqlParameter("@ApplyEmpId", SqlDbType.Int, 4);
            applyempidpara.Value = doc_documentorder.ApplyEmpId;
            paras.Add(applyempidpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            DocumentOrder documentorder = new DocumentOrder();

            int indexOrderId = dr.GetOrdinal("OrderId");
            documentorder.OrderId = Convert.ToInt32(dr[indexOrderId]);

            int indexOrderNo = dr.GetOrdinal("OrderNo");
            if (dr["OrderNo"] != DBNull.Value)
            {
                documentorder.OrderNo = Convert.ToString(dr[indexOrderNo]);
            }

            int indexCommercialId = dr.GetOrdinal("CommercialId");
            if (dr["CommercialId"] != DBNull.Value)
            {
                documentorder.CommercialId = Convert.ToInt32(dr[indexCommercialId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                documentorder.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexContractNo = dr.GetOrdinal("ContractNo");
            if (dr["ContractNo"] != DBNull.Value)
            {
                documentorder.ContractNo = Convert.ToString(dr[indexContractNo]);
            }

            int indexSubId = dr.GetOrdinal("SubId");
            if (dr["SubId"] != DBNull.Value)
            {
                documentorder.SubId = Convert.ToInt32(dr[indexSubId]);
            }

            int indexLCId = dr.GetOrdinal("LCId");
            if (dr["LCId"] != DBNull.Value)
            {
                documentorder.LCId = Convert.ToInt32(dr[indexLCId]);
            }

            int indexLCNo = dr.GetOrdinal("LCNo");
            if (dr["LCNo"] != DBNull.Value)
            {
                documentorder.LCNo = Convert.ToString(dr[indexLCNo]);
            }

            int indexLCDay = dr.GetOrdinal("LCDay");
            if (dr["LCDay"] != DBNull.Value)
            {
                documentorder.LCDay = Convert.ToInt32(dr[indexLCDay]);
            }

            int indexOrderType = dr.GetOrdinal("OrderType");
            if (dr["OrderType"] != DBNull.Value)
            {
                documentorder.OrderType = Convert.ToInt32(dr[indexOrderType]);
            }

            int indexOrderDate = dr.GetOrdinal("OrderDate");
            if (dr["OrderDate"] != DBNull.Value)
            {
                documentorder.OrderDate = Convert.ToDateTime(dr[indexOrderDate]);
            }

            int indexApplyCorp = dr.GetOrdinal("ApplyCorp");
            if (dr["ApplyCorp"] != DBNull.Value)
            {
                documentorder.ApplyCorp = Convert.ToInt32(dr[indexApplyCorp]);
            }

            int indexApplyDept = dr.GetOrdinal("ApplyDept");
            if (dr["ApplyDept"] != DBNull.Value)
            {
                documentorder.ApplyDept = Convert.ToInt32(dr[indexApplyDept]);
            }

            int indexSellerCorp = dr.GetOrdinal("SellerCorp");
            if (dr["SellerCorp"] != DBNull.Value)
            {
                documentorder.SellerCorp = Convert.ToInt32(dr[indexSellerCorp]);
            }

            int indexBuyerCorp = dr.GetOrdinal("BuyerCorp");
            if (dr["BuyerCorp"] != DBNull.Value)
            {
                documentorder.BuyerCorp = Convert.ToInt32(dr[indexBuyerCorp]);
            }

            int indexBuyerCorpName = dr.GetOrdinal("BuyerCorpName");
            if (dr["BuyerCorpName"] != DBNull.Value)
            {
                documentorder.BuyerCorpName = Convert.ToString(dr[indexBuyerCorpName]);
            }

            int indexBuyerAddress = dr.GetOrdinal("BuyerAddress");
            if (dr["BuyerAddress"] != DBNull.Value)
            {
                documentorder.BuyerAddress = Convert.ToString(dr[indexBuyerAddress]);
            }

            int indexPaymentStyle = dr.GetOrdinal("PaymentStyle");
            if (dr["PaymentStyle"] != DBNull.Value)
            {
                documentorder.PaymentStyle = Convert.ToInt32(dr[indexPaymentStyle]);
            }

            int indexRecBankId = dr.GetOrdinal("RecBankId");
            if (dr["RecBankId"] != DBNull.Value)
            {
                documentorder.RecBankId = Convert.ToInt32(dr[indexRecBankId]);
            }

            int indexDiscountBase = dr.GetOrdinal("DiscountBase");
            if (dr["DiscountBase"] != DBNull.Value)
            {
                documentorder.DiscountBase = Convert.ToString(dr[indexDiscountBase]);
            }

            int indexAssetId = dr.GetOrdinal("AssetId");
            if (dr["AssetId"] != DBNull.Value)
            {
                documentorder.AssetId = Convert.ToInt32(dr[indexAssetId]);
            }

            int indexBrandId = dr.GetOrdinal("BrandId");
            if (dr["BrandId"] != DBNull.Value)
            {
                documentorder.BrandId = Convert.ToInt32(dr[indexBrandId]);
            }

            int indexAreaId = dr.GetOrdinal("AreaId");
            if (dr["AreaId"] != DBNull.Value)
            {
                documentorder.AreaId = Convert.ToInt32(dr[indexAreaId]);
            }

            int indexAreaName = dr.GetOrdinal("AreaName");
            if (dr["AreaName"] != DBNull.Value)
            {
                documentorder.AreaName = Convert.ToString(dr[indexAreaName]);
            }

            int indexBankCode = dr.GetOrdinal("BankCode");
            if (dr["BankCode"] != DBNull.Value)
            {
                documentorder.BankCode = Convert.ToString(dr[indexBankCode]);
            }

            int indexGrossAmount = dr.GetOrdinal("GrossAmount");
            if (dr["GrossAmount"] != DBNull.Value)
            {
                documentorder.GrossAmount = Convert.ToDecimal(dr[indexGrossAmount]);
            }

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            if (dr["NetAmount"] != DBNull.Value)
            {
                documentorder.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
            }

            int indexUnitId = dr.GetOrdinal("UnitId");
            if (dr["UnitId"] != DBNull.Value)
            {
                documentorder.UnitId = Convert.ToInt32(dr[indexUnitId]);
            }

            int indexCurrency = dr.GetOrdinal("Currency");
            if (dr["Currency"] != DBNull.Value)
            {
                documentorder.Currency = Convert.ToInt32(dr[indexCurrency]);
            }

            int indexUnitPrice = dr.GetOrdinal("UnitPrice");
            if (dr["UnitPrice"] != DBNull.Value)
            {
                documentorder.UnitPrice = Convert.ToDecimal(dr[indexUnitPrice]);
            }

            int indexInvBala = dr.GetOrdinal("InvBala");
            if (dr["InvBala"] != DBNull.Value)
            {
                documentorder.InvBala = Convert.ToDecimal(dr[indexInvBala]);
            }

            int indexInvGap = dr.GetOrdinal("InvGap");
            if (dr["InvGap"] != DBNull.Value)
            {
                documentorder.InvGap = Convert.ToDecimal(dr[indexInvGap]);
            }

            int indexBundles = dr.GetOrdinal("Bundles");
            if (dr["Bundles"] != DBNull.Value)
            {
                documentorder.Bundles = Convert.ToInt32(dr[indexBundles]);
            }

            int indexMeno = dr.GetOrdinal("Meno");
            if (dr["Meno"] != DBNull.Value)
            {
                documentorder.Meno = Convert.ToString(dr[indexMeno]);
            }

            int indexOrderStatus = dr.GetOrdinal("OrderStatus");
            if (dr["OrderStatus"] != DBNull.Value)
            {
                documentorder.OrderStatus = (StatusEnum)Convert.ToInt32(dr[indexOrderStatus]);
            }

            int indexApplyEmpId = dr.GetOrdinal("ApplyEmpId");
            if (dr["ApplyEmpId"] != DBNull.Value)
            {
                documentorder.ApplyEmpId = Convert.ToInt32(dr[indexApplyEmpId]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                documentorder.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                documentorder.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                documentorder.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                documentorder.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return documentorder;
        }

        public override string TableName
        {
            get
            {
                return "Doc_DocumentOrder";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            DocumentOrder doc_documentorder = (DocumentOrder)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter orderidpara = new SqlParameter("@OrderId", SqlDbType.Int, 4);
            orderidpara.Value = doc_documentorder.OrderId;
            paras.Add(orderidpara);

            if (!string.IsNullOrEmpty(doc_documentorder.OrderNo))
            {
                SqlParameter ordernopara = new SqlParameter("@OrderNo", SqlDbType.VarChar, 200);
                ordernopara.Value = doc_documentorder.OrderNo;
                paras.Add(ordernopara);
            }

            SqlParameter commercialidpara = new SqlParameter("@CommercialId", SqlDbType.Int, 4);
            commercialidpara.Value = doc_documentorder.CommercialId;
            paras.Add(commercialidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = doc_documentorder.ContractId;
            paras.Add(contractidpara);

            if (!string.IsNullOrEmpty(doc_documentorder.ContractNo))
            {
                SqlParameter contractnopara = new SqlParameter("@ContractNo", SqlDbType.VarChar, 200);
                contractnopara.Value = doc_documentorder.ContractNo;
                paras.Add(contractnopara);
            }

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = doc_documentorder.SubId;
            paras.Add(subidpara);

            SqlParameter lcidpara = new SqlParameter("@LCId", SqlDbType.Int, 4);
            lcidpara.Value = doc_documentorder.LCId;
            paras.Add(lcidpara);

            if (!string.IsNullOrEmpty(doc_documentorder.LCNo))
            {
                SqlParameter lcnopara = new SqlParameter("@LCNo", SqlDbType.VarChar, 200);
                lcnopara.Value = doc_documentorder.LCNo;
                paras.Add(lcnopara);
            }

            SqlParameter lcdaypara = new SqlParameter("@LCDay", SqlDbType.Int, 4);
            lcdaypara.Value = doc_documentorder.LCDay;
            paras.Add(lcdaypara);

            SqlParameter ordertypepara = new SqlParameter("@OrderType", SqlDbType.Int, 4);
            ordertypepara.Value = doc_documentorder.OrderType;
            paras.Add(ordertypepara);

            SqlParameter orderdatepara = new SqlParameter("@OrderDate", SqlDbType.DateTime, 8);
            orderdatepara.Value = doc_documentorder.OrderDate;
            paras.Add(orderdatepara);

            SqlParameter applycorppara = new SqlParameter("@ApplyCorp", SqlDbType.Int, 4);
            applycorppara.Value = doc_documentorder.ApplyCorp;
            paras.Add(applycorppara);

            SqlParameter applydeptpara = new SqlParameter("@ApplyDept", SqlDbType.Int, 4);
            applydeptpara.Value = doc_documentorder.ApplyDept;
            paras.Add(applydeptpara);

            SqlParameter sellercorppara = new SqlParameter("@SellerCorp", SqlDbType.Int, 4);
            sellercorppara.Value = doc_documentorder.SellerCorp;
            paras.Add(sellercorppara);

            SqlParameter buyercorppara = new SqlParameter("@BuyerCorp", SqlDbType.Int, 4);
            buyercorppara.Value = doc_documentorder.BuyerCorp;
            paras.Add(buyercorppara);

            if (!string.IsNullOrEmpty(doc_documentorder.BuyerCorpName))
            {
                SqlParameter buyercorpnamepara = new SqlParameter("@BuyerCorpName", SqlDbType.VarChar, 200);
                buyercorpnamepara.Value = doc_documentorder.BuyerCorpName;
                paras.Add(buyercorpnamepara);
            }

            if (!string.IsNullOrEmpty(doc_documentorder.BuyerAddress))
            {
                SqlParameter buyeraddresspara = new SqlParameter("@BuyerAddress", SqlDbType.VarChar, 800);
                buyeraddresspara.Value = doc_documentorder.BuyerAddress;
                paras.Add(buyeraddresspara);
            }

            SqlParameter paymentstylepara = new SqlParameter("@PaymentStyle", SqlDbType.Int, 4);
            paymentstylepara.Value = doc_documentorder.PaymentStyle;
            paras.Add(paymentstylepara);

            SqlParameter recbankidpara = new SqlParameter("@RecBankId", SqlDbType.Int, 4);
            recbankidpara.Value = doc_documentorder.RecBankId;
            paras.Add(recbankidpara);

            if (!string.IsNullOrEmpty(doc_documentorder.DiscountBase))
            {
                SqlParameter discountbasepara = new SqlParameter("@DiscountBase", SqlDbType.VarChar, 200);
                discountbasepara.Value = doc_documentorder.DiscountBase;
                paras.Add(discountbasepara);
            }

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = doc_documentorder.AssetId;
            paras.Add(assetidpara);

            SqlParameter brandidpara = new SqlParameter("@BrandId", SqlDbType.Int, 4);
            brandidpara.Value = doc_documentorder.BrandId;
            paras.Add(brandidpara);

            SqlParameter areaidpara = new SqlParameter("@AreaId", SqlDbType.Int, 4);
            areaidpara.Value = doc_documentorder.AreaId;
            paras.Add(areaidpara);

            if (!string.IsNullOrEmpty(doc_documentorder.AreaName))
            {
                SqlParameter areanamepara = new SqlParameter("@AreaName", SqlDbType.VarChar, 400);
                areanamepara.Value = doc_documentorder.AreaName;
                paras.Add(areanamepara);
            }

            if (!string.IsNullOrEmpty(doc_documentorder.BankCode))
            {
                SqlParameter bankcodepara = new SqlParameter("@BankCode", SqlDbType.VarChar, 400);
                bankcodepara.Value = doc_documentorder.BankCode;
                paras.Add(bankcodepara);
            }

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = doc_documentorder.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = doc_documentorder.NetAmount;
            paras.Add(netamountpara);

            SqlParameter unitidpara = new SqlParameter("@UnitId", SqlDbType.Int, 4);
            unitidpara.Value = doc_documentorder.UnitId;
            paras.Add(unitidpara);

            SqlParameter currencypara = new SqlParameter("@Currency", SqlDbType.Int, 4);
            currencypara.Value = doc_documentorder.Currency;
            paras.Add(currencypara);

            SqlParameter unitpricepara = new SqlParameter("@UnitPrice", SqlDbType.Decimal, 9);
            unitpricepara.Value = doc_documentorder.UnitPrice;
            paras.Add(unitpricepara);

            SqlParameter invbalapara = new SqlParameter("@InvBala", SqlDbType.Decimal, 9);
            invbalapara.Value = doc_documentorder.InvBala;
            paras.Add(invbalapara);

            SqlParameter invgappara = new SqlParameter("@InvGap", SqlDbType.Decimal, 9);
            invgappara.Value = doc_documentorder.InvGap;
            paras.Add(invgappara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = doc_documentorder.Bundles;
            paras.Add(bundlespara);

            if (!string.IsNullOrEmpty(doc_documentorder.Meno))
            {
                SqlParameter menopara = new SqlParameter("@Meno", SqlDbType.VarChar, 4000);
                menopara.Value = doc_documentorder.Meno;
                paras.Add(menopara);
            }

            SqlParameter orderstatuspara = new SqlParameter("@OrderStatus", SqlDbType.Int, 4);
            orderstatuspara.Value = doc_documentorder.OrderStatus;
            paras.Add(orderstatuspara);

            SqlParameter applyempidpara = new SqlParameter("@ApplyEmpId", SqlDbType.Int, 4);
            applyempidpara.Value = doc_documentorder.ApplyEmpId;
            paras.Add(applyempidpara);

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
                return 101;
            }
        }

        #endregion
    }
}
