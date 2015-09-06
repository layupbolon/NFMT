/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractSubAttachDAL.cs
// 文件功能描述：子合约附件dbo.Con_ContractSubAttach数据交互类。
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
    /// 子合约附件dbo.Con_ContractSubAttach数据交互类。
    /// </summary>
    public class ContractSubAttachDAL : DataOperate, IContractSubAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractSubAttachDAL()
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
            ContractSubAttach con_contractsubattach = (ContractSubAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@SubAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = con_contractsubattach.SubId;
            paras.Add(subidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = con_contractsubattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractSubAttach contractsubattach = new ContractSubAttach();

            int indexSubAttachId = dr.GetOrdinal("SubAttachId");
            contractsubattach.SubAttachId = Convert.ToInt32(dr[indexSubAttachId]);

            int indexSubId = dr.GetOrdinal("SubId");
            if (dr["SubId"] != DBNull.Value)
            {
                contractsubattach.SubId = Convert.ToInt32(dr[indexSubId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                contractsubattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return contractsubattach;
        }

        public override string TableName
        {
            get
            {
                return "Con_ContractSubAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractSubAttach con_contractsubattach = (ContractSubAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter subattachidpara = new SqlParameter("@SubAttachId", SqlDbType.Int, 4);
            subattachidpara.Value = con_contractsubattach.SubAttachId;
            paras.Add(subattachidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = con_contractsubattach.SubId;
            paras.Add(subidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = con_contractsubattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion
    }
}
