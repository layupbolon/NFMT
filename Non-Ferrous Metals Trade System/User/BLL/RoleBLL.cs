/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RoleBLL.cs
// 文件功能描述：dbo.Role业务逻辑类。
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
    /// dbo.Role业务逻辑类。
    /// </summary>
    public class RoleBLL : Common.DataBLL
    {
        private RoleDAL roleDAL = new RoleDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(RoleDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RoleBLL()
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
            get { return this.roleDAL; }
        }

        #endregion

        #region 新增方法

        /// <summary>
        /// 获取分页查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, string key, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "r.RoleId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " r.RoleId,r.RoleName,r.RoleStatus,bd.StatusName  ";
            select.TableName = string.Format(" dbo.Role r left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = r.RoleStatus and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(key))
                sb.AppendFormat(" and r.RoleName like '%{0}%'", key);
            if (status>0)
                sb.AppendFormat(" and r.RoleStatus = {0}", status);

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
                Model.Role obj1 = (Model.Role)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, obj.Id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Role resultObj = (Model.Role)result.ReturnValue;

                    if (resultObj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }
                    resultObj.RoleName = obj1.RoleName;

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
