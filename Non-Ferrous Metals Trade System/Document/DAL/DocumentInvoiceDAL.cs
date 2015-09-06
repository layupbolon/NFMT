/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentInvoiceDAL.cs
// 文件功能描述：制单发票明细dbo.Doc_DocumentInvoice数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
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
    /// 制单发票明细dbo.Doc_DocumentInvoice数据交互类。
    /// </summary>
    public class DocumentInvoiceDAL : DetailOperate, IDocumentInvoiceDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DocumentInvoiceDAL()
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
            DocumentInvoice doc_documentinvoice = (DocumentInvoice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter documentidpara = new SqlParameter("@DocumentId", SqlDbType.Int, 4);
            documentidpara.Value = doc_documentinvoice.DocumentId;
            paras.Add(documentidpara);

            SqlParameter orderidpara = new SqlParameter("@OrderId", SqlDbType.Int, 4);
            orderidpara.Value = doc_documentinvoice.OrderId;
            paras.Add(orderidpara);

            SqlParameter stockdetailidpara = new SqlParameter("@StockDetailId", SqlDbType.Int, 4);
            stockdetailidpara.Value = doc_documentinvoice.StockDetailId;
            paras.Add(stockdetailidpara);

            SqlParameter orderinvoicedetailidpara = new SqlParameter("@OrderInvoiceDetailId", SqlDbType.Int, 4);
            orderinvoicedetailidpara.Value = doc_documentinvoice.OrderInvoiceDetailId;
            paras.Add(orderinvoicedetailidpara);

            if (!string.IsNullOrEmpty(doc_documentinvoice.InvoiceNo))
            {
                SqlParameter invoicenopara = new SqlParameter("@InvoiceNo", SqlDbType.VarChar, 200);
                invoicenopara.Value = doc_documentinvoice.InvoiceNo;
                paras.Add(invoicenopara);
            }

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = doc_documentinvoice.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter invoicebalapara = new SqlParameter("@InvoiceBala", SqlDbType.Decimal, 9);
            invoicebalapara.Value = doc_documentinvoice.InvoiceBala;
            paras.Add(invoicebalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = doc_documentinvoice.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            DocumentInvoice documentinvoice = new DocumentInvoice();

            int indexDetailId = dr.GetOrdinal("DetailId");
            documentinvoice.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexDocumentId = dr.GetOrdinal("DocumentId");
            if (dr["DocumentId"] != DBNull.Value)
            {
                documentinvoice.DocumentId = Convert.ToInt32(dr[indexDocumentId]);
            }

            int indexOrderId = dr.GetOrdinal("OrderId");
            if (dr["OrderId"] != DBNull.Value)
            {
                documentinvoice.OrderId = Convert.ToInt32(dr[indexOrderId]);
            }

            int indexStockDetailId = dr.GetOrdinal("StockDetailId");
            if (dr["StockDetailId"] != DBNull.Value)
            {
                documentinvoice.StockDetailId = Convert.ToInt32(dr[indexStockDetailId]);
            }

            int indexOrderInvoiceDetailId = dr.GetOrdinal("OrderInvoiceDetailId");
            if (dr["OrderInvoiceDetailId"] != DBNull.Value)
            {
                documentinvoice.OrderInvoiceDetailId = Convert.ToInt32(dr[indexOrderInvoiceDetailId]);
            }

            int indexInvoiceNo = dr.GetOrdinal("InvoiceNo");
            if (dr["InvoiceNo"] != DBNull.Value)
            {
                documentinvoice.InvoiceNo = Convert.ToString(dr[indexInvoiceNo]);
            }

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            if (dr["InvoiceId"] != DBNull.Value)
            {
                documentinvoice.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
            }

            int indexInvoiceBala = dr.GetOrdinal("InvoiceBala");
            if (dr["InvoiceBala"] != DBNull.Value)
            {
                documentinvoice.InvoiceBala = Convert.ToDecimal(dr[indexInvoiceBala]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                documentinvoice.DetailStatus = (StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                documentinvoice.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                documentinvoice.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                documentinvoice.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                documentinvoice.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return documentinvoice;
        }

        public override string TableName
        {
            get
            {
                return "Doc_DocumentInvoice";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            DocumentInvoice doc_documentinvoice = (DocumentInvoice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = doc_documentinvoice.DetailId;
            paras.Add(detailidpara);

            SqlParameter documentidpara = new SqlParameter("@DocumentId", SqlDbType.Int, 4);
            documentidpara.Value = doc_documentinvoice.DocumentId;
            paras.Add(documentidpara);

            SqlParameter orderidpara = new SqlParameter("@OrderId", SqlDbType.Int, 4);
            orderidpara.Value = doc_documentinvoice.OrderId;
            paras.Add(orderidpara);

            SqlParameter stockdetailidpara = new SqlParameter("@StockDetailId", SqlDbType.Int, 4);
            stockdetailidpara.Value = doc_documentinvoice.StockDetailId;
            paras.Add(stockdetailidpara);

            SqlParameter orderinvoicedetailidpara = new SqlParameter("@OrderInvoiceDetailId", SqlDbType.Int, 4);
            orderinvoicedetailidpara.Value = doc_documentinvoice.OrderInvoiceDetailId;
            paras.Add(orderinvoicedetailidpara);

            if (!string.IsNullOrEmpty(doc_documentinvoice.InvoiceNo))
            {
                SqlParameter invoicenopara = new SqlParameter("@InvoiceNo", SqlDbType.VarChar, 200);
                invoicenopara.Value = doc_documentinvoice.InvoiceNo;
                paras.Add(invoicenopara);
            }

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = doc_documentinvoice.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter invoicebalapara = new SqlParameter("@InvoiceBala", SqlDbType.Decimal, 9);
            invoicebalapara.Value = doc_documentinvoice.InvoiceBala;
            paras.Add(invoicebalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = doc_documentinvoice.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int documentId, NFMT.Common.StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from NFMT.dbo.Doc_DocumentInvoice where DocumentId ={0} and DetailStatus = {1} ", documentId, (int)status);

            ResultModel result = this.Load<Model.DocumentInvoice>(user, CommandType.Text, cmdText);

            return result;
        }

        #endregion
    }
}
