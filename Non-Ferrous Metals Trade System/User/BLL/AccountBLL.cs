/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AccountBLL.cs
// 文件功能描述：账户表dbo.Account业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年5月4日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.User.Model;
using NFMT.User.DAL;
using NFMT.User.IDAL;
using NFMT.Common;

namespace NFMT.User.BLL
{
    /// <summary>
    /// 账户表dbo.Account业务逻辑类。
    /// </summary>
    public class AccountBLL : Common.DataBLL
    {
        private AccountDAL accountDAL = new AccountDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(AccountDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountBLL()
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
            get { return this.accountDAL; }
        }
        #endregion

        #region 新增方法

        public ResultModel CheckLogin(string userName, string passWord)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = accountDAL.CheckLogin(userName.Trim(), passWord);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("验证用户登录失败，失败原因：{0}", result.Message);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("验证用户登录，验证结果为：{0}，提示信息为：", result.ReturnValue, result.Message);
            }

            return result;
        }

        public ResultModel ValidateAccountName(UserModel user, string accountName)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = accountDAL.ValidateAccountName(user, accountName);
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

        public ResultModel ChangePwd(UserModel user, string oldPwd, string newPwd)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = accountDAL.ValidatePwd(user, oldPwd);
                    if (result.ResultStatus != 0)
                        return result;

                    result = accountDAL.ChangePwd(user, newPwd);
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

        public ResultModel ResetPassword(UserModel user, int empId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = accountDAL.ResetPassword(user, empId);
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
