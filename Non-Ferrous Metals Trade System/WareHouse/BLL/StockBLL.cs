/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockBLL.cs
// 文件功能描述：库存dbo.Stock业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
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

namespace NFMT.WareHouse.BLL
{
    /// <summary>
    /// 库存dbo.Stock业务逻辑类。
    /// </summary>
    public class StockBLL : Common.ExecBLL
    {
        private StockDAL stockDAL = new StockDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StockDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockBLL()
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
            get { return this.stockDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel UpdateStockStatus(UserModel user, Stock stock, StockOperateEnum operate)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    ResultModel stockResult = this.Get(user, stock.StockId);
                    stock = stockResult.ReturnValue as Stock;

                    if (stock == null || stock.StockId <= 0)
                    {
                        result.Message = "库存不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    switch (operate)
                    {
                        case StockOperateEnum.出库:
                            if (stock.StockStatus != StockStatusEnum.在库正常)
                            {
                                result.Message = "非正常库存不能进行出库操作";
                                return result;
                            }
                            result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.预售库存);
                            break;
                        case StockOperateEnum.入库:
                            result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.预入库存);
                            break;
                        case StockOperateEnum.质押:
                            if (stock.StockStatus != StockStatusEnum.在库正常)
                            {
                                result.Message = "非正常库存不能进行质押操作";
                                return result;
                            }
                            result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.预押库存);
                            break;
                        case StockOperateEnum.回购:
                            if (stock.StockStatus != StockStatusEnum.质押库存)
                            {
                                result.Message = "非质押库存不能进行回购操作";
                                return result;
                            }
                            result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.预回购库存);
                            break;
                        default:
                            result.Message = "不存在库存操作类型";
                            result.ResultStatus = -1;
                            break;
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

        /// <summary>
        /// 获取分页查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="stockName">业务单号</param>
        /// <param name="stockDateBegin">入库日期开始</param>
        /// <param name="stockDateEnd">入库日期结束</param>
        /// <param name="corpId">归属公司</param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, string stockName, string stockDateBegin, string stockDateEnd, int corpId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "S.StockId asc";
            else
                select.OrderStr = orderStr;


            select.ColumnName = "S.StockId,S.StockDate,SN.StockName,C.CorpName,A.AssetName,S.StockQty + M.MUName as Weight,B.BrandName,BD.StatusName,BDD.DetailName as CustomStatus,BDD2.DetailName as StockType ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_Stock S left join dbo.St_StockName SN on S.StockNameId = SN.StockNameId  ");
            sb.Append(" left join NFMT_User.dbo.Corporation C on S.CorpId = C.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset A on S.AssetId = A.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit M on S.UintId = M.MUId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand B on B.BrandId = S.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStatusDetail BD on BD.StatusCode = S.StockStatus ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail BDD on BDD.StyleDetailId = S.CustomStatus ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail BDD2 on BDD2.StyleDetailId = S.StockType ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" BD.StatusId = (select top 1 StatusId from dbo.BDStatus where ISNULL(StatusName,'') = '库存状态') ");
            sb.Append(" and BDD.BDStyleId = (select top 1 BDStyleId from dbo.BDStyle where isnull(BDStyleName,'') = '报关状态') ");
            sb.Append(" and BDD2.BDStyleId = (select top 1 BDStyleId from dbo.BDStyle where isnull(BDStyleName,'') = '库存类型') ");

            if (!string.IsNullOrEmpty(stockDateBegin))
                sb.AppendFormat(" and S.StockDate >= '{0}'", stockDateBegin);
            if (!string.IsNullOrEmpty(stockDateEnd))
                sb.AppendFormat(" and S.StockDate <= '{0}'", stockDateEnd);
            if (corpId >= 0)
                sb.AppendFormat(" and S.CorpId = {0}", corpId);
            if (!string.IsNullOrEmpty(stockName))
                sb.AppendFormat(" and SN.StockName like '%{0}%'", stockName);

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 可配货库存列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <returns></returns>
        public SelectModel GetCanSalesSelect(int pageIndex, int pageSize, string orderStr, string sids = "", int contractId = 0, int StockApplyId = 0, string dids = "", string refNo = "", int ownCorpId = 0)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId asc";
            else
                select.OrderStr = orderStr;

            if (string.IsNullOrEmpty(dids))
                dids = "0";

            int readyStatus = (int)Common.StatusEnum.已生效;

            select.ColumnName = "sto.StockId,sn.RefNo,sto.UintId,sto.CurNetAmount,mu.MUName,sto.StockStatus,sd.StatusName,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,soad.ApplyAmount,ISNULL(sto.CurNetAmount,0) - ISNULL(soad.ApplyAmount,0) as LastAmount,ISNULL(sto.CurNetAmount,0) - ISNULL(soad.ApplyAmount,0) as NetAmount,sto.Bundles - isnull(soad.ApplyBundles,0) as LaveBundles,sto.Bundles - isnull(soad.ApplyBundles,0) as Bundles,dp.DPName,sto.CardNo ";

            int statusId = (int)Common.StatusTypeEnum.库存状态;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_Stock sto ");
            sb.AppendFormat(" inner join NFMT.dbo.Con_Contract con on con.AssetId = sto.AssetId and con.UnitId = sto.UintId and con.ContractId = {0} ", contractId);
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = sto.StockStatus and sd.StatusId ={0} ", statusId);
            sb.Append(" left join NFMT_User.dbo.Corporation cor on cor.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");

            sb.AppendFormat("left join (select sum(NetAmount) as ApplyAmount,sum(Bundles) as ApplyBundles,StockId from NFMT.dbo.St_StockOutApplyDetail where DetailStatus ={0} and DetailId not in ({1}) group by StockId) as soad on soad.StockId = sto.StockId", readyStatus, dids);
            select.TableName = sb.ToString();

            sb.Clear();

            int planStockInStatus = (int)StockStatusEnum.预入库存;
            int planCustomsStatus = (int)StockStatusEnum.新拆库存;

            sb.AppendFormat(" sto.StockStatus between {0} and {1} ", planStockInStatus, planCustomsStatus);
            sb.AppendFormat(" and ISNULL(sto.CurNetAmount,0) - ISNULL(soad.ApplyAmount,0) > 0 ");
            if (StockApplyId > 0)
            {
                sb.AppendFormat(" and sto.StockId not in (select StockId from dbo.St_StockOutApplyDetail where StockOutApplyId ={0} and DetailStatus ={1} ) ", StockApplyId, readyStatus);
            }
            if (!string.IsNullOrEmpty(sids))
            {
                sids = sids.Trim();
                sb.AppendFormat(" and sto.StockId not in ({0})", sids);
            }

            if (!string.IsNullOrEmpty(refNo))
                sb.AppendFormat(" and sn.RefNo like '%{0}%'", refNo);

            if (ownCorpId > 0)
                sb.AppendFormat(" and sto.CorpId={0} ", ownCorpId);

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 库存查看列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <returns></returns>
        public SelectModel GetStockListSelect(int pageIndex, int pageSize, string orderStr, string stockName, DateTime stockDateBegin, DateTime stockDateEnd, int corpId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "sto.StockId,sto.StockDate,sn.RefNo,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,cast(sto.NetAmount as varchar)+mu.MUName as StockWeight,sto.BrandId,bra.BrandName,sto.CustomsType ,custom.DetailName as CustomsTypeName,sto.StockStatus,sd.StatusName,dp.DPName,sto.CardNo ";

            int customsType = (int)Data.StyleEnum.报关状态;
            int statusType = (int)Common.StatusTypeEnum.库存状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_Stock sto ");
            sb.Append(" left join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
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

        public SelectModel GetStockListSelect(int pageIndex, int pageSize, string orderStr, string stockName, DateTime stockDateBegin, DateTime stockDateEnd, int corpId, int stockStatus)
        {
            SelectModel select = this.GetStockListSelect(pageIndex, pageSize, orderStr, stockName, stockDateBegin, stockDateEnd, corpId);

            if (stockStatus > 0)
                select.WhereStr += string.Format(" and sto.StockStatus = {0}", stockStatus);

            return select;
        }

        public ResultModel StockInHandle(UserModel user, NFMT.WorkFlow.Model.DataSource dataSource, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    NFMT.Common.Operate operate = NFMT.Common.Operate.CreateOperate(dataSource.DalName, dataSource.AssName);
                    if (operate == null)
                    {
                        result.Message = "模板不存在";
                        return result;
                    }

                    NFMT.Common.AuditModel model = new AuditModel()
                    {
                        AssName = dataSource.AssName,
                        DalName = dataSource.DalName,
                        DataBaseName = dataSource.BaseName,
                        Id = dataSource.RowId,
                        Status = dataSource.DataStatus,
                        TableName = dataSource.TableCode
                    };

                    //审核，修改数据状态
                    result = operate.Audit(user, model, isPass);
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

        public ResultModel GetStockContractOutCorp(UserModel user, int stockId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = stockDAL.GetStockContractOutCorp(user, stockId);
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

        public ResultModel GetCurrencyId(UserModel user, int stockId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = stockDAL.GetCurrencyId(user, stockId);
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
        /// 可拆单库存列表
        /// 判断依据
        /// 1：关外库存
        /// 2：拆单表中无已录入数据
        /// 3：排他表中无库存记录
        /// 4：在库正常下的库存
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="stockName"></param>
        /// <param name="stockDateBegin"></param>
        /// <param name="stockDateEnd"></param>
        /// <param name="corpId"></param>
        /// <param name="stockStatus"></param>
        /// <returns></returns>
        public SelectModel GetStockListSelectForSplitDoc(int pageIndex, int pageSize, string orderStr, string stockName, DateTime stockDateBegin, DateTime stockDateEnd, int corpId, int stockStatus)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " sto.StockId,sto.StockDate,sn.RefNo,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,cast(sto.NetAmount as varchar)+mu.MUName as StockWeight,sto.BrandId,bra.BrandName,sto.CustomsType ,custom.DetailName as CustomsTypeName,sto.StockStatus,sd.StatusName,dp.DPName,sto.CardNo ";

            int customsType = (int)Data.StyleEnum.报关状态;
            int stockStatusType = (int)Common.StatusTypeEnum.库存状态;
            int readyStatus = (int)Common.StatusEnum.已生效;
            int entryStatus = (int)Common.StatusEnum.已录入;
            int inWare = (int)NFMT.WareHouse.StockStatusEnum.在库正常;
            int outCustoms = (int)NFMT.WareHouse.CustomTypeEnum.关外;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_Stock sto ");
            sb.Append(" left join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail sd on sd.DetailId = sto.StockStatus  and sd.StatusId ={0} ", stockStatusType);
            sb.Append(" left join NFMT_User.dbo.Corporation cor on sto.CorpId = cor.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail custom on custom.StyleDetailId = sto.CustomsType and custom.BDStyleId ={0} ", customsType);

            sb.AppendFormat(" left join dbo.St_SplitDoc sdoc on sdoc.OldStockId = StockId and sdoc.SplitDocStatus>={0} ", entryStatus);
            sb.AppendFormat(" left join ( select StockId from dbo.St_StockExclusive where ExclusiveStatus>={0} group by StockId ) as se on se.StockId = sto.StockId ", readyStatus);
            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" sdoc.OldStockId is null and se.StockId is null ");
            //sb.AppendFormat(" and sto.CustomsType = {0} ", outCustoms);
            sb.AppendFormat(" and sto.StockStatus = {0} ", inWare);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel UpdateStockInfo(UserModel user, int stockId, string cardNo, string refNo, int stockType, int stockOperateType, string stockNo)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = stockDAL.Get(user, stockId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Stock stock = result.ReturnValue as Model.Stock;
                    if (stock == null || stock.StockId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取库存失败";
                        return result;
                    }

                    DAL.StockNameDAL stockNameDAL = new StockNameDAL();
                    result = stockNameDAL.Get(user, stock.StockNameId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockName stockName = result.ReturnValue as Model.StockName;
                    if (stockName == null || stockName.StockNameId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取业务单号失败";
                        return result;
                    }

                    stockName.RefNo = refNo;

                    result = stockNameDAL.Update(user, stockName);
                    if (result.ResultStatus != 0)
                        return result;

                    stock.StockNo = stockNo;
                    stock.CardNo = cardNo;
                    stock.StockType = stockType;
                    stock.Memo = stockOperateType.ToString();
                    stock.StockStatus = StockStatusEnum.流程中库存;

                    result = stockDAL.Update(user, stock);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ResultStatus == 0)
                        result.ReturnValue = stock;

                    scope.Complete();
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel PreToRealHandle(UserModel user, NFMT.WorkFlow.Model.DataSource dataSource, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.stockDAL.Get(user, dataSource.RowId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Stock stock = result.ReturnValue as Model.Stock;

                    if (isPass)
                    {
                        result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.在库正常);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                    else
                    {
                        result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.预入库存);
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

        #region Report

        public SelectModel GetStockReportSelect(int pageIndex, int pageSize, string orderStr, string stockName, DateTime stockDateBegin, DateTime stockDateEnd, int corpId, int stockStatus, int saleInfo)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "sto.StockId,sto.StockDate,sn.RefNo,sto.CorpId,cor.CorpName,sto.AssetId,ass.AssetName,cast(sto.CurNetAmount as varchar)+mu.MUName as NetWeight,cast(sto.CurGrossAmount as varchar)+mu.MUName as GrossWeight,sto.CurNetAmount,sto.CurGrossAmount,mu.MUName,sto.BrandId,bra.BrandName,sto.CustomsType ,custom.DetailName as CustomsTypeName,sto.StockStatus,sd.StatusName,sto.DeliverPlaceId,dp.DPName,sto.PaperNo,sto.CardNo ";//,sa.AttachId,attach.AttachName

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
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            //sb.Append(" left join dbo.St_StocktAttach sa on sa.StockId = sto.StockId ");
            //sb.Append(" left join dbo.Attach attach on sa.AttachId = attach.AttachId ");
            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(stockName))
                sb.AppendFormat(" and sn.RefNo like '%{0}%'", stockName);
            if (corpId > 0)
                sb.AppendFormat(" and sto.CorpId = {0}", corpId);
            if (stockStatus > 0)
                sb.AppendFormat(" and sto.StockStatus = {0}", stockStatus);
            if (stockDateBegin > NFMT.Common.DefaultValue.DefaultTime && stockDateEnd > stockDateBegin)
                sb.AppendFormat(" and sto.StockDate between '{0}' and '{1}' ", stockDateBegin.ToString(), stockDateEnd.ToString());
            if (saleInfo == 1)//当前库存
                sb.Append(" and sto.CurNetAmount >0 ");
            if (saleInfo == 2)//已售库存
                sb.Append(" and sto.CurNetAmount = 0 ");

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion
    }
}
