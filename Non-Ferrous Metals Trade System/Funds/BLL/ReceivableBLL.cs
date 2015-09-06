/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ReceivableBLL.cs
// 文件功能描述：收款dbo.Fun_Receivable业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
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
    /// 收款dbo.Fun_Receivable业务逻辑类。
    /// </summary>
    public class ReceivableBLL : Common.ExecBLL
    {
        private ReceivableDAL receivableDAL = new ReceivableDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ReceivableDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReceivableBLL()
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
            get { return this.receivableDAL; }
        }

        /// <summary>
        /// 更新fun_receivable
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="fun_receivable">Receivable对象</param>
        /// <returns></returns>
        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();
            if (obj == null)
            {
                result.Message = "该数据不存在，不能更新";
                return result;
            }

            Receivable fun_receivable = (Receivable)obj;

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, fun_receivable.ReceivableId);
                    if (result.ResultStatus != 0)
                        return result;
                    Receivable resultObj = result.ReturnValue as Receivable;
                    if (resultObj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }

                    if (resultObj.Status != StatusEnum.已录入)
                    {
                        result.ResultStatus = -1;
                        result.Message = "非已录入状态的数据不允许修改";
                        return result;
                    }
                    resultObj.ReceiveEmpId = fun_receivable.ReceiveEmpId;
                    resultObj.ReceiveDate = fun_receivable.ReceiveDate;
                    resultObj.ReceivableGroupId = fun_receivable.ReceivableGroupId;
                    resultObj.ReceivableCorpId = fun_receivable.ReceivableCorpId;
                    resultObj.CurrencyId = fun_receivable.CurrencyId;
                    resultObj.PayBala = fun_receivable.PayBala;
                    resultObj.ReceivableBank = fun_receivable.ReceivableBank;
                    resultObj.ReceivableAccoontId = fun_receivable.ReceivableAccoontId;
                    resultObj.PayWord = fun_receivable.PayWord;
                    resultObj.PayGroupId = fun_receivable.PayGroupId;
                    resultObj.PayCorpId = fun_receivable.PayCorpId;
                    resultObj.PayBankId = fun_receivable.PayBankId;
                    resultObj.PayAccountId = fun_receivable.PayAccountId;
                    resultObj.PayCorpName = fun_receivable.PayCorpName;
                    resultObj.PayBank = fun_receivable.PayBank;
                    resultObj.PayAccount = fun_receivable.PayAccount;
                    resultObj.BankLog = fun_receivable.BankLog;

                    result = receivableDAL.Update(user, resultObj);

                    if (result.ResultStatus == 0)
                        scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, DateTime fromDate, DateTime toDate, int empId, int bank, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "r.ReceivableId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" r.ReceivableId,r.ReceiveDate,c.CorpName as InnerCorp,CONVERT(varchar,r.PayBala)+cu.CurrencyName as PayBala,b.BankName,r.PayCorpName as OutCorp,r.ReceiveStatus,bd.StatusName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_Receivable r left join NFMT_User.dbo.Corporation c on r.ReceivableCorpId = c.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cu on r.CurrencyId = cu.CurrencyId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank b on r.ReceivableBank = b.BankId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c2 on r.PayCorpId = c2.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on r.ReceiveStatus = bd.DetailId and StatusId = {0} ", statusId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1");
            if (status > 0)
                sb.AppendFormat(" and r.ReceiveStatus = {0} ", status);
            if (empId > 0)
                sb.AppendFormat(" and r.ReceiveEmpId = {0} ", empId);
            if (bank > 0)
                sb.AppendFormat(" and r.ReceivableBank = {0} ", bank);
            if (fromDate > Common.DefaultValue.DefaultTime && toDate >= fromDate)
                sb.AppendFormat(" and r.ReceiveDate between '{0}' and '{1}' ", fromDate.ToString(), toDate.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Create(UserModel user, Model.Receivable receivable)
        {
            ResultModel result = new ResultModel();

            try 
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {

                    NFMT.User.Model.Corporation innerCorp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == receivable.ReceivableCorpId);
                    if (innerCorp != null)
                        receivable.ReceivableGroupId = innerCorp.ParentId;

                    receivable.ReceiveEmpId = user.EmpId;

                    NFMT.User.Model.Corporation outCorp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == receivable.PayCorpId);
                    if (outCorp != null)
                    {
                        receivable.PayGroupId = outCorp.ParentId;

                        if (string.IsNullOrEmpty(receivable.PayCorpName))
                            receivable.PayCorpName = outCorp.CorpName;
                    }

                    if (string.IsNullOrEmpty(receivable.PayBank))
                    {
                        NFMT.Data.Model.Bank bank = NFMT.Data.BasicDataProvider.Banks.SingleOrDefault(a => a.BankId == receivable.PayBankId);
                        if(bank!=null)
                            receivable.PayBank = bank.BankName;
                    }

                    if (string.IsNullOrEmpty(receivable.PayAccount))
                    {
                        NFMT.Data.Model.BankAccount bankAccount = NFMT.Data.BasicDataProvider.BankAccounts.SingleOrDefault(a => a.BankAccId == receivable.PayAccountId);
                        if(bankAccount!=null)
                            receivable.PayAccount = bankAccount.AccountNo;
                    }
                    
                    result = this.receivableDAL.Insert(user, receivable);
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

        public ResultModel Invalid(UserModel user, int receivableId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, receivableId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.Receivable receivable = result.ReturnValue as Receivable;

                    if (receivable == null)
                    {
                        result.Message = "该数据不存在，不能作废";
                        return result;
                    }

                    if (receivable.Status != StatusEnum.已录入)
                    {
                        result.Message = "非已录入状态的数据不允许作废";
                        return result;
                    }

                    result = receivableDAL.Invalid(user, receivable);

                    if (result.ResultStatus == 0)
                        scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        /// <summary>
        /// 数据撤返
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="fun_receivable">Receivable对象</param>
        /// <returns></returns>
        public ResultModel GoBack(UserModel user, int receivableId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, receivableId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.Receivable receivable = result.ReturnValue as Receivable;

                    if (receivable == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能撤返";
                        return result;
                    }

                    if (receivable.Status != StatusEnum.待审核 && receivable.Status != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "非待审核或已生效状态的数据不允许撤返";
                        return result;
                    }

                    result = receivableDAL.Goback(user, receivable);

                    if (result.ResultStatus != 0)
                        return result;

                    //工作流任务关闭
                    WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, receivable);
                    if (result.ResultStatus != 0)
                        return result;
                    
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Confirm(UserModel user, int receivableId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, receivableId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.Receivable receivable = result.ReturnValue as Receivable;

                    if (receivable == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收款登记不存在";
                        return result;
                    }

                    //获取当前收款登记下的所有分配


                    //验证收款分配是否有未完成
                    //验证是否有未分配的余额

                    result = receivableDAL.Confirm(user, receivable);

                    if (result.ResultStatus != 0)
                        return result;
                   
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        #endregion
    }
}
