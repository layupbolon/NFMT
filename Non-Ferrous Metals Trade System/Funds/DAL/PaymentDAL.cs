/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PaymentDAL.cs
// 文件功能描述：财务付款dbo.Fun_Payment数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年12月24日
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
    /// 财务付款dbo.Fun_Payment数据交互类。
    /// </summary>
    public class PaymentDAL : ExecOperate, IPaymentDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PaymentDAL()
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
            Payment fun_payment = (Payment)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@PaymentId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter payapplyidpara = new SqlParameter("@PayApplyId", SqlDbType.Int, 4);
            payapplyidpara.Value = fun_payment.PayApplyId;
            paras.Add(payapplyidpara);

            if (!string.IsNullOrEmpty(fun_payment.PaymentCode))
            {
                SqlParameter paymentcodepara = new SqlParameter("@PaymentCode", SqlDbType.VarChar, 20);
                paymentcodepara.Value = fun_payment.PaymentCode;
                paras.Add(paymentcodepara);
            }

            SqlParameter paybalapara = new SqlParameter("@PayBala", SqlDbType.Decimal, 9);
            paybalapara.Value = fun_payment.PayBala;
            paras.Add(paybalapara);

            SqlParameter fundsbalapara = new SqlParameter("@FundsBala", SqlDbType.Decimal, 9);
            fundsbalapara.Value = fun_payment.FundsBala;
            paras.Add(fundsbalapara);

            SqlParameter virtualbalapara = new SqlParameter("@VirtualBala", SqlDbType.Decimal, 9);
            virtualbalapara.Value = fun_payment.VirtualBala;
            paras.Add(virtualbalapara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_payment.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter paystylepara = new SqlParameter("@PayStyle", SqlDbType.Int, 4);
            paystylepara.Value = fun_payment.PayStyle;
            paras.Add(paystylepara);

            SqlParameter paybankidpara = new SqlParameter("@PayBankId", SqlDbType.Int, 4);
            paybankidpara.Value = fun_payment.PayBankId;
            paras.Add(paybankidpara);

            SqlParameter paybankaccountidpara = new SqlParameter("@PayBankAccountId", SqlDbType.Int, 4);
            paybankaccountidpara.Value = fun_payment.PayBankAccountId;
            paras.Add(paybankaccountidpara);

            SqlParameter paycorppara = new SqlParameter("@PayCorp", SqlDbType.Int, 4);
            paycorppara.Value = fun_payment.PayCorp;
            paras.Add(paycorppara);

            SqlParameter paydeptpara = new SqlParameter("@PayDept", SqlDbType.Int, 4);
            paydeptpara.Value = fun_payment.PayDept;
            paras.Add(paydeptpara);

            SqlParameter payempidpara = new SqlParameter("@PayEmpId", SqlDbType.Int, 4);
            payempidpara.Value = fun_payment.PayEmpId;
            paras.Add(payempidpara);

            SqlParameter paydatetimepara = new SqlParameter("@PayDatetime", SqlDbType.DateTime, 8);
            paydatetimepara.Value = fun_payment.PayDatetime;
            paras.Add(paydatetimepara);

            SqlParameter recevablecorppara = new SqlParameter("@RecevableCorp", SqlDbType.Int, 4);
            recevablecorppara.Value = fun_payment.RecevableCorp;
            paras.Add(recevablecorppara);

            SqlParameter recebankidpara = new SqlParameter("@ReceBankId", SqlDbType.Int, 4);
            recebankidpara.Value = fun_payment.ReceBankId;
            paras.Add(recebankidpara);

            SqlParameter recebankaccountidpara = new SqlParameter("@ReceBankAccountId", SqlDbType.Int, 4);
            recebankaccountidpara.Value = fun_payment.ReceBankAccountId;
            paras.Add(recebankaccountidpara);

            if (!string.IsNullOrEmpty(fun_payment.ReceBankAccount))
            {
                SqlParameter recebankaccountpara = new SqlParameter("@ReceBankAccount", SqlDbType.VarChar, 200);
                recebankaccountpara.Value = fun_payment.ReceBankAccount;
                paras.Add(recebankaccountpara);
            }

            SqlParameter paymentstatuspara = new SqlParameter("@PaymentStatus", SqlDbType.Int, 4);
            paymentstatuspara.Value = fun_payment.PaymentStatus;
            paras.Add(paymentstatuspara);

            if (!string.IsNullOrEmpty(fun_payment.FlowName))
            {
                SqlParameter flownamepara = new SqlParameter("@FlowName", SqlDbType.VarChar, 200);
                flownamepara.Value = fun_payment.FlowName;
                paras.Add(flownamepara);
            }

            if (!string.IsNullOrEmpty(fun_payment.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = fun_payment.Memo;
                paras.Add(memopara);
            }

            SqlParameter fundslogidpara = new SqlParameter("@FundsLogId", SqlDbType.Int, 4);
            fundslogidpara.Value = fun_payment.FundsLogId;
            paras.Add(fundslogidpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Payment payment = new Payment();

            int indexPaymentId = dr.GetOrdinal("PaymentId");
            payment.PaymentId = Convert.ToInt32(dr[indexPaymentId]);

            int indexPayApplyId = dr.GetOrdinal("PayApplyId");
            if (dr["PayApplyId"] != DBNull.Value)
            {
                payment.PayApplyId = Convert.ToInt32(dr[indexPayApplyId]);
            }

            int indexPaymentCode = dr.GetOrdinal("PaymentCode");
            if (dr["PaymentCode"] != DBNull.Value)
            {
                payment.PaymentCode = Convert.ToString(dr[indexPaymentCode]);
            }

            int indexPayBala = dr.GetOrdinal("PayBala");
            if (dr["PayBala"] != DBNull.Value)
            {
                payment.PayBala = Convert.ToDecimal(dr[indexPayBala]);
            }

            int indexFundsBala = dr.GetOrdinal("FundsBala");
            if (dr["FundsBala"] != DBNull.Value)
            {
                payment.FundsBala = Convert.ToDecimal(dr[indexFundsBala]);
            }

            int indexVirtualBala = dr.GetOrdinal("VirtualBala");
            if (dr["VirtualBala"] != DBNull.Value)
            {
                payment.VirtualBala = Convert.ToDecimal(dr[indexVirtualBala]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                payment.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexPayStyle = dr.GetOrdinal("PayStyle");
            if (dr["PayStyle"] != DBNull.Value)
            {
                payment.PayStyle = Convert.ToInt32(dr[indexPayStyle]);
            }

            int indexPayBankId = dr.GetOrdinal("PayBankId");
            if (dr["PayBankId"] != DBNull.Value)
            {
                payment.PayBankId = Convert.ToInt32(dr[indexPayBankId]);
            }

            int indexPayBankAccountId = dr.GetOrdinal("PayBankAccountId");
            if (dr["PayBankAccountId"] != DBNull.Value)
            {
                payment.PayBankAccountId = Convert.ToInt32(dr[indexPayBankAccountId]);
            }

            int indexPayCorp = dr.GetOrdinal("PayCorp");
            if (dr["PayCorp"] != DBNull.Value)
            {
                payment.PayCorp = Convert.ToInt32(dr[indexPayCorp]);
            }

            int indexPayDept = dr.GetOrdinal("PayDept");
            if (dr["PayDept"] != DBNull.Value)
            {
                payment.PayDept = Convert.ToInt32(dr[indexPayDept]);
            }

            int indexPayEmpId = dr.GetOrdinal("PayEmpId");
            if (dr["PayEmpId"] != DBNull.Value)
            {
                payment.PayEmpId = Convert.ToInt32(dr[indexPayEmpId]);
            }

            int indexPayDatetime = dr.GetOrdinal("PayDatetime");
            if (dr["PayDatetime"] != DBNull.Value)
            {
                payment.PayDatetime = Convert.ToDateTime(dr[indexPayDatetime]);
            }

            int indexRecevableCorp = dr.GetOrdinal("RecevableCorp");
            if (dr["RecevableCorp"] != DBNull.Value)
            {
                payment.RecevableCorp = Convert.ToInt32(dr[indexRecevableCorp]);
            }

            int indexReceBankId = dr.GetOrdinal("ReceBankId");
            if (dr["ReceBankId"] != DBNull.Value)
            {
                payment.ReceBankId = Convert.ToInt32(dr[indexReceBankId]);
            }

            int indexReceBankAccountId = dr.GetOrdinal("ReceBankAccountId");
            if (dr["ReceBankAccountId"] != DBNull.Value)
            {
                payment.ReceBankAccountId = Convert.ToInt32(dr[indexReceBankAccountId]);
            }

            int indexReceBankAccount = dr.GetOrdinal("ReceBankAccount");
            if (dr["ReceBankAccount"] != DBNull.Value)
            {
                payment.ReceBankAccount = Convert.ToString(dr[indexReceBankAccount]);
            }

            int indexPaymentStatus = dr.GetOrdinal("PaymentStatus");
            if (dr["PaymentStatus"] != DBNull.Value)
            {
                payment.PaymentStatus = (StatusEnum)Convert.ToInt32(dr[indexPaymentStatus]);
            }

            int indexFlowName = dr.GetOrdinal("FlowName");
            if (dr["FlowName"] != DBNull.Value)
            {
                payment.FlowName = Convert.ToString(dr[indexFlowName]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                payment.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexFundsLogId = dr.GetOrdinal("FundsLogId");
            if (dr["FundsLogId"] != DBNull.Value)
            {
                payment.FundsLogId = Convert.ToInt32(dr[indexFundsLogId]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                payment.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                payment.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                payment.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                payment.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return payment;
        }

        public override string TableName
        {
            get
            {
                return "Fun_Payment";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Payment fun_payment = (Payment)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter paymentidpara = new SqlParameter("@PaymentId", SqlDbType.Int, 4);
            paymentidpara.Value = fun_payment.PaymentId;
            paras.Add(paymentidpara);

            SqlParameter payapplyidpara = new SqlParameter("@PayApplyId", SqlDbType.Int, 4);
            payapplyidpara.Value = fun_payment.PayApplyId;
            paras.Add(payapplyidpara);

            if (!string.IsNullOrEmpty(fun_payment.PaymentCode))
            {
                SqlParameter paymentcodepara = new SqlParameter("@PaymentCode", SqlDbType.VarChar, 20);
                paymentcodepara.Value = fun_payment.PaymentCode;
                paras.Add(paymentcodepara);
            }

            SqlParameter paybalapara = new SqlParameter("@PayBala", SqlDbType.Decimal, 9);
            paybalapara.Value = fun_payment.PayBala;
            paras.Add(paybalapara);

            SqlParameter fundsbalapara = new SqlParameter("@FundsBala", SqlDbType.Decimal, 9);
            fundsbalapara.Value = fun_payment.FundsBala;
            paras.Add(fundsbalapara);

            SqlParameter virtualbalapara = new SqlParameter("@VirtualBala", SqlDbType.Decimal, 9);
            virtualbalapara.Value = fun_payment.VirtualBala;
            paras.Add(virtualbalapara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_payment.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter paystylepara = new SqlParameter("@PayStyle", SqlDbType.Int, 4);
            paystylepara.Value = fun_payment.PayStyle;
            paras.Add(paystylepara);

            SqlParameter paybankidpara = new SqlParameter("@PayBankId", SqlDbType.Int, 4);
            paybankidpara.Value = fun_payment.PayBankId;
            paras.Add(paybankidpara);

            SqlParameter paybankaccountidpara = new SqlParameter("@PayBankAccountId", SqlDbType.Int, 4);
            paybankaccountidpara.Value = fun_payment.PayBankAccountId;
            paras.Add(paybankaccountidpara);

            SqlParameter paycorppara = new SqlParameter("@PayCorp", SqlDbType.Int, 4);
            paycorppara.Value = fun_payment.PayCorp;
            paras.Add(paycorppara);

            SqlParameter paydeptpara = new SqlParameter("@PayDept", SqlDbType.Int, 4);
            paydeptpara.Value = fun_payment.PayDept;
            paras.Add(paydeptpara);

            SqlParameter payempidpara = new SqlParameter("@PayEmpId", SqlDbType.Int, 4);
            payempidpara.Value = fun_payment.PayEmpId;
            paras.Add(payempidpara);

            SqlParameter paydatetimepara = new SqlParameter("@PayDatetime", SqlDbType.DateTime, 8);
            paydatetimepara.Value = fun_payment.PayDatetime;
            paras.Add(paydatetimepara);

            SqlParameter recevablecorppara = new SqlParameter("@RecevableCorp", SqlDbType.Int, 4);
            recevablecorppara.Value = fun_payment.RecevableCorp;
            paras.Add(recevablecorppara);

            SqlParameter recebankidpara = new SqlParameter("@ReceBankId", SqlDbType.Int, 4);
            recebankidpara.Value = fun_payment.ReceBankId;
            paras.Add(recebankidpara);

            SqlParameter recebankaccountidpara = new SqlParameter("@ReceBankAccountId", SqlDbType.Int, 4);
            recebankaccountidpara.Value = fun_payment.ReceBankAccountId;
            paras.Add(recebankaccountidpara);

            if (!string.IsNullOrEmpty(fun_payment.ReceBankAccount))
            {
                SqlParameter recebankaccountpara = new SqlParameter("@ReceBankAccount", SqlDbType.VarChar, 200);
                recebankaccountpara.Value = fun_payment.ReceBankAccount;
                paras.Add(recebankaccountpara);
            }

            SqlParameter paymentstatuspara = new SqlParameter("@PaymentStatus", SqlDbType.Int, 4);
            paymentstatuspara.Value = fun_payment.PaymentStatus;
            paras.Add(paymentstatuspara);

            if (!string.IsNullOrEmpty(fun_payment.FlowName))
            {
                SqlParameter flownamepara = new SqlParameter("@FlowName", SqlDbType.VarChar, 200);
                flownamepara.Value = fun_payment.FlowName;
                paras.Add(flownamepara);
            }

            if (!string.IsNullOrEmpty(fun_payment.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = fun_payment.Memo;
                paras.Add(memopara);
            }

            SqlParameter fundslogidpara = new SqlParameter("@FundsLogId", SqlDbType.Int, 4);
            fundslogidpara.Value = fun_payment.FundsLogId;
            paras.Add(fundslogidpara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);

            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int payApplyId)
        {
            int entryStatus = (int)Common.StatusEnum.已录入;
            string cmdText = string.Format("select pay.* from dbo.Fun_Payment pay where pay.PayApplyId={0} and pay.PaymentStatus >={1}", payApplyId, entryStatus);
            return Load<Model.Payment>(user, CommandType.Text, cmdText);
        }

        public ResultModel GetCountByPayApplyId(UserModel user, int payApplyId)
        {
            ResultModel result = new ResultModel();
            try
            {
                int entryStatus = (int)Common.StatusEnum.已录入;
                int readyStatus = (int)Common.StatusEnum.已生效;

                string cmdText = string.Format("select count(*) from dbo.Fun_Payment pay where pay.PayApplyId={0} and pay.PaymentStatus >={1} and pay.PaymentStatus<={2}", payApplyId, entryStatus, readyStatus);

                object obj = SqlHelper.ExecuteScalar(ConnectString, CommandType.Text, cmdText, null);
                int count = 0;
                if (obj == null || !int.TryParse(obj.ToString(), out count))
                {
                    result.ResultStatus = -1;
                    result.Message = "查询失败";
                }

                result.ResultStatus = 0;
                result.ReturnValue = count;
                result.AffectCount = count;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public override IAuthority Authority
        {
            get
            {
                NFMT.Authority.CorpAuth auth = new Authority.CorpAuth();
                auth.AuthColumnNames.Add("pay.PayCorp");
                return auth;
            }
        }

        public override int MenuId
        {
            get
            {
                return 53;
            }
        }

        public override ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate)
        {
            if (operate == OperateEnum.修改)
            {
                ResultModel result = new ResultModel();
                result.ResultStatus = 0;
                return result;
            }

            return base.AllowOperate(user, obj, operate);
        }

        #endregion
    }
}
