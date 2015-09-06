/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractFundsAllotStockDAL.cs
// 文件功能描述：合约款分配至库存dbo.Fun_ContractFundsAllotStock_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Funds.Model;
using NFMT.DBUtility;
using NFMT.Funds.IDAL;
using NFMT.Common;

namespace NFMT.Funds.DAL
{
    /// <summary>
    /// 合约款分配至库存dbo.Fun_ContractFundsAllotStock_Ref数据交互类。
    /// </summary>
    public class ContractFundsAllotStockDAL : DataOperate, IContractFundsAllotStockDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractFundsAllotStockDAL()
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
            ContractFundsAllotStock fun_contractfundsallotstock_ref = (ContractFundsAllotStock)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_contractfundsallotstock_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter receidpara = new SqlParameter("@ReceId", SqlDbType.Int, 4);
            receidpara.Value = fun_contractfundsallotstock_ref.ReceId;
            paras.Add(receidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = fun_contractfundsallotstock_ref.StockId;
            paras.Add(stockidpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_contractfundsallotstock_ref.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_contractfundsallotstock_ref.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractFundsAllotStock contractfundsallotstock = new ContractFundsAllotStock();

            int indexRefId = dr.GetOrdinal("RefId");
            contractfundsallotstock.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexAllotId = dr.GetOrdinal("AllotId");
            if (dr["AllotId"] != DBNull.Value)
            {
                contractfundsallotstock.AllotId = Convert.ToInt32(dr[indexAllotId]);
            }

            int indexReceId = dr.GetOrdinal("ReceId");
            if (dr["ReceId"] != DBNull.Value)
            {
                contractfundsallotstock.ReceId = Convert.ToInt32(dr[indexReceId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                contractfundsallotstock.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexAllotBala = dr.GetOrdinal("AllotBala");
            if (dr["AllotBala"] != DBNull.Value)
            {
                contractfundsallotstock.AllotBala = Convert.ToDecimal(dr[indexAllotBala]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                contractfundsallotstock.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                contractfundsallotstock.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                contractfundsallotstock.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                contractfundsallotstock.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                contractfundsallotstock.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return contractfundsallotstock;
        }

        public override string TableName
        {
            get
            {
                return "Fun_ContractFundsAllotStock_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractFundsAllotStock fun_contractfundsallotstock_ref = (ContractFundsAllotStock)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = fun_contractfundsallotstock_ref.RefId;
            paras.Add(refidpara);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_contractfundsallotstock_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter receidpara = new SqlParameter("@ReceId", SqlDbType.Int, 4);
            receidpara.Value = fun_contractfundsallotstock_ref.ReceId;
            paras.Add(receidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = fun_contractfundsallotstock_ref.StockId;
            paras.Add(stockidpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_contractfundsallotstock_ref.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_contractfundsallotstock_ref.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
