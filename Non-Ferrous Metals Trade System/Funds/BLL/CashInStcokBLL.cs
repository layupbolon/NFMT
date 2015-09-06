/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInStcokBLL.cs
// 文件功能描述：收款分配至库存dbo.Fun_CashInStcok_Ref业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年9月18日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Funds.Model;
using NFMT.Funds.DAL;
using NFMT.Funds.IDAL;
using NFMT.Common;
using System.Linq;

namespace NFMT.Funds.BLL
{
    /// <summary>
    /// 收款分配至库存dbo.Fun_CashInStcok_Ref业务逻辑类。
    /// </summary>
    public class CashInStcokBLL : Common.ExecBLL
    {
        private CashInStcokDAL cashinstcokDAL = new CashInStcokDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CashInStcokDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CashInStcokBLL()
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
            get { return this.cashinstcokDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetAllotStockSelect(int pageIndex, int pageSize, string orderStr, int stockStatus, string subNo)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cisr.AllotId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;
            int stockStatusType = (int)NFMT.Common.StatusTypeEnum.库存状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" cisr.AllotId,sl.StockLogId,sto.StockId,sn.StockNameId,ass.AssetId,bra.BrandId,sn.RefNo,cs.SubNo,sl.LogDate,ass.AssetName ");
            sb.Append(" ,bra.BrandName,sto.StockStatus,ss.StatusName as StockStatusName,sto.StockNo,cur.CurrencyName,bd.StatusName ");//,ref.SumBala
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_CashInStcok_Ref cisr ");
            sb.Append(" inner join dbo.St_StockLog sl on cisr.StockLogId = sl.StockLogId ");
            sb.AppendFormat(" inner join dbo.Con_ContractSub cs on cs.SubId = sl.SubContractId and cs.SubStatus = {0} ", readyStatus);
            sb.Append(" inner join dbo.St_Stock sto on sto.StockId = sl.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sn.StockNameId = sto.StockNameId ");
            //sb.Append(" inner join (select r1.StockId,r2.SubContractId,SUM(r1.AllotBala) as SumBala ");
            //sb.Append(" from dbo.Fun_CashInStcok_Ref r1 inner join dbo.Fun_CashInContract_Ref r2 on r1.ContractRefId = r2.RefId ");
            //sb.AppendFormat(" where r1.DetailStatus = {0} group by r1.StockId,r2.SubContractId) as ref on ref.StockId = sto.StockId and cs.SubId = ref.SubContractId ", readyStatus);
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sto.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on sto.BrandId = bra.BrandId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId ={0} ", stockStatusType);
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = cs.SettleCurrency ");
            sb.Append(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = cisr.DetailStatus ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" sl.LogStatus = {0} ", readyStatus);

            if (stockStatus > 0)
                sb.AppendFormat(" and sto.StockStatus={0} ", stockStatus);
            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and cs.SubNo like '%{0 }%' ", subNo);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetAllotReadyStockSelect(int pageIndex, int pageSize, string orderStr, int stockStatus, string subNo)
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
            sb.Append(" sl.StockLogId,sto.StockId,sn.StockNameId,ass.AssetId,bra.BrandId,sn.RefNo,cs.SubNo,sl.LogDate,ass.AssetName ");
            sb.Append(" ,bra.BrandName,sto.StockStatus,ss.StatusName as StockStatusName,sto.StockNo,ref.SumBala,cur.CurrencyName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockLog sl ");
            sb.AppendFormat(" inner join dbo.Con_ContractSub cs on cs.SubId = sl.SubContractId and cs.SubStatus = {0} ", readyStatus);
            sb.Append(" inner join dbo.St_Stock sto on sto.StockId = sl.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sn.StockNameId = sto.StockNameId ");
            sb.Append(" left join (select r1.StockId,r2.SubContractId,SUM(r1.AllotBala) as SumBala ");
            sb.Append(" from dbo.Fun_CashInStcok_Ref r1 inner join dbo.Fun_CashInContract_Ref r2 on r1.ContractRefId = r2.RefId ");
            sb.AppendFormat(" where r1.DetailStatus = {0} group by r1.StockId,r2.SubContractId) as ref on ref.StockId = sto.StockId and cs.SubId = ref.SubContractId ", readyStatus);
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sto.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on sto.BrandId = bra.BrandId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId ={0} ", stockStatusType);
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = cs.SettleCurrency ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" sl.LogStatus = {0} ", readyStatus);

            if (stockStatus > 0)
                sb.AppendFormat(" and sto.StockStatus={0} ", stockStatus);
            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and cs.SubNo like '%{0 }%' ", subNo);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetOtherAllotInStock(int pageIndex, int pageSize, string orderStr, int stockLogId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cia.AllotId desc";
            else
                select.OrderStr = orderStr;

            int commonStatusType = (int)NFMT.Common.StatusTypeEnum.通用状态;
            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;
            int entryStatus = (int)NFMT.Common.StatusEnum.已录入;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("cia.AllotId,cia.AllotTime,cia.Alloter,emp.Name as AlloterName,cisr.SumBala");
            sb.Append(",cia.CurrencyId,cur.CurrencyName,cia.AllotStatus,sd.StatusName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_CashInAllot cia ");
            sb.Append(" inner join(select AllotId,SUM(AllotBala) as SumBala from dbo.Fun_CashInStcok_Ref ");
            sb.AppendFormat(" where DetailStatus >={0} and StockLogId={1} group by AllotId)as cisr on cisr.AllotId = cia.AllotId ", readyStatus, stockLogId);
            sb.Append(" left join NFMT_User.dbo.Employee emp on emp.EmpId = cia.Alloter ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = cia.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = cia.AllotStatus and sd.StatusId ={0} ", commonStatusType);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" cia.AllotStatus>={0} ", entryStatus);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel CreateDirectStock(UserModel user, Model.CashInAllot allot, List<Model.CashInCorp> details, int stockLogId)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.WareHouse.DAL.StockLogDAL stockLogDAL = new WareHouse.DAL.StockLogDAL();
                NFMT.WareHouse.DAL.StockDAL stockDAL = new WareHouse.DAL.StockDAL();
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
                DAL.CashInAllotDAL allotDAL = new CashInAllotDAL();
                DAL.CashInDAL cashInDAL = new CashInDAL();
                DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
                DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取库存流水
                    result = stockLogDAL.Get(user, stockLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.StockLog stockLog = result.ReturnValue as NFMT.WareHouse.Model.StockLog;
                    if (stockLog == null || stockLog.StockLogId <= 0)
                    {
                        result.Message = "库存流水不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //获取库存
                    result = stockDAL.Get(user, stockLog.StockId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                    if (stock == null || stock.StockId <= 0)
                    {
                        result.Message = "库存不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //获取子合约
                    result = subDAL.Get(user, stockLog.SubContractId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.Message = "子合约不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    allot.AllotBala = details.Sum(temp => temp.AllotBala);
                    allot.Alloter = user.EmpId;
                    allot.AllotStatus = StatusEnum.已录入;
                    allot.AllotTime = DateTime.Now;
                    allot.AllotType = (int)NFMT.Funds.CashInAllotTypeEnum.Stock;
                    allot.CurrencyId = sub.SettleCurrency;

                    result = allotDAL.Insert(user, allot);
                    if (result.ResultStatus != 0)
                        return result;

                    int allotId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out allotId) || allotId <= 0)
                    {
                        result.Message = "收款分配新增失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //获取外部公司
                    NFMT.Contract.DAL.ContractCorporationDetailDAL corpDAL = new Contract.DAL.ContractCorporationDetailDAL();
                    result = corpDAL.LoadCorpListByContractId(user, sub.ContractId, false);
                    if (result.ResultStatus != 0)
                        return result;
                    List<NFMT.Contract.Model.ContractCorporationDetail> outCorps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
                    if (outCorps == null)
                    {
                        result.Message = "合约对方抬头获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    foreach (Model.CashInCorp corpDetail in details)
                    {
                        NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == corpDetail.CorpId);
                        if (corp == null || corp.CorpId <= 0)
                        {
                            result.Message = "收款分配公司不存在";
                            result.ResultStatus = -1;
                            return result;
                        }

                        if (!outCorps.Any(temp => temp.CorpId == corp.CorpId))
                        {
                            result.Message = "收款分配公司不在合约对方抬头，收款分配失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        result = cashInDAL.Get(user, corpDetail.CashInId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashIn cashIn = result.ReturnValue as Model.CashIn;
                        if (cashIn == null || cashIn.CashInId <= 0)
                        {
                            result.Message = "收款登记不存在";
                            result.ResultStatus = -1;
                            return result;
                        }

                        if (cashIn.CurrencyId != sub.SettleCurrency)
                        {
                            result.Message = "收款登记币种与合约币种不相同";
                            result.ResultStatus = -1;
                            return result;
                        }

                        corpDetail.AllotId = allotId;
                        corpDetail.BlocId = corp.ParentId;
                        corpDetail.CashInId = cashIn.CashInId;
                        corpDetail.CorpId = corp.CorpId;
                        corpDetail.DetailStatus = StatusEnum.已生效;
                        corpDetail.IsShare = false;

                        result = cashInCorpDAL.Insert(user, corpDetail);
                        if (result.ResultStatus != 0)
                            return result;

                        int corpRefId = 0;
                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out corpRefId) || corpRefId <= 0)
                        {
                            result.Message = "收款分配新增失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        Model.CashInContract cashInContract = new CashInContract();
                        cashInContract.AllotBala = corpDetail.AllotBala;
                        cashInContract.AllotId = corpDetail.AllotId;
                        cashInContract.CashInId = corpDetail.CashInId;
                        cashInContract.ContractId = sub.ContractId;
                        cashInContract.CorpRefId = corpRefId;
                        cashInContract.DetailStatus = StatusEnum.已生效;
                        cashInContract.SubContractId = sub.SubId;

                        result = cashInContractDAL.Insert(user, cashInContract);
                        if (result.ResultStatus != 0)
                            return result;

                        int contractRefId = 0;
                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out contractRefId) || contractRefId <= 0)
                        {
                            result.Message = "收款分配新增失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        Model.CashInStcok cashInStock = new CashInStcok();
                        cashInStock.AllotBala = cashInContract.AllotBala;
                        cashInStock.AllotId = cashInContract.AllotId;
                        cashInStock.CashInId = cashInContract.CashInId;
                        cashInStock.ContractRefId = contractRefId;
                        cashInStock.CorpRefId = cashInContract.CorpRefId;
                        cashInStock.DetailStatus = StatusEnum.已生效;
                        cashInStock.StockId = stockLog.StockId;
                        cashInStock.StockLogId = stockLog.StockLogId;
                        cashInStock.StockNameId = stockLog.StockNameId;

                        result = this.cashinstcokDAL.Insert(user, cashInStock);
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

        public ResultModel CreateContractStock(UserModel user, Model.CashInAllot allot, List<Model.CashInContract> details, int stockLogId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.CashInAllotDAL cashInAllotDAL = new CashInAllotDAL();
                DAL.CashInDAL cashInDAL = new CashInDAL();
                DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
                DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();
                NFMT.WareHouse.DAL.StockLogDAL stockLogDAL = new WareHouse.DAL.StockLogDAL();
                NFMT.WareHouse.DAL.StockDAL stockDAL = new WareHouse.DAL.StockDAL();
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
                NFMT.Contract.DAL.ContractCorporationDetailDAL corpDetailDAL = new Contract.DAL.ContractCorporationDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取库存流水
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

                    //获取库存
                    result = stockDAL.Get(user, stockLog.StockId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                    if (stock == null || stock.StockId <= 0)
                    {
                        result.Message = "库存不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //获取合约
                    result = subDAL.Get(user, stockLog.SubContractId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "合约不存在";
                        return result;
                    }

                    if (sub.SubStatus != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "合约状态不允许分配收款";
                        return result;
                    }

                    //获取外部公司
                    result = corpDetailDAL.LoadCorpListByContractId(user, sub.ContractId, false);
                    if (result.ResultStatus != 0)
                        return result;
                    List<NFMT.Contract.Model.ContractCorporationDetail> outCorps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
                    if (outCorps == null)
                    {
                        result.Message = "合约对方抬头获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    foreach (Model.CashInContract detail in details)
                    {
                        //收款登记验证
                        result = cashInDAL.Get(user, detail.CashInId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashIn cashIn = result.ReturnValue as Model.CashIn;
                        if (cashIn == null || cashIn.CashInId <= 0)
                        {
                            result.Message = "收款登记不存在";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //收款登记与合约币种验证
                        if (cashIn.CurrencyId != sub.SettleCurrency)
                        {
                            result.Message = "收款登记币种与合约币种不相同";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //获取公司收款分配
                        result = cashInCorpDAL.Get(user, detail.CorpRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashInCorp cashInCorp = result.ReturnValue as Model.CashInCorp;
                        if (cashInCorp == null || cashInCorp.RefId <= 0)
                        {
                            result.Message = "公司收款分配不存在";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //验证公司抬头
                        if (!outCorps.Any(temp => temp.CorpId == cashInCorp.CorpId))
                        {
                            result.Message = "收款分配公司不在合约对方抬头，收款分配失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //新增合约收款分配
                        detail.AllotId = cashInCorp.AllotId;
                        detail.CashInId = cashInCorp.CashInId;
                        detail.ContractId = sub.ContractId;
                        detail.CorpRefId = cashInCorp.RefId;
                        detail.DetailStatus = StatusEnum.已生效;
                        detail.SubContractId = sub.SubId;

                        result = cashInContractDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        int contractRefId = 0;
                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out contractRefId) || contractRefId <= 0)
                        {
                            result.Message = "新增收款分配失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //新增库存收款分配
                        Model.CashInStcok cashInStock = new CashInStcok();
                        cashInStock.AllotBala = detail.AllotBala;
                        cashInStock.AllotId = detail.AllotId;
                        cashInStock.CashInId = detail.CashInId;
                        cashInStock.ContractRefId = contractRefId;
                        cashInStock.CorpRefId = detail.CorpRefId;
                        cashInStock.DetailStatus = StatusEnum.已生效;
                        cashInStock.StockId = stockLog.StockId;
                        cashInStock.StockLogId = stockLog.StockLogId;
                        cashInStock.StockNameId = stock.StockNameId;

                        result = this.cashinstcokDAL.Insert(user, cashInStock);
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

            return result;
        }

        public ResultModel CreateStock(UserModel user, Model.CashInAllot allot, List<Model.CashInStcok> details, int stockLogId)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.Contract.DAL.ContractCorporationDetailDAL corpDetailDAL = new Contract.DAL.ContractCorporationDetailDAL();
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
                DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();
                DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
                DAL.CashInDAL cashInDAL = new CashInDAL();
                NFMT.WareHouse.DAL.StockLogDAL stockLogDAL = new WareHouse.DAL.StockLogDAL();
                NFMT.WareHouse.DAL.StockDAL stockDAL = new WareHouse.DAL.StockDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取库存流水
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

                    //获取库存
                    result = stockDAL.Get(user, stockLog.StockId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                    if (stock == null || stock.StockId <= 0)
                    {
                        result.Message = "库存不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //获取合约
                    result = subDAL.Get(user, stockLog.SubContractId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "合约不存在";
                        return result;
                    }

                    if (sub.SubStatus != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "合约状态不允许分配收款";
                        return result;
                    }

                    //获取外部公司
                    result = corpDetailDAL.LoadCorpListByContractId(user, sub.ContractId, false);
                    if (result.ResultStatus != 0)
                        return result;
                    List<NFMT.Contract.Model.ContractCorporationDetail> outCorps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
                    if (outCorps == null)
                    {
                        result.Message = "合约对方抬头获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    foreach (Model.CashInStcok detail in details)
                    {
                        //收款登记验证
                        result = cashInDAL.Get(user, detail.CashInId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashIn cashIn = result.ReturnValue as Model.CashIn;
                        if (cashIn == null || cashIn.CashInId <= 0)
                        {
                            result.Message = "收款登记不存在";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //收款登记与合约币种验证
                        if (cashIn.CurrencyId != sub.SettleCurrency)
                        {
                            result.Message = "收款登记币种与合约币种不相同";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //获取合约收款分配
                        result = cashInContractDAL.Get(user, detail.ContractRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashInContract cashInContract = result.ReturnValue as Model.CashInContract;
                        if (cashInContract == null || cashInContract.RefId <= 0)
                        {
                            result.Message = "收款分配失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        if (cashInContract.DetailStatus != StatusEnum.已生效)
                        {
                            result.Message = "收款分配失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //获取公司收款分配
                        result = cashInCorpDAL.Get(user, cashInContract.CorpRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashInCorp cashInCorp = result.ReturnValue as Model.CashInCorp;
                        if (cashInCorp == null || cashInCorp.RefId <= 0 || cashInCorp.DetailStatus != StatusEnum.已生效)
                        {
                            result.Message = "收款分配失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //验证公司抬头
                        if (!outCorps.Any(temp => temp.CorpId == cashInCorp.CorpId))
                        {
                            result.Message = "收款分配公司不在合约对方抬头，收款分配失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //新增库存收款分配
                        detail.AllotId = cashInContract.AllotId;
                        detail.CashInId = cashInContract.CashInId;
                        detail.ContractRefId = cashInContract.RefId;
                        detail.CorpRefId = cashInContract.CorpRefId;
                        detail.DetailStatus = StatusEnum.已生效;
                        detail.StockId = stock.StockId;
                        detail.StockLogId = stockLog.StockLogId;
                        detail.StockNameId = stock.StockNameId;

                        result = this.cashinstcokDAL.Insert(user, detail);
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

            return result;
        }

        public SelectModel GetCurDetailsSelect(int pageIndex, int pageSize, string orderStr, int stockLogId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cisr.RefId";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" cisr.RefId as DetailId,cisr.ContractRefId,cicr.CorpRefId,cicr.AllotId,cicr.CashInId,cicr.ContractId,cicr.SubContractId ");
            sb.Append(" ,ci.CashInDate,ci.CashInCorpId,inCorp.CorpName as InCorp,ci.PayCorpName as OutCorp ");
            sb.Append(" ,ci.CashInBank,ban.BankName as CashInBankName,ci.CashInBala,cisr.AllotBala,ci.CurrencyId,cur.CurrencyName ");
            sb.Append(" ,ref.CorpId as AllotCorpId,allotCorp.CorpName as AllotCorp ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_CashInStcok_Ref cisr ");
            sb.AppendFormat(" inner join dbo.Fun_CashIn ci on cisr.CashInId = ci.CashInId and ci.CashInStatus ={0} ", readyStatus);
            sb.Append(" inner join dbo.Fun_CashInCorp_Ref ref on cisr.CorpRefId = ref.RefId ");
            sb.Append(" inner join dbo.Fun_CashInContract_Ref cicr on cicr.RefId = cisr.ContractRefId ");
            sb.Append(" left join NFMT_User.dbo.Corporation allotCorp on allotCorp.CorpId = ref.CorpId ");

            sb.Append(" left join NFMT_User.dbo.Corporation inCorp on inCorp.CorpId = ci.CashInCorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank ban on ban.BankId = ci.CashInBank ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = ci.CurrencyId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" cisr.StockLogId ={0} ", stockLogId);
            sb.AppendFormat(" and cicr.DetailStatus={0} ", readyStatus);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel UpdateStock(UserModel user, List<Model.CashInStcok> details, int stockLogId)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.WareHouse.DAL.StockDAL stockDAL = new WareHouse.DAL.StockDAL();
                NFMT.WareHouse.DAL.StockLogDAL stockLogDAL = new WareHouse.DAL.StockLogDAL();
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
                NFMT.Contract.DAL.ContractCorporationDetailDAL corpDetailDAL = new Contract.DAL.ContractCorporationDetailDAL();
                DAL.CashInDAL cashInDAL = new CashInDAL();
                DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
                DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取库存流水
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

                    //获取库存
                    result = stockDAL.Get(user, stockLog.StockId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                    if (stock == null || stock.StockId <= 0)
                    {
                        result.Message = "库存不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //获取合约
                    result = subDAL.Get(user, stockLog.SubContractId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "合约不存在";
                        return result;
                    }

                    if (sub.SubStatus != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "合约状态不允许分配收款";
                        return result;
                    }

                    //获取外部公司
                    result = corpDetailDAL.LoadCorpListByContractId(user, sub.ContractId, false);
                    if (result.ResultStatus != 0)
                        return result;
                    List<NFMT.Contract.Model.ContractCorporationDetail> outCorps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
                    if (outCorps == null)
                    {
                        result.Message = "合约对方抬头获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //获取库存流水所有有效配款明细
                    result = this.cashinstcokDAL.Load(user, stockLog.StockLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInStcok> resultDetails = result.ReturnValue as List<Model.CashInStcok>;
                    if (resultDetails == null)
                    {
                        result.Message = "获取明细失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //作废现有明细
                    foreach (Model.CashInStcok detail in resultDetails)
                    {
                        if (detail.DetailStatus == StatusEnum.已生效)
                            detail.DetailStatus = StatusEnum.已录入;

                        result = this.cashinstcokDAL.Invalid(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //添加新明细
                    foreach (Model.CashInStcok detail in details)
                    {
                        //获取合约收款分配
                        result = cashInContractDAL.Get(user, detail.ContractRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashInContract cashInContract = result.ReturnValue as Model.CashInContract;
                        if (cashInContract == null || cashInContract.RefId <= 0)
                        {
                            result.Message = "收款分配失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        if (cashInContract.DetailStatus != StatusEnum.已生效)
                        {
                            result.Message = "收款分配失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //获取公司收款分配
                        result = cashInCorpDAL.Get(user, cashInContract.CorpRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashInCorp cashInCorp = result.ReturnValue as Model.CashInCorp;
                        if (cashInCorp == null || cashInCorp.RefId <= 0 || cashInCorp.DetailStatus != StatusEnum.已生效)
                        {
                            result.Message = "收款分配失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //收款登记验证
                        result = cashInDAL.Get(user, cashInContract.CashInId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashIn cashIn = result.ReturnValue as Model.CashIn;
                        if (cashIn == null || cashIn.CashInId <= 0)
                        {
                            result.Message = "收款登记不存在";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //收款登记与合约币种验证
                        if (cashIn.CurrencyId != sub.SettleCurrency)
                        {
                            result.Message = "收款登记币种与合约币种不相同";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //验证公司抬头
                        if (!outCorps.Any(temp => temp.CorpId == cashInCorp.CorpId))
                        {
                            result.Message = "收款分配公司不在合约对方抬头，收款分配失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //新增库存收款分配
                        detail.AllotId = cashInContract.AllotId;
                        detail.CashInId = cashInContract.CashInId;
                        detail.ContractRefId = cashInContract.RefId;
                        detail.CorpRefId = cashInContract.CorpRefId;
                        detail.DetailStatus = StatusEnum.已生效;
                        detail.StockId = stock.StockId;
                        detail.StockLogId = stockLog.StockLogId;
                        detail.StockNameId = stock.StockNameId;

                        result = this.cashinstcokDAL.Insert(user, detail);
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

            return result;
        }

        public ResultModel LoadByAllot(UserModel user, int allotId, NFMT.Common.StatusEnum status)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.cashinstcokDAL.LoadByAllot(user, allotId, status);
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

        public ResultModel Audit(UserModel user, NFMT.WorkFlow.Model.DataSource dataSource, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.CashInAllotDAL cashInAllotDAL = new CashInAllotDAL();
                DAL.CashInDAL cashInDAL = new CashInDAL();
                Model.CashIn cashIn = null;
                DAL.FundsLogDAL fundsLogDAL = new FundsLogDAL();
                DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = cashInAllotDAL.Get(user, dataSource.RowId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashInAllot cashInAllot = result.ReturnValue as Model.CashInAllot;
                    if (cashInAllot == null || cashInAllot.AllotId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收款分配不存在";
                        return result;
                    }

                    //审核，修改数据状态
                    result = cashInAllotDAL.Audit(user, cashInAllot, isPass);
                    if (result.ResultStatus != 0)
                        return result;

                    //审核通过
                    if (isPass)
                    {
                        result = this.cashinstcokDAL.LoadByAllot(user, cashInAllot.AllotId, NFMT.Common.StatusEnum.已生效);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.CashInStcok> cashInStcoks = result.ReturnValue as List<Model.CashInStcok>;
                        if (cashInStcoks == null || !cashInStcoks.Any())
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取明细失败";
                            return result;
                        }

                        foreach (Model.CashInStcok cashInStcok in cashInStcoks)
                        {
                            result = cashInDAL.Get(user, cashInStcok.CashInId);
                            if (result.ResultStatus != 0)
                                return result;

                            cashIn = result.ReturnValue as Model.CashIn;
                            if (cashIn == null)
                            {
                                result.ResultStatus = -1;
                                result.Message = "获取收款失败";
                                return result;
                            }

                            result = cashInContractDAL.Get(user, cashInStcok.ContractRefId);
                            if (result.ResultStatus != 0)
                                return result;

                            Model.CashInContract cashInContract = result.ReturnValue as Model.CashInContract;
                            if (cashInContract == null)
                            {
                                result.ResultStatus = -1;
                                result.Message = "获取失败";
                                return result;
                            }

                            result = fundsLogDAL.Insert(user, new FundsLog()
                            {
                                //FundsLogId
                                ContractId = cashInContract.ContractId,
                                SubId = cashInContract.SubContractId,
                                //InvoiceId
                                LogDate = DateTime.Now,
                                InBlocId = cashIn.CashInBlocId,
                                InCorpId = cashIn.CashInCorpId,
                                InBankId = cashIn.CashInBank,
                                InAccountId = cashIn.CashInAccoontId,
                                OutBlocId = cashIn.PayBlocId,
                                OutCorpId = cashIn.PayCorpId,
                                OutBankId = cashIn.PayBankId,
                                OutBank = cashIn.PayBank,
                                OutAccountId = cashIn.PayAccountId,
                                OutAccount = cashIn.PayAccount,
                                FundsBala = cashInStcok.AllotBala,
                                //FundsType 
                                CurrencyId = cashInAllot.CurrencyId,
                                LogDirection = (int)NFMT.WareHouse.LogDirectionEnum.In,
                                LogType = (int)NFMT.WareHouse.LogTypeEnum.收款,
                                //PayMode 
                                //IsVirtualPay
                                FundsDesc = cashInAllot.AllotDesc,
                                OpPerson = user.EmpId,
                                LogSourceBase = "NFMT",
                                LogSource = "dbo.Fun_CashInContract_Ref",
                                SourceId = dataSource.RowId,
                                LogStatus = StatusEnum.已生效
                            });
                            if (result.ResultStatus != 0)
                                return result;

                            int fundsLogId = (int)result.ReturnValue;

                            cashInStcok.FundsLogId = fundsLogId;
                            result = cashinstcokDAL.Update(user, cashInStcok);
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

        public ResultModel Invalid(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.CashInAllotDAL cashInAllotDAL = new CashInAllotDAL();
                DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
                DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = cashInAllotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashInAllot cashInAllot = result.ReturnValue as Model.CashInAllot;
                    if (cashInAllot == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    //作废付款分配
                    result = cashInAllotDAL.Invalid(user, cashInAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    result = cashinstcokDAL.LoadByAllot(user, allotId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInStcok> cashInStcoks = result.ReturnValue as List<Model.CashInStcok>;
                    if (cashInStcoks == null || !cashInStcoks.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取明细失败";
                        return result;
                    }

                    foreach (Model.CashInStcok cashInStcok in cashInStcoks)
                    {
                        //作废明细
                        result = cashinstcokDAL.Invalid(user, cashInStcok);
                        if (result.ResultStatus != 0)
                            return result;

                        result = cashInContractDAL.Get(user, cashInStcok.ContractRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashInContract cashInContract = result.ReturnValue as Model.CashInContract;
                        if (cashInContract == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取失败";
                            return result;
                        }

                        result = cashInContractDAL.Invalid(user, cashInContract);
                        if (result.ResultStatus != 0)
                            return result;

                        result = cashInCorpDAL.Get(user, cashInContract.CorpRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashInCorp cashInCorp = result.ReturnValue as Model.CashInCorp;
                        if (cashInCorp == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取失败";
                            return result;
                        }

                        result = cashInCorpDAL.Invalid(user, cashInCorp);
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
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Complete(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.CashInAllotDAL cashInAllotDAL = new CashInAllotDAL();
                DAL.FundsLogDAL fundsLogDAL = new FundsLogDAL();
                Model.FundsLog fundsLog = null;
                DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
                DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = cashInAllotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashInAllot cashInAllot = result.ReturnValue as Model.CashInAllot;
                    if (cashInAllot == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    //完成付款分配
                    result = cashInAllotDAL.Complete(user, cashInAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    result = cashinstcokDAL.LoadByAllot(user, allotId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInStcok> cashInStcoks = result.ReturnValue as List<Model.CashInStcok>;
                    if (cashInStcoks == null || !cashInStcoks.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取明细失败";
                        return result;
                    }

                    foreach (Model.CashInStcok cashInStcok in cashInStcoks)
                    {
                        //完成明细
                        result = cashinstcokDAL.Complete(user, cashInStcok);
                        if (result.ResultStatus != 0)
                            return result;

                        result = cashInContractDAL.Get(user, cashInStcok.ContractRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashInContract cashInContract = result.ReturnValue as Model.CashInContract;
                        if (cashInContract == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取失败";
                            return result;
                        }

                        result = cashInContractDAL.Complete(user, cashInContract);
                        if (result.ResultStatus != 0)
                            return result;

                        result = cashInCorpDAL.Get(user, cashInContract.CorpRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashInCorp cashInCorp = result.ReturnValue as Model.CashInCorp;
                        if (cashInCorp == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取失败";
                            return result;
                        }

                        result = cashInCorpDAL.Complete(user, cashInCorp);
                        if (result.ResultStatus != 0)
                            return result;

                        result = fundsLogDAL.Get(user, cashInStcok.FundsLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        fundsLog = result.ReturnValue as Model.FundsLog;
                        if (fundsLog == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取资金流水失败";
                            return result;
                        }

                        //完成流水
                        result = fundsLogDAL.Complete(user, fundsLog);
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
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel CompleteCancel(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.CashInAllotDAL cashInAllotDAL = new CashInAllotDAL();
                DAL.FundsLogDAL fundsLogDAL = new FundsLogDAL();
                Model.FundsLog fundsLog = null;
                DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
                DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = cashInAllotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashInAllot cashInAllot = result.ReturnValue as Model.CashInAllot;
                    if (cashInAllot == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    //完成撤销付款分配
                    result = cashInAllotDAL.CompleteCancel(user, cashInAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    result = cashinstcokDAL.LoadByAllot(user, allotId, StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInStcok> cashInStcoks = result.ReturnValue as List<Model.CashInStcok>;
                    if (cashInStcoks == null || !cashInStcoks.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取明细失败";
                        return result;
                    }

                    foreach (Model.CashInStcok cashInStcok in cashInStcoks)
                    {
                        //完成撤销明细
                        result = cashinstcokDAL.CompleteCancel(user, cashInStcok);
                        if (result.ResultStatus != 0)
                            return result;

                        result = cashInContractDAL.Get(user, cashInStcok.ContractRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashInContract cashInContract = result.ReturnValue as Model.CashInContract;
                        if (cashInContract == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取失败";
                            return result;
                        }

                        result = cashInContractDAL.CompleteCancel(user, cashInContract);
                        if (result.ResultStatus != 0)
                            return result;

                        result = cashInCorpDAL.Get(user, cashInContract.CorpRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashInCorp cashInCorp = result.ReturnValue as Model.CashInCorp;
                        if (cashInCorp == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取失败";
                            return result;
                        }

                        result = cashInCorpDAL.CompleteCancel(user, cashInCorp);
                        if (result.ResultStatus != 0)
                            return result;

                        result = fundsLogDAL.Get(user, cashInStcok.FundsLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        fundsLog = result.ReturnValue as Model.FundsLog;
                        if (fundsLog == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取资金流水失败";
                            return result;
                        }

                        //完成撤销流水
                        result = fundsLogDAL.CompleteCancel(user, fundsLog);
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
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Close(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.CashInAllotDAL cashInAllotDAL = new CashInAllotDAL();
                DAL.FundsLogDAL fundsLogDAL = new FundsLogDAL();
                Model.FundsLog fundsLog = null;
                DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
                DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = cashInAllotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashInAllot cashInAllot = result.ReturnValue as Model.CashInAllot;
                    if (cashInAllot == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    //关闭付款分配
                    result = cashInAllotDAL.Close(user, cashInAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    result = cashinstcokDAL.LoadByAllot(user, allotId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInStcok> cashInStcoks = result.ReturnValue as List<Model.CashInStcok>;
                    if (cashInStcoks == null || !cashInStcoks.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取明细失败";
                        return result;
                    }

                    foreach (Model.CashInStcok cashInStcok in cashInStcoks)
                    {
                        //关闭明细
                        result = cashinstcokDAL.Close(user, cashInStcok);
                        if (result.ResultStatus != 0)
                            return result;

                        result = cashInContractDAL.Get(user, cashInStcok.ContractRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashInContract cashInContract = result.ReturnValue as Model.CashInContract;
                        if (cashInContract == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取失败";
                            return result;
                        }

                        result = cashInContractDAL.Close(user, cashInContract);
                        if (result.ResultStatus != 0)
                            return result;

                        result = cashInCorpDAL.Get(user, cashInContract.CorpRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashInCorp cashInCorp = result.ReturnValue as Model.CashInCorp;
                        if (cashInCorp == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取失败";
                            return result;
                        }

                        result = cashInCorpDAL.Close(user, cashInCorp);
                        if (result.ResultStatus != 0)
                            return result;

                        result = fundsLogDAL.Get(user, cashInStcok.FundsLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        fundsLog = result.ReturnValue as Model.FundsLog;
                        if (fundsLog == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取资金流水失败";
                            return result;
                        }

                        //关闭流水
                        result = fundsLogDAL.Close(user, fundsLog);
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
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel GetStockInfoByAlotId(UserModel user, int allotId,NFMT.Common.StatusEnum status)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = cashinstcokDAL.GetStockInfoByAlotId(user, allotId, status);
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

        #endregion
    }
}
