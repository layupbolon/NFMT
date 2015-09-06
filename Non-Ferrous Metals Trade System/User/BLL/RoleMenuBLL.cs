/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RoleMenuBLL.cs
// 文件功能描述：dbo.RoleMenu业务逻辑类。
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
    /// dbo.RoleMenu业务逻辑类。
    /// </summary>
    public class RoleMenuBLL : Common.DataBLL
    {
        private RoleMenuDAL rolemenuDAL = new RoleMenuDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(RoleMenuDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RoleMenuBLL()
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
            get { return this.rolemenuDAL; }
        }

        #endregion

        /// <summary>
        /// 获取分页查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int roleId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "RM.RoleMenuID desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " RM.RoleMenuID,R.RoleName,M.MenuName,M.MenuDesc  ";
            select.TableName = " dbo.RoleMenu RM left join dbo.Role R on RM.RoleId = R.RoleId left join dbo.Menu M on RM.MenuId = M.MenuId ";

            if (roleId > 0)
                select.WhereStr = string.Format(" RM.RoleId = {0}", roleId);

            return select;
        }
       
    }
}
