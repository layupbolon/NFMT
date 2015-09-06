/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CustomsClearanceApplyBLL.cs
// 文件功能描述：报关申请dbo.St_CustomsClearanceApply业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年10月29日
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
    /// 报关申请dbo.St_CustomsClearanceApply业务逻辑类。
    /// </summary>
    public class CustomsClearanceApplyBLL : Common.ApplyBLL
    {
        private CustomsClearanceApplyDAL customsclearanceapplyDAL = new CustomsClearanceApplyDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CustomsClearanceApplyDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomsClearanceApplyBLL()
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
            get { return this.customsclearanceapplyDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int applyPerson, int status, DateTime applyTimeBegin, DateTime applyTimeEnd, int corpId, int assetId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cca.CustomsApplyId desc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)NFMT.Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" cca.CustomsApplyId,a.ApplyNo,e.Name,a.ApplyTime,a.ApplyDesc,ass.AssetName,CONVERT(varchar,cca.GrossWeight) + mu.MUName as GrossWeight,CONVERT(varchar,cca.NetWeight) + mu.MUName as NetWeight,c1.CorpName as OutCorpId,c2.CorpName as InCorpId,c3.CorpName as CustomsCorpId,CONVERT(varchar,cca.CustomsPrice) + c.CurrencyName as CustomsPrice,a.ApplyStatus,bd.StatusName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_CustomsClearanceApply cca ");
            sb.Append(" left join dbo.Apply a on cca.ApplyId = a.ApplyId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on a.EmpId = e.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on a.ApplyStatus = bd.DetailId and bd.StatusId = {0} ", statusId);
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on cca.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on cca.UnitId = mu.MUId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c1 on cca.OutCorpId = c1.CorpId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c2 on cca.InCorpId = c2.CorpId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c3 on cca.CustomsCorpId = c3.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on cca.CurrencyId = c.CurrencyId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (applyPerson > 0)
                sb.AppendFormat(" and a.EmpId = {0} ", applyPerson);
            if (status > 0)
                sb.AppendFormat(" and a.ApplyStatus = {0} ", status);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd >= applyTimeBegin)
                sb.AppendFormat(" and a.ApplyTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());
            if (corpId > 0)
                sb.AppendFormat(" and cca.CustomsCorpId = {0} ", corpId);
            if (assetId > 0)
                sb.AppendFormat(" and cca.AssetId = {0} ", assetId);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "sto.StockId,sto.StockDate,sn.RefNo,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.Bundles,sto.UintId,sto.CurGrossAmount,cast(sto.CurGrossAmount as varchar)+mu.MUName as CurGrossAmountName,sto.CurNetAmount,cast(sto.CurNetAmount as varchar)+mu.MUName as CurNetAmountName,bra.BrandName,custom.DetailName as CustomsTypeName,sd.StatusName,dp.DPName,sto.CardNo ";

            int customsType = (int)Data.StyleEnum.报关状态;
            int statusType = (int)Common.StatusTypeEnum.库存状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_Stock sto ");
            sb.Append(" left join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = sto.StockStatus  and sd.StatusId ={0} ", statusType);
            sb.Append(" left join NFMT_User.dbo.Corporation cor on sto.CorpId = cor.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail custom on custom.StyleDetailId = sto.CustomsType and custom.BDStyleId ={0} ", customsType);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" (sto.CurGrossAmount > 0 or sto.CurNetAmount > 0) ");
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetCanCustomStockListSelect(int pageIndex, int pageSize, string orderStr, string stockName, int corpId, int assetId, int unitId, string sids)
        {
            NFMT.Common.SelectModel select = GetSelectModel(pageIndex, pageSize, orderStr);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat(" and sto.CustomsType = {0} and sto.StockStatus = {1} ", NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.报关状态)["OutsideCustom"].StyleDetailId, (int)StockStatusEnum.在库正常);
            if (!string.IsNullOrEmpty(stockName))
                sb.AppendFormat(" and sn.RefNo like '%{0}%'", stockName);
            if (corpId > 0)
                sb.AppendFormat(" and sto.CorpId = {0}", corpId);
            if (assetId > 0)
                sb.AppendFormat(" and sto.AssetId = {0} ", assetId);
            if (unitId > 0)
                sb.AppendFormat(" and sto.UintId = {0} ", unitId);
            if (!string.IsNullOrEmpty(sids))
                sb.AppendFormat(" and sto.StockId not in ({0}) ", sids);

            select.WhereStr += sb.ToString();

            return select;
        }

        public SelectModel GetStockListSelect(int pageIndex, int pageSize, string orderStr, string sids)
        {
            NFMT.Common.SelectModel select = GetSelectModel(pageIndex, pageSize, orderStr);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(sids))
                sb.AppendFormat(" and sto.StockId in ({0}) ", sids);
            else
                sb.Append(" and 1=2");

            select.WhereStr += sb.ToString();

            return select;
        }

        public ResultModel Create(UserModel user, NFMT.Operate.Model.Apply apply, Model.CustomsClearanceApply customsClearanceApply, List<Model.CustomsApplyDetail> details)
        {
            ResultModel result = new ResultModel();
            NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
            DAL.CustomsApplyDetailDAL customsApplyDetailDAL = new CustomsApplyDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = applyDAL.Insert(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    int applyId = (int)result.ReturnValue;

                    customsClearanceApply.ApplyId = applyId;

                    result = customsclearanceapplyDAL.Insert(user, customsClearanceApply);
                    if (result.ResultStatus != 0)
                        return result;

                    int customsApplyId = (int)result.ReturnValue;

                    if (details != null && details.Any())
                    {
                        foreach (Model.CustomsApplyDetail detail in details)
                        {
                            detail.CustomsApplyId = customsApplyId;
                            result = customsApplyDetailDAL.Insert(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = applyId;
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

        public ResultModel Update(UserModel user, NFMT.Operate.Model.Apply apply, Model.CustomsClearanceApply customsClearanceApply, List<Model.CustomsApplyDetail> details)
        {
            ResultModel result = new ResultModel();
            NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
            DAL.CustomsApplyDetailDAL customsApplyDetailDAL = new CustomsApplyDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = applyDAL.Get(user, apply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply applyResult = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (applyResult == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取数据失败";
                        return result;
                    }

                    applyResult.ApplyType = apply.ApplyType;
                    applyResult.EmpId = user.EmpId;
                    applyResult.ApplyTime = DateTime.Now;
                    applyResult.ApplyDept = apply.ApplyDept;
                    applyResult.ApplyDesc = apply.ApplyDesc;

                    result = applyDAL.Update(user, applyResult);
                    if (result.ResultStatus != 0)
                        return result;

                    result = customsclearanceapplyDAL.Get(user, customsClearanceApply.CustomsApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.CustomsClearanceApply customsClearanceApplyResult = result.ReturnValue as NFMT.WareHouse.Model.CustomsClearanceApply;
                    if (customsClearanceApplyResult == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取数据失败";
                        return result;
                    }

                    customsClearanceApplyResult.ApplyId = customsClearanceApply.ApplyId;
                    customsClearanceApplyResult.AssetId = customsClearanceApply.AssetId;
                    customsClearanceApplyResult.GrossWeight = customsClearanceApply.GrossWeight;
                    customsClearanceApplyResult.NetWeight = customsClearanceApply.NetWeight;
                    customsClearanceApplyResult.UnitId = customsClearanceApply.UnitId;
                    customsClearanceApplyResult.OutCorpId = customsClearanceApply.OutCorpId;
                    customsClearanceApplyResult.InCorpId = customsClearanceApply.InCorpId;
                    customsClearanceApplyResult.CustomsCorpId = customsClearanceApply.CustomsCorpId;
                    customsClearanceApplyResult.CustomsPrice = customsClearanceApply.CustomsPrice;
                    customsClearanceApplyResult.CurrencyId = customsClearanceApply.CurrencyId;

                    customsClearanceApplyResult.Status = Common.StatusEnum.已录入;
                    result = customsclearanceapplyDAL.Update(user, customsClearanceApplyResult);
                    if (result.ResultStatus != 0)
                        return result;

                    result = customsApplyDetailDAL.Load(user, customsClearanceApply.CustomsApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CustomsApplyDetail> customsApplyDetails = result.ReturnValue as List<Model.CustomsApplyDetail>;
                    if (customsApplyDetails == null || !customsApplyDetails.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取数据失败";
                        return result;
                    }

                    foreach (Model.CustomsApplyDetail detail in customsApplyDetails)
                    {
                        if (detail.DetailStatus == Common.StatusEnum.已生效)
                            detail.DetailStatus = Common.StatusEnum.已录入;
                        result = customsApplyDetailDAL.Invalid(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (details != null && details.Any())
                    {
                        foreach (Model.CustomsApplyDetail detail in details)
                        {
                            detail.CustomsApplyId = customsClearanceApply.CustomsApplyId;
                            result = customsApplyDetailDAL.Insert(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }
                    if (result.ResultStatus == 0)
                        result.ReturnValue = customsClearanceApply.CustomsApplyId;
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

        public ResultModel Goback(UserModel user, int customApplyId)
        {
            ResultModel result = new ResultModel();
            Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证报关申请
                    result = customsclearanceapplyDAL.Get(user, customApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CustomsClearanceApply customsClearanceApply = result.ReturnValue as Model.CustomsClearanceApply;
                    if (customsClearanceApply == null || customsClearanceApply.CustomsApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "报关申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, customsClearanceApply.ApplyId);
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

        public ResultModel Invalid(UserModel user, int customApplyId)
        {
            ResultModel result = new ResultModel();
            Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
            DAL.CustomsApplyDetailDAL customsApplyDetailDAL = new CustomsApplyDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证出库申请
                    result = customsclearanceapplyDAL.Get(user, customApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CustomsClearanceApply customsClearanceApply = result.ReturnValue as Model.CustomsClearanceApply;
                    if (customsClearanceApply == null || customsClearanceApply.CustomsApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "报关申请不存在";
                        return result;
                    }

                    //获取主申请实体
                    result = applyDAL.Get(user, customsClearanceApply.ApplyId);
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
                    result = customsApplyDetailDAL.Load(user, customsClearanceApply.CustomsApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CustomsApplyDetail> details = result.ReturnValue as List<Model.CustomsApplyDetail>;
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
                    foreach (Model.CustomsApplyDetail detail in details)
                    {
                        if (detail.DetailStatus == StatusEnum.已生效)
                            detail.DetailStatus = StatusEnum.已录入;
                        result = customsApplyDetailDAL.Invalid(user, detail);
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

        public ResultModel Confirm(UserModel user, int customApplyId)
        {
            ResultModel result = new ResultModel();
            Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
            DAL.CustomsApplyDetailDAL customsApplyDetailDAL = new CustomsApplyDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证报关申请
                    result = customsclearanceapplyDAL.Get(user, customApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CustomsClearanceApply customsClearanceApply = result.ReturnValue as Model.CustomsClearanceApply;
                    if (customsClearanceApply == null || customsClearanceApply.CustomsApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "报关申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, customsClearanceApply.ApplyId);
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
                    result = customsclearanceapplyDAL.CheckCustomCanConfirm(user, customsClearanceApply.CustomsApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Common.StatusEnum status = (Common.StatusEnum)result.ReturnValue;

                    //获取已生效报关申请明细
                    result = customsApplyDetailDAL.Load(user, customsClearanceApply.CustomsApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CustomsApplyDetail> details = result.ReturnValue as List<Model.CustomsApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取报关申请明细失败";
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

                    //报关申请明细更新状态至已完成
                    foreach (Model.CustomsApplyDetail detail in details)
                    {
                        //报关申请明细更新状态至已完成
                        result = customsApplyDetailDAL.Confirm(user, detail);
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

        public ResultModel ConfirmCancel(UserModel user, int customApplyId)
        {
            ResultModel result = new ResultModel();
            DAL.CustomsApplyDetailDAL customsApplyDetailDAL = new CustomsApplyDetailDAL();
            Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证报关申请
                    result = customsclearanceapplyDAL.Get(user, customApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CustomsClearanceApply customsClearanceApply = result.ReturnValue as Model.CustomsClearanceApply;
                    if (customsClearanceApply == null || customsClearanceApply.CustomsApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "报关申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, customsClearanceApply.ApplyId);
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

                    //报关申请明细，在已完成状态下的更新至已生效
                    //获取已关闭的明细
                    result = customsApplyDetailDAL.Load(user, customsClearanceApply.CustomsApplyId, Common.StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CustomsApplyDetail> details = result.ReturnValue as List<Model.CustomsApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取申请明细失败";
                        return result;
                    }

                    foreach (Model.CustomsApplyDetail detail in details)
                    {
                        result = customsApplyDetailDAL.ConfirmCancel(user, detail);
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
                result = customsclearanceapplyDAL.GetModelByApplyId(user, applyId);
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
