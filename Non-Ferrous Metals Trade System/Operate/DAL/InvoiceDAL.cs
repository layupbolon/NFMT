/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceDAL.cs
// 文件功能描述：发票dbo.Invoice数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月26日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Operate.Model;
using NFMT.DBUtility;
using NFMT.Operate.IDAL;
using NFMT.Common;

namespace NFMT.Operate.DAL
{
    /// <summary>
    /// 发票dbo.Invoice数据交互类。
    /// </summary>
    public class InvoiceDAL : ExecOperate, IInvoiceDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InvoiceDAL()
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
            Invoice invoice = (Invoice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@InvoiceId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter invoicedatepara = new SqlParameter("@InvoiceDate", SqlDbType.DateTime, 8);
            invoicedatepara.Value = invoice.InvoiceDate;
            paras.Add(invoicedatepara);

            if (!string.IsNullOrEmpty(invoice.InvoiceNo))
            {
                SqlParameter invoicenopara = new SqlParameter("@InvoiceNo", SqlDbType.VarChar, 200);
                invoicenopara.Value = invoice.InvoiceNo;
                paras.Add(invoicenopara);
            }

            if (!string.IsNullOrEmpty(invoice.InvoiceName))
            {
                SqlParameter invoicenamepara = new SqlParameter("@InvoiceName", SqlDbType.VarChar, 80);
                invoicenamepara.Value = invoice.InvoiceName;
                paras.Add(invoicenamepara);
            }

            SqlParameter invoicetypepara = new SqlParameter("@InvoiceType", SqlDbType.Int, 4);
            invoicetypepara.Value = invoice.InvoiceType;
            paras.Add(invoicetypepara);

            SqlParameter invoicebalapara = new SqlParameter("@InvoiceBala", SqlDbType.Decimal, 9);
            invoicebalapara.Value = invoice.InvoiceBala;
            paras.Add(invoicebalapara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = invoice.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter invoicedirectionpara = new SqlParameter("@InvoiceDirection", SqlDbType.Int, 4);
            invoicedirectionpara.Value = invoice.InvoiceDirection;
            paras.Add(invoicedirectionpara);

            SqlParameter outblocidpara = new SqlParameter("@OutBlocId", SqlDbType.Int, 4);
            outblocidpara.Value = invoice.OutBlocId;
            paras.Add(outblocidpara);

            SqlParameter outcorpidpara = new SqlParameter("@OutCorpId", SqlDbType.Int, 4);
            outcorpidpara.Value = invoice.OutCorpId;
            paras.Add(outcorpidpara);

            if (!string.IsNullOrEmpty(invoice.OutCorpName))
            {
                SqlParameter outcorpnamepara = new SqlParameter("@OutCorpName", SqlDbType.VarChar, 200);
                outcorpnamepara.Value = invoice.OutCorpName;
                paras.Add(outcorpnamepara);
            }

            SqlParameter inblocidpara = new SqlParameter("@InBlocId", SqlDbType.Int, 4);
            inblocidpara.Value = invoice.InBlocId;
            paras.Add(inblocidpara);

            SqlParameter incorpidpara = new SqlParameter("@InCorpId", SqlDbType.Int, 4);
            incorpidpara.Value = invoice.InCorpId;
            paras.Add(incorpidpara);

            if (!string.IsNullOrEmpty(invoice.InCorpName))
            {
                SqlParameter incorpnamepara = new SqlParameter("@InCorpName", SqlDbType.VarChar, 200);
                incorpnamepara.Value = invoice.InCorpName;
                paras.Add(incorpnamepara);
            }

            SqlParameter invoicestatuspara = new SqlParameter("@InvoiceStatus", SqlDbType.Int, 4);
            invoicestatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(invoicestatuspara);

            if (!string.IsNullOrEmpty(invoice.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 800);
                memopara.Value = invoice.Memo;
                paras.Add(memopara);
            }

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Invoice invoice = new Invoice();

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            invoice.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);

            int indexInvoiceDate = dr.GetOrdinal("InvoiceDate");
            if (dr["InvoiceDate"] != DBNull.Value)
            {
                invoice.InvoiceDate = Convert.ToDateTime(dr[indexInvoiceDate]);
            }

            int indexInvoiceNo = dr.GetOrdinal("InvoiceNo");
            if (dr["InvoiceNo"] != DBNull.Value)
            {
                invoice.InvoiceNo = Convert.ToString(dr[indexInvoiceNo]);
            }

            int indexInvoiceName = dr.GetOrdinal("InvoiceName");
            if (dr["InvoiceName"] != DBNull.Value)
            {
                invoice.InvoiceName = Convert.ToString(dr[indexInvoiceName]);
            }

            int indexInvoiceType = dr.GetOrdinal("InvoiceType");
            if (dr["InvoiceType"] != DBNull.Value)
            {
                invoice.InvoiceType = Convert.ToInt32(dr[indexInvoiceType]);
            }

            int indexInvoiceBala = dr.GetOrdinal("InvoiceBala");
            if (dr["InvoiceBala"] != DBNull.Value)
            {
                invoice.InvoiceBala = Convert.ToDecimal(dr[indexInvoiceBala]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                invoice.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexInvoiceDirection = dr.GetOrdinal("InvoiceDirection");
            if (dr["InvoiceDirection"] != DBNull.Value)
            {
                invoice.InvoiceDirection = Convert.ToInt32(dr[indexInvoiceDirection]);
            }

            int indexOutBlocId = dr.GetOrdinal("OutBlocId");
            if (dr["OutBlocId"] != DBNull.Value)
            {
                invoice.OutBlocId = Convert.ToInt32(dr[indexOutBlocId]);
            }

            int indexOutCorpId = dr.GetOrdinal("OutCorpId");
            if (dr["OutCorpId"] != DBNull.Value)
            {
                invoice.OutCorpId = Convert.ToInt32(dr[indexOutCorpId]);
            }

            int indexOutCorpName = dr.GetOrdinal("OutCorpName");
            if (dr["OutCorpName"] != DBNull.Value)
            {
                invoice.OutCorpName = Convert.ToString(dr[indexOutCorpName]);
            }

            int indexInBlocId = dr.GetOrdinal("InBlocId");
            if (dr["InBlocId"] != DBNull.Value)
            {
                invoice.InBlocId = Convert.ToInt32(dr[indexInBlocId]);
            }

            int indexInCorpId = dr.GetOrdinal("InCorpId");
            if (dr["InCorpId"] != DBNull.Value)
            {
                invoice.InCorpId = Convert.ToInt32(dr[indexInCorpId]);
            }

            int indexInCorpName = dr.GetOrdinal("InCorpName");
            if (dr["InCorpName"] != DBNull.Value)
            {
                invoice.InCorpName = Convert.ToString(dr[indexInCorpName]);
            }

            int indexInvoiceStatus = dr.GetOrdinal("InvoiceStatus");
            if (dr["InvoiceStatus"] != DBNull.Value)
            {
                invoice.InvoiceStatus = (StatusEnum)Convert.ToInt32(dr[indexInvoiceStatus]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                invoice.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                invoice.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                invoice.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                invoice.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                invoice.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return invoice;
        }

        public override string TableName
        {
            get
            {
                return "Invoice";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Invoice invoice = (Invoice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = invoice.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter invoicedatepara = new SqlParameter("@InvoiceDate", SqlDbType.DateTime, 8);
            invoicedatepara.Value = invoice.InvoiceDate;
            paras.Add(invoicedatepara);

            if (!string.IsNullOrEmpty(invoice.InvoiceNo))
            {
                SqlParameter invoicenopara = new SqlParameter("@InvoiceNo", SqlDbType.VarChar, 200);
                invoicenopara.Value = invoice.InvoiceNo;
                paras.Add(invoicenopara);
            }

            if (!string.IsNullOrEmpty(invoice.InvoiceName))
            {
                SqlParameter invoicenamepara = new SqlParameter("@InvoiceName", SqlDbType.VarChar, 80);
                invoicenamepara.Value = invoice.InvoiceName;
                paras.Add(invoicenamepara);
            }

            SqlParameter invoicetypepara = new SqlParameter("@InvoiceType", SqlDbType.Int, 4);
            invoicetypepara.Value = invoice.InvoiceType;
            paras.Add(invoicetypepara);

            SqlParameter invoicebalapara = new SqlParameter("@InvoiceBala", SqlDbType.Decimal, 9);
            invoicebalapara.Value = invoice.InvoiceBala;
            paras.Add(invoicebalapara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = invoice.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter invoicedirectionpara = new SqlParameter("@InvoiceDirection", SqlDbType.Int, 4);
            invoicedirectionpara.Value = invoice.InvoiceDirection;
            paras.Add(invoicedirectionpara);

            SqlParameter outblocidpara = new SqlParameter("@OutBlocId", SqlDbType.Int, 4);
            outblocidpara.Value = invoice.OutBlocId;
            paras.Add(outblocidpara);

            SqlParameter outcorpidpara = new SqlParameter("@OutCorpId", SqlDbType.Int, 4);
            outcorpidpara.Value = invoice.OutCorpId;
            paras.Add(outcorpidpara);

            if (!string.IsNullOrEmpty(invoice.OutCorpName))
            {
                SqlParameter outcorpnamepara = new SqlParameter("@OutCorpName", SqlDbType.VarChar, 200);
                outcorpnamepara.Value = invoice.OutCorpName;
                paras.Add(outcorpnamepara);
            }

            SqlParameter inblocidpara = new SqlParameter("@InBlocId", SqlDbType.Int, 4);
            inblocidpara.Value = invoice.InBlocId;
            paras.Add(inblocidpara);

            SqlParameter incorpidpara = new SqlParameter("@InCorpId", SqlDbType.Int, 4);
            incorpidpara.Value = invoice.InCorpId;
            paras.Add(incorpidpara);

            if (!string.IsNullOrEmpty(invoice.InCorpName))
            {
                SqlParameter incorpnamepara = new SqlParameter("@InCorpName", SqlDbType.VarChar, 200);
                incorpnamepara.Value = invoice.InCorpName;
                paras.Add(incorpnamepara);
            }

            SqlParameter invoicestatuspara = new SqlParameter("@InvoiceStatus", SqlDbType.Int, 4);
            invoicestatuspara.Value = invoice.InvoiceStatus;
            paras.Add(invoicestatuspara);

            if (!string.IsNullOrEmpty(invoice.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 800);
                memopara.Value = invoice.Memo;
                paras.Add(memopara);
            }

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetByBussinessInvoiceId(UserModel user, int bussinessInvoiceId)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                string sql = string.Format("select * from dbo.Invoice where InvoiceId = (select InvoiceId from dbo.Inv_BusinessInvoice where BusinessInvoiceId = {0})", bussinessInvoiceId);
                dr = SqlHelper.ExecuteReader(this.ConnectString, CommandType.Text, sql, null);

                Model.Invoice model = null;

                if (dr.Read())
                {
                    model = CreateModel<Model.Invoice>(dr);

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = model;
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        #endregion
    }
}
