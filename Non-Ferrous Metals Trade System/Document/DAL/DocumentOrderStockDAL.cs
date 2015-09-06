/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentOrderStockDAL.cs
// 文件功能描述：制单指令库存明细dbo.Doc_DocumentOrderStock数据交互类。
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
    /// 制单指令库存明细dbo.Doc_DocumentOrderStock数据交互类。
    /// </summary>
    public class DocumentOrderStockDAL :DetailOperate, IDocumentOrderStockDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DocumentOrderStockDAL()
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
            DocumentOrderStock doc_documentorderstock = (DocumentOrderStock)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter comdetailidpara = new SqlParameter("@ComDetailId", SqlDbType.Int, 4);
            comdetailidpara.Value = doc_documentorderstock.ComDetailId;
            paras.Add(comdetailidpara);

            SqlParameter orderidpara = new SqlParameter("@OrderId", SqlDbType.Int, 4);
            orderidpara.Value = doc_documentorderstock.OrderId;
            paras.Add(orderidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = doc_documentorderstock.StockId;
            paras.Add(stockidpara);

            SqlParameter stocknameidpara = new SqlParameter("@StockNameId", SqlDbType.Int, 4);
            stocknameidpara.Value = doc_documentorderstock.StockNameId;
            paras.Add(stocknameidpara);

            if (!string.IsNullOrEmpty(doc_documentorderstock.RefNo))
            {
                SqlParameter refnopara = new SqlParameter("@RefNo", SqlDbType.VarChar, 200);
                refnopara.Value = doc_documentorderstock.RefNo;
                paras.Add(refnopara);
            }

            SqlParameter applyamountpara = new SqlParameter("@ApplyAmount", SqlDbType.Decimal, 9);
            applyamountpara.Value = doc_documentorderstock.ApplyAmount;
            paras.Add(applyamountpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = doc_documentorderstock.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            DocumentOrderStock documentorderstock = new DocumentOrderStock();

            int indexDetailId = dr.GetOrdinal("DetailId");
            documentorderstock.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexComDetailId = dr.GetOrdinal("ComDetailId");
            if (dr["ComDetailId"] != DBNull.Value)
            {
                documentorderstock.ComDetailId = Convert.ToInt32(dr[indexComDetailId]);
            }

            int indexOrderId = dr.GetOrdinal("OrderId");
            if (dr["OrderId"] != DBNull.Value)
            {
                documentorderstock.OrderId = Convert.ToInt32(dr[indexOrderId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                documentorderstock.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockNameId = dr.GetOrdinal("StockNameId");
            if (dr["StockNameId"] != DBNull.Value)
            {
                documentorderstock.StockNameId = Convert.ToInt32(dr[indexStockNameId]);
            }

            int indexRefNo = dr.GetOrdinal("RefNo");
            if (dr["RefNo"] != DBNull.Value)
            {
                documentorderstock.RefNo = Convert.ToString(dr[indexRefNo]);
            }

            int indexApplyAmount = dr.GetOrdinal("ApplyAmount");
            if (dr["ApplyAmount"] != DBNull.Value)
            {
                documentorderstock.ApplyAmount = Convert.ToDecimal(dr[indexApplyAmount]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                documentorderstock.DetailStatus = (StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                documentorderstock.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                documentorderstock.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                documentorderstock.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                documentorderstock.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return documentorderstock;
        }

        public override string TableName
        {
            get
            {
                return "Doc_DocumentOrderStock";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            DocumentOrderStock doc_documentorderstock = (DocumentOrderStock)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = doc_documentorderstock.DetailId;
            paras.Add(detailidpara);

            SqlParameter comdetailidpara = new SqlParameter("@ComDetailId", SqlDbType.Int, 4);
            comdetailidpara.Value = doc_documentorderstock.ComDetailId;
            paras.Add(comdetailidpara);

            SqlParameter orderidpara = new SqlParameter("@OrderId", SqlDbType.Int, 4);
            orderidpara.Value = doc_documentorderstock.OrderId;
            paras.Add(orderidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = doc_documentorderstock.StockId;
            paras.Add(stockidpara);

            SqlParameter stocknameidpara = new SqlParameter("@StockNameId", SqlDbType.Int, 4);
            stocknameidpara.Value = doc_documentorderstock.StockNameId;
            paras.Add(stocknameidpara);

            if (!string.IsNullOrEmpty(doc_documentorderstock.RefNo))
            {
                SqlParameter refnopara = new SqlParameter("@RefNo", SqlDbType.VarChar, 200);
                refnopara.Value = doc_documentorderstock.RefNo;
                paras.Add(refnopara);
            }

            SqlParameter applyamountpara = new SqlParameter("@ApplyAmount", SqlDbType.Decimal, 9);
            applyamountpara.Value = doc_documentorderstock.ApplyAmount;
            paras.Add(applyamountpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = doc_documentorderstock.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int orderId, StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.Doc_DocumentOrderStock where OrderId={0} and DetailStatus>={1}", orderId, (int)status);
            ResultModel result = this.Load<Model.DocumentOrderStock>(user, CommandType.Text, cmdText);
            return result;
        }

        #endregion
    }
}
