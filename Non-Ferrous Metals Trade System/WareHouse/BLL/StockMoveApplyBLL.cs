/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockMoveApplyBLL.cs
// 文件功能描述：移库申请dbo.StockMoveApply业务逻辑类。
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
    /// 移库申请dbo.StockMoveApply业务逻辑类。
    /// </summary>
    public class StockMoveApplyBLL : Common.ApplyBLL
    {
        private StockMoveApplyDAL stockmoveapplyDAL = new StockMoveApplyDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StockMoveApplyDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockMoveApplyBLL()
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
            get { return this.stockmoveapplyDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel Complete(UserModel user, StockMoveApply stockMoveApply)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, stockMoveApply.StockMoveApplyId);
                    stockMoveApply = result.ReturnValue as StockMoveApply;

                    if (stockMoveApply == null)
                    {
                        result.Message = "该数据不存在，不能完成";
                        return result;
                    }

                    NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
                    result = applyDAL.Get(user, stockMoveApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;

                    if (apply.ApplyStatus != StatusEnum.已生效)
                    {
                        result.Message = "非已生效状态的数据不允许完成";
                        return result;
                    }

                    DAL.StockMoveDAL stockMoveDAL = new StockMoveDAL();
                    result = stockMoveDAL.GetStockMoveIdByApplyId(user, stockMoveApply.StockMoveApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    int stockMoveId = (int)result.ReturnValue;
                    result = stockMoveDAL.Get(user, stockMoveId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockMove stockMove = result.ReturnValue as StockMove;
                    if (stockMove.Status != StatusEnum.已完成)
                    {
                        result.Message = "该申请对应的移库操作未完成，不能申请完成";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = applyDAL.Confirm(user, apply);
                    //result = stockmoveapplyDAL.Complete(user, stockMoveApply);

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

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int applyPerson, int status, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sma.StockMoveApplyId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)NFMT.Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" sma.StockMoveApplyId,a.ApplyTime,bd.StatusName,e.Name,d.DeptName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockMoveApply sma left join dbo.Apply a on sma.ApplyId = a.ApplyId ");
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
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd >= applyTimeBegin)
                sb.AppendFormat(" and a.ApplyTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 可移库库存列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="sids"></param>
        /// <returns></returns>
        public SelectModel GetCanMoveSelect(int pageIndex, int pageSize, string orderStr, string sids)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "st.StockId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "st.StockId,stn.RefNo,st.PaperNo,st.DeliverPlaceId,dp.DPName as DeliverPlace,a.AssetName,convert(varchar,st.GrossAmount) + mu.MUName as GrossAmount,CONVERT(varchar,st.NetAmount) + mu.MUName as NetAmount,bra.BrandName,st.CardNo ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_Stock st left join dbo.St_StockName stn on st.StockNameId = stn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on a.AssetId = st.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = st.UintId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = st.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = st.BrandId ");
            select.TableName = sb.ToString();

            sb.Clear();
            int status = (int)StockStatusEnum.在库正常;
            sb.AppendFormat(" st.StockStatus = {0} and st.StockId not in (select StockId from dbo.St_StockExclusive where ExclusiveStatus = {1}) ", status, (int)Common.StatusEnum.已生效);

            if (!string.IsNullOrEmpty(sids))
                sb.AppendFormat(" and st.StockId not in ({0})", sids);
            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="sids"></param>
        /// <returns></returns>
        public SelectModel GetStockMoveApplyInfoSelect(int pageIndex, int pageSize, string orderStr, string sids, int applyId, int mode)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "st.StockId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " st.StockId,stn.RefNo,st.PaperNo,st.DeliverPlaceId,dp.DPName as DeliverPlace,a.AssetName,convert(varchar,st.GrossAmount) + mu.MUName as GrossAmount,CONVERT(varchar,st.NetAmount) + mu.MUName as NetAmount,bra.BrandName,st.CardNo ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_Stock st left join dbo.St_StockName stn on st.StockNameId = stn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on a.AssetId = st.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = st.UintId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = st.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = st.BrandId ");
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

        public SelectModel GetStockMoveApplyDetailSelect(int pageIndex, int pageSize, string orderStr, int id)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "detail.DetailId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " detail.DetailId,stn.RefNo,st.PaperNo,st.DeliverPlaceId,dp.DPName as DeliverPlace,a.AssetName,convert(varchar,st.GrossAmount) + mu.MUName as GrossAmount,CONVERT(varchar,st.NetAmount) + mu.MUName as NetAmount,bra.BrandName,st.CardNo ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_StockMoveApplyDetail detail left join dbo.St_Stock st on st.StockId = detail.StockId ");
            sb.Append(" left join dbo.St_StockName stn on st.StockNameId = stn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on a.AssetId = st.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = st.UintId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = st.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = st.BrandId ");
            select.TableName = sb.ToString();

            sb.Clear();

            int statusId = (int)Common.StatusEnum.已生效;
            if (id > 0)
                sb.AppendFormat("  detail.StockMoveApplyId = {0} and detail.DetailStatus = {1}", id, statusId);

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="apply"></param>
        /// <param name="stockMoveApply"></param>
        /// <param name="stockMoveApplyDetails"></param>
        /// <returns></returns>
        public ResultModel StockMoveApplyCreateHandle(UserModel user, NFMT.Operate.Model.Apply apply, List<Model.StockMoveApplyDetail> stockMoveApplyDetails)
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

                    //写入移库申请表
                    NFMT.WareHouse.DAL.StockMoveApplyDAL stockMoveApplyDAL = new StockMoveApplyDAL();
                    result = stockMoveApplyDAL.Insert(user, new Model.StockMoveApply()
                    {
                        ApplyId = applyId
                    });
                    if (result.ResultStatus != 0)
                        return result;

                    int stockMoveApplyId = (int)result.ReturnValue;

                    //写入移库申请明细表
                    NFMT.WareHouse.DAL.StockMoveApplyDetailDAL detailDAL = new StockMoveApplyDetailDAL();
                    foreach (Model.StockMoveApplyDetail detail in stockMoveApplyDetails)
                    {
                        detail.StockMoveApplyId = stockMoveApplyId;
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
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="apply"></param>
        /// <param name="stockMoveApply"></param>
        /// <param name="stockIds"></param>
        /// <returns></returns>
        public ResultModel StockMoveApplyUpdateHandle(UserModel user, NFMT.Operate.Model.Apply apply, List<Model.StockMoveApplyDetail> details, int stockMoveApplyId)
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
                        result.Message = "获取失败";
                        return result;
                    }

                    applyRes.ApplyCorp = apply.ApplyCorp;
                    applyRes.EmpId = user.EmpId;
                    applyRes.ApplyTime = DateTime.Now;
                    applyRes.ApplyDept = apply.ApplyDept;
                    applyRes.ApplyDesc = apply.ApplyDesc;

                    result = applyDAL.Update(user, applyRes);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.DAL.StockMoveApplyDetailDAL stockMoveApplyDetailDAL = new StockMoveApplyDetailDAL();
                    result = stockMoveApplyDetailDAL.InvalidAll(user, stockMoveApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    foreach (Model.StockMoveApplyDetail detail in details)
                    {
                        detail.StockMoveApplyId = stockMoveApplyId;
                        result = stockMoveApplyDetailDAL.Insert(user, detail);
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

        public ResultModel StockMoveApplyInvalid(UserModel user, int stockMoveApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取移库申请
                    NFMT.WareHouse.DAL.StockMoveApplyDAL stockMoveApplyDAL = new StockMoveApplyDAL();
                    result = stockMoveApplyDAL.Get(user, stockMoveApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.StockMoveApply StockMoveApply = result.ReturnValue as NFMT.WareHouse.Model.StockMoveApply;

                    //获取申请主表
                    NFMT.Operate.BLL.ApplyBLL applyBLL = new Operate.BLL.ApplyBLL();
                    result = applyBLL.Get(user, StockMoveApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;

                    //作废申请主表
                    result = applyBLL.Invalid(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废移库申请明细
                    NFMT.WareHouse.DAL.StockMoveApplyDetailDAL stockMoveApplyDetailDAL = new StockMoveApplyDetailDAL();
                    result = stockMoveApplyDetailDAL.Invalid(user, stockMoveApplyId, string.Empty);
                    if (result.ResultStatus != 0)
                        return result;

                    //NFMT.WareHouse.DAL.StockExclusiveDAL stockExclusiveDAL = new StockExclusiveDAL();
                    //result = stockExclusiveDAL.Invalid(user, apply.ApplyId, stockMoveApplyId, string.Empty);
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

        public SelectModel GetCanStockMoveApplySelectModel(int pageIndex, int pageSize, string orderStr, int applyDept, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sma.StockMoveApplyId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)NFMT.Common.StatusTypeEnum.通用状态;
            int status = (int)NFMT.Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" sma.StockMoveApplyId,a.ApplyTime,bd.StatusName,e.Name,d.DeptName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockMoveApply sma left join dbo.Apply a on sma.ApplyId = a.ApplyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = a.ApplyStatus and bd.StatusId = {0} ", statusId);
            sb.Append(" left join NFMT_User.dbo.Employee e on e.EmpId = a.EmpId ");
            sb.Append(" left join NFMT_User.dbo.Department d on d.DeptId = a.ApplyDept ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" a.ApplyStatus = {0} and sma.StockMoveApplyId not in (select StockMoveApplyId from NFMT.dbo.St_StockMove where MoveStatus not in ({1},{2})) ", status, (int)NFMT.Common.StatusEnum.已作废,(int)NFMT.Common.StatusEnum.已关闭);

            if (applyDept > 0)
                sb.AppendFormat(" and d.DeptId = {0} ", applyDept);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd >= applyTimeBegin)
                sb.AppendFormat(" and a.ApplyTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel GoBack(UserModel user, int stockMoveApplyId)
        {
            ResultModel result = new ResultModel();
            NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = stockmoveapplyDAL.Get(user, stockMoveApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockMoveApply stockMoveApply = result.ReturnValue as Model.StockMoveApply;
                    if (stockMoveApply == null || stockMoveApply.StockMoveApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "数据不存在，无法撤返";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, stockMoveApply.ApplyId);
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
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel GetStockMoveApplyByApplyId(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = stockmoveapplyDAL.GetStockMoveApplyByApplyId(user, applyId);
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
