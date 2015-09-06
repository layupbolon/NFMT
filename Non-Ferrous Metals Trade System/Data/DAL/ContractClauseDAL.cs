/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractClauseDAL.cs
// 文件功能描述：合约条款dbo.ContractClause数据交互类。
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
    /// 合约条款dbo.ContractClause数据交互类。
    /// </summary>
    public class ContractClauseDAL : DataOperate, IContractClauseDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractClauseDAL()
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
            ContractClause contractclause = (ContractClause)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ClauseId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(contractclause.ClauseText))
            {
                SqlParameter clausetextpara = new SqlParameter("@ClauseText", SqlDbType.VarChar, -1);
                clausetextpara.Value = contractclause.ClauseText;
                paras.Add(clausetextpara);
            }

            if (!string.IsNullOrEmpty(contractclause.ClauseEnText))
            {
                SqlParameter clauseentextpara = new SqlParameter("@ClauseEnText", SqlDbType.VarChar, -1);
                clauseentextpara.Value = contractclause.ClauseEnText;
                paras.Add(clauseentextpara);
            }

            SqlParameter clausestatuspara = new SqlParameter("@ClauseStatus", SqlDbType.Int, 4);
            clausestatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(clausestatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractClause contractclause = new ContractClause();

            int indexClauseId = dr.GetOrdinal("ClauseId");
            contractclause.ClauseId = Convert.ToInt32(dr[indexClauseId]);

            int indexClauseText = dr.GetOrdinal("ClauseText");
            if (dr["ClauseText"] != DBNull.Value)
            {
                contractclause.ClauseText = Convert.ToString(dr[indexClauseText]);
            }

            int indexClauseEnText = dr.GetOrdinal("ClauseEnText");
            if (dr["ClauseEnText"] != DBNull.Value)
            {
                contractclause.ClauseEnText = Convert.ToString(dr[indexClauseEnText]);
            }

            int indexClauseStatus = dr.GetOrdinal("ClauseStatus");
            if (dr["ClauseStatus"] != DBNull.Value)
            {
                contractclause.ClauseStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexClauseStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                contractclause.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                contractclause.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                contractclause.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                contractclause.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return contractclause;
        }

        public override string TableName
        {
            get
            {
                return "dbo.ContractClause";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractClause contractclause = (ContractClause)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter clauseidpara = new SqlParameter("@ClauseId", SqlDbType.Int, 4);
            clauseidpara.Value = contractclause.ClauseId;
            paras.Add(clauseidpara);

            if (!string.IsNullOrEmpty(contractclause.ClauseText))
            {
                SqlParameter clausetextpara = new SqlParameter("@ClauseText", SqlDbType.VarChar, -1);
                clausetextpara.Value = contractclause.ClauseText;
                paras.Add(clausetextpara);
            }

            if (!string.IsNullOrEmpty(contractclause.ClauseEnText))
            {
                SqlParameter clauseentextpara = new SqlParameter("@ClauseEnText", SqlDbType.VarChar, -1);
                clauseentextpara.Value = contractclause.ClauseEnText;
                paras.Add(clauseentextpara);
            }

            SqlParameter clausestatuspara = new SqlParameter("@ClauseStatus", SqlDbType.Int, 4);
            clausestatuspara.Value = contractclause.ClauseStatus;
            paras.Add(clausestatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public override int MenuId
        {
            get
            {
                return 78;
            }
        }

        #endregion
    }
}
