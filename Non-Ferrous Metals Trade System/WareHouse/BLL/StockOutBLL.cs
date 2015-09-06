/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockOutBLL.cs
// 文件功能描述：出库dbo.St_StockOut业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月4日
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
    /// 出库dbo.St_StockOut业务逻辑类。
    /// </summary>
    public class StockOutBLL:Common.ExecBLL
    {
        private StockOutDAL stockoutDAL = new StockOutDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StockOutDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public StockOutBLL()
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
            get { return this.stockoutDAL; }
        }
		        
        #endregion

        #region 新增方法

        /// <summary>
        /// 获取分页查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="subContractNo">子合约号</param>
        /// <param name="stockOutTimeBegin">出库日期开始</param>
        /// <param name="stockOutTimeEnd">出库日期结束</param>
        /// <param name="status">出库状态</param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, string contractNo, DateTime stockOutTimeBegin, DateTime stockOutTimeEnd, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            int readyStatus = (int)Common.StatusEnum.已生效;
            int statusType = (int)Common.StatusTypeEnum.通用状态;

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "so.StockOutId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("so.StockOutId,so.StockOutTime,cs.SubId,cs.SubNo,soa.StockOutApplyId,app.ApplyId,app.ApplyNo,so.StockOutStatus,sos.StatusName, inCorp.CorpId as InCorpId ,inCorp.CorpName as InCorpName,outCorp.CorpId as OutCorpId,outCorp.CorpName as OutCorpName,cs.UnitId,ass.AssetName,so.Executor , emp.Name as ExecutorName,od.SumWeight,CAST(od.SumWeight as varchar)+mu.MUName as OutWeight");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockOut so ");
            sb.Append(" left join dbo.St_StockOutApply soa on so.StockOutApplyId= soa.StockOutApplyId ");
            sb.Append(" left join dbo.Apply app on soa.ApplyId = app.ApplyId ");
            sb.Append(" left join dbo.Con_ContractSub cs on soa.SubContractId = cs.SubId ");
            sb.Append(" left join dbo.Con_Contract con on cs.ContractId = con.ContractId ");
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail inCorp on con.ContractId = inCorp.ContractId and inCorp.IsDefaultCorp=1 and inCorp.IsInnerCorp=1 and inCorp.DetailStatus={0} ", readyStatus);
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail outCorp on con.ContractId = outCorp.ContractId and outCorp.IsDefaultCorp=1 and outCorp.IsInnerCorp=0 and outCorp.DetailStatus={0} ", readyStatus);
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on con.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId= cs.UnitId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sos on so.StockOutStatus = sos.DetailId and sos.StatusId={0} ", statusType);
            sb.Append(" left join NFMT_User.dbo.Employee emp on so.Executor = emp.EmpId ");
            sb.AppendFormat(" left join (select StockOutId,SUM(isnull(NetAmount,0)) as SumWeight from dbo.St_StockOutDetail where DetailStatus >={0} group by StockOutId) od on od.StockOutId = so.StockOutId", readyStatus);
            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" 1=1 ");

            if (!string.IsNullOrEmpty(contractNo))
                sb.AppendFormat(" and con.ContractNo like '%{0}%' ", contractNo);
            if (stockOutTimeBegin > Common.DefaultValue.DefaultTime && stockOutTimeEnd > stockOutTimeBegin)
                sb.AppendFormat(" and so.StockOutTime between '{0}' and '{1}' ", stockOutTimeBegin, stockOutTimeEnd);
            if (status > 0)
                sb.AppendFormat(" and so.StockOutStatus = {0}", status);

            select.WhereStr = sb.ToString();

            return select;
        }
        
        public SelectModel GetStockOutedSelect(int pageIndex, int pageSize, string orderStr, int stockOutApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            int readyStatus = (int)Common.StatusEnum.已生效;
            int stockStatusType = (int)Common.StatusTypeEnum.库存状态;

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sod.DetailId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("sod.DetailId,so.StockOutId,soa.StockOutApplyId,app.ApplyId,sto.StockId,sn.StockNameId,sn.RefNo,sto.StockStatus,ss.StatusName as StockStatusName,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,sod.NetAmount,mu.MUName,sto.CurNetAmount,dp.DPName,sto.CardNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockOutDetail sod ");
            sb.Append(" inner join dbo.St_StockOut so on sod.StockOutId = so.StockOutId ");
            sb.AppendFormat(" inner join dbo.St_StockOutApply soa on so.StockOutApplyId = soa.StockOutApplyId and soa.StockOutApplyId={0} ", stockOutApplyId);
            sb.Append(" left join dbo.Apply app on soa.ApplyId = app.ApplyId ");
            sb.Append(" left join dbo.St_Stock sto on sto.StockId = sod.StockId ");
            sb.Append(" left join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on sto.UintId = mu.MUId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId ={0} ",stockStatusType);
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sto.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" sod.DetailStatus>={0} ", readyStatus);
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetStockOutingSelect(int pageIndex, int pageSize, string orderStr, int stockOutApplyId,string sids,int stockOutId =0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            int readyStatus = (int)Common.StatusEnum.已生效;
            int stockStatusType = (int)Common.StatusTypeEnum.库存状态;

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "soad.DetailId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("soad.DetailId,soa.StockOutApplyId,app.ApplyId,sto.StockId,sn.StockNameId,sn.RefNo,sto.CurNetAmount");
            sb.Append(",sto.StockStatus,ss.StatusName as StockStatusName,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName");
            sb.Append(",soad.NetAmount,mu.MUName,soad.Bundles,dp.DPName,sto.CardNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockOutApplyDetail soad ");
            sb.Append(" inner join dbo.St_StockOutApply soa on soad.StockOutApplyId = soa.StockOutApplyId ");
            sb.Append(" inner join dbo.Apply app on app.ApplyId = soa.ApplyId ");
            sb.Append(" inner join dbo.St_Stock sto on soad.StockId = sto.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on sto.UintId = mu.MUId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId ={0} ",stockStatusType);
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sto.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" soad.DetailStatus={0} ", readyStatus);
            sb.AppendFormat(" and soa.StockOutApplyId ={0} ", stockOutApplyId);
            sb.AppendFormat("and soad.DetailId not in (select sod.StockOutApplyDetailId from dbo.St_StockOutDetail sod inner join dbo.St_StockOut so on sod.StockOutId = so.StockOutId where so.StockOutApplyId ={0} and sod.DetailStatus>={1} and sod.StockOutId != {2} )", stockOutApplyId, readyStatus,stockOutId);


            if (!string.IsNullOrEmpty(sids))
                sb.AppendFormat(" and soad.DetailId not in ({0}) ", sids);
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetStockOutSelectedSelect(int pageIndex, int pageSize, string orderStr,string sids)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();
            int stockStatusType = (int)Common.StatusTypeEnum.库存状态;

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "soad.DetailId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("soad.DetailId,soa.StockOutApplyId,app.ApplyId,sto.StockId,sn.StockNameId,sn.RefNo,sto.CurNetAmount,sto.StockStatus,ss.StatusName as StockStatusName,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,soad.NetAmount,mu.MUName,soad.Bundles,dp.DPName,sto.CardNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockOutApplyDetail soad ");
            sb.Append(" inner join dbo.St_StockOutApply soa on soad.StockOutApplyId = soa.StockOutApplyId ");
            sb.Append(" inner join dbo.Apply app on app.ApplyId = soa.ApplyId ");
            sb.Append(" inner join dbo.St_Stock sto on soad.StockId = sto.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on sto.UintId = mu.MUId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId ={0} ", stockStatusType);
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sto.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" 1=2 ");
            if (!string.IsNullOrEmpty(sids))
                sb.AppendFormat(" or soad.DetailId in ({0}) ", sids);
            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel CreateStockOut(NFMT.Common.UserModel user, int stockOutApplyId, List<int> detailIds, string memo)
        {
            ResultModel result = new ResultModel();

            try
            {
                //dal init
                DAL.StockDAL stockDAL = new StockDAL();
                DAL.StockOutApplyDAL stockOutApplyDAL = new StockOutApplyDAL();
                DAL.StockOutApplyDetailDAL stockOutApplyDetailDAL = new StockOutApplyDetailDAL();
                DAL.StockOutDAL stockOutDAL = new StockOutDAL();
                DAL.StockOutDetailDAL stockOutDetailDAL = new StockOutDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取出库申请
                    result = stockOutApplyDAL.Get(user, stockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockOutApply stockOutApply = result.ReturnValue as Model.StockOutApply;
                    if (stockOutApply == null || stockOutApply.StockOutApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库申请不存在";
                        return result;
                    }

                    //获取出库申请明细
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

                    decimal sumGrossAmount = 0;
                    decimal sumNetAmount = 0;
                    int sumBundles = 0;

                    //验证库存及申请明细
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
                            result.Message = "选中库存存在已作废状态库存";
                            return result;
                        }

                        //获取库存
                        result = stockDAL.Get(user, applyDetail.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.Stock stock = result.ReturnValue as Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存不存在";
                            return result;
                        }

                        //库存状态校验
                        if (stock.StockStatus != StockStatusEnum.在库正常 && stock.StockStatus != StockStatusEnum.新拆库存 && stock.StockStatus != StockStatusEnum.质押库存)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存不允许出库，出库失败";
                            return result;
                        }

                        sumGrossAmount += applyDetail.GrossAmount;
                        sumNetAmount += applyDetail.NetAmount;
                        sumBundles += applyDetail.Bundles;
                    }
                    

                    //新增出库表
                    Model.StockOut stockOut = new StockOut();
                    stockOut.Executor = user.EmpId;
                    stockOut.Memo = memo;
                    stockOut.StockOutApplyId = stockOutApply.StockOutApplyId;
                    stockOut.StockOutStatus = StatusEnum.已录入;
                    stockOut.StockOutTime = DateTime.Now;
                    stockOut.Unit = stockOutApply.UnitId;
                    stockOut.GrosstAmount = sumGrossAmount;
                    stockOut.NetAmount = sumNetAmount;
                    stockOut.Bundles = sumBundles;

                    result = stockoutDAL.Insert(user, stockOut);
                    if (result.ResultStatus != 0)
                        return result;

                    int stockOutId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out stockOutId))
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库新增失败";
                        return result;
                    }

                    stockOut.StockOutId = stockOutId;

                    //新增出库明细表
                    foreach (int id in detailIds)
                    {
                        Model.StockOutApplyDetail applyDetail = stockOutApplyDetails.FirstOrDefault(temp => temp.DetailId == id);                        

                        Model.StockOutDetail stockOutDetail = new StockOutDetail();
                        stockOutDetail.DetailStatus = StatusEnum.已生效;
                        stockOutDetail.GrossAmount = applyDetail.GrossAmount;
                        stockOutDetail.NetAmount = applyDetail.NetAmount;
                        stockOutDetail.Bundles = applyDetail.Bundles;
                        stockOutDetail.StockId = applyDetail.StockId;
                        stockOutDetail.StockOutApplyDetailId = applyDetail.DetailId;
                        stockOutDetail.StockOutId = stockOutId;

                        result = stockOutDetailDAL.Insert(user, stockOutDetail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = stockOut;

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

        public ResultModel UpdateStockOut(NFMT.Common.UserModel user, int stockOutId, List<int> detailIds, string memo)
        {
            ResultModel result = new ResultModel();

            try
            {
                //dal init
                DAL.StockDAL stockDAL = new StockDAL();
                DAL.StockOutApplyDAL stockOutApplyDAL = new StockOutApplyDAL();
                DAL.StockOutApplyDetailDAL stockOutApplyDetailDAL = new StockOutApplyDetailDAL();
                DAL.StockOutDAL stockOutDAL = new StockOutDAL();
                DAL.StockOutDetailDAL stockOutDetailDAL = new StockOutDetailDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取出库
                    result = stockOutDAL.Get(user, stockOutId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.StockOut stockOut = result.ReturnValue as NFMT.WareHouse.Model.StockOut;
                    if (stockOut == null || stockOut.StockOutId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库不存在";
                        return result;
                    }

                    //获取出库申请
                    result = stockOutApplyDAL.Get(user, stockOut.StockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockOutApply stockOutApply = result.ReturnValue as Model.StockOutApply;
                    if (stockOutApply == null || stockOutApply.StockOutApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库申请不存在";
                        return result;
                    }

                    //获取出库申请明细
                    result = stockOutApplyDetailDAL.Load(user, stockOut.StockOutApplyId);
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

                    //作废原有出库明细
                    result = stockOutDetailDAL.Load(user, stockOut.StockOutId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockOutDetail> stockOutDetails = result.ReturnValue as List<Model.StockOutDetail>;
                    if (stockOutDetails == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库明细获取失败";
                        return result;
                    }

                    foreach (Model.StockOutDetail stockOutDetail in stockOutDetails)
                    {
                        stockOutDetail.DetailStatus = StatusEnum.已录入;
                        result = stockOutDetailDAL.Invalid(user, stockOutDetail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    decimal sumGrossAmount = 0;
                    decimal sumNetAmount = 0;
                    int sumBundles = 0;

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
                            result.Message = "选中库存存在已作废状态库存";
                            return result;
                        }

                        //获取库存
                        result = stockDAL.Get(user, applyDetail.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.Stock stock = result.ReturnValue as Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存不存在";
                            return result;
                        }

                        //库存状态校验
                        if (stock.StockStatus != StockStatusEnum.在库正常 && stock.StockStatus != StockStatusEnum.新拆库存 && stock.StockStatus != StockStatusEnum.质押库存)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存不允许出库，出库失败";
                            return result;
                        }

                        sumGrossAmount += applyDetail.GrossAmount;
                        sumNetAmount += applyDetail.NetAmount;
                        sumBundles += applyDetail.Bundles;
                    }

                    //更新出库表                   
                    stockOut.Executor = user.EmpId;
                    stockOut.Memo = memo;
                    stockOut.GrosstAmount = sumGrossAmount;
                    stockOut.NetAmount = sumNetAmount;
                    stockOut.Bundles = sumBundles;

                    result = stockoutDAL.Update(user, stockOut);
                    if (result.ResultStatus != 0)
                        return result;

                    //新增出库明细表
                    foreach (int id in detailIds)
                    {
                        Model.StockOutApplyDetail applyDetail = stockOutApplyDetails.FirstOrDefault(temp => temp.DetailId == id);                        

                        Model.StockOutDetail stockOutDetail = new StockOutDetail();
                        stockOutDetail.DetailStatus = StatusEnum.已生效;
                        stockOutDetail.GrossAmount = applyDetail.GrossAmount;
                        stockOutDetail.NetAmount = applyDetail.NetAmount;
                        stockOutDetail.Bundles = applyDetail.Bundles;
                        stockOutDetail.StockId = applyDetail.StockId;
                        stockOutDetail.StockOutApplyDetailId = applyDetail.DetailId;
                        stockOutDetail.StockOutId = stockOut.StockOutId;                        

                        result = stockOutDetailDAL.Insert(user, stockOutDetail);
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

        public ResultModel Invalid(UserModel user, int stockOutId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = stockoutDAL.Get(user, stockOutId);
                    if (result.ResultStatus != 0)
                        return result;

                    StockOut resultObj = result.ReturnValue as StockOut;

                    if (resultObj == null || resultObj.StockOutId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能作废";
                        return result;
                    }

                    result = stockoutDAL.Invalid(user, resultObj);
                    if (result.ResultStatus != 0)
                        return result;

                    StockOutDetailDAL stockOutDetailDAL = new StockOutDetailDAL();
                    result = stockOutDetailDAL.Load(user, resultObj.StockOutId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockOutDetail> details = result.ReturnValue as List<Model.StockOutDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取出库明细失败";
                        return result;
                    }
                    foreach (Model.StockOutDetail detail in details)
                    {
                        detail.Status = StatusEnum.已录入;
                        result = stockOutDetailDAL.Invalid(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                    
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

        public ResultModel Complete(UserModel user, int stockOutId)
        {
            ResultModel result = new ResultModel();

            try
            {
                StockDAL stockDAL = new StockDAL();
                StockLogDAL stockLogDAL = new StockLogDAL();
                StockNameDAL stockNameDAL = new StockNameDAL();
                StockOutDetailDAL stockOutDetailDAL = new StockOutDetailDAL();
                StockOutApplyDAL applyDAL = new StockOutApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证出库
                    result = stockoutDAL.Get(user, stockOutId);
                    if (result.ResultStatus != 0)
                        return result;
                    StockOut resultObj = result.ReturnValue as StockOut;
                    if (resultObj == null || resultObj.StockOutId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能进行执行完成操作";
                        return result;
                    }

                    //执行完成
                    result = stockoutDAL.Complete(user, resultObj);
                    if (result.ResultStatus != 0)
                        return result;

                    //加载明细                    
                    result = stockOutDetailDAL.Load(user, resultObj.StockOutId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockOutDetail> details = result.ReturnValue as List<Model.StockOutDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取出库明细失败";
                        return result;
                    }

                    //加载对应出库申请                    
                    result = applyDAL.Get(user, resultObj.StockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    StockOutApply apply = result.ReturnValue as StockOutApply;
                    if (apply == null || apply.StockOutApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "对应出库申请加载失败";
                        return result;
                    }

                    //明细执行完成
                    foreach(Model.StockOutDetail detail in details)
                    {
                        //出库明细完成
                        result = stockOutDetailDAL.Complete(user,detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取出库流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "出库流水获取失败";
                            return result;
                        }

                        //完成出库流水
                        result = stockLogDAL.Complete(user, stockLog);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取库存
                        result = stockDAL.Get(user, stockLog.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.Stock stock = result.ReturnValue as Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存获取失败";
                            return result;
                        }

                        //库存重量捆数减少
                        stock.CurGrossAmount -= detail.GrossAmount;
                        stock.CurNetAmount -= detail.NetAmount;
                        stock.Bundles -= detail.Bundles;                      

                        //库存状态修改
                        if (stock.StockStatus == StockStatusEnum.预售库存)
                            stock.StockStatus = StockStatusEnum.已售库存;

                        result = stockDAL.Update(user, stock);
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

        public ResultModel Close(UserModel user, int stockOutId)
        {
            ResultModel result = new ResultModel();

            try
            {
                StockOutDetailDAL stockOutDetailDAL = new StockOutDetailDAL();
                StockOutApplyDAL applyDAL = new StockOutApplyDAL();
                StockLogDAL stockLogDAL = new StockLogDAL();
                StockDAL stockDAL = new StockDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证出库
                    result = stockoutDAL.Get(user, stockOutId);
                    if (result.ResultStatus != 0)
                        return result;

                    StockOut resultObj = result.ReturnValue as StockOut;
                    if (resultObj == null || resultObj.StockOutId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能进行执行完成操作";
                        return result;
                    }
                   
                    //关闭出库
                    result = stockoutDAL.Close(user, resultObj);
                    if (result.ResultStatus != 0)
                        return result;

                    //加载对应出库申请                    
                    result = applyDAL.Get(user, resultObj.StockOutApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    StockOutApply apply = result.ReturnValue as StockOutApply;
                    if (apply == null || apply.StockOutApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "对应出库申请加载失败";
                        return result;
                    }

                    //加载明细                   
                    result = stockOutDetailDAL.Load(user, resultObj.StockOutId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<StockOutDetail> details = result.ReturnValue as List<StockOutDetail>;
                    if(details ==null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取明细失败";
                        return result;
                    }

                    foreach (StockOutDetail detail in details)
                    {
                        //关闭明细
                        result = stockOutDetailDAL.Close(user,detail);
                        if(result.ResultStatus !=0)
                            return result;

                        //获取出库流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取出库流水失败";
                            return result;
                        }

                        //关闭出库流水
                        result = stockLogDAL.Close(user, stockLog);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取库存
                        result = stockDAL.Get(user, stockLog.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.Stock stock = result.ReturnValue as Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取库存失败";
                            return result;
                        }

                        if (stock.StockStatus == StockStatusEnum.预售库存)
                        {
                            result = stockDAL.UpdateStockStatusToPrevious(user, stock);
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

        public ResultModel CompleteCancel(UserModel user, int stockOutId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                DAL.StockDAL stockDAL = new StockDAL();
                DAL.StockNameDAL stockNameDAL = new StockNameDAL();
                DAL.StockOutDAL stockOutDAL = new StockOutDAL();
                DAL.StockOutDetailDAL stockOutDetailDAL = new StockOutDetailDAL(); 
                DAL.StockOutApplyDAL outApplyDAL = new StockOutApplyDAL();
                Operate.DAL.ApplyDAL applyDAL = new Operate.DAL.ApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证出库
                    result = stockoutDAL.Get(user, stockOutId);
                    if (result.ResultStatus != 0)
                        return result;
                    StockOut stockOut = result.ReturnValue as StockOut;
                    if (stockOut == null || stockOut.StockOutId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能进行执行完成操作";
                        return result;
                    }

                    //撤销出库
                    result = this.stockoutDAL.CompleteCancel(user, stockOut);
                    if (result.ResultStatus != 0)
                        return result;

                    //验证出库申请
                    result = outApplyDAL.Get(user, stockOut.StockOutApplyId);
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

                    //判断主申请状态是否为已生效，
                    if (apply.ApplyStatus != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请状态只有在已生效下才能进行执行完成撤销";
                        return result;
                    }

                    //获取已完成出库明细
                    result = stockOutDetailDAL.Load(user, stockOut.StockOutId, Common.StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;
                    List<Model.StockOutDetail> details = result.ReturnValue as List<Model.StockOutDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取出库明细错误";
                        return result;
                    }
                    
                    //将所有已完成出库明细更新至已生效
                    foreach (Model.StockOutDetail detail in details)
                    {
                        result = stockOutDetailDAL.CompleteCancel(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取出库流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取出库流水失败";
                            return result;
                        }

                        //更新出库流水状态为已生效
                        result = stockLogDAL.CompleteCancel(user, stockLog);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取库存
                        result = stockDAL.Get(user,stockLog.StockId);
                        if(result.ResultStatus!=0)
                            return result;

                        Model.Stock stock = result.ReturnValue as Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取库存错误";
                            return result;
                        }

                        //库存重量添加
                        stock.CurGrossAmount += detail.GrossAmount;
                        stock.CurNetAmount += detail.NetAmount;
                        stock.Bundles += detail.Bundles;                        

                        //明细关联库存状态更新至前一状态
                        if (stock.StockStatus == StockStatusEnum.已售库存)
                            stock.StockStatus = StockStatusEnum.预售库存;

                        result = stockDAL.Update(user, stock);
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

        public ResultModel GoBack(UserModel user, int stockOutId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证出库
                    result = stockoutDAL.Get(user, stockOutId);
                    if (result.ResultStatus != 0)
                        return result;

                    StockOut resultObj = result.ReturnValue as StockOut;

                    if (resultObj == null || resultObj.StockOutId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能撤返";
                        return result;
                    }

                    //撤返出库
                    result = stockoutDAL.Goback(user, resultObj);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废工作流审核
                    WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, resultObj);
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

        public ResultModel Audit(UserModel user, NFMT.WorkFlow.Model.DataSource dataSource, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockDAL stockDAL = new StockDAL();
                DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                DAL.StockOutDetailDAL detailDAL = new StockOutDetailDAL();
                DAL.StockNameDAL stockNameDAL = new StockNameDAL();
                DAL.StockOutApplyDAL stockOutApplyDAL = new StockOutApplyDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.stockoutDAL.Get(NFMT.Common.DefaultValue.SysUser, dataSource.RowId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockOut stockOut = result.ReturnValue as Model.StockOut;
                    if (stockOut == null || stockOut.StockOutId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库不存在";
                        return result;
                    }

                    //审核，修改数据状态
                    result = this.stockoutDAL.Audit(user, stockOut, isPass);
                    if (result.ResultStatus != 0)
                        return result;

                    //审核通过
                    if (isPass)
                    {
                        //流水操作：写入 流水类型：出库 流水状态：已生效    库存操作：更新 库存状态：预售库存

                        //获取出库申请
                        result = stockOutApplyDAL.Get(user,stockOut.StockOutApplyId);
                        if(result.ResultStatus!=0)
                            return result;

                        Model.StockOutApply stockOutApply = result.ReturnValue as Model.StockOutApply;
                        if(stockOutApply == null || stockOutApply.StockOutApplyId<=0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "出库申请获取失败";
                            return result;
                        }

                        //获取出库明细
                        result = detailDAL.Load(user, stockOut.StockOutId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.StockOutDetail> outDetails = result.ReturnValue as List<Model.StockOutDetail>;
                        if (outDetails == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "出库明细获取失败";
                            return result;
                        }

                        foreach (Model.StockOutDetail outDetail in outDetails)
                        {
                            //获取库存
                            result = stockDAL.Get(user, outDetail.StockId);
                            if (result.ResultStatus != 0)
                                return result;

                            Model.Stock stock = result.ReturnValue as Model.Stock;
                            if (stock == null || stock.StockId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "出库库存不存在";
                                return result;
                            }

                            //获取业务单
                            result = stockNameDAL.Get(user,stock.StockNameId);
                            if(result.ResultStatus!=0)
                                return result;

                            Model.StockName stockName = result.ReturnValue as Model.StockName;
                            if(stockName == null || stockName.StockNameId <=0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "业务单号获取失败";
                                return result;
                            }

                            //出库净重==当前净重时 库存状态更新至 预售库存
                            if (outDetail.NetAmount == stock.CurNetAmount)
                            {
                                result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.预售库存);
                                if (result.ResultStatus != 0)
                                    return result;
                            }

                            //写出库流水，流水状态为已生效
                            Model.StockLog stockLog = new StockLog()
                            {
                                AssetId = stock.AssetId,
                                BrandId = stock.BrandId,
                                CardNo = stock.CardNo,
                                DeliverPlaceId = stock.DeliverPlaceId,
                                GrossAmount = outDetail.GrossAmount,
                                LogDate = DateTime.Now,
                                LogDirection = (int)LogDirectionEnum.Out,
                                LogSource = "dbo.St_StockOutDetail",
                                LogSourceBase = "NFMT",
                                LogStatus = StatusEnum.已生效,
                                LogType = (int)LogTypeEnum.出库,
                                NetAmount = outDetail.NetAmount,
                                MUId = stock.UintId,
                                OpPerson = user.EmpId,
                                PaperHolder = stock.PaperHolder,
                                PaperNo = stock.PaperNo,
                                RefNo = stockName.RefNo,
                                SourceId = outDetail.DetailId,
                                StockId = stock.StockId,
                                StockNameId = stock.StockNameId,
                                SubContractId = stockOutApply.SubContractId,
                                ContractId = stockOutApply.ContractId,
                                Memo = stockOut.Memo,
                                Bundles = outDetail.Bundles
                            };

                            result = stockLogDAL.Insert(user, stockLog);
                            if (result.ResultStatus != 0)
                                return result;

                            int stockLogId = 0;
                            if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out stockLogId) || stockLogId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "出库流水写入失败";
                                return result;
                            }

                            //更新出库明细的出库流水
                            outDetail.StockLogId = stockLogId;
                            result = detailDAL.Update(user, outDetail);
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

        #endregion        
    }
}
