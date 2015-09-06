/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInDAL.cs
// 文件功能描述：收款dbo.Fun_CashIn数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月18日
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
    /// 收款dbo.Fun_CashIn数据交互类。
    /// </summary>
    public partial class CashInDAL : ApplyOperate, ICashInDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CashInDAL()
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
            CashIn fun_cashin = (CashIn)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@CashInId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter cashinempidpara = new SqlParameter("@CashInEmpId", SqlDbType.Int, 4);
            cashinempidpara.Value = fun_cashin.CashInEmpId;
            paras.Add(cashinempidpara);

            SqlParameter cashindatepara = new SqlParameter("@CashInDate", SqlDbType.DateTime, 8);
            cashindatepara.Value = fun_cashin.CashInDate;
            paras.Add(cashindatepara);

            SqlParameter cashinblocidpara = new SqlParameter("@CashInBlocId", SqlDbType.Int, 4);
            cashinblocidpara.Value = fun_cashin.CashInBlocId;
            paras.Add(cashinblocidpara);

            SqlParameter cashincorpidpara = new SqlParameter("@CashInCorpId", SqlDbType.Int, 4);
            cashincorpidpara.Value = fun_cashin.CashInCorpId;
            paras.Add(cashincorpidpara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_cashin.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter cashinbalapara = new SqlParameter("@CashInBala", SqlDbType.Decimal, 9);
            cashinbalapara.Value = fun_cashin.CashInBala;
            paras.Add(cashinbalapara);

            SqlParameter cashinbankpara = new SqlParameter("@CashInBank", SqlDbType.Int, 4);
            cashinbankpara.Value = fun_cashin.CashInBank;
            paras.Add(cashinbankpara);

            SqlParameter cashinaccoontidpara = new SqlParameter("@CashInAccoontId", SqlDbType.Int, 4);
            cashinaccoontidpara.Value = fun_cashin.CashInAccoontId;
            paras.Add(cashinaccoontidpara);

            SqlParameter cashinmodepara = new SqlParameter("@CashInMode", SqlDbType.Int, 4);
            cashinmodepara.Value = fun_cashin.CashInMode;
            paras.Add(cashinmodepara);

            SqlParameter payblocidpara = new SqlParameter("@PayBlocId", SqlDbType.Int, 4);
            payblocidpara.Value = fun_cashin.PayBlocId;
            paras.Add(payblocidpara);

            SqlParameter paycorpidpara = new SqlParameter("@PayCorpId", SqlDbType.Int, 4);
            paycorpidpara.Value = fun_cashin.PayCorpId;
            paras.Add(paycorpidpara);

            if (!string.IsNullOrEmpty(fun_cashin.PayCorpName))
            {
                SqlParameter paycorpnamepara = new SqlParameter("@PayCorpName", SqlDbType.VarChar, 50);
                paycorpnamepara.Value = fun_cashin.PayCorpName;
                paras.Add(paycorpnamepara);
            }

            SqlParameter paybankidpara = new SqlParameter("@PayBankId", SqlDbType.Int, 4);
            paybankidpara.Value = fun_cashin.PayBankId;
            paras.Add(paybankidpara);

            if (!string.IsNullOrEmpty(fun_cashin.PayBank))
            {
                SqlParameter paybankpara = new SqlParameter("@PayBank", SqlDbType.VarChar, 50);
                paybankpara.Value = fun_cashin.PayBank;
                paras.Add(paybankpara);
            }

            SqlParameter payaccountidpara = new SqlParameter("@PayAccountId", SqlDbType.Int, 4);
            payaccountidpara.Value = fun_cashin.PayAccountId;
            paras.Add(payaccountidpara);

            if (!string.IsNullOrEmpty(fun_cashin.PayAccount))
            {
                SqlParameter payaccountpara = new SqlParameter("@PayAccount", SqlDbType.VarChar, 50);
                payaccountpara.Value = fun_cashin.PayAccount;
                paras.Add(payaccountpara);
            }

            if (!string.IsNullOrEmpty(fun_cashin.PayWord))
            {
                SqlParameter paywordpara = new SqlParameter("@PayWord", SqlDbType.VarChar, 400);
                paywordpara.Value = fun_cashin.PayWord;
                paras.Add(paywordpara);
            }

            if (!string.IsNullOrEmpty(fun_cashin.BankLog))
            {
                SqlParameter banklogpara = new SqlParameter("@BankLog", SqlDbType.VarChar, 4000);
                banklogpara.Value = fun_cashin.BankLog;
                paras.Add(banklogpara);
            }

            SqlParameter cashinstatuspara = new SqlParameter("@CashInStatus", SqlDbType.Int, 4);
            cashinstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(cashinstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            CashIn cashin = new CashIn();

            cashin.CashInId = Convert.ToInt32(dr["CashInId"]);

            if (dr["CashInEmpId"] != DBNull.Value)
            {
                cashin.CashInEmpId = Convert.ToInt32(dr["CashInEmpId"]);
            }

            if (dr["CashInDate"] != DBNull.Value)
            {
                cashin.CashInDate = Convert.ToDateTime(dr["CashInDate"]);
            }

            if (dr["CashInBlocId"] != DBNull.Value)
            {
                cashin.CashInBlocId = Convert.ToInt32(dr["CashInBlocId"]);
            }

            if (dr["CashInCorpId"] != DBNull.Value)
            {
                cashin.CashInCorpId = Convert.ToInt32(dr["CashInCorpId"]);
            }

            if (dr["CurrencyId"] != DBNull.Value)
            {
                cashin.CurrencyId = Convert.ToInt32(dr["CurrencyId"]);
            }

            if (dr["CashInBala"] != DBNull.Value)
            {
                cashin.CashInBala = Convert.ToDecimal(dr["CashInBala"]);
            }

            if (dr["CashInBank"] != DBNull.Value)
            {
                cashin.CashInBank = Convert.ToInt32(dr["CashInBank"]);
            }

            if (dr["CashInAccoontId"] != DBNull.Value)
            {
                cashin.CashInAccoontId = Convert.ToInt32(dr["CashInAccoontId"]);
            }

            if (dr["CashInMode"] != DBNull.Value)
            {
                cashin.CashInMode = Convert.ToInt32(dr["CashInMode"]);
            }

            if (dr["PayBlocId"] != DBNull.Value)
            {
                cashin.PayBlocId = Convert.ToInt32(dr["PayBlocId"]);
            }

            if (dr["PayCorpId"] != DBNull.Value)
            {
                cashin.PayCorpId = Convert.ToInt32(dr["PayCorpId"]);
            }

            if (dr["PayCorpName"] != DBNull.Value)
            {
                cashin.PayCorpName = Convert.ToString(dr["PayCorpName"]);
            }

            if (dr["PayBankId"] != DBNull.Value)
            {
                cashin.PayBankId = Convert.ToInt32(dr["PayBankId"]);
            }

            if (dr["PayBank"] != DBNull.Value)
            {
                cashin.PayBank = Convert.ToString(dr["PayBank"]);
            }

            if (dr["PayAccountId"] != DBNull.Value)
            {
                cashin.PayAccountId = Convert.ToInt32(dr["PayAccountId"]);
            }

            if (dr["PayAccount"] != DBNull.Value)
            {
                cashin.PayAccount = Convert.ToString(dr["PayAccount"]);
            }

            if (dr["PayWord"] != DBNull.Value)
            {
                cashin.PayWord = Convert.ToString(dr["PayWord"]);
            }

            if (dr["BankLog"] != DBNull.Value)
            {
                cashin.BankLog = Convert.ToString(dr["BankLog"]);
            }

            if (dr["CashInStatus"] != DBNull.Value)
            {
                cashin.CashInStatus = (Common.StatusEnum)Convert.ToInt32(dr["CashInStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                cashin.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                cashin.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                cashin.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                cashin.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return cashin;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            CashIn cashin = new CashIn();

            int indexCashInId = dr.GetOrdinal("CashInId");
            cashin.CashInId = Convert.ToInt32(dr[indexCashInId]);

            int indexCashInEmpId = dr.GetOrdinal("CashInEmpId");
            if (dr["CashInEmpId"] != DBNull.Value)
            {
                cashin.CashInEmpId = Convert.ToInt32(dr[indexCashInEmpId]);
            }

            int indexCashInDate = dr.GetOrdinal("CashInDate");
            if (dr["CashInDate"] != DBNull.Value)
            {
                cashin.CashInDate = Convert.ToDateTime(dr[indexCashInDate]);
            }

            int indexCashInBlocId = dr.GetOrdinal("CashInBlocId");
            if (dr["CashInBlocId"] != DBNull.Value)
            {
                cashin.CashInBlocId = Convert.ToInt32(dr[indexCashInBlocId]);
            }

            int indexCashInCorpId = dr.GetOrdinal("CashInCorpId");
            if (dr["CashInCorpId"] != DBNull.Value)
            {
                cashin.CashInCorpId = Convert.ToInt32(dr[indexCashInCorpId]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                cashin.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexCashInBala = dr.GetOrdinal("CashInBala");
            if (dr["CashInBala"] != DBNull.Value)
            {
                cashin.CashInBala = Convert.ToDecimal(dr[indexCashInBala]);
            }

            int indexCashInBank = dr.GetOrdinal("CashInBank");
            if (dr["CashInBank"] != DBNull.Value)
            {
                cashin.CashInBank = Convert.ToInt32(dr[indexCashInBank]);
            }

            int indexCashInAccoontId = dr.GetOrdinal("CashInAccoontId");
            if (dr["CashInAccoontId"] != DBNull.Value)
            {
                cashin.CashInAccoontId = Convert.ToInt32(dr[indexCashInAccoontId]);
            }

            int indexCashInMode = dr.GetOrdinal("CashInMode");
            if (dr["CashInMode"] != DBNull.Value)
            {
                cashin.CashInMode = Convert.ToInt32(dr[indexCashInMode]);
            }

            int indexPayBlocId = dr.GetOrdinal("PayBlocId");
            if (dr["PayBlocId"] != DBNull.Value)
            {
                cashin.PayBlocId = Convert.ToInt32(dr[indexPayBlocId]);
            }

            int indexPayCorpId = dr.GetOrdinal("PayCorpId");
            if (dr["PayCorpId"] != DBNull.Value)
            {
                cashin.PayCorpId = Convert.ToInt32(dr[indexPayCorpId]);
            }

            int indexPayCorpName = dr.GetOrdinal("PayCorpName");
            if (dr["PayCorpName"] != DBNull.Value)
            {
                cashin.PayCorpName = Convert.ToString(dr[indexPayCorpName]);
            }

            int indexPayBankId = dr.GetOrdinal("PayBankId");
            if (dr["PayBankId"] != DBNull.Value)
            {
                cashin.PayBankId = Convert.ToInt32(dr[indexPayBankId]);
            }

            int indexPayBank = dr.GetOrdinal("PayBank");
            if (dr["PayBank"] != DBNull.Value)
            {
                cashin.PayBank = Convert.ToString(dr[indexPayBank]);
            }

            int indexPayAccountId = dr.GetOrdinal("PayAccountId");
            if (dr["PayAccountId"] != DBNull.Value)
            {
                cashin.PayAccountId = Convert.ToInt32(dr[indexPayAccountId]);
            }

            int indexPayAccount = dr.GetOrdinal("PayAccount");
            if (dr["PayAccount"] != DBNull.Value)
            {
                cashin.PayAccount = Convert.ToString(dr[indexPayAccount]);
            }

            int indexPayWord = dr.GetOrdinal("PayWord");
            if (dr["PayWord"] != DBNull.Value)
            {
                cashin.PayWord = Convert.ToString(dr[indexPayWord]);
            }

            int indexBankLog = dr.GetOrdinal("BankLog");
            if (dr["BankLog"] != DBNull.Value)
            {
                cashin.BankLog = Convert.ToString(dr[indexBankLog]);
            }

            int indexCashInStatus = dr.GetOrdinal("CashInStatus");
            if (dr["CashInStatus"] != DBNull.Value)
            {
                cashin.CashInStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexCashInStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                cashin.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                cashin.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                cashin.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                cashin.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return cashin;
        }

        public override string TableName
        {
            get
            {
                return "Fun_CashIn";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CashIn fun_cashin = (CashIn)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter cashinidpara = new SqlParameter("@CashInId", SqlDbType.Int, 4);
            cashinidpara.Value = fun_cashin.CashInId;
            paras.Add(cashinidpara);

            SqlParameter cashinempidpara = new SqlParameter("@CashInEmpId", SqlDbType.Int, 4);
            cashinempidpara.Value = fun_cashin.CashInEmpId;
            paras.Add(cashinempidpara);

            SqlParameter cashindatepara = new SqlParameter("@CashInDate", SqlDbType.DateTime, 8);
            cashindatepara.Value = fun_cashin.CashInDate;
            paras.Add(cashindatepara);

            SqlParameter cashinblocidpara = new SqlParameter("@CashInBlocId", SqlDbType.Int, 4);
            cashinblocidpara.Value = fun_cashin.CashInBlocId;
            paras.Add(cashinblocidpara);

            SqlParameter cashincorpidpara = new SqlParameter("@CashInCorpId", SqlDbType.Int, 4);
            cashincorpidpara.Value = fun_cashin.CashInCorpId;
            paras.Add(cashincorpidpara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_cashin.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter cashinbalapara = new SqlParameter("@CashInBala", SqlDbType.Decimal, 9);
            cashinbalapara.Value = fun_cashin.CashInBala;
            paras.Add(cashinbalapara);

            SqlParameter cashinbankpara = new SqlParameter("@CashInBank", SqlDbType.Int, 4);
            cashinbankpara.Value = fun_cashin.CashInBank;
            paras.Add(cashinbankpara);

            SqlParameter cashinaccoontidpara = new SqlParameter("@CashInAccoontId", SqlDbType.Int, 4);
            cashinaccoontidpara.Value = fun_cashin.CashInAccoontId;
            paras.Add(cashinaccoontidpara);

            SqlParameter cashinmodepara = new SqlParameter("@CashInMode", SqlDbType.Int, 4);
            cashinmodepara.Value = fun_cashin.CashInMode;
            paras.Add(cashinmodepara);

            SqlParameter payblocidpara = new SqlParameter("@PayBlocId", SqlDbType.Int, 4);
            payblocidpara.Value = fun_cashin.PayBlocId;
            paras.Add(payblocidpara);

            SqlParameter paycorpidpara = new SqlParameter("@PayCorpId", SqlDbType.Int, 4);
            paycorpidpara.Value = fun_cashin.PayCorpId;
            paras.Add(paycorpidpara);

            if (!string.IsNullOrEmpty(fun_cashin.PayCorpName))
            {
                SqlParameter paycorpnamepara = new SqlParameter("@PayCorpName", SqlDbType.VarChar, 50);
                paycorpnamepara.Value = fun_cashin.PayCorpName;
                paras.Add(paycorpnamepara);
            }

            SqlParameter paybankidpara = new SqlParameter("@PayBankId", SqlDbType.Int, 4);
            paybankidpara.Value = fun_cashin.PayBankId;
            paras.Add(paybankidpara);

            if (!string.IsNullOrEmpty(fun_cashin.PayBank))
            {
                SqlParameter paybankpara = new SqlParameter("@PayBank", SqlDbType.VarChar, 50);
                paybankpara.Value = fun_cashin.PayBank;
                paras.Add(paybankpara);
            }

            SqlParameter payaccountidpara = new SqlParameter("@PayAccountId", SqlDbType.Int, 4);
            payaccountidpara.Value = fun_cashin.PayAccountId;
            paras.Add(payaccountidpara);

            if (!string.IsNullOrEmpty(fun_cashin.PayAccount))
            {
                SqlParameter payaccountpara = new SqlParameter("@PayAccount", SqlDbType.VarChar, 50);
                payaccountpara.Value = fun_cashin.PayAccount;
                paras.Add(payaccountpara);
            }

            if (!string.IsNullOrEmpty(fun_cashin.PayWord))
            {
                SqlParameter paywordpara = new SqlParameter("@PayWord", SqlDbType.VarChar, 400);
                paywordpara.Value = fun_cashin.PayWord;
                paras.Add(paywordpara);
            }

            if (!string.IsNullOrEmpty(fun_cashin.BankLog))
            {
                SqlParameter banklogpara = new SqlParameter("@BankLog", SqlDbType.VarChar, 4000);
                banklogpara.Value = fun_cashin.BankLog;
                paras.Add(banklogpara);
            }

            SqlParameter cashinstatuspara = new SqlParameter("@CashInStatus", SqlDbType.Int, 4);
            cashinstatuspara.Value = fun_cashin.CashInStatus;
            paras.Add(cashinstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion        
    }
}
