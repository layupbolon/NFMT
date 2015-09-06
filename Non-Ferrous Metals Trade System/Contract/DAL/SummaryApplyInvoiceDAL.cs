/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SummaryApplyInvoiceDAL.cs
// 文件功能描述：制单指令发票关联dbo.Con_SummaryApplyInvoice_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Contract.Model;
using NFMT.DBUtility;
using NFMT.Contract.IDAL;
using NFMT.Common;

namespace NFMT.Contract.DAL
{
    /// <summary>
    /// 制单指令发票关联dbo.Con_SummaryApplyInvoice_Ref数据交互类。
    /// </summary>
    public class SummaryApplyInvoiceDAL : DataOperate, ISummaryApplyInvoiceDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SummaryApplyInvoiceDAL()
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
            SummaryApplyInvoice con_summaryapplyinvoice_ref = (SummaryApplyInvoice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter saidpara = new SqlParameter("@SAId", SqlDbType.Int, 4);
            saidpara.Value = con_summaryapplyinvoice_ref.SAId;
            paras.Add(saidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = con_summaryapplyinvoice_ref.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = con_summaryapplyinvoice_ref.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            SummaryApplyInvoice summaryapplyinvoice = new SummaryApplyInvoice();

            int indexRefId = dr.GetOrdinal("RefId");
            summaryapplyinvoice.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexSAId = dr.GetOrdinal("SAId");
            if (dr["SAId"] != DBNull.Value)
            {
                summaryapplyinvoice.SAId = Convert.ToInt32(dr[indexSAId]);
            }

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            if (dr["InvoiceId"] != DBNull.Value)
            {
                summaryapplyinvoice.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
            }

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                summaryapplyinvoice.RefStatus = Convert.ToInt32(dr[indexRefStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                summaryapplyinvoice.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                summaryapplyinvoice.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                summaryapplyinvoice.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                summaryapplyinvoice.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return summaryapplyinvoice;
        }

        public override string TableName
        {
            get
            {
                return "Con_SummaryApplyInvoice_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SummaryApplyInvoice con_summaryapplyinvoice_ref = (SummaryApplyInvoice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = con_summaryapplyinvoice_ref.RefId;
            paras.Add(refidpara);

            SqlParameter saidpara = new SqlParameter("@SAId", SqlDbType.Int, 4);
            saidpara.Value = con_summaryapplyinvoice_ref.SAId;
            paras.Add(saidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = con_summaryapplyinvoice_ref.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = con_summaryapplyinvoice_ref.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

    }
}
