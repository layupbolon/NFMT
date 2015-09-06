/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PaymentBLL.cs
// 文件功能描述：财务付款dbo.Fun_Payment业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月15日
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
using System.Linq;

namespace NFMT.Funds.BLL
{
    /// <summary>
    /// 财务付款dbo.Fun_Payment业务逻辑类。
    /// </summary>
    public class PaymentBLL : Common.ExecBLL
    {
        private PaymentDAL paymentDAL = new PaymentDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PaymentDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PaymentBLL()
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
            get { return this.paymentDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, DateTime fromDate, DateTime toDate, int empId, int corpId, int status,int payApplyId=0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pay.PaymentId desc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;
            int readyStatus = (int)Common.StatusEnum.已生效;
            int payMode = (int)Data.StyleEnum.付款方式;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" pay.PaymentId,pa.PayApplyId,app.ApplyId,pay.PaymentCode,app.ApplyNo,pay.RecevableCorp,recCorp.CorpName as RecevableCorpName,pay.PayStyle,ps.DetailName as PayStyleName,pay.PayBala,pay.CurrencyId,cur.CurrencyName,pay.PayDatetime,pay.PayEmpId,emp.Name as PayEmpName,pay.PaymentStatus,sd.StatusName as PaymentStatusName,pa.PayApplySource ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_Payment pay ");
            sb.Append(" inner join dbo.Fun_PayApply pa on pay.PayApplyId = pa.PayApplyId ");
            sb.AppendFormat(" inner join dbo.Apply app on pa.ApplyId = app.ApplyId and app.ApplyStatus >= {0} ", readyStatus);
            sb.Append(" inner join NFMT_User.dbo.Corporation recCorp on recCorp.CorpId = pay.RecevableCorp ");
            sb.AppendFormat(" inner join NFMT_Basic.dbo.BDStyleDetail ps on ps.StyleDetailId = pay.PayStyle and ps.BDStyleId ={0} ", payMode);
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = pay.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Employee emp on emp.EmpId = pay.PayEmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = pay.PaymentStatus and sd.StatusId={0} ", statusId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1");

            if (payApplyId > 0)
                sb.AppendFormat(" and pay.PayApplyId={0} ", payApplyId);

            if (status > 0)
                sb.AppendFormat(" and pay.PaymentStatus ={0} ", status);
            if (empId > 0)
                sb.AppendFormat(" and pay.PayEmpId ={0} ", empId);
            if (corpId > 0)
                sb.AppendFormat(" and pay.RecevableCorp = {0} ", corpId);
            if (fromDate > Common.DefaultValue.DefaultTime && toDate >= fromDate)
                sb.AppendFormat(" and pay.PayDatetime between '{0}' and '{1}' ", fromDate.ToString(), toDate.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel PaymentContractCreate(UserModel user, Model.Payment payment)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.PayApplyDAL payApplyDAL = new PayApplyDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
                DAL.ContractPayApplyDAL contractPayApplyDAL = new ContractPayApplyDAL();
                DAL.PaymentContractDetailDAL paymentContractDetailDAL = new PaymentContractDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证付款总额是否等于财务付款金额+虚拟付款金额
                    if (payment.PayBala != payment.FundsBala + payment.VirtualBala)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款总额必须等于财务付款金额+虚拟付款金额";
                        return result;
                    }

                    //验证付款申请
                    result = payApplyDAL.Get(user, payment.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.PayApply payApply = result.ReturnValue as Model.PayApply;
                    if (payApply == null || payApply.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请不存在";
                        return result;
                    }

                    //获取主申请
                    result = applyDAL.Get(user, payApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款主申请不存在";
                        return result;
                    }

                    //判断申请状态是否已生效
                    if (apply.ApplyStatus != NFMT.Common.StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请非已生效状态，不能进行付款";
                        return result;
                    }

                    //获取当前付款申请中的所有付款
                    result = paymentDAL.Load(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.Payment> payments = result.ReturnValue as List<Model.Payment>;
                    if (payments == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取当前付款申请的付款执行失败";
                        return result;
                    }

                    //判断付款申请可付余额
                    decimal payedBala = payments.Sum(temp => temp.PayBala);
                    if (payApply.ApplyBala - payedBala < 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "当前付款申请已无余款可付";
                        return result;
                    }

                    if (payApply.ApplyBala - payedBala - payment.PayBala < 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "当前付款金额大于可付余额";
                        return result;
                    }

                    //获取付款申请对应合约明细表
                    result = contractPayApplyDAL.GetByPayApplyId(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ContractPayApply contractPayApply = result.ReturnValue as Model.ContractPayApply;
                    if (contractPayApply == null || contractPayApply.RefId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请关联合约获取失败";
                        return result;
                    }

                    //添加付款表
                    payment.PayEmpId = user.EmpId;
                    payment.PayDept = apply.ApplyDept;
                    payment.PayApplyId = payApply.PayApplyId;
                    payment.PaymentStatus = StatusEnum.已录入;

                    result = paymentDAL.Insert(user, payment);
                    if (result.ResultStatus != 0)
                        return result;

                    int paymentId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out paymentId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款新增失败";
                        return result;
                    }
                    payment.PaymentId = paymentId;

                    //添加付款合约明细表
                    Model.PaymentContractDetail paymentContractDetail = new PaymentContractDetail();
                    paymentContractDetail.ContractId = contractPayApply.ContractId;
                    paymentContractDetail.ContractSubId = contractPayApply.ContractSubId;
                    paymentContractDetail.PayApplyDetailId = contractPayApply.RefId;
                    paymentContractDetail.PaymentId = paymentId;
                    paymentContractDetail.PayApplyId = payApply.PayApplyId;
                    paymentContractDetail.PayBala = payment.PayBala;
                    paymentContractDetail.FundsBala = payment.FundsBala;
                    paymentContractDetail.VirtualBala = payment.VirtualBala;

                    result = paymentContractDetailDAL.Insert(user, paymentContractDetail);
                    if (result.ResultStatus != 0)
                        return result;

                    //添加虚拟收付款
                    if (payment.VirtualBala > 0)
                    {
                        if (payment.VirtualBala > payment.PayBala)
                        {
                            result.ResultStatus = -1;
                            result.Message = "虚拟付款金额不能大于付款总额";
                            return result;
                        }

                        DAL.PaymentVirtualDAL virtualDAL = new PaymentVirtualDAL();
                        Model.PaymentVirtual paymentVirtual = new PaymentVirtual();
                        paymentVirtual.PayApplyId = payApply.PayApplyId;
                        paymentVirtual.PayBala = payment.VirtualBala;
                        paymentVirtual.PaymentId = paymentId;
                        paymentVirtual.DetailStatus = StatusEnum.已录入;

                        result = virtualDAL.Insert(user, paymentVirtual);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = paymentId;

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

        public ResultModel PaymentContractUpdate(UserModel user, Model.Payment payment)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.PayApplyDAL payApplyDAL = new PayApplyDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
                DAL.ContractPayApplyDAL contractPayApplyDAL = new ContractPayApplyDAL();
                DAL.PaymentContractDetailDAL paymentContractDetailDAL = new PaymentContractDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证付款总额是否等于财务付款金额+虚拟付款金额
                    if (payment.PayBala != payment.FundsBala + payment.VirtualBala)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款总额必须等于财务付款金额+虚拟付款金额";
                        return result;
                    }

                    //验证财务付款
                    result = paymentDAL.Get(user, payment.PaymentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Payment resultObj = result.ReturnValue as Model.Payment;
                    if (resultObj == null || resultObj.PaymentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "财务付款不存在，不能进行修改";
                        return result;
                    }

                    //验证付款申请
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

                    //获取主申请
                    result = applyDAL.Get(user, payApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款主申请不存在";
                        return result;
                    }

                    //判断申请状态是否已生效
                    if (apply.ApplyStatus != NFMT.Common.StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请非已生效状态，不能进行付款";
                        return result;
                    }

                    //获取当前付款申请中的所有付款
                    result = paymentDAL.Load(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.Payment> payments = result.ReturnValue as List<Model.Payment>;
                    if (payments == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取当前付款申请的付款执行失败";
                        return result;
                    }

                    //判断付款申请可付余额
                    decimal payedBala = payments.Sum(temp => temp.PayBala);

                    if (payApply.ApplyBala - payedBala - payment.PayBala + resultObj.PayBala < 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "当前付款金额大于可付余额";
                        return result;
                    }

                    //获取付款申请对应合约明细表
                    result = contractPayApplyDAL.GetByPayApplyId(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ContractPayApply contractPayApply = result.ReturnValue as Model.ContractPayApply;
                    if (contractPayApply == null || contractPayApply.RefId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请关联合约获取失败";
                        return result;
                    }

                    //修改付款表
                    resultObj.PayBala = payment.PayBala;
                    resultObj.FundsBala = payment.FundsBala;
                    resultObj.VirtualBala = payment.VirtualBala;
                    resultObj.PayStyle = payment.PayStyle;
                    resultObj.PayBankId = payment.PayBankId;
                    resultObj.PayBankAccountId = payment.PayBankAccountId;
                    resultObj.PayCorp = payment.PayCorp;
                    resultObj.PayDatetime = payment.PayDatetime;
                    resultObj.RecevableCorp = payment.RecevableCorp;
                    resultObj.ReceBankId = payment.ReceBankId;
                    resultObj.ReceBankAccountId = payment.ReceBankAccountId;
                    resultObj.ReceBankAccount = payment.ReceBankAccount;
                    resultObj.FlowName = payment.FlowName;
                    resultObj.Memo = payment.Memo;

                    result = paymentDAL.Update(user, resultObj);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取付款合约明细
                    result = paymentContractDetailDAL.GetByPaymentId(user, payment.PaymentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PaymentContractDetail resultDetail = result.ReturnValue as PaymentContractDetail;
                    if (resultDetail == null || resultDetail.DetailId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "财务付款合约明细信息获取失败";
                        return result;
                    }

                    //修改付款合约明细
                    resultDetail.PaymentId = payment.PaymentId;
                    resultDetail.PayBala = payment.PayBala;
                    resultDetail.VirtualBala = payment.VirtualBala;
                    resultDetail.FundsBala = payment.FundsBala;

                    result = paymentContractDetailDAL.Update(user, resultDetail);
                    if (result.ResultStatus != 0)
                        return result;

                    //虚拟收付款
                    //获取虚拟收付款明细
                    DAL.PaymentVirtualDAL paymentVirtualDAL = new PaymentVirtualDAL();

                    result = paymentVirtualDAL.GetByPaymentId(user, payment.PaymentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PaymentVirtual paymentVirtual = result.ReturnValue as Model.PaymentVirtual;
                    if (paymentVirtual == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取虚拟收付款信息失败";
                        return result;
                    }
                    if (payment.VirtualBala == 0)
                    {
                        if (paymentVirtual.VirtualId > 0)
                        {
                            //作废原有虚拟收付款
                            result = paymentVirtualDAL.Invalid(user, paymentVirtual);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }
                    else
                    {
                        if (payment.VirtualBala > payment.PayBala)
                        {
                            result.ResultStatus = -1;
                            result.Message = "虚拟付款金额不能大于付款总额";
                            return result;
                        }

                        if (paymentVirtual.VirtualId > 0)
                        {
                            //更新虚拟收付款
                            paymentVirtual.PayBala = payment.VirtualBala;
                            result = paymentVirtualDAL.Update(user, paymentVirtual);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                        else
                        {
                            //新增虚拟收付款
                            paymentVirtual.PayApplyId = resultObj.PayApplyId;
                            paymentVirtual.PayBala = payment.VirtualBala;
                            paymentVirtual.PaymentId = resultObj.PaymentId;
                            paymentVirtual.DetailStatus = StatusEnum.已录入;

                            result = paymentVirtualDAL.Insert(user, paymentVirtual);
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

        public ResultModel PaymentStockCreate(UserModel user, Model.Payment payment, List<Model.PaymentStockDetail> paymentStockDetails, int payApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.PayApplyDAL payApplyDAL = new PayApplyDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
                DAL.StockPayApplyDAL stockPayApplyDAL = new StockPayApplyDAL();
                DAL.PaymentStockDetailDAL paymentStockDetailDAL = new PaymentStockDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //设置库存明细付款总额
                    foreach (Model.PaymentStockDetail d in paymentStockDetails)
                    {
                        d.PayBala = d.FundsBala + d.VirtualBala;
                    }

                    //验证付款总额是否等于财务付款金额+虚拟付款金额
                    if (payment.PayBala != payment.FundsBala + payment.VirtualBala)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款总额必须等于财务付款金额+虚拟付款金额";
                        return result;
                    }

                    //验证付款申请
                    result = payApplyDAL.Get(user, payApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.PayApply payApply = result.ReturnValue as Model.PayApply;
                    if (payApply == null || payApply.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请不存在";
                        return result;
                    }

                    //获取主申请
                    result = applyDAL.Get(user, payApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款主申请不存在";
                        return result;
                    }

                    //判断申请状态是否已生效
                    if (apply.ApplyStatus != NFMT.Common.StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请非已生效状态，不能进行付款";
                        return result;
                    }

                    //获取当前付款申请中的所有付款
                    result = paymentDAL.Load(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.Payment> payments = result.ReturnValue as List<Model.Payment>;
                    if (payments == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取当前付款申请的付款执行失败";
                        return result;
                    }

                    //判断付款申请可付余额
                    decimal payedBala = payments.Sum(temp => temp.PayBala);
                    if (payApply.ApplyBala - payedBala < 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "当前付款申请已无余款可付";
                        return result;
                    }

                    if (payApply.ApplyBala - payedBala - payment.PayBala < 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "当前付款金额大于可付余额";
                        return result;
                    }

                    //获取付款申请对应库存明细列表
                    result = stockPayApplyDAL.Load(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockPayApply> stockPayApplies = result.ReturnValue as List<Model.StockPayApply>;
                    if (stockPayApplies == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请关联合约获取失败";
                        return result;
                    }

                    foreach (PaymentStockDetail d in paymentStockDetails)
                    {
                        if (d.PayBala > 0)
                        {
                            //获取当前库存付款明细列表
                            result = paymentStockDetailDAL.LoadByStockPayApplyId(user, d.PayApplyDetailId);
                            if (result.ResultStatus != 0)
                                return result;

                            List<Model.PaymentStockDetail> details = result.ReturnValue as List<Model.PaymentStockDetail>;
                            decimal detailPayedBala = details.Sum(temp => temp.PayBala);

                            var ls = paymentStockDetails.Where(temp => temp.PayApplyDetailId == d.PayApplyDetailId);
                            decimal detailPayingBala = ls.Sum(temp => temp.PayBala);

                            //获取对应的申请明细
                            result = stockPayApplyDAL.Get(user, d.PayApplyDetailId);
                            if (result.ResultStatus != 0)
                                return result;

                            StockPayApply stockPayApply = result.ReturnValue as StockPayApply;
                            if (stockPayApply == null || stockPayApply.RefId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = string.Format("库存明细获取失败");
                                return result;
                            }

                            if (stockPayApply.ApplyBala - detailPayedBala - detailPayingBala < 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = string.Format("库存明细付款超额");
                                return result;
                            }
                        }
                    }

                    decimal sumFundsBala = paymentStockDetails.Sum(temp => temp.FundsBala);
                    decimal sumVirtualBala = paymentStockDetails.Sum(temp => temp.VirtualBala);
                    decimal sumPayBala = sumFundsBala + sumVirtualBala;
                    //添加付款表
                    payment.PayEmpId = user.EmpId;
                    payment.PayDept = apply.ApplyDept;
                    payment.PayApplyId = payApply.PayApplyId;
                    payment.PayBala = sumPayBala;
                    payment.FundsBala = sumFundsBala;
                    payment.VirtualBala = sumVirtualBala;
                    payment.PaymentStatus = StatusEnum.已录入;

                    result = paymentDAL.Insert(user, payment);
                    if (result.ResultStatus != 0)
                        return result;

                    int paymentId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out paymentId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款新增失败";
                        return result;
                    }
                    payment.PaymentId = paymentId;

                    //添加付款合约明细表
                    Model.StockPayApply payApplyStock = stockPayApplies[0];
                    Model.PaymentContractDetail paymentContractDetail = new PaymentContractDetail();
                    paymentContractDetail.ContractId = payApplyStock.ContractId;
                    paymentContractDetail.ContractSubId = payApplyStock.SubId;
                    paymentContractDetail.PayApplyDetailId = payApplyStock.ContractRefId;
                    paymentContractDetail.PayApplyId = payApplyStock.PayApplyId;
                    paymentContractDetail.PayBala = sumPayBala;
                    paymentContractDetail.FundsBala = sumFundsBala;
                    paymentContractDetail.VirtualBala = sumVirtualBala;
                    paymentContractDetail.PaymentId = paymentId;

                    DAL.PaymentContractDetailDAL paymentContractDetailDAL = new PaymentContractDetailDAL();
                    result = paymentContractDetailDAL.Insert(user, paymentContractDetail);
                    if (result.ResultStatus != 0)
                        return result;

                    int contractRefId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out contractRefId) || contractRefId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "财务付款失败";
                        return result;
                    }

                    //添加付款库存明细表
                    foreach (PaymentStockDetail d in paymentStockDetails)
                    {
                        if (d.PayBala > 0)
                        {
                            //获取库存付款申请
                            result = stockPayApplyDAL.Get(user, d.PayApplyDetailId);
                            if (result.ResultStatus != 0)
                                return result;

                            Model.StockPayApply dp = result.ReturnValue as Model.StockPayApply;
                            if (dp == null || dp.RefId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "对应付款申请获取失败";
                                return result;
                            }

                            d.DetailStatus = Common.StatusEnum.已生效;
                            d.PayApplyId = payApply.PayApplyId;
                            d.PaymentId = paymentId;
                            d.ContractDetailId = contractRefId;
                            d.ContractId = paymentContractDetail.ContractId;
                            d.SubId = paymentContractDetail.ContractSubId;
                            d.StockId = dp.StockId;
                            d.StockLogId = dp.StockLogId;
                            d.PayBala = d.FundsBala + d.VirtualBala;

                            result = paymentStockDetailDAL.Insert(user, d);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    //添加虚拟收付款
                    if (payment.VirtualBala > 0)
                    {
                        if (payment.VirtualBala > payment.PayBala)
                        {
                            result.ResultStatus = -1;
                            result.Message = "虚拟付款金额不能大于付款总额";
                            return result;
                        }

                        DAL.PaymentVirtualDAL virtualDAL = new PaymentVirtualDAL();
                        Model.PaymentVirtual paymentVirtual = new PaymentVirtual();
                        paymentVirtual.PayApplyId = payApply.PayApplyId;
                        paymentVirtual.PayBala = payment.VirtualBala;
                        paymentVirtual.PaymentId = paymentId;
                        paymentVirtual.DetailStatus = StatusEnum.已录入;

                        result = virtualDAL.Insert(user, paymentVirtual);
                        if (result.ResultStatus != 0)
                            return result;
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

        public ResultModel PaymentStockUpdate(UserModel user, Model.Payment payment, List<Model.PaymentStockDetail> paymentStockDetails)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.PayApplyDAL payApplyDAL = new PayApplyDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
                DAL.StockPayApplyDAL stockPayApplyDAL = new StockPayApplyDAL();
                DAL.PaymentStockDetailDAL paymentStockDetailDAL = new PaymentStockDetailDAL();
                DAL.PaymentContractDetailDAL paymentContractDetailDAL = new PaymentContractDetailDAL();
                DAL.ContractPayApplyDAL payApplyContractDAL = new ContractPayApplyDAL();
                NFMT.WareHouse.DAL.StockLogDAL stockLogDAL = new WareHouse.DAL.StockLogDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //设置库存明细付款总额
                    foreach (Model.PaymentStockDetail d in paymentStockDetails)
                    {
                        d.PayBala = d.FundsBala + d.VirtualBala;
                    }

                    //验证付款总额是否等于财务付款金额+虚拟付款金额
                    if (payment.PayBala != payment.FundsBala + payment.VirtualBala)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款总额必须等于财务付款金额+虚拟付款金额";
                        return result;
                    }

                    //验证财务付款
                    result = paymentDAL.Get(user, payment.PaymentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Payment resultObj = result.ReturnValue as Model.Payment;
                    if (resultObj == null || resultObj.PaymentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "财务付款不存在，不能进行修改";
                        return result;
                    }

                    //验证付款申请
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

                    //获取主申请
                    result = applyDAL.Get(user, payApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款主申请不存在";
                        return result;
                    }

                    //判断申请状态是否已生效
                    if (apply.ApplyStatus != NFMT.Common.StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请非已生效状态，不能进行付款";
                        return result;
                    }

                    //获取当前付款申请中的所有付款
                    result = paymentDAL.Load(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.Payment> payments = result.ReturnValue as List<Model.Payment>;
                    if (payments == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取当前付款申请的付款执行失败";
                        return result;
                    }

                    //判断付款申请可付余额
                    decimal payedBala = payments.Sum(temp => temp.PayBala);

                    if (payApply.ApplyBala - payedBala - payment.PayBala + resultObj.PayBala < 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "当前付款金额大于可付余额";
                        return result;
                    }

                    //获取付款申请对应库存明细列表
                    result = stockPayApplyDAL.Load(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockPayApply> stockPayApplies = result.ReturnValue as List<Model.StockPayApply>;
                    if (stockPayApplies == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请关联合约获取失败";
                        return result;
                    }

                    //修改付款表
                    resultObj.PayBala = payment.PayBala;
                    resultObj.FundsBala = payment.FundsBala;
                    resultObj.VirtualBala = payment.VirtualBala;
                    resultObj.PayStyle = payment.PayStyle;
                    resultObj.PayBankId = payment.PayBankId;
                    resultObj.PayBankAccountId = payment.PayBankAccountId;
                    resultObj.PayCorp = payment.PayCorp;
                    resultObj.PayDatetime = payment.PayDatetime;
                    resultObj.RecevableCorp = payment.RecevableCorp;
                    resultObj.ReceBankId = payment.ReceBankId;
                    resultObj.ReceBankAccountId = payment.ReceBankAccountId;
                    resultObj.ReceBankAccount = payment.ReceBankAccount;
                    resultObj.FlowName = payment.FlowName;
                    resultObj.Memo = payment.Memo;

                    result = paymentDAL.Update(user, resultObj);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取付款库存明细列表
                    result = paymentStockDetailDAL.LoadByPaymentId(user, payment.PaymentId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.PaymentStockDetail> resultDetails = result.ReturnValue as List<PaymentStockDetail>;
                    if (resultDetails == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "财务付款库存明细信息获取失败";
                        return result;
                    }

                    //作废现有库存明细
                    foreach (Model.PaymentStockDetail detail in resultDetails)
                    {
                        if (detail.DetailStatus == StatusEnum.已生效)
                            detail.DetailStatus = StatusEnum.已录入;

                        result = paymentStockDetailDAL.Invalid(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取执行对应合约明细
                    result = paymentContractDetailDAL.GetByPaymentId(user, payment.PaymentId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.PaymentContractDetail paymentContractDetail = result.ReturnValue as Model.PaymentContractDetail;
                    if (paymentContractDetail == null || paymentContractDetail.DetailId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取对应合约明细失败";
                        return result;
                    }

                    //更新合约明细
                    paymentContractDetail.PayBala = resultObj.PayBala;
                    paymentContractDetail.FundsBala = resultObj.FundsBala;
                    paymentContractDetail.VirtualBala = resultObj.VirtualBala;
                    result = paymentContractDetailDAL.Update(user, paymentContractDetail);
                    if (result.ResultStatus != 0)
                        return result;

                    //新增付款执行库存明细
                    foreach (PaymentStockDetail d in paymentStockDetails)
                    {
                        if (d.PayBala > 0)
                        {
                            //获取当前库存申请付款明细列表
                            result = paymentStockDetailDAL.LoadByStockPayApplyId(user, d.PayApplyDetailId);
                            if (result.ResultStatus != 0)
                                return result;

                            List<Model.PaymentStockDetail> details = result.ReturnValue as List<Model.PaymentStockDetail>;
                            decimal detailPayedBala = details.Sum(temp => temp.PayBala);

                            var ls = paymentStockDetails.Where(temp => temp.PayApplyDetailId == d.PayApplyDetailId);
                            decimal detailPayingBala = ls.Sum(temp => temp.PayBala);

                            //获取对应的申请明细
                            result = stockPayApplyDAL.Get(user, d.PayApplyDetailId);
                            if (result.ResultStatus != 0)
                                return result;

                            StockPayApply stockPayApply = result.ReturnValue as StockPayApply;
                            if (stockPayApply == null || stockPayApply.RefId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = string.Format("库存明细获取失败");
                                return result;
                            }

                            //验证明细付款是否超额
                            if (stockPayApply.ApplyBala - detailPayedBala - detailPayingBala < 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = string.Format("库存明细付款超额");
                                return result;
                            }

                            //新增明细
                            d.DetailStatus = StatusEnum.已生效;
                            d.PayApplyId = resultObj.PayApplyId;
                            d.PaymentId = resultObj.PaymentId;
                            d.ContractDetailId = paymentContractDetail.DetailId;
                            d.ContractId = paymentContractDetail.ContractId;
                            d.PayApplyDetailId = stockPayApply.RefId;
                            d.SubId = paymentContractDetail.ContractSubId;

                            result = paymentStockDetailDAL.Insert(user, d);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    //虚拟收付款
                    //获取虚拟收付款明细
                    DAL.PaymentVirtualDAL paymentVirtualDAL = new PaymentVirtualDAL();

                    result = paymentVirtualDAL.GetByPaymentId(user, payment.PaymentId);
                    if (result.ResultStatus == 0)
                    {
                        Model.PaymentVirtual paymentVirtual = result.ReturnValue as Model.PaymentVirtual;
                        if (paymentVirtual == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取虚拟收付款信息失败";
                            return result;
                        }
                        if (payment.VirtualBala == 0)
                        {
                            if (paymentVirtual.VirtualId > 0)
                            {
                                //作废原有虚拟收付款
                                result = paymentVirtualDAL.Invalid(user, paymentVirtual);
                                if (result.ResultStatus != 0)
                                    return result;
                            }
                        }
                        else
                        {
                            if (payment.VirtualBala > payment.PayBala)
                            {
                                result.ResultStatus = -1;
                                result.Message = "虚拟付款金额不能大于付款总额";
                                return result;
                            }

                            if (paymentVirtual.VirtualId > 0)
                            {
                                //更新虚拟收付款
                                paymentVirtual.PayBala = payment.VirtualBala;
                                result = paymentVirtualDAL.Update(user, paymentVirtual);
                                if (result.ResultStatus != 0)
                                    return result;
                            }
                            else
                            {
                                //新增虚拟收付款
                                paymentVirtual.PayApplyId = resultObj.PayApplyId;
                                paymentVirtual.PayBala = payment.VirtualBala;
                                paymentVirtual.PaymentId = resultObj.PaymentId;
                                paymentVirtual.DetailStatus = StatusEnum.已录入;

                                result = paymentVirtualDAL.Insert(user, paymentVirtual);
                                if (result.ResultStatus != 0)
                                    return result;
                            }
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

        public ResultModel Invalid(UserModel user, int paymentId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.PayApplyDAL payApplyDAL = new PayApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证财务付款
                    result = paymentDAL.Get(user, paymentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Payment payment = result.ReturnValue as Model.Payment;
                    if (payment == null || payment.PaymentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "财务付款不存在";
                        return result;
                    }

                    //获取财务付款对应付款申请
                    result = payApplyDAL.Get(user, payment.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PayApply payApply = result.ReturnValue as Model.PayApply;
                    if (payApply == null || payApply.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "财务付款对应的付款申请获取失败";
                        return result;
                    }

                    //作废财务付款
                    result = paymentDAL.Invalid(user, payment);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废虚拟明细
                    DAL.PaymentVirtualDAL paymentVirtualDAL = new PaymentVirtualDAL();

                    result = paymentVirtualDAL.GetByPaymentId(user, payment.PaymentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PaymentVirtual paymentVirtual = result.ReturnValue as Model.PaymentVirtual;
                    if (paymentVirtual == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取虚拟收付款信息失败";
                        return result;
                    }
                    if (paymentVirtual.VirtualId > 0 && paymentVirtual.PayBala > 0)
                    {
                        if (paymentVirtual.DetailStatus == StatusEnum.已生效 || paymentVirtual.DetailStatus == StatusEnum.待审核)
                            paymentVirtual.DetailStatus = StatusEnum.已录入;

                        result = paymentVirtualDAL.Invalid(user, paymentVirtual);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (payApply.PayApplySource == (int)Funds.FundsStyleEnum.ContractPayApply)
                    {
                        //财务付款--关联合约
                        //获取财务付款关联的合约明细，并作废
                        DAL.PaymentContractDetailDAL paymentContractDetailDAL = new PaymentContractDetailDAL();
                        result = paymentContractDetailDAL.GetByPaymentId(user, payment.PaymentId);
                        if (result.ResultStatus != 0)
                            return result;
                        Model.PaymentContractDetail paymentContractDetail = result.ReturnValue as Model.PaymentContractDetail;
                        if (paymentContractDetail == null || paymentContractDetail.DetailId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "财务付款对应的明细获取失败";
                            return result;
                        }

                        result = paymentContractDetailDAL.Invalid(user, paymentContractDetail);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                    else if (payApply.PayApplySource == (int)Funds.FundsStyleEnum.StockPayApply)
                    {
                        //财务付款--关联库存
                        //获取财务付款关联的库存明细，并作废

                        DAL.PaymentStockDetailDAL paymentStockDetailDAL = new PaymentStockDetailDAL();
                        result = paymentStockDetailDAL.LoadByPaymentId(user, payment.PaymentId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.PaymentStockDetail> details = result.ReturnValue as List<Model.PaymentStockDetail>;
                        if (details == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "财务付款库存明细获取失败";
                            return result;
                        }

                        foreach (Model.PaymentStockDetail detail in details)
                        {
                            //作废明细
                            if (detail.Status == StatusEnum.已生效)
                                detail.Status = StatusEnum.已录入;

                            result = paymentStockDetailDAL.Invalid(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }
                    result.Message = "作废成功";

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

        public ResultModel Goback(UserModel user, int paymentId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.PayApplyDAL payApplyDAL = new PayApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证财务付款
                    result = paymentDAL.Get(user, paymentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Payment payment = result.ReturnValue as Payment;
                    if (payment == null || payment.PaymentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "财务付款不存在";
                        return result;
                    }

                    //更新财务付款状态至已撤返
                    result = paymentDAL.Goback(user, payment);
                    if (result.ResultStatus != 0)
                        return result;

                    //工作流任务关闭
                    WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, payment);
                    if (result.ResultStatus != 0)
                        return result;

                    result.Message = "撤返成功";
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

        public ResultModel Complete(UserModel user, int paymentId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证付款
                    result = paymentDAL.Get(user, paymentId);
                    if (result.ResultStatus != 0)
                        return result;
                    Payment payment = result.ReturnValue as Payment;
                    if (payment == null || payment.PaymentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能进行执行完成操作";
                        return result;
                    }

                    //执行完成
                    result = paymentDAL.Complete(user, payment);
                    if (result.ResultStatus != 0)
                        return result;

                    ////虚拟明细执行完成
                    //DAL.PaymentVirtualDAL paymentVirtualDAL = new PaymentVirtualDAL();

                    ////获取已生效的虚拟付款明细
                    //result = paymentVirtualDAL.GetByPaymentId(user, payment.PaymentId, Common.StatusEnum.已生效);
                    //if (result.ResultStatus != 0)
                    //    return result;

                    //Model.PaymentVirtual paymentVirtual = result.ReturnValue as Model.PaymentVirtual;
                    //if (paymentVirtual == null)
                    //{
                    //    result.ResultStatus = -1;
                    //    result.Message = "获取虚拟收付款信息失败";
                    //    return result;
                    //}
                    //if (paymentVirtual.VirtualId > 0 && paymentVirtual.PayBala > 0)
                    //{
                    //    result = paymentVirtualDAL.Complete(user, paymentVirtual);
                    //    if (result.ResultStatus != 0)
                    //        return result;
                    //}

                    //获取对应付款申请
                    DAL.PayApplyDAL payApplyDAL = new PayApplyDAL();
                    result = payApplyDAL.Get(user, payment.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.PayApply payApply = result.ReturnValue as PayApply;
                    if (payApply == null || payApply.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "当前付款对应的付款申请获取失败";
                        return result;
                    }

                    if (payApply.PayApplySource == (int)Funds.FundsStyleEnum.InvoicePayApply)
                    {
                        DAL.PaymentInvioceDetailDAL paymentInvoiceDetailDAL = new PaymentInvioceDetailDAL();
                        result = paymentInvoiceDetailDAL.Load(user, payment.PaymentId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.PaymentInvioceDetail> details = result.ReturnValue as List<Model.PaymentInvioceDetail>;
                        if (details == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "财务付款发票明细获取失败";
                            return result;
                        }

                        foreach (Model.PaymentInvioceDetail detail in details)
                        {
                            //完成发票明细
                            result = paymentInvoiceDetailDAL.Complete(user,detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }
                    else if (payApply.PayApplySource == (int)Funds.FundsStyleEnum.StockPayApply)
                    {
                        DAL.PaymentStockDetailDAL paymentStockDetailDAL = new PaymentStockDetailDAL();
                        result = paymentStockDetailDAL.LoadByPaymentId(user, payment.PaymentId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.PaymentStockDetail> details = result.ReturnValue as List<Model.PaymentStockDetail>;
                        if (details == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "财务付款库存明细获取失败";
                            return result;
                        }

                        foreach (Model.PaymentStockDetail detail in details)
                        {
                            //更新状态至已完成
                            result = paymentStockDetailDAL.Complete(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    //更新财务流水至已完成
                    DAL.FundsLogDAL fundsLogDAL = new FundsLogDAL();
                    result = fundsLogDAL.Get(user, payment.FundsLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.FundsLog fundsLog = result.ReturnValue as Model.FundsLog;
                    if (fundsLog == null || fundsLog.FundsLogId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款流水获取失败";
                        return result;
                    }

                    result = fundsLogDAL.Complete(user, fundsLog);
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

        public ResultModel CompleteCancel(UserModel user, int paymentId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证付款
                    result = paymentDAL.Get(user, paymentId);
                    if (result.ResultStatus != 0)
                        return result;
                    Payment payment = result.ReturnValue as Payment;
                    if (payment == null || payment.PaymentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能进行执行完成操作";
                        return result;
                    }

                    //获取对应的付款申请
                    DAL.PayApplyDAL payApplyDAL = new PayApplyDAL();
                    result = payApplyDAL.Get(user, payment.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PayApply payApply = result.ReturnValue as Model.PayApply;
                    if (payApply == null || payApply.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请不存在";
                        return result;
                    }

                    //验证对应付款申请状态
                    //获取主申请
                    NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
                    result = applyDAL.Get(user, payApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请不存在";
                        return result;
                    }

                    if (apply.ApplyStatus != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请状态非已生效，不能进行执行完成撤销";
                        return result;
                    }

                    //更新付款状态至已生效
                    result = paymentDAL.CompleteCancel(user, payment);
                    if (result.ResultStatus != 0)
                        return result;                    

                    if (payApply.PayApplySource == (int)Funds.FundsStyleEnum.InvoicePayApply)
                    {
                        DAL.PaymentInvioceDetailDAL paymentInvoiceDetailDAL = new PaymentInvioceDetailDAL();
                        result = paymentInvoiceDetailDAL.Load(user, payment.PaymentId, Common.StatusEnum.已完成);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.PaymentInvioceDetail> details = result.ReturnValue as List<Model.PaymentInvioceDetail>;
                        if (details == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "财务付款发票明细获取失败";
                            return result;
                        }

                        foreach (Model.PaymentInvioceDetail detail in details)
                        {
                            //更新已完成状态的明细至已生效
                            result = paymentInvoiceDetailDAL.CompleteCancel(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }
                    else if (payApply.PayApplySource == (int)Funds.FundsStyleEnum.StockPayApply)
                    {
                        DAL.PaymentStockDetailDAL paymentStockDetailDAL = new PaymentStockDetailDAL();
                        result = paymentStockDetailDAL.LoadByPaymentId(user, payment.PaymentId, Common.StatusEnum.已完成);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.PaymentStockDetail> details = result.ReturnValue as List<Model.PaymentStockDetail>;
                        if (details == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "财务付款库存明细获取失败";
                            return result;
                        }

                        foreach (Model.PaymentStockDetail detail in details)
                        {
                            //更新已完成状态的明细至已生效
                            result = paymentStockDetailDAL.CompleteCancel(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    //更新财务流水至已生效
                    DAL.FundsLogDAL fundsLogDAL = new FundsLogDAL();
                    result = fundsLogDAL.Get(user, payment.FundsLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.FundsLog fundsLog = result.ReturnValue as Model.FundsLog;
                    if (fundsLog == null || fundsLog.FundsLogId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款流水获取失败";
                        return result;
                    }

                    result = fundsLogDAL.CompleteCancel(user, fundsLog);
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

        public ResultModel Close(UserModel user, int paymentId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证付款
                    result = paymentDAL.Get(user, paymentId);
                    if (result.ResultStatus != 0)
                        return result;
                    Payment payment = result.ReturnValue as Payment;
                    if (payment == null || payment.PaymentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能进行执行完成操作";
                        return result;
                    }

                    //关闭付款
                    result = paymentDAL.Close(user, payment);
                    if (result.ResultStatus != 0)
                        return result;                   

                    //获取对应付款申请
                    DAL.PayApplyDAL payApplyDAL = new PayApplyDAL();
                    result = payApplyDAL.Get(user, payment.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.PayApply payApply = result.ReturnValue as PayApply;
                    if (payApply == null || payApply.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "当前付款对应的付款申请获取失败";
                        return result;
                    }

                    if (payApply.PayApplySource == (int)Funds.FundsStyleEnum.InvoicePayApply)
                    {
                        DAL.PaymentInvioceDetailDAL paymentInvoiceDetailDAL = new PaymentInvioceDetailDAL();
                        result = paymentInvoiceDetailDAL.Load(user, payment.PaymentId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.PaymentInvioceDetail> details = result.ReturnValue as List<Model.PaymentInvioceDetail>;
                        if (details == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "财务付款发票明细获取失败";
                            return result;
                        }

                        foreach (Model.PaymentInvioceDetail detail in details)
                        {
                            //关闭发票明细
                            result = paymentInvoiceDetailDAL.Close(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }
                    else if (payApply.PayApplySource == (int)Funds.FundsStyleEnum.StockPayApply)
                    {
                        DAL.PaymentStockDetailDAL paymentStockDetailDAL = new PaymentStockDetailDAL();
                        result = paymentStockDetailDAL.LoadByPaymentId(user, payment.PaymentId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.PaymentStockDetail> details = result.ReturnValue as List<Model.PaymentStockDetail>;
                        if (details == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "财务付款库存明细获取失败";
                            return result;
                        }

                        foreach (Model.PaymentStockDetail detail in details)
                        {
                            //关闭库存明细
                            result = paymentStockDetailDAL.Close(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    //更新财务流水至已关闭
                    DAL.FundsLogDAL fundsLogDAL = new FundsLogDAL();
                    result = fundsLogDAL.Get(user, payment.FundsLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.FundsLog fundsLog = result.ReturnValue as Model.FundsLog;
                    if (fundsLog == null || fundsLog.FundsLogId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款流水获取失败";
                        return result;
                    }

                    result = fundsLogDAL.Close(user, fundsLog);
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

        public Common.SelectModel GetPaymentStockUpdateSelect(int pageIndex, int pageSize, string orderStr, int payApplyId, int paymentId, bool isDetail = false)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId desc";
            else
                select.OrderStr = orderStr;

            int stockStatusType = (int)Common.StatusTypeEnum.库存状态;
            int readyStatus = (int)Common.StatusEnum.已生效;

            int inLogDirection = (int)NFMT.WareHouse.LogDirectionEnum.In;//NFMT.Data.DetailProvider.Details(Data.StyleEnum.LogDirection)["In"].StyleDetailId;
            //int CancelStockLogType = NFMT.Data.DetailProvider.Details(Data.StyleEnum.LogType)["Reverse"].StyleDetailId;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("sto.StockId,sl.StockLogId,spa.RefId,sn.StockNameId,ass.AssetId,bra.BrandId,cor.CorpId,mu.MUId,sto.StockDate,sn.RefNo,sto.GrossAmount,CAST(sto.GrossAmount as varchar) + mu.MUName as StockWeight,cor.CorpName,ass.AssetName,bra.BrandName,sto.StockStatus,sd.StatusName as StockStatusName,spa.ApplyBala,isnull(psd.PayBala,0) as PayBala ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_StockPayApply_Ref spa ");
            sb.Append(" inner join dbo.St_Stock sto on spa.StockId = sto.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" left join dbo.St_StockLog sl on sto.StockId = sl.StockId and sl.LogDirection = {0} ", inLogDirection);
            //PayBala
            sb.AppendFormat(" left join dbo.Fun_PaymentStockDetail psd on spa.RefId = psd.PayApplyDetailId and psd.PaymentId={0} and psd.DetailStatus >= {1} ", paymentId, readyStatus);
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sto.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_User.dbo.Corporation cor on sto.CorpId = cor.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on sto.BrandId = bra.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on sto.UintId = mu.MUId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sto.StockStatus = sd.DetailId and sd.StatusId={0} ", stockStatusType);
            select.TableName = sb.ToString();

            sb.Clear();

            //sb.AppendFormat(" sl.StockLogId not in (select StockLogId from dbo.St_StockLog where LogType = {0}) ", CancelStockLogType);
            sb.AppendFormat(" spa.PayApplyId={0} and spa.RefStatus>={1} ", payApplyId, readyStatus);
            if (isDetail)
                sb.AppendFormat(" and  isnull(psd.PayBala,0)>0 ");
            select.WhereStr = sb.ToString();

            return select;
        }

        public Common.ResultModel PaymentCheckAudit(UserModel user, int paymentId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证付款
                    result = paymentDAL.Get(user, paymentId);
                    if (result.ResultStatus != 0)
                        return result;
                    Payment payment = result.ReturnValue as Payment;
                    if (payment == null || payment.PaymentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能进行执行完成操作";
                        return result;
                    }

                    //虚拟收付款
                    //获取虚拟收付款明细
                    DAL.PaymentVirtualDAL paymentVirtualDAL = new PaymentVirtualDAL();

                    result = paymentVirtualDAL.HasEntryVirtaul(user, payment.PaymentId);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.AffectCount != 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款中包含未确认的虚拟付款，不允许提交审核";
                        return result;
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

        public SelectModel GetInvoiceCreateSelect(int pageIndex, int pageSize, string orderStr, int payApplyId, int paymentId = 0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ipar.RefId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" ipar.RefId as PayApplyDetailId,pa.PayApplyId,app.ApplyId,si.SIId,inv.InvoiceId,app.ApplyTime,inv.InvoiceDate,inv.InvoiceNo,inv.InCorpId");
            sb.Append(",inv.InCorpName,inv.OutCorpId,inv.OutCorpName,inv.InvoiceBala,ipar.ApplyBala,pa.CurrencyId,cur.CurrencyName,pa.PayMatter");
            sb.Append(",pma.DetailName as PayMatterName,pa.PayMode,pmo.DetailName as PayModeName,si.PayDept,dept.DeptName");
            sb.Append(",ISNULL(ipar.ApplyBala,0) - ISNULL(pid.SumBala,0) as LastBala");
            sb.Append(",ISNULL(pid1.FundsBala,ISNULL(ipar.ApplyBala,0) - ISNULL(pid.SumBala,0)) as FundsBala,ISNULL(pid1.VirtualBala,0) as VirtualBala ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_InvoicePayApply_Ref ipar ");
            sb.Append(" inner join dbo.Fun_PayApply pa on ipar.PayApplyId = pa.PayApplyId ");
            sb.AppendFormat(" inner join dbo.Apply app on app.ApplyId = pa.ApplyId and app.ApplyStatus = {0} ", readyStatus);
            sb.Append(" inner join dbo.Inv_SI si on si.SIId = ipar.SIId ");
            sb.Append(" inner join dbo.Invoice inv on si.InvoiceId = inv.InvoiceId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = pa.CurrencyId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail pma on pma.StyleDetailId = pa.PayMatter ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail pmo on pmo.StyleDetailId = pa.PayMode ");
            sb.Append(" left join NFMT_User.dbo.Department dept on dept.DeptId = si.PayDept ");
            sb.AppendFormat(" left join (select SUM(PayBala) as SumBala,PayApplyDetailId from dbo.Fun_PaymentInvioceDetail where DetailStatus >={0} and PaymentId !={1} group by PayApplyDetailId) as pid on pid.PayApplyDetailId = ipar.RefId", readyStatus, paymentId);
            sb.AppendFormat(" left join (select SUM(FundsBala) as FundsBala,SUM(VirtualBala) as VirtualBala,PayApplyDetailId from dbo.Fun_PaymentInvioceDetail where DetailStatus >={0} and PaymentId ={1} group by PayApplyDetailId) as pid1 on pid1.PayApplyDetailId = ipar.RefId", readyStatus, paymentId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" ipar.DetailStatus = {0} and ipar.PayApplyId = {1} ", readyStatus, payApplyId);
            sb.AppendFormat(" and ISNULL(pa.ApplyBala,0) - ISNULL(pid.SumBala,0)>0 ");

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel PaymentInvoiceCreate(UserModel user, Model.Payment payment, List<Model.PaymentInvioceDetail> paymentInvoiceDetails, int payApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.PayApplyDAL payApplyDAL = new PayApplyDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
                DAL.InvoicePayApplyDAL invoicePayApplyDAL = new InvoicePayApplyDAL();
                DAL.PaymentInvioceDetailDAL paymentInvoiceDetailDAL = new PaymentInvioceDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (Model.PaymentInvioceDetail d in paymentInvoiceDetails)
                    {
                        d.PayBala = d.FundsBala + d.VirtualBala;
                    }

                    //验证付款总额是否等于财务付款金额+虚拟付款金额
                    if (payment.PayBala != payment.FundsBala + payment.VirtualBala)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款总额必须等于财务付款金额+虚拟付款金额";
                        return result;
                    }

                    //验证付款申请
                    result = payApplyDAL.Get(user, payApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.PayApply payApply = result.ReturnValue as Model.PayApply;
                    if (payApply == null || payApply.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请不存在";
                        return result;
                    }

                    //获取主申请
                    result = applyDAL.Get(user, payApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款主申请不存在";
                        return result;
                    }

                    //判断申请状态是否已生效
                    if (apply.ApplyStatus != NFMT.Common.StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请非已生效状态，不能进行付款";
                        return result;
                    }

                    //获取当前付款申请中的所有付款
                    result = paymentDAL.Load(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.Payment> payments = result.ReturnValue as List<Model.Payment>;
                    if (payments == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取当前付款申请的付款执行失败";
                        return result;
                    }

                    //判断付款申请可付余额
                    decimal payedBala = payments.Sum(temp => temp.PayBala);
                    if (payApply.ApplyBala - payedBala < 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "当前付款申请已无余款可付";
                        return result;
                    }

                    if (payApply.ApplyBala - payedBala - payment.PayBala < 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "当前付款金额大于可付余额";
                        return result;
                    }

                    //获取付款申请对应发票明细列表
                    result = invoicePayApplyDAL.Load(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.InvoicePayApply> invoicePayApplies = result.ReturnValue as List<Model.InvoicePayApply>;
                    if (invoicePayApplies == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请明细失败";
                        return result;
                    }

                    foreach (Model.PaymentInvioceDetail d in paymentInvoiceDetails)
                    {
                        if (d.PayBala > 0)
                        {
                            //获取当前发票付款明细列表
                            result = paymentInvoiceDetailDAL.LoadByInvoicePayApplyId(user, d.PayApplyDetailId);
                            if (result.ResultStatus != 0)
                                return result;

                            List<Model.PaymentInvioceDetail> details = result.ReturnValue as List<Model.PaymentInvioceDetail>;
                            decimal detailPayedBala = details.Sum(temp => temp.PayBala);

                            var ls = paymentInvoiceDetails.Where(temp => temp.PayApplyDetailId == d.PayApplyDetailId);
                            decimal detailPayingBala = ls.Sum(temp => temp.PayBala);

                            //获取对应的申请明细
                            result = invoicePayApplyDAL.Get(user, d.PayApplyDetailId);
                            if (result.ResultStatus != 0)
                                return result;

                            InvoicePayApply invoicePayApply = result.ReturnValue as InvoicePayApply;
                            if (invoicePayApply == null || invoicePayApply.RefId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = string.Format("发票申请明细获取失败");
                                return result;
                            }

                            if (invoicePayApply.ApplyBala - detailPayedBala - detailPayingBala < 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = string.Format("发票申请明细付款超额");
                                return result;
                            }
                        }
                    }

                    decimal sumPayBala = paymentInvoiceDetails.Sum(temp => temp.PayBala);
                    decimal sumFundsBala = paymentInvoiceDetails.Sum(temp => temp.FundsBala);
                    decimal sumVirtualBala = paymentInvoiceDetails.Sum(temp => temp.VirtualBala);

                    //添加付款表
                    payment.PayEmpId = user.EmpId;
                    payment.PayDept = apply.ApplyDept;
                    payment.PayApplyId = payApply.PayApplyId;
                    payment.PayBala = sumPayBala;
                    payment.FundsBala = sumFundsBala;
                    payment.VirtualBala = sumVirtualBala;
                    payment.PaymentStatus = StatusEnum.已录入;

                    result = paymentDAL.Insert(user, payment);
                    if (result.ResultStatus != 0)
                        return result;

                    int paymentId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out paymentId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款新增失败";
                        return result;
                    }
                    payment.PaymentId = paymentId;

                    //添加付款发票明细表
                    foreach (PaymentInvioceDetail d in paymentInvoiceDetails)
                    {
                        if (d.PayBala > 0)
                        {
                            //获取发票付款申请
                            result = invoicePayApplyDAL.Get(user, d.PayApplyDetailId);
                            if (result.ResultStatus != 0)
                                return result;

                            Model.InvoicePayApply dp = result.ReturnValue as Model.InvoicePayApply;
                            if (dp == null || dp.RefId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "对应付款申请获取失败";
                                return result;
                            }

                            d.DetailStatus = Common.StatusEnum.已生效;
                            d.PaymentId = paymentId;
                            d.InvoiceId = dp.InvoiceId;
                            d.PayApplyId = dp.PayApplyId;
                            d.PayApplyDetailId = dp.RefId;

                            result = paymentInvoiceDetailDAL.Insert(user, d);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    //添加虚拟收付款
                    if (payment.VirtualBala > 0)
                    {
                        if (payment.VirtualBala > payment.PayBala)
                        {
                            result.ResultStatus = -1;
                            result.Message = "虚拟付款金额不能大于付款总额";
                            return result;
                        }

                        DAL.PaymentVirtualDAL virtualDAL = new PaymentVirtualDAL();
                        Model.PaymentVirtual paymentVirtual = new PaymentVirtual();
                        paymentVirtual.PayApplyId = payApply.PayApplyId;
                        paymentVirtual.PayBala = payment.VirtualBala;
                        paymentVirtual.PaymentId = paymentId;
                        paymentVirtual.DetailStatus = StatusEnum.已录入;

                        result = virtualDAL.Insert(user, paymentVirtual);
                        if (result.ResultStatus != 0)
                            return result;
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

        public ResultModel PaymentInvoiceUpdate(UserModel user, Model.Payment payment, List<Model.PaymentInvioceDetail> paymentInvoiceDetails)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.PayApplyDAL payApplyDAL = new PayApplyDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
                DAL.InvoicePayApplyDAL invoicePayApplyDAL = new InvoicePayApplyDAL();
                DAL.PaymentInvioceDetailDAL paymentInvoiceDetailDAL = new PaymentInvioceDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (paymentInvoiceDetails == null || paymentInvoiceDetails.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "未填写任何发票明细";
                        return result;
                    }

                    foreach (Model.PaymentInvioceDetail d in paymentInvoiceDetails)
                    {
                        d.PayBala = d.FundsBala + d.VirtualBala;
                    }

                    //验证付款总额是否等于财务付款金额+虚拟付款金额
                    if (payment.PayBala != payment.FundsBala + payment.VirtualBala)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款总额必须等于财务付款金额+虚拟付款金额";
                        return result;
                    }

                    //获取财务付款
                    result = this.paymentDAL.Get(user, payment.PaymentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Payment resultPayment = result.ReturnValue as Model.Payment;
                    if (resultPayment == null || resultPayment.PaymentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "财务付款不存在，不能进行修改";
                        return result;
                    }

                    //验证付款申请
                    result = payApplyDAL.Get(user, resultPayment.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.PayApply payApply = result.ReturnValue as Model.PayApply;
                    if (payApply == null || payApply.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请不存在";
                        return result;
                    }

                    //获取主申请
                    result = applyDAL.Get(user, payApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款主申请不存在";
                        return result;
                    }

                    //判断申请状态是否已生效
                    if (apply.ApplyStatus != NFMT.Common.StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请非已生效状态，不能进行付款";
                        return result;
                    }

                    //获取当前付款申请中的所有付款
                    result = paymentDAL.Load(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.Payment> payments = result.ReturnValue as List<Model.Payment>;
                    if (payments == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取当前付款申请的付款执行失败";
                        return result;
                    }

                    //判断付款申请可付余额
                    decimal payedBala = payments.Sum(temp => temp.PayBala);
                    if (payApply.ApplyBala - payedBala < 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "当前付款申请已无余款可付";
                        return result;
                    }

                    if (payApply.ApplyBala - payedBala - payment.PayBala + resultPayment.PayBala < 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "当前付款金额大于可付余额";
                        return result;
                    }

                    //获取付款申请对应发票明细列表
                    result = invoicePayApplyDAL.Load(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.InvoicePayApply> invoicePayApplies = result.ReturnValue as List<Model.InvoicePayApply>;
                    if (invoicePayApplies == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请明细失败";
                        return result;
                    }

                    //作废原有财务付款发票明细
                    result = paymentInvoiceDetailDAL.Load(user, payment.PaymentId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.PaymentInvioceDetail> resultDetails = result.ReturnValue as List<Model.PaymentInvioceDetail>;
                    if (resultDetails == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取财务付款明细失败";
                        return result;
                    }

                    foreach (PaymentInvioceDetail d in resultDetails)
                    {
                        d.Status = StatusEnum.已录入;
                        result = paymentInvoiceDetailDAL.Invalid(user, d);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    foreach (Model.PaymentInvioceDetail d in paymentInvoiceDetails)
                    {
                        if (d.PayBala > 0)
                        {
                            //获取当前发票付款明细列表
                            result = paymentInvoiceDetailDAL.LoadByInvoicePayApplyId(user, d.PayApplyDetailId);
                            if (result.ResultStatus != 0)
                                return result;

                            List<Model.PaymentInvioceDetail> details = result.ReturnValue as List<Model.PaymentInvioceDetail>;
                            decimal detailPayedBala = details.Sum(temp => temp.PayBala);

                            var ls = paymentInvoiceDetails.Where(temp => temp.PayApplyDetailId == d.PayApplyDetailId);
                            decimal detailPayingBala = ls.Sum(temp => temp.PayBala);

                            //获取对应的申请明细
                            result = invoicePayApplyDAL.Get(user, d.PayApplyDetailId);
                            if (result.ResultStatus != 0)
                                return result;

                            InvoicePayApply invoicePayApply = result.ReturnValue as InvoicePayApply;
                            if (invoicePayApply == null || invoicePayApply.RefId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = string.Format("发票申请明细获取失败");
                                return result;
                            }

                            if (invoicePayApply.ApplyBala - detailPayedBala - detailPayingBala < 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = string.Format("发票申请明细付款超额");
                                return result;
                            }
                        }
                    }

                    decimal sumPayBala = paymentInvoiceDetails.Sum(temp => temp.PayBala);
                    decimal sumFundsBala = paymentInvoiceDetails.Sum(temp => temp.FundsBala);
                    decimal sumVirtualBala = paymentInvoiceDetails.Sum(temp => temp.VirtualBala);

                    //添加付款表
                    resultPayment.PayEmpId = user.EmpId;
                    resultPayment.PayDept = apply.ApplyDept;
                    resultPayment.PayApplyId = payApply.PayApplyId;
                    resultPayment.PayBala = sumPayBala;
                    resultPayment.FundsBala = sumFundsBala;
                    resultPayment.VirtualBala = sumVirtualBala;
                    resultPayment.CurrencyId = payment.CurrencyId;
                    resultPayment.PayStyle = payment.PayStyle;
                    resultPayment.PayBankId = payment.PayBankId;
                    resultPayment.PayBankAccountId = payment.PayBankAccountId;
                    resultPayment.PayCorp = payment.PayCorp;
                    resultPayment.PayDatetime = payment.PayDatetime;
                    resultPayment.RecevableCorp = payment.RecevableCorp;
                    resultPayment.ReceBankId = payment.ReceBankId;
                    resultPayment.ReceBankAccountId = payment.ReceBankAccountId;
                    resultPayment.ReceBankAccount = payment.ReceBankAccount;
                    resultPayment.FlowName = payment.FlowName;
                    resultPayment.Memo = payment.Memo;

                    result = paymentDAL.Update(user, resultPayment);
                    if (result.ResultStatus != 0)
                        return result;

                    //添加付款发票明细表
                    foreach (PaymentInvioceDetail d in paymentInvoiceDetails)
                    {
                        if (d.PayBala > 0)
                        {
                            //获取发票付款申请
                            result = invoicePayApplyDAL.Get(user, d.PayApplyDetailId);
                            if (result.ResultStatus != 0)
                                return result;

                            Model.InvoicePayApply dp = result.ReturnValue as Model.InvoicePayApply;
                            if (dp == null || dp.RefId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "对应付款申请获取失败";
                                return result;
                            }

                            d.DetailStatus = Common.StatusEnum.已生效;
                            d.PaymentId = resultPayment.PaymentId;
                            d.InvoiceId = dp.InvoiceId;
                            d.PayApplyId = dp.PayApplyId;
                            d.PayApplyDetailId = dp.RefId;

                            result = paymentInvoiceDetailDAL.Insert(user, d);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    //虚拟收付款
                    //获取虚拟收付款明细
                    DAL.PaymentVirtualDAL paymentVirtualDAL = new PaymentVirtualDAL();

                    result = paymentVirtualDAL.GetByPaymentId(user, payment.PaymentId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PaymentVirtual paymentVirtual = result.ReturnValue as Model.PaymentVirtual;
                    if (paymentVirtual == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取虚拟收付款信息失败";
                        return result;
                    }
                    if (payment.VirtualBala == 0)
                    {
                        if (paymentVirtual.VirtualId > 0)
                        {
                            //作废原有虚拟收付款
                            result = paymentVirtualDAL.Invalid(user, paymentVirtual);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }
                    else
                    {
                        if (payment.VirtualBala > payment.PayBala)
                        {
                            result.ResultStatus = -1;
                            result.Message = "虚拟付款金额不能大于付款总额";
                            return result;
                        }

                        if (paymentVirtual.VirtualId > 0)
                        {
                            //更新虚拟收付款
                            paymentVirtual.PayBala = payment.VirtualBala;
                            result = paymentVirtualDAL.Update(user, paymentVirtual);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                        else
                        {
                            //新增虚拟收付款
                            paymentVirtual.PayApplyId = resultPayment.PayApplyId;
                            paymentVirtual.PayBala = payment.VirtualBala;
                            paymentVirtual.PaymentId = resultPayment.PaymentId;
                            paymentVirtual.DetailStatus = StatusEnum.已录入;

                            result = paymentVirtualDAL.Insert(user, paymentVirtual);
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

        public ResultModel Audit(UserModel user, NFMT.WorkFlow.Model.DataSource dataSource, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.PayApplyDAL payApplyDAL = new PayApplyDAL();
                NFMT.User.DAL.BlocDAL blocDAL = new User.DAL.BlocDAL();
                DAL.FundsLogDAL fundsLogDAL = new FundsLogDAL();
                NFMT.Data.DAL.BankDAL outBankDAL = new Data.DAL.BankDAL();
                DAL.PaymentVirtualDAL paymentVirtualDAL = new PaymentVirtualDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.paymentDAL.Get(NFMT.Common.DefaultValue.SysUser, dataSource.RowId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Payment payment = result.ReturnValue as Model.Payment;
                    if (payment == null || payment.PaymentId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款不存在";
                        return result;
                    }

                    //审核，修改数据状态
                    result = this.paymentDAL.Audit(user, payment, isPass);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取虚拟付款
                    result = paymentVirtualDAL.GetByPaymentId(user, payment.PaymentId, StatusEnum.已录入);
                    if (result.ResultStatus == 0)
                    {
                        Model.PaymentVirtual paymentVirtual = result.ReturnValue as Model.PaymentVirtual;
                        if (paymentVirtual != null && paymentVirtual.VirtualId > 0)
                        {
                            //审核虚拟付款
                            result = paymentVirtualDAL.Audit(user, paymentVirtual, isPass);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    //审核通过
                    if (isPass)
                    {
                        //获取对方的付款申请
                        result = payApplyDAL.Get(user, payment.PayApplyId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.PayApply payApply = result.ReturnValue as Model.PayApply;
                        if (payApply == null || payApply.PayApplyId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "付款申请不存在";
                            return result;
                        }

                        // 流水操作：写入 流水类型：付款 流水状态：已生效 【仅写入实际付款】
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
                        fundsLog.LogSource = "dbo.Fun_Payment";
                        fundsLog.LogSourceBase = "NFMT";
                        fundsLog.LogStatus = StatusEnum.已生效;
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

                        //更新付款表的资金流水序号
                        payment.FundsLogId = fundsLogId;
                        payment.PaymentStatus = StatusEnum.已生效;
                        result = this.paymentDAL.Update(user, payment);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
                return result;
            }

            return result;
        }

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, DateTime fromDate, DateTime toDate, int empId, int corpId, string status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pay.PaymentId desc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;
            int readyStatus = (int)Common.StatusEnum.已生效;
            int payMode = (int)Data.StyleEnum.付款方式;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" pay.PaymentId,pa.PayApplyId,app.ApplyId,pay.PaymentCode,app.ApplyNo,pay.RecevableCorp,recCorp.CorpName as RecevableCorpName,pay.PayStyle,ps.DetailName as PayStyleName,pay.PayBala,pay.CurrencyId,cur.CurrencyName,pay.PayDatetime,pay.PayEmpId,emp.Name as PayEmpName,pay.PaymentStatus,sd.StatusName as PaymentStatusName,pa.PayApplySource ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_Payment pay ");
            sb.Append(" inner join dbo.Fun_PayApply pa on pay.PayApplyId = pa.PayApplyId ");
            sb.AppendFormat(" inner join dbo.Apply app on pa.ApplyId = app.ApplyId and app.ApplyStatus >= {0} ", readyStatus);
            sb.Append(" inner join NFMT_User.dbo.Corporation recCorp on recCorp.CorpId = pay.RecevableCorp ");
            sb.AppendFormat(" inner join NFMT_Basic.dbo.BDStyleDetail ps on ps.StyleDetailId = pay.PayStyle and ps.BDStyleId ={0} ", payMode);
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = pay.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Employee emp on emp.EmpId = pay.PayEmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = pay.PaymentStatus and sd.StatusId={0} ", statusId);
            sb.Append(" inner join dbo.Fun_PaymentContractDetail pcd on pcd.PaymentId = pay.PaymentId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" exists (select sl.StockLogId from dbo.St_StockLog sl where sl.SubContractId = pcd.ContractSubId and sl.LogStatus <> {0} and not exists(select 1 from dbo.Fun_PaymentStockDetail detail where detail.StockLogId = sl.StockLogId and detail.SubId = pcd.ContractSubId and detail.DetailStatus <> {0}))  ", (int)Common.StatusEnum.已作废);

            if (!string.IsNullOrEmpty(status))
                sb.AppendFormat(" and pay.PaymentStatus in ({0}) ", status);
            if (empId > 0)
                sb.AppendFormat(" and pay.PayEmpId ={0} ", empId);
            if (corpId > 0)
                sb.AppendFormat(" and pay.RecevableCorp = {0} ", corpId);
            if (fromDate > Common.DefaultValue.DefaultTime && toDate >= fromDate)
                sb.AppendFormat(" and pay.PayDatetime between '{0}' and '{1}' ", fromDate.ToString(), toDate.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion

        #region report

        public SelectModel GetPayReportSelect(int pageIndex, int pageSize, string orderStr, int payApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pay.PaymentId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)Common.StatusEnum.已生效;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            select.ColumnName = " pay.PaymentId,pay.PayDatetime,pay.FundsPayBala,ISNULL(pv.PayBala,0) as VirtualBala,cur.CurrencyName,corp.CorpName,bank.BankName,ba.AccountNo,bdPayMode.DetailName ";

            sb.Clear();
            sb.Append(" NFMT..Fun_Payment pay ");
            sb.AppendFormat(" left join NFMT..Fun_PaymentVirtual pv on pay.PaymentId = pv.PaymentId and pv.DetailStatus >={0} ", readyStatus);
            sb.Append(" left join NFMT_Basic..Currency cur on cur.CurrencyId = pay.CurrencyId ");
            sb.Append(" left join NFMT_User..Corporation corp on corp.CorpId = pay.PayCorp ");
            sb.Append(" left join NFMT_Basic..Bank bank on bank.BankId = pay.PayBankId ");
            sb.Append(" left join NFMT_Basic..BankAccount ba on ba.BankAccId = pay.PayBankAccountId ");
            sb.AppendFormat(" left join NFMT_Basic..BDStyleDetail bdPayMode on bdPayMode.StyleDetailId = pay.PayStyle and bdPayMode.BDStyleId = {0} ", (int)NFMT.Data.StyleEnum.PayMode);

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" pay.PayApplyId = {0} and pay.PaymentStatus >={1} ", payApplyId, readyStatus);
            select.WhereStr = sb.ToString();

            return select;
        }

        //public override string[,] SetExcelRangeData(DataTable source)
        //{
        //    string[,] objData = new string[source.Rows.Count, 8];

        //    for (int i = 0; i < source.Rows.Count; i++)
        //    {
        //        System.Data.DataRow dr = source.Rows[i];

        //        objData[i, 0] = ((DateTime)dr["CustomsDate"]).ToString("yyyy-MM-dd");
        //        objData[i, 1] = dr["RefNo"].ToString();
        //        objData[i, 2] = dr["GrossWeight"].ToString();
        //        objData[i, 3] = dr["NetWeight"].ToString();
        //        objData[i, 4] = dr["MUName"].ToString();
        //        objData[i, 5] = dr["CorpName"].ToString();
        //        objData[i, 6] = dr["CurrencyName"].ToString();
        //        objData[i, 7] = dr["CustomsPrice"].ToString();
        //    }

        //    return objData;
        //}

        #endregion
    }
}
