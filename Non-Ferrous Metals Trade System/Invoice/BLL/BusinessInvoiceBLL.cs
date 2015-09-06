/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BusinessInvoiceBLL.cs
// 文件功能描述：业务发票dbo.Inv_BusinessInvoice业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月25日
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using log4net;
using NFMT.Common;
using NFMT.Contract;
using NFMT.Contract.DAL;
using NFMT.Contract.Model;
using NFMT.Data;
using NFMT.Data.Model;
using NFMT.Invoice.DAL;
using NFMT.Invoice.Model;
using NFMT.Operate.DAL;
using NFMT.User;
using NFMT.User.Model;
using NFMT.WareHouse.DAL;
using NFMT.WareHouse.Model;
using NFMT.WorkFlow.DAL;

namespace NFMT.Invoice.BLL
{
    /// <summary>
    /// 业务发票dbo.Inv_BusinessInvoice业务逻辑类。
    /// </summary>
    public class BusinessInvoiceBLL : ExecBLL
    {
        private BusinessInvoiceDAL businessinvoiceDAL = new BusinessInvoiceDAL();
        private ILog log = LogManager.GetLogger(typeof(BusinessInvoiceDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BusinessInvoiceBLL()
        {
        }

        #endregion

        #region 数据库操作

        protected override ILog Log
        {
            get { return this.log; }
        }

        public override IOperate Operate
        {
            get { return this.businessinvoiceDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, DateTime invoiceDateBegin, DateTime invoiceDateEnd, int status, int inCorpId, int outCorpId, InvoiceTypeEnum invoiceType)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "bi.BusinessInvoiceId desc";
            else
                select.OrderStr = orderStr;

            int commonStatusType = (int)StatusTypeEnum.通用状态;
            int InvoiceDirection = (int)StyleEnum.InvoiceDirection;

            StringBuilder sb = new StringBuilder();
            sb.Append("bi.BusinessInvoiceId,inv.InvoiceId,inv.InvoiceDate,inv.InvoiceNo,inv.InvoiceName,inv.InvoiceBala,inv.CurrencyId,cur.CurrencyName,inv.OutBlocId,outBloc.BlocName as OutBlocName,inv.OutCorpId,OutCorpName,inv.InBlocId,inBloc.BlocName as InBlocName,inv.InCorpId,InCorpName,inv.InvoiceStatus,sd.StatusName,bi.VATRatio,bi.VATBala,inv.InvoiceDirection,invDir.DetailName as DirectionName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Inv_BusinessInvoice bi ");
            sb.Append(" inner join dbo.Invoice inv on bi.InvoiceId = inv.InvoiceId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = inv.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Bloc outBloc on outBloc.BlocId = inv.OutBlocId ");
            sb.Append(" left join NFMT_User.dbo.Bloc inBloc on inBloc.BlocId = inv.InBlocId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = inv.InvoiceStatus and sd.StatusId ={0} ", commonStatusType);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail invDir on inv.InvoiceDirection = invDir.StyleDetailId and invDir.BDStyleId ={0} ", InvoiceDirection);
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" inv.InvoiceType = {0} ", (int)invoiceType);

            if (status > 0)
                sb.AppendFormat(" and inv.InvoiceStatus = {0} ", status);
            if (inCorpId > 0)
                sb.AppendFormat(" and inv.InCorpId = {0} ", inCorpId);
            if (outCorpId > 0)
                sb.AppendFormat(" and inv.OutCorpId = {0} ", outCorpId);
            if (invoiceDateBegin > DefaultValue.DefaultTime && invoiceDateEnd > invoiceDateBegin)
                sb.AppendFormat(" and inv.InvoiceDate between '{0}' and '{1}' ", invoiceDateBegin.ToString(), invoiceDateEnd.AddDays
                    (1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        #region 发票列表SelectModel

        public SelectModel GetDirectFinalSelect(int pageIndex, int pageSize, string orderStr, DateTime invoiceDateBegin, DateTime invoiceDateEnd, int status, int inCorpId, int outCorpId)
        {
            return this.GetSelectModel(pageIndex, pageSize, orderStr, invoiceDateBegin, invoiceDateEnd, status, inCorpId, outCorpId, InvoiceTypeEnum.DirectFinalInvoice);
        }

        public SelectModel GetProvisionalSelect(int pageIndex, int pageSize, string orderStr, DateTime invoiceDateBegin, DateTime invoiceDateEnd, int status, int inCorpId, int outCorpId)
        {
            return this.GetSelectModel(pageIndex, pageSize, orderStr, invoiceDateBegin, invoiceDateEnd, status, inCorpId, outCorpId, InvoiceTypeEnum.ProvisionalInvoice);
        }

        public SelectModel GetReplaceFinalSelect(int pageIndex, int pageSize, string orderStr, DateTime invoiceDateBegin, DateTime invoiceDateEnd, int status, int inCorpId, int outCorpId)
        {
            return this.GetSelectModel(pageIndex, pageSize, orderStr, invoiceDateBegin, invoiceDateEnd, status, inCorpId, outCorpId, InvoiceTypeEnum.ReplaceFinalInvoice);
        }

        public SelectModel GetSuppleFinalSelect(int pageIndex, int pageSize, string orderStr, DateTime invoiceDateBegin, DateTime invoiceDateEnd, int status, int inCorpId, int outCorpId)
        {
            return this.GetSelectModel(pageIndex, pageSize, orderStr, invoiceDateBegin, invoiceDateEnd, status, inCorpId, outCorpId, InvoiceTypeEnum.SuppleFinalInvoice);
        }

        public SelectModel GetProvisionaContractListSelect(int pageIndex, int pageSize, string orderStr, string subNo, int outCorpId, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "soa.StockOutApplyId asc";
            else
                select.OrderStr = orderStr;

            int status = (int)StatusEnum.已生效;
            int statusType = (int)StatusTypeEnum.通用状态;

            StringBuilder sb = new StringBuilder();
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

            sb.AppendFormat(" con.ContractStatus = {0} and cs.SubStatus = {0} ", status);
            sb.AppendFormat(" and con.TradeBorder = {0}", (int)TradeBorderEnum.ForeignTrade);

            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and cs.SubNo like '%{0}%'", subNo);
            if (outCorpId > 0)
                sb.AppendFormat("  and outccd.CorpId ={0}", outCorpId);
            if (applyTimeBegin > DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and cs.CreateTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays
                    (1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetDirectFinalContractListSelect(int pageIndex, int pageSize, string orderStr, string subNo, int outCorpId, DateTime applyTimeBegin, DateTime applyTimeEnd, int tradeDir)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "soa.StockOutApplyId asc";
            else
                select.OrderStr = orderStr;

            int status = (int)StatusEnum.已生效;
            int statusType = (int)StatusTypeEnum.通用状态;

            StringBuilder sb = new StringBuilder();
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

            sb.AppendFormat(" con.ContractStatus = {0} and cs.SubStatus = {0} ", status);
            sb.AppendFormat(
                " and cs.SubId not in (select SubContractId from dbo.Inv_BusinessInvoice bi left join dbo.Invoice inv on bi.InvoiceId = inv.InvoiceId where inv.InvoiceStatus >= {0}) ",
                status);
            //sb.AppendFormat(" and con.TradeBorder = {0}", (int)NFMT.Contract.TradeBorderEnum.ForeignTrade);

            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and cs.SubNo like '%{0}%'", subNo);
            if (outCorpId > 0)
                sb.AppendFormat("  and outccd.CorpId ={0}", outCorpId);
            if (applyTimeBegin > DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and cs.CreateTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays
                    (1).ToString());
            if (tradeDir > 0)
                sb.AppendFormat(" and cs.TradeDirection = {0} ", tradeDir);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetReadyFinalSelect(int pageIndex, int pageSize, string orderStr)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "fi.BusinessInvoiceId";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)StatusEnum.已生效;
            int commonStatus = (int)StatusTypeEnum.通用状态;
            int replaceType = (int)InvoiceTypeEnum.ReplaceFinalInvoice;
            int directType = (int)InvoiceTypeEnum.DirectFinalInvoice;

            StringBuilder sb = new StringBuilder();
            sb.Append(" fi.BusinessInvoiceId,inv.InvoiceId ");
            sb.Append(" ,inv.InvoiceDate,inv.InvoiceNo,inv.InvoiceName,inv.InvoiceDirection,id.DetailName as DirectionName ");
            sb.Append(" ,inv.InvoiceBala,inv.CurrencyId,cur.CurrencyName,inv.OutCorpId,inv.OutCorpName,inv.InCorpId,inv.InCorpName ");
            sb.Append(" ,fi.VATRatio,fi.VATBala,inv.InvoiceStatus,sd.StatusName,inv.InvoiceType,it.DetailName as TypeName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Inv_BusinessInvoice fi ");
            sb.AppendFormat(" inner join dbo.Invoice inv on fi.InvoiceId = inv.InvoiceId and inv.InvoiceType in ({0},{1}) ", replaceType, directType);
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail id on id.StyleDetailId = inv.InvoiceDirection ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = inv.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = inv.InvoiceStatus and sd.StatusId={0} ", commonStatus);
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail it on it.StyleDetailId = inv.InvoiceType ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" inv.InvoiceStatus = {0} ", readyStatus);
            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion

        #region 获取库存

        public SelectModel GetBusinessInvoiceContractStockListSelect(int pageIndex, int pageSize, string orderStr, int subId, InvoiceTypeEnum invoiceType, int businessInvoiceId = 0, bool isDetail = false, bool isInvoice = false)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)StatusEnum.已生效;
            int entryStatus = (int)StatusEnum.已录入;
            int stockStatusType = (int)StatusTypeEnum.库存状态;
            int invType = (int)invoiceType;

            int proInvoiceType = (int)InvoiceTypeEnum.ProvisionalInvoice;
            int dirInvoiceType = (int)InvoiceTypeEnum.DirectFinalInvoice;

            StringBuilder sb = new StringBuilder();
            sb.Append("sl.StockLogId,sto.StockId,sn.StockNameId");
            sb.Append(",sto.StockDate,sn.RefNo,sto.UintId,mu.MUName");
            sb.Append(",sto.CorpId,cor.CorpName");
            sb.Append(",sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,sto.StockStatus");
            sb.Append(",ss.StatusName as StockStatusName");
            sb.Append(",sl.NetAmount - isnull(abi.SumNet,0) as LastAmount");
            sb.Append(",sl.GrossAmount - isnull(abi.SumGross,0) as LastGross");
            sb.Append(",sl.NetAmount - isnull(abi.SumNet,0) as NetAmount");
            sb.Append(",sl.GrossAmount - isnull(abi.SumGross,0) as IntegerAmount");
            sb.Append(",isnull(cbi.SumBala,0) as Bala");
            sb.Append(",sto.CardNo,dp.DPName ");
            select.ColumnName = sb.ToString();

            sb.Clear();

            sb.Append(" dbo.St_StockLog sl ");
            sb.AppendFormat(" inner join dbo.St_Stock sto on sl.StockId = sto.StockId ");

            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId ={0} ", stockStatusType);

            sb.Append(" left join (select SUM(isnull(bid.NetAmount,0)) as SumNet,SUM(isnull(bid.Bala,0)) as SumBala ,bid.StockLogId ");
            sb.Append(" from dbo.Inv_BusinessInvoiceDetail bid ");
            sb.AppendFormat(" inner join dbo.Inv_BusinessInvoice bi on bid.BusinessInvoiceId = bi.BusinessInvoiceId and bi.SubContractId={0} ", subId);
            sb.AppendFormat(" inner join dbo.Invoice inv on inv.InvoiceId = bi.InvoiceId and inv.InvoiceStatus>={0} ", entryStatus);
            sb.AppendFormat(" where bid.BusinessInvoiceId = {0} and bid.DetailStatus>={1} and inv.InvoiceType in ({2},{3}) ", businessInvoiceId, readyStatus, proInvoiceType, dirInvoiceType);
            sb.Append(" group by bid.StockLogId) as cbi on cbi.StockLogId = sl.StockLogId ");

            sb.Append(" left join (select SUM(isnull(bid.NetAmount,0)) as SumNet,SUM(isnull(bid.IntegerAmount,0)) as SumGross,bid.StockLogId ");
            sb.Append(" from dbo.Inv_BusinessInvoiceDetail bid ");
            sb.AppendFormat(" inner join dbo.Inv_BusinessInvoice bi on bid.BusinessInvoiceId = bi.BusinessInvoiceId and bi.SubContractId={0} ", subId);
            sb.AppendFormat(" inner join dbo.Invoice inv on inv.InvoiceId = bi.InvoiceId and inv.InvoiceStatus>={0} ", entryStatus);
            sb.AppendFormat(" where bid.BusinessInvoiceId != {0} and bid.DetailStatus>={1} and inv.InvoiceType in ({2},{3}) ", businessInvoiceId, readyStatus, proInvoiceType, dirInvoiceType);
            sb.Append(" group by bid.StockLogId) as abi on abi.StockLogId = sl.StockLogId ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" sl.LogStatus >= {0} and sl.SubContractId = {1} ", readyStatus, subId);
            sb.Append(" and sl.NetAmount - isnull(abi.SumNet,0)>0 ");
            if (isDetail)
                sb.Append(" and isnull(cbi.SumBala,0)>0 ");
            if (isInvoice)
                sb.Append(" and cbi.StockLogId is not null ");
            else
                sb.Append(" and cbi.StockLogId is null ");
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetDirectFinalContractStockListSelect(int pageIndex, int pageSize, string orderStr, int subId, int businessInvoiceId = 0, bool isDetail = false, bool isInvoice = false)
        {
            return this.GetBusinessInvoiceContractStockListSelect(pageIndex, pageSize, orderStr, subId, InvoiceTypeEnum.DirectFinalInvoice, businessInvoiceId, isDetail, isInvoice);
        }

        public SelectModel GetDirectFinalStocksSelect(int pageIndex, int pageSize, string orderStr, int subId, int businessInvoiceId = 0, bool isDetail = false, bool isInvoice = false)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)StatusEnum.已生效;
            int entryStatus = (int)StatusEnum.已录入;
            int stockStatusType = (int)StatusTypeEnum.库存状态;

            int proInvoiceType = (int)InvoiceTypeEnum.ProvisionalInvoice;
            int dirInvoiceType = (int)InvoiceTypeEnum.DirectFinalInvoice;

            StringBuilder sb = new StringBuilder();
            sb.Append("sl.StockLogId,sto.StockId,sn.StockNameId");
            sb.Append(",sto.StockDate,sn.RefNo,sto.UintId,mu.MUName");
            sb.Append(",sto.CorpId,cor.CorpName");
            sb.Append(",sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,sto.StockStatus");
            sb.Append(",ss.StatusName as StockStatusName");
            sb.Append(",sl.NetAmount - isnull(abi.SumNet,0) + ISNULL(sl.GapAmount,0) as LastAmount");
            sb.Append(",sl.GrossAmount - isnull(abi.SumGross,0) as LastGross");
            sb.Append(",isnull(cbi.SumNet,sl.NetAmount - isnull(abi.SumNet,0) + ISNULL(sl.GapAmount,0)) as NetAmount");
            sb.Append(",sl.GrossAmount - isnull(abi.SumGross,0) as IntegerAmount");
            sb.Append(",sp.FixedPrice * isnull(cbi.SumNet,sl.NetAmount - isnull(abi.SumNet,0) + ISNULL(sl.GapAmount,0)) as Bala ");
            //sb.Append(",isnull(cbi.SumBala,sp.FixedPrice * isnull(cbi.SumNet,sl.NetAmount - isnull(abi.SumNet,0) + ISNULL(sl.GapAmount,0))) as Bala ");
            sb.Append(",sp.FixedPrice as UnitPrice");
            select.ColumnName = sb.ToString();

            sb.Clear();

            sb.Append(" dbo.St_StockLog sl ");
            sb.AppendFormat(" inner join dbo.St_Stock sto on sl.StockId = sto.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");

            sb.Append("left join dbo.Con_SubPrice sp on sp.SubId = sl.SubContractId");

            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId ={0} ", stockStatusType);

            sb.Append(" left join (select SUM(isnull(bid.NetAmount,0)) as SumNet,SUM(isnull(bid.Bala,0)) as SumBala ,bid.StockLogId ");
            sb.Append(" from dbo.Inv_BusinessInvoiceDetail bid ");
            sb.AppendFormat(" inner join dbo.Inv_BusinessInvoice bi on bid.BusinessInvoiceId = bi.BusinessInvoiceId and bi.SubContractId={0} ", subId);
            sb.AppendFormat(" inner join dbo.Invoice inv on inv.InvoiceId = bi.InvoiceId and inv.InvoiceStatus>={0} ", entryStatus);
            sb.AppendFormat(" where bid.BusinessInvoiceId = {0} and bid.DetailStatus>={1} and inv.InvoiceType in ({2},{3}) ", businessInvoiceId, readyStatus, proInvoiceType, dirInvoiceType);
            sb.Append(" group by bid.StockLogId) as cbi on cbi.StockLogId = sl.StockLogId ");

            sb.Append(" left join (select SUM(isnull(bid.NetAmount,0)) as SumNet,SUM(isnull(bid.IntegerAmount,0)) as SumGross,bid.StockLogId ");
            sb.Append(" from dbo.Inv_BusinessInvoiceDetail bid ");
            sb.AppendFormat(" inner join dbo.Inv_BusinessInvoice bi on bid.BusinessInvoiceId = bi.BusinessInvoiceId and bi.SubContractId={0} ", subId);
            sb.AppendFormat(" inner join dbo.Invoice inv on inv.InvoiceId = bi.InvoiceId and inv.InvoiceStatus>={0} ", entryStatus);
            sb.AppendFormat(" where bid.BusinessInvoiceId != {0} and bid.DetailStatus>={1} and inv.InvoiceType in ({2},{3}) ", businessInvoiceId, readyStatus, proInvoiceType, dirInvoiceType);
            sb.Append(" group by bid.StockLogId) as abi on abi.StockLogId = sl.StockLogId ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" sl.LogStatus >= {0} and sl.SubContractId = {1} ", readyStatus, subId);
            sb.Append(" and sl.NetAmount - isnull(abi.SumNet,0)>0 ");
            if (isDetail)
                sb.Append(" and isnull(cbi.SumBala,0)>0 ");
            if (isInvoice)
                sb.Append(" and cbi.StockLogId is not null ");
            else
                sb.Append(" and cbi.StockLogId is null ");
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetProvisionalContractStockListSelect(int pageIndex, int pageSize, string orderStr, int subId, int businessInvoiceId = 0, bool isDetail = false, bool isInvoice = false)
        {
            return this.GetBusinessInvoiceContractStockListSelect(pageIndex, pageSize, orderStr, subId, InvoiceTypeEnum.ProvisionalInvoice, businessInvoiceId, isDetail, isInvoice);
        }

        /// <summary>
        /// 替临终票中临票包含库存开票明细列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="provisionalInvoiceId"></param>
        /// <returns></returns>
        public SelectModel GetReplaceFinalByProvisionalStockListSelect(int pageIndex, int pageSize, string orderStr, int provisionalInvoiceId, int replaceInvoiceId = 0, bool isReplace = false)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)StatusEnum.已生效;
            int entryStatus = (int)StatusEnum.已录入;
            int stockStatusType = (int)StatusTypeEnum.库存状态;

            StringBuilder sb = new StringBuilder();
            sb.Append(" bid.DetailId as RefDetailId,bi.BusinessInvoiceId,inv.InvoiceId,sto.StockId,sl.StockLogId,sn.StockNameId,sto.StockDate,sn.RefNo,sto.CorpId ");
            sb.Append(" ,corp.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,sto.StockStatus,ss.StatusName as StockStatusName ");
            sb.Append(" ,inv.CurrencyId,cur.CurrencyName,mu.MUId,mu.MUName ");

            sb.Append(" ,ISNULL(bid.NetAmount,0) - ISNULL(lastDetail.SumAmount,0) as LastAmount ");
            sb.Append(",ISNULL(bid.IntegerAmount,0) - ISNULL(lastDetail.SumGross,0) as LastGross");
            sb.Append(" ,ISNULL(bid.Bala,0) - ISNULL(lastDetail.SumBala,0) as LastBala ");

            sb.Append(" ,ISNULL(curDetail.SumAmount,ISNULL(bid.NetAmount,0) - ISNULL(lastDetail.SumAmount,0)) as NetAmount ");
            sb.Append(",ISNULL(curDetail.SumGross,ISNULL(bid.IntegerAmount,0) - ISNULL(lastDetail.SumGross,0)) as IntegerAmount");
            sb.Append(" ,ISNULL(curDetail.SumBala,0) as Bala ");
            sb.Append(",sto.CardNo,dp.DPName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Inv_BusinessInvoiceDetail bid ");
            sb.Append(" inner join dbo.Inv_BusinessInvoice bi on bi.BusinessInvoiceId = bid.BusinessInvoiceId ");
            sb.AppendFormat(" inner join dbo.Invoice inv on bi.InvoiceId = inv.InvoiceId and inv.InvoiceStatus>={0} ", entryStatus);
            sb.AppendFormat(" inner join dbo.St_Stock sto on bid.StockId = sto.StockId ");
            sb.Append(" inner join dbo.St_StockLog sl on bid.StockLogId = sl.StockLogId ");
            sb.Append(" inner join dbo.St_StockName sn on sn.StockNameId = sto.StockNameId ");
            sb.Append(" left join NFMT_User.dbo.Corporation corp on corp.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = inv.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId ={0} ", stockStatusType);
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");

            sb.Append(" left join (select sum(isnull(bid.NetAmount,0)) as SumAmount,SUM(ISNULL(bid.IntegerAmount,0)) as SumGross,sum(isnull(bid.Bala,0)) as SumBala,bid.RefDetailId ");
            sb.AppendFormat(" from dbo.Inv_BusinessInvoiceDetail bid where bid.BusinessInvoiceId != {0} and bid.DetailStatus >=50 group by bid.RefDetailId) lastDetail ", replaceInvoiceId);
            sb.Append(" on lastDetail.RefDetailId = bid.DetailId ");

            sb.Append(" left join (select sum(isnull(bid.NetAmount,0)) as SumAmount,SUM(ISNULL(bid.IntegerAmount,0)) as SumGross,sum(isnull(bid.Bala,0)) as SumBala,bid.RefDetailId ");
            sb.AppendFormat(" from dbo.Inv_BusinessInvoiceDetail bid where bid.BusinessInvoiceId = {0} and bid.DetailStatus >=50 group by bid.RefDetailId) curDetail ", replaceInvoiceId);
            sb.Append(" on curDetail.RefDetailId = bid.DetailId ");

            select.TableName = sb.ToString();
            sb.Clear();

            sb.AppendFormat(" bid.DetailStatus >= {0} ", readyStatus);
            sb.AppendFormat(" and bid.BusinessInvoiceId = {0} ", provisionalInvoiceId);
            sb.Append(" and ISNULL(bid.NetAmount,0) - ISNULL(lastDetail.SumAmount,0) >0 ");

            if (isReplace)
                sb.Append(" and curDetail.RefDetailId is not null ");
            else
                sb.Append(" and curDetail.RefDetailId is null ");
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetReplaceFinalStocksSelect(int pageIndex, int pageSize, string orderStr, int provisionalInvoiceId, int replaceInvoiceId = 0, bool isReplace = false)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "bid.DetailId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)StatusEnum.已生效;
            int stockStatusType = (int)StatusTypeEnum.库存状态;
            int proInvoiceType = (int)InvoiceTypeEnum.临时发票;

            StringBuilder sb = new StringBuilder();
            sb.Append(" bid.DetailId as RefDetailId,bi.BusinessInvoiceId,inv.InvoiceId,sto.StockId,sl.StockLogId,sn.StockNameId,sto.StockDate,sn.RefNo,sto.CorpId ");
            sb.Append(" ,corp.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,sto.StockStatus,ss.StatusName as StockStatusName ");
            sb.Append(" ,inv.CurrencyId,cur.CurrencyName,mu.MUId,mu.MUName ");

            sb.Append(" ,ISNULL(bid.NetAmount,0) - ISNULL(lastDetail.SumAmount,0) as LastAmount ");
            //sb.Append(",ISNULL(bid.IntegerAmount,0) - ISNULL(lastDetail.SumGross,0) as LastGross");
            sb.Append(" ,ISNULL(bid.Bala,0) - ISNULL(lastDetail.SumBala,0) as LastBala ");

            sb.Append(" ,ISNULL(curDetail.SumAmount,ISNULL(bid.NetAmount,0) - ISNULL(lastDetail.SumAmount,0)) as NetAmount ");
            //sb.Append(",ISNULL(curDetail.SumGross,ISNULL(bid.IntegerAmount,0) - ISNULL(lastDetail.SumGross,0)) as IntegerAmount");
            sb.Append(" ,cast(ISNULL(curDetail.SumBala, (isnull(bid.UnitPrice,0) - isnull(curDetail.UnitPrice,pcd.SettlePrice)) * ISNULL(curDetail.SumAmount,ISNULL(bid.NetAmount,0) - ISNULL(lastDetail.SumAmount,0))) as decimal(18,2)) as Bala ");
            sb.Append(" ,isnull(curDetail.UnitPrice,pcd.SettlePrice) as UnitPrice ");
            sb.Append(" ,isnull(bid.UnitPrice,0) - isnull(curDetail.UnitPrice,pcd.SettlePrice) as GapPrice ");
            sb.Append(" ,isnull(bid.UnitPrice,0) as ProPrice ");

            sb.Append(" ,pcd.PriceConfirmId as ConfirmPriceId,pcd.DetailId as ConfirmDetailId,inv.InvoiceDate,sl.CardNo");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Inv_BusinessInvoiceDetail bid ");
            sb.Append(" inner join dbo.Inv_BusinessInvoice bi on bi.BusinessInvoiceId = bid.BusinessInvoiceId ");
            sb.AppendFormat(" inner join dbo.Invoice inv on bi.InvoiceId = inv.InvoiceId and inv.InvoiceStatus>={0} and inv.InvoiceType = {1} ", readyStatus, proInvoiceType);
            sb.AppendFormat(" inner join dbo.St_Stock sto on bid.StockId = sto.StockId ");
            sb.Append(" inner join dbo.St_StockLog sl on bid.StockLogId = sl.StockLogId ");
            sb.Append(" inner join dbo.St_StockName sn on sn.StockNameId = sto.StockNameId ");

            sb.AppendFormat(" inner join dbo.Pri_PriceConfirmDetail pcd on pcd.StockLogId = sl.StockLogId and pcd.DetailStatus>={0} ", readyStatus);

            sb.Append(" left join NFMT_User.dbo.Corporation corp on corp.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = inv.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId ={0} ", stockStatusType);
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");

            sb.Append(" left join (select sum(isnull(bid.NetAmount,0)) as SumAmount,SUM(ISNULL(bid.IntegerAmount,0)) as SumGross,sum(isnull(bid.Bala,0)) as SumBala,bid.RefDetailId ");
            sb.AppendFormat(" from dbo.Inv_BusinessInvoiceDetail bid where bid.BusinessInvoiceId != {0} and bid.DetailStatus >=50 group by bid.RefDetailId) lastDetail ", replaceInvoiceId);
            sb.Append(" on lastDetail.RefDetailId = bid.DetailId ");

            sb.Append(" left join (select sum(isnull(bid.NetAmount,0)) as SumAmount,SUM(ISNULL(bid.IntegerAmount,0)) as SumGross,sum(isnull(bid.Bala,0)) as SumBala,avg(bid.UnitPrice) as UnitPrice,bid.RefDetailId ");
            sb.AppendFormat(" from dbo.Inv_BusinessInvoiceDetail bid where bid.BusinessInvoiceId = {0} and bid.DetailStatus >=50 group by bid.RefDetailId) curDetail ", replaceInvoiceId);
            sb.Append(" on curDetail.RefDetailId = bid.DetailId ");

            select.TableName = sb.ToString();
            sb.Clear();

            sb.AppendFormat(" bid.DetailStatus >= {0} ", readyStatus);
            sb.AppendFormat(" and bid.BusinessInvoiceId = {0} ", provisionalInvoiceId);
            sb.Append(" and ISNULL(bid.NetAmount,0) - ISNULL(lastDetail.SumAmount,0) >0 ");

            if (isReplace)
                sb.Append(" and curDetail.RefDetailId is not null ");
            else
                sb.Append(" and curDetail.RefDetailId is null ");
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetSuppleFinalByFinalStockListSelect(int pageIndex, int pageSize, string orderStr, int finalInvoiceId, int suppleInvoiceId = 0, bool isUpdate = false)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "fid.DetailId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)StatusEnum.已生效;
            int stockStatusType = (int)StatusTypeEnum.库存状态;
            int suppleInvoiceType = (int)InvoiceTypeEnum.补临终票;

            StringBuilder sb = new StringBuilder();
            sb.Append(" fid.DetailId as RefDetailId,inv.InvoiceId,fi.BusinessInvoiceId,cs.SubId,sto.StockId,sl.StockLogId,sn.StockNameId,mu.MUName ");
            sb.Append(" ,sto.StockDate,sn.RefNo,corp.CorpName,ass.AssetName,bra.BrandName,sto.StockStatus,ss.StatusName as StockStatusName");
            sb.Append(" ,srd.QtyMiss,isnull(curDetail.NetAmount,srd.QtyMiss) as NetAmount,isnull(curDetail.NetAmount,srd.QtyMiss) as IntegerAmount");
            sb.Append(" ,ISNULL(curDetail.Bala,0) as Bala ");
            sb.Append(",sto.CardNo,dp.DPName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Inv_BusinessInvoiceDetail fid ");
            sb.Append(" inner join dbo.Inv_BusinessInvoice fi on fid.BusinessInvoiceId = fi.BusinessInvoiceId ");
            sb.AppendFormat(" inner join dbo.Invoice inv on fi.InvoiceId = inv.InvoiceId and inv.InvoiceStatus >= {0} ", readyStatus);
            sb.Append(" inner join dbo.Con_ContractSub cs on fi.SubContractId = cs.SubId ");
            sb.Append(" inner join dbo.St_StockLog sl on sl.StockLogId  = fid.StockLogId ");
            sb.Append(" inner join dbo.St_Stock sto on sl.StockId = sto.StockId and fid.StockId = sto.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sn.StockNameId = sto.StockNameId ");
            sb.Append(" left join NFMT_User.dbo.Corporation corp on corp.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId ={0} ", stockStatusType);
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");

            sb.AppendFormat(" left join (select srd.StockLogId,SUM(srd.QtyMiss) as QtyMiss from dbo.St_StockReceiptDetail srd where srd.DetailStatus>={0} ", readyStatus);
            sb.Append(" group by srd.StockLogId) srd on fid.StockLogId = srd.StockLogId ");

            sb.Append(" left join (select SUM(bid.Bala) as Bala,SUM(bid.NetAmount) as NetAmount, bid.RefDetailId ");
            sb.Append(" from dbo.Inv_BusinessInvoiceDetail bid inner join dbo.Invoice inv on inv.InvoiceId= bid.InvoiceId ");
            sb.AppendFormat(" where bid.DetailStatus >={0} and inv.InvoiceType ={1} and bid.BusinessInvoiceId ={2} group by bid.RefDetailId) curDetail on curDetail.RefDetailId = fid.DetailId ", readyStatus, suppleInvoiceType, suppleInvoiceId);

            if (suppleInvoiceId == 0)
            {
                sb.Append(" left join (select SUM(bid.Bala) as Bala,SUM(bid.NetAmount) as NetAmount, bid.RefDetailId ");
                sb.Append(" from dbo.Inv_BusinessInvoiceDetail bid inner join dbo.Invoice inv on inv.InvoiceId= bid.InvoiceId ");
                sb.AppendFormat(" where bid.DetailStatus >={0} and inv.InvoiceType ={1} and bid.BusinessInvoiceId !={2} group by bid.RefDetailId) othDetail on othDetail.RefDetailId = fid.DetailId ", readyStatus, suppleInvoiceType, suppleInvoiceId);
            }

            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" fi.BusinessInvoiceId = {0} and fid.DetailStatus>={1}", finalInvoiceId, readyStatus);
            sb.Append(" and srd.QtyMiss != 0 and isnull(curDetail.NetAmount,srd.QtyMiss) != 0 ");

            if (suppleInvoiceId > 0)
            {
                if (isUpdate)
                    sb.Append(" and curDetail.RefDetailId is not null ");
                else
                    sb.Append(" and curDetail.RefDetailId is null ");
            }
            else if (suppleInvoiceId == 0)
                sb.Append(" and othDetail.RefDetailId is null ");

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion

        #region 迈科资产发票

        public SelectModel GetDirectStocksModel(int pageIndex, int pageSize, string orderStr,int subId,bool isInvoice, int directInvoiceId = 0)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pcd.DetailId desc";
            else
                select.OrderStr = orderStr;

            int entryStatus = (int)StatusEnum.已录入;
            int readyStatus = (int)StatusEnum.已生效;
            int stockStatusType =(int)StatusTypeEnum.库存状态;
            int directFinalType = (int)InvoiceTypeEnum.DirectFinalInvoice;
            int replaceFinalType = (int)InvoiceTypeEnum.ReplaceFinalInvoice;

            StringBuilder sb = new StringBuilder();
            sb.Append("pcd.DetailId as ConfirmDetailId, pcd.PriceConfirmId  as ConfirmPriceId");
            sb.Append(",sl.StockLogId,sto.StockId,sn.StockNameId,sto.StockDate,sn.RefNo,sto.CorpId,cor.CorpName");
            sb.Append(",sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,sto.StockStatus,ss.StatusName as StockStatusName");
            //sb.Append(",pcd.ConfirmAmount - ISNULL(othDetail.NetAmount,0) as LastAmount");
            //sb.Append(",ISNULL(curDetail.NetAmount,pcd.ConfirmAmount - ISNULL(othDetail.NetAmount,0)) as NetAmount");
            sb.Append(",sto.CurNetAmount - ISNULL(othDetail.NetAmount,0) as LastAmount");
            sb.Append(",ISNULL(curDetail.NetAmount,sto.CurNetAmount - ISNULL(othDetail.NetAmount,0)) as NetAmount");
            sb.Append(",pcd.SettlePrice as UnitPrice");
            sb.Append(",cast(ISNULL(curDetail.NetAmount,sto.CurNetAmount - ISNULL(othDetail.NetAmount,0)) * pcd.SettlePrice as decimal(18,4)) as Bala");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_PriceConfirmDetail pcd ");
            sb.AppendFormat(" inner join dbo.Pri_PriceConfirm pc on pcd.PriceConfirmId = pc.PriceConfirmId and pc.PriceConfirmStatus>= {0} and pc.SubId ={1} ",readyStatus,subId);
            sb.AppendFormat(" inner join dbo.St_StockLog sl on pcd.StockLogId = sl.StockLogId and sl.LogStatus>= {0} and sl.SubContractId = {1} ", readyStatus, subId);
            sb.Append(" inner join dbo.St_Stock sto on sl.StockId = sto.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId ={0} ",stockStatusType);

            sb.Append(" left join ( ");
            sb.Append(" select SUM(isnull(bid.NetAmount,0)) as NetAmount,SUM(isnull(bid.Bala,0)) as InvoiceBala ,bid.ConfirmDetailId ");
            sb.Append(" from dbo.Inv_BusinessInvoiceDetail bid  inner join dbo.Inv_BusinessInvoice bi on bid.BusinessInvoiceId = bi.BusinessInvoiceId ");
            sb.AppendFormat(" and bi.SubContractId={0} inner join dbo.Invoice inv on inv.InvoiceId = bi.InvoiceId and inv.InvoiceStatus>={1} ",subId,entryStatus);
            sb.AppendFormat(" where bid.BusinessInvoiceId ={0} and bid.DetailStatus>={1} and inv.InvoiceType in ({2},{3})  group by bid.ConfirmDetailId ",directInvoiceId,readyStatus,directFinalType,replaceFinalType);
            sb.Append(" ) as curDetail on curDetail.ConfirmDetailId = pcd.DetailId ");

            sb.Append(" left join ( ");
            sb.Append(" select SUM(isnull(bid.NetAmount,0)) as NetAmount,SUM(isnull(bid.Bala,0)) as InvoiceBala ,bid.ConfirmDetailId ");
            sb.Append(" from dbo.Inv_BusinessInvoiceDetail bid inner join dbo.Inv_BusinessInvoice bi on bid.BusinessInvoiceId = bi.BusinessInvoiceId ");
            sb.AppendFormat(" and bi.SubContractId={0} inner join dbo.Invoice inv on inv.InvoiceId = bi.InvoiceId and inv.InvoiceStatus>={1} ",subId,entryStatus);
            sb.AppendFormat(" where bid.BusinessInvoiceId !={0} and bid.DetailStatus>={1} and inv.InvoiceType in ({2},{3})  group by bid.ConfirmDetailId ",directInvoiceId,readyStatus,directFinalType,replaceFinalType);
            sb.Append(" ) as othDetail on othDetail.ConfirmDetailId = pcd.DetailId ");

            select.TableName = sb.ToString();
            sb.Clear();

            sb.AppendFormat(" pcd.DetailStatus>={0} and pc.SubId = {1}",readyStatus,subId);
            sb.Append(" and pcd.ConfirmAmount - ISNULL(othDetail.NetAmount,0) > 0 ");

            if(isInvoice)
                sb.Append(" and curDetail.ConfirmDetailId is not null ");
            else
                sb.Append(" and curDetail.ConfirmDetailId is null ");

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion

        public ResultModel CreateBusinessInvoice(UserModel user, Operate.Model.Invoice invoice, BusinessInvoice invoiceBusiness, List<BusinessInvoiceDetail> details, InvoiceTypeEnum invoiceType)
        {
            ResultModel result = new ResultModel();

            try
            {
                BusinessInvoiceDAL businessInvoiceDAL = new BusinessInvoiceDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();
                BusinessInvoiceDetailDAL detailDAL = new BusinessInvoiceDetailDAL();
                ContractSubDAL subDAL = new ContractSubDAL();
                StockReceiptDetailDAL stockReceiptDetailDAL = new StockReceiptDetailDAL();
                SubDetailDAL subDetailDAL = new SubDetailDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证子合约
                    result = subDAL.Get(user, invoiceBusiness.SubContractId);
                    if (result.ResultStatus != 0)
                        return result;

                    ContractSub sub = result.ReturnValue as ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约不存在";
                        return result;
                    }

                    if (sub.SubStatus != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约非已生效状态，不能新增临票";
                        return result;
                    }

                    //获取子合约明细
                    result = subDetailDAL.GetDetailBySubId(user, sub.SubId);
                    if (result.ResultStatus != 0)
                        return result;

                    SubDetail subDetail = result.ReturnValue as SubDetail;
                    if (subDetail == null || subDetail.SubDetailId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约明细获取失败";
                        return result;
                    }

                    //获取子合约下所有
                    result = this.businessinvoiceDAL.LoadBySubId(user, invoiceBusiness.SubContractId, invoiceType);
                    if (result.ResultStatus != 0)
                        return result;

                    List<BusinessInvoice> bis = result.ReturnValue as List<BusinessInvoice>;
                    decimal sumNetAmount = bis.Sum(temp => temp.NetAmount);
                    decimal maxNetAmount = sub.SignAmount * (1 + subDetail.MoreOrLess);
                    //if (maxNetAmount < sumNetAmount + invoiceBusiness.NetAmount)
                    //{
                    //    result.ResultStatus = -1;
                    //    result.Message = "开票重量超额";
                    //    return result;
                    //}

                    //新增发票主表
                    int invoiceId = 0;
                    invoice.InvoiceType = (int)invoiceType;
                    invoice.InvoiceStatus = StatusEnum.已录入;
                    Corporation outCorp = UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == invoice.OutCorpId);
                    if (outCorp == null || outCorp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "开票公司信息错误";
                        return result;
                    }
                    invoice.OutCorpName = outCorp.CorpName;

                    Corporation inCorp = UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == invoice.InCorpId);
                    if (inCorp == null || inCorp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收票公司信息错误";
                        return result;
                    }
                    invoice.InCorpName = inCorp.CorpName;

                    result = invoiceDAL.Insert(user, invoice);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out invoiceId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票添加失败";
                        return result;
                    }

                    //新增业务发票表
                    int businessInvoiceId = 0;
                    invoiceBusiness.InvoiceId = invoiceId;
                    result = businessInvoiceDAL.Insert(user, invoiceBusiness);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out businessInvoiceId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "新增发票失败";
                        return result;
                    }

                    //新增业务发票明细
                    StockLogDAL stockLogDAL = new StockLogDAL();
                    StockDAL stockDAL = new StockDAL();
                    foreach (BusinessInvoiceDetail detail in details)
                    {
                        if (detail.NetAmount != 0 && detail.StockId > 0)
                        {
                            //验证库存
                            result = stockDAL.Get(user, detail.StockId);
                            if (result.ResultStatus != 0)
                                return result;
                            Stock stock = result.ReturnValue as Stock;
                            if (stock == null || stock.StockId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "发票库存获取失败";
                                return result;
                            }

                            //验证库存流水
                            result = stockLogDAL.Get(user, detail.StockLogId);
                            if (result.ResultStatus != 0)
                                return result;

                            StockLog stockLog = result.ReturnValue as StockLog;
                            if (stockLog == null || stockLog.StockLogId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "新增发票流水获取失败";
                                return result;
                            }

                            //基本验证
                            if (stockLog.StockId != stock.StockId)
                            {
                                result.ResultStatus = -1;
                                result.Message = "库存与库存流水不匹配";
                                return result;
                            }

                            //验证流水合约与发票合约是否相同
                            if (stockLog.SubContractId != invoiceBusiness.SubContractId)
                            {
                                result.ResultStatus = -1;
                                result.Message = "合约与库存流水不匹配";
                                return result;
                            }

                            detail.DetailStatus = StatusEnum.已生效;
                            detail.InvoiceId = invoiceId;
                            detail.BusinessInvoiceId = businessInvoiceId;
                            detail.StockId = stockLog.StockId;
                            detail.StockLogId = stockLog.StockLogId;

                            if (invoice.InvoiceType == (int)InvoiceTypeEnum.SuppleFinalInvoice)
                            {
                                //获取当前流水回执信息
                                result = stockReceiptDetailDAL.LoadByStockLogId(user, stockLog.StockLogId);
                                List<StockReceiptDetail> stockReceiptDetails = result.ReturnValue as List<StockReceiptDetail>;
                                if (stockReceiptDetails == null)
                                {
                                    result.ResultStatus = -1;
                                    result.Message = "当前流水未回执，不能补零";
                                    return result;
                                }

                                decimal sumQtyMiss = stockReceiptDetails.Sum(temp => temp.QtyMiss);
                                detail.IntegerAmount = sumQtyMiss;
                                detail.NetAmount = sumQtyMiss;
                            }
                            //else
                            //{
                            //    detail.IntegerAmount = stockLog.GrossAmount;
                            //    detail.NetAmount = stockLog.NetAmount;
                            //}
                            result = detailDAL.Insert(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    invoice.InvoiceId = invoiceId;
                    if (result.ResultStatus == 0) result.ReturnValue = invoice;

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

        public ResultModel CreateDirect(UserModel user, Operate.Model.Invoice invoice, BusinessInvoice invoiceBusiness, List<BusinessInvoiceDetail> details)
        {
            ResultModel result = new ResultModel();

            ContractSubDAL subDAL = new ContractSubDAL();
            result = subDAL.Get(user, invoiceBusiness.SubContractId);
            if(result.ResultStatus!=0)
                return result;

            ContractSub sub = result.ReturnValue as ContractSub;
            if (sub == null || sub.SubId <= 0)
            {
                result.ResultStatus = -1;
                result.Message = "子合约不存在";
                return result;
            }

            StockLogDAL stockLogDAL = new StockLogDAL();
            //明细表数据：毛重、金额计算
            foreach (BusinessInvoiceDetail detail in details)
            {
                detail.Bala = Math.Round(detail.NetAmount * detail.UnitPrice, 2, MidpointRounding.AwayFromZero);                

                if (sub.TradeBorder == (int)TradeBorderEnum.内贸)
                {
                    //内贸毛重==净重
                    detail.IntegerAmount = detail.NetAmount;
                }
                else 
                {
                    //整笔开票情况下，外贸毛重等于库存流水毛重；分笔开票情况下，等于净重。
                    result = stockLogDAL.Get(user, detail.StockLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    StockLog stockLog = result.ReturnValue as StockLog;
                    if (stockLog == null || stockLog.StockLogId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "当前合约不存在该笔库存";
                        return result;
                    }

                    if (stockLog.NetAmount == detail.NetAmount)
                    {
                        detail.IntegerAmount = stockLog.GrossAmount;
                    }
                    else 
                    {
                        detail.IntegerAmount = detail.NetAmount;
                    }
                }
            }

            //主表数据：毛重、净重、均价、总额计算
            decimal sumGrossAmount = details.Sum(temp => temp.IntegerAmount);
            invoiceBusiness.IntegerAmount = sumGrossAmount;

            decimal sumNetAmount = details.Sum(temp => temp.NetAmount);
            if (sumNetAmount != invoiceBusiness.NetAmount)
            {
                result.ResultStatus = -1;
                result.Message = "明细净重之和与总净重不相等";
                return result;
            }

            decimal sumBala = details.Sum(temp => temp.Bala);
            if (Math.Abs(sumBala - invoice.InvoiceBala) >= 1)
            {
                result.ResultStatus = -1;
                result.Message = "明细金额之和与总金额不相等";
                return result;
            }

            decimal avgPrice = Math.Round(sumBala / sumNetAmount, 4, MidpointRounding.AwayFromZero);
            if (avgPrice != invoiceBusiness.UnitPrice)
            {
                result.ResultStatus = -1;
                result.Message = "计算均价错误";
                return result; 
            }

            //验证是否已开临票
            //result = this.businessinvoiceDAL.LoadBySubId(user, invoiceBusiness.SubContractId, InvoiceTypeEnum.ProvisionalInvoice);
            //if (result.ResultStatus != 0)
            //    return result;

            //List<Model.BusinessInvoice> proInvoices = result.ReturnValue as List<Model.BusinessInvoice>;
            //if (proInvoices == null)
            //{
            //    result.ResultStatus = -1;
            //    result.Message = "验证是否拥有临票失败";
            //    return result;
            //}
            //if (proInvoices.Count > 0)
            //{
            //    result.ResultStatus = -1;
            //    result.Message = "当前合约已拥有临票，直接终票失败";
            //    return result;
            //}

            result = this.CreateBusinessInvoice(user, invoice, invoiceBusiness, details, InvoiceTypeEnum.DirectFinalInvoice);

            return result;
        }

        public ResultModel CreateProvisional(UserModel user, Operate.Model.Invoice invoice, BusinessInvoice invoiceBusiness, List<BusinessInvoiceDetail> details)
        {
            ResultModel result = new ResultModel();

            StockLogDAL stockLogDAL = new StockLogDAL();

            foreach (BusinessInvoiceDetail detail in details)
            {
                detail.UnitPrice = invoiceBusiness.UnitPrice;
                result = stockLogDAL.Get(user, detail.StockLogId);
                if (result.ResultStatus != 0)
                    return result;

                StockLog stockLog = result.ReturnValue as StockLog;
                if (stockLog == null || stockLog.StockLogId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "当前合约不存在该笔库存";
                    return result;
                }
                detail.IntegerAmount = stockLog.GrossAmount;
            }

            invoiceBusiness.IntegerAmount = details.Sum(temp => temp.IntegerAmount);

            //验证是否已开直接终票
            result = this.businessinvoiceDAL.LoadBySubId(user, invoiceBusiness.SubContractId, InvoiceTypeEnum.DirectFinalInvoice);
            if (result.ResultStatus != 0)
                return result;

            List<BusinessInvoice> dirInvoices = result.ReturnValue as List<BusinessInvoice>;
            if (dirInvoices == null)
            {
                result.ResultStatus = -1;
                result.Message = "验证是否拥有直接终票失败";
                return result;
            }
            if (dirInvoices.Count > 0)
            {
                result.ResultStatus = -1;
                result.Message = "当前合约已拥有直接终票，临票失败";
                return result;
            }

            result = this.CreateBusinessInvoice(user, invoice, invoiceBusiness, details, InvoiceTypeEnum.ProvisionalInvoice);

            return result;
        }

        public ResultModel CreateReplaceFinal(UserModel user, Operate.Model.Invoice invoice, BusinessInvoice invoiceBusiness, List<BusinessInvoiceDetail> details)
        {
            ResultModel result = new ResultModel();

            StockLogDAL stockLogDAL = new StockLogDAL();

            foreach (BusinessInvoiceDetail detail in details)
            {
                result = stockLogDAL.Get(user, detail.StockLogId);
                if (result.ResultStatus != 0)
                    return result;

                StockLog stockLog = result.ReturnValue as StockLog;
                if (stockLog == null || stockLog.StockLogId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "当前合约不存在该笔库存";
                    return result;
                }
                detail.IntegerAmount = stockLog.GrossAmount;
            }

            decimal sumNetAmount = details.Sum(temp => temp.NetAmount);
            if (sumNetAmount != invoiceBusiness.NetAmount)
            {
                result.ResultStatus = -1;
                result.Message = "明细净重之和与总净重不相等";
                return result;
            }

            decimal sumBala = details.Sum(temp => temp.Bala);
            if (sumBala != invoice.InvoiceBala)
            {
                result.ResultStatus = -1;
                result.Message = "明细金额之和与总金额不相等";
                return result;
            }

            decimal sumUnitBala = Math.Round(details.Sum(temp => temp.UnitPrice * temp.NetAmount),2,MidpointRounding.AwayFromZero);
            decimal avgPrice = Math.Round(sumUnitBala / sumNetAmount, 4, MidpointRounding.AwayFromZero);

            invoiceBusiness.UnitPrice = avgPrice;
            invoiceBusiness.IntegerAmount = details.Sum(temp => temp.IntegerAmount);

            result = this.CreateBusinessInvoice(user, invoice, invoiceBusiness, details, InvoiceTypeEnum.ReplaceFinalInvoice);

            return result;
        }

        public ResultModel CreateSuppleFinal(UserModel user, Operate.Model.Invoice invoice, BusinessInvoice invoiceBusiness, List<BusinessInvoiceDetail> details)
        {
            return this.CreateBusinessInvoice(user, invoice, invoiceBusiness, details, InvoiceTypeEnum.SuppleFinalInvoice);
        }

        public ResultModel UpdateBusinessInvoice(UserModel user, Operate.Model.Invoice invoice, BusinessInvoice invoiceBusiness, List<BusinessInvoiceDetail> details)
        {
            ResultModel result = new ResultModel();

            try
            {
                BusinessInvoiceDAL businessInvoiceDAL = new BusinessInvoiceDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();
                BusinessInvoiceDetailDAL detailDAL = new BusinessInvoiceDetailDAL();
                StockReceiptDetailDAL stockReceiptDetailDAL = new StockReceiptDetailDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证发票
                    if (invoiceBusiness.BusinessInvoiceId <= 0)
                    {
                        result.Message = "发票不存在";
                        return result;
                    }

                    result = businessinvoiceDAL.Get(user, invoiceBusiness.BusinessInvoiceId);
                    if (result.ResultStatus != 0)
                        return result;

                    BusinessInvoice resultBusinessInvoice = result.ReturnValue as BusinessInvoice;
                    if (resultBusinessInvoice == null || resultBusinessInvoice.BusinessInvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票不存在";
                        return result;
                    }

                    //更新发票
                    resultBusinessInvoice.IntegerAmount = invoiceBusiness.IntegerAmount;
                    resultBusinessInvoice.NetAmount = invoiceBusiness.NetAmount;
                    resultBusinessInvoice.VATRatio = invoiceBusiness.VATRatio;
                    resultBusinessInvoice.VATBala = invoiceBusiness.VATBala;
                    resultBusinessInvoice.UnitPrice = invoiceBusiness.UnitPrice;

                    resultBusinessInvoice.Status = StatusEnum.已录入;

                    result = businessinvoiceDAL.Update(user, resultBusinessInvoice);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取发票主表
                    result = invoiceDAL.Get(user, resultBusinessInvoice.InvoiceId);
                    if (result.ResultStatus != 0)
                        return result;

                    Operate.Model.Invoice resultInvoice = result.ReturnValue as Operate.Model.Invoice;
                    if (resultInvoice == null || resultInvoice.InvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票不存在";
                        return result;
                    }

                    //更新发票
                    resultInvoice.InvoiceDate = invoice.InvoiceDate;
                    resultInvoice.InvoiceName = invoice.InvoiceName;
                    resultInvoice.InvoiceBala = invoice.InvoiceBala;
                    resultInvoice.OutCorpId = invoice.OutCorpId;
                    resultInvoice.InCorpId = invoice.InCorpId;
                    resultInvoice.Memo = invoice.Memo;

                    result = invoiceDAL.Update(user, resultInvoice);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取发票明细
                    result = detailDAL.Load(user, resultBusinessInvoice.BusinessInvoiceId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<BusinessInvoiceDetail> resultDetails = result.ReturnValue as List<BusinessInvoiceDetail>;
                    if (resultDetails == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取发票明细失败";
                        return result;
                    }

                    //作废发票明细
                    foreach (BusinessInvoiceDetail detail in resultDetails)
                    {
                        if (detail.DetailStatus == StatusEnum.已生效)
                            detail.DetailStatus = StatusEnum.已录入;

                        result = detailDAL.Invalid(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //新增发票明细
                    StockLogDAL stockLogDAL = new StockLogDAL();
                    StockDAL stockDAL = new StockDAL();
                    foreach (BusinessInvoiceDetail detail in details)
                    {
                        if (detail.NetAmount != 0 && detail.StockId > 0)
                        {
                            //验证库存
                            result = stockDAL.Get(user, detail.StockId);
                            if (result.ResultStatus != 0)
                                return result;
                            Stock stock = result.ReturnValue as Stock;
                            if (stock == null || stock.StockId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "发票库存获取失败";
                                return result;
                            }

                            //验证库存流水
                            result = stockLogDAL.Get(user, detail.StockLogId);
                            if (result.ResultStatus != 0)
                                return result;

                            StockLog stockLog = result.ReturnValue as StockLog;
                            if (stockLog == null || stockLog.StockLogId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "新增发票流水获取失败";
                                return result;
                            }

                            //基本验证
                            if (stockLog.StockId != stock.StockId)
                            {
                                result.ResultStatus = -1;
                                result.Message = "库存与库存流水不匹配";
                                return result;
                            }

                            //验证流水合约与发票合约是否相同
                            if (stockLog.SubContractId != resultBusinessInvoice.SubContractId)
                            {
                                result.ResultStatus = -1;
                                result.Message = "合约与库存流水不匹配";
                                return result;
                            }

                            detail.DetailStatus = StatusEnum.已生效;
                            detail.InvoiceId = resultInvoice.InvoiceId;
                            detail.BusinessInvoiceId = resultBusinessInvoice.BusinessInvoiceId;
                            detail.StockId = stockLog.StockId;
                            detail.StockLogId = stockLog.StockLogId;

                            if (resultInvoice.InvoiceType == (int)InvoiceTypeEnum.SuppleFinalInvoice)
                            {
                                //获取当前流水回执信息
                                result = stockReceiptDetailDAL.LoadByStockLogId(user, stockLog.StockLogId);
                                List<StockReceiptDetail> stockReceiptDetails = result.ReturnValue as List<StockReceiptDetail>;
                                if (stockReceiptDetails == null)
                                {
                                    result.ResultStatus = -1;
                                    result.Message = "当前流水未回执，不能补零";
                                    return result;
                                }

                                decimal sumQtyMiss = stockReceiptDetails.Sum(temp => temp.QtyMiss);
                                detail.IntegerAmount = sumQtyMiss;
                                detail.NetAmount = sumQtyMiss;
                            }
                            else
                            {
                                detail.IntegerAmount = stockLog.GrossAmount;
                                detail.NetAmount = stockLog.NetAmount;
                            }

                            result = detailDAL.Insert(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = resultInvoice;
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

        public ResultModel UpdateDirect(UserModel user, Operate.Model.Invoice invoice, BusinessInvoice invoiceBusiness, List<BusinessInvoiceDetail> details)
        {
            ResultModel result = new ResultModel();
            ContractSubDAL subDAL = new ContractSubDAL();
            result = subDAL.Get(user, invoiceBusiness.SubContractId);
            if (result.ResultStatus != 0)
                return result;

            ContractSub sub = result.ReturnValue as ContractSub;
            if (sub == null || sub.SubId <= 0)
            {
                result.ResultStatus = -1;
                result.Message = "子合约不存在";
                return result;
            }

            StockLogDAL stockLogDAL = new StockLogDAL();
            //明细表数据：毛重、金额计算
            foreach (BusinessInvoiceDetail detail in details)
            {
                detail.Bala = Math.Round(detail.NetAmount * detail.UnitPrice, 2, MidpointRounding.AwayFromZero);

                if (sub.TradeBorder == (int)TradeBorderEnum.内贸)
                {
                    //内贸毛重==净重
                    detail.IntegerAmount = detail.NetAmount;
                }
                else
                {
                    //整笔开票情况下，外贸毛重等于库存流水毛重；分笔开票情况下，等于净重。
                    result = stockLogDAL.Get(user, detail.StockLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    StockLog stockLog = result.ReturnValue as StockLog;
                    if (stockLog == null || stockLog.StockLogId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "当前合约不存在该笔库存";
                        return result;
                    }

                    if (stockLog.NetAmount == detail.NetAmount)
                    {
                        detail.IntegerAmount = stockLog.GrossAmount;
                    }
                    else
                    {
                        detail.IntegerAmount = detail.NetAmount;
                    }
                }
            }

            //主表数据：毛重、净重、均价、总额计算
            decimal sumGrossAmount = details.Sum(temp => temp.IntegerAmount);
            invoiceBusiness.IntegerAmount = sumGrossAmount;

            decimal sumNetAmount = details.Sum(temp => temp.NetAmount);
            if (sumNetAmount != invoiceBusiness.NetAmount)
            {
                result.ResultStatus = -1;
                result.Message = "明细净重之和与总净重不相等";
                return result;
            }

            decimal sumBala = details.Sum(temp => temp.Bala);
            if (Math.Abs(sumBala - invoice.InvoiceBala) >=1)
            {
                result.ResultStatus = -1;
                result.Message = "明细金额之和与总金额不相等";
                return result;
            }

            decimal avgPrice = Math.Round(sumBala / sumNetAmount, 4, MidpointRounding.AwayFromZero);
            if (avgPrice != invoiceBusiness.UnitPrice)
            {
                result.ResultStatus = -1;
                result.Message = "计算均价错误";
                return result;
            }

            return this.UpdateBusinessInvoice(user, invoice, invoiceBusiness, details);
            //ResultModel result = new ResultModel();

            //try
            //{
            //    DAL.BusinessInvoiceDAL businessInvoiceDAL = new BusinessInvoiceDAL();
            //    NFMT.Operate.DAL.InvoiceDAL invoiceDAL = new Operate.DAL.InvoiceDAL();
            //    DAL.BusinessInvoiceDetailDAL detailDAL = new BusinessInvoiceDetailDAL();

            //    using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            //    {
            //        //验证发票
            //        if (invoiceBusiness.BusinessInvoiceId <= 0)
            //        {
            //            result.Message = "直发票不存在";
            //            return result;
            //        }

            //        result = businessinvoiceDAL.Get(user, invoiceBusiness.BusinessInvoiceId);
            //        if (result.ResultStatus != 0)
            //            return result;

            //        NFMT.Invoice.Model.BusinessInvoice resultBusinessInvoice = result.ReturnValue as NFMT.Invoice.Model.BusinessInvoice;
            //        if (resultBusinessInvoice == null || resultBusinessInvoice.BusinessInvoiceId <= 0)
            //        {
            //            result.ResultStatus = -1;
            //            result.Message = "发票不存在";
            //            return result;
            //        }

            //        //更新发票
            //        resultBusinessInvoice.IntegerAmount = invoiceBusiness.IntegerAmount;
            //        resultBusinessInvoice.NetAmount = invoiceBusiness.NetAmount;
            //        resultBusinessInvoice.VATRatio = invoiceBusiness.VATRatio;
            //        resultBusinessInvoice.VATBala = invoiceBusiness.VATBala;
            //        resultBusinessInvoice.UnitPrice = invoiceBusiness.UnitPrice;

            //        resultBusinessInvoice.Status = StatusEnum.已录入;

            //        result = businessinvoiceDAL.Update(user, resultBusinessInvoice);
            //        if (result.ResultStatus != 0)
            //            return result;

            //        //获取发票主表
            //        result = invoiceDAL.Get(user, resultBusinessInvoice.InvoiceId);
            //        if (result.ResultStatus != 0)
            //            return result;

            //        NFMT.Operate.Model.Invoice resultInvoice = result.ReturnValue as NFMT.Operate.Model.Invoice;
            //        if (resultInvoice == null || resultInvoice.InvoiceId <= 0)
            //        {
            //            result.ResultStatus = -1;
            //            result.Message = "发票不存在";
            //            return result;
            //        }

            //        //更新发票
            //        resultInvoice.InvoiceDate = invoice.InvoiceDate;
            //        resultInvoice.InvoiceName = invoice.InvoiceName;
            //        resultInvoice.InvoiceBala = invoice.InvoiceBala;
            //        resultInvoice.OutCorpId = invoice.OutCorpId;
            //        resultInvoice.InCorpId = invoice.InCorpId;
            //        resultInvoice.Memo = invoice.Memo;

            //        result = invoiceDAL.Update(user, resultInvoice);
            //        if (result.ResultStatus != 0)
            //            return result;

            //        //获取发票明细
            //        result = detailDAL.Load(user, resultBusinessInvoice.BusinessInvoiceId);
            //        if (result.ResultStatus != 0)
            //            return result;

            //        List<Model.BusinessInvoiceDetail> resultDetails = result.ReturnValue as List<Model.BusinessInvoiceDetail>;
            //        if (resultDetails == null)
            //        {
            //            result.ResultStatus = -1;
            //            result.Message = "获取发票明细失败";
            //            return result;
            //        }

            //        //作废发票明细
            //        foreach (Model.BusinessInvoiceDetail detail in resultDetails)
            //        {
            //            if (detail.DetailStatus == StatusEnum.已生效)
            //                detail.DetailStatus = StatusEnum.已录入;

            //            result = detailDAL.Invalid(user, detail);
            //            if (result.ResultStatus != 0)
            //                return result;
            //        }

            //        //新增发票明细
            //        foreach (Model.BusinessInvoiceDetail detail in details)
            //        {
            //            if (detail.NetAmount > 0 && detail.Bala > 0 && detail.StockId > 0)
            //            {
            //                detail.DetailStatus = StatusEnum.已生效;
            //                detail.InvoiceId = invoice.InvoiceId;
            //                detail.BusinessInvoiceId = invoiceBusiness.BusinessInvoiceId;
            //                result = detailDAL.Insert(user, detail);
            //                if (result.ResultStatus != 0)
            //                    return result;
            //            }
            //        }

            //        scope.Complete();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    result.ResultStatus = -1;
            //    result.Message = ex.Message;
            //}

            //return result;
        }

        public ResultModel UpdateProvisional(UserModel user, Operate.Model.Invoice invoice, BusinessInvoice provisionalInvoice, List<BusinessInvoiceDetail> details)
        {
            ResultModel result = new ResultModel();

            StockLogDAL stockLogDAL = new StockLogDAL();

            foreach (BusinessInvoiceDetail detail in details)
            {
                detail.UnitPrice = provisionalInvoice.UnitPrice;
                result = stockLogDAL.Get(user, detail.StockLogId);
                if (result.ResultStatus != 0)
                    return result;

                StockLog stockLog = result.ReturnValue as StockLog;
                if (stockLog == null || stockLog.StockLogId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "当前合约不存在该笔库存";
                    return result;
                }
                detail.IntegerAmount = stockLog.GrossAmount;
            }
            provisionalInvoice.IntegerAmount = details.Sum(temp => temp.IntegerAmount);

            result = this.UpdateBusinessInvoice(user, invoice, provisionalInvoice, details);

            return result;
        }

        public ResultModel UpdateReplaceFinal(UserModel user, Operate.Model.Invoice invoice, BusinessInvoice replaceBusiness, List<BusinessInvoiceDetail> details)
        {
            ResultModel result = new ResultModel();

            StockLogDAL stockLogDAL = new StockLogDAL();

            foreach (BusinessInvoiceDetail detail in details)
            {
                result = stockLogDAL.Get(user, detail.StockLogId);
                if (result.ResultStatus != 0)
                    return result;

                StockLog stockLog = result.ReturnValue as StockLog;
                if (stockLog == null || stockLog.StockLogId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "当前合约不存在该笔库存";
                    return result;
                }
                detail.IntegerAmount = stockLog.GrossAmount;
            }

            decimal sumNetAmount = details.Sum(temp => temp.NetAmount);
            if (sumNetAmount != replaceBusiness.NetAmount)
            {
                result.ResultStatus = -1;
                result.Message = "明细净重之和与总净重不相等";
                return result;
            }

            decimal sumBala = details.Sum(temp => temp.Bala);
            if (sumBala != invoice.InvoiceBala)
            {
                result.ResultStatus = -1;
                result.Message = "明细金额之和与总金额不相等";
                return result;
            }

            decimal sumUnitBala = Math.Round(details.Sum(temp => temp.UnitPrice * temp.NetAmount), 2, MidpointRounding.AwayFromZero);
            decimal avgPrice = Math.Round(sumUnitBala / sumNetAmount, 4, MidpointRounding.AwayFromZero);

            replaceBusiness.UnitPrice = avgPrice;
            replaceBusiness.IntegerAmount = details.Sum(temp => temp.IntegerAmount);

            result =  this.UpdateBusinessInvoice(user, invoice, replaceBusiness, details);

            return result;
        }

        public ResultModel UpdateSuppleFinal(UserModel user, Operate.Model.Invoice invoice, BusinessInvoice replaceBusiness, List<BusinessInvoiceDetail> details)
        {
            return this.UpdateBusinessInvoice(user, invoice, replaceBusiness, details);
        }

        public ResultModel GetByInvoiceId(UserModel user, int invoiceId)
        {
            return this.businessinvoiceDAL.GetByInvoiceId(user, invoiceId);
        }

        public ResultModel Goback(UserModel user, int businessInvoiceId)
        {
            ResultModel result = new ResultModel();

            try
            {
                BusinessInvoiceDAL businessInvoiceDAL = new BusinessInvoiceDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取财务票
                    result = businessInvoiceDAL.Get(user, businessInvoiceId);
                    if (result.ResultStatus != 0)
                        return result;

                    BusinessInvoice businessInvoice = result.ReturnValue as BusinessInvoice;
                    if (businessInvoice == null || businessInvoice.BusinessInvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票验证失败";
                        return result;
                    }

                    //获取发票
                    result = invoiceDAL.Get(user, businessInvoice.InvoiceId);
                    if (result.ResultStatus != 0)
                        return result;
                    Operate.Model.Invoice invoice = result.ReturnValue as Operate.Model.Invoice;
                    if (invoice == null || invoice.InvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票获取失败";
                        return result;
                    }

                    //撤返发票
                    result = invoiceDAL.Goback(user, invoice);
                    if (result.ResultStatus != 0)
                        return result;

                    //工作流任务关闭
                    DataSourceDAL sourceDAL = new DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, invoice);
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

        public ResultModel Invalid(UserModel user, int businessInvoiceId)
        {
            ResultModel result = new ResultModel();

            try
            {
                BusinessInvoiceDAL businessInvoiceDAL = new BusinessInvoiceDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();
                BusinessInvoiceDetailDAL detailDAL = new BusinessInvoiceDetailDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取财务票
                    result = businessInvoiceDAL.Get(user, businessInvoiceId);
                    if (result.ResultStatus != 0)
                        return result;

                    BusinessInvoice businessInvoice = result.ReturnValue as BusinessInvoice;
                    if (businessInvoice == null || businessInvoice.BusinessInvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票验证失败";
                        return result;
                    }

                    //获取发票
                    result = invoiceDAL.Get(user, businessInvoice.InvoiceId);
                    if (result.ResultStatus != 0)
                        return result;
                    Operate.Model.Invoice invoice = result.ReturnValue as Operate.Model.Invoice;
                    if (invoice == null || invoice.InvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票获取失败";
                        return result;
                    }

                    //作废发票
                    result = invoiceDAL.Invalid(user, invoice);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取明细
                    result = detailDAL.Load(user, businessInvoice.BusinessInvoiceId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<BusinessInvoiceDetail> details = result.ReturnValue as List<BusinessInvoiceDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票明细获取失败";
                        return result;
                    }

                    foreach (BusinessInvoiceDetail detail in details)
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

            return result;
        }

        public ResultModel Complete(UserModel user, int businessInvoiceId)
        {
            ResultModel result = new ResultModel();

            try
            {
                BusinessInvoiceDAL businessInvoiceDAL = new BusinessInvoiceDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();
                BusinessInvoiceDetailDAL detailDAL = new BusinessInvoiceDetailDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取业务票
                    result = businessInvoiceDAL.Get(user, businessInvoiceId);
                    if (result.ResultStatus != 0)
                        return result;

                    BusinessInvoice businessInvoice = result.ReturnValue as BusinessInvoice;
                    if (businessInvoice == null || businessInvoice.BusinessInvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票验证失败";
                        return result;
                    }

                    //获取发票
                    result = invoiceDAL.Get(user, businessInvoice.InvoiceId);
                    if (result.ResultStatus != 0)
                        return result;
                    Operate.Model.Invoice invoice = result.ReturnValue as Operate.Model.Invoice;
                    if (invoice == null || invoice.InvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票获取失败";
                        return result;
                    }

                    //获取明细
                    result = detailDAL.Load(user, businessInvoice.BusinessInvoiceId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<BusinessInvoiceDetail> details = result.ReturnValue as List<BusinessInvoiceDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票明细获取失败";
                        return result;
                    }

                    //验证
                    switch (invoice.InvoiceType)
                    {
                        case (int)InvoiceTypeEnum.ProvisionalInvoice:
                            //临票验证
                            //必须所有替临终票为已完成状态

                            break;
                    }


                    //发票完成
                    result = invoiceDAL.Complete(user, invoice);
                    if (result.ResultStatus != 0)
                        return result;

                    foreach (BusinessInvoiceDetail detail in details)
                    {
                        result = detailDAL.Complete(user, detail);
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

        public ResultModel CompleteCancel(UserModel user, int businessInvoiceId)
        {
            ResultModel result = new ResultModel();

            try
            {
                BusinessInvoiceDAL businessInvoiceDAL = new BusinessInvoiceDAL();
                InvoiceDAL invoiceDAL = new InvoiceDAL();
                BusinessInvoiceDetailDAL detailDAL = new BusinessInvoiceDetailDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取财务票
                    result = businessInvoiceDAL.Get(user, businessInvoiceId);
                    if (result.ResultStatus != 0)
                        return result;

                    BusinessInvoice businessInvoice = result.ReturnValue as BusinessInvoice;
                    if (businessInvoice == null || businessInvoice.BusinessInvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票验证失败";
                        return result;
                    }

                    //获取发票
                    result = invoiceDAL.Get(user, businessInvoice.InvoiceId);
                    if (result.ResultStatus != 0)
                        return result;
                    Operate.Model.Invoice invoice = result.ReturnValue as Operate.Model.Invoice;
                    if (invoice == null || invoice.InvoiceId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票获取失败";
                        return result;
                    }

                    //发票
                    result = invoiceDAL.CompleteCancel(user, invoice);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取明细
                    result = detailDAL.Load(user, businessInvoice.BusinessInvoiceId, StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<BusinessInvoiceDetail> details = result.ReturnValue as List<BusinessInvoiceDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "发票明细获取失败";
                        return result;
                    }

                    foreach (BusinessInvoiceDetail detail in details)
                    {
                        result = detailDAL.CompleteCancel(user, detail);
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

        /// <summary>
        /// 财务发票可分配业务发票列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="currencyId"></param>
        /// <param name="invoiceDirection"></param>
        /// <param name="outCorpId"></param>
        /// <param name="inCorpId"></param>
        /// <returns></returns>
        public SelectModel GetCanAllotSelectModel(int pageIndex, int pageSize, string orderStr, int currencyId, int invoiceDirection, int outCorpId, int inCorpId, string bIds)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "bus.BusinessInvoiceId desc";
            else
                select.OrderStr = orderStr;

            int InvoiceDirection = (int)StyleEnum.InvoiceDirection;

            StringBuilder sb = new StringBuilder();
            sb.Append(" bus.BusinessInvoiceId,inv.InvoiceDate,inv.InvoiceNo,inv.InvoiceName,bd.DetailName,cur.CurrencyName,inv.OutCorpName,inv.InCorpName,bus.VATRatio,bus.VATBala,inv.InvoiceBala ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Inv_BusinessInvoice bus ");
            sb.Append(" inner join dbo.Invoice inv on bus.InvoiceId = inv.InvoiceId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on inv.CurrencyId = cur.CurrencyId  ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail bd on inv.InvoiceDirection = bd.StyleDetailId and bd.BDStyleId ={0} ", InvoiceDirection);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" inv.InvoiceStatus = {0} ", (int)StatusEnum.已生效);
            sb.AppendFormat(" and inv.InvoiceType in ({0},{1},{2}) ", (int)InvoiceTypeEnum.补临终票, (int)InvoiceTypeEnum.替临终票, (int)InvoiceTypeEnum.直接终票);
            sb.AppendFormat(" and inv.CurrencyId = {0} ", currencyId);
            sb.AppendFormat(" and inv.InvoiceDirection = {0} ", invoiceDirection);
            if (invoiceDirection == (int)InvoiceDirectionEnum.开具)
                sb.AppendFormat(" and inv.InCorpId = {0} ", inCorpId);
            else if (invoiceDirection == (int)InvoiceDirectionEnum.收取)
                sb.AppendFormat(" and inv.OutCorpId = {0} ", outCorpId);
            if (!string.IsNullOrEmpty(bIds))
                sb.AppendFormat(" and bus.BusinessInvoiceId not in ({0}) ", bIds);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetCanAllotBISelectModel(int pageIndex, int pageSize, string orderStr, int dir, int currencyId, int assetId, int outCorpId, int inCorpId, string iids)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "inv.InvoiceId desc";
            else
                select.OrderStr = orderStr;

            StringBuilder sb = new StringBuilder();
            sb.Append(" inv.InvoiceId,bi.BusinessInvoiceId,inv.InvoiceNo,inv.InvoiceDate,inv.InvoiceName,inv.InvoiceBala,CONVERT(varchar,inv.InvoiceBala)+cur.CurrencyName as InoviceBalaName,ISNULL(inv.InCorpName,inCorp.CorpName) InCorpName,ISNULL(inv.OutCorpName,outCorp.CorpName) OutCorpName,inv.Memo,sub.SubNo,ass.AssetName,bi.IntegerAmount,CONVERT(varchar,bi.IntegerAmount)+mu.MUName IntegerAmountName,bi.NetAmount,CONVERT(varchar,bi.NetAmount)+MUName as NetAmountName,bi.UnitPrice,bi.MarginRatio,bi.VATRatio,bi.VATBala,bi.AssetId,inv.CurrencyId,inv.OutCorpId,inv.InCorpId,bi.MUId ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Inv_BusinessInvoice bi ");
            sb.Append(" inner join dbo.Invoice inv on bi.InvoiceId = inv.InvoiceId ");
            sb.Append(" left join dbo.Con_ContractSub sub on sub.SubId = bi.SubContractId  ");
            sb.Append(" left join NFMT_Basic..Currency cur on inv.CurrencyId = cur.CurrencyId");
            sb.Append(" left join NFMT_User..Corporation inCorp on inCorp.CorpId = inv.InCorpId ");
            sb.Append(" left join NFMT_User..Corporation outCorp on outCorp.CorpId = inv.OutCorpId ");
            sb.Append(" left join NFMT_Basic..Asset ass on bi.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic..MeasureUnit mu on mu.MUId = bi.MUId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" inv.InvoiceStatus = {0} ", (int)StatusEnum.已生效);
            sb.AppendFormat(" and bi.BusinessInvoiceId not in (select BusinessInvoiceId from dbo.Inv_FinBusInvAllotDetail where DetailStatus <> {0}) ", (int)StatusEnum.已作废);
            //sb.AppendFormat(" and inv.InvoiceType in ({0},{1},{2}) ", (int)InvoiceTypeEnum.补临终票, (int)InvoiceTypeEnum.替临终票, (int)InvoiceTypeEnum.直接终票);
            if (dir > 0)
                sb.AppendFormat(" and inv.InvoiceDirection = {0} ", dir);
            if (currencyId > 0)
                sb.AppendFormat(" and inv.CurrencyId = {0} ", currencyId);
            if (assetId > 0)
                sb.AppendFormat(" and bi.AssetId = {0} ", assetId);
            if (inCorpId > 0)
                sb.AppendFormat(" and inv.InCorpId = {0} ", inCorpId);
            if (outCorpId > 0)
                sb.AppendFormat(" and inv.OutCorpId = {0} ", outCorpId);
            if (!string.IsNullOrEmpty(iids))
                sb.AppendFormat(" and bi.BusinessInvoiceId not in ({0}) ", iids);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetBISelectModel(int pageIndex, int pageSize, string orderStr, string iids)
        {
            SelectModel select = this.GetCanAllotBISelectModel(pageIndex, pageSize, orderStr, 0, 0, 0, 0, 0, iids);

            if (!string.IsNullOrEmpty(iids))
                select.WhereStr = string.Format(" bi.BusinessInvoiceId in ({0}) ", iids);
            else
                select.WhereStr = " 1=2 ";

            return select;
        }

        public ResultModel CheckContractSubBusinessInvoiceApplyConfirm(UserModel user, int subId)
        {
            return this.businessinvoiceDAL.CheckContractSubBusinessInvoiceApplyConfirm(user, subId);
        }

        #endregion

        #region report

        public SelectModel GetBusInvReportSelect(int pageIndex, int pageSize, string orderStr, int innerCorpId, int outerCorpId, int invType, int assetId, DateTime startDate, DateTime endDate)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "inv.InvoiceId desc";
            else
                select.OrderStr = orderStr;

            BDStyleDetail bdIssue = DetailProvider.Details(StyleEnum.发票方向)["Issue"];//发票开具
            BDStyleDetail bdCollect = DetailProvider.Details(StyleEnum.发票方向)["Collect"];//发票收取

            int readyStatus = (int)StatusEnum.已生效;
            StringBuilder sb = new StringBuilder();

            sb.Append("inv.InvoiceId,inv.InvoiceDate,inv.InvoiceNo,inv.InvoiceName,");
            sb.AppendFormat("case inv.InvoiceDirection when {0} then outCorp.CorpName when {1} then inCorp.CorpName else '' end as innerCorp,", bdIssue.StyleDetailId, bdCollect.StyleDetailId);
            sb.AppendFormat("case inv.InvoiceDirection when {0} then inCorp.CorpName when {1} then outCorp.CorpName else '' end as outerCorp,", bdIssue.StyleDetailId, bdCollect.StyleDetailId);
            sb.Append("bdInvoiceDirection.DetailName as InvoiceDirection,bdInvoiceType.DetailName as InvoiceType,ass.AssetName,biDetail.NetAmount,mu.MUName,biDetail.Bala,cur.CurrencyName,sn.RefNo");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT..Invoice inv ");
            sb.Append(" left join NFMT_User..Corporation outCorp on outCorp.CorpId = inv.OutCorpId ");
            sb.Append(" left join NFMT_User..Corporation inCorp on inCorp.CorpId = inv.InCorpId ");
            sb.AppendFormat(" left join NFMT_Basic..BDStyleDetail bdInvoiceDirection on bdInvoiceDirection.StyleDetailId = inv.InvoiceDirection and bdInvoiceDirection.BDStyleId = {0} ", (int)StyleEnum.发票方向);
            sb.AppendFormat(" left join NFMT_Basic..BDStyleDetail bdInvoiceType on bdInvoiceType.StyleDetailId = inv.InvoiceType and bdInvoiceType.BDStyleId = {0} ", (int)StyleEnum.发票类型);
            sb.Append(" inner join NFMT..Inv_BusinessInvoice bi on bi.InvoiceId = inv.InvoiceId ");
            sb.Append(" left join NFMT_Basic..Asset ass on ass.AssetId = bi.AssetId ");
            sb.Append(" left join NFMT_Basic..MeasureUnit mu on mu.MUId = bi.MUId ");
            sb.Append(" left join NFMT_Basic..Currency cur on cur.CurrencyId = inv.CurrencyId ");
            sb.AppendFormat(" inner join NFMT..Inv_BusinessInvoiceDetail biDetail on biDetail.BusinessInvoiceId = bi.BusinessInvoiceId and biDetail.DetailStatus >={0} ", readyStatus);
            sb.Append(" left join NFMT..St_Stock st on st.StockId = biDetail.StockId ");
            sb.Append(" left join NFMT..St_StockName sn on sn.StockNameId = st.StockNameId ");

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" inv.InvoiceStatus >= {0} ", readyStatus);
            if (innerCorpId > 0)
                sb.AppendFormat(" and ((outCorp.CorpId = {0} and inv.InvoiceDirection = {1}) or (inCorp.CorpId = {0} and inv.InvoiceDirection = {2})) ", innerCorpId, bdIssue.StyleDetailId, bdCollect.StyleDetailId);
            if (outerCorpId > 0)
                sb.AppendFormat(" and ((inCorp.CorpId = {0} and inv.InvoiceDirection = {1}) or (outCorp.CorpId = {0} and inv.InvoiceDirection = {2})) ", outerCorpId, bdIssue.StyleDetailId, bdCollect.StyleDetailId);
            if (invType > 0)
                sb.AppendFormat(" and inv.InvoiceType = {0} ", invType);
            if (assetId > 0)
                sb.AppendFormat(" and bi.AssetId = {0} ", assetId);
            if (startDate > DefaultValue.DefaultTime && endDate > startDate)
                sb.AppendFormat(" and inv.InvoiceDate between '{0}' and '{1}' ", startDate.ToString(), endDate.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        //public override string[,] SetExcelRangeData(DataTable source)
        //{
        //    string[,] objData = new string[source.Rows.Count, 13];

        //    for (int i = 0; i < source.Rows.Count; i++)
        //    {
        //        System.Data.DataRow dr = source.Rows[i];

        //        objData[i, 0] = ((DateTime)dr["InvoiceDate"]).ToString("yyyy-MM-dd");
        //        objData[i, 1] = dr["InvoiceNo"].ToString();
        //        objData[i, 2] = dr["InvoiceName"].ToString();
        //        objData[i, 3] = dr["innerCorp"].ToString();
        //        objData[i, 4] = dr["outerCorp"].ToString();
        //        objData[i, 5] = dr["InvoiceDirection"].ToString();
        //        objData[i, 6] = dr["InvoiceType"].ToString();
        //        objData[i, 7] = dr["AssetName"].ToString();
        //        objData[i, 8] = dr["NetAmount"].ToString();
        //        objData[i, 9] = dr["MUName"].ToString();
        //        objData[i, 10] = dr["Bala"].ToString();
        //        objData[i, 11] = dr["CurrencyName"].ToString();
        //        objData[i, 12] = dr["RefNo"].ToString();
        //    }

        //    return objData;
        //}

        public override DataTable SetExcelRangeData(DataTable source)
        {
            string[] strs = new string[] { "InvoiceDate", "InvoiceNo", "InvoiceName", "innerCorp", "outerCorp", "InvoiceDirection", "InvoiceType", "AssetName", "NetAmount", "MUName", "Bala", "CurrencyName", "RefNo" };

            return source.ConvertDataTable(strs);
        }

        #endregion
    }
}
