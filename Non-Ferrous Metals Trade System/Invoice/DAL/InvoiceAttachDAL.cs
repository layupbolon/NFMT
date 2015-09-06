/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceAttachDAL.cs
// 文件功能描述：发票附件dbo.InvoiceAttach数据交互类。
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
    /// 发票附件dbo.InvoiceAttach数据交互类。
    /// </summary>
    public class InvoiceAttachDAL : ExecOperate , IInvoiceAttachDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public InvoiceAttachDAL()
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
            InvoiceAttach invoiceattach = (InvoiceAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@InvoiceAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = invoiceattach.AttachId;
            paras.Add(attachidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = invoiceattach.InvoiceId;
            paras.Add(invoiceidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            InvoiceAttach invoiceattach = new InvoiceAttach();

            int indexInvoiceAttachId = dr.GetOrdinal("InvoiceAttachId");
            invoiceattach.InvoiceAttachId = Convert.ToInt32(dr[indexInvoiceAttachId]);

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                invoiceattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            if (dr["InvoiceId"] != DBNull.Value)
            {
                invoiceattach.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
            }


            return invoiceattach;
        }

        public override string TableName
        {
            get
            {
                return "InvoiceAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            InvoiceAttach invoiceattach = (InvoiceAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter invoiceattachidpara = new SqlParameter("@InvoiceAttachId", SqlDbType.Int, 4);
            invoiceattachidpara.Value = invoiceattach.InvoiceAttachId;
            paras.Add(invoiceattachidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = invoiceattach.AttachId;
            paras.Add(attachidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = invoiceattach.InvoiceId;
            paras.Add(invoiceidpara);


            return paras;
        }

        #endregion
    }
}
