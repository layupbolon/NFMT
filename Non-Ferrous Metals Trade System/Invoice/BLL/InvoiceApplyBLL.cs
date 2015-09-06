/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceApplyBLL.cs
// 文件功能描述：开票申请dbo.Inv_InvoiceApply业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年1月27日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Invoice.Model;
using NFMT.Invoice.DAL;
using NFMT.Invoice.IDAL;
using NFMT.Common;
using System.Linq;

namespace NFMT.Invoice.BLL
{
    /// <summary>
    /// 开票申请dbo.Inv_InvoiceApply业务逻辑类。
    /// </summary>
    public class InvoiceApplyBLL : Common.ExecBLL
    {
        private InvoiceApplyDAL invoiceapplyDAL = new InvoiceApplyDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(InvoiceApplyDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InvoiceApplyBLL()
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
            get { return this.invoiceapplyDAL; }
        }

        #endregion

        #region 新增方法

        public Common.SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, DateTime dateBegin, DateTime dateEnd, int empId, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "bi.BusinessInvoiceId desc";
            else
                select.OrderStr = orderStr;

            int commonStatusType = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("ia.InvoiceApplyId,a.ApplyId,a.ApplyNo,e.Name,a.ApplyTime,dept.DeptName,corp.CorpName,a.ApplyDesc,bd.StatusName,a.ApplyStatus,ISNULL(biApply.BIApply,0) BIApply,ISNULL(siApply.SIApply,0) SIApply");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Inv_InvoiceApply ia  ");
            sb.Append(" inner join NFMT..Apply a on ia.ApplyId = a.ApplyId ");
            sb.Append(" left join NFMT_User..Corporation corp on a.ApplyCorp = corp.CorpId ");
            sb.Append(" left join NFMT_User..Department dept on a.ApplyDept = dept.DeptId ");
            sb.Append(" left join NFMT_User..Employee e on a.EmpId = e.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic..BDStatusDetail bd on a.ApplyStatus = bd.DetailId and bd.StatusId ={0} ", commonStatusType);
            sb.AppendFormat(" left join (select distinct InvoiceApplyId,1 as BIApply from dbo.Inv_InvoiceApplyDetail where DetailStatus >={0}) biApply on ia.InvoiceApplyId = biApply.InvoiceApplyId", (int)Common.StatusEnum.已生效);
            sb.AppendFormat(" left join (select distinct InvoiceApplyId,1 as SIApply from dbo.Inv_InvoiceApplySIDetail where DetailStatus >={0}) siApply on ia.InvoiceApplyId = siApply.InvoiceApplyId", (int)Common.StatusEnum.已生效);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (status > 0)
                sb.AppendFormat(" and a.ApplyStatus = {0} ", status);
            if (empId > 0)
                sb.AppendFormat(" and a.EmpId = {0} ", empId);
            if (dateBegin > Common.DefaultValue.DefaultTime && dateEnd > dateBegin)
                sb.AppendFormat(" and a.ApplyTime between '{0}' and '{1}' ", dateBegin.ToString(), dateEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetCanApplyBISelectModel(int pageIndex, int pageSize, string orderStr, int assetId, int customerCorpId, string subNo)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "inv.InvoiceId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" inv.InvoiceId,bi.BusinessInvoiceId,inv.InvoiceNo,inv.InvoiceDate,inv.InvoiceName,inv.InvoiceBala,CONVERT(varchar,inv.InvoiceBala)+cur.CurrencyName as InoviceBalaName,ISNULL(inv.InCorpName,inCorp.CorpName) InCorpName,ISNULL(inv.OutCorpName,outCorp.CorpName) OutCorpName,inv.Memo,sub.SubNo,ass.AssetName,bi.IntegerAmount,CONVERT(varchar,bi.IntegerAmount)+mu.MUName IntegerAmountName,bi.NetAmount,CONVERT(varchar,bi.NetAmount)+MUName as NetAmountName,bi.UnitPrice,bi.MarginRatio,bi.VATRatio,bi.VATBala,bi.AssetId,inv.CurrencyId,inv.OutCorpId,inv.InCorpId,bi.MUId ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Inv_BusinessInvoice bi ");
            sb.Append(" inner join dbo.Invoice inv on bi.InvoiceId = inv.InvoiceId ");
            sb.Append(" left join dbo.Con_ContractSub sub on sub.SubId = bi.SubContractId ");
            sb.Append(" left join NFMT_Basic..Currency cur on inv.CurrencyId = cur.CurrencyId");
            sb.Append(" left join NFMT_User..Corporation inCorp on inCorp.CorpId = inv.InCorpId ");
            sb.Append(" left join NFMT_User..Corporation outCorp on outCorp.CorpId = inv.OutCorpId ");
            sb.Append(" left join NFMT_Basic..Asset ass on bi.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic..MeasureUnit mu on mu.MUId = bi.MUId ");
            //sb.Append(" left join NFMT..Pri_PriceConfirm pc on bi.ContractId = pc.ContractId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" inv.InvoiceDirection = {0} and inv.InvoiceStatus >= {1}", NFMT.Data.DetailProvider.Details(Data.StyleEnum.InvoiceDirection)["Issue"].StyleDetailId, (int)Common.StatusEnum.已生效);
            sb.AppendFormat(" and bi.BusinessInvoiceId not in (select BussinessInvoiceId from dbo.Inv_InvoiceApplyDetail where DetailStatus >= {0}) ", (int)Common.StatusEnum.已生效);
            //sb.AppendFormat(" and ISNULL(pc.PriceConfirmId,0) <> 0 ");//必须要先做过价格确认单

            if (assetId > 0)
                sb.AppendFormat(" and bi.AssetId = {0} ", assetId);
            if (customerCorpId > 0)
                sb.AppendFormat(" and inv.OutCorpId = {0} ", customerCorpId);
            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and sub.SubNo like '%{0}%' ", subNo);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetApplyBIInvInfoSelectModel(int pageIndex, int pageSize, string orderStr, string busInvIds, string stockLogIds)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "inv.InvoiceId desc";
            else
                select.OrderStr = orderStr;

            int status = (int)Common.StatusEnum.已生效;
            NFMT.Data.Model.BDStyleDetail pricing = NFMT.Data.DetailProvider.Details(Data.StyleEnum.PriceMode)["Pricing"];
            NFMT.Data.Model.BDStyleDetail fixedPrice = NFMT.Data.DetailProvider.Details(Data.StyleEnum.PriceMode)["FixedPrice"];

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" bi.BusinessInvoiceId BussinessInvoiceId,inv.InvoiceId,inv.InvoiceNo,inv.InvoiceDate,inv.InvoiceBala,cur.CurrencyName,sub.SubNo,isnull(sub.OutContractNo,con.OutContractNo) as OutContractNo,customerCorp.CorpName,ass.AssetName,bi.NetAmount,mu.MUName,bid.StockLogId,bi.ContractId,bi.SubContractId,slog.CardNo, ");
            sb.AppendFormat(" case con.PriceMode when {0} then pc.SettlePrice when {1} then cp.FixedPrice end as InvoicePrice ", pricing.StyleDetailId, fixedPrice.StyleDetailId);
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Inv_BusinessInvoice bi ");
            sb.Append(" inner join dbo.Invoice inv on bi.InvoiceId = inv.InvoiceId ");
            sb.AppendFormat(" inner join dbo.Inv_BusinessInvoiceDetail bid on bi.BusinessInvoiceId = bid.BusinessInvoiceId and bid.DetailStatus >= {0} ", status);
            sb.Append(" left join NFMT..Con_ContractSub sub on bi.SubContractId = sub.SubId ");
            sb.Append(" left join NFMT..Con_Contract con on bi.ContractId = con.ContractId ");
            sb.Append(" left join NFMT_User..Corporation customerCorp on inv.InCorpId = customerCorp.CorpId ");
            sb.Append(" left join NFMT_Basic..Asset ass on bi.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic..MeasureUnit mu on bi.MUId = mu.MUId ");
            sb.AppendFormat(" left join NFMT..Pri_PriceConfirm pc on bi.SubContractId = pc.SubId and pc.PriceConfirmStatus >= {0} ", status);
            sb.Append(" left join NFMT..Con_ContractPrice cp on con.ContractId = cp.ContractId ");
            sb.Append(" left join NFMT_Basic..Currency cur on cur.CurrencyId = con.SettleCurrency ");
            sb.Append(" left join NFMT..St_StockLog slog on slog.StockLogId = bid.StockLogId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" inv.InvoiceDirection = {0} and inv.InvoiceStatus >= {1} ", NFMT.Data.DetailProvider.Details(Data.StyleEnum.InvoiceDirection)["Issue"].StyleDetailId, (int)Common.StatusEnum.已生效);

            if (!string.IsNullOrEmpty(busInvIds))
                sb.AppendFormat(" and bi.BusinessInvoiceId in ({0}) ", busInvIds);
            if (!string.IsNullOrEmpty(stockLogIds))
                sb.AppendFormat(" and bid.StockLogId not in ({0}) ", stockLogIds);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Create(UserModel user, NFMT.Operate.Model.Apply apply, List<Model.InvoiceApplyDetail> details, bool isSubmitAudit)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
                    result = applyDAL.Insert(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    int applyId = (int)result.ReturnValue;

                    result = invoiceapplyDAL.Insert(user, new Model.InvoiceApply()
                    {
                        ApplyId = applyId,
                        TotalBala = details.Sum(a => a.InvoiceBala)
                    });
                    if (result.ResultStatus != 0)
                        return result;

                    int invoiceApplyId = (int)result.ReturnValue;

                    DAL.InvoiceApplyDetailDAL invoiceApplyDetailDAL = new InvoiceApplyDetailDAL();
                    foreach (Model.InvoiceApplyDetail detail in details)
                    {
                        detail.InvoiceApplyId = invoiceApplyId;
                        detail.ApplyId = applyId;
                        result = invoiceApplyDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (isSubmitAudit)
                    {
                        apply.ApplyId = applyId;

                        NFMT.WorkFlow.AutoSubmit submit = new WorkFlow.AutoSubmit();
                        NFMT.WorkFlow.ITaskProvider taskProvider = new NFMT.Invoice.TaskProvider.InvoiceApplyTaskProvider();
                        result = submit.Submit(user, apply, taskProvider, WorkFlow.MasterEnum.发票申请审核);
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
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Update(UserModel user, NFMT.Operate.Model.Apply apply, Model.InvoiceApply invoiceApply, List<Model.InvoiceApplyDetail> details)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
                    result = applyDAL.Get(user, apply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply applyRes = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (applyRes == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取申请信息失败";
                        return result;
                    }
                    applyRes.ApplyCorp = apply.ApplyCorp;
                    applyRes.ApplyDept = apply.ApplyDept;
                    applyRes.EmpId = user.EmpId;
                    applyRes.ApplyTime = DateTime.Now;
                    applyRes.ApplyDesc = apply.ApplyDesc;

                    result = applyDAL.Update(user, applyRes);
                    if (result.ResultStatus != 0)
                        return result;


                    invoiceApply.TotalBala = details.Sum(a => a.InvoiceBala);
                    result = invoiceapplyDAL.Update(user, invoiceApply);
                    if (result.ResultStatus != 0)
                        return result;

                    DAL.InvoiceApplyDetailDAL invoiceApplyDetailDAL = new InvoiceApplyDetailDAL();
                    result = invoiceApplyDetailDAL.InvalidAll(user, invoiceApply.InvoiceApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    foreach (Model.InvoiceApplyDetail detail in details)
                    {
                        result = invoiceApplyDetailDAL.Insert(user, detail);
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
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Goback(UserModel user, int invoiceApplyId)
        {
            ResultModel result = new ResultModel();
            NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = invoiceapplyDAL.Get(user, invoiceApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.InvoiceApply invoiceApply = result.ReturnValue as Model.InvoiceApply;
                    if (invoiceApply == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取发票申请失败";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, invoiceApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Operate.Model.Apply apply = result.ReturnValue as Operate.Model.Apply;
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
                    WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

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

        public ResultModel Invalid(UserModel user, int invoiceApplyId)
        {
            ResultModel result = new ResultModel();
            Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
            DAL.InvoiceApplyDetailDAL invoiceApplyDetailDAL = new InvoiceApplyDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证出库申请
                    result = invoiceapplyDAL.Get(user, invoiceApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.InvoiceApply invoiceApply = result.ReturnValue as Model.InvoiceApply;
                    if (invoiceApply == null || invoiceApply.InvoiceApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票申请不存在";
                        return result;
                    }

                    //获取主申请实体
                    result = applyDAL.Get(user, invoiceApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Operate.Model.Apply apply = result.ReturnValue as Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请不存在";
                        return result;
                    }

                    //获取申请明细
                    result = invoiceApplyDetailDAL.Load(user, invoiceApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.InvoiceApplyDetail> details = result.ReturnValue as List<Model.InvoiceApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请明细获取失败";
                        return result;
                    }

                    //作废主申请
                    result = applyDAL.Invalid(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废申请明细
                    foreach (Model.InvoiceApplyDetail detail in details)
                    {
                        if (detail.DetailStatus == StatusEnum.已生效)
                            detail.DetailStatus = StatusEnum.已录入;
                        result = invoiceApplyDetailDAL.Invalid(user, detail);
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

        public ResultModel InvalidSI(UserModel user, int invoiceApplyId)
        {
            ResultModel result = new ResultModel();
            Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
            DAL.InvoiceApplySIDetailDAL invoiceApplySIDetailDAL = new InvoiceApplySIDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = invoiceapplyDAL.Get(user, invoiceApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.InvoiceApply invoiceApply = result.ReturnValue as Model.InvoiceApply;
                    if (invoiceApply == null || invoiceApply.InvoiceApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票申请不存在";
                        return result;
                    }

                    //获取主申请实体
                    result = applyDAL.Get(user, invoiceApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Operate.Model.Apply apply = result.ReturnValue as Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请不存在";
                        return result;
                    }

                    //获取申请明细
                    result = invoiceApplySIDetailDAL.Load(user, invoiceApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.InvoiceApplySIDetail> details = result.ReturnValue as List<Model.InvoiceApplySIDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请明细获取失败";
                        return result;
                    }

                    //作废主申请
                    result = applyDAL.Invalid(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废申请明细
                    foreach (Model.InvoiceApplySIDetail detail in details)
                    {
                        if (detail.DetailStatus == StatusEnum.已生效)
                            detail.DetailStatus = StatusEnum.已录入;
                        result = invoiceApplySIDetailDAL.Invalid(user, detail);
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

        public ResultModel Confirm(UserModel user, int invoiceApplyId)
        {
            ResultModel result = new ResultModel();
            Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
            DAL.InvoiceApplyDetailDAL invoiceApplyDetailDAL = new InvoiceApplyDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证报关申请
                    result = invoiceapplyDAL.Get(user, invoiceApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.InvoiceApply invoiceApply = result.ReturnValue as Model.InvoiceApply;
                    if (invoiceApply == null || invoiceApply.InvoiceApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, invoiceApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Operate.Model.Apply apply = result.ReturnValue as Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请不存在";
                        return result;
                    }

                    //验证是否执行完成 TODO

                    result = invoiceApplyDetailDAL.Load(user, invoiceApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.InvoiceApplyDetail> details = result.ReturnValue as List<Model.InvoiceApplyDetail>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取发票申请明细失败";
                        return result;
                    }

                    result = applyDAL.Confirm(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    foreach (Model.InvoiceApplyDetail detail in details)
                    {
                        result = invoiceApplyDetailDAL.Confirm(user, detail);
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

        public ResultModel ConfirmCancel(UserModel user, int invoiceApplyId)
        {
            ResultModel result = new ResultModel();
            DAL.InvoiceApplyDetailDAL invoiceApplyDetailDAL = new InvoiceApplyDetailDAL();
            Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证报关申请
                    result = invoiceapplyDAL.Get(user, invoiceApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.InvoiceApply invoiceApply = result.ReturnValue as Model.InvoiceApply;
                    if (invoiceApply == null || invoiceApply.InvoiceApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, invoiceApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Operate.Model.Apply apply = result.ReturnValue as Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请不存在";
                        return result;
                    }

                    //主申请状态更新至已生效
                    result = applyDAL.ConfirmCancel(user, apply);
                    if (result.ResultStatus != 0)
                        return result;


                    result = invoiceApplyDetailDAL.Load(user, invoiceApplyId, Common.StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.InvoiceApplyDetail> details = result.ReturnValue as List<Model.InvoiceApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取申请明细失败";
                        return result;
                    }

                    foreach (Model.InvoiceApplyDetail detail in details)
                    {
                        result = invoiceApplyDetailDAL.ConfirmCancel(user, detail);
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

        public ResultModel GetByApplyId(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = invoiceapplyDAL.GetByApplyId(user, applyId);
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

        public SelectModel GetCanIssueApplyListSelectModel(int pageIndex, int pageSize, string orderStr, DateTime dateBegin, DateTime dateEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ia.InvoiceApplyId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" ia.InvoiceApplyId,a.ApplyId,a.ApplyNo,e.Name,a.ApplyTime,ia.TotalBala ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Inv_InvoiceApply ia ");
            sb.Append(" inner join dbo.Apply a on ia.ApplyId = a.ApplyId ");
            sb.Append(" left join NFMT_User..Employee e on e.EmpId = a.EmpId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" a.ApplyStatus >={0} ", (int)Common.StatusEnum.已生效);
            sb.AppendFormat(" and ia.InvoiceApplyId not in (select InvoiceApplyId from dbo.Inv_InvoiceApplyFinance where RefStatus >= {0}) ", (int)Common.StatusEnum.已生效);

            if (dateBegin > Common.DefaultValue.DefaultTime && dateEnd > dateBegin)
                sb.AppendFormat(" and a.ApplyTime between '{0}' and '{1}' ", dateBegin.ToString(), dateEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel GetBIidsByInvApplyId(UserModel user, int invApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = invoiceapplyDAL.GetBIidsByInvApplyId(user, invApplyId);
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

        public ResultModel GetBIidsByInvApplyIdExceptFinInvoice(UserModel user, int invApplyId, int financeInvoiceId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = invoiceapplyDAL.GetBIidsByInvApplyIdExceptFinInvoice(user, invApplyId, financeInvoiceId);
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

        public SelectModel GetCanApplySISelectModel(int pageIndex, int pageSize, string orderStr, int customerCorpId, DateTime beginDate, DateTime endDate)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "inv.InvoiceId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" inv.InvoiceId,inv.InvoiceNo,inv.InvoiceName,inv.InvoiceDate, CONVERT(varchar,inv.InvoiceBala) + c.CurrencyName as InvoiceBala,c1.CorpName as InnerCorp,c2.CorpName as OutCorp,bd.StatusName,inv.InvoiceStatus,si.SIId,inv.OutCorpId,inv.InCorpId ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Invoice inv ");
            sb.Append(" left join dbo.Inv_SI si on inv.InvoiceId = si.InvoiceId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on inv.CurrencyId = c.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c1 on inv.InCorpId = c1.CorpId");
            sb.Append(" left join NFMT_User.dbo.Corporation c2 on inv.OutCorpId = c2.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = inv.InvoiceStatus and StatusId ={0} ", (int)NFMT.Common.StatusTypeEnum.通用状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" inv.InvoiceType ={0} and inv.InvoiceDirection = {1} and inv.InvoiceStatus >={2}", (int)NFMT.Invoice.InvoiceTypeEnum.价外票, (int)NFMT.Invoice.InvoiceDirectionEnum.开具, (int)Common.StatusEnum.已生效);
            sb.AppendFormat(" and inv.InvoiceId not in (select InvoiceId from dbo.Inv_InvoiceApplySIDetail where DetailStatus>={0}) ", (int)Common.StatusEnum.已生效);

            if (customerCorpId > 0)
                sb.AppendFormat(" and inv.InCorpId = {0} ", customerCorpId);
            if (beginDate > NFMT.Common.DefaultValue.DefaultTime && endDate > beginDate)
                sb.AppendFormat(" and inv.InvoiceDate between '{0}' and '{1}' ", beginDate.ToShortDateString(), endDate.ToShortDateString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetSISelectListModel(int pageIndex, int pageSize, string orderStr, string sIIds)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "inv.InvoiceId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" inv.InvoiceId,inv.InvoiceNo,inv.InvoiceName,inv.InvoiceDate, CONVERT(varchar,inv.InvoiceBala) + c.CurrencyName as InvoiceBala,c1.CorpName as InnerCorp,c2.CorpName as OutCorp,bd.StatusName,inv.InvoiceStatus,si.SIId ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Invoice inv ");
            sb.Append(" left join dbo.Inv_SI si on inv.InvoiceId = si.InvoiceId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on inv.CurrencyId = c.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c1 on inv.InCorpId = c1.CorpId");
            sb.Append(" left join NFMT_User.dbo.Corporation c2 on inv.OutCorpId = c2.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = inv.InvoiceStatus and StatusId ={0} ", (int)NFMT.Common.StatusTypeEnum.通用状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" inv.InvoiceType ={0} and inv.InvoiceDirection = {1} and inv.InvoiceStatus >={2}", (int)NFMT.Invoice.InvoiceTypeEnum.价外票, (int)NFMT.Invoice.InvoiceDirectionEnum.开具, (int)Common.StatusEnum.已生效);
            sb.AppendFormat(" and inv.InvoiceId not in (select InvoiceId from dbo.Inv_InvoiceApplySIDetail where DetailStatus>={0}) ", (int)Common.StatusEnum.已生效);
            if (!string.IsNullOrEmpty(sIIds))
                sb.AppendFormat(" and si.SIId in ({0}) ", sIIds);
            else
                sb.Append(" and 1=2 ");

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetSISelectedListModel(int pageIndex, int pageSize, string orderStr, string sIIds, string exceptIds)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "inv.InvoiceId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" inv.InvoiceId,inv.InvoiceNo,inv.InvoiceName,inv.InvoiceDate, CONVERT(varchar,inv.InvoiceBala) + c.CurrencyName as InvoiceBala,c1.CorpName as InnerCorp,c2.CorpName as OutCorp,bd.StatusName,inv.InvoiceStatus,si.SIId,inv.InvoiceBala  InvoiceBalaValue");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Invoice inv ");
            sb.Append(" left join dbo.Inv_SI si on inv.InvoiceId = si.InvoiceId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on inv.CurrencyId = c.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c1 on inv.InCorpId = c1.CorpId");
            sb.Append(" left join NFMT_User.dbo.Corporation c2 on inv.OutCorpId = c2.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = inv.InvoiceStatus and StatusId ={0} ", (int)NFMT.Common.StatusTypeEnum.通用状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" inv.InvoiceType ={0} and inv.InvoiceDirection = {1} and inv.InvoiceStatus >={2}", (int)NFMT.Invoice.InvoiceTypeEnum.价外票, (int)NFMT.Invoice.InvoiceDirectionEnum.开具, (int)Common.StatusEnum.已生效);

            if (!string.IsNullOrEmpty(sIIds))
                sb.AppendFormat(" and si.SIId in ({0}) ", sIIds);
            else
                sb.Append(" and 1=2 ");

            if (!string.IsNullOrEmpty(exceptIds))
                sb.AppendFormat(" and si.SIID not in ({0}) ", exceptIds);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetCashInAllotMainSIInvliceModel(int pageIndex, int pageSize, string orderStr, string sIIds, string exceptIds)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "inv.InvoiceId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" inv.InvoiceId,inv.InvoiceNo,inv.InvoiceName,inv.InvoiceDate, CONVERT(varchar,inv.InvoiceBala) + c.CurrencyName as InvoiceBala,c1.CorpName as InnerCorp,c2.CorpName as OutCorp,bd.StatusName,inv.InvoiceStatus,si.SIId,inv.InvoiceBala  InvoiceBalaValue,inv.InvoiceBala as AllotBala");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Invoice inv ");
            sb.Append(" left join dbo.Inv_SI si on inv.InvoiceId = si.InvoiceId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on inv.CurrencyId = c.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c1 on inv.InCorpId = c1.CorpId");
            sb.Append(" left join NFMT_User.dbo.Corporation c2 on inv.OutCorpId = c2.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = inv.InvoiceStatus and StatusId ={0} ", (int)NFMT.Common.StatusTypeEnum.通用状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" inv.InvoiceType ={0} and inv.InvoiceDirection = {1} and inv.InvoiceStatus >={2}", (int)NFMT.Invoice.InvoiceTypeEnum.价外票, (int)NFMT.Invoice.InvoiceDirectionEnum.开具, (int)Common.StatusEnum.已生效);

            if (!string.IsNullOrEmpty(sIIds))
                sb.AppendFormat(" and si.SIId in ({0}) ", sIIds);
            else
                sb.Append(" and 1=2 ");

            if (!string.IsNullOrEmpty(exceptIds))
                sb.AppendFormat(" and si.SIID not in ({0}) ", exceptIds);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel CreateSIApply(UserModel user, NFMT.Operate.Model.Apply apply, List<Model.InvoiceApplySIDetail> details, bool isSubmitAudit)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
                    result = applyDAL.Insert(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    int applyId = (int)result.ReturnValue;

                    decimal totalBala = 0;
                    NFMT.Operate.DAL.InvoiceDAL invoiceDAL = new Operate.DAL.InvoiceDAL();
                    foreach (Model.InvoiceApplySIDetail detail in details)
                    {
                        result = invoiceDAL.Get(user, detail.InvoiceId);
                        if (result.ResultStatus != 0)
                            return result;

                        NFMT.Operate.Model.Invoice invoice = result.ReturnValue as NFMT.Operate.Model.Invoice;

                        totalBala += invoice.InvoiceBala;
                    }

                    result = invoiceapplyDAL.Insert(user, new Model.InvoiceApply()
                    {
                        ApplyId = applyId,
                        TotalBala = totalBala
                    });
                    if (result.ResultStatus != 0)
                        return result;

                    int invoiceApplyId = (int)result.ReturnValue;

                    DAL.InvoiceApplySIDetailDAL invoiceApplySIDetailDAL = new InvoiceApplySIDetailDAL();
                    foreach (Model.InvoiceApplySIDetail detail in details)
                    {
                        detail.InvoiceApplyId = invoiceApplyId;
                        detail.ApplyId = applyId;
                        result = invoiceApplySIDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (isSubmitAudit)
                    {
                        apply.ApplyId = applyId;

                        NFMT.WorkFlow.AutoSubmit submit = new WorkFlow.AutoSubmit();
                        NFMT.WorkFlow.ITaskProvider taskProvider = new NFMT.Invoice.TaskProvider.InvoiceApplyTaskProvider();
                        result = submit.Submit(user, apply, taskProvider, WorkFlow.MasterEnum.发票申请审核);
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
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel UpdateSIApply(UserModel user, NFMT.Operate.Model.Apply apply, Model.InvoiceApply invoiceApply, List<Model.InvoiceApplySIDetail> details)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
                    result = applyDAL.Get(user, apply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply applyRes = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (applyRes == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取申请信息失败";
                        return result;
                    }
                    applyRes.ApplyCorp = apply.ApplyCorp;
                    applyRes.ApplyDept = apply.ApplyDept;
                    applyRes.EmpId = user.EmpId;
                    applyRes.ApplyTime = DateTime.Now;
                    applyRes.ApplyDesc = apply.ApplyDesc;

                    result = applyDAL.Update(user, applyRes);
                    if (result.ResultStatus != 0)
                        return result;


                    decimal totalBala = 0;
                    NFMT.Operate.DAL.InvoiceDAL invoiceDAL = new Operate.DAL.InvoiceDAL();
                    foreach (Model.InvoiceApplySIDetail detail in details)
                    {
                        result = invoiceDAL.Get(user, detail.InvoiceId);
                        if (result.ResultStatus != 0)
                            return result;

                        NFMT.Operate.Model.Invoice invoice = result.ReturnValue as NFMT.Operate.Model.Invoice;

                        totalBala += invoice.InvoiceBala;
                    }

                    invoiceApply.TotalBala = totalBala;
                    result = invoiceapplyDAL.Update(user, invoiceApply);
                    if (result.ResultStatus != 0)
                        return result;

                    DAL.InvoiceApplySIDetailDAL invoiceApplySIDetailDAL = new InvoiceApplySIDetailDAL();
                    result = invoiceApplySIDetailDAL.InvalidAll(user, invoiceApply.InvoiceApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    foreach (Model.InvoiceApplySIDetail detail in details)
                    {
                        detail.InvoiceApplyId = invoiceApply.InvoiceApplyId;
                        detail.ApplyId = apply.ApplyId;
                        result = invoiceApplySIDetailDAL.Insert(user, detail);
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
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel GetPrintInfo(UserModel user, string invApplyIds)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = invoiceapplyDAL.GetDtForExport(user, invApplyIds);
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
