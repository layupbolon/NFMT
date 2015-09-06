/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ExchangeBLL.cs
// 文件功能描述：dbo.Exchange业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年5月4日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Data.Model;
using NFMT.Data.DAL;
using NFMT.Data.IDAL;
using NFMT.Common;

namespace NFMT.Data.BLL
{
    /// <summary>
    /// dbo.Exchange业务逻辑类。
    /// </summary>
    public class ExchangeBLL : Common.DataBLL
    {
        private ExchangeDAL exchangeDAL = new ExchangeDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ExchangeDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ExchangeBLL()
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
            get { return this.exchangeDAL; }
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
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int status, string key, string exchangeCode)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "E.ExchangeId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " ExchangeId,ExchangeName,ExchangeCode,bd.StatusName ";
            select.TableName = " dbo.Exchange e inner join dbo.BDStatusDetail bd on e.ExchangeStatus = bd.DetailId ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" bd.StatusId = 1 ");

            if (status > 0)
                sb.AppendFormat(" and ExchangeStatus = {0}", status);
            if (!string.IsNullOrEmpty(key))
                sb.AppendFormat(" and ExchangeName like '%{0}%'", key);
            if (!string.IsNullOrEmpty(exchangeCode))
                sb.AppendFormat(" and ExchangeCode like '%{0}%'", exchangeCode);

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
                Model.Exchange obj1 = (Model.Exchange)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, obj.Id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Exchange resultObj = (Model.Exchange)result.ReturnValue;

                    if (resultObj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }

                    resultObj.ExchangeName = obj1.ExchangeName;
                    resultObj.ExchangeCode = obj1.ExchangeCode;
                    resultObj.ExchangeId = obj1.ExchangeId;
                    resultObj.ExchangeStatus = obj1.ExchangeStatus;

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
