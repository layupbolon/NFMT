/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContactBLL.cs
// 文件功能描述：联系人dbo.Contact业务逻辑类。
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
    /// 联系人dbo.Contact业务逻辑类。
    /// </summary>
    public class ContactBLL : Common.DataBLL
    {
        private ContactDAL contactDAL = new ContactDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ContactDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContactBLL()
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
            get { return this.contactDAL; }
        }
        #endregion

        #region 新增方法

        /// <summary>
        /// 获取分页查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="status"></param>
        /// <param name="key"></param>
        /// <param name="corpId"></param>
        /// <param name="empIdFrom"></param>
        /// <param name="empIdTo"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, string key, int corpId, int empIdFrom)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();
            int dataStatus = (int)Common.StatusEnum.已生效;

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "C.ContactId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " C.ContactId,C.ContactName,C.ContactCode,C.ContactTel,C.ContactFax,C.ContactAddress,Co.CorpName,BD.StatusName,e.ECId ";
            select.TableName = string.Format(" dbo.Contact C left join dbo.Corporation Co on C.CompanyId = Co.CorpId left join NFMT_Basic.dbo.BDStatusDetail BD on BD.DetailId = C.ContactStatus left join dbo.EmployeeContact e on C.ContactId = e.ContactId and e.EmpId = {0} and e.RefStatus = {1} ", empIdFrom, (int)Common.StatusEnum.已生效);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" 1=1 ");

            if (!string.IsNullOrEmpty(key))
                sb.AppendFormat(" and C.ContactName like '%{0}%'", key);
            if (corpId > 0)
                sb.AppendFormat(" and C.CompanyId = {0}", corpId);
            if (empIdFrom > 0)
                sb.AppendFormat(" and C.ContactId in (select ContactId from dbo.EmployeeContact where EmpId = {0} and RefStatus = {1}) ", empIdFrom, dataStatus);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int status, string key, int corpId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "C.ContactId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " C.ContactId,C.ContactName,C.ContactCode,C.ContactTel,C.ContactFax,C.ContactAddress,Co.CorpName,C.ContactStatus,BD.StatusName ";
            select.TableName = " dbo.Contact C left join dbo.Corporation Co on C.CompanyId = Co.CorpId left join NFMT_Basic.dbo.BDStatusDetail BD on BD.DetailId = C.ContactStatus ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" 1 = 1 ");

            if (status > 0)
                sb.AppendFormat(" and C.ContactStatus = {0}", status);
            if (!string.IsNullOrEmpty(key))
                sb.AppendFormat(" and C.ContactName like '%{0}%'", key);
            if (corpId > 0)
                sb.AppendFormat(" and C.CompanyId = {0}", corpId);

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion

        #region 重载方法

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                Model.Contact obj1 = (Model.Contact)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, obj.Id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Contact resultObj = (Model.Contact)result.ReturnValue;

                    if (resultObj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }

                    resultObj.ContactName = obj1.ContactName;
                    resultObj.ContactCode = obj1.ContactCode;
                    resultObj.ContactTel = obj1.ContactTel;
                    resultObj.ContactFax = obj1.ContactFax;
                    resultObj.ContactAddress = obj1.ContactAddress;
                    resultObj.CompanyId = obj1.CompanyId;

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

        public override ResultModel Insert(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();
            DAL.EmployeeContactDAL employeeContactDAL = new EmployeeContactDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Operate.Insert(user, obj);
                    if (result.ResultStatus != 0)
                        return result;

                    int contactId = (int)result.ReturnValue;

                    result = employeeContactDAL.Insert(user, new Model.EmployeeContact()
                    {
                        ContactId = contactId,
                        EmpId = user.EmpId,
                        RefStatus = StatusEnum.已生效
                    });
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
