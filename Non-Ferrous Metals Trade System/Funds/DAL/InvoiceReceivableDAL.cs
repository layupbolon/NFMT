/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceReceivableDAL.cs
// 文件功能描述：收款分配至发票dbo.Fun_InvoiceReceivable_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Funds.Model;
using NFMT.DBUtility;
using NFMT.Funds.IDAL;
using NFMT.Common;

namespace NFMT.Funds.DAL
{
    /// <summary>
    /// 收款分配至发票dbo.Fun_InvoiceReceivable_Ref数据交互类。
    /// </summary>
    public class InvoiceReceivableDAL : ExecOperate, IInvoiceReceivableDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public InvoiceReceivableDAL()
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
            InvoiceReceivable fun_invoicereceivable_ref = (InvoiceReceivable)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter receidpara = new SqlParameter("@ReceId", SqlDbType.Int, 4);
            receidpara.Value = fun_invoicereceivable_ref.ReceId;
            paras.Add(receidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = fun_invoicereceivable_ref.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = fun_invoicereceivable_ref.DetailId;
            paras.Add(detailidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            InvoiceReceivable invoicereceivable = new InvoiceReceivable();

            int indexRefId = dr.GetOrdinal("RefId");
            invoicereceivable.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexReceId = dr.GetOrdinal("ReceId");
            if (dr["ReceId"] != DBNull.Value)
            {
                invoicereceivable.ReceId = Convert.ToInt32(dr[indexReceId]);
            }

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            if (dr["InvoiceId"] != DBNull.Value)
            {
                invoicereceivable.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
            }

            int indexDetailId = dr.GetOrdinal("DetailId");
            if (dr["DetailId"] != DBNull.Value)
            {
                invoicereceivable.DetailId = Convert.ToInt32(dr[indexDetailId]);
            }


            return invoicereceivable;
        }

        public override string TableName
        {
            get
            {
                return "Fun_InvoiceReceivable_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            InvoiceReceivable fun_invoicereceivable_ref = (InvoiceReceivable)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = fun_invoicereceivable_ref.RefId;
            paras.Add(refidpara);

            SqlParameter receidpara = new SqlParameter("@ReceId", SqlDbType.Int, 4);
            receidpara.Value = fun_invoicereceivable_ref.ReceId;
            paras.Add(receidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = fun_invoicereceivable_ref.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = fun_invoicereceivable_ref.DetailId;
            paras.Add(detailidpara);


            return paras;
        }

        #endregion
    }
}
