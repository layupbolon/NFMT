/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SummaryDAL.cs
// 文件功能描述：制单dbo.Con_Summary数据交互类。
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
    /// 制单dbo.Con_Summary数据交互类。
    /// </summary>
    public class SummaryDAL : DataOperate, ISummaryDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SummaryDAL()
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
            Summary con_summary = (Summary)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@SummaryId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter summaryapplyidpara = new SqlParameter("@SummaryApplyId", SqlDbType.Int, 4);
            summaryapplyidpara.Value = con_summary.SummaryApplyId;
            paras.Add(summaryapplyidpara);

            SqlParameter summarydatepara = new SqlParameter("@SummaryDate", SqlDbType.DateTime, 8);
            summarydatepara.Value = con_summary.SummaryDate;
            paras.Add(summarydatepara);

            SqlParameter summaryempidpara = new SqlParameter("@SummaryEmpId", SqlDbType.Int, 4);
            summaryempidpara.Value = con_summary.SummaryEmpId;
            paras.Add(summaryempidpara);

            if (!string.IsNullOrEmpty(con_summary.Meno))
            {
                SqlParameter menopara = new SqlParameter("@Meno", SqlDbType.VarChar, 4000);
                menopara.Value = con_summary.Meno;
                paras.Add(menopara);
            }

            SqlParameter summarystatuspara = new SqlParameter("@SummaryStatus", SqlDbType.Int, 4);
            summarystatuspara.Value = con_summary.SummaryStatus;
            paras.Add(summarystatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Summary summary = new Summary();

            int indexSummaryId = dr.GetOrdinal("SummaryId");
            summary.SummaryId = Convert.ToInt32(dr[indexSummaryId]);

            int indexSummaryApplyId = dr.GetOrdinal("SummaryApplyId");
            if (dr["SummaryApplyId"] != DBNull.Value)
            {
                summary.SummaryApplyId = Convert.ToInt32(dr[indexSummaryApplyId]);
            }

            int indexSummaryDate = dr.GetOrdinal("SummaryDate");
            if (dr["SummaryDate"] != DBNull.Value)
            {
                summary.SummaryDate = Convert.ToDateTime(dr[indexSummaryDate]);
            }

            int indexSummaryEmpId = dr.GetOrdinal("SummaryEmpId");
            if (dr["SummaryEmpId"] != DBNull.Value)
            {
                summary.SummaryEmpId = Convert.ToInt32(dr[indexSummaryEmpId]);
            }

            int indexMeno = dr.GetOrdinal("Meno");
            if (dr["Meno"] != DBNull.Value)
            {
                summary.Meno = Convert.ToString(dr[indexMeno]);
            }

            int indexSummaryStatus = dr.GetOrdinal("SummaryStatus");
            if (dr["SummaryStatus"] != DBNull.Value)
            {
                summary.SummaryStatus = Convert.ToInt32(dr[indexSummaryStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                summary.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                summary.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                summary.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                summary.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return summary;
        }

        public override string TableName
        {
            get
            {
                return "Con_Summary";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Summary con_summary = (Summary)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter summaryidpara = new SqlParameter("@SummaryId", SqlDbType.Int, 4);
            summaryidpara.Value = con_summary.SummaryId;
            paras.Add(summaryidpara);

            SqlParameter summaryapplyidpara = new SqlParameter("@SummaryApplyId", SqlDbType.Int, 4);
            summaryapplyidpara.Value = con_summary.SummaryApplyId;
            paras.Add(summaryapplyidpara);

            SqlParameter summarydatepara = new SqlParameter("@SummaryDate", SqlDbType.DateTime, 8);
            summarydatepara.Value = con_summary.SummaryDate;
            paras.Add(summarydatepara);

            SqlParameter summaryempidpara = new SqlParameter("@SummaryEmpId", SqlDbType.Int, 4);
            summaryempidpara.Value = con_summary.SummaryEmpId;
            paras.Add(summaryempidpara);

            if (!string.IsNullOrEmpty(con_summary.Meno))
            {
                SqlParameter menopara = new SqlParameter("@Meno", SqlDbType.VarChar, 4000);
                menopara.Value = con_summary.Meno;
                paras.Add(menopara);
            }

            SqlParameter summarystatuspara = new SqlParameter("@SummaryStatus", SqlDbType.Int, 4);
            summarystatuspara.Value = con_summary.SummaryStatus;
            paras.Add(summarystatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
