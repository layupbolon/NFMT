/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：LcBLL.cs
// 文件功能描述：信用证dbo.Con_Lc业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月23日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Contract.Model;
using NFMT.Contract.DAL;
using NFMT.Contract.IDAL;
using NFMT.Common;

namespace NFMT.Contract.BLL
{
    /// <summary>
    /// 信用证dbo.Con_Lc业务逻辑类。
    /// </summary>
    public class LcBLL : Common.DataBLL
    {
        private LcDAL lcDAL = new LcDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(LcDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public LcBLL()
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
            get { return this.lcDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int issueBank, int adviseBank, int status, DateTime datef, DateTime datet)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "lc.LcId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)NFMT.Common.StatusTypeEnum.通用状态;

            select.ColumnName = " lc.LcId,b1.BankName as IssueBank,b2.BankName as AdviseBank,lc.IssueDate,CONVERT(varchar,lc.FutureDay) + '天' as FutureDay,CONVERT(varchar,lc.LcBala) + c.CurrencyName as LcBala,lc.LCStatus,bd.StatusName ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.Con_Lc lc left join NFMT_Basic.dbo.Bank b1 on lc.IssueBank = b1.BankId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank b2 on lc.AdviseBank = b2.BankId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on lc.Currency = c.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on lc.LCStatus = bd.DetailId and bd.StatusId = {0}", statusId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (issueBank > 0)
                sb.AppendFormat(" and lc.IssueBank = {0}", issueBank);
            if (adviseBank > 0)
                sb.AppendFormat(" and lc.AdviseBank = {0}", adviseBank);
            if (status > 0)
                sb.AppendFormat(" and lc.LCStatus = {0}", status);
            if (datef > Common.DefaultValue.DefaultTime && datet >= datef)
                sb.AppendFormat(" and lc.IssueDate >= '{0}' and lc.IssueDate < '{1}' ", datef.ToString(), datet.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public override ResultModel GoBack(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            using (System.Transactions.TransactionScope scope = new TransactionScope())
            {
                result = base.GoBack(user, obj);
                if (result.ResultStatus != 0)
                    return result;

                //工作流任务关闭
                WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                result = sourceDAL.SynchronousStatus(user, obj);
                if (result.ResultStatus != 0)
                    return result;

                scope.Complete();
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
                Model.Lc obj1 = (Model.Lc)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, obj.Id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Lc resultObj = (Model.Lc)result.ReturnValue;
                    if (resultObj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }

                    resultObj.LcId = obj1.LcId;
                    resultObj.IssueBank = obj1.IssueBank;
                    resultObj.AdviseBank = obj1.AdviseBank;
                    resultObj.IssueDate = obj1.IssueDate;
                    resultObj.FutureDay = obj1.FutureDay;
                    resultObj.LcBala = obj1.LcBala;
                    resultObj.Currency = obj1.Currency;

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
