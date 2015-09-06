/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockLogBLL.cs
// 文件功能描述：出入库流水dbo.St_StockLog业务逻辑类。
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

namespace NFMT.WareHouse.BLL
{
    /// <summary>
    /// 出入库流水dbo.St_StockLog业务逻辑类。
    /// </summary>
    public class StockLogBLL : Common.ExecBLL
    {
        private StockLogDAL stocklogDAL = new StockLogDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StockLogDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockLogBLL()
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
            get { return this.stocklogDAL; }
        }

        #endregion

        #region 新增方法

        /// <summary>
        /// 获取分页查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "SL.StockLogId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " SL.StockLogId,SL.OpFlag,SL.LogDate,E.Name,SL.Memo ";
            select.TableName = "St_StockLog SL left join dbo.Employee E on E.EmpId = SL.OpPerson";

            return select;
        }

        /// <summary>
        /// 库存处理（此方法不包含事务，必须在事务下执行）
        /// </summary>
        /// <param name="stockLog">库存流水</param>
        /// <param name="stockLogAttach">库存流水附件</param>
        /// <param name="user">当前用户</param>
        /// <param name="bdStyleDetail">库存操作类型</param>
        /// <returns></returns>
        public NFMT.Common.ResultModel StockHandle(NFMT.Common.UserModel user, NFMT.WareHouse.Model.StockLog stockLog, List<NFMT.WareHouse.Model.StockLogAttach> stockLogAttachs, StockOperateEnum stockOperate)
        {
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            //result = CheckUser(user);
            //if (result.ResultStatus != 0)
            //    return result;

            try
            {
                //获取stock
                NFMT.WareHouse.BLL.StockBLL stockBLL = new NFMT.WareHouse.BLL.StockBLL();
                result = stockBLL.Get(user, stockLog.StockId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;

                //根据库存处理类型修改库存状态
                result = stockBLL.UpdateStockStatus(user, stock, stockOperate);
                if (result.ResultStatus != 0)
                    return result;

                //写库存流水
                NFMT.WareHouse.DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                result = stockLogDAL.Insert(user, stockLog);
                if (result.ResultStatus != 0)
                    return result;

                int stockLogId = (int)result.ReturnValue;

                //写库存流水附件
                if (stockLogAttachs != null)
                {
                    NFMT.WareHouse.DAL.StockLogAttachDAL stockLogAttachDAL = new StockLogAttachDAL();
                    foreach (NFMT.WareHouse.Model.StockLogAttach stockLogAttach in stockLogAttachs)
                    {
                        stockLogAttach.StockLogId = stockLogId;
                        result = stockLogAttachDAL.Insert(user, stockLogAttach);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
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

        public SelectModel GetLogListByStockIdSelect(int pageIndex, int pageSize, string orderStr, int stockId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId asc";
            else
                select.OrderStr = orderStr;

            int styleId = (int)NFMT.Data.StyleEnum.流水类型;

            select.ColumnName = " sl.StockLogId,sl.LogDate,sl.LogType,operate.DetailName,sl.OpPerson,emp.Name,sl.Memo,sl.SourceId,sl.LogSource ";
            select.TableName = string.Format(" dbo.St_StockLog sl left join NFMT_Basic.dbo.BDStyleDetail operate on sl.LogType = operate.StyleDetailId and operate.BDStyleId ={0}   left join NFMT_User.dbo.Employee emp on emp.EmpId = sl.OpPerson", styleId);

            select.WhereStr = string.Format(" sl.StockId={0}", stockId);

            return select;
        }

        public SelectModel GetLogListBySubSelect(int pageIndex, int pageSize, string orderStr, int subId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)Common.StatusEnum.已生效;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            select.ColumnName = " sl.StockLogId,sn.RefNo,sto.StockNo,sl.GrossAmount,sl.NetAmount,sl.LogDate,sl.Memo,sl.Bundles,sto.AssetId,sl.BrandId,ass.AssetName,bra.BrandName,sto.UintId,mu.MUName ";


            sb.Clear();
            sb.Append(" dbo.St_StockLog sl ");
            sb.Append(" inner join dbo.St_Stock sto on sl.StockId = sto.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");
            sb.Append(" inner join dbo.Con_ContractSub cs on sl.SubContractId = cs.SubId");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sl.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" cs.SubId = {0} ",subId);
            sb.AppendFormat(" and cs.SubStatus ={0} ",readyStatus);
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetLogsBySubIdSelect(int pageIndex, int pageSize, string orderStr, int subId,string logIds,string refIds)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            if(string.IsNullOrEmpty(logIds))
                logIds="0";
            if (string.IsNullOrEmpty(refIds))
                refIds = "0";

            int stockStatusType = (int)Common.StatusTypeEnum.库存状态;
            int readyStatus = (int)Common.StatusEnum.已生效;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("sl.StockLogId,sto.StockId,sn.StockNameId,sn.RefNo,sto.StockStatus,ss.StatusName,sto.CorpId,ownCorp.CorpName as OwnCorpName");
            sb.Append(",sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,sl.NetAmount,sl.Bundles,sto.UintId,mu.MUName,sl.NetAmount as AppAmount");
            //sb.Append(",0 as ApplyBala");
            sb.Append(",case cs.PriceMode when 43 then sp.AlmostPrice when 44 then sp.FixedPrice else 0 end as Price");
            sb.Append(",cast (case cs.PriceMode when 43 then sp.AlmostPrice when 44 then sp.FixedPrice else 0 end * sl.NetAmount as decimal(18,2)) as ApplyBala");
            sb.Append(",sto.CardNo,dp.DPName ");
            select.ColumnName = sb.ToString();
            
            sb.Clear();
            sb.Append(" NFMT.dbo.St_StockLog sl ");
            sb.Append(" inner join NFMT.dbo.St_Stock sto on sl.StockId = sto.StockId ");
            sb.Append (" inner join NFMT.dbo.St_StockName sn on sn.StockNameId = sto.StockNameId and sl.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId = {0} ",stockStatusType);
            sb.Append(" left join NFMT_User.dbo.Corporation ownCorp on ownCorp.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sto.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");

            sb.AppendFormat(" left join NFMT.dbo.Fun_StockPayApply_Ref spa on spa.StockLogId = sl.StockLogId and spa.RefStatus>={0} and spa.RefId not in ({1}) ",readyStatus, refIds);

            sb.Append(" left join NFMT.dbo.Con_ContractSub cs on cs.SubId = sl.SubContractId ");
            sb.Append(" left join NFMT.dbo.Con_SubPrice sp on sp.SubId = sl.SubContractId ");


            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" sl.LogStatus>={0} and  sl.SubContractId ={1} ",readyStatus,subId);
            sb.AppendFormat(" and sl.StockLogId not in ({0}) ", logIds);
            sb.Append(" and spa.RefId is null ");
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetLogsBySubIdsSelect(int pageIndex, int pageSize, string orderStr, string subIds, string logIds, string refIds)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            if (string.IsNullOrEmpty(logIds))
                logIds = "0";
            if (string.IsNullOrEmpty(refIds))
                refIds = "0";

            int stockStatusType = (int)Common.StatusTypeEnum.库存状态;
            int readyStatus = (int)Common.StatusEnum.已生效;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("sl.StockLogId,sto.StockId,sn.StockNameId,sn.RefNo,sto.StockStatus,ss.StatusName,sto.CorpId,ownCorp.CorpName as OwnCorpName");
            sb.Append(",sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,sl.NetAmount,sl.Bundles,sto.UintId,mu.MUName,sl.NetAmount as AppAmount");
            //sb.Append(",0 as ApplyBala");
            sb.Append(",case cs.PriceMode when 43 then sp.AlmostPrice when 44 then sp.FixedPrice else 0 end as Price");
            sb.Append(",cast (case cs.PriceMode when 43 then sp.AlmostPrice when 44 then sp.FixedPrice else 0 end * sl.NetAmount as decimal(18,2)) as ApplyBala");
            sb.Append(",sto.CardNo,dp.DPName,sl.ContractId,sl.SubContractId as SubId ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT.dbo.St_StockLog sl ");
            sb.Append(" inner join NFMT.dbo.St_Stock sto on sl.StockId = sto.StockId ");
            sb.Append(" inner join NFMT.dbo.St_StockName sn on sn.StockNameId = sto.StockNameId and sl.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId = {0} ", stockStatusType);
            sb.Append(" left join NFMT_User.dbo.Corporation ownCorp on ownCorp.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sto.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");

            sb.AppendFormat(" left join NFMT.dbo.Fun_StockPayApply_Ref spa on spa.StockLogId = sl.StockLogId and spa.RefStatus>={0} and spa.RefId not in ({1}) ", readyStatus, refIds);

            sb.Append(" left join NFMT.dbo.Con_ContractSub cs on cs.SubId = sl.SubContractId ");
            sb.Append(" left join NFMT.dbo.Con_SubPrice sp on sp.SubId = sl.SubContractId ");


            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" sl.LogStatus>={0} and  sl.SubContractId in ({1}) ", readyStatus, subIds);
            sb.AppendFormat(" and sl.StockLogId not in ({0}) ", logIds);
            sb.Append(" and spa.RefId is null ");
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetLogsBySubIdSelect(int pageIndex, int pageSize, string orderStr, int subId, string logIds)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            int stockStatusType = (int)Common.StatusTypeEnum.库存状态;
            int readyStatus = (int)Common.StatusEnum.已生效;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("sl.StockLogId,sto.StockId,sn.StockNameId,sn.RefNo,sto.StockStatus,ss.StatusName,sto.CorpId,ownCorp.CorpName as OwnCorpName,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,sl.NetAmount,sl.Bundles,sto.UintId,mu.MUName,sl.NetAmount as AppAmount,dp.DPName,sto.CardNo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT.dbo.St_StockLog sl ");
            sb.Append(" inner join NFMT.dbo.St_Stock sto on sl.StockId = sto.StockId ");
            sb.Append(" inner join NFMT.dbo.St_StockName sn on sn.StockNameId = sto.StockNameId and sl.StockNameId = sn.StockNameId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus and ss.StatusId = {0} ", stockStatusType);
            sb.Append(" left join NFMT_User.dbo.Corporation ownCorp on ownCorp.CorpId = sto.CorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sto.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            sb.Append(" left join NFMT.dbo.Con_ContractSub cs on cs.SubId = sl.SubContractId ");

            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" sl.LogStatus>={0} and  sl.SubContractId ={1} ", readyStatus, subId);
            sb.AppendFormat(" and sl.StockLogId not in (select StockLogId from dbo.Fun_PaymentStockDetail where DetailStatus <> {0} and SubId = {1}) ", (int)Common.StatusEnum.已作废, subId);
            if (!string.IsNullOrEmpty(logIds))
                sb.AppendFormat(" and sl.StockLogId not in ({0}) ", logIds);
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetSubStockLogsSelect(int pageIndex, int pageSize, string orderStr, string stockName, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("sl.StockLogId,sl.RefNo,sl.AssetId,ass.AssetName,sl.BrandId,bra.BrandName,sl.Format,sl.OriginPlace,sl.NetAmount");
            sb.Append(",sl.DeliverPlaceId,dp.DPName,sl.LogDate,sl.MUId,mu.MUName,sl.CustomsType,ct.DetailName as CustomsTypeName,sl.CardNo ");
            select.ColumnName = sb.ToString();

            int readyStatus = (int)Common.StatusEnum.已生效;

            sb.Clear();
            sb.Append(" dbo.St_StockLog sl ");

            sb.Append(" inner join dbo.St_StockInStock_Ref sis on sl.StockLogId = sis.StockLogId ");

            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sl.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on sl.BrandId = bra.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on sl.DeliverPlaceId = dp.DPId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on sl.MUId= mu.MUId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail ct on ct.StyleDetailId = sl.CustomsType ");

            sb.AppendFormat(" left join dbo.St_ContractStockIn_Ref csi on csi.StockInId = sis.StockInId and csi.RefStatus>={0} ", readyStatus);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" sl.LogStatus={0} and sl.ContractId=0 and sl.LogDirection = {1} ", readyStatus,(int)NFMT.WareHouse.LogDirectionEnum.In);
            sb.Append(" and csi.StockInId is null");

            if (!string.IsNullOrEmpty(stockName))
                sb.AppendFormat(" and sl.RefNo like '%{0}%' ", stockName);          
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and sl.LogDate between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetSubSelectStockLogsSelect(int pageIndex, int pageSize, string orderStr,string stockLogIds)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            if (string.IsNullOrEmpty(stockLogIds))
                stockLogIds = "0";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("sl.StockLogId,sl.RefNo,sl.AssetId,ass.AssetName,sl.BrandId,bra.BrandName,sl.Format,sl.OriginPlace,sl.NetAmount");
            sb.Append(",sl.DeliverPlaceId,dp.DPName,sl.LogDate,sl.MUId,mu.MUName,sl.CustomsType,ct.DetailName as CustomsTypeName,sl.CardNo ");
            select.ColumnName = sb.ToString();

            int readyStatus = (int)Common.StatusEnum.已生效;

            sb.Clear();
            sb.Append(" dbo.St_StockLog sl ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sl.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on sl.BrandId = bra.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on sl.DeliverPlaceId = dp.DPId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on sl.MUId= mu.MUId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail ct on ct.StyleDetailId = sl.CustomsType ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" sl.LogStatus={0} and sl.ContractId=0 ", readyStatus);
            sb.AppendFormat(" and sl.StockLogId in ({0}) ", stockLogIds);
        
            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetStockLogsByContractId(int pageIndex, int pageSize, string orderStr, int contractId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("csi.RefId,sl.LogDate,sl.RefNo,sl.CardNo,sl.AssetId,sl.CustomsType,sl.NetAmount,sl.MUId");
            sb.Append(",ass.AssetName,ct.DetailName as CustomsTypeName,mu.MUName ");
            select.ColumnName = sb.ToString();

            int readyStatus = (int)Common.StatusEnum.已生效;

            sb.Clear();
            sb.Append(" dbo.St_ContractStockIn_Ref csi ");
            sb.Append(" inner join dbo.St_StockInStock_Ref sis on sis.StockInId = csi.StockInId ");
            sb.AppendFormat(" inner join dbo.St_StockLog sl on sis.StockLogId = sl.StockLogId  and sl.LogStatus >={0} ",readyStatus);
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on ass.AssetId = sl.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail ct on ct.StyleDetailId = sl.CustomsType ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sl.MUId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" csi.RefStatus >={0} and csi.ContractId = {1} ", readyStatus,contractId);

            select.WhereStr = sb.ToString();

            return select;
        }
        
        public SelectModel GetContractOutStockSelect(int pageIndex, int pageSize, string orderStr, string stockName, DateTime beginDate, DateTime endDate,string stockIds = "")
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("sto.StockId,sn.RefNo,sto.AssetId,ass.AssetName,sto.BrandId,bra.BrandName,sto.Format,sto.OriginPlace,sto.CurNetAmount,sto.DeliverPlaceId");
            sb.Append(",dp.DPName,sto.StockDate,sto.UintId,mu.MUName,sto.CustomsType,ct.DetailName as CustomsTypeName,sto.CardNo,sto.StockStatus,ss.StatusName");
            sb.Append(",se.ExclusiveAmount, ISNULL(sto.CurNetAmount,0) - ISNULL(se.ExclusiveAmount,0) as NetAmount, ISNULL(sto.CurNetAmount,0) - ISNULL(se.ExclusiveAmount,0) as OutAmount,sto.Bundles,sto.Bundles as OutBundles");
            select.ColumnName = sb.ToString();

            int readyStatus = (int)Common.StatusEnum.已生效;

            sb.Clear();
            sb.Append(" dbo.St_Stock sto ");
            sb.Append("  left join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");

            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sto.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on sto.BrandId = bra.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on sto.DeliverPlaceId = dp.DPId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on sto.UintId= mu.MUId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail ct on ct.StyleDetailId = sto.CustomsType ");
            sb.Append("  left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus ");

            sb.AppendFormat("  left join (select Sum(ExclusiveAmount) as ExclusiveAmount,StockId from dbo.St_StockExclusive where ExclusiveStatus = {0} group by StockId ) as se on se.StockId = sto.StockId ", readyStatus);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" sto.StockStatus in ({0},{1},{2}) ", (int)StockStatusEnum.预入库存, (int)StockStatusEnum.在库正常, (int)StockStatusEnum.新拆库存);
            sb.Append(" and ISNULL(sto.CurNetAmount,0) - ISNULL(se.ExclusiveAmount,0) > 5 ");

            if (!string.IsNullOrEmpty(stockName))
                sb.AppendFormat(" and sn.RefNo like '%{0}%' ", stockName);
            if (beginDate > Common.DefaultValue.DefaultTime && endDate > beginDate)
                sb.AppendFormat(" and sto.StockDate between '{0}' and '{1}' ", beginDate.ToString(), endDate.AddDays(1).ToString());

            if (!string.IsNullOrEmpty(stockIds))
                sb.AppendFormat(" and sto.StockId in ({0})",stockIds);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetContractOutStockSelect(int pageIndex, int pageSize, string orderStr, int subId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("sto.StockId,sod.DetailId,sod.GrossAmount,sod.NetAmount,sod.Bundles,sto.StockDate,sto.CardNo,sto.AssetId,ass.AssetName");
            sb.Append(",sto.BrandId,bra.BrandName,sto.CustomsType,ct.DetailName as CustomsTypeName,sto.UintId,mu.MUName,sn.RefNo");
            sb.Append(",sto.StockStatus,ss.StatusName");
            select.ColumnName = sb.ToString();

            int readyStatus = (int)Common.StatusEnum.已生效;

            sb.Clear();
            sb.Append(" dbo.St_StockOutApplyDetail sod  ");
            sb.Append(" inner join dbo.St_Stock sto on sto.StockId = sod.StockId ");
            sb.Append(" inner join dbo.St_StockName sn on sto.StockNameId = sn.StockNameId ");

            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sto.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on sto.BrandId = bra.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on sto.DeliverPlaceId = dp.DPId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on sto.UintId= mu.MUId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail ct on ct.StyleDetailId = sto.CustomsType ");
            sb.Append("  left join NFMT_Basic.dbo.BDStatusDetail ss on ss.DetailId = sto.StockStatus ");          
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" sod.DetailStatus>={0} and sod.SubContractId = {1} ", readyStatus,subId);
            select.WhereStr = sb.ToString();

            return select;
        }
        #endregion

        #region report

        public SelectModel GetStockLogReportSelect(int pageIndex, int pageSize, string orderStr,string refNo,int logType,int customsType,int assetId,DateTime logStartDate,DateTime logEndDate)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sl.StockLogId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)Common.StatusEnum.已生效;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            select.ColumnName = " sl.StockLogId,sl.LogDate,sl.LogType,lt.DetailName as LogTypeName,sn.RefNo,sto.PaperNo,ass.AssetName,bra.BrandName,sto.CustomsType,cus.DetailName as CustomsTypeName,sto.DeliverPlaceId,dp.DPName,sl.GrossAmount,sl.NetAmount,mu.MUName,outCorp.CorpName as OutCorpName,inCorp.CorpName as InCorpName,cs.SubNo,td.DetailName as TradeDirectionName,sto.CardNo,pd.AvgPrice,pri.CurrencyId,cur.CurrencyName ";
            
            sb.Clear();
            sb.Append(" dbo.St_StockLog sl ");
            sb.Append(" inner join dbo.St_StockName sn on sl.StockNameId = sn.StockNameId ");
            sb.Append(" inner join dbo.St_Stock sto on sl.StockId = sto.StockId ");
            sb.Append(" left join dbo.Con_ContractSub cs on cs.SubId = sl.SubContractId ");
            sb.Append(" left join dbo.Con_Contract con on con.ContractId = sl.ContractId ");
            sb.Append(" left join dbo.Con_SubCorporationDetail outCorp on outCorp.SubId = cs.SubId and outCorp.IsInnerCorp = 0 and outCorp.IsDefaultCorp = 1 ");
            sb.AppendFormat(" and outCorp.DetailStatus >={0} ",readyStatus);
            sb.Append(" left join dbo.Con_SubCorporationDetail inCorp on inCorp.SubId = cs.SubId and inCorp.IsInnerCorp = 1 and inCorp.IsDefaultCorp =1 ");
            sb.AppendFormat(" and inCorp.DetailStatus >={0} ", readyStatus);
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sto.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on sto.BrandId = bra.BrandId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail lt on lt.StyleDetailId = sl.LogType ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail cus on cus.StyleDetailId = sto.CustomsType ");
            sb.Append(" left join NFMT_Basic.dbo.BDStyleDetail td on td.StyleDetailId = con.TradeDirection ");
            sb.Append(" left join NFMT.dbo.Pri_PricingDetail pd on pd.StockLogId = sl.StockLogId ");
            sb.Append(" left join NFMT.dbo.Pri_Pricing pri on pri.PricingId = pd.PricingId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = pri.CurrencyId ");

            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(refNo))
                sb.AppendFormat(" and sn.RefNo like '%{0}%' ",refNo);
            if (logType > 0)
                sb.AppendFormat(" and sl.LogType = {0} ",logType);
            if (customsType > 0)
                sb.AppendFormat(" and sto.CustomsType = {0} ",customsType);
            if (assetId > 0)
                sb.AppendFormat(" and sto.AssetId = {0} ",assetId);
            if (logStartDate > Common.DefaultValue.DefaultTime && logEndDate > logStartDate)
                sb.AppendFormat(" and sl.LogDate between '{0}' and '{1}' ",logStartDate.ToString(),logEndDate.ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        //public override string[,] SetExcelRangeData(DataTable source)
        //{
        //    string[,] objData = new string[source.Rows.Count, 18];

        //    for (int i = 0; i < source.Rows.Count; i++)
        //    {
        //        System.Data.DataRow dr = source.Rows[i];

        //        objData[i, 0] = ((DateTime)dr["LogDate"]).ToString("yyyy-MM-dd");
        //        objData[i, 1] = dr["LogTypeName"].ToString();
        //        objData[i, 2] = dr["RefNo"].ToString();
        //        objData[i, 3] = dr["PaperNo"].ToString();
        //        objData[i, 4] = dr["AssetName"].ToString();
        //        objData[i, 5] = dr["BrandName"].ToString();
        //        objData[i, 6] = dr["CardNo"].ToString();
        //        objData[i, 7] = dr["CustomsTypeName"].ToString();
        //        objData[i, 8] = dr["DPName"].ToString();
        //        objData[i, 9] = dr["GrossAmount"].ToString();
        //        objData[i, 10] = dr["NetAmount"].ToString();
        //        objData[i, 11] = dr["MUName"].ToString();
        //        objData[i, 12] = dr["InCorpName"].ToString();
        //        objData[i, 13] = dr["OutCorpName"].ToString();
        //        objData[i, 14] = dr["SubNo"].ToString();
        //        objData[i, 15] = dr["TradeDirectionName"].ToString();
        //        objData[i, 16] = dr["AvgPrice"].ToString();
        //        objData[i, 17] = dr["CurrencyName"].ToString();
        //    }

        //    return objData;
        //}

        public override DataTable SetExcelRangeData(DataTable source)
        {
            string[] strs = new string[] { "LogDate", "LogTypeName", "RefNo", "PaperNo", "AssetName", "BrandName", "CardNo", "CustomsTypeName", "DPName", "GrossAmount", "NetAmount", "MUName", "InCorpName", "OutCorpName", "SubNo", "TradeDirectionName", "AvgPrice", "CurrencyName" };

            return source.ConvertDataTable(strs);
        }

        #endregion

        #region 采购合约审核生效同时操作库存流水

        public ResultModel ContractInAuditStockLogOperate(UserModel user,int contractId, int subId)
        {
            ResultModel result = new ResultModel();

            DAL.ContractStockInDAL contractStockInDAL = new ContractStockInDAL();
            DAL.StockInStockDAL stockInStockDAL = new StockInStockDAL();

            result = contractStockInDAL.Load(user, contractId);
            if (result.ResultStatus != 0)
                return result;

            List<NFMT.WareHouse.Model.ContractStockIn> contractStockIns = result.ReturnValue as List<NFMT.WareHouse.Model.ContractStockIn>;
            if (contractStockIns == null)
            {
                result.Message = "合约入库分配获取失败";
                result.ResultStatus = -1;
                return result;
            }

            foreach (NFMT.WareHouse.Model.ContractStockIn contractStcokIn in contractStockIns)
            {
                //获取入库登记库存关联
                result = stockInStockDAL.GetByStockIn(NFMT.Common.DefaultValue.SysUser, contractStcokIn.StockInId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.StockInStock stockInStock = result.ReturnValue as NFMT.WareHouse.Model.StockInStock;
                if (stockInStock == null || stockInStock.RefId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取入库登记与库存关联失败";
                    return result;
                }

                //获取库存流水
                result = this.stocklogDAL.Get(user, stockInStock.StockLogId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.StockLog stockLog = result.ReturnValue as NFMT.WareHouse.Model.StockLog;
                if (stockLog == null || stockLog.StockLogId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取入库流水失败";
                    return result;
                }

                //判断库存流水是否关联合约
                if (stockLog.ContractId <= 0)
                {
                    stockLog.ContractId = contractId;
                    stockLog.SubContractId = subId;

                    result = this.stocklogDAL.Update(user, stockLog);
                    if (result.ResultStatus != 0)
                        return result;
                }
            }

            return result;
        }

        #endregion

    }
}
