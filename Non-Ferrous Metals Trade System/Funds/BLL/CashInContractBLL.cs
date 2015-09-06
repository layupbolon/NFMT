/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInContractBLL.cs
// 文件功能描述：收款分配至合约dbo.Fun_CashInContract_Ref业务逻辑类。
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
    /// 收款分配至合约dbo.Fun_CashInContract_Ref业务逻辑类。
    /// </summary>
    public class CashInContractBLL : Common.ExecBLL
    {
        private CashInContractDAL cashincontractDAL = new CashInContractDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CashInContractDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CashInContractBLL()
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
            get { return this.cashincontractDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetAllotContractSelect(int pageIndex, int pageSize, string orderStr, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cia.AllotId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;
            int readyStatus = (int)StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" cs.SubId,cs.SubNo,cs.ContractId,inCorp.CorpId as InCorpId,inCorp.CorpName as InCorpName,outCorp.CorpId as OutCorpId,outCorp.CorpName as OutCorpName ");
            sb.Append(" , ref.SumBala,cs.SettleCurrency,cur.CurrencyName,cs.ContractDate,cs.SubStatus,sd.StatusName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Con_ContractSub cs ");
            sb.Append(" inner join dbo.Con_Contract con on cs.ContractId = con.ContractId ");
            sb.Append(" inner join (select SUM(AllotBala) as SumBala,SubContractId from dbo.Fun_CashInContract_Ref ");
            sb.AppendFormat(" where DetailStatus >={0} group by SubContractId) as ref on ref.SubContractId = cs.SubId ", readyStatus);
            sb.AppendFormat(" inner join dbo.Con_ContractCorporationDetail inCorp on inCorp.ContractId = cs.ContractId and inCorp.IsDefaultCorp= 1 and inCorp.IsInnerCorp = 1 and inCorp.DetailStatus={0} ", readyStatus);
            sb.AppendFormat(" inner join dbo.Con_ContractCorporationDetail outCorp on outCorp.ContractId = cs.ContractId and outCorp.IsDefaultCorp =1 and outCorp.IsInnerCorp =0 and outCorp.DetailStatus = {0} ", readyStatus);
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cs.SettleCurrency = cur.CurrencyId  ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = cs.SubStatus and sd.StatusId={0} ", statusId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");
            if (status > 0)
                sb.AppendFormat(" and cs.SubStatus = {0} ", status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetAllotReadyContractListSelect(int pageIndex, int pageSize, string orderStr, string contractNo, int outCorpId, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cs.SubId desc";
            else
                select.OrderStr = orderStr;

            int tradeDirectionStyleId = (int)NFMT.Data.StyleEnum.贸易方向;
            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;
            int statusType = (int)NFMT.Common.StatusTypeEnum.通用状态;
            int priceModeStyleId = (int)NFMT.Data.StyleEnum.PriceMode;
            int tradeDirection = NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.贸易方向)["Sell"].StyleDetailId;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" cs.SubId,cs.ContractId,cs.ContractDate,con.ContractNo,con.OutContractNo,con.TradeDirection,tradeDirection.DetailName as TradeDirectionName,inCorp.CorpId as InCorpId , inCorp.CorpName as InCorpName,outCorp.CorpId as OutCorpId , outCorp.CorpName as OutCorpName,con.AssetId,ass.AssetName,cs.SignAmount,cs.UnitId,cast(cs.SignAmount as varchar(20))+mu.MUName as ContractWeight,cs.PriceMode,priceMode.DetailName as PriceModeName,cs.SubStatus,subStatus.StatusName");
            sb.Append(",ref.SumBala,CAST(isnull(ref.SumBala,0) as varchar) + cur.CurrencyName as AllotBala,cur.CurrencyId");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Con_ContractSub cs ");
            sb.Append(" left join dbo.Con_SubDetail sd on cs.SubId = sd.SubId ");
            sb.Append(" left join dbo.Con_SubPrice sp on cs.SubId = sp.SubId ");
            sb.Append(" left join dbo.Con_Contract con on cs.ContractId = con.ContractId ");
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail outCorp on con.ContractId = outCorp.ContractId and outCorp.IsInnerCorp= 0 and outCorp.IsDefaultCorp =1 and outCorp.DetailStatus={0} ", readyStatus);
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail inCorp on con.ContractId = inCorp.ContractId and inCorp.IsInnerCorp=1 and inCorp.IsDefaultCorp=1 and inCorp.DetailStatus = {0} ", readyStatus);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail tradeDirection on con.TradeDirection = tradeDirection.StyleDetailId and tradeDirection.BDStyleId={0}  ", tradeDirectionStyleId);
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on con.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on cs.UnitId = mu.MUId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail priceMode on cs.PriceMode = priceMode.StyleDetailId and priceMode.BDStyleId ={0} ", priceModeStyleId);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail subStatus on cs.SubStatus = subStatus.DetailId and subStatus.StatusId={0} ", statusType);
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cs.SettleCurrency = cur.CurrencyId");

            //sb.Append(" left join (select crr.SubContractId,SUM(rad.AllotBala) as SumBala from dbo.Fun_ContractReceivable_Ref crr  ");
            //sb.Append(" inner join dbo.Fun_RecAllotDetail rad on crr.DetailId = rad.DetailId and rad.DetailStatus>=20 group by crr.SubContractId) ");
            sb.Append(" left join (select SubContractId,SUM(AllotBala) as SumBala from dbo.Fun_CashInContract_Ref ");
            sb.AppendFormat(" where DetailStatus>={0} group by SubContractId) ", readyStatus);
            sb.Append(" as ref on ref.SubContractId = cs.SubId ");

            select.TableName = sb.ToString();

            sb.Clear();

            //sb.AppendFormat(" c.TradeDirection = {0} ", tradeDirection);
            sb.Append(" 1=1 ");

            if (!string.IsNullOrEmpty(contractNo))
                sb.AppendFormat(" and cs.SubNo like '%{0}%' ", contractNo);
            if (outCorpId > 0)
                sb.AppendFormat(" and outCorp.CorpId = {0} ", outCorpId);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and cs.ContractDate between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetOtherAllotInContract(int pageIndex, int pageSize, string orderStr, int subId, int allotId = 0)
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
            sb.Append("cia.AllotId,cia.AllotTime,cia.Alloter,emp.Name as AlloterName,cia.AllotBala,cia.CurrencyId,cur.CurrencyName");
            sb.Append(",cia.AllotStatus,sd.StatusName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_CashInAllot cia ");

            //sb.Append(" inner join(select ciad.AllotId from dbo.Fun_CashInAllotDetail ciad ");
            //sb.AppendFormat(" inner join dbo.Fun_CashInContract_Ref cicr on cicr.DetailId = ciad.DetailId and cicr.SubContractId = {0} ", subId);
            //sb.AppendFormat(" where ciad.AllotId != {0} and ciad.DetailStatus>={1} group by ciad.AllotId) as ref on ref.AllotId = cia.AllotId ", allotId,readyStatus);


            sb.AppendFormat(" inner join(select cicr.AllotId from dbo.Fun_CashInContract_Ref cicr where cicr.SubContractId ={0} ", subId);
            sb.AppendFormat(" and cicr.AllotId != {0} and cicr.DetailStatus>={1} group by cicr.AllotId) as ref on ref.AllotId = cia.AllotId ", allotId, readyStatus);

            sb.Append(" left join NFMT_User.dbo.Employee emp on cia.Alloter = emp.EmpId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = cia.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = cia.AllotStatus and sd.StatusId ={0} ", commonStatusType);

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" cia.AllotStatus>={0} ", entryStatus);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel CreateDirectContract(UserModel user, CashInAllot allot, List<CashInContractDirect> directs, int subId)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
                DAL.CashInDAL cashInDAL = new CashInDAL();
                DAL.CashInAllotDAL cashInAllotDAL = new CashInAllotDAL();
                DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取子合约
                    result = subDAL.Get(user, subId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约不存在，不能进行收款分配";
                        return result;
                    }

                    //判断合约状态

                    //获取合约对方抬头
                    result = subDAL.GetContractOutCorp(user, sub.SubId);
                    if (result.ResultStatus != 0)
                        return result;

                    DataTable outCorpTable = result.ReturnValue as DataTable;

                    if (outCorpTable == null || outCorpTable.Rows.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "合约对方抬头不存在，收款分配失败";
                        return result;
                    }

                    List<int> outCorpIds = new List<int>();
                    foreach (DataRow dr in outCorpTable.Rows)
                    {
                        int outCorpId = 0;
                        if (dr["CorpId"] == DBNull.Value || !int.TryParse(dr["CorpId"].ToString(), out outCorpId) || outCorpId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "合约对方抬头获取失败";
                            return result;
                        }

                        outCorpIds.Add(outCorpId);
                    }

                    List<Model.CashInCorp> cashCorps = new List<CashInCorp>();

                    foreach (CashInContractDirect direct in directs)
                    {
                        //判断收款分配到的对方抬头是否包含在合约抬头中
                        if (!outCorpIds.Contains(direct.AllotCorpId))
                        {
                            result.ResultStatus = -1;
                            result.Message = "收款分配选择的分配公司不在该合约对方抬头中，收款分配失败";
                            return result;
                        }

                        //判断收款登记的币种是否和合约币种相同
                        result = cashInDAL.Get(user, direct.CashInId);
                        if (result.ResultStatus != 0)
                            return result;
                        Model.CashIn cashIn = result.ReturnValue as Model.CashIn;
                        if (cashIn == null || cashIn.CashInId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "收款登记不存在";
                            return result;
                        }

                        if (cashIn.CashInStatus != StatusEnum.已生效)
                        {
                            result.ResultStatus = -1;
                            result.Message = "收款登记状态错误，不能进行分配";
                            return result;
                        }

                        if (cashIn.CurrencyId != sub.SettleCurrency)
                        {
                            result.ResultStatus = -1;
                            result.Message = "收款登记币种与合约币种不相同，分配错误";
                            return result;
                        }

                        CashInCorp cashCorp = new CashInCorp();

                        NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == direct.AllotCorpId);
                        if (corp == null || corp.CorpId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "收款分配选择的分配公司不存在，分配失败";
                            return result;
                        }

                        cashCorp.AllotBala = direct.AllotBala;
                        cashCorp.BlocId = corp.ParentId;
                        cashCorp.CashInId = direct.CashInId;
                        cashCorp.CorpId = corp.CorpId;
                        cashCorp.DetailStatus = StatusEnum.已生效;
                        cashCorp.IsShare = false;
                        cashCorp.AllotId = 0;

                        cashCorps.Add(cashCorp);

                    }

                    decimal sumBala = directs.Sum(temp => temp.AllotBala);

                    allot.AllotBala = sumBala;
                    allot.Alloter = user.EmpId;
                    allot.AllotStatus = StatusEnum.已录入;
                    allot.AllotTime = DateTime.Now;
                    allot.AllotType = (int)NFMT.Funds.CashInAllotTypeEnum.Contract;
                    allot.CurrencyId = sub.SettleCurrency;

                    //新增主分配
                    result = cashInAllotDAL.Insert(user, allot);
                    if (result.ResultStatus != 0)
                        return result;

                    int allotId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out allotId) || allotId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收款分配新增失败";
                        return result;
                    }

                    //新增公司收款分配
                    foreach (Model.CashInCorp cd in cashCorps)
                    {
                        cd.AllotId = allotId;
                        result = cashInCorpDAL.Insert(user, cd);
                        if (result.ResultStatus != 0)
                            return result;

                        int refId = 0;
                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out refId) || refId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "公司收款分配新增失败";
                            return result;
                        }

                        CashInContract cashContract = new CashInContract();

                        cashContract.AllotBala = cd.AllotBala;
                        cashContract.AllotId = cd.AllotId;
                        cashContract.CashInId = cd.CashInId;
                        cashContract.ContractId = sub.ContractId;
                        cashContract.CorpRefId = refId;
                        cashContract.DetailStatus = StatusEnum.已生效;
                        cashContract.SubContractId = sub.SubId;

                        result = this.cashincontractDAL.Insert(user, cashContract);
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

        public ResultModel CreateContract(UserModel user, List<CashInContract> details, int subId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取子合约
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

                    //验证子合约状态

                    foreach (CashInContract detail in details)
                    {
                        //获取公司分配
                        result = cashInCorpDAL.Get(user, detail.CorpRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashInCorp cashInCorp = result.ReturnValue as Model.CashInCorp;
                        if (cashInCorp == null || cashInCorp.RefId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "公司款不存在";
                            return result;
                        }

                        if (cashInCorp.DetailStatus != StatusEnum.已生效)
                        {
                            result.ResultStatus = -1;
                            result.Message = "公司款状态错误，分配失败";
                            return result;
                        }

                        detail.AllotId = cashInCorp.AllotId;
                        detail.CashInId = cashInCorp.CashInId;
                        detail.ContractId = sub.ContractId;
                        detail.CorpRefId = cashInCorp.RefId;
                        detail.DetailStatus = StatusEnum.已生效;
                        detail.SubContractId = sub.SubId;

                        result = cashincontractDAL.Insert(user, detail);
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

        public SelectModel GetCurDetailsSelect(int pageIndex, int pageSize, string orderStr, int subId, NFMT.Common.StatusEnum status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cicr.RefId asc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" cicr.RefId as DetailId,cicr.CorpRefId,cicr.AllotId,cicr.CashInId,cicr.ContractId,cicr.SubContractId ");
            sb.Append(" ,ci.CashInDate,ci.CashInCorpId,inCorp.CorpName as InCorp,ci.PayCorpName as OutCorp ");
            sb.Append(" ,ci.CashInBank,ban.BankName as CashInBankName,ci.CashInBala,cicr.AllotBala,ci.CurrencyId,cur.CurrencyName ");
            sb.Append(" ,ref.CorpId as AllotCorpId,allotCorp.CorpName as AllotCorp ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_CashInContract_Ref cicr ");
            sb.AppendFormat(" inner join dbo.Fun_CashIn ci on cicr.CashInId = ci.CashInId and ci.CashInStatus ={0} ", readyStatus);

            sb.Append(" inner join dbo.Fun_CashInCorp_Ref ref on cicr.CorpRefId = ref.RefId ");
            sb.Append(" left join NFMT_User.dbo.Corporation allotCorp on allotCorp.CorpId = ref.CorpId ");

            sb.Append(" left join NFMT_User.dbo.Corporation inCorp on inCorp.CorpId = ci.CashInCorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank ban on ban.BankId = ci.CashInBank ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = ci.CurrencyId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" cicr.SubContractId ={0} ", subId);
            sb.AppendFormat(" and cicr.DetailStatus={0} ", (int)status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel UpdateContract(UserModel user, List<CashInContract> details, int subId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.CashInDAL cashInDAL = new CashInDAL();
                DAL.CashInAllotDAL cashInAllotDAL = new CashInAllotDAL();
                DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
                DAL.CashInStcokDAL cashInStockDAL = new CashInStcokDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取子合约
                    result = subDAL.Get(user, subId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.Message = "子合约不存在，不能进行修改";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //获取当前合约下的有效明细
                    result = this.cashincontractDAL.Load(user, sub.SubId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInContract> resultDetails = result.ReturnValue as List<Model.CashInContract>;

                    if (resultDetails == null)
                    {
                        result.Message = "获取明细失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //作废现有有效明细
                    foreach (Model.CashInContract nd in resultDetails)
                    {
                        //验证当前合约明细是否已分配至库存中
                        result = cashInStockDAL.LoadByContractRefId(user, nd.RefId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.CashInStcok> stocks = result.ReturnValue as List<Model.CashInStcok>;
                        if (stocks != null && stocks.Count > 0)
                        {
                            result.Message = "合约款已全部或部分配款至库存，不能进行修改";
                            result.ResultStatus = -1;
                            return result;
                        }

                        if (nd.DetailStatus == StatusEnum.已生效)
                            nd.DetailStatus = StatusEnum.已录入;

                        result = this.cashincontractDAL.Invalid(user, nd);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //新增明细
                    foreach (Model.CashInContract det in details)
                    {
                        //获取公司分配
                        result = cashInCorpDAL.Get(user, det.CorpRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashInCorp cashInCrop = result.ReturnValue as Model.CashInCorp;
                        if (cashInCrop == null || cashInCrop.RefId <= 0)
                        {
                            result.Message = "修改失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //获取分配
                        result = cashInAllotDAL.Get(user, cashInCrop.AllotId);
                        if (result.ResultStatus != 0)
                            return result;

                        NFMT.Funds.Model.CashInAllot allot = result.ReturnValue as NFMT.Funds.Model.CashInAllot;
                        if (allot == null || allot.AllotId <= 0)
                        {
                            result.Message = "收款分配不存在，不能进行修改";
                            result.ResultStatus = -1;
                            return result;
                        }

                        //获取收款登记
                        result = cashInDAL.Get(user, cashInCrop.CashInId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashIn cashIn = result.ReturnValue as Model.CashIn;
                        if (cashIn == null || cashIn.CashInId <= 0)
                        {
                            result.Message = "修改失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        if (cashIn.CurrencyId != sub.SettleCurrency)
                        {
                            result.Message = "修改失败";
                            result.ResultStatus = -1;
                            return result;
                        }

                        det.AllotId = cashInCrop.AllotId;
                        det.CorpRefId = cashInCrop.RefId;
                        det.ContractId = sub.ContractId;
                        det.CashInId = cashIn.CashInId;
                        det.CorpRefId = cashInCrop.RefId;
                        det.DetailStatus = StatusEnum.已生效;
                        det.SubContractId = sub.SubId;

                        result = this.cashincontractDAL.Insert(user, det);

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

        public SelectModel GetContractAllotLastSelect(int pageIndex, int pageSize, string orderStr, string contractRefids, string stockRefIds, int subId, int currencyId = 0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            if (string.IsNullOrEmpty(stockRefIds))
                stockRefIds = "0";
            if (string.IsNullOrEmpty(contractRefids))
                contractRefids = "0";
            if (subId <= 0)
                return null;

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ci.CashInId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" 0 as DetailId,ci.CashInId,cicr.RefId as ContractRefId,ci.CashInDate,ci.CashInCorpId,inCorp.CorpName as InCorp ");
            sb.Append(" ,ci.PayCorpId,outCorp.CorpName as OutCorp,ci.CashInBank,ban.BankName as CashInBankName ");
            sb.Append(" ,ci.CurrencyId,cur.CurrencyName,ci.CashInBala,cicr.AllotBala as CorpAllotBala ");
            sb.Append(" ,isnull(ref.SumBala,0) as SumBala,cicr.AllotBala - ISNULL(ref.SumBala,0) as LastBala ");
            sb.Append(" ,cicr.AllotBala - ISNULL(ref.SumBala,0) as AllotBala ");
            sb.Append(" ,corpRef.CorpId,allotCorp.CorpName as AllotCorp,cs.SubId,cs.SubNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_CashInContract_Ref cicr ");

            sb.Append(" inner join dbo.Fun_CashInCorp_Ref corpRef on cicr.CorpRefId = corpRef.RefId ");
            sb.Append(" inner join dbo.Fun_CashIn ci on cicr.CashInId = ci.CashInId ");

            sb.Append(" left join (select SUM(ref.AllotBala) as SumBala,ref.ContractRefId from dbo.Fun_CashInStcok_Ref ref ");
            sb.AppendFormat(" where ref.DetailStatus={0} and ref.RefId not in ({1}) group by ref.ContractRefId) as ref on ref.ContractRefId = cicr.RefId ", readyStatus, stockRefIds);

            sb.Append(" left join NFMT_User.dbo.Corporation inCorp on ci.CashInCorpId = inCorp.CorpId ");
            sb.Append(" left join NFMT_User.dbo.Corporation outCorp on ci.PayCorpId = outCorp.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank ban on ban.BankId = ci.CashInBank ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = ci.CurrencyId ");

            sb.Append(" left join NFMT_User.dbo.Corporation allotCorp on allotCorp.CorpId = corpRef.CorpId ");
            sb.Append(" left join dbo.Con_ContractSub cs on cs.SubId = cicr.SubContractId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" cicr.SubContractId ={0} ", subId);
            sb.AppendFormat(" and ci.CashInStatus ={0} ", readyStatus);
            sb.AppendFormat(" and cicr.DetailStatus ={0} ", readyStatus);
            sb.AppendFormat(" and cicr.RefId not in ({0}) ", contractRefids);
            sb.Append(" and cicr.AllotBala>isnull(ref.SumBala,0) ");

            if (currencyId > 0)
                sb.AppendFormat(" and ci.CurrencyId={0} ", currencyId);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel GetByAllot(UserModel user, int allotId, StatusEnum status)
        {
            ResultModel result = this.cashincontractDAL.LoadByAllot(user, allotId, status);

            if (result.ResultStatus == 0)
            {
                List<Model.CashInContract> details = result.ReturnValue as List<Model.CashInContract>;
                if (details != null && details.Count > 0)
                    result.ReturnValue = details[0];
            }

            return result;
        }

        public ResultModel Create(UserModel user, CashInAllot allot, List<CashInContractDirect> directs, int subId)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
                NFMT.WareHouse.DAL.StockLogDAL stockLogDAL = new WareHouse.DAL.StockLogDAL();

                DAL.CashInDAL cashInDAL = new CashInDAL();
                DAL.CashInAllotDAL cashInAllotDAL = new CashInAllotDAL();
                DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
                DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();
                DAL.CashInStcokDAL cashInStockDAL = new CashInStcokDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    if (directs == null || directs.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "未分配任务收款";
                        return result;
                    }

                    //获取子合约
                    result = subDAL.Get(user, subId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约不存在，不能进行收款分配";
                        return result;
                    }

                    //判断合约状态
                    if (sub.SubStatus != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约非已生效状态，不能进行收款分配";
                        return result;
                    }

                    //获取合约对方抬头
                    result = subDAL.GetContractOutCorp(user, sub.SubId);
                    if (result.ResultStatus != 0)
                        return result;

                    DataTable outCorpTable = result.ReturnValue as DataTable;

                    if (outCorpTable == null || outCorpTable.Rows.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "合约对方抬头不存在，收款分配失败";
                        return result;
                    }

                    List<int> outCorpIds = new List<int>();
                    foreach (DataRow dr in outCorpTable.Rows)
                    {
                        int outCorpId = 0;
                        if (dr["CorpId"] == DBNull.Value || !int.TryParse(dr["CorpId"].ToString(), out outCorpId) || outCorpId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "合约对方抬头获取失败";
                            return result;
                        }

                        outCorpIds.Add(outCorpId);
                    }

                    //获取合约关联库存信息
                    result = stockLogDAL.LoadStockLogBySubId(user, sub.SubId);
                    if (result.ResultStatus != 0)
                        return result;
                    List<NFMT.WareHouse.Model.StockLog> stockLogs = result.ReturnValue as List<NFMT.WareHouse.Model.StockLog>;
                    if (stockLogs == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "合约库存流水获取失败";
                        return result;
                    }

                    decimal sumBala = directs.Sum(temp => temp.AllotBala);

                    allot.AllotBala = sumBala;
                    allot.Alloter = user.EmpId;
                    allot.AllotStatus = StatusEnum.已录入;
                    allot.AllotTime = DateTime.Now;
                    allot.AllotType = (int)NFMT.Funds.CashInAllotTypeEnum.Contract;
                    allot.CurrencyId = sub.SettleCurrency;

                    //新增主分配
                    result = cashInAllotDAL.Insert(user, allot);
                    if (result.ResultStatus != 0)
                        return result;

                    int allotId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out allotId) || allotId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收款分配新增失败";
                        return result;
                    }

                    foreach (CashInContractDirect direct in directs)
                    {
                        //判断收款分配到的对方抬头是否包含在合约抬头中
                        if (!outCorpIds.Contains(direct.AllotCorpId))
                        {
                            result.ResultStatus = -1;
                            result.Message = "收款分配选择的分配公司不在该合约对方抬头中，收款分配失败";
                            return result;
                        }

                        //判断收款登记的币种是否和合约币种相同
                        result = cashInDAL.Get(user, direct.CashInId);
                        if (result.ResultStatus != 0)
                            return result;
                        Model.CashIn cashIn = result.ReturnValue as Model.CashIn;
                        if (cashIn == null || cashIn.CashInId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "收款登记不存在";
                            return result;
                        }

                        if (cashIn.CashInStatus != StatusEnum.已生效)
                        {
                            result.ResultStatus = -1;
                            result.Message = "收款登记状态错误，不能进行分配";
                            return result;
                        }

                        if (cashIn.CurrencyId != sub.SettleCurrency)
                        {
                            result.ResultStatus = -1;
                            result.Message = "收款登记币种与合约币种不相同，分配错误";
                            return result;
                        }

                        CashInCorp cashCorp = new CashInCorp();

                        NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == direct.AllotCorpId);
                        if (corp == null || corp.CorpId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "收款分配选择的分配公司不存在，分配失败";
                            return result;
                        }

                        //新增公司分配
                        cashCorp.AllotBala = direct.AllotBala;
                        cashCorp.BlocId = corp.ParentId;
                        cashCorp.CashInId = direct.CashInId;
                        cashCorp.CorpId = corp.CorpId;
                        cashCorp.DetailStatus = StatusEnum.已生效;
                        cashCorp.IsShare = false;
                        cashCorp.AllotId = allotId;

                        result = cashInCorpDAL.Insert(user, cashCorp);
                        if (result.ResultStatus != 0)
                            return result;

                        int corpRefId = 0;
                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out corpRefId) || corpRefId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "公司收款分配新增失败";
                            return result;
                        }

                        //新增合约分配
                        CashInContract cashContract = new CashInContract();

                        cashContract.AllotBala = cashCorp.AllotBala;
                        cashContract.AllotId = cashCorp.AllotId;
                        cashContract.CashInId = cashCorp.CashInId;
                        cashContract.ContractId = sub.ContractId;
                        cashContract.CorpRefId = corpRefId;
                        cashContract.DetailStatus = StatusEnum.已生效;
                        cashContract.SubContractId = sub.SubId;

                        result = this.cashincontractDAL.Insert(user, cashContract);
                        if (result.ResultStatus != 0)
                            return result;

                        int contractRefId = 0;
                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out contractRefId) || contractRefId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "合约收款分配新增失败";
                            return result;
                        }

                        //新增库存分配
                        if (direct.StockLogId > 0)
                        {
                            result = stockLogDAL.Get(user, direct.StockLogId);
                            if (result.ResultStatus != 0)
                                return result;

                            NFMT.WareHouse.Model.StockLog stockLog = result.ReturnValue as NFMT.WareHouse.Model.StockLog;
                            if (stockLog == null || stockLog.StockLogId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "库存流水不存在";
                                return result;
                            }

                            if (stockLog.SubContractId != sub.SubId)
                            {
                                result.ResultStatus = -1;
                                result.Message = "库存流水与合约不匹配";
                                return result;
                            }

                            Model.CashInStcok cashInStock = new CashInStcok();
                            cashInStock.AllotBala = cashContract.AllotBala;
                            cashInStock.AllotId = cashContract.AllotId;
                            cashInStock.CashInId = cashContract.CashInId;
                            cashInStock.ContractRefId = contractRefId;
                            cashInStock.CorpRefId = cashContract.CorpRefId;
                            cashInStock.DetailStatus = StatusEnum.已生效;
                            cashInStock.StockLogId = stockLog.StockLogId;
                            cashInStock.StockId = stockLog.StockId;
                            cashInStock.StockNameId = stockLog.StockNameId;

                            result = cashInStockDAL.Insert(user, cashInStock);
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
                        result = this.cashincontractDAL.LoadDetail(user, cashInAllot.AllotId, NFMT.Common.StatusEnum.已生效);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.CashInContract> cashInContracts = result.ReturnValue as List<Model.CashInContract>;
                        if (cashInContracts == null || !cashInContracts.Any())
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取明细失败";
                            return result;
                        }

                        foreach (Model.CashInContract cashInContract in cashInContracts)
                        {
                            result = cashInDAL.Get(user, cashInContract.CashInId);
                            if (result.ResultStatus != 0)
                                return result;

                            cashIn = result.ReturnValue as Model.CashIn;
                            if (cashIn == null)
                            {
                                result.ResultStatus = -1;
                                result.Message = "获取收款失败";
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
                                FundsBala = cashInContract.AllotBala,
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

                            cashInContract.FundsLogId = fundsLogId;
                            result = cashincontractDAL.Update(user, cashInContract);
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

                    result = cashincontractDAL.LoadDetail(user, allotId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInContract> cashInContracts = result.ReturnValue as List<Model.CashInContract>;
                    if (cashInContracts == null || !cashInContracts.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取明细失败";
                        return result;
                    }

                    foreach (Model.CashInContract cashInContract in cashInContracts)
                    {
                        //作废 付款分配合约
                        result = cashincontractDAL.Invalid(user, cashInContract);
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

                        //作废 付款分配公司
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

                    result = cashincontractDAL.LoadDetail(user, allotId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInContract> cashInContracts = result.ReturnValue as List<Model.CashInContract>;
                    if (cashInContracts == null || !cashInContracts.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取明细失败";
                        return result;
                    }

                    foreach (Model.CashInContract cashInContract in cashInContracts)
                    {
                        //完成明细
                        result = cashincontractDAL.Complete(user, cashInContract);
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

                        //作废 付款分配公司
                        result = cashInCorpDAL.Complete(user, cashInCorp);
                        if (result.ResultStatus != 0)
                            return result;

                        result = fundsLogDAL.Get(user, cashInContract.FundsLogId);
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

                    result = cashincontractDAL.LoadDetail(user, allotId, StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInContract> cashInContracts = result.ReturnValue as List<Model.CashInContract>;
                    if (cashInContracts == null || !cashInContracts.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取明细失败";
                        return result;
                    }

                    foreach (Model.CashInContract cashInContract in cashInContracts)
                    {
                        //完成撤销明细
                        result = cashincontractDAL.CompleteCancel(user, cashInContract);
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

                        //完成 付款分配公司
                        result = cashInCorpDAL.CompleteCancel(user, cashInCorp);
                        if (result.ResultStatus != 0)
                            return result;

                        result = fundsLogDAL.Get(user, cashInContract.FundsLogId);
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

                    result = cashincontractDAL.LoadDetail(user, allotId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInContract> cashInContracts = result.ReturnValue as List<Model.CashInContract>;
                    if (cashInContracts == null || !cashInContracts.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取明细失败";
                        return result;
                    }

                    foreach (Model.CashInContract cashInContract in cashInContracts)
                    {
                        //关闭明细
                        result = cashincontractDAL.Close(user, cashInContract);
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

                        //完成 付款分配公司
                        result = cashInCorpDAL.Close(user, cashInCorp);
                        if (result.ResultStatus != 0)
                            return result;

                        result = fundsLogDAL.Get(user, cashInContract.FundsLogId);
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

        #endregion
    }
}
