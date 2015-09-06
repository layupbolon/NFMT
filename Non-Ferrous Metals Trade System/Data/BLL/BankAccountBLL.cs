/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BankAccountBLL.cs
// 文件功能描述：银行账号dbo.BankAccount业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年5月4日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Data.Model;
using NFMT.Data.DAL;
using NFMT.Data.IDAL;
using NFMT.Common;
using System.Text;

namespace NFMT.Data.BLL
{
    /// <summary>
    /// 银行账号dbo.BankAccount业务逻辑类。
    /// </summary>
    public class BankAccountBLL : Common.DataBLL
    {
        private BankAccountDAL bankaccountDAL = new BankAccountDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(BankAccountDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BankAccountBLL()
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
            get { return this.bankaccountDAL; }
        }
        #endregion

        #region 新增方法

        /// <summary>
        /// 获取分页查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="companyId"></param>
        /// <param name="key"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int companyId, string accountNo, int bankId, int currencyId, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "BA.BankAccId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " bk.BankAccId,bk.CurrencyId as CurrencyId,bk.BankId as BankId, bk.CompanyId as CorpId,CorpName,BankName,AccountNo,CurrencyName,BankAccDesc,bd.StatusName ";

            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append(" dbo.BankAccount bk ");
            sb.AppendFormat(" left join dbo.BDStatusDetail bd on bk.BankAccStatus = bd.DetailId and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);
            sb.Append(" left join dbo.Bank b on b.BankId = bk.BankId ");
            sb.Append(" left join dbo.Currency c on c.CurrencyId = bk.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Corporation cp on cp.CorpId = bk.CompanyId ");

            select.TableName = sb.ToString();
            sb.Clear();

            sb.Append(" 1 = 1 ");

            if (companyId > 0)
                sb.AppendFormat(" and bk.CompanyId = {0} ", companyId);
            if (!string.IsNullOrEmpty(accountNo))
                sb.AppendFormat(" and bk.AccountNo like '%{0}%' ", accountNo);
            if (currencyId > 0)
                sb.AppendFormat(" and bk.CurrencyId = {0} ", currencyId);
            if (bankId > 0)
                sb.AppendFormat(" and bk.BankId = {0} ", bankId);
            if (status > 0)
                sb.AppendFormat(" and bk.BankAccStatus = {0} ", status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel InsertOrUpdateBankAccountInfo(UserModel user, string bankName, string bankAccountName,int corpId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = bankaccountDAL.InsertOrUpdateBankAccountInfo(user, bankName, bankAccountName, corpId);
            }
            catch (Exception e)
            {
                this.log.ErrorFormat("新增或修改银行信息错误，{0}", e.Message);
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        #endregion

        #region 重载方法

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                Model.BankAccount obj1 = (Model.BankAccount)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, obj.Id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.BankAccount resultObj = (Model.BankAccount)result.ReturnValue;

                    if (resultObj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }
                    resultObj.BankAccId = obj1.BankAccId;
                    resultObj.CompanyId = obj1.CompanyId;
                    resultObj.BankId = obj1.BankId;
                    resultObj.CurrencyId = obj1.CurrencyId;
                    resultObj.BankAccDesc = obj1.BankAccDesc;
                    resultObj.AccountNo = obj1.AccountNo;
                    resultObj.BankAccStatus = obj1.BankAccStatus;

                    result = this.Operate.Update(user, resultObj);

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
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        #endregion

    }
}
