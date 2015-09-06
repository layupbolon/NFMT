/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeApplyBLL.cs
// 文件功能描述：质押申请dbo.PledgeApply业务逻辑类。
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

namespace NFMT.WareHouse.BLL
{
    /// <summary>
    /// 质押申请dbo.PledgeApply业务逻辑类。
    /// </summary>
    public class PledgeApplyBLL : Common.ApplyBLL
    {
        private PledgeApplyDAL pledgeapplyDAL = new PledgeApplyDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PledgeApplyDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PledgeApplyBLL()
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
            get { return this.pledgeapplyDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int applyPerson, int status, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pa.PledgeApplyId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)NFMT.Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" pa.PledgeApplyId,app.ApplyNo,app.ApplyTime,app.ApplyDesc,bd.StatusName,bank.BankName,app.ApplyStatus,e.Name,d.DeptName,");
            sb.AppendFormat("convert(varchar,(select SUM(ISNULL(GrossAmount,0)) from dbo.St_PledgeDetial where DetailStatus in ({0},{1}) and PledgeId in (select PledgeId from dbo.St_Pledge where PledgeApplyId = pa.PledgeApplyId)))+mu.MUName as ExecAmount", (int)Common.StatusEnum.已生效, (int)Common.StatusEnum.已完成);
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_PledgeApply pa left join dbo.Apply app on pa.ApplyId = app.ApplyId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank bank on pa.PledgeBank = bank.BankId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = app.ApplyStatus and bd.StatusId = {0} ", statusId);
            sb.Append(" left join NFMT_User.dbo.Employee e on e.EmpId = app.EmpId ");
            sb.Append(" left join NFMT_User.dbo.Department d on d.DeptId = app.ApplyDept ");
            sb.Append(" left join (select distinct PledgeApplyId,UintId from NFMT.dbo.St_PledgeApplyDetail) detail on detail.PledgeApplyId = pa.PledgeApplyId   ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = detail.UintId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (applyPerson > 0)
                sb.AppendFormat(" and app.EmpId = {0} ", applyPerson);
            if (status > 0)
                sb.AppendFormat(" and app.ApplyStatus = {0} ", status);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd >= applyTimeBegin)
                sb.AppendFormat(" and app.ApplyTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel PledgeApplyCreateHandle(UserModel user, NFMT.Operate.Model.Apply apply, Model.PledgeApply pledgeApply, List<Model.PledgeApplyDetail> pledgeApplyDetails)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
                    result = applyDAL.Insert(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    int applyId = (int)result.ReturnValue;
                    pledgeApply.ApplyId = applyId;

                    NFMT.WareHouse.DAL.PledgeApplyDAL pledgeApplyDAL = new PledgeApplyDAL();
                    result = pledgeApplyDAL.Insert(user, pledgeApply);
                    if (result.ResultStatus != 0)
                        return result;

                    int pledgeApplyId = (int)result.ReturnValue;


                    NFMT.WareHouse.DAL.PledgeApplyDetailDAL detailDAL = new PledgeApplyDetailDAL();
                    foreach (Model.PledgeApplyDetail detail in pledgeApplyDetails)
                    {
                        detail.PledgeApplyId = pledgeApplyId;
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

        public SelectModel GetPledgeApplyInfoSelect(int pageIndex, int pageSize, string orderStr, string sids, int pledgeApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "st.StockId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " st.StockId,stn.RefNo,st.PaperNo,a.AssetName,convert(varchar,st.GrossAmount) + mu.MUName as GrossAmount,CONVERT(varchar,st.NetAmount) + mu.MUName as NetAmount,pad.ApplyQty,pad.UintId,pad.PledgePrice,pad.CurrencyId,cur.CurrencyName ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append(" dbo.St_PledgeApplyDetail pad left join");
            sb.Append(" dbo.St_Stock st on pad.StockId = st.StockId ");
            sb.Append(" left join dbo.St_StockName stn on st.StockNameId = stn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on a.AssetId = st.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = st.UintId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = pad.CurrencyId");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" pad.PledgeApplyId = {0} and pad.DetailStatus = {1}", pledgeApplyId, (int)Common.StatusEnum.已生效);

            if (string.IsNullOrEmpty(sids.Trim()))
                sb.Append(" and 1=2");
            else
                sb.AppendFormat(" and pad.StockId in ({0}) ", sids);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel PledgeApplyUpdateHandle(UserModel user, NFMT.Operate.Model.Apply apply, Model.PledgeApply pledgeApply, List<Model.PledgeApplyDetail> details)
        {
            ResultModel result = new ResultModel(); 
            NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = pledgeapplyDAL.Get(user, pledgeApply.PledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.PledgeApply pledgeApplyRe = result.ReturnValue as NFMT.WareHouse.Model.PledgeApply;
                    if (pledgeApplyRe == null || pledgeApplyRe.PledgeApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = applyDAL.Get(user, apply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply applyRe = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (result.ResultStatus != 0)
                        return result;
                    applyRe.EmpId = apply.EmpId;
                    applyRe.ApplyTime = apply.ApplyTime;
                    applyRe.ApplyDept = apply.ApplyDept;
                    applyRe.ApplyCorp = apply.ApplyCorp;
                    applyRe.ApplyDesc = apply.ApplyDesc;

                    //更新主申请
                    result = applyDAL.Update(user, applyRe);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.DAL.PledgeApplyDetailDAL pledgeApplyDetailDAL = new PledgeApplyDetailDAL();
                    //作废所有明细
                    result = pledgeApplyDetailDAL.InvalidAll(user, pledgeApply.PledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.DAL.StockDAL stockDAL = new StockDAL();
                    foreach(Model.PledgeApplyDetail detail in details)
                    {
                        //写入质押申请明细
                        detail.PledgeApplyId = pledgeApply.PledgeApplyId;
                        result = pledgeApplyDetailDAL.Insert(user, detail);
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
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public SelectModel GetPledgeApplyDetailSelect(int pageIndex, int pageSize, string orderStr, int id)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "detail.DetailId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " detail.DetailId,stn.RefNo,st.PaperNo,a.AssetName,convert(varchar,st.GrossAmount) + mu.MUName as GrossAmount,CONVERT(varchar,st.NetAmount) + mu.MUName as NetAmount ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_PledgeApplyDetail detail left join dbo.St_Stock st on st.StockId = detail.StockId ");
            sb.Append(" left join dbo.St_StockName stn on st.StockNameId = stn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on a.AssetId = st.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = st.UintId ");
            select.TableName = sb.ToString();

            sb.Clear();

            if (id > 0)
                sb.AppendFormat("  detail.PledgeApplyId = {0} and detail.DetailStatus <> {1} ", id, (int)Common.StatusEnum.已作废);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel PledgeApplyInvalid(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    NFMT.WareHouse.DAL.PledgeApplyDAL pledgeApplyDAL = new PledgeApplyDAL();
                    result = pledgeApplyDAL.Get(user, pledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.PledgeApply pledgeApply = result.ReturnValue as NFMT.WareHouse.Model.PledgeApply;

                    NFMT.Operate.BLL.ApplyBLL applyBLL = new Operate.BLL.ApplyBLL();
                    result = applyBLL.Get(user, pledgeApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;

                    result = applyBLL.Invalid(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.DAL.PledgeApplyDetailDAL pledgeApplyDetailDAL = new PledgeApplyDetailDAL();
                    result = pledgeApplyDetailDAL.Invalid(user, pledgeApplyId, string.Empty);
                    if (result.ResultStatus != 0)
                        return result;

                    //NFMT.WareHouse.DAL.StockExclusiveDAL stockExclusiveDAL = new StockExclusiveDAL();
                    //result = stockExclusiveDAL.Invalid(user, apply.ApplyId, pledgeApplyId, string.Empty);
                    //if (result.ResultStatus != 0)
                    //    return result;

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

        public SelectModel GetCanPledgeApplySelectModel(int pageIndex, int pageSize, string orderStr, int applyDept, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pa.PledgeApplyId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)NFMT.Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" pa.PledgeApplyId,a.ApplyNo,a.ApplyTime,bd.StatusName,e.Name,d.DeptName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_PledgeApply pa left join dbo.Apply a on pa.ApplyId = a.ApplyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = a.ApplyStatus and bd.StatusId = {0} ", statusId);
            sb.Append(" left join NFMT_User.dbo.Employee e on e.EmpId = a.EmpId ");
            sb.Append(" left join NFMT_User.dbo.Department d on d.DeptId = a.ApplyDept ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" a.ApplyStatus = {0} and pa.PledgeApplyId in (select a.PledgeApplyId from St_PledgeApplyDetail a left join St_PledgeDetial b on a.DetailId = b.PledgeApplyDetailId and a.DetailStatus <> {1} and ISNULL(b.DetailStatus,0) not in ({1},{2})where  ISNULL(b.DetailId,0) = 0 )", (int)NFMT.Common.StatusEnum.已生效, (int)Common.StatusEnum.已作废, (int)Common.StatusEnum.已关闭);

            if (applyDept > 0)
                sb.AppendFormat(" and d.DeptId = {0} ", applyDept);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd >= applyTimeBegin)
                sb.AppendFormat(" and a.ApplyTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel GetPledgeStockId(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = pledgeapplyDAL.GetPledgeStockId(user, pledgeApplyId);
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

        public ResultModel GetPledgeApplyStockId(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = pledgeapplyDAL.GetPledgeApplyStockId(user, pledgeApplyId);
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

        public ResultModel Complete(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, pledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.PledgeApply pledgeApply = result.ReturnValue as PledgeApply;

                    if (pledgeApply == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能完成";
                        return result;
                    }

                    NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
                    result = applyDAL.Get(user, pledgeApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;

                    if (apply.ApplyStatus != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "非已生效状态的数据不允许完成";
                        return result;
                    }

                    DAL.PledgeDAL pledgeDAL = new PledgeDAL();
                    result = pledgeDAL.GetPledgeIdByApplyId(user, pledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    if (!string.IsNullOrEmpty(result.ReturnValue.ToString()))
                    {
                        Model.Pledge pledge = new Pledge();
                        foreach (string s in result.ReturnValue.ToString().Split(','))
                        {
                            result = pledgeDAL.Get(user, Convert.ToInt32(s));
                            if (result.ResultStatus != 0)
                                return result;
                            pledge = result.ReturnValue as Model.Pledge;

                            if (pledge.Status != StatusEnum.已完成)
                            {
                                result.ResultStatus = -1;
                                result.Message = "该质押申请对应的质押并未全部完成";
                                return result;
                            }
                        }
                    }

                    result = applyDAL.Confirm(user, apply);

                    if (result.ResultStatus == 0)
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

        public ResultModel Goback(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();
            try
            {
                DAL.PledgeApplyDAL pledgeApplyDAL = new PledgeApplyDAL();
                Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证质押申请
                    result = pledgeApplyDAL.Get(user, pledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PledgeApply pledgeApply = result.ReturnValue as Model.PledgeApply;
                    if (pledgeApply == null || pledgeApply.PledgeApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "质押申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, pledgeApply.ApplyId);
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

        public ResultModel Invalid(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();

            DAL.StockDAL stockDAL = new StockDAL();
            DAL.PledgeApplyDAL pledgeApplyDAL = new PledgeApplyDAL();
            Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
            DAL.PledgeApplyDetailDAL pledgeApplyDetailDAL = new PledgeApplyDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证出库申请
                    result = pledgeApplyDAL.Get(user, pledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PledgeApply pledgeApply = result.ReturnValue as Model.PledgeApply;
                    if (pledgeApply == null || pledgeApply.PledgeApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "质押申请不存在";
                        return result;
                    }

                    //获取主申请实体
                    result = applyDAL.Get(user, pledgeApply.ApplyId);
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
                    result = pledgeApplyDetailDAL.Load(user, pledgeApply.PledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<PledgeApplyDetail> details = result.ReturnValue as List<PledgeApplyDetail>;
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
                    foreach (Model.PledgeApplyDetail detail in details)
                    {
                        if (detail.DetailStatus == StatusEnum.已生效)
                            detail.DetailStatus = StatusEnum.已录入;
                        result = pledgeApplyDetailDAL.Invalid(user, detail);
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

        public ResultModel Confirm(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockDAL stockDAL = new StockDAL();
                DAL.PledgeApplyDAL pledgeApplyDAL = new PledgeApplyDAL();
                Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
                DAL.PledgeApplyDetailDAL pledgeApplyDetailDAL = new PledgeApplyDetailDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证质押申请
                    result = pledgeApplyDAL.Get(user, pledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PledgeApply pledgeApply = result.ReturnValue as Model.PledgeApply;
                    if (pledgeApply == null || pledgeApply.PledgeApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "质押申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, pledgeApply.ApplyId);
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
                    result = pledgeApplyDAL.CheckPledgeCanConfirm(user, pledgeApply.PledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Common.StatusEnum status = (Common.StatusEnum)result.ReturnValue;

                    //获取已生效质押申请明细
                    result = pledgeApplyDetailDAL.Load(user, pledgeApply.PledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.PledgeApplyDetail> details = result.ReturnValue as List<Model.PledgeApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取质押申请明细失败";
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

                    //质押申请明细更新状态至已完成
                    foreach (Model.PledgeApplyDetail detail in details)
                    {
                        //质押申请明细更新状态至已完成
                        result = pledgeApplyDetailDAL.Confirm(user, detail);
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

        public ResultModel ConfirmCancel(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.PledgeApplyDAL pledgeApplyDAL = new PledgeApplyDAL();
                DAL.PledgeApplyDetailDAL pledgeApplyDetailDAL = new PledgeApplyDetailDAL();
                Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证质押申请
                    result = pledgeApplyDAL.Get(user, pledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PledgeApply pledgeApply = result.ReturnValue as Model.PledgeApply;
                    if (pledgeApply == null || pledgeApply.PledgeApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "质押申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, pledgeApply.ApplyId);
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

                    //质押申请明细，在已完成状态下的更新至已生效
                    //获取已关闭的明细
                    result = pledgeApplyDetailDAL.Load(user, pledgeApply.PledgeApplyId, Common.StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.PledgeApplyDetail> details = result.ReturnValue as List<Model.PledgeApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取申请明细失败";
                        return result;
                    }

                    foreach (Model.PledgeApplyDetail detail in details)
                    {
                        result = pledgeApplyDetailDAL.ConfirmCancel(user, detail);
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

        public ResultModel GetPledgeByApplyId(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = pledgeapplyDAL.GetPledgeByApplyId(user, applyId);
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

        public ResultModel GetPledgeApplyDetails(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = pledgeapplyDAL.GetPledgeApplyDetails(user, pledgeApplyId);
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
