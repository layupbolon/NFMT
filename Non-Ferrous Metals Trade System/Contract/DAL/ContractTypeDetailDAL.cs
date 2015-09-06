/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractTypeDetailDAL.cs
// 文件功能描述：合约类型明细dbo.Con_ContractTypeDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
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
    /// 合约类型明细dbo.Con_ContractTypeDetail数据交互类。
    /// </summary>
    public partial class ContractTypeDetailDAL : DataOperate, IContractTypeDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractTypeDetailDAL()
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
            ContractTypeDetail con_contracttypedetail = (ContractTypeDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contracttypedetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter contracttypepara = new SqlParameter("@ContractType", SqlDbType.Int, 4);
            contracttypepara.Value = con_contracttypedetail.ContractType;
            paras.Add(contracttypepara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = con_contracttypedetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractTypeDetail contracttypedetail = new ContractTypeDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            contracttypedetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                contracttypedetail.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexContractType = dr.GetOrdinal("ContractType");
            if (dr["ContractType"] != DBNull.Value)
            {
                contracttypedetail.ContractType = Convert.ToInt32(dr[indexContractType]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                contracttypedetail.DetailStatus = (StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                contracttypedetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                contracttypedetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                contracttypedetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                contracttypedetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return contracttypedetail;
        }

        public override string TableName
        {
            get
            {
                return "Con_ContractTypeDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractTypeDetail con_contracttypedetail = (ContractTypeDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = con_contracttypedetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contracttypedetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter contracttypepara = new SqlParameter("@ContractType", SqlDbType.Int, 4);
            contracttypepara.Value = con_contracttypedetail.ContractType;
            paras.Add(contracttypepara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = con_contracttypedetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
