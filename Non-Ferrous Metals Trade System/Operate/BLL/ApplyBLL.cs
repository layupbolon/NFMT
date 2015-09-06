/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ApplyBLL.cs
// 文件功能描述：申请dbo.Apply业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月18日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Operate.Model;
using NFMT.Operate.DAL;
using NFMT.Operate.IDAL;
using NFMT.Common;

namespace NFMT.Operate.BLL
{
    /// <summary>
    /// 申请dbo.Apply业务逻辑类。
    /// </summary>
    public class ApplyBLL : Common.ApplyBLL
    {
        private ApplyDAL applyDAL = new ApplyDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ApplyDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ApplyBLL()
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
            get { return this.applyDAL; }
        }

        #endregion

        #region 新增方法

        /// <summary>
        /// 更新apply
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="apply">Apply对象</param>
        /// <returns></returns>
        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            if (obj == null)
            {
                result.Message = "该数据不存在，不能更新";
                return result;
            }

            Apply apply = (Apply)obj;

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    ResultModel applyResult = this.Get(user, apply.ApplyId);
                    Apply resultObj = applyResult.ReturnValue as Apply;

                    if (resultObj == null || resultObj.Id <= 0)
                    {
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }

                    resultObj.ApplyDept = apply.ApplyDept;
                    //resultObj.ApplyDeptName = apply.ApplyDeptName;
                    resultObj.ApplyDesc = apply.ApplyDesc;
                    resultObj.ApplyStatus = StatusEnum.已录入;
                    resultObj.EmpId = apply.EmpId;
                    resultObj.LastModifyId = user.EmpId;

                    result = applyDAL.Update(user, apply);

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
                    log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        #endregion

    }
}
