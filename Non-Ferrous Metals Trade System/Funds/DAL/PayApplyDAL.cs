/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PayApplyDAL.cs
// 文件功能描述：付款申请dbo.Fun_PayApply数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年8月13日
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
    /// 付款申请dbo.Fun_PayApply数据交互类。
    /// </summary>
    public partial class PayApplyDAL : ApplyOperate, IPayApplyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PayApplyDAL()
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
            PayApply fun_payapply = (PayApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@PayApplyId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = fun_payapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter payapplysourcepara = new SqlParameter("@PayApplySource", SqlDbType.Int, 4);
            payapplysourcepara.Value = fun_payapply.PayApplySource;
            paras.Add(payapplysourcepara);

            SqlParameter recblocidpara = new SqlParameter("@RecBlocId", SqlDbType.Int, 4);
            recblocidpara.Value = fun_payapply.RecBlocId;
            paras.Add(recblocidpara);

            SqlParameter reccorpidpara = new SqlParameter("@RecCorpId", SqlDbType.Int, 4);
            reccorpidpara.Value = fun_payapply.RecCorpId;
            paras.Add(reccorpidpara);

            SqlParameter recbankidpara = new SqlParameter("@RecBankId", SqlDbType.Int, 4);
            recbankidpara.Value = fun_payapply.RecBankId;
            paras.Add(recbankidpara);

            SqlParameter recbankaccountidpara = new SqlParameter("@RecBankAccountId", SqlDbType.Int, 4);
            recbankaccountidpara.Value = fun_payapply.RecBankAccountId;
            paras.Add(recbankaccountidpara);

            if (!string.IsNullOrEmpty(fun_payapply.RecBankAccount))
            {
                SqlParameter recbankaccountpara = new SqlParameter("@RecBankAccount", SqlDbType.VarChar, 50);
                recbankaccountpara.Value = fun_payapply.RecBankAccount;
                paras.Add(recbankaccountpara);
            }

            SqlParameter applybalapara = new SqlParameter("@ApplyBala", SqlDbType.Decimal, 9);
            applybalapara.Value = fun_payapply.ApplyBala;
            paras.Add(applybalapara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_payapply.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter paymodepara = new SqlParameter("@PayMode", SqlDbType.Int, 4);
            paymodepara.Value = fun_payapply.PayMode;
            paras.Add(paymodepara);

            SqlParameter paydeadlinepara = new SqlParameter("@PayDeadline", SqlDbType.DateTime, 8);
            paydeadlinepara.Value = fun_payapply.PayDeadline;
            paras.Add(paydeadlinepara);

            if (!string.IsNullOrEmpty(fun_payapply.SpecialDesc))
            {
                SqlParameter specialdescpara = new SqlParameter("@SpecialDesc", SqlDbType.VarChar, 4000);
                specialdescpara.Value = fun_payapply.SpecialDesc;
                paras.Add(specialdescpara);
            }

            SqlParameter paymatterpara = new SqlParameter("@PayMatter", SqlDbType.Int, 4);
            paymatterpara.Value = fun_payapply.PayMatter;
            paras.Add(paymatterpara);

            SqlParameter realpaycorpidpara = new SqlParameter("@RealPayCorpId", SqlDbType.Int, 4);
            realpaycorpidpara.Value = fun_payapply.RealPayCorpId;
            paras.Add(realpaycorpidpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PayApply payapply = new PayApply();

            int indexPayApplyId = dr.GetOrdinal("PayApplyId");
            payapply.PayApplyId = Convert.ToInt32(dr[indexPayApplyId]);

            int indexApplyId = dr.GetOrdinal("ApplyId");
            if (dr["ApplyId"] != DBNull.Value)
            {
                payapply.ApplyId = Convert.ToInt32(dr[indexApplyId]);
            }

            int indexPayApplySource = dr.GetOrdinal("PayApplySource");
            if (dr["PayApplySource"] != DBNull.Value)
            {
                payapply.PayApplySource = Convert.ToInt32(dr[indexPayApplySource]);
            }

            int indexRecBlocId = dr.GetOrdinal("RecBlocId");
            if (dr["RecBlocId"] != DBNull.Value)
            {
                payapply.RecBlocId = Convert.ToInt32(dr[indexRecBlocId]);
            }

            int indexRecCorpId = dr.GetOrdinal("RecCorpId");
            if (dr["RecCorpId"] != DBNull.Value)
            {
                payapply.RecCorpId = Convert.ToInt32(dr[indexRecCorpId]);
            }

            int indexRecBankId = dr.GetOrdinal("RecBankId");
            if (dr["RecBankId"] != DBNull.Value)
            {
                payapply.RecBankId = Convert.ToInt32(dr[indexRecBankId]);
            }

            int indexRecBankAccountId = dr.GetOrdinal("RecBankAccountId");
            if (dr["RecBankAccountId"] != DBNull.Value)
            {
                payapply.RecBankAccountId = Convert.ToInt32(dr[indexRecBankAccountId]);
            }

            int indexRecBankAccount = dr.GetOrdinal("RecBankAccount");
            if (dr["RecBankAccount"] != DBNull.Value)
            {
                payapply.RecBankAccount = Convert.ToString(dr[indexRecBankAccount]);
            }

            int indexApplyBala = dr.GetOrdinal("ApplyBala");
            if (dr["ApplyBala"] != DBNull.Value)
            {
                payapply.ApplyBala = Convert.ToDecimal(dr[indexApplyBala]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                payapply.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexPayMode = dr.GetOrdinal("PayMode");
            if (dr["PayMode"] != DBNull.Value)
            {
                payapply.PayMode = Convert.ToInt32(dr[indexPayMode]);
            }

            int indexPayDeadline = dr.GetOrdinal("PayDeadline");
            if (dr["PayDeadline"] != DBNull.Value)
            {
                payapply.PayDeadline = Convert.ToDateTime(dr[indexPayDeadline]);
            }

            int indexSpecialDesc = dr.GetOrdinal("SpecialDesc");
            if (dr["SpecialDesc"] != DBNull.Value)
            {
                payapply.SpecialDesc = Convert.ToString(dr[indexSpecialDesc]);
            }

            int indexPayMatter = dr.GetOrdinal("PayMatter");
            if (dr["PayMatter"] != DBNull.Value)
            {
                payapply.PayMatter = Convert.ToInt32(dr[indexPayMatter]);
            }

            int indexRealPayCorpId = dr.GetOrdinal("RealPayCorpId");
            if (dr["RealPayCorpId"] != DBNull.Value)
            {
                payapply.RealPayCorpId = Convert.ToInt32(dr[indexRealPayCorpId]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                payapply.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                payapply.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                payapply.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                payapply.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return payapply;
        }

        public override string TableName
        {
            get
            {
                return "Fun_PayApply";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PayApply fun_payapply = (PayApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter payapplyidpara = new SqlParameter("@PayApplyId", SqlDbType.Int, 4);
            payapplyidpara.Value = fun_payapply.PayApplyId;
            paras.Add(payapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = fun_payapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter payapplysourcepara = new SqlParameter("@PayApplySource", SqlDbType.Int, 4);
            payapplysourcepara.Value = fun_payapply.PayApplySource;
            paras.Add(payapplysourcepara);

            SqlParameter recblocidpara = new SqlParameter("@RecBlocId", SqlDbType.Int, 4);
            recblocidpara.Value = fun_payapply.RecBlocId;
            paras.Add(recblocidpara);

            SqlParameter reccorpidpara = new SqlParameter("@RecCorpId", SqlDbType.Int, 4);
            reccorpidpara.Value = fun_payapply.RecCorpId;
            paras.Add(reccorpidpara);

            SqlParameter recbankidpara = new SqlParameter("@RecBankId", SqlDbType.Int, 4);
            recbankidpara.Value = fun_payapply.RecBankId;
            paras.Add(recbankidpara);

            SqlParameter recbankaccountidpara = new SqlParameter("@RecBankAccountId", SqlDbType.Int, 4);
            recbankaccountidpara.Value = fun_payapply.RecBankAccountId;
            paras.Add(recbankaccountidpara);

            if (!string.IsNullOrEmpty(fun_payapply.RecBankAccount))
            {
                SqlParameter recbankaccountpara = new SqlParameter("@RecBankAccount", SqlDbType.VarChar, 50);
                recbankaccountpara.Value = fun_payapply.RecBankAccount;
                paras.Add(recbankaccountpara);
            }

            SqlParameter applybalapara = new SqlParameter("@ApplyBala", SqlDbType.Decimal, 9);
            applybalapara.Value = fun_payapply.ApplyBala;
            paras.Add(applybalapara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_payapply.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter paymodepara = new SqlParameter("@PayMode", SqlDbType.Int, 4);
            paymodepara.Value = fun_payapply.PayMode;
            paras.Add(paymodepara);

            SqlParameter paydeadlinepara = new SqlParameter("@PayDeadline", SqlDbType.DateTime, 8);
            paydeadlinepara.Value = fun_payapply.PayDeadline;
            paras.Add(paydeadlinepara);

            if (!string.IsNullOrEmpty(fun_payapply.SpecialDesc))
            {
                SqlParameter specialdescpara = new SqlParameter("@SpecialDesc", SqlDbType.VarChar, 4000);
                specialdescpara.Value = fun_payapply.SpecialDesc;
                paras.Add(specialdescpara);
            }

            SqlParameter paymatterpara = new SqlParameter("@PayMatter", SqlDbType.Int, 4);
            paymatterpara.Value = fun_payapply.PayMatter;
            paras.Add(paymatterpara);

            SqlParameter realpaycorpidpara = new SqlParameter("@RealPayCorpId", SqlDbType.Int, 4);
            realpaycorpidpara.Value = fun_payapply.RealPayCorpId;
            paras.Add(realpaycorpidpara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        
    }
}
