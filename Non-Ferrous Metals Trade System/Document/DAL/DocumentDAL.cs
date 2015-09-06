/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentDAL.cs
// 文件功能描述：制单dbo.Doc_Document数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月20日
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
    /// 制单dbo.Doc_Document数据交互类。
    /// </summary>
    public class DocumentDAL : ExecOperate, IDocumentDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DocumentDAL()
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
            Model.Document doc_document = (Model.Document)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DocumentId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter orderidpara = new SqlParameter("@OrderId", SqlDbType.Int, 4);
            orderidpara.Value = doc_document.OrderId;
            paras.Add(orderidpara);

            SqlParameter documentdatepara = new SqlParameter("@DocumentDate", SqlDbType.DateTime, 8);
            documentdatepara.Value = doc_document.DocumentDate;
            paras.Add(documentdatepara);

            SqlParameter docempidpara = new SqlParameter("@DocEmpId", SqlDbType.Int, 4);
            docempidpara.Value = doc_document.DocEmpId;
            paras.Add(docempidpara);

            SqlParameter presentdatepara = new SqlParameter("@PresentDate", SqlDbType.DateTime, 8);
            presentdatepara.Value = doc_document.PresentDate;
            paras.Add(presentdatepara);

            SqlParameter presenterpara = new SqlParameter("@Presenter", SqlDbType.Int, 4);
            presenterpara.Value = doc_document.Presenter;
            paras.Add(presenterpara);

            SqlParameter acceptancedatepara = new SqlParameter("@AcceptanceDate", SqlDbType.DateTime, 8);
            acceptancedatepara.Value = doc_document.AcceptanceDate;
            paras.Add(acceptancedatepara);

            SqlParameter acceptancerpara = new SqlParameter("@Acceptancer", SqlDbType.Int, 4);
            acceptancerpara.Value = doc_document.Acceptancer;
            paras.Add(acceptancerpara);

            if (!string.IsNullOrEmpty(doc_document.Meno))
            {
                SqlParameter menopara = new SqlParameter("@Meno", SqlDbType.VarChar, 4000);
                menopara.Value = doc_document.Meno;
                paras.Add(menopara);
            }

            SqlParameter documentstatuspara = new SqlParameter("@DocumentStatus", SqlDbType.Int, 4);
            documentstatuspara.Value = doc_document.DocumentStatus;
            paras.Add(documentstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Model.Document document = new Model.Document();

            int indexDocumentId = dr.GetOrdinal("DocumentId");
            document.DocumentId = Convert.ToInt32(dr[indexDocumentId]);

            int indexOrderId = dr.GetOrdinal("OrderId");
            if (dr["OrderId"] != DBNull.Value)
            {
                document.OrderId = Convert.ToInt32(dr[indexOrderId]);
            }

            int indexDocumentDate = dr.GetOrdinal("DocumentDate");
            if (dr["DocumentDate"] != DBNull.Value)
            {
                document.DocumentDate = Convert.ToDateTime(dr[indexDocumentDate]);
            }

            int indexDocEmpId = dr.GetOrdinal("DocEmpId");
            if (dr["DocEmpId"] != DBNull.Value)
            {
                document.DocEmpId = Convert.ToInt32(dr[indexDocEmpId]);
            }

            int indexPresentDate = dr.GetOrdinal("PresentDate");
            if (dr["PresentDate"] != DBNull.Value)
            {
                document.PresentDate = Convert.ToDateTime(dr[indexPresentDate]);
            }

            int indexPresenter = dr.GetOrdinal("Presenter");
            if (dr["Presenter"] != DBNull.Value)
            {
                document.Presenter = Convert.ToInt32(dr[indexPresenter]);
            }

            int indexAcceptanceDate = dr.GetOrdinal("AcceptanceDate");
            if (dr["AcceptanceDate"] != DBNull.Value)
            {
                document.AcceptanceDate = Convert.ToDateTime(dr[indexAcceptanceDate]);
            }

            int indexAcceptancer = dr.GetOrdinal("Acceptancer");
            if (dr["Acceptancer"] != DBNull.Value)
            {
                document.Acceptancer = Convert.ToInt32(dr[indexAcceptancer]);
            }

            int indexMeno = dr.GetOrdinal("Meno");
            if (dr["Meno"] != DBNull.Value)
            {
                document.Meno = Convert.ToString(dr[indexMeno]);
            }

            int indexDocumentStatus = dr.GetOrdinal("DocumentStatus");
            if (dr["DocumentStatus"] != DBNull.Value)
            {
                document.DocumentStatus = (DocumentStatusEnum)Convert.ToInt32(dr[indexDocumentStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                document.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                document.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                document.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                document.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return document;
        }

        public override string TableName
        {
            get
            {
                return "Doc_Document";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Model.Document doc_document = (Model.Document)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter documentidpara = new SqlParameter("@DocumentId", SqlDbType.Int, 4);
            documentidpara.Value = doc_document.DocumentId;
            paras.Add(documentidpara);

            SqlParameter orderidpara = new SqlParameter("@OrderId", SqlDbType.Int, 4);
            orderidpara.Value = doc_document.OrderId;
            paras.Add(orderidpara);

            SqlParameter documentdatepara = new SqlParameter("@DocumentDate", SqlDbType.DateTime, 8);
            documentdatepara.Value = doc_document.DocumentDate;
            paras.Add(documentdatepara);

            SqlParameter docempidpara = new SqlParameter("@DocEmpId", SqlDbType.Int, 4);
            docempidpara.Value = doc_document.DocEmpId;
            paras.Add(docempidpara);

            SqlParameter presentdatepara = new SqlParameter("@PresentDate", SqlDbType.DateTime, 8);
            presentdatepara.Value = doc_document.PresentDate;
            paras.Add(presentdatepara);

            SqlParameter presenterpara = new SqlParameter("@Presenter", SqlDbType.Int, 4);
            presenterpara.Value = doc_document.Presenter;
            paras.Add(presenterpara);

            SqlParameter acceptancedatepara = new SqlParameter("@AcceptanceDate", SqlDbType.DateTime, 8);
            acceptancedatepara.Value = doc_document.AcceptanceDate;
            paras.Add(acceptancedatepara);

            SqlParameter acceptancerpara = new SqlParameter("@Acceptancer", SqlDbType.Int, 4);
            acceptancerpara.Value = doc_document.Acceptancer;
            paras.Add(acceptancerpara);

            if (!string.IsNullOrEmpty(doc_document.Meno))
            {
                SqlParameter menopara = new SqlParameter("@Meno", SqlDbType.VarChar, 4000);
                menopara.Value = doc_document.Meno;
                paras.Add(menopara);
            }

            SqlParameter documentstatuspara = new SqlParameter("@DocumentStatus", SqlDbType.Int, 4);
            documentstatuspara.Value = doc_document.DocumentStatus;
            paras.Add(documentstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public override ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate)
        {
            Model.Document document = obj as Model.Document;

            if (operate == OperateEnum.修改 && (document.DocumentStatus == DocumentStatusEnum.已生效 || document.DocumentStatus == DocumentStatusEnum.已交单 || document.DocumentStatus == DocumentStatusEnum.银行退单)) 
            {
                ResultModel result = new ResultModel();
                result.ResultStatus = 0;
                return result;
            }

            return base.AllowOperate(user, obj, operate);
        }

        /// <summary>
        /// 交单
        /// </summary>
        /// <param name="user"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ResultModel Present(UserModel user, Model.Document obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "交单对象不能为null";
                    result.ResultStatus = -1;
                    return result;
                }

                if (obj.DocumentStatus != DocumentStatusEnum.已生效 && obj.DocumentStatus != DocumentStatusEnum.银行退单)
                {
                    result.ResultStatus = -1;
                    result.Message = "制单非已生效状态，不能交单";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter idPara = new SqlParameter("@id", SqlDbType.Int, 4);
                idPara.Value = obj.Id;
                paras.Add(idPara);

                SqlParameter statusPara = new SqlParameter("@status", SqlDbType.Int, 4);
                statusPara.Value = (int)DocumentStatusEnum.已交单;
                paras.Add(statusPara);

                SqlParameter lastModifyIdPara = new SqlParameter("@lastModifyId", SqlDbType.Int, 4);
                lastModifyIdPara.Value = user.AccountId;
                paras.Add(lastModifyIdPara);

                int i = SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.StoredProcedure, string.Format("{0}UpdateStatus", obj.TableName), paras.ToArray());

                if (i == 1)
                {
                    result.Message = "交单成功";
                    result.ResultStatus = 0;
                }
                else
                    result.Message = "交单失败";

                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 承兑
        /// </summary>
        /// <param name="user"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ResultModel Acceptan(UserModel user, Model.Document obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "交单对象不能为null";
                    result.ResultStatus = -1;
                    return result;
                }

                if (obj.DocumentStatus != DocumentStatusEnum.已交单)
                {
                    result.ResultStatus = -1;
                    result.Message = "制单非已交单状态，不能承兑";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter idPara = new SqlParameter("@id", SqlDbType.Int, 4);
                idPara.Value = obj.Id;
                paras.Add(idPara);

                SqlParameter statusPara = new SqlParameter("@status", SqlDbType.Int, 4);
                statusPara.Value = (int)DocumentStatusEnum.已承兑;
                paras.Add(statusPara);

                SqlParameter lastModifyIdPara = new SqlParameter("@lastModifyId", SqlDbType.Int, 4);
                lastModifyIdPara.Value = user.AccountId;
                paras.Add(lastModifyIdPara);

                int i = SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.StoredProcedure, string.Format("{0}UpdateStatus", obj.TableName), paras.ToArray());

                if (i == 1)
                {
                    result.Message = "承兑成功";
                    result.ResultStatus = 0;
                }
                else
                    result.Message = "承兑失败";

                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 银行单据退回
        /// </summary>
        /// <param name="user"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ResultModel BackDocument(UserModel user, Model.Document obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "交单对象不能为null";
                    result.ResultStatus = -1;
                    return result;
                }

                if (obj.DocumentStatus != DocumentStatusEnum.已交单)
                {
                    result.ResultStatus = -1;
                    result.Message = "制单非已交单状态，不能单据退回";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter idPara = new SqlParameter("@id", SqlDbType.Int, 4);
                idPara.Value = obj.Id;
                paras.Add(idPara);

                SqlParameter statusPara = new SqlParameter("@status", SqlDbType.Int, 4);
                statusPara.Value = (int)DocumentStatusEnum.银行退单;
                paras.Add(statusPara);

                SqlParameter lastModifyIdPara = new SqlParameter("@lastModifyId", SqlDbType.Int, 4);
                lastModifyIdPara.Value = user.AccountId;
                paras.Add(lastModifyIdPara);

                int i = SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.StoredProcedure, string.Format("{0}UpdateStatus", obj.TableName), paras.ToArray());

                if (i == 1)
                {
                    result.Message = "单据退回成功";
                    result.ResultStatus = 0;
                }
                else
                    result.Message = "单据退回失败";

                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }
        
        public override int MenuId
        {
            get
            {
                return 103;
            }
        }

        #endregion
    }
}
