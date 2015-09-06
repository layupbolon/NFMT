/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PayApplyBLL.cs
// 文件功能描述：付款申请dbo.Fun_PayApply业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月12日
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using log4net;
using NFMT.Common;
using NFMT.Contract;
using NFMT.Contract.DAL;
using NFMT.Contract.Model;
using NFMT.Data;
using NFMT.Funds.DAL;
using NFMT.Funds.Model;
using NFMT.Invoice.DAL;
using NFMT.Invoice.Model;
using NFMT.Operate;
using NFMT.Operate.DAL;
using NFMT.Operate.Model;
using NFMT.User;
using NFMT.User.Model;
using NFMT.WareHouse.DAL;
using NFMT.WareHouse.Model;
using NFMT.WorkFlow.DAL;

namespace NFMT.Funds.BLL
{
    /// <summary>
    /// 付款申请dbo.Fun_PayApply业务逻辑类。
    /// </summary>
    public class PayApplyBLL : ApplyBLL
    {
        private PayApplyDAL payapplyDAL = new PayApplyDAL();
        private ILog log = LogManager.GetLogger(typeof(PayApplyDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PayApplyBLL()
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
            get { return this.payapplyDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, DateTime applyTimeBegin, DateTime applyTimeEnd, int status, int applyDeptId, int recCorpId)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pa.PayApplyId desc";
            else
                select.OrderStr = orderStr;

            int commonStatusType = (int)StatusTypeEnum.通用状态;
            int entryStatus = (int)StatusEnum.已录入;
            int payMatterStyle = (int)StyleEnum.PayMatter;
            int payModeStyle = (int)StyleEnum.PayMode;

            StringBuilder sb = new StringBuilder();
            sb.Append("pa.PayApplyId,app.ApplyId,app.ApplyNo,pa.PayApplySource,pa.PayMatter,pma.DetailName as PayMatterName,pa.PayMode,pm.DetailName as PayModeName, pa.RecCorpId,recCor.CorpName as RecCorpName,pa.ApplyBala,pa.CurrencyId,cur.CurrencyName,app.ApplyDept,dep.DeptName,app.ApplyTime,app.ApplyStatus,sd.StatusName,app.EmpId,emp.Name,pay.SumPayBala, CAST(ISNULL(pay.SumPayBala,0) as varchar) + cur.CurrencyName as PayBala");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_PayApply pa ");
            sb.Append(" inner join dbo.Apply app on app.ApplyId = pa.ApplyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail pma on pma.StyleDetailId = pa.PayMatter and pma.BDStyleId={0} ", payMatterStyle);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail pm on pm.StyleDetailId = pa.PayMode and pm.BDStyleId = {0} ", payModeStyle);
            sb.Append(" left join NFMT_User.dbo.Corporation recCor on recCor.CorpId = pa.RecCorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = pa.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Department dep on dep.DeptId = app.ApplyDept ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = app.ApplyStatus and sd.StatusId={0} ", commonStatusType);
            sb.Append(" left join NFMT_User.dbo.Employee emp on emp.EmpId= app.EmpId ");

            sb.AppendFormat("left join (select SUM(isnull(PayBala,0)) as SumPayBala,PayApplyId from dbo.Fun_Payment where PaymentStatus >={0} ", entryStatus);
            sb.Append(" group by PayApplyId) as pay on pay.PayApplyId = pa.PayApplyId");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" 1= 1 ");
            if (status > 0)
                sb.AppendFormat(" and app.ApplyStatus = {0} ", status);
            if (applyDeptId > 0)
                sb.AppendFormat(" and app.ApplyDept ={0} ", applyDeptId);
            if (recCorpId > 0)
                sb.AppendFormat(" and pa.RecCorpId={0} ", recCorpId);
            if (applyTimeBegin > DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and app.ApplyTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays
                    (1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetContractListSelect(int pageIndex, int pageSize, string orderStr, string subNo, int outCorpId, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "soa.StockOutApplyId asc";
            else
                select.OrderStr = orderStr;

            int status = (int)StatusEnum.已生效;
            int statusType = (int)StatusTypeEnum.通用状态;

            StringBuilder sb = new StringBuilder();
            sb.Append(" con.ContractId,cs.SubId,cs.ContractDate,con.ContractNo,cs.SubNo,inccd.CorpName as InCorpName");
            sb.Append(",outccd.CorpId,outccd.CorpName as OutCorpName,a.AssetName,cs.SubStatus,sd.StatusName,pa.ApplyBala,cur.CurrencyName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Con_ContractSub cs ");
            sb.Append(" left join dbo.Con_Contract con on con.ContractId = cs.ContractId ");
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail inccd on inccd.ContractId = con.ContractId and inccd.IsDefaultCorp = 1 and inccd.IsInnerCorp = 1 and inccd.DetailStatus= {0} ", status);
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail outccd on outccd.ContractId = con.ContractId and outccd.IsDefaultCorp = 1 and outccd.IsInnerCorp = 0 and outccd.DetailStatus={0} ", status);
            sb.Append(" left join NFMT_Basic.dbo.Asset a on con.AssetId = a.AssetId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on cs.SubStatus = sd.DetailId and sd.StatusId = {0} ", statusType);


            sb.Append(" left join (select SUM(pa.ApplyBala) as ApplyBala,cpa.ContractSubId,pa.CurrencyId from NFMT.dbo.Fun_PayApply pa inner join NFMT.dbo.Apply app on app.ApplyId = pa.ApplyId ");
            sb.Append(" inner join NFMT.dbo.Fun_ContractPayApply_Ref cpa on cpa.PayApplyId = pa.PayApplyId ");
            sb.AppendFormat(" where app.ApplyStatus >={0} group by cpa.ContractSubId,pa.CurrencyId) as pa on pa.ContractSubId = cs.SubId ", status);
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = pa.CurrencyId ");

            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on cs.UnitId = mu.MUId ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" con.ContractStatus = {0} and cs.SubStatus = {0} and con.TradeDirection = {1} ", status, (int)TradeDirectionEnum.采购);

            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and cs.SubNo like '%{0}%'", subNo);
            if (outCorpId > 0)
                sb.AppendFormat("  and outccd.CorpId ={0}", outCorpId);
            if (applyTimeBegin > DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and cs.CreateTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays
                    (1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel PayApplyContractCreate(UserModel user, PayApply payApply, int subId, string memo, int deptId, int corpId)
        {
            ResultModel result = new ResultModel();

            try
            {
                ContractSubDAL subDAL = new ContractSubDAL();
                ApplyDAL applyDAL = new ApplyDAL();
                PayApplyDAL payApplyDAL = new PayApplyDAL();
                ContractPayApplyDAL contractPayApplyDAL = new ContractPayApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证子合约信息
                    result = subDAL.Get(user, subId);
                    if (result.ResultStatus != 0)
                        return result;
                    ContractSub sub = result.ReturnValue as ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约不存在";
                        return result;
                    }

                    //验证可申请金额
                    //NFMT.Data.Model.FuturesPrice futuresPrice = NFMT.Data.BasicDataProvider.FuturesPrices[0];

                    //新增申请主表
                    Department dept = UserProvider.Departments.FirstOrDefault(temp => temp.DeptId == deptId);
                    if (dept == null || dept.DeptId < 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请部门不存在";
                        return result;
                    }

                    int applyId = 0;
                    Apply apply = new Apply();

                    apply.ApplyDept = dept.DeptId;
                    apply.ApplyCorp = corpId;
                    apply.ApplyDesc = memo;
                    apply.ApplyTime = DateTime.Now;
                    apply.ApplyType = ApplyType.付款申请;
                    apply.EmpId = user.EmpId;

                    result = applyDAL.Insert(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out applyId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请主表新增失败";
                        return result;
                    }

                    //新增付款申请表
                    payApply.ApplyId = applyId;
                    payApply.PayApplySource = (int)FundsStyleEnum.ContractPayApply;
                    result = payapplyDAL.Insert(user, payApply);
                    if (result.ResultStatus != 0)
                        return result;
                    int payApplyId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out payApplyId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请新增失败";
                        return result;
                    }

                    //新增Fun_ContractPayApply_Ref合约付款申请
                    ContractPayApply contractPayApply = new ContractPayApply();
                    contractPayApply.ContractId = sub.ContractId;
                    contractPayApply.ContractSubId = sub.SubId;
                    contractPayApply.PayApplyId = payApplyId;
                    contractPayApply.ApplyBala = payApply.ApplyBala;

                    result = contractPayApplyDAL.Insert(user, contractPayApply);
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

        public ResultModel PayApplyContractUpdate(UserModel user, PayApply payApply, string memo, int deptId, int corpId)
        {
            ResultModel result = new ResultModel();

            try
            {
                ContractSubDAL subDAL = new ContractSubDAL();
                ApplyDAL applyDAL = new ApplyDAL();
                PayApplyDAL payApplyDAL = new PayApplyDAL();
                ContractPayApplyDAL contractPayApplyDAL = new ContractPayApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证付款申请
                    result = payApplyDAL.Get(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    PayApply resultObj = result.ReturnValue as PayApply;
                    if (resultObj == null || resultObj.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请不存在";
                        return result;
                    }

                    //验证申请主表
                    result = applyDAL.Get(user, resultObj.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    Apply apply = result.ReturnValue as Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请主表不存在";
                        return result;
                    }

                    //验证合约付款申请关联表
                    result = contractPayApplyDAL.GetByPayApplyId(user, resultObj.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    ContractPayApply contractPayApply = result.ReturnValue as ContractPayApply;
                    if (contractPayApply == null || contractPayApply.RefId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "与合约关联错误";
                        return result;
                    }

                    //验证子合约信息
                    result = subDAL.Get(user, contractPayApply.ContractSubId);
                    if (result.ResultStatus != 0)
                        return result;
                    ContractSub sub = result.ReturnValue as ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约不存在";
                        return result;
                    }

                    //验证可申请金额
                    //NFMT.Data.Model.FuturesPrice futuresPrice = NFMT.Data.BasicDataProvider.FuturesPrices[0];

                    //更新申请主表
                    Department dept = UserProvider.Departments.FirstOrDefault(temp => temp.DeptId == deptId);
                    if (dept == null || dept.DeptId < 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请部门不存在";
                        return result;
                    }

                    apply.ApplyDept = dept.DeptId;
                    apply.ApplyCorp = corpId;
                    apply.ApplyDesc = memo;
                    apply.ApplyType = ApplyType.付款申请;
                    apply.EmpId = user.EmpId;

                    result = applyDAL.Update(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    //更新付款申请表
                    resultObj.PayApplySource = (int)FundsStyleEnum.ContractPayApply;
                    resultObj.RecCorpId = payApply.RecCorpId;
                    resultObj.RecBankId = payApply.RecBankId;
                    resultObj.RecBankAccountId = payApply.RecBankAccountId;
                    resultObj.RecBankAccount = payApply.RecBankAccount;
                    resultObj.ApplyBala = payApply.ApplyBala;
                    resultObj.CurrencyId = payApply.CurrencyId;
                    resultObj.PayMode = payApply.PayMode;
                    resultObj.PayDeadline = payApply.PayDeadline;
                    resultObj.PayMatter = payApply.PayMatter;
                    resultObj.SpecialDesc = payApply.SpecialDesc;

                    result = payapplyDAL.Update(user, resultObj);
                    if (result.ResultStatus != 0)
                        return result;

                    //更新Fun_ContractPayApply_Ref合约付款申请                    
                    contractPayApply.ApplyBala = payApply.ApplyBala;
                    result = contractPayApplyDAL.Update(user, contractPayApply);
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

        public ResultModel GetByApplyId(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.payapplyDAL.GetByApplyId(user, applyId);
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

        public ResultModel Goback(UserModel user, int payApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                PayApplyDAL payApplyDAL = new PayApplyDAL();
                ApplyDAL applyDAL = new ApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证付款申请
                    result = payApplyDAL.Get(user, payApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    PayApply payApply = result.ReturnValue as PayApply;
                    if (payApply == null || payApply.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, payApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Apply apply = result.ReturnValue as Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请不存在";
                        return result;
                    }

                    //主申请状态修改至已撤返
                    result = applyDAL.Goback(user, apply);

                    if (result.ResultStatus != 0)
                        return result;

                    //工作流任务关闭
                    DataSourceDAL sourceDAL = new DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, apply);
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

        public ResultModel Invalid(UserModel user, int payApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                PayApplyDAL payApplyDAL = new PayApplyDAL();
                ApplyDAL applyDAL = new ApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证付款申请
                    result = payApplyDAL.Get(user, payApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    PayApply payApply = result.ReturnValue as PayApply;
                    if (payApply == null || payApply.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, payApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Apply apply = result.ReturnValue as Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请不存在";
                        return result;
                    }

                    //作废主申请
                    result = applyDAL.Invalid(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    //如果是库存付款申请，同时作废出库付款申请明细
                    if (payApply.PayApplySource == (int)FundsStyleEnum.StockPayApply)
                    {
                        StockPayApplyDAL stockPayApplyDAL = new StockPayApplyDAL();
                        result = stockPayApplyDAL.Load(user, payApply.PayApplyId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<StockPayApply> stockPayApplies = result.ReturnValue as List<StockPayApply>;
                        if (stockPayApplies == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存付款明细获取错误";
                            return result;
                        }

                        foreach (StockPayApply stockPayApply in stockPayApplies)
                        {
                            //作废明细
                            if (stockPayApply.Status == StatusEnum.已生效)
                                stockPayApply.Status = StatusEnum.已录入;

                            result = stockPayApplyDAL.Invalid(user, stockPayApply);
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

        public ResultModel Confirm(UserModel user, int payApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                PayApplyDAL payApplyDAL = new PayApplyDAL();
                ApplyDAL applyDAL = new ApplyDAL();
                PaymentDAL paymentDAL = new PaymentDAL();
                StockPayApplyDAL payApplyStockDAL = new StockPayApplyDAL();
                ContractPayApplyDAL payApplyContractDAL = new ContractPayApplyDAL();
                PaymentStockDetailDAL paymentStockDetailDAL = new PaymentStockDetailDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证付款申请
                    result = payApplyDAL.Get(user, payApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    PayApply payApply = result.ReturnValue as PayApply;
                    if (payApply == null || payApply.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, payApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Apply apply = result.ReturnValue as Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请不存在";
                        return result;
                    }

                    //验证当前付款申请下是否有已录入至已生效状态下的关联付款
                    //如果存在，则不能进行执行完成确认
                    result = paymentDAL.GetCountByPayApplyId(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result == null || result.AffectCount > 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请存在未完成的关联付款，不能进行执行完成确认";
                        return result;
                    }

                    //验证当前付款申请的申请金额与已执行完成的关联付款的付款金额是否相等
                    //如果不相等则不能进行执行完成确认
                    result = paymentDAL.Load(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Payment> payments = result.ReturnValue as List<Payment>;
                    if (payments == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取关联付款失败";
                        return result;
                    }

                    decimal sumPayBala = payments.Sum(temp => temp.PayBala);
                    if (sumPayBala != payApply.ApplyBala)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请金额与付款执行金额不相等，不能进行付款申请执行完成确认";
                        return result;
                    }

                    //验证库存付款申请明细的申请金额与付款执行的付款金额是否相等
                    //如果不相等则不能进行执行完成确认
                    if (payApply.PayApplySource == (int)FundsStyleEnum.ContractPayApply)
                    {
                        //获取付款申请下合约申请明细
                        result = payApplyContractDAL.GetByPayApplyId(user, payApply.PayApplyId);
                        if (result.ResultStatus != 0)
                            return result;
                        ContractPayApply payApplyContract = result.ReturnValue as ContractPayApply;
                        if (payApplyContract == null || payApplyContract.RefId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取明细失败";
                            return result;
                        }

                        //完成明细
                        //result = payApplyContractDAL.Confirm(user, payApplyContract);
                        //if (result.ResultStatus != 0)
                        //    return result;
                    }
                    else if (payApply.PayApplySource == (int)FundsStyleEnum.StockPayApply)
                    {
                        //获取付款申请下库存申请明细

                        result = payApplyStockDAL.Load(user, payApply.PayApplyId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<StockPayApply> payApplyStocks = result.ReturnValue as List<StockPayApply>;
                        if (payApplyStocks == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取付款申请库存明细失败";
                            return result;
                        }

                        foreach (StockPayApply payApplyStock in payApplyStocks)
                        {
                            //获取付款申请库存明细关联的付款库存明细
                            result = paymentStockDetailDAL.LoadByStockPayApplyId(user, payApplyStock.RefId);
                            if (result.ResultStatus != 0)
                                return result;

                            List<PaymentStockDetail> details = result.ReturnValue as List<PaymentStockDetail>;

                            decimal sumStockPayBala = details.Sum(temp => temp.PayBala);

                            if (sumStockPayBala != payApplyStock.ApplyBala)
                            {
                                result.ResultStatus = -1;
                                result.Message = "付款申请存在未付完的金额，不能进行执行确认完成";
                                return result;
                            }

                            //明细完成
                            result = payApplyStockDAL.Confirm(user, payApplyStock);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    //付款申请完成确认
                    //因付款执行不进行部分关闭，因此不存在部分完成状态
                    result = applyDAL.Confirm(user, apply);
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

        public ResultModel ConfirmCancel(UserModel user, int payApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                PayApplyDAL payApplyDAL = new PayApplyDAL();
                ApplyDAL applyDAL = new ApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证付款申请
                    result = payApplyDAL.Get(user, payApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    PayApply payApply = result.ReturnValue as PayApply;
                    if (payApply == null || payApply.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, payApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Apply apply = result.ReturnValue as Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请不存在";
                        return result;
                    }

                    //完成撤销付款申请
                    result = applyDAL.ConfirmCancel(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    if (payApply.PayApplySource == (int)FundsStyleEnum.ContractPayApply)
                    {
                        //获取付款申请下合约申请明细
                        ContractPayApplyDAL payApplyContractDAL = new ContractPayApplyDAL();

                        result = payApplyContractDAL.GetByPayApplyId(user, payApply.PayApplyId);
                        if (result.ResultStatus != 0)
                            return result;
                        ContractPayApply payApplyContract = result.ReturnValue as ContractPayApply;
                        if (payApplyContract == null || payApplyContract.RefId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取明细失败";
                            return result;
                        }

                        //完成撤销明细
                        //result = payApplyContractDAL.ConfirmCancel(user, payApplyContract);
                        //if (result.ResultStatus != 0)
                        //    return result;
                    }
                    else if (payApply.PayApplySource == (int)FundsStyleEnum.StockPayApply)
                    {
                        //获取付款申请下库存申请明细
                        StockPayApplyDAL payApplyStockDAL = new StockPayApplyDAL();
                        result = payApplyStockDAL.Load(user, payApply.PayApplyId, StatusEnum.已完成);
                        if (result.ResultStatus != 0)
                            return result;

                        List<StockPayApply> payApplyStocks = result.ReturnValue as List<StockPayApply>;
                        if (payApplyStocks == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取付款申请库存明细失败";
                            return result;
                        }

                        foreach (StockPayApply payApplyStock in payApplyStocks)
                        {
                            //明细完成撤销
                            result = payApplyStockDAL.ConfirmCancel(user, payApplyStock);
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

        public ResultModel Close(UserModel user, int payApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                PayApplyDAL payApplyDAL = new PayApplyDAL();
                ApplyDAL applyDAL = new ApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证付款申请
                    result = payApplyDAL.Get(user, payApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    PayApply payApply = result.ReturnValue as PayApply;
                    if (payApply == null || payApply.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, payApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Apply apply = result.ReturnValue as Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请不存在";
                        return result;
                    }

                    //关闭主申请
                    result = applyDAL.Close(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    //关闭明细申请

                    if (payApply.PayApplySource == (int)FundsStyleEnum.ContractPayApply)
                    {
                        //获取付款申请下合约申请明细
                        ContractPayApplyDAL payApplyContractDAL = new ContractPayApplyDAL();

                        result = payApplyContractDAL.GetByPayApplyId(user, payApply.PayApplyId);
                        if (result.ResultStatus != 0)
                            return result;
                        ContractPayApply payApplyContract = result.ReturnValue as ContractPayApply;
                        if (payApplyContract == null || payApplyContract.RefId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取明细失败";
                            return result;
                        }

                        //完成撤销明细
                        result = payApplyContractDAL.Close(user, payApplyContract);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                    else if (payApply.PayApplySource == (int)FundsStyleEnum.StockPayApply)
                    {
                        //获取付款申请下库存申请明细
                        StockPayApplyDAL payApplyStockDAL = new StockPayApplyDAL();
                        result = payApplyStockDAL.Load(user, payApply.PayApplyId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<StockPayApply> payApplyStocks = result.ReturnValue as List<StockPayApply>;
                        if (payApplyStocks == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取付款申请库存明细失败";
                            return result;
                        }

                        foreach (StockPayApply payApplyStock in payApplyStocks)
                        {
                            //明细完成撤销
                            result = payApplyStockDAL.Close(user, payApplyStock);
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

        public SelectModel GetStockListSelect(int pageIndex, int pageSize, string orderStr, string sids, int corpId = 0)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            int stockStatusType = (int)StatusTypeEnum.库存状态;
            int readyStatus = (int)StatusEnum.已生效;

            int inLogDirection = DetailProvider.Details(StyleEnum.LogDirection)["In"].StyleDetailId;
            int CancelStockLogType = DetailProvider.Details(StyleEnum.LogType)["Reverse"].StyleDetailId;

            StringBuilder sb = new StringBuilder();
            sb.Append("sto.StockId,sl.StockLogId,sn.StockNameId,ass.AssetId,bra.BrandId,cor.CorpId,mu.MUId,sto.StockDate,sn.RefNo,sl.GrossAmount,CAST(sl.GrossAmount as varchar) + mu.MUName as StockWeight,cor.CorpName,ass.AssetName,bra.BrandName,sto.StockStatus,sd.StatusName as StockStatusName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockLog sl ");
            sb.Append(" inner join St_Stock sto on sto.StockId = sl.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" inner join dbo.Con_ContractSub cs on sl.SubContractId = cs.SubId and cs.SubStatus = {0} ", readyStatus);
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sto.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_User.dbo.Corporation cor on sto.CorpId = cor.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on sto.BrandId = bra.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on sto.UintId = mu.MUId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sto.StockStatus = sd.DetailId and sd.StatusId={0} ", stockStatusType);
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" sl.LogType != {0} ", CancelStockLogType);
            sb.AppendFormat(" and sl.StockLogId not in (select StockLogId from dbo.Fun_StockPayApply_Ref where RefStatus >= {0}) ", readyStatus);
            if (corpId > 0)
                sb.AppendFormat(" and sto.CorpId = {0} ", corpId);
            if (!string.IsNullOrEmpty(sids))
                sb.AppendFormat(" and sto.StockId not in ({0}) ", sids);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel PayApplyStockCreate(UserModel user, PayApply payApply, List<StockPayApply> stockPayApplies, string memo, int deptId, int corpId)
        {
            ResultModel result = new ResultModel();

            try
            {
                StockPayApplyDAL stockPayApplyDAL = new StockPayApplyDAL();
                ApplyDAL applyDAL = new ApplyDAL();
                PayApplyDAL payApplyDAL = new PayApplyDAL();
                ContractPayApplyDAL contractPayApplyDAL = new ContractPayApplyDAL();

                StockLogDAL stockLogDAL = new StockLogDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证可申请金额
                    //NFMT.Data.Model.FuturesPrice futuresPrice = NFMT.Data.BasicDataProvider.FuturesPrices[0];

                    //验证库存流水是否在同一子合约中
                    int subId = 0;
                    int contractId = 0;
                    foreach (StockPayApply stockPayApply in stockPayApplies)
                    {
                        result = stockLogDAL.Get(user, stockPayApply.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        StockLog stockLog = result.ReturnValue as StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存流水不存在";
                            return result;
                        }

                        if (subId == 0)
                        {
                            subId = stockLog.SubContractId;
                            contractId = stockLog.ContractId;
                        }

                        if (stockLog.SubContractId != subId)
                        {
                            result.ResultStatus = -1;
                            result.Message = "不能同时申请不同子合约中的库存付款";
                            return result;
                        }
                    }

                    //新增申请主表
                    Department dept = UserProvider.Departments.FirstOrDefault(temp => temp.DeptId == deptId);
                    if (dept == null || dept.DeptId < 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请部门不存在";
                        return result;
                    }

                    int applyId = 0;
                    Apply apply = new Apply();

                    apply.ApplyDept = dept.DeptId;
                    apply.ApplyCorp = corpId;
                    apply.ApplyDesc = memo;
                    apply.ApplyTime = DateTime.Now;
                    apply.ApplyType = ApplyType.付款申请;
                    apply.EmpId = user.EmpId;

                    result = applyDAL.Insert(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out applyId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请主表新增失败";
                        return result;
                    }

                    decimal sumApplyBala = stockPayApplies.Sum(temp => temp.ApplyBala);
                    //新增付款申请表
                    payApply.ApplyId = applyId;
                    payApply.PayApplySource = (int)FundsStyleEnum.StockPayApply;
                    payApply.ApplyBala = sumApplyBala;
                    result = payapplyDAL.Insert(user, payApply);
                    if (result.ResultStatus != 0)
                        return result;
                    int payApplyId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out payApplyId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请新增失败";
                        return result;
                    }

                    //新增合约付款申请关联表
                    ContractPayApply payApplyContract = new ContractPayApply();
                    payApplyContract.ApplyBala = sumApplyBala;
                    payApplyContract.ContractId = contractId;
                    payApplyContract.ContractSubId = subId;
                    payApplyContract.PayApplyId = payApplyId;

                    result = contractPayApplyDAL.Insert(user, payApplyContract);
                    if (result.ResultStatus != 0)
                        return result;

                    int contractRefId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out contractRefId) || contractRefId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "新增付款申请失败";
                        return result;
                    }

                    //新增dbo.Fun_StockPayApply_Ref库存付款申请                    
                    foreach (StockPayApply stockPayApply in stockPayApplies)
                    {
                        stockPayApply.ContractId = contractId;
                        stockPayApply.ContractRefId = contractRefId;
                        stockPayApply.SubId = subId;
                        stockPayApply.PayApplyId = payApplyId;
                        stockPayApply.RefStatus = StatusEnum.已生效;
                        result = stockPayApplyDAL.Insert(user, stockPayApply);
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

        public SelectModel GetSelectedSelect(int pageIndex, int pageSize, string orderStr, int payApplyId)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId desc";
            else
                select.OrderStr = orderStr;

            int stockStatusType = (int)StatusTypeEnum.库存状态;
            int readyStatus = (int)StatusEnum.已生效;

            int inLogDirection = DetailProvider.Details(StyleEnum.LogDirection)["In"].StyleDetailId;
            int CancelStockLogType = DetailProvider.Details(StyleEnum.LogType)["Reverse"].StyleDetailId;

            StringBuilder sb = new StringBuilder();
            sb.Append("sto.StockId,sl.StockLogId,spa.RefId,sn.StockNameId,ass.AssetId,bra.BrandId,cor.CorpId,mu.MUId,sto.StockDate,sn.RefNo,sto.GrossAmount,CAST(sto.GrossAmount as varchar) + mu.MUName as StockWeight,cor.CorpName,ass.AssetName,bra.BrandName,sto.StockStatus,sd.StatusName as StockStatusName,spa.ApplyBala,0 as PayBala");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" St_Stock sto ");
            sb.AppendFormat(" inner join dbo.Fun_StockPayApply_Ref spa on spa.StockId = sto.StockId and spa.PayApplyId={0} and spa.RefStatus>={1} ", payApplyId, readyStatus);
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" left join dbo.St_StockLog sl on sto.StockId = sl.StockId and sl.LogDirection = {0} ", inLogDirection);
            //PayBala
            //sb.AppendFormat(" left join dbo.Fun_PaymentStockDetail psd on spa.RefId = psd.PayApplyDetailId and psd.DetailStatus >= {0} ", readyStatus);
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sto.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_User.dbo.Corporation cor on sto.CorpId = cor.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on sto.BrandId = bra.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on sto.UintId = mu.MUId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sto.StockStatus = sd.DetailId and sd.StatusId={0} ", stockStatusType);
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" sl.StockLogId not in (select StockLogId from dbo.St_StockLog where LogType = {0}) ", CancelStockLogType);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetPaymentStockCreateSelect(int pageIndex, int pageSize, string orderStr, int payApplyId, int paymentId = 0)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId desc";
            else
                select.OrderStr = orderStr;

            int stockStatusType = (int)StatusTypeEnum.库存状态;
            int readyStatus = (int)StatusEnum.已生效;

            int inLogDirection = DetailProvider.Details(StyleEnum.LogDirection)["In"].StyleDetailId;
            int CancelStockLogType = DetailProvider.Details(StyleEnum.LogType)["Reverse"].StyleDetailId;

            StringBuilder sb = new StringBuilder();
            sb.Append("sto.StockId,sl.StockLogId,spa.RefId as PayApplyDetailId,sn.StockNameId,ass.AssetId,bra.BrandId,cor.CorpId");
            sb.Append(",mu.MUId,sto.StockDate,sn.RefNo,sl.NetAmount,CAST(sl.NetAmount as varchar) + mu.MUName as StockWeight");
            sb.Append(",cor.CorpName,ass.AssetName,bra.BrandName,sto.StockStatus,sd.StatusName as StockStatusName,spa.ApplyBala");
            sb.Append(",psd.PayedBala,ISNULL(spa.ApplyBala,0) - ISNULL(psd.PayedBala,0) as LastBala");
            sb.Append(",isnull(psd1.FundsBala,ISNULL(spa.ApplyBala,0) - ISNULL(psd.PayedBala,0)) as FundsBala,ISNULL(psd1.VirtualBala,0) as VirtualBala ");
            sb.Append(",sto.CardNo,dp.DPName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_StockPayApply_Ref spa ");
            sb.Append(" inner join St_Stock sto on spa.StockId = sto.StockId");
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" left join dbo.St_StockLog sl on sto.StockId = sl.StockId and sl.LogDirection = {0} ", inLogDirection);
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sto.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_User.dbo.Corporation cor on sto.CorpId = cor.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on sto.BrandId = bra.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on sto.UintId = mu.MUId ");

            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sto.StockStatus = sd.DetailId and sd.StatusId={0} ", stockStatusType);
            sb.Append(" left join (select PayApplyDetailId,isnull(SUM(PayBala),0) as PayedBala ");
            sb.AppendFormat(" from dbo.Fun_PaymentStockDetail where DetailStatus>={0} and PaymentId !={1} group by PayApplyDetailId) ", readyStatus, paymentId);
            sb.Append(" as psd on psd.PayApplyDetailId = spa.RefId ");

            sb.Append(" left join (select PayApplyDetailId,SUM(FundsBala) as FundsBala,SUM(VirtualBala) as VirtualBala from dbo.Fun_PaymentStockDetail ");
            sb.AppendFormat(" where DetailStatus>={0} and PaymentId ={1} ", readyStatus, paymentId);
            sb.Append(" group by PayApplyDetailId) as psd1 on psd1.PayApplyDetailId = spa.RefId ");

            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" spa.PayApplyId={0} and spa.RefStatus>={1} ", payApplyId, readyStatus);
            sb.Append(" and ISNULL(spa.ApplyBala,0) - ISNULL(psd.PayedBala,0)>0 ");
            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel PayApplyStockUpdate(UserModel user, PayApply payApply, List<StockPayApply> stockPayApplies, string memo, int deptId, int corpId)
        {
            ResultModel result = new ResultModel();
            try
            {
                StockPayApplyDAL stockPayApplyDAL = new StockPayApplyDAL();
                ContractPayApplyDAL contractPayApplyDAL = new ContractPayApplyDAL();
                ApplyDAL applyDAL = new ApplyDAL();
                PayApplyDAL payApplyDAL = new PayApplyDAL();
                StockLogDAL stockLogDAL = new StockLogDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证可申请金额
                    //NFMT.Data.Model.FuturesPrice futuresPrice = NFMT.Data.BasicDataProvider.FuturesPrices[0];

                    //获取付款申请
                    result = payapplyDAL.Get(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    PayApply resultObj = result.ReturnValue as PayApply;
                    if (resultObj == null || resultObj.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请不存在";
                        return result;
                    }

                    //获取申请主表
                    result = applyDAL.Get(user, resultObj.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Apply apply = result.ReturnValue as Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请主表获取失败";
                        return result;
                    }

                    //获取库存明细列表
                    result = stockPayApplyDAL.Load(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<StockPayApply> readyStockPayApplies = result.ReturnValue as List<StockPayApply>;
                    if (readyStockPayApplies == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "库存申请明细获取失败";
                        return result;
                    }

                    //更新申请主表
                    Department dept = UserProvider.Departments.FirstOrDefault(temp => temp.DeptId == deptId);
                    if (dept == null || dept.DeptId < 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请部门不存在";
                        return result;
                    }

                    apply.ApplyDept = dept.DeptId;
                    apply.ApplyCorp = corpId;
                    apply.ApplyDesc = memo;
                    apply.ApplyType = ApplyType.付款申请;
                    apply.EmpId = user.EmpId;

                    result = applyDAL.Update(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    //更新付款申请表
                    resultObj.RecCorpId = payApply.RecCorpId;
                    resultObj.RecBankId = payApply.RecBankId;
                    resultObj.RecBankAccountId = payApply.RecBankAccountId;
                    resultObj.RecBankAccount = payApply.RecBankAccount;
                    resultObj.ApplyBala = payApply.ApplyBala;
                    resultObj.CurrencyId = payApply.CurrencyId;
                    resultObj.PayMode = payApply.PayMode;
                    resultObj.PayDeadline = payApply.PayDeadline;
                    resultObj.PayMatter = payApply.PayMatter;
                    resultObj.SpecialDesc = payApply.SpecialDesc;

                    result = payapplyDAL.Update(user, resultObj);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废现有已生效库存分配明细
                    foreach (StockPayApply stockPayApply in readyStockPayApplies)
                    {
                        stockPayApply.RefStatus = StatusEnum.已录入;
                        result = stockPayApplyDAL.Invalid(user, stockPayApply);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取合约付款申请
                    result = contractPayApplyDAL.GetByPayApplyId(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    ContractPayApply contractPayApply = result.ReturnValue as ContractPayApply;
                    if (contractPayApply == null || contractPayApply.RefId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取对应合约付款失败";
                        return result;
                    }

                    //验证库存流水是否在同一子合约中
                    int subId = 0;
                    int contractId = 0;
                    foreach (StockPayApply stockPayApply in stockPayApplies)
                    {
                        result = stockLogDAL.Get(user, stockPayApply.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        StockLog stockLog = result.ReturnValue as StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存流水不存在";
                            return result;
                        }

                        if (subId == 0)
                        {
                            subId = stockLog.SubContractId;
                            contractId = stockLog.ContractId;
                        }

                        if (stockLog.SubContractId != subId)
                        {
                            result.ResultStatus = -1;
                            result.Message = "不能同时申请不同子合约中的库存付款";
                            return result;
                        }
                    }

                    //更新合约付款
                    contractPayApply.ApplyBala = stockPayApplies.Sum(temp => temp.ApplyBala);
                    contractPayApply.ContractId = contractId;
                    contractPayApply.ContractSubId = subId;

                    result = contractPayApplyDAL.Update(user, contractPayApply);
                    if (result.ResultStatus != 0)
                        return result;

                    //新增dbo.Fun_StockPayApply_Ref库存付款申请                    
                    foreach (StockPayApply stockPayApply in stockPayApplies)
                    {
                        stockPayApply.ContractId = contractPayApply.ContractId;
                        stockPayApply.ContractRefId = contractPayApply.RefId;
                        stockPayApply.SubId = contractPayApply.ContractSubId;
                        stockPayApply.PayApplyId = contractPayApply.PayApplyId;
                        stockPayApply.RefStatus = StatusEnum.已生效;
                        result = stockPayApplyDAL.Insert(user, stockPayApply);
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

        public SelectModel GetInvoiceListSelect(int pageIndex, int pageSize, string orderStr, string ids = "", string refIds = "", int outCorpId = 0, int payDept = 0)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "si.SIId desc";
            else
                select.OrderStr = orderStr;

            if (string.IsNullOrEmpty(refIds))
                refIds = "0";

            int readyStatus = (int)StatusEnum.已生效;

            StringBuilder sb = new StringBuilder();
            sb.Append("si.SIId,inv.InvoiceId,si.PayDept,dept.DeptName,inv.InvoiceDate,inv.InvoiceNo,inv.InvoiceBala,inv.CurrencyId,cur.CurrencyName,inv.OutCorpId,inv.OutCorpName,inv.InCorpId,inv.InCorpName,inv.Memo,ipar.SumBala,ISNULL(inv.InvoiceBala,0) - ISNULL(ipar.SumBala,0) as LastBala,ISNULL(inv.InvoiceBala,0) - ISNULL(ipar.SumBala,0) as ApplyBala");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Inv_SI si ");
            sb.Append(" inner join dbo.Invoice inv on si.InvoiceId = inv.InvoiceId ");
            sb.Append(" left join NFMT_User.dbo.Department dept on dept.DeptId = si.PayDept ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = inv.CurrencyId ");
            sb.AppendFormat(" left join (select SUM(ApplyBala) as SumBala,SIId from dbo.Fun_InvoicePayApply_Ref where DetailStatus>={0} and RefId not in ({1}) group by SIId) ", readyStatus, refIds);
            sb.Append(" as ipar on ipar.SIId = si.SIId ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" inv.InvoiceStatus = {0} and ISNULL(inv.InvoiceBala,0) - ISNULL(ipar.SumBala,0)>0 ", readyStatus);
            if (!string.IsNullOrEmpty(ids))
                sb.AppendFormat(" and si.SIId not in ({0}) ", ids);
            if (outCorpId > 0)
                sb.AppendFormat(" and inv.OutCorpId = {0} ", outCorpId);
            if (payDept > 0)
                sb.AppendFormat(" and si.PayDept ={0} ", payDept);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel PayApplyInvoiceCreate(UserModel user, Apply apply, PayApply payApply, List<InvoicePayApply> details)
        {
            ResultModel result = new ResultModel();

            try
            {
                InvoicePayApplyDAL invoicePayApplyDAL = new InvoicePayApplyDAL();
                ApplyDAL applyDAL = new ApplyDAL();
                PayApplyDAL payApplyDAL = new PayApplyDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();
                SIDAL sIDAL = new SIDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (details == null || details.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "未选中任务发票";
                        return result;
                    }

                    //验证总额
                    decimal sumApplyBala = details.Sum(temp => temp.ApplyBala);
                    if (sumApplyBala != payApply.ApplyBala)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请总额与分项总额不相等";
                        return result;
                    }

                    //验证发票
                    int payDept = 0;
                    int outCorpId = 0;
                    foreach (InvoicePayApply detail in details)
                    {
                        //获取价外票
                        result = sIDAL.Get(user, detail.SIId);
                        if (result.ResultStatus != 0)
                            return result;

                        SI sI = result.ReturnValue as SI;
                        if (sI == null || sI.SIId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "价外票不存在";
                            return result;
                        }

                        //获取发票
                        result = invoiceDAL.Get(user, detail.InvoiceId);
                        if (result.ResultStatus != 0)
                            return result;

                        Operate.Model.Invoice invoice = result.ReturnValue as Operate.Model.Invoice;
                        if (invoice == null || invoice.InvoiceId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "发票不存在";
                            return result;
                        }

                        //验证币种
                        if (payApply.CurrencyId != invoice.CurrencyId)
                        {
                            result.ResultStatus = -1;
                            result.Message = "发票币种不一致";
                            return result;
                        }

                        //验证开票抬头
                        if (outCorpId == 0) { outCorpId = invoice.OutCorpId; }
                        if (outCorpId != invoice.OutCorpId)
                        {
                            result.ResultStatus = -1;
                            result.Message = "发票开票公司不一致";
                            return result;
                        }

                        //验证成本部门
                        if (payDept == 0) { payDept = sI.PayDept; }
                        if (payDept != sI.PayDept)
                        {
                            result.ResultStatus = -1;
                            result.Message = "发票成本部门不一致";
                            return result;
                        }

                        //验证可申请余额
                        result = invoicePayApplyDAL.LoadByInvoice(user, detail.InvoiceId);
                        if (result.ResultStatus != 0)
                            return result;
                        List<InvoicePayApply> resultDetails = result.ReturnValue as List<InvoicePayApply>;
                        if (resultDetails == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取发票已申请列表失败";
                            return result;
                        }

                        decimal applyBala = resultDetails.Sum(temp => temp.ApplyBala);
                        if (applyBala >= invoice.InvoiceBala)
                        {
                            result.ResultStatus = -1;
                            result.Message = string.Format("发票{0}款项已全部申请", invoice.InvoiceNo);
                            return result;
                        }

                        if (invoice.InvoiceBala - applyBala < detail.ApplyBala)
                        {
                            result.ResultStatus = -1;
                            result.Message = string.Format("发票{0}申请金额超过可申请余额，申请失败", invoice.InvoiceNo);
                            return result;
                        }
                    }

                    //新增申请主表
                    Department dept = UserProvider.Departments.FirstOrDefault(temp => temp.DeptId == apply.ApplyDept);
                    if (dept == null || dept.DeptId < 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请部门不存在";
                        return result;
                    }

                    int applyId = 0;
                    apply.ApplyDept = dept.DeptId;
                    apply.ApplyType = ApplyType.付款申请;
                    apply.EmpId = user.EmpId;
                    apply.ApplyStatus = StatusEnum.已录入;
                    result = applyDAL.Insert(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out applyId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请主表新增失败";
                        return result;
                    }

                    //新增付款申请表
                    payApply.ApplyId = applyId;
                    payApply.PayApplySource = (int)FundsStyleEnum.InvoicePayApply;
                    payApply.ApplyBala = sumApplyBala;
                    result = payapplyDAL.Insert(user, payApply);
                    if (result.ResultStatus != 0)
                        return result;
                    int payApplyId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out payApplyId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请新增失败";
                        return result;
                    }

                    //新增发票付款申请关联表                    
                    foreach (InvoicePayApply detail in details)
                    {
                        detail.DetailStatus = StatusEnum.已生效;
                        detail.PayApplyId = payApplyId;

                        result = invoicePayApplyDAL.Insert(user, detail);
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

        public SelectModel GetInvoiceListByApplySelect(int pageIndex, int pageSize, string orderStr, int payApplyId)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "si.SIId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)StatusEnum.已生效;

            StringBuilder sb = new StringBuilder();
            sb.Append("ipar.RefId,si.SIId,inv.InvoiceId,si.PayDept,dept.DeptName,inv.InvoiceDate,inv.InvoiceNo,inv.InvoiceBala,inv.CurrencyId,cur.CurrencyName,inv.OutCorpId,inv.OutCorpName,inv.InCorpId,inv.InCorpName,inv.Memo,ipar.ApplyBala,0 as LastBala");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_InvoicePayApply_Ref ipar ");
            sb.Append(" inner join dbo.Inv_SI si on ipar.SIId = si.SIId ");
            sb.Append(" inner join dbo.Invoice inv on si.InvoiceId = inv.InvoiceId ");
            sb.Append(" left join NFMT_User.dbo.Department dept on dept.DeptId = si.PayDept ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = inv.CurrencyId ");
            select.TableName = sb.ToString();
            sb.Clear();

            sb.AppendFormat(" ipar.PayApplyId = {0} and ipar.DetailStatus >= {1} ", payApplyId, readyStatus);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel PayApplyInvoiceUpdate(UserModel user, Apply apply, PayApply payApply, List<InvoicePayApply> details)
        {
            ResultModel result = new ResultModel();

            try
            {
                InvoicePayApplyDAL invoicePayApplyDAL = new InvoicePayApplyDAL();
                ApplyDAL applyDAL = new ApplyDAL();
                PayApplyDAL payApplyDAL = new PayApplyDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();
                SIDAL sIDAL = new SIDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (details == null || details.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "未选中任务发票";
                        return result;
                    }

                    //验证总额
                    decimal sumApplyBala = details.Sum(temp => temp.ApplyBala);
                    if (sumApplyBala != payApply.ApplyBala)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请总额与分项总额不相等";
                        return result;
                    }

                    //获取付款申请
                    result = this.payapplyDAL.Get(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    PayApply resultPayApply = result.ReturnValue as PayApply;
                    if (resultPayApply == null || resultPayApply.PayApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请不存在";
                        return result;
                    }

                    //获取发票申请列表
                    result = invoicePayApplyDAL.Load(user, resultPayApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<InvoicePayApply> resultDetails = result.ReturnValue as List<InvoicePayApply>;
                    if (resultDetails == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "付款申请发票明细列表获取失败";
                        return result;
                    }

                    //作废原有发票申请明细
                    foreach (InvoicePayApply detail in resultDetails)
                    {
                        detail.DetailStatus = StatusEnum.已录入;
                        result = invoicePayApplyDAL.Invalid(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //验证发票
                    int payDept = 0;
                    int outCorpId = 0;
                    foreach (InvoicePayApply detail in details)
                    {
                        //获取价外票
                        result = sIDAL.Get(user, detail.SIId);
                        if (result.ResultStatus != 0)
                            return result;

                        SI sI = result.ReturnValue as SI;
                        if (sI == null || sI.SIId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "价外票不存在";
                            return result;
                        }

                        //获取发票
                        result = invoiceDAL.Get(user, detail.InvoiceId);
                        if (result.ResultStatus != 0)
                            return result;

                        Operate.Model.Invoice invoice = result.ReturnValue as Operate.Model.Invoice;
                        if (invoice == null || invoice.InvoiceId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "发票不存在";
                            return result;
                        }

                        //验证币种
                        if (payApply.CurrencyId != invoice.CurrencyId)
                        {
                            result.ResultStatus = -1;
                            result.Message = "发票币种不一致";
                            return result;
                        }

                        //验证开票抬头
                        if (outCorpId == 0) { outCorpId = invoice.OutCorpId; }
                        if (outCorpId != invoice.OutCorpId)
                        {
                            result.ResultStatus = -1;
                            result.Message = "发票开票公司不一致";
                            return result;
                        }

                        //验证成本部门
                        if (payDept == 0) { payDept = sI.PayDept; }
                        if (payDept != sI.PayDept)
                        {
                            result.ResultStatus = -1;
                            result.Message = "发票成本部门不一致";
                            return result;
                        }

                        //验证可申请余额
                        result = invoicePayApplyDAL.LoadByInvoice(user, detail.InvoiceId);
                        if (result.ResultStatus != 0)
                            return result;
                        resultDetails = result.ReturnValue as List<InvoicePayApply>;
                        if (resultDetails == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取发票已申请列表失败";
                            return result;
                        }

                        decimal applyBala = resultDetails.Sum(temp => temp.ApplyBala);
                        if (applyBala >= invoice.InvoiceBala)
                        {
                            result.ResultStatus = -1;
                            result.Message = string.Format("发票{0}款项已全部申请", invoice.InvoiceNo);
                            return result;
                        }

                        if (invoice.InvoiceBala - applyBala < detail.ApplyBala)
                        {
                            result.ResultStatus = -1;
                            result.Message = string.Format("发票{0}申请金额超过可申请余额，申请失败", invoice.InvoiceNo);
                            return result;
                        }
                    }

                    //更新申请主表
                    Department dept = UserProvider.Departments.FirstOrDefault(temp => temp.DeptId == apply.ApplyDept);
                    if (dept == null || dept.DeptId < 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请部门不存在";
                        return result;
                    }

                    //获取主申请
                    result = applyDAL.Get(user, resultPayApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Apply resultApply = result.ReturnValue as Apply;
                    if (resultApply == null || resultApply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请获取失败";
                        return result;
                    }

                    resultApply.ApplyDept = dept.DeptId;
                    resultApply.ApplyCorp = apply.ApplyCorp;
                    resultApply.ApplyDesc = apply.ApplyDesc;
                    resultApply.ApplyTime = apply.ApplyTime;
                    resultApply.EmpId = user.EmpId;
                    result = applyDAL.Update(user, resultApply);
                    if (result.ResultStatus != 0)
                        return result;

                    //更新付款申请表
                    resultPayApply.RecCorpId = payApply.RecCorpId;
                    resultPayApply.RecBankId = payApply.RecBankId;
                    resultPayApply.RecBankAccountId = payApply.RecBankAccountId;
                    resultPayApply.RecBankAccount = payApply.RecBankAccount;
                    resultPayApply.CurrencyId = payApply.CurrencyId;
                    resultPayApply.ApplyBala = sumApplyBala;
                    resultPayApply.PayMode = payApply.PayMode;
                    resultPayApply.PayDeadline = payApply.PayDeadline;
                    resultPayApply.PayMatter = payApply.PayMatter;
                    resultPayApply.SpecialDesc = payApply.SpecialDesc;

                    result = payapplyDAL.Update(user, resultPayApply);
                    if (result.ResultStatus != 0)
                        return result;

                    //新增发票付款申请关联表
                    foreach (InvoicePayApply detail in details)
                    {
                        detail.DetailStatus = StatusEnum.已生效;
                        detail.PayApplyId = resultPayApply.PayApplyId;

                        result = invoicePayApplyDAL.Insert(user, detail);
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

        public ResultModel GetCondition(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = payapplyDAL.GetCondition(user, applyId);
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

        /// <summary>
        /// 获取付款申请审核内容
        /// </summary>
        /// <param name="user"></param>
        /// <param name="applyId"></param>
        /// <param name="fundsStyleEnum"></param>
        /// <returns></returns>
        public ResultModel GetAuditInfo(UserModel user, int applyId, FundsStyleEnum fundsStyleEnum)
        {
            ResultModel result = new ResultModel();
            ApplyDAL applyDAL = new ApplyDAL();
            string customBala = string.Empty;
            int customCorpId = 0;

            try
            {

                //获取付款申请
                result = this.payapplyDAL.GetByApplyId(user, applyId);
                if (result.ResultStatus != 0)
                    return result;

                PayApply payApply = result.ReturnValue as PayApply;
                if (payApply == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                    return result;
                }

                customCorpId = payApply.RecCorpId;
                int subContractId;

                switch (fundsStyleEnum)
                {
                    case FundsStyleEnum.合约付款申请:
                        ContractPayApplyDAL contractPayApplyDAL = new ContractPayApplyDAL();
                        result = contractPayApplyDAL.GetByPayApplyId(user, payApply.PayApplyId);
                        if (result.ResultStatus != 0)
                            return result;

                        ContractPayApply contractPayApply = result.ReturnValue as ContractPayApply;
                        if (contractPayApply == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取失败";
                            return result;
                        }

                        subContractId = contractPayApply.ContractSubId;
                        break;

                    case FundsStyleEnum.库存付款申请:
                        StockPayApplyDAL stockPayApplyDAl = new StockPayApplyDAL();
                        result = stockPayApplyDAl.Load(user, payApply.PayApplyId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<StockPayApply> stockPayApplys = result.ReturnValue as List<StockPayApply>;
                        if (stockPayApplys == null || !stockPayApplys.Any())
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取失败";
                            return result;
                        }

                        subContractId = stockPayApplys.First().SubId;
                        break;

                    case FundsStyleEnum.发票付款申请://价外付款
                        subContractId = 0;
                        break;

                    default:
                        subContractId = 0;
                        break;
                }

                //获取客户余额
                result = applyDAL.GetCustomerBala(user, customCorpId, subContractId, applyId, payApply.ApplyBala, 0);
                if (result.ResultStatus != 0)
                    return result;

                customBala = result.ReturnValue.ToString();

                if (!string.IsNullOrEmpty(customBala) && customBala.IndexOf('_') > -1)
                {
                    string corpName = string.Empty;
                    Corporation corp = UserProvider.Corporations.SingleOrDefault(a => a.CorpId == customCorpId);
                    if (corp != null)
                        corpName = corp.CorpName;

                    decimal customBalaValue = 0;
                    if (!decimal.TryParse(customBala.Split('_')[0], out customBalaValue))
                    {
                        result.ResultStatus = -1;
                        result.Message = "转换失败";
                        return result;
                    }

                    string currencyName = customBala.Split('_')[1].ToString();
                    string returnValue = string.Format("本次申请后，按照合同结算，{0} {1}（合{2}）{3}", corpName, customBalaValue > 0 ? "余款" : "欠款", currencyName, string.Format("{0:N}", customBalaValue));

                    if (customBalaValue < 0)
                    {
                        returnValue = string.Format("<font color='#660000'>{0}</font>", returnValue);
                    }

                    result.ResultStatus = 0;
                    result.ReturnValue = returnValue;
                }
                else
                {
                    result.Message = "获取客户余额失败";
                    result.ResultStatus = -1;
                    return result;
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

        public ResultModel PayApplyCreate(UserModel user, Apply apply, PayApply payApply, List<StockPayApply> stockDetails, int subId, bool isAudit)
        {
            ResultModel result = new ResultModel();

            try
            {
                ApplyDAL applyDAL = new ApplyDAL();
                ContractPayApplyDAL contractPayApplyDAL = new ContractPayApplyDAL();
                StockPayApplyDAL stockPayApplyDAL = new StockPayApplyDAL();
                StockDAL stockDAL = new StockDAL();
                StockLogDAL stockLogDAL = new StockLogDAL();
                ContractSubDAL subDAL = new ContractSubDAL();

                using (TransactionScope scope = new TransactionScope())
                {
                    //新增主申请
                    apply.ApplyStatus = StatusEnum.已录入;
                    apply.ApplyType = ApplyType.PayApply;
                    apply.EmpId = user.EmpId;
                    result = applyDAL.Insert(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    int applyId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out applyId) || applyId <= 0)
                    {
                        result.Message = "主申请新增失败";
                        result.ResultStatus = -1;
                        return result;
                    }
                    apply.ApplyId = applyId;

                    //新增付款申请
                    payApply.ApplyId = apply.ApplyId;
                    payApply.PayApplySource = (int)FundsStyleEnum.ContractPayApply;
                    if (stockDetails != null && stockDetails.Count > 0)
                        payApply.PayApplySource = (int)FundsStyleEnum.StockPayApply;

                    result = this.payapplyDAL.Insert(user, payApply);
                    if (result.ResultStatus != 0)
                        return result;

                    int payApplyId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out payApplyId) || payApplyId <= 0)
                    {
                        result.Message = "付款申请新增失败";
                        result.ResultStatus = -1;
                        return result;
                    }
                    payApply.PayApplyId = payApplyId;

                    //获取子合约
                    result = subDAL.Get(user, subId);
                    if (result.ResultStatus != 0)
                        return result;

                    ContractSub sub = result.ReturnValue as ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.Message = "子合约获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    if (sub.SubStatus != StatusEnum.已生效)
                    {
                        result.Message = "子合约状态不正确，不能进行付款申请";
                        result.ResultStatus = -1;
                        return result;
                    }
                    //新增合约付款申请
                    ContractPayApply contractPayApply = new ContractPayApply();
                    contractPayApply.ApplyBala = payApply.ApplyBala;
                    contractPayApply.ContractId = sub.ContractId;
                    contractPayApply.ContractSubId = sub.SubId;
                    contractPayApply.PayApplyId = payApply.PayApplyId;
                    contractPayApply.Status = StatusEnum.已录入;
                    result = contractPayApplyDAL.Insert(user, contractPayApply);
                    if (result.ResultStatus != 0)
                        return result;

                    int contractPayApplyId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out contractPayApplyId) || contractPayApplyId <= 0)
                    {
                        result.Message = "合约关联新增失败";
                        result.ResultStatus = -1;
                        return result;
                    }
                    contractPayApply.RefId = contractPayApplyId;

                    //新增库存付款申请
                    if (stockDetails != null && stockDetails.Count > 0)
                    {
                        foreach (StockPayApply detail in stockDetails)
                        {
                            detail.ContractId = contractPayApply.ContractId;
                            detail.ContractRefId = contractPayApply.RefId;
                            detail.PayApplyId = contractPayApply.PayApplyId;
                            detail.RefStatus = StatusEnum.已生效;
                            detail.SubId = contractPayApply.ContractSubId;
                            result = stockPayApplyDAL.Insert(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = payApply;

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

        public SelectModel GetPayApplyStocksSelect(int pageIndex, int pageSize, string orderStr, int payApplyId)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "spa.RefId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)StatusEnum.已生效;
            int entryStatus = (int)StatusEnum.已录入;
            int stockStatusType = (int)StatusTypeEnum.库存状态;

            StringBuilder sb = new StringBuilder();
            sb.Append("spa.RefId,pa.PayApplyId,sl.StockLogId,sto.StockId,sn.StockNameId,sn.RefNo,sto.StockStatus,ss.StatusName");
            sb.Append(",sto.CorpId,ownCorp.CorpName as OwnCorpName");
            sb.Append(",sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,sl.NetAmount,sl.Bundles,sto.UintId,mu.MUName,sl.NetAmount as AppAmount,spa.ApplyBala,sto.CardNo,dp.DPName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT.dbo.Fun_StockPayApply_Ref spa ");
            sb.Append(" inner join NFMT.dbo.Fun_ContractPayApply_Ref cpa on cpa.RefId = spa.ContractRefId ");
            sb.Append(" inner join NFMT.dbo.Fun_PayApply pa on pa.PayApplyId = spa.PayApplyId ");
            sb.AppendFormat(" inner join NFMT.dbo.Apply app on app.ApplyId = pa.ApplyId and app.ApplyStatus>={0} ", entryStatus);
            sb.AppendFormat(" inner join NFMT.dbo.St_StockLog sl on spa.StockLogId = sl.StockLogId and sl.LogStatus>={0} ", readyStatus);
            sb.Append(" inner join NFMT.dbo.St_Stock sto on sl.StockId = sto.StockId and spa.StockId = sto.StockId ");
            sb.Append(" inner join NFMT.dbo.St_StockName sn on sn.StockNameId = sl.StockNameId and sn.StockNameId = sto.StockNameId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId = {0} ", stockStatusType);
            sb.Append(" left join NFMT_User.dbo.Corporation ownCorp on ownCorp.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sto.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            select.TableName = sb.ToString();
            sb.Clear();

            sb.AppendFormat(" spa.PayApplyId = {0} and spa.RefStatus >={1} ", payApplyId, readyStatus);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel PayApplyUpdate(UserModel user, Apply apply, PayApply payApply, List<StockPayApply> stockDetails)
        {
            ResultModel result = new ResultModel();

            try
            {
                ApplyDAL applyDAL = new ApplyDAL();
                ContractPayApplyDAL contractPayApplyDAL = new ContractPayApplyDAL();
                StockPayApplyDAL stockPayApplyDAL = new StockPayApplyDAL();
                StockDAL stockDAL = new StockDAL();
                StockLogDAL stockLogDAL = new StockLogDAL();
                ContractSubDAL subDAL = new ContractSubDAL();

                using (TransactionScope scope = new TransactionScope())
                {
                    //获取主申请
                    result = applyDAL.Get(user, apply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Apply resultApply = result.ReturnValue as Apply;
                    if (resultApply == null || resultApply.ApplyId <= 0)
                    {
                        result.Message = "主申请获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    resultApply.EmpId = user.EmpId;
                    resultApply.ApplyDept = apply.ApplyDept;
                    resultApply.ApplyCorp = apply.ApplyCorp;
                    resultApply.ApplyDesc = apply.ApplyDesc;
                    resultApply.ApplyTime = apply.ApplyTime;

                    result = applyDAL.Update(user, resultApply);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取付款申请
                    result = this.payapplyDAL.Get(user, payApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    PayApply resultPayApply = result.ReturnValue as PayApply;
                    if (resultPayApply == null || resultPayApply.PayApplyId <= 0)
                    {
                        result.Message = "付款申请获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }
                    if (resultPayApply.ApplyId != resultApply.ApplyId)
                    {
                        result.Message = "付款申请与主申请不一致，更新失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    resultPayApply.RecCorpId = payApply.RecCorpId;
                    resultPayApply.RecBankId = payApply.RecBankId;
                    resultPayApply.RecBankAccountId = payApply.RecBankAccountId;
                    resultPayApply.RecBankAccount = payApply.RecBankAccount;
                    resultPayApply.ApplyBala = payApply.ApplyBala;
                    resultPayApply.CurrencyId = payApply.CurrencyId;
                    resultPayApply.PayMode = payApply.PayMode;
                    resultPayApply.PayDeadline = payApply.PayDeadline;
                    resultPayApply.PayMatter = payApply.PayMatter;
                    resultPayApply.SpecialDesc = payApply.SpecialDesc;

                    if (stockDetails.Count > 0)
                        resultPayApply.PayApplySource = (int)FundsStyleEnum.StockPayApply;
                    else
                        resultPayApply.PayApplySource = (int)FundsStyleEnum.ContractPayApply;

                    result = this.payapplyDAL.Update(user, resultPayApply);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取合约关联付款申请
                    result = contractPayApplyDAL.GetByPayApplyId(user, resultPayApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    ContractPayApply contractPayApply = result.ReturnValue as ContractPayApply;
                    if (contractPayApply == null || contractPayApply.RefId <= 0)
                    {
                        result.Message = "合约关联付款申请获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    contractPayApply.ApplyBala = resultPayApply.ApplyBala;
                    result = contractPayApplyDAL.Update(user, contractPayApply);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取现有库存明细
                    result = stockPayApplyDAL.Load(user, resultPayApply.PayApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<StockPayApply> resultDetails = result.ReturnValue as List<StockPayApply>;
                    if (resultDetails == null)
                    {
                        result.Message = "库存付款申请获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //作废现有库存明细
                    foreach (StockPayApply stockPayApply in resultDetails)
                    {
                        stockPayApply.RefStatus = StatusEnum.已录入;
                        result = stockPayApplyDAL.Invalid(user, stockPayApply);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //新增库存付款申请
                    if (stockDetails != null && stockDetails.Count > 0)
                    {
                        foreach (StockPayApply detail in stockDetails)
                        {
                            detail.ContractId = contractPayApply.ContractId;
                            detail.ContractRefId = contractPayApply.RefId;
                            detail.PayApplyId = contractPayApply.PayApplyId;
                            detail.RefStatus = StatusEnum.已生效;
                            detail.SubId = contractPayApply.ContractSubId;
                            result = stockPayApplyDAL.Insert(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = payApply.PayApplyId;

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

        public ResultModel CheckContractSubPayApplyConfirm(UserModel user, int subId)
        {
            return this.payapplyDAL.CheckContractSubPayApplyConfirm(user, subId);
        }

        public ResultModel GetContractBalancePayment(UserModel user, int subContractId, int payApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = payapplyDAL.GetContractBalancePayment(user, subContractId, payApplyId);
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

        public ResultModel PayApplyMultiCreate(UserModel user, Apply apply, PayApply payApply, List<StockPayApply> stockDetails, bool isAudit)
        {
            ResultModel result = new ResultModel();

            try
            {
                ApplyDAL applyDAL = new ApplyDAL();
                ContractPayApplyDAL contractPayApplyDAL = new ContractPayApplyDAL();
                StockPayApplyDAL stockPayApplyDAL = new StockPayApplyDAL();

                using (TransactionScope scope = new TransactionScope())
                {
                    //新增主申请
                    apply.ApplyStatus = StatusEnum.已录入;
                    apply.ApplyType = ApplyType.PayApply;
                    apply.EmpId = user.EmpId;
                    result = applyDAL.Insert(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    int applyId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out applyId) || applyId <= 0)
                    {
                        result.Message = "主申请新增失败";
                        result.ResultStatus = -1;
                        return result;
                    }
                    apply.ApplyId = applyId;

                    //新增付款申请
                    payApply.ApplyId = apply.ApplyId;
                    payApply.PayApplySource = (int)FundsStyleEnum.ContractPayApply;
                    if (stockDetails != null && stockDetails.Any())
                        payApply.PayApplySource = (int)FundsStyleEnum.StockPayApply;

                    result = this.payapplyDAL.Insert(user, payApply);
                    if (result.ResultStatus != 0)
                        return result;

                    int payApplyId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out payApplyId) || payApplyId <= 0)
                    {
                        result.Message = "付款申请新增失败";
                        result.ResultStatus = -1;
                        return result;
                    }
                    payApply.PayApplyId = payApplyId;

                    if (stockDetails != null)
                        foreach (int subId in stockDetails.Select(a => a.SubId).Distinct())
                        {
                            List<Model.StockPayApply> stockPayApplyDetails = stockDetails.Where(a => a.SubId == subId).ToList();

                            //新增合约付款申请
                            ContractPayApply contractPayApply = new ContractPayApply
                            {
                                ApplyBala = stockPayApplyDetails.Sum(a=>a.ApplyBala),
                                ContractId = stockPayApplyDetails.First().ContractId,
                                ContractSubId = subId,
                                PayApplyId = payApply.PayApplyId,
                                Status = StatusEnum.已录入
                            };
                            result = contractPayApplyDAL.Insert(user, contractPayApply);
                            if (result.ResultStatus != 0)
                                return result;

                            int contractPayApplyId = 0;
                            if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out contractPayApplyId) || contractPayApplyId <= 0)
                            {
                                result.Message = "合约关联新增失败";
                                result.ResultStatus = -1;
                                return result;
                            }
                            contractPayApply.RefId = contractPayApplyId;

                            foreach (StockPayApply detail in stockPayApplyDetails)
                            {
                                detail.ContractId = contractPayApply.ContractId;
                                detail.ContractRefId = contractPayApply.RefId;
                                detail.PayApplyId = contractPayApply.PayApplyId;
                                detail.RefStatus = StatusEnum.已生效;
                                detail.SubId = contractPayApply.ContractSubId;
                                result = stockPayApplyDAL.Insert(user, detail);
                                if (result.ResultStatus != 0)
                                    return result;
                            }
                        }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = payApply;

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

        public SelectModel GetPayApplyReportSelect(int pageIndex, int pageSize, string orderStr, int applyCorpId, int applyDeptId, int currencyId, DateTime startDate, DateTime endDate)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pa.PayApplyId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)StatusEnum.已生效;
            StringBuilder sb = new StringBuilder();

            select.ColumnName = " pa.PayApplyId,a.ApplyTime,a.ApplyNo,pa.ApplyBala,cur.CurrencyName,corp.CorpName as ApplyCorpName,dept.DeptName,recCorp.CorpName as RecCorpName,bank.BankName,pa.RecBankAccount,bdPayMode.DetailName ";

            sb.Clear();
            sb.Append(" NFMT..Fun_PayApply pa ");
            sb.Append(" inner join NFMT..Apply a on pa.ApplyId = a.ApplyId ");
            sb.Append(" left join NFMT_Basic..Currency cur on cur.CurrencyId = pa.CurrencyId ");
            sb.Append(" left join NFMT_User..Corporation corp on corp.CorpId = a.ApplyCorp ");
            sb.Append(" left join NFMT_User..Department dept on dept.DeptId = a.ApplyDept ");
            sb.Append(" left join NFMT_User..Corporation recCorp on recCorp.CorpId = pa.RecCorpId ");
            sb.Append(" left join NFMT_Basic..Bank bank on bank.BankId = pa.RecBankId ");
            sb.AppendFormat(" left join NFMT_Basic..BDStyleDetail bdPayMode on bdPayMode.StyleDetailId = pa.PayMode and bdPayMode.BDStyleId = {0} ", (int)StyleEnum.PayMode);

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" a.ApplyStatus >= {0} ", readyStatus);
            if (applyCorpId > 0)
                sb.AppendFormat(" and a.ApplyCorp = {0} ", applyCorpId);
            if (applyDeptId > 0)
                sb.AppendFormat(" and a.ApplyDept = {0} ", applyDeptId);
            if (currencyId > 0)
                sb.AppendFormat(" and pa.CurrencyId = {0} ", currencyId);
            if (startDate > DefaultValue.DefaultTime && endDate > startDate)
                sb.AppendFormat(" and a.ApplyTime between '{0}' and '{1}' ", startDate.ToString(), endDate.AddDays(1).ToString());

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
