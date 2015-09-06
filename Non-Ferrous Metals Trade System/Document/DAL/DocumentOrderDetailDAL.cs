/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentOrderDetailDAL.cs
// 文件功能描述：制单指令明细dbo.Doc_DocumentOrderDetail数据交互类。
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
    /// 制单指令明细dbo.Doc_DocumentOrderDetail数据交互类。
    /// </summary>
    public class DocumentOrderDetailDAL : DetailOperate, IDocumentOrderDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DocumentOrderDetailDAL()
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
            DocumentOrderDetail doc_documentorderdetail = (DocumentOrderDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter orderidpara = new SqlParameter("@OrderId", SqlDbType.Int, 4);
            orderidpara.Value = doc_documentorderdetail.OrderId;
            paras.Add(orderidpara);

            SqlParameter invoicecopiespara = new SqlParameter("@InvoiceCopies", SqlDbType.Int, 4);
            invoicecopiespara.Value = doc_documentorderdetail.InvoiceCopies;
            paras.Add(invoicecopiespara);

            if (!string.IsNullOrEmpty(doc_documentorderdetail.InvoiceSpecific))
            {
                SqlParameter invoicespecificpara = new SqlParameter("@InvoiceSpecific", SqlDbType.VarChar, 2000);
                invoicespecificpara.Value = doc_documentorderdetail.InvoiceSpecific;
                paras.Add(invoicespecificpara);
            }

            SqlParameter qualitycopiespara = new SqlParameter("@QualityCopies", SqlDbType.Int, 4);
            qualitycopiespara.Value = doc_documentorderdetail.QualityCopies;
            paras.Add(qualitycopiespara);

            if (!string.IsNullOrEmpty(doc_documentorderdetail.QualitySpecific))
            {
                SqlParameter qualityspecificpara = new SqlParameter("@QualitySpecific", SqlDbType.VarChar, 2000);
                qualityspecificpara.Value = doc_documentorderdetail.QualitySpecific;
                paras.Add(qualityspecificpara);
            }

            SqlParameter weightcopiespara = new SqlParameter("@WeightCopies", SqlDbType.Int, 4);
            weightcopiespara.Value = doc_documentorderdetail.WeightCopies;
            paras.Add(weightcopiespara);

            if (!string.IsNullOrEmpty(doc_documentorderdetail.WeightSpecific))
            {
                SqlParameter weightspecificpara = new SqlParameter("@WeightSpecific", SqlDbType.VarChar, 2000);
                weightspecificpara.Value = doc_documentorderdetail.WeightSpecific;
                paras.Add(weightspecificpara);
            }

            SqlParameter texcopiespara = new SqlParameter("@TexCopies", SqlDbType.Int, 4);
            texcopiespara.Value = doc_documentorderdetail.TexCopies;
            paras.Add(texcopiespara);

            if (!string.IsNullOrEmpty(doc_documentorderdetail.TexSpecific))
            {
                SqlParameter texspecificpara = new SqlParameter("@TexSpecific", SqlDbType.VarChar, 2000);
                texspecificpara.Value = doc_documentorderdetail.TexSpecific;
                paras.Add(texspecificpara);
            }

            SqlParameter delivercopiespara = new SqlParameter("@DeliverCopies", SqlDbType.Int, 4);
            delivercopiespara.Value = doc_documentorderdetail.DeliverCopies;
            paras.Add(delivercopiespara);

            if (!string.IsNullOrEmpty(doc_documentorderdetail.DeliverSpecific))
            {
                SqlParameter deliverspecificpara = new SqlParameter("@DeliverSpecific", SqlDbType.VarChar, 2000);
                deliverspecificpara.Value = doc_documentorderdetail.DeliverSpecific;
                paras.Add(deliverspecificpara);
            }

            SqlParameter totalinvcopiespara = new SqlParameter("@TotalInvCopies", SqlDbType.Int, 4);
            totalinvcopiespara.Value = doc_documentorderdetail.TotalInvCopies;
            paras.Add(totalinvcopiespara);

            if (!string.IsNullOrEmpty(doc_documentorderdetail.TotalInvSpecific))
            {
                SqlParameter totalinvspecificpara = new SqlParameter("@TotalInvSpecific", SqlDbType.VarChar, 2000);
                totalinvspecificpara.Value = doc_documentorderdetail.TotalInvSpecific;
                paras.Add(totalinvspecificpara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = doc_documentorderdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            DocumentOrderDetail documentorderdetail = new DocumentOrderDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            documentorderdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexOrderId = dr.GetOrdinal("OrderId");
            if (dr["OrderId"] != DBNull.Value)
            {
                documentorderdetail.OrderId = Convert.ToInt32(dr[indexOrderId]);
            }

            int indexInvoiceCopies = dr.GetOrdinal("InvoiceCopies");
            if (dr["InvoiceCopies"] != DBNull.Value)
            {
                documentorderdetail.InvoiceCopies = Convert.ToInt32(dr[indexInvoiceCopies]);
            }

            int indexInvoiceSpecific = dr.GetOrdinal("InvoiceSpecific");
            if (dr["InvoiceSpecific"] != DBNull.Value)
            {
                documentorderdetail.InvoiceSpecific = Convert.ToString(dr[indexInvoiceSpecific]);
            }

            int indexQualityCopies = dr.GetOrdinal("QualityCopies");
            if (dr["QualityCopies"] != DBNull.Value)
            {
                documentorderdetail.QualityCopies = Convert.ToInt32(dr[indexQualityCopies]);
            }

            int indexQualitySpecific = dr.GetOrdinal("QualitySpecific");
            if (dr["QualitySpecific"] != DBNull.Value)
            {
                documentorderdetail.QualitySpecific = Convert.ToString(dr[indexQualitySpecific]);
            }

            int indexWeightCopies = dr.GetOrdinal("WeightCopies");
            if (dr["WeightCopies"] != DBNull.Value)
            {
                documentorderdetail.WeightCopies = Convert.ToInt32(dr[indexWeightCopies]);
            }

            int indexWeightSpecific = dr.GetOrdinal("WeightSpecific");
            if (dr["WeightSpecific"] != DBNull.Value)
            {
                documentorderdetail.WeightSpecific = Convert.ToString(dr[indexWeightSpecific]);
            }

            int indexTexCopies = dr.GetOrdinal("TexCopies");
            if (dr["TexCopies"] != DBNull.Value)
            {
                documentorderdetail.TexCopies = Convert.ToInt32(dr[indexTexCopies]);
            }

            int indexTexSpecific = dr.GetOrdinal("TexSpecific");
            if (dr["TexSpecific"] != DBNull.Value)
            {
                documentorderdetail.TexSpecific = Convert.ToString(dr[indexTexSpecific]);
            }

            int indexDeliverCopies = dr.GetOrdinal("DeliverCopies");
            if (dr["DeliverCopies"] != DBNull.Value)
            {
                documentorderdetail.DeliverCopies = Convert.ToInt32(dr[indexDeliverCopies]);
            }

            int indexDeliverSpecific = dr.GetOrdinal("DeliverSpecific");
            if (dr["DeliverSpecific"] != DBNull.Value)
            {
                documentorderdetail.DeliverSpecific = Convert.ToString(dr[indexDeliverSpecific]);
            }

            int indexTotalInvCopies = dr.GetOrdinal("TotalInvCopies");
            if (dr["TotalInvCopies"] != DBNull.Value)
            {
                documentorderdetail.TotalInvCopies = Convert.ToInt32(dr[indexTotalInvCopies]);
            }

            int indexTotalInvSpecific = dr.GetOrdinal("TotalInvSpecific");
            if (dr["TotalInvSpecific"] != DBNull.Value)
            {
                documentorderdetail.TotalInvSpecific = Convert.ToString(dr[indexTotalInvSpecific]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                documentorderdetail.DetailStatus = (StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                documentorderdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                documentorderdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                documentorderdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                documentorderdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return documentorderdetail;
        }

        public override string TableName
        {
            get
            {
                return "Doc_DocumentOrderDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            DocumentOrderDetail doc_documentorderdetail = (DocumentOrderDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = doc_documentorderdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter orderidpara = new SqlParameter("@OrderId", SqlDbType.Int, 4);
            orderidpara.Value = doc_documentorderdetail.OrderId;
            paras.Add(orderidpara);

            SqlParameter invoicecopiespara = new SqlParameter("@InvoiceCopies", SqlDbType.Int, 4);
            invoicecopiespara.Value = doc_documentorderdetail.InvoiceCopies;
            paras.Add(invoicecopiespara);

            if (!string.IsNullOrEmpty(doc_documentorderdetail.InvoiceSpecific))
            {
                SqlParameter invoicespecificpara = new SqlParameter("@InvoiceSpecific", SqlDbType.VarChar, 2000);
                invoicespecificpara.Value = doc_documentorderdetail.InvoiceSpecific;
                paras.Add(invoicespecificpara);
            }

            SqlParameter qualitycopiespara = new SqlParameter("@QualityCopies", SqlDbType.Int, 4);
            qualitycopiespara.Value = doc_documentorderdetail.QualityCopies;
            paras.Add(qualitycopiespara);

            if (!string.IsNullOrEmpty(doc_documentorderdetail.QualitySpecific))
            {
                SqlParameter qualityspecificpara = new SqlParameter("@QualitySpecific", SqlDbType.VarChar, 2000);
                qualityspecificpara.Value = doc_documentorderdetail.QualitySpecific;
                paras.Add(qualityspecificpara);
            }

            SqlParameter weightcopiespara = new SqlParameter("@WeightCopies", SqlDbType.Int, 4);
            weightcopiespara.Value = doc_documentorderdetail.WeightCopies;
            paras.Add(weightcopiespara);

            if (!string.IsNullOrEmpty(doc_documentorderdetail.WeightSpecific))
            {
                SqlParameter weightspecificpara = new SqlParameter("@WeightSpecific", SqlDbType.VarChar, 2000);
                weightspecificpara.Value = doc_documentorderdetail.WeightSpecific;
                paras.Add(weightspecificpara);
            }

            SqlParameter texcopiespara = new SqlParameter("@TexCopies", SqlDbType.Int, 4);
            texcopiespara.Value = doc_documentorderdetail.TexCopies;
            paras.Add(texcopiespara);

            if (!string.IsNullOrEmpty(doc_documentorderdetail.TexSpecific))
            {
                SqlParameter texspecificpara = new SqlParameter("@TexSpecific", SqlDbType.VarChar, 2000);
                texspecificpara.Value = doc_documentorderdetail.TexSpecific;
                paras.Add(texspecificpara);
            }

            SqlParameter delivercopiespara = new SqlParameter("@DeliverCopies", SqlDbType.Int, 4);
            delivercopiespara.Value = doc_documentorderdetail.DeliverCopies;
            paras.Add(delivercopiespara);

            if (!string.IsNullOrEmpty(doc_documentorderdetail.DeliverSpecific))
            {
                SqlParameter deliverspecificpara = new SqlParameter("@DeliverSpecific", SqlDbType.VarChar, 2000);
                deliverspecificpara.Value = doc_documentorderdetail.DeliverSpecific;
                paras.Add(deliverspecificpara);
            }

            SqlParameter totalinvcopiespara = new SqlParameter("@TotalInvCopies", SqlDbType.Int, 4);
            totalinvcopiespara.Value = doc_documentorderdetail.TotalInvCopies;
            paras.Add(totalinvcopiespara);

            if (!string.IsNullOrEmpty(doc_documentorderdetail.TotalInvSpecific))
            {
                SqlParameter totalinvspecificpara = new SqlParameter("@TotalInvSpecific", SqlDbType.VarChar, 2000);
                totalinvspecificpara.Value = doc_documentorderdetail.TotalInvSpecific;
                paras.Add(totalinvspecificpara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = doc_documentorderdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetByOrderId(UserModel user, int orderId)
        {
            ResultModel result = new ResultModel();

            if (orderId < 1)
            {
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@orderId", SqlDbType.Int, 4);
            para.Value = orderId;
            paras.Add(para);

            para = new SqlParameter("@status", SqlDbType.Int, 4);
            para.Value = (int)StatusEnum.已生效;
            paras.Add(para);

            SqlDataReader dr = null;
            try
            {
                string cmdText = "select * from dbo.Doc_DocumentOrderDetail where OrderId =@orderId and DetailStatus >=@status";
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, paras.ToArray());

                IModel model = null;

                if (dr.Read())
                {
                    model = CreateModel(dr);

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = model;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
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
