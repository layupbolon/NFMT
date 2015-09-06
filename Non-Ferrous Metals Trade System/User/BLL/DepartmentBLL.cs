/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DepartmentBLL.cs
// 文件功能描述：部门dbo.Department业务逻辑类。
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
    /// 部门dbo.Department业务逻辑类。
    /// </summary>
    public class DepartmentBLL : Common.DataBLL
    {
        private DepartmentDAL departmentDAL = new DepartmentDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(DepartmentDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DepartmentBLL()
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
            get { return this.departmentDAL; }
        }

        /// <summary>
        /// 获取分页查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="status"></param>
        /// <param name="key"></param>
        /// <param name="corpId"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int status, string key, int corpId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "D.DeptId desc";
            else
                select.OrderStr = orderStr;

            //部门类型
            int deptType = (int)NFMT.Data.StyleEnum.DeptType;

            select.ColumnName = " D.DeptId,C.CorpName,D.DeptName,D.DeptFullName,D.DeptShort,D.DeptType,D2.DeptName as ParentDeptName,D.DeptStatus,BD.StatusName,D.DeptLevel,ct.DetailName as DeptTypeName ";
            select.TableName = string.Format(" dbo.Department D left join dbo.Department D2 on D.ParentLeve = D2.DeptId left join NFMT_Basic.dbo.BDStatusDetail BD on BD.DetailId = D.DeptStatus left join dbo.Corporation C on C.CorpId = D.CorpId left join (select * from NFMT_Basic.dbo.BDStyleDetail sd where sd.BDStyleId = {0}) as ct on ct.StyleDetailId = D.DeptType ",deptType);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" 1=1 ");

            if (corpId > 0)
                sb.AppendFormat(" and D.CorpId = {0}", corpId);
            if (status > 0)
                sb.AppendFormat(" and D.DeptStatus = {0} ", status);
            if (!string.IsNullOrEmpty(key))
                sb.AppendFormat(" and D.DeptName like '%{0}%' ", key);

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 获取部门列表(用于绑定下拉框)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultModel GetDeptList(UserModel user,int corpId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.departmentDAL.GetDeptList(user, corpId);
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

        #endregion
    }
}
