/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PricingBLL.cs
// 文件功能描述：点价表dbo.Pri_Pricing业务逻辑类。
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

namespace NFMT.DoPrice.BLL
{
    /// <summary>
    /// 点价表dbo.Pri_Pricing业务逻辑类。
    /// </summary>
    public class PricingBLL : Common.ExecBLL
    {
        private PricingDAL pricingDAL = new PricingDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PricingDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PricingBLL()
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
            get { return this.pricingDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int person, int assetId, int exchangeId, int status, string subNo)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "p.PricingId asc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" p.PricingId,apply.ApplyNo,con.ContractNo,sub.SubNo,e.Name,ex.ExchangeName,fcode.TradeCode,a.AssetName,CONVERT(varchar,p.PricingWeight) + m.MUName as PricingWeight,CONVERT(varchar,p.AvgPrice)+ c.CurrencyName as AvgPrice,p.PricingTime,bd.StatusName,p.PricingStatus,isnull(det.row,0) as DetailRow ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_Pricing p ");
            sb.Append(" left join dbo.Pri_PricingApply pa on p.PricingApplyId = pa.PricingApplyId ");
            sb.Append(" left join dbo.Apply apply on pa.ApplyId = apply.ApplyId ");
            sb.Append(" left join dbo.Con_Contract con on pa.ContractId = con.ContractId ");
            sb.Append(" left join dbo.Con_ContractSub sub on pa.SubContractId = sub.SubId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on p.Pricinger = e.EmpId ");
            sb.Append(" left join NFMT_Basic.dbo.Exchange ex on p.ExchangeId = ex.ExchangeId ");
            sb.Append(" left join NFMT_Basic.dbo.FuturesCode fcode on p.FuturesCodeId = fcode.FuturesCodeId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on p.AssertId = a.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit m on p.MUId = m.MUId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on p.CurrencyId = c.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = p.PricingStatus and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);

            sb.AppendFormat(" left join (select COUNT(*) as row,PricingId from dbo.Pri_PricingDetail where DetailStatus ={0} ", readyStatus);
            sb.Append(" group by PricingId) det on det.PricingId = p.PricingId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (person > 0)
                sb.AppendFormat(" and p.Pricinger = {0} ", person);
            if (assetId > 0)
                sb.AppendFormat(" and p.AssertId = {0} ", assetId);
            if (exchangeId > 0)
                sb.AppendFormat(" and p.ExchangeId = {0} ", exchangeId);
            if (status > 0)
                sb.AppendFormat(" and p.PricingStatus = {0} ", status);
            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and sub.SubNo like '%{0}%' ", subNo);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetCanStopLossApplySelectModel(int pageIndex, int pageSize, string orderStr, int person, int assetId, int exchangeId, int status, string subNo)
        {
            SelectModel select = this.GetSelectModel(pageIndex, pageSize, orderStr, person, assetId, exchangeId, status, subNo);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat(" left join (select SUM(ISNULL(StopLossWeight,0)) as alreadyStopLossWeight,PricingId from dbo.Pri_StopLossApply sla left join dbo.Apply a on sla.ApplyId = a.ApplyId where a.ApplyStatus <> {0} group by PricingId) as al on p.PricingId = al.PricingId ", (int)Common.StatusEnum.已作废);
            select.TableName += sb.ToString();

            sb.Clear();
            sb.Append(" and ISNULL(p.PricingWeight,0) > ISNULL(al.alreadyStopLossWeight,0) ");
            select.WhereStr += sb.ToString();

            return select;
        }

        public SelectModel GetCanDoPriceApplySelectModel(int pageIndex, int pageSize, string orderStr, int applyDept, DateTime applyTimeBegin, DateTime applyTimeEnd, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pa.PricingApplyId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" pa.PricingApplyId,a.ApplyNo,a.ApplyTime,e.Name,d.DeptName,ass.AssetName,CONVERT(varchar,pa.PricingWeight) + m.MUName as PricingWeight,con.ContractNo,sub.SubNo,bd.StatusName,ISNULL(det.row,0) as DetailRow ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_PricingApply pa ");
            sb.AppendFormat(" left join (select SUM(ISNULL(PricingWeight,0)) as alreadyPricingWeight,PricingApplyId from dbo.Pri_Pricing where PricingStatus <> {0} group by PricingApplyId) as al on pa.PricingApplyId = al.PricingApplyId ", (int)Common.StatusEnum.已作废);
            sb.Append(" left join dbo.Apply a on pa.ApplyId = a.ApplyId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on a.EmpId = e.EmpId ");
            sb.Append(" left join NFMT_User.dbo.Department d on a.ApplyDept = d.DeptId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on pa.AssertId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit m on pa.MUId = m.MUId ");
            sb.Append(" left join dbo.Con_Contract con on pa.ContractId = con.ContractId ");
            sb.Append(" left join dbo.Con_ContractSub sub on pa.SubContractId = sub.SubId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on a.ApplyStatus = bd.DetailId and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);

            sb.AppendFormat(" left join (select COUNT(*) row,PricingApplyId from dbo.Pri_PricingApplyDetail where DetailStatus ={0} ", (int)Common.StatusEnum.已生效);
            sb.Append(" group by PricingApplyId) det on det.PricingApplyId = pa.PricingApplyId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" a.ApplyStatus = {0} and isnull(pa.PricingWeight,0) > isnull(al.alreadyPricingWeight,0) ", status);

            if (applyDept > 0)
                sb.AppendFormat(" and a.ApplyDept = {0} ", applyDept);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd >= applyTimeBegin)
                sb.AppendFormat(" and a.ApplyTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetCanDelayApplySelectModel(int pageIndex, int pageSize, string orderStr, int applyDept, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pa.PricingApplyId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" pa.PricingApplyId,a.ApplyNo,a.ApplyTime,e.Name,d.DeptName,ass.AssetName,CONVERT(varchar,pa.PricingWeight) + m.MUName as PricingWeight,con.ContractNo,sub.SubNo,bd.StatusName,pad.DelayAmount ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_PricingApply pa ");
            sb.Append(" left join dbo.Apply a on pa.ApplyId = a.ApplyId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on a.EmpId = e.EmpId ");
            sb.Append(" left join NFMT_User.dbo.Department d on a.ApplyDept = d.DeptId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on pa.AssertId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit m on pa.MUId = m.MUId ");
            sb.Append(" left join dbo.Con_Contract con on pa.ContractId = con.ContractId ");
            sb.Append(" left join dbo.Con_ContractSub sub on pa.SubContractId = sub.SubId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on a.ApplyStatus = bd.DetailId and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);
            sb.AppendFormat(" left join (select SUM(ISNULL(DelayAmount,0)) as DelayAmount,PricingApplyId from dbo.Pri_PricingApplyDelay where DetailStatus >={0} group by PricingApplyId) pad on pa.PricingApplyId = pad.PricingApplyId ", (int)Common.StatusEnum.已生效);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" a.ApplyStatus = {0} and ISNULL(pa.PricingWeight,0) > ISNULL(pad.DelayAmount,0) ", (int)Common.StatusEnum.已生效);

            if (applyDept > 0)
                sb.AppendFormat(" and a.ApplyDept = {0} ", applyDept);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd >= applyTimeBegin)
                sb.AppendFormat(" and a.ApplyTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetDetailSelectModel(int pageIndex, int pageSize, string orderStr, int pricingApplyId, string detailsId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "detail.DetailId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" detail.DetailId,detail.StockId,detail.StockLogId,sname.RefNo,st.StockDate,slog.GrossAmount,mu.MUName,a.AssetName,b.BrandName,CONVERT(varchar,slog.GrossAmount) + mu.MUName as GrossAmoutName,c.CorpName,ss.StatusName as StockStatusName,detail.PricingWeight,CONVERT(varchar,detail.PricingWeight)+mu.MUName as PricingWeightName,dp.DPName,st.CardNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_PricingApplyDetail detail ");
            sb.Append(" left join dbo.Pri_PricingApply apply on apply.PricingApplyId = detail.PricingApplyId ");
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
            sb.AppendFormat(" detail.PricingApplyId = {0} ", pricingApplyId);
            if (!string.IsNullOrEmpty(detailsId))
                sb.AppendFormat(" and detail.DetailId in ({0}) ", detailsId);
            else
                sb.Append(" and 1=2 ");

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel GetCanDoPriceDetailIds(UserModel user, int pricingApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = pricingDAL.GetCanDoPriceDetailIds(user, pricingApplyId);
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

        public ResultModel Create(UserModel user, Model.Pricing pricing, List<Model.PricingDetail> pricingDetails)
        {
            ResultModel result = new ResultModel();
            DAL.PricingDetailDAL pricingDetailDAL = new PricingDetailDAL();
            DAL.PricingApplyDAL pricingApplyDAL = new PricingApplyDAL();
            DAL.PricingApplyDetailDAL applyDetail = new PricingApplyDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取点价申请
                    result = pricingApplyDAL.Get(user, pricing.PricingApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PricingApply pricingApply = result.ReturnValue as Model.PricingApply;
                    if (pricingApply == null || pricingApply.PricingApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "对应点价申请不存在";
                        return result;
                    }

                    if (pricing.AvgPrice < pricingApply.MinPrice || pricing.AvgPrice > pricingApply.MaxPrice)
                    {
                        result.ResultStatus = -1;
                        result.Message = "期货成交价不在点价申请价格范围内";
                        return result;
                    }

                    result = pricingDAL.IsWeightCanPricing(user, pricing.PricingApplyId, pricing.FuturesCodeId, pricingApply.PricingWeight, pricing.PricingWeight, false, 0);
                    if (result.ResultStatus != 0)
                        return result;

                    pricing.PricingDirection = pricingApply.PricingDirection;
                    pricing.PricingStatus = StatusEnum.已录入;
                    result = pricingDAL.Insert(user, pricing);

                    if (result.ResultStatus != 0)
                        return result;
                    int pricingId = (int)result.ReturnValue;

                    foreach (Model.PricingDetail detail in pricingDetails)
                    {
                        detail.PricingId = pricingId;
                        detail.DetailStatus = StatusEnum.已生效;
                        result = pricingDetailDAL.Insert(user, detail);
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
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Update(UserModel user, Model.Pricing pricing, List<Model.PricingDetail> pricingDetails)
        {
            ResultModel result = new ResultModel();
            DAL.PricingDetailDAL pricingDetailDAL = new PricingDetailDAL();
            DAL.PricingApplyDAL pricingApplyDAL = new PricingApplyDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = pricingDAL.Get(user, pricing.PricingId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Pricing resPricing = result.ReturnValue as Model.Pricing;
                    if (resPricing == null || resPricing.PricingId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "验证数据失败";
                        return result;
                    }

                    //获取点价申请
                    result = pricingApplyDAL.Get(user, pricing.PricingApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PricingApply pricingApply = result.ReturnValue as Model.PricingApply;
                    if (pricingApply == null || pricingApply.PricingApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "对应点价申请不存在";
                        return result;
                    }

                    result = pricingDAL.IsWeightCanPricing(user, pricing.PricingApplyId, pricing.FuturesCodeId, pricingApply.PricingWeight, pricing.PricingWeight, true, resPricing.PricingWeight);
                    if (result.ResultStatus != 0)
                        return result;

                    resPricing.PricingWeight = pricing.PricingWeight;
                    resPricing.MUId = pricing.MUId;
                    resPricing.ExchangeId = pricing.ExchangeId;
                    resPricing.FuturesCodeEndDate = pricing.FuturesCodeEndDate;
                    resPricing.FuturesCodeId = pricing.FuturesCodeId;
                    resPricing.SpotQP = pricing.SpotQP;
                    resPricing.DelayFee = pricing.DelayFee;
                    resPricing.Spread = pricing.Spread;
                    resPricing.OtherFee = pricing.OtherFee;
                    resPricing.AvgPrice = pricing.AvgPrice;
                    resPricing.PricingTime = pricing.PricingTime;
                    resPricing.CurrencyId = pricing.CurrencyId;
                    resPricing.Pricinger = pricing.Pricinger;
                    resPricing.AssertId = pricing.AssertId;
                    resPricing.FinalPrice = pricing.FinalPrice;

                    result = pricingDAL.Update(user, resPricing);
                    if (result.ResultStatus != 0)
                        return result;

                    result = pricingDetailDAL.InvalidAll(user, pricing.PricingId);
                    if (result.ResultStatus != 0)
                        return result;

                    foreach (Model.PricingDetail detail in pricingDetails)
                    {
                        detail.PricingId = pricing.PricingId;
                        detail.DetailStatus = StatusEnum.已生效;
                        detail.PricingApplyId = resPricing.PricingApplyId;

                        result = pricingDetailDAL.Insert(user, detail);
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
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel GoBack(UserModel user, int pricingId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = pricingDAL.Get(user, pricingId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.Pricing pricing = result.ReturnValue as Pricing;
                    if (pricing == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能撤返";
                        return result;
                    }

                    //获取点价申请实体
                    DAL.PricingApplyDAL pricingApplyDAL = new PricingApplyDAL();
                    result = pricingApplyDAL.Get(user, pricing.PricingApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PricingApply pricingApply = result.ReturnValue as Model.PricingApply;

                    //获取申请实体
                    NFMT.Operate.DAL.ApplyDAL applyDAl = new Operate.DAL.ApplyDAL();
                    result = applyDAl.Get(user, pricingApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply.Status != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "点价对应的申请非已生效，不能进行撤返操作";
                        return result;
                    }

                    result = pricingDAL.Goback(user, pricing);
                    if (result.ResultStatus != 0)
                        return result;

                    //同步工作流状态
                    NFMT.WorkFlow.BLL.DataSourceBLL dataSourceBLL = new WorkFlow.BLL.DataSourceBLL();
                    result = dataSourceBLL.SynchronousStatus(user, pricing);
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

        public ResultModel Invalid(UserModel user, int pricingId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = pricingDAL.Get(user, pricingId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Pricing pricing = result.ReturnValue as Model.Pricing;
                    if (pricing == null)
                    {
                        result.Message = "不存在点价信息";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = pricingDAL.Invalid(user, pricing);
                    if (result.ResultStatus != 0)
                        return result;

                    DAL.PricingDetailDAL pricingDetailDAL = new PricingDetailDAL();
                    result = pricingDetailDAL.InvalidAll(user, pricingId);
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
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Complete(UserModel user, int pricingId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = pricingDAL.Get(user, pricingId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Pricing resultObj = result.ReturnValue as Pricing;
                    if (resultObj == null || resultObj.PricingId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能进行执行完成操作";
                        return result;
                    }

                    //执行完成
                    result = pricingDAL.Complete(user, resultObj);
                    if (result.ResultStatus != 0)
                        return result;

                    //加载明细
                    DAL.PricingDetailDAL pricingDetailDAL = new PricingDetailDAL();
                    result = pricingDetailDAL.Load(user, resultObj.PricingId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.PricingDetail> details = result.ReturnValue as List<Model.PricingDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取点价明细失败";
                        return result;
                    }

                    foreach (Model.PricingDetail detail in details)
                    {
                        result = pricingDetailDAL.Complete(user, detail);
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

        public ResultModel CompleteCancel(UserModel user, int pricingId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.PricingDetailDAL pricingDetailDAL = new PricingDetailDAL();
                DAL.PricingApplyDAL pricingApplyDAL = new PricingApplyDAL();
                Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证点价
                    result = pricingDAL.Get(user, pricingId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Pricing pricing = result.ReturnValue as Pricing;
                    if (pricing == null || pricing.PricingId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能进行执行完成取消操作";
                        return result;
                    }

                    //点价完成取消
                    result = pricingDAL.CompleteCancel(user, pricing);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取已完成点价明细
                    result = pricingDetailDAL.Load(user, pricing.PricingId, Common.StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;
                    List<Model.PricingDetail> details = result.ReturnValue as List<Model.PricingDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取点价明细错误";
                        return result;
                    }

                    //将所有已完成点价明细更新至已生效
                    foreach (Model.PricingDetail detail in details)
                    {
                        result = pricingDetailDAL.CompleteCancel(user, detail);
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

        public ResultModel GetAlreadyStopLossWeight(UserModel user, int pricingId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = pricingDAL.GetAlreadyStopLossWeight(user, pricingId);
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
        /// 获取子合约下的点价列表
        /// </summary>
        /// <param name="user"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        public ResultModel Load(UserModel user, int subId)
        {
            return this.pricingDAL.Load(user, subId);
        }

        #endregion

        #region report

        public SelectModel GetDoPriceReportSelect(int pageIndex, int pageSize, string orderStr, string contractNo, int assetId, DateTime startDate, DateTime endDate)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)Common.StatusEnum.已生效;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            select.ColumnName = " p.PricingId,con.ContractId,con.ContractNo,con.OutContractNo,CustCorp.CorpName,asset.AssetName,p.FuturesCodeId,fc.TradeCode,ex.ExchangeCode,cur.CurrencyName,CONVERT(numeric(10,4),p.PricingWeight/fc.CodeSize) as TurnoverHand,p.PricingWeight as Turnover,bdPricingStyle.DetailName as PricingStyle,case when p.SpotQP = '1800-01-01 00:00:00.000' then null else p.SpotQP end as SpotQP,p.Spread,con.Premium,p.DelayFee,p.OtherFee,p.FinalPrice";

            sb.Clear();
            sb.Append(" NFMT..Pri_Pricing p ");
            sb.Append(" left join NFMT..Pri_PricingApply pa on pa.PricingApplyId = p.PricingApplyId ");
            sb.Append(" left join NFMT..Apply a on a.ApplyId = pa.ApplyId ");
            sb.Append(" left join NFMT..Con_Contract con on con.ContractId = pa.ContractId ");
            sb.AppendFormat(" left join NFMT..Con_SubCorporationDetail subCorpDetail on subCorpDetail.ContractId = con.ContractId and subCorpDetail.DetailStatus = {0} and subCorpDetail.IsInnerCorp = 0 and subCorpDetail.IsDefaultCorp = 1 ", readyStatus);
            sb.AppendFormat(" left join NFMT_User..Corporation CustCorp on CustCorp.CorpId = subCorpDetail.CorpId  and CustCorp.CorpStatus = {0} ", readyStatus);
            sb.Append(" left join NFMT_Basic..Asset asset on asset.AssetId = con.AssetId ");
            sb.Append(" left join NFMT_Basic..Exchange ex on ex.ExchangeId = p.ExchangeId ");
            sb.Append(" left join NFMT_Basic..FuturesCode fc on fc.FuturesCodeId = p.FuturesCodeId ");
            sb.Append(" left join NFMT_Basic..Currency cur on cur.CurrencyId = pa.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic..BDStyleDetail bdPricingStyle on bdPricingStyle.StyleDetailId = pa.PricingStyle and bdPricingStyle.BDStyleId = {0} ", (int)NFMT.Data.StyleEnum.计价方式);
            sb.Append(" left join NFMT_Basic..MeasureUnit mu on mu.MUId = con.UnitId ");

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" p.PricingStatus = {0} and a.ApplyStatus = {0} and con.ContractStatus = {0} ", readyStatus);
            if (!string.IsNullOrEmpty(contractNo))
                sb.AppendFormat(" and con.ContractNo like '%{0}%' ", contractNo);
            if (assetId > 0)
                sb.AppendFormat(" and con.AssetId = {0} ", assetId);
            if (startDate > Common.DefaultValue.DefaultTime && endDate > startDate)
                sb.AppendFormat(" and p.PricingTime between '{0}' and '{1}' ", startDate.ToString(), endDate.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        //public override string[,] SetExcelRangeData(DataTable source)
        //{
        //    string[,] objData = new string[source.Rows.Count, 17];

        //    for (int i = 0; i < source.Rows.Count; i++)
        //    {
        //        System.Data.DataRow dr = source.Rows[i];

        //        objData[i, 0] = dr["PricingId"].ToString();
        //        objData[i, 1] = dr["ContractNo"].ToString();
        //        objData[i, 2] = dr["OutContractNo"].ToString();
        //        objData[i, 3] = dr["CorpName"].ToString();
        //        objData[i, 4] = dr["AssetName"].ToString();
        //        objData[i, 5] = dr["TradeCode"].ToString();
        //        objData[i, 6] = dr["ExchangeCode"].ToString();
        //        objData[i, 7] = dr["CurrencyName"].ToString();
        //        objData[i, 8] = dr["TurnoverHand"].ToString();
        //        objData[i, 9] = dr["Turnover"].ToString();
        //        objData[i, 10] = dr["PricingStyle"].ToString();
        //        objData[i, 11] = dr["SpotQP"].ToString();
        //        objData[i, 12] = dr["Spread"].ToString();
        //        objData[i, 13] = dr["Premium"].ToString();
        //        objData[i, 14] = dr["DelayFee"].ToString();
        //        objData[i, 15] = dr["OtherFee"].ToString();
        //        objData[i, 16] = dr["FinalPrice"].ToString();
        //    }

        //    return objData;
        //}

        public override DataTable SetExcelRangeData(DataTable source)
        {
            string[] strs = new string[] { "PricingId", "ContractNo", "OutContractNo", "CorpName", "AssetName", "TradeCode", "ExchangeCode", "CurrencyName", "TurnoverHand", "Turnover", "PricingStyle", "SpotQP", "Spread", "Premium", "DelayFee", "OtherFee", "FinalPrice" };

            return source.ConvertDataTable(strs);
        }

        #endregion
    }
}
