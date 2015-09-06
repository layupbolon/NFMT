/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：EmployeeBLL.cs
// 文件功能描述：员工表dbo.Employee业务逻辑类。
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
    /// 员工表dbo.Employee业务逻辑类。
    /// </summary>
    public class EmployeeBLL : Common.DataBLL
    {
        private EmployeeDAL employeeDAL = new EmployeeDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(EmployeeDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public EmployeeBLL()
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
            get { return this.employeeDAL; }
        }

        #endregion

        #region 新增方法

        /// <summary>
        /// 获取分页查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="name"></param>
        /// <param name="empCode"></param>
        /// <param name="workStatus"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, string name, string empCode, int workStatus)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "emp.EmpId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            select.ColumnName = " emp.EmpId,acc.AccountName,dep.DeptId,cor.CorpId,blo.BlocId,dep.DeptName,cor.CorpName,blo.BlocName,emp.EmpCode,emp.Name,case emp.Sex when 0 then '女' when 1 then '男' else '' end as Sex,CONVERT(varchar(12),emp.BirthDay,111) as BirthDay,emp.Telephone,emp.Phone,BD.StatusName";

            sb.Append(" dbo.Employee emp ");
            sb.Append(" left join dbo.Account acc on emp.EmpId = acc.EmpId ");
            sb.Append(" left join dbo.Department dep on emp.DeptId = dep.DeptId ");
            sb.Append(" left join dbo.Corporation cor on cor.CorpId = dep.CorpId ");
            sb.Append(" left join dbo.Bloc blo on blo.BlocId = cor.ParentId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail BD on BD.DetailId = emp.WorkStatus and BD.StatusId = {0} ", (int)Common.StatusTypeEnum.在职状态);

            //sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail BD on BD.DetailId = E.WorkStatus and BD.StatusId = {0} ", (int)Common.StatusTypeEnum.在职状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (workStatus > 0)
                sb.AppendFormat(" and emp.WorkStatus = {0}", workStatus);
            if (!string.IsNullOrEmpty(name))
                sb.AppendFormat(" and emp.Name like '%{0}%'", name);
            if (!string.IsNullOrEmpty(empCode))
                sb.AppendFormat(" and emp.EmpCode like '%{0}%'", empCode);

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 获取分页查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="name"></param>
        /// <param name="empCode"></param>
        /// <param name="workStatus"></param>
        /// <param name="excepteEmpId"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, string name, string empCode, int workStatus, int excepteEmpId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "emp.EmpId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            select.ColumnName = " emp.EmpId,dep.DeptId,cor.CorpId,blo.BlocId,dep.DeptName,cor.CorpName,blo.BlocName,emp.EmpCode,emp.Name,case emp.Sex when 0 then '女' when 1 then '男' else '' end as Sex,CONVERT(varchar(12),emp.BirthDay,111) as BirthDay,emp.Telephone,emp.Phone,BD.StatusName";

            sb.Append(" dbo.Employee emp ");
            sb.Append(" left join dbo.Department dep on emp.DeptId = dep.DeptId ");
            sb.Append(" left join dbo.Corporation cor on cor.CorpId = dep.CorpId ");
            sb.Append(" left join dbo.Bloc blo on blo.BlocId = cor.ParentId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail BD on BD.DetailId = emp.WorkStatus and BD.StatusId = {0} ", (int)Common.StatusTypeEnum.在职状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (workStatus > 0)
                sb.AppendFormat(" and emp.WorkStatus = {0}", workStatus);
            if (!string.IsNullOrEmpty(name))
                sb.AppendFormat(" and emp.Name like '%{0}%'", name);
            if (!string.IsNullOrEmpty(empCode))
                sb.AppendFormat(" and emp.EmpCode like '%{0}%'", empCode);
            if (excepteEmpId > 0)
                sb.AppendFormat(" and emp.EmpId <> {0}", excepteEmpId);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetDeptEmpSelect(int pageIndex, int pageSize, string orderStr, int deptId, bool isHas = false, string empName = "")
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int status = (int)NFMT.Common.StatusEnum.已生效;

            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();
            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "E.EmpId desc";
            else
                select.OrderStr = orderStr;
            select.ColumnName = " E.EmpId,E.EmpCode,E.Name,case E.Sex when 0 then '女' when 1 then '男' else '' end as Sex,CONVERT(varchar(12),E.BirthDay,111) as BirthDay,E.Telephone,E.Phone,BD.StatusName ,de.DeptId,de.DeptEmpId";

            sb.Append(" dbo.Employee E ");
            sb.Append(" left join NFMT_Basic.dbo.BDStatusDetail BD on BD.DetailId = E.WorkStatus ");

            sb.AppendFormat("left join (select EmpId,DeptId,DeptEmpId from DeptEmp where DeptId = {0} and RefStatus = {1}) as de on de.EmpId = E.EmpId ", deptId, status);

            select.TableName = sb.ToString();
            sb.Clear();

            sb.Append(" 1=1 ");
            if (isHas)
                sb.AppendFormat(" and de.DeptId = {0} ", deptId);
            else
            {
                if (!string.IsNullOrEmpty(empName))
                    sb.AppendFormat(" and E.Name like '%{0}%'", empName);
                if (deptId > 0)
                    sb.AppendFormat(" and de.EmpId is null ", deptId, status);
            }
            select.WhereStr = sb.ToString();


            return select;
        }

        public SelectModel GetEmpRoleSelect(int pageIndex, int pageSize, string orderStr, int roleId, string name, int type)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int status = (int)NFMT.Common.StatusEnum.已生效;

            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();
            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "emp.EmpId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " emp.EmpId,emp.EmpCode,emp.Name,case emp.Sex when 0 then '女' when 1 then '男' else '' end as Sex,CONVERT(varchar(12),emp.BirthDay,111) as BirthDay,emp.Telephone,emp.Phone,BD.StatusName ,er.EmpRoleId";

            sb.Append(" dbo.Employee emp ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail BD on BD.DetailId = emp.WorkStatus and BD.StatusId = {0} ", (int)Common.StatusTypeEnum.在职状态);
            sb.AppendFormat(" left join (select EmpId,RoleId,EmpRoleId from dbo.EmpRole where RoleId = {0} and RefStatus = {1}) as er on er.EmpId = emp.EmpId ", roleId, status);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (type == 0)
            {
                if (roleId > 0)
                    sb.AppendFormat(" and er.RoleId = {0}", roleId);
            }
            else if (type == 1)
            {
                sb.AppendFormat(" and emp.EmpId not in (select EmpId from dbo.EmpRole where RoleId = {0} and RefStatus = {1}) ", roleId, status);
                if (!string.IsNullOrEmpty(name))
                    sb.AppendFormat(" and emp.Name like '%{0}%'", name);
            }

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 获取在职状态
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultModel GetWorkStatusList(UserModel user)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = employeeDAL.GetWorkStatusList(user);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }
            return result;
        }

        public ResultModel CreateHandler(UserModel user, Model.Employee emp, Model.Account acc)
        {
            ResultModel result = new ResultModel();
            DAL.AccountDAL accountDAL = new AccountDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = accountDAL.ValidateAccountName(user, acc.AccountName);
                    if (result.ResultStatus != 0)
                        return result;

                    result = employeeDAL.Insert(user, emp);
                    if (result.ResultStatus != 0)
                        return result;

                    int empId = (int)result.ReturnValue;

                    acc.EmpId = empId;
                    acc.IsValid = true;
                    acc.AccStatus = StatusEnum.已生效;
                    
                    result = accountDAL.Insert(user, acc);
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

        #region 重载方法

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                Model.Employee obj1 = (Model.Employee)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, obj.Id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Employee resultObj = (Model.Employee)result.ReturnValue;

                    if (resultObj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }

                    resultObj.DeptId = obj1.DeptId;
                    resultObj.EmpCode = obj1.EmpCode;
                    resultObj.Name = obj1.Name;
                    resultObj.Sex = obj1.Sex;
                    resultObj.BirthDay = obj1.BirthDay;
                    resultObj.Telephone = obj1.Telephone;
                    resultObj.Phone = obj1.Phone;
                    resultObj.WorkStatus = obj1.WorkStatus;

                    //result = employeeDAL.IsEmpCodeRepeat(user, resultObj.EmpCode);
                    //if (result.ResultStatus != 0)
                    //    return result;

                    result = this.Operate.Update(user, resultObj);
                    if (result.ResultStatus != 0)
                        return result;

                    if (resultObj.WorkStatus != (int)WorkStatusEnum.在职)
                    {
                        DAL.AccountDAL accountDAL = new AccountDAL();
                        result = accountDAL.UpdateAccountValidate(user, resultObj.EmpId);
                        if (result.ResultStatus != 0)
                            return result;
                    }

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

        public override ResultModel Insert(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    Model.Employee employee = (Model.Employee)obj;

                    result = employeeDAL.IsEmpCodeRepeat(user, employee.EmpCode);
                    if (result.ResultStatus != 0)
                        return result;

                    result = this.Operate.Insert(user, obj);
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
