/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StopLossApplyBLL.cs
// 文件功能描述：止损申请dbo.Pri_StopLossApply业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年10月23日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.DoPrice.Model;
using NFMT.DoPrice.DAL;
using NFMT.DoPrice.IDAL;
using NFMT.Common;
using System.Linq;

namespace NFMT.DoPrice.BLL
{
    /// <summary>
    /// 止损申请dbo.Pri_StopLossApply业务逻辑类。
    /// </summary>
    public class StopLossApplyBLL : Common.ApplyBLL
    {
        private StopLossApplyDAL stoplossapplyDAL = new StopLossApplyDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StopLossApplyDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StopLossApplyBLL()
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
            get { return this.stoplossapplyDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int applyPerson, string subNo, int assetId, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sla.StopLossApplyId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" sla.StopLossApplyId,a.ApplyStatus,e.Name,d.DeptName,c2.CorpName as ApplyCorp,a.ApplyDesc,con.ContractNo,sub.SubNo,asset.AssetName,CONVERT(varchar,sla.StopLossWeight) + mu.MUName as StopLossWeight,CONVERT(varchar,sla.StopLossPrice) + cur.CurrencyName as StopLossPrice,ss.StatusName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_StopLossApply sla ");
            sb.Append(" left join dbo.Apply a on sla.ApplyId = a.ApplyId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on a.EmpId = e.EmpId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c2 on a.ApplyCorp = c2.CorpId ");
            sb.Append(" left join NFMT_User.dbo.Department d on a.ApplyDept = d.DeptId ");
            sb.Append(" left join dbo.Con_ContractSub sub on sla.SubContractId = sub.SubId ");
            sb.Append(" left join dbo.Con_Contract con on sla.ContractId = con.ContractId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset asset on sla.AssertId = asset.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on sla.MUId = mu.MUId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on sla.CurrencyId = cur.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = a.ApplyStatus and ss.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (applyPerson > 0)
                sb.AppendFormat(" and a.EmpId = {0} ", applyPerson);
            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and (sub.SubNo like '%{0}%' or con.ContractNo like '%{1}%') ", subNo, subNo);
            if (assetId > 0)
                sb.AppendFormat(" and sla.AssertId = {0} ", assetId);
            if (status > 0)
                sb.AppendFormat(" and a.ApplyStatus = {0} ", status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetStockSelectModel(int pageIndex, int pageSize, string orderStr, int pricingId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pd.DetailId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" pd.DetailId,pd.StockId,pd.StockLogId,sname.RefNo,st.StockDate,st.GrossAmount,mu.MUName,a.AssetName,b.BrandName,CONVERT(varchar,st.GrossAmount) + mu.MUName as GrossAmoutName,c.CorpName,ss.StatusName as StockStatusName,Convert(varchar,pd.PricingWeight) + mu2.MUName as PricingWeight,al.alreadyStopLossWeight,dp.DPName,st.CardNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_PricingDetail pd ");
            sb.AppendFormat(" left join (select SUM(ISNULL(slad.StopLossWeight,0)) as alreadyStopLossWeight,slad.StockId from dbo.Pri_StopLossApplyDetail slad where slad.DetailStatus <> {0} group by slad.StockId) as al on pd.StockId = al.StockId ", (int)Common.StatusEnum.已作废);
            sb.Append(" left join dbo.Pri_Pricing p on pd.PricingId = p.PricingId ");
            sb.Append(" left join dbo.Pri_PricingApply apply on p.PricingApplyId = apply.PricingApplyId ");
            sb.Append(" left join dbo.Con_Contract con on apply.ContractId = con.ContractId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu2 on p.MUId = mu2.MUId ");
            sb.Append(" left join dbo.St_Stock st on pd.StockId = st.StockId ");
            sb.Append(" left join dbo.St_StockName sname on st.StockNameId = sname.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on st.AssetId = a.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand b on st.BrandId = b.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = st.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on st.UintId = mu.MUId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c on st.CorpId = c.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = st.StockStatus and ss.StatusId = {0} ", (int)Common.StatusTypeEnum.库存状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" pd.DetailStatus = {0} and pd.PricingId = {1} ", (int)Common.StatusEnum.已完成, pricingId);

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 新增止损申请
        /// </summary>
        /// <param name="user"></param>
        /// <param name="apply"></param>
        /// <param name="stopLossApply"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public ResultModel Create(UserModel user, NFMT.Operate.Model.Apply apply, NFMT.DoPrice.Model.StopLossApply stopLossApply, List<NFMT.DoPrice.Model.StopLossApplyDetail> details)
        {
            ResultModel result = new ResultModel();
            NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
            DAL.StopLossApplyDetailDAL stopLossApplyDetailDAL = new StopLossApplyDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    NFMT.User.Model.Department dept = NFMT.User.UserProvider.Departments.FirstOrDefault(temp => temp.DeptId == apply.ApplyDept);
                    if (dept == null || dept.DeptId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请部门不存在";
                        return result;
                    }

                    //新增主申请
                    apply.EmpId = user.EmpId;
                    apply.ApplyTime = DateTime.Now;
                    apply.ApplyType = NFMT.Operate.ApplyType.StopLossApply;
                    apply.ApplyDept = dept.DeptId;
                    //apply.ApplyDeptName = dept.DeptName;
                    result = applyDAL.Insert(user, apply);
                    if (result.ResultStatus != 0)
                        return result;
                    int applyId = (int)result.ReturnValue;

                    //新增止损申请
                    stopLossApply.ApplyId = applyId;

                    result = stoplossapplyDAL.Insert(user, stopLossApply);
                    if (result.ResultStatus != 0)
                        return result;

                    int stopLossApplyId = (int)result.ReturnValue;

                    if (details.Any())
                    {
                        foreach (Model.StopLossApplyDetail detail in details)
                        {
                            detail.StopLossApplyId = stopLossApplyId;
                            detail.ApplyId = applyId;

                            result = stopLossApplyDetailDAL.Insert(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel HasStopLossApplyDetail(UserModel user, int stopLossApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = stoplossapplyDAL.HasStopLossApplyDetail(user, stopLossApplyId);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public SelectModel GetStockSelectModelForUpdate(int pageIndex, int pageSize, string orderStr, int stopLossApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pd.DetailId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" pd.DetailId,sad.StockId,sad.StockLogId,sname.RefNo,st.StockDate,st.GrossAmount,mu.MUName,a.AssetName,b.BrandName,CONVERT(varchar,st.GrossAmount) + mu.MUName as GrossAmoutName,c.CorpName,ss.StatusName as StockStatusName,Convert(varchar,pd.PricingWeight) + mu.MUName as PricingWeight,sad.StopLossWeight,(al.alreadyStopLossWeight - sad.StopLossWeight) as alreadyStopLossWeight,dp.DPName,st.CardNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_StopLossApplyDetail sad ");
            sb.Append(" left join Pri_StopLossApply apply on sad.StopLossApplyId = apply.StopLossApplyId ");
            sb.Append(" left join dbo.Con_Contract con on apply.ContractId = con.ContractId ");
            sb.Append(" left join dbo.Pri_PricingDetail pd on sad.PricingDetailId = pd.DetailId ");
            sb.AppendFormat(" left join (select SUM(ISNULL(slad.StopLossWeight,0)) as alreadyStopLossWeight,slad.StockId from dbo.Pri_StopLossApplyDetail slad where slad.DetailStatus <> {0} group by slad.StockId) as al on sad.StockId = al.StockId ", (int)Common.StatusEnum.已作废);
            //sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu2 on p.MUId = mu2.MUId ");
            sb.Append(" left join dbo.St_Stock st on sad.StockId = st.StockId ");
            sb.Append(" left join dbo.St_StockName sname on st.StockNameId = sname.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on st.AssetId = a.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand b on st.BrandId = b.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = st.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on st.UintId = mu.MUId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c on st.CorpId = c.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = st.StockStatus and ss.StatusId = {0} ", (int)Common.StatusTypeEnum.库存状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" sad.DetailStatus = {0} and sad.StopLossApplyId = {1} ", (int)Common.StatusEnum.已生效, stopLossApplyId);

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 修改止损申请
        /// </summary>
        /// <param name="user"></param>
        /// <param name="apply"></param>
        /// <param name="stopLossApply"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public ResultModel Update(UserModel user, NFMT.Operate.Model.Apply apply, NFMT.DoPrice.Model.StopLossApply stopLossApply, List<NFMT.DoPrice.Model.StopLossApplyDetail> details)
        {
            ResultModel result = new ResultModel();
            NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
            DAL.StopLossApplyDetailDAL stopLossApplyDetailDAL = new StopLossApplyDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = applyDAL.Get(user, apply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply applyResult = result.ReturnValue as NFMT.Operate.Model.Apply;
                    applyResult.ApplyDept = apply.ApplyDept;
                    applyResult.ApplyCorp = apply.ApplyCorp;
                    applyResult.ApplyDesc = apply.ApplyDesc;

                    result = applyDAL.Update(user, applyResult);
                    if (result.ResultStatus != 0)
                        return result;

                    //修改止损申请
                    result = stoplossapplyDAL.Get(user, stopLossApply.StopLossApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.DoPrice.Model.StopLossApply stopLossApplyResult = result.ReturnValue as NFMT.DoPrice.Model.StopLossApply;
                    stopLossApplyResult.StopLossPrice = stopLossApply.StopLossPrice;
                    stopLossApplyResult.StopLossWeight = stopLossApply.StopLossWeight;
                    stopLossApplyResult.Status = StatusEnum.已录入;

                    result = stoplossapplyDAL.Update(user, stopLossApplyResult);
                    if (result.ResultStatus != 0)
                        return result;

                    if (details.Any())
                    {
                        result = stopLossApplyDetailDAL.InvalidAll(user, stopLossApply.StopLossApplyId);
                        if (result.ResultStatus != 0)
                            return result;

                        foreach (Model.StopLossApplyDetail detail in details)
                        {
                            detail.StopLossApplyId = stopLossApply.StopLossApplyId;
                            detail.ApplyId = apply.ApplyId;

                            result = stopLossApplyDetailDAL.Insert(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel Goback(UserModel user, int stopLossApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取止损申请
                    result = stoplossapplyDAL.Get(user, stopLossApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.DoPrice.Model.StopLossApply stopLossApply = result.ReturnValue as NFMT.DoPrice.Model.StopLossApply;
                    if (stopLossApply == null || stopLossApply.StopLossApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "止损申请验证失败";
                        return result;
                    }

                    //获取申请
                    result = applyDAL.Get(user, stopLossApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请获取失败";
                        return result;
                    }

                    //撤返申请
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
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel Invalid(UserModel user, int stopLossApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
                DAL.StopLossApplyDetailDAL stopLossApplyDetailDAL = new StopLossApplyDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取止损申请
                    result = stoplossapplyDAL.Get(user, stopLossApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.DoPrice.Model.StopLossApply stopLossApply = result.ReturnValue as NFMT.DoPrice.Model.StopLossApply;
                    if (stopLossApply == null || stopLossApply.StopLossApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "止损申请验证失败";
                        return result;
                    }

                    //获取申请
                    result = applyDAL.Get(user, stopLossApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请获取失败";
                        return result;
                    }

                    //作废申请
                    result = applyDAL.Invalid(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废明细
                    result = stopLossApplyDetailDAL.InvalidAll(user, stopLossApplyId);
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

        public ResultModel Confirm(UserModel user, int stopLossApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
                DAL.StopLossApplyDetailDAL stopLossApplyDetailDAL = new StopLossApplyDetailDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证止损申请
                    result = stoplossapplyDAL.Get(user, stopLossApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StopLossApply stopLossApply = result.ReturnValue as Model.StopLossApply;
                    if (stopLossApply == null || stopLossApply.StopLossApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "止损申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, stopLossApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Operate.Model.Apply apply = result.ReturnValue as Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请不存在";
                        return result;
                    }

                    //验证是否执行完成
                    result = stoplossapplyDAL.CheckStopLossCanConfirm(user, stopLossApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Common.StatusEnum status = (Common.StatusEnum)result.ReturnValue;

                    //获取已生效止损申请明细
                    result = stopLossApplyDetailDAL.Load(user, stopLossApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StopLossApplyDetail> details = result.ReturnValue as List<Model.StopLossApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取止损申请明细失败";
                        return result;
                    }

                    //主申请更新状态至已完成
                    if (status == StatusEnum.已完成)
                        result = applyDAL.Confirm(user, apply);
                    else if (status == StatusEnum.部分完成)
                        result = applyDAL.PartiallyConfirm(user, apply);
                    else
                    {
                        result.ResultStatus = -1;
                        result.Message = "更新主申请状态失败";
                        return result;
                    }
                    if (result.ResultStatus != 0)
                        return result;

                    //止损申请明细更新状态至已完成
                    foreach (Model.StopLossApplyDetail detail in details)
                    {
                        //止损申请明细更新状态至已完成
                        result = stopLossApplyDetailDAL.Confirm(user, detail);
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

        public ResultModel ConfirmCancel(UserModel user, int stopLossApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StopLossApplyDetailDAL stopLossApplyDetailDAL = new StopLossApplyDetailDAL();
                Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证止损申请
                    result = stoplossapplyDAL.Get(user, stopLossApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StopLossApply stopLossApply = result.ReturnValue as Model.StopLossApply;
                    if (stopLossApply == null || stopLossApply.StopLossApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "止损申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, stopLossApply.ApplyId);
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

                    //止损申请明细，在已完成状态下的更新至已生效
                    //获取已关闭的明细
                    result = stopLossApplyDetailDAL.Load(user, stopLossApply.StopLossApplyId, Common.StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StopLossApplyDetail> details = result.ReturnValue as List<Model.StopLossApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取申请明细失败";
                        return result;
                    }

                    foreach (Model.StopLossApplyDetail detail in details)
                    {
                        result = stopLossApplyDetailDAL.ConfirmCancel(user, detail);
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

        public ResultModel GetModelByApplyId(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = stoplossapplyDAL.GetModelByApplyId(user, applyId);
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

        #endregion
    }
}
