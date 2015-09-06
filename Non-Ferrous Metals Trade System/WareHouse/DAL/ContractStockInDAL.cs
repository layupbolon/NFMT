/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractStockInDAL.cs
// 文件功能描述：入库登记合约关联dbo.St_ContractStockIn_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年11月6日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.WareHouse.Model;
using NFMT.DBUtility;
using NFMT.WareHouse.IDAL;
using NFMT.Common;

namespace NFMT.WareHouse.DAL
{
    /// <summary>
    /// 入库登记合约关联dbo.St_ContractStockIn_Ref数据交互类。
    /// </summary>
    public partial class ContractStockInDAL : ExecOperate, IContractStockInDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractStockInDAL()
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
            ContractStockIn st_contractstockin_ref = (ContractStockIn)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stockinidpara = new SqlParameter("@StockInId", SqlDbType.Int, 4);
            stockinidpara.Value = st_contractstockin_ref.StockInId;
            paras.Add(stockinidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = st_contractstockin_ref.ContractId;
            paras.Add(contractidpara);

            SqlParameter contractsubidpara = new SqlParameter("@ContractSubId", SqlDbType.Int, 4);
            contractsubidpara.Value = st_contractstockin_ref.ContractSubId;
            paras.Add(contractsubidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = st_contractstockin_ref.RefStatus;
            paras.Add(refstatuspara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractStockIn contractstockin = new ContractStockIn();

            int indexRefId = dr.GetOrdinal("RefId");
            contractstockin.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexStockInId = dr.GetOrdinal("StockInId");
            if (dr["StockInId"] != DBNull.Value)
            {
                contractstockin.StockInId = Convert.ToInt32(dr[indexStockInId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                contractstockin.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexContractSubId = dr.GetOrdinal("ContractSubId");
            if (dr["ContractSubId"] != DBNull.Value)
            {
                contractstockin.ContractSubId = Convert.ToInt32(dr[indexContractSubId]);
            }

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                contractstockin.RefStatus = (StatusEnum)Convert.ToInt32(dr[indexRefStatus]);
            }


            return contractstockin;
        }

        public override string TableName
        {
            get
            {
                return "St_ContractStockIn_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractStockIn st_contractstockin_ref = (ContractStockIn)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = st_contractstockin_ref.RefId;
            paras.Add(refidpara);

            SqlParameter stockinidpara = new SqlParameter("@StockInId", SqlDbType.Int, 4);
            stockinidpara.Value = st_contractstockin_ref.StockInId;
            paras.Add(stockinidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = st_contractstockin_ref.ContractId;
            paras.Add(contractidpara);

            SqlParameter contractsubidpara = new SqlParameter("@ContractSubId", SqlDbType.Int, 4);
            contractsubidpara.Value = st_contractstockin_ref.ContractSubId;
            paras.Add(contractsubidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = st_contractstockin_ref.RefStatus;
            paras.Add(refstatuspara);


            return paras;
        }

        #endregion        
    }
}
