/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StopLossBLL.cs
// 文件功能描述：止损表dbo.Pri_StopLoss业务逻辑类。
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
    /// 止损表dbo.Pri_StopLoss业务逻辑类。
    /// </summary>
    public class StopLossBLL : Common.ExecBLL
    {
        private StopLossDAL stoplossDAL = new StopLossDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StopLossDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StopLossBLL()
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
            get { return this.stoplossDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int person, int assetId, int exchangeId, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StopLossId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" sl.StopLossId,e.Name,ex.ExchangeName,fcode.TradeCode,a.AssetName,CONVERT(varchar,sl.StopLossWeight) + m.MUName as StopLossWeight,CONVERT(varchar,sl.AvgPrice)+ c.CurrencyName as AvgPrice,sl.PricingTime,bd.StatusName,sl.StopLossStatus,isnull(det.row,0) as DetailRow ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_StopLoss sl ");
            sb.Append(" left join NFMT_User.dbo.Employee e on sl.StopLosser = e.EmpId ");
            sb.Append(" left join NFMT_Basic.dbo.Exchange ex on sl.ExchangeId = ex.ExchangeId ");
            sb.Append(" left join NFMT_Basic.dbo.FuturesCode fcode on sl.FuturesCodeId = fcode.FuturesCodeId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on sl.AssertId = a.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit m on sl.MUId = m.MUId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on sl.CurrencyId = c.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = sl.StopLossStatus and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);

            sb.AppendFormat(" left join (select COUNT(*) as row,StopLossId from dbo.Pri_StopLossDetail where DetailStatus >={0} ", readyStatus);
            sb.Append(" group by StopLossId) det on det.StopLossId = sl.StopLossId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (person > 0)
                sb.AppendFormat(" and sl.StopLosser = {0} ", person);
            if (assetId > 0)
                sb.AppendFormat(" and sl.AssertId = {0} ", assetId);
            if (exchangeId > 0)
                sb.AppendFormat(" and sl.ExchangeId = {0} ", exchangeId);
            if (status > 0)
                sb.AppendFormat(" and sl.StopLossStatus = {0} ", status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetCanStopLossApplySelectModel(int pageIndex, int pageSize, string orderStr, int applyDept, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StopLossApplyId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" sl.StopLossApplyId,a.ApplyNo,a.ApplyTime,e.Name,d.DeptName,ass.AssetName,CONVERT(varchar,sl.StopLossWeight) + m.MUName as StopLossWeight,c.ContractNo,sub.SubNo,bd.StatusName,ISNULL(det.row,0) as DetailRow ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_StopLossApply sl ");
            sb.AppendFormat(" left join (select SUM(ISNULL(StopLossWeight,0)) as alreadyStopLossWeight,StopLossApplyId from dbo.Pri_StopLoss where StopLossStatus <> {0} group by StopLossApplyId ) as al on sl.StopLossApplyId = al.StopLossApplyId ", (int)Common.StatusEnum.已作废);
            sb.Append(" left join dbo.Apply a on sl.ApplyId = a.ApplyId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on a.EmpId = e.EmpId ");
            sb.Append(" left join NFMT_User.dbo.Department d on a.ApplyDept = d.DeptId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sl.AssertId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit m on sl.MUId = m.MUId ");
            sb.Append(" left join dbo.Con_Contract c on sl.ContractId = c.ContractId ");
            sb.Append(" left join dbo.Con_ContractSub sub on sl.SubContractId = sub.SubId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on a.ApplyStatus = bd.DetailId and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);

            sb.AppendFormat(" left join (select COUNT(*) row,StopLossApplyId from dbo.Pri_StopLossApplyDetail where DetailStatus ={0} ", readyStatus);
            sb.Append(" group by StopLossApplyId) det on det.StopLossApplyId = sl.StopLossApplyId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" a.ApplyStatus = {0} and ISNULL(sl.StopLossWeight,0) > ISNULL(al.alreadyStopLossWeight,0) ", readyStatus);

            if (applyDept > 0)
                sb.AppendFormat(" and a.ApplyDept = {0} ", applyDept);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd >= applyTimeBegin)
                sb.AppendFormat(" and a.ApplyTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetDetailSelectModel(int pageIndex, int pageSize, string orderStr, int stopLossApplyId, string detailsId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "detail.DetailId asc";
            else
                select.OrderStr = orderStr;
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" detail.DetailId,detail.StockId,sname.RefNo,st.StockDate,st.GrossAmount,mu.MUName,a.AssetName,b.BrandName,CONVERT(varchar,st.GrossAmount) + mu.MUName as GrossAmoutName,c.CorpName,ss.StatusName as StockStatusName,detail.StopLossWeight,CONVERT(varchar,detail.StopLossWeight)+mu.MUName as StopLossWeightName ");
            sb.Append(",dp.DPName,st.CardNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_StopLossApplyDetail detail ");
            sb.Append(" left join dbo.St_Stock st on detail.StockId = st.StockId ");
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
            sb.AppendFormat(" detail.StopLossApplyId = {0} ", stopLossApplyId);
            if (!string.IsNullOrEmpty(detailsId))
                sb.AppendFormat(" and detail.DetailId in ({0}) ", detailsId);
            else
                sb.Append(" and 1=2 ");

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel GetCanStopLossDetailIds(UserModel user, int stopLossApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = stoplossDAL.GetCanStopLossDetailIds(user, stopLossApplyId);
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

        public ResultModel Create(UserModel user, NFMT.DoPrice.Model.StopLoss stopLoss, List<NFMT.DoPrice.Model.StopLossDetail> details)
        {
            ResultModel result = new ResultModel();
            DAL.StopLossApplyDAL stopLossApplyDAL = new StopLossApplyDAL();
            DAL.StopLossDetailDAL stopLossDetailDAL = new StopLossDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = stopLossApplyDAL.Get(user, stopLoss.StopLossApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.DoPrice.Model.StopLossApply stopLossApply = result.ReturnValue as NFMT.DoPrice.Model.StopLossApply;
                    if (stopLossApply == null)
                    {
                        result.Message = "获取止损申请失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = stoplossDAL.IsWeightCanStopLoss(user, stopLoss.StopLossApplyId, stopLoss.FuturesCodeId, stopLossApply.StopLossWeight, stopLoss.StopLossWeight, false, 0);
                    if (result.ResultStatus != 0)
                        return result;

                    stopLoss.PricingDirection = stopLossApply.PricingDirection;
                    stopLoss.PricingTime = DateTime.Now;
                    stopLoss.StopLosser = user.EmpId;
                    stopLoss.StopLossStatus = Common.StatusEnum.已录入;

                    //写入止损信息
                    result = stoplossDAL.Insert(user, stopLoss);
                    if (result.ResultStatus != 0)
                        return result;

                    //写入明细信息
                    if (details != null && details.Any())
                    {
                        int StopLossId = (int)result.ReturnValue;
                        foreach (Model.StopLossDetail detail in details)
                        {
                            detail.StopLossId = StopLossId;
                            result = stopLossDetailDAL.Insert(user, detail);
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

        public ResultModel Update(UserModel user, NFMT.DoPrice.Model.StopLoss stopLoss, List<NFMT.DoPrice.Model.StopLossDetail> details)
        {
            ResultModel result = new ResultModel();
            DAL.StopLossApplyDAL stopLossApplyDAL = new StopLossApplyDAL();
            DAL.StopLossDetailDAL stopLossDetailDAL = new StopLossDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = stoplossDAL.Get(user, stopLoss.StopLossId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.DoPrice.Model.StopLoss stopLossResult = result.ReturnValue as NFMT.DoPrice.Model.StopLoss;
                    if (stopLossResult == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取止损信息错误";
                        return result;  
                    }

                    result = stopLossApplyDAL.Get(user, stopLoss.StopLossApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.DoPrice.Model.StopLossApply stopLossApply = result.ReturnValue as NFMT.DoPrice.Model.StopLossApply;
                    if (stopLossApply == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取止损申请信息错误";
                        return result;
                    }
                    result = stoplossDAL.IsWeightCanStopLoss(user, stopLoss.StopLossApplyId, stopLoss.FuturesCodeId, stopLossApply.StopLossWeight, stopLoss.StopLossWeight, true, stopLossResult.StopLossWeight);
                    if (result.ResultStatus != 0)
                        return result;

                    stopLossResult.StopLossWeight = stopLoss.StopLossWeight;
                    stopLossResult.ExchangeId = stopLoss.ExchangeId;
                    stopLossResult.FuturesCodeId = stopLoss.FuturesCodeId;
                    stopLossResult.AvgPrice = stopLoss.AvgPrice;
                    stopLossResult.PricingTime = DateTime.Now;
                    stopLossResult.StopLosser = user.EmpId;

                    //写入止损信息
                    result = stoplossDAL.Update(user, stopLossResult);
                    if (result.ResultStatus != 0)
                        return result;

                    if (details != null && details.Any())
                    {
                        //作废明细
                        result = stopLossDetailDAL.InvalidAll(user, stopLoss.StopLossId);
                        if (result.ResultStatus != 0)
                            return result;

                        //写入明细信息
                        foreach (Model.StopLossDetail detail in details)
                        {
                            result = stopLossDetailDAL.Insert(user, detail);
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

        public ResultModel GoBack(UserModel user, int stopLossId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = stoplossDAL.Get(user, stopLossId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StopLoss stopLoss = result.ReturnValue as StopLoss;
                    if (stopLoss == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能撤返";
                        return result;
                    }

                    //获取止损申请实体
                    DAL.StopLossApplyDAL stopLossApplyDAL = new StopLossApplyDAL();
                    result = stopLossApplyDAL.Get(user, stopLoss.StopLossApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StopLossApply stopLossApply = result.ReturnValue as Model.StopLossApply;

                    //获取申请实体
                    NFMT.Operate.DAL.ApplyDAL applyDAl = new Operate.DAL.ApplyDAL();
                    result = applyDAl.Get(user, stopLossApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply.Status != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "止损对应的申请非已生效，不能进行撤返操作";
                        return result;
                    }

                    result = stoplossDAL.Goback(user, stopLoss);
                    if (result.ResultStatus != 0)
                        return result;

                    //同步工作流状态
                    NFMT.WorkFlow.BLL.DataSourceBLL dataSourceBLL = new WorkFlow.BLL.DataSourceBLL();
                    result = dataSourceBLL.SynchronousStatus(user, stopLoss);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ResultStatus == 0)
                        result.Message = "撤返成功";
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

        public ResultModel Invalid(UserModel user, int stopLossId)
        {
            ResultModel result = new ResultModel();
            DAL.StopLossDetailDAL stopLossDetailDAL = new StopLossDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = stoplossDAL.Get(user, stopLossId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StopLoss stopLoss = result.ReturnValue as Model.StopLoss;
                    if (stopLoss == null)
                    {
                        result.Message = "不存在止损信息";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = stoplossDAL.Invalid(user, stopLoss);
                    if (result.ResultStatus != 0)
                        return result;

                    result = stopLossDetailDAL.Load(user, stopLossId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StopLossDetail> details = result.ReturnValue as List<Model.StopLossDetail>;
                    if (details != null && details.Any())
                    {
                        result = stopLossDetailDAL.InvalidAll(user, stopLossId);
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

        public ResultModel Complete(UserModel user, int stopLossId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = stoplossDAL.Get(user, stopLossId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StopLoss resultObj = result.ReturnValue as StopLoss;
                    if (resultObj == null || resultObj.StopLossId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能进行执行完成操作";
                        return result;
                    }

                    //执行完成
                    result = stoplossDAL.Complete(user, resultObj);
                    if (result.ResultStatus != 0)
                        return result;

                    //加载明细
                    DAL.StopLossDetailDAL stopLossDetailDAL = new StopLossDetailDAL();
                    result = stopLossDetailDAL.Load(user, resultObj.StopLossId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StopLossDetail> details = result.ReturnValue as List<Model.StopLossDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取止损明细失败";
                        return result;
                    }

                    foreach (Model.StopLossDetail detail in details)
                    {
                        result = stopLossDetailDAL.Complete(user, detail);
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

        public ResultModel CompleteCancel(UserModel user, int stopLossId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StopLossDetailDAL stopLossDetailDAL = new StopLossDetailDAL();
                Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证止损
                    result = stoplossDAL.Get(user, stopLossId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StopLoss stopLoss = result.ReturnValue as StopLoss;
                    if (stopLoss == null || stopLoss.StopLossId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能进行执行完成取消操作";
                        return result;
                    }

                    //止损完成取消
                    result = stoplossDAL.CompleteCancel(user, stopLoss);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取已完成止损明细
                    result = stopLossDetailDAL.Load(user, stopLoss.StopLossId, Common.StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StopLossDetail> details = result.ReturnValue as List<Model.StopLossDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取止损明细错误";
                        return result;
                    }

                    //将所有已完成止损明细更新至已生效
                    foreach (Model.StopLossDetail detail in details)
                    {
                        result = stopLossDetailDAL.CompleteCancel(user, detail);
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

        #endregion
    }
}
