/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ReceivableAllotBLL.cs
// 文件功能描述：收款分配dbo.Fun_ReceivableAllot业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
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
    /// 收款分配dbo.Fun_ReceivableAllot业务逻辑类。
    /// </summary>
    public class ReceivableAllotBLL : Common.ApplyBLL
    {
        private ReceivableAllotDAL receivableallotDAL = new ReceivableAllotDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ReceivableAllotDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReceivableAllotBLL()
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
            get { return this.receivableallotDAL; }
        }

        #endregion

        #region 添加方法

        /// <summary>
        /// 有用，合约分配列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="empId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int empId, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ra.ReceivableAllotId desc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("  ra.ReceivableAllotId,ra.AllotTime,ra.AllotDesc,CONVERT(varchar,ra.AllotBala) + c.CurrencyName as AllotBala,e.Name,ra.AllotStatus,bd.StatusName,sub.SubNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_ReceivableAllot ra ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on ra.CurrencyId = c.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on ra.EmpId =e.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on ra.AllotStatus = bd.DetailId and StatusId = {0} ", statusId);
            sb.Append(" left join (select distinct AllotId,SubContractId from dbo.Fun_ContractReceivable_Ref ) as ref  on ref.AllotId = ra.ReceivableAllotId ");
            sb.Append(" left join dbo.Con_ContractSub sub on ref.SubContractId = sub.SubId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" ref.SubContractId is not null ");
            if (status > 0)
                sb.AppendFormat(" and ra.AllotStatus = {0} ", status);
            if (empId > 0)
                sb.AppendFormat(" and ra.EmpId = {0} ", empId);

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 有用，合约分配，已分配列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        public SelectModel GetAllotAmountSelectModel(int pageIndex, int pageSize, string orderStr, int subId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ra.ReceivableAllotId desc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;
            int readyStatus = (int)Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("ra.ReceivableAllotId,");
            sb.Append("ra.AllotTime,");
            sb.Append("e.Name,");
            sb.Append("CONVERT(varchar,ra.AllotBala)+c.CurrencyName as AllotBala,");
            sb.Append("bd.StatusName,");
            sb.Append("ra.AllotStatus");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_ReceivableAllot ra ");

            sb.Append(" inner join ( ");
            sb.Append(" select distinct ref.AllotId ");
            sb.Append(" from dbo.Fun_ContractReceivable_Ref ref ");
            sb.AppendFormat(" inner join dbo.Fun_RecAllotDetail rad on ref.DetailId = rad.DetailId and rad.DetailStatus>={0} ",readyStatus);
            sb.Append(" inner join dbo.Con_ContractSub cs on ref.SubContractId= cs.SubId ");
            sb.AppendFormat(" where ref.SubContractId = {0} ",subId);
            sb.Append(" ) ref on ref.AllotId = ra.ReceivableAllotId ");

            sb.Append(" left join NFMT_User.dbo.Employee e on ra.EmpId = e.EmpId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on ra.CurrencyId = c.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on ra.AllotStatus = bd.DetailId and StatusId = {0} ", statusId);
            //sb.AppendFormat(" right join (select distinct AllotId from dbo.Fun_ContractReceivable_Ref ref left join dbo.Con_ContractSub cs on ref.SubContractId= cs.SubId where ref.SubContractId = {0}) ref on ref.AllotId = ra.ReceivableAllotId", subId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" ra.AllotStatus = {0}", (int)Common.StatusEnum.已生效);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetCanAllotReceSelectModel(int pageIndex, int pageSize, string orderStr, string rids, int subId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "r.ReceivableId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" r.ReceivableId,");
            sb.Append(" r.ReceiveDate,");
            sb.Append(" c.CorpName as InnerCorp,");
            sb.Append(" b.BankName,");
            sb.Append(" r.PayCorpName as OutCorp,");
            sb.Append(" CONVERT(varchar,r.PayBala)+cu.CurrencyName as PayBala,");
            sb.Append(" (r.PayBala - isnull((select SUM(ISNULL(ref1.AllotBala,0)) from dbo.Fun_CorpReceivable_Ref ref1 where ref1.RecId = r.ReceivableId),0))  as CanAllotBala");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_Receivable r left join NFMT_User.dbo.Corporation c on r.ReceivableCorpId = c.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cu on r.CurrencyId = cu.CurrencyId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank b on r.ReceivableBank = b.BankId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c2 on r.PayCorpId = c2.CorpId ");
            sb.Append(" left join NFMT.dbo.Con_ContractSub sub on r.CurrencyId = sub.SettleCurrency ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" r.ReceiveStatus = {0} and sub.SubId = {1} ", (int)Common.StatusEnum.已生效, subId);
            sb.Append(" and (r.PayBala - isnull((select SUM(ISNULL(ref1.AllotBala,0)) from dbo.Fun_CorpReceivable_Ref ref1 where ref1.RecId = r.ReceivableId),0))>0 ");
            if (!string.IsNullOrEmpty(rids))
                sb.AppendFormat(" and r.ReceivableId not in ({0})", rids);

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 获取指定子合约的分配金额
        /// </summary>
        /// <param name="user"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        public ResultModel GetSubContractAllotAmount(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = receivableallotDAL.GetSubContractAllotAmount(user, subId);
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

        public ResultModel ReceivableAllotCreateHandle(UserModel user, List<Model.CorpReceivable> corpReceivables,int contractId,int contractSubId, int curId, string memo, int allotFrom)
        {
            ResultModel result = new ResultModel();
            DAL.ReceivableDAL receivableDAL = new ReceivableDAL();
            DAL.CorpReceivableDAL corpReceivableDAL = new CorpReceivableDAL();
            DAL.ContractReceivableDAL contractReceivableDAL = new ContractReceivableDAL();
            NFMT.User.DAL.CorporationDAL corporationDAL = new User.DAL.CorporationDAL();
            Model.Receivable receivable = new Receivable();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    decimal allotBala = 0;
                    foreach (Model.CorpReceivable corpReceivable in corpReceivables)
                    {
                        //allotBala += corpReceivable.AllotBala;
                    }

                    //收款分配信息
                    Model.ReceivableAllot receivableAllot = new ReceivableAllot()
                    {
                        AllotFrom = allotFrom,
                        AllotBala = allotBala,
                        CurrencyId = curId,
                        AllotDesc = memo,
                        EmpId = user.EmpId,
                        AllotTime = DateTime.Now
                    };

                    //写入收款分配
                    result = receivableallotDAL.Insert(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    int receivableAllotId = (int)result.ReturnValue;

                    foreach (Model.CorpReceivable corpReceivable in corpReceivables)
                    {
                        corpReceivable.AllotId = receivableAllotId;
                        result = corporationDAL.Get(user, corpReceivable.CorpId);
                        if (result.ResultStatus != 0)
                            return result;
                        
                        //获取公司信息
                        NFMT.User.Model.Corporation corp = result.ReturnValue as NFMT.User.Model.Corporation;
                        if (corp == null)
                            return result;

                        corpReceivable.BlocId = corp.ParentId;

                        //写入公司收款分配表
                        result = corpReceivableDAL.Insert(user, corpReceivable);
                        if (result.ResultStatus != 0)
                            return result;

                        int refId = (int)result.ReturnValue;

                        Model.ContractReceivable contractReceivable = new ContractReceivable()
                        {
                            CorpRefId = refId,
                            AllotId = corpReceivable.AllotId,
                            RecId = corpReceivable.RecId,
                            ContractId = contractId,
                            SubContractId = contractSubId,
                            //AllotBala = corpReceivable.AllotBala
                        };

                        //写入合约收款分配表
                        result = contractReceivableDAL.Insert(user, contractReceivable);
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

        public SelectModel GetAllotForContractSelectModel(int pageIndex, int pageSize, string orderStr, int receivableAllotId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ref.RefId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" ref.RefId,CONVERT(varchar,r.PayBala)+c1.CurrencyName as PayBala,CONVERT(varchar,ref.AllotBala) +c1.CurrencyName as AllotBala,sub.SubNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_ContractReceivable_Ref ref  ");
            sb.Append(" left join dbo.Fun_Receivable r on ref.RecId = r.ReceivableId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c1 on r.CurrencyId = c1.CurrencyId ");
            sb.Append(" left join NFMT.dbo.Con_ContractSub sub on ref.SubContractId = sub.SubId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" ref.RefStatus = {0} and ref.AllotId = {1}", (int)Common.StatusEnum.已生效, receivableAllotId);
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetUpdateListSelectModel(int pageIndex, int pageSize, string orderStr,int allotId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ref.RefId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" ref.RecId as ReceivableId,r.ReceiveDate,c.CorpName as InnerCorp,b.BankName,c2.CorpName as OutCorp,CONVERT(varchar,r.PayBala)+cu.CurrencyName as PayBala,ref.AllotBala as CanAllotBala");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_ContractReceivable_Ref ref  ");
            sb.Append(" left join dbo.Fun_Receivable r on ref.RecId = r.ReceivableId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cu on r.CurrencyId = cu.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c on r.ReceivableCorpId = c.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank b on r.ReceivableBank = b.BankId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c2 on r.PayCorpId = c2.CorpId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" ref.RefStatus = {0} and ref.AllotId = {1} ", (int)Common.StatusEnum.已生效, allotId);
            //sb.AppendFormat(" and ref.RecId in ({0})", string.IsNullOrEmpty(rids)?"0":rids);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel ReceivableAllotUpdateHandle(UserModel user, List<Model.CorpReceivable> corpReceivables, int curId, string memo, int allotId, int contractId, int subId)
        {
            ResultModel result = new ResultModel();
            DAL.ReceivableDAL receivableDAL = new ReceivableDAL();
            DAL.ContractReceivableDAL contractReceivableDAL = new ContractReceivableDAL();
            DAL.CorpReceivableDAL corpReceivableDAL = new CorpReceivableDAL();
            NFMT.User.DAL.CorporationDAL corporationDAL = new User.DAL.CorporationDAL();
            Model.Receivable receivable = new Receivable();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    decimal allotBala = 0;
                    foreach (Model.CorpReceivable corpReceivable in corpReceivables)
                    {
                        //allotBala += corpReceivable.AllotBala;
                    }

                    result = receivableallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ReceivableAllot receivableAllot = result.ReturnValue as Model.ReceivableAllot;
                    receivableAllot.AllotDesc = memo;
                    receivableAllot.AllotBala = allotBala;

                    //更新分配信息
                    result = receivableallotDAL.Update(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废所有allotId下的合约收款分配
                    result = contractReceivableDAL.InvalidAll(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废所有allotId下的公司收款分配
                    result = corpReceivableDAL.InvalidAll(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    foreach (Model.CorpReceivable corpReceivable in corpReceivables)
                    {
                        corpReceivable.AllotId = allotId;
                        result = corporationDAL.Get(user, corpReceivable.CorpId);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取公司信息
                        NFMT.User.Model.Corporation corp = result.ReturnValue as NFMT.User.Model.Corporation;
                        if (corp == null)
                            return result;

                        corpReceivable.BlocId = corp.ParentId;

                        //写入公司收款分配表
                        result = corpReceivableDAL.Insert(user, corpReceivable);
                        if (result.ResultStatus != 0)
                            return result;

                        int refId = (int)result.ReturnValue;

                        Model.ContractReceivable contractReceivable = new ContractReceivable()
                        {
                            CorpRefId = refId,
                            AllotId = corpReceivable.AllotId,
                            RecId = corpReceivable.RecId,
                            ContractId = contractId,
                            SubContractId = subId,
                            //AllotBala = corpReceivable.AllotBala
                        };

                        //写入合约收款分配表
                        result = contractReceivableDAL.Insert(user, contractReceivable);
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

        /// <summary>
        ///  有用，创建分配主表和明细表
        /// </summary>
        /// <param name="user"></param>
        /// <param name="rec"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public ResultModel CreateMain(UserModel user,Model.ReceivableAllot rec,ref List<Model.RecAllotDetail> details)
        {
            ResultModel result = new ResultModel();

            decimal sumBala = details.Sum(temp=>temp.AllotBala);

            rec.AllotBala = sumBala;
            rec.AllotStatus = StatusEnum.已录入;
            result = this.receivableallotDAL.Insert(user,rec);
            if (result.ResultStatus != 0)
                return result;

            int allotId = 0;
            if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out allotId))
            {
                result.ResultStatus = -1;
                result.Message = "收款分配失败";
                return result;
            }

            DAL.RecAllotDetailDAL detailDAL = new RecAllotDetailDAL();
            foreach (Model.RecAllotDetail detail in details)
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
        /// 有用，创建分配公司关联表
        /// </summary>
        /// <param name="user"></param>
        /// <param name="rec"></param>
        /// <param name="details"></param>
        /// <param name="corpDetail"></param>
        /// <returns></returns>
        public ResultModel CreateCorp(UserModel user, Model.ReceivableAllot rec, List<Model.RecAllotDetail> details,Model.CorpReceivable corpDetail)
        {
            ResultModel result = new ResultModel();

            try 
            {
                DAL.CorpReceivableDAL corpRecDAL = new CorpReceivableDAL();
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    rec.AllotType = (int)CashInAllotTypeEnum.Corp;
                    result = this.CreateMain(user, rec, ref details);
                    if (result.ResultStatus != 0)
                        return result;

                    foreach (Model.RecAllotDetail detail in details)
                    {
                        Model.CorpReceivable corpD = new CorpReceivable();
                        corpD.AllotId = detail.AllotId;
                        corpD.BlocId = corpDetail.BlocId;
                        corpD.CorpId = corpDetail.CorpId;
                        corpD.DetailId = detail.DetailId;
                        corpD.IsShare = corpDetail.IsShare;
                        corpD.RecId = detail.RecId;

                        result = corpRecDAL.Insert(user, corpD);
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

        public ResultModel UpdateMain(UserModel user, Model.ReceivableAllot rec, ref List<Model.RecAllotDetail> details)
        {
            ResultModel result = new ResultModel();

            //获取分配
            result = this.receivableallotDAL.Get(user, rec.ReceivableAllotId);
            if (result.ResultStatus != 0)
                return result;

            Model.ReceivableAllot resultObj = result.ReturnValue as Model.ReceivableAllot;
            if (resultObj == null || resultObj.ReceivableAllotId <= 0)
            {
                result.ResultStatus = -1;
                result.Message = "分配不存在";
                return result;
            }

            //更新分配
            decimal sumBala = details.Sum(temp => temp.AllotBala);
            resultObj.AllotBala = sumBala;
            resultObj.AllotDesc = rec.AllotDesc;
            result = this.receivableallotDAL.Update(user, resultObj);
            if (result.ResultStatus != 0)
                return result;            

            DAL.RecAllotDetailDAL detailDAL = new RecAllotDetailDAL();
            //获取当前分配下所有有效明细
            result = detailDAL.Load(user, resultObj.ReceivableAllotId);
            if (result.ResultStatus != 0)
                return result;

            List<Model.RecAllotDetail> resultDetails = result.ReturnValue as List<Model.RecAllotDetail>;
            if (resultDetails == null)
            {
                result.ResultStatus = -1;
                result.Message = "获取明细失败";
                return result;
            }
            //作废所有有效明细
            foreach (Model.RecAllotDetail d in resultDetails)
            {
                if (d.DetailStatus == StatusEnum.已生效)
                    d.DetailStatus = StatusEnum.已录入;
                result = detailDAL.Invalid(user, d);
                if (result.ResultStatus != 0)
                    return result;
            }

            foreach (Model.RecAllotDetail detail in details)
            {
                int detailId = 0;

                detail.DetailStatus = StatusEnum.已生效;
                detail.AllotId = resultObj.ReceivableAllotId;
                result = detailDAL.Insert(user, detail);
                if (result.ResultStatus != 0)
                    return result;

                if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out detailId))
                {
                    result.ResultStatus = -1;
                    result.Message = "收款分配明细更新失败";
                    return result;
                }

                detail.DetailId = detailId;
            }

            return result;
        }

        public ResultModel UpdateCorp(UserModel user, Model.ReceivableAllot rec, List<Model.RecAllotDetail> details, Model.CorpReceivable corpDetail)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.CorpReceivableDAL corpRecDAL = new CorpReceivableDAL();
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {

                    result = this.UpdateMain(user, rec, ref details);
                    if (result.ResultStatus != 0)
                        return result;

                    result = corpRecDAL.Load(user, rec.ReceivableAllotId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CorpReceivable> corpDetails = result.ReturnValue as List<Model.CorpReceivable>;
                    if (corpDetails == null || corpDetails.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取公司明细失败";
                        return result;
                    }

                    corpDetail.BlocId = corpDetails[0].BlocId;
                    corpDetail.CorpId = corpDetails[0].CorpId;
                    
                    foreach (Model.RecAllotDetail detail in details)
                    {
                        Model.CorpReceivable corpD = new CorpReceivable();
                        corpD.AllotId = detail.AllotId;
                        corpD.BlocId = corpDetail.BlocId;
                        corpD.CorpId = corpDetail.CorpId;
                        corpD.DetailId = detail.DetailId;
                        corpD.IsShare = corpDetail.IsShare;
                        corpD.RecId = detail.RecId;

                        result = corpRecDAL.Insert(user, corpD);
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
        
        public ResultModel Invalid(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = receivableallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ReceivableAllot receivableAllot = result.ReturnValue as Model.ReceivableAllot;
                    if (receivableAllot == null || receivableAllot.ReceivableAllotId <= 0)
                    {
                        result.Message = "分配不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = receivableallotDAL.Invalid(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    DAL.RecAllotDetailDAL detailDAL = new RecAllotDetailDAL();
                    result = detailDAL.Load(user, receivableAllot.ReceivableAllotId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.RecAllotDetail> details = result.ReturnValue as List<Model.RecAllotDetail>;
                    if (details == null)
                    {
                        result.Message = "明细获取失败";
                        result.ResultStatus = -1;
                        return result;
                    }

                    foreach (Model.RecAllotDetail d in details)
                    {
                        if (d.DetailStatus == StatusEnum.已生效)
                            d.DetailStatus = StatusEnum.已录入;

                        result = detailDAL.Invalid(user, d);
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

        public ResultModel GoBack(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = receivableallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ReceivableAllot receivableAllot = result.ReturnValue as Model.ReceivableAllot;
                    result = receivableallotDAL.Goback(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WorkFlow.DAL.DataSourceDAL dataSourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = dataSourceDAL.SynchronousStatus(user, receivableAllot);
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

        public SelectModel GetReceStockSelectModel(int pageIndex, int pageSize, string orderStr, int empId, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ra.ReceivableAllotId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" ra.ReceivableAllotId,ra.AllotTime,ra.AllotDesc,CONVERT(varchar,ra.AllotBala) + c.CurrencyName as AllotBala,e.Name,ra.AllotStatus,bd.StatusName,sn.RefNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_ReceivableAllot ra ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on ra.CurrencyId = c.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on ra.EmpId =e.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on ra.AllotStatus = bd.DetailId and StatusId = {0} ", statusId);
            sb.Append(" right join (select distinct AllotId,StockId from dbo.Fun_StcokReceivable_Ref ) as ref  on ref.AllotId = ra.ReceivableAllotId ");
            sb.Append(" left join dbo.St_Stock st on ref.StockId = st.StockId ");
            sb.Append(" left join dbo.St_StockName sn on sn.StockNameId = st.StockNameId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1");
            if (status > 0)
                sb.AppendFormat(" and ra.AllotStatus = {0} ", status);
            if (empId > 0)
                sb.AppendFormat(" and ra.EmpId = {0} ", empId);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetReceStockReadyListSelect(int pageIndex, int pageSize, string orderStr, string stockName, DateTime stockDateBegin, DateTime stockDateEnd, int corpId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = string.Format("sto.StockId,sto.StockDate,sn.RefNo,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,cast(sto.GrossAmount as varchar)+mu.MUName as StockWeight,sto.BrandId,bra.BrandName,sto.CustomsType ,custom.DetailName as CustomsTypeName,sto.StockStatus,sd.StatusName,Convert(varchar,(select SUM(ref.AllotBala) from dbo.Fun_StcokReceivable_Ref ref left join dbo.Fun_ReceivableAllot allot on ref.AllotId = allot.ReceivableAllotId where ref.StockId = sto.StockId and allot.AllotStatus = {0} and ref.RefStatus = {1})) + (select top 1 c.CurrencyName from dbo.Fun_StcokReceivable_Ref ref1 left join dbo.Fun_Receivable r on ref1.RecId = r.ReceivableId left join NFMT_Basic.dbo.Currency c on r.CurrencyId = c.CurrencyId where ref1.StockId = sto.StockId) as AllotBala", (int)Common.StatusEnum.已生效, (int)Common.StatusEnum.已生效);

            int customsType = (int)Data.StyleEnum.报关状态;
            int statusType = (int)Common.StatusTypeEnum.库存状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_Stock sto ");
            sb.Append(" left join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = sto.StockStatus  and sd.StatusId ={0} ", statusType);
            sb.Append(" left join NFMT_User.dbo.Corporation cor on sto.CorpId = cor.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail custom on custom.StyleDetailId = sto.CustomsType and custom.BDStyleId ={0} ", customsType);
            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(stockName))
                sb.AppendFormat(" and sn.RefNo like '%{0}%'", stockName);
            if (corpId > 0)
                sb.AppendFormat(" and sto.CorpId = {0}", corpId);

            if (stockDateBegin > NFMT.Common.DefaultValue.DefaultTime && stockDateEnd > stockDateBegin)
                sb.AppendFormat(" and sto.StockDate between '{0}' and '{1}' ", stockDateBegin.ToString(), stockDateEnd.ToString());
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetReceStockAllotSelectModel(int pageIndex, int pageSize, string orderStr, int sid)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ra.ReceivableAllotId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("ra.ReceivableAllotId,");
            sb.Append("ra.AllotTime,");
            sb.Append("e.Name,");
            sb.Append("CONVERT(varchar,ra.AllotBala)+c.CurrencyName as AllotBala,");
            sb.Append("bd.StatusName,");
            sb.Append("ra.AllotStatus");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_ReceivableAllot ra left join NFMT_User.dbo.Employee e on ra.EmpId = e.EmpId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on ra.CurrencyId = c.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on ra.AllotStatus = bd.DetailId and StatusId = {0} ", statusId);
            sb.AppendFormat(" right join (select distinct AllotId from dbo.Fun_StcokReceivable_Ref ref left join dbo.St_Stock s on ref.StockId= s.StockId where ref.StockId = {0}) ref on ref.AllotId = ra.ReceivableAllotId", sid);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" ra.AllotStatus = {0}", (int)Common.StatusEnum.已生效);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetCanAllotReceStockSelectModel(int pageIndex, int pageSize, string orderStr, string rids,int stockId,int t)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "r.ReceivableId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" r.ReceivableId,r.ReceiveDate,c.CorpName as InnerCorp,b.BankName,r.PayCorpName as OutCorp,CONVERT(varchar,r.PayBala)+cu.CurrencyName as PayBala,");
            if (t == 0)
                sb.Append(" (r.PayBala - isnull((select SUM(ISNULL(ref1.AllotBala,0)) from dbo.Fun_CorpReceivable_Ref ref1 where ref1.RecId = r.ReceivableId),0))  as CanAllotBala");
            else if (t == 1)
                sb.AppendFormat(" (r.PayBala - isnull((select SUM(ISNULL(ref1.AllotBala,0)) from dbo.Fun_CorpReceivable_Ref ref1 where ref1.RecId = r.ReceivableId and ref1.RecId not in ({0}),0))  as CanAllotBala", rids);
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_Receivable r left join NFMT_User.dbo.Corporation c on r.ReceivableCorpId = c.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cu on r.CurrencyId = cu.CurrencyId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank b on r.ReceivableBank = b.BankId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c2 on r.PayCorpId = c2.CorpId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" r.ReceiveStatus = {0}", (int)Common.StatusEnum.已生效);
            if (t == 0)
                sb.Append(" and (r.PayBala - isnull((select SUM(ISNULL(ref1.AllotBala,0)) from dbo.Fun_CorpReceivable_Ref ref1 where ref1.RecId = r.ReceivableId),0))>0 ");
            else if (t == 1)
                sb.AppendFormat(" and (r.PayBala - isnull((select SUM(ISNULL(ref1.AllotBala,0)) from dbo.Fun_CorpReceivable_Ref ref1 where ref1.RecId = r.ReceivableId and ref1.RecId not in ({0}),0))>0 ", rids);
            sb.AppendFormat(" and r.CurrencyId = (select sub.SettleCurrency from dbo.St_StockLog slog left join dbo.Con_ContractSub sub on slog.SubContractId = sub.SubId where slog.StockId = {0})", stockId);
            if (!string.IsNullOrEmpty(rids))
                sb.AppendFormat(" and r.ReceivableId not in ({0})", rids);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel ReceivableStockCreateHandle(UserModel user, List<Model.CorpReceivable> corpReceivables, int curId, string memo, int allotFrom, int stockId, int stockNameId)
        {
            ResultModel result = new ResultModel();
            DAL.ReceivableDAL receivableDAL = new ReceivableDAL();
            DAL.CorpReceivableDAL corpReceivableDAL = new CorpReceivableDAL();
            DAL.ContractReceivableDAL contractReceivableDAL = new ContractReceivableDAL();
            DAL.StcokReceivableDAL stcokReceivableDAL = new StcokReceivableDAL();
            NFMT.User.DAL.CorporationDAL corporationDAL = new User.DAL.CorporationDAL();
            NFMT.WareHouse.DAL.StockLogDAL stockLogDAL=new WareHouse.DAL.StockLogDAL();
            Model.Receivable receivable = new Receivable();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    decimal allotBala = 0;
                    foreach (Model.CorpReceivable corpReceivable in corpReceivables)
                    {
                        //allotBala += corpReceivable.AllotBala;
                    }

                    Model.ReceivableAllot receivableAllot = new ReceivableAllot()
                    {
                        AllotFrom = allotFrom,
                        AllotBala = allotBala,
                        CurrencyId = curId,
                        AllotDesc = memo,
                        EmpId = user.EmpId,
                        AllotTime = DateTime.Now
                    };

                    //写入收款分配表
                    result = receivableallotDAL.Insert(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    int receivableAllotId = (int)result.ReturnValue;

                    result = stockLogDAL.GetStockContractId(user, stockId);
                    if (result.ResultStatus != 0)
                        return result;

                    string contractStr = result.ReturnValue.ToString();

                    foreach (Model.CorpReceivable corpReceivable in corpReceivables)
                    {
                        corpReceivable.AllotId = receivableAllotId;

                        //获取公司信息
                        result = corporationDAL.Get(user, corpReceivable.CorpId);
                        if (result.ResultStatus != 0)
                            return result;

                        NFMT.User.Model.Corporation corp = result.ReturnValue as NFMT.User.Model.Corporation;
                        if (corp == null)
                        {
                            result.ResultStatus = -1;
                            return result;
                        }
                        corpReceivable.BlocId = corp.ParentId;

                        //写入公司收款表
                        result = corpReceivableDAL.Insert(user, corpReceivable);
                        if (result.ResultStatus != 0)
                            return result;

                        int corpRefId = (int)result.ReturnValue;

                        //新建合约收款信息
                        Model.ContractReceivable contractReceivable = new ContractReceivable()
                        {
                            CorpRefId = corpRefId,
                            AllotId = receivableAllotId,
                            RecId = corpReceivable.RecId,
                            ContractId = Convert.ToInt32(contractStr.Split(',')[0]),
                            SubContractId = Convert.ToInt32(contractStr.Split(',')[1]),
                            //AllotBala = corpReceivable.AllotBala
                        };
                        
                        //写入合约收款信息
                        result = contractReceivableDAL.Insert(user, contractReceivable);
                        if (result.ResultStatus != 0)
                            return result;

                        int contractRefId = (int)result.ReturnValue;

                        //新建库存收款信息
                        Model.StcokReceivable stcokReceivable = new StcokReceivable()
                        {
                            AllotId = receivableAllotId,
                            CorpRefId = corpRefId,
                            ContractRefId = contractRefId,
                            RecId = corpReceivable.RecId,
                            StockId = stockId,
                            StockNameId = stockNameId,
                            //AllotBala = corpReceivable.AllotBala
                        };

                        //写入库存收款信息
                        result = stcokReceivableDAL.Insert(user, stcokReceivable);
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

        public ResultModel GetStockAllotAmount(UserModel user, int stockId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = receivableallotDAL.GetStockAllotAmount(user, stockId);
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

        public ResultModel ReceivableStockUpdateHandle(UserModel user, List<Model.CorpReceivable> corpReceivables, int curId, string memo, int allotId,int stockId,
            int stockNameId,int allotFrom)
        {
            ResultModel result = new ResultModel();
            DAL.ReceivableDAL receivableDAL = new ReceivableDAL();
            DAL.CorpReceivableDAL corpReceivableDAL = new CorpReceivableDAL();
            DAL.ContractReceivableDAL contractReceivableDAL = new ContractReceivableDAL();
            DAL.StcokReceivableDAL stcokReceivableDAL = new StcokReceivableDAL();
            NFMT.User.DAL.CorporationDAL corporationDAL = new User.DAL.CorporationDAL();
            NFMT.WareHouse.DAL.StockLogDAL stockLogDAL=new WareHouse.DAL.StockLogDAL();
            Model.Receivable receivable = new Receivable();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    decimal allotBala = 0;
                    foreach (Model.CorpReceivable corpReceivable in corpReceivables)
                    {
                        //allotBala += corpReceivable.AllotBala;
                    }

                    result = receivableallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ReceivableAllot receivableAllot = result.ReturnValue as Model.ReceivableAllot;
                    receivableAllot.AllotDesc = memo;
                    receivableAllot.AllotBala = allotBala;
                    receivableAllot.CurrencyId = curId;
                    receivableAllot.AllotFrom = allotFrom;
                    
                    //更新收款分配表
                    result = receivableallotDAL.Update(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废公司收款分配
                    result = corpReceivableDAL.InvalidAll(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废合约收款分配
                    result = contractReceivableDAL.InvalidAll(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废库存收款分配
                    result = stcokReceivableDAL.InvalidAll(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取合约Id
                    result = stockLogDAL.GetStockContractId(user, stockId);
                    if (result.ResultStatus != 0)
                        return result;

                    string contractStr = result.ReturnValue.ToString();

                    foreach (Model.CorpReceivable corpReceivable in corpReceivables)
                    {
                        result = corporationDAL.Get(user, corpReceivable.CorpId);
                        if (result.ResultStatus != 0)
                            return result;

                        User.Model.Corporation corp = result.ReturnValue as User.Model.Corporation;
                        corpReceivable.BlocId = corp.ParentId;

                        //写入公司收款分配
                        result = corpReceivableDAL.Insert(user, corpReceivable);
                        if (result.ResultStatus != 0)
                            return result;

                        int corpRefId = (int)result.ReturnValue;

                        Model.ContractReceivable contractReceivable = new ContractReceivable()
                        {
                            CorpRefId = corpRefId,
                            AllotId = allotId,
                            RecId = corpReceivable.RecId,
                            ContractId = Convert.ToInt32(contractStr.Split(',')[0]),
                            SubContractId = Convert.ToInt32(contractStr.Split(',')[1]),
                            //AllotBala = corpReceivable.AllotBala
                        };

                        //写入合约收款分配
                        result = contractReceivableDAL.Insert(user, contractReceivable);
                        if (result.ResultStatus != 0)
                            return result;

                        int contractRefId = (int)result.ReturnValue;

                        Model.StcokReceivable stcokReceivable = new StcokReceivable()
                        {
                            AllotId = allotId,
                            CorpRefId = corpRefId,
                            ContractRefId = contractRefId,
                            RecId = corpReceivable.RecId,
                            StockId = stockId,
                            StockNameId = stockNameId,
                            //AllotBala = corpReceivable.AllotBala
                        };

                        //写入库存收款分配
                        result = stcokReceivableDAL.Insert(user, stcokReceivable);
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

        public SelectModel GetAllotForStockSelectModel(int pageIndex, int pageSize, string orderStr, int receivableAllotId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ref.RefId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" ref.RefId,CONVERT(varchar,r.PayBala)+c1.CurrencyName as PayBala,CONVERT(varchar,ref.AllotBala) +c1.CurrencyName as AllotBala,sn.RefNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_StcokReceivable_Ref ref  ");
            sb.Append(" left join dbo.Fun_Receivable r on ref.RecId = r.ReceivableId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c1 on r.CurrencyId = c1.CurrencyId ");
            sb.Append(" left join NFMT.dbo.St_Stock s on ref.StockId = s.StockId ");
            sb.Append(" left join NFMT.dbo.St_StockName sn on s.StockNameId = sn.StockNameId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" ref.RefStatus = {0} and ref.AllotId = {1}", (int)Common.StatusEnum.已生效, receivableAllotId);
            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel ReceivableStockInvalid(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = receivableallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ReceivableAllot receivableAllot = result.ReturnValue as Model.ReceivableAllot;
                    result = receivableallotDAL.Invalid(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    DAL.StcokReceivableDAL stcokReceivableDAL = new StcokReceivableDAL();
                    result = stcokReceivableDAL.InvalidAll(user, allotId);
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

        /// <summary>
        /// 有用，公司分配列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="empId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public SelectModel GetReceCorpSelectModel(int pageIndex, int pageSize, string orderStr, int empId, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ra.ReceivableAllotId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;
            //int readyStatus = (int)Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("  ra.ReceivableAllotId,ra.AllotTime,ra.AllotDesc,CONVERT(varchar,ra.AllotBala) + c.CurrencyName as AllotBala,e.Name,ra.AllotStatus,bd.StatusName,co.CorpName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_ReceivableAllot ra ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on ra.CurrencyId = c.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on ra.EmpId =e.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on ra.AllotStatus = bd.DetailId and StatusId = {0} ", statusId);
            sb.Append(" left join (select distinct ref.AllotId,ref.CorpId from dbo.Fun_CorpReceivable_Ref ref inner join dbo.Fun_RecAllotDetail rad on rad.DetailId = ref.DetailId) as ref  on ref.AllotId = ra.ReceivableAllotId ");
            sb.Append(" left join NFMT_User.dbo.Corporation co on ref.CorpId = co.CorpId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" ref.CorpId is not null ");
            if (status > 0)
                sb.AppendFormat(" and ra.AllotStatus = {0} ", status);
            if (empId > 0)
                sb.AppendFormat(" and ra.EmpId = {0} ", empId);

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 有用，外部公司列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="CorpCode"></param>
        /// <param name="CorpName"></param>
        /// <param name="blocId"></param>
        /// <returns></returns>
        public SelectModel GetReceCorpReadyListSelect(int pageIndex, int pageSize, string orderStr, string CorpCode, string CorpName, int blocId)
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
            //int status = (int)NFMT.Common.StatusEnum.已录入;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("C.CorpId,B.BlocName,C.CorpName,C.CorpEName,C.TaxPayerId,C.CorpFullName,C.CorpFullEName,C.CorpAddress,BD.DetailName");
            sb.Append(",BDD2.StatusName,case C.IsSelf when 1 then '己方公司' when 0 then '非己方公司' else '' end as IsSelf");
            //sb.Append(",allot.SumAllotBala,cur.CurrencyName,cast(isnull(allot.SumAllotBala,0) as varchar) + cur.CurrencyName as AllotBala");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT_User.dbo.Corporation C ");
            sb.Append(" left join NFMT_User.dbo.Bloc B on C.ParentId = B.BlocId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail BD on BD.StyleDetailId = C.CorpType and BD.BDStyleId ={0} ", bDStyleId);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail BDD2 on BDD2.DetailId = C.CorpStatus and BDD2.StatusId= {0} ",bDStatusId);

            //sb.Append(" left join (select SUM(isnull(rad.AllotBala,0)) as SumAllotBala,ra.CurrencyId,cr.CorpId ");
            //sb.Append(" from dbo.Fun_CorpReceivable_Ref cr ");
            //sb.Append(" inner join dbo.Fun_RecAllotDetail rad on rad.DetailId = cr.DetailId ");
            //sb.Append(" inner join dbo.Fun_ReceivableAllot ra on cr.AllotId = ra.ReceivableAllotId ");
            //sb.AppendFormat(" where rad.DetailStatus>={0} and ra.AllotStatus>={0} ", readyStatus);
            //sb.Append(" group by ra.CurrencyId,cr.CorpId) as allot on allot.CorpId = c.CorpId ");

            //sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = allot.CurrencyId ");
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

        public SelectModel GetReceCorpAllotSelectModel(int pageIndex, int pageSize, string orderStr, int corpId,int allotId =0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ra.ReceivableAllotId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("ra.ReceivableAllotId,");
            sb.Append("ra.AllotTime,");
            sb.Append("e.Name,");
            sb.Append("CONVERT(varchar,ra.AllotBala)+c.CurrencyName as AllotBala,");
            sb.Append("bd.StatusName,");
            sb.Append("ra.AllotStatus");
            select.ColumnName = sb.ToString();

            sb.Clear();
            //sb.Append(" dbo.Fun_ReceivableAllot ra left join NFMT_User.dbo.Employee e on ra.EmpId = e.EmpId ");
            //sb.Append(" left join NFMT_Basic.dbo.Currency c on ra.CurrencyId = c.CurrencyId ");
            //sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on ra.AllotStatus = bd.DetailId and StatusId = {0} ", statusId);
            //sb.AppendFormat(" right join (select distinct AllotId from dbo.Fun_CorpReceivable_Ref ref left join NFMT_User.dbo.Corporation co on ref.CorpId= co.CorpId where ref.CorpId = {0}) ref on ref.AllotId = ra.ReceivableAllotId", corpId);
            sb.Append(" dbo.Fun_ReceivableAllot ra ");
            sb.Append(" inner join (select distinct AllotId ");
            sb.Append(" from dbo.Fun_CorpReceivable_Ref ref ");
            sb.AppendFormat(" left join NFMT_User.dbo.Corporation co on ref.CorpId= co.CorpId where ref.CorpId = {0} and ref.AllotId!={1} ",corpId,allotId);
            sb.Append(" ) ref on ref.AllotId = ra.ReceivableAllotId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on ra.EmpId = e.EmpId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c on ra.CurrencyId = c.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on ra.AllotStatus = bd.DetailId and StatusId = {0} ", statusId);

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" ra.AllotStatus >= {0}", (int)Common.StatusEnum.已录入);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel ReceivableCorpCreateHandle(UserModel user, List<Model.CorpReceivable> corpReceivables, int curId, string memo)
        {
            ResultModel result = new ResultModel();
            DAL.ReceivableDAL receivableDAL = new ReceivableDAL();
            DAL.CorpReceivableDAL corpReceivableDAL = new CorpReceivableDAL();
            Model.Receivable receivable = new Receivable();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    decimal allotBala = 0;
                    foreach (Model.CorpReceivable corpReceivable in corpReceivables)
                    {
                        //allotBala += corpReceivable.AllotBala;
                    }

                    Model.ReceivableAllot receivableAllot = new ReceivableAllot()
                    {
                        AllotFrom = NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.分配来源)["Receivable"].StyleDetailId,
                        AllotBala = allotBala,
                        CurrencyId = curId,
                        AllotDesc = memo,
                        EmpId = user.EmpId,
                        AllotTime = DateTime.Now
                    };

                    result = receivableallotDAL.Insert(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    int receivableAllotId = (int)result.ReturnValue;

                    foreach (Model.CorpReceivable corpReceivable in corpReceivables)
                    {
                        corpReceivable.AllotId = receivableAllotId;
                        result = corpReceivableDAL.Insert(user, corpReceivable);
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

        public ResultModel ReceivableCorpUpdateHandle(UserModel user, List<Model.CorpReceivable> corpReceivables, int curId, string memo, int allotId)
        {
            ResultModel result = new ResultModel();
            DAL.ReceivableDAL receivableDAL = new ReceivableDAL();
            DAL.CorpReceivableDAL corpReceivableDAL = new CorpReceivableDAL();
            Model.Receivable receivable = new Receivable();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    decimal allotBala = 0;
                    foreach (Model.CorpReceivable corpReceivable in corpReceivables)
                    {
                        //allotBala += corpReceivable.AllotBala;
                    }

                    result = receivableallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ReceivableAllot receivableAllot = result.ReturnValue as Model.ReceivableAllot;
                    receivableAllot.AllotDesc = memo;
                    receivableAllot.AllotBala = allotBala;

                    result = receivableallotDAL.Update(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    result = corpReceivableDAL.InvalidAll(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    foreach (Model.CorpReceivable corpReceivable in corpReceivables)
                    {
                        corpReceivable.AllotId = allotId;
                        result = corpReceivableDAL.Insert(user, corpReceivable);
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

        public SelectModel GetAllotForCorpSelectModel(int pageIndex, int pageSize, string orderStr, int receivableAllotId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ref.RefId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" ref.RefId,CONVERT(varchar,r.PayBala)+c1.CurrencyName as PayBala,CONVERT(varchar,ref.AllotBala) +c1.CurrencyName as AllotBala,co.CorpName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_CorpReceivable_Ref ref  ");
            sb.Append(" left join dbo.Fun_Receivable r on ref.RecId = r.ReceivableId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency c1 on r.CurrencyId = c1.CurrencyId ");
            sb.Append(" left join NFMT_User.dbo.Corporation co on ref.CorpId = co.CorpId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" ref.RefStatus = {0} and ref.AllotId = {1}", (int)Common.StatusEnum.已生效, receivableAllotId);
            select.WhereStr = sb.ToString();

            return select;
        }       

        public SelectModel GetCorpCanAllotSelectModel(int pageIndex, int pageSize, string orderStr, string refIds, int subId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ref.RefId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat(" ref.RefId,c.CorpName,CONVERT(varchar,ref.AllotBala)+(select top 1 c.CurrencyName from dbo.Fun_CorpReceivable_Ref a left join dbo.Fun_Receivable b on a.RecId = b.ReceivableId left join NFMT_Basic.dbo.Currency c on b.CurrencyId = c.CurrencyId where a.RefId = ref.RefId) as AllotBala,(ref.AllotBala - Isnull((select SUM(ISNULL(crr.AllotBala,0)) from dbo.Fun_ContractReceivable_Ref crr where crr.RefStatus ={0} and crr.CorpRefId = ref.RefId),0)) as CanAllotBala ", (int)Common.StatusEnum.已生效);
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_CorpReceivable_Ref ref ");
            sb.Append(" left join NFMT_User.dbo.Corporation c on ref.CorpId = c.CorpId ");
            sb.Append(" left join dbo.Fun_ReceivableAllot allot on ref.AllotId = allot.ReceivableAllotId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" allot.CurrencyId = (select SettleCurrency from dbo.Con_ContractSub where SubId = {0}) ", subId);
            sb.AppendFormat(" and ref.RefStatus = {0} ", (int)Common.StatusEnum.已生效);
            sb.AppendFormat(" and (ref.AllotBala - Isnull((select SUM(ISNULL(crr.AllotBala,0)) from dbo.Fun_ContractReceivable_Ref crr where crr.RefStatus ={0} and  crr.CorpRefId = ref.RefId),0)) >0 ", (int)Common.StatusEnum.已生效);
            if (!string.IsNullOrEmpty(refIds))
                sb.AppendFormat(" and ref.RefId not in ({0})", refIds);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel ReceivableCorpAllotCreateHandle(UserModel user, List<Model.ContractReceivable> contractReceivables, int curId, string memo,int allotFrom)
        {
            ResultModel result = new ResultModel();
            DAL.ReceivableDAL receivableDAL = new ReceivableDAL();
            DAL.ContractReceivableDAL contractReceivableDAL = new ContractReceivableDAL();
            DAL.CorpReceivableDAL corpReceivableDAL = new CorpReceivableDAL();
            Model.Receivable receivable = new Receivable();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    decimal allotBala = 0;
                    foreach (Model.ContractReceivable ContractReceivable in contractReceivables)
                    {
                        //allotBala += ContractReceivable.AllotBala;
                    }

                    Model.ReceivableAllot receivableAllot = new ReceivableAllot()
                    {
                        AllotFrom = allotFrom,
                        AllotBala = allotBala,
                        CurrencyId = curId,
                        AllotDesc = memo,
                        EmpId = user.EmpId,
                        AllotTime = DateTime.Now
                    };

                    result = receivableallotDAL.Insert(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    int receivableAllotId = (int)result.ReturnValue;

                    foreach (Model.ContractReceivable contractReceivable in contractReceivables)
                    {
                        contractReceivable.AllotId = receivableAllotId;
                        result = corpReceivableDAL.Get(user, contractReceivable.CorpRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CorpReceivable corpReceivable = result.ReturnValue as Model.CorpReceivable;
                        if (corpReceivable == null)
                            return result;

                        contractReceivable.RecId = corpReceivable.RecId;

                        result = contractReceivableDAL.Insert(user, contractReceivable);
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

        public ResultModel ReceAllotCorpUpdateHandle(UserModel user, List<Model.ContractReceivable> contractReceivables, int curId, string memo, int allotId, int contractId, int subId)
        {
            ResultModel result = new ResultModel();
            DAL.ReceivableDAL receivableDAL = new ReceivableDAL();
            DAL.ContractReceivableDAL contractReceivableDAL = new ContractReceivableDAL();
            DAL.CorpReceivableDAL corpReceivableDAL = new CorpReceivableDAL();
            NFMT.User.DAL.CorporationDAL corporationDAL = new User.DAL.CorporationDAL();
            Model.Receivable receivable = new Receivable();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    decimal allotBala = 0;
                    foreach (Model.ContractReceivable contractReceivable in contractReceivables)
                    {
                        //allotBala += contractReceivable.AllotBala;
                    }

                    result = receivableallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ReceivableAllot receivableAllot = result.ReturnValue as Model.ReceivableAllot;
                    receivableAllot.AllotDesc = memo;
                    receivableAllot.AllotBala = allotBala;

                    //更新分配信息
                    result = receivableallotDAL.Update(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废所有allotId下的合约收款分配
                    result = contractReceivableDAL.InvalidAll(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    foreach (Model.ContractReceivable contractReceivable in contractReceivables)
                    {
                        result = corpReceivableDAL.Get(user, contractReceivable.CorpRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CorpReceivable corpReceivable = result.ReturnValue as Model.CorpReceivable;

                        contractReceivable.RecId = corpReceivable.RecId;

                        //写入合约收款分配表
                        result = contractReceivableDAL.Insert(user, contractReceivable);
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

        public SelectModel GetCorpCanAllotForStockSelectModel(int pageIndex, int pageSize, string orderStr, string refIds,int stockId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ref.RefId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat(" ref.RefId,c.CorpName,CONVERT(varchar,ref.AllotBala)+(select top 1 c.CurrencyName from dbo.Fun_CorpReceivable_Ref a left join dbo.Fun_Receivable b on a.RecId = b.ReceivableId left join NFMT_Basic.dbo.Currency c on b.CurrencyId = c.CurrencyId where a.RefId = ref.RefId) as AllotBala,(ref.AllotBala - Isnull((select SUM(ISNULL(crr.AllotBala,0)) from dbo.Fun_ContractReceivable_Ref crr where crr.RefStatus ={0} and crr.CorpRefId = ref.RefId),0)) as CanAllotBala ", (int)Common.StatusEnum.已生效);
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_CorpReceivable_Ref ref ");
            sb.Append(" left join NFMT_User.dbo.Corporation c on ref.CorpId = c.CorpId ");
            sb.Append(" left join dbo.Fun_Receivable r on ref.RecId = r.ReceivableId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on r.CurrencyId = cur.CurrencyId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" ref.RefStatus = {0} ", (int)Common.StatusEnum.已生效);
            sb.AppendFormat(" and (ref.AllotBala - Isnull((select SUM(ISNULL(crr.AllotBala,0)) from dbo.Fun_ContractReceivable_Ref crr where crr.RefStatus ={0} and  crr.CorpRefId = ref.RefId),0)) >0 ", (int)Common.StatusEnum.已生效);
            sb.AppendFormat(" and cur.CurrencyId = (select sub.SettleCurrency from dbo.St_StockLog slog left join dbo.Con_ContractSub sub on slog.SubContractId = sub.SubId where slog.StockId = {0})", stockId);
            if (!string.IsNullOrEmpty(refIds))
                sb.AppendFormat(" and ref.RefId not in ({0})", refIds);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetContractCanAllotForStockSelectModel(int pageIndex, int pageSize, string orderStr, string refIds, int stockId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ref.RefId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat(" ref.RefId,sub.SubNo,CONVERT(varchar,ref.AllotBala)+(select top 1 c.CurrencyName from dbo.Fun_ContractReceivable_Ref a left join dbo.Fun_Receivable b on a.RecId = b.ReceivableId left join NFMT_Basic.dbo.Currency c on b.CurrencyId = c.CurrencyId where a.RefId = ref.RefId) as AllotBala,(ref.AllotBala - Isnull((select SUM(ISNULL(crr.AllotBala,0)) from dbo.Fun_StcokReceivable_Ref crr where crr.RefStatus ={0} and crr.ContractRefId = ref.RefId),0)) as CanAllotBala ", (int)Common.StatusEnum.已生效);
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_ContractReceivable_Ref ref ");
            sb.Append(" left join NFMT.dbo.Con_ContractSub sub on ref.SubContractId = sub.SubId ");
            sb.Append(" left join dbo.Fun_Receivable r on ref.RecId = r.ReceivableId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on r.CurrencyId = cur.CurrencyId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" ref.RefStatus = {0} ", (int)Common.StatusEnum.已生效);
            sb.AppendFormat(" and (ref.AllotBala - Isnull((select SUM(ISNULL(crr.AllotBala,0)) from dbo.Fun_StcokReceivable_Ref crr where crr.RefStatus ={0} and  crr.ContractRefId = ref.RefId),0)) >0 ", (int)Common.StatusEnum.已生效);
            sb.AppendFormat(" and cur.CurrencyId = (select sub.SettleCurrency from dbo.St_StockLog slog left join dbo.Con_ContractSub sub on slog.SubContractId = sub.SubId where slog.StockId = {0})", stockId);
            if (!string.IsNullOrEmpty(refIds))
                sb.AppendFormat(" and ref.RefId not in ({0})", refIds);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel ReceivableStockCreateByCorpHandle(UserModel user, List<Model.ContractReceivable> contractReceivables, int curId, string memo, int allotFrom, int stockId, int stockNameId)
        {
            ResultModel result = new ResultModel();
            DAL.ReceivableDAL receivableDAL = new ReceivableDAL();
            DAL.CorpReceivableDAL corpReceivableDAL = new CorpReceivableDAL();
            DAL.ContractReceivableDAL contractReceivableDAL = new ContractReceivableDAL();
            DAL.StcokReceivableDAL stcokReceivableDAL = new StcokReceivableDAL();
            NFMT.User.DAL.CorporationDAL corporationDAL = new User.DAL.CorporationDAL();
            NFMT.WareHouse.DAL.StockLogDAL stockLogDAL = new WareHouse.DAL.StockLogDAL();
            Model.Receivable receivable = new Receivable();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    decimal allotBala = 0;
                    foreach (Model.ContractReceivable contractReceivable in contractReceivables)
                    {
                        //allotBala += contractReceivable.AllotBala;
                    }

                    Model.ReceivableAllot receivableAllot = new ReceivableAllot()
                    {
                        AllotFrom = allotFrom,
                        AllotBala = allotBala,
                        CurrencyId = curId,
                        AllotDesc = memo,
                        EmpId = user.EmpId,
                        AllotTime = DateTime.Now
                    };

                    //写入收款分配表
                    result = receivableallotDAL.Insert(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    int receivableAllotId = (int)result.ReturnValue;

                    result = stockLogDAL.GetStockContractId(user, stockId);
                    if (result.ResultStatus != 0)
                        return result;

                    string contractStr = result.ReturnValue.ToString();

                    foreach (Model.ContractReceivable contractReceivable in contractReceivables)
                    {
                        result = corpReceivableDAL.Get(user, contractReceivable.CorpRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CorpReceivable corpReceivable = result.ReturnValue as Model.CorpReceivable;

                        contractReceivable.AllotId = receivableAllotId;
                        contractReceivable.ContractId = Convert.ToInt32(contractStr.Split(',')[0]);
                        contractReceivable.SubContractId = Convert.ToInt32(contractStr.Split(',')[1]);
                        contractReceivable.RecId = corpReceivable.RecId;

                        //写入合约收款信息
                        result = contractReceivableDAL.Insert(user, contractReceivable);
                        if (result.ResultStatus != 0)
                            return result;

                        int contractRefId = (int)result.ReturnValue;

                        //新建库存收款信息
                        Model.StcokReceivable stcokReceivable = new StcokReceivable()
                        {
                            AllotId = receivableAllotId,
                            CorpRefId = contractReceivable.CorpRefId,
                            ContractRefId = contractRefId,
                            RecId = corpReceivable.RecId,
                            StockId = stockId,
                            StockNameId = stockNameId,
                            //AllotBala = contractReceivable.AllotBala
                        };

                        //写入库存收款信息
                        result = stcokReceivableDAL.Insert(user, stcokReceivable);
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

        public ResultModel ReceivableStockCreateByContractHandle(UserModel user, List<Model.StcokReceivable> stcokReceivables, int curId, string memo, int allotFrom)
        {
            ResultModel result = new ResultModel();
            DAL.ReceivableDAL receivableDAL = new ReceivableDAL();
            DAL.CorpReceivableDAL corpReceivableDAL = new CorpReceivableDAL();
            DAL.ContractReceivableDAL contractReceivableDAL = new ContractReceivableDAL();
            DAL.StcokReceivableDAL stcokReceivableDAL = new StcokReceivableDAL();
            NFMT.User.DAL.CorporationDAL corporationDAL = new User.DAL.CorporationDAL();
            NFMT.WareHouse.DAL.StockLogDAL stockLogDAL = new WareHouse.DAL.StockLogDAL();
            Model.Receivable receivable = new Receivable();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    decimal allotBala = 0;
                    foreach (Model.StcokReceivable stcokReceivable in stcokReceivables)
                    {
                        //allotBala += stcokReceivable.AllotBala;
                    }

                    Model.ReceivableAllot receivableAllot = new ReceivableAllot()
                    {
                        AllotFrom = allotFrom,
                        AllotBala = allotBala,
                        CurrencyId = curId,
                        AllotDesc = memo,
                        EmpId = user.EmpId,
                        AllotTime = DateTime.Now
                    };

                    //写入收款分配表
                    result = receivableallotDAL.Insert(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    int receivableAllotId = (int)result.ReturnValue;

                    foreach (Model.StcokReceivable stcokReceivable in stcokReceivables)
                    {
                        stcokReceivable.AllotId = receivableAllotId;

                        result = contractReceivableDAL.Get(user, stcokReceivable.ContractRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.ContractReceivable contractReceivable = result.ReturnValue as Model.ContractReceivable;
                        stcokReceivable.CorpRefId = contractReceivable.CorpRefId;

                        stcokReceivable.RecId = contractReceivable.RecId;

                        //写入库存收款信息
                        result = stcokReceivableDAL.Insert(user, stcokReceivable);
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

        public ResultModel ReceivableStockUpdateForCorpHandle(UserModel user, List<Model.ContractReceivable> contractReceivables, int curId, string memo, int allotId, int stockId, int stockNameId, int allotFrom)
        {
            ResultModel result = new ResultModel();
            DAL.ReceivableDAL receivableDAL = new ReceivableDAL();
            DAL.CorpReceivableDAL corpReceivableDAL = new CorpReceivableDAL();
            DAL.ContractReceivableDAL contractReceivableDAL = new ContractReceivableDAL();
            DAL.StcokReceivableDAL stcokReceivableDAL = new StcokReceivableDAL();
            NFMT.User.DAL.CorporationDAL corporationDAL = new User.DAL.CorporationDAL();
            NFMT.WareHouse.DAL.StockLogDAL stockLogDAL = new WareHouse.DAL.StockLogDAL();
            Model.Receivable receivable = new Receivable();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    decimal allotBala = 0;
                    foreach (Model.ContractReceivable contractReceivable in contractReceivables)
                    {
                        //allotBala += contractReceivable.AllotBala;
                    }

                    result = receivableallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ReceivableAllot receivableAllot = result.ReturnValue as Model.ReceivableAllot;
                    receivableAllot.AllotDesc = memo;
                    receivableAllot.AllotBala = allotBala;
                    receivableAllot.CurrencyId = curId;
                    receivableAllot.AllotFrom = allotFrom;

                    //更新收款分配表
                    result = receivableallotDAL.Update(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    ////作废公司收款分配
                    //result = corpReceivableDAL.InvalidAll(user, allotId);
                    //if (result.ResultStatus != 0)
                    //    return result;

                    //作废合约收款分配
                    result = contractReceivableDAL.InvalidAll(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废库存收款分配
                    result = stcokReceivableDAL.InvalidAll(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取合约Id
                    result = stockLogDAL.GetStockContractId(user, stockId);
                    if (result.ResultStatus != 0)
                        return result;

                    string contractStr = result.ReturnValue.ToString();

                    foreach (Model.ContractReceivable contractReceivable in contractReceivables)
                    {
                        result = corpReceivableDAL.Get(user,contractReceivable.CorpRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.CorpReceivable corpReceivable = result.ReturnValue as Model.CorpReceivable;

                        contractReceivable.RecId = corpReceivable.RecId;
                        contractReceivable.ContractId = Convert.ToInt32(contractStr.Split(',')[0]);
                        contractReceivable.SubContractId = Convert.ToInt32(contractStr.Split(',')[1]);

                        //写入合约收款分配
                        result = contractReceivableDAL.Insert(user, contractReceivable);
                        if (result.ResultStatus != 0)
                            return result;

                        int contractRefId = (int)result.ReturnValue;

                        Model.StcokReceivable stcokReceivable = new StcokReceivable()
                        {
                            AllotId = allotId,
                            CorpRefId = contractReceivable.CorpRefId,
                            ContractRefId = contractRefId,
                            RecId = corpReceivable.RecId,
                            StockId = stockId,
                            StockNameId = stockNameId,
                            //AllotBala = corpReceivable.AllotBala
                        };

                        //写入库存收款分配
                        result = stcokReceivableDAL.Insert(user, stcokReceivable);
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

        public ResultModel ReceivableStockUpdateForContractHandle(UserModel user, List<Model.StcokReceivable> stcokReceivables, int curId, string memo, int allotId, int allotFrom)
        {
            ResultModel result = new ResultModel();
            DAL.ReceivableDAL receivableDAL = new ReceivableDAL();
            DAL.CorpReceivableDAL corpReceivableDAL = new CorpReceivableDAL();
            DAL.ContractReceivableDAL contractReceivableDAL = new ContractReceivableDAL();
            DAL.StcokReceivableDAL stcokReceivableDAL = new StcokReceivableDAL();
            NFMT.User.DAL.CorporationDAL corporationDAL = new User.DAL.CorporationDAL();
            NFMT.WareHouse.DAL.StockLogDAL stockLogDAL = new WareHouse.DAL.StockLogDAL();
            Model.Receivable receivable = new Receivable();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    decimal allotBala = 0;
                    foreach (Model.StcokReceivable stcokReceivable in stcokReceivables)
                    {
                        //allotBala += stcokReceivable.AllotBala;
                    }

                    result = receivableallotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ReceivableAllot receivableAllot = result.ReturnValue as Model.ReceivableAllot;
                    receivableAllot.AllotDesc = memo;
                    receivableAllot.AllotBala = allotBala;
                    receivableAllot.CurrencyId = curId;
                    receivableAllot.AllotFrom = allotFrom;

                    //更新收款分配表
                    result = receivableallotDAL.Update(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    ////作废公司收款分配
                    //result = corpReceivableDAL.InvalidAll(user, allotId);
                    //if (result.ResultStatus != 0)
                    //    return result;

                    ////作废合约收款分配
                    //result = contractReceivableDAL.InvalidAll(user, allotId);
                    //if (result.ResultStatus != 0)
                    //    return result;

                    //作废库存收款分配
                    result = stcokReceivableDAL.InvalidAll(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    foreach (Model.StcokReceivable stcokReceivable in stcokReceivables)
                    {
                        result = contractReceivableDAL.Get(user, stcokReceivable.ContractRefId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.ContractReceivable contractReceivable = result.ReturnValue as Model.ContractReceivable;
                        stcokReceivable.CorpRefId = contractReceivable.CorpRefId;
                        stcokReceivable.RecId = contractReceivable.RecId;

                        //写入库存收款分配
                        result = stcokReceivableDAL.Insert(user, stcokReceivable);
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

        /// <summary>
        /// 有用，可分配收款登记
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="rids"></param>
        /// <param name="corpName"></param>
        /// <param name="currencyId"></param>
        /// <param name="dids"></param>
        /// <param name="allotId"></param>
        /// <returns></returns>
        public SelectModel GetReceivableForCorpSelectModel(int pageIndex, int pageSize, string orderStr, string rids, string corpName, int currencyId,string dids,int allotId =0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "r.ReceivableId asc";
            else
                select.OrderStr = orderStr;

            if (string.IsNullOrEmpty(rids))
                rids = "0";
            if (string.IsNullOrEmpty(dids))
                dids = "0";

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("r.ReceivableId,r.ReceiveDate,c.CorpName as InnerCorp,b.BankName,r.PayCorpName as OutCorp");
            sb.Append(",CONVERT(varchar,r.PayBala)+cu.CurrencyName as PayBala,cu.CurrencyId");
            sb.Append(",isnull(allot.AllotBala,0) as AllotBala, r.PayBala -ISNULL(allot.AllotBala,0) as CanAllotBala");
            select.ColumnName = sb.ToString();

            sb.Clear();            
            sb.Append(" dbo.Fun_Receivable r ");
            sb.Append(" left join NFMT_User.dbo.Corporation c on r.ReceivableCorpId = c.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cu on r.CurrencyId = cu.CurrencyId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank b on r.ReceivableBank = b.BankId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c2 on r.PayCorpId = c2.CorpId ");

            //sb.Append(" left join (select SUM(isnull(rad.AllotBala,0)) as AllotBala,rad.RecId ");
            //sb.AppendFormat(" from dbo.Fun_RecAllotDetail rad where rad.DetailStatus>={0} group by rad.RecId) as allot on allot.RecId= r.ReceivableId ", readyStatus);

            sb.Append(" left join (select SUM(isnull(rad.AllotBala,0)) as AllotBala,rad.RecId ");
            sb.AppendFormat(" from dbo.Fun_RecAllotDetail rad where rad.DetailStatus>={0} ",readyStatus);
            sb.AppendFormat(" and rad.DetailId not in ({0}) ",dids);
            sb.Append(" group by rad.RecId) as allot on allot.RecId= r.ReceivableId ");

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" r.ReceiveStatus >= {0}", readyStatus);
            sb.Append(" and r.PayBala > isnull(allot.AllotBala,0) ");
            if (currencyId > 0)
                sb.AppendFormat(" and r.CurrencyId = {0} ", currencyId);
            if (!string.IsNullOrEmpty(corpName))
                sb.AppendFormat(" and r.PayCorpName like '%{0}%' ", corpName);
            if (!string.IsNullOrEmpty(rids))
                sb.AppendFormat(" and r.ReceivableId not in ({0})", rids);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetReceivableForCorpUpdateSelectModel(int pageIndex, int pageSize, string orderStr, string rids, int corpId, int currencyId,string selectRids)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "r.ReceivableId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" r.ReceivableId,r.ReceiveDate,c.CorpName as InnerCorp,b.BankName,c2.CorpName as OutCorp,CONVERT(varchar,r.PayBala)+cu.CurrencyName as PayBala,");
            if (!string.IsNullOrEmpty(selectRids))
                sb.AppendFormat(" (r.PayBala - isnull((select SUM(ISNULL(ref1.AllotBala,0)) from dbo.Fun_CorpReceivable_Ref ref1 where ref1.RecId = r.ReceivableId and ref1.RecId not in ({0})),0))  as CanAllotBala", selectRids);
            else
                sb.Append(" (r.PayBala - isnull((select SUM(ISNULL(ref1.AllotBala,0)) from dbo.Fun_CorpReceivable_Ref ref1 where ref1.RecId = r.ReceivableId),0))  as CanAllotBala");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_Receivable r left join NFMT_User.dbo.Corporation c on r.ReceivableCorpId = c.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cu on r.CurrencyId = cu.CurrencyId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank b on r.ReceivableBank = b.BankId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c2 on r.PayCorpId = c2.CorpId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" r.ReceiveStatus = {0}", (int)Common.StatusEnum.已生效);
            if (string.IsNullOrEmpty(selectRids))
                sb.Append(" and (r.PayBala - isnull((select SUM(ISNULL(ref1.AllotBala,0)) from dbo.Fun_CorpReceivable_Ref ref1 where ref1.RecId = r.ReceivableId),0))>0 ");
            else
                sb.AppendFormat(" and (r.PayBala - isnull((select SUM(ISNULL(ref1.AllotBala,0)) from dbo.Fun_CorpReceivable_Ref ref1 where ref1.RecId = r.ReceivableId and ref1.RecId not in ({0})),0))>0 ", selectRids);
            if (currencyId > 0)
                sb.AppendFormat(" and r.CurrencyId = {0} ", currencyId);
            if (corpId > 0)
                sb.AppendFormat(" and r.PayCorpId = {0} ", corpId);
            if (!string.IsNullOrEmpty(rids))
                sb.AppendFormat(" and r.ReceivableId not in ({0})", rids);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel ReceCorpComplete(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAL.ReceivableAllotDAL receivableAllotDAL = new ReceivableAllotDAL();
                    result = receivableAllotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ReceivableAllot receivableAllot = result.ReturnValue as Model.ReceivableAllot;
                    if (receivableAllot == null || receivableAllot.ReceivableAllotId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收款分配不存在";
                        return result;
                    }

                    //收款分配完成
                    result = receivableAllotDAL.Confirm(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    //根据收款分配ID获取公司收款分配
                    DAL.CorpReceivableDAL corpReceivableDAL = new CorpReceivableDAL();
                    result = corpReceivableDAL.GetCorpReceivableByAllotId(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CorpReceivable> corpReceivables = result.ReturnValue as List<Model.CorpReceivable>;
                    if (corpReceivables == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "公司收款分配不存在";
                        return result;
                    }

                    //公司收款分配完成
                    foreach (Model.CorpReceivable corpReceivable in corpReceivables)
                    {
                        result = corpReceivableDAL.Complete(user, corpReceivable);
                        if (result.ResultStatus != 0)
                            return result;
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

        public ResultModel ReceContractComplete(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAL.ReceivableAllotDAL receivableAllotDAL = new ReceivableAllotDAL();
                    result = receivableAllotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ReceivableAllot receivableAllot = result.ReturnValue as Model.ReceivableAllot;
                    if (receivableAllot == null || receivableAllot.ReceivableAllotId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收款分配不存在";
                        return result;
                    }

                    //收款分配完成
                    result = receivableAllotDAL.Confirm(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    //根据收款分配ID获取公司收款分配
                    DAL.CorpReceivableDAL corpReceivableDAL = new CorpReceivableDAL();
                    result = corpReceivableDAL.GetCorpReceivableByAllotId(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CorpReceivable> corpReceivables = result.ReturnValue as List<Model.CorpReceivable>;
                    if (corpReceivables == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "公司收款分配不存在";
                        return result;
                    }

                    //公司收款分配完成
                    foreach (Model.CorpReceivable corpReceivable in corpReceivables)
                    {
                        result = corpReceivableDAL.Complete(user, corpReceivable);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //根据收款分配ID获取合约收款分配
                    DAL.ContractReceivableDAL contractReceivableDAL = new ContractReceivableDAL();
                    result = contractReceivableDAL.GetContractReceivableByAllotId(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.ContractReceivable> contractReceivables = result.ReturnValue as List<Model.ContractReceivable>;
                    if (contractReceivables == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "合约收款分配不存在";
                        return result;
                    }

                    //合约收款分配完成
                    foreach (Model.ContractReceivable contractReceivable in contractReceivables)
                    {
                        result = contractReceivableDAL.Complete(user, contractReceivable);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    scope.Complete();
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return result;
        }

        public ResultModel ReceStockComplete(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAL.ReceivableAllotDAL receivableAllotDAL = new ReceivableAllotDAL();
                    result = receivableAllotDAL.Get(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ReceivableAllot receivableAllot = result.ReturnValue as Model.ReceivableAllot;
                    if (receivableAllot == null || receivableAllot.ReceivableAllotId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "收款分配不存在";
                        return result;
                    }

                    //收款分配完成
                    result = receivableAllotDAL.Confirm(user, receivableAllot);
                    if (result.ResultStatus != 0)
                        return result;

                    //根据收款分配ID获取公司收款分配
                    DAL.CorpReceivableDAL corpReceivableDAL = new CorpReceivableDAL();
                    result = corpReceivableDAL.GetCorpReceivableByAllotId(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.CorpReceivable> corpReceivables = result.ReturnValue as List<Model.CorpReceivable>;
                    if (corpReceivables == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "公司收款分配不存在";
                        return result;
                    }

                    //公司收款分配完成
                    foreach (Model.CorpReceivable corpReceivable in corpReceivables)
                    {
                        result = corpReceivableDAL.Complete(user, corpReceivable);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //根据收款分配ID获取合约收款分配
                    DAL.ContractReceivableDAL contractReceivableDAL = new ContractReceivableDAL();
                    result = contractReceivableDAL.GetContractReceivableByAllotId(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.ContractReceivable> contractReceivables = result.ReturnValue as List<Model.ContractReceivable>;
                    if (contractReceivables == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "合约收款分配不存在";
                        return result;
                    }

                    //合约收款分配完成
                    foreach (Model.ContractReceivable contractReceivable in contractReceivables)
                    {
                        result = contractReceivableDAL.Complete(user, contractReceivable);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //根据收款分配ID获取库存收款分配
                    DAL.StcokReceivableDAL stcokReceivableDAL = new StcokReceivableDAL();
                    result = stcokReceivableDAL.GetStockReceivableByAllotId(user, allotId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StcokReceivable> stcokReceivables = result.ReturnValue as List<Model.StcokReceivable>;
                    if (stcokReceivables == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "库存收款分配不存在";
                        return result;
                    }

                    //库存收款分配完成
                    foreach (Model.StcokReceivable stcokReceivable in stcokReceivables)
                    {
                        result = stcokReceivableDAL.Complete(user, stcokReceivable);
                        if (result.ResultStatus != 0)
                            return result;
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

        #endregion
    }
}
