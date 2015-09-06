/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentStockDAL.cs
// 文件功能描述：制单库存明细dbo.Doc_DocumentStock数据交互类。
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
    /// 制单库存明细dbo.Doc_DocumentStock数据交互类。
    /// </summary>
    public class DocumentStockDAL : DetailOperate, IDocumentStockDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DocumentStockDAL()
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
            DocumentStock doc_documentstock = (DocumentStock)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter documentidpara = new SqlParameter("@DocumentId", SqlDbType.Int, 4);
            documentidpara.Value = doc_documentstock.DocumentId;
            paras.Add(documentidpara);

            SqlParameter orderidpara = new SqlParameter("@OrderId", SqlDbType.Int, 4);
            orderidpara.Value = doc_documentstock.OrderId;
            paras.Add(orderidpara);

            SqlParameter orderstockdetailidpara = new SqlParameter("@OrderStockDetailId", SqlDbType.Int, 4);
            orderstockdetailidpara.Value = doc_documentstock.OrderStockDetailId;
            paras.Add(orderstockdetailidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = doc_documentstock.StockId;
            paras.Add(stockidpara);

            SqlParameter stocknameidpara = new SqlParameter("@StockNameId", SqlDbType.Int, 4);
            stocknameidpara.Value = doc_documentstock.StockNameId;
            paras.Add(stocknameidpara);

            if (!string.IsNullOrEmpty(doc_documentstock.RefNo))
            {
                SqlParameter refnopara = new SqlParameter("@RefNo", SqlDbType.VarChar, 200);
                refnopara.Value = doc_documentstock.RefNo;
                paras.Add(refnopara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = doc_documentstock.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            DocumentStock documentstock = new DocumentStock();

            int indexDetailId = dr.GetOrdinal("DetailId");
            documentstock.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexDocumentId = dr.GetOrdinal("DocumentId");
            if (dr["DocumentId"] != DBNull.Value)
            {
                documentstock.DocumentId = Convert.ToInt32(dr[indexDocumentId]);
            }

            int indexOrderId = dr.GetOrdinal("OrderId");
            if (dr["OrderId"] != DBNull.Value)
            {
                documentstock.OrderId = Convert.ToInt32(dr[indexOrderId]);
            }

            int indexOrderStockDetailId = dr.GetOrdinal("OrderStockDetailId");
            if (dr["OrderStockDetailId"] != DBNull.Value)
            {
                documentstock.OrderStockDetailId = Convert.ToInt32(dr[indexOrderStockDetailId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                documentstock.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockNameId = dr.GetOrdinal("StockNameId");
            if (dr["StockNameId"] != DBNull.Value)
            {
                documentstock.StockNameId = Convert.ToInt32(dr[indexStockNameId]);
            }

            int indexRefNo = dr.GetOrdinal("RefNo");
            if (dr["RefNo"] != DBNull.Value)
            {
                documentstock.RefNo = Convert.ToString(dr[indexRefNo]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                documentstock.DetailStatus = (StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                documentstock.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                documentstock.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                documentstock.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                documentstock.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return documentstock;
        }

        public override string TableName
        {
            get
            {
                return "Doc_DocumentStock";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            DocumentStock doc_documentstock = (DocumentStock)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = doc_documentstock.DetailId;
            paras.Add(detailidpara);

            SqlParameter documentidpara = new SqlParameter("@DocumentId", SqlDbType.Int, 4);
            documentidpara.Value = doc_documentstock.DocumentId;
            paras.Add(documentidpara);

            SqlParameter orderidpara = new SqlParameter("@OrderId", SqlDbType.Int, 4);
            orderidpara.Value = doc_documentstock.OrderId;
            paras.Add(orderidpara);

            SqlParameter orderstockdetailidpara = new SqlParameter("@OrderStockDetailId", SqlDbType.Int, 4);
            orderstockdetailidpara.Value = doc_documentstock.OrderStockDetailId;
            paras.Add(orderstockdetailidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = doc_documentstock.StockId;
            paras.Add(stockidpara);

            SqlParameter stocknameidpara = new SqlParameter("@StockNameId", SqlDbType.Int, 4);
            stocknameidpara.Value = doc_documentstock.StockNameId;
            paras.Add(stocknameidpara);

            if (!string.IsNullOrEmpty(doc_documentstock.RefNo))
            {
                SqlParameter refnopara = new SqlParameter("@RefNo", SqlDbType.VarChar, 200);
                refnopara.Value = doc_documentstock.RefNo;
                paras.Add(refnopara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = doc_documentstock.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int documentId,NFMT.Common.StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from NFMT.dbo.Doc_DocumentStock where DocumentId ={0} and DetailStatus = {1} ",documentId,(int)status);

            ResultModel result = this.Load<Model.DocumentStock>(user, CommandType.Text, cmdText);

            return result;
        }

        #endregion
    }
}
