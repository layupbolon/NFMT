/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockInBLL.cs
// 文件功能描述：入库登记dbo.St_StockIn业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月16日
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
    /// 入库登记dbo.St_StockIn业务逻辑类。
    /// </summary>
    public class StockInBLL : Common.ExecBLL
    {
        private StockInDAL stockinDAL = new StockInDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StockInDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockInBLL()
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
            get { return this.stockinDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, string stockName, int status, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "st.StockInId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append(" st.StockInId,convert(varchar,st.StockInId)+','+ convert(varchar,Isnull(ref.RefId,0)) as RefId,st.StockInDate,st.RefNo,case when ISNULL(c.ContractId,0)=0 then c.ContractNo else ISNULL(c.ContractNo,' ') end as ContractNo,case when ISNULL(cs.SubId,0)=0 then cs.SubNo else ISNULL(cs.SubNo,' ') end as SubNo,a.AssetName,CONVERT(varchar,st.GrossAmount) + mu.MUName as GrossAmount,st.StockInStatus,bd.StatusName ");
            sb.Append("st.StockInId,Isnull(ref.RefId,0) as RefId,st.StockInDate,st.RefNo,con.ContractNo,cs.SubId,cs.SubNo,a.AssetName,st.GrossAmount,st.NetAmount,mu.MUName,st.StockInStatus,bd.StatusName,dp.DPName,bra.BrandName,st.CardNo ");
            select.ColumnName = sb.ToString();

            int statusId = (int)Common.StatusEnum.已生效;

            sb.Clear();
            sb.Append(" dbo.St_StockIn st left join dbo.St_ContractStockIn_Ref ref on ref.StockInId = st.StockInId and ref.RefStatus>=50 ");
            sb.Append(" left join dbo.Con_Contract con on ref.ContractId = con.ContractId ");
            sb.Append(" left join dbo.Con_ContractSub cs on ref.ContractSubId = cs.SubId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on st.AssetId = a.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = st.StockInStatus ");
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail outccd on outccd.ContractId = con.ContractId and outccd.IsDefaultCorp = 1 and outccd.IsInnerCorp = 0 and outccd.DetailStatus = {0} ", statusId);
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on st.UintId = mu.MUId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = st.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = st.BrandId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (!string.IsNullOrEmpty(stockName))
                sb.AppendFormat(" and st.RefNo like '%{0}%' ", stockName);
            if (status > 0)
                sb.AppendFormat(" and st.StockInStatus = {0} ", status);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and st.StockInDate between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetSelectModelByConSubId(int pageIndex, int pageSize, string orderStr, int subId = 0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "st.StockInId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)NFMT.Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" st.StockInId,st.RefNo,CONVERT(varchar,st.GrossAmount) + mu.MUName as GrossAmount,st.StockInStatus,bd.StatusName,c.CorpName,a.AssetName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_StockIn st  left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = st.UintId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = st.StockInStatus and StatusId = {0} ", statusId);
            sb.Append(" left join NFMT_User.dbo.Corporation c on st.CorpId = c.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on a.AssetId = st.AssetId ");
            sb.Append(" left join dbo.St_ContractStockIn_Ref ref on ref.StockInId = st.StockInId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" st.StockInStatus>= {0} ", (int)Common.StatusEnum.已录入);

            if (subId > 0)
                sb.AppendFormat(" and ref.ContractSubId = {0}", subId);
            select.WhereStr = sb.ToString();

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
            sb.Append(" con.ContractId,cs.SubId,cs.ContractDate,con.ContractNo,cs.SubNo,inCorp.CorpName as InCorpName,outCorp.CorpName as OutCorpName,ass.AssetName,convert(varchar,cs.SignAmount) + mu.MUName as SignAmount,cast(isnull(ref.SumAmount,0) as varchar) + mu.MUName as StockInWeight ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Con_ContractSub cs ");
            sb.Append(" inner join dbo.Con_Contract con on con.ContractId = cs.ContractId ");
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail inCorp on inCorp.ContractId = cs.ContractId and inCorp.IsDefaultCorp = 1 and inCorp.IsInnerCorp = 1 and inCorp.DetailStatus = {0}  ", readyStatus);
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail outCorp on outCorp.ContractId = cs.ContractId and outCorp.IsDefaultCorp = 1 and outCorp.IsInnerCorp = 0 and outCorp.DetailStatus = {0} ", readyStatus);
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on con.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = cs.UnitId ");

            sb.Append(" left join (select SUM(isnull(si.GrossAmount,0)) as SumAmount,csi.ContractSubId from dbo.St_StockIn si ");
            sb.AppendFormat(" inner join dbo.St_ContractStockIn_Ref csi on csi.StockInId = si.StockInId and csi.RefStatus = {0} ", readyStatus);
            sb.AppendFormat(" where si.StockInStatus >= {0} group by csi.ContractSubId) as ref on ref.ContractSubId = cs.SubId ", entryStatus);
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

        public ResultModel Load(UserModel user, int subId, StatusEnum status = StatusEnum.已生效)
        {
            return stockinDAL.Load(user, subId, status);
        }

        public ResultModel Create(UserModel user, Model.StockIn stockIn, int subId, bool isSubmitAudit)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    NFMT.User.Model.Corporation corporation = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == stockIn.CorpId);
                    if (corporation == null || corporation.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "归属公司不存在";
                        return result;
                    }

                    result = this.ValidateAuth(user, stockIn);
                    if (result.ResultStatus != 0)
                        return result;

                    stockIn.GroupId = corporation.ParentId;
                    stockIn.StockInStatus = StatusEnum.已录入;

                    result = stockinDAL.Insert(user, stockIn);
                    if (result.ResultStatus != 0)
                        return result;

                    int stockInId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out stockInId) || stockInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "入库登录失败";
                        return result;
                    }

                    if (subId > 0)
                    {
                        //获取子合约
                        NFMT.Contract.DAL.ContractSubDAL subDAL = new Contract.DAL.ContractSubDAL();
                        result = subDAL.Get(user, subId);
                        if (result.ResultStatus != 0)
                            return result;

                        Contract.Model.ContractSub sub = result.ReturnValue as Contract.Model.ContractSub;
                        if (sub == null || sub.SubId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "子合约序号错误";
                            return result;
                        }

                        if (stockIn.GrossAmount > sub.SignAmount)
                        {
                            result.ResultStatus = -1;
                            result.Message = "入库重量不能大于子合约签订数量";
                            return result;
                        }

                        //获取当前子合约下所有大于已录状态的入库登记
                        result = stockinDAL.Load(user, sub.SubId, StatusEnum.已录入);
                        if (result.ResultStatus != 0)
                            return result;

                        List<StockIn> stockIns = result.ReturnValue as List<Model.StockIn>;
                        if (stockIns == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取子合约下入库登记失败";
                            return result;
                        }

                        decimal sumAmount = stockIns.Sum(temp => temp.GrossAmount);
                        //验证子合约下入库重量是否超额
                        if (stockIn.GrossAmount > sub.SignAmount - sumAmount)
                        {
                            result.ResultStatus = -1;
                            result.Message = "入库数量大于当前子合约下可入库数量";
                            return result;
                        }

                        NFMT.WareHouse.Model.ContractStockIn contractStockIn = new NFMT.WareHouse.Model.ContractStockIn()
                        {
                            ContractId = sub.ContractId,
                            ContractSubId = sub.SubId,
                            StockInId = stockInId,
                            RefStatus = StatusEnum.已生效
                        };

                        DAL.ContractStockInDAL contractStockInDAL = new ContractStockInDAL();
                        result = contractStockInDAL.Insert(user, contractStockIn);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (result.ResultStatus == 0)
                    {
                        stockIn.StockInId = stockInId;
                        result.ReturnValue = stockIn;
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

        public ResultModel StockInUpdate(UserModel user, Model.StockIn stockIn)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取入库登记
                    result = this.stockinDAL.Get(user, stockIn.StockInId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockIn resultStockIn = result.ReturnValue as Model.StockIn;
                    if (resultStockIn == null || resultStockIn.StockInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "入库登记不存在，不能进行修改";
                        return result;
                    }

                    result = this.ValidateAuth(user, stockIn);
                    if (result.ResultStatus != 0)
                        return result;

                    resultStockIn.CorpId = stockIn.CorpId;
                    resultStockIn.DeptId = stockIn.DeptId;
                    resultStockIn.StockInDate = stockIn.StockInDate;
                    resultStockIn.CustomType = stockIn.CustomType;
                    resultStockIn.GrossAmount = stockIn.GrossAmount;
                    resultStockIn.NetAmount = stockIn.NetAmount;
                    resultStockIn.UintId = stockIn.UintId;
                    resultStockIn.AssetId = stockIn.AssetId;
                    resultStockIn.Bundles = stockIn.Bundles;
                    resultStockIn.BrandId = stockIn.BrandId;
                    resultStockIn.DeliverPlaceId = stockIn.DeliverPlaceId;
                    resultStockIn.ProducerId = stockIn.ProducerId;
                    resultStockIn.PaperNo = stockIn.PaperNo;
                    resultStockIn.PaperHolder = stockIn.PaperHolder;
                    resultStockIn.CardNo = stockIn.CardNo;
                    resultStockIn.RefNo = stockIn.RefNo;
                    resultStockIn.OriginPlaceId = stockIn.OriginPlaceId;
                    resultStockIn.OriginPlace = stockIn.OriginPlace;
                    resultStockIn.Format = stockIn.Format;
                    resultStockIn.StockType = stockIn.StockType;
                    resultStockIn.StockOperateType = stockIn.StockOperateType;

                    result = this.stockinDAL.Update(user, resultStockIn);
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

        public ResultModel InsertStockInAndRef(UserModel user, Model.StockIn stockIn, Model.ContractStockIn contractStockIn)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Insert(user, stockIn);
                    if (result.ResultStatus != 0)
                        return result;

                    contractStockIn.StockInId = (int)result.ReturnValue;

                    NFMT.WareHouse.BLL.ContractStockIn_BLL bll = new ContractStockIn_BLL();
                    result = bll.Insert(user, contractStockIn);

                    if (result.ResultStatus == 0)
                        scope.Complete();
                    else
                        return result;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Invalid(UserModel user, int stockInId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, stockInId);
                    StockIn stockIn = result.ReturnValue as StockIn;

                    if (stockIn == null)
                    {
                        result.Message = "该数据不存在，不能作废";
                        return result;
                    }

                    result = stockinDAL.Invalid(user, stockIn);

                    if (result.ResultStatus == 0)
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
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel GoBack(UserModel user, int stockInId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = stockinDAL.Get(user, stockInId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockIn stockIn = result.ReturnValue as Model.StockIn;
                    if (stockIn == null || stockIn.StockInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "数据不存在，无法撤返";
                        return result;
                    }

                    result = stockinDAL.Goback(user, stockIn);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WorkFlow.DAL.DataSourceDAL dataSourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = dataSourceDAL.SynchronousStatus(user, stockIn);
                    if (result.ResultStatus != 0)
                        return result;

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

        public ResultModel Complete(UserModel user, int stockInId)
        {
            //总体业务描述
            //1：验证入库登记
            //2：完成入库登记
            //3：完成入库登记与合约关联
            //4：完成库存流水与入库登记合约关联
            //5：更新库存流水状态为已完成
            //6：更新库存状态为在库正常

            ResultModel result = new ResultModel();

            try
            {
                DAL.StockInStockDAL stockInStockDAL = new StockInStockDAL();
                DAL.ContractStockInDAL contractStockInDAL = new ContractStockInDAL();
                NFMT.WareHouse.DAL.StockDAL stockDAL = new StockDAL();
                DAL.StockLogDAL stockLogDAL = new StockLogDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = stockinDAL.Get(user, stockInId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockIn stockIn = result.ReturnValue as Model.StockIn;
                    if (stockIn == null || stockIn.StockInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "数据不存在，无法完成";
                        return result;
                    }

                    //入库登记完成
                    result = stockinDAL.Complete(user, stockIn);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取合约关联
                    result = contractStockInDAL.GetByStockInId(user, stockIn.StockInId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ContractStockIn contractStockIn = result.ReturnValue as Model.ContractStockIn;
                    if (contractStockIn == null || contractStockIn.RefId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "入库登记未关联合约，不允许确认完成";
                        return result;
                    }

                    //完成合约关联
                    result = contractStockInDAL.Complete(user, contractStockIn);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取入库登记库存关联
                    result = stockInStockDAL.GetByStockIn(NFMT.Common.DefaultValue.SysUser, stockIn.StockInId);

                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockInStock stockInStock = result.ReturnValue as Model.StockInStock;
                    if (stockInStock == null || stockInStock.RefId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取入库登记与库存关联失败";
                        return result;
                    }

                    //完成入库登记库存关联
                    result = stockInStockDAL.Complete(user, stockInStock);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取库存流水
                    result = stockLogDAL.Get(user, stockInStock.StockLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
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

                    //更新库存表
                    result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.在库正常);
                    if (result.ResultStatus != 0)
                        return result;

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

        public ResultModel CompleteCancel(UserModel user, int stockInId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockDAL stockDAL = new StockDAL();
                DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                DAL.StockInStockDAL stockInStockDAL = new StockInStockDAL();
                DAL.ContractStockInDAL contractStockInDAL = new ContractStockInDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取入库登记
                    result = this.stockinDAL.Get(user, stockInId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockIn stockIn = result.ReturnValue as Model.StockIn;
                    if (stockIn == null || stockIn.StockInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "入库登记不存在";
                        return result;
                    }

                    //入库登记撤销完成
                    result = this.stockinDAL.CompleteCancel(user, stockIn);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取合约关联
                    result = contractStockInDAL.GetByStockInId(user, stockIn.StockInId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ContractStockIn contractStockIn = result.ReturnValue as Model.ContractStockIn;
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

                    Model.StockInStock stockInStock = result.ReturnValue as Model.StockInStock;
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

                    Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
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

                    Model.Stock stock = result.ReturnValue as Model.Stock;
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

        public ResultModel Close(UserModel user, int stockInId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockDAL stockDAL = new StockDAL();
                DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                DAL.StockInStockDAL stockInStockDAL = new StockInStockDAL();
                DAL.StockExclusiveDAL stockExclusiveDAL = new StockExclusiveDAL();
                DAL.ContractStockInDAL contractStockInDAL = new ContractStockInDAL();

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取入库登记
                    result = this.stockinDAL.Get(user, stockInId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockIn stockIn = result.ReturnValue as Model.StockIn;
                    if (stockIn == null || stockIn.StockInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "入库登记不存在";
                        return result;
                    }

                    //入库登记关闭
                    result = this.stockinDAL.Close(user, stockIn);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取合约关联
                    result = contractStockInDAL.GetByStockInId(user, stockIn.StockInId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.ContractStockIn contractStockIn = result.ReturnValue as Model.ContractStockIn;
                    if (contractStockIn != null && contractStockIn.RefId > 0)
                    {
                        //关闭合约关联
                        result = contractStockInDAL.Close(user, contractStockIn);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取入库登记与库存流水关联
                    result = stockInStockDAL.GetByStockIn(user, stockIn.StockInId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockInStock stockInStock = result.ReturnValue as Model.StockInStock;
                    if (stockInStock == null || stockInStock.RefId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "关联获取失败";
                        return result;
                    }

                    //关闭关联
                    result = stockInStockDAL.Close(user, stockInStock);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取库存流水
                    result = stockLogDAL.Get(user, stockInStock.StockLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                    if (stockLog == null || stockLog.StockLogId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "库存流水获取失败";
                        return result;
                    }

                    //关闭库存流水
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
                        result.Message = "库存获取失败";
                        return result;
                    }

                    //库存校验是否在库正常
                    if (stock.StockStatus != StockStatusEnum.预入库存)
                    {
                        result.ResultStatus = -1;
                        result.Message = "库存已由业务关联，不允许关闭";
                        return result;
                    }

                    //库存流水校验是否配货
                    result = stockExclusiveDAL.CheckStockIsInExclusive(user, stock.StockId);
                    if (result.ResultStatus != 0)
                        return result;

                    //更新库存状态为作废库存
                    result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.作废库存);
                    if (result.ResultStatus != 0)
                        return result;

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

        public ResultModel ValidateAuth(NFMT.Common.UserModel user, Model.StockIn stockIn)
        {
            NFMT.Common.ResultModel result = new ResultModel();

            //验证权限
            //1：验证是否有己方公司权限
            //3：以上条件下是否具有内外贸权限
            //5：以上条件下是否具有品种权限

            NFMT.User.DAL.AuthGroupDAL dal = new User.DAL.AuthGroupDAL();

            result = dal.LoadByEmpId(user.EmpId);
            if (result.ResultStatus != 0)
                return result;

            List<NFMT.User.Model.AuthGroup> authGroups = result.ReturnValue as List<NFMT.User.Model.AuthGroup>;

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
            temps = authGroups.FindAll(temp => (temp.CorpId == stockIn.CorpId || temp.CorpId == 0));

            NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == stockIn.CorpId);
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

            sb.AppendFormat("[{0}]", corp.CorpName);

            NFMT.WareHouse.CustomTypeEnum customsType = (NFMT.WareHouse.CustomTypeEnum)stockIn.CustomType;
            sb.AppendFormat(" {0} ", customsType.ToString("F"));
            NFMT.Data.Model.Asset asset = NFMT.Data.BasicDataProvider.Assets.FirstOrDefault(temp => temp.AssetId == stockIn.AssetId);
            if (asset == null || asset.AssetId <= 0)
            {
                result.ResultStatus = -1;
                result.Message = "所选品种不存在";
                return result;
            }
            sb.AppendFormat(" {0} ", asset.AssetName);
            sb.Append("权限");

            //1：以上条件下是否具有内外贸权限
            //temps = temps.Where(temp => temp.TradeBorder == stockIn.CustomType || temp.TradeBorder == 0);

            authGroups = new List<User.Model.AuthGroup>();
            foreach (NFMT.User.Model.AuthGroup auth in temps)
            {
                NFMT.Contract.TradeBorderEnum tradeBorder = (NFMT.Contract.TradeBorderEnum)auth.TradeBorder;
                if (auth.TradeBorder == 0)
                    authGroups.Add(auth);
                else if (customsType == CustomTypeEnum.关外 && tradeBorder == Contract.TradeBorderEnum.外贸)
                    authGroups.Add(auth);
                else if (customsType == CustomTypeEnum.关内 && tradeBorder == Contract.TradeBorderEnum.内贸)
                    authGroups.Add(auth);
            }
            temps = authGroups;
            if (temps == null || temps.Count() == 0)
            {
                result.ResultStatus = -1;
                result.Message = sb.ToString();
                return result;
            }

            //2：以上条件下是否具有品种权限
            temps = authGroups.Where(temp => temp.AssetId == stockIn.AssetId || temp.AssetId == 0);
            if (temps == null || temps.Count() == 0)
            {
                result.ResultStatus = -1;
                result.Message = sb.ToString();
                return result;
            }

            if (temps.Count() > 0)
            {
                result.ResultStatus = 0;
                result.Message = "满足权限";
            }

            return result;
        }

        public ResultModel Audit(UserModel user, NFMT.WorkFlow.Model.DataSource dataSource, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockDAL stockDAL = new StockDAL();
                NFMT.WareHouse.DAL.StockNameDAL stockNameDAL = new StockNameDAL();
                DAL.ContractStockInDAL contractStockInDAL = new ContractStockInDAL();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.stockinDAL.Get(user, dataSource.RowId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockIn stockIn = result.ReturnValue as Model.StockIn;
                    if (stockIn == null || stockIn.StockInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "入库登记不存在";
                        return result;
                    }

                    //审核，修改数据状态
                    result = this.stockinDAL.Audit(user, stockIn, isPass);
                    if (result.ResultStatus != 0)
                        return result;

                    //审核通过
                    if (isPass)
                    {
                        //写入St_StockName表                    
                        result = stockNameDAL.Insert(user, new Model.StockName() { RefNo = stockIn.RefNo });
                        if (result.ResultStatus != 0)
                            return result;

                        int stockNameId = 0;
                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out stockNameId) || stockNameId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "写库存失败";
                            return result;
                        }

                        //写入St_Stock库存表
                        result = stockDAL.Insert(user, new Model.Stock()
                        {
                            StockNameId = stockNameId,
                            StockDate = stockIn.StockInDate,
                            AssetId = stockIn.AssetId,
                            Bundles = stockIn.Bundles,
                            GrossAmount = stockIn.GrossAmount,
                            NetAmount = stockIn.NetAmount,
                            ReceiptInGap = 0,
                            ReceiptOutGap = 0,
                            CurGrossAmount = stockIn.GrossAmount,
                            CurNetAmount = stockIn.NetAmount,
                            UintId = stockIn.UintId,
                            DeliverPlaceId = stockIn.DeliverPlaceId,
                            BrandId = stockIn.BrandId,
                            CustomsType = stockIn.CustomType,
                            GroupId = stockIn.GroupId,
                            CorpId = stockIn.CorpId,
                            DeptId = stockIn.DeptId,
                            ProducerId = stockIn.ProducerId,
                            PaperNo = stockIn.PaperNo,
                            PaperHolder = stockIn.PaperHolder,
                            PreStatus = StockStatusEnum.预入库存,
                            StockStatus = StockStatusEnum.预入库存,
                            CardNo = stockIn.CardNo,
                            Memo = string.Empty,
                            StockType = stockIn.StockType,
                            OriginPlaceId = stockIn.OriginPlaceId,
                            OriginPlace = stockIn.OriginPlace,
                            Format = stockIn.Format
                        });
                        if (result.ResultStatus != 0)
                            return result;

                        int stockId = 0;
                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out stockId) || stockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "写库存失败";
                            return result;
                        }

                        int contractId = 0, subId = 0;

                        //获取合约关联
                        result = contractStockInDAL.GetByStockInId(user, stockIn.StockInId);
                        if (result.ResultStatus == 0)
                        {
                            Model.ContractStockIn contractStockIn = result.ReturnValue as Model.ContractStockIn;
                            if (contractStockIn != null && contractStockIn.RefId > 0)
                            {
                                contractId = contractStockIn.ContractId;
                                subId = contractStockIn.ContractSubId;
                            }
                        }

                        //创建stockLog对象
                        NFMT.WareHouse.Model.StockLog stockLog = new StockLog()
                        {
                            StockId = stockId,
                            StockNameId = stockNameId,
                            RefNo = stockIn.RefNo,
                            LogDirection = (int)LogDirectionEnum.In,
                            LogType = (int)LogTypeEnum.入库,
                            ContractId = contractId,
                            SubContractId = subId,
                            LogDate = DateTime.Now,
                            OpPerson = user.EmpId,
                            Bundles = stockIn.Bundles,
                            GrossAmount = stockIn.GrossAmount,
                            NetAmount = stockIn.NetAmount,
                            MUId = stockIn.UintId,
                            BrandId = stockIn.BrandId,
                            DeliverPlaceId = stockIn.DeliverPlaceId,
                            PaperNo = stockIn.PaperNo,
                            PaperHolder = stockIn.PaperHolder,
                            CardNo = stockIn.CardNo,
                            Memo = string.Empty,
                            LogStatus = StatusEnum.已生效,
                            LogSourceBase = "NFMT",
                            LogSource = "dbo.St_StockIn",
                            SourceId = stockIn.StockInId,

                            AssetId = stockIn.AssetId,
                            CorpId = stockIn.CorpId,
                            CustomsType = stockIn.CustomType,
                            DeptId = stockIn.DeptId,
                            ProducerId = stockIn.ProducerId,
                            StockType = stockIn.StockType,
                            OriginPlaceId = stockIn.OriginPlaceId,
                            OriginPlace = stockIn.OriginPlace,
                            Format = stockIn.Format
                        };

                        DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                        result = stockLogDAL.Insert(user, stockLog);
                        if (result.ResultStatus != 0)
                            return result;

                        int stockLogId = 0;
                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out stockLogId) || stockLogId == 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "流水添加失败";
                            return result;
                        }

                        //写入库登记库存关联表
                        DAL.StockInStockDAL stockInStockDAL = new StockInStockDAL();
                        result = stockInStockDAL.Insert(user, new Model.StockInStock()
                        {
                            StockInId = stockIn.StockInId,
                            StockId = stockId,
                            StockLogId = stockLogId,
                            RefStatus = StatusEnum.已生效
                        });

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

        public ResultModel CheckContractSubStockInConfirm(UserModel user, int subId)
        {
            return this.stockinDAL.CheckContractSubStockInConfirm(user, subId);
        }

        #endregion
    }
}
