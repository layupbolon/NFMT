/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInBLL.cs
// 文件功能描述：收款dbo.Fun_CashIn业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年9月18日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Funds.Model;
using NFMT.Funds.DAL;
using NFMT.Funds.IDAL;
using NFMT.Common;
using System.Linq;

namespace NFMT.Funds.BLL
{
    /// <summary>
    /// 收款dbo.Fun_CashIn业务逻辑类。
    /// </summary>
    public class CashInBLL : Common.ApplyBLL
    {
        private CashInDAL cashinDAL = new CashInDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CashInDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CashInBLL()
        {
        }

        #endregion

        #region 数据库操作

        protected override log4net.ILog Log
        {
            get { return this.log; }
        }

        public override IOperate Operate
        {
            get { return this.cashinDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, DateTime fromDate, DateTime toDate, int empId, int bank, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ci.CashInId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" ci.CashInId,ci.CashInDate,c.CorpName as InnerCorp,CONVERT(varchar,ci.CashInBala)+cu.CurrencyName as CashInBala,b.BankName,ci.PayCorpName as OutCorp,ci.CashInStatus,bd.StatusName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_CashIn ci ");
            sb.Append(" left join NFMT_User.dbo.Corporation c on ci.CashInCorpId = c.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cu on ci.CurrencyId = cu.CurrencyId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank b on ci.CashInBank = b.BankId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on ci.CashInStatus = bd.DetailId and StatusId = {0} ", statusId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1");
            if (status > 0)
                sb.AppendFormat(" and ci.CashInStatus = {0} ", status);
            if (empId > 0)
                sb.AppendFormat(" and ci.CashInEmpId = {0} ", empId);
            if (bank > 0)
                sb.AppendFormat(" and ci.CashInBank = {0} ", bank);
            if (fromDate > Common.DefaultValue.DefaultTime && toDate >= fromDate)
                sb.AppendFormat(" and ci.CashInDate between '{0}' and '{1}' ", fromDate.ToString(), toDate.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Create(UserModel user, Model.CashIn cashIn)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {

                    NFMT.User.Model.Corporation innerCorp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == cashIn.CashInCorpId);
                    if (innerCorp != null && innerCorp.CorpId > 0)
                        cashIn.CashInBlocId = innerCorp.ParentId;

                    cashIn.CashInEmpId = user.EmpId;

                    NFMT.User.Model.Corporation outCorp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == cashIn.PayCorpId);
                    if (outCorp != null && outCorp.CorpId > 0)
                    {
                        cashIn.PayBlocId = outCorp.ParentId;

                        if (string.IsNullOrEmpty(cashIn.PayCorpName))
                            cashIn.PayCorpName = outCorp.CorpName;
                    }

                    if (string.IsNullOrEmpty(cashIn.PayBank))
                    {
                        NFMT.Data.Model.Bank bank = NFMT.Data.BasicDataProvider.Banks.SingleOrDefault(a => a.BankId == cashIn.PayBankId);
                        if (bank != null && bank.BankId > 0)
                            cashIn.PayBank = bank.BankName;
                    }

                    if (string.IsNullOrEmpty(cashIn.PayAccount))
                    {
                        NFMT.Data.Model.BankAccount bankAccount = NFMT.Data.BasicDataProvider.BankAccounts.SingleOrDefault(a => a.BankAccId == cashIn.PayAccountId);
                        if (bankAccount != null && bankAccount.BankAccId > 0)
                            cashIn.PayAccount = bankAccount.AccountNo;
                    }

                    result = this.cashinDAL.Insert(user, cashIn);
                    if (result.ResultStatus != 0)
                        return result;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel Update(UserModel user, Model.CashIn cashIn)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {

                    NFMT.User.Model.Corporation innerCorp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == cashIn.CashInCorpId);
                    if (innerCorp != null && innerCorp.CorpId > 0)
                        cashIn.CashInBlocId = innerCorp.ParentId;

                    cashIn.CashInEmpId = user.EmpId;

                    NFMT.User.Model.Corporation outCorp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == cashIn.PayCorpId);
                    if (outCorp != null && outCorp.CorpId > 0)
                    {
                        cashIn.PayBlocId = outCorp.ParentId;

                        if (string.IsNullOrEmpty(cashIn.PayCorpName))
                            cashIn.PayCorpName = outCorp.CorpName;
                    }

                    if (string.IsNullOrEmpty(cashIn.PayBank))
                    {
                        NFMT.Data.Model.Bank bank = NFMT.Data.BasicDataProvider.Banks.SingleOrDefault(a => a.BankId == cashIn.PayBankId);
                        if (bank != null && bank.BankId > 0)
                            cashIn.PayBank = bank.BankName;
                    }

                    if (string.IsNullOrEmpty(cashIn.PayAccount))
                    {
                        NFMT.Data.Model.BankAccount bankAccount = NFMT.Data.BasicDataProvider.BankAccounts.SingleOrDefault(a => a.BankAccId == cashIn.PayAccountId);
                        if (bankAccount != null && bankAccount.BankAccId > 0)
                            cashIn.PayAccount = bankAccount.AccountNo;
                    }

                    //验证收款
                    result = this.cashinDAL.Get(user, cashIn.CashInId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashIn resultObj = result.ReturnValue as Model.CashIn;
                    if (resultObj == null || resultObj.CashInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收款不存在，不能修改";
                        return result;
                    }

                    resultObj.CashInDate = cashIn.CashInDate;
                    resultObj.CashInCorpId = cashIn.CashInCorpId;
                    resultObj.CurrencyId = cashIn.CurrencyId;
                    resultObj.CashInBala = cashIn.CashInBala;
                    resultObj.CashInBank = cashIn.CashInBank;
                    resultObj.CashInAccoontId = cashIn.CashInAccoontId;
                    resultObj.PayWord = cashIn.PayWord;
                    resultObj.PayCorpId = cashIn.PayCorpId;
                    resultObj.PayCorpName = cashIn.PayCorpName;
                    resultObj.PayBankId = cashIn.PayBankId;
                    resultObj.PayBank = cashIn.PayBank;
                    resultObj.PayAccountId = cashIn.PayAccountId;
                    resultObj.PayAccount = cashIn.PayAccount;
                    resultObj.BankLog = cashIn.BankLog;
                    resultObj.CashInMode = cashIn.CashInMode;

                    result = this.cashinDAL.Update(user, resultObj);
                    if (result.ResultStatus != 0)
                        return result;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel Goback(UserModel user, int cashInId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取收款登记
                    result = this.cashinDAL.Get(user, cashInId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashIn cashIn = result.ReturnValue as Model.CashIn;
                    if (cashIn == null || cashIn.CashInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收款登记不存在";
                        return result;
                    }

                    result = this.cashinDAL.Goback(user, cashIn);

                    //工作流任务关闭
                    WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, cashIn);
                    if (result.ResultStatus != 0)
                        return result;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel Invalid(UserModel user, int cashInId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取收款登记
                    result = this.cashinDAL.Get(user, cashInId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashIn cashIn = result.ReturnValue as Model.CashIn;
                    if (cashIn == null || cashIn.CashInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收款登记不存在";
                        return result;
                    }

                    result = this.cashinDAL.Invalid(user, cashIn);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel Confirm(UserModel user, int cashInId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取收款登记
                    result = this.cashinDAL.Get(user, cashInId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashIn cashIn = result.ReturnValue as Model.CashIn;
                    if (cashIn == null || cashIn.CashInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收款登记不存在";
                        return result;
                    }

                    result = this.cashinDAL.Confirm(user, cashIn);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel ConfirmCancel(UserModel user, int cashInId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取收款登记
                    result = this.cashinDAL.Get(user, cashInId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashIn cashIn = result.ReturnValue as Model.CashIn;
                    if (cashIn == null || cashIn.CashInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收款登记不存在";
                        return result;
                    }

                    result = this.cashinDAL.ConfirmCancel(user, cashIn);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel CheckContractSubCashInConfirm(UserModel user, int subId)
        {
            return this.cashinDAL.CheckContractSubCashInConfirm(user, subId);
        }

        #endregion

        #region report

        public SelectModel GetCashInReportSelect(int pageIndex, int pageSize, string orderStr, int cashInCorpId, int cashInBank, DateTime startDate, DateTime endDate)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ci.CashInId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)Common.StatusEnum.已生效;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            select.ColumnName = " ci.CashInId,ci.CashInDate,CONVERT(varchar,ci.CashInBala)+cur.CurrencyName as CashInBala,bank.BankName,corp.CorpName ";

            sb.Clear();
            sb.Append(" NFMT..Fun_CashIn ci ");
            sb.Append(" left join NFMT_Basic..Currency cur on cur.CurrencyId = ci.CurrencyId ");
            sb.Append(" left join NFMT_Basic..Bank bank on bank.BankId = ci.CashInBank ");
            sb.Append(" left join NFMT_User..Corporation corp on corp.CorpId = ci.CashInCorpId ");

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" ci.CashInStatus >= {0} ", readyStatus);
            if (cashInCorpId > 0)
                sb.AppendFormat(" and ci.CashInCorpId = {0} ", cashInCorpId);
            if (cashInBank > 0)
                sb.AppendFormat(" and ci.CashInBank = {0} ", cashInBank);
            if (startDate > Common.DefaultValue.DefaultTime && endDate > startDate)
                sb.AppendFormat(" and ci.CashInDate between '{0}' and '{1}' ", startDate.ToString(), endDate.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        //public override string[,] SetExcelRangeData(DataTable source)
        //{
        //    string[,] objData = new string[source.Rows.Count, 8];

        //    for (int i = 0; i < source.Rows.Count; i++)
        //    {
        //        System.Data.DataRow dr = source.Rows[i];

        //        objData[i, 0] = ((DateTime)dr["CustomsDate"]).ToString("yyyy-MM-dd");
        //        objData[i, 1] = dr["RefNo"].ToString();
        //        objData[i, 2] = dr["GrossWeight"].ToString();
        //        objData[i, 3] = dr["NetWeight"].ToString();
        //        objData[i, 4] = dr["MUName"].ToString();
        //        objData[i, 5] = dr["CorpName"].ToString();
        //        objData[i, 6] = dr["CurrencyName"].ToString();
        //        objData[i, 7] = dr["CustomsPrice"].ToString();
        //    }

        //    return objData;
        //}

        #endregion

        #region 新收款分配

        public SelectModel GetCanAllotSelectModel(int pageIndex, int pageSize, string orderStr,DateTime fromDate, DateTime toDate, int empId, int bank, int status)
        {
            SelectModel select = this.GetSelectModel(pageIndex, pageSize, orderStr, fromDate, toDate, empId, bank, status);
            select.ColumnName += ",Convert(varchar, ci.CashInBala - ISNULL(ref.bala,0)) + cu.CurrencyName CanAllotBala ";
            select.TableName += string.Format(" left join (select ref.CashInId,SUM(ISNULL(ref.AllotBala,0)) bala  from dbo.Fun_CashInCorp_Ref ref where ref.DetailStatus >={0}  group by ref.CashInId) ref on ci.CashInId = ref.CashInId ", (int)Common.StatusEnum.已生效);
            select.WhereStr += " and ci.CashInBala - ISNULL(ref.bala,0) > 0 ";
            return select;
        }

        #endregion
    }
}
