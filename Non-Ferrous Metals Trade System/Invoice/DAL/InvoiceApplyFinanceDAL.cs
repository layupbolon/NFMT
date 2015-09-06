/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceApplyFinanceDAL.cs
// 文件功能描述：开票申请与财务票关联表dbo.Inv_InvoiceApplyFinance数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年7月21日
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
    /// 开票申请与财务票关联表dbo.Inv_InvoiceApplyFinance数据交互类。
    /// </summary>
    public partial class InvoiceApplyFinanceDAL : DataOperate, IInvoiceApplyFinanceDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InvoiceApplyFinanceDAL()
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
            InvoiceApplyFinance inv_invoiceapplyfinance = (InvoiceApplyFinance)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = inv_invoiceapplyfinance.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter financeinvoiceidpara = new SqlParameter("@FinanceInvoiceId", SqlDbType.Int, 4);
            financeinvoiceidpara.Value = inv_invoiceapplyfinance.FinanceInvoiceId;
            paras.Add(financeinvoiceidpara);

            SqlParameter invoiceapplyidpara = new SqlParameter("@InvoiceApplyId", SqlDbType.Int, 4);
            invoiceapplyidpara.Value = inv_invoiceapplyfinance.InvoiceApplyId;
            paras.Add(invoiceapplyidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(refstatuspara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            InvoiceApplyFinance invoiceapplyfinance = new InvoiceApplyFinance();

            invoiceapplyfinance.RefId = Convert.ToInt32(dr["RefId"]);

            if (dr["InvoiceId"] != DBNull.Value)
            {
                invoiceapplyfinance.InvoiceId = Convert.ToInt32(dr["InvoiceId"]);
            }

            if (dr["FinanceInvoiceId"] != DBNull.Value)
            {
                invoiceapplyfinance.FinanceInvoiceId = Convert.ToInt32(dr["FinanceInvoiceId"]);
            }

            if (dr["InvoiceApplyId"] != DBNull.Value)
            {
                invoiceapplyfinance.InvoiceApplyId = Convert.ToInt32(dr["InvoiceApplyId"]);
            }

            if (dr["RefStatus"] != DBNull.Value)
            {
                invoiceapplyfinance.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr["RefStatus"]);
            }


            return invoiceapplyfinance;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            InvoiceApplyFinance invoiceapplyfinance = new InvoiceApplyFinance();

            int indexRefId = dr.GetOrdinal("RefId");
            invoiceapplyfinance.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            if (dr["InvoiceId"] != DBNull.Value)
            {
                invoiceapplyfinance.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
            }

            int indexFinanceInvoiceId = dr.GetOrdinal("FinanceInvoiceId");
            if (dr["FinanceInvoiceId"] != DBNull.Value)
            {
                invoiceapplyfinance.FinanceInvoiceId = Convert.ToInt32(dr[indexFinanceInvoiceId]);
            }

            int indexInvoiceApplyId = dr.GetOrdinal("InvoiceApplyId");
            if (dr["InvoiceApplyId"] != DBNull.Value)
            {
                invoiceapplyfinance.InvoiceApplyId = Convert.ToInt32(dr[indexInvoiceApplyId]);
            }

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                invoiceapplyfinance.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRefStatus]);
            }


            return invoiceapplyfinance;
        }

        public override string TableName
        {
            get
            {
                return "Inv_InvoiceApplyFinance";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            InvoiceApplyFinance inv_invoiceapplyfinance = (InvoiceApplyFinance)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = inv_invoiceapplyfinance.RefId;
            paras.Add(refidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = inv_invoiceapplyfinance.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter financeinvoiceidpara = new SqlParameter("@FinanceInvoiceId", SqlDbType.Int, 4);
            financeinvoiceidpara.Value = inv_invoiceapplyfinance.FinanceInvoiceId;
            paras.Add(financeinvoiceidpara);

            SqlParameter invoiceapplyidpara = new SqlParameter("@InvoiceApplyId", SqlDbType.Int, 4);
            invoiceapplyidpara.Value = inv_invoiceapplyfinance.InvoiceApplyId;
            paras.Add(invoiceapplyidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = inv_invoiceapplyfinance.RefStatus;
            paras.Add(refstatuspara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetInvApplyIdByFinInvId(UserModel user, int finacneInvoiceId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select top 1 InvoiceApplyId from dbo.Inv_InvoiceApplyFinance where FinanceInvoiceId = {0} and RefStatus >={1}", finacneInvoiceId, (int)Common.StatusEnum.已生效);
                int invoiceApplyId = 0;
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && int.TryParse(obj.ToString(), out invoiceApplyId))
                {
                    result.ReturnValue = invoiceApplyId;
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
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

        #endregion
    }
}
