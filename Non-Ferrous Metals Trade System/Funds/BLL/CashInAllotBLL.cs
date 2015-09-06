/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInAllotBLL.cs
// 文件功能描述：收款分配dbo.Fun_CashInAllot业务逻辑类。
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
    /// 收款分配dbo.Fun_CashInAllot业务逻辑类。
    /// </summary>
    public class CashInAllotBLL : Common.ExecBLL
    {
        private CashInAllotDAL cashinallotDAL = new CashInAllotDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CashInAllotDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CashInAllotBLL()
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
            get { return this.cashinallotDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetAllotSelect(int pageIndex, int pageSize, string orderStr, int empId, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cia.AllotId desc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" cia.AllotId,cia.AllotTime,cia.AllotType,cia.AllotDesc,CONVERT(varchar,cia.AllotBala) + c.CurrencyName as AllotBala,e.Name as AlloterName,cia.AllotStatus,bd.StatusName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_CashInAllot cia ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on cia.CurrencyId = c.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on cia.Alloter =e.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on cia.AllotStatus = bd.DetailId and StatusId = {0} ", statusId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");
            if (status > 0)
                sb.AppendFormat(" and cia.AllotStatus = {0} ", status);
            if (empId > 0)
                sb.AppendFormat(" and cia.Alloter = {0} ", empId);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetCashInLastSelect(int pageIndex, int pageSize, string orderStr, string detailIds = "", string cashInIds = "", int currenctId = 0, int subId = 0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            if (string.IsNullOrEmpty(detailIds))
                detailIds = "0";
            if (string.IsNullOrEmpty(cashInIds))
                cashInIds = "0";

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ci.CashInId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("0 as DetailId,ci.CashInId,ci.CashInDate,ci.CashInCorpId,inCorp.CorpName as InCorp");
            sb.Append(",ci.PayCorpId,outCorp.CorpName as OutCorp,ci.CashInBank,ban.BankName as CashInBankName");
            sb.Append(",ci.CurrencyId,cur.CurrencyName,ci.CashInBala,isnull(ref.SumBala,0) as SumBala,ci.CashInBala - ISNULL(ref.SumBala,0) as LastBala");
            sb.Append(",ci.CashInBala - ISNULL(ref.SumBala,0) as AllotBala,allot.CorpId as AllotCorpId,allot.CorpName as AllotCorp");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_CashIn ci ");

            sb.Append(" left join (select SUM(cicr.AllotBala) as SumBala,cicr.CashInId  from dbo.Fun_CashInCorp_Ref cicr ");
            sb.AppendFormat(" where cicr.DetailStatus>={0} and cicr.RefId not in ({1}) group by cicr.CashInId  ) as ref on ref.CashInId = ci.CashInId ", readyStatus, detailIds);

            sb.Append(" left join NFMT_User.dbo.Corporation inCorp on ci.CashInCorpId = inCorp.CorpId ");
            sb.Append(" left join NFMT_User.dbo.Corporation outCorp on ci.PayCorpId = outCorp.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank ban on ban.BankId = ci.CashInBank ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = ci.CurrencyId ");

            sb.Append(" left join (select ac.CorpId,ac.CorpName,cs.SettleCurrency from dbo.Con_ContractSub cs ");
            sb.AppendFormat(" inner join dbo.Con_ContractCorporationDetail ac on cs.ContractId = ac.ContractId and ac.DetailStatus >={0} ", readyStatus);
            sb.Append(" and ac.IsDefaultCorp =1 and ac.IsInnerCorp = 0 ");
            sb.AppendFormat(" where cs.SubId = {0}) as allot on allot.SettleCurrency = ci.CurrencyId", subId);

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" ci.CashInStatus ={0} ", readyStatus);
            sb.AppendFormat(" and ci.CashInId not in ({0}) ", cashInIds);
            sb.Append(" and ci.CashInBala>isnull(ref.SumBala,0) ");

            if (currenctId > 0)
                sb.AppendFormat(" and ci.CurrencyId={0} ", currenctId);

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 创建分配主表和明细表
        /// </summary>
        /// <param name="user"></param>
        /// <param name="rec"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public ResultModel CreateMain(UserModel user, ref Model.CashInAllot allot, ref List<Model.CashInAllotDetail> details)
        {
            ResultModel result = new ResultModel();

            if (details.Count == 0)
            {
                result.ResultStatus = -1;
                result.Message = "新增收款分配失败，未分配任何款项";
                return result;
            }

            //验证明细币种是否相同
            int currencyId = 0;
            NFMT.Funds.DAL.CashInDAL cashInDAL = new CashInDAL();
            foreach (Model.CashInAllotDetail detail in details)
            {
                result = cashInDAL.Get(user, detail.CashInId);
                if (result.ResultStatus != 0)
                    return result;

                Model.CashIn cashIn = result.ReturnValue as Model.CashIn;
                if (cashIn == null || cashIn.CashInId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "分配的收款登记不存在";
                    return result;
                }

                if (cashIn.CashInStatus != StatusEnum.已生效)
                {
                    result.ResultStatus = -1;
                    result.Message = "分配的收款登记状态错误";
                    return result;
                }

                if (currencyId == 0)
                    currencyId = cashIn.CurrencyId;

                if (currencyId != cashIn.CurrencyId)
                {
                    result.ResultStatus = -1;
                    result.Message = "分配的收款登记币种不相同";
                    return result;
                }
            }

            decimal sumBala = details.Sum(temp => temp.AllotBala);

            allot.AllotBala = sumBala;
            allot.AllotStatus = StatusEnum.已录入;
            allot.Alloter = user.EmpId;
            allot.AllotTime = DateTime.Now;
            allot.CurrencyId = currencyId;

            result = this.cashinallotDAL.Insert(user, allot);
            if (result.ResultStatus != 0)
                return result;

            int allotId = 0;
            if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out allotId))
            {
                result.ResultStatus = -1;
                result.Message = "收款分配失败";
                return result;
            }

            DAL.CashInAllotDetailDAL detailDAL = new CashInAllotDetailDAL();
            foreach (Model.CashInAllotDetail detail in details)
            {
                int detailId = 0;

                detail.DetailStatus = StatusEnum.已生效;
                detail.AllotId = allotId;
                result = detailDAL.Insert(user, detail);
                if (result.ResultStatus != 0)
                    return result;

                if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out detailId))
                {
                    result.ResultStatus = -1;
                    result.Message = "收款分配明细新增失败";
                    return result;
                }

                detail.DetailId = detailId;
            }

            return result;
        }

        /// <summary>
        /// 创建分配公司关联表
        /// </summary>
        /// <param name="user"></param>
        /// <param name="rec"></param>
        /// <param name="details"></param>
        /// <param name="corpDetail"></param>
        /// <returns></returns>
        public ResultModel CreateCorp(UserModel user, Model.CashInAllot allot, List<Model.CashInCorp> details)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.CashInCorpDAL corpDAL = new CashInCorpDAL();
                NFMT.Funds.DAL.CashInDAL cashInDAL = new CashInDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    if (details.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "新增收款分配失败，未分配任何款项";
                        return result;
                    }

                    //验证明细币种是否相同
                    int currencyId = 0;

                    foreach (Model.CashInCorp detail in details)
                    {
                        result = cashInDAL.Get(user, detail.CashInId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashIn cashIn = result.ReturnValue as Model.CashIn;
                        if (cashIn == null || cashIn.CashInId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "分配的收款登记不存在";
                            return result;
                        }

                        if (cashIn.CashInStatus != StatusEnum.已生效)
                        {
                            result.ResultStatus = -1;
                            result.Message = "分配的收款登记状态错误";
                            return result;
                        }

                        if (currencyId == 0)
                            currencyId = cashIn.CurrencyId;

                        if (currencyId != cashIn.CurrencyId)
                        {
                            result.ResultStatus = -1;
                            result.Message = "分配的收款登记币种不相同";
                            return result;
                        }
                    }

                    decimal sumBala = details.Sum(temp => temp.AllotBala);

                    allot.AllotBala = sumBala;
                    allot.AllotStatus = StatusEnum.已录入;
                    allot.Alloter = user.EmpId;
                    allot.AllotTime = DateTime.Now;
                    allot.CurrencyId = currencyId;
                    allot.AllotType = (int)CashInAllotTypeEnum.Corp;

                    result = this.cashinallotDAL.Insert(user, allot);
                    if (result.ResultStatus != 0)
                        return result;

                    int allotId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out allotId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "收款分配失败";
                        return result;
                    }

                    NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == details[0].CorpId);
                    if (corp == null || corp.CorpId <= 0)
                    {
                        result.Message = "公司不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    foreach (Model.CashInCorp detail in details)
                    {
                        detail.AllotId = allotId;
                        detail.BlocId = corp.ParentId;
                        detail.CorpId = corp.CorpId;
                        detail.DetailStatus = StatusEnum.已生效;

                        result = corpDAL.Insert(user, detail);
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
        /// 修改分配主表和明细表
        /// </summary>
        /// <param name="user"></param>
        /// <param name="rec"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public ResultModel UpdateMain(UserModel user, Model.CashInAllot allot, ref List<Model.CashInAllotDetail> details)
        {
            ResultModel result = new ResultModel();

            if (details.Count == 0)
            {
                result.ResultStatus = -1;
                result.Message = "修改收款分配失败，未分配任何款项";
                return result;
            }

            //验证明细币种是否相同
            int currencyId = 0;
            NFMT.Funds.DAL.CashInDAL cashInDAL = new CashInDAL();
            foreach (Model.CashInAllotDetail detail in details)
            {
                result = cashInDAL.Get(user, detail.CashInId);
                if (result.ResultStatus != 0)
                    return result;

                Model.CashIn cashIn = result.ReturnValue as Model.CashIn;
                if (cashIn == null || cashIn.CashInId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "分配的收款登记不存在";
                    return result;
                }

                if (cashIn.CashInStatus != StatusEnum.已生效)
                {
                    result.ResultStatus = -1;
                    result.Message = "分配的收款登记状态错误";
                    return result;
                }

                if (currencyId == 0)
                    currencyId = cashIn.CurrencyId;

                if (currencyId != cashIn.CurrencyId)
                {
                    result.ResultStatus = -1;
                    result.Message = "分配的收款登记币种不相同";
                    return result;
                }
            }

            //获取分配
            result = this.cashinallotDAL.Get(user, allot.AllotId);
            if (result.ResultStatus != 0)
                return result;

            Model.CashInAllot resultObj = result.ReturnValue as Model.CashInAllot;
            if (resultObj == null || resultObj.AllotId <= 0)
            {
                result.Message = "分配不存在，不能进行修改";
                result.ResultStatus = -1;
                return result;
            }

            //更新主表
            decimal sumBala = details.Sum(temp => temp.AllotBala);

            resultObj.AllotBala = sumBala;
            resultObj.Alloter = user.EmpId;
            resultObj.AllotTime = DateTime.Now;
            resultObj.CurrencyId = currencyId;

            result = this.cashinallotDAL.Update(user, resultObj);
            if (result.ResultStatus != 0)
                return result;

            //获取原有明细并作废
            DAL.CashInAllotDetailDAL detailDAL = new CashInAllotDetailDAL();
            result = detailDAL.Load(user, resultObj.AllotId);
            if (result.ResultStatus != 0)
                return result;

            List<Model.CashInAllotDetail> resultDetails = result.ReturnValue as List<Model.CashInAllotDetail>;
            if (resultDetails == null)
            {
                result.Message = "获取明细失败";
                result.ResultStatus = -1;
                return result;
            }

            foreach (Model.CashInAllotDetail detail in resultDetails)
            {
                if (detail.DetailStatus == StatusEnum.已生效)
                    detail.DetailStatus = StatusEnum.已录入;

                result = detailDAL.Invalid(user, detail);
                if (result.ResultStatus != 0)
                    return result;
            }

            foreach (Model.CashInAllotDetail detail in details)
            {
                int detailId = 0;

                detail.DetailStatus = StatusEnum.已生效;
                detail.AllotId = resultObj.AllotId;
                result = detailDAL.Insert(user, detail);
                if (result.ResultStatus != 0)
                    return result;

                if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out detailId))
                {
                    result.ResultStatus = -1;
                    result.Message = "收款分配明细新增失败";
                    return result;
                }

                detail.DetailId = detailId;
            }

            return result;
        }

        /// <summary>
        /// 创建分配公司关联表
        /// </summary>
        /// <param name="user"></param>
        /// <param name="rec"></param>
        /// <param name="details"></param>
        /// <param name="corpDetail"></param>
        /// <returns></returns>
        public ResultModel UpdateCorp(UserModel user, Model.CashInAllot allot, List<Model.CashInCorp> details)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.CashInCorpDAL corpDAL = new CashInCorpDAL();
                NFMT.Funds.DAL.CashInDAL cashInDAL = new CashInDAL();
                DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    if (details.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "修改收款分配失败，未分配任何款项";
                        return result;
                    }

                    //获取分配
                    result = this.cashinallotDAL.Get(user, allot.AllotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashInAllot resultObj = result.ReturnValue as Model.CashInAllot;
                    if (resultObj == null || resultObj.AllotId <= 0)
                    {
                        result.Message = "分配不存在，不能进行修改";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //验证明细币种是否相同
                    int currencyId = 0;

                    foreach (Model.CashInCorp detail in details)
                    {
                        result = cashInDAL.Get(user, detail.CashInId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CashIn cashIn = result.ReturnValue as Model.CashIn;
                        if (cashIn == null || cashIn.CashInId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "分配的收款登记不存在";
                            return result;
                        }

                        if (cashIn.CashInStatus != StatusEnum.已生效)
                        {
                            result.ResultStatus = -1;
                            result.Message = "分配的收款登记状态错误";
                            return result;
                        }

                        if (currencyId == 0)
                            currencyId = cashIn.CurrencyId;

                        if (currencyId != cashIn.CurrencyId)
                        {
                            result.ResultStatus = -1;
                            result.Message = "分配的收款登记币种不相同";
                            return result;
                        }
                    }

                    //更新主表
                    decimal sumBala = details.Sum(temp => temp.AllotBala);

                    resultObj.AllotBala = sumBala;
                    resultObj.Alloter = user.EmpId;
                    resultObj.AllotTime = DateTime.Now;
                    resultObj.CurrencyId = currencyId;

                    result = this.cashinallotDAL.Update(user, resultObj);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取原有明细并作废
                    result = corpDAL.Load(user, resultObj.AllotId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInCorp> resultDetails = result.ReturnValue as List<Model.CashInCorp>;
                    if (resultDetails == null)
                    {
                        result.Message = "获取明细失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    foreach (Model.CashInCorp detail in resultDetails)
                    {
                        //验证公司款是否已分配至合约中
                        result = cashInContractDAL.LoadByCorpRefId(user, detail.RefId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.CashInContract> contracts = result.ReturnValue as List<Model.CashInContract>;
                        if (contracts != null && contracts.Count > 0)
                        {
                            result.Message = "公司款已全部或部分已分配至合约中，不能进行修改";
                            result.ResultStatus = -1;
                            return result;
                        }

                        if (detail.DetailStatus == StatusEnum.已生效)
                            detail.DetailStatus = StatusEnum.已录入;

                        result = corpDAL.Invalid(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //添加新明细
                    NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == resultDetails[0].CorpId);
                    if (corp == null || corp.CorpId <= 0)
                    {
                        result.Message = "公司不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    foreach (Model.CashInCorp detail in details)
                    {
                        detail.AllotId = resultObj.AllotId;
                        detail.BlocId = corp.ParentId;
                        detail.CorpId = corp.CorpId;
                        detail.DetailStatus = StatusEnum.已生效;

                        result = corpDAL.Insert(user, detail);
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


        public ResultModel Goback(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取主分配
                    result = this.cashinallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashInAllot allot = result.ReturnValue as Model.CashInAllot;
                    if (allot == null || allot.AllotId <= 0)
                    {
                        result.Message = "收款分配不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = this.cashinallotDAL.Goback(user, allot);
                    if (result.ResultStatus != 0)
                        return result;

                    //工作流任务关闭
                    WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, allot);
                    if (result.ResultStatus != 0)
                        return result;

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

        public ResultModel Complete(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //验证主分配
                    result = this.cashinallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashInAllot allot = result.ReturnValue as Model.CashInAllot;
                    if (allot == null || allot.AllotId <= 0)
                    {
                        result.Message = "收款分配不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //获取当前分配在库存款存中的剩余
                    result = this.cashinallotDAL.GetLastBalaByAllotId(user, allot.AllotId);
                    if (result.ResultStatus != 0)
                        return result;

                    object obj = result.ReturnValue;
                    decimal lastBala = 0;
                    if (result.ReturnValue == null || !decimal.TryParse(result.ReturnValue.ToString(), out lastBala))
                    {
                        result.Message = "获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    if (lastBala > 0)
                    {
                        result.Message = "有余款未分配，不能关闭";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //获取公司分配明细并完成
                    CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
                    result = cashInCorpDAL.Load(user, allot.AllotId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInCorp> cashInCorps = result.ReturnValue as List<Model.CashInCorp>;
                    if (cashInCorps == null || cashInCorps.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "公司分配明细获取失败";
                        return result;
                    }

                    foreach (Model.CashInCorp cashInCorp in cashInCorps)
                    {
                        result = cashInCorpDAL.Complete(user, cashInCorp);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取合约分配明细并完成
                    DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();
                    result = cashInContractDAL.LoadByAllot(user, allot.AllotId);
                    if (result.ResultStatus != 0)
                        return result;
                    List<Model.CashInContract> cashInContracts = result.ReturnValue as List<Model.CashInContract>;
                    if (cashInContracts == null || cashInContracts.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "合约分配明细获取失败";
                        return result;
                    }

                    foreach (Model.CashInContract cashInContract in cashInContracts)
                    {
                        result = cashInContractDAL.Complete(user, cashInContract);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取库存分配明细并完成
                    DAL.CashInStcokDAL cashInStockDAL = new CashInStcokDAL();
                    result = cashInStockDAL.LoadByAllot(user, allot.AllotId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInStcok> cashInStocks = result.ReturnValue as List<Model.CashInStcok>;
                    if (cashInStocks == null || cashInStocks.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "库存分配明细获取失败";
                        return result;
                    }

                    foreach (Model.CashInStcok cashInStock in cashInStocks)
                    {
                        result = cashInStockDAL.Complete(user, cashInStock);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //完成主分配
                    result = this.cashinallotDAL.Complete(user, allot);
                    if (result.ResultStatus != 0)
                        return result;

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

        public ResultModel CompleteCancel(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.CashInAllotDetailDAL detailDAL = new CashInAllotDetailDAL();
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //验证主分配
                    result = this.cashinallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashInAllot allot = result.ReturnValue as Model.CashInAllot;
                    if (allot == null || allot.AllotId <= 0)
                    {
                        result.Message = "收款分配不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //获取有效明细
                    result = detailDAL.Load(user, allot.AllotId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInAllotDetail> details = result.ReturnValue as List<Model.CashInAllotDetail>;
                    if (details == null)
                    {
                        result.Message = "获取明细失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //明细执行完成撤销
                    foreach (Model.CashInAllotDetail detail in details)
                    {
                        result = detailDAL.CompleteCancel(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //主分配执行完成撤销
                    result = this.cashinallotDAL.CompleteCancel(user, allot);
                    if (result.ResultStatus != 0)
                        return result;

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

        public ResultModel Close(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.CashInAllotDetailDAL detailDAL = new CashInAllotDetailDAL();
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //验证主分配
                    result = this.cashinallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashInAllot allot = result.ReturnValue as Model.CashInAllot;
                    if (allot == null || allot.AllotId <= 0)
                    {
                        result.Message = "收款分配不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //获取有效明细
                    result = detailDAL.Load(user, allot.AllotId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInAllotDetail> details = result.ReturnValue as List<Model.CashInAllotDetail>;
                    if (details == null)
                    {
                        result.Message = "获取明细失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //明细关闭
                    foreach (Model.CashInAllotDetail detail in details)
                    {
                        result = detailDAL.Close(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //完成主分配
                    result = this.cashinallotDAL.Close(user, allot);
                    if (result.ResultStatus != 0)
                        return result;

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

        public SelectModel GetCashInAllotLastSelect(int pageIndex, int pageSize, string orderStr, string detailIds = "", string cashInIds = "", int currenctId = 0, int subId = 0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            if (string.IsNullOrEmpty(detailIds))
                detailIds = "0";
            if (string.IsNullOrEmpty(cashInIds))
                cashInIds = "0";

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ci.CashInId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("0 as DetailId,ci.CashInId,ci.CashInDate,ci.CashInCorpId,inCorp.CorpName as InCorp");
            sb.Append(",ci.PayCorpId,outCorp.CorpName as OutCorp,ci.CashInBank,ban.BankName as CashInBankName");
            sb.Append(",ci.CurrencyId,cur.CurrencyName,ci.CashInBala,isnull(ref.SumBala,0) as SumBala,ci.CashInBala - ISNULL(ref.SumBala,0) as LastBala");
            sb.Append(",ci.CashInBala - ISNULL(ref.SumBala,0) as AllotBala,allot.CorpId as AllotCorpId,allot.CorpName as AllotCorp");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_CashIn ci ");

            sb.Append(" left join (select SUM(cicr.AllotBala) as SumBala,cicr.CashInId  from dbo.Fun_CashInCorp_Ref cicr ");
            sb.AppendFormat(" where cicr.DetailStatus>={0} and cicr.RefId not in ({1}) group by cicr.CashInId  ) as ref on ref.CashInId = ci.CashInId ", readyStatus, detailIds);

            sb.Append(" left join NFMT_User.dbo.Corporation inCorp on ci.CashInCorpId = inCorp.CorpId ");
            sb.Append(" left join NFMT_User.dbo.Corporation outCorp on ci.PayCorpId = outCorp.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank ban on ban.BankId = ci.CashInBank ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = ci.CurrencyId ");

            sb.Append(" left join (select ac.CorpId,ac.CorpName,cs.SettleCurrency from dbo.Con_ContractSub cs ");
            sb.AppendFormat(" inner join dbo.Con_ContractCorporationDetail ac on cs.ContractId = ac.ContractId and ac.DetailStatus >={0} ", readyStatus);
            sb.Append(" and ac.IsDefaultCorp =1 and ac.IsInnerCorp = 0 ");
            sb.AppendFormat(" where cs.SubId = {0}) as allot on allot.SettleCurrency = ci.CurrencyId", subId);

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" ci.CashInStatus ={0} ", readyStatus);
            sb.AppendFormat(" and ci.CashInId not in ({0}) ", cashInIds);
            sb.Append(" and ci.CashInBala>isnull(ref.SumBala,0) ");

            if (currenctId > 0)
                sb.AppendFormat(" and ci.CurrencyId={0} ", currenctId);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel LoadAllotBySubId(UserModel user, int subId)
        {
            return this.cashinallotDAL.LoadAllotBySubId(user, subId);
        }

        #endregion

        #region report

        public SelectModel GetCustomReportSelect(int pageIndex, int pageSize, string orderStr, int cashInId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ci.CashInId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)Common.StatusEnum.已生效;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("ci.CashInId,");
            sb.Append("corp.CorpName,CONVERT(varchar,corpAllotInfo.corpAllotBala)+corpCur.CurrencyName as corpAllotBala,");
            sb.Append("con.ContractNo,CONVERT(varchar,conAllotInfo.conAllotBala)+conCur.CurrencyName as conAllotBala,");
            sb.Append("sn.RefNo,CONVERT(varchar,stAllotInfo.stAllotBala)+stCur.CurrencyName as stAllotBala ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT..Fun_CashIn ci ");
            sb.Append(" left join (select corpRef.CashInId,corpRef.CorpId,SUM(ISNULL(corpRef.AllotBala,0))as corpAllotBala,corpAllot.CurrencyId  ");
            sb.Append(" 		   from NFMT..Fun_CashInAllot corpAllot  ");
            sb.Append(" 		   inner join NFMT..Fun_CashInCorp_Ref corpRef on corpRef.AllotId = corpAllot.AllotId ");
            sb.AppendFormat(" 	   where corpAllot.AllotStatus >={0} and corpRef.DetailStatus >= {0} ", readyStatus);
            sb.Append(" 		   group by corpRef.CashInId,corpRef.CorpId,corpAllot.CurrencyId) corpAllotInfo on corpAllotInfo.CashInId = ci.CashInId ");
            sb.Append(" left join NFMT_User..Corporation corp on corp.CorpId = corpAllotInfo.CorpId ");
            sb.Append(" left join NFMT_Basic..Currency corpCur on corpCur.CurrencyId = corpAllotInfo.CurrencyId ");
            sb.Append(" left join (select conRef.CashInId,conRef.ContractId,SUM(ISNULL(conRef.AllotBala,0))as conAllotBala,conAllot.CurrencyId ");
            sb.Append("            from NFMT..Fun_CashInAllot conAllot ");
            sb.Append("            inner join NFMT..Fun_CashInContract_Ref conRef on conRef.AllotId = conAllot.AllotId ");
            sb.AppendFormat("      where conAllot.AllotStatus >={0} and conRef.DetailStatus >={0} ", readyStatus);
            sb.Append("            group by conRef.CashInId,conRef.ContractId,conAllot.CurrencyId) conAllotInfo on conAllotInfo.CashInId = ci.CashInId ");
            sb.Append(" left join NFMT..Con_Contract con on con.ContractId = conAllotInfo.ContractId ");
            sb.Append(" left join NFMT_Basic..Currency conCur on conCur.CurrencyId = conAllotInfo.CurrencyId ");
            sb.Append(" left join (select stRef.CashInId,stRef.StockId,SUM(ISNULL(stRef.AllotBala,0))as stAllotBala,stAllot.CurrencyId  ");
            sb.Append("            from NFMT..Fun_CashInAllot stAllot ");
            sb.Append("            inner join NFMT..Fun_CashInStcok_Ref stRef on stRef.AllotId = stAllot.AllotId ");
            sb.AppendFormat("      where stAllot.AllotStatus >={0} and stRef.DetailStatus >={0} ", readyStatus);
            sb.Append("            group by stRef.CashInId,stRef.StockId,stAllot.CurrencyId) stAllotInfo on stAllotInfo.CashInId = ci.CashInId ");
            sb.Append(" left join NFMT..St_Stock st on st.StockId = stAllotInfo.StockId ");
            sb.Append(" left join NFMT..St_StockName sn on sn.StockNameId = st.StockNameId ");
            sb.Append(" left join NFMT_Basic..Currency stCur on stCur.CurrencyId = stAllotInfo.CurrencyId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" ci.CashInStatus >= {0} ", readyStatus);
            if (cashInId > 0)
                sb.AppendFormat(" and ci.CashInId = {0} ", cashInId);

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

        #endregion

        #region 新收款分配

        public ResultModel GetContractStock(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = cashinallotDAL.GetContractStock(user, contractId);
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

        public ResultModel Create(UserModel user, Model.CashInAllot cashInAllot, Model.CashInCorp cashInCorp, Model.CashInContract cashInContract, List<Model.CashInStcok> cashInStocks, List<NFMT.Funds.Model.CashInInvoice> cashInInvoices)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = cashinallotDAL.Insert(user, cashInAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    int cashInAllotId = (int)result.ReturnValue;

                    cashInCorp.AllotId = cashInAllotId;
                    DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
                    result = cashInCorpDAL.Insert(user, cashInCorp);
                    if (result.ResultStatus != 0)
                        return result;

                    int corpRefId = (int)result.ReturnValue;

                    int cashInContractCId = 0;
                    if (cashInContract.SubContractId > 0)
                    {
                        NFMT.Contract.DAL.ContractDAL contractDAL = new Contract.DAL.ContractDAL();
                        result = contractDAL.Get(user, cashInContract.SubContractId);
                        if (result.ResultStatus != 0)
                            return result;

                        NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                        if (contract == null || contract.ContractId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取合约失败";
                            return result;
                        }
                        cashInContractCId = contract.ContractId;
                    }

                    cashInContract.CorpRefId = corpRefId;
                    cashInContract.AllotId = cashInAllotId;
                    cashInContract.ContractId = cashInContractCId;
                    DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();
                    result = cashInContractDAL.Insert(user, cashInContract);
                    if (result.ResultStatus != 0)
                        return result;

                    int contractRefId = (int)result.ReturnValue;

                    if (cashInStocks != null && cashInStocks.Any())
                    {
                        DAL.CashInStcokDAL cashInStcokDAL = new CashInStcokDAL();
                        foreach (Model.CashInStcok cashInStcok in cashInStocks)
                        {
                            cashInStcok.CashInId = cashInCorp.CashInId;
                            cashInStcok.AllotId = cashInAllotId;
                            cashInStcok.CorpRefId = corpRefId;
                            cashInStcok.ContractRefId = contractRefId;
                            result = cashInStcokDAL.Insert(user, cashInStcok);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    if (cashInInvoices != null && cashInInvoices.Any())
                    {
                        DAL.CashInInvoiceDAL cashInInvoiceDAL = new CashInInvoiceDAL();
                        foreach (Model.CashInInvoice cashInInvoice in cashInInvoices)
                        {
                            cashInInvoice.CashInId = cashInCorp.CashInId;
                            cashInInvoice.AllotId = cashInAllotId;
                            cashInInvoice.CorpRefId = corpRefId;
                            cashInInvoice.ContractRefId = contractRefId;
                            result = cashInInvoiceDAL.Insert(user, cashInInvoice);
                            if (result.ResultStatus != 0)
                                return result;
                        }
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

        public ResultModel Update(UserModel user, Model.CashInAllot cashInAllot, Model.CashInCorp cashInCorp, Model.CashInContract cashInContract, List<Model.CashInStcok> cashInStocks, List<NFMT.Funds.Model.CashInInvoice> cashInInvoices)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = cashinallotDAL.Get(user, cashInAllot.AllotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashInAllot cashInAllotres = result.ReturnValue as Model.CashInAllot;
                    if (cashInAllotres == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取收款分配失败";
                        return result;
                    }
                    cashInAllotres.Alloter = user.EmpId;
                    cashInAllotres.AllotTime = DateTime.Now;
                    cashInAllotres.AllotBala = cashInAllot.AllotBala;
                    cashInAllotres.AllotType = cashInAllot.AllotType;
                    cashInAllotres.CurrencyId = cashInAllot.CurrencyId;
                    cashInAllotres.AllotDesc = cashInAllot.AllotDesc;

                    result = cashinallotDAL.Update(user, cashInAllotres);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废收款分配至公司
                    DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
                    result = cashInCorpDAL.InvalidAll(user, cashInAllot.AllotId);
                    if (result.ResultStatus != 0)
                        return result;

                    result = cashInCorpDAL.Insert(user, cashInCorp);
                    if (result.ResultStatus != 0)
                        return result;

                    int corpRefId = (int)result.ReturnValue;

                    //作废收款分配至合同
                    DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();
                    result = cashInContractDAL.InvalidAll(user, cashInAllot.AllotId);
                    if (result.ResultStatus != 0)
                        return result;

                    cashInContract.CorpRefId = corpRefId;
                    result = cashInContractDAL.Insert(user, cashInContract);
                    if (result.ResultStatus != 0)
                        return result;

                    int contractRefId = (int)result.ReturnValue;

                    if (cashInStocks != null && cashInStocks.Any())
                    {
                        DAL.CashInStcokDAL cashInStcokDAL = new CashInStcokDAL();
                        result = cashInStcokDAL.InvalidAll(user, cashInAllot.AllotId);
                        if (result.ResultStatus != 0)
                            return result;

                        foreach (Model.CashInStcok cashInStcok in cashInStocks)
                        {
                            cashInStcok.CashInId = cashInCorp.CashInId;
                            cashInStcok.AllotId = cashInAllot.AllotId;
                            cashInStcok.CorpRefId = corpRefId;
                            cashInStcok.ContractRefId = contractRefId;
                            result = cashInStcokDAL.Insert(user, cashInStcok);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    if (cashInInvoices != null && cashInInvoices.Any())
                    {
                        DAL.CashInInvoiceDAL cashInInvoiceDAL = new CashInInvoiceDAL();
                        result = cashInInvoiceDAL.InvalidAll(user, cashInAllot.AllotId);
                        if (result.ResultStatus != 0)
                            return result;

                        foreach (Model.CashInInvoice cashInInvoice in cashInInvoices)
                        {
                            cashInInvoice.CashInId = cashInCorp.CashInId;
                            cashInInvoice.AllotId = cashInAllot.AllotId;
                            cashInInvoice.CorpRefId = corpRefId;
                            cashInInvoice.ContractRefId = contractRefId;
                            result = cashInInvoiceDAL.Insert(user, cashInInvoice);
                            if (result.ResultStatus != 0)
                                return result;
                        }
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

        public ResultModel Invalid(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();
            DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
            DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();
            DAL.CashInStcokDAL cashInStcokDAL = new CashInStcokDAL();
            DAL.CashInInvoiceDAL cashInInvoiceDAL = new CashInInvoiceDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = cashinallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashInAllot cashInAllot = result.ReturnValue as Model.CashInAllot;
                    if (cashInAllot == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取收款分配失败";
                        return result;
                    }

                    //作废主收款分配
                    result = cashinallotDAL.Invalid(user, cashInAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废收款分配至公司
                    result = cashInCorpDAL.InvalidAll(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废收款分配至合约
                    result = cashInContractDAL.InvalidAll(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废收款分配至库存
                    result = cashInStcokDAL.InvalidAll(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废收款分配至发票
                    result = cashInInvoiceDAL.InvalidAll(user, allotId);

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

        public ResultModel Audit(UserModel user, NFMT.WorkFlow.Model.DataSource dataSource, bool isPass)
        {
            ResultModel result = new ResultModel();
            DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
            DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();
            DAL.CashInStcokDAL cashInStcokDAL = new CashInStcokDAL();
            DAL.CashInDAL cashInDAL = new CashInDAL();
            DAL.FundsLogDAL fundsLogDAL = new FundsLogDAL();
            DAL.CashInInvoiceDAL cashInInvoiceDAL = new CashInInvoiceDAL();

            Model.CashIn cashIn = null;

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = cashinallotDAL.Get(user, dataSource.RowId);
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
                    result = cashinallotDAL.Audit(user, cashInAllot, isPass);
                    if (result.ResultStatus != 0)
                        return result;

                    //审核通过
                    if (isPass)
                    {
                        //获取已生效的收款分配至公司
                        result = cashInCorpDAL.Load(user, cashInAllot.AllotId, NFMT.Common.StatusEnum.已生效);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.CashInCorp> cashInCorps = result.ReturnValue as List<Model.CashInCorp>;
                        if (cashInCorps == null || !cashInCorps.Any())
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取收款分配至公司失败";
                            return result;
                        }

                        Model.CashInCorp cashInCorp = cashInCorps.FirstOrDefault(a => a.RefId > 0);

                        //获取已生效的收款分配至合约
                        result = cashInContractDAL.LoadDetail(user, cashInAllot.AllotId, StatusEnum.已生效);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.CashInContract> cashInContracts = result.ReturnValue as List<Model.CashInContract>;
                        if (cashInContracts == null || !cashInContracts.Any())
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取收款分配至合约失败";
                            return result;
                        }

                        Model.CashInContract cashInContract = cashInContracts.FirstOrDefault(a => a.RefId > 0);

                        //获取已生效的收款分配至库存
                        result = cashInStcokDAL.LoadByAllot(user, cashInAllot.AllotId, StatusEnum.已生效);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.CashInStcok> cashInStcoks = result.ReturnValue as List<Model.CashInStcok>;
                        if (cashInStcoks == null || !cashInStcoks.Any())
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取收款分配至库存失败";
                            return result;
                        }

                        //获取已生效的收款分配至发票
                        List<Model.CashInInvoice> cashInInvoices = new List<CashInInvoice>();
                        result = cashInInvoiceDAL.LoadByAllot(user, cashInAllot.AllotId, StatusEnum.已生效);
                        if (result.ResultStatus == 0 && result.ReturnValue != null)
                        {
                            cashInInvoices = result.ReturnValue as List<Model.CashInInvoice>;
                            if (cashInInvoices == null || !cashInInvoices.Any())
                            {
                                result.ResultStatus = -1;
                                result.Message = "获取收款分配至发票失败";
                                return result;
                            }
                        }

                        //获取收款
                        result = cashInDAL.Get(user, cashInCorp.CashInId);
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
                            FundsBala = cashInAllot.AllotBala,
                            //FundsType 
                            CurrencyId = cashInAllot.CurrencyId,
                            LogDirection = (int)NFMT.WareHouse.LogDirectionEnum.In,
                            LogType = (int)NFMT.WareHouse.LogTypeEnum.收款,
                            //PayMode 
                            //IsVirtualPay
                            FundsDesc = cashInAllot.AllotDesc,
                            OpPerson = user.EmpId,
                            LogSourceBase = "NFMT",
                            LogSource = "dbo.Fun_CashInAllot",
                            SourceId = dataSource.RowId,
                            LogStatus = StatusEnum.已生效
                        });
                        if (result.ResultStatus != 0)
                            return result;

                        int fundsLogId = (int)result.ReturnValue;

                        //反向更新流水ID
                        cashInCorp.FundsLogId = fundsLogId;
                        result = cashInCorpDAL.Update(user, cashInCorp);
                        if (result.ResultStatus != 0)
                            return result;

                        cashInContract.FundsLogId = fundsLogId;
                        result = cashInContractDAL.Update(user, cashInContract);
                        if (result.ResultStatus != 0)
                            return result;

                        foreach (Model.CashInStcok cashInStock in cashInStcoks)
                        {
                            cashInStock.FundsLogId = fundsLogId;
                            result = cashInStcokDAL.Update(user, cashInStock);
                            if (result.ResultStatus != 0)
                                return result;
                        }

                        if (cashInInvoices != null && cashInInvoices.Any())
                        {
                            foreach (Model.CashInInvoice cashInInvoice in cashInInvoices)
                            {
                                cashInInvoice.FundsLogId = fundsLogId;
                                result = cashInInvoiceDAL.Update(user, cashInInvoice);
                                if (result.ResultStatus != 0)
                                    return result;
                            }
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

        public ResultModel Complete_NewVersion(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();
            DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
            DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();
            DAL.CashInStcokDAL cashInStcokDAL = new CashInStcokDAL();
            DAL.FundsLogDAL fundsLogDAL = new FundsLogDAL();
            DAL.CashInInvoiceDAL cashInInvoiceDAL = new CashInInvoiceDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取主收款分配
                    result = cashinallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashInAllot cashInAllot = result.ReturnValue as Model.CashInAllot;
                    if (cashInAllot == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取收款分配失败";
                        return result;
                    }

                    //完成主收款分配
                    result = cashinallotDAL.Complete(user, cashInAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取已生效的收款分配至公司
                    result = cashInCorpDAL.Load(user, cashInAllot.AllotId, NFMT.Common.StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInCorp> cashInCorps = result.ReturnValue as List<Model.CashInCorp>;
                    if (cashInCorps == null || !cashInCorps.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取收款分配至公司失败";
                        return result;
                    }

                    Model.CashInCorp cashInCorp = cashInCorps.FirstOrDefault(a => a.RefId > 0);

                    //完成收款分配至公司
                    result = cashInCorpDAL.Complete(user, cashInCorp);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取已生效的收款分配至合约
                    result = cashInContractDAL.LoadDetail(user, cashInAllot.AllotId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInContract> cashInContracts = result.ReturnValue as List<Model.CashInContract>;
                    if (cashInContracts == null || !cashInContracts.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取收款分配至合约失败";
                        return result;
                    }

                    Model.CashInContract cashInContract = cashInContracts.FirstOrDefault(a => a.RefId > 0);

                    //完成收款分配至合约
                    result = cashInContractDAL.Complete(user, cashInContract);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取已生效的收款分配至库存
                    result = cashInStcokDAL.LoadByAllot(user, cashInAllot.AllotId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInStcok> cashInStcoks = result.ReturnValue as List<Model.CashInStcok>;
                    if (cashInStcoks == null || !cashInStcoks.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取收款分配至库存失败";
                        return result;
                    }

                    foreach (Model.CashInStcok cashInStcok in cashInStcoks)
                    {
                        //完成收款分配至库存
                        result = cashInStcokDAL.Complete(user, cashInStcok);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取已生效的收款分配至发票
                    result = cashInInvoiceDAL.LoadByAllot(user, cashInAllot.AllotId, StatusEnum.已生效);
                    if (result.ResultStatus == 0)
                    {
                        List<Model.CashInInvoice> cashInInvoices = result.ReturnValue as List<Model.CashInInvoice>;
                        if (cashInInvoices == null || !cashInInvoices.Any())
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取收款分配至发票失败";
                            return result;
                        }

                        foreach (Model.CashInInvoice cashInInvoice in cashInInvoices)
                        {
                            //完成收款分配至发票
                            result = cashInInvoiceDAL.Complete(user, cashInInvoice);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    //获取资金流水
                    result = fundsLogDAL.Get(user, cashInCorp.FundsLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.FundsLog fundsLog = result.ReturnValue as Model.FundsLog;
                    if (fundsLog == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取资金流水失败";
                        return result;
                    }

                    //资金流水完成
                    result = fundsLogDAL.Complete(user, fundsLog);
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
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel CompleteCancel_NewVersion(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();
            DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
            DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();
            DAL.CashInStcokDAL cashInStcokDAL = new CashInStcokDAL();
            DAL.FundsLogDAL fundsLogDAL = new FundsLogDAL();
            DAL.CashInInvoiceDAL cashInInvoiceDAL = new CashInInvoiceDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取主收款分配
                    result = cashinallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashInAllot cashInAllot = result.ReturnValue as Model.CashInAllot;
                    if (cashInAllot == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取收款分配失败";
                        return result;
                    }

                    //完成主收款分配
                    result = cashinallotDAL.CompleteCancel(user, cashInAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取收款分配至公司
                    result = cashInCorpDAL.Load(user, cashInAllot.AllotId, NFMT.Common.StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInCorp> cashInCorps = result.ReturnValue as List<Model.CashInCorp>;
                    if (cashInCorps == null || !cashInCorps.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取收款分配至公司失败";
                        return result;
                    }

                    Model.CashInCorp cashInCorp = cashInCorps.FirstOrDefault(a => a.RefId > 0);

                    //完成收款分配至公司
                    result = cashInCorpDAL.CompleteCancel(user, cashInCorp);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取收款分配至合约
                    result = cashInContractDAL.LoadDetail(user, cashInAllot.AllotId, StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInContract> cashInContracts = result.ReturnValue as List<Model.CashInContract>;
                    if (cashInContracts == null || !cashInContracts.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取收款分配至合约失败";
                        return result;
                    }

                    Model.CashInContract cashInContract = cashInContracts.FirstOrDefault(a => a.RefId > 0);

                    //完成收款分配至合约
                    result = cashInContractDAL.CompleteCancel(user, cashInContract);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取收款分配至库存
                    result = cashInStcokDAL.LoadByAllot(user, cashInAllot.AllotId, StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInStcok> cashInStcoks = result.ReturnValue as List<Model.CashInStcok>;
                    if (cashInStcoks == null || !cashInStcoks.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取收款分配至库存失败";
                        return result;
                    }

                    foreach (Model.CashInStcok cashInStcok in cashInStcoks)
                    {
                        //完成收款分配至库存
                        result = cashInStcokDAL.CompleteCancel(user, cashInStcok);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取收款分配至发票
                    result = cashInInvoiceDAL.LoadByAllot(user, cashInAllot.AllotId, StatusEnum.已完成);
                    if (result.ResultStatus == 0)
                    {
                        List<Model.CashInInvoice> cashInInvoices = result.ReturnValue as List<Model.CashInInvoice>;
                        if (cashInInvoices == null || !cashInInvoices.Any())
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取收款分配至发票失败";
                            return result;
                        }

                        foreach (Model.CashInInvoice cashInInvoice in cashInInvoices)
                        {
                            //完成收款分配至发票
                            result = cashInInvoiceDAL.CompleteCancel(user, cashInInvoice);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }

                    //获取资金流水
                    result = fundsLogDAL.Get(user, cashInCorp.FundsLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.FundsLog fundsLog = result.ReturnValue as Model.FundsLog;
                    if (fundsLog == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取资金流水失败";
                        return result;
                    }

                    //资金流水完成
                    result = fundsLogDAL.CompleteCancel(user, fundsLog);
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
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Close_NewVersion(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();
            DAL.CashInCorpDAL cashInCorpDAL = new CashInCorpDAL();
            DAL.CashInContractDAL cashInContractDAL = new CashInContractDAL();
            DAL.CashInStcokDAL cashInStcokDAL = new CashInStcokDAL();
            DAL.FundsLogDAL fundsLogDAL = new FundsLogDAL();
            DAL.CashInInvoiceDAL cashInInvoiceDAL = new CashInInvoiceDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取主收款分配
                    result = cashinallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CashInAllot cashInAllot = result.ReturnValue as Model.CashInAllot;
                    if (cashInAllot == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取收款分配失败";
                        return result;
                    }

                    //完成主收款分配
                    result = cashinallotDAL.Close(user, cashInAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取收款分配至公司
                    result = cashInCorpDAL.Load(user, cashInAllot.AllotId, NFMT.Common.StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInCorp> cashInCorps = result.ReturnValue as List<Model.CashInCorp>;
                    if (cashInCorps == null || !cashInCorps.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取收款分配至公司失败";
                        return result;
                    }

                    Model.CashInCorp cashInCorp = cashInCorps.FirstOrDefault(a => a.RefId > 0);

                    //完成收款分配至公司
                    result = cashInCorpDAL.Close(user, cashInCorp);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取收款分配至合约
                    result = cashInContractDAL.LoadDetail(user, cashInAllot.AllotId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInContract> cashInContracts = result.ReturnValue as List<Model.CashInContract>;
                    if (cashInContracts == null || !cashInContracts.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取收款分配至合约失败";
                        return result;
                    }

                    Model.CashInContract cashInContract = cashInContracts.FirstOrDefault(a => a.RefId > 0);

                    //完成收款分配至合约
                    result = cashInContractDAL.Close(user, cashInContract);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取收款分配至库存
                    result = cashInStcokDAL.LoadByAllot(user, cashInAllot.AllotId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInStcok> cashInStcoks = result.ReturnValue as List<Model.CashInStcok>;
                    if (cashInStcoks == null || !cashInStcoks.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取收款分配至库存失败";
                        return result;
                    }

                    foreach (Model.CashInStcok cashInStcok in cashInStcoks)
                    {
                        //完成收款分配至库存
                        result = cashInStcokDAL.Close(user, cashInStcok);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取收款分配至库存
                    result = cashInInvoiceDAL.LoadByAllot(user, cashInAllot.AllotId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInInvoice> cashInInvoices = result.ReturnValue as List<Model.CashInInvoice>;
                    if (cashInInvoices == null || !cashInInvoices.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取收款分配至发票失败";
                        return result;
                    }

                    foreach (Model.CashInInvoice cashInInvoice in cashInInvoices)
                    {
                        //完成收款分配至库存
                        result = cashInInvoiceDAL.Close(user, cashInInvoice);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取资金流水
                    result = fundsLogDAL.Get(user, cashInCorp.FundsLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.FundsLog fundsLog = result.ReturnValue as Model.FundsLog;
                    if (fundsLog == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取资金流水失败";
                        return result;
                    }

                    //资金流水完成
                    result = fundsLogDAL.Close(user, fundsLog);
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
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel GetCanAllotBala(UserModel user, int cashInId, bool isUpdate, decimal alreadyAllotBala = 0)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = cashinallotDAL.GetCanAllotBala(user, cashInId, isUpdate, alreadyAllotBala);
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
