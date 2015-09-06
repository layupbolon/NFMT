/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoApplyBLL.cs
// 文件功能描述：回购申请dbo.RepoApply业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.WareHouse.Model;
using NFMT.WareHouse.DAL;
using NFMT.WareHouse.IDAL;
using NFMT.Common;
using System.Linq;

namespace NFMT.WareHouse.BLL
{
    /// <summary>
    /// 回购申请dbo.RepoApply业务逻辑类。
    /// </summary>
    public class RepoApplyBLL : Common.ApplyBLL
    {
        private RepoApplyDAL repoapplyDAL = new RepoApplyDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(RepoApplyDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepoApplyBLL()
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
            get { return this.repoapplyDAL; }
        }

        #endregion

        #region 新增方法

        /// <summary>
        /// 回购申请列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="applyPerson"></param>
        /// <param name="status"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int applyPerson, int status, DateTime fromDate, DateTime toDate)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ra.RepoApplyId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)NFMT.Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" ra.RepoApplyId,a.ApplyTime,a.ApplyNo,bd.StatusName,e.Name,d.DeptName,a.ApplyStatus ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_RepoApply ra  inner join dbo.Apply a on ra.ApplyId = a.ApplyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = a.ApplyStatus and bd.StatusId = {0} ", statusId);
            sb.Append(" left join NFMT_User.dbo.Employee e on e.EmpId = a.EmpId ");
            sb.Append(" left join NFMT_User.dbo.Department d on d.DeptId = a.ApplyDept ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (applyPerson > 0)
                sb.AppendFormat(" and a.EmpId = {0} ", applyPerson);
            if (status > 0)
                sb.AppendFormat(" and a.ApplyStatus = {0} ", status);
            if (fromDate > Common.DefaultValue.DefaultTime && toDate >= fromDate)
                sb.AppendFormat(" and a.ApplyTime between '{0}' and '{1}' ", fromDate.ToString(), toDate.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel RepoApplyCreateHandle(UserModel user, Operate.Model.Apply apply, RepoApply repoApply, List<RepoApplyDetail> details)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //写入申请主表
                    NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
                    result = applyDAL.Insert(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    int applyId = (int)result.ReturnValue;
                    repoApply.ApplyId = applyId;

                    //写入回购申请表
                    NFMT.WareHouse.DAL.RepoApplyDAL repoApplyDAL = new RepoApplyDAL();
                    result = repoApplyDAL.Insert(user, repoApply);
                    if (result.ResultStatus != 0)
                        return result;

                    int repoApplyId = (int)result.ReturnValue;

                    //List<Model.StockExclusive> listStockExclusives = new List<StockExclusive>();

                    //写入回购申请明细表
                    NFMT.WareHouse.DAL.RepoApplyDetailDAL detailDAL = new RepoApplyDetailDAL();
                    foreach (Model.RepoApplyDetail detail in details)
                    {
                        detail.RepoApplyId = repoApplyId;
                        result = detailDAL.Insert(user, detail);
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
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        /// <summary>
        /// 回购申请信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="sids"></param>
        /// <param name="applyId"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public SelectModel GetRepoApplyInfoSelect(int pageIndex, int pageSize, string orderStr, string sids, int applyId, int mode)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "st.StockId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " st.StockId,stn.RefNo,st.PaperNo,a.AssetName,convert(varchar,st.GrossAmount) + mu.MUName as GrossAmount,CONVERT(varchar,st.NetAmount) + mu.MUName as NetAmount,cp.CorpName as CorpName,dp.DPName as DPName,stBank.BankName,st.CardNo,bra.BrandName ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_Stock st left join dbo.St_StockName stn on st.StockNameId = stn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on a.AssetId = st.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = st.UintId ");
            sb.Append(" left join NFMT_User.dbo.Corporation cp on st.CorpId=cp.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on st.DeliverPlaceId=dp.DPId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = st.BrandId ");
            sb.Append(" left join ( ");
            sb.Append(" select detail.StockId,p.PledgeBank,bank.BankName ");
            sb.Append(" from NFMT..St_Pledge p ");
            sb.AppendFormat(" inner join NFMT..St_PledgeDetial detail on p.PledgeId = detail.PledgeId and detail.DetailStatus <> {0} ", (int)Common.StatusEnum.已作废);
            sb.Append(" left join NFMT_Basic..Bank bank on bank.BankId = p.PledgeBank ");
            sb.AppendFormat(" where p.PledgeStatus <> {0}) stBank on st.StockId = stBank.StockId  ", (int)Common.StatusEnum.已作废);
            if (mode == 2)
                sb.Append(" left join dbo.St_StockMoveApplyDetail detail on detail.StockId = st.StockId ");
            select.TableName = sb.ToString();

            sb.Clear();

            if (string.IsNullOrEmpty(sids.Trim()))
                sb.Append(" 1=2");
            else
            {
                if (mode == 2)
                    sb.AppendFormat(" detail.DetailStatus = {0} and st.StockId in ({1}) and detail.StockMoveApplyId = {2} ", (int)NFMT.Common.StatusEnum.已生效, sids, applyId);
                else if (mode == 1)
                    sb.AppendFormat("  st.StockId in ({0})  ", sids);
            }


            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 可回购库存列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="sids"></param>
        /// <returns></returns>
        public SelectModel GetCanRepurchaseSelect(int pageIndex, int pageSize, string orderStr, string sids, int repoApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "st.StockId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "st.StockId,stn.RefNo,st.PaperNo,a.AssetName,convert(varchar,st.GrossAmount) + mu.MUName as GrossAmount,CONVERT(varchar,st.NetAmount) + mu.MUName as NetAmount,cp.CorpName as CorpName,dp.DPName as DPName,stBank.BankName,bra.BrandName,st.CardNo ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_Stock st left join dbo.St_StockName stn on st.StockNameId = stn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on a.AssetId = st.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = st.UintId ");
            sb.Append(" left join NFMT_User.dbo.Corporation cp on st.CorpId=cp.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on st.DeliverPlaceId=dp.DPId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = st.BrandId ");
            sb.Append(" left join ( ");
            sb.Append(" select detail.StockId,p.PledgeBank,bank.BankName  ");
            sb.Append(" from NFMT..St_Pledge p ");
            sb.AppendFormat(" inner join NFMT..St_PledgeDetial detail on p.PledgeId = detail.PledgeId and detail.DetailStatus <> {0} ", (int)Common.StatusEnum.已作废);
            sb.Append(" left join NFMT_Basic..Bank bank on bank.BankId = p.PledgeBank ");
            sb.AppendFormat(" where p.PledgeStatus <> {0}) stBank on st.StockId = stBank.StockId  ", (int)Common.StatusEnum.已作废);
            select.TableName = sb.ToString();

            sb.Clear();
            int status = (int)StockStatusEnum.质押库存;
            sb.AppendFormat(" st.StockStatus = {0}", status);
            sb.AppendFormat(" and st.StockId not in (select StockId from dbo.St_RepoApplyDetail where DetailStatus <> {0} ",(int)Common.StatusEnum.已作废);
            if (repoApplyId > 0)
                sb.AppendFormat(" and RepoApplyId <> {0}", repoApplyId);
            sb.Append(")");
            if (!string.IsNullOrEmpty(sids))
                sb.AppendFormat(" and st.StockId not in ({0})", sids);
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetRepoApplyDetailSelect(int pageIndex, int pageSize, string orderStr, int id)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "detail.DetailId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " detail.DetailId,stn.RefNo,st.PaperNo,a.AssetName,convert(varchar,st.GrossAmount) + mu.MUName as GrossAmount,CONVERT(varchar,st.NetAmount) + mu.MUName as NetAmount,cp.CorpName as CorpName,dp.DPName as DPName ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_RepoApplyDetail  detail left join dbo.St_Stock st on st.StockId = detail.StockId ");
            sb.Append("  left join dbo.St_StockName stn on st.StockNameId = stn.StockNameId  ");
            sb.Append("  left join NFMT_Basic.dbo.Asset a on a.AssetId = st.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = st.UintId ");
            sb.Append("   left join NFMT_User.dbo.Corporation cp on st.CorpId=cp.CorpId   ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on st.DeliverPlaceId=dp.DPId  ");
            select.TableName = sb.ToString();

            sb.Clear();

            if (id > 0)
                sb.AppendFormat("  detail.DetailId = {0} ", id);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel RepoApplyInvalid(UserModel user, int repoApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取回购申请
                    NFMT.WareHouse.DAL.RepoApplyDAL repoApplyDAL = new RepoApplyDAL();
                    result = repoApplyDAL.Get(user, repoApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.RepoApply repoApply = result.ReturnValue as NFMT.WareHouse.Model.RepoApply;

                    //获取申请主表
                    NFMT.Operate.BLL.ApplyBLL applyBLL = new Operate.BLL.ApplyBLL();
                    result = applyBLL.Get(user, repoApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;

                    //作废申请主表
                    result = applyBLL.Invalid(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废回购申请明细
                    NFMT.WareHouse.DAL.RepoApplyDetailDAL repoApplyDetailDAL = new RepoApplyDetailDAL();
                    result = repoApplyDetailDAL.Invalid(user, repoApplyId, string.Empty);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.DAL.StockExclusiveDAL stockExclusiveDAL = new StockExclusiveDAL();
                    result = stockExclusiveDAL.Invalid(user, apply.ApplyId, repoApplyId, string.Empty);
                    if (result.ResultStatus != 0)
                        return result;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.Message = string.Format("操作失败,{0}", ex.Message);
                result.ResultStatus = -1;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel RepoApplyUpdateHandle(UserModel user, NFMT.Operate.Model.Apply apply, int repoApplyId, List<Model.RepoApplyDetail> details)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
                    result = applyDAL.Get(user, apply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply applyRes = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (applyRes == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取申请失败";
                        return result;
                    }

                    applyRes.ApplyTime = apply.ApplyTime;
                    applyRes.EmpId = apply.EmpId;
                    applyRes.ApplyDept = apply.ApplyDept;
                    applyRes.ApplyCorp = apply.ApplyCorp;
                    applyRes.ApplyDesc = apply.ApplyDesc;

                    result = applyDAL.Update(user, applyRes);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.DAL.RepoApplyDetailDAL repoApplyDetailDAL = new RepoApplyDetailDAL();
                    result = repoApplyDetailDAL.InvalidAll(user, repoApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    foreach (Model.RepoApplyDetail detail in details)
                    {
                        result = repoApplyDetailDAL.Insert(user, detail);
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
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        /// <summary>
        /// 可回购申请列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="applyDept"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public SelectModel GetCanRepoApplySelectModel(int pageIndex, int pageSize, string orderStr, int applyDept, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ra.RepoApplyId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)NFMT.Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" ra.RepoApplyId,a.ApplyNo,a.ApplyTime,bd.StatusName,e.Name,d.DeptName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_RepoApply ra  ");
            sb.Append(" left join dbo.Apply a on ra.ApplyId = a.ApplyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = a.ApplyStatus and bd.StatusId = {0} ", statusId);
            sb.Append(" left join NFMT_User.dbo.Employee e on e.EmpId = a.EmpId ");
            sb.Append(" left join NFMT_User.dbo.Department d on d.DeptId = a.ApplyDept ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" a.ApplyStatus = {0} and ra.RepoApplyId in (select a.RepoApplyId from St_RepoApplyDetail a left join St_RepoDetail b on a.DetailId = b.RepoApplyDetailId and isnull(b.RepoDetailStatus,0) not in ({1},{2}) and b.DetailId is null where a.DetailStatus <> {1} ) ", (int)NFMT.Common.StatusEnum.已生效, (int)Common.StatusEnum.已作废, (int)Common.StatusEnum.已关闭);

            if (applyDept > 0)
                sb.AppendFormat(" and d.DeptId = {0} ", applyDept);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd >= applyTimeBegin)
                sb.AppendFormat(" and a.ApplyTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Complete(UserModel user, RepoApply repoApply)
        {
            throw new NotImplementedException();
        }

        public ResultModel Update(UserModel user, int repoApplyId, List<int> stockIds, string memo, int deptId)
        {
            ResultModel result = new ResultModel();

            //获取质押申请
            try
            {
                DAL.StockDAL stockDAL = new StockDAL();
                DAL.RepoApplyDetailDAL detailDAL = new RepoApplyDetailDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证质押申请
                    result = repoapplyDAL.Get(user, repoApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.RepoApply repoApply = result.ReturnValue as Model.RepoApply;
                    if (repoApply == null || repoApply.RepoApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "质押申请获取失败";
                        return result;
                    }

                    //获取主申请
                    result = applyDAL.Get(user, repoApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请获取失败";
                        return result;
                    }

                    //更新主申请
                    NFMT.User.Model.Department dept = NFMT.User.UserProvider.Departments.FirstOrDefault(temp => temp.DeptId == deptId);
                    if (dept == null || dept.DeptId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请部门不存在";
                        return result;
                    }

                    apply.ApplyDesc = memo;
                    apply.ApplyDept = dept.DeptId;
                    //apply.ApplyDeptName = dept.DeptName;
                    result = applyDAL.Update(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    //获了质押申请明细
                    result = detailDAL.Load(user, repoApply.RepoApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    List<Model.RepoApplyDetail> details = result.ReturnValue as List<Model.RepoApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "明细获取失败";
                        return result;
                    }

                    //作废明细
                    foreach (Model.RepoApplyDetail detail in details)
                    {
                        if (detail.DetailStatus == StatusEnum.已生效)
                            detail.DetailStatus = StatusEnum.已录入;
                        result = detailDAL.Invalid(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取库存
                    foreach (int stockId in stockIds)
                    {
                        result = stockDAL.Get(user, stockId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.Stock stock = result.ReturnValue as Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存获取失败";
                            return result;
                        }

                        if (stock.StockStatus != StockStatusEnum.质押库存)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存非质押状态，不能进行回购操作";
                            return result;
                        }

                        //添加明细
                        Model.RepoApplyDetail detail = new RepoApplyDetail();
                        detail.DetailStatus = StatusEnum.已生效;
                        detail.RepoApplyId = repoApply.RepoApplyId;
                        detail.StockId = stock.StockId;
                        result = detailDAL.Insert(user, detail);
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
            return this.repoapplyDAL.GetByApplyId(user, applyId);
        }

        public ResultModel Goback(UserModel user, int repoApplyId)
        {
            ResultModel result = new ResultModel();
            try
            {
                DAL.RepoApplyDAL repoApplyDAL = new RepoApplyDAL();
                Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证回购申请
                    result = repoApplyDAL.Get(user, repoApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.RepoApply repoApply = result.ReturnValue as Model.RepoApply;
                    if (repoApply == null || repoApply.RepoApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "回购申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, repoApply.ApplyId);
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
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel Invalid(UserModel user, int repoApplyId)
        {
            ResultModel result = new ResultModel();

            DAL.StockDAL stockDAL = new StockDAL();
            DAL.RepoApplyDAL repoApplyDAL = new RepoApplyDAL();
            Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
            DAL.RepoApplyDetailDAL detailDAL = new RepoApplyDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证出库申请
                    result = repoApplyDAL.Get(user, repoApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.RepoApply repoApply = result.ReturnValue as Model.RepoApply;
                    if (repoApply == null || repoApply.RepoApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "回购申请不存在";
                        return result;
                    }

                    //获取主申请实体
                    result = applyDAL.Get(user, repoApply.ApplyId);
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
                    result = detailDAL.Load(user, repoApply.RepoApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<RepoApplyDetail> details = result.ReturnValue as List<RepoApplyDetail>;
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
                    foreach (Model.RepoApplyDetail detail in details)
                    {
                        if (detail.DetailStatus == StatusEnum.已生效)
                            detail.DetailStatus = StatusEnum.已录入;
                        result = detailDAL.Invalid(user, detail);
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

        public ResultModel Confirm(UserModel user, int repoApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockDAL stockDAL = new StockDAL();
                DAL.RepoApplyDAL repoApplyDAL = new RepoApplyDAL();
                Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
                DAL.RepoApplyDetailDAL detailDAL = new RepoApplyDetailDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证回购申请
                    result = repoApplyDAL.Get(user, repoApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.RepoApply repoApply = result.ReturnValue as Model.RepoApply;
                    if (repoApply == null || repoApply.RepoApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "回购申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, repoApply.ApplyId);
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
                    result = repoapplyDAL.CheckStockOutCanConfirm(user, repoApply.RepoApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Common.StatusEnum status = (Common.StatusEnum)result.ReturnValue;

                    //获取已生效回购申请明细
                    result = detailDAL.Load(user, repoApply.RepoApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.RepoApplyDetail> details = result.ReturnValue as List<Model.RepoApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取回购申请明细失败";
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

                    //回购申请明细更新状态至已完成
                    foreach (Model.RepoApplyDetail detail in details)
                    {
                        //回购申请明细更新状态至已完成
                        result = detailDAL.Confirm(user, detail);
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

        public ResultModel ConfirmCancel(UserModel user, int repoApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.RepoApplyDAL repoApplyDAL = new RepoApplyDAL();
                DAL.RepoApplyDetailDAL repoApplyDetailDAL = new RepoApplyDetailDAL();
                Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证回购申请
                    result = repoApplyDAL.Get(user, repoApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.RepoApply repoApply = result.ReturnValue as Model.RepoApply;
                    if (repoApply == null || repoApply.RepoApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "回购申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, repoApply.ApplyId);
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

                    //回购申请明细，在已完成状态下的更新至已生效
                    //获取已关闭的明细
                    result = repoApplyDetailDAL.Load(user, repoApply.RepoApplyId, Common.StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.RepoApplyDetail> details = result.ReturnValue as List<Model.RepoApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取申请明细失败";
                        return result;
                    }

                    foreach (Model.RepoApplyDetail detail in details)
                    {
                        result = repoApplyDetailDAL.ConfirmCancel(user, detail);
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

        public ResultModel GetPledgeStockId(UserModel user, int repoApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = repoapplyDAL.GetPledgeStockId(user, repoApplyId);
            }
            catch (Exception ex)
            {
                result.Message = string.Format("操作失败,{0}", ex.Message);
                result.ResultStatus = -1;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        #endregion
    }
}
