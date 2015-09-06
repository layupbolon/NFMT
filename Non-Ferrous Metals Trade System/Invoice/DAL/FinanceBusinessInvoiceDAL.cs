/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FinanceBusinessInvoiceDAL.cs
// 文件功能描述：业务发票财务发票关联表dbo.Inv_FinanceBusinessInvoice_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年8月25日
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
    /// 业务发票财务发票关联表dbo.Inv_FinanceBusinessInvoice_Ref数据交互类。
    /// </summary>
    public class FinanceBusinessInvoiceDAL : ExecOperate, IFinanceBusinessInvoiceDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public FinanceBusinessInvoiceDAL()
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
            FinanceBusinessInvoice inv_financebusinessinvoice_ref = (FinanceBusinessInvoice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter businessinvoiceidpara = new SqlParameter("@BusinessInvoiceId", SqlDbType.Int, 4);
            businessinvoiceidpara.Value = inv_financebusinessinvoice_ref.BusinessInvoiceId;
            paras.Add(businessinvoiceidpara);

            SqlParameter financeinvoiceidpara = new SqlParameter("@FinanceInvoiceId", SqlDbType.Int, 4);
            financeinvoiceidpara.Value = inv_financebusinessinvoice_ref.FinanceInvoiceId;
            paras.Add(financeinvoiceidpara);

            SqlParameter balapara = new SqlParameter("@Bala", SqlDbType.Decimal, 9);
            balapara.Value = inv_financebusinessinvoice_ref.Bala;
            paras.Add(balapara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            FinanceBusinessInvoice financebusinessinvoice = new FinanceBusinessInvoice();

            int indexRefId = dr.GetOrdinal("RefId");
            financebusinessinvoice.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexBusinessInvoiceId = dr.GetOrdinal("BusinessInvoiceId");
            if (dr["BusinessInvoiceId"] != DBNull.Value)
            {
                financebusinessinvoice.BusinessInvoiceId = Convert.ToInt32(dr[indexBusinessInvoiceId]);
            }

            int indexFinanceInvoiceId = dr.GetOrdinal("FinanceInvoiceId");
            if (dr["FinanceInvoiceId"] != DBNull.Value)
            {
                financebusinessinvoice.FinanceInvoiceId = Convert.ToInt32(dr[indexFinanceInvoiceId]);
            }

            int indexBala = dr.GetOrdinal("Bala");
            if (dr["Bala"] != DBNull.Value)
            {
                financebusinessinvoice.Bala = Convert.ToDecimal(dr[indexBala]);
            }


            return financebusinessinvoice;
        }

        public override string TableName
        {
            get
            {
                return "Inv_FinanceBusinessInvoice_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            FinanceBusinessInvoice inv_financebusinessinvoice_ref = (FinanceBusinessInvoice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = inv_financebusinessinvoice_ref.RefId;
            paras.Add(refidpara);

            SqlParameter businessinvoiceidpara = new SqlParameter("@BusinessInvoiceId", SqlDbType.Int, 4);
            businessinvoiceidpara.Value = inv_financebusinessinvoice_ref.BusinessInvoiceId;
            paras.Add(businessinvoiceidpara);

            SqlParameter financeinvoiceidpara = new SqlParameter("@FinanceInvoiceId", SqlDbType.Int, 4);
            financeinvoiceidpara.Value = inv_financebusinessinvoice_ref.FinanceInvoiceId;
            paras.Add(financeinvoiceidpara);

            SqlParameter balapara = new SqlParameter("@Bala", SqlDbType.Decimal, 9);
            balapara.Value = inv_financebusinessinvoice_ref.Bala;
            paras.Add(balapara);


            return paras;
        }

        #endregion
    }
}
