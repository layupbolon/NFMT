/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractStockIn_BLL.cs
// 文件功能描述：入库登记合约关联dbo.ContractStockIn_Ref业务逻辑类。
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
using System.Linq;

namespace NFMT.WareHouse.BLL
{
    /// <summary>
    /// 入库登记合约关联dbo.ContractStockIn_Ref业务逻辑类。
    /// </summary>
    public class ContractStockIn_BLL : Common.ExecBLL
    {
        private ContractStockInDAL contractstockin_DAL = new ContractStockInDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ContractStockInDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractStockIn_BLL()
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
            get { return this.contractstockin_DAL; }
        }
        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, string stockName, int status, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "csir.RefId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" csir.RefId,si.StockInId,si.StockInDate,si.RefNo,si.GrossAmount,si.UintId ");
            sb.Append(" ,CAST(ISNULL(si.GrossAmount,0) as varchar) + mu.MUName as GrossWeight ");
            sb.Append(" ,si.AssetId,ass.AssetName,cs.SubId,cs.SubNo,csir.RefStatus,rs.StatusName ");
            sb.Append(" ,con.ContractId,con.ContractNo,inCorp.CorpId as InCorpId,inCorp.CorpName as InCorpName,outCorp.CorpId as OutCorpId,outCorp.CorpName as OutCorpName ");
            select.ColumnName = sb.ToString();

            int readyStatus = (int)Common.StatusEnum.已生效;
            int commonSstatusType = (int)Common.StatusTypeEnum.通用状态;

            sb.Clear();
            sb.Append(" dbo.St_ContractStockIn_Ref csir ");
            sb.Append(" inner join dbo.St_StockIn si on csir.StockInId = si.StockInId ");
            sb.Append(" inner join dbo.Con_ContractSub cs on csir.ContractSubId = cs.SubId ");
            sb.Append(" inner join dbo.Con_Contract con on con.ContractId = cs.ContractId ");
            sb.AppendFormat(" inner join dbo.Con_ContractCorporationDetail inCorp on inCorp.ContractId = con.ContractId and inCorp.IsDefaultCorp= 1 and inCorp.IsInnerCorp =1 and inCorp.DetailStatus>={0} ", readyStatus);
            sb.AppendFormat(" inner join dbo.Con_ContractCorporationDetail outCorp on outCorp.ContractId = con.ContractId and outCorp.IsDefaultCorp=1 and outCorp.IsInnerCorp = 0 and outCorp.DetailStatus>={0} ", readyStatus);
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = si.UintId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = si.AssetId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail rs on csir.RefStatus = rs.DetailId  and rs.StatusId ={0} ", commonSstatusType);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (!string.IsNullOrEmpty(stockName))
                sb.AppendFormat(" and si.RefNo like '%{0}%' ", stockName);
            if (status > 0)
                sb.AppendFormat(" and csir.RefStatus = {0} ", status);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and si.StockInDate between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetStockInNoContractSelect(int pageIndex, int pageSize, string orderStr, string stockName, int status, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "si.StockInId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" si.StockInId,si.StockInDate,si.RefNo,si.GrossAmount,si.UintId");
            sb.Append(",CAST(ISNULL(si.GrossAmount,0) as varchar) + mu.MUName as GrossWeight");
            sb.Append(",si.AssetId,ass.AssetName,si.StockInStatus,rs.StatusName");
            sb.Append(",bra.BrandId,bra.BrandName,dp.DPName,si.CardNo ");
            select.ColumnName = sb.ToString();

            int commonStatusType = (int)Common.StatusTypeEnum.通用状态;
            int entryStatus = (int)Common.StatusEnum.已录入;

            sb.Clear();
            sb.Append(" dbo.St_StockIn si ");
            sb.AppendFormat(" left join dbo.St_ContractStockIn_Ref csir on si.StockInId = csir.StockInId  and csir.RefStatus>={0} ", entryStatus);
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = si.UintId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = si.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = si.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = si.DeliverPlaceId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail rs on si.StockInStatus = rs.DetailId  and rs.StatusId ={0} ", commonStatusType);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" csir.RefId is null ");

            if (!string.IsNullOrEmpty(stockName))
                sb.AppendFormat(" and si.RefNo like '%{0}%' ", stockName);
            if (status > 0)
                sb.AppendFormat(" and si.StockInStatus = {0} ", status);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and si.StockInDate between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetContractSelect(int pageIndex, int pageSize, string orderStr, int stockInId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "si.StockInId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("csir.RefId,cs.SubId,con.ContractId,inCorp.CorpId as InCorpId,outCorp.CorpId as OutCorpId");
            sb.Append(",cs.ContractDate,con.ContractNo,cs.SubNo,ass.AssetId,ass.AssetName,cs.SignAmount,cast(isnull(cs.SignAmount,0) as varchar) + mu.MUName as SignWeight");
            sb.Append(",outCorp.CorpName as OutCorpName,inCorp.CorpName as InCorpName,si.StockInId");
            sb.Append(",ss.SumAmount,CAST(isnull(ss.SumAmount,0) as varchar) + mu.MUName as SumWeight");
            select.ColumnName = sb.ToString();

            //int commonStatusType = (int)Common.StatusTypeEnum.通用状态;
            int readyStatus = (int)Common.StatusEnum.已生效;
            int entryStatus = (int)Common.StatusEnum.已录入;

            sb.Clear();

            sb.Append(" dbo.St_ContractStockIn_Ref csir ");
            sb.Append(" inner join dbo.St_StockIn si on csir.StockInId = si.StockInId ");
            sb.Append(" inner join dbo.Con_ContractSub cs on csir.ContractSubId = cs.SubId ");
            sb.Append(" inner join dbo.Con_Contract con on csir.ContractId= con.ContractId and cs.ContractId = con.ContractId ");
            sb.AppendFormat(" inner join dbo.Con_ContractCorporationDetail inCorp on inCorp.ContractId = con.ContractId and inCorp.CorpId= si.CorpId and inCorp.IsInnerCorp=1 and inCorp.DetailStatus >={0} ", readyStatus);
            sb.AppendFormat(" inner join dbo.Con_ContractCorporationDetail outCorp on outCorp.ContractId = con.ContractId and outCorp.IsDefaultCorp=1 and outCorp.IsInnerCorp =0 and outCorp.DetailStatus>={0} ", readyStatus);

            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = con.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId= cs.UnitId ");

            sb.Append(" left join ( ");
            sb.Append(" select SUM(sto.GrossAmount) as SumAmount,ref.ContractSubId from dbo.St_ContractStockIn_Ref ref  ");
            sb.AppendFormat(" inner join dbo.St_StockIn sto on ref.StockInId = sto.StockInId and sto.StockInStatus>={0} ", entryStatus);
            sb.AppendFormat(" where ref.RefStatus>={0} group by ref.ContractSubId) as ss on ss.ContractSubId = cs.SubId ", entryStatus);

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" csir.StockInId = {0} and csir.RefStatus >= {1} ", stockInId, entryStatus);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetStockInCanSelect(int pageIndex, int pageSize, string orderStr, int subId, int assetId, int corpId, int customsType)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "si.StockInId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("cs.SubId,con.ContractId,inCorp.CorpId as InCorpId,outCorp.CorpId as OutCorpId");
            sb.Append(",cs.ContractDate,con.ContractNo,cs.SubNo,ass.AssetId,ass.AssetName,cs.SignAmount,cast(isnull(cs.SignAmount,0) as varchar) + mu.MUName as SignWeight");
            sb.Append(",outCorp.CorpName as OutCorpName,inCorp.CorpName as InCorpName");
            sb.Append(",ss.SumAmount,CAST(isnull(ss.SumAmount,0) as varchar) + mu.MUName as SumWeight");
            select.ColumnName = sb.ToString();

            int readyStatus = (int)Common.StatusEnum.已生效;
            int entryStatus = (int)Common.StatusEnum.已录入;
            int buyDirection = (int)NFMT.Contract.TradeDirectionEnum.Buy;

            sb.Clear();

            sb.Append(" dbo.Con_ContractSub cs ");
            sb.AppendFormat(" inner join dbo.Con_Contract con on cs.ContractId = con.ContractId and con.TradeDirection ={0} ", buyDirection);
            //sb.AppendFormat(" inner join dbo.Con_ContractCorporationDetail inCorp on inCorp.ContractId = con.ContractId and inCorp.IsDefaultCorp= 1 and inCorp.IsInnerCorp=1 and inCorp.DetailStatus >={0} ", readyStatus);
            sb.AppendFormat(" inner join dbo.Con_ContractCorporationDetail inCorp on inCorp.ContractId = con.ContractId and inCorp.CorpId= {1} and inCorp.IsInnerCorp=1 and inCorp.DetailStatus >={0} ", readyStatus, corpId);
            sb.AppendFormat(" inner join dbo.Con_ContractCorporationDetail outCorp on outCorp.ContractId = con.ContractId and outCorp.IsDefaultCorp=1 and outCorp.IsInnerCorp =0 and outCorp.DetailStatus>={0} ", readyStatus);
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = con.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId= cs.UnitId ");
            sb.Append(" left join ( ");
            sb.Append(" select SUM(sto.GrossAmount) as SumAmount,ref.ContractSubId from dbo.St_ContractStockIn_Ref ref  ");
            sb.AppendFormat(" inner join dbo.St_StockIn sto on ref.StockInId = sto.StockInId and sto.StockInStatus>={0} ", entryStatus);
            sb.AppendFormat(" where ref.RefStatus>={0} group by ref.ContractSubId) as ss on ss.ContractSubId = cs.SubId ", entryStatus);

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" cs.SubStatus>={0} and cs.SubId !={1} ", entryStatus, subId);
            if (assetId > 0)
                sb.AppendFormat(" and con.AssetId ={0} ", assetId);
            if (customsType > 0)
                sb.AppendFormat(" and con.TradeBorder = case when {0} = 21 then 136 when {0} = 20 then 137 end ", customsType);
            //if (corpId > 0)
            //    sb.AppendFormat(" and {0} in (select CorpId from dbo.Con_ContractCorporationDetail where ContractId = con.ContractId and IsInnerCorp = 1 and DetailStatus = {1})  ",corpId,readyStatus);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetSelectedModel(int pageIndex, int pageSize, string orderStr, int subId, int refId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "csir.RefId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("csir.RefId,si.StockInId,si.StockInDate,si.RefNo,si.GrossAmount,si.UintId ");
            sb.Append(",CAST(ISNULL(si.GrossAmount,0) as varchar) + mu.MUName as GrossWeight");
            sb.Append(" ,si.AssetId,ass.AssetName,csir.RefStatus,rs.StatusName ");
            sb.Append(",bra.BrandId,bra.BrandName");
            select.ColumnName = sb.ToString();

            int entryStatus = (int)Common.StatusEnum.已录入;
            int commonSstatusType = (int)Common.StatusTypeEnum.通用状态;

            sb.Clear();
            sb.Append(" dbo.St_ContractStockIn_Ref csir ");
            sb.Append(" inner join dbo.St_StockIn si on csir.StockInId = si.StockInId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = si.UintId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = si.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = si.BrandId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail rs on csir.RefStatus = rs.DetailId  and rs.StatusId ={0} ", commonSstatusType);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" csir.ContractSubId = {0} ", subId);
            sb.AppendFormat(" and csir.RefStatus>={0} ", entryStatus);
            sb.AppendFormat(" and csir.RefId not in ({0}) ", refId);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Create(UserModel user, int subId, int stockInId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockInDAL stockInDAL = new StockInDAL();
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //验证子合约
                    result = subDAL.Get(user, subId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约不存在";
                        return result;
                    }

                    //验证子合约下分配入库重量
                    result = stockInDAL.Load(user, subId, StatusEnum.已录入);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockIn> stockIns = result.ReturnValue as List<Model.StockIn>;
                    if (stockIns == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约下的入库登记获取失败";
                        return result;
                    }

                    decimal sumAmount = stockIns.Sum(temp => temp.GrossAmount);

                    if (sumAmount == sub.SignAmount)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约已分配满额";
                        return result;
                    }

                    //验证入库登记
                    result = stockInDAL.Get(user, stockInId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockIn stockIn = result.ReturnValue as Model.StockIn;
                    if (stockIn == null || stockIn.StockInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "入库登记不存在";
                        return result;
                    }

                    if (stockIn.GrossAmount > sub.SignAmount)
                    {
                        result.ResultStatus = -1;
                        result.Message = "分配入库的重量大于子合约签订数量";
                        return result;
                    }

                    if (stockIn.GrossAmount > sub.SignAmount - sumAmount)
                    {
                        result.ResultStatus = -1;
                        result.Message = "分配入库的重量大于子合约可分配重量";
                        return result;
                    }

                    //新增入库分配
                    Model.ContractStockIn contractStockIn = new ContractStockIn();
                    contractStockIn.ContractId = sub.ContractId;
                    contractStockIn.ContractSubId = sub.SubId;
                    contractStockIn.RefStatus = StatusEnum.已录入;
                    contractStockIn.StockInId = stockIn.StockInId;

                    result = this.contractstockin_DAL.Insert(user, contractStockIn);
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

        public ResultModel Update(UserModel user, int subId, int refId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockInDAL stockInDAL = new StockInDAL();
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //验证子合约
                    result = subDAL.Get(user, subId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约不存在";
                        return result;
                    }

                    //验证入库分配
                    result = this.contractstockin_DAL.Get(user, refId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ContractStockIn contractStockIn = result.ReturnValue as Model.ContractStockIn;
                    if (contractStockIn == null || contractStockIn.StockInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "入库分配不存在";
                        return result;
                    }

                    //获取入库登记
                    result = stockInDAL.Get(user, contractStockIn.StockInId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockIn stockIn = result.ReturnValue as Model.StockIn;
                    if (stockIn == null || stockIn.StockInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "入库登记不存在";
                        return result;
                    }

                    //更新入库分配
                    contractStockIn.ContractSubId = sub.SubId;
                    contractStockIn.ContractId = sub.ContractId;

                    result = this.contractstockin_DAL.Update(user, contractStockIn);
                    if (result.ResultStatus != 0)
                        return result;

                    //验证子合约下分配入库重量
                    result = stockInDAL.Load(user, subId, StatusEnum.已录入);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockIn> stockIns = result.ReturnValue as List<Model.StockIn>;
                    if (stockIns == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约下的入库登记获取失败";
                        return result;
                    }

                    decimal sumAmount = stockIns.Sum(temp => temp.GrossAmount);

                    if (sumAmount > sub.SignAmount)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约分配入库超额满额";
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

        public ResultModel Goback(UserModel user, int refId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockInDAL stockInDAL = new StockInDAL();
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取入库分配
                    result = this.contractstockin_DAL.Get(user, refId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ContractStockIn contractStockIn = result.ReturnValue as Model.ContractStockIn;
                    if (contractStockIn == null || contractStockIn.RefId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "入库分配不存在";
                        return result;
                    }

                    //撤返入库分配
                    result = this.contractstockin_DAL.Goback(user, contractStockIn);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废工作流审核
                    WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, contractStockIn);
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

        public ResultModel Invalid(UserModel user, int refId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockInDAL stockInDAL = new StockInDAL();
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取入库分配
                    result = this.contractstockin_DAL.Get(user, refId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ContractStockIn contractStockIn = result.ReturnValue as Model.ContractStockIn;
                    if (contractStockIn == null || contractStockIn.RefId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "入库分配不存在";
                        return result;
                    }

                    //作废入库分配
                    result = this.contractstockin_DAL.Invalid(user, contractStockIn);
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

        public ResultModel GetByStockInId(UserModel user, int stockInId, Common.StatusEnum status = StatusEnum.已生效)
        {
            return this.contractstockin_DAL.GetByStockInId(user, stockInId, status);
        }

        public ResultModel Audit(UserModel user, NFMT.WorkFlow.Model.DataSource dataSource, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                DAL.StockInDAL stockInDAL = new StockInDAL();
                DAL.StockInStockDAL stockInStockDAL = new StockInStockDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.contractstockin_DAL.Get(NFMT.Common.DefaultValue.SysUser, dataSource.RowId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ContractStockIn stockInContract = result.ReturnValue as Model.ContractStockIn;
                    if (stockInContract == null || stockInContract.StockInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "入库登记合约关联不存在";
                        return result;
                    }

                    //审核，修改数据状态
                    result = this.contractstockin_DAL.Audit(user, stockInContract, isPass);
                    if (result.ResultStatus != 0)
                        return result;

                    //审核通过
                    if (isPass)
                    {
                        //获取入库登记 
                        result = stockInDAL.Get(user, stockInContract.StockInId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockIn stockIn = result.ReturnValue as Model.StockIn;
                        if (stockIn == null || stockIn.StockInId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "入库登记不存在";
                            return result;
                        }

                        //获取入库登记与流水关联
                        result = stockInStockDAL.GetByStockIn(user, stockIn.StockInId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockInStock stockInStock = result.ReturnValue as Model.StockInStock;
                        if (stockInStock == null || stockInStock.RefId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "入库登记不存在";
                            return result;
                        }

                        //获取流水
                        result = stockLogDAL.Get(user, stockInStock.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "入库流水获取失败不存在";
                            return result;
                        }

                        //更新流水合约关联
                        stockLog.SubContractId = stockInContract.ContractSubId;
                        stockLog.ContractId = stockInContract.ContractId;

                        result = stockLogDAL.Update(user, stockLog);
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

        #region 采购合约新增同时操作入库与合约关联

        public ResultModel ContractInCreateStockOperate(UserModel user, NFMT.Contract.Model.Contract contract, int subId, List<int> stockLogIds)
        {
            ResultModel result = new ResultModel();

            int assetId = contract.AssetId;
            int logDirection = (int)NFMT.WareHouse.LogDirectionEnum.In;
            int customsType = (int)NFMT.WareHouse.CustomTypeEnum.关外;
            if (contract.TradeBorder == (int)NFMT.Contract.TradeBorderEnum.内贸)
                customsType = (int)NFMT.WareHouse.CustomTypeEnum.关内;

            StockLogDAL stockLogDAL = new StockLogDAL();
            StockInDAL stockInDAL = new StockInDAL();
            ContractStockInDAL contractStockInDAL = new ContractStockInDAL();
            StockInStockDAL stockInStockDAL = new StockInStockDAL();

            foreach (int stockLogId in stockLogIds)
            {
                result = stockLogDAL.Get(user, stockLogId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.StockLog stockLog = result.ReturnValue as NFMT.WareHouse.Model.StockLog;
                if (stockLog == null || stockLog.StockLogId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "库存流水不存在";
                    return result;
                }

                if (stockLog.LogStatus != StatusEnum.已生效)
                {
                    result.ResultStatus = -1;
                    result.Message = "库存流水状态不正确";
                    return result;
                }

                if (assetId != stockLog.AssetId)
                {
                    result.ResultStatus = -1;
                    result.Message = "选中库存存在品种不一致";
                    return result;
                }

                if (logDirection != stockLog.LogDirection)
                {
                    result.ResultStatus = -1;
                    result.Message = "选中库存存在流水方向不一致";
                    return result;
                }

                if (customsType != stockLog.CustomsType)
                {
                    result.ResultStatus = -1;
                    result.Message = "选中库存存在关境不一致";
                    return result;
                }

                result = stockInStockDAL.GetByStockLogId(user, stockLog.StockLogId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.StockInStock stockInStock = result.ReturnValue as NFMT.WareHouse.Model.StockInStock;
                if (stockInStock == null || stockInStock.RefId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "库存流水与入库登记关系获取失败";
                    return result;
                }

                NFMT.WareHouse.Model.ContractStockIn contractStockIn = new NFMT.WareHouse.Model.ContractStockIn();
                contractStockIn.ContractId = contract.ContractId;
                contractStockIn.ContractSubId = subId;
                contractStockIn.RefStatus = StatusEnum.已生效;
                contractStockIn.StockInId = stockInStock.StockInId;

                result = contractStockInDAL.Insert(user, contractStockIn);
                if (result.ResultStatus != 0)
                    return result;
            }

            return result;
        }

        public ResultModel ContractInInvalidStockOperate(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();

            ContractStockInDAL contractStockInDAL = new ContractStockInDAL();
            result = contractStockInDAL.Load(user, contractId);
            if (result.ResultStatus != 0)
                return result;

            List<NFMT.WareHouse.Model.ContractStockIn> contractStockIns = result.ReturnValue as List<NFMT.WareHouse.Model.ContractStockIn>;
            if (contractStockIns == null)
            {
                result.Message = "合约入库分配获取失败";
                result.ResultStatus = -1;
                return result;
            }

            foreach (NFMT.WareHouse.Model.ContractStockIn contractStcokIn in contractStockIns)
            {
                result = contractStockInDAL.Invalid(user, contractStcokIn);
                if (result.ResultStatus != 0)
                    return result;
            }

            return result;
        }

        public ResultModel ContractInCompleteStockOperate(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();
            StockInDAL stockInDAL = new StockInDAL();
            StockLogDAL stockLogDAL = new StockLogDAL();
            StockDAL stockDAL = new StockDAL();
            ContractStockInDAL contractStockInDAL = new ContractStockInDAL();
            StockInStockDAL stockInStockDAL = new StockInStockDAL();

            result = contractStockInDAL.Load(user, contractId);
            if (result.ResultStatus != 0)
                return result;

            List<NFMT.WareHouse.Model.ContractStockIn> contractStockIns = result.ReturnValue as List<NFMT.WareHouse.Model.ContractStockIn>;
            if (contractStockIns == null)
            {
                result.Message = "合约入库分配获取失败";
                result.ResultStatus = -1;
                return result;
            }

            foreach (NFMT.WareHouse.Model.ContractStockIn contractStcokIn in contractStockIns)
            {
                //获取入库登记
                result = stockInDAL.Get(user, contractStcokIn.StockInId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.StockIn stockIn = result.ReturnValue as NFMT.WareHouse.Model.StockIn;
                if (stockIn == null || stockIn.StockInId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "数据不存在，无法完成";
                    return result;
                }

                //入库登记完成
                if (stockIn.StockInStatus == StatusEnum.已生效)
                {
                    result = stockInDAL.Complete(user, stockIn);
                    if (result.ResultStatus != 0)
                        return result;
                }

                //获取合约关联
                result = contractStockInDAL.GetByStockInId(user, stockIn.StockInId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.ContractStockIn contractStockIn = result.ReturnValue as NFMT.WareHouse.Model.ContractStockIn;
                if (contractStockIn == null || contractStockIn.RefId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "入库登记未关联合约，不允许确认完成";
                    return result;
                }

                //完成合约关联
                if (contractStockIn.RefStatus == StatusEnum.已生效)
                {
                    result = contractStockInDAL.Complete(user, contractStockIn);
                    if (result.ResultStatus != 0)
                        return result;
                }

                //获取入库登记库存关联
                result = stockInStockDAL.GetByStockIn(NFMT.Common.DefaultValue.SysUser, stockIn.StockInId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.StockInStock stockInStock = result.ReturnValue as NFMT.WareHouse.Model.StockInStock;
                if (stockInStock == null || stockInStock.RefId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取入库登记与库存关联失败";
                    return result;
                }

                //完成入库登记库存关联
                if (stockInStock.RefStatus == StatusEnum.已生效)
                {
                    result = stockInStockDAL.Complete(user, stockInStock);
                    if (result.ResultStatus != 0)
                        return result;
                }

                //获取库存流水
                result = stockLogDAL.Get(user, stockInStock.StockLogId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.StockLog stockLog = result.ReturnValue as NFMT.WareHouse.Model.StockLog;
                if (stockLog == null || stockLog.StockLogId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取入库流水失败";
                    return result;
                }

                //判断库存流水是否关联合约
                if (stockLog.ContractId <= 0)
                {
                    stockLog.ContractId = contractStockIn.ContractId;
                    stockLog.SubContractId = contractStockIn.ContractSubId;

                    result = stockLogDAL.Update(user, stockLog);
                    if (result.ResultStatus != 0)
                        return result;
                }

                if (stockLog.LogStatus == StatusEnum.已生效)
                {
                    result = stockLogDAL.Complete(user, stockLog);
                    if (result.ResultStatus != 0)
                        return result;
                }

                //获取库存
                result = stockDAL.Get(user, stockLog.StockId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                if (stock == null || stock.StockId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "库存获取失败";
                    return result;
                }

                //更新库存表
                if (stock.StockStatus == NFMT.WareHouse.StockStatusEnum.预入库存)
                {
                    result = stockDAL.UpdateStockStatus(stock, NFMT.WareHouse.StockStatusEnum.在库正常);
                    if (result.ResultStatus != 0)
                        return result;
                }
            }

            return result;
        }

        public ResultModel ContractInCompleteCancelStockOperate(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();
            StockDAL stockDAL = new StockDAL();
            StockLogDAL stockLogDAL = new StockLogDAL();
            StockInStockDAL stockInStockDAL = new StockInStockDAL();
            ContractStockInDAL contractStockInDAL = new ContractStockInDAL();
            StockInDAL stockInDAL = new StockInDAL();

            result = contractStockInDAL.Load(user, contractId);
            if (result.ResultStatus != 0)
                return result;

            List<NFMT.WareHouse.Model.ContractStockIn> contractStockIns = result.ReturnValue as List<NFMT.WareHouse.Model.ContractStockIn>;
            if (contractStockIns == null)
            {
                result.Message = "合约入库分配获取失败";
                result.ResultStatus = -1;
                return result;
            }

            foreach (NFMT.WareHouse.Model.ContractStockIn contractStcokIn in contractStockIns)
            {
                //获取入库登记
                result = stockInDAL.Get(user, contractStcokIn.StockInId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.StockIn stockIn = result.ReturnValue as NFMT.WareHouse.Model.StockIn;
                if (stockIn == null || stockIn.StockInId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "入库登记不存在";
                    return result;
                }

                //入库登记撤销完成
                result = stockInDAL.CompleteCancel(user, stockIn);
                if (result.ResultStatus != 0)
                    return result;

                //获取合约关联
                result = contractStockInDAL.GetByStockInId(user, stockIn.StockInId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.ContractStockIn contractStockIn = result.ReturnValue as NFMT.WareHouse.Model.ContractStockIn;
                if (contractStockIn == null || contractStockIn.RefId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "入库登记未关联合约，不允许确认完成";
                    return result;
                }

                //完成撤销合约关联
                result = contractStockInDAL.CompleteCancel(user, contractStockIn);
                if (result.ResultStatus != 0)
                    return result;

                //获取入库登记与库存流水关联
                result = stockInStockDAL.GetByStockIn(user, stockIn.StockInId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.StockInStock stockInStock = result.ReturnValue as NFMT.WareHouse.Model.StockInStock;
                if (stockInStock == null || stockInStock.RefId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "关联获取失败";
                    return result;
                }

                //撤销关联
                result = stockInStockDAL.CompleteCancel(user, stockInStock);
                if (result.ResultStatus != 0)
                    return result;

                //获取库存流水
                result = stockLogDAL.Get(user, stockInStock.StockLogId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.StockLog stockLog = result.ReturnValue as NFMT.WareHouse.Model.StockLog;
                if (stockLog == null || stockLog.StockLogId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "库存流水获取失败";
                    return result;
                }

                //撤销库存流水
                result = stockLogDAL.CompleteCancel(user, stockLog);
                if (result.ResultStatus != 0)
                    return result;

                //获取库存
                result = stockDAL.Get(user, stockLog.StockId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                if (stock == null || stock.StockId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "库存获取失败";
                    return result;
                }

                //更新库存状态为预入库存
                result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.预入库存);
                if (result.ResultStatus != 0)
                    return result;
            }

            return result;
        }

        #endregion
    }
}
