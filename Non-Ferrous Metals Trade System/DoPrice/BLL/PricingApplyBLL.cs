/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PricingApplyBLL.cs
// 文件功能描述：点价申请dbo.Pri_PricingApply业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年9月2日
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
    /// 点价申请dbo.Pri_PricingApply业务逻辑类。
    /// </summary>
    public class PricingApplyBLL : Common.ApplyBLL
    {
        private PricingApplyDAL pricingapplyDAL = new PricingApplyDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PricingApplyDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PricingApplyBLL()
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
            get { return this.pricingapplyDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int applyPerson, string subNo, int assetId, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pa.PricingApplyId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" pa.PricingApplyId,a.ApplyStatus,e.Name,d.DeptName,c2.CorpName as ApplyCorp,a.ApplyDesc,pa.StartTime,pa.EndTime,sub.SubNo,asset.AssetName,CONVERT(varchar,pa.PricingWeight) + mu.MUName as PricingWeight,c.CorpName,pp.Job,pp.PricingPhone,ss.StatusName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_PricingApply pa  ");
            sb.Append(" left join dbo.Apply a on pa.ApplyId = a.ApplyId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on a.EmpId = e.EmpId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c2 on c2.CorpId = a.ApplyCorp ");
            sb.Append(" left join NFMT_User.dbo.Department d on a.ApplyDept = d.DeptId ");
            sb.Append(" left join dbo.Con_ContractSub sub on pa.SubContractId = sub.SubId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset asset on pa.AssertId = asset.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on pa.MUId = mu.MUId ");
            sb.Append(" left join dbo.Pri_PricingPerson pp on pa.PricingPersoinId = pp.PersoinId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c on pp.CorpId = c.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = a.ApplyStatus and ss.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (applyPerson > 0)
                sb.AppendFormat(" and a.EmpId = {0} ", applyPerson);
            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and sub.SubNo like '%{0}%' ", subNo);
            if (assetId > 0)
                sb.AppendFormat(" and pa.AssertId = {0} ", assetId);
            if (status > 0)
                sb.AppendFormat(" and a.ApplyStatus = {0} ", status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetStockJson(int pageIndex, int pageSize, string orderStr, int subId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "slog.StockLogId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" slog.StockLogId,st.StockId,sname.RefNo,st.StockDate,slog.GrossAmount,mu.MUName,a.AssetName,b.BrandName,CONVERT(varchar,slog.GrossAmount) + mu.MUName as GrossAmoutName,c.CorpName,ss.StatusName as StockStatusName,dp.DPName,st.CardNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockLog slog ");
            sb.Append(" left join dbo.St_Stock st on slog.StockId = st.StockId ");
            sb.Append(" left join dbo.St_StockName sname on slog.StockNameId = sname.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on st.AssetId = a.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand b on st.BrandId = b.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = st.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on slog.MUId = mu.MUId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c on st.CorpId = c.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = st.StockStatus and ss.StatusId = {0} ", (int)Common.StatusTypeEnum.库存状态);
            sb.Append(" left join dbo.Con_Contract con on slog.ContractId = con.ContractId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" slog.SubContractId = {0} ", subId);
            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Create(UserModel user, NFMT.Operate.Model.Apply apply, NFMT.DoPrice.Model.PricingApply pricingApply, List<NFMT.DoPrice.Model.PricingApplyDetail> pricingApplyDetails)
        {
            ResultModel result = new ResultModel();
            NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
            DAL.PricingApplyDAL pricingApplyDAL = new PricingApplyDAL();
            DAL.PricingApplyDetailDAL pricingApplyDetailDAL = new PricingApplyDetailDAL();
            NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
            NFMT.Contract.DAL.ContractDAL contractDAL = new Contract.DAL.ContractDAL();
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
                    apply.ApplyType = NFMT.Operate.ApplyType.PricingApply;
                    apply.ApplyDept = dept.DeptId;
                    result = applyDAL.Insert(user, apply);
                    if (result.ResultStatus != 0)
                        return result;
                    int applyId = (int)result.ReturnValue;
                    pricingApply.ApplyId = applyId;

                    //验证子合约
                    result = subDAL.Get(user, pricingApply.SubContractId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约不存在";
                        return result;
                    }

                    if (sub.SubStatus != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约状态非已生效，不能进行点价申请";
                        return result;
                    }

                    //decimal missRate = Convert.ToDecimal(0.05);
                    //if (pricingApply.PricingWeight > sub.SignAmount)
                    //{
                    //    result.ResultStatus = -1;
                    //    result.Message = "点价重量超过子合约签订数量";
                    //    return result;
                    //}

                    //获取合约
                    result = contractDAL.Get(user, sub.ContractId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                    if (contract == null || contract.ContractId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "关联合约不存在";
                        return result;
                    }

                    if (contract.TradeDirection == (int)NFMT.Contract.TradeDirectionEnum.Buy)
                        pricingApply.PricingDirection = (int)NFMT.DoPrice.PricingDirection.空头;
                    else
                        pricingApply.PricingDirection = (int)NFMT.DoPrice.PricingDirection.多头;

                    //新增点价申请
                    result = pricingApplyDAL.Insert(user, pricingApply);
                    if (result.ResultStatus != 0)
                        return result;

                    int pricingApplyId = (int)result.ReturnValue;

                    foreach (Model.PricingApplyDetail detail in pricingApplyDetails)
                    {
                        detail.PricingApplyId = pricingApplyId;
                        detail.DetailStatus = StatusEnum.已生效;
                        result = pricingApplyDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
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

        public SelectModel GetUpdateSelectModel(int pageIndex, int pageSize, string orderStr, int pricingApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "slog.StockLogId desc";
            else
                select.OrderStr = orderStr;

            int status = (int)Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" slog.StockLogId,st.StockId,sname.RefNo,st.StockDate,slog.GrossAmount,mu.MUName,a.AssetName,b.BrandName,CONVERT(varchar,slog.GrossAmount) + mu.MUName as GrossAmoutName,c.CorpName,ss.StatusName as StockStatusName ");
            sb.AppendFormat(",(select top 1 detail.PricingWeight from dbo.Pri_PricingApplyDetail detail where detail.StockId = st.StockId and detail.DetailStatus = {0} and detail.PricingApplyId = {1}) as PricingWeight ", status, pricingApplyId);
            sb.Append(",st.CardNo,dp.DPName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockLog slog ");
            sb.Append(" left join dbo.St_Stock st on slog.StockId = st.StockId ");
            sb.Append(" left join dbo.St_StockName sname on slog.StockNameId = sname.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on st.AssetId = a.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand b on st.BrandId = b.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = st.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on st.UintId = mu.MUId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c on st.CorpId = c.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = st.StockStatus and ss.StatusId = {0} ", (int)Common.StatusTypeEnum.库存状态);
            sb.Append(" left join dbo.Con_Contract con on slog.ContractId = con.ContractId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" slog.SubContractId = (select SubContractId from dbo.Pri_PricingApply where PricingApplyId = {0}) ", pricingApplyId);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Update(UserModel user, NFMT.Operate.Model.Apply apply, NFMT.DoPrice.Model.PricingApply pricingApply, List<NFMT.DoPrice.Model.PricingApplyDetail> pricingApplyDetails)
        {
            ResultModel result = new ResultModel();
            NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
            DAL.PricingApplyDAL pricingApplyDAL = new PricingApplyDAL();
            DAL.PricingApplyDetailDAL pricingApplyDetailDAL = new PricingApplyDetailDAL();
            NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
            NFMT.Contract.DAL.ContractDAL contractDAL = new Contract.DAL.ContractDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = applyDAL.Get(user, apply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    NFMT.Operate.Model.Apply getApply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (getApply == null || getApply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取申请错误";
                        return result;
                    }

                    NFMT.User.Model.Department dept = NFMT.User.UserProvider.Departments.FirstOrDefault(temp => temp.DeptId == apply.ApplyDept);
                    if (dept == null || dept.DeptId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请部门不存在";
                        return result;
                    }

                    getApply.EmpId = user.EmpId;
                    getApply.ApplyTime = DateTime.Now;
                    getApply.ApplyDept = apply.ApplyDept;
                    getApply.ApplyCorp = apply.ApplyCorp;
                    getApply.ApplyDesc = apply.ApplyDesc;
                    result = applyDAL.Update(user, getApply);
                    if (result.ResultStatus != 0)
                        return result;

                    result = pricingApplyDAL.Get(user, pricingApply.PricingApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.DoPrice.Model.PricingApply getPricingApply = result.ReturnValue as NFMT.DoPrice.Model.PricingApply;
                    if (getPricingApply == null || getPricingApply.PricingApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取点价申请错误";
                        return result;
                    }

                    getPricingApply.StartTime = pricingApply.StartTime;
                    getPricingApply.EndTime = pricingApply.EndTime;
                    getPricingApply.MinPrice = pricingApply.MinPrice;
                    getPricingApply.MaxPrice = pricingApply.MaxPrice;
                    getPricingApply.CurrencyId = pricingApply.CurrencyId;
                    getPricingApply.PricingCorpId = pricingApply.PricingCorpId;
                    getPricingApply.PricingWeight = pricingApply.PricingWeight;
                    getPricingApply.MUId = pricingApply.MUId;
                    getPricingApply.AssertId = pricingApply.AssertId;
                    getPricingApply.PricingPersoinId = pricingApply.PricingPersoinId;

                    //验证子合约
                    result = subDAL.Get(user, pricingApply.SubContractId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约不存在";
                        return result;
                    }

                    if (sub.SubStatus != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约状态非已生效，不能进行点价申请";
                        return result;
                    }

                    //获取合约
                    result = contractDAL.Get(user, sub.ContractId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                    if (contract == null || contract.ContractId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "关联合约不存在";
                        return result;
                    }

                    if (contract.TradeDirection == (int)NFMT.Contract.TradeDirectionEnum.Buy)
                        getPricingApply.PricingDirection = (int)NFMT.DoPrice.PricingDirection.空头;
                    else
                        getPricingApply.PricingDirection = (int)NFMT.DoPrice.PricingDirection.多头;

                    result = pricingApplyDAL.Update(user, getPricingApply);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废明细
                    result = pricingApplyDetailDAL.InvalidAll(user, getPricingApply.PricingApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    //写入明细
                    foreach (Model.PricingApplyDetail detail in pricingApplyDetails)
                    {
                        detail.PricingApplyId = getPricingApply.PricingApplyId;
                        detail.DetailStatus = StatusEnum.已生效;

                        result = pricingApplyDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
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

        public SelectModel GetDetailSelectModel(int pageIndex, int pageSize, string orderStr, int pricingApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "st.StockId asc";
            else
                select.OrderStr = orderStr;

            int status = (int)Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" detail.DetailId,detail.StockId,sname.RefNo,st.StockDate,slog.GrossAmount,mu.MUName,a.AssetName,b.BrandName,CONVERT(varchar,slog.GrossAmount) + mu.MUName as GrossAmoutName,c.CorpName,ss.StatusName as StockStatusName,CONVERT(varchar,detail.PricingWeight)+mu.MUName as PricingWeight,dp.DPName,st.CardNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_PricingApplyDetail detail ");
            sb.Append(" left join dbo.Pri_PricingApply apply on detail.PricingApplyId = apply.PricingApplyId ");
            sb.Append(" left join dbo.Con_Contract con on apply.ContractId = con.ContractId ");
            sb.Append(" left join dbo.St_Stock st on detail.StockId = st.StockId ");
            sb.Append(" left join dbo.St_StockLog slog on detail.StockLogId = slog.StockLogId ");
            sb.Append(" left join dbo.St_StockName sname on st.StockNameId = sname.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on st.AssetId = a.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand b on st.BrandId = b.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = st.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on st.UintId = mu.MUId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c on st.CorpId = c.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = st.StockStatus and ss.StatusId = {0} ", (int)Common.StatusTypeEnum.库存状态);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = detail.DetailStatus and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" detail.PricingApplyId = {0} and detail.DetailStatus >= {1} ", pricingApplyId, status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Goback(UserModel user, int pricingApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.PricingApplyDAL pricingApplyDAL = new PricingApplyDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取点价申请
                    result = pricingApplyDAL.Get(user, pricingApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.DoPrice.Model.PricingApply pricingApply = result.ReturnValue as NFMT.DoPrice.Model.PricingApply;
                    if (pricingApply == null || pricingApply.PricingApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "点价申请验证失败";
                        return result;
                    }

                    //获取申请
                    result = applyDAL.Get(user, pricingApply.ApplyId);
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

        public ResultModel Invalid(UserModel user, int pricingApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.PricingApplyDAL pricingApplyDAL = new PricingApplyDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
                DAL.PricingApplyDetailDAL pricingApplyDetailDAL = new PricingApplyDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取点价申请
                    result = pricingApplyDAL.Get(user, pricingApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.DoPrice.Model.PricingApply pricingApply = result.ReturnValue as NFMT.DoPrice.Model.PricingApply;
                    if (pricingApply == null || pricingApply.PricingApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "点价申请验证失败";
                        return result;
                    }

                    //获取申请
                    result = applyDAL.Get(user, pricingApply.ApplyId);
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
                    result = pricingApplyDetailDAL.InvalidAll(user, pricingApplyId);
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

        public ResultModel Confirm(UserModel user, int pricingApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.PricingApplyDAL pricingApplyDAL = new PricingApplyDAL();
                Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
                DAL.PricingApplyDetailDAL pricingApplyDetailDAL = new PricingApplyDetailDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证点价申请
                    result = pricingApplyDAL.Get(user, pricingApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PricingApply pricingApply = result.ReturnValue as Model.PricingApply;
                    if (pricingApply == null || pricingApply.PricingApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "点价申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, pricingApply.ApplyId);
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
                    result = pricingApplyDAL.CheckPledgeCanConfirm(user, pricingApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Common.StatusEnum status = (Common.StatusEnum)result.ReturnValue;

                    //获取已生效点价申请明细
                    result = pricingApplyDetailDAL.Load(user, pricingApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.PricingApplyDetail> details = result.ReturnValue as List<Model.PricingApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取点价申请明细失败";
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

                    //点价申请明细更新状态至已完成
                    foreach (Model.PricingApplyDetail detail in details)
                    {
                        //点价申请明细更新状态至已完成
                        result = pricingApplyDetailDAL.Confirm(user, detail);
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

        public ResultModel ConfirmCancel(UserModel user, int pricingApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.PricingApplyDAL pricingApplyDAL = new PricingApplyDAL();
                DAL.PricingApplyDetailDAL pricingApplyDetailDAL = new PricingApplyDetailDAL();
                Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证点价申请
                    result = pricingApplyDAL.Get(user, pricingApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PricingApply pricingApply = result.ReturnValue as Model.PricingApply;
                    if (pricingApply == null || pricingApply.PricingApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "点价申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, pricingApply.ApplyId);
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

                    //点价申请明细，在已完成状态下的更新至已生效
                    //获取已关闭的明细
                    result = pricingApplyDetailDAL.Load(user, pricingApply.PricingApplyId, Common.StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.PricingApplyDetail> details = result.ReturnValue as List<Model.PricingApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取申请明细失败";
                        return result;
                    }

                    foreach (Model.PricingApplyDetail detail in details)
                    {
                        result = pricingApplyDetailDAL.ConfirmCancel(user, detail);
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

        public SelectModel GetDoPriceInfoSelectModel(int pageIndex, int pageSize, string orderStr, int pricingApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "p.PricingId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" p.PricingId,e.Name,ex.ExchangeName,fcode.TradeCode,a.AssetName,CONVERT(varchar,p.PricingWeight) + m.MUName as PricingWeight,CONVERT(varchar,p.AvgPrice)+ c.CurrencyName as AvgPrice,p.PricingTime,bd.StatusName,p.PricingStatus ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_Pricing p ");
            sb.Append(" left join dbo.Pri_PricingApply apply on p.PricingApplyId = apply.PricingApplyId ");
            sb.Append(" left join dbo.Con_Contract con on apply.ContractId = con.ContractId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on p.Pricinger = e.EmpId ");
            sb.Append(" left join NFMT_Basic.dbo.Exchange ex on p.ExchangeId = ex.ExchangeId ");
            sb.Append(" left join NFMT_Basic.dbo.FuturesCode fcode on p.FuturesCodeId = fcode.FuturesCodeId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on p.AssertId = a.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit m on p.MUId = m.MUId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on p.CurrencyId = c.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = p.PricingStatus and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");
            if (pricingApplyId > 0)
                sb.AppendFormat(" and p.PricingApplyId = {0} ", pricingApplyId);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetCanApplyContractListSelect(int pageIndex, int pageSize, string orderStr, string subNo, int outCorpId, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "soa.StockOutApplyId asc";
            else
                select.OrderStr = orderStr;

            int status = (int)NFMT.Common.StatusEnum.已生效;
            int statusType = (int)NFMT.Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" con.ContractId,cs.SubId,cs.ContractDate,con.ContractNo,cs.SubNo,cast (cs.SignAmount as varchar) + mu.MUName as SignWeight,inccd.CorpName as InCorpName,outccd.CorpName as OutCorpName,a.AssetName,cs.SubStatus,sd.StatusName,con.TradeDirection,tradeDir.DetailName as TradeDirectionName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Con_ContractSub cs ");
            sb.Append(" left join dbo.Con_Contract con on con.ContractId = cs.ContractId ");
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail inccd on inccd.ContractId = con.ContractId and inccd.IsDefaultCorp = 1 and inccd.IsInnerCorp = 1 and inccd.DetailStatus= {0} ", status);
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail outccd on outccd.ContractId = con.ContractId and outccd.IsDefaultCorp = 1 and outccd.IsInnerCorp = 0 and outccd.DetailStatus={0} ", status);
            sb.Append(" left join NFMT_Basic.dbo.Asset a on con.AssetId = a.AssetId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on cs.SubStatus = sd.DetailId and sd.StatusId = {0} ", statusType);
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on cs.UnitId = mu.MUId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail tradeDir on tradeDir.StyleDetailId = con.TradeDirection ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" con.ContractStatus = {0} and cs.SubStatus = {0} and con.PriceMode = {1} ", status, (int)NFMT.Contract.PriceModeEnum.点价);
            sb.AppendFormat(" and cs.SignAmount > (select ISNULL(SUM(pa.PricingWeight),0) from dbo.Pri_PricingApply pa left join dbo.Apply app on pa.ApplyId = app.ApplyId where pa.SubContractId = cs.SubId and app.ApplyStatus <> {0}) ", (int)Common.StatusEnum.已作废);

            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and cs.SubNo like '%{0}%'", subNo);
            if (outCorpId > 0)
                sb.AppendFormat("  and outccd.CorpId ={0}", outCorpId);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and cs.CreateTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays
                    (1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel GetAlreadyPricingWeight(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = pricingapplyDAL.GetAlreadyPricingWeight(user, subId);
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

        public ResultModel GetModelByApplyId(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = pricingapplyDAL.GetModelByApplyId(user, applyId);
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

        public ResultModel CheckContractSubPricingApplyConfirm(UserModel user, int subId)
        {
            return this.pricingapplyDAL.CheckContractSubPricingApplyConfirm(user, subId);
        }

        #endregion
    }
}
