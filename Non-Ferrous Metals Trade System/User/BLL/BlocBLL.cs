/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BlocBLL.cs
// 文件功能描述：dbo.Bloc业务逻辑类。
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
    /// dbo.Bloc业务逻辑类。
    /// </summary>
    public class BlocBLL : Common.DataBLL
    {
        private BlocDAL blocDAL = new BlocDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(BlocDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BlocBLL()
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
            get { return this.blocDAL; }
        }

        #endregion

        #region 新增方法
        
        /// <summary>
        /// 获取集团列表(用于绑定下拉框)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultModel GetBlocList(UserModel user)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = blocDAL.GetBlocList(user);
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

        /// <summary>
        /// 获取分页查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="status"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int status, string key)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "B.BlocId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " B.BlocId,B.BlocName,B.BlocFullName,B.BlocEname,B.BlocStatus,BD.StatusName,case B.IsSelf when 1 then '己方集团' when 0 then '非己方集团' else '' end as IsSelf  ";
            select.TableName = " dbo.Bloc B left join NFMT_Basic.dbo.BDStatusDetail BD on B.BlocStatus = BD.DetailId ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" 1 = 1 ");

            if (status > 0)
                sb.AppendFormat(" and B.BlocStatus = {0}", status);
            if (!string.IsNullOrEmpty(key))
                sb.AppendFormat(" and B.BlocFullName like '%{0}%'", key);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel IsExistSelfBolc(UserModel user)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = blocDAL.IsExistSelfBolc(user);
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

        #region 重载方法

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                Model.Bloc obj1 = (Model.Bloc)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, obj.Id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Bloc resultObj = (Model.Bloc)result.ReturnValue;

                    if (resultObj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }
                    resultObj.BlocName = obj1.BlocName;
                    resultObj.BlocEname = obj1.BlocEname;
                    resultObj.BlocFullName = obj1.BlocFullName;

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

        public override ResultModel GoBack(UserModel user, IModel obj)
        {
            ResultModel result = base.GoBack(user, obj);

            if (result.ResultStatus != 0)
                return result;           

            return result;
        }

        #endregion
    }
}
