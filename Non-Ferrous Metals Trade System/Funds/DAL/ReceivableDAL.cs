/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ReceivableDAL.cs
// 文件功能描述：收款dbo.Fun_Receivable数据交互类。
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
    /// 收款dbo.Fun_Receivable数据交互类。
    /// </summary>
    public class ReceivableDAL : ApplyOperate, IReceivableDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReceivableDAL()
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
            Receivable fun_receivable = (Receivable)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ReceivableId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter receiveempidpara = new SqlParameter("@ReceiveEmpId", SqlDbType.Int, 4);
            receiveempidpara.Value = fun_receivable.ReceiveEmpId;
            paras.Add(receiveempidpara);

            SqlParameter receivedatepara = new SqlParameter("@ReceiveDate", SqlDbType.DateTime, 8);
            receivedatepara.Value = fun_receivable.ReceiveDate;
            paras.Add(receivedatepara);

            SqlParameter receivablegroupidpara = new SqlParameter("@ReceivableGroupId", SqlDbType.Int, 4);
            receivablegroupidpara.Value = fun_receivable.ReceivableGroupId;
            paras.Add(receivablegroupidpara);

            SqlParameter receivablecorpidpara = new SqlParameter("@ReceivableCorpId", SqlDbType.Int, 4);
            receivablecorpidpara.Value = fun_receivable.ReceivableCorpId;
            paras.Add(receivablecorpidpara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_receivable.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter paybalapara = new SqlParameter("@PayBala", SqlDbType.Decimal, 9);
            paybalapara.Value = fun_receivable.PayBala;
            paras.Add(paybalapara);

            SqlParameter receivablebankpara = new SqlParameter("@ReceivableBank", SqlDbType.Int, 4);
            receivablebankpara.Value = fun_receivable.ReceivableBank;
            paras.Add(receivablebankpara);

            SqlParameter receivableaccoontidpara = new SqlParameter("@ReceivableAccoontId", SqlDbType.Int, 4);
            receivableaccoontidpara.Value = fun_receivable.ReceivableAccoontId;
            paras.Add(receivableaccoontidpara);

            SqlParameter paygroupidpara = new SqlParameter("@PayGroupId", SqlDbType.Int, 4);
            paygroupidpara.Value = fun_receivable.PayGroupId;
            paras.Add(paygroupidpara);

            SqlParameter paycorpidpara = new SqlParameter("@PayCorpId", SqlDbType.Int, 4);
            paycorpidpara.Value = fun_receivable.PayCorpId;
            paras.Add(paycorpidpara);

            if (!string.IsNullOrEmpty(fun_receivable.PayCorpName))
            {
                SqlParameter paycorpnamepara = new SqlParameter("@PayCorpName", SqlDbType.VarChar, 50);
                paycorpnamepara.Value = fun_receivable.PayCorpName;
                paras.Add(paycorpnamepara);
            }

            SqlParameter paybankidpara = new SqlParameter("@PayBankId", SqlDbType.Int, 4);
            paybankidpara.Value = fun_receivable.PayBankId;
            paras.Add(paybankidpara);

            if (!string.IsNullOrEmpty(fun_receivable.PayBank))
            {
                SqlParameter paybankpara = new SqlParameter("@PayBank", SqlDbType.VarChar, 50);
                paybankpara.Value = fun_receivable.PayBank;
                paras.Add(paybankpara);
            }

            SqlParameter payaccountidpara = new SqlParameter("@PayAccountId", SqlDbType.Int, 4);
            payaccountidpara.Value = fun_receivable.PayAccountId;
            paras.Add(payaccountidpara);

            if (!string.IsNullOrEmpty(fun_receivable.PayAccount))
            {
                SqlParameter payaccountpara = new SqlParameter("@PayAccount", SqlDbType.VarChar, 50);
                payaccountpara.Value = fun_receivable.PayAccount;
                paras.Add(payaccountpara);
            }

            if (!string.IsNullOrEmpty(fun_receivable.PayWord))
            {
                SqlParameter paywordpara = new SqlParameter("@PayWord", SqlDbType.VarChar, 400);
                paywordpara.Value = fun_receivable.PayWord;
                paras.Add(paywordpara);
            }

            if (!string.IsNullOrEmpty(fun_receivable.BankLog))
            {
                SqlParameter banklogpara = new SqlParameter("@BankLog", SqlDbType.VarChar, 4000);
                banklogpara.Value = fun_receivable.BankLog;
                paras.Add(banklogpara);
            }

            SqlParameter receivestatuspara = new SqlParameter("@ReceiveStatus", SqlDbType.Int, 4);
            receivestatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(receivestatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Receivable receivable = new Receivable();

            int indexReceivableId = dr.GetOrdinal("ReceivableId");
            receivable.ReceivableId = Convert.ToInt32(dr[indexReceivableId]);

            int indexReceiveEmpId = dr.GetOrdinal("ReceiveEmpId");
            if (dr["ReceiveEmpId"] != DBNull.Value)
            {
                receivable.ReceiveEmpId = Convert.ToInt32(dr[indexReceiveEmpId]);
            }

            int indexReceiveDate = dr.GetOrdinal("ReceiveDate");
            if (dr["ReceiveDate"] != DBNull.Value)
            {
                receivable.ReceiveDate = Convert.ToDateTime(dr[indexReceiveDate]);
            }

            int indexReceivableGroupId = dr.GetOrdinal("ReceivableGroupId");
            if (dr["ReceivableGroupId"] != DBNull.Value)
            {
                receivable.ReceivableGroupId = Convert.ToInt32(dr[indexReceivableGroupId]);
            }

            int indexReceivableCorpId = dr.GetOrdinal("ReceivableCorpId");
            if (dr["ReceivableCorpId"] != DBNull.Value)
            {
                receivable.ReceivableCorpId = Convert.ToInt32(dr[indexReceivableCorpId]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                receivable.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexPayBala = dr.GetOrdinal("PayBala");
            if (dr["PayBala"] != DBNull.Value)
            {
                receivable.PayBala = Convert.ToDecimal(dr[indexPayBala]);
            }

            int indexReceivableBank = dr.GetOrdinal("ReceivableBank");
            if (dr["ReceivableBank"] != DBNull.Value)
            {
                receivable.ReceivableBank = Convert.ToInt32(dr[indexReceivableBank]);
            }

            int indexReceivableAccoontId = dr.GetOrdinal("ReceivableAccoontId");
            if (dr["ReceivableAccoontId"] != DBNull.Value)
            {
                receivable.ReceivableAccoontId = Convert.ToInt32(dr[indexReceivableAccoontId]);
            }

            int indexPayGroupId = dr.GetOrdinal("PayGroupId");
            if (dr["PayGroupId"] != DBNull.Value)
            {
                receivable.PayGroupId = Convert.ToInt32(dr[indexPayGroupId]);
            }

            int indexPayCorpId = dr.GetOrdinal("PayCorpId");
            if (dr["PayCorpId"] != DBNull.Value)
            {
                receivable.PayCorpId = Convert.ToInt32(dr[indexPayCorpId]);
            }

            int indexPayCorpName = dr.GetOrdinal("PayCorpName");
            if (dr["PayCorpName"] != DBNull.Value)
            {
                receivable.PayCorpName = Convert.ToString(dr[indexPayCorpName]);
            }

            int indexPayBankId = dr.GetOrdinal("PayBankId");
            if (dr["PayBankId"] != DBNull.Value)
            {
                receivable.PayBankId = Convert.ToInt32(dr[indexPayBankId]);
            }

            int indexPayBank = dr.GetOrdinal("PayBank");
            if (dr["PayBank"] != DBNull.Value)
            {
                receivable.PayBank = Convert.ToString(dr[indexPayBank]);
            }

            int indexPayAccountId = dr.GetOrdinal("PayAccountId");
            if (dr["PayAccountId"] != DBNull.Value)
            {
                receivable.PayAccountId = Convert.ToInt32(dr[indexPayAccountId]);
            }

            int indexPayAccount = dr.GetOrdinal("PayAccount");
            if (dr["PayAccount"] != DBNull.Value)
            {
                receivable.PayAccount = Convert.ToString(dr[indexPayAccount]);
            }

            int indexPayWord = dr.GetOrdinal("PayWord");
            if (dr["PayWord"] != DBNull.Value)
            {
                receivable.PayWord = Convert.ToString(dr[indexPayWord]);
            }

            int indexBankLog = dr.GetOrdinal("BankLog");
            if (dr["BankLog"] != DBNull.Value)
            {
                receivable.BankLog = Convert.ToString(dr[indexBankLog]);
            }

            int indexReceiveStatus = dr.GetOrdinal("ReceiveStatus");
            if (dr["ReceiveStatus"] != DBNull.Value)
            {
                receivable.ReceiveStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexReceiveStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                receivable.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                receivable.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                receivable.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                receivable.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return receivable;
        }

        public override string TableName
        {
            get
            {
                return "Fun_Receivable";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Receivable fun_receivable = (Receivable)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter receivableidpara = new SqlParameter("@ReceivableId", SqlDbType.Int, 4);
            receivableidpara.Value = fun_receivable.ReceivableId;
            paras.Add(receivableidpara);

            SqlParameter receiveempidpara = new SqlParameter("@ReceiveEmpId", SqlDbType.Int, 4);
            receiveempidpara.Value = fun_receivable.ReceiveEmpId;
            paras.Add(receiveempidpara);

            SqlParameter receivedatepara = new SqlParameter("@ReceiveDate", SqlDbType.DateTime, 8);
            receivedatepara.Value = fun_receivable.ReceiveDate;
            paras.Add(receivedatepara);

            SqlParameter receivablegroupidpara = new SqlParameter("@ReceivableGroupId", SqlDbType.Int, 4);
            receivablegroupidpara.Value = fun_receivable.ReceivableGroupId;
            paras.Add(receivablegroupidpara);

            SqlParameter receivablecorpidpara = new SqlParameter("@ReceivableCorpId", SqlDbType.Int, 4);
            receivablecorpidpara.Value = fun_receivable.ReceivableCorpId;
            paras.Add(receivablecorpidpara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_receivable.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter paybalapara = new SqlParameter("@PayBala", SqlDbType.Decimal, 9);
            paybalapara.Value = fun_receivable.PayBala;
            paras.Add(paybalapara);

            SqlParameter receivablebankpara = new SqlParameter("@ReceivableBank", SqlDbType.Int, 4);
            receivablebankpara.Value = fun_receivable.ReceivableBank;
            paras.Add(receivablebankpara);

            SqlParameter receivableaccoontidpara = new SqlParameter("@ReceivableAccoontId", SqlDbType.Int, 4);
            receivableaccoontidpara.Value = fun_receivable.ReceivableAccoontId;
            paras.Add(receivableaccoontidpara);

            SqlParameter paygroupidpara = new SqlParameter("@PayGroupId", SqlDbType.Int, 4);
            paygroupidpara.Value = fun_receivable.PayGroupId;
            paras.Add(paygroupidpara);

            SqlParameter paycorpidpara = new SqlParameter("@PayCorpId", SqlDbType.Int, 4);
            paycorpidpara.Value = fun_receivable.PayCorpId;
            paras.Add(paycorpidpara);

            if (!string.IsNullOrEmpty(fun_receivable.PayCorpName))
            {
                SqlParameter paycorpnamepara = new SqlParameter("@PayCorpName", SqlDbType.VarChar, 50);
                paycorpnamepara.Value = fun_receivable.PayCorpName;
                paras.Add(paycorpnamepara);
            }

            SqlParameter paybankidpara = new SqlParameter("@PayBankId", SqlDbType.Int, 4);
            paybankidpara.Value = fun_receivable.PayBankId;
            paras.Add(paybankidpara);

            if (!string.IsNullOrEmpty(fun_receivable.PayBank))
            {
                SqlParameter paybankpara = new SqlParameter("@PayBank", SqlDbType.VarChar, 50);
                paybankpara.Value = fun_receivable.PayBank;
                paras.Add(paybankpara);
            }

            SqlParameter payaccountidpara = new SqlParameter("@PayAccountId", SqlDbType.Int, 4);
            payaccountidpara.Value = fun_receivable.PayAccountId;
            paras.Add(payaccountidpara);

            if (!string.IsNullOrEmpty(fun_receivable.PayAccount))
            {
                SqlParameter payaccountpara = new SqlParameter("@PayAccount", SqlDbType.VarChar, 50);
                payaccountpara.Value = fun_receivable.PayAccount;
                paras.Add(payaccountpara);
            }

            if (!string.IsNullOrEmpty(fun_receivable.PayWord))
            {
                SqlParameter paywordpara = new SqlParameter("@PayWord", SqlDbType.VarChar, 400);
                paywordpara.Value = fun_receivable.PayWord;
                paras.Add(paywordpara);
            }

            if (!string.IsNullOrEmpty(fun_receivable.BankLog))
            {
                SqlParameter banklogpara = new SqlParameter("@BankLog", SqlDbType.VarChar, 4000);
                banklogpara.Value = fun_receivable.BankLog;
                paras.Add(banklogpara);
            }

            SqlParameter receivestatuspara = new SqlParameter("@ReceiveStatus", SqlDbType.Int, 4);
            receivestatuspara.Value = fun_receivable.ReceiveStatus;
            paras.Add(receivestatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
