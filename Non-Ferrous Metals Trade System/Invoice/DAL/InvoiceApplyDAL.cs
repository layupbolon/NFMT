/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceApplyDAL.cs
// 文件功能描述：开票申请dbo.Inv_InvoiceApply数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年1月28日
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
    /// 开票申请dbo.Inv_InvoiceApply数据交互类。
    /// </summary>
    public partial class InvoiceApplyDAL : DataOperate, IInvoiceApplyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InvoiceApplyDAL()
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
            InvoiceApply inv_invoiceapply = (InvoiceApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@InvoiceApplyId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = inv_invoiceapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter totalbalapara = new SqlParameter("@TotalBala", SqlDbType.Decimal, 9);
            totalbalapara.Value = inv_invoiceapply.TotalBala;
            paras.Add(totalbalapara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            InvoiceApply invoiceapply = new InvoiceApply();

            invoiceapply.InvoiceApplyId = Convert.ToInt32(dr["InvoiceApplyId"]);

            if (dr["ApplyId"] != DBNull.Value)
            {
                invoiceapply.ApplyId = Convert.ToInt32(dr["ApplyId"]);
            }

            if (dr["TotalBala"] != DBNull.Value)
            {
                invoiceapply.TotalBala = Convert.ToDecimal(dr["TotalBala"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                invoiceapply.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                invoiceapply.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                invoiceapply.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                invoiceapply.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return invoiceapply;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            InvoiceApply invoiceapply = new InvoiceApply();

            int indexInvoiceApplyId = dr.GetOrdinal("InvoiceApplyId");
            invoiceapply.InvoiceApplyId = Convert.ToInt32(dr[indexInvoiceApplyId]);

            int indexApplyId = dr.GetOrdinal("ApplyId");
            if (dr["ApplyId"] != DBNull.Value)
            {
                invoiceapply.ApplyId = Convert.ToInt32(dr[indexApplyId]);
            }

            int indexTotalBala = dr.GetOrdinal("TotalBala");
            if (dr["TotalBala"] != DBNull.Value)
            {
                invoiceapply.TotalBala = Convert.ToDecimal(dr[indexTotalBala]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                invoiceapply.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                invoiceapply.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                invoiceapply.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                invoiceapply.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return invoiceapply;
        }

        public override string TableName
        {
            get
            {
                return "Inv_InvoiceApply";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            InvoiceApply inv_invoiceapply = (InvoiceApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter invoiceapplyidpara = new SqlParameter("@InvoiceApplyId", SqlDbType.Int, 4);
            invoiceapplyidpara.Value = inv_invoiceapply.InvoiceApplyId;
            paras.Add(invoiceapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = inv_invoiceapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter totalbalapara = new SqlParameter("@TotalBala", SqlDbType.Decimal, 9);
            totalbalapara.Value = inv_invoiceapply.TotalBala;
            paras.Add(totalbalapara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetBIidsByInvApplyId(UserModel user, int invApplyId)
        {
            ResultModel result = new ResultModel();
            try
            {
                string sql = string.Format("select distinct BussinessInvoiceId from Inv_InvoiceApplyDetail where InvoiceApplyId = {0} and DetailStatus >= {1}", invApplyId, (int)Common.StatusEnum.已生效);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string ids = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        ids += dr["BussinessInvoiceId"].ToString() + ",";
                    }
                    if (!string.IsNullOrEmpty(ids) && ids.IndexOf(',') > 0)
                        ids = ids.Substring(0, ids.Length - 1);

                    result.ReturnValue = ids;
                    result.Message = "获取成功";
                    result.ResultStatus = 0;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        public ResultModel GetBIidsByInvApplyIdExceptFinInvoice(UserModel user, int invApplyId, int financeInvoiceId)
        {
            ResultModel result = new ResultModel();
            try
            {
                string sql = string.Format("select distinct BussinessInvoiceId from Inv_InvoiceApplyDetail where InvoiceApplyId = {0} and DetailStatus >= {1} and BussinessInvoiceId not in (select BusinessInvoiceId from dbo.Inv_FinBusInvAllotDetail where FinanceInvoiceId = {2} and DetailStatus = {1})", invApplyId, (int)Common.StatusEnum.已生效, financeInvoiceId);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string ids = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        ids += dr["BussinessInvoiceId"].ToString() + ",";
                    }
                    if (!string.IsNullOrEmpty(ids) && ids.IndexOf(',') > 0)
                        ids = ids.Substring(0, ids.Length - 1);

                    result.ReturnValue = ids;
                    result.Message = "获取成功";

                }

                result.ResultStatus = 0;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        public ResultModel GetDtForExport(UserModel user, string invoiceApplyIds)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(
                    "select con.ContractDate,con.OutContractNo,corp.CorpName,Sum(bi.NetAmount) NetAmount,bi.UnitPrice,Sum(inv.InvoiceBala) InvoiceBala ");
                sb.Append(" from dbo.Inv_InvoiceApplyDetail iad ");
                sb.AppendFormat(" inner join dbo.Apply a on iad.ApplyId = a.ApplyId and a.ApplyStatus = {0} ",
                    (int) StatusEnum.已生效);
                sb.Append(" left join dbo.Con_Contract con on iad.ContractId = con.ContractId ");
                sb.Append(" left join dbo.Invoice inv on iad.InvoiceId = inv.InvoiceId ");
                sb.Append(" left join NFMT_User.dbo.Corporation corp on corp.CorpId = inv.OutCorpId ");
                sb.Append(" left join dbo.Inv_BusinessInvoice bi on iad.BussinessInvoiceId = bi.BusinessInvoiceId ");
                sb.AppendFormat(" where iad.InvoiceApplyId in ({0}) ", invoiceApplyIds);
                sb.Append(" group by con.ContractDate,con.OutContractNo,corp.CorpName,bi.UnitPrice ");
                sb.Append(" order by con.ContractDate ");

                DataTable dt = SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null,
                    CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = dt;
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

        #endregion
    }
}
