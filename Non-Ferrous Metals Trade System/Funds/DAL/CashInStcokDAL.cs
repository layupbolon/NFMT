/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInStcokDAL.cs
// 文件功能描述：收款分配至库存dbo.Fun_CashInStcok_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年3月19日
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
    /// 收款分配至库存dbo.Fun_CashInStcok_Ref数据交互类。
    /// </summary>
    public partial class CashInStcokDAL : DetailOperate, ICashInStcokDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CashInStcokDAL()
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
            CashInStcok fun_cashinstcok_ref = (CashInStcok)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_cashinstcok_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter corprefidpara = new SqlParameter("@CorpRefId", SqlDbType.Int, 4);
            corprefidpara.Value = fun_cashinstcok_ref.CorpRefId;
            paras.Add(corprefidpara);

            SqlParameter contractrefidpara = new SqlParameter("@ContractRefId", SqlDbType.Int, 4);
            contractrefidpara.Value = fun_cashinstcok_ref.ContractRefId;
            paras.Add(contractrefidpara);

            SqlParameter cashinidpara = new SqlParameter("@CashInId", SqlDbType.Int, 4);
            cashinidpara.Value = fun_cashinstcok_ref.CashInId;
            paras.Add(cashinidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = fun_cashinstcok_ref.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = fun_cashinstcok_ref.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter stocknameidpara = new SqlParameter("@StockNameId", SqlDbType.Int, 4);
            stocknameidpara.Value = fun_cashinstcok_ref.StockNameId;
            paras.Add(stocknameidpara);

            SqlParameter allotnetamountpara = new SqlParameter("@AllotNetAmount", SqlDbType.Decimal, 9);
            allotnetamountpara.Value = fun_cashinstcok_ref.AllotNetAmount;
            paras.Add(allotnetamountpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_cashinstcok_ref.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);

            SqlParameter fundslogidpara = new SqlParameter("@FundsLogId", SqlDbType.Int, 4);
            fundslogidpara.Value = fun_cashinstcok_ref.FundsLogId;
            paras.Add(fundslogidpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            CashInStcok cashinstcok = new CashInStcok();

            cashinstcok.RefId = Convert.ToInt32(dr["RefId"]);

            if (dr["AllotId"] != DBNull.Value)
            {
                cashinstcok.AllotId = Convert.ToInt32(dr["AllotId"]);
            }

            if (dr["CorpRefId"] != DBNull.Value)
            {
                cashinstcok.CorpRefId = Convert.ToInt32(dr["CorpRefId"]);
            }

            if (dr["ContractRefId"] != DBNull.Value)
            {
                cashinstcok.ContractRefId = Convert.ToInt32(dr["ContractRefId"]);
            }

            if (dr["CashInId"] != DBNull.Value)
            {
                cashinstcok.CashInId = Convert.ToInt32(dr["CashInId"]);
            }

            if (dr["StockId"] != DBNull.Value)
            {
                cashinstcok.StockId = Convert.ToInt32(dr["StockId"]);
            }

            if (dr["StockLogId"] != DBNull.Value)
            {
                cashinstcok.StockLogId = Convert.ToInt32(dr["StockLogId"]);
            }

            if (dr["StockNameId"] != DBNull.Value)
            {
                cashinstcok.StockNameId = Convert.ToInt32(dr["StockNameId"]);
            }

            if (dr["AllotNetAmount"] != DBNull.Value)
            {
                cashinstcok.AllotNetAmount = Convert.ToDecimal(dr["AllotNetAmount"]);
            }

            if (dr["AllotBala"] != DBNull.Value)
            {
                cashinstcok.AllotBala = Convert.ToDecimal(dr["AllotBala"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                cashinstcok.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }

            if (dr["FundsLogId"] != DBNull.Value)
            {
                cashinstcok.FundsLogId = Convert.ToInt32(dr["FundsLogId"]);
            }


            return cashinstcok;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            CashInStcok cashinstcok = new CashInStcok();

            int indexRefId = dr.GetOrdinal("RefId");
            cashinstcok.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexAllotId = dr.GetOrdinal("AllotId");
            if (dr["AllotId"] != DBNull.Value)
            {
                cashinstcok.AllotId = Convert.ToInt32(dr[indexAllotId]);
            }

            int indexCorpRefId = dr.GetOrdinal("CorpRefId");
            if (dr["CorpRefId"] != DBNull.Value)
            {
                cashinstcok.CorpRefId = Convert.ToInt32(dr[indexCorpRefId]);
            }

            int indexContractRefId = dr.GetOrdinal("ContractRefId");
            if (dr["ContractRefId"] != DBNull.Value)
            {
                cashinstcok.ContractRefId = Convert.ToInt32(dr[indexContractRefId]);
            }

            int indexCashInId = dr.GetOrdinal("CashInId");
            if (dr["CashInId"] != DBNull.Value)
            {
                cashinstcok.CashInId = Convert.ToInt32(dr[indexCashInId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                cashinstcok.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                cashinstcok.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexStockNameId = dr.GetOrdinal("StockNameId");
            if (dr["StockNameId"] != DBNull.Value)
            {
                cashinstcok.StockNameId = Convert.ToInt32(dr[indexStockNameId]);
            }

            int indexAllotNetAmount = dr.GetOrdinal("AllotNetAmount");
            if (dr["AllotNetAmount"] != DBNull.Value)
            {
                cashinstcok.AllotNetAmount = Convert.ToDecimal(dr[indexAllotNetAmount]);
            }

            int indexAllotBala = dr.GetOrdinal("AllotBala");
            if (dr["AllotBala"] != DBNull.Value)
            {
                cashinstcok.AllotBala = Convert.ToDecimal(dr[indexAllotBala]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                cashinstcok.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexFundsLogId = dr.GetOrdinal("FundsLogId");
            if (dr["FundsLogId"] != DBNull.Value)
            {
                cashinstcok.FundsLogId = Convert.ToInt32(dr[indexFundsLogId]);
            }


            return cashinstcok;
        }

        public override string TableName
        {
            get
            {
                return "Fun_CashInStcok_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CashInStcok fun_cashinstcok_ref = (CashInStcok)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = fun_cashinstcok_ref.RefId;
            paras.Add(refidpara);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_cashinstcok_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter corprefidpara = new SqlParameter("@CorpRefId", SqlDbType.Int, 4);
            corprefidpara.Value = fun_cashinstcok_ref.CorpRefId;
            paras.Add(corprefidpara);

            SqlParameter contractrefidpara = new SqlParameter("@ContractRefId", SqlDbType.Int, 4);
            contractrefidpara.Value = fun_cashinstcok_ref.ContractRefId;
            paras.Add(contractrefidpara);

            SqlParameter cashinidpara = new SqlParameter("@CashInId", SqlDbType.Int, 4);
            cashinidpara.Value = fun_cashinstcok_ref.CashInId;
            paras.Add(cashinidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = fun_cashinstcok_ref.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = fun_cashinstcok_ref.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter stocknameidpara = new SqlParameter("@StockNameId", SqlDbType.Int, 4);
            stocknameidpara.Value = fun_cashinstcok_ref.StockNameId;
            paras.Add(stocknameidpara);

            SqlParameter allotnetamountpara = new SqlParameter("@AllotNetAmount", SqlDbType.Decimal, 9);
            allotnetamountpara.Value = fun_cashinstcok_ref.AllotNetAmount;
            paras.Add(allotnetamountpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_cashinstcok_ref.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = fun_cashinstcok_ref.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter fundslogidpara = new SqlParameter("@FundsLogId", SqlDbType.Int, 4);
            fundslogidpara.Value = fun_cashinstcok_ref.FundsLogId;
            paras.Add(fundslogidpara);


            return paras;
        }

        #endregion
    }
}
