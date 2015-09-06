/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：EmpRoleBLL.cs
// 文件功能描述：员工角色关联表dbo.EmpRole业务逻辑类。
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
    /// 员工角色关联表dbo.EmpRole业务逻辑类。
    /// </summary>
    public class EmpRoleBLL : Common.DataBLL
    {
        private EmpRoleDAL emproleDAL = new EmpRoleDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(EmpRoleDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public EmpRoleBLL()
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
            get { return this.emproleDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel GetEmpIdsByRoleId(UserModel user, int roleId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = emproleDAL.GetEmpIdsByRoleId(user, roleId);

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
