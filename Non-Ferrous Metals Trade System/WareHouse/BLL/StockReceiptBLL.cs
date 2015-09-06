/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockReceiptBLL.cs
// 文件功能描述：仓库库存净重确认回执，磅差回执dbo.St_StockReceipt业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年9月3日
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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NFMT.WareHouse.BLL
{
    /// <summary>
    /// 仓库库存净重确认回执，磅差回执dbo.St_StockReceipt业务逻辑类。
    /// </summary>
    public class StockReceiptBLL : Common.ExecBLL
    {
        private StockReceiptDAL stockreceiptDAL = new StockReceiptDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StockReceiptDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockReceiptBLL()
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
            get { return this.stockreceiptDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, string contractNo, DateTime stockReceiptTimeBegin, DateTime stockReceiptTimeEnd, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            int receiptType = (int)NFMT.Data.StyleEnum.ReceiptType;
            int statusType = (int)Common.StatusTypeEnum.通用状态;

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sr.ReceiptId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("sr.ReceiptId,cs.SubId,cs.SubNo,sr.ReceiptDate,sr.Receipter,emp.Name as ReceipterName,sr.Memo,sr.ReceiptType,rt.DetailName as ReceiptTypeName,sr.ReceiptStatus,rs.StatusName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockReceipt sr ");
            sb.Append(" inner join dbo.Con_ContractSub cs on sr.ContractSubId = cs.SubId ");
            sb.Append(" inner join dbo.Con_Contract con on con.ContractId = cs.ContractId");
            sb.Append(" left join NFMT_User.dbo.Employee emp on sr.Receipter = emp.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail rt on sr.ReceiptType = rt.StyleDetailId and rt.BDStyleId ={0} ", receiptType);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail rs on rs.DetailId = sr.ReceiptStatus and rs.StatusId ={0} ", statusType);
            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" 1=1 ");

            if (!string.IsNullOrEmpty(contractNo))
                sb.AppendFormat(" and cs.SubNo like '%{0}%' ", contractNo);
            if (stockReceiptTimeBegin > Common.DefaultValue.DefaultTime && stockReceiptTimeEnd > stockReceiptTimeBegin)
                sb.AppendFormat(" and sr.ReceiptDate between '{0}' and '{1}' ", stockReceiptTimeBegin, stockReceiptTimeEnd);
            if (status > 0)
                sb.AppendFormat(" and sr.ReceiptStatus = {0}", status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetContractListSelect(int pageIndex, int pageSize, string orderStr, string subNo, int outCorpId, DateTime contractDateBegin, DateTime contractDateEnd, int tradeDir)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cs.SubId desc";
            else
                select.OrderStr = orderStr;

            int status = (int)NFMT.Common.StatusEnum.已生效;
            int statusType = (int)NFMT.Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat(" con.ContractId,cs.SubId,cs.ContractDate,con.ContractNo,cs.SubNo,cast (cs.SignAmount as varchar) + mu.MUName as SignWeight,inCorp.CorpName as InCorpName,outCorp.CorpName as OutCorpName,a.AssetName,cs.SubStatus,sd.StatusName,isnull(sr.SumAmount,0) as SumAmount,cast(isnull(sr.SumAmount,0) as varchar)+ mu.MUName as SumWeight,cs.OutContractNo,case cs.PriceMode when {0} then sp.FixedPrice when {1} then sp.AlmostPrice else sp.FixedPrice end as price ", (int)NFMT.Contract.PriceModeEnum.定价, (int)NFMT.Contract.PriceModeEnum.点价);
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Con_ContractSub cs ");
            sb.Append(" left join dbo.Con_Contract con on con.ContractId = cs.ContractId ");
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail inCorp on inCorp.ContractId = con.ContractId and inCorp.IsDefaultCorp = 1 and inCorp.IsInnerCorp = 1 and inCorp.DetailStatus= {0} ", status);
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail outCorp on outCorp.ContractId = con.ContractId and outCorp.IsDefaultCorp = 1 and outCorp.IsInnerCorp = 0 and outCorp.DetailStatus={0} ", status);
            sb.Append(" left join NFMT_Basic.dbo.Asset a on con.AssetId = a.AssetId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on cs.SubStatus = sd.DetailId and sd.StatusId = {0} ", statusType);
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on cs.UnitId = mu.MUId ");
            sb.AppendFormat(" left join (select SUM(isnull(srd.ReceiptAmount,0)) as SumAmount,srd.ContractSubId from dbo.St_StockReceiptDetail srd where srd.DetailStatus >={0} group by srd.ContractSubId) sr on sr.ContractSubId = cs.SubId ", status);
            sb.Append(" left join dbo.Con_SubPrice sp on sp.SubId = cs.SubId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" con.ContractStatus = {0} and cs.SubStatus = {0} ", status);
            sb.AppendFormat(" and cs.SubId not in (select ContractSubId from dbo.St_StockReceipt where ReceiptStatus >= {0}) ", status);

            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and cs.SubNo like '%{0}%'", subNo);
            if (outCorpId > 0)
                sb.AppendFormat("  and outCorp.CorpId ={0}", outCorpId);
            if (contractDateBegin > Common.DefaultValue.DefaultTime && contractDateEnd > contractDateBegin)
                sb.AppendFormat(" and cs.CreateTime between '{0}' and '{1}' ", contractDateBegin.ToString(), contractDateEnd.AddDays(1).ToString());
            if (tradeDir > 0)
                sb.AppendFormat(" and cs.TradeDirection = {0}", tradeDir);

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 获取合约下,除当前回执外所有已回执的库存列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="subId">合约序号</param>
        /// <param name="receiptId">当前回执序号</param>
        /// <returns></returns>
        public SelectModel GetReceiptedStockListSelect(int pageIndex, int pageSize, string orderStr, int subId, int receiptId = 0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;
            int stockStatusType = (int)NFMT.Common.StatusTypeEnum.库存状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" sto.StockId,sl.StockLogId,sn.StockNameId ");
            sb.Append(" ,sto.StockDate,sn.RefNo,sl.NetAmount,sto.UintId,mu.MUName,cast(sl.NetAmount as varchar)+mu.MUName as StockWeight ");
            sb.Append(" ,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,sto.StockStatus,ss.StatusName as StockStatusName ");
            sb.Append(" ,isnull(sr.SumAmount,0) as ReceiptAmount,cast(isnull(sr.SumAmount,0) as varchar) + mu.MUName as ReceiptWeight ");
            sb.Append(" ,isnull(sr.SumAmount,0) -isnull(sto.NetAmount,0) as MissAmount,cast(cast((isnull(sr.SumAmount,0)/isnull(sto.NetAmount,0)-1)*100 as numeric(18,4)) as varchar)+'%' as MissRate ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockLog sl ");
            sb.Append(" inner join dbo.St_Stock sto on sl.StockId = sto.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId ={0} ", stockStatusType);
            sb.Append(" left join ( ");
            sb.Append(" select srd.StockLogId,SUM(isnull(srd.ReceiptAmount,0)) as SumAmount ");
            sb.Append(" from dbo.St_StockReceiptDetail srd ");
            sb.AppendFormat(" where srd.DetailStatus>={0} and srd.ContractSubId ={1} and srd.ReceiptId!={2} group by srd.StockLogId ", readyStatus, subId, receiptId);
            sb.Append(" ) as sr on sr.StockLogId = sl.StockLogId ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" sl.LogStatus>={0} and sl.SubContractId = {1} ", readyStatus, subId);
            sb.Append(" and sr.StockLogId is not null ");

            //sb.AppendFormat(" sod.DetailStatus>{0} and sr.StockId is not null ", readyStatus);
            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 获取合约下，所有未回执的库存列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        public SelectModel GetCanReceiptStockListSelect(int pageIndex, int pageSize, string orderStr, int subId, string logs, int receiptId = 0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;
            int stockStatusType = (int)NFMT.Common.StatusTypeEnum.库存状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" sto.StockId,sl.StockLogId,sn.StockNameId,sl.ContractId,sl.SubContractId as ContractSubId ");
            sb.Append(" ,sto.StockDate,sn.RefNo,sl.NetAmount,sto.UintId,mu.MUName,cast(sl.NetAmount as varchar)+mu.MUName as StockWeight ");
            sb.Append(" ,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,sto.StockStatus,ss.StatusName as StockStatusName ");
            sb.Append(" ,isnull(sr.SumAmount,0) as ReceiptAmount,cast(isnull(sr.SumAmount,0) as varchar) + mu.MUName as ReceiptWeight ");
            sb.Append(" ,isnull(sr.SumAmount,0) -isnull(sto.NetAmount,0) as MissAmount,cast(cast((isnull(sr.SumAmount,0)/isnull(sto.NetAmount,0)-1)*100 as numeric(18,4)) as varchar)+'%' as MissRate ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            //sb.Append(" dbo.St_StockOutDetail sod ");
            //sb.Append(" inner join dbo.St_StockOut so on sod.StockOutId = so.StockOutId ");
            //sb.AppendFormat(" inner join dbo.St_StockOutApply soa on so.StockOutApplyId = soa.StockOutApplyId and soa.SubContractId = {0} ", subId);
            //sb.AppendFormat(" inner join dbo.Apply app on app.ApplyId = soa.ApplyId and app.ApplyStatus>{0} ", readyStatus);
            //sb.Append(" inner join dbo.St_Stock sto on sod.StockId = sto.StockId ");


            sb.Append(" dbo.St_StockLog sl ");
            sb.Append(" inner join dbo.St_Stock sto on sl.StockId = sto.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId ={0} ", stockStatusType);
            sb.Append(" left join ( ");
            sb.Append(" select srd.StockLogId,SUM(isnull(srd.ReceiptAmount,0)) as SumAmount ");
            sb.Append(" from dbo.St_StockReceiptDetail srd ");
            sb.AppendFormat(" where srd.DetailStatus>={0} and srd.ContractSubId ={1} and srd.ReceiptId!={2} group by srd.StockLogId ", readyStatus, subId, receiptId);
            sb.Append(" ) as sr on sr.StockLogId = sl.StockLogId ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" sl.LogStatus>={0} and sl.SubContractId = {1} ", readyStatus, subId);
            sb.Append(" and sr.StockLogId is null ");

            //sb.AppendFormat(" and sto.StockId not in ({0}) ", sids);
            sb.AppendFormat(" and sl.StockLogId not in ({0}) ", logs);
            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 获取销售合约下，所有未回执的库存列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="subId">采购子合约Id</param>
        /// <returns></returns>
        public SelectModel GetSaleCanReceiptStockListSelect(int pageIndex, int pageSize, string orderStr, int subId, string logs, int receiptId = 0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;
            int stockStatusType = (int)NFMT.Common.StatusTypeEnum.库存状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" sto.StockId,sl.StockLogId,sn.StockNameId,sl.ContractId,sl.SubContractId as ContractSubId ");
            sb.Append(" ,sto.StockDate,sn.RefNo,sl.NetAmount,sto.UintId,mu.MUName,cast(sl.NetAmount as varchar)+mu.MUName as StockWeight ");
            sb.Append(" ,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,sto.StockStatus,ss.StatusName as StockStatusName ");
            sb.Append(" ,isnull(sr.SumAmount,0) as ReceiptAmount,cast(isnull(sr.SumAmount,0) as varchar) + mu.MUName as ReceiptWeight ");
            sb.Append(" ,isnull(sr.SumAmount,0) -isnull(sto.NetAmount,0) as MissAmount,cast(cast((isnull(sr.SumAmount,0)/isnull(sto.NetAmount,0)-1)*100 as numeric(18,4)) as varchar)+'%' as MissRate ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            //sb.Append(" dbo.St_StockOutDetail sod ");
            //sb.Append(" inner join dbo.St_StockOut so on sod.StockOutId = so.StockOutId ");
            //sb.AppendFormat(" inner join dbo.St_StockOutApply soa on so.StockOutApplyId = soa.StockOutApplyId and soa.SubContractId = {0} ", subId);
            //sb.AppendFormat(" inner join dbo.Apply app on app.ApplyId = soa.ApplyId and app.ApplyStatus>{0} ", readyStatus);
            //sb.Append(" inner join dbo.St_Stock sto on sod.StockId = sto.StockId ");


            sb.Append(" dbo.St_StockLog sl ");
            sb.Append(" inner join dbo.St_Stock sto on sl.StockId = sto.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId ={0} ", stockStatusType);
            sb.Append(" left join ( ");
            sb.Append(" select srd.StockLogId,SUM(isnull(srd.ReceiptAmount,0)) as SumAmount ");
            sb.Append(" from dbo.St_StockReceiptDetail srd ");
            sb.AppendFormat(" where srd.DetailStatus>={0} and srd.StockLogId in (select StockLogId from dbo.St_StockLog where LogType = {1} and StockId in (select StockId from dbo.St_StockLog where SubContractId = {3})) and srd.ReceiptId!={2} group by srd.StockLogId ", readyStatus, (int)NFMT.WareHouse.LogTypeEnum.出库, receiptId, subId);
            sb.Append(" ) as sr on sr.StockLogId = sl.StockLogId ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" sl.LogStatus>={0} ", readyStatus);
            sb.AppendFormat(" and sl.StockLogId in (select StockLogId from dbo.St_StockLog where LogType = {0} and StockId in (select StockId from dbo.St_StockLog where SubContractId = {1}))", (int)NFMT.WareHouse.LogTypeEnum.出库, subId);
            sb.Append(" and sr.StockLogId is null ");

            //sb.AppendFormat(" and sto.StockId not in ({0}) ", sids);
            sb.AppendFormat(" and sl.StockLogId not in ({0}) ", logs);
            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 获取合约下，当前回执选中的库存列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        public SelectModel GetReceiptingStockListSelect(int pageIndex, int pageSize, string orderStr, int subId, int receiptId, string sids)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;
            int stockStatusType = (int)NFMT.Common.StatusTypeEnum.库存状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" sto.StockId,sl.StockLogId,sn.StockNameId,sl.ContractId,sl.SubContractId ");
            sb.Append(" ,sto.StockDate,sn.RefNo,sl.NetAmount,sto.UintId,mu.MUName,cast(sl.NetAmount as varchar)+mu.MUName as StockWeight ");
            sb.Append(" ,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,sto.StockStatus,ss.StatusName as StockStatusName ");
            sb.Append(" ,isnull(sr.SumAmount,0) as ReceiptAmount,cast(isnull(sr.SumAmount,0) as varchar) + mu.MUName as ReceiptWeight ");
            sb.Append(" ,isnull(sr.SumAmount,0) -isnull(sto.NetAmount,0) as MissAmount,cast(cast((isnull(sr.SumAmount,0)/isnull(sto.NetAmount,0)-1)*100 as numeric(18,4)) as varchar)+'%' as MissRate ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockLog sl ");
            sb.Append(" inner join dbo.St_Stock sto on sl.StockId = sto.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId ={0} ", stockStatusType);
            sb.Append(" left join ( ");
            sb.Append(" select srd.StockLogId,SUM(isnull(srd.ReceiptAmount,0)) as SumAmount ");
            sb.Append(" from dbo.St_StockReceiptDetail srd ");
            sb.AppendFormat(" where srd.DetailStatus>={0} and srd.ContractSubId ={1} and srd.ReceiptId={2} group by srd.StockLogId ", readyStatus, subId, receiptId);
            sb.Append(" ) as sr on sr.StockLogId = sl.StockLogId ");
            select.TableName = sb.ToString();

            sb.Clear();

            //sb.AppendFormat(" sod.DetailStatus>{0} and sr.StockId is not null ", readyStatus);
            //sb.AppendFormat(" and sto.StockId in ({0}) ",sids);
            sb.AppendFormat(" sl.LogStatus>={0} and sl.SubContractId = {1} ", readyStatus, subId);
            sb.Append(" and sr.StockLogId is not null ");
            sb.AppendFormat(" and sl.StockLogId in ({0}) ", sids);
            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel CreateStockReceipt(UserModel user, NFMT.WareHouse.Model.StockReceipt stockReceipt, List<NFMT.WareHouse.Model.StockReceiptDetail> details, List<NFMT.WareHouse.Model.StockReceiptDetail> saleDetails, bool isAudit)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
                NFMT.Contract.DAL.ContractDAL contractDAL = new Contract.DAL.ContractDAL();
                NFMT.WareHouse.DAL.StockDAL stockDAL = new StockDAL();
                NFMT.WareHouse.DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                DAL.StockReceiptDAL receiptDAL = new StockReceiptDAL();
                DAL.StockReceiptDetailDAL detailDAL = new StockReceiptDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证合约
                    result = subDAL.Get(user, stockReceipt.ContractSubId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                    if (sub.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约不存在";
                        return result;
                    }

                    result = contractDAL.Get(user, sub.ContractId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                    if (contract == null || contract.ContractId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "合约不存在";
                        return result;
                    }

                    decimal sumNetAmount = 0;
                    decimal sumReceiptAmount = 0;
                    decimal sumMissAmount = 0;
                    decimal missRate = 0;

                    //明细验证
                    foreach (NFMT.WareHouse.Model.StockReceiptDetail detail in details)
                    {
                        if (detail.ReceiptAmount > 0)
                        {
                            //验证库存
                            result = stockDAL.Get(user, detail.StockId);
                            if (result.ResultStatus != 0)
                                return result;

                            Model.Stock stock = result.ReturnValue as Model.Stock;
                            if (stock == null || stock.StockId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "库存不存在";
                                return result;
                            }

                            //验证流水
                            result = stockLogDAL.Get(user, detail.StockLogId);
                            if (result.ResultStatus != 0)
                                return result;

                            Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                            if (stockLog == null || stockLog.StockLogId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "库存流水不存在";
                                return result;
                            }

                            if (stockLog.StockId != stock.StockId)
                            {
                                result.ResultStatus = -1;
                                result.Message = "库存与库存流水不匹配";
                                return result;
                            }

                            if (stockLog.SubContractId != stockReceipt.ContractSubId)
                            {
                                result.ResultStatus = -1;
                                result.Message = "库存流水与合约不匹配";
                                return result;
                            }

                            detail.ContractId = sub.ContractId;
                            detail.ContractSubId = sub.SubId;
                            detail.DetailStatus = StatusEnum.已生效;
                            detail.PreNetAmount = stock.NetAmount;
                            detail.QtyMiss = detail.ReceiptAmount - stock.NetAmount;
                            detail.QtyRate = (detail.ReceiptAmount / stock.NetAmount - 1) / 100;

                            sumNetAmount += stock.NetAmount;
                            sumReceiptAmount += detail.ReceiptAmount;
                        }
                    }

                    sumMissAmount = sumReceiptAmount - sumNetAmount;
                    missRate = (sumReceiptAmount / sumNetAmount - 1) / 100;

                    //赋值StockReceipt
                    stockReceipt.ContractId = sub.ContractId;
                    stockReceipt.ContractSubId = sub.SubId;
                    stockReceipt.PreNetAmount = sumNetAmount;
                    stockReceipt.QtyMiss = sumMissAmount;
                    stockReceipt.QtyRate = missRate;
                    stockReceipt.ReceiptAmount = sumReceiptAmount;
                    stockReceipt.Receipter = user.EmpId;
                    stockReceipt.ReceiptDate = DateTime.Now;

                    if (contract.TradeDirection == (int)NFMT.Contract.TradeDirectionEnum.Buy)
                        stockReceipt.ReceiptType = (int)WareHouse.ReceiptTypeEnum.StockInReceipt;
                    else
                        stockReceipt.ReceiptType = (int)WareHouse.ReceiptTypeEnum.StockOutReceipt;

                    stockReceipt.UnitId = sub.UnitId;


                    //新增StockReceipt
                    result = stockreceiptDAL.Insert(user, stockReceipt);
                    if (result.ResultStatus != 0)
                        return result;

                    int receiptId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out receiptId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "新增库存回执失败";
                        return result;
                    }

                    //新增明细
                    foreach (NFMT.WareHouse.Model.StockReceiptDetail detail in details)
                    {
                        detail.ReceiptId = receiptId;
                        result = detailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    stockReceipt.ReceiptId = receiptId;
                    string resultStr = Newtonsoft.Json.JsonConvert.SerializeObject(stockReceipt);

                    //if(isAudit)
                    //{
                    //    stockReceipt.ReceiptId = receiptId;
                    //    NFMT.WorkFlow.AutoSubmit submit = new NFMT.WorkFlow.AutoSubmit();
                    //    result = submit.Submit(user, stockReceipt, new TaskProvider.StockReceiptTaskProvider(), NFMT.WorkFlow.MasterEnum.库存净重回执审核);
                    //    if (result.ResultStatus != 0)
                    //        return result;
                    //}

                    #region 销售合约库存回执

                    if (saleDetails != null && saleDetails.Any())
                    {
                        foreach (int subId in saleDetails.Select(a => a.ContractSubId).Distinct())
                        {
                            //验证合约
                            result = subDAL.Get(user, subId);
                            if (result.ResultStatus != 0)
                                return result;

                            NFMT.Contract.Model.ContractSub saleSub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                            if (saleSub.SubId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "子合约不存在";
                                return result;
                            }

                            sumNetAmount = 0;
                            sumReceiptAmount = 0;
                            sumMissAmount = 0;
                            missRate = 0;

                            //明细验证
                            foreach (NFMT.WareHouse.Model.StockReceiptDetail detail in saleDetails.Where(a => a.ContractSubId == subId))
                            {
                                if (detail.ReceiptAmount > 0)
                                {
                                    //验证库存
                                    result = stockDAL.Get(user, detail.StockId);
                                    if (result.ResultStatus != 0)
                                        return result;

                                    Model.Stock stock = result.ReturnValue as Model.Stock;
                                    if (stock == null || stock.StockId <= 0)
                                    {
                                        result.ResultStatus = -1;
                                        result.Message = "库存不存在";
                                        return result;
                                    }

                                    //验证流水
                                    result = stockLogDAL.Get(user, detail.StockLogId);
                                    if (result.ResultStatus != 0)
                                        return result;

                                    Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                                    if (stockLog == null || stockLog.StockLogId <= 0)
                                    {
                                        result.ResultStatus = -1;
                                        result.Message = "库存流水不存在";
                                        return result;
                                    }

                                    if (stockLog.StockId != stock.StockId)
                                    {
                                        result.ResultStatus = -1;
                                        result.Message = "库存与库存流水不匹配";
                                        return result;
                                    }

                                    if (stockLog.SubContractId != subId)
                                    {
                                        result.ResultStatus = -1;
                                        result.Message = "库存流水与合约不匹配";
                                        return result;
                                    }

                                    detail.ContractId = saleSub.ContractId;
                                    detail.ContractSubId = saleSub.SubId;
                                    detail.DetailStatus = StatusEnum.已生效;
                                    detail.PreNetAmount = stock.NetAmount;
                                    detail.QtyMiss = detail.ReceiptAmount - stock.NetAmount;
                                    detail.QtyRate = (detail.ReceiptAmount / stock.NetAmount - 1) / 100;

                                    sumNetAmount += stock.NetAmount;
                                    sumReceiptAmount += detail.ReceiptAmount;
                                }
                            }

                            sumMissAmount = sumReceiptAmount - sumNetAmount;
                            missRate = (sumReceiptAmount / sumNetAmount - 1) / 100;

                            //赋值StockReceipt
                            stockReceipt.ContractId = saleSub.ContractId;
                            stockReceipt.ContractSubId = saleSub.SubId;
                            stockReceipt.PreNetAmount = sumNetAmount;
                            stockReceipt.ReceiptAmount = sumReceiptAmount;
                            stockReceipt.UnitId = saleSub.UnitId;
                            stockReceipt.QtyMiss = sumMissAmount;
                            stockReceipt.QtyRate = missRate;
                            stockReceipt.Receipter = user.EmpId;
                            stockReceipt.ReceiptDate = DateTime.Now;

                            if (saleSub.TradeDirection == (int)NFMT.Contract.TradeDirectionEnum.Buy)
                                stockReceipt.ReceiptType = (int)WareHouse.ReceiptTypeEnum.StockInReceipt;
                            else
                                stockReceipt.ReceiptType = (int)WareHouse.ReceiptTypeEnum.StockOutReceipt;

                            //新增StockReceipt
                            result = stockreceiptDAL.Insert(user, stockReceipt);
                            if (result.ResultStatus != 0)
                                return result;

                            receiptId = 0;
                            if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out receiptId))
                            {
                                result.ResultStatus = -1;
                                result.Message = "新增库存回执失败";
                                return result;
                            }

                            //新增明细
                            foreach (NFMT.WareHouse.Model.StockReceiptDetail detail in saleDetails.Where(a => a.ContractSubId == subId))
                            {
                                detail.ReceiptId = receiptId;
                                result = detailDAL.Insert(user, detail);
                                if (result.ResultStatus != 0)
                                    return result;
                            }
                        }


                        //if (isAudit)
                        //{
                        //    stockReceipt.ReceiptId = receiptId;
                        //    NFMT.WorkFlow.AutoSubmit submit = new NFMT.WorkFlow.AutoSubmit();
                        //    result = submit.Submit(user, stockReceipt, new TaskProvider.StockReceiptTaskProvider(), NFMT.WorkFlow.MasterEnum.库存净重回执审核);
                        //    if (result.ResultStatus != 0)
                        //        return result;
                        //}
                    }

                    #endregion

                    stockReceipt.ReceiptId = receiptId;
                    if (result.ResultStatus == 0)
                    {
                        if (saleDetails != null && saleDetails.Any())
                            result.ReturnValue = stockReceipt;
                        else
                            result.ReturnValue = null;

                        result.Message = resultStr;
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel Update(UserModel user, NFMT.WareHouse.Model.StockReceipt stockReceipt, List<NFMT.WareHouse.Model.StockReceiptDetail> details)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
                NFMT.Contract.DAL.ContractDAL contractDAL = new Contract.DAL.ContractDAL();
                NFMT.WareHouse.DAL.StockDAL stockDAL = new StockDAL();
                DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                DAL.StockReceiptDAL receiptDAL = new StockReceiptDAL();
                DAL.StockReceiptDetailDAL detailDAL = new StockReceiptDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证仓库回执
                    result = receiptDAL.Get(user, stockReceipt.ReceiptId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockReceipt resultReceipt = result.ReturnValue as Model.StockReceipt;
                    if (resultReceipt == null || resultReceipt.ReceiptId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "仓库回执不存在";
                        return result;
                    }

                    //获取仓库回执明细
                    result = detailDAL.Load(user, resultReceipt.ReceiptId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockReceiptDetail> resultDetails = result.ReturnValue as List<Model.StockReceiptDetail>;
                    if (resultDetails == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "仓库回执明细获取失败";
                        return result;
                    }

                    //作废现有仓库回执明细
                    foreach (Model.StockReceiptDetail d in resultDetails)
                    {
                        if (d.DetailStatus == StatusEnum.已生效)
                            d.DetailStatus = StatusEnum.已录入;

                        result = detailDAL.Invalid(user, d);
                        if (result.ResultStatus != 0)
                            return result;
                    }


                    //验证合约
                    result = subDAL.Get(user, resultReceipt.ContractSubId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                    if (sub.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约不存在";
                        return result;
                    }

                    result = contractDAL.Get(user, sub.ContractId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                    if (contract == null || contract.ContractId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "合约不存在";
                        return result;
                    }

                    decimal sumNetAmount = 0;
                    decimal sumReceiptAmount = 0;
                    decimal sumMissAmount = 0;
                    decimal missRate = 0;

                    //明细验证
                    foreach (NFMT.WareHouse.Model.StockReceiptDetail detail in details)
                    {
                        //验证库存
                        result = stockDAL.Get(user, detail.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.Stock stock = result.ReturnValue as Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存不存在";
                            return result;
                        }

                        //验证流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存流水不存在";
                            return result;
                        }

                        if (stockLog.StockId != stock.StockId)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存与库存流水不匹配";
                            return result;
                        }

                        if (stockLog.SubContractId != stockReceipt.ContractSubId)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存流水与合约不匹配";
                            return result;
                        }

                        detail.ReceiptId = resultReceipt.ReceiptId;
                        detail.ContractId = sub.ContractId;
                        detail.ContractSubId = sub.SubId;
                        detail.DetailStatus = StatusEnum.已生效;
                        detail.PreNetAmount = stock.NetAmount;
                        detail.QtyMiss = detail.ReceiptAmount - stock.NetAmount;
                        detail.QtyRate = (detail.ReceiptAmount / stock.NetAmount - 1) / 100;

                        sumNetAmount += stock.NetAmount;
                        sumReceiptAmount += detail.ReceiptAmount;

                        //新增仓库回执明细
                        result = detailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    sumMissAmount = sumReceiptAmount - sumNetAmount;
                    missRate = (sumReceiptAmount / sumNetAmount - 1) / 100;

                    //赋值StockReceipt                   
                    resultReceipt.PreNetAmount = sumReceiptAmount;
                    resultReceipt.QtyMiss = sumMissAmount;
                    resultReceipt.QtyRate = missRate;
                    resultReceipt.ReceiptAmount = sumReceiptAmount;
                    resultReceipt.Receipter = user.EmpId;
                    resultReceipt.Memo = stockReceipt.Memo;
                    resultReceipt.ReceiptDate = stockReceipt.ReceiptDate;
                    if (contract.TradeDirection == (int)NFMT.Contract.TradeDirectionEnum.Buy)
                        resultReceipt.ReceiptType = (int)WareHouse.ReceiptTypeEnum.StockInReceipt;
                    else
                        resultReceipt.ReceiptType = (int)WareHouse.ReceiptTypeEnum.StockOutReceipt;
                    resultReceipt.UnitId = sub.UnitId;

                    //修改StockReceipt
                    result = stockreceiptDAL.Update(user, resultReceipt);
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

        public ResultModel GoBack(UserModel user, int receiptId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证回执
                    result = this.stockreceiptDAL.Get(user, receiptId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockReceipt stockReceipt = result.ReturnValue as StockReceipt;

                    if (stockReceipt == null || stockReceipt.ReceiptId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能撤返";
                        return result;
                    }

                    //撤返出库
                    result = this.stockreceiptDAL.Goback(user, stockReceipt);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废工作流审核
                    WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, stockReceipt);
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

        public ResultModel Invalid(UserModel user, int receiptId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.stockreceiptDAL.Get(user, receiptId);
                    if (result.ResultStatus != 0)
                        return result;

                    StockReceipt stockReceipt = result.ReturnValue as StockReceipt;

                    if (stockReceipt == null || stockReceipt.ReceiptId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能作废";
                        return result;
                    }

                    result = this.stockreceiptDAL.Invalid(user, stockReceipt);
                    if (result.ResultStatus != 0)
                        return result;

                    StockReceiptDetailDAL detailDAL = new StockReceiptDetailDAL();
                    result = detailDAL.Load(user, stockReceipt.ReceiptId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockReceiptDetail> details = result.ReturnValue as List<Model.StockReceiptDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取回执明细失败";
                        return result;
                    }
                    foreach (Model.StockReceiptDetail detail in details)
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
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Complete(UserModel user, int receiptId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockDAL stockDAL = new StockDAL();
                DAL.StockLogDAL stockLogDAL = new StockLogDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.stockreceiptDAL.Get(user, receiptId);
                    if (result.ResultStatus != 0)
                        return result;

                    StockReceipt stockReceipt = result.ReturnValue as StockReceipt;

                    if (stockReceipt == null || stockReceipt.ReceiptId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能作废";
                        return result;
                    }

                    result = this.stockreceiptDAL.Complete(user, stockReceipt);
                    if (result.ResultStatus != 0)
                        return result;

                    StockReceiptDetailDAL detailDAL = new StockReceiptDetailDAL();
                    result = detailDAL.Load(user, stockReceipt.ReceiptId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockReceiptDetail> details = result.ReturnValue as List<Model.StockReceiptDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取回执明细失败";
                        return result;
                    }
                    foreach (Model.StockReceiptDetail detail in details)
                    {
                        result = detailDAL.Complete(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        ////获取库存
                        //result = stockDAL.Get(user, detail.StockId);
                        //if (result.ResultStatus != 0)
                        //    return result;

                        //Model.Stock stock = result.ReturnValue as Model.Stock;
                        //if (stock == null || stock.StockId <= 0)
                        //{
                        //    result.ResultStatus = -1;
                        //    result.Message = "库存不存在";
                        //    return result;
                        //}

                        ////更新库存净重
                        //if (stockReceipt.ReceiptType == (int)ReceiptTypeEnum.入库回执)
                        //{
                        //    stock.ReceiptInGap += detail.QtyMiss;
                        //    //stock.NetAmount += detail.QtyMiss;
                        //}
                        //else
                        //    stock.ReceiptOutGap += detail.QtyMiss;
                        //stock.CurNetAmount += detail.QtyMiss;

                        //result = stockDAL.Update(user, stock);
                        //if (result.ResultStatus != 0)
                        //    return result;
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
                    log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel CompleteCancel(UserModel user, int receiptId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockDAL stockDAL = new StockDAL();
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.stockreceiptDAL.Get(user, receiptId);
                    if (result.ResultStatus != 0)
                        return result;

                    StockReceipt stockReceipt = result.ReturnValue as StockReceipt;

                    if (stockReceipt == null || stockReceipt.ReceiptId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能作废";
                        return result;
                    }

                    result = this.stockreceiptDAL.CompleteCancel(user, stockReceipt);
                    if (result.ResultStatus != 0)
                        return result;

                    StockReceiptDetailDAL detailDAL = new StockReceiptDetailDAL();
                    result = detailDAL.Load(user, stockReceipt.ReceiptId, StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockReceiptDetail> details = result.ReturnValue as List<Model.StockReceiptDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取回执明细失败";
                        return result;
                    }
                    foreach (Model.StockReceiptDetail detail in details)
                    {
                        result = detailDAL.CompleteCancel(user, detail);
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
                            result.Message = "库存不存在";
                            return result;
                        }

                        //更新库存净重
                        if (stockReceipt.ReceiptType == (int)ReceiptTypeEnum.入库回执)
                        {
                            stock.ReceiptInGap -= detail.QtyMiss;
                            stock.NetAmount -= detail.QtyMiss;
                        }
                        else
                            stock.ReceiptOutGap -= detail.QtyMiss;
                        stock.CurNetAmount -= detail.QtyMiss;

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
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Audit(UserModel user, NFMT.WorkFlow.Model.DataSource dataSource, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockDAL stockDAL = new StockDAL();
                NFMT.WareHouse.DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                NFMT.WareHouse.DAL.StockReceiptDetailDAL stockReceiptDetailDAL = new StockReceiptDetailDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.stockreceiptDAL.Get(NFMT.Common.DefaultValue.SysUser, dataSource.RowId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockReceipt stockReceipt = result.ReturnValue as Model.StockReceipt;
                    if (stockReceipt == null || stockReceipt.ReceiptId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "库存回执不存在";
                        return result;
                    }

                    //审核，修改数据状态
                    result = this.stockreceiptDAL.Audit(user, stockReceipt, isPass);
                    if (result.ResultStatus != 0)
                        return result;

                    //审核通过
                    if (isPass)
                    {
                        //获取回执明细
                        result = stockReceiptDetailDAL.Load(user, stockReceipt.ReceiptId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.StockReceiptDetail> stockReceiptDetails = result.ReturnValue as List<Model.StockReceiptDetail>;
                        if (stockReceiptDetails == null || stockReceiptDetails.Count == 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取回执明细失败";
                            return result;
                        }

                        foreach (Model.StockReceiptDetail detail in stockReceiptDetails)
                        {
                            //获取库存流水
                            result = stockLogDAL.Get(NFMT.Common.DefaultValue.SysUser, detail.StockLogId);
                            if (result.ResultStatus != 0)
                                return result;

                            Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                            if (stockLog == null || stockLog.StockLogId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "获取库存流水失败";
                                return result;
                            }

                            //更新库存流水回执重量
                            stockLog.GapAmount += detail.QtyMiss;
                            result = stockLogDAL.Update(user, stockLog);
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
                                result.Message = "库存不存在";
                                return result;
                            }

                            //更新库存净重
                            if (stockReceipt.ReceiptType == (int)ReceiptTypeEnum.入库回执)
                            {
                                stock.ReceiptInGap += detail.QtyMiss;
                                //stock.NetAmount += detail.QtyMiss;
                            }
                            else
                                stock.ReceiptOutGap += detail.QtyMiss;
                            stock.CurNetAmount = detail.ReceiptAmount;

                            result = stockDAL.Update(user, stock);
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
                return result;
            }


            return result;
        }

        public ResultModel Close(UserModel user, int receiptId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                DAL.StockReceiptDetailDAL stockReceiptDetailDAL = new StockReceiptDetailDAL();
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取回执
                    result = this.stockreceiptDAL.Get(user, receiptId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockReceipt stockReceipt = result.ReturnValue as Model.StockReceipt;
                    if (stockReceipt == null || stockReceipt.ReceiptId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "库存回执不存在";
                        return result;
                    }

                    //关闭回执
                    result = this.stockreceiptDAL.Close(user, stockReceipt);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取回执明细
                    result = stockReceiptDetailDAL.Load(user, stockReceipt.ReceiptId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockReceiptDetail> details = result.ReturnValue as List<Model.StockReceiptDetail>;
                    if (details == null || details.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "回执明细获取失败";
                        return result;
                    }

                    foreach (Model.StockReceiptDetail detail in details)
                    {
                        //关闭回执明细
                        result = stockReceiptDetailDAL.Close(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取库存流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存流水获取失败";
                            return result;
                        }

                        //更新库存流水磅差
                        stockLog.GapAmount -= detail.QtyMiss;
                        result = stockLogDAL.Update(user, stockLog);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public SelectModel GetStockReceiptUpdateListSelectModel(int pageIndex, int pageSize, string orderStr, string refNo, int receiptType)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "srd.DetailId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("srd.DetailId,srd.StockId,srd.StockLogId,ass.AssetName,sn.RefNo,st.StockDate,st.CardNo,srd.PreNetAmount,srd.ReceiptAmount,sr.ReceiptType,bd.DetailName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockReceipt sr ");
            sb.AppendFormat(" inner join dbo.St_StockReceiptDetail srd on sr.ReceiptId = srd.ReceiptId and srd.DetailStatus >={0} ", (int)Common.StatusEnum.已生效);
            sb.Append(" left join dbo.St_Stock st on st.StockId = srd.StockId");
            sb.Append(" left join NFMT_Basic..Asset ass on ass.AssetId = st.AssetId ");
            sb.Append(" left join dbo.St_StockName sn on sn.StockNameId = st.StockNameId ");
            sb.Append(" left join NFMT_Basic..BDStyleDetail bd on bd.StyleDetailId = sr.ReceiptType ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" srd.StockLogId not in ( ");
            sb.Append(" select distinct bid.StockLogId ");
            sb.Append(" from dbo.Inv_FinBusInvAllotDetail fiad ");
            sb.Append(" left join dbo.Inv_BusinessInvoice bi on fiad.BusinessInvoiceId = bi.BusinessInvoiceId ");
            sb.AppendFormat(" inner join dbo.Invoice inv on bi.InvoiceId = inv.InvoiceId and inv.InvoiceStatus >={0} ", (int)Common.StatusEnum.已生效);
            sb.AppendFormat(" left join dbo.Inv_BusinessInvoiceDetail bid on bi.BusinessInvoiceId = bid.BusinessInvoiceId and bid.DetailStatus >={0}) ", (int)Common.StatusEnum.已生效);

            if (receiptType > 0)
                sb.AppendFormat(" and sr.ReceiptType = {0}", receiptType);
            if (!string.IsNullOrEmpty(refNo))
                sb.AppendFormat(" and sn.RefNo like '%{0}%' ", refNo);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel StockReceiptUpdate(UserModel user, int detailId, int stockId, int stockLogId, decimal receiptAmount)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockDAL stockDAL = new StockDAL();
                DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                DAL.StockReceiptDetailDAL stockReceiptDetailDAL = new StockReceiptDetailDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = stockReceiptDetailDAL.Get(user, detailId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockReceiptDetail stockReceiptDetail = result.ReturnValue as Model.StockReceiptDetail;
                    if (stockReceiptDetail == null || stockReceiptDetail.DetailId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取库存回执错误";
                        return result;
                    }
                    stockReceiptDetail.ReceiptAmount = receiptAmount;
                    stockReceiptDetail.QtyMiss = receiptAmount - stockReceiptDetail.PreNetAmount;
                    stockReceiptDetail.QtyRate = (receiptAmount / stockReceiptDetail.PreNetAmount - 1) / 100;

                    //修改库存回执明细
                    result = stockReceiptDetailDAL.Update(user, stockReceiptDetail);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取库存回执
                    result = stockreceiptDAL.Get(user, stockReceiptDetail.ReceiptId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockReceipt stockReceipt = result.ReturnValue as Model.StockReceipt;
                    if (stockReceipt == null || stockReceipt.ReceiptId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取库存回执出错";
                        return result;
                    }

                    result = stockLogDAL.Get(user, stockLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                    if (stockLog == null || stockLog.StockLogId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取库存流水出错";
                        return result;
                    }

                    stockLog.GapAmount = receiptAmount - stockReceiptDetail.PreNetAmount;

                    //更新库存流水
                    result = stockLogDAL.Update(user, stockLog);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取库存
                    result = stockDAL.Get(user, stockId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Stock stock = result.ReturnValue as Model.Stock;
                    if (stock == null || stock.StockId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取库存出错";
                        return result;
                    }

                    if (stockReceipt.ReceiptType == (int)NFMT.WareHouse.ReceiptTypeEnum.入库回执)
                    {
                        stock.ReceiptInGap = receiptAmount - stockReceiptDetail.PreNetAmount;
                    }
                    else if (stockReceipt.ReceiptType == (int)NFMT.WareHouse.ReceiptTypeEnum.出库回执)
                    {
                        stock.ReceiptOutGap = receiptAmount - stockReceiptDetail.PreNetAmount;
                    }
                    stock.CurNetAmount = receiptAmount;

                    //更新库存
                    result = stockDAL.Update(user, stock);
                    if (result.ResultStatus != 0)
                        return result;

                    //若此库存已开业务票，则修改业务票中的净重
                    result = stockreceiptDAL.UpdateBussinessInvDetail(user, stockLogId, receiptAmount);
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
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        #endregion

        #region report

        public SelectModel GetReceiptReportSelect(int pageIndex, int pageSize, string orderStr, int assetId, string refNo)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sis.RefId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)Common.StatusEnum.已生效;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("sis.RefId,ass.AssetName,sn.RefNo,st.PaperNo,st.CardNo,st.StockDate,sub.OutContractNo,inCorp.CorpName as InCorpName,outCorp.CorpName as OutCorpName,");
            sb.Append("mu.MUName,inLog.NetAmount as inNetAmount,inLog.NetAmount+inLog.GapAmount as ReceiptInGap,inLog.GapAmount as inGapAmount,case when ISNULL(inLog.NetAmount+inLog.GapAmount,0)<>0 then convert(varchar,cast((inLog.GapAmount/inLog.NetAmount*100) as decimal(10,4)))+'%' else '' end as inRate,");
            sb.Append("outLog.NetAmount as outNetAmount,outLog.ReceiptOutGap,outLog.GapAmount as outGapAmount,case when ISNULL(outLog.ReceiptOutGap,0)<>0 then convert(varchar,cast((outLog.GapAmount/outLog.NetAmount*100) as decimal(10,4)))+'%' else '' end as outRate,(inLog.NetAmount+inLog.GapAmount-inLog.NetAmount)+outLog.ReceiptOutGap-outLog.NetAmount as ProfitOrLoss");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT..St_StockInStock_Ref sis ");
            sb.AppendFormat(" left join NFMT..St_Stock st on sis.StockId = st.StockId and st.StockStatus not in ({0},{1}) ", (int)NFMT.WareHouse.StockStatusEnum.已拆库存, (int)NFMT.WareHouse.StockStatusEnum.作废库存);
            sb.Append(" left join NFMT_Basic..MeasureUnit mu on st.UintId = mu.MUId ");
            sb.Append(" left join NFMT_Basic..Asset ass on st.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT..St_StockName sn on st.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" left join NFMT..St_ContractStockIn_Ref csi on sis.StockInId = csi.StockInId and csi.RefStatus >= {0} ", readyStatus);
            sb.Append(" left join NFMT..Con_ContractSub sub on csi.ContractSubId = sub.SubId ");
            sb.AppendFormat(" left join NFMT..Con_SubCorporationDetail inCorpDetail on sub.SubId = inCorpDetail.SubId and inCorpDetail.IsDefaultCorp = 1 and inCorpDetail.IsInnerCorp = 1 and inCorpDetail.DetailStatus >= {0} ", readyStatus);
            sb.Append(" left join NFMT_User..Corporation inCorp on inCorpDetail.CorpId = inCorp.CorpId ");
            sb.AppendFormat(" left join NFMT..Con_SubCorporationDetail outCorpDetail on sub.SubId = outCorpDetail.SubId and outCorpDetail.IsDefaultCorp = 1 and outCorpDetail.IsInnerCorp = 0 and outCorpDetail.DetailStatus >={0} ", readyStatus);
            sb.Append(" left join NFMT_User..Corporation outCorp on outCorpDetail.CorpId = outCorp.CorpId ");
            sb.AppendFormat(" left join NFMT..St_StockLog inLog on st.StockId = inLog.StockId and inLog.LogType = {0} ", (int)NFMT.WareHouse.LogTypeEnum.入库);
            sb.AppendFormat(" left join (select StockId,LogType,Sum(NetAmount) NetAmount,sum(NetAmount+GapAmount) as ReceiptOutGap,SUM(GapAmount) as GapAmount from NFMT..St_StockLog slog where slog.LogType = {0} group by StockId,LogType) outLog on st.StockId = outLog.StockId ", (int)NFMT.WareHouse.LogTypeEnum.出库);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" sis.RefStatus >= {0} ", readyStatus);
            if (assetId > 0)
                sb.AppendFormat(" and st.AssetId = {0} ", assetId);
            if (!string.IsNullOrEmpty(refNo))
                sb.AppendFormat(" and sn.RefNo like '%{0}%' ", refNo);

            select.WhereStr = sb.ToString();

            return select;
        }

        //public override string[,] SetExcelRangeData(DataTable source)
        //{
        //    string[,] objData = new string[source.Rows.Count, 18];

        //    for (int i = 0; i < source.Rows.Count; i++)
        //    {
        //        System.Data.DataRow dr = source.Rows[i];

        //        objData[i, 0] = dr["AssetName"].ToString();
        //        objData[i, 1] = dr["RefNo"].ToString();
        //        objData[i, 2] = dr["PaperNo"].ToString();
        //        objData[i, 3] = dr["CardNo"].ToString();
        //        objData[i, 4] = ((DateTime)dr["StockDate"]).ToString("yyyy-MM-dd");
        //        objData[i, 5] = dr["OutContractNo"].ToString();
        //        objData[i, 6] = dr["InCorpName"].ToString();
        //        objData[i, 7] = dr["OutCorpName"].ToString();
        //        objData[i, 8] = dr["MUName"].ToString();
        //        objData[i, 9] = dr["inNetAmount"].ToString();
        //        objData[i, 10] = dr["ReceiptInGap"].ToString();
        //        objData[i, 11] = dr["inGapAmount"].ToString();
        //        objData[i, 12] = dr["inRate"].ToString();
        //        objData[i, 13] = dr["outNetAmount"].ToString();
        //        objData[i, 14] = dr["ReceiptOutGap"].ToString();
        //        objData[i, 15] = dr["outGapAmount"].ToString();
        //        objData[i, 16] = dr["outRate"].ToString();
        //        objData[i, 17] = dr["ProfitOrLoss"].ToString();
        //    }

        //    return objData;
        //}

        public override DataTable SetExcelRangeData(DataTable source)
        {
            string[] strs = new string[] { "AssetName", "RefNo", "PaperNo", "CardNo", "StockDate", "OutContractNo", "InCorpName", "OutCorpName", "MUName", "inNetAmount", "ReceiptInGap", "inGapAmount", "inRate", "outNetAmount", "ReceiptOutGap", "outGapAmount", "outRate", "ProfitOrLoss" };

            return source.ConvertDataTable(strs);
        }

        #endregion
    }
}
