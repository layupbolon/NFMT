/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CustomsClearanceBLL.cs
// 文件功能描述：报关dbo.St_CustomsClearance业务逻辑类。
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
    /// 报关dbo.St_CustomsClearance业务逻辑类。
    /// </summary>
    public class CustomsClearanceBLL : Common.ExecBLL
    {
        private CustomsClearanceDAL customsclearanceDAL = new CustomsClearanceDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CustomsClearanceDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomsClearanceBLL()
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
            get { return this.customsclearanceDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int person, int corpId, int assetId, DateTime fromDate, DateTime toDate, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cc.CustomsId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "cc.CustomsId,e.Name,c.CorpName,cc.CustomsDate,ass.AssetName,CONVERT(varchar,cc.GrossWeight)+mu.MUName as  GrossWeight,CONVERT(varchar,cc.NetWeight)+mu.MUName as NetWeight,CONVERT(varchar,cc.CustomsPrice)+cur.CurrencyName as CustomsPrice,cc.TariffRate,cc.AddedValueRate,CONVERT(varchar,cc.OtherFees)+cur.CurrencyName as OtherFees,cc.Memo,cc.CustomsStatus,bd.StatusName ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_CustomsClearance cc ");
            sb.Append(" left join dbo.St_CustomsClearanceApply cca on cc.CustomsApplyId = cca.CustomsApplyId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on cc.Customser = e.EmpId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c on cc.CustomsCorpId = c.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on cca.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on cca.UnitId = mu.MUId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cc.CurrencyId = cur.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on cc.CustomsStatus = bd.DetailId and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (person > 0)
                sb.AppendFormat(" and cc.Customser = {0} ", person);
            if (corpId > 0)
                sb.AppendFormat(" and cc.CustomsCorpId = {0} ", corpId);
            if (assetId > 0)
                sb.AppendFormat(" and cca.AssetId = {0} ", assetId);
            if (fromDate > Common.DefaultValue.DefaultTime && toDate >= fromDate)
                sb.AppendFormat(" and cc.CustomsDate between '{0}' and '{1}' ", fromDate.ToString(), toDate.AddDays(1).ToString());
            if (status > 0)
                sb.AppendFormat(" and cc.CustomsStatus = {0} ", status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetCanApplySelectModel(int pageIndex, int pageSize, string orderStr, int applyDept, DateTime applyTimeBegin, DateTime applyTimeEnd, int assetId, int outCorpId, int customCorpId)
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
            sb.Append(" cca.CustomsApplyId,a.ApplyNo,a.ApplyTime,bd.StatusName,e.Name,d.DeptName,ass.AssetName,c1.CorpName as OutCorpId,c2.CorpName as CustomsCorpId,CONVERT(varchar,cca.GrossWeight)+mu.MUName as GrossWeight,CONVERT(varchar,cca.NetWeight)+mu.MUName as NetWeight ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_CustomsClearanceApply cca ");
            sb.Append(" left join dbo.Apply a on cca.ApplyId = a.ApplyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = a.ApplyStatus and bd.StatusId = {0} ", statusId);
            sb.Append(" left join NFMT_User.dbo.Employee e on e.EmpId = a.EmpId ");
            sb.Append(" left join NFMT_User.dbo.Department d on d.DeptId = a.ApplyDept ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on cca.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c1 on cca.OutCorpId = c1.CorpId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c2 on cca.CustomsCorpId = c2.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on cca.UnitId = mu.MUId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" a.ApplyStatus = {0} and cca.CustomsApplyId in (select a.CustomsApplyId  from St_CustomsApplyDetail a where (select COUNT(DetailId) from St_CustomsApplyDetail cad where cad.DetailStatus = {0} and cad.CustomsApplyId = a.CustomsApplyId)>(select COUNT(CustomsApplyDetailId) from St_CustomsDetail cd where cd.DetailStatus = {0} and cd.CustomsApplyId = a.CustomsApplyId)) ", (int)NFMT.Common.StatusEnum.已生效);

            if (applyDept > 0)
                sb.AppendFormat(" and a.ApplyDept = {0} ", applyDept);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd >= applyTimeBegin)
                sb.AppendFormat(" and a.ApplyTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());
            if (assetId > 0)
                sb.AppendFormat(" and cca.AssetId = {0} ", assetId);
            if (outCorpId > 0)
                sb.AppendFormat(" and cca.OutCorpId = {0} ", outCorpId);
            if (customCorpId > 0)
                sb.AppendFormat(" and cca.CustomsCorpId = {0} ", customCorpId);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cad.DetailId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "cad.DetailId,sto.StockId,sto.StockDate,sn.RefNo,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.Bundles,sto.UintId,sto.CurGrossAmount,cast(sto.CurGrossAmount as varchar)+mu.MUName as CurGrossAmountName,sto.CurNetAmount,cast(sto.CurNetAmount as varchar)+mu.MUName as CurNetAmountName,bra.BrandName,custom.DetailName as CustomsTypeName,sd.StatusName,dp.DPName,sto.CardNo ";

            int customsType = (int)Data.StyleEnum.报关状态;
            int statusType = (int)Common.StatusTypeEnum.库存状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat(" dbo.St_CustomsApplyDetail cad left join dbo.St_Stock sto on cad.StockId = sto.StockId and cad.DetailStatus = {0} ", (int)Common.StatusEnum.已生效);
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
            sb.Append(" 1=1 ");
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetStockListSelectModel(int pageIndex, int pageSize, string orderStr, string sids, int customsApplyId)
        {
            SelectModel select = GetSelectModel(pageIndex, pageSize, orderStr);

            if (!string.IsNullOrEmpty(sids))
                select.WhereStr += string.Format(" and sto.StockId in ({0}) and cad.CustomsApplyId = {1}", sids, customsApplyId);
            else
                select.WhereStr += " and 1=2 ";

            return select;
        }

        public SelectModel GetUpdateStockListSelectModel(int pageIndex, int pageSize, string orderStr, string sids, int customId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cad.DetailId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "cad.DetailId,sto.StockId,sto.StockDate,sn.RefNo,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.Bundles,sto.UintId,sto.CurGrossAmount,cast(sto.CurGrossAmount as varchar)+mu.MUName as CurGrossAmountName,sto.CurNetAmount,cast(sto.CurNetAmount as varchar)+mu.MUName as CurNetAmountName,bra.BrandName,custom.DetailName as CustomsTypeName,sd.StatusName,cd.DeliverPlaceId,del.DPName as DeliverPlace,cd.CardNo";

            int customsType = (int)Data.StyleEnum.报关状态;
            int statusType = (int)Common.StatusTypeEnum.库存状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat(" dbo.St_CustomsApplyDetail cad left join dbo.St_Stock sto on cad.StockId = sto.StockId and cad.DetailStatus = {0} ", (int)Common.StatusEnum.已生效);
            sb.AppendFormat(" left join dbo.St_CustomsDetail cd on cad.DetailId = cd.CustomsApplyDetailId and cd.DetailStatus = {0} and cd.CustomsId= {1}", (int)Common.StatusEnum.已生效, customId);
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace del on cd.DeliverPlaceId = del.DPId ");
            sb.Append(" left join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = sto.StockStatus  and sd.StatusId ={0} ", statusType);
            sb.Append(" left join NFMT_User.dbo.Corporation cor on sto.CorpId = cor.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail custom on custom.StyleDetailId = sto.CustomsType and custom.BDStyleId ={0} ", customsType);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" cad.CustomsApplyId = (select CustomsApplyId from dbo.St_CustomsClearance where CustomsId = {0}) ", customId);
            if (!string.IsNullOrEmpty(sids))
                sb.AppendFormat(" and sto.StockId in ({0}) ", sids);
            else
                sb.Append(" and 1=2 ");
            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Create(UserModel user, Model.CustomsClearance customsClearance, List<Model.CustomsDetail> details)
        {
            ResultModel result = new ResultModel();
            DAL.CustomsDetailDAL customsDetailDAL = new CustomsDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    customsClearance.Customser = user.EmpId;
                    customsClearance.CustomsDate = DateTime.Now;
                    customsClearance.CustomsName = 0;

                    result = customsclearanceDAL.Insert(user, customsClearance);
                    if (result.ResultStatus != 0)
                        return result;

                    int customsId = (int)result.ReturnValue;

                    if (details != null && details.Any())
                    {
                        foreach (Model.CustomsDetail detail in details)
                        {
                            detail.CustomsId = customsId;
                            result = customsDetailDAL.Insert(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = customsId;

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

        public ResultModel Update(UserModel user, Model.CustomsClearance customsClearance, List<Model.CustomsDetail> details)
        {
            ResultModel result = new ResultModel();
            DAL.CustomsDetailDAL customsDetailDAL = new CustomsDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = customsclearanceDAL.Get(user, customsClearance.CustomsId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CustomsClearance customsClearanceResult = result.ReturnValue as Model.CustomsClearance;
                    if (customsClearanceResult == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    customsClearanceResult.Customser = user.EmpId;
                    customsClearanceResult.CustomsCorpId = customsClearance.CustomsCorpId;
                    customsClearanceResult.CustomsDate = DateTime.Now;
                    customsClearanceResult.CustomsName = 0;
                    customsClearanceResult.GrossWeight = customsClearance.GrossWeight;
                    customsClearanceResult.NetWeight = customsClearance.NetWeight;
                    customsClearanceResult.CurrencyId = customsClearance.CurrencyId;
                    customsClearanceResult.CustomsPrice = customsClearance.CustomsPrice;
                    customsClearanceResult.TariffRate = customsClearance.TariffRate;
                    customsClearanceResult.AddedValueRate = customsClearance.AddedValueRate;
                    customsClearanceResult.OtherFees = customsClearance.OtherFees;
                    customsClearanceResult.Memo = customsClearance.Memo;

                    result = customsclearanceDAL.Update(user, customsClearanceResult);
                    if (result.ResultStatus != 0)
                        return result;

                    result = customsDetailDAL.Load(user, customsClearance.CustomsId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CustomsDetail> customsDetails = result.ReturnValue as List<Model.CustomsDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取明细失败";
                        return result;
                    }

                    foreach (Model.CustomsDetail detail in customsDetails)
                    {
                        if (detail.DetailStatus == Common.StatusEnum.已生效)
                            detail.DetailStatus = Common.StatusEnum.已录入;
                        result = customsDetailDAL.Invalid(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (details != null && details.Any())
                    {
                        foreach (Model.CustomsDetail detail in details)
                        {
                            detail.CustomsId = customsClearance.CustomsId;
                            result = customsDetailDAL.Insert(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = customsClearance.CustomsId;

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

        public ResultModel GoBack(UserModel user, int customId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = customsclearanceDAL.Get(user, customId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CustomsClearance customsClearance = result.ReturnValue as Model.CustomsClearance;
                    if (customsClearance == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取数据出错";
                        return result;
                    }

                    result = customsclearanceDAL.Goback(user, customsClearance);
                    if (result.ResultStatus != 0)
                        return result;

                    //工作流任务关闭
                    WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, customsClearance);
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

        public ResultModel Invalid(UserModel user, int customId)
        {
            ResultModel result = new ResultModel();
            DAL.CustomsDetailDAL customsDetailDAL = new CustomsDetailDAL();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = customsclearanceDAL.Get(user, customId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CustomsClearance customsClearance = result.ReturnValue as Model.CustomsClearance;
                    if (customsClearance == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取数据出错";
                        return result;
                    }

                    result = customsclearanceDAL.Invalid(user, customsClearance);
                    if (result.ResultStatus != 0)
                        return result;

                    result = customsDetailDAL.Load(user, customId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CustomsDetail> details = result.ReturnValue as List<Model.CustomsDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取数据出错";
                        return result;
                    }

                    foreach (Model.CustomsDetail detail in details)
                    {
                        if (detail.DetailStatus == Common.StatusEnum.已生效)
                            detail.DetailStatus = Common.StatusEnum.已录入;

                        result = customsDetailDAL.Invalid(user, detail);
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

        public ResultModel Complete(UserModel user, int customId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.customsclearanceDAL.Get(user, customId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CustomsClearance customsClearance = result.ReturnValue as CustomsClearance;
                    if (customsClearance == null)
                    {
                        result.Message = "该数据不存在，不能完成";
                        return result;
                    }

                    result = customsclearanceDAL.Complete(user, customsClearance);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取所有已生效的明细
                    DAL.CustomsDetailDAL customsDetailDAL = new CustomsDetailDAL();
                    result = customsDetailDAL.Load(user, customId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CustomsDetail> details = result.ReturnValue as List<Model.CustomsDetail>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                    DAL.StockDAL stockDAL = new StockDAL();

                    foreach (Model.CustomsDetail detail in details)
                    {
                        //明细完成
                        result = customsDetailDAL.Complete(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取报关流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "报关流水获取失败";
                            return result;
                        }

                        //完成报关流水
                        result = stockLogDAL.Complete(user, stockLog);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取库存
                        result = stockDAL.Get(user, detail.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.Stock stock = result.ReturnValue as Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存获取失败";
                            return result;
                        }

                        stock.StockStatus = (int)stock.PreStatus == 0 ? StockStatusEnum.在库正常 : stock.PreStatus;
                        stock.CustomsType = NFMT.Data.DetailProvider.Details(Data.StyleEnum.CustomType)["InsideCustom"].StyleDetailId;//关内
                        result = stockDAL.Update(user, stock);
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

        public ResultModel CompleteCancel(UserModel user, int customId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    result = this.customsclearanceDAL.Get(user, customId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CustomsClearance customsClearance = result.ReturnValue as CustomsClearance;
                    if (customsClearance == null)
                    {
                        result.Message = "该数据不存在，不能完成";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = customsclearanceDAL.CompleteCancel(user, customsClearance);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取所有已生效的明细
                    DAL.CustomsDetailDAL customsDetailDAL = new CustomsDetailDAL();
                    result = customsDetailDAL.Load(user, customId, StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CustomsDetail> details = result.ReturnValue as List<Model.CustomsDetail>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                    DAL.StockDAL stockDAL = new StockDAL();

                    foreach (Model.CustomsDetail detail in details)
                    {
                        //明细完成
                        result = customsDetailDAL.CompleteCancel(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取报关流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "报关流水获取失败";
                            return result;
                        }

                        //完成报关流水
                        result = stockLogDAL.CompleteCancel(user, stockLog);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取库存
                        result = stockDAL.Get(user, detail.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.Stock stock = result.ReturnValue as Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存获取失败";
                            return result;
                        }

                        stock.StockStatus = StockStatusEnum.预报关库存;
                        stock.CustomsType = NFMT.Data.DetailProvider.Details(Data.StyleEnum.CustomType)["OutsideCustom"].StyleDetailId;//关外
                        result = stockDAL.Update(user, stock);
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

        //报关审核生效	   流水操作：写入 流水类型：报关 流水状态：已生效    库存操作：更新 库存状态：预报关库存
        public ResultModel Audit(UserModel user, NFMT.WorkFlow.Model.DataSource dataSource, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.customsclearanceDAL.Get(user, dataSource.RowId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CustomsClearance customsClearance = result.ReturnValue as Model.CustomsClearance;
                    if (customsClearance == null || customsClearance.CustomsId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "报关不存在";
                        return result;
                    }

                    //审核，修改数据状态
                    result = this.customsclearanceDAL.Audit(user, customsClearance, isPass);
                    if (result.ResultStatus != 0)
                        return result;

                    //审核通过
                    if (isPass)
                    {
                        //加载已生效明细
                        DAL.CustomsDetailDAL customsDetailDAL = new CustomsDetailDAL();
                        result = customsDetailDAL.Load(user, customsClearance.CustomsId, StatusEnum.已生效);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.CustomsDetail> details = result.ReturnValue as List<Model.CustomsDetail>;
                        if (details == null || !details.Any())
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取失败";
                            return result;
                        }

                        DAL.StockDAL stockDAL = new StockDAL();
                        DAL.StockNameDAL stockNameDAL = new StockNameDAL();
                        DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                        Model.Stock stock = null;
                        Model.StockName stockName = null;
                        Model.StockLog stockLog = null;

                        foreach (Model.CustomsDetail detail in details)
                        {
                            //获取库存
                            result = stockDAL.Get(user, detail.StockId);
                            if (result.ResultStatus != 0)
                                return result;

                            stock = result.ReturnValue as Model.Stock;
                            if (stock == null)
                            {
                                result.ResultStatus = -1;
                                result.Message = "获取库存失败";
                                return result;
                            }

                            //获取业务单号
                            result = stockNameDAL.Get(user, stock.StockNameId);
                            if (result.ResultStatus != 0)
                                return result;

                            stockName = result.ReturnValue as Model.StockName;
                            if (stockName == null)
                            {
                                result.ResultStatus = -1;
                                result.Message = "获取业务单号失败";
                                return result;
                            }

                            //创建库存流水
                            stockLog = new StockLog()
                            {
                                StockId = stock.StockId,
                                StockNameId = stock.StockNameId,
                                RefNo = stockName.RefNo,
                                LogDirection = (int)LogDirectionEnum.Out,
                                LogType = (int)LogTypeEnum.报关,
                                //ContractId = 
                                //SubContractId = 
                                LogDate = DateTime.Now,
                                OpPerson = user.EmpId,
                                Bundles = stock.Bundles,
                                GrossAmount = stock.GrossAmount,
                                NetAmount = stock.NetAmount,
                                MUId = stock.UintId,
                                BrandId = stock.BrandId,
                                DeliverPlaceId = stock.DeliverPlaceId,
                                PaperNo = stock.PaperNo,
                                PaperHolder = stock.PaperHolder,
                                CardNo = stock.CardNo,
                                Memo = stock.Memo,
                                LogStatus = StatusEnum.已生效,
                                LogSourceBase = "NFMT",
                                LogSource = "dbo.St_CustomsClearance",
                                SourceId = customsClearance.CustomsId
                            };

                            result = stockLogDAL.Insert(user, stockLog);
                            if (result.ResultStatus != 0)
                                return result;

                            int stockLogId = 0;
                            if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out stockLogId) || stockLogId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "流水添加失败";
                                return result;
                            }

                            //将库存流水Id反向更新到明细表中
                            detail.StockLogId = stockLogId;
                            result = customsDetailDAL.Update(user, detail);
                            if (result.ResultStatus != 0)
                                return result;

                            //更新库存状态
                            result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.预报关库存);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

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

        public ResultModel Close(UserModel user, int customId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取报关
                    result = this.customsclearanceDAL.Get(user, customId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CustomsClearance customsClearance = result.ReturnValue as Model.CustomsClearance;
                    if (customsClearance == null || customsClearance.CustomsId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "报关不存在";
                        return result;
                    }

                    //报关关闭
                    result = this.customsclearanceDAL.Close(user, customsClearance);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取所有已完成的明细
                    DAL.CustomsDetailDAL customsDetailDAL = new CustomsDetailDAL();
                    result = customsDetailDAL.Load(user, customId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CustomsDetail> details = result.ReturnValue as List<Model.CustomsDetail>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                    DAL.StockDAL stockDAL = new StockDAL();

                    foreach (Model.CustomsDetail detail in details)
                    {
                        //明细关闭
                        result = customsDetailDAL.Close(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取报关流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "报关流水获取失败";
                            return result;
                        }

                        //完成报关流水
                        result = stockLogDAL.Close(user, stockLog);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取库存
                        result = stockDAL.Get(user, detail.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.Stock stock = result.ReturnValue as Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存获取失败";
                            return result;
                        }

                        result = stockDAL.UpdateStockStatusDirect(stock, StockStatusEnum.在库正常);
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

        #endregion

        #region report

        public SelectModel GetCustomReportSelect(int pageIndex, int pageSize, string orderStr, int customsCorpId, string refNo, DateTime startDate, DateTime endDate)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cc.CustomsId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)Common.StatusEnum.已生效;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            select.ColumnName = " cc.CustomsId,cc.CustomsDate,sn.RefNo,detail.GrossWeight,detail.NetWeight,mu.MUName,corp.CorpName,cur.CurrencyName,detail.CustomsPrice ";

            sb.Clear();
            sb.Append(" NFMT..St_CustomsClearance cc ");
            sb.Append(" left join NFMT..St_CustomsClearanceApply cca on cca.CustomsApplyId = cc.CustomsApplyId ");
            sb.Append(" left join NFMT_Basic..MeasureUnit mu on mu.MUId = cca.UnitId ");
            sb.Append(" left join NFMT..St_CustomsDetail detail on detail.CustomsId = cc.CustomsId ");
            sb.Append(" left join NFMT..St_Stock st on st.StockId = detail.StockId ");
            sb.Append(" left join NFMT..St_StockName sn on sn.StockNameId = st.StockNameId ");
            sb.Append(" left join NFMT_User..Corporation corp on cc.CustomsCorpId = corp.CorpId ");
            sb.Append(" left join NFMT_Basic..Currency cur on cur.CurrencyId = cc.CurrencyId ");

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" cc.CustomsStatus >= {0} ", readyStatus);
            if (!string.IsNullOrEmpty(refNo))
                sb.AppendFormat(" and sn.RefNo like '%{0}%' ", refNo);
            if (customsCorpId > 0)
                sb.AppendFormat(" and cc.CustomsCorpId = {0} ", customsCorpId);
            if (startDate > Common.DefaultValue.DefaultTime && endDate > startDate)
                sb.AppendFormat(" and cc.CustomsDate between '{0}' and '{1}' ", startDate.ToString(), endDate.AddDays(1).ToString());

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

        public override DataTable SetExcelRangeData(DataTable source)
        {
            string[] strs = new string[] { "CustomsDate", "RefNo", "GrossWeight", "NetWeight", "MUName", "CorpName", "CurrencyName", "CustomsPrice" };

            return source.ConvertDataTable(strs);
        }

        #endregion
    }
}
