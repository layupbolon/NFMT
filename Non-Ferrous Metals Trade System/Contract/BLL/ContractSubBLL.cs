/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractSubBLL.cs
// 文件功能描述：子合约dbo.Con_ContractSub业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月28日
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
    /// 子合约dbo.Con_ContractSub业务逻辑类。
    /// </summary>
    public class ContractSubBLL : Common.ExecBLL
    {
        private ContractSubDAL contractsubDAL = new ContractSubDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ContractSubDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractSubBLL()
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
            get { return this.contractsubDAL; }
        }
        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, string contractNo, int outCorpId, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cs.SubId asc";
            else
                select.OrderStr = orderStr;

            int bDStyleId = (int)NFMT.Data.StyleEnum.贸易方向;
            int status = (int)NFMT.Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" c.ContractId,cs.SubId,cs.ContractDate,c.ContractNo,cs.SubNo,inccd.CorpName as InCorpName,outccd.CorpName as OutCorpName,a.AssetName,convert(varchar,c.SignAmount) + mu.MUName as SignAmount,");
            sb.Append("convert(varchar,(select SUM(GrossAmount)from dbo.St_StockIn st2 left join NFMT_Basic.dbo.MeasureUnit mu2 on st2.UintId = mu2.MUId where StockInId in (select StockInId from dbo.St_ContractStockIn_Ref where ContractSubId = cs.SubId))) + mu.MUName as StockInWeight ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Con_ContractSub cs ");
            sb.Append(" left join dbo.Con_Contract c on c.ContractId = cs.ContractId ");
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail inccd on inccd.ContractId = c.ContractId and inccd.IsDefaultCorp = 1 and inccd.IsInnerCorp = 1 and inccd.DetailStatus = {0} ", status);
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail outccd on outccd.ContractId = c.ContractId and outccd.IsDefaultCorp = 1 and outccd.IsInnerCorp = 0 and outccd.DetailStatus = {0} ", status);
            sb.Append(" left join NFMT_Basic.dbo.Asset a on c.AssetId = a.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = c.UnitId ");
            //sb.Append(" left join dbo.St_ContractStockIn_Ref ref on ref.ContractSubId = cs.SubId ");
            //sb.Append(" left join dbo.St_StockIn st on st.StockInId = ref.StockInId ");
            //sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu2 on mu2.MUId = st.UintId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" c.TradeDirection = (select StyleDetailId from NFMT_Basic.dbo.BDStyleDetail where BDStyleId = {0} and DetailName = '采购') and c.ContractStatus = {1}", bDStyleId, status);

            if (!string.IsNullOrEmpty(contractNo))
                sb.AppendFormat(" and cs.SubNo like '%{0}%' ", contractNo);
            if (outCorpId > 0)
                sb.AppendFormat(" and outccd.CorpId = {0} ", outCorpId);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and cs.ContractDate between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetListSelect(int pageIndex, int pageSize, string orderStr, string contractNo, int outCorpId, int subStatus, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cs.SubId asc";
            else
                select.OrderStr = orderStr;

            int tradeDirectionStyleId = (int)NFMT.Data.StyleEnum.贸易方向;
            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;
            int statusType = (int)NFMT.Common.StatusTypeEnum.通用状态;
            int priceModeStyleId = (int)NFMT.Data.StyleEnum.PriceMode;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" cs.SubId,cs.ContractId,cs.ContractDate,con.ContractNo,cs.SubNo,con.OutContractNo,con.TradeDirection,tradeDirection.DetailName as TradeDirectionName,inCorp.CorpId as InCorpId , inCorp.CorpName as InCorpName,outCorp.CorpId as OutCorpId , outCorp.CorpName as OutCorpName,con.AssetId,ass.AssetName,cs.SignAmount,cs.UnitId,cast(cs.SignAmount as varchar(20))+mu.MUName as ContractWeight,cs.PriceMode,priceMode.DetailName as PriceModeName,cs.SubStatus,subStatus.StatusName,cs.CreateFrom");
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
            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" 1=1 ");

            if (!string.IsNullOrEmpty(contractNo))
                sb.AppendFormat(" and cs.SubNo like '%{0}%' ", contractNo);
            if (outCorpId > 0)
                sb.AppendFormat(" and outCorp.CorpId = {0} ", outCorpId);
            if (subStatus > 0)
                sb.AppendFormat(" and cs.SubStatus ={0} ", subStatus);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and cs.ContractDate between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetListSelectForCashInAllot(int pageIndex, int pageSize, string orderStr, string contractNo, int outCorpId, int subStatus, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cs.SubId asc";
            else
                select.OrderStr = orderStr;

            int tradeDirectionStyleId = (int)NFMT.Data.StyleEnum.贸易方向;
            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;
            int statusType = (int)NFMT.Common.StatusTypeEnum.通用状态;
            int priceModeStyleId = (int)NFMT.Data.StyleEnum.PriceMode;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" cs.SubId,cs.ContractId,cs.ContractDate,con.ContractNo,cs.SubNo,con.OutContractNo,con.TradeDirection,tradeDirection.DetailName as TradeDirectionName,inCorp.CorpId as InCorpId , inCorp.CorpName as InCorpName,outCorp.CorpId as OutCorpId , outCorp.CorpName as OutCorpName,con.AssetId,ass.AssetName,cs.SignAmount,cs.UnitId,cast(cs.SignAmount as varchar(20))+mu.MUName as ContractWeight,cs.PriceMode,priceMode.DetailName as PriceModeName,cs.SubStatus,subStatus.StatusName,cs.CreateFrom");
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
            select.TableName = sb.ToString();

            sb.Clear();

            //sb.AppendFormat(" cs.SubId not in (select SubContractId from dbo.Fun_CashInContract_Ref where DetailStatus >= {0}) ", readyStatus);

            if (!string.IsNullOrEmpty(contractNo))
                sb.AppendFormat(" and cs.SubNo like '%{0}%' ", contractNo);
            if (outCorpId > 0)
                sb.AppendFormat(" and outCorp.CorpId = {0} ", outCorpId);
            if (subStatus > 0)
                sb.AppendFormat(" and cs.SubStatus ={0} ", subStatus);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and cs.ContractDate between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetListSelect(int pageIndex, int pageSize, string orderStr, int subId)
        {
            NFMT.Common.SelectModel select = this.GetListSelect(pageIndex, pageSize, orderStr, string.Empty, 0, 0, Common.DefaultValue.DefaultTime, Common.DefaultValue.DefaultTime);

            select.WhereStr += string.Format(" and cs.SubId = {0} ", subId);

            return select;
        }

        public SelectModel GetReadyContractSubSelect(int pageIndex, int pageSize, string orderStr, string contractNo, int outCorpId, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cs.SubId desc";
            else
                select.OrderStr = orderStr;

            NFMT.Data.Model.BDStyleDetail detail = NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.贸易方向)["Buy"];
            int buyStyleId = detail.StyleDetailId;
            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;
            int entryStatus = (int)NFMT.Common.StatusEnum.已录入;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" con.ContractId,cs.SubId,cs.ContractDate,con.ContractNo,cs.SubNo,inCorp.CorpName as InCorpName,outCorp.CorpName as OutCorpName,ass.AssetName,convert(varchar,cs.SignAmount) + mu.MUName as SignAmount,cast(isnull(ref.SumAmount,0) as varchar) + mu.MUName as StockInWeight,cs.SettleCurrency,payRef.SumBala,CAST(isnull(payRef.SumBala,0) as varchar) + cur.CurrencyName as PayBala ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Con_ContractSub cs ");
            sb.Append(" inner join dbo.Con_Contract con on con.ContractId = cs.ContractId ");
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail inCorp on inCorp.ContractId = cs.ContractId and inCorp.IsDefaultCorp = 1 and inCorp.IsInnerCorp = 1 and inCorp.DetailStatus = {0}  ", readyStatus);
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail outCorp on outCorp.ContractId = cs.ContractId and outCorp.IsDefaultCorp = 1 and outCorp.IsInnerCorp = 0 and outCorp.DetailStatus = {0} ", readyStatus);
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on con.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = cs.UnitId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = cs.SettleCurrency ");
            sb.Append(" left join (select SUM(isnull(si.GrossAmount,0)) as SumAmount,csi.ContractSubId from dbo.St_StockIn si ");
            sb.AppendFormat(" inner join dbo.St_ContractStockIn_Ref csi on csi.StockInId = si.StockInId and csi.RefStatus >= {0} ", readyStatus);
            sb.AppendFormat(" where si.StockInStatus >= {0} group by csi.ContractSubId) as ref on ref.ContractSubId = cs.SubId ", entryStatus);

            sb.Append(" left join (select SUM(isnull(pcd.PayBala,0)) as SumBala,pcd.ContractSubId from dbo.Fun_PaymentContractDetail pcd ");
            sb.AppendFormat(" group by pcd.ContractSubId) as payRef on payRef.ContractSubId = cs.SubId ", entryStatus);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" con.TradeDirection = {0} and con.ContractStatus = {1} and cs.SubStatus = {1} ", buyStyleId, readyStatus);

            if (!string.IsNullOrEmpty(contractNo))
                sb.AppendFormat(" and cs.SubNo like '%{0}%' ", contractNo);
            if (outCorpId > 0)
                sb.AppendFormat(" and outCorp.CorpId = {0} ", outCorpId);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and cs.ContractDate between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel CreateSub(NFMT.Common.UserModel user, NFMT.Contract.Model.ContractSub sub, NFMT.Contract.Model.SubDetail detail, NFMT.Contract.Model.SubPrice price, List<Model.SubCorporationDetail> outCorps, List<Model.SubCorporationDetail> inCorps)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Create(user, sub, detail, price, outCorps, inCorps);

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

        public ResultModel UpdateSub(NFMT.Common.UserModel user, NFMT.Contract.Model.ContractSub sub, NFMT.Contract.Model.SubDetail detail, NFMT.Contract.Model.SubPrice price, List<Model.SubCorporationDetail> outCorps, List<Model.SubCorporationDetail> inCorps)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Update(user, sub, detail, price, outCorps, inCorps);
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

        public ResultModel GetContractOutCorp(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = contractsubDAL.GetContractOutCorp(user, subId);
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

        public ResultModel GetStockInWeightBySubId(UserModel user, int sudId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = contractsubDAL.GetStockInWeightBySubId(user, sudId);
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

        public ResultModel Invalid(UserModel user, int id)
        {
            Model.ContractSub sub = new ContractSub();
            sub.SubId = id;

            ResultModel result = this.Invalid(user, sub);

            return result;
        }

        public ResultModel GoBack(UserModel user, int id)
        {
            ResultModel result = new ResultModel();

            using (System.Transactions.TransactionScope scope = new TransactionScope())
            {
                result = this.contractsubDAL.Get(user, id);
                if (result.ResultStatus != 0)
                    return result;

                IModel model = (IModel)result.ReturnValue;
                result = this.contractsubDAL.Goback(user, model);
                if (result.ResultStatus != 0)
                    return result;

                //工作流任务关闭
                WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                result = sourceDAL.SynchronousStatus(user, model);
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
                    //验证子合约
                    result = this.contractsubDAL.Get(user, id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ContractSub resultObj = result.ReturnValue as Model.ContractSub;
                    if (resultObj == null || resultObj.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约不存在";
                        return result;
                    }

                    //1:验证付款申请是否全部完成
                    //2:验证收款登记是否全部完成
                    //3:验证入库登记是否全部完成
                    //4:验证出库申请是否全部完成
                    //5:验证临票是否全部完成
                    //6:验证直接终票是否全部完成
                    //7:验证补零终票是否全部完成
                    //8:验证点价是否全部完成

                    result = this.contractsubDAL.Complete(user, resultObj);
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
                    //验证子合约
                    result = this.contractsubDAL.Get(user, id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ContractSub resultObj = result.ReturnValue as Model.ContractSub;
                    if (resultObj == null || resultObj.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约不存在";
                        return result;
                    }

                    //验证合约，合约状态不为已生效则不能进行完成撤销
                    DAL.ContractDAL contractDAL = new ContractDAL();
                    result = contractDAL.Get(user, resultObj.ContractId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Contract contract = result.ReturnValue as Model.Contract;
                    if (contract == null || contract.ContractId <= 0)
                    {
                        result.Message = "子合约未有对应的合约";
                        result.ResultStatus = -1;
                        return result;
                    }

                    if (contract.ContractStatus != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主合约非已生效状态，不能进行完成撤销操作。";
                        return result;
                    }

                    result = this.contractsubDAL.CompleteCancel(user, resultObj);
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

        public ResultModel GetSubByContractId(UserModel user, int contractId)
        {
            return this.contractsubDAL.GetSubByContractId(user, contractId);
        }

        #endregion

        #region 封装方法

        internal ResultModel Create(NFMT.Common.UserModel user, NFMT.Contract.Model.ContractSub sub, NFMT.Contract.Model.SubDetail detail, NFMT.Contract.Model.SubPrice price, List<Model.SubCorporationDetail> outCorps, List<Model.SubCorporationDetail> inCorps, List<Model.SubTypeDetail> subTypes = null)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.ContractDAL contractDAL = new ContractDAL();
                DAL.SubDetailDAL detailDAL = new SubDetailDAL();
                DAL.SubPriceDAL priceDAL = new SubPriceDAL();

                DAL.ContractCorporationDetailDAL conCropDAL = new ContractCorporationDetailDAL();
                DAL.SubCorporationDetailDAL subCorpDAL = new SubCorporationDetailDAL();

                result = contractDAL.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "主合约不存在，不能新增子合约";
                    return result;
                }

                //验证子合约签订数量是否在主合约范围内
                if (sub.SignAmount > contract.SignAmount)
                {
                    result.ResultStatus = -1;
                    result.Message = "子合约签订数量不能大于主合约签订数量";
                    return result;
                }

                result = contractsubDAL.Load(user, contract.ContractId, NFMT.Common.StatusEnum.已录入);
                if (result.ResultStatus != 0)
                    return result;

                List<Model.ContractSub> subs = result.ReturnValue as List<Model.ContractSub>;
                if (subs == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取合约失败";
                    return result;
                }

                decimal sumSignAmount = subs.Sum(temp => temp.SignAmount);
                if (sub.SignAmount > contract.SignAmount - sumSignAmount)
                {
                    result.ResultStatus = -1;
                    result.Message = "子合约签订数量不能超过主合约下可签数量";
                    return result;
                }

                sub.SubStatus = StatusEnum.已录入;
                result = contractsubDAL.Insert(user, sub);
                if (result.ResultStatus != 0)
                    return result;

                //获取合约序号
                int subId = (int)result.ReturnValue;
                if (subId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "子合约新增失败";
                    return result;
                }

                detail.SubId = subId;
                detail.MoreOrLess = detail.MoreOrLess / 100;
                detail.DiscountRate = detail.DiscountRate / 100;

                result = detailDAL.Insert(user, detail);
                if (result.ResultStatus != 0)
                    return result;

                price.SubId = subId;
                result = priceDAL.Insert(user, price);
                if (result.ResultStatus != 0)
                    return result;

                if (result.ResultStatus == 0)
                {
                    sub.SubId = subId;
                    result.ReturnValue = sub;
                }

                #region 子合约抬头
                //校验抬头是否存在主合约中
                //新增抬头到子合约抬头明细表

                //获取主合约抬头
                result = conCropDAL.LoadCorpListByContractId(user, contract.ContractId, false);
                if (result.ResultStatus != 0)
                    return result;

                List<Model.ContractCorporationDetail> conOutCorps = result.ReturnValue as List<Model.ContractCorporationDetail>;
                if (conOutCorps == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "主合约对方抬头获取失败";
                    return result;
                }

                result = conCropDAL.LoadCorpListByContractId(user, contract.ContractId, true);
                if (result.ResultStatus != 0)
                    return result;

                List<Model.ContractCorporationDetail> conInCorps = result.ReturnValue as List<Model.ContractCorporationDetail>;
                if (conInCorps == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "主合约我方抬头获取失败";
                    return result;
                }

                foreach (Model.SubCorporationDetail outCorp in outCorps)
                {
                    //验证抬头是否在主合约中
                    if (!conOutCorps.Exists(temp => temp.CorpId == outCorp.CorpId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "主合约对方抬头不存在选中公司，新增失败";
                        return result;
                    }

                    NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == outCorp.CorpId);
                    if (corp == null || corp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约对方抬头不存在";
                        return result;
                    }

                    outCorp.ContractId = contract.ContractId;
                    outCorp.CorpName = corp.CorpName;
                    outCorp.SubId = subId;
                    outCorp.IsInnerCorp = false;
                    if (outCorps.IndexOf(outCorp) == 0)
                        outCorp.IsDefaultCorp = true;
                    else
                        outCorp.IsDefaultCorp = false;
                    outCorp.DetailStatus = StatusEnum.已生效;

                    result = subCorpDAL.Insert(user, outCorp);
                    if (result.ResultStatus != 0)
                        return result;
                }

                foreach (Model.SubCorporationDetail inCorp in inCorps)
                {
                    //验证抬头是否在主合约中
                    if (!conInCorps.Exists(temp => temp.CorpId == inCorp.CorpId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "主合约我方抬头不存在选中公司，新增失败";
                        return result;
                    }

                    NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == inCorp.CorpId);
                    if (corp == null || corp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约我方抬头不存在";
                        return result;
                    }

                    inCorp.ContractId = contract.ContractId;
                    inCorp.SubId = subId;
                    inCorp.CorpName = corp.CorpName;
                    inCorp.IsInnerCorp = true;
                    if (inCorps.IndexOf(inCorp) == 0)
                        inCorp.IsDefaultCorp = true;
                    else
                        inCorp.IsDefaultCorp = false;
                    inCorp.DetailStatus = StatusEnum.已生效;

                    result = subCorpDAL.Insert(user, inCorp);
                    if (result.ResultStatus != 0)
                        return result;
                }

                #endregion

                //子合约类型明细
                DAL.SubTypeDetailDAL subTypeDAL = new SubTypeDetailDAL();
                if (subTypes != null)
                {
                    foreach (Model.SubTypeDetail subType in subTypes)
                    {
                        subType.ContractId = sub.ContractId;
                        subType.DetailStatus = StatusEnum.已生效;
                        subType.SubId = subId;
                        result = subTypeDAL.Insert(user, subType);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                }

                if (result.ResultStatus == 0)
                {
                    sub.SubId = subId;
                    result.ReturnValue = sub;
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        internal ResultModel Update(NFMT.Common.UserModel user, NFMT.Contract.Model.ContractSub sub, NFMT.Contract.Model.SubDetail detail, NFMT.Contract.Model.SubPrice price, List<Model.SubCorporationDetail> outCorps, List<Model.SubCorporationDetail> inCorps,List<Model.SubTypeDetail> subTypes = null)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.ContractDAL contractDAL = new ContractDAL();
                DAL.SubDetailDAL detailDAL = new SubDetailDAL();
                DAL.SubPriceDAL priceDAL = new SubPriceDAL();
                DAL.ContractCorporationDetailDAL conCropDAL = new ContractCorporationDetailDAL();
                DAL.SubCorporationDetailDAL subCorpDAL = new SubCorporationDetailDAL();

                //加载子合约
                result = contractsubDAL.Get(user, sub.SubId);
                if (result.ResultStatus != 0)
                    return result;
                NFMT.Contract.Model.ContractSub resultSub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (resultSub == null || resultSub.SubId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "更新子合约失败";
                    return result;
                }

                //验证主合约是否存在
                result = contractDAL.Get(user, resultSub.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "主合约不存在";
                    return result;
                }

                resultSub.AssetId = sub.AssetId;
                resultSub.ContractDate = sub.ContractDate;
                resultSub.ContractLimit = sub.ContractLimit;
                resultSub.ContractSide = sub.ContractSide;
                resultSub.DeliveryDate = sub.DeliveryDate;
                resultSub.DeliveryStyle = sub.DeliveryStyle;
                resultSub.InitQP = sub.InitQP;
                resultSub.Memo = sub.Memo;
                resultSub.OutContractNo = sub.OutContractNo;
                resultSub.Premium = sub.Premium;
                resultSub.PriceMode = sub.PriceMode;
                resultSub.SettleCurrency = sub.SettleCurrency;
                resultSub.TradeBorder = sub.TradeBorder;
                resultSub.TradeDirection = sub.TradeDirection;
                resultSub.UnitId = sub.UnitId;

                resultSub.ShipTime = sub.ShipTime;
                resultSub.ArriveTime = sub.ArriveTime;
                resultSub.ContractPeriodS = sub.ContractPeriodS;
                resultSub.ContractPeriodE = sub.ContractPeriodE;                
                resultSub.SignAmount = sub.SignAmount;

                //更新子合约主表
                result = contractsubDAL.Update(user, resultSub);
                if (result.ResultStatus != 0)
                    return result;

                //加载子合约明细表
                result = detailDAL.GetDetailBySubId(user, sub.SubId);
                if (result.ResultStatus != 0)
                    return result;
                NFMT.Contract.Model.SubDetail resultDetail = result.ReturnValue as NFMT.Contract.Model.SubDetail;
                if (resultDetail == null || resultDetail.SubDetailId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "子合约明细数据不存在";
                    return result;
                }

                resultDetail.DiscountBase = detail.DiscountBase;
                resultDetail.DiscountType = detail.DiscountType;
                resultDetail.DiscountRate = detail.DiscountRate;
                resultDetail.DelayType = detail.DelayType;
                resultDetail.DelayRate = detail.DelayRate;
                resultDetail.MoreOrLess = detail.MoreOrLess;

                resultDetail.Status = resultSub.Status;

                //更新子合约明细表
                result = detailDAL.Update(user, resultDetail);
                if (result.ResultStatus != 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "子合约明细更新失败";
                    return result;
                }

                //加载子合约价格表
                result = priceDAL.GetPriceBySubId(user, sub.SubId);
                if (result.ResultStatus != 0)
                    return result;
                NFMT.Contract.Model.SubPrice resultPrice = result.ReturnValue as NFMT.Contract.Model.SubPrice;
                if (resultPrice == null || resultPrice.SubPriceId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "子合约价格数据不存在";
                    return result;
                }

                resultPrice.FixedPrice = price.FixedPrice;
                resultPrice.FixedPriceMemo = price.FixedPriceMemo;
                resultPrice.WhoDoPrice = price.WhoDoPrice;
                resultPrice.DoPriceBeginDate = price.DoPriceBeginDate;
                resultPrice.DoPriceEndDate = price.DoPriceEndDate;
                resultPrice.IsQP = price.IsQP;
                resultPrice.PriceFrom = price.PriceFrom;
                resultPrice.PriceStyle1 = price.PriceStyle1;
                resultPrice.PriceStyle2 = price.PriceStyle2;
                resultPrice.MarginMode = price.MarginMode;
                resultPrice.MarginAmount = price.MarginAmount;
                resultPrice.MarginMemo = price.MarginMemo;
                resultPrice.AlmostPrice = price.AlmostPrice;

                resultPrice.Status = resultSub.Status;

                result = priceDAL.Update(user, resultPrice);
                if (result.ResultStatus != 0)
                    return result;

                #region 子合约抬头
                //校验抬头是否存在主合约中
                //新增抬头到子合约抬头明细表

                //作废现有子合约抬头
                result = subCorpDAL.Load(user, resultSub.SubId, false);
                if (result.ResultStatus != 0)
                    return result;

                List<Model.SubCorporationDetail> resultOutCorps = result.ReturnValue as List<Model.SubCorporationDetail>;
                if (resultOutCorps == null || resultOutCorps.Count == 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "子合约对方抬头加载失败";
                    return result;
                }

                foreach (Model.SubCorporationDetail corp in resultOutCorps)
                {
                    result = subCorpDAL.Invalid(user, corp);
                    if (result.ResultStatus != 0)
                        return result;
                }

                result = subCorpDAL.Load(user, resultSub.SubId, true);
                if (result.ResultStatus != 0)
                    return result;

                List<Model.SubCorporationDetail> resultInCorps = result.ReturnValue as List<Model.SubCorporationDetail>;
                if (resultInCorps == null || resultInCorps.Count == 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "子合约我方抬头加载失败";
                    return result;
                }

                foreach (Model.SubCorporationDetail corp in resultInCorps)
                {
                    result = subCorpDAL.Invalid(user, corp);
                    if (result.ResultStatus != 0)
                        return result;
                }

                //获取主合约抬头
                result = conCropDAL.LoadCorpListByContractId(user, contract.ContractId, false);
                if (result.ResultStatus != 0)
                    return result;

                List<Model.ContractCorporationDetail> conOutCorps = result.ReturnValue as List<Model.ContractCorporationDetail>;
                if (conOutCorps == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "主合约对方抬头获取失败";
                    return result;
                }

                result = conCropDAL.LoadCorpListByContractId(user, contract.ContractId, true);
                if (result.ResultStatus != 0)
                    return result;

                List<Model.ContractCorporationDetail> conInCorps = result.ReturnValue as List<Model.ContractCorporationDetail>;
                if (conInCorps == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "主合约我方抬头获取失败";
                    return result;
                }

                foreach (Model.SubCorporationDetail outCorp in outCorps)
                {
                    //验证抬头是否在主合约中
                    if (!conOutCorps.Exists(temp => temp.CorpId == outCorp.CorpId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "主合约对方抬头不存在选中公司，新增失败";
                        return result;
                    }

                    NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == outCorp.CorpId);
                    if (corp == null || corp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约对方抬头不存在";
                        return result;
                    }

                    outCorp.ContractId = contract.ContractId;
                    outCorp.CorpName = corp.CorpName;
                    outCorp.SubId = resultSub.SubId;
                    outCorp.IsInnerCorp = false;
                    if (outCorps.IndexOf(outCorp) == 0)
                        outCorp.IsDefaultCorp = true;
                    else
                        outCorp.IsDefaultCorp = false;
                    outCorp.DetailStatus = StatusEnum.已生效;

                    result = subCorpDAL.Insert(user, outCorp);
                    if (result.ResultStatus != 0)
                        return result;
                }

                foreach (Model.SubCorporationDetail inCorp in inCorps)
                {
                    //验证抬头是否在主合约中
                    if (!conInCorps.Exists(temp => temp.CorpId == inCorp.CorpId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "主合约我方抬头不存在选中公司，新增失败";
                        return result;
                    }

                    NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == inCorp.CorpId);
                    if (corp == null || corp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约我方抬头不存在";
                        return result;
                    }

                    inCorp.ContractId = contract.ContractId;
                    inCorp.SubId = resultSub.SubId;
                    inCorp.CorpName = corp.CorpName;
                    inCorp.IsInnerCorp = true;
                    if (inCorps.IndexOf(inCorp) == 0)
                        inCorp.IsDefaultCorp = true;
                    else
                        inCorp.IsDefaultCorp = false;
                    inCorp.DetailStatus = StatusEnum.已生效;

                    result = subCorpDAL.Insert(user, inCorp);
                    if (result.ResultStatus != 0)
                        return result;
                }

                #endregion

                if (subTypes != null) 
                {
                    DAL.SubTypeDetailDAL subTypeDAL = new SubTypeDetailDAL();
                    result = subTypeDAL.LoadSubTypesById(user, resultSub.SubId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.SubTypeDetail> resultSubTypes = result.ReturnValue as List<Model.SubTypeDetail>;
                    if (resultSubTypes == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取子合约类型失败";
                        return result;
                    }

                    //作废原有子合约类型
                    foreach (Model.SubTypeDetail subType in resultSubTypes)
                    {
                        subType.DetailStatus = StatusEnum.已录入;
                        result = subTypeDAL.Invalid(user, subType);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //新增新子合约类型
                    foreach (Model.SubTypeDetail subType in subTypes)
                    {
                        subType.ContractId = resultSub.ContractId;
                        subType.DetailStatus = StatusEnum.已生效;
                        subType.SubId = resultSub.SubId;

                        result = subTypeDAL.Insert(user, subType);
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
    }
}
