/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInCorpBLL.cs
// 文件功能描述：收款分配至公司dbo.Fun_CashInCorp_Ref业务逻辑类。
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
    /// 收款分配至公司dbo.Fun_CashInCorp_Ref业务逻辑类。
    /// </summary>
    public class CashInCorpBLL : Common.ExecBLL
    {
        private CashInCorpDAL cashincorpDAL = new CashInCorpDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CashInCorpDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CashInCorpBLL()
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
            get { return this.cashincorpDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetAllotCorpSelect(int pageIndex, int pageSize, string orderStr, int empId, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cia.AllotId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;
            int readyStatus = (int)Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" cia.AllotId,cia.AllotTime,cia.AllotDesc,CONVERT(varchar,cia.AllotBala) + c.CurrencyName as AllotBala,e.Name,cia.AllotStatus,bd.StatusName,co.CorpName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_CashInAllot cia ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on cia.CurrencyId = c.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on cia.Alloter =e.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on cia.AllotStatus = bd.DetailId and StatusId = {0} ", statusId);
            //sb.Append(" left join (select distinct ref.AllotId,ref.CorpId ");
            //sb.Append(" from dbo.Fun_CashInCorp_Ref ref ");
            //sb.Append(" inner join dbo.Fun_CashInAllotDetail rad on rad.DetailId = ref.DetailId ");
            //sb.Append(" ) as ref on ref.AllotId = cia.AllotId ");
            //sb.Append(" left join dbo.Fun_CashInCorp_Ref ref on ref.AllotId = cia.AllotId ");
            sb.AppendFormat(" left join (select distinct AllotId,CorpId from dbo.Fun_CashInCorp_Ref where DetailStatus>={0}) as ref on ref.AllotId = cia.AllotId ", readyStatus);
            sb.Append(" left join NFMT_User.dbo.Corporation co on ref.CorpId = co.CorpId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" ref.CorpId is not null ");
            if (status > 0)
                sb.AppendFormat(" and cia.AllotStatus = {0} ", status);
            if (empId > 0)
                sb.AppendFormat(" and cia.Alloter = {0} ", empId);

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 外部公司列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="CorpCode"></param>
        /// <param name="CorpName"></param>
        /// <param name="blocId"></param>
        /// <returns></returns>
        public SelectModel GetAllotReadyCorpListSelect(int pageIndex, int pageSize, string orderStr, string CorpCode, string CorpName, int blocId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "C.CorpId asc";
            else
                select.OrderStr = orderStr;

            int bDStyleId = (int)NFMT.Data.StyleEnum.公司类型;
            int bDStatusId = (int)NFMT.Common.StatusTypeEnum.通用状态;
            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("C.CorpId,B.BlocName,C.CorpName,C.CorpEName,C.TaxPayerId,C.CorpFullName,C.CorpFullEName,C.CorpAddress,BD.DetailName");
            sb.Append(",BDD2.StatusName,case C.IsSelf when 1 then '己方公司' when 0 then '非己方公司' else '' end as IsSelf");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT_User.dbo.Corporation C ");
            sb.Append(" left join NFMT_User.dbo.Bloc B on C.ParentId = B.BlocId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail BD on BD.StyleDetailId = C.CorpType and BD.BDStyleId ={0} ", bDStyleId);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail BDD2 on BDD2.DetailId = C.CorpStatus and BDD2.StatusId= {0} ", bDStatusId);

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" C.IsSelf = 0 and C.CorpStatus = {0} ", readyStatus);

            if (blocId > 0)
                sb.AppendFormat(" and C.ParentId = {0}", blocId);
            if (!string.IsNullOrEmpty(CorpCode))
                sb.AppendFormat(" and C.CorpCode like '%{0}%'", CorpCode);
            if (!string.IsNullOrEmpty(CorpName))
                sb.AppendFormat(" and C.CorpName like '%{0}%'", CorpName);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetOtherAllotInCorp(int pageIndex, int pageSize, string orderStr, int corpId, int allotId = 0)
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

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("cia.AllotId,cia.AllotTime,cia.Alloter,emp.Name as AlloterName,cia.AllotBala,cia.CurrencyId,cur.CurrencyName");
            sb.Append(",cia.AllotStatus,sd.StatusName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_CashInAllot cia ");

            //sb.Append(" inner join(select ciad.AllotId from dbo.Fun_CashInAllotDetail ciad ");
            //sb.AppendFormat(" inner join dbo.Fun_CashInCorp_Ref cicr on cicr.DetailId = ciad.DetailId and cicr.CorpId = {0} ",corpId);
            //sb.AppendFormat(" where ciad.AllotId != {0} and ciad.DetailStatus>={1} group by ciad.AllotId) as ref on ref.AllotId = cia.AllotId ", allotId,readyStatus);

            sb.Append(" inner join(select cicr.AllotId from dbo.Fun_CashInCorp_Ref cicr where cicr.CorpId = 10 ");
            sb.AppendFormat(" and cicr.AllotId != {0} and cicr.DetailStatus>={1} group by cicr.AllotId) as ref on ref.AllotId = cia.AllotId ", allotId, readyStatus);

            sb.Append(" left join NFMT_User.dbo.Employee emp on cia.Alloter = emp.EmpId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = cia.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = cia.AllotStatus and sd.StatusId ={0} ", commonStatusType);

            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Load(UserModel user, int allotId, NFMT.Common.StatusEnum status = StatusEnum.已生效)
        {
            return this.cashincorpDAL.Load(user, allotId, status);
        }

        public SelectModel GetCorpAllotLastSelect(int pageIndex, int pageSize, string orderStr, string corpRefIds, string contractRefids, string outCorpIds, int currencyId = 0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            if (string.IsNullOrEmpty(corpRefIds))
                corpRefIds = "0";
            if (string.IsNullOrEmpty(contractRefids))
                contractRefids = "0";
            if (string.IsNullOrEmpty(outCorpIds))
                return null;

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ci.CashInId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" 0 as DetailId,ci.CashInId,cicr.RefId as CorpRefId,ci.CashInDate,ci.CashInCorpId,inCorp.CorpName as InCorp ");
            sb.Append(" ,ci.PayCorpId,outCorp.CorpName as OutCorp,ci.CashInBank,ban.BankName as CashInBankName ");
            sb.Append(" ,ci.CurrencyId,cur.CurrencyName,ci.CashInBala,cicr.AllotBala as CorpAllotBala ");
            sb.Append(" ,isnull(ref.SumBala,0) as SumBala,cicr.AllotBala - ISNULL(ref.SumBala,0) as LastBala ");
            sb.Append(" ,cicr.AllotBala - ISNULL(ref.SumBala,0) as AllotBala,cicr.CorpId,allotCorp.CorpName as AllotCorp ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_CashInCorp_Ref cicr ");

            sb.Append(" inner join dbo.Fun_CashIn ci on cicr.CashInId = ci.CashInId ");
            sb.Append(" left join (select SUM(ref.AllotBala) as SumBala,ref.CorpRefId from dbo.Fun_CashInContract_Ref ref ");
            sb.AppendFormat(" where ref.DetailStatus={0} and ref.RefId not in ({1}) group by ref.CorpRefId) as ref on ref.CorpRefId = cicr.RefId ", readyStatus, contractRefids);
            sb.Append(" left join NFMT_User.dbo.Corporation inCorp on ci.CashInCorpId = inCorp.CorpId ");
            sb.Append(" left join NFMT_User.dbo.Corporation outCorp on ci.PayCorpId = outCorp.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank ban on ban.BankId = ci.CashInBank ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = ci.CurrencyId ");

            sb.Append(" left join NFMT_User.dbo.Corporation allotCorp on allotCorp.CorpId = cicr.CorpId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" cicr.CorpId in ({0}) ", outCorpIds);
            sb.AppendFormat(" and ci.CashInStatus ={0} ", readyStatus);
            sb.AppendFormat(" and cicr.DetailStatus ={0} ", readyStatus);
            sb.AppendFormat(" and cicr.RefId not in ({0}) ", corpRefIds);
            sb.Append(" and cicr.AllotBala>isnull(ref.SumBala,0) ");

            if (currencyId > 0)
                sb.AppendFormat(" and ci.CurrencyId={0} ", currencyId);

            select.WhereStr = sb.ToString();

            return select;
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
                        result = this.cashincorpDAL.Load(user, cashInAllot.AllotId, NFMT.Common.StatusEnum.已生效);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.CashInCorp> cashInCorps = result.ReturnValue as List<Model.CashInCorp>;
                        if (cashInCorps == null || !cashInCorps.Any())
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取明细失败";
                            return result;
                        }

                        foreach (Model.CashInCorp cashInCorp in cashInCorps)
                        {
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
                                //ContractId
                                //SubId
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
                                FundsBala = cashInCorp.AllotBala,
                                //FundsType 
                                CurrencyId = cashInAllot.CurrencyId,
                                LogDirection = (int)NFMT.WareHouse.LogDirectionEnum.In,
                                LogType = (int)NFMT.WareHouse.LogTypeEnum.收款,
                                //PayMode 
                                //IsVirtualPay
                                FundsDesc = cashInAllot.AllotDesc,
                                OpPerson = user.EmpId,
                                LogSourceBase = "NFMT",
                                LogSource = "dbo.Fun_CashInCorp_Ref",
                                SourceId = dataSource.RowId,
                                LogStatus = StatusEnum.已生效
                            });
                            if (result.ResultStatus != 0)
                                return result;

                            int fundsLogId = (int)result.ReturnValue;

                            cashInCorp.FundsLogId = fundsLogId;
                            result = cashincorpDAL.Update(user, cashInCorp);
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

                    result = cashincorpDAL.Load(user, allotId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInCorp> cashInCorps = result.ReturnValue as List<Model.CashInCorp>;
                    if (cashInCorps == null || !cashInCorps.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取明细失败";
                        return result;
                    }

                    foreach (Model.CashInCorp cashInCorp in cashInCorps)
                    {
                        //作废明细
                        result = cashincorpDAL.Invalid(user, cashInCorp);
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

                    result = cashincorpDAL.Load(user, allotId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInCorp> cashInCorps = result.ReturnValue as List<Model.CashInCorp>;
                    if (cashInCorps == null || !cashInCorps.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取明细失败";
                        return result;
                    }

                    foreach (Model.CashInCorp cashInCorp in cashInCorps)
                    {
                        //完成明细
                        result = cashincorpDAL.Complete(user, cashInCorp);
                        if (result.ResultStatus != 0)
                            return result;

                        result = fundsLogDAL.Get(user, cashInCorp.FundsLogId);
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

                    result = cashincorpDAL.Load(user, allotId, StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInCorp> cashInCorps = result.ReturnValue as List<Model.CashInCorp>;
                    if (cashInCorps == null || !cashInCorps.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取明细失败";
                        return result;
                    }

                    foreach (Model.CashInCorp cashInCorp in cashInCorps)
                    {
                        //完成撤销明细
                        result = cashincorpDAL.CompleteCancel(user, cashInCorp);
                        if (result.ResultStatus != 0)
                            return result;

                        result = fundsLogDAL.Get(user, cashInCorp.FundsLogId);
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

                    result = cashincorpDAL.Load(user, allotId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CashInCorp> cashInCorps = result.ReturnValue as List<Model.CashInCorp>;
                    if (cashInCorps == null || !cashInCorps.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取明细失败";
                        return result;
                    }

                    foreach (Model.CashInCorp cashInCorp in cashInCorps)
                    {
                        //关闭明细
                        result = cashincorpDAL.Close(user, cashInCorp);
                        if (result.ResultStatus != 0)
                            return result;

                        result = fundsLogDAL.Get(user, cashInCorp.FundsLogId);
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
