/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceDAL.cs
// 文件功能描述：发票dbo.Invoice数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月1日
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
    /// 发票dbo.Invoice数据交互类。
    /// </summary>
    public class InvoiceDAL : DataOperate, IInvoiceDAL
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

        /// <summary>
        /// 新增invoice信息
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">Invoice对象</param>
        /// <returns></returns>
        public override ResultModel Insert(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();
            try
            {
                Model.Invoice invoice = (Model.Invoice)obj;

                if (invoice == null)
                {
                    result.Message = "新增对象不能为null";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();
                SqlParameter invoiceidpara = new SqlParameter();
                invoiceidpara.Direction = ParameterDirection.Output;
                invoiceidpara.SqlDbType = SqlDbType.Int;
                invoiceidpara.ParameterName = "@InvoiceId";
                invoiceidpara.Size = 4;
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

                SqlParameter inviocetypepara = new SqlParameter("@InvoiceType", SqlDbType.Int, 4);
                inviocetypepara.Value = invoice.InvoiceType;
                paras.Add(inviocetypepara);

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

                SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
                creatoridpara.Value = user.AccountId;
                paras.Add(creatoridpara);


                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, "InvoiceInsert", paras.ToArray());

                if (i == 1)
                {
                    result.AffectCount = i;
                    result.ResultStatus = 0;
                    result.Message = "Invoice添加成功";
                    result.ReturnValue = invoiceidpara.Value;
                }
                else
                    result.Message = "Invoice添加失败";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 获取指定invoiceId的invoice对象
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="invoiceId">主键值</param>
        /// <returns></returns>
        public override ResultModel Get(UserModel user, int invoiceId)
        {
            ResultModel result = new ResultModel();

            if (invoiceId < 1)
            {
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            para.Value = invoiceId;
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.StoredProcedure, "InvoiceGet", paras.ToArray());

                Model.Invoice invoice = new Model.Invoice();

                if (dr.Read())
                {
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

                    int indexInvioceType = dr.GetOrdinal("InvoiceType");
                    if (dr["InvoiceType"] != DBNull.Value)
                    {
                        invoice.InvoiceType = (InvoiceTypeEnum)Convert.ToInt32(dr[indexInvioceType]);
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
                        invoice.InvoiceStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexInvoiceStatus]);
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

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = invoice;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        /// <summary>
        /// 获取invoice集合
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <returns></returns>
        public override ResultModel Load(UserModel user)
        {
            ResultModel result = new ResultModel();
            try
            {
                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, "InvoiceLoad", null, CommandType.StoredProcedure);

                List<Model.Invoice> invoices = new List<Model.Invoice>();

                foreach (DataRow dr in dt.Rows)
                {
                    Model.Invoice invoice = new Model.Invoice();
                    invoice.InvoiceId = Convert.ToInt32(dr["InvoiceId"]);

                    if (dr["InvoiceDate"] != DBNull.Value)
                    {
                        invoice.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                    }
                    if (dr["InvoiceNo"] != DBNull.Value)
                    {
                        invoice.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);
                    }
                    if (dr["InvoiceName"] != DBNull.Value)
                    {
                        invoice.InvoiceName = Convert.ToString(dr["InvoiceName"]);
                    }
                    if (dr["InvoiceType"] != DBNull.Value)
                    {
                        invoice.InvoiceType = (InvoiceTypeEnum)Convert.ToInt32(dr["InvoiceType"]);
                    }
                    if (dr["InvoiceBala"] != DBNull.Value)
                    {
                        invoice.InvoiceBala = Convert.ToDecimal(dr["InvoiceBala"]);
                    }
                    if (dr["CurrencyId"] != DBNull.Value)
                    {
                        invoice.CurrencyId = Convert.ToInt32(dr["CurrencyId"]);
                    }
                    if (dr["InvoiceDirection"] != DBNull.Value)
                    {
                        invoice.InvoiceDirection = Convert.ToInt32(dr["InvoiceDirection"]);
                    }
                    if (dr["OutBlocId"] != DBNull.Value)
                    {
                        invoice.OutBlocId = Convert.ToInt32(dr["OutBlocId"]);
                    }
                    if (dr["OutCorpId"] != DBNull.Value)
                    {
                        invoice.OutCorpId = Convert.ToInt32(dr["OutCorpId"]);
                    }
                    if (dr["OutCorpName"] != DBNull.Value)
                    {
                        invoice.OutCorpName = Convert.ToString(dr["OutCorpName"]);
                    }
                    if (dr["InBlocId"] != DBNull.Value)
                    {
                        invoice.InBlocId = Convert.ToInt32(dr["InBlocId"]);
                    }
                    if (dr["InCorpId"] != DBNull.Value)
                    {
                        invoice.InCorpId = Convert.ToInt32(dr["InCorpId"]);
                    }
                    if (dr["InCorpName"] != DBNull.Value)
                    {
                        invoice.InCorpName = Convert.ToString(dr["InCorpName"]);
                    }
                    if (dr["InvoiceStatus"] != DBNull.Value)
                    {
                        invoice.InvoiceStatus = (Common.StatusEnum)Convert.ToInt32(dr["InvoiceStatus"]);
                    }
                    if (dr["Memo"] != DBNull.Value)
                    {
                        invoice.Memo = Convert.ToString(dr["Memo"]);
                    }
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        invoice.CreatorId = Convert.ToInt32(dr["CreatorId"]);
                    }
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        invoice.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    }
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        invoice.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    }
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        invoice.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    }
                    invoices.Add(invoice);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = invoices;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Model.Invoice invoice = (Model.Invoice)obj;

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

            SqlParameter inviocetypepara = new SqlParameter("@InvoiceType", SqlDbType.Int, 4);
            inviocetypepara.Value = invoice.InvoiceType;
            paras.Add(inviocetypepara);

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
    }
}
