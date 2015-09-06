/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FundsLogDAL.cs
// 文件功能描述：资金流水dbo.Fun_FundsLog数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年12月22日
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
    /// 资金流水dbo.Fun_FundsLog数据交互类。
    /// </summary>
    public class FundsLogDAL : ExecOperate, IFundsLogDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FundsLogDAL()
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
            FundsLog fun_fundslog = (FundsLog)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@FundsLogId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = fun_fundslog.ContractId;
            paras.Add(contractidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = fun_fundslog.SubId;
            paras.Add(subidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = fun_fundslog.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter logdatepara = new SqlParameter("@LogDate", SqlDbType.DateTime, 8);
            logdatepara.Value = fun_fundslog.LogDate;
            paras.Add(logdatepara);

            SqlParameter inblocidpara = new SqlParameter("@InBlocId", SqlDbType.Int, 4);
            inblocidpara.Value = fun_fundslog.InBlocId;
            paras.Add(inblocidpara);

            SqlParameter incorpidpara = new SqlParameter("@InCorpId", SqlDbType.Int, 4);
            incorpidpara.Value = fun_fundslog.InCorpId;
            paras.Add(incorpidpara);

            SqlParameter inbankidpara = new SqlParameter("@InBankId", SqlDbType.Int, 4);
            inbankidpara.Value = fun_fundslog.InBankId;
            paras.Add(inbankidpara);

            SqlParameter inaccountidpara = new SqlParameter("@InAccountId", SqlDbType.Int, 4);
            inaccountidpara.Value = fun_fundslog.InAccountId;
            paras.Add(inaccountidpara);

            SqlParameter outblocidpara = new SqlParameter("@OutBlocId", SqlDbType.Int, 4);
            outblocidpara.Value = fun_fundslog.OutBlocId;
            paras.Add(outblocidpara);

            SqlParameter outcorpidpara = new SqlParameter("@OutCorpId", SqlDbType.Int, 4);
            outcorpidpara.Value = fun_fundslog.OutCorpId;
            paras.Add(outcorpidpara);

            SqlParameter outbankidpara = new SqlParameter("@OutBankId", SqlDbType.Int, 4);
            outbankidpara.Value = fun_fundslog.OutBankId;
            paras.Add(outbankidpara);

            if (!string.IsNullOrEmpty(fun_fundslog.OutBank))
            {
                SqlParameter outbankpara = new SqlParameter("@OutBank", SqlDbType.VarChar, 200);
                outbankpara.Value = fun_fundslog.OutBank;
                paras.Add(outbankpara);
            }

            SqlParameter outaccountidpara = new SqlParameter("@OutAccountId", SqlDbType.Int, 4);
            outaccountidpara.Value = fun_fundslog.OutAccountId;
            paras.Add(outaccountidpara);

            if (!string.IsNullOrEmpty(fun_fundslog.OutAccount))
            {
                SqlParameter outaccountpara = new SqlParameter("@OutAccount", SqlDbType.VarChar, 200);
                outaccountpara.Value = fun_fundslog.OutAccount;
                paras.Add(outaccountpara);
            }

            SqlParameter fundsbalapara = new SqlParameter("@FundsBala", SqlDbType.Decimal, 9);
            fundsbalapara.Value = fun_fundslog.FundsBala;
            paras.Add(fundsbalapara);

            SqlParameter fundstypepara = new SqlParameter("@FundsType", SqlDbType.Int, 4);
            fundstypepara.Value = fun_fundslog.FundsType;
            paras.Add(fundstypepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_fundslog.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter logdirectionpara = new SqlParameter("@LogDirection", SqlDbType.Int, 4);
            logdirectionpara.Value = fun_fundslog.LogDirection;
            paras.Add(logdirectionpara);

            SqlParameter logtypepara = new SqlParameter("@LogType", SqlDbType.Int, 4);
            logtypepara.Value = fun_fundslog.LogType;
            paras.Add(logtypepara);

            SqlParameter paymodepara = new SqlParameter("@PayMode", SqlDbType.Int, 4);
            paymodepara.Value = fun_fundslog.PayMode;
            paras.Add(paymodepara);

            SqlParameter isvirtualpaypara = new SqlParameter("@IsVirtualPay", SqlDbType.Bit, 1);
            isvirtualpaypara.Value = fun_fundslog.IsVirtualPay;
            paras.Add(isvirtualpaypara);

            if (!string.IsNullOrEmpty(fun_fundslog.FundsDesc))
            {
                SqlParameter fundsdescpara = new SqlParameter("@FundsDesc", SqlDbType.VarChar, 800);
                fundsdescpara.Value = fun_fundslog.FundsDesc;
                paras.Add(fundsdescpara);
            }

            SqlParameter oppersonpara = new SqlParameter("@OpPerson", SqlDbType.Int, 4);
            oppersonpara.Value = fun_fundslog.OpPerson;
            paras.Add(oppersonpara);

            if (!string.IsNullOrEmpty(fun_fundslog.LogSourceBase))
            {
                SqlParameter logsourcebasepara = new SqlParameter("@LogSourceBase", SqlDbType.VarChar, 50);
                logsourcebasepara.Value = fun_fundslog.LogSourceBase;
                paras.Add(logsourcebasepara);
            }

            if (!string.IsNullOrEmpty(fun_fundslog.LogSource))
            {
                SqlParameter logsourcepara = new SqlParameter("@LogSource", SqlDbType.VarChar, 200);
                logsourcepara.Value = fun_fundslog.LogSource;
                paras.Add(logsourcepara);
            }

            SqlParameter sourceidpara = new SqlParameter("@SourceId", SqlDbType.Int, 4);
            sourceidpara.Value = fun_fundslog.SourceId;
            paras.Add(sourceidpara);

            SqlParameter logstatuspara = new SqlParameter("@LogStatus", SqlDbType.Int, 4);
            logstatuspara.Value = fun_fundslog.LogStatus;
            paras.Add(logstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            FundsLog fundslog = new FundsLog();

            int indexFundsLogId = dr.GetOrdinal("FundsLogId");
            fundslog.FundsLogId = Convert.ToInt32(dr[indexFundsLogId]);

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                fundslog.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexSubId = dr.GetOrdinal("SubId");
            if (dr["SubId"] != DBNull.Value)
            {
                fundslog.SubId = Convert.ToInt32(dr[indexSubId]);
            }

            int indexInvoiceId = dr.GetOrdinal("InvoiceId");
            if (dr["InvoiceId"] != DBNull.Value)
            {
                fundslog.InvoiceId = Convert.ToInt32(dr[indexInvoiceId]);
            }

            int indexLogDate = dr.GetOrdinal("LogDate");
            if (dr["LogDate"] != DBNull.Value)
            {
                fundslog.LogDate = Convert.ToDateTime(dr[indexLogDate]);
            }

            int indexInBlocId = dr.GetOrdinal("InBlocId");
            if (dr["InBlocId"] != DBNull.Value)
            {
                fundslog.InBlocId = Convert.ToInt32(dr[indexInBlocId]);
            }

            int indexInCorpId = dr.GetOrdinal("InCorpId");
            if (dr["InCorpId"] != DBNull.Value)
            {
                fundslog.InCorpId = Convert.ToInt32(dr[indexInCorpId]);
            }

            int indexInBankId = dr.GetOrdinal("InBankId");
            if (dr["InBankId"] != DBNull.Value)
            {
                fundslog.InBankId = Convert.ToInt32(dr[indexInBankId]);
            }

            int indexInAccountId = dr.GetOrdinal("InAccountId");
            if (dr["InAccountId"] != DBNull.Value)
            {
                fundslog.InAccountId = Convert.ToInt32(dr[indexInAccountId]);
            }

            int indexOutBlocId = dr.GetOrdinal("OutBlocId");
            if (dr["OutBlocId"] != DBNull.Value)
            {
                fundslog.OutBlocId = Convert.ToInt32(dr[indexOutBlocId]);
            }

            int indexOutCorpId = dr.GetOrdinal("OutCorpId");
            if (dr["OutCorpId"] != DBNull.Value)
            {
                fundslog.OutCorpId = Convert.ToInt32(dr[indexOutCorpId]);
            }

            int indexOutBankId = dr.GetOrdinal("OutBankId");
            if (dr["OutBankId"] != DBNull.Value)
            {
                fundslog.OutBankId = Convert.ToInt32(dr[indexOutBankId]);
            }

            int indexOutBank = dr.GetOrdinal("OutBank");
            if (dr["OutBank"] != DBNull.Value)
            {
                fundslog.OutBank = Convert.ToString(dr[indexOutBank]);
            }

            int indexOutAccountId = dr.GetOrdinal("OutAccountId");
            if (dr["OutAccountId"] != DBNull.Value)
            {
                fundslog.OutAccountId = Convert.ToInt32(dr[indexOutAccountId]);
            }

            int indexOutAccount = dr.GetOrdinal("OutAccount");
            if (dr["OutAccount"] != DBNull.Value)
            {
                fundslog.OutAccount = Convert.ToString(dr[indexOutAccount]);
            }

            int indexFundsBala = dr.GetOrdinal("FundsBala");
            if (dr["FundsBala"] != DBNull.Value)
            {
                fundslog.FundsBala = Convert.ToDecimal(dr[indexFundsBala]);
            }

            int indexFundsType = dr.GetOrdinal("FundsType");
            if (dr["FundsType"] != DBNull.Value)
            {
                fundslog.FundsType = Convert.ToInt32(dr[indexFundsType]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                fundslog.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexLogDirection = dr.GetOrdinal("LogDirection");
            if (dr["LogDirection"] != DBNull.Value)
            {
                fundslog.LogDirection = Convert.ToInt32(dr[indexLogDirection]);
            }

            int indexLogType = dr.GetOrdinal("LogType");
            fundslog.LogType = Convert.ToInt32(dr[indexLogType]);

            int indexPayMode = dr.GetOrdinal("PayMode");
            if (dr["PayMode"] != DBNull.Value)
            {
                fundslog.PayMode = Convert.ToInt32(dr[indexPayMode]);
            }

            int indexIsVirtualPay = dr.GetOrdinal("IsVirtualPay");
            if (dr["IsVirtualPay"] != DBNull.Value)
            {
                fundslog.IsVirtualPay = Convert.ToBoolean(dr[indexIsVirtualPay]);
            }

            int indexFundsDesc = dr.GetOrdinal("FundsDesc");
            if (dr["FundsDesc"] != DBNull.Value)
            {
                fundslog.FundsDesc = Convert.ToString(dr[indexFundsDesc]);
            }

            int indexOpPerson = dr.GetOrdinal("OpPerson");
            if (dr["OpPerson"] != DBNull.Value)
            {
                fundslog.OpPerson = Convert.ToInt32(dr[indexOpPerson]);
            }

            int indexLogSourceBase = dr.GetOrdinal("LogSourceBase");
            if (dr["LogSourceBase"] != DBNull.Value)
            {
                fundslog.LogSourceBase = Convert.ToString(dr[indexLogSourceBase]);
            }

            int indexLogSource = dr.GetOrdinal("LogSource");
            if (dr["LogSource"] != DBNull.Value)
            {
                fundslog.LogSource = Convert.ToString(dr[indexLogSource]);
            }

            int indexSourceId = dr.GetOrdinal("SourceId");
            if (dr["SourceId"] != DBNull.Value)
            {
                fundslog.SourceId = Convert.ToInt32(dr[indexSourceId]);
            }

            int indexLogStatus = dr.GetOrdinal("LogStatus");
            if (dr["LogStatus"] != DBNull.Value)
            {
                fundslog.LogStatus = (StatusEnum)Convert.ToInt32(dr[indexLogStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                fundslog.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                fundslog.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                fundslog.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                fundslog.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return fundslog;
        }

        public override string TableName
        {
            get
            {
                return "Fun_FundsLog";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            FundsLog fun_fundslog = (FundsLog)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter fundslogidpara = new SqlParameter("@FundsLogId", SqlDbType.Int, 4);
            fundslogidpara.Value = fun_fundslog.FundsLogId;
            paras.Add(fundslogidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = fun_fundslog.ContractId;
            paras.Add(contractidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = fun_fundslog.SubId;
            paras.Add(subidpara);

            SqlParameter invoiceidpara = new SqlParameter("@InvoiceId", SqlDbType.Int, 4);
            invoiceidpara.Value = fun_fundslog.InvoiceId;
            paras.Add(invoiceidpara);

            SqlParameter logdatepara = new SqlParameter("@LogDate", SqlDbType.DateTime, 8);
            logdatepara.Value = fun_fundslog.LogDate;
            paras.Add(logdatepara);

            SqlParameter inblocidpara = new SqlParameter("@InBlocId", SqlDbType.Int, 4);
            inblocidpara.Value = fun_fundslog.InBlocId;
            paras.Add(inblocidpara);

            SqlParameter incorpidpara = new SqlParameter("@InCorpId", SqlDbType.Int, 4);
            incorpidpara.Value = fun_fundslog.InCorpId;
            paras.Add(incorpidpara);

            SqlParameter inbankidpara = new SqlParameter("@InBankId", SqlDbType.Int, 4);
            inbankidpara.Value = fun_fundslog.InBankId;
            paras.Add(inbankidpara);

            SqlParameter inaccountidpara = new SqlParameter("@InAccountId", SqlDbType.Int, 4);
            inaccountidpara.Value = fun_fundslog.InAccountId;
            paras.Add(inaccountidpara);

            SqlParameter outblocidpara = new SqlParameter("@OutBlocId", SqlDbType.Int, 4);
            outblocidpara.Value = fun_fundslog.OutBlocId;
            paras.Add(outblocidpara);

            SqlParameter outcorpidpara = new SqlParameter("@OutCorpId", SqlDbType.Int, 4);
            outcorpidpara.Value = fun_fundslog.OutCorpId;
            paras.Add(outcorpidpara);

            SqlParameter outbankidpara = new SqlParameter("@OutBankId", SqlDbType.Int, 4);
            outbankidpara.Value = fun_fundslog.OutBankId;
            paras.Add(outbankidpara);

            if (!string.IsNullOrEmpty(fun_fundslog.OutBank))
            {
                SqlParameter outbankpara = new SqlParameter("@OutBank", SqlDbType.VarChar, 200);
                outbankpara.Value = fun_fundslog.OutBank;
                paras.Add(outbankpara);
            }

            SqlParameter outaccountidpara = new SqlParameter("@OutAccountId", SqlDbType.Int, 4);
            outaccountidpara.Value = fun_fundslog.OutAccountId;
            paras.Add(outaccountidpara);

            if (!string.IsNullOrEmpty(fun_fundslog.OutAccount))
            {
                SqlParameter outaccountpara = new SqlParameter("@OutAccount", SqlDbType.VarChar, 200);
                outaccountpara.Value = fun_fundslog.OutAccount;
                paras.Add(outaccountpara);
            }

            SqlParameter fundsbalapara = new SqlParameter("@FundsBala", SqlDbType.Decimal, 9);
            fundsbalapara.Value = fun_fundslog.FundsBala;
            paras.Add(fundsbalapara);

            SqlParameter fundstypepara = new SqlParameter("@FundsType", SqlDbType.Int, 4);
            fundstypepara.Value = fun_fundslog.FundsType;
            paras.Add(fundstypepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_fundslog.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter logdirectionpara = new SqlParameter("@LogDirection", SqlDbType.Int, 4);
            logdirectionpara.Value = fun_fundslog.LogDirection;
            paras.Add(logdirectionpara);

            SqlParameter logtypepara = new SqlParameter("@LogType", SqlDbType.Int, 4);
            logtypepara.Value = fun_fundslog.LogType;
            paras.Add(logtypepara);

            SqlParameter paymodepara = new SqlParameter("@PayMode", SqlDbType.Int, 4);
            paymodepara.Value = fun_fundslog.PayMode;
            paras.Add(paymodepara);

            SqlParameter isvirtualpaypara = new SqlParameter("@IsVirtualPay", SqlDbType.Bit, 1);
            isvirtualpaypara.Value = fun_fundslog.IsVirtualPay;
            paras.Add(isvirtualpaypara);

            if (!string.IsNullOrEmpty(fun_fundslog.FundsDesc))
            {
                SqlParameter fundsdescpara = new SqlParameter("@FundsDesc", SqlDbType.VarChar, 800);
                fundsdescpara.Value = fun_fundslog.FundsDesc;
                paras.Add(fundsdescpara);
            }

            SqlParameter oppersonpara = new SqlParameter("@OpPerson", SqlDbType.Int, 4);
            oppersonpara.Value = fun_fundslog.OpPerson;
            paras.Add(oppersonpara);

            if (!string.IsNullOrEmpty(fun_fundslog.LogSourceBase))
            {
                SqlParameter logsourcebasepara = new SqlParameter("@LogSourceBase", SqlDbType.VarChar, 50);
                logsourcebasepara.Value = fun_fundslog.LogSourceBase;
                paras.Add(logsourcebasepara);
            }

            if (!string.IsNullOrEmpty(fun_fundslog.LogSource))
            {
                SqlParameter logsourcepara = new SqlParameter("@LogSource", SqlDbType.VarChar, 200);
                logsourcepara.Value = fun_fundslog.LogSource;
                paras.Add(logsourcepara);
            }

            SqlParameter sourceidpara = new SqlParameter("@SourceId", SqlDbType.Int, 4);
            sourceidpara.Value = fun_fundslog.SourceId;
            paras.Add(sourceidpara);

            SqlParameter logstatuspara = new SqlParameter("@LogStatus", SqlDbType.Int, 4);
            logstatuspara.Value = fun_fundslog.LogStatus;
            paras.Add(logstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);

            return paras;
        }

        #endregion
    }
}
