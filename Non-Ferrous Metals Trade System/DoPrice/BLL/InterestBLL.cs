/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InterestBLL.cs
// 文件功能描述：利息表dbo.Pri_Interest业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
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
    /// 利息表dbo.Pri_Interest业务逻辑类。
    /// </summary>
    public class InterestBLL : Common.ExecBLL
    {
        private InterestDAL interestDAL = new InterestDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(InterestDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InterestBLL()
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
            get { return this.interestDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetInterestsSelect(int pageIndex, int pageSize, string orderStr, DateTime fromDate, DateTime toDate, int status, int inCorpId, int outCorpId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "it.InterestId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("it.InterestId,con.ContractId,it.SubContractId,con.SettleCurrency,it.Unit,con.ContractNo");
            sb.Append(",it.InterestDate,tradeDir.DetailName as TradeDirection,cur.CurrencyName,it.InterestPrice");
            sb.Append(",it.InterestBala,it.InterestAmount,mu.MUName,outCorp.CorpName as OutCorpName,inCorp.CorpName as InCorpName");
            sb.Append(",it.InterestStatus,sd.StatusName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_Interest it ");
            sb.Append(" inner join dbo.Con_Contract con on it.ContractId = con.ContractId ");
            sb.Append("left join dbo.Con_SubCorporationDetail outCorp on outCorp.SubId = it.SubContractId and outCorp.IsDefaultCorp = 1 ");
            sb.AppendFormat("and outCorp.IsInnerCorp = 0 and outCorp.DetailStatus>={0} ", readyStatus);
            sb.Append("left join dbo.Con_SubCorporationDetail inCorp on inCorp.SubId = it.SubContractId and inCorp.IsDefaultCorp =1 ");
            sb.AppendFormat("and inCorp.IsInnerCorp =1 and inCorp.DetailStatus>={0} ", readyStatus);
            sb.Append("left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = it.CurrencyId ");
            sb.Append("left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = it.Unit ");
            sb.Append("left join NFMT_Basic.dbo.BDStyleDetail tradeDir on tradeDir.StyleDetailId = con.TradeDirection ");
            sb.Append("left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = it.InterestStatus ");
            select.TableName = sb.ToString();

            sb.Clear();

            if (status > 0)
                sb.AppendFormat(" and it.InterestStatus = {0} ", status);
            if (inCorpId > 0)
                sb.AppendFormat(" and inv.InCorpId = {0} ", inCorpId);
            if (outCorpId > 0)
                sb.AppendFormat(" and inv.OutCorpId = {0} ", outCorpId);
            if (fromDate > Common.DefaultValue.DefaultTime && toDate > fromDate)
                sb.AppendFormat(" and inv.InvoiceDate between '{0}' and '{1}' ", fromDate.ToString(), toDate.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetPledgeContractListSelect(int pageIndex, int pageSize, string orderStr, string subNo)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cs.SubId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;
            int commonStatusType = (int)NFMT.Common.StatusTypeEnum.通用状态;
            int pledgeContract = (int)NFMT.Contract.ContractType.质押合约;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("con.ContractId,cs.SubId,cs.ContractDate,con.ContractNo,cs.SubNo");
            sb.Append(",cast (cs.SignAmount as varchar) + mu.MUName as SignWeight");
            sb.Append(",inccd.CorpName as InCorpName,outccd.CorpName as OutCorpName,a.AssetName,cs.SubStatus");
            sb.Append(",sd.StatusName,con.TradeDirection,tradeDir.DetailName as TradeDirectionName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Con_ContractSub cs ");
            sb.Append(" inner join dbo.Con_Contract con on con.ContractId = cs.ContractId ");
            sb.AppendFormat(" inner join (select * from dbo.Con_SubTypeDetail where DetailStatus >={0} and ContractType ={1}) ", readyStatus, pledgeContract);
            sb.Append(" std on std.SubId = cs.SubId ");
            sb.Append(" left join dbo.Con_ContractCorporationDetail inccd on inccd.ContractId = con.ContractId and inccd.IsDefaultCorp = 1 ");
            sb.AppendFormat(" and inccd.IsInnerCorp = 1 and inccd.DetailStatus= {0} ", readyStatus);
            sb.Append(" left join dbo.Con_ContractCorporationDetail outccd on outccd.ContractId = con.ContractId and outccd.IsDefaultCorp = 1 ");
            sb.AppendFormat(" and outccd.IsInnerCorp = 0 and outccd.DetailStatus={0} ",readyStatus);
            sb.Append(" left join NFMT_Basic.dbo.Asset a on con.AssetId = a.AssetId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on cs.SubStatus = sd.DetailId and sd.StatusId = {0} ",commonStatusType);
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on cs.UnitId = mu.MUId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail tradeDir on tradeDir.StyleDetailId = con.TradeDirection ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" con.ContractStatus = {0} and cs.SubStatus = {0} and con.PriceMode = {1} ", readyStatus, (int)NFMT.Contract.PriceModeEnum.点价);
            sb.AppendFormat(" and cs.SubId not in (select SubId from dbo.Pri_PriceConfirm where PriceConfirmStatus <> {0}) ", (int)Common.StatusEnum.已作废);

            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and (cs.SubNo like '%{0}%' or con.ContractNo like '%{0}%') ", subNo);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetInterestStocksSelect(int pageIndex, int pageSize, string orderStr, int subId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("sl.StockLogId,sto.StockId,sn.RefNo,sl.LogDate,ass.AssetId,ass.AssetName,sl.CardNo,sl.BrandId,bra.BrandName");
            sb.Append(",sl.DeliverPlaceId,dp.DPName,sl.MUId,mu.MUName");
            sb.Append(",ISNULL(sl.NetAmount,0) - ISNULL(sl.GapAmount,0) - ISNULL(ind.InterestAmount,0) as NetAmount");
            sb.Append(",ISNULL(sl.NetAmount,0) - ISNULL(sl.GapAmount,0) - ISNULL(ind.InterestAmount,0) as InterestAmount");
            sb.Append(",0 as LastAmount");
            sb.Append(",0 as StockBala");
            sb.Append(",cast(GETDATE() as date) as InterestStartDate");
            sb.Append(",cast(GETDATE() as date) as InterestEndDate");
            sb.Append(",DATEDIFF(day, cast(GETDATE() as date), cast(GETDATE() as date)) as InterestDay");
            sb.Append(",0 as InterestUnit");
            sb.Append(",0 as InterestBala ");
            sb.Append(",344 as InterestType");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockLog sl ");
            sb.Append(" inner join dbo.St_Stock sto on sl.StockId = sto.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sl.StockNameId = sn.StockNameId  and sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sl.AssetId and ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sl.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sl.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sl.MUId ");

            sb.Append(" left join (select StockLogId,SUM(InterestAmount) as InterestAmount,SUM(InterestBala) as InterestBala,Sum(StockBala) as StockBala ");
            sb.AppendFormat(" from dbo.Pri_InterestDetail ind where DetailStatus >={0} and SubContractId = {1} group by StockLogId ) as ind ", readyStatus, subId);
            sb.Append(" on ind.StockLogId = sl.StockLogId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" sl.SubContractId ={0} and sl.LogStatus>={1} and ISNULL(sl.NetAmount,0) - ISNULL(sl.GapAmount,0) - ISNULL(ind.InterestAmount,0) >0 ", subId, readyStatus);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Create(UserModel user, NFMT.DoPrice.Model.Interest interest, List<NFMT.DoPrice.Model.InterestDetail> details, bool isSubmitAudit)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.InterestDetailDAL detailDAL = new InterestDetailDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (details == null || details.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "请添加利息信息";
                        return result;
                    }
                    
                    //本金计息方式下，计算当前结息成本
                    if (interest.InterestStyle == (int)NFMT.DoPrice.InterestStyleEnum.本金计息)
                    {
                        decimal curCapital = details.Where(temp=> temp.InterestType != (int)NFMT.DoPrice.InterestTypeEnum.差额计息).Sum(temp => temp.StockBala);
                        interest.CurCapital = Math.Round(curCapital,2,MidpointRounding.AwayFromZero);                        

                        decimal sumCapital =details.Sum(temp=>temp.StockBala);
                        if(sumCapital != interest.PayCapital)
                        {
                            result.Message="结算总金额必须等于预付本金";
                            result.ResultStatus=-1;
                            return result;
                        }
                    }

                    decimal sumInterestBala = details.Sum(temp => temp.InterestBala);
                    if (sumInterestBala != interest.InterestBala)
                    {
                        result.Message = "利息明细之和必须等于利息总额";
                        result.ResultStatus = -1;
                        return result;
                    }


                    interest.InterestStatus = StatusEnum.已录入;
                    result = this.interestDAL.Insert(user, interest);
                    if (result.ResultStatus != 0)
                        return result;

                    int interestId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out interestId) || interestId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "利息新增失败";
                        return result;
                    }

                    foreach (NFMT.DoPrice.Model.InterestDetail detail in details)
                    {
                        detail.InterestId = interestId;
                        detail.DetailStatus = StatusEnum.已生效;
                        detail.SubContractId = interest.SubContractId;
                        detail.ContractId = interest.ContractId;
                        detail.InterestPrice = interest.InterestPrice;
                        detail.PricingUnit = interest.PricingUnit;
                        detail.Premium = interest.Premium;
                        detail.OtherPrice = interest.OtherPrice;

                        if (detail.InterestType != (int)InterestTypeEnum.差额计息)
                        {
                            if (interest.InterestStyle == (int)InterestStyleEnum.本金计息)
                                detail.InterestType = (int)InterestTypeEnum.本金计息;
                            else
                                detail.InterestType = (int)InterestTypeEnum.货值计息;

                        } result = detailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (isSubmitAudit)
                    {
                        interest.InterestId = interestId;

                        NFMT.WorkFlow.AutoSubmit submit = new WorkFlow.AutoSubmit();
                        NFMT.WorkFlow.ITaskProvider taskProvider = new NFMT.DoPrice.TaskProvider.InterestTaskProvider();
                        result = submit.Submit(user, interest, taskProvider, WorkFlow.MasterEnum.利息结算审核);
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

        /// <summary>
        /// 获取当前结息明细列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="status"></param>
        /// <param name="inCorpId"></param>
        /// <param name="outCorpId"></param>
        /// <param name="interestId"></param>
        /// <returns></returns>
        public SelectModel GetCurDetailsSelect(int pageIndex, int pageSize, string orderStr, int subId, int interestId = 0)
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
            sb.Append("isnull(sl.StockLogId,0) as StockLogId,isnull(sto.StockId,0) as StockId,isnull(sn.RefNo,'本金余额') as RefNo");
            sb.Append(",isnull(sl.LogDate,getdate()) as LogDate,isnull(ass.AssetId,0) as AssetId,isnull(ass.AssetName,'--') as AssetName");
            sb.Append(",isnull(sl.CardNo,'--') as CardNo,isnull(sl.BrandId,0) as BrandId,isnull(bra.BrandName,'--') as BrandName");
            sb.Append(",isnull(sl.DeliverPlaceId,0) as DeliverPlaceId,isnull(dp.DPName,'--') as DPName,isnull(sl.MUId,0) as MUId,isnull(mu.MUName,'--') as MUName");
            sb.Append(",ISNULL(sl.NetAmount,0) - ISNULL(sl.GapAmount,0) - ISNULL(othInt.InterestAmount,0) as NetAmount");
            sb.Append(",isnull(curInt.InterestAmount,ISNULL(sl.NetAmount,0) - ISNULL(sl.GapAmount,0) - ISNULL(othInt.InterestAmount,0)) as InterestAmount");
            sb.Append(",ISNULL(sl.NetAmount,0) - ISNULL(sl.GapAmount,0) - ISNULL(othInt.InterestAmount,0) - ");
            sb.Append("isnull(curInt.InterestAmount,ISNULL(sl.NetAmount,0) - ISNULL(sl.GapAmount,0) - ISNULL(othInt.InterestAmount,0)) as LastAmount");
            sb.Append(",ISNULL(curInt.StockBala,0) as StockBala");
            sb.Append(",isnull(curInt.InterestStartDate,cast(GETDATE() as date)) as InterestStartDate");
            sb.Append(",isnull(curInt.InterestEndDate,cast(GETDATE() as date)) as InterestEndDate");
            sb.Append(",DATEDIFF(day, isnull(curInt.InterestStartDate,cast(GETDATE() as date)), isnull(curInt.InterestEndDate,cast(GETDATE() as date))) as InterestDay");
            sb.Append(",isnull(curInt.InterestUnit,0) as InterestUnit");
            sb.Append(",isnull(curInt.InterestBala,0) as InterestBala");
            sb.Append(",curInt.InterestType");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_InterestDetail curInt ");
            //sb.Append(" dbo.St_StockLog sl ");
            sb.Append(" left join dbo.St_StockLog sl on curInt.StockLogId = sl.StockLogId ");
            sb.Append(" left join dbo.St_Stock sto on sl.StockId = sto.StockId ");
            sb.Append(" left join dbo.St_StockName sn on sl.StockNameId = sn.StockNameId and sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sl.AssetId and ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sl.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sl.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sl.MUId ");

            //sb.AppendFormat(" left join dbo.Pri_InterestDetail curInt on curInt.StockLogId = sl.StockLogId and curInt.DetailStatus >={0} ", readyStatus);
            //sb.AppendFormat(" and curInt.SubContractId = {0} and curInt.InterestId = {1} ", subId, interestId);

            sb.AppendFormat(" left join dbo.Pri_InterestDetail othInt on othInt.StockLogId = sl.StockLogId and othInt.DetailStatus >={0} ", readyStatus);
            sb.AppendFormat(" and othInt.SubContractId = {0} and othInt.InterestId != {1} ", subId, interestId);
            select.TableName = sb.ToString();

            sb.Clear();

            //sb.AppendFormat(" sl.SubContractId ={0} and sl.LogStatus>={1} and curInt.InterestId is not null ",subId,readyStatus);
            sb.AppendFormat(" curInt.SubContractId ={0} and curInt.DetailStatus>={1} and curInt.InterestId = {2} and curInt.InterestId is not null ",subId,readyStatus,interestId);

            //sb.Append(" union ");
            //sb.Append(" select 0 as rowSerial, 0 as StockLogId,0 as StockId,'本金余额' as RefNo, GETDATE() as LogDate,0 as AssetName,'--' as AssetName,'--' as CardNo ");
            //sb.Append(" ,0 as BrandId,'--' as BrandName,0 as DeliverPlaceId,'--' as DPName,0 as MUId,'--' as MUName,0 as NetAmount,0 as InterestAmount ");
            //sb.Append(" ,0 as LastAmount,capDetail.StockBala,capDetail.InterestStartDate,capDetail.InterestEndDate,capDetail.InterestDay,capDetail.InterestUnit ");
            //sb.Append(" ,capDetail.InterestBala,capDetail.InterestType ");
            //sb.Append(" from dbo.Pri_InterestDetail capDetail ");
            //sb.AppendFormat(" where capDetail.InterestId = {0} and capDetail.SubContractId = {1} ",interestId,subId);
            //sb.AppendFormat(" and capDetail.DetailStatus>= {0} and capDetail.InterestType = {1} ",readyStatus,(int)NFMT.DoPrice.InterestTypeEnum.差额计息);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetOthDetailsSelect(int pageIndex, int pageSize, string orderStr, int subId, int interestId = 0)
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
            sb.Append("sl.StockLogId,sto.StockId,sn.RefNo,sl.LogDate,ass.AssetId,ass.AssetName,sl.CardNo,sl.BrandId,bra.BrandName");
            sb.Append(",sl.DeliverPlaceId,dp.DPName,sl.MUId,mu.MUName");
            sb.Append(",ISNULL(sl.NetAmount,0) - ISNULL(sl.GapAmount,0) - ISNULL(othInt.InterestAmount,0) as NetAmount");
            sb.Append(",isnull(curInt.InterestAmount,ISNULL(sl.NetAmount,0) - ISNULL(sl.GapAmount,0) - ISNULL(othInt.InterestAmount,0)) as InterestAmount");
            sb.Append(",ISNULL(sl.NetAmount,0) - ISNULL(sl.GapAmount,0) - ISNULL(othInt.InterestAmount,0) - ");
            sb.Append("isnull(curInt.InterestAmount,ISNULL(sl.NetAmount,0) - ISNULL(sl.GapAmount,0) - ISNULL(othInt.InterestAmount,0)) as LastAmount");
            sb.Append(",ISNULL(curInt.StockBala,0) as StockBala");
            sb.Append(",isnull(curInt.InterestStartDate,cast(GETDATE() as date)) as InterestStartDate");
            sb.Append(",isnull(curInt.InterestEndDate,cast(GETDATE() as date)) as InterestEndDate");
            sb.Append(",DATEDIFF(day, isnull(curInt.InterestStartDate,cast(GETDATE() as date)), isnull(curInt.InterestEndDate,cast(GETDATE() as date))) as InterestDay");
            sb.Append(",isnull(curInt.InterestUnit,0) as InterestUnit");
            sb.Append(",isnull(curInt.InterestBala,0) as InterestBala");
            sb.Append(",344 as InterestType");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockLog sl ");
            sb.Append(" inner join dbo.St_Stock sto on sl.StockId = sto.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sl.StockNameId = sn.StockNameId and sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sl.AssetId and ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sl.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sl.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sl.MUId ");

            sb.AppendFormat(" left join dbo.Pri_InterestDetail curInt on curInt.StockLogId = sl.StockLogId and curInt.DetailStatus >={0} ", readyStatus);
            sb.AppendFormat(" and curInt.SubContractId = {0} and curInt.InterestId = {1} ", subId, interestId);

            sb.AppendFormat(" left join dbo.Pri_InterestDetail othInt on othInt.StockLogId = sl.StockLogId and othInt.DetailStatus >={0} ", readyStatus);
            sb.AppendFormat(" and othInt.SubContractId = {0} and othInt.InterestId != {1} ", subId, interestId);
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" sl.SubContractId ={0} and sl.LogStatus>={1} and curInt.InterestId is null ", subId, readyStatus);
            sb.Append(" and ISNULL(sl.NetAmount,0) - ISNULL(sl.GapAmount,0) - ISNULL(othInt.InterestAmount,0) >0 ");
            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Update(UserModel user, NFMT.DoPrice.Model.Interest interest, List<NFMT.DoPrice.Model.InterestDetail> details)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.InterestDetailDAL detailDAL = new InterestDetailDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (details == null || details.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "请添加利息信息";
                        return result;
                    }                    

                    //获取结算
                    result = this.interestDAL.Get(user, interest.InterestId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Interest resultInterest = result.ReturnValue as Model.Interest;
                    if (resultInterest == null || resultInterest.InterestId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "结息不存在";
                        return result;
                    }
                    
                    //本金计息方式下，计算当前结息成本
                    if (resultInterest.InterestStyle == (int)NFMT.DoPrice.InterestStyleEnum.本金计息)
                    {
                        decimal curCapital = details.Where(temp => temp.InterestType != (int)NFMT.DoPrice.InterestTypeEnum.差额计息).Sum(temp => temp.StockBala);
                        interest.CurCapital = Math.Round(curCapital, 2, MidpointRounding.AwayFromZero);

                        decimal sumCapital = details.Sum(temp => temp.StockBala);
                        if (sumCapital != interest.PayCapital)
                        {
                            result.Message = "结算总金额必须等于预付本金";
                            result.ResultStatus = -1;
                            return result;
                        }
                    }

                    decimal sumInterestBala = details.Sum(temp => temp.InterestBala);
                    if (sumInterestBala != interest.InterestBala)
                    {
                        result.Message = "利息明细之和必须等于利息总额";
                        result.ResultStatus = -1;
                        return result;
                    }                   

                    //赋值主表
                    resultInterest.CurCapital = interest.CurCapital;
                    resultInterest.InterestAmount = interest.InterestAmount;
                    resultInterest.InterestAmountDay = interest.InterestAmountDay;
                    resultInterest.InterestBala = interest.InterestBala;
                    resultInterest.InterestDate = interest.InterestDate;
                    resultInterest.InterestPrice = interest.InterestPrice;
                    resultInterest.InterestRate = interest.InterestRate;
                    resultInterest.Memo = interest.Memo;
                    resultInterest.OtherPrice = interest.OtherPrice;
                    resultInterest.PayCapital = interest.PayCapital;
                    resultInterest.Premium = interest.Premium;
                    resultInterest.PricingUnit = interest.PricingUnit;

                    result = this.interestDAL.Update(user, resultInterest);
                    if (result.ResultStatus != 0)
                        return result;
                   
                    //作废原有明细
                    result = detailDAL.Load(user, resultInterest.InterestId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.InterestDetail> resultDetails = result.ReturnValue as List<Model.InterestDetail>;
                    if (resultDetails == null || resultDetails.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "结息明细获取失败";
                        return result;
                    }

                    foreach (Model.InterestDetail detail in resultDetails)
                    {
                        detail.DetailStatus = StatusEnum.已录入;
                        result = detailDAL.Invalid(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //添加新明细
                    foreach (NFMT.DoPrice.Model.InterestDetail detail in details)
                    {
                        detail.InterestId = resultInterest.InterestId;
                        detail.DetailStatus = StatusEnum.已生效;
                        detail.SubContractId = interest.SubContractId;
                        detail.ContractId = interest.ContractId;
                        detail.InterestPrice = interest.InterestPrice;
                        detail.PricingUnit = interest.PricingUnit;
                        detail.Premium = interest.Premium;
                        detail.OtherPrice = interest.OtherPrice;

                        if (detail.InterestType != (int)InterestTypeEnum.差额计息)
                        {
                            if (resultInterest.InterestStyle == (int)InterestStyleEnum.本金计息)
                                detail.InterestType = (int)InterestTypeEnum.本金计息;
                            else
                                detail.InterestType = (int)InterestTypeEnum.货值计息;

                        } 
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
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel GoBack(UserModel user, int interestId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.interestDAL.Get(user, interestId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.Interest interest = result.ReturnValue as Model.Interest;
                    if (interest == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能撤返";
                        return result;
                    }

                    result = this.interestDAL.Goback(user, interest);
                    if (result.ResultStatus != 0)
                        return result;

                    //同步工作流状态
                    NFMT.WorkFlow.BLL.DataSourceBLL dataSourceBLL = new WorkFlow.BLL.DataSourceBLL();
                    result = dataSourceBLL.SynchronousStatus(user, interest);
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

        public ResultModel Invalid(UserModel user, int interestId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.InterestDetailDAL detailDAL = new InterestDetailDAL();
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.interestDAL.Get(user, interestId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.Interest interest = result.ReturnValue as Model.Interest;
                    if (interest == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能作废";
                        return result;
                    }
                   
                    //获取明细
                    result = detailDAL.Load(user, interest.InterestId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.InterestDetail> details = result.ReturnValue as List<Model.InterestDetail>;
                    if (details == null || details.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "结息明细获取失败";
                        return result;
                    }

                    //作废明细
                    foreach (Model.InterestDetail detail in details)
                    {
                        detail.DetailStatus = StatusEnum.已录入;
                        result = detailDAL.Invalid(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //作废主表
                    result = this.interestDAL.Invalid(user, interest);
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

        public ResultModel Complete(UserModel user, int interestId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.InterestDetailDAL detailDAL = new InterestDetailDAL();
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.interestDAL.Get(user, interestId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.Interest interest = result.ReturnValue as Model.Interest;
                    if (interest == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能完成";
                        return result;
                    }

                    //获取明细
                    result = detailDAL.Load(user, interest.InterestId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.InterestDetail> details = result.ReturnValue as List<Model.InterestDetail>;
                    if (details == null || details.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "结息明细获取失败";
                        return result;
                    }

                    //完成明细
                    foreach (Model.InterestDetail detail in details)
                    {
                        result = detailDAL.Complete(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //完成主表
                    result = this.interestDAL.Complete(user, interest);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ResultStatus == 0)
                        result.Message = "执行完成成功";

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

        public ResultModel CompleteCancel(UserModel user, int interestId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.InterestDetailDAL detailDAL = new InterestDetailDAL();
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.interestDAL.Get(user, interestId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.Interest interest = result.ReturnValue as Model.Interest;
                    if (interest == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能撤销完成";
                        return result;
                    }

                    //获取明细
                    result = detailDAL.Load(user, interest.InterestId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.InterestDetail> details = result.ReturnValue as List<Model.InterestDetail>;
                    if (details == null || details.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "结息明细获取失败";
                        return result;
                    }

                    //撤销完成明细
                    foreach (Model.InterestDetail detail in details)
                    {
                        result = detailDAL.CompleteCancel(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //撤销完成主表
                    result = this.interestDAL.CompleteCancel(user, interest);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ResultStatus == 0)
                        result.Message = "撤销完成成功";

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

        public ResultModel LoadInterestListBySubId(UserModel user, int subId)
        {
            return this.interestDAL.LoadInterestListBySubId(user, subId);
        }

        public ResultModel GetLastCapitalBySubId(UserModel user, int subId)
        {
            return this.interestDAL.GetLastCapitalBySubId(user, subId);
        }

        public ResultModel GetLastNetWeightBySubId(UserModel user, int subId)
        {
            return this.interestDAL.GetLastNetWeightBySubId(user, subId);
        }

        #endregion
    }
}
