/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PaymentVirtualBLL.cs
// 文件功能描述：虚拟收付款dbo.Fun_PaymentVirtual业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月20日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Funds.Model;
using NFMT.Funds.DAL;
using NFMT.Funds.IDAL;
using NFMT.Common;

namespace NFMT.Funds.BLL
{
    /// <summary>
    /// 虚拟收付款dbo.Fun_PaymentVirtual业务逻辑类。
    /// </summary>
    public class PaymentVirtualBLL : Common.ExecBLL
    {
        private PaymentVirtualDAL paymentvirtualDAL = new PaymentVirtualDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PaymentVirtualDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PaymentVirtualBLL()
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
            get { return this.paymentvirtualDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel GetByPaymentId(UserModel user, int paymentId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.paymentvirtualDAL.GetByPaymentId(user, paymentId);                
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

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, DateTime fromDate, DateTime toDate, int empId, int corpId, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pv.VirtualId desc";
            else
                select.OrderStr = orderStr;

            int commonStatusType = (int)Common.StatusTypeEnum.通用状态;
            int closeStatus = (int)Common.StatusEnum.已关闭;
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" pv.VirtualId,pay.PaymentId,pa.PayApplyId,app.ApplyId,pay.PaymentStatus,pay.PayDatetime,pay.PayCorp,payCorp.CorpName as PayCorpName,pay.RecevableCorp,recCorp.CorpName as RecCorpName,app.ApplyTime,app.ApplyNo,pa.PayApplySource,pa.ApplyBala,pay.PayBala,pay.CurrencyId,cur.CurrencyName,pv.PayBala as VirtualBala,pay.Memo,pay.FlowName,pv.DetailStatus,sd.StatusName,pay.PayEmpId,emp.Name as PayEmpName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_PaymentVirtual pv ");
            sb.Append(" inner join dbo.Fun_Payment pay on pv.PaymentId = pay.PaymentId ");
            sb.Append(" inner join dbo.Fun_PayApply pa on pv.PayApplyId = pa.PayApplyId ");
            sb.Append(" inner join dbo.Apply app on pa.ApplyId = app.ApplyId ");
            sb.Append(" left join NFMT_User.dbo.Corporation payCorp on payCorp.CorpId = pay.PayCorp ");
            sb.Append(" left join NFMT_User.dbo.Corporation recCorp on recCorp.CorpId = pay.RecevableCorp ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = pay.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = pv.DetailStatus and sd.StatusId={0} ", commonStatusType);
            sb.Append(" left join NFMT_User.dbo.Employee emp on emp.EmpId = pay.PayEmpId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" pv.DetailStatus>={0} ", closeStatus);
            if (status > 0)
                sb.AppendFormat(" and pv.DetailStatus ={0} ", status);
            if (empId > 0)
                sb.AppendFormat(" and pay.PayEmpId ={0} ", empId);
            if (corpId > 0)
                sb.AppendFormat(" and pay.RecevableCorp = {0} ", corpId);
            if (fromDate > Common.DefaultValue.DefaultTime && toDate >= fromDate)
                sb.AppendFormat(" and app.ApplyTime between '{0}' and '{1}' ", fromDate.ToString(), toDate.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel PaymentVirtualConfirm(UserModel user, Model.PaymentVirtual paymentVirtual)
        {
            ResultModel result = new ResultModel();

            try 
            {
                DAL.PaymentDAL paymentDAL = new PaymentDAL();
                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证虚拟付款
                    result = paymentvirtualDAL.Get(user, paymentVirtual.VirtualId);

                    if (result.ResultStatus != 0)
                        return result;

                    Model.PaymentVirtual resultObj = result.ReturnValue as Model.PaymentVirtual;
                    if (resultObj == null || resultObj.VirtualId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "虚拟付款不存在";
                        return result;
                    }

                    if (resultObj.DetailStatus != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "虚拟付款状态不能进行确认";
                        return result;
                    }

                    //获取财务付款
                    result = paymentDAL.Get(user, resultObj.PaymentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Payment payment = result.ReturnValue as Model.Payment;
                    if (payment == null || payment.PaymentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款不存在";
                        return result;
                    }

                    //更新虚拟付款
                    resultObj.ConfirmMemo = paymentVirtual.ConfirmMemo;
                    resultObj.DetailStatus = StatusEnum.已完成;

                    result = paymentvirtualDAL.Update(user,resultObj);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取对应的付款申请
                    DAL.PayApplyDAL payApplyDAL = new PayApplyDAL();
                    result = payApplyDAL.Get(user, resultObj.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PayApply payApply = result.ReturnValue as Model.PayApply;
                    if (payApply == null || payApply.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请不存在";
                        return result;
                    }

                    NFMT.Data.DAL.BankDAL outBankDAL = new Data.DAL.BankDAL();
                    NFMT.User.DAL.BlocDAL blocDAL = new User.DAL.BlocDAL();
                    DAL.FundsLogDAL fundsLogDAL = new FundsLogDAL();

                    //写付款资金流水，状态为已完成
                    Model.FundsLog fundsLog = new FundsLog();
                    fundsLog.CurrencyId = payment.CurrencyId;
                    fundsLog.FundsBala = payment.FundsBala;
                    fundsLog.FundsDesc = payment.Memo;
                    fundsLog.InAccountId = payment.PayBankAccountId;
                    fundsLog.InBankId = payment.PayBankId;
                    fundsLog.InBlocId = user.BlocId;
                    fundsLog.InCorpId = payment.PayCorp;
                    fundsLog.IsVirtualPay = false;
                    fundsLog.LogDate = DateTime.Now;
                    fundsLog.LogDirection = (int)NFMT.WareHouse.LogDirectionEnum.Out;
                    fundsLog.LogSource = "dbo.Fun_PaymentVirtual";
                    fundsLog.LogSourceBase = "NFMT";
                    fundsLog.LogStatus = StatusEnum.已完成;
                    fundsLog.LogType = (int)NFMT.WareHouse.LogTypeEnum.付款;
                    fundsLog.OpPerson = user.EmpId;
                    fundsLog.OutAccount = payment.ReceBankAccount;
                    fundsLog.OutAccountId = payment.ReceBankAccountId;
                    result = outBankDAL.Get(user, payment.ReceBankId);
                    if (result.ResultStatus != 0)
                        return result;
                    NFMT.Data.Model.Bank outBank = result.ReturnValue as NFMT.Data.Model.Bank;
                    if (outBank == null || outBank.BankId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收款银行不存在";
                        return result;
                    }
                    fundsLog.OutBank = outBank.BankName;
                    fundsLog.OutBankId = payment.ReceBankId;

                    result = blocDAL.GetBlocByCorpId(user, payment.RecevableCorp);
                    if (result.ResultStatus == 0)
                    {
                        NFMT.User.Model.Bloc bloc = result.ReturnValue as NFMT.User.Model.Bloc;
                        if (bloc != null && bloc.BlocId > 0)
                            fundsLog.OutBlocId = bloc.BlocId;
                    }
                    fundsLog.OutCorpId = payment.RecevableCorp;
                    fundsLog.PayMode = payment.PayStyle;
                    fundsLog.SourceId = payment.PaymentId;

                    if (payApply.PayApplySource == (int)FundsStyleEnum.库存付款申请 || payApply.PayApplySource == (int)FundsStyleEnum.合约付款申请)
                    {
                        //加载合约明细
                        DAL.PaymentContractDetailDAL paymentContractDetailDAL = new PaymentContractDetailDAL();
                        result = paymentContractDetailDAL.GetByPaymentId(user, payment.PaymentId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.PaymentContractDetail paymentContractDetail = result.ReturnValue as Model.PaymentContractDetail;
                        if (paymentContractDetail == null || paymentContractDetail.DetailId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "付款合约明细加载失败";
                            return result;
                        }

                        fundsLog.ContractId = paymentContractDetail.ContractId;
                        fundsLog.SubId = paymentContractDetail.ContractSubId;
                    }

                    result = fundsLogDAL.Insert(user, fundsLog);
                    if (result.ResultStatus != 0)
                        return result;

                    int fundsLogId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out fundsLogId) || fundsLogId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款流水新增失败";
                        return result;
                    }

                    //更新虚拟付款表的资金流水序号
                    resultObj.FundsLogId = fundsLogId;
                    resultObj.DetailStatus = StatusEnum.已完成;
                    result = this.paymentvirtualDAL.Update(user, resultObj);
                    if (result.ResultStatus != 0)
                        return result;
                    
                    //反向分配一收款至对应公司

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

        public ResultModel Close(UserModel user, int virtualId)
        {
            ResultModel result = new ResultModel();

            try 
            {
                DAL.PayApplyDAL payApplyDAL = new PayApplyDAL();
                DAL.PaymentDAL paymentDAL = new PaymentDAL();
                DAL.PaymentInvioceDetailDAL invoiceDetailDAL = new PaymentInvioceDetailDAL();
                DAL.PaymentStockDetailDAL stockDetailDAL = new PaymentStockDetailDAL();
                DAL.PaymentContractDetailDAL contractDetailDAL = new PaymentContractDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取虚拟付款
                    result = this.paymentvirtualDAL.Get(user, virtualId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PaymentVirtual paymentVirtual = result.ReturnValue as Model.PaymentVirtual;
                    if (paymentVirtual == null || paymentVirtual.VirtualId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "虚拟付款不存在";
                        return result;
                    }

                    //关闭虚拟付款
                    result = this.paymentvirtualDAL.Close(user, paymentVirtual);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取主付款
                    result = paymentDAL.Get(user, paymentVirtual.PaymentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Payment payment = result.ReturnValue as Model.Payment;
                    if (payment == null || payment.PaymentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主付款不存在";
                        return result;
                    }

                    //当前主付款虚拟金额更新为0
                    payment.VirtualBala = 0;
                    payment.PayBala = payment.FundsBala;
                    result = paymentDAL.Update(user, payment);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取对应付款申请
                    result = payApplyDAL.Get(user, payment.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PayApply payApply = result.ReturnValue as Model.PayApply;
                    if (payApply == null || payApply.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请获取失败";
                        return result;
                    }

                    if (payApply.PayApplySource == (int)FundsStyleEnum.InvoicePayApply)
                    {
                        //获取所有发票明细
                        result = invoiceDetailDAL.Load(user, payment.PaymentId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.PaymentInvioceDetail> invoiceDetails = result.ReturnValue as List<Model.PaymentInvioceDetail>;
                        if (invoiceDetails == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "发票明细获取失败";
                            return result;
                        }

                        //当前主付款发票明细虚拟金额更新为0
                        foreach (Model.PaymentInvioceDetail invoiceDetail in invoiceDetails)
                        {
                            invoiceDetail.VirtualBala =0;
                            invoiceDetail.PayBala = invoiceDetail.FundsBala;
                            result = invoiceDetailDAL.Update(user, invoiceDetail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }
                    else if (payApply.PayApplySource == (int)FundsStyleEnum.ContractPayApply)
                    {
                        //获取合约明细
                        result = contractDetailDAL.GetByPaymentId(user, payment.PaymentId);
                        if(result.ResultStatus!=0)
                            return result;

                        Model.PaymentContractDetail contractDetail = result.ReturnValue as Model.PaymentContractDetail;
                        if (contractDetail == null || contractDetail.DetailId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "合约明细获取失败";
                            return result;
                        }

                        //当前主付款合约明细虚拟金额更新为0
                        contractDetail.VirtualBala = 0;
                        contractDetail.PayBala = contractDetail.FundsBala;
                        result = contractDetailDAL.Update(user, contractDetail);
                        if (result.ResultStatus != 0)
                            return result;

                    }
                    else if (payApply.PayApplySource == (int)FundsStyleEnum.StockPayApply)
                    {
                        //获取合约明细
                        result = contractDetailDAL.GetByPaymentId(user, payment.PaymentId);
                        if(result.ResultStatus!=0)
                            return result;

                        Model.PaymentContractDetail contractDetail = result.ReturnValue as Model.PaymentContractDetail;
                        if (contractDetail == null || contractDetail.DetailId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "合约明细获取失败";
                            return result;
                        }

                        //当前主付款合约明细虚拟金额更新为0
                        contractDetail.VirtualBala = 0;
                        contractDetail.PayBala = contractDetail.FundsBala;
                        result = contractDetailDAL.Update(user, contractDetail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取库存明细
                        result = stockDetailDAL.LoadByPaymentId(user, payment.PaymentId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.PaymentStockDetail> stockDetails = result.ReturnValue as List<Model.PaymentStockDetail>;
                        if (stockDetails == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存明细获取失败";
                            return result;
                        }

                        //当前主付款库存明细虚拟金额更新为0
                        foreach (Model.PaymentStockDetail stockDetail in stockDetails)
                        {
                            stockDetail.VirtualBala = 0;
                            stockDetail.PayBala = stockDetail.FundsBala;
                            result = stockDetailDAL.Update(user, stockDetail);

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
    }
}
