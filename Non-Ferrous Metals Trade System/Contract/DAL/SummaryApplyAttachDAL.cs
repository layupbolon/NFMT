/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SummaryApplyAttachDAL.cs
// 文件功能描述：制单指令附件dbo.Con_SummaryApplyAttach数据交互类。
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
    /// 制单指令附件dbo.Con_SummaryApplyAttach数据交互类。
    /// </summary>
    public class SummaryApplyAttachDAL : DataOperate, ISummaryApplyAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SummaryApplyAttachDAL()
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
            SummaryApplyAttach con_summaryapplyattach = (SummaryApplyAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@SAAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter summaryapplyidpara = new SqlParameter("@SummaryApplyId", SqlDbType.Int, 4);
            summaryapplyidpara.Value = con_summaryapplyattach.SummaryApplyId;
            paras.Add(summaryapplyidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = con_summaryapplyattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            SummaryApplyAttach summaryapplyattach = new SummaryApplyAttach();

            int indexSAAttachId = dr.GetOrdinal("SAAttachId");
            summaryapplyattach.SAAttachId = Convert.ToInt32(dr[indexSAAttachId]);

            int indexSummaryApplyId = dr.GetOrdinal("SummaryApplyId");
            if (dr["SummaryApplyId"] != DBNull.Value)
            {
                summaryapplyattach.SummaryApplyId = Convert.ToInt32(dr[indexSummaryApplyId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                summaryapplyattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return summaryapplyattach;
        }

        public override string TableName
        {
            get
            {
                return "Con_SummaryApplyAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SummaryApplyAttach con_summaryapplyattach = (SummaryApplyAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter saattachidpara = new SqlParameter("@SAAttachId", SqlDbType.Int, 4);
            saattachidpara.Value = con_summaryapplyattach.SAAttachId;
            paras.Add(saattachidpara);

            SqlParameter summaryapplyidpara = new SqlParameter("@SummaryApplyId", SqlDbType.Int, 4);
            summaryapplyidpara.Value = con_summaryapplyattach.SummaryApplyId;
            paras.Add(summaryapplyidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = con_summaryapplyattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion
    }
}
