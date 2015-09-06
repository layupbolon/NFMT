/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FinanceInvoiceBLL.cs
// 文件功能描述：财务发票dbo.Inv_FinanceInvoice业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月25日
----------------------------------------------------------------*/

using System;
using System.Linq;
using System.Text;
using System.Transactions;
using log4net;
using NFMT.Common;
using NFMT.Data;
using NFMT.Data.Model;
using NFMT.Invoice.DAL;
using NFMT.Invoice.Model;
using NFMT.Operate.DAL;
using NFMT.User;
using NFMT.User.Model;
using NFMT.WorkFlow.DAL;

namespace NFMT.Invoice.BLL
{
    /// <summary>
    /// 财务发票dbo.Inv_FinanceInvoice业务逻辑类。
    /// </summary>
    public class FinanceInvoiceBLL : ExecBLL
    {
        private FinanceInvoiceDAL financeinvoiceDAL = new FinanceInvoiceDAL();
        private ILog log = LogManager.GetLogger(typeof(FinanceInvoiceDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FinanceInvoiceBLL()
        {
        }

        #endregion

        #region 数据库操作

        protected override ILog Log
        {
            get { return this.log; }
        }

        public override IOperate Operate
        {
            get { return this.financeinvoiceDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, DateTime invoiceDateBegin, DateTime invoiceDateEnd, int status, int inCorpId, int outCorpId)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "fi.FinanceInvoiceId desc";
            else
                select.OrderStr = orderStr;

            int commonStatusType = (int)StatusTypeEnum.通用状态;
            int InvoiceDirection = (int)StyleEnum.InvoiceDirection;

            StringBuilder sb = new StringBuilder();
            sb.Append("fi.FinanceInvoiceId,inv.InvoiceId,inv.InvoiceDate,inv.InvoiceNo,inv.InvoiceName,inv.InvoiceBala,inv.CurrencyId,cur.CurrencyName,inv.OutBlocId,outBloc.BlocName as OutBlocName,inv.OutCorpId,OutCorpName,inv.InBlocId,inBloc.BlocName as InBlocName,inv.InCorpId,InCorpName,inv.InvoiceStatus,sd.StatusName,fi.VATRatio,fi.VATBala,inv.InvoiceDirection,invDir.DetailName as DirectionName,ISNULL(iaf.RefId,0) RefId");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Inv_FinanceInvoice fi ");
            sb.Append(" inner join dbo.Invoice inv on fi.InvoiceId = inv.InvoiceId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = inv.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Bloc outBloc on outBloc.BlocId = inv.OutBlocId ");
            sb.Append(" left join NFMT_User.dbo.Bloc inBloc on inBloc.BlocId = inv.InBlocId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = inv.InvoiceStatus and sd.StatusId ={0} ", commonStatusType);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail invDir on inv.InvoiceDirection = invDir.StyleDetailId and invDir.BDStyleId ={0} ", InvoiceDirection);
            sb.AppendFormat(" left join dbo.Inv_InvoiceApplyFinance iaf on fi.FinanceInvoiceId = iaf.FinanceInvoiceId and iaf.RefStatus >= {0}", (int)StatusEnum.已生效);
            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" 1=1 ");
            //sb.AppendFormat(" inv.InvoiceDirection= {0} ", InvoiceDirection);
            if (status > 0)
                sb.AppendFormat(" and inv.InvoiceStatus = {0} ", status);
            if (inCorpId > 0)
                sb.AppendFormat(" and inv.InCorpId = {0} ", inCorpId);
            if (outCorpId > 0)
                sb.AppendFormat(" and inv.OutCorpId = {0} ", outCorpId);
            if (invoiceDateBegin > DefaultValue.DefaultTime && invoiceDateEnd > invoiceDateBegin)
                sb.AppendFormat(" and inv.InvoiceDate between '{0}' and '{1}' ", invoiceDateBegin.ToString(), invoiceDateEnd.AddDays
                    (1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Create(UserModel user, Operate.Model.Invoice invoice, FinanceInvoice fundsInvoice)
        {
            ResultModel result = new ResultModel();

            try
            {
                FinanceInvoiceDAL fundsInvoiceDAL = new FinanceInvoiceDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //新增发票主表
                    int invoiceId = 0;

                    invoice.InvoiceType = (int)InvoiceTypeEnum.财务发票;

                    invoice.InvoiceStatus = StatusEnum.已录入;
                    Corporation outCorp = UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == invoice.OutCorpId);
                    if (outCorp == null || outCorp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "开票公司信息错误";
                        return result;
                    }
                    invoice.OutCorpName = outCorp.CorpName;
                    //invoice.OutBlocId = outCorp.b

                    Corporation inCorp = UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == invoice.InCorpId);
                    if (inCorp == null || inCorp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收票公司信息错误";
                        return result;
                    }
                    invoice.InCorpName = inCorp.CorpName;

                    result = invoiceDAL.Insert(user, invoice);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out invoiceId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票添加失败";
                        return result;
                    }

                    //新增财务发票表
                    fundsInvoice.InvoiceId = invoiceId;
                    fundsInvoice.VATRatio = fundsInvoice.VATRatio;
                    result = fundsInvoiceDAL.Insert(user, fundsInvoice);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ResultStatus == 0) result.ReturnValue = invoiceId;

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

        public ResultModel Create(UserModel user, Operate.Model.Invoice invoice, FinanceInvoice fundsInvoice, string bids)
        {
            ResultModel result = new ResultModel();

            try
            {
                FinanceInvoiceDAL fundsInvoiceDAL = new FinanceInvoiceDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //新增发票主表
                    int invoiceId = 0;

                    invoice.InvoiceType = (int)InvoiceTypeEnum.财务发票;

                    invoice.InvoiceStatus = StatusEnum.已录入;
                    Corporation outCorp = UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == invoice.OutCorpId);
                    if (outCorp == null || outCorp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "开票公司信息错误";
                        return result;
                    }
                    invoice.OutCorpName = outCorp.CorpName;
                    //invoice.OutBlocId = outCorp.b

                    Corporation inCorp = UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == invoice.InCorpId);
                    if (inCorp == null || inCorp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收票公司信息错误";
                        return result;
                    }
                    invoice.InCorpName = inCorp.CorpName;

                    result = invoiceDAL.Insert(user, invoice);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out invoiceId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票添加失败";
                        return result;
                    }

                    //新增财务发票表
                    fundsInvoice.InvoiceId = invoiceId;
                    fundsInvoice.VATRatio = fundsInvoice.VATRatio;
                    result = fundsInvoiceDAL.Insert(user, fundsInvoice);
                    if (result.ResultStatus != 0)
                        return result;

                    int financeInvoiceId = (int)result.ReturnValue;


                    //财务发票业务发票分配
                    if (!string.IsNullOrEmpty(bids))
                    {
                        FinBusInvAllotDAL finBusInvAllotDAL = new FinBusInvAllotDAL();
                        result = finBusInvAllotDAL.Insert(user, new FinBusInvAllot()
                        {
                            AllotBala = invoice.InvoiceBala,
                            CurrencyId = invoice.CurrencyId,
                            Alloter = user.EmpId,
                            AllotDate = DateTime.Now,
                            AllotStatus = StatusEnum.已生效
                        });

                        if (result.ResultStatus != 0)
                            return result;

                        int allotId = (int)result.ReturnValue;

                        FinBusInvAllotDetailDAL finBusInvAllotDetailDAL = new FinBusInvAllotDetailDAL();
                        foreach (string businessInvoiceId in bids.Split(','))
                        {
                            result = invoiceDAL.GetByBussinessInvoiceId(user, Convert.ToInt32(businessInvoiceId));
                            if (result.ResultStatus != 0)
                                return result;

                            Operate.Model.Invoice busInvoice = result.ReturnValue as Operate.Model.Invoice;

                            result = finBusInvAllotDetailDAL.Insert(user, new FinBusInvAllotDetail()
                            {
                                AllotId = allotId,
                                BusinessInvoiceId = Convert.ToInt32(businessInvoiceId),
                                FinanceInvoiceId = financeInvoiceId,
                                AllotBala = busInvoice.InvoiceBala,
                                DetailStatus = StatusEnum.已生效
                            });
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    invoice.InvoiceId = invoiceId;
                    if (result.ResultStatus == 0) result.ReturnValue = invoice;

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

        public ResultModel Update(UserModel user, Operate.Model.Invoice invoice, FinanceInvoice fundsInvoice)
        {
            ResultModel result = new ResultModel();

            try
            {
                FinanceInvoiceDAL fundsInvoiceDAL = new FinanceInvoiceDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {

                    //获取财务发票
                    result = fundsInvoiceDAL.Get(user, fundsInvoice.FinanceInvoiceId);
                    if (result.ResultStatus != 0)
                        return result;

                    FinanceInvoice resultFundsInvoice = result.ReturnValue as FinanceInvoice;
                    if (resultFundsInvoice == null || resultFundsInvoice.FinanceInvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "财务发票获取失败";
                        return result;
                    }

                    //更新财务发票
                    resultFundsInvoice.AssetId = fundsInvoice.AssetId;
                    resultFundsInvoice.IntegerAmount = fundsInvoice.IntegerAmount;
                    resultFundsInvoice.NetAmount = fundsInvoice.NetAmount;
                    resultFundsInvoice.MUId = fundsInvoice.MUId;
                    resultFundsInvoice.VATRatio = fundsInvoice.VATRatio;
                    resultFundsInvoice.VATBala = fundsInvoice.VATBala;

                    result = fundsInvoiceDAL.Update(user, resultFundsInvoice);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取发票
                    result = invoiceDAL.Get(user, resultFundsInvoice.InvoiceId);
                    if (result.ResultStatus != 0)
                        return result;
                    Operate.Model.Invoice resultInvoice = result.ReturnValue as Operate.Model.Invoice;
                    if (resultInvoice == null || resultInvoice.InvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票信息获取失败";
                        return result;
                    }

                    resultInvoice.InvoiceDate = invoice.InvoiceDate;
                    resultInvoice.InvoiceName = invoice.InvoiceName;
                    resultInvoice.InvoiceBala = invoice.InvoiceBala;
                    resultInvoice.CurrencyId = invoice.CurrencyId;
                    resultInvoice.OutCorpId = invoice.OutCorpId;

                    Corporation outCorp = UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == invoice.OutCorpId);
                    if (outCorp == null || outCorp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "开票公司信息错误";
                        return result;
                    }
                    resultInvoice.OutCorpName = outCorp.CorpName;

                    resultInvoice.InCorpId = invoice.InCorpId;
                    Corporation inCorp = UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == invoice.InCorpId);
                    if (inCorp == null || inCorp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收票公司信息错误";
                        return result;
                    }
                    resultInvoice.InCorpName = inCorp.CorpName;
                    resultInvoice.Memo = invoice.Memo;

                    result = invoiceDAL.Update(user, resultInvoice);
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

        public ResultModel Update(UserModel user, Operate.Model.Invoice invoice, FinanceInvoice fundsInvoice,string bids)
        {
            ResultModel result = new ResultModel();

            try
            {
                FinanceInvoiceDAL fundsInvoiceDAL = new FinanceInvoiceDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {

                    //获取财务发票
                    result = fundsInvoiceDAL.Get(user, fundsInvoice.FinanceInvoiceId);
                    if (result.ResultStatus != 0)
                        return result;

                    FinanceInvoice resultFundsInvoice = result.ReturnValue as FinanceInvoice;
                    if (resultFundsInvoice == null || resultFundsInvoice.FinanceInvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "财务发票获取失败";
                        return result;
                    }

                    //更新财务发票
                    resultFundsInvoice.AssetId = fundsInvoice.AssetId;
                    resultFundsInvoice.IntegerAmount = fundsInvoice.IntegerAmount;
                    resultFundsInvoice.NetAmount = fundsInvoice.NetAmount;
                    resultFundsInvoice.MUId = fundsInvoice.MUId;
                    resultFundsInvoice.VATRatio = fundsInvoice.VATRatio;
                    resultFundsInvoice.VATBala = fundsInvoice.VATBala;

                    result = fundsInvoiceDAL.Update(user, resultFundsInvoice);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取发票
                    result = invoiceDAL.Get(user, resultFundsInvoice.InvoiceId);
                    if (result.ResultStatus != 0)
                        return result;
                    Operate.Model.Invoice resultInvoice = result.ReturnValue as Operate.Model.Invoice;
                    if (resultInvoice == null || resultInvoice.InvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票信息获取失败";
                        return result;
                    }

                    resultInvoice.InvoiceDate = invoice.InvoiceDate;
                    resultInvoice.InvoiceName = invoice.InvoiceName;
                    resultInvoice.InvoiceBala = invoice.InvoiceBala;
                    resultInvoice.CurrencyId = invoice.CurrencyId;
                    resultInvoice.OutCorpId = invoice.OutCorpId;

                    Corporation outCorp = UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == invoice.OutCorpId);
                    if (outCorp == null || outCorp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "开票公司信息错误";
                        return result;
                    }
                    resultInvoice.OutCorpName = outCorp.CorpName;

                    resultInvoice.InCorpId = invoice.InCorpId;
                    Corporation inCorp = UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == invoice.InCorpId);
                    if (inCorp == null || inCorp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收票公司信息错误";
                        return result;
                    }
                    resultInvoice.InCorpName = inCorp.CorpName;
                    resultInvoice.Memo = invoice.Memo;

                    result = invoiceDAL.Update(user, resultInvoice);
                    if (result.ResultStatus != 0)
                        return result;

                    if (!string.IsNullOrEmpty(bids))
                    {
                        FinBusInvAllotDAL finBusInvAllotDAL = new FinBusInvAllotDAL();
                        FinBusInvAllotDetailDAL finBusInvAllotDetailDAL = new FinBusInvAllotDetailDAL();
                        result = finBusInvAllotDetailDAL.GetAllotIdByFid(user, fundsInvoice.FinanceInvoiceId);
                        if (result.ResultStatus != 0)
                            return result;

                        int allotId = (int)result.ReturnValue;

                        result = finBusInvAllotDAL.Get(user, allotId);
                        if (result.ResultStatus != 0)
                            return result;

                        FinBusInvAllot finBusInvAllot = result.ReturnValue as FinBusInvAllot;
                        finBusInvAllot.AllotBala = invoice.InvoiceBala;

                        result = finBusInvAllotDAL.Update(user, finBusInvAllot);
                        if (result.ResultStatus != 0)
                            return result;

                        result = finBusInvAllotDetailDAL.InvalidAll(user, allotId);
                        if (result.ResultStatus != 0)
                            return result;

                        foreach (string businessInvoiceId in bids.Split(','))
                        {
                            result = invoiceDAL.GetByBussinessInvoiceId(user, Convert.ToInt32(businessInvoiceId));
                            if (result.ResultStatus != 0)
                                return result;

                            Operate.Model.Invoice busInvoice = result.ReturnValue as Operate.Model.Invoice;

                            result = finBusInvAllotDetailDAL.Insert(user, new FinBusInvAllotDetail()
                            {
                                AllotId = allotId,
                                BusinessInvoiceId = Convert.ToInt32(businessInvoiceId),
                                FinanceInvoiceId = fundsInvoice.FinanceInvoiceId,
                                AllotBala = busInvoice.InvoiceBala,
                                DetailStatus = StatusEnum.已生效
                            });
                            if (result.ResultStatus != 0)
                                return result;
                        }

                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = invoice.InvoiceId;

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

        public ResultModel GetByInvoiceId(UserModel user, int invoiceId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.financeinvoiceDAL.GetByInvoiceId(user, invoiceId);
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

        public ResultModel Goback(UserModel user, int fundsInvoiceId)
        {
            ResultModel result = new ResultModel();

            try
            {
                FinanceInvoiceDAL fundsInvoiceDAL = new FinanceInvoiceDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取财务票
                    result = fundsInvoiceDAL.Get(user, fundsInvoiceId);
                    if (result.ResultStatus != 0)
                        return result;

                    FinanceInvoice fundsInvoice = result.ReturnValue as FinanceInvoice;
                    if (fundsInvoice == null || fundsInvoice.FinanceInvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "财务发票验证失败";
                        return result;
                    }

                    //获取发票
                    result = invoiceDAL.Get(user, fundsInvoice.InvoiceId);
                    if (result.ResultStatus != 0)
                        return result;
                    Operate.Model.Invoice invoice = result.ReturnValue as Operate.Model.Invoice;
                    if (invoice == null || invoice.InvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票获取失败";
                        return result;
                    }

                    //撤返发票
                    result = invoiceDAL.Goback(user, invoice);
                    if (result.ResultStatus != 0)
                        return result;

                    //工作流任务关闭
                    DataSourceDAL sourceDAL = new DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, invoice);
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

        public ResultModel Invalid(UserModel user, int fundsInvoiceId)
        {
            ResultModel result = new ResultModel();

            try
            {
                FinanceInvoiceDAL fundsInvoiceDAL = new FinanceInvoiceDAL();
                DAL.FinBusInvAllotDetailDAL finBusInvAllotDetailDal = new FinBusInvAllotDetailDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取财务票
                    result = fundsInvoiceDAL.Get(user, fundsInvoiceId);
                    if (result.ResultStatus != 0)
                        return result;

                    FinanceInvoice fundsInvoice = result.ReturnValue as FinanceInvoice;
                    if (fundsInvoice == null || fundsInvoice.FinanceInvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "财务发票验证失败";
                        return result;
                    }

                    //获取发票
                    result = invoiceDAL.Get(user, fundsInvoice.InvoiceId);
                    if (result.ResultStatus != 0)
                        return result;
                    Operate.Model.Invoice invoice = result.ReturnValue as Operate.Model.Invoice;
                    if (invoice == null || invoice.InvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票获取失败";
                        return result;
                    }

                    //作废发票
                    result = invoiceDAL.Invalid(user, invoice);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废财务发票与业务发票关联  2015/8/4
                    result = finBusInvAllotDetailDal.InvalidAllByFinInvoiceId(user, fundsInvoiceId);
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

        public ResultModel Complete(UserModel user, int fundsInvoiceId)
        {
            ResultModel result = new ResultModel();

            try
            {
                FinanceInvoiceDAL fundsInvoiceDAL = new FinanceInvoiceDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取财务票
                    result = fundsInvoiceDAL.Get(user, fundsInvoiceId);
                    if (result.ResultStatus != 0)
                        return result;

                    FinanceInvoice fundsInvoice = result.ReturnValue as FinanceInvoice;
                    if (fundsInvoice == null || fundsInvoice.FinanceInvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "财务发票验证失败";
                        return result;
                    }

                    //获取发票
                    result = invoiceDAL.Get(user, fundsInvoice.InvoiceId);
                    if (result.ResultStatus != 0)
                        return result;
                    Operate.Model.Invoice invoice = result.ReturnValue as Operate.Model.Invoice;
                    if (invoice == null || invoice.InvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票获取失败";
                        return result;
                    }

                    //作废发票
                    result = invoiceDAL.Complete(user, invoice);
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

        public ResultModel CompleteCancel(UserModel user, int fundsInvoiceId)
        {
            ResultModel result = new ResultModel();

            try
            {
                FinanceInvoiceDAL fundsInvoiceDAL = new FinanceInvoiceDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取财务票
                    result = fundsInvoiceDAL.Get(user, fundsInvoiceId);
                    if (result.ResultStatus != 0)
                        return result;

                    FinanceInvoice fundsInvoice = result.ReturnValue as FinanceInvoice;
                    if (fundsInvoice == null || fundsInvoice.FinanceInvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "财务发票验证失败";
                        return result;
                    }

                    //获取发票
                    result = invoiceDAL.Get(user, fundsInvoice.InvoiceId);
                    if (result.ResultStatus != 0)
                        return result;
                    Operate.Model.Invoice invoice = result.ReturnValue as Operate.Model.Invoice;
                    if (invoice == null || invoice.InvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票获取失败";
                        return result;
                    }

                    //作废发票
                    result = invoiceDAL.CompleteCancel(user, invoice);
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

        public ResultModel GetAllotAmount(UserModel user, int fundsInvoiceId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = financeinvoiceDAL.GetAllotAmount(user, fundsInvoiceId);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public SelectModel GetCanAllotSelectModel(int pageIndex, int pageSize, string orderStr, DateTime invoiceDateBegin, DateTime invoiceDateEnd, int status, int inCorpId, int outCorpId)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "fi.FinanceInvoiceId desc";
            else
                select.OrderStr = orderStr;

            int commonStatusType = (int)StatusTypeEnum.通用状态;
            int InvoiceDirection = (int)StyleEnum.InvoiceDirection;

            StringBuilder sb = new StringBuilder();
            sb.Append("fi.FinanceInvoiceId,inv.InvoiceId,inv.InvoiceDate,inv.InvoiceNo,inv.InvoiceName,CONVERT(varchar,inv.InvoiceBala) + cur.CurrencyName as InvoiceBala,inv.CurrencyId,cur.CurrencyName,inv.OutBlocId,outBloc.BlocName as OutBlocName,inv.OutCorpId,OutCorpName,inv.InBlocId,inBloc.BlocName as InBlocName,inv.InCorpId,InCorpName,inv.InvoiceStatus,sd.StatusName,fi.VATRatio,fi.VATBala,inv.InvoiceDirection,invDir.DetailName as DirectionName,CONVERT(varchar,isnull(allotDetail.SumAllotBala,0)) + cur.CurrencyName as SumAllotBala");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Inv_FinanceInvoice fi ");
            sb.Append(" inner join dbo.Invoice inv on fi.InvoiceId = inv.InvoiceId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = inv.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Bloc outBloc on outBloc.BlocId = inv.OutBlocId ");
            sb.Append(" left join NFMT_User.dbo.Bloc inBloc on inBloc.BlocId = inv.InBlocId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = inv.InvoiceStatus and sd.StatusId ={0} ", commonStatusType);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail invDir on inv.InvoiceDirection = invDir.StyleDetailId and invDir.BDStyleId ={0} ", InvoiceDirection);
            sb.AppendFormat(" left join (select SUM(AllotBala) as SumAllotBala,FinanceInvoiceId,DetailStatus from dbo.Inv_FinBusInvAllotDetail group by FinanceInvoiceId,DetailStatus having DetailStatus ={0}) allotDetail on fi.FinanceInvoiceId = allotDetail.FinanceInvoiceId ", (int)StatusEnum.已完成);
            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" inv.InvoiceBala - isnull(allotDetail.SumAllotBala,0) >0 ");

            if (status > 0)
                sb.AppendFormat(" and inv.InvoiceStatus = {0} ", status);
            if (inCorpId > 0)
                sb.AppendFormat(" and inv.InCorpId = {0} ", inCorpId);
            if (outCorpId > 0)
                sb.AppendFormat(" and inv.OutCorpId = {0} ", outCorpId);
            if (invoiceDateBegin > DefaultValue.DefaultTime && invoiceDateEnd > invoiceDateBegin)
                sb.AppendFormat(" and inv.InvoiceDate between '{0}' and '{1}' ", invoiceDateBegin.ToString(), invoiceDateEnd.AddDays
                    (1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel CreateByInvApply(UserModel user, Operate.Model.Invoice invoice, FinanceInvoice fundsInvoice, string bids, int invoiceApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                FinanceInvoiceDAL fundsInvoiceDAL = new FinanceInvoiceDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //新增发票主表
                    int invoiceId = 0;

                    invoice.InvoiceType = (int)InvoiceTypeEnum.财务发票;

                    invoice.InvoiceStatus = StatusEnum.已录入;
                    Corporation outCorp = UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == invoice.OutCorpId);
                    if (outCorp == null || outCorp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "开票公司信息错误";
                        return result;
                    }
                    invoice.OutCorpName = outCorp.CorpName;
                    //invoice.OutBlocId = outCorp.b

                    Corporation inCorp = UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == invoice.InCorpId);
                    if (inCorp == null || inCorp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收票公司信息错误";
                        return result;
                    }
                    invoice.InCorpName = inCorp.CorpName;

                    result = invoiceDAL.Insert(user, invoice);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out invoiceId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票添加失败";
                        return result;
                    }

                    //新增财务发票表
                    fundsInvoice.InvoiceId = invoiceId;
                    fundsInvoice.VATRatio = fundsInvoice.VATRatio;
                    result = fundsInvoiceDAL.Insert(user, fundsInvoice);
                    if (result.ResultStatus != 0)
                        return result;

                    int financeInvoiceId = (int)result.ReturnValue;


                    //财务发票业务发票分配
                    if (!string.IsNullOrEmpty(bids))
                    {
                        FinBusInvAllotDAL finBusInvAllotDAL = new FinBusInvAllotDAL();
                        result = finBusInvAllotDAL.Insert(user, new FinBusInvAllot()
                        {
                            AllotBala = invoice.InvoiceBala,
                            CurrencyId = invoice.CurrencyId,
                            Alloter = user.EmpId,
                            AllotDate = DateTime.Now,
                            AllotStatus = StatusEnum.已生效
                        });

                        if (result.ResultStatus != 0)
                            return result;

                        int allotId = (int)result.ReturnValue;

                        FinBusInvAllotDetailDAL finBusInvAllotDetailDAL = new FinBusInvAllotDetailDAL();
                        foreach (string businessInvoiceId in bids.Split(','))
                        {
                            result = invoiceDAL.GetByBussinessInvoiceId(user, Convert.ToInt32(businessInvoiceId));
                            if (result.ResultStatus != 0)
                                return result;

                            Operate.Model.Invoice busInvoice = result.ReturnValue as Operate.Model.Invoice;

                            result = finBusInvAllotDetailDAL.Insert(user, new FinBusInvAllotDetail()
                            {
                                AllotId = allotId,
                                BusinessInvoiceId = Convert.ToInt32(businessInvoiceId),
                                FinanceInvoiceId = financeInvoiceId,
                                AllotBala = busInvoice.InvoiceBala,
                                DetailStatus = StatusEnum.已生效
                            });
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    //添加开票申请与财务票关联
                    InvoiceApplyFinanceDAL invoiceApplyFinanceDAL = new InvoiceApplyFinanceDAL();
                    result = invoiceApplyFinanceDAL.Insert(user, new InvoiceApplyFinance()
                    {
                        InvoiceId = invoiceId,
                        FinanceInvoiceId = financeInvoiceId,
                        InvoiceApplyId = invoiceApplyId
                    });
                    if (result.ResultStatus != 0)
                        return result;

                    invoice.InvoiceId = invoiceId;
                    if (result.ResultStatus == 0) result.ReturnValue = invoice;

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

        public SelectModel GetFinInvReportSelect(int pageIndex, int pageSize, string orderStr, string invNo, string invName, int invDir, DateTime startDate, DateTime endDate)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "inv.InvoiceId desc";
            else
                select.OrderStr = orderStr;

            BDStyleDetail bdIssue = DetailProvider.Details(StyleEnum.发票方向)["Issue"];//发票开具
            BDStyleDetail bdCollect = DetailProvider.Details(StyleEnum.发票方向)["Collect"];//发票收取

            int readyStatus = (int)StatusEnum.已生效;
            StringBuilder sb = new StringBuilder();

            sb.Append("inv.InvoiceId,fi.FinanceInvoiceId,inv.InvoiceDate,inv.InvoiceName,inv.InvoiceNo,bdInvoiceDirection.DetailName,ass.AssetName,fi.NetAmount,mu.MUName,inv.InvoiceBala,cur.CurrencyName,");
            sb.AppendFormat("case inv.InvoiceDirection when {0} then outCorp.CorpName when {1} then inCorp.CorpName else '' end as innerCorp,", bdIssue.StyleDetailId, bdCollect.StyleDetailId);
            sb.AppendFormat("case inv.InvoiceDirection when {0} then inCorp.CorpName when {1} then outCorp.CorpName else '' end as outerCorp", bdIssue.StyleDetailId, bdCollect.StyleDetailId);
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT..Inv_FinanceInvoice fi ");
            sb.Append(" inner join NFMT..Invoice inv on fi.InvoiceId = inv.InvoiceId ");
            sb.AppendFormat(" left join NFMT_Basic..BDStyleDetail bdInvoiceDirection on bdInvoiceDirection.StyleDetailId = inv.InvoiceDirection and bdInvoiceDirection.BDStyleId = {0} ", (int)StyleEnum.发票方向);
            sb.Append(" left join NFMT_Basic..Asset ass on ass.AssetId = fi.AssetId ");
            sb.Append(" left join NFMT_Basic..MeasureUnit mu on mu.MUId = fi.MUId ");
            sb.Append(" left join NFMT_Basic..Currency cur on cur.CurrencyId = inv.CurrencyId ");
            sb.Append(" left join NFMT_User..Corporation outCorp on outCorp.CorpId = inv.OutCorpId ");
            sb.Append(" left join NFMT_User..Corporation inCorp on inCorp.CorpId = inv.InCorpId ");

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" inv.InvoiceStatus >= {0} ", readyStatus);
            if (!string.IsNullOrEmpty(invNo))
                sb.AppendFormat(" and inv.InvoiceNo like '%{0}%' ", invNo);
            if (!string.IsNullOrEmpty(invName))
                sb.AppendFormat(" and inv.InvoiceName like '%{0}%' ", invName);
            if (invDir > 0)
                sb.AppendFormat(" and inv.InvoiceDirection = {0} ", invDir);
            if (startDate > DefaultValue.DefaultTime && endDate > startDate)
                sb.AppendFormat(" and inv.InvoiceDate between '{0}' and '{1}' ", startDate.ToString(), endDate.AddDays(1).ToString());

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
