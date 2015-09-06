/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractBLL.cs
// 文件功能描述：合约dbo.Con_Contract业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月16日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Contract.Model;
using NFMT.Contract.DAL;
using NFMT.Contract.IDAL;
using NFMT.Common;
using System.Linq;

namespace NFMT.Contract.BLL
{
    /// <summary>
    /// 合约dbo.Con_Contract业务逻辑类。
    /// </summary>
    public class ContractBLL : Common.ExecBLL
    {
        private ContractDAL contractDAL = new ContractDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ContractDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractBLL()
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
            get { return this.contractDAL; }
        }

        #endregion

        #region 新增方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="contractNo"></param>
        /// <param name="outerCorp"></param>
        /// <param name="status"></param>
        /// <param name="contractDateBegin"></param>
        /// <param name="contractDateEnd"></param>
        /// <param name="outContractNo"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, DateTime beginDate, DateTime endDate, string contractNo = "", int outerCorp = 0, int status = 0)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            select.OrderStr = string.IsNullOrEmpty(orderStr) ? "con.ContractId asc" : orderStr;

            select.ColumnName = "con.ContractId,con.ContractDate,con.ContractNo,con.OutContractNo,con.TradeDirection,td.DetailName as TradeDirectionName,inCorp.CorpId as InCorpId,inCorp.CorpName as InCorpName,outCorp.CorpId as OutCorpId,outCorp.CorpName as OutCorpName,con.AssetId,ass.AssetName,con.SignAmount,con.UnitId,mu.MUName,cast(con.SignAmount as varchar(20))+mu.MUName as ContractWeight,con.PriceMode,pm.DetailName as PriceModeName,con.ContractStatus,sd.StatusName,con.CreateFrom";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int statusType = (int)NFMT.Common.StatusTypeEnum.通用状态;
            int tradeDirectionStyle = (int)NFMT.Data.StyleEnum.TradeDirection;
            int priceModeStyle = (int)NFMT.Data.StyleEnum.PriceMode;

            sb.Append(" dbo.Con_Contract con ");
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail inCorp on con.ContractId = inCorp.ContractId and inCorp.IsInnerCorp = 1 and inCorp.IsDefaultCorp =1 and inCorp.DetailStatus = {0}", (int)StatusEnum.已生效);
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail outCorp on con.ContractId = outCorp.ContractId and outCorp.IsInnerCorp=0 and outCorp.IsDefaultCorp=1 and outCorp.DetailStatus = {0}", (int)StatusEnum.已生效);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail td on con.TradeDirection = td.StyleDetailId and td.BDStyleId={0} ", tradeDirectionStyle);
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on con.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId= con.UnitId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail pm on pm.StyleDetailId = con.PriceMode and pm.BDStyleId ={0} ", priceModeStyle);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId= con.ContractStatus and sd.StatusId={0} ", statusType);

            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" 1=1 ");

            if (beginDate > NFMT.Common.DefaultValue.DefaultTime && endDate > beginDate)
                sb.AppendFormat(" and con.ContractDate between '{0}' and '{1}' ", beginDate.ToShortDateString(), endDate.ToShortDateString());

            if (!string.IsNullOrEmpty(contractNo))
                sb.AppendFormat(" and con.OutContractNo like '%{0}%' ", contractNo);

            if (outerCorp > 0)
                sb.AppendFormat(" and outCorp.CorpId = {0} ", outerCorp);

            if (status > 0)
                sb.AppendFormat(" and con.ContractStatus = {0} ", status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel CreateContract(NFMT.Common.UserModel user, Contract.Model.Contract contract, Model.ContractDetail contractDetail, Model.ContractPrice contractPrice, List<Model.ContractCorporationDetail> outCorps, List<Model.ContractCorporationDetail> inCorps, List<Model.ContractDept> depts, bool isSubmitAudit, List<NFMT.Contract.Model.ContractClause> contractClauses)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    contract.CreateFrom = (int)NFMT.Common.CreateFromEnum.合约直接创建;
                    result = this.Create(user, contract, contractDetail, contractPrice, outCorps, inCorps, depts, contractClauses);
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

        public ResultModel UpdateContract(NFMT.Common.UserModel user, Contract.Model.Contract contract, Model.ContractDetail contractDetail, Model.ContractPrice contractPrice, List<Model.ContractCorporationDetail> outCorps, List<Model.ContractCorporationDetail> inCorps, List<Model.ContractDept> depts, List<NFMT.Contract.Model.ContractClause> contractClauses)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (contract == null)
                {
                    result.Message = "合约不存在";
                    return result;
                }

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Update(user, contract, contractDetail, contractPrice, outCorps, inCorps, depts, contractClauses);

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

        public override ResultModel GoBack(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            using (System.Transactions.TransactionScope scope = new TransactionScope())
            {
                result = base.GoBack(user, obj);
                if (result.ResultStatus != 0)
                    return result;

                //工作流任务关闭
                WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                result = sourceDAL.SynchronousStatus(user, obj);
                if (result.ResultStatus != 0)
                    return result;

                scope.Complete();
            }

            return result;
        }

        public ResultModel Complete(UserModel user, int id)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取合约
                    result = this.contractDAL.Get(user, id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Contract resultObj = result.ReturnValue as Model.Contract;
                    if (resultObj == null || resultObj.ContractId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "合约不存在";
                        return result;
                    }

                    //验证所有子合约是否全部关闭
                    DAL.ContractSubDAL subDAL = new ContractSubDAL();
                    result = subDAL.Load(user, resultObj.ContractId, StatusEnum.已录入);
                    if (result.ResultStatus != 0)
                        return result;

                    List<ContractSub> subs = result.ReturnValue as List<Model.ContractSub>;
                    if (subs == null || subs.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约获取失败";
                        return result;
                    }

                    foreach (Model.ContractSub sub in subs)
                    {
                        if (sub.SubStatus != StatusEnum.已完成 && sub.SubStatus != StatusEnum.已作废 && sub.SubStatus != StatusEnum.部分完成 && sub.SubStatus != StatusEnum.已关闭)
                        {
                            result.ResultStatus = -1;
                            result.Message = "有未完成子合约，合约不能完成";
                            return result;
                        }
                    }

                    //验证子合约的签订重量之和是否等于合约重量
                    decimal sumAmount = subs.Sum(temp => temp.SignAmount);
                    decimal missRate = Convert.ToDecimal(0.05);
                    decimal missValue = resultObj.SignAmount * missRate;
                    decimal maxAmount = resultObj.SignAmount + missValue;
                    decimal minAmount = resultObj.SignAmount - missValue;

                    if (sumAmount > maxAmount || sumAmount < minAmount)
                    {
                        result.Message = "子合约执行总量不在合约签订量范围内，不能完成。";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = this.contractDAL.Complete(user, resultObj);

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

        public ResultModel CompleteCancel(UserModel user, int id)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取合约
                    result = this.contractDAL.Get(user, id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Contract resultObj = result.ReturnValue as Model.Contract;
                    if (resultObj == null || resultObj.ContractId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "合约不存在";
                        return result;
                    }

                    result = this.contractDAL.CompleteCancel(user, resultObj);

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

        public SelectModel GetSelectModel(UserModel user, int contractId)
        {
            SelectModel select = this.GetSelectModel(1, 200, string.Empty, NFMT.Common.DefaultValue.DefaultTime, NFMT.Common.DefaultValue.DefaultTime);
            select.WhereStr = string.Format(" con.ContractId ={0} ", contractId);
            return select;
        }

        #endregion

        #region report

        public SelectModel GetContractProgressSelect(int pageIndex, int pageSize, string orderStr, DateTime startDate, DateTime endDate, string contractNo, int inCorpId, int outCorpId, int tradeBorder, int tradeDirection)
        {
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            select.OrderStr = string.IsNullOrEmpty(orderStr) ? "con.ContractId desc" : orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(" con.ContractId,con.ContractDate,con.ContractNo,con.OutContractNo,inCorp.CorpName as InCorpName,outCorp.CorpName as OutCorpName");
            sb.Append(",con.TradeDirection,traDir.DetailName as TradeDirectionName,con.AssetId,ass.AssetName,con.PriceMode,pm.DetailName as PriceModeName");
            sb.Append(",con.SettleCurrency,cur.CurrencyName,con.SignAmount,mu.MUName,inSto.InAmount,outSto.OutAmount,pri.PricingWeight,pri.AvgPrice");
            sb.Append(",proInv.ProAmount,proInv.ProBala,finInv.FinAmount,finInv.FinBala,inCash.InBala,outCash.OutBala,trdBor.DetailName as TradeBorderName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            int readeyStatus = (int)Common.StatusEnum.已生效;

            sb.Append(" dbo.Con_Contract con ");
            sb.Append(" inner join dbo.Con_ContractCorporationDetail inCorp on inCorp.ContractId = con.ContractId and inCorp.IsInnerCorp = 1 ");
            sb.AppendFormat(" and inCorp.IsDefaultCorp = 1 and inCorp.DetailStatus >={0} ", readeyStatus);
            sb.Append(" inner join dbo.Con_ContractCorporationDetail outCorp on outCorp.ContractId = con.ContractId and outCorp.IsInnerCorp = 0 ");
            sb.AppendFormat(" and outCorp.IsDefaultCorp = 1 and outCorp.DetailStatus >={0} ", readeyStatus);
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail traDir on traDir.StyleDetailId = con.TradeDirection ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail trdBor on trdBor.StyleDetailId = con.TradeBorder ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = con.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail pm on pm.StyleDetailId = con.PriceMode ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on con.SettleCurrency = cur.CurrencyId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = con.UnitId ");
            sb.AppendFormat(" left join (select ContractId,SUM(NetAmount) as InAmount from NFMT.dbo.St_StockLog where LogType ={0} group by ContractId) ", (int)LogTypeEnum.入库);
            sb.Append(" as inSto on inSto.ContractId = con.ContractId ");
            sb.AppendFormat(" left join (select ContractId,SUM(NetAmount) as OutAmount from NFMT.dbo.St_StockLog where LogType ={0} group by ContractId) ", (int)LogTypeEnum.出库);
            sb.Append(" as outSto on outSto.ContractId = con.ContractId ");
            sb.Append(" left join ( ");
            sb.Append(" select pa.ContractId,SUM(pri.PricingWeight) as PricingWeight ");
            sb.Append(" ,cast(round(SUM(pri.FinalPrice*pri.PricingWeight)/SUM(pri.PricingWeight),4) as decimal(18,4)) as AvgPrice ");
            sb.Append(" from NFMT.dbo.Pri_Pricing pri ");
            sb.Append(" inner join NFMT.dbo.Pri_PricingApply pa on pri.PricingApplyId = pa.PricingApplyId ");
            sb.AppendFormat(" where PricingStatus >=50 group by pa.ContractId) as pri on pri.ContractId = con.ContractId ", readeyStatus);
            sb.Append(" left join ( ");
            sb.Append(" select bi.ContractId,SUM(inv.InvoiceBala) as ProBala,SUM(bi.NetAmount) as ProAmount from NFMT.dbo.Inv_BusinessInvoice bi ");
            sb.AppendFormat(" inner join NFMT.dbo.Invoice inv on bi.InvoiceId = inv.InvoiceId and inv.InvoiceStatus >={0} ", readeyStatus);
            sb.AppendFormat(" where inv.InvoiceType = {0} ", (int)InvoiceTypeEnum.ProvisionalInvoice);
            sb.Append(" group by bi.ContractId ");
            sb.Append(" ) as proInv on proInv.ContractId = con.ContractId ");
            sb.Append(" left join ( ");
            sb.Append(" select bi.ContractId,SUM(inv.InvoiceBala) as FinBala,SUM(bi.NetAmount) as FinAmount from NFMT.dbo.Inv_BusinessInvoice bi ");
            sb.AppendFormat(" inner join NFMT.dbo.Invoice inv on bi.InvoiceId = inv.InvoiceId and inv.InvoiceStatus >={0} ", readeyStatus);
            sb.AppendFormat(" where inv.InvoiceType in ({0},{1},{2}) ", (int)InvoiceTypeEnum.直接终票, (int)InvoiceTypeEnum.替临终票, (int)InvoiceTypeEnum.补临终票);
            sb.Append(" group by bi.ContractId ");
            sb.Append(" ) as finInv on finInv.ContractId = con.ContractId ");
            sb.Append(" left join ( ");
            sb.AppendFormat(" select SUM(FundsBala) as InBala,ContractId from NFMT.dbo.Fun_FundsLog fl where LogType = {0} ", (int)LogTypeEnum.收款);
            sb.Append(" group by ContractId ");
            sb.Append(" ) as inCash on inCash.ContractId = con.ContractId ");
            sb.Append(" left join ( ");
            sb.AppendFormat(" select SUM(FundsBala) as OutBala,ContractId from NFMT.dbo.Fun_FundsLog fl where LogType = {0} ", (int)LogTypeEnum.付款);
            sb.Append(" group by ContractId ");
            sb.Append(" ) as outCash on outCash.ContractId = con.ContractId ");

            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" con.ContractStatus>={0} ", readeyStatus);
            if (!string.IsNullOrEmpty(contractNo))
                sb.AppendFormat(" and con.ContractNo like '%{0}%' ", contractNo);
            if (outCorpId > 0)
                sb.AppendFormat(" and outCorp.CorpId = {0} ", outCorpId);
            if (inCorpId > 0)
                sb.AppendFormat(" and inCorp.CorpId = {0} ", inCorpId);
            if (tradeBorder > 0)
                sb.AppendFormat(" and con.TradeBorder = {0} ", tradeBorder);
            if (tradeDirection > 0)
                sb.AppendFormat(" and con.TradeDirection = {0} ", tradeDirection);

            select.WhereStr = sb.ToString();

            return select;
        }

        //public override string[,] SetExcelRangeData(DataTable source)
        //{
        //    string[,] objData = new string[source.Rows.Count, 22];

        //    for (int i = 0; i < source.Rows.Count; i++)
        //    {
        //        System.Data.DataRow dr = source.Rows[i];

        //        objData[i, 0] = ((DateTime)dr["ContractDate"]).ToShortDateString();
        //        objData[i, 1] = dr["ContractNo"].ToString();
        //        objData[i, 2] = dr["OutContractNo"].ToString();
        //        objData[i, 3] = dr["InCorpName"].ToString();
        //        objData[i, 4] = dr["OutCorpName"].ToString();
        //        objData[i, 5] = dr["TradeDirectionName"].ToString();
        //        objData[i, 6] = dr["PriceModeName"].ToString();
        //        objData[i, 7] = dr["TradeBorderName"].ToString();
        //        objData[i, 8] = dr["AssetName"].ToString();
        //        objData[i, 9] = dr["MUName"].ToString();
        //        objData[i, 10] = dr["CurrencyName"].ToString();
        //        objData[i, 11] = dr["SignAmount"].ToString();
        //        objData[i, 12] = dr["InAmount"].ToString();
        //        objData[i, 13] = dr["OutAmount"].ToString();
        //        objData[i, 14] = dr["PricingWeight"].ToString();
        //        objData[i, 15] = dr["AvgPrice"].ToString();
        //        objData[i, 16] = dr["ProAmount"].ToString();
        //        objData[i, 17] = dr["ProBala"].ToString();
        //        objData[i, 18] = dr["FinAmount"].ToString();
        //        objData[i, 19] = dr["FinBala"].ToString();
        //        objData[i, 20] = dr["OutBala"].ToString();
        //        objData[i, 21] = dr["InBala"].ToString();
        //    }

        //    return objData;
        //}

        public override DataTable SetExcelRangeData(DataTable source)
        {
            string[] strs = new string[] { "ContractDate", "ContractNo", "OutContractNo", "InCorpName", "OutCorpName", "TradeDirectionName", "PriceModeName", "TradeBorderName", "AssetName", "MUName", "CurrencyName", "SignAmount", "InAmount", "OutAmount", "PricingWeight", "AvgPrice", "ProAmount", "ProBala", "FinAmount", "FinBala", "OutBala", "InBala" };

            return source.ConvertDataTable(strs);
        }

        #endregion

        #region 合约条款

        public enum OperateEnum
        {
            新增,
            修改,
            明细
        }

        /// <summary>
        /// 获取所有已生效模版条款关联数据
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private List<NFMT.Data.Model.ClauseContract> GetClauseContracts(UserModel user)
        {
            NFMT.Data.BLL.ClauseContractBLL clauseContractBLL = new NFMT.Data.BLL.ClauseContractBLL();
            NFMT.Common.ResultModel result = clauseContractBLL.Load(user, NFMT.Common.StatusEnum.已生效);
            if (result.ResultStatus != 0)
                return null;

            List<NFMT.Data.Model.ClauseContract> clauseContracts = result.ReturnValue as List<NFMT.Data.Model.ClauseContract>;
            if (clauseContracts == null || !clauseContracts.Any())
                return null;

            return clauseContracts;
        }

        /// <summary>
        /// 动态生成html代码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetTabString(UserModel user)
        {
            try
            {
                List<NFMT.Data.Model.ClauseContract> clauseContracts = GetClauseContracts(user);
                if (clauseContracts == null || !clauseContracts.Any())
                    return string.Empty;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append("<div id=\"jqxTabs\"> ");
                sb.Append("<ul> ");
                foreach (int masterId in clauseContracts.Select(a => a.MasterId).Distinct())
                {
                    NFMT.Data.Model.ContractMaster contractMaster = NFMT.Data.BasicDataProvider.ContractMasters.SingleOrDefault(a => a.MasterId == masterId);
                    if (contractMaster == null)
                        continue;

                    sb.AppendFormat("<li>{0}</li> ", contractMaster.MasterName);
                }
                sb.Append("</ul> ");

                int i = 0;
                foreach (int masterId in clauseContracts.Select(a => a.MasterId).Distinct())
                {
                    sb.Append("<div > ");
                    sb.AppendFormat("<div id=\"listbox_{0}\" class=\"ListBoxClass\"></div> ", i.ToString());
                    sb.Append(Environment.NewLine);
                    sb.Append("</div>");

                    i++;
                }

                sb.Append("</div>");

                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 动态生成控件绑定javascript代码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isCreate">是否在新增时调用</param>
        /// <returns></returns>
        public string GetListBoxsInit(UserModel user, OperateEnum operate, int contractId)
        {
            try
            {
                List<NFMT.Data.Model.ClauseContract> clauseContracts = GetClauseContracts(user);
                if (clauseContracts == null || !clauseContracts.Any())
                    return string.Empty;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.Text.StringBuilder sbCheck = new System.Text.StringBuilder();
                System.Text.StringBuilder sbMasterIdsArray = new System.Text.StringBuilder();
                NFMT.Data.BLL.ContractClauseBLL contractClauseBLL = new NFMT.Data.BLL.ContractClauseBLL();
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                sb.Append("$(\"#jqxTabs\").jqxTabs({ width: \"99%\", position: \"top\" }); ");
                sb.Append(Environment.NewLine);

                int i = 0;
                foreach (int masterId in clauseContracts.Select(a => a.MasterId).Distinct())
                {
                    if (i == 1)
                    {
                        sb.Append("$('#jqxTabs').on('selected', function (event) {");
                        sb.Append(Environment.NewLine);
                        sb.Append("var clickedItem = event.args.item;");
                        sb.Append(Environment.NewLine);
                    }
                    if (i != 0)
                    {
                        sb.AppendFormat("if(clickedItem =={0}&&is{0}Inited==false) ", i.ToString());
                        sb.Append("{");
                    }

                    sb.Append("var localdataArray = [");
                    foreach (NFMT.Data.Model.ClauseContract clauseContract in clauseContracts.Where(a => a.MasterId == masterId).OrderByDescending(a => a.IsChose).ThenBy(b => b.Sort))
                    {
                        result = contractClauseBLL.Get(user, clauseContract.ClauseId);
                        if (result.ResultStatus != 0)
                            continue;
                        NFMT.Data.Model.ContractClause contractClause = result.ReturnValue as NFMT.Data.Model.ContractClause;
                        if (contractClause == null)
                            continue;

                        sb.AppendFormat("[\"{0}\",\"{1}\"],", clauseContract.ClauseId.ToString(), contractClause.ClauseText);

                        //勾选
                        if (operate == OperateEnum.新增)
                            sbCheck.Append(CreateCheckItems(user, clauseContract, masterId, i));
                    }
                    if (!string.IsNullOrEmpty(sb.ToString()))
                        sb = sb.Remove(sb.Length - 1, 1);

                    sb.Append("];");
                    sb.Append(Environment.NewLine);

                    sb.AppendFormat("var source{0}=", masterId.ToString());
                    sb.Append("{localdata:localdataArray,async:false ,datatype:\"array\",datafields: [{ name: \"id\",type: \"int\", map: \"0\" },{ name: \"name\",type: \"string\", map: \"1\"}]};");

                    sb.Append(Environment.NewLine);
                    sb.Append("try{");

                    sb.Append(Environment.NewLine);
                    sb.AppendFormat("var listboxDataAdapter{0} = new $.jqx.dataAdapter(source{0});", masterId.ToString());
                    sb.Append(Environment.NewLine);
                    sb.AppendFormat("$(\"#listbox_{0}\").jqxListBox( ", i.ToString());
                    sb.Append("{ ");
                    sb.AppendFormat("source: listboxDataAdapter{0}, checkboxes: true,displayMember: \"name\", valueMember: \"id\",autoHeight:true,width:\"99%\",allowDrag:true,autoItemsHeight: true ", masterId.ToString());
                    sb.Append(Environment.NewLine);

                    sbMasterIdsArray.Append(Environment.NewLine);
                    sbMasterIdsArray.AppendFormat("masterIds.push({0});", masterId);
                    sbMasterIdsArray.Append(Environment.NewLine);
                    sbMasterIdsArray.AppendFormat(" var is{0}Inited = false;", i.ToString());

                    sb.Append("}); ");
                    sb.Append(Environment.NewLine);
                    sb.AppendFormat("is{0}Inited = true;", i.ToString());
                    sb.Append(Environment.NewLine);
                    sb.Append(sbCheck);
                    sb.Append(Environment.NewLine);
                    sb.Append("}catch(e){alert(e.Message);}");
                    sb.Append(Environment.NewLine);

                    if (i != 0)
                        sb.Append("}");

                    i++;

                }
                if (i > 1)
                    sb.Append("})");

                //勾选
                if (operate == OperateEnum.修改 || operate == OperateEnum.明细)
                {
                    ContractClauseDAL dal = new ContractClauseDAL();
                    result = dal.LoadByContractId(user, contractId);
                    if (result.ResultStatus == 0)
                    {
                        List<Model.ContractClause> contractClauses = result.ReturnValue as List<Model.ContractClause>;
                        if (contractClauses != null && contractClauses.Any())
                        {
                            int masterId = contractClauses.First().MasterId;

                            sbCheck.AppendFormat("$('#jqxTabs').jqxTabs('select', {0}); ", string.Format("masterIds.indexOf({0})", masterId));
                            sbCheck.Append("for(var i =0;i<masterIds.length;i++){");
                            sbCheck.AppendFormat("if(masterIds[i]=={0})", masterId);
                            sbCheck.Append("{continue;}");
                            if (operate == OperateEnum.明细)
                                sbCheck.Append("$('#jqxTabs').jqxTabs('removeAt', i);");
                            //else
                            //    sbCheck.Append("$('#jqxTabs').jqxTabs('disableAt', i);");
                            sbCheck.Append("}");

                            sbCheck.Append(UpdateCheckItems(user, contractId));
                            if (operate == OperateEnum.明细)
                            {
                                sbCheck.AppendFormat("var items = $(\"#listbox_\"+{0}).jqxListBox('getItems');", string.Format("masterIds.indexOf({0})", masterId));
                                sbCheck.Append("var i =items.length-1;");
                                sbCheck.Append("for(;i>=0;i--){if(items[i].checked==false){");
                                sbCheck.AppendFormat("$(\"#listbox_\"+{0}).jqxListBox('removeItem', items[i]);", string.Format("masterIds.indexOf({0})", masterId));
                                sbCheck.Append("}}");
                            }

                            sbCheck.Append(Environment.NewLine);

                            if (operate == OperateEnum.明细)
                                sbCheck.AppendFormat("$(\"#listbox_\"+{0}).jqxListBox(\"disabled\",true);", string.Format("masterIds.indexOf({0})", masterId));
                        }
                    }
                }
                else
                    sbCheck.Clear();

                return sbMasterIdsArray.Append(sb).Append(Environment.NewLine).Append(sbCheck).Append(Environment.NewLine).ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 动态生成保存按钮中javascript代码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetCreateString(UserModel user)
        {
            try
            {
                List<NFMT.Data.Model.ClauseContract> clauseContracts = GetClauseContracts(user);
                if (clauseContracts == null || !clauseContracts.Any())
                    return string.Empty;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.Text.StringBuilder sbItems = new System.Text.StringBuilder();

                sb.Append(" var tabSelectedIndex = $(\"#jqxTabs\").val();");
                sb.Append(" var selectedMasterId = masterIds[tabSelectedIndex];");
                sb.Append("var checkedClause = \"[\";");

                //int i = 0;
                //foreach (int masterId in clauseContracts.Select(a => a.MasterId).Distinct())
                //{
                sbItems.Append("var items = $(\"#listbox_\"+" + "$(\"#jqxTabs\").val()" + ").jqxListBox(\"getCheckedItems\");");
                sbItems.Append(Environment.NewLine);

                sb.Append(Environment.NewLine);
                sb.Append("for(var i in items){");
                sb.Append(Environment.NewLine);
                sb.Append("checkedClause+=\"{");
                sb.Append("MasterId:selectedMasterId,ClauseId:\"+");
                sb.Append("items[i].value+");
                sb.Append("\"},\";}");

                //    i++;
                //}
                sb.Append(Environment.NewLine);
                sb.Append("checkedClause+=\"]\";");

                return sbItems.Append(sb).ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 新增时勾选listbox
        /// </summary>
        /// <param name="user"></param>
        /// <param name="clauseContract"></param>
        /// <param name="masterId"></param>
        /// <returns></returns>
        private System.Text.StringBuilder CreateCheckItems(UserModel user, NFMT.Data.Model.ClauseContract clauseContract, int masterId, int i)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (clauseContract.IsChose)
            {
                sb.AppendFormat("$(\"#listbox_{0}\").jqxListBox(\"checkItem\",$(\"#listbox_{0}\").jqxListBox(\"getItemByValue\", \"{1}\"));", i.ToString(), clauseContract.ClauseId);
                sb.Append(Environment.NewLine);
            }

            return sb;
        }

        /// <summary>
        /// 修改时勾选listbox
        /// </summary>
        /// <param name="user"></param>
        /// <param name="clauseContract"></param>
        /// <param name="masterId"></param>
        /// <param name="isCreate"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        private System.Text.StringBuilder UpdateCheckItems(UserModel user, int contractId)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            try
            {
                ContractClauseDAL dal = new ContractClauseDAL();
                NFMT.Common.ResultModel result = dal.LoadByContractId(user, contractId);
                if (result.ResultStatus != 0)
                    return sb.Clear();

                List<Model.ContractClause> contractClauses = result.ReturnValue as List<Model.ContractClause>;
                if (contractClauses == null || !contractClauses.Any())
                    return sb.Clear();

                foreach (Model.ContractClause contractClause in contractClauses.OrderBy(a => a.ClauseId))
                {
                    sb.AppendFormat("var item = $(\"#listbox_\"+{0}).jqxListBox(\"getItemByValue\", \"{1}\");", string.Format("masterIds.indexOf({0})", contractClause.MasterId), contractClause.ClauseId);
                    sb.AppendFormat("$(\"#listbox_\"+{0}).jqxListBox(\"removeItem\",item);", string.Format("masterIds.indexOf({0})", contractClause.MasterId));
                    sb.AppendFormat("$(\"#listbox_\"+{0}).jqxListBox(\"insertAt\",item, 0);", string.Format("masterIds.indexOf({0})", contractClause.MasterId));
                    sb.AppendFormat("$(\"#listbox_\"+{0}).jqxListBox(\"checkIndex\",0);", string.Format("masterIds.indexOf({0})", contractClause.MasterId));
                    sb.Append(Environment.NewLine);
                }
            }
            catch
            {
                return sb.Clear();
            }

            return sb;
        }

        public class MasterIndex
        {
            public int MasterId { get; set; }
            public int SelectedIndex { get; set; }
        }

        #endregion

        #region 采购合约

        public ResultModel ContractInCreate(NFMT.Common.UserModel user, Contract.Model.Contract contract, Model.ContractDetail contractDetail, Model.ContractPrice contractPrice, List<Model.ContractCorporationDetail> outCorps, List<Model.ContractCorporationDetail> inCorps, List<Model.ContractDept> depts, List<NFMT.Contract.Model.ContractClause> contractClauses, Model.ContractSub sub,List<Model.ContractTypeDetail> contractTypes)
        {
            ResultModel result = new ResultModel();

            try
            {
                BLL.ContractSubBLL subBLL = new ContractSubBLL();

                //新增主合约
                contract.CreateFrom = (int)NFMT.Common.CreateFromEnum.采购合约库存创建;
                result = this.Create(user, contract, contractDetail, contractPrice, outCorps, inCorps, depts, contractClauses, contractTypes);

                if (result.ResultStatus != 0)
                    return result;

                Model.Contract resultContract = result.ReturnValue as Model.Contract;
                if (resultContract == null || resultContract.ContractId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "新增主合约失败";
                    return result;
                }

                //新增子合约
                sub.AssetId = resultContract.AssetId;
                sub.ContractDate = resultContract.ContractDate;
                sub.ContractId = resultContract.ContractId;
                sub.ContractLimit = resultContract.ContractLimit;
                sub.ContractSide = resultContract.ContractSide;
                sub.CreateFrom = resultContract.CreateFrom;
                sub.DeliveryDate = resultContract.DeliveryDate;
                sub.DeliveryStyle = resultContract.DeliveryStyle;
                sub.InitQP = resultContract.InitQP;
                sub.Memo = resultContract.Memo;
                sub.OutContractNo = resultContract.OutContractNo;
                sub.Premium = resultContract.Premium;
                sub.PriceMode = resultContract.PriceMode;
                sub.SettleCurrency = resultContract.SettleCurrency;
                sub.SubStatus = StatusEnum.已录入;
                sub.TradeBorder = resultContract.TradeBorder;
                sub.TradeDirection = resultContract.TradeDirection;
                sub.UnitId = resultContract.UnitId;

                //子合约明细
                Model.SubDetail detail = new SubDetail();
                detail.DelayRate = contractDetail.DelayRate;
                detail.DelayType = contractDetail.DelayType;
                detail.DiscountBase = contractDetail.DiscountBase;
                detail.DiscountRate = contractDetail.DiscountRate;
                detail.DiscountType = contractDetail.DiscountType;
                detail.MoreOrLess = contractDetail.MoreOrLess;
                detail.Status = StatusEnum.已生效;

                //子合约价格明细
                Model.SubPrice price = new SubPrice();
                price.DoPriceBeginDate = contractPrice.DoPriceBeginDate;
                price.DoPriceEndDate = contractPrice.DoPriceEndDate;
                price.FixedPrice = contractPrice.FixedPrice;
                price.FixedPriceMemo = contractPrice.FixedPriceMemo;
                price.IsQP = contractPrice.IsQP;
                price.MarginAmount = contractPrice.MarginAmount;
                price.MarginMemo = contractPrice.MarginMemo;
                price.MarginMode = contractPrice.MarginMode;
                price.PriceFrom = contractPrice.PriceFrom;
                price.PriceStyle1 = contractPrice.PriceStyle1;
                price.PriceStyle2 = contractPrice.PriceStyle2;
                price.Status = StatusEnum.已生效;
                price.WhoDoPrice = contractPrice.WhoDoPrice;
                price.AlmostPrice = contractPrice.AlmostPrice;

                //子合约抬头
                List<Model.SubCorporationDetail> outSubCorps = new List<SubCorporationDetail>();
                List<Model.SubCorporationDetail> inSubCorps = new List<SubCorporationDetail>();

                foreach (Model.ContractCorporationDetail corp in outCorps)
                {
                    Model.SubCorporationDetail subCorp = new SubCorporationDetail();
                    subCorp.ContractId = resultContract.ContractId;
                    subCorp.CorpId = corp.CorpId;
                    subCorp.CorpName = corp.CorpName;
                    subCorp.DetailStatus = StatusEnum.已生效;
                    subCorp.IsDefaultCorp = corp.IsDefaultCorp;
                    subCorp.IsInnerCorp = corp.IsInnerCorp;

                    outSubCorps.Add(subCorp);
                }

                foreach (Model.ContractCorporationDetail corp in inCorps)
                {
                    Model.SubCorporationDetail subCorp = new SubCorporationDetail();
                    subCorp.ContractId = resultContract.ContractId;
                    subCorp.CorpId = corp.CorpId;
                    subCorp.CorpName = corp.CorpName;
                    subCorp.DetailStatus = StatusEnum.已生效;
                    subCorp.IsDefaultCorp = corp.IsDefaultCorp;
                    subCorp.IsInnerCorp = corp.IsInnerCorp;

                    inSubCorps.Add(subCorp);
                }

                //子合约类型明细
                List<Model.SubTypeDetail> subTypes = new List<SubTypeDetail>();
                foreach (Model.ContractTypeDetail contractType in contractTypes)
                {
                    Model.SubTypeDetail subType = new SubTypeDetail();
                    subType.ContractId = resultContract.ContractId;
                    subType.ContractType = contractType.ContractType;
                    subType.DetailStatus = StatusEnum.已生效;

                    subTypes.Add(subType);
                }

                result = subBLL.Create(user, sub, detail, price, outSubCorps, inSubCorps, subTypes);
                if (result.ResultStatus != 0)
                    return result;

                Model.ContractSub resultSub = result.ReturnValue as Model.ContractSub;
                if (resultSub == null || resultSub.SubId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "新增子合约失败";
                    return result;
                }

                result.ReturnValue = resultContract;
                result.AffectCount = resultSub.SubId;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 采购合约作废
        /// </summary>
        /// <param name="user"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public ResultModel ContractInInvalid(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();

            try
            {

                //获取合约
                result = this.contractDAL.Get(user, contractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.Contract contract = result.ReturnValue as Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                {
                    result.Message = "合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                if (contract.CreateFrom != (int)NFMT.Common.CreateFromEnum.采购合约库存创建)
                {
                    result.Message = "合约创建来源不正确";
                    result.ResultStatus = -1;
                    return result;
                }

                //获取子合约
                DAL.ContractSubDAL subDAL = new ContractSubDAL();
                result = subDAL.GetSubByContractId(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.ContractSub sub = result.ReturnValue as Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                {
                    result.Message = "子合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                //作废子合约
                result = subDAL.Invalid(user, sub);
                if (result.ResultStatus != 0)
                    return result;

                //作废合约
                result = this.contractDAL.Invalid(user, contract);
                if (result.ResultStatus != 0)
                    return result;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel ContractInGoBack(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.ContractSubDAL subDAL = new ContractSubDAL();


                //获取合约
                result = this.contractDAL.Get(user, contractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.Contract contract = result.ReturnValue as Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                {
                    result.Message = "合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                if (contract.CreateFrom != (int)NFMT.Common.CreateFromEnum.采购合约库存创建)
                {
                    result.Message = "合约创建来源不正确";
                    result.ResultStatus = -1;
                    return result;
                }

                //获取子合约
                result = subDAL.GetSubByContractId(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.ContractSub sub = result.ReturnValue as Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                {
                    result.Message = "子合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                //撤返合约
                result = this.contractDAL.Goback(user, contract);
                if (result.ResultStatus != 0)
                    return result;

                //撤返子合约
                result = subDAL.Goback(user, sub);
                if (result.ResultStatus != 0)
                    return result;

                //删除工作流审核数据
                WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                result = sourceDAL.SynchronousStatus(user, contract);
                if (result.ResultStatus != 0)
                    return result;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel ContractInComplete(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.ContractSubDAL subDAL = new ContractSubDAL();

                //获取合约
                result = this.contractDAL.Get(user, contractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.Contract contract = result.ReturnValue as Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                {
                    result.Message = "合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                if (contract.CreateFrom != (int)NFMT.Common.CreateFromEnum.采购合约库存创建)
                {
                    result.Message = "合约创建来源不正确";
                    result.ResultStatus = -1;
                    return result;
                }

                //获取子合约
                result = subDAL.GetSubByContractId(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.ContractSub sub = result.ReturnValue as Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                {
                    result.Message = "子合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                //子合约完成
                result = subDAL.Complete(user, sub);
                if (result.ResultStatus != 0)
                    return result;

                //主合约完成
                result = this.contractDAL.Complete(user, contract);
                if (result.ResultStatus != 0)
                    return result;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel ContractInCompleteCancel(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.ContractSubDAL subDAL = new ContractSubDAL();

                //获取合约
                result = this.contractDAL.Get(user, contractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.Contract contract = result.ReturnValue as Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                {
                    result.Message = "合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                if (contract.CreateFrom != (int)NFMT.Common.CreateFromEnum.采购合约库存创建)
                {
                    result.Message = "合约创建来源不正确";
                    result.ResultStatus = -1;
                    return result;
                }

                //获取子合约
                result = subDAL.GetSubByContractId(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.ContractSub sub = result.ReturnValue as Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                {
                    result.Message = "子合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                //主合约完成撤销
                result = this.contractDAL.CompleteCancel(user, contract);
                if (result.ResultStatus != 0)
                    return result;

                //子合约完成撤销
                result = subDAL.CompleteCancel(user, sub);
                if (result.ResultStatus != 0)
                    return result;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel ContractInAudit(UserModel user, NFMT.WorkFlow.Model.DataSource dataSource, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.ContractSubDAL subDAL = new ContractSubDAL();

                result = this.contractDAL.Get(NFMT.Common.DefaultValue.SysUser, dataSource.RowId);
                if (result.ResultStatus != 0)
                    return result;

                Model.Contract contract = result.ReturnValue as Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "合约不存在";
                    return result;
                }

                //审核，修改数据状态
                result = this.contractDAL.Audit(user, contract, isPass);
                if (result.ResultStatus != 0)
                    return result;

                //获取子合约
                result = subDAL.GetSubByContractId(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.ContractSub sub = result.ReturnValue as Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "子合约不存在";
                    return result;
                }

                result = subDAL.Audit(user, sub, isPass);
                if (result.ResultStatus != 0)
                    return result;

                result.ReturnValue = sub.ContractId;
                result.AffectCount = sub.SubId;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
                return result;
            }
            return result;
        }

        public ResultModel ContractInUpdate(NFMT.Common.UserModel user, Contract.Model.Contract contract, Model.ContractDetail contractDetail, Model.ContractPrice contractPrice, List<Model.ContractCorporationDetail> outCorps, List<Model.ContractCorporationDetail> inCorps, List<Model.ContractDept> depts, List<NFMT.Contract.Model.ContractClause> contractClauses, Model.ContractSub sub, List<Model.ContractTypeDetail> contractTypes)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (contract == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "合约不存在";
                    return result;
                }

                BLL.ContractSubBLL subBLL = new ContractSubBLL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Update(user, contract, contractDetail, contractPrice, outCorps, inCorps, depts, contractClauses, contractTypes);

                    //子合约
                    sub.ContractId = contract.ContractId;
                    sub.AssetId = contract.AssetId;
                    sub.ContractDate = contract.ContractDate;
                    sub.ContractId = contract.ContractId;
                    sub.ContractLimit = contract.ContractLimit;
                    sub.ContractSide = contract.ContractSide;
                    sub.DeliveryDate = contract.DeliveryDate;
                    sub.DeliveryStyle = contract.DeliveryStyle;
                    sub.InitQP = contract.InitQP;
                    sub.Memo = contract.Memo;
                    sub.OutContractNo = contract.OutContractNo;
                    sub.Premium = contract.Premium;
                    sub.PriceMode = contract.PriceMode;
                    sub.SettleCurrency = contract.SettleCurrency;
                    sub.TradeBorder = contract.TradeBorder;
                    sub.TradeDirection = contract.TradeDirection;
                    sub.UnitId = contract.UnitId;

                    //子合约明细
                    Model.SubDetail detail = new SubDetail();
                    detail.DelayRate = contractDetail.DelayRate;
                    detail.DelayType = contractDetail.DelayType;
                    detail.DiscountBase = contractDetail.DiscountBase;
                    detail.DiscountRate = contractDetail.DiscountRate;
                    detail.DiscountType = contractDetail.DiscountType;
                    detail.MoreOrLess = contractDetail.MoreOrLess;
                    detail.Status = StatusEnum.已生效;

                    //子合约价格明细
                    Model.SubPrice price = new SubPrice();
                    price.DoPriceBeginDate = contractPrice.DoPriceBeginDate;
                    price.DoPriceEndDate = contractPrice.DoPriceEndDate;
                    price.FixedPrice = contractPrice.FixedPrice;
                    price.FixedPriceMemo = contractPrice.FixedPriceMemo;
                    price.IsQP = contractPrice.IsQP;
                    price.MarginAmount = contractPrice.MarginAmount;
                    price.MarginMemo = contractPrice.MarginMemo;
                    price.MarginMode = contractPrice.MarginMode;
                    price.PriceFrom = contractPrice.PriceFrom;
                    price.PriceStyle1 = contractPrice.PriceStyle1;
                    price.PriceStyle2 = contractPrice.PriceStyle2;
                    price.Status = StatusEnum.已生效;
                    price.WhoDoPrice = contractPrice.WhoDoPrice;
                    price.AlmostPrice = contractPrice.AlmostPrice;

                    //子合约抬头
                    List<Model.SubCorporationDetail> outSubCorps = new List<SubCorporationDetail>();
                    List<Model.SubCorporationDetail> inSubCorps = new List<SubCorporationDetail>();

                    foreach (Model.ContractCorporationDetail corp in outCorps)
                    {
                        Model.SubCorporationDetail subCorp = new SubCorporationDetail();
                        subCorp.ContractId = contract.ContractId;
                        subCorp.CorpId = corp.CorpId;
                        subCorp.CorpName = corp.CorpName;
                        subCorp.DetailStatus = StatusEnum.已生效;
                        subCorp.IsDefaultCorp = corp.IsDefaultCorp;
                        subCorp.IsInnerCorp = corp.IsInnerCorp;

                        outSubCorps.Add(subCorp);
                    }

                    foreach (Model.ContractCorporationDetail corp in inCorps)
                    {
                        Model.SubCorporationDetail subCorp = new SubCorporationDetail();
                        subCorp.ContractId = contract.ContractId;
                        subCorp.CorpId = corp.CorpId;
                        subCorp.CorpName = corp.CorpName;
                        subCorp.DetailStatus = StatusEnum.已生效;
                        subCorp.IsDefaultCorp = corp.IsDefaultCorp;
                        subCorp.IsInnerCorp = corp.IsInnerCorp;

                        inSubCorps.Add(subCorp);
                    }

                    //子合约类型明细
                    List<Model.SubTypeDetail> subTypes = new List<SubTypeDetail>();
                    foreach (Model.ContractTypeDetail contractType in contractTypes)
                    {
                        Model.SubTypeDetail subType = new SubTypeDetail();
                        subType.ContractId = contract.ContractId;
                        subType.ContractType = contractType.ContractType;
                        subType.DetailStatus = StatusEnum.已生效;

                        subTypes.Add(subType);
                    }

                    result = subBLL.Update(user, sub, detail, price, outSubCorps, inSubCorps, subTypes);

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

        #endregion

        #region 销售合约

        public ResultModel ContractOutCreate(NFMT.Common.UserModel user, Contract.Model.Contract contract, Model.ContractDetail contractDetail, Model.ContractPrice contractPrice, List<Model.ContractCorporationDetail> outCorps, List<Model.ContractCorporationDetail> inCorps, List<Model.ContractDept> depts, List<NFMT.Contract.Model.ContractClause> contractClauses, Model.ContractSub sub, List<Model.ContractTypeDetail> contractTypes)
        {
            ResultModel result = new ResultModel();

            try
            {
                BLL.ContractSubBLL subBLL = new ContractSubBLL();

                //新增主合约
                contract.CreateFrom = (int)NFMT.Common.CreateFromEnum.销售合约库存创建;
                result = this.Create(user, contract, contractDetail, contractPrice, outCorps, inCorps, depts, contractClauses, contractTypes);

                if (result.ResultStatus != 0)
                    return result;

                Model.Contract resultContract = result.ReturnValue as Model.Contract;
                if (resultContract == null || resultContract.ContractId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "新增主合约失败";
                    return result;
                }

                //新增子合约
                sub.AssetId = resultContract.AssetId;
                sub.ContractDate = resultContract.ContractDate;
                sub.ContractId = resultContract.ContractId;
                sub.ContractLimit = resultContract.ContractLimit;
                sub.ContractSide = resultContract.ContractSide;
                sub.CreateFrom = resultContract.CreateFrom;
                sub.DeliveryDate = resultContract.DeliveryDate;
                sub.DeliveryStyle = resultContract.DeliveryStyle;
                sub.InitQP = resultContract.InitQP;
                sub.Memo = resultContract.Memo;
                sub.OutContractNo = resultContract.OutContractNo;
                sub.Premium = resultContract.Premium;
                sub.PriceMode = resultContract.PriceMode;
                sub.SettleCurrency = resultContract.SettleCurrency;
                sub.SubStatus = StatusEnum.已录入;
                sub.TradeBorder = resultContract.TradeBorder;
                sub.TradeDirection = resultContract.TradeDirection;
                sub.UnitId = resultContract.UnitId;

                //子合约明细
                Model.SubDetail detail = new SubDetail();
                detail.DelayRate = contractDetail.DelayRate;
                detail.DelayType = contractDetail.DelayType;
                detail.DiscountBase = contractDetail.DiscountBase;
                detail.DiscountRate = contractDetail.DiscountRate;
                detail.DiscountType = contractDetail.DiscountType;
                detail.MoreOrLess = contractDetail.MoreOrLess;
                detail.Status = StatusEnum.已生效;

                //子合约价格明细
                Model.SubPrice price = new SubPrice();
                price.DoPriceBeginDate = contractPrice.DoPriceBeginDate;
                price.DoPriceEndDate = contractPrice.DoPriceEndDate;
                price.FixedPrice = contractPrice.FixedPrice;
                price.FixedPriceMemo = contractPrice.FixedPriceMemo;
                price.IsQP = contractPrice.IsQP;
                price.MarginAmount = contractPrice.MarginAmount;
                price.MarginMemo = contractPrice.MarginMemo;
                price.MarginMode = contractPrice.MarginMode;
                price.PriceFrom = contractPrice.PriceFrom;
                price.PriceStyle1 = contractPrice.PriceStyle1;
                price.PriceStyle2 = contractPrice.PriceStyle2;
                price.Status = StatusEnum.已生效;
                price.WhoDoPrice = contractPrice.WhoDoPrice;
                price.AlmostPrice = contractPrice.AlmostPrice;

                //子合约抬头
                List<Model.SubCorporationDetail> outSubCorps = new List<SubCorporationDetail>();
                List<Model.SubCorporationDetail> inSubCorps = new List<SubCorporationDetail>();

                foreach (Model.ContractCorporationDetail corp in outCorps)
                {
                    Model.SubCorporationDetail subCorp = new SubCorporationDetail();
                    subCorp.ContractId = resultContract.ContractId;
                    subCorp.CorpId = corp.CorpId;
                    subCorp.CorpName = corp.CorpName;
                    subCorp.DetailStatus = StatusEnum.已生效;
                    subCorp.IsDefaultCorp = corp.IsDefaultCorp;
                    subCorp.IsInnerCorp = corp.IsInnerCorp;

                    outSubCorps.Add(subCorp);
                }

                foreach (Model.ContractCorporationDetail corp in inCorps)
                {
                    Model.SubCorporationDetail subCorp = new SubCorporationDetail();
                    subCorp.ContractId = resultContract.ContractId;
                    subCorp.CorpId = corp.CorpId;
                    subCorp.CorpName = corp.CorpName;
                    subCorp.DetailStatus = StatusEnum.已生效;
                    subCorp.IsDefaultCorp = corp.IsDefaultCorp;
                    subCorp.IsInnerCorp = corp.IsInnerCorp;

                    inSubCorps.Add(subCorp);
                }

                //子合约类型明细
                List<Model.SubTypeDetail> subTypes = new List<SubTypeDetail>();
                foreach (Model.ContractTypeDetail contractType in contractTypes)
                {
                    Model.SubTypeDetail subType = new SubTypeDetail();
                    subType.ContractId = resultContract.ContractId;
                    subType.ContractType = contractType.ContractType;
                    subType.DetailStatus = StatusEnum.已生效;

                    subTypes.Add(subType);
                }

                result = subBLL.Create(user, sub, detail, price, outSubCorps, inSubCorps, subTypes);
                if (result.ResultStatus != 0)
                    return result;

                Model.ContractSub resultSub = result.ReturnValue as Model.ContractSub;
                if (resultSub == null || resultSub.SubId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "新增子合约失败";
                    return result;
                }

                result.ReturnValue = resultContract;
                result.AffectCount = resultSub.SubId;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel ContractOutGoBack(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.ContractSubDAL subDAL = new ContractSubDAL();

                //获取合约
                result = this.contractDAL.Get(user, contractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.Contract contract = result.ReturnValue as Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                {
                    result.Message = "合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                if (contract.CreateFrom != (int)NFMT.Common.CreateFromEnum.销售合约库存创建)
                {
                    result.Message = "合约创建来源不正确";
                    result.ResultStatus = -1;
                    return result;
                }

                //获取子合约
                result = subDAL.GetSubByContractId(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.ContractSub sub = result.ReturnValue as Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                {
                    result.Message = "子合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                //撤返合约
                result = this.contractDAL.Goback(user, contract);
                if (result.ResultStatus != 0)
                    return result;

                //撤返子合约
                result = subDAL.Goback(user, sub);
                if (result.ResultStatus != 0)
                    return result;

                //删除工作流审核数据
                WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                result = sourceDAL.SynchronousStatus(user, contract);
                if (result.ResultStatus != 0)
                    return result;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel ContractOutInvalid(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();

            try
            {
                //获取合约
                result = this.contractDAL.Get(user, contractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.Contract contract = result.ReturnValue as Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                {
                    result.Message = "合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                if (contract.CreateFrom != (int)NFMT.Common.CreateFromEnum.销售合约库存创建)
                {
                    result.Message = "合约创建来源不正确";
                    result.ResultStatus = -1;
                    return result;
                }

                //获取子合约
                DAL.ContractSubDAL subDAL = new ContractSubDAL();
                result = subDAL.GetSubByContractId(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.ContractSub sub = result.ReturnValue as Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                {
                    result.Message = "子合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                //作废子合约
                result = subDAL.Invalid(user, sub);
                if (result.ResultStatus != 0)
                    return result;

                //作废合约
                result = this.contractDAL.Invalid(user, contract);
                if (result.ResultStatus != 0)
                    return result;

                result.AffectCount = sub.SubId;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel ContractOutComplete(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.ContractSubDAL subDAL = new ContractSubDAL();
                //获取合约
                result = this.contractDAL.Get(user, contractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.Contract contract = result.ReturnValue as Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                {
                    result.Message = "合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                if (contract.CreateFrom != (int)NFMT.Common.CreateFromEnum.销售合约库存创建)
                {
                    result.Message = "合约创建来源不正确";
                    result.ResultStatus = -1;
                    return result;
                }

                //获取子合约
                result = subDAL.GetSubByContractId(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.ContractSub sub = result.ReturnValue as Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                {
                    result.Message = "子合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                //子合约完成
                result = subDAL.Complete(user, sub);
                if (result.ResultStatus != 0)
                    return result;

                //主合约完成
                result = this.contractDAL.Complete(user, contract);
                if (result.ResultStatus != 0)
                    return result;

                result.AffectCount = sub.SubId;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel ContractOutAudit(UserModel user, NFMT.WorkFlow.Model.DataSource dataSource, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.ContractSubDAL subDAL = new ContractSubDAL();

                result = this.contractDAL.Get(NFMT.Common.DefaultValue.SysUser, dataSource.RowId);
                if (result.ResultStatus != 0)
                    return result;

                Model.Contract contract = result.ReturnValue as Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "合约不存在";
                    return result;
                }

                //审核，修改数据状态
                result = this.contractDAL.Audit(user, contract, isPass);
                if (result.ResultStatus != 0)
                    return result;

                //获取子合约
                result = subDAL.GetSubByContractId(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.ContractSub sub = result.ReturnValue as Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "子合约不存在";
                    return result;
                }

                result = subDAL.Audit(user, sub, isPass);
                if (result.ResultStatus != 0)
                    return result;

                result.AffectCount = sub.SubId;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
                return result;
            }

            return result;
        }

        public ResultModel ContractOutCompleteCancel(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.ContractSubDAL subDAL = new ContractSubDAL();
                //获取合约
                result = this.contractDAL.Get(user, contractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.Contract contract = result.ReturnValue as Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                {
                    result.Message = "合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                if (contract.CreateFrom != (int)NFMT.Common.CreateFromEnum.销售合约库存创建)
                {
                    result.Message = "合约创建来源不正确";
                    result.ResultStatus = -1;
                    return result;
                }

                //获取子合约
                result = subDAL.GetSubByContractId(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.ContractSub sub = result.ReturnValue as Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                {
                    result.Message = "子合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                //主合约完成撤销
                result = this.contractDAL.CompleteCancel(user, contract);
                if (result.ResultStatus != 0)
                    return result;

                //子合约完成撤销
                result = subDAL.CompleteCancel(user, sub);
                if (result.ResultStatus != 0)
                    return result;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        #endregion

        #region 封装方法

        internal ResultModel ValidateAuth(NFMT.Common.UserModel user, Contract.Model.Contract contract, List<Model.ContractCorporationDetail> outCorps, List<Model.ContractCorporationDetail> inCorps)
        {
            NFMT.Common.ResultModel result = new ResultModel();

            //验证权限
            //1：验证是否有己方公司权限
            //2：验证在第一个己方公司下是否有购销权限
            //3：以上条件下是否具有内外贸权限
            //4：以上条件下是否具有长零单权限
            //5：以上条件下是否具有品种权限
            //6：以上条件下是否具有内外部交易权限

            NFMT.User.DAL.AuthGroupDAL dal = new User.DAL.AuthGroupDAL();

            result = dal.LoadByEmpId(user.EmpId);
            if (result.ResultStatus != 0)
                return result;

            List<NFMT.User.Model.AuthGroup> authGroups = result.ReturnValue as List<NFMT.User.Model.AuthGroup>;
            List<NFMT.User.Model.AuthGroup> validates = new List<NFMT.User.Model.AuthGroup>();

            if (authGroups == null)
            {
                result.ResultStatus = -1;
                result.Message = "权限验证失败";
                return result;
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("当前用户不拥有");

            IEnumerable<NFMT.User.Model.AuthGroup> temps = new List<User.Model.AuthGroup>();

            //验证是否有己方公司权限
            foreach (ContractCorporationDetail c in inCorps)
            {
                temps = authGroups.FindAll(temp => (temp.CorpId == c.CorpId || temp.CorpId == 0));

                NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == c.CorpId);
                if (corp == null || corp.CorpId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "己方公司不存在";
                    return result;
                }

                if (temps == null || temps.Count() == 0)
                {
                    result.ResultStatus = -1;
                    result.Message = string.Format("当前用户不拥有[{0}]权限", corp.CorpName);
                    return result;
                }
                else
                {
                    validates.AddRange(temps);
                    sb.AppendFormat("[{0}]", corp.CorpName);
                }
            }

            NFMT.Contract.TradeDirectionEnum tradeDirection = (TradeDirectionEnum)contract.TradeDirection;
            sb.AppendFormat(" {0} ", tradeDirection.ToString("F"));

            NFMT.Contract.TradeBorderEnum tradeBorder = (TradeBorderEnum)contract.TradeBorder;
            sb.AppendFormat(" {0} ", tradeBorder.ToString("F"));

            NFMT.Contract.ContractLimitEnum contractLimit = (ContractLimitEnum)contract.ContractLimit;
            sb.AppendFormat(" {0} ", contractLimit.ToString("G"));

            NFMT.Data.Model.Asset asset = NFMT.Data.BasicDataProvider.Assets.FirstOrDefault(temp => temp.AssetId == contract.AssetId);
            if (asset == null || asset.AssetId <= 0)
            {
                result.ResultStatus = -1;
                result.Message = "所选品种不存在";
                return result;
            }
            sb.AppendFormat(" {0} ", asset.AssetName);

            //NFMT.Contract.ContractInOutEnum contractInOut = (ContractInOutEnum)contract.ContractSide;
            //sb.AppendFormat(" {0} ", contractInOut.ToString("G"));

            sb.Append("权限");


            //验证在第一个己方公司下是否有购销权限
            temps = new List<User.Model.AuthGroup>();
            authGroups = validates;
            temps = authGroups.Where(temp => temp.TradeDirection == contract.TradeDirection || temp.TradeDirection == 0);
            if (temps == null || temps.Count() == 0)
            {
                result.ResultStatus = -1;
                result.Message = sb.ToString();
                return result;
            }
            authGroups = temps.ToList();

            //3：以上条件下是否具有内外贸权限
            temps = authGroups.Where(temp => temp.TradeBorder == contract.TradeBorder || temp.TradeBorder == 0);
            if (temps == null || temps.Count() == 0)
            {
                result.ResultStatus = -1;
                result.Message = sb.ToString();
                return result;
            }
            authGroups = temps.ToList();

            //4：以上条件下是否具有长零单权限
            temps = authGroups.Where(temp => temp.ContractLimit == contract.ContractLimit || temp.ContractLimit == 0);
            if (temps == null || temps.Count() == 0)
            {
                result.ResultStatus = -1;
                result.Message = sb.ToString();
                return result;
            }
            authGroups = temps.ToList();


            //5：以上条件下是否具有品种权限
            temps = authGroups.Where(temp => temp.AssetId == contract.AssetId || temp.AssetId == 0);
            if (temps == null || temps.Count() == 0)
            {
                result.ResultStatus = -1;
                result.Message = sb.ToString();
                return result;
            }
            authGroups = temps.ToList();

            //6：以上条件下是否具有内外部交易权限
            //temps = authGroups.Where(temp => temp.ContractInOut == contract.ContractSide || temp.ContractInOut==0);
            //if (temps == null || temps.Count() == 0)
            //{
            //    result.ResultStatus = -1;
            //    result.Message = sb.ToString();
            //    return result;
            //}
            //authGroups = temps.ToList();

            return result;
        }

        internal ResultModel Create(NFMT.Common.UserModel user, Contract.Model.Contract contract, Model.ContractDetail contractDetail, Model.ContractPrice contractPrice, List<Model.ContractCorporationDetail> outCorps, List<Model.ContractCorporationDetail> inCorps, List<Model.ContractDept> depts, List<NFMT.Contract.Model.ContractClause> contractClauses,List<Model.ContractTypeDetail> contractTypes = null)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.ContractDetailDAL detailDAL = new ContractDetailDAL();
                DAL.ContractPriceDAL priceDAL = new ContractPriceDAL();
                DAL.ContractCorporationDetailDAL corpDAL = new ContractCorporationDetailDAL();
                DAL.ContractDeptDAL deptDAL = new ContractDeptDAL();
                NFMT.User.DAL.CorporationDAL dal = new User.DAL.CorporationDAL();

                result = this.ValidateAuth(user, contract, outCorps, inCorps);
                if (result.ResultStatus != 0)
                    return result;

                contract.ContractStatus = StatusEnum.已录入;
                result = contractDAL.Insert(user, contract);
                if (result.ResultStatus != 0)
                    return result;
                //获取合约序号
                int contractId = (int)result.ReturnValue;

                contractDetail.ContractId = contractId;
                contractDetail.MoreOrLess = contractDetail.MoreOrLess / 100;
                contractDetail.DiscountRate = contractDetail.DiscountRate / 100;
                result = detailDAL.Insert(user, contractDetail);
                if (result.ResultStatus != 0)
                    return result;

                contractPrice.ContractId = contractId;
                result = priceDAL.Insert(user, contractPrice);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.User.Model.Corporation outCorpTemp = null;
                //新增合约对方抬头明细
                foreach (ContractCorporationDetail corp in outCorps)
                {
                    NFMT.User.Model.Corporation c = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == corp.CorpId);
                    if (c == null || c.CorpId <= 0)
                        return result;

                    if (outCorpTemp == null)
                        outCorpTemp = c;
                    if ((outCorpTemp.ParentId == 0 && outCorps.Count > 1) || (outCorpTemp.ParentId != c.ParentId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "选择的外部公司不属于同一集团";
                        return result;
                    }

                    corp.ContractId = contractId;
                    corp.CorpName = c.CorpName;
                    corp.IsInnerCorp = false;
                    if (outCorps.IndexOf(corp) == 0)
                        corp.IsDefaultCorp = true;
                    else
                        corp.IsDefaultCorp = false;
                    corp.DetailStatus = StatusEnum.已生效;

                    result = corpDAL.Insert(user, corp);
                    if (result.ResultStatus != 0)
                        return result;
                }

                //新增己方合约抬头明细
                foreach (ContractCorporationDetail corp in inCorps)
                {
                    result = dal.Get(user, corp.CorpId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.User.Model.Corporation c = result.ReturnValue as NFMT.User.Model.Corporation;
                    if (c == null)
                        return result;
                    corp.CorpName = c.CorpName;
                    corp.ContractId = contractId;
                    corp.IsInnerCorp = true;
                    if (inCorps.IndexOf(corp) == 0)
                        corp.IsDefaultCorp = true;
                    else
                        corp.IsDefaultCorp = false;
                    corp.DetailStatus = StatusEnum.已生效;

                    result = corpDAL.Insert(user, corp);
                    if (result.ResultStatus != 0)
                        return result;
                }

                //新增执行部门
                foreach (Model.ContractDept dept in depts)
                {
                    dept.ContractId = contractId;
                    dept.DetailStatus = Common.StatusEnum.已生效;
                    result = deptDAL.Insert(user, dept);
                    if (result.ResultStatus != 0)
                        return result;
                }

                //新增合约类型明细
                if (contractTypes != null)
                {
                    DAL.ContractTypeDetailDAL contractTypeDAL = new ContractTypeDetailDAL();
                    foreach (Model.ContractTypeDetail contractType in contractTypes)
                    {
                        contractType.ContractId = contractId;
                        contractType.DetailStatus = StatusEnum.已生效;
                        result = contractTypeDAL.Insert(user, contractType);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                }

                //新增合约条款
                if (contractClauses != null && contractClauses.Any())
                {
                    DAL.ContractClauseDAL contractClauseDAL = new ContractClauseDAL();
                    foreach (Model.ContractClause contractClause in contractClauses)
                    {
                        contractClause.ContractId = contractId;
                        result = contractClauseDAL.Insert(user, contractClause);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                }

                if (result.ResultStatus == 0)
                {
                    contract.ContractId = contractId;
                    result.ReturnValue = contract;
                }                

                //附件使用
                //创建方法最后返回业务数据Id 
                if (result.ResultStatus == 0)
                {
                    contract.ContractId = contractId;
                    result.ReturnValue = contract;
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        internal ResultModel Update(NFMT.Common.UserModel user, Contract.Model.Contract contract, Model.ContractDetail contractDetail, Model.ContractPrice contractPrice, List<Model.ContractCorporationDetail> outCorps, List<Model.ContractCorporationDetail> inCorps, List<Model.ContractDept> depts, List<NFMT.Contract.Model.ContractClause> contractClauses, List<Model.ContractTypeDetail> contractTypes = null)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.ContractDetailDAL detailDAL = new ContractDetailDAL();
                DAL.ContractPriceDAL priceDAL = new ContractPriceDAL();
                DAL.ContractCorporationDetailDAL corpDAL = new ContractCorporationDetailDAL();
                DAL.ContractDeptDAL deptDAL = new ContractDeptDAL();

                if (contract == null)
                {
                    result.Message = "合约不存在";
                    return result;
                }

                result = this.ValidateAuth(user, contract, outCorps, inCorps);
                if (result.ResultStatus != 0)
                    return result;

                result = contractDAL.Get(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.Contract.Model.Contract resultObj = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (resultObj == null)
                {
                    result.Message = "合约不存在";
                    return result;
                }

                //合约更新
                resultObj.ContractDate = contract.ContractDate;
                resultObj.OutContractNo = contract.OutContractNo;
                resultObj.Premium = contract.Premium;
                resultObj.TradeBorder = contract.TradeBorder;
                resultObj.ContractLimit = contract.ContractLimit;
                resultObj.TradeDirection = contract.TradeDirection;
                resultObj.ContractPeriodS = contract.ContractPeriodS;
                resultObj.ContractPeriodE = contract.ContractPeriodE;
                resultObj.InitQP = contract.InitQP;
                resultObj.AssetId = contract.AssetId;
                resultObj.SettleCurrency = contract.SettleCurrency;
                resultObj.SignAmount = contract.SignAmount;
                resultObj.UnitId = contract.UnitId;
                resultObj.PriceMode = contract.PriceMode;
                resultObj.Memo = contract.Memo;
                resultObj.DeliveryStyle = contract.DeliveryStyle;
                resultObj.DeliveryDate = contract.DeliveryDate;

                result = contractDAL.Update(user, resultObj);
                if (result.ResultStatus != 0)
                    return result;

                //明细更新
                result = detailDAL.GetDetailByContractId(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    return result;
                NFMT.Contract.Model.ContractDetail resultDetail = result.ReturnValue as NFMT.Contract.Model.ContractDetail;
                if (resultDetail == null)
                {
                    result.Message = "明细不存在";
                    return result;
                }

                resultDetail.DiscountBase = contractDetail.DiscountBase;
                //resultDetail.DiscountType = contractDetail.DiscountType;
                resultDetail.DiscountRate = contractDetail.DiscountRate / 100;
                resultDetail.DelayType = contractDetail.DelayType;
                resultDetail.DelayRate = contractDetail.DelayRate;
                resultDetail.MoreOrLess = contractDetail.MoreOrLess / 100;

                resultDetail.Status = resultObj.Status;

                result = detailDAL.Update(user, resultDetail);
                if (result.ResultStatus != 0)
                    return result;

                //价格更新
                result = priceDAL.GetPriceByContractId(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    return result;
                NFMT.Contract.Model.ContractPrice resultPrice = result.ReturnValue as NFMT.Contract.Model.ContractPrice;
                if (resultPrice == null)
                {
                    result.Message = "合约价格不存在";
                    return result;
                }
                resultPrice.FixedPrice = contractPrice.FixedPrice;
                resultPrice.FixedPriceMemo = contractPrice.FixedPriceMemo;
                resultPrice.WhoDoPrice = contractPrice.WhoDoPrice;
                resultPrice.DoPriceBeginDate = contractPrice.DoPriceBeginDate;
                resultPrice.DoPriceEndDate = contractPrice.DoPriceEndDate;
                resultPrice.IsQP = contractPrice.IsQP;
                resultPrice.PriceFrom = contractPrice.PriceFrom;
                resultPrice.PriceStyle1 = contractPrice.PriceStyle1;
                resultPrice.PriceStyle2 = contractPrice.PriceStyle2;
                resultPrice.MarginMode = contractPrice.MarginMode;
                resultPrice.MarginAmount = contractPrice.MarginAmount;
                resultPrice.MarginMemo = contractPrice.MarginMemo;
                resultPrice.AlmostPrice = contractPrice.AlmostPrice;

                resultPrice.Status = resultObj.Status;

                result = priceDAL.Update(user, resultPrice);
                if (result.ResultStatus != 0)
                    return result;

                //更新抬头
                //外部公司
                result = corpDAL.LoadCorpListByContractId(user, contract.ContractId, false);
                if (result.ResultStatus != 0)
                    return result;

                List<NFMT.Contract.Model.ContractCorporationDetail> ocs = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
                if (ocs == null || ocs.Count == 0)
                {
                    result.Message = "外部公司更新错误";
                    return result;
                }
                foreach (NFMT.Contract.Model.ContractCorporationDetail c in ocs)
                {
                    if (c.DetailStatus == StatusEnum.已生效)
                        c.DetailStatus = StatusEnum.已录入;

                    result = corpDAL.Invalid(user, c);
                    if (result.ResultStatus != 0)
                        return result;
                }

                NFMT.User.Model.Corporation outCorpTemp = null;

                foreach (ContractCorporationDetail corp in outCorps)
                {
                    NFMT.User.Model.Corporation c = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == corp.CorpId);
                    if (c == null || c.CorpId <= 0)
                        return result;

                    if (outCorpTemp == null)
                        outCorpTemp = c;
                    if ((outCorpTemp.ParentId == 0 && outCorps.Count > 1) || (outCorpTemp.ParentId != c.ParentId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "选择的外部公司不属于同一集团";
                        return result;
                    }

                    corp.ContractId = contract.ContractId;
                    corp.CorpName = c.CorpName;
                    corp.IsInnerCorp = false;
                    if (outCorps.IndexOf(corp) == 0)
                        corp.IsDefaultCorp = true;
                    else
                        corp.IsDefaultCorp = false;
                    corp.DetailStatus = StatusEnum.已生效;

                    result = corpDAL.Insert(user, corp);
                    if (result.ResultStatus != 0)
                        return result;
                }

                //内部公司
                result = corpDAL.LoadCorpListByContractId(user, contract.ContractId, true);
                if (result.ResultStatus != 0)
                    return result;

                ocs = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
                if (ocs == null || ocs.Count == 0)
                {
                    result.Message = "内部公司更新错误";
                    return result;
                }
                foreach (NFMT.Contract.Model.ContractCorporationDetail c in ocs)
                {
                    if (c.DetailStatus == StatusEnum.已生效)
                        c.DetailStatus = StatusEnum.已录入;

                    result = corpDAL.Invalid(user, c);
                    if (result.ResultStatus != 0)
                        return result;
                }

                foreach (ContractCorporationDetail corp in inCorps)
                {
                    NFMT.User.Model.Corporation c = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == corp.CorpId);

                    if (c == null || c.CorpId <= 0)
                        return result;
                    corp.CorpName = c.CorpName;
                    corp.ContractId = contract.ContractId;
                    corp.IsInnerCorp = true;
                    if (inCorps.IndexOf(corp) == 0)
                        corp.IsDefaultCorp = true;
                    else
                        corp.IsDefaultCorp = false;
                    corp.DetailStatus = StatusEnum.已生效;

                    result = corpDAL.Insert(user, corp);
                    if (result.ResultStatus != 0)
                        return result;
                }

                //更新执行部门
                result = deptDAL.LoadDeptByContractId(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    return result;
                List<NFMT.Contract.Model.ContractDept> ds = result.ReturnValue as List<NFMT.Contract.Model.ContractDept>;
                foreach (NFMT.Contract.Model.ContractDept d in ds)
                {
                    if (d.DetailStatus == StatusEnum.已生效)
                        d.DetailStatus = StatusEnum.已录入;

                    result = deptDAL.Invalid(user, d);
                    if (result.ResultStatus != 0)
                        return result;
                }

                foreach (Model.ContractDept dept in depts)
                {
                    dept.ContractId = contract.ContractId;
                    dept.DetailStatus = Common.StatusEnum.已生效;
                    result = deptDAL.Insert(user, dept);
                    if (result.ResultStatus != 0)
                        return result;
                }

                //更新合约类型
                DAL.ContractTypeDetailDAL contractTypeDAL = new ContractTypeDetailDAL();
                result = contractTypeDAL.LoadContractTypesById(user,resultObj.ContractId);
                if(result.ResultStatus!=0)
                    return result;
                List<Model.ContractTypeDetail> resultContractTypes = result.ReturnValue as List<Model.ContractTypeDetail>;
                if(resultContractTypes == null)
                {
                    result.Message="获取合约类型失败";
                    result.ResultStatus=-1;
                    return result;
                }

                //作废原有合约类型
                foreach(Model.ContractTypeDetail contractType in resultContractTypes)
                {
                    contractType.DetailStatus = StatusEnum.已录入;
                    result = contractTypeDAL.Invalid(user,contractType);
                    if(result.ResultStatus!=0)
                        return result;
                }

                //新增新的合约类型
                foreach (Model.ContractTypeDetail contractType in contractTypes)
                {
                    contractType.ContractId = resultObj.ContractId;
                    contractType.DetailStatus = StatusEnum.已生效;
                    result = contractTypeDAL.Insert(user, contractType);
                    if (result.ResultStatus != 0)
                        return result;
                }

                //作废所有关联合约条款
                DAL.ContractClauseDAL contractClauseDAL = new ContractClauseDAL();
                result = contractClauseDAL.InvalidAll(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                if (contractClauses != null && contractClauses.Any())
                {
                    foreach (Model.ContractClause contractClause in contractClauses)
                    {
                        contractClause.ContractId = contract.ContractId;
                        contractClause.RefStatus = StatusEnum.已生效;
                        result = contractClauseDAL.Insert(user, contractClause);
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

        #endregion

        #region 合约文本

        public ResultModel GetContractDetail(UserModel user, int contractId, TradeDirectionEnum tradeDirection)
        {
            return contractDAL.GetContractDetail(user, contractId, tradeDirection);
        }

        #endregion
    }
}
