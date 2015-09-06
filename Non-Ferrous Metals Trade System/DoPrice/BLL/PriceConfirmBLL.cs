/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PriceConfirmBLL.cs
// 文件功能描述：价格确认表dbo.Pri_PriceConfirm业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年1月26日
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
    /// 价格确认表dbo.Pri_PriceConfirm业务逻辑类。
    /// </summary>
    public class PriceConfirmBLL : Common.ExecBLL
    {
        private PriceConfirmDAL priceconfirmDAL = new PriceConfirmDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PriceConfirmDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PriceConfirmBLL()
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
            get { return this.priceconfirmDAL; }
        }

        #endregion

        #region 新增方法

        public Common.SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, string subNo, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pc.PriceConfirmId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("pc.PriceConfirmId,corpIn.CorpName as InCorpName,corpOut.CorpName as OutCorpName,sub.OutContractNo,CONVERT(varchar,pc.ContractAmount)+mu.MUName as ContractAmount,CONVERT(varchar,pc.SubAmount)+mu.MUName as SubAmount,CONVERT(varchar,pc.RealityAmount)+mu.MUName as RealityAmount,CONVERT(varchar,pc.PricingAvg)+cur.CurrencyName as PricingAvg,CONVERT(varchar,pc.PremiumAvg)+cur.CurrencyName as PremiumAvg,CONVERT(varchar,pc.InterestAvg)+cur.CurrencyName as InterestAvg,CONVERT(varchar,pc.InterestBala)+cur.CurrencyName as InterestBala,CONVERT(varchar,pc.SettlePrice)+cur.CurrencyName as SettlePrice,CONVERT(varchar,pc.SettleBala)+cur.CurrencyName as SettleBala,pc.PricingDate,corpTake.CorpName as TakeCorpName,pc.ContactPerson,pc.Memo,bd.StatusName,pc.PriceConfirmStatus");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_PriceConfirm pc ");
            sb.Append(" left join NFMT_User..Corporation corpIn on pc.InCorpId = corpIn.CorpId ");
            sb.Append(" left join NFMT_User..Corporation corpOut on pc.OutCorpId = corpOut.CorpId ");
            sb.Append(" left join NFMT..Con_Contract con on pc.ContractId = con.ContractId ");
            sb.Append(" left join NFMT..Con_ContractSub sub on pc.SubId = sub.SubId ");
            sb.Append(" left join NFMT_Basic..MeasureUnit mu on con.UnitId = mu.MUId ");
            sb.Append(" left join NFMT_Basic..Currency cur on sub.SettleCurrency = cur.CurrencyId ");
            sb.Append(" left join NFMT_User..Corporation corpTake on pc.TakeCorpId = corpTake.CorpId ");
            sb.Append(" left join NFMT_Basic..BDStatusDetail bd on pc.PriceConfirmStatus = bd.DetailId ");
            select.TableName = sb.ToString();

            sb.Clear();
            if (status > 0)
                sb.AppendFormat(" pc.PriceConfirmStatus = {0} ", status);
            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and sub.OutContractNo like '%{0}%' ", subNo);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetPriceConfirmContractListSelect(int pageIndex, int pageSize, string orderStr, string subNo)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cs.SubId asc";
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
            //sb.AppendFormat(" and exists (select 1 from (select ISNULL(idetail.DetailId,0) as InterestDetailId,ISNULL(slog.StockLogId,0) as StockLogId from NFMT..Pri_InterestDetail idetail inner join NFMT..Pri_Interest i on i.InterestId = idetail.InterestId and idetail.DetailStatus>={0} and idetail.SubContractId = cs.SubId full outer join (select * from NFMT..St_StockLog slog where slog.LogStatus>={0} and slog.SubContractId = cs.SubId) slog on slog.StockLogId = idetail.StockLogId) a left join (select detail.InterestDetailId,detail.StockLogId from dbo.Pri_PriceConfirmDetail detail inner join dbo.Pri_PriceConfirm pc on pc.PriceConfirmId = detail.PriceConfirmId where detail.DetailStatus>={0} and pc.PriceConfirmStatus<>{1}) b on a.InterestDetailId = b.InterestDetailId and a.StockLogId = b.StockLogId where ISNULL(b.InterestDetailId,0)=0 and ISNULL(b.StockLogId,0)=0)", status,(int)Common.StatusEnum.已作废);

            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and (cs.SubNo like '%{0}%' or con.ContractNo like '%{0}%') ", subNo);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Create(UserModel user, NFMT.DoPrice.Model.PriceConfirm priceConfirm, List<NFMT.DoPrice.Model.PriceConfirmDetail> details,bool isSubmitAudit)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = priceconfirmDAL.Insert(user, priceConfirm);
                    if (result.ResultStatus != 0)
                        return result;

                    int priceConfirmId = (int)result.ReturnValue;

                    if (details != null && details.Any())
                    {
                        DAL.PriceConfirmDetailDAL priceConfirmDetailDAL = new PriceConfirmDetailDAL();
                        foreach (NFMT.DoPrice.Model.PriceConfirmDetail detail in details)
                        {
                            detail.PriceConfirmId = priceConfirmId;
                            result = priceConfirmDetailDAL.Insert(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    if (isSubmitAudit)
                    {
                        priceConfirm.PriceConfirmId = priceConfirmId;

                        NFMT.WorkFlow.AutoSubmit submit = new WorkFlow.AutoSubmit();
                        NFMT.WorkFlow.ITaskProvider taskProvider = new NFMT.DoPrice.TaskProvider.PriceConfirmTaskProvider();
                        result = submit.Submit(user, priceConfirm, taskProvider, WorkFlow.MasterEnum.价格确认单审核);
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
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Update(UserModel user, NFMT.DoPrice.Model.PriceConfirm priceConfirm, List<NFMT.DoPrice.Model.PriceConfirmDetail> details)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = priceconfirmDAL.Get(user, priceConfirm.PriceConfirmId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PriceConfirm priceConfirmResult = result.ReturnValue as Model.PriceConfirm;
                    if (priceConfirmResult == null || priceConfirmResult.PriceConfirmId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    priceConfirmResult.RealityAmount = priceConfirm.RealityAmount;
                    priceConfirmResult.PricingAvg = priceConfirm.PricingAvg;
                    priceConfirmResult.PremiumAvg = priceConfirm.PremiumAvg;
                    priceConfirmResult.OtherAvg = priceConfirm.OtherAvg;
                    priceConfirmResult.InterestAvg = priceConfirm.InterestAvg;
                    priceConfirmResult.InterestBala = priceConfirm.InterestBala;
                    priceConfirmResult.SettlePrice = priceConfirm.SettlePrice;
                    priceConfirmResult.SettleBala = priceConfirm.SettleBala;
                    priceConfirmResult.PricingDate = priceConfirm.PricingDate;
                    priceConfirmResult.TakeCorpId = priceConfirm.TakeCorpId;
                    priceConfirmResult.ContactPerson = priceConfirm.ContactPerson;
                    priceConfirmResult.Memo = priceConfirm.Memo;

                    result = priceconfirmDAL.Update(user, priceConfirmResult);
                    if (result.ResultStatus != 0)
                        return result;

                    DAL.PriceConfirmDetailDAL priceConfirmDetailDAL = new PriceConfirmDetailDAL();
                    result = priceConfirmDetailDAL.InvalidAll(user, priceConfirm.PriceConfirmId);
                    if (result.ResultStatus != 0)
                        return result;

                    if (details != null && details.Any())
                    {
                        foreach (NFMT.DoPrice.Model.PriceConfirmDetail detail in details)
                        {
                            detail.PriceConfirmId = priceConfirm.PriceConfirmId;
                            result = priceConfirmDetailDAL.Insert(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
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

        public ResultModel GoBack(UserModel user, int priceConfirmId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = priceconfirmDAL.Get(user, priceConfirmId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.PriceConfirm priceConfirm = result.ReturnValue as Model.PriceConfirm;
                    if (priceConfirm == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能撤返";
                        return result;
                    }

                    result = priceconfirmDAL.Goback(user, priceConfirm);
                    if (result.ResultStatus != 0)
                        return result;

                    //同步工作流状态
                    NFMT.WorkFlow.BLL.DataSourceBLL dataSourceBLL = new WorkFlow.BLL.DataSourceBLL();
                    result = dataSourceBLL.SynchronousStatus(user, priceConfirm);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ResultStatus == 0)
                        result.Message = "撤返成功";

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

        public ResultModel Invalid(UserModel user, int priceConfirmId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = priceconfirmDAL.Get(user, priceConfirmId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.PriceConfirm priceConfirm = result.ReturnValue as Model.PriceConfirm;
                    if (priceConfirm == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能作废";
                        return result;
                    }

                    result = priceconfirmDAL.Invalid(user, priceConfirm);
                    if (result.ResultStatus != 0)
                        return result;

                    DAL.PriceConfirmDetailDAL priceConfirmDetailDAL = new PriceConfirmDetailDAL();
                    result = priceConfirmDetailDAL.InvalidAll(user, priceConfirmId);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ResultStatus == 0)
                        result.Message = "作废成功";

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

        public ResultModel Complete(UserModel user, int priceConfirmId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = priceconfirmDAL.Get(user, priceConfirmId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.PriceConfirm priceConfirm = result.ReturnValue as Model.PriceConfirm;
                    if (priceConfirm == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能完成";
                        return result;
                    }

                    result = priceconfirmDAL.Complete(user, priceConfirm);
                    if (result.ResultStatus != 0)
                        return result;

                    DAL.PriceConfirmDetailDAL priceConfirmDetailDAL = new PriceConfirmDetailDAL();
                    result = priceConfirmDetailDAL.Load(user, priceConfirmId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.PriceConfirmDetail> details = result.ReturnValue as List<Model.PriceConfirmDetail>;

                    if (details != null && details.Any())
                    {
                        foreach (Model.PriceConfirmDetail detail in details)
                        {
                            result = priceConfirmDetailDAL.Complete(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }                   

                    if (result.ResultStatus == 0)
                        result.Message = "完成成功";

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

        public ResultModel CompleteCancel(UserModel user, int priceConfirmId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = priceconfirmDAL.Get(user, priceConfirmId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.PriceConfirm priceConfirm = result.ReturnValue as Model.PriceConfirm;
                    if (priceConfirm == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能完成撤销";
                        return result;
                    }

                    result = priceconfirmDAL.CompleteCancel(user, priceConfirm);
                    if (result.ResultStatus != 0)
                        return result;

                    DAL.PriceConfirmDetailDAL priceConfirmDetailDAL = new PriceConfirmDetailDAL();
                    result = priceConfirmDetailDAL.Load(user, priceConfirmId, Common.StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.PriceConfirmDetail> details = result.ReturnValue as List<Model.PriceConfirmDetail>;

                    if (details != null && details.Any())
                    {
                        foreach (Model.PriceConfirmDetail detail in details)
                        {
                            result = priceConfirmDetailDAL.CompleteCancel(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    if (result.ResultStatus == 0)
                        result.Message = "完成撤销成功";

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

        public SelectModel GetPriceConfirmStockListSelect(int pageIndex, int pageSize, string orderStr, int subId)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "slog.StockLogId desc";
            else
                select.OrderStr = orderStr;

            int status = (int)Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("ISNULL(slog.StockLogId,0) as StockLogId,ISNULL(slog.StockId,0) as StockId,ISNULL(idetail.DetailId,0) as InterestDetailId,ISNULL(idetail.InterestId,0) as InterestId,ISNULL(i.SubContractId,0) as SubContractId,cur.CurrencyName,ISNULL(i.PricingUnit,0) as TotalPricingUnit,ISNULL(i.Premium,0) as TotalPremium,ISNULL(i.OtherPrice,0) as TotalOtherPrice,ISNULL(i.InterestPrice,0) as TotalInterestPrice,i.PayCapital,i.CurCapital,i.InterestRate,ISNULL(i.InterestBala,0) as TotalInterestBala,i.InterestAmountDay,ISNULL(i.InterestAmount,slog.NetAmount) as TotalInterestAmount,i.Unit,mu.MUName,i.InterestStyle,bd.DetailName as InterestStyleName,ISNULL(idetail.InterestAmount,slog.NetAmount) as ConfirmAmount,ISNULL(idetail.PricingUnit,0) as PricingUnit,ISNULL(idetail.Premium,0) as Premium,ISNULL(idetail.OtherPrice,0) as OtherPrice,ISNULL(idetail.InterestPrice,0) as SettlePrice,idetail.StockBala,idetail.InterestStartDate,idetail.InterestEndDate,idetail.InterestDay,ISNULL(idetail.InterestUnit,0) as InterestUnit,ISNULL(idetail.InterestBala,0) as SettleBala,idetail.InterestType,bd2.DetailName as InterestTypeName,ISNULL(sn.RefNo,'本金余额') as RefNo,ISNULL(ass.AssetName,'--') as AssetName,ISNULL(bra.BrandName,'--') as BrandName,ISNULL(dp.DPName,'--') as DPName,ISNULL(slog.CardNo,'--') as CardNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT..Pri_InterestDetail idetail  ");
            sb.AppendFormat(" inner join NFMT..Pri_Interest i on i.InterestId = idetail.InterestId and idetail.DetailStatus>={0} and idetail.SubContractId = {1} ", status, subId);
            sb.AppendFormat(" full outer join (select * from NFMT..St_StockLog slog where slog.LogStatus>={0} and slog.SubContractId = {1}) slog on slog.StockLogId = idetail.StockLogId ", status, subId);
            sb.Append(" left join NFMT_Basic..Currency cur on cur.CurrencyId = i.CurrencyId ");
            sb.Append(" left join NFMT_Basic..MeasureUnit mu on mu.MUId = slog.MUId ");
            sb.Append(" left join NFMT_Basic..BDStyleDetail bd on bd.StyleDetailId = i.InterestStyle ");
            sb.Append(" left join NFMT_Basic..BDStyleDetail bd2 on bd2.StyleDetailId = idetail.InterestType ");
            sb.Append(" left join NFMT..St_StockName sn on sn.StockNameId = slog.StockNameId ");
            sb.Append(" left join NFMT_Basic..Asset ass on ass.AssetId = slog.AssetId ");
            sb.Append(" left join NFMT_Basic..DeliverPlace dp on dp.DPId = slog.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic..Brand bra on bra.BrandId = slog.BrandId ");

            select.TableName = sb.ToString();
            select.WhereStr = " 1=1 ";

            return select;
        }

        public SelectModel GetInterestStocksSelect(int pageIndex, int pageSize, string orderStr, int subId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "i.InterestId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("*");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_Interest i ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" i.SubContractId ={0} and i.InterestStatus >={1}", subId, (int)Common.StatusEnum.已生效);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetPriceConfirmStockListSelectForCreate(int pageIndex, int pageSize, string orderStr, int subId)
        {
            SelectModel select = this.GetPriceConfirmStockListSelect(pageIndex, pageSize, orderStr, subId);

            select.TableName += string.Format(" left join (select pcDetail.InterestDetailId,pcDetail.StockLogId from dbo.Pri_PriceConfirm pc inner join dbo.Pri_PriceConfirmDetail pcDetail on pc.PriceConfirmId = pcDetail.PriceConfirmId where pc.SubId = {0} and pcDetail.DetailStatus>={1}) pcdetail on ISNULL(slog.StockLogId,0) = pcdetail.StockLogId and ISNULL(idetail.DetailId,0) = pcdetail.InterestDetailId ", subId, (int)Common.StatusEnum.已生效);

            select.WhereStr += " and ISNULL(pcdetail.InterestDetailId,0)=0 and ISNULL(pcdetail.StockLogId,0)=0 ";

            return select;
        }

        public SelectModel GetPriceConfirmStockListSelectForUpdateUp(int pageIndex, int pageSize, string orderStr, int subId, int priceConfirmId)
        {
            SelectModel select = this.GetPriceConfirmStockListSelect(pageIndex, pageSize, orderStr, subId);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" Pri_PriceConfirmDetail pcDetail ");
            sb.Append(" left join NFMT..Pri_InterestDetail idetail on pcDetail.InterestDetailId = idetail.DetailId ");
            sb.Append(" left join NFMT..Pri_Interest i on i.InterestId = pcDetail.InterestId  ");
            sb.Append(" left join NFMT..St_StockLog slog on slog.StockLogId = pcDetail.StockLogId ");
            sb.Append(" left join NFMT_Basic..Currency cur on cur.CurrencyId = i.CurrencyId ");
            sb.Append(" left join NFMT_Basic..MeasureUnit mu on mu.MUId = slog.MUId ");
            sb.Append(" left join NFMT_Basic..BDStyleDetail bd on bd.StyleDetailId = i.InterestStyle ");
            sb.Append(" left join NFMT_Basic..BDStyleDetail bd2 on bd2.StyleDetailId = idetail.InterestType ");
            sb.Append(" left join NFMT..St_StockName sn on sn.StockNameId = slog.StockNameId ");
            sb.Append(" left join NFMT_Basic..Asset ass on ass.AssetId = slog.AssetId ");
            sb.Append(" left join NFMT_Basic..DeliverPlace dp on dp.DPId = slog.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic..Brand bra on bra.BrandId = slog.BrandId ");
            select.TableName = sb.ToString();

            select.WhereStr = string.Format(" pcDetail.PriceConfirmId = {0} and pcDetail.DetailStatus >= {1} ", priceConfirmId, (int)Common.StatusEnum.已生效);

            return select;
        }

        public SelectModel GetPriceConfirmStockListSelectForUpdateDown(int pageIndex, int pageSize, string orderStr, int subId, int priceConfirmId)
        {
            SelectModel select = this.GetPriceConfirmStockListSelect(pageIndex, pageSize, orderStr, subId);

            select.TableName += string.Format(" left join dbo.Pri_PriceConfirmDetail pcdetail on pcdetail.InterestDetailId = ISNULL(idetail.DetailId,0) and pcdetail.StockLogId = ISNULL(slog.StockLogId,0) and pcdetail.DetailStatus >= {0} ", (int)Common.StatusEnum.已生效);//and pcdetail.PriceConfirmId = 17

            select.WhereStr += " and ISNULL(pcdetail.DetailId,0)=0 ";

            return select;
        }

        public ResultModel LoadPriceConfirmListBySubId(UserModel user, int subId)
        {
            return this.priceconfirmDAL.LoadPriceConfirmListBySubId(user, subId);
        }

        #endregion
    }
}
