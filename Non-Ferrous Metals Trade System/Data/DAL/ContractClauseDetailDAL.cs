/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractClauseDetailDAL.cs
// 文件功能描述：合约条款明细dbo.ContractClauseDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Data.Model;
using NFMT.DBUtility;
using NFMT.Data.IDAL;
using NFMT.Common;

namespace NFMT.Data.DAL
{
    /// <summary>
    /// 合约条款明细dbo.ContractClauseDetail数据交互类。
    /// </summary>
    public class ContractClauseDetailDAL : DataOperate, IContractClauseDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractClauseDetailDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringBasic;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            ContractClauseDetail contractclausedetail = (ContractClauseDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ClauseDetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter clauseidpara = new SqlParameter("@ClauseId", SqlDbType.Int, 4);
            clauseidpara.Value = contractclausedetail.ClauseId;
            paras.Add(clauseidpara);

            SqlParameter detaildisplaytypepara = new SqlParameter("@DetailDisplayType", SqlDbType.Int, 4);
            detaildisplaytypepara.Value = contractclausedetail.DetailDisplayType;
            paras.Add(detaildisplaytypepara);

            SqlParameter detaildatatypepara = new SqlParameter("@DetailDataType", SqlDbType.Int, 4);
            detaildatatypepara.Value = contractclausedetail.DetailDataType;
            paras.Add(detaildatatypepara);

            SqlParameter formatserialpara = new SqlParameter("@FormatSerial", SqlDbType.Int, 4);
            formatserialpara.Value = contractclausedetail.FormatSerial;
            paras.Add(formatserialpara);

            if (!string.IsNullOrEmpty(contractclausedetail.DetailValue))
            {
                SqlParameter detailvaluepara = new SqlParameter("@DetailValue", SqlDbType.VarChar, 200);
                detailvaluepara.Value = contractclausedetail.DetailValue;
                paras.Add(detailvaluepara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractClauseDetail contractclausedetail = new ContractClauseDetail();

            int indexClauseDetailId = dr.GetOrdinal("ClauseDetailId");
            contractclausedetail.ClauseDetailId = Convert.ToInt32(dr[indexClauseDetailId]);

            int indexClauseId = dr.GetOrdinal("ClauseId");
            if (dr["ClauseId"] != DBNull.Value)
            {
                contractclausedetail.ClauseId = Convert.ToInt32(dr[indexClauseId]);
            }

            int indexDetailDisplayType = dr.GetOrdinal("DetailDisplayType");
            if (dr["DetailDisplayType"] != DBNull.Value)
            {
                contractclausedetail.DetailDisplayType = Convert.ToInt32(dr[indexDetailDisplayType]);
            }

            int indexDetailDataType = dr.GetOrdinal("DetailDataType");
            if (dr["DetailDataType"] != DBNull.Value)
            {
                contractclausedetail.DetailDataType = Convert.ToInt32(dr[indexDetailDataType]);
            }

            int indexFormatSerial = dr.GetOrdinal("FormatSerial");
            if (dr["FormatSerial"] != DBNull.Value)
            {
                contractclausedetail.FormatSerial = Convert.ToInt32(dr[indexFormatSerial]);
            }

            int indexDetailValue = dr.GetOrdinal("DetailValue");
            if (dr["DetailValue"] != DBNull.Value)
            {
                contractclausedetail.DetailValue = Convert.ToString(dr[indexDetailValue]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                contractclausedetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                contractclausedetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                contractclausedetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                contractclausedetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                contractclausedetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return contractclausedetail;
        }

        public override string TableName
        {
            get
            {
                return "ContractClauseDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractClauseDetail contractclausedetail = (ContractClauseDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter clausedetailidpara = new SqlParameter("@ClauseDetailId", SqlDbType.Int, 4);
            clausedetailidpara.Value = contractclausedetail.ClauseDetailId;
            paras.Add(clausedetailidpara);

            SqlParameter clauseidpara = new SqlParameter("@ClauseId", SqlDbType.Int, 4);
            clauseidpara.Value = contractclausedetail.ClauseId;
            paras.Add(clauseidpara);

            SqlParameter detaildisplaytypepara = new SqlParameter("@DetailDisplayType", SqlDbType.Int, 4);
            detaildisplaytypepara.Value = contractclausedetail.DetailDisplayType;
            paras.Add(detaildisplaytypepara);

            SqlParameter detaildatatypepara = new SqlParameter("@DetailDataType", SqlDbType.Int, 4);
            detaildatatypepara.Value = contractclausedetail.DetailDataType;
            paras.Add(detaildatatypepara);

            SqlParameter formatserialpara = new SqlParameter("@FormatSerial", SqlDbType.Int, 4);
            formatserialpara.Value = contractclausedetail.FormatSerial;
            paras.Add(formatserialpara);

            if (!string.IsNullOrEmpty(contractclausedetail.DetailValue))
            {
                SqlParameter detailvaluepara = new SqlParameter("@DetailValue", SqlDbType.VarChar, 200);
                detailvaluepara.Value = contractclausedetail.DetailValue;
                paras.Add(detailvaluepara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = contractclausedetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
