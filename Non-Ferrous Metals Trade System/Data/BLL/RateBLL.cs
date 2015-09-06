/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RateBLL.cs
// 文件功能描述：汇率dbo.Rate业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
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
    /// 汇率dbo.Rate业务逻辑类。
    /// </summary>
    public class RateBLL : Common.DataBLL
    {
        private RateDAL rateDAL = new RateDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(RateDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RateBLL()
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
            get { return this.rateDAL; }
        }

        #endregion

        #region 新增方法

        /// <summary>
        /// 分页获取查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="rateDate"></param>
        /// <param name="currency1"></param>
        /// <param name="currency2"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, DateTime rateDate, int currency1, int currency2)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "r.RateId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "r.RateId,r.CreateTime,c1.CurrencyName as CurrencyName_1,r.RateValue,c2.CurrencyName as CurrencyName_2,bd.StatusName ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.Rate r ");
            sb.Append(" left join dbo.Currency c1 on r.FromCurrencyId = c1.CurrencyId ");
            sb.Append(" left join dbo.Currency c2 on r.ToCurrencyId = c2.CurrencyId ");
            sb.AppendFormat(" left join dbo.BDStatusDetail bd on bd.DetailId = r.RateStatus and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);
            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" 1=1 ");

            if (currency1 > 0)
                sb.AppendFormat(" r.FromCurrencyId = {0} ", currency1);
            if (currency2 > 0)
                sb.AppendFormat(" r.ToCurrencyId = {0} ", currency2);
            if (rateDate > Common.DefaultValue.DefaultTime)
                sb.AppendFormat(" and r.CreateTime between '{0}' and '{1}' ", rateDate.ToString(), rateDate.AddDays(1).ToString());

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
                Model.Rate obj1 = (Model.Rate)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Operate.Get(user, obj.Id);
                    Model.Rate resultobj = (Model.Rate)result.ReturnValue;

                    if (resultobj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能修改";
                        return result;
                    }
                    resultobj.FromCurrencyId = obj1.FromCurrencyId;
                    resultobj.ToCurrencyId = obj1.ToCurrencyId;
                    resultobj.RateValue = obj1.RateValue;
                    resultobj.RateStatus = obj1.RateStatus;
                    resultobj.CreateTime = obj1.CreateTime;

                    result = rateDAL.Update(user, resultobj);

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
