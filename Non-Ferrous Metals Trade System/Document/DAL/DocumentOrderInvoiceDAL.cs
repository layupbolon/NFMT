/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentOrderInvoiceDAL.cs
// 文件功能描述：制单指令发票明细dbo.Doc_DocumentOrderInvoice数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
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
    /// 制单指令发票明细dbo.Doc_DocumentOrderInvoice数据交互类。
    /// </summary>
    public class DocumentOrderInvoiceDAL : DetailOperate, IDocumentOrderInvoiceDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DocumentOrderInvoiceDAL()
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
            DocumentOrderInvoice doc_documentorderinvoice = (DocumentOrderInvoice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter orderidpara = new SqlParameter("@OrderId", SqlDbType.Int, 4);
            orderidpara.Value = doc_documentorderinvoice.OrderId;
            paras.Add(orderidpara);

            SqlParameter stockdetailidpara = new SqlParameter("@StockDetailId", SqlDbType.Int, 4);
            stockdetailidpara.Value = doc_documentorderinvoice.StockDetailId;
            paras.Add(stockdetailidpara);

            SqlParameter invoicenopara = new SqlParameter("@InvoiceNo", SqlDbType.VarChar, 200);
            invoicenopara.Value = doc_documentorderinvoice.InvoiceNo;
            paras.Add(invoicenopara);

            SqlParameter invoicebalapara = new SqlParameter("@InvoiceBala", SqlDbType.Decimal, 9);
            invoicebalapara.Value = doc_documentorderinvoice.InvoiceBala;
            paras.Add(invoicebalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = doc_documentorderinvoice.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            DocumentOrderInvoice documentorderinvoice = new DocumentOrderInvoice();

            int indexDetailId = dr.GetOrdinal("DetailId");
            documentorderinvoice.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexOrderId = dr.GetOrdinal("OrderId");
            if (dr["OrderId"] != DBNull.Value)
            {
                documentorderinvoice.OrderId = Convert.ToInt32(dr[indexOrderId]);
            }

            int indexStockDetailId = dr.GetOrdinal("StockDetailId");
            if (dr["StockDetailId"] != DBNull.Value)
            {
                documentorderinvoice.StockDetailId = Convert.ToInt32(dr[indexStockDetailId]);
            }

            int indexInvoiceNo = dr.GetOrdinal("InvoiceNo");
            if (dr["InvoiceNo"] != DBNull.Value)
            {
                documentorderinvoice.InvoiceNo = Convert.ToString(dr[indexInvoiceNo]);
            }

            int indexInvoiceBala = dr.GetOrdinal("InvoiceBala");
            if (dr["InvoiceBala"] != DBNull.Value)
            {
                documentorderinvoice.InvoiceBala = Convert.ToDecimal(dr[indexInvoiceBala]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                documentorderinvoice.DetailStatus = (StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                documentorderinvoice.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                documentorderinvoice.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                documentorderinvoice.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                documentorderinvoice.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return documentorderinvoice;
        }

        public override string TableName
        {
            get
            {
                return "Doc_DocumentOrderInvoice";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            DocumentOrderInvoice doc_documentorderinvoice = (DocumentOrderInvoice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = doc_documentorderinvoice.DetailId;
            paras.Add(detailidpara);

            SqlParameter orderidpara = new SqlParameter("@OrderId", SqlDbType.Int, 4);
            orderidpara.Value = doc_documentorderinvoice.OrderId;
            paras.Add(orderidpara);

            SqlParameter stockdetailidpara = new SqlParameter("@StockDetailId", SqlDbType.Int, 4);
            stockdetailidpara.Value = doc_documentorderinvoice.StockDetailId;
            paras.Add(stockdetailidpara);

            SqlParameter invoicenopara = new SqlParameter("@InvoiceNo", SqlDbType.Int, 4);
            invoicenopara.Value = doc_documentorderinvoice.InvoiceNo;
            paras.Add(invoicenopara);

            SqlParameter invoicebalapara = new SqlParameter("@InvoiceBala", SqlDbType.Decimal, 9);
            invoicebalapara.Value = doc_documentorderinvoice.InvoiceBala;
            paras.Add(invoicebalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = doc_documentorderinvoice.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region

        public ResultModel Load(UserModel user, int orderId, StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.Doc_DocumentOrderInvoice where OrderId={0} and DetailStatus>={1}",orderId,(int)status);
            ResultModel result = this.Load<Model.DocumentOrderInvoice>(user, CommandType.Text, cmdText);
            return result;
        }

        public ResultModel GetByStockDetailId(UserModel user, int orderStockDetailId)
        {
            string cmdText = string.Format("select * from dbo.Doc_DocumentOrderInvoice where DetailStatus >={0} and StockDetailId = {1}",(int)StatusEnum.已生效,orderStockDetailId);

            ResultModel result = this.Get(user, CommandType.Text, cmdText, null);

            return result;
        }

        #endregion
    }
}
