/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：EmployeeContactBLL.cs
// 文件功能描述：联系人员工关系表dbo.EmployeeContact业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
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
    /// 联系人员工关系表dbo.EmployeeContact业务逻辑类。
    /// </summary>
    public class EmployeeContactBLL : Common.DataBLL
    {
        private EmployeeContactDAL employeecontactDAL = new EmployeeContactDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(EmployeeContactDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public EmployeeContactBLL()
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
            get { return this.employeecontactDAL; }
        }

        public ResultModel InsertRange(UserModel user, List<EmployeeContact> employeecontacts)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (EmployeeContact empContact in employeecontacts)
                    {
                        result = this.Insert(user, empContact);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ResultStatus = -1;
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

        public ResultModel Invalid(UserModel user, List<EmployeeContact> employeecontacts)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (EmployeeContact empContact in employeecontacts)
                    {
                        if (empContact.Status == Common.StatusEnum.已生效)
                            empContact.Status = Common.StatusEnum.已录入;
                        result = employeecontactDAL.Invalid(user, empContact);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ResultStatus = -1;
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

        public ResultModel ContactTransferHandler(UserModel user,List<Model.EmployeeContact> employeecontacts,List<Model.EmployeeContact> invalidemployeecontacts)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (EmployeeContact empContact in employeecontacts)
                    {
                        result = this.Insert(user, empContact);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    foreach (EmployeeContact empContact in invalidemployeecontacts)
                    {
                        empContact.Status = Common.StatusEnum.已录入;
                        result = employeecontactDAL.Invalid(user, empContact);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ResultStatus = -1;
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
