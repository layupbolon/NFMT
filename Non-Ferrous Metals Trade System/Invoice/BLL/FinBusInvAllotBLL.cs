/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FinBusInvAllotBLL.cs
// 文件功能描述：业务发票财务发票分配dbo.Inv_FinBusInvAllot业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年9月24日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using System.Linq;
using NFMT.Invoice.Model;
using NFMT.Invoice.DAL;
using NFMT.Invoice.IDAL;
using NFMT.Common;

namespace NFMT.Invoice.BLL
{
    /// <summary>
    /// 业务发票财务发票分配dbo.Inv_FinBusInvAllot业务逻辑类。
    /// </summary>
    public class FinBusInvAllotBLL : Common.ExecBLL
    {
        private FinBusInvAllotDAL finbusinvallotDAL = new FinBusInvAllotDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(FinBusInvAllotDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FinBusInvAllotBLL()
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
            get { return this.finbusinvallotDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int person, int status, DateTime fromdate, DateTime todate)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "allot.AllotId desc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" allot.AllotId,CONVERT(varchar,ISNULL(allot.AllotBala,0)) + c.CurrencyName as AllotBala,e.Name,allot.AllotDate,allot.AllotStatus,bd.StatusName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Inv_FinBusInvAllot allot ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on allot.CurrencyId = c.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on allot.Alloter = e.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on allot.AllotStatus = bd.DetailId and bd.StatusId = {0} ", statusId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (person > 0)
                sb.AppendFormat(" and allot.Alloter = {0} ", person);
            if (status > 0)
                sb.AppendFormat(" and allot.AllotStatus = {0} ", status);
            if (fromdate > Common.DefaultValue.DefaultTime && todate > fromdate)
                sb.AppendFormat(" and allot.AllotDate between '{0}' and '{1}' ", fromdate.ToString(), todate.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel CreateAllot(UserModel user, int currencyId, List<Model.FinBusInvAllotDetail> details)
        {
            ResultModel result = new ResultModel();
            DAL.FinBusInvAllotDetailDAL finBusInvAllotDetailDAL = new FinBusInvAllotDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    decimal sumAllotAmount = 0;
                    if (details != null && details.Any())
                    {
                        foreach (Model.FinBusInvAllotDetail detail in details)
                        {
                            sumAllotAmount += detail.AllotBala;
                        }
                    }

                    if (sumAllotAmount == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "分配金额不能为0";
                        return result;
                    }

                    Model.FinBusInvAllot finBusInvAllot = new FinBusInvAllot()
                    {
                        AllotBala = sumAllotAmount,
                        CurrencyId = currencyId,
                        Alloter = user.EmpId,
                        AllotDate = DateTime.Now
                    };

                    result = finbusinvallotDAL.Insert(user, finBusInvAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    int allotId = (int)result.ReturnValue;

                    foreach (Model.FinBusInvAllotDetail detail in details)
                    {
                        detail.AllotId = allotId;
                        result = finBusInvAllotDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

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
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel GetFinanceInvoiceId(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = finbusinvallotDAL.GetFinanceInvoiceId(user, allotId);
            }
            catch (Exception ex)
            {
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

        public ResultModel UpdateAllot(UserModel user, List<Model.FinBusInvAllotDetail> details, int allotId)
        {
            ResultModel result = new ResultModel();
            DAL.FinBusInvAllotDetailDAL finBusInvAllotDetailDAL = new FinBusInvAllotDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    decimal sumAllotAmount = 0;
                    if (details != null && details.Any())
                    {
                        foreach (Model.FinBusInvAllotDetail detail in details)
                        {
                            sumAllotAmount += detail.AllotBala;
                        }
                    }

                    if (sumAllotAmount == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "分配金额不能为0";
                        return result;
                    }

                    result = finbusinvallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.FinBusInvAllot finBusInvAllot = result.ReturnValue as Model.FinBusInvAllot;
                    if (finBusInvAllot == null || finBusInvAllot.AllotId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "验证失败";
                        return result;
                    }
                    finBusInvAllot.AllotBala = sumAllotAmount;
                    finBusInvAllot.AllotDate = DateTime.Now;
                    finBusInvAllot.Alloter = user.EmpId;

                    result = finbusinvallotDAL.Update(user, finBusInvAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    result = finBusInvAllotDetailDAL.InvalidAll(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    foreach (Model.FinBusInvAllotDetail detail in details)
                    {
                        detail.AllotId = allotId;
                        result = finBusInvAllotDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

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
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Goback(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = finbusinvallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.FinBusInvAllot finBusInvAllot = result.ReturnValue as Model.FinBusInvAllot;
                    if (finBusInvAllot == null || finBusInvAllot.AllotId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "分配验证失败";
                        return result;
                    }

                    //撤返
                    result = finbusinvallotDAL.Goback(user, finBusInvAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    //工作流任务关闭
                    WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, finBusInvAllot);
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

            return result;
        }

        public ResultModel Invalid(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.FinBusInvAllotDetailDAL finBusInvAllotDetailDAL = new FinBusInvAllotDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = finbusinvallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.FinBusInvAllot finBusInvAllot = result.ReturnValue as Model.FinBusInvAllot;
                    if (finBusInvAllot == null || finBusInvAllot.AllotId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "分配验证失败";
                        return result;
                    }

                    result = finbusinvallotDAL.Invalid(user, finBusInvAllot);
                    if (result.ResultStatus != 0)
                        return result;


                    result = finBusInvAllotDetailDAL.InvalidAll(user, allotId);
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

            return result;
        }

        public ResultModel Complete(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.FinBusInvAllotDetailDAL finBusInvAllotDetailDAL = new FinBusInvAllotDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = finbusinvallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.FinBusInvAllot finBusInvAllot = result.ReturnValue as Model.FinBusInvAllot;
                    if (finBusInvAllot == null || finBusInvAllot.AllotId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "分配验证失败";
                        return result;
                    }

                    result = finbusinvallotDAL.Complete(user, finBusInvAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    result = finBusInvAllotDetailDAL.Load(user, allotId, Common.StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<FinBusInvAllotDetail> finBusInvAllotDetails = result.ReturnValue as List<FinBusInvAllotDetail>;
                    if (finBusInvAllotDetails != null && finBusInvAllotDetails.Any())
                    {
                        foreach (FinBusInvAllotDetail detail in finBusInvAllotDetails)
                        {
                            result = finBusInvAllotDetailDAL.Complete(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel CompleteCancel(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.FinBusInvAllotDetailDAL finBusInvAllotDetailDAL = new FinBusInvAllotDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = finbusinvallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.FinBusInvAllot finBusInvAllot = result.ReturnValue as Model.FinBusInvAllot;
                    if (finBusInvAllot == null || finBusInvAllot.AllotId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "分配验证失败";
                        return result;
                    }

                    result = finbusinvallotDAL.CompleteCancel(user, finBusInvAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    result = finBusInvAllotDetailDAL.Load(user, allotId, Common.StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<FinBusInvAllotDetail> finBusInvAllotDetails = result.ReturnValue as List<FinBusInvAllotDetail>;
                    if (finBusInvAllotDetails != null && finBusInvAllotDetails.Any())
                    {
                        foreach (FinBusInvAllotDetail detail in finBusInvAllotDetails)
                        {
                            result = finBusInvAllotDetailDAL.CompleteCancel(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        #endregion

        #region report

        public SelectModel GetFinBusAllotReportSelect(int pageIndex, int pageSize, string orderStr, int fIId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "inv.InvoiceId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)Common.StatusEnum.已生效;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("inv.InvoiceId,bi.BusinessInvoiceId,inv.InvoiceDate,inv.InvoiceNo,inv.InvoiceName,bdInvoiceDirection.DetailName,ass.AssetName,bi.NetAmount,mu.MUName,inv.InvoiceBala,cur.CurrencyName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            //sb.Append(" NFMT..Inv_FinBusInvAllot allot ");
            sb.Append(" NFMT..Inv_FinBusInvAllotDetail detail  ");
            //sb.AppendFormat(" inner join NFMT..Inv_FinBusInvAllotDetail detail on detail.AllotId = allot.AllotId and detail.DetailStatus >= {0} ", readyStatus);
            sb.Append(" left join NFMT..Inv_BusinessInvoice bi on bi.BusinessInvoiceId = detail.BusinessInvoiceId ");
            sb.Append(" inner join NFMT..Invoice inv on bi.InvoiceId = inv.InvoiceId ");
            sb.AppendFormat(" left join NFMT_Basic..BDStyleDetail bdInvoiceDirection on bdInvoiceDirection.StyleDetailId = inv.InvoiceDirection and bdInvoiceDirection.BDStyleId = {0} ", (int)NFMT.Data.StyleEnum.InvoiceDirection);
            sb.Append(" left join NFMT_Basic..Asset ass on ass.AssetId = bi.AssetId ");
            sb.Append(" left join NFMT_Basic..MeasureUnit mu on mu.MUId = bi.MUId ");
            sb.Append(" left join NFMT_Basic..Currency cur on cur.CurrencyId = inv.CurrencyId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" detail.DetailStatus >={0} and detail.FinanceInvoiceId = {1} ", readyStatus, fIId);
            select.WhereStr = sb.ToString();

            return select;
        }

        //public override string[,] SetExcelRangeData(DataTable source)
        //{
        //    string[,] objData = new string[source.Rows.Count, 13];

        //    for (int i = 0; i < source.Rows.Count; i++)
        //    {
        //        System.Data.DataRow dr = source.Rows[i];

        //        objData[i, 0] = ((DateTime)dr["InvoiceDate"]).ToString("yyyy-MM-dd");
        //        objData[i, 1] = dr["InvoiceNo"].ToString();
        //        objData[i, 2] = dr["InvoiceName"].ToString();
        //        objData[i, 3] = dr["innerCorp"].ToString();
        //        objData[i, 4] = dr["outerCorp"].ToString();
        //        objData[i, 5] = dr["InvoiceDirection"].ToString();
        //        objData[i, 6] = dr["InvoiceType"].ToString();
        //        objData[i, 7] = dr["AssetName"].ToString();
        //        objData[i, 8] = dr["NetAmount"].ToString();
        //        objData[i, 9] = dr["MUName"].ToString();
        //        objData[i, 10] = dr["Bala"].ToString();
        //        objData[i, 11] = dr["CurrencyName"].ToString();
        //        objData[i, 12] = dr["RefNo"].ToString();
        //    }

        //    return objData;
        //}

        #endregion
    }
}
