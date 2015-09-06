/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractAttachDAL.cs
// 文件功能描述：合约附件dbo.Con_ContractAttach数据交互类。
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
    /// 合约附件dbo.Con_ContractAttach数据交互类。
    /// </summary>
    public class ContractAttachDAL : DataOperate, IContractAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractAttachDAL()
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
            ContractAttach con_contractattach = (ContractAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ContractAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contractattach.ContractId;
            paras.Add(contractidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = con_contractattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractAttach contractattach = new ContractAttach();

            int indexContractAttachId = dr.GetOrdinal("ContractAttachId");
            contractattach.ContractAttachId = Convert.ToInt32(dr[indexContractAttachId]);

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                contractattach.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                contractattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return contractattach;
        }

        public override string TableName
        {
            get
            {
                return "Con_ContractAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractAttach con_contractattach = (ContractAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter contractattachidpara = new SqlParameter("@ContractAttachId", SqlDbType.Int, 4);
            contractattachidpara.Value = con_contractattach.ContractAttachId;
            paras.Add(contractattachidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contractattach.ContractId;
            paras.Add(contractidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = con_contractattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion

    }
}
