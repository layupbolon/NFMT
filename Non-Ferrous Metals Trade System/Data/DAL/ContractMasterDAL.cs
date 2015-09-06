/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractMasterDAL.cs
// 文件功能描述：合约模板dbo.ContractMaster数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月8日
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
    /// 合约模板dbo.ContractMaster数据交互类。
    /// </summary>
    public class ContractMasterDAL : DataOperate, IContractMasterDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractMasterDAL()
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
            ContractMaster contractmaster = (ContractMaster)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@MasterId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(contractmaster.MasterName))
            {
                SqlParameter masternamepara = new SqlParameter("@MasterName", SqlDbType.VarChar, 200);
                masternamepara.Value = contractmaster.MasterName;
                paras.Add(masternamepara);
            }

            if (!string.IsNullOrEmpty(contractmaster.MasterEname))
            {
                SqlParameter masterenamepara = new SqlParameter("@MasterEname", SqlDbType.VarChar, 200);
                masterenamepara.Value = contractmaster.MasterEname;
                paras.Add(masterenamepara);
            }

            SqlParameter masterstatuspara = new SqlParameter("@MasterStatus", SqlDbType.Int, 4);
            masterstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(masterstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractMaster contractmaster = new ContractMaster();

            int indexMasterId = dr.GetOrdinal("MasterId");
            contractmaster.MasterId = Convert.ToInt32(dr[indexMasterId]);

            int indexMasterName = dr.GetOrdinal("MasterName");
            if (dr["MasterName"] != DBNull.Value)
            {
                contractmaster.MasterName = Convert.ToString(dr[indexMasterName]);
            }

            int indexMasterEname = dr.GetOrdinal("MasterEname");
            if (dr["MasterEname"] != DBNull.Value)
            {
                contractmaster.MasterEname = Convert.ToString(dr[indexMasterEname]);
            }

            int indexMasterStatus = dr.GetOrdinal("MasterStatus");
            if (dr["MasterStatus"] != DBNull.Value)
            {
                contractmaster.MasterStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexMasterStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                contractmaster.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                contractmaster.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                contractmaster.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                contractmaster.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return contractmaster;
        }

        public override string TableName
        {
            get
            {
                return "ContractMaster";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractMaster contractmaster = (ContractMaster)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter masteridpara = new SqlParameter("@MasterId", SqlDbType.Int, 4);
            masteridpara.Value = contractmaster.MasterId;
            paras.Add(masteridpara);

            if (!string.IsNullOrEmpty(contractmaster.MasterName))
            {
                SqlParameter masternamepara = new SqlParameter("@MasterName", SqlDbType.VarChar, 200);
                masternamepara.Value = contractmaster.MasterName;
                paras.Add(masternamepara);
            }

            if (!string.IsNullOrEmpty(contractmaster.MasterEname))
            {
                SqlParameter masterenamepara = new SqlParameter("@MasterEname", SqlDbType.VarChar, 200);
                masterenamepara.Value = contractmaster.MasterEname;
                paras.Add(masterenamepara);
            }

            SqlParameter masterstatuspara = new SqlParameter("@MasterStatus", SqlDbType.Int, 4);
            masterstatuspara.Value = contractmaster.MasterStatus;
            paras.Add(masterstatuspara);

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
                return 77;
            }
        }

        #endregion
    }
}
