/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceApplySIDetailDAL.cs
// 文件功能描述：价外票开票申请明细dbo.Inv_InvoiceApplySIDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年7月28日
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
    /// 价外票开票申请明细dbo.Inv_InvoiceApplySIDetail数据交互类。
    /// </summary>
    public partial class InvoiceApplySIDetailDAL : DataOperate, IInvoiceApplySIDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InvoiceApplySIDetailDAL()
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
            InvoiceApplySIDetail inv_invoiceapplysidetail = (InvoiceApplySIDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@SIDetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter invoiceapplyidpara = new SqlParameter("@InvoiceApplyId", SqlDbType.Int, 4);
            invoiceapplyidpara.Value = inv_invoiceapplysidetail.InvoiceApplyId;
            paras.Add(invoiceapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = inv_invoiceapplysidetail.ApplyId;
            paras.Add(applyidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = inv_invoiceapplysidetail.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter siidpara = new SqlParameter("@SIId", SqlDbType.Int, 4);
            siidpara.Value = inv_invoiceapplysidetail.SIId;
            paras.Add(siidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            InvoiceApplySIDetail invoiceapplysidetail = new InvoiceApplySIDetail();

            invoiceapplysidetail.SIDetailId = Convert.ToInt32(dr["SIDetailId"]);

            if (dr["InvoiceApplyId"] != DBNull.Value)
            {
                invoiceapplysidetail.InvoiceApplyId = Convert.ToInt32(dr["InvoiceApplyId"]);
            }

            if (dr["ApplyId"] != DBNull.Value)
            {
                invoiceapplysidetail.ApplyId = Convert.ToInt32(dr["ApplyId"]);
            }

            if (dr["InvoiceId"] != DBNull.Value)
            {
                invoiceapplysidetail.InvoiceId = Convert.ToInt32(dr["InvoiceId"]);
            }

            if (dr["SIId"] != DBNull.Value)
            {
                invoiceapplysidetail.SIId = Convert.ToInt32(dr["SIId"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                invoiceapplysidetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                invoiceapplysidetail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                invoiceapplysidetail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                invoiceapplysidetail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                invoiceapplysidetail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return invoiceapplysidetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            InvoiceApplySIDetail invoiceapplysidetail = new InvoiceApplySIDetail();

            int indexSIDetailId = dr.GetOrdinal("SIDetailId");
            invoiceapplysidetail.SIDetailId = Convert.ToInt32(dr[indexSIDetailId]);

            int indexInvoiceApplyId = dr.GetOrdinal("InvoiceApplyId");
            if (dr["InvoiceApplyId"] != DBNull.Value)
            {
                invoiceapplysidetail.InvoiceApplyId = Convert.ToInt32(dr[indexInvoiceApplyId]);
            }

            int indexApplyId = dr.GetOrdinal("ApplyId");
            if (dr["ApplyId"] != DBNull.Value)
            {
                invoiceapplysidetail.ApplyId = Convert.ToInt32(dr[indexApplyId]);
            }

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            if (dr["InvoiceId"] != DBNull.Value)
            {
                invoiceapplysidetail.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
            }

            int indexSIId = dr.GetOrdinal("SIId");
            if (dr["SIId"] != DBNull.Value)
            {
                invoiceapplysidetail.SIId = Convert.ToInt32(dr[indexSIId]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                invoiceapplysidetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                invoiceapplysidetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                invoiceapplysidetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                invoiceapplysidetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                invoiceapplysidetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return invoiceapplysidetail;
        }

        public override string TableName
        {
            get
            {
                return "Inv_InvoiceApplySIDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            InvoiceApplySIDetail inv_invoiceapplysidetail = (InvoiceApplySIDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter sidetailidpara = new SqlParameter("@SIDetailId", SqlDbType.Int, 4);
            sidetailidpara.Value = inv_invoiceapplysidetail.SIDetailId;
            paras.Add(sidetailidpara);

            SqlParameter invoiceapplyidpara = new SqlParameter("@InvoiceApplyId", SqlDbType.Int, 4);
            invoiceapplyidpara.Value = inv_invoiceapplysidetail.InvoiceApplyId;
            paras.Add(invoiceapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = inv_invoiceapplysidetail.ApplyId;
            paras.Add(applyidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = inv_invoiceapplysidetail.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter siidpara = new SqlParameter("@SIId", SqlDbType.Int, 4);
            siidpara.Value = inv_invoiceapplysidetail.SIId;
            paras.Add(siidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = inv_invoiceapplysidetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetSIIDs(UserModel user, int invoiceApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select SIId from dbo.Inv_InvoiceApplySIDetail where InvoiceApplyId = {0} and DetailStatus >= {1}", invoiceApplyId, (int)Common.StatusEnum.已生效);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                string resultStr = string.Empty;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        resultStr += dr["SIId"].ToString() + ",";
                    }
                    if (!string.IsNullOrEmpty(resultStr) && resultStr.IndexOf(',') > 0)
                        resultStr = resultStr.Substring(0, resultStr.Length - 1);

                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = resultStr;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel InvalidAll(UserModel user, int invoiceApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Inv_InvoiceApplySIDetail set DetailStatus = {0} where InvoiceApplyId = {1} ", (int)Common.StatusEnum.已作废, invoiceApplyId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i >0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "作废失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel Load(UserModel user, int invoiceApplyId, Common.StatusEnum status = StatusEnum.已生效)
        {
            string sql = string.Format("select * from dbo.Inv_InvoiceApplySIDetail where InvoiceApplyId = {0} and DetailStatus = {1}", invoiceApplyId, (int)status);
            return base.Load<Model.InvoiceApplySIDetail>(user, CommandType.Text, sql);
        }

        #endregion
    }
}
