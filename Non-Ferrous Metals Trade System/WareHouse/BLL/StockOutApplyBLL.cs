/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockOutApplyBLL.cs
// 文件功能描述：出库申请dbo.St_StockOutApply业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月31日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using NFMT.WareHouse.Model;
using NFMT.WareHouse.DAL;
using NFMT.WareHouse.IDAL;
using NFMT.Common;
using Excel = Microsoft.Office.Interop.Excel;

namespace NFMT.WareHouse.BLL
{
    /// <summary>
    /// 出库申请dbo.St_StockOutApply业务逻辑类。
    /// </summary>
    public class StockOutApplyBLL : Common.ApplyBLL
    {
        private StockOutApplyDAL stockoutapplyDAL = new StockOutApplyDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StockOutApplyDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockOutApplyBLL()
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
            get { return this.stockoutapplyDAL; }
        }

        /// <summary>
        /// 更新st_stockoutapply
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="st_stockoutapply">StockOutApply对象</param>
        /// <returns></returns>
        public ResultModel Update(UserModel user, StockOutApply st_stockoutapply)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    ResultModel st_stockoutapplyResult = this.Get(user, st_stockoutapply.StockOutApplyId);
                    StockOutApply resultObj = st_stockoutapplyResult.ReturnValue as StockOutApply;

                    if (st_stockoutapply == null)
                    {
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }
                    result = stockoutapplyDAL.Update(user, st_stockoutapply);

                    if (result.ResultStatus == 0)
                        scope.Complete();
                }
            }
            catch (Exception ex)
            {
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

        #region 新增方法

        /// <summary>
        /// 获取分页查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="SubContractNo">子合约号</param>
        /// <param name="applyTimeBegin">申请日期开始</param>
        /// <param name="applyTimeEnd">申请日期结束</param>
        /// <param name="status">出库申请状态</param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, string subNo, DateTime applyTimeBegin, DateTime applyTimeEnd, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "soa.StockOutApplyId asc";
            else
                select.OrderStr = orderStr;

            int statusType = (int)Common.StatusTypeEnum.通用状态;
            int readyStatus = (int)Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("soa.StockOutApplyId,a.ApplyDept as ApplyDeptId,dept.DeptName,a.ApplyId,a.ApplyTime,a.ApplyStatus,sd.StatusName,a.ApplyDesc,a.ApplyNo,soa.SubContractId,cs.SubNo,ccdin.CorpName as InnerCorpName,ccdout.CorpName as OutCorpName,a.EmpId,e.Name");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockOutApply soa ");
            sb.Append(" inner join dbo.Apply a on soa.ApplyId = a.ApplyId");
            //sb.Append(" left join dbo.St_StockOutApplyDetail soad on soa.StockOutApplyId = soad.StockOutApplyId");
            sb.Append(" left join dbo.Con_ContractSub cs on soa.SubContractId = cs.SubId");
            sb.Append(" left join dbo.Con_Contract con on con.ContractId = cs.ContractId");
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail ccdin on con.ContractId = ccdin.ContractId and ccdin.IsInnerCorp= 1 and ccdin.IsDefaultCorp=1 and ccdin.DetailStatus={0} ", readyStatus);
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail ccdout on con.ContractId = ccdout.ContractId and ccdout.IsInnerCorp= 0 and ccdout.IsDefaultCorp=1 and ccdout.DetailStatus={0} ", readyStatus);
            sb.Append(" left join NFMT_User.dbo.Employee e on e.EmpId = a.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = a.ApplyStatus and sd.StatusId={0} ", statusType);
            sb.Append(" left join NFMT_User.dbo.Department dept on dept.DeptId = a.ApplyDept ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" 1= 1 ");
            if (status > 0)
                sb.AppendFormat(" and a.ApplyStatus = {0} ", status);
            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and cs.SubNo like '%{0}%'", subNo);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and cs.CreateTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays
                    (1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetContractListSelect(int pageIndex, int pageSize, string orderStr, string subNo, int outCorpId, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "soa.StockOutApplyId asc";
            else
                select.OrderStr = orderStr;

            int status = (int)NFMT.Common.StatusEnum.已生效;
            int statusType = (int)NFMT.Common.StatusTypeEnum.通用状态;

            int tradeDirection = (int)NFMT.Data.DetailProvider.Details(Data.StyleEnum.TradeDirection)["Sell"].StyleDetailId;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" con.ContractId,cs.SubId,cs.ContractDate,con.ContractNo,cs.SubNo,cast (cs.SignAmount as varchar) + mu.MUName as SignWeight,cast(isnull(crr.SumBala,0) as varchar) + cur.CurrencyName as AllotBala,cast(isnull(soad.SumWeight,0) as varchar)+mu.MUName as AllotWeigth, cast((cs.SignAmount -isnull(soad.SumWeight,0)) as varchar) + mu.MUName as LaveWeight,inccd.CorpName as InCorpName,outccd.CorpName as OutCorpName,a.AssetName,cs.SubStatus,sd.StatusName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Con_ContractSub cs ");
            sb.Append(" inner join dbo.Con_Contract con on con.ContractId = cs.ContractId ");
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail inccd on inccd.ContractId = con.ContractId and inccd.IsDefaultCorp = 1 and inccd.IsInnerCorp = 1 and inccd.DetailStatus= {0} ", status);
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail outccd on outccd.ContractId = con.ContractId and outccd.IsDefaultCorp = 1 and outccd.IsInnerCorp = 0 and outccd.DetailStatus={0} ", status);
            sb.Append(" left join NFMT_Basic.dbo.Asset a on con.AssetId = a.AssetId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on cs.SubStatus = sd.DetailId and sd.StatusId = {0} ", statusType);
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = cs.SettleCurrency ");

            sb.AppendFormat(" left join (select SubContractId,SUM(IsNull(NetAmount,0)) as SumWeight from dbo.St_StockOutApplyDetail where DetailStatus >={0} group by SubContractId) as soad on soad.SubContractId = cs.SubId ", status);

            sb.AppendFormat("left join(select SubContractId,SUM(isnull(AllotBala,0)) as SumBala from dbo.Fun_CashInContract_Ref where DetailStatus >= {0} group by SubContractId) as crr on crr.SubContractId = cs.SubId", status);

            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on cs.UnitId = mu.MUId ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" con.ContractStatus = {0} and cs.SubStatus = {0} and con.TradeDirection ={1} ", status, tradeDirection);

            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and cs.SubNo like '%{0}%'", subNo);
            if (outCorpId > 0)
                sb.AppendFormat("  and outccd.CorpId ={0}", outCorpId);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and cs.CreateTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays
                    (1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetAllotStockListSelect(int pageIndex, int pageSize, string orderStr, int subId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "soa.StockOutApplyId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("s.StockId,soa.StockOutApplyId,app.ApplyId,s.StockDate,sn.RefNo,s.AssetId,ass.AssetName,s.UintId,s.StockStatus,sd.StatusName");
            sb.Append(",s.BrandId,bra.BrandName,s.CorpId,cor.CorpName,mu.MUName,soad.NetAmount,s.CurNetAmount,dp.DPName,s.CardNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            int entryStatus = (int)Common.StatusEnum.已录入;
            int readyStatus = (int)Common.StatusEnum.已生效;
            int stockStatus = (int)Common.StatusTypeEnum.库存状态;
            sb.Append(" dbo.St_StockOutApplyDetail soad ");
            sb.Append(" inner join dbo.St_StockOutApply soa on soa.StockOutApplyId = soad.StockOutApplyId ");
            sb.AppendFormat(" inner join dbo.Apply app on app.ApplyId = soa.ApplyId and app.ApplyStatus >= {0} ", entryStatus);
            sb.Append(" left join dbo.St_Stock s on soad.StockId = s.StockId ");
            sb.Append(" left join dbo.St_StockName sn  on sn.StockNameId = s.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on s.AssetId = ass.AssetId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = s.StockStatus and sd.StatusId={0} ", stockStatus);
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = s.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = s.DeliverPlaceId ");
            sb.Append(" left join NFMT_User.dbo.Corporation cor on s.CorpId = cor.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = s.UintId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" soad.DetailStatus >= {0} and soad.SubContractId ={1} ", readyStatus, subId);
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetAllotStockListByApplyIdSelect(int pageIndex, int pageSize, string orderStr, int stockOutApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "soa.StockOutApplyId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("soad.DetailId,s.StockId,soa.StockOutApplyId,app.ApplyId,s.StockDate,sn.RefNo,s.AssetId,ass.AssetName,s.GrossAmount,CAST(s.GrossAmount as varchar)+ mu.MUName as StockWeight,s.StockStatus,sd.StatusName,s.BrandId,bra.BrandName,s.CorpId,cor.CorpName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            int status = (int)Common.StatusEnum.已生效;
            int enteryStatus = (int)Common.StatusEnum.已录入;
            int stockStatusType = (int)Common.StatusTypeEnum.库存状态;
            sb.Append(" dbo.St_StockOutApplyDetail soad ");
            sb.AppendFormat(" inner join dbo.St_StockOutApply soa on soa.StockOutApplyId = soa.StockOutApplyId  and soa.StockOutApplyId ={0} ", stockOutApplyId);
            sb.AppendFormat(" inner join dbo.Apply app on app.ApplyId = soa.ApplyId and app.ApplyStatus = {0} ", enteryStatus);
            sb.Append(" left join dbo.St_Stock s on soad.StockId = s.StockId ");
            sb.Append(" left join dbo.St_StockName sn  on sn.StockNameId = s.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on s.AssetId = ass.AssetId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = s.StockStatus and sd.StatusId={0} ", stockStatusType);
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = s.BrandId ");
            sb.Append(" left join NFMT_User.dbo.Corporation cor on s.CorpId = cor.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = s.UintId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" soad.DetailStatus = {0}  ", status);
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetOutApplySelectedSelect(int pageIndex, int pageSize, string orderStr, string sids = "", int stockOutApplyId = 0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "soad.DetailId,sto.StockId,sn.RefNo,sto.CurNetAmount,sto.UintId,sto.StockStatus,sd.StatusName,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,mu.MUName,soad.NetAmount,soad.Bundles,dp.DPName,sto.CardNo ";

            int statusId = (int)Common.StatusTypeEnum.库存状态;

            int planStockInStatus = (int)StockStatusEnum.预入库存;
            int planCustomsStatus = (int)StockStatusEnum.预报关库存;

            int readyStatus = (int)Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_StockOutApplyDetail soad ");
            sb.Append(" inner join dbo.St_Stock sto on sto.StockId = soad.StockId ");
            sb.Append(" left join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = sto.StockStatus and sd.StatusId ={0} ", statusId);
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" sto.StockStatus between {0} and {1} ", planStockInStatus, planCustomsStatus);
            sb.AppendFormat(" and soad.DetailStatus >={0} and soad.StockOutApplyId={1} ", readyStatus, stockOutApplyId);

            if (!string.IsNullOrEmpty(sids))
            {
                sids = sids.Trim();
                sb.AppendFormat(" or sto.StockId in ({0})", sids);
            }

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetOutApplyDetailSelect(int pageIndex, int pageSize, string orderStr, string sids = "", int stockOutApplyId = 0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "sto.StockId,sn.RefNo,sto.GrossAmount,sto.UintId,CAST(sto.GrossAmount as varchar) + mu.MUName as StockWeight,sto.StockStatus,sd.StatusName,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,soad.DetailStatus,ds.StatusName as DetailStatusName ";

            int stockStatusType = (int)Common.StatusTypeEnum.库存状态;
            int commonStatusType = (int)Common.StatusTypeEnum.通用状态;
            int closeStatus = (int)Common.StatusEnum.已关闭;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_StockOutApplyDetail soad ");
            sb.Append(" inner join dbo.St_Stock sto  on soad.StockId = sto.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" inner join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = sto.StockStatus and sd.StatusId ={0} ", stockStatusType);
            sb.Append(" inner join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" inner join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" inner join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" inner join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            sb.AppendFormat(" inner join NFMT_Basic.dbo.BDStatusDetail ds on ds.DetailId = soad.DetailStatus and ds.StatusId ={0} ", commonStatusType);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" soad.StockOutApplyId = {0} ", stockOutApplyId);
            sb.AppendFormat(" and soad.DetailStatus>={0} ", closeStatus);
            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel CreateStockOutApply(UserModel user, int subId, List<Model.StockOutApplyDetail> details, int deptId, string memo, int corpId, int buyCorpId)
        {
            ResultModel result = new ResultModel();

            NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
            NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
            NFMT.WareHouse.DAL.StockOutApplyDAL outApplyDAL = new DAL.StockOutApplyDAL();
            NFMT.WareHouse.DAL.StockOutApplyDetailDAL detailDAL = new DAL.StockOutApplyDetailDAL();
            NFMT.WareHouse.DAL.StockExclusiveDAL exclusiveDAL = new DAL.StockExclusiveDAL();
            NFMT.WareHouse.DAL.StockDAL stockDAL = new StockDAL();
            NFMT.Contract.DAL.ContractDAL contractDAL = new Contract.DAL.ContractDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证分配库存
                    if (details == null || details.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "未分配任务库存";
                        return result;
                    }

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

                    //验证合约
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

                    //获取部门信息
                    NFMT.User.Model.Department dept = User.UserProvider.Departments.Single(temp => temp.DeptId == deptId);
                    if (dept == null || dept.DeptId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "部门不存在";
                        return result;
                    }

                    //添加主申请表
                    NFMT.Operate.Model.Apply apply = new Operate.Model.Apply();
                    apply.ApplyDept = deptId;
                    apply.ApplyCorp = corpId;
                    apply.ApplyTime = DateTime.Now;
                    apply.ApplyDesc = memo;
                    apply.ApplyType = NFMT.Operate.ApplyType.出库申请;
                    apply.EmpId = user.EmpId;
                    apply.ApplyStatus = Common.StatusEnum.已录入;

                    result = applyDAL.Insert(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    int applyId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out applyId) || applyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请主表添加失败";
                        return result;
                    }

                    decimal sumGrossAmount = details.Sum(temp => temp.GrossAmount);
                    decimal sumNetAmount = details.Sum(temp => temp.NetAmount);
                    int sumBundles = details.Sum(temp => temp.Bundles);

                    //添加出库申请主表
                    NFMT.WareHouse.Model.StockOutApply outApply = new StockOutApply();
                    outApply.ApplyId = applyId;
                    outApply.ContractId = sub.ContractId;
                    outApply.SubContractId = sub.SubId;
                    outApply.GrossAmount = sumGrossAmount;
                    outApply.NetAmount = sumNetAmount;
                    outApply.Bundles = sumBundles;
                    outApply.UnitId = sub.UnitId;
                    outApply.BuyCorpId = buyCorpId;

                    result = outApplyDAL.Insert(user, outApply);

                    if (result.ResultStatus != 0)
                        return result;

                    int outApplyId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out outApplyId) || outApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库申请添加失败";
                        return result;
                    }

                    foreach (Model.StockOutApplyDetail applyDetail in details)
                    {
                        //验证库存
                        result = stockDAL.Get(user, applyDetail.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存不存在";
                            return result;
                        }

                        //判断库存品种是否与合约品种相同
                        if (stock.AssetId != contract.AssetId)
                        {
                            result.ResultStatus = -1;
                            result.Message = "分配库存的品种与合约品种不一致";
                            return result;
                        }

                        //不允许配货库存
                        int planStockInStatus = (int)NFMT.WareHouse.StockStatusEnum.预入库存;
                        int planCustomsStatus = (int)NFMT.WareHouse.StockStatusEnum.预报关库存;
                        int stockStatus = (int)stock.StockStatus;
                        if (stockStatus > planCustomsStatus || stockStatus < planStockInStatus)
                        {
                            result.ResultStatus = -1;
                            result.Message = "访笔库存不能进行出库申请分配";
                            return result;
                        }

                        applyDetail.ContractId = sub.ContractId;
                        applyDetail.DetailStatus = StatusEnum.已生效;
                        applyDetail.SubContractId = sub.SubId;
                        applyDetail.StockOutApplyId = outApplyId;

                        if (contract.TradeBorder == (int)NFMT.Contract.TradeBorderEnum.ForeignTrade)
                            applyDetail.GrossAmount = stock.GrossAmount;
                        else
                            applyDetail.GrossAmount = applyDetail.NetAmount;

                        result = detailDAL.Insert(user, applyDetail);
                        if (result.ResultStatus != 0)
                            return result;

                        int detailApplyId = 0;
                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out detailApplyId) || detailApplyId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存明细添加失败";
                            return result;
                        }

                        //排他表校验
                        result = exclusiveDAL.LoadByStockId(user, stock.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.StockExclusive> excs = result.ReturnValue as List<Model.StockExclusive>;
                        if (excs == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取排他库存信息失败";
                            return result;
                        }

                        decimal excAmount = excs.Sum(temp => temp.ExclusiveAmount);
                        if (excAmount >= stock.CurNetAmount)
                        {
                            result.ResultStatus = -1;
                            result.Message = "该笔库存剩余净重不足，配货失败";
                            return result;
                        }

                        //排他表新增                        
                        NFMT.WareHouse.Model.StockExclusive exculsive = new StockExclusive();
                        exculsive.ApplyId = applyId;
                        exculsive.DetailApplyId = detailApplyId;
                        exculsive.ExclusiveStatus = StatusEnum.已生效;
                        exculsive.StockApplyId = outApplyId;
                        exculsive.StockId = stock.StockId;
                        exculsive.ExclusiveAmount = applyDetail.NetAmount;

                        result = exclusiveDAL.Insert(user, exculsive);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //验证出库申请与子合约签订数量
                    if (sumNetAmount > sub.SignAmount)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库申请数量不能大于子合约签订数量";
                        return result;
                    }

                    //获取当前子合约下所有出库申请明细
                    result = detailDAL.LoadBySubId(user, sub.SubId, NFMT.Common.StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockOutApplyDetail> ds = result.ReturnValue as List<Model.StockOutApplyDetail>;
                    sumNetAmount = ds.Sum(temp => temp.NetAmount);

                    if (sumNetAmount > sub.SignAmount)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库申请数量不能大于子合约可分配数量";
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

        public ResultModel UpdateStockOutApply(UserModel user, int stockOutApplyId, List<Model.StockOutApplyDetail> details, int deptId, string memo, int corpId, int buyCorpId)
        {
            ResultModel result = new ResultModel();

            NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
            NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
            NFMT.WareHouse.DAL.StockOutApplyDAL outApplyDAL = new DAL.StockOutApplyDAL();
            NFMT.WareHouse.DAL.StockOutApplyDetailDAL detailDAL = new DAL.StockOutApplyDetailDAL();
            NFMT.WareHouse.DAL.StockExclusiveDAL exclusiveDAL = new DAL.StockExclusiveDAL();
            NFMT.WareHouse.DAL.StockDAL stockDAL = new StockDAL();
            NFMT.Contract.DAL.ContractDAL contractDAL = new Contract.DAL.ContractDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证分配库存
                    if (details == null || details.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "未分配任务库存";
                        return result;
                    }

                    //验证出库申请
                    result = outApplyDAL.Get(user, stockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    NFMT.WareHouse.Model.StockOutApply stockOutApply = result.ReturnValue as NFMT.WareHouse.Model.StockOutApply;
                    if (stockOutApply == null || stockOutApply.StockOutApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库申请不存在";
                        return result;
                    }

                    //验证子合约
                    result = subDAL.Get(user, stockOutApply.SubContractId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                    if (sub == null || sub.SubId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "子合约不存在";
                        return result;
                    }

                    //验证合约
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

                    //获取部门信息
                    NFMT.User.Model.Department dept = User.UserProvider.Departments.Single(temp => temp.DeptId == deptId);
                    if (dept == null || dept.DeptId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "部门不存在";
                        return result;
                    }

                    //更新主申请表
                    result = applyDAL.Get(user, stockOutApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请不存在";
                        return result;
                    }

                    apply.ApplyDept = dept.DeptId;
                    apply.ApplyCorp = corpId;
                    apply.ApplyDesc = memo;
                    apply.ApplyId = stockOutApply.ApplyId;
                    apply.EmpId = user.EmpId;
                    result = applyDAL.Update(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    //更新出库申请表
                    decimal sumGrossAmount = details.Sum(temp => temp.GrossAmount);
                    decimal sumNetAmount = details.Sum(temp => temp.NetAmount);
                    int sumBundles = details.Sum(temp => temp.Bundles);

                    stockOutApply.GrossAmount = sumGrossAmount;
                    stockOutApply.NetAmount = sumNetAmount;
                    stockOutApply.Bundles = sumBundles;
                    stockOutApply.Status = StatusEnum.已录入;
                    stockOutApply.BuyCorpId = buyCorpId;
                    result = this.stockoutapplyDAL.Update(user, stockOutApply);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废出库申请明细表
                    result = detailDAL.Load(user, stockOutApply.StockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    List<NFMT.WareHouse.Model.StockOutApplyDetail> resultDetails = result.ReturnValue as List<NFMT.WareHouse.Model.StockOutApplyDetail>;
                    if (resultDetails == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库申请明细获取失败";
                        return result;
                    }
                    foreach (NFMT.WareHouse.Model.StockOutApplyDetail d in resultDetails)
                    {
                        d.DetailStatus = StatusEnum.已录入;
                        result = detailDAL.Invalid(user, d);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //作废排他表
                    result = exclusiveDAL.Load(user, stockOutApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    List<NFMT.WareHouse.Model.StockExclusive> exculsives = result.ReturnValue as List<NFMT.WareHouse.Model.StockExclusive>;
                    if (exculsives == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "排他表获取失败";
                        return result;
                    }
                    foreach (NFMT.WareHouse.Model.StockExclusive e in exculsives)
                    {
                        e.ExclusiveStatus = StatusEnum.已录入;
                        result = exclusiveDAL.Invalid(user, e);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //新增出库申请明细表
                    foreach (Model.StockOutApplyDetail detail in details)
                    {
                        //验证库存
                        result = stockDAL.Get(user, detail.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存不存在";
                            return result;
                        }

                        //判断库存品种是否与合约品种相同
                        if (stock.AssetId != contract.AssetId)
                        {
                            result.ResultStatus = -1;
                            result.Message = "分配库存的品种与合约品种不一致";
                            return result;
                        }

                        //不允许配货库存
                        int planStockInStatus = (int)NFMT.WareHouse.StockStatusEnum.预入库存;
                        int planCustomsStatus = (int)NFMT.WareHouse.StockStatusEnum.预报关库存;
                        int stockStatus = (int)stock.StockStatus;
                        if (stockStatus > planCustomsStatus || stockStatus < planStockInStatus)
                        {
                            result.ResultStatus = -1;
                            result.Message = "访笔库存不能进行出库申请分配";
                            return result;
                        }

                        detail.ContractId = sub.ContractId;
                        detail.DetailStatus = StatusEnum.已生效;
                        detail.SubContractId = sub.SubId;
                        detail.StockOutApplyId = stockOutApply.StockOutApplyId;

                        if (contract.TradeBorder == (int)NFMT.Contract.TradeBorderEnum.ForeignTrade)
                            detail.GrossAmount = stock.GrossAmount;
                        else
                            detail.GrossAmount = detail.NetAmount;

                        result = detailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        int detailApplyId = 0;
                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out detailApplyId) || detailApplyId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存明细添加失败";
                            return result;
                        }

                        //排他表校验
                        result = exclusiveDAL.LoadByStockId(user, stock.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.StockExclusive> excs = result.ReturnValue as List<Model.StockExclusive>;
                        if (excs == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取排他库存信息失败";
                            return result;
                        }

                        decimal excAmount = excs.Sum(temp => temp.ExclusiveAmount);
                        if (excAmount >= stock.CurNetAmount)
                        {
                            result.ResultStatus = -1;
                            result.Message = "该笔库存剩余净重不足，配货失败";
                            return result;
                        }

                        //排他表新增                        
                        NFMT.WareHouse.Model.StockExclusive exculsive = new StockExclusive();
                        exculsive.ApplyId = apply.ApplyId;
                        exculsive.DetailApplyId = detailApplyId;
                        exculsive.ExclusiveStatus = StatusEnum.已生效;
                        exculsive.StockApplyId = stockOutApply.StockOutApplyId;
                        exculsive.StockId = stock.StockId;
                        exculsive.ExclusiveAmount = detail.NetAmount;

                        result = exclusiveDAL.Insert(user, exculsive);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //验证出库申请与子合约签订数量
                    if (sumNetAmount > sub.SignAmount)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库申请数量不能大于子合约签订数量";
                        return result;
                    }

                    //获取当前子合约下所有出库申请明细
                    result = detailDAL.LoadBySubId(user, sub.SubId, NFMT.Common.StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockOutApplyDetail> ds = result.ReturnValue as List<Model.StockOutApplyDetail>;
                    sumNetAmount = ds.Sum(temp => temp.NetAmount);

                    if (sumNetAmount > sub.SignAmount)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库申请数量不能大于子合约可分配数量";
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

            //

            return result;
        }

        public ResultModel Invalid(UserModel user, int stockOutApplyId)
        {
            ResultModel result = new ResultModel();

            DAL.StockDAL stockDAL = new StockDAL();
            DAL.StockExclusiveDAL exclusiveDAL = new StockExclusiveDAL();
            DAL.StockOutApplyDAL outApplyDAL = new StockOutApplyDAL();
            Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
            DAL.StockOutApplyDetailDAL detailDAL = new StockOutApplyDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证出库申请
                    result = outApplyDAL.Get(user, stockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockOutApply outApply = result.ReturnValue as Model.StockOutApply;
                    if (outApply == null || outApply.StockOutApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库申请不存在";
                        return result;
                    }

                    //获取主申请实体
                    result = applyDAL.Get(user, outApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Operate.Model.Apply apply = result.ReturnValue as Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请不存在";
                        return result;
                    }

                    //获取申请明细
                    result = detailDAL.Load(user, outApply.StockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<StockOutApplyDetail> details = result.ReturnValue as List<StockOutApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请明细获取失败";
                        return result;
                    }

                    //获取排他表数据
                    result = exclusiveDAL.Load(user, outApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<StockExclusive> exclusives = result.ReturnValue as List<StockExclusive>;
                    if (exclusives == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "排他明细获取失败";
                        return result;
                    }

                    //作废主申请
                    result = applyDAL.Invalid(user, apply);
                    if (result.ResultStatus != 0)
                        return result;
                    //作废申请明细
                    foreach (Model.StockOutApplyDetail detail in details)
                    {
                        detail.DetailStatus = StatusEnum.已录入;
                        result = detailDAL.Invalid(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                    //作废排他表
                    foreach (Model.StockExclusive exclusive in exclusives)
                    {
                        exclusive.ExclusiveStatus = StatusEnum.已录入;
                        result = exclusiveDAL.Invalid(user, exclusive);
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

        public ResultModel GetByApplyId(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = stockoutapplyDAL.GetByApplyId(user, applyId);
            }
            catch (Exception ex)
            {
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

        /// <summary>
        /// 撤返出库申请
        /// 待审核状态下出库申请允许撤返 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stockOutApplyId"></param>
        /// <returns></returns>
        public ResultModel Goback(UserModel user, int stockOutApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockOutApplyDAL outApplyDAL = new StockOutApplyDAL();
                Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证出库申请
                    result = outApplyDAL.Get(user, stockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockOutApply outApply = result.ReturnValue as Model.StockOutApply;
                    if (outApply == null || outApply.StockOutApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, outApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Operate.Model.Apply apply = result.ReturnValue as Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请不存在";
                        return result;
                    }

                    //主申请状态修改至已撤返
                    result = applyDAL.Goback(user, apply);

                    if (result.ResultStatus != 0)
                        return result;

                    //工作流任务关闭
                    WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, apply);
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

        public ResultModel Confirm(UserModel user, int stockOutApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockDAL stockDAL = new StockDAL();
                DAL.StockExclusiveDAL exclusiveDAL = new StockExclusiveDAL();
                DAL.StockOutApplyDAL outApplyDAL = new StockOutApplyDAL();
                Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
                DAL.StockOutApplyDetailDAL detailDAL = new StockOutApplyDetailDAL();
                DAL.StockLogDAL stockLogDAL = new StockLogDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证出库申请
                    result = outApplyDAL.Get(user, stockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockOutApply outApply = result.ReturnValue as Model.StockOutApply;
                    if (outApply == null || outApply.StockOutApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, outApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Operate.Model.Apply apply = result.ReturnValue as Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请不存在";
                        return result;
                    }

                    //验证是否执行完成
                    result = stockoutapplyDAL.CheckStockOutCanConfirm(user, outApply.StockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Common.StatusEnum status = (Common.StatusEnum)result.ReturnValue;

                    //获取已生效出库申请明细
                    result = detailDAL.Load(user, outApply.StockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockOutApplyDetail> details = result.ReturnValue as List<Model.StockOutApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取出库申请明细失败";
                        return result;
                    }

                    //获取已生效排他明细
                    result = exclusiveDAL.Load(user, outApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockExclusive> exclusives = result.ReturnValue as List<Model.StockExclusive>;
                    if (exclusives == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取排他明细失败";
                        return result;
                    }

                    //主申请更新状态至已完成
                    if (status == StatusEnum.已完成)
                        result = applyDAL.Confirm(user, apply);
                    else if (status == StatusEnum.部分完成)
                        result = applyDAL.PartiallyConfirm(user, apply);
                    else
                    {
                        result.ResultStatus = -1;
                        result.Message = "更新主申请状态失败";
                        return result;
                    }
                    if (result.ResultStatus != 0)
                        return result;

                    //出库申请明细更新状态至已完成
                    foreach (Model.StockOutApplyDetail detail in details)
                    {
                        //出库申请明细更新状态至已完成
                        result = detailDAL.Confirm(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //排他明细更新状态至已完成
                    foreach (Model.StockExclusive exc in exclusives)
                    {
                        result = exclusiveDAL.Confirm(user, exc);
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

        public ResultModel StockOutApplyDetailClose(UserModel user, int stockOutApplyId, List<int> detailIds, string memo)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //加载对应出库申请
                    StockOutApplyDAL applyDAL = new StockOutApplyDAL();
                    result = applyDAL.Get(user, stockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    StockOutApply stockOutApply = result.ReturnValue as StockOutApply;
                    if (stockOutApply == null || stockOutApply.StockOutApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "对应出库申请加载失败";
                        return result;
                    }

                    //获取出库申请明细
                    DAL.StockOutApplyDetailDAL stockOutApplyDetailDAL = new StockOutApplyDetailDAL();
                    result = stockOutApplyDetailDAL.Load(user, stockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;
                    List<Model.StockOutApplyDetail> stockOutApplyDetails = result.ReturnValue as List<Model.StockOutApplyDetail>;
                    if (stockOutApplyDetails == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库申请明细获取失败";
                        return result;
                    }

                    //判断detailIds 是否都在出库申请明细中
                    List<int> applyDetails = new List<int>();
                    foreach (Model.StockOutApplyDetail detail in stockOutApplyDetails)
                    {
                        applyDetails.Add(detail.DetailId);
                    }

                    foreach (int id in detailIds)
                    {
                        if (!applyDetails.Contains(id))
                        {
                            result.ResultStatus = -1;
                            result.Message = "出库申请不包含选中库存";
                            return result;
                        }
                    }

                    //关闭申请明细
                    StockExclusiveDAL excDAL = new StockExclusiveDAL();
                    foreach (int id in detailIds)
                    {
                        Model.StockOutApplyDetail applyDetail = stockOutApplyDetails.FirstOrDefault(temp => temp.DetailId == id);
                        if (applyDetail == null || applyDetail.DetailId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "出库申请不包含选中库存";
                            return result;
                        }

                        if (applyDetail.DetailStatus != StatusEnum.已生效)
                        {
                            result.ResultStatus = -1;
                            result.Message = "选中库存状态不匹配，不能进行关闭";
                            return result;
                        }

                        result = stockOutApplyDetailDAL.Close(user, applyDetail);
                        if (result.ResultStatus != 0)
                            return result;

                        //加载对应排他明细
                        result = excDAL.Get(user, stockOutApply.ApplyId, stockOutApply.StockOutApplyId, applyDetail.DetailId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockExclusive excl = result.ReturnValue as Model.StockExclusive;
                        if (excl == null || excl.ExclusiveId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取排他明细失败";
                            return result;
                        }

                        //关闭排他明细
                        result = excDAL.Close(user, excl);
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

        public ResultModel ConfirmCancel(UserModel user, int stockOutApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockOutApplyDAL outApplyDAL = new StockOutApplyDAL();
                Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();
                DAL.StockOutApplyDetailDAL detailDAL = new StockOutApplyDetailDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证出库申请
                    result = outApplyDAL.Get(user, stockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockOutApply outApply = result.ReturnValue as Model.StockOutApply;
                    if (outApply == null || outApply.StockOutApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库申请不存在";
                        return result;
                    }

                    //验证主申请
                    result = applyDAL.Get(user, outApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Operate.Model.Apply apply = result.ReturnValue as Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "主申请不存在";
                        return result;
                    }

                    //主申请状态更新至已生效
                    result = applyDAL.ConfirmCancel(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    //出库申请明细，在已关闭状态下的更新至已生效
                    //获取已关闭的明细
                    result = detailDAL.Load(user, outApply.StockOutApplyId, Common.StatusEnum.已关闭);
                    if (result.ResultStatus != 0)
                        return result;
                    List<Model.StockOutApplyDetail> details = result.ReturnValue as List<Model.StockOutApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取申请明细失败";
                        return result;
                    }

                    foreach (Model.StockOutApplyDetail detail in details)
                    {
                        result = detailDAL.ConfirmCancel(user, detail);
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

        public ResultModel GetCondition(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = stockoutapplyDAL.GetCondition(user, applyId);
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

        public ResultModel GetAuditInfo(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();
            NFMT.Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = stockoutapplyDAL.GetByApplyId(user, applyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockOutApply stockOutApply = result.ReturnValue as Model.StockOutApply;
                    if (stockOutApply == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    result = applyDAL.GetCustomerBala(user, stockOutApply.BuyCorpId, stockOutApply.SubContractId, applyId, 0, stockOutApply.NetAmount);
                    if (result.ResultStatus != 0)
                        return result;

                    string customBala = result.ReturnValue.ToString();

                    if (!string.IsNullOrEmpty(customBala) && customBala.IndexOf('_') > -1)
                    {
                        string corpName = string.Empty;
                        NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == stockOutApply.BuyCorpId);
                        if (corp != null)
                            corpName = corp.CorpName;

                        decimal customBalaValue = 0;
                        if (!decimal.TryParse(customBala.Split('_')[0], out customBalaValue))
                        {
                            result.ResultStatus = -1;
                            result.Message = "转换失败";
                            return result;
                        }

                        string currencyName = customBala.Split('_')[1].ToString();
                        string returnValue = string.Format("本次申请后，按照合同结算，{0} {1}（合{2}）{3}", corpName, customBalaValue > 0 ? "余款" : "欠款", currencyName, string.Format("{0:N}", customBalaValue));

                        if (customBalaValue < 0)
                        {
                            returnValue = string.Format("<font color='#660000'>{0}</font>", returnValue);
                        }

                        result.ResultStatus = 0;
                        result.ReturnValue = returnValue;
                    }
                    else
                    {
                        result.Message = "获取客户余额失败";
                        result.ResultStatus = -1;
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

        public ResultModel CheckContractSubStockOutApplyConfirm(UserModel user, int subId)
        {
            return this.stockoutapplyDAL.CheckContractSubStockOutApplyConfirm(user, subId);
        }

        public SelectModel GetCanStockOutApplyListSelectModel(int pageIndex, int pageSize, string orderStr, string subNo, DateTime applyTimeBegin, DateTime applyTimeEnd, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "soa.StockOutApplyId asc";
            else
                select.OrderStr = orderStr;

            int statusType = (int)Common.StatusTypeEnum.通用状态;
            int readyStatus = (int)Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("soa.StockOutApplyId,a.ApplyDept as ApplyDeptId,dept.DeptName,a.ApplyId,a.ApplyTime,a.ApplyStatus,sd.StatusName,a.ApplyDesc,a.ApplyNo,soa.SubContractId,cs.SubNo,ccdin.CorpName as InnerCorpName,ccdout.CorpName as OutCorpName,a.EmpId,e.Name");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockOutApply soa ");
            sb.Append(" inner join dbo.Apply a on soa.ApplyId = a.ApplyId");
            //sb.Append(" left join dbo.St_StockOutApplyDetail soad on soa.StockOutApplyId = soad.StockOutApplyId");
            sb.Append(" left join dbo.Con_ContractSub cs on soa.SubContractId = cs.SubId");
            sb.Append(" left join dbo.Con_Contract con on con.ContractId = cs.ContractId");
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail ccdin on con.ContractId = ccdin.ContractId and ccdin.IsInnerCorp= 1 and ccdin.IsDefaultCorp=1 and ccdin.DetailStatus={0} ", readyStatus);
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail ccdout on con.ContractId = ccdout.ContractId and ccdout.IsInnerCorp= 0 and ccdout.IsDefaultCorp=1 and ccdout.DetailStatus={0} ", readyStatus);
            sb.Append(" left join NFMT_User.dbo.Employee e on e.EmpId = a.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = a.ApplyStatus and sd.StatusId={0} ", statusType);
            sb.Append(" left join NFMT_User.dbo.Department dept on dept.DeptId = a.ApplyDept ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" soa.StockOutApplyId not in (select StockOutApplyId from dbo.St_StockOut where StockOutStatus >= {0}) ", readyStatus);

            if (status > 0)
                sb.AppendFormat(" and a.ApplyStatus = {0} ", status);
            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and cs.SubNo like '%{0}%'", subNo);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and cs.CreateTime between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays
                    (1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion

        #region report

        public SelectModel GetStockOutReportSelect(int pageIndex, int pageSize, string orderStr, DateTime startDate, DateTime endDate, int outCorpId, int inCorpId, int assetId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "soa.StockOutApplyId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;

            select.ColumnName = " soa.StockOutApplyId,app.ApplyId,app.ApplyTime,app.ApplyNo,appCorp.CorpName as ApplyCorpName,appDept.DeptName as ApplyDeptName,emp.Name as EmpName,cs.SubId,cs.SubNo,inCorp.CorpName as InCorpName,outCorp.CorpName as OutCorpName,ass.AssetName,sd.StatusName,soa.UnitId,mu.MUName,soa.NetAmount ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" NFMT.dbo.St_StockOutApply soa ");
            sb.Append(" inner join NFMT.dbo.Apply app on soa.ApplyId = app.ApplyId ");
            sb.Append(" left join NFMT_User.dbo.Corporation appCorp on appCorp.CorpId = app.ApplyCorp ");
            sb.Append(" left join NFMT_User.dbo.Department appDept on appDept.DeptId = app.ApplyDept ");
            sb.Append(" left join NFMT_User.dbo.Employee emp on emp.EmpId = app.EmpId ");
            sb.Append(" left join NFMT.dbo.Con_ContractSub cs on cs.SubId = soa.SubContractId ");
            sb.Append(" left join NFMT.dbo.Con_SubCorporationDetail inCorp on inCorp.SubId = cs.SubId and inCorp.IsInnerCorp = 1 and inCorp.IsDefaultCorp = 1 ");
            sb.AppendFormat(" and inCorp.DetailStatus >= {0} ", readyStatus);
            sb.Append(" left join NFMT.dbo.Con_SubCorporationDetail outCorp on outCorp.SubId = cs.SubId and outCorp.IsInnerCorp = 0 and outCorp.IsDefaultCorp =1 ");
            sb.AppendFormat(" and outCorp.DetailStatus >= {0} ", readyStatus);
            sb.Append(" left join NFMT.dbo.Con_Contract con on con.ContractId = soa.ContractId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = con.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = app.ApplyStatus ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = soa.UnitId ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" 1=1 ");

            if (startDate > NFMT.Common.DefaultValue.DefaultTime && endDate > startDate)
                sb.AppendFormat(" and app.ApplyTime between '{0}' and '{1}' ", startDate.ToString(), endDate.ToString());
            if (outCorpId > 0)
                sb.AppendFormat(" and outCorp.CorpId = {0} ", outCorpId);
            if (inCorpId > 0)
                sb.AppendFormat(" and inCorp.CorpId = {0}", inCorpId);
            if (assetId > 0)
                sb.AppendFormat(" and con.AssetId = {0}", assetId);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetStockOutDetailReportSelect(int pageIndex, int pageSize, string orderStr, int stockOutApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sod.DetailId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;

            select.ColumnName = " sod.DetailId,so.StockOutTime,sn.RefNo,sto.CardNo,sto.PaperNo,bra.BrandName,soad.ApplyWeight,sd.StatusName ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" NFMT.dbo.St_StockOutDetail sod ");
            sb.Append(" inner join NFMT.dbo.St_StockOut so on sod.StockOutId = so.StockOutId ");
            sb.Append(" inner join NFMT.dbo.St_StockOutApplyDetail soad on soad.DetailId = sod.StockOutApplyDetailId ");
            sb.Append(" inner join NFMT.dbo.St_Stock sto on sod.StockId = sto.StockId ");
            sb.Append(" inner join NFMT.dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = sod.DetailStatus ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" sod.DetailStatus>={0} and so.StockOutApplyId ={1} ", readyStatus, stockOutApplyId);

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="source">数据源,DataTable</param>
        /// <param name="modelPath">模板文件夹路径</param>
        /// <param name="filePath">新文件存放路径</param>
        /// <param name="reportType">报表类型</param>
        /// <returns>新文件全称</returns>
        public override string CreateExcel(System.Data.DataTable source, string modelPath, string filePath, ReportType reportType)
        {
            string newPath = string.Empty;

            try
            {
                newPath = filePath + Guid.NewGuid() + ".xlsx";

                //调用的模板文件
                System.IO.FileInfo mode = new System.IO.FileInfo(string.Format("{0}{1}.xlsx", modelPath, reportType.ToString("F")));
                //mode.IsReadOnly = false;

                Excel.Application app = new Excel.Application();
                if (app == null)
                {
                    return string.Empty;
                }
                app.Application.DisplayAlerts = false;
                app.Visible = false;

                if (mode.Exists)
                {
                    Excel.Workbook tworkbook;
                    Object missing = System.Reflection.Missing.Value;

                    app.Workbooks.Add(missing);
                    //调用模板
                    tworkbook = app.Workbooks.Open(mode.FullName, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                    Excel.Worksheet tworksheet = (Excel.Worksheet)tworkbook.Sheets[1];
                    Excel.Range r = tworksheet.get_Range("A2", missing);

                    string[,] objData = null;

                    if (objData == null)
                        return string.Empty;

                    r = r.get_Resize(objData.GetLength(0), objData.GetLength(1));

                    r.Value = objData;

                    tworksheet.SaveAs(newPath, missing, missing, missing, missing, missing, missing, missing, missing, missing);

                    tworkbook.Close(false, mode.FullName, missing);
                    app.Workbooks.Close();
                    app.Quit();
                }
                else
                    return string.Empty;
            }
            catch
            {
                return string.Empty;
            }

            return newPath;
        }

        #endregion       

        #region 销售合约同时对出库申请的操作

        public ResultModel ContractOutCreateStockOperate(UserModel user, NFMT.Contract.Model.Contract contract, int subId, int outCorpId, List<NFMT.WareHouse.Model.StockOutApplyDetail> details)
        {
            ResultModel result = new ResultModel();

            NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
            NFMT.WareHouse.DAL.StockOutApplyDAL outApplyDAL = new NFMT.WareHouse.DAL.StockOutApplyDAL();
            NFMT.WareHouse.DAL.StockOutApplyDetailDAL detailDAL = new NFMT.WareHouse.DAL.StockOutApplyDetailDAL();
            NFMT.WareHouse.DAL.StockExclusiveDAL exclusiveDAL = new NFMT.WareHouse.DAL.StockExclusiveDAL();
            NFMT.WareHouse.DAL.StockDAL stockDAL = new NFMT.WareHouse.DAL.StockDAL();

            try
            {
                //验证分配库存
                if (details == null || details.Count == 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "未分配任务库存";
                    return result;
                }

                //添加主申请表
                NFMT.Operate.Model.Apply apply = new NFMT.Operate.Model.Apply();
                apply.ApplyDept = user.DeptId;
                apply.ApplyCorp = user.CorpId;
                apply.ApplyTime = DateTime.Now;
                apply.ApplyDesc = string.Empty;
                apply.ApplyType = NFMT.Operate.ApplyType.出库申请;
                apply.EmpId = user.EmpId;
                apply.ApplyStatus = NFMT.Common.StatusEnum.绑定合约;

                result = applyDAL.Insert(user, apply);
                if (result.ResultStatus != 0)
                    return result;

                int applyId = 0;
                if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out applyId) || applyId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "申请主表添加失败";
                    return result;
                }

                decimal sumNetAmount = details.Sum(temp => temp.NetAmount);
                int sumBundles = details.Sum(temp => temp.Bundles);

                //添加出库申请主表
                NFMT.WareHouse.Model.StockOutApply outApply = new NFMT.WareHouse.Model.StockOutApply();
                outApply.ApplyId = applyId;
                outApply.ContractId = contract.ContractId;
                outApply.SubContractId = subId;
                outApply.NetAmount = sumNetAmount;
                outApply.Bundles = sumBundles;
                outApply.UnitId = contract.UnitId;
                outApply.BuyCorpId = outCorpId;

                foreach (NFMT.WareHouse.Model.StockOutApplyDetail applyDetail in details)
                {
                    //验证库存
                    result = stockDAL.Get(user, applyDetail.StockId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                    if (stock == null || stock.StockId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "库存不存在";
                        return result;
                    }

                    //判断库存品种是否与合约品种相同
                    if (stock.AssetId != contract.AssetId)
                    {
                        result.ResultStatus = -1;
                        result.Message = "分配库存的品种与合约品种不一致";
                        return result;
                    }

                    //验证关境
                    if (contract.TradeBorder == (int)NFMT.Contract.TradeBorderEnum.外贸 && stock.CustomsType != (int)NFMT.WareHouse.CustomTypeEnum.关外)
                    {
                        result.ResultStatus = -1;
                        result.Message = "库存关境与合约不一致";
                        return result;
                    }
                    else if (contract.TradeBorder == (int)NFMT.Contract.TradeBorderEnum.内贸 && stock.CustomsType != (int)NFMT.WareHouse.CustomTypeEnum.关内)
                    {
                        result.ResultStatus = -1;
                        result.Message = "库存关境与合约不一致";
                        return result;
                    }

                    //不允许配货库存
                    int planStockInStatus = (int)NFMT.WareHouse.StockStatusEnum.预入库存;
                    int planCustomsStatus = (int)NFMT.WareHouse.StockStatusEnum.预报关库存;
                    int stockStatus = (int)stock.StockStatus;
                    if (stockStatus > planCustomsStatus || stockStatus < planStockInStatus)
                    {
                        result.ResultStatus = -1;
                        result.Message = "访笔库存不能进行出库申请分配";
                        return result;
                    }

                    applyDetail.ContractId = contract.ContractId;
                    applyDetail.DetailStatus = StatusEnum.已生效;
                    applyDetail.SubContractId = subId;

                    if (contract.ContractId == (int)NFMT.Contract.TradeBorderEnum.ForeignTrade)
                        applyDetail.GrossAmount = stock.GrossAmount;
                    else
                        applyDetail.GrossAmount = applyDetail.NetAmount;
                }

                decimal sumGrossAmount = details.Sum(temp => temp.GrossAmount);
                outApply.GrossAmount = sumGrossAmount;
                outApply.CreateFrom = (int)NFMT.Common.CreateFromEnum.销售合约库存创建;
                result = outApplyDAL.Insert(user, outApply);

                if (result.ResultStatus != 0)
                    return result;

                int outApplyId = 0;
                if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out outApplyId) || outApplyId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "出库申请添加失败";
                    return result;
                }

                foreach (NFMT.WareHouse.Model.StockOutApplyDetail applyDetail in details)
                {
                    NFMT.WareHouse.Model.StockOutApplyDetail appDetail = new NFMT.WareHouse.Model.StockOutApplyDetail();
                    appDetail.StockOutApplyId = outApplyId;
                    appDetail.Bundles = applyDetail.Bundles;
                    appDetail.ContractId = applyDetail.ContractId;
                    appDetail.DetailStatus = StatusEnum.已生效;
                    appDetail.GrossAmount = applyDetail.GrossAmount;
                    appDetail.NetAmount = applyDetail.NetAmount;
                    appDetail.StockId = applyDetail.StockId;
                    appDetail.SubContractId = applyDetail.SubContractId;

                    result = detailDAL.Insert(user, appDetail);
                    if (result.ResultStatus != 0)
                        return result;

                    int detailApplyId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out detailApplyId) || detailApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "库存明细添加失败";
                        return result;
                    }

                    //验证库存
                    result = stockDAL.Get(user, applyDetail.StockId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                    if (stock == null || stock.StockId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "库存不存在";
                        return result;
                    }

                    //排他表校验
                    result = exclusiveDAL.LoadByStockId(user, stock.StockId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<NFMT.WareHouse.Model.StockExclusive> excs = result.ReturnValue as List<NFMT.WareHouse.Model.StockExclusive>;
                    if (excs == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取排他库存信息失败";
                        return result;
                    }

                    decimal excAmount = excs.Sum(temp => temp.ExclusiveAmount);
                    if (excAmount + applyDetail.NetAmount > stock.CurNetAmount)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该笔库存剩余净重不足，配货失败";
                        return result;
                    }

                    //排他表新增                        
                    NFMT.WareHouse.Model.StockExclusive exculsive = new NFMT.WareHouse.Model.StockExclusive();
                    exculsive.ApplyId = applyId;
                    exculsive.DetailApplyId = detailApplyId;
                    exculsive.ExclusiveStatus = StatusEnum.已生效;
                    exculsive.StockApplyId = outApplyId;
                    exculsive.StockId = stock.StockId;
                    exculsive.ExclusiveAmount = applyDetail.NetAmount;

                    result = exclusiveDAL.Insert(user, exculsive);
                    if (result.ResultStatus != 0)
                        return result;
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }      

        public ResultModel ContractOutInvalidStockOperate(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.WareHouse.DAL.StockOutApplyDAL stockOutApplyDAL = new NFMT.WareHouse.DAL.StockOutApplyDAL();
                NFMT.WareHouse.DAL.StockOutApplyDetailDAL stockOutApplyDetailDAL = new NFMT.WareHouse.DAL.StockOutApplyDetailDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
                NFMT.WareHouse.DAL.StockExclusiveDAL stockExclusiveDAL = new NFMT.WareHouse.DAL.StockExclusiveDAL();

                //获取子合约出库申请
                result = stockOutApplyDAL.LoadBySubId(user, subId);
                if (result.ResultStatus != 0)
                    return result;

                List<NFMT.WareHouse.Model.StockOutApply> outApplies = result.ReturnValue as List<NFMT.WareHouse.Model.StockOutApply>;
                if (outApplies == null || outApplies.Count == 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取出库申请失败";
                    return result;
                }

                foreach (NFMT.WareHouse.Model.StockOutApply outApply in outApplies)
                {
                    result = applyDAL.Get(user, outApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取主申请失败";
                        return result;
                    }

                    //申请作废
                    result = applyDAL.Invalid(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取出库申请明细
                    result = stockOutApplyDetailDAL.Load(user, outApply.StockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<NFMT.WareHouse.Model.StockOutApplyDetail> details = result.ReturnValue as List<NFMT.WareHouse.Model.StockOutApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取出库申请明细失败";
                        return result;
                    }

                    foreach (NFMT.WareHouse.Model.StockOutApplyDetail detail in details)
                    {
                        detail.DetailStatus = StatusEnum.已录入;
                        result = stockOutApplyDetailDAL.Invalid(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取排他明细
                        result = stockExclusiveDAL.Get(user, apply.ApplyId, outApply.StockOutApplyId, detail.DetailId);
                        if (result.ResultStatus != 0)
                            return result;

                        NFMT.WareHouse.Model.StockExclusive stockExclusive = result.ReturnValue as NFMT.WareHouse.Model.StockExclusive;
                        if (stockExclusive == null || stockExclusive.ExclusiveId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取排他明细失败";
                            return result;
                        }

                        //作废排他明细
                        stockExclusive.ExclusiveStatus = StatusEnum.已录入;
                        result = stockExclusiveDAL.Invalid(user, stockExclusive);
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

        public ResultModel ContractOutCompleteStockOperate(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.WareHouse.DAL.StockOutApplyDAL stockOutApplyDAL = new NFMT.WareHouse.DAL.StockOutApplyDAL();
                NFMT.WareHouse.DAL.StockOutApplyDetailDAL stockOutApplyDetailDAL = new NFMT.WareHouse.DAL.StockOutApplyDetailDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
                NFMT.WareHouse.DAL.StockExclusiveDAL stockExclusiveDAL = new NFMT.WareHouse.DAL.StockExclusiveDAL();

                //获取子合约出库申请
                result = stockOutApplyDAL.LoadBySubId(user, subId);
                if (result.ResultStatus != 0)
                    return result;

                List<NFMT.WareHouse.Model.StockOutApply> outApplies = result.ReturnValue as List<NFMT.WareHouse.Model.StockOutApply>;
                if (outApplies == null || outApplies.Count == 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取出库申请失败";
                    return result;
                }

                foreach (NFMT.WareHouse.Model.StockOutApply outApply in outApplies)
                {
                    //验证是否执行完成
                    result = stockOutApplyDAL.CheckStockOutCanConfirm(user, outApply.StockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    result = applyDAL.Get(user, outApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取主申请失败";
                        return result;
                    }

                    //主申请完成
                    result = applyDAL.Confirm(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取出库申请明细
                    result = stockOutApplyDetailDAL.Load(user, outApply.StockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<NFMT.WareHouse.Model.StockOutApplyDetail> details = result.ReturnValue as List<NFMT.WareHouse.Model.StockOutApplyDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取出库申请明细失败";
                        return result;
                    }

                    foreach (NFMT.WareHouse.Model.StockOutApplyDetail detail in details)
                    {
                        //出库申请明细完成
                        result = stockOutApplyDetailDAL.Confirm(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取排他明细
                        result = stockExclusiveDAL.Get(user, apply.ApplyId, outApply.StockOutApplyId, detail.DetailId);
                        if (result.ResultStatus != 0)
                            return result;

                        NFMT.WareHouse.Model.StockExclusive stockExclusive = result.ReturnValue as NFMT.WareHouse.Model.StockExclusive;
                        if (stockExclusive == null || stockExclusive.ExclusiveId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取排他明细失败";
                            return result;
                        }

                        //排他明细完成
                        result = stockExclusiveDAL.Confirm(user, stockExclusive);
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

        public ResultModel ContractOutAuditStockOperate(UserModel user, int subId, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.WareHouse.DAL.StockOutApplyDAL stockOutApplyDAL = new NFMT.WareHouse.DAL.StockOutApplyDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();

                //获取子合约出库申请
                result = stockOutApplyDAL.LoadBySubId(user, subId);
                if (result.ResultStatus != 0)
                    return result;

                List<NFMT.WareHouse.Model.StockOutApply> outApplies = result.ReturnValue as List<NFMT.WareHouse.Model.StockOutApply>;
                if (outApplies == null || outApplies.Count == 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取出库申请失败";
                    return result;
                }

                foreach (NFMT.WareHouse.Model.StockOutApply outApply in outApplies)
                {
                    result = applyDAL.Get(user, outApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply == null || apply.ApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取主申请失败";
                        return result;
                    }

                    //审核出库申请
                    result = applyDAL.Audit(user, apply, isPass);
                    if (result.ResultStatus != 0)
                        return result;
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
