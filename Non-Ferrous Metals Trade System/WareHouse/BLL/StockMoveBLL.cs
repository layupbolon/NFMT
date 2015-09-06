/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockMoveBLL.cs
// 文件功能描述：移库dbo.StockMove业务逻辑类。
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
using System.Linq;

namespace NFMT.WareHouse.BLL
{
    /// <summary>
    /// 移库dbo.StockMove业务逻辑类。
    /// </summary>
    public class StockMoveBLL : Common.ExecBLL
    {
        private StockMoveDAL stockmoveDAL = new StockMoveDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StockMoveDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockMoveBLL()
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
            get { return this.stockmoveDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int status, int mover, int deliverPlaceId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sm.StockMoveId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" sm.StockMoveId,e.Name,sm.MoveTime,bd.StatusName,d.DPName,sm.MoveMemo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT.dbo.St_StockMove sm left join NFMT_User.dbo.Employee e on sm.Mover = e.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on sm.MoveStatus = bd.DetailId and StatusId = {0} ", statusId);
            sb.Append(" left join NFMT.dbo.St_StockMoveDetail smd on smd.StockMoveId = sm.StockMoveId ");
            sb.Append(" left join NFMT.dbo.St_Stock s on smd.StockId = s.StockId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace d on s.DeliverPlaceId = d.DPId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" smd.StockId = (select top 1 StockId from NFMT.dbo.St_StockMoveDetail detail where detail.StockMoveId = sm.StockMoveId) ");

            if (status > 0)
                sb.AppendFormat(" and sm.MoveStatus = {0} ", status);
            if (mover > 0)
                sb.AppendFormat(" and sm.Mover = {0} ", mover);
            if (deliverPlaceId > 0)
                sb.AppendFormat(" and s.DeliverPlaceId = {0} ", deliverPlaceId);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int stockMoveId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sm.StockMoveId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" sm.StockMoveId,e.Name,sm.MoveTime,bd.StatusName,d.DPName,sm.MoveMemo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT.dbo.St_StockMove sm left join NFMT_User.dbo.Employee e on sm.Mover = e.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on sm.MoveStatus = bd.DetailId and StatusId = {0} ", statusId);
            sb.Append(" left join NFMT.dbo.St_StockMoveDetail smd on smd.StockMoveId = sm.StockMoveId ");
            sb.Append(" left join NFMT.dbo.St_Stock s on smd.StockId = s.StockId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace d on s.DeliverPlaceId = d.DPId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" smd.StockId = (select top 1 StockId from NFMT.dbo.St_StockMoveDetail detail where detail.StockMoveId = sm.StockMoveId) ");
            sb.AppendFormat(" and sm.StockMoveId = {0} ", stockMoveId);
            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel StockMoveCreateHandle(UserModel user, int stockMoveApplyId, string memo)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    Model.StockMove stockMove = new StockMove()
                    {
                        StockMoveApplyId = stockMoveApplyId,
                        Mover = user.EmpId,
                        MoveTime = DateTime.Now,
                        MoveMemo = memo,
                    };

                    DAL.StockMoveDAL stockMoveDAL = new StockMoveDAL();
                    result = stockmoveDAL.Insert(user, stockMove);
                    if (result.ResultStatus != 0)
                        return result;

                    int stockMoveId = (int)result.ReturnValue;

                    DAL.StockMoveApplyDetailDAL stockMoveApplyDetailDAL = new StockMoveApplyDetailDAL();
                    result = stockMoveApplyDetailDAL.Load(user, stockMoveApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockMoveApplyDetail> details = result.ReturnValue as List<Model.StockMoveApplyDetail>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    DAL.StockMoveDetailDAL stockMoveDetailDAL = new StockMoveDetailDAL();
                    foreach (Model.StockMoveApplyDetail stockMoveApplyDetail in details)
                    {
                        result = stockMoveDetailDAL.Insert(user, new StockMoveDetail()
                        {
                            StockMoveId = stockMoveId,
                            MoveDetailStatus = StatusEnum.已生效,
                            StockId = stockMoveApplyDetail.StockId,
                            PaperNo = stockMoveApplyDetail.PaperNo,
                            DeliverPlaceId = stockMoveApplyDetail.DeliverPlaceId
                        });

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
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel StockMoveUpdateHandle(UserModel user, int stockMoveId, string memo)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAL.StockMoveDAL stockMoveDAL = new StockMoveDAL();
                    result = stockMoveDAL.Get(user, stockMoveId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockMove stockMove = result.ReturnValue as Model.StockMove;

                    stockMove.MoveMemo = memo;
                    stockMove.MoveTime = DateTime.Now;
                    stockMove.Mover = user.EmpId;

                    stockMove.Status = StatusEnum.已录入;
                    result = stockMoveDAL.Update(user, stockMove);
                    if (result.ResultStatus != 0)
                        return result;

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
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Invalid(UserModel user, int stockMoveId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, stockMoveId);
                    NFMT.WareHouse.Model.StockMove stockMove = result.ReturnValue as StockMove;

                    if (stockMove == null)
                    {
                        result.Message = "该数据不存在，不能作废";
                        return result;
                    }

                    if (stockMove.Status != StatusEnum.已录入)
                    {
                        result.Message = "非已录入状态的数据不允许作废";
                        return result;
                    }

                    result = stockmoveDAL.Invalid(user, stockMove);
                    if (result.ResultStatus != 0)
                        return result;

                    DAL.StockMoveDetailDAL stockMoveDetailDAL = new DAL.StockMoveDetailDAL();
                    result = stockMoveDetailDAL.GetDetailId(user, stockMove.StockMoveId);
                    if (result.ResultStatus != 0 || string.IsNullOrEmpty(result.ReturnValue.ToString()))
                        return result;

                    foreach (string s in result.ReturnValue.ToString().Split(','))
                    {
                        Model.StockMoveDetail detail = new StockMoveDetail() { DetailId = Convert.ToInt32(s) };
                        result = stockmoveDAL.Invalid(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
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
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel GetStockMoveIdByApplyId(UserModel user, int stockMoveApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = stockmoveDAL.GetStockMoveIdByApplyId(user, stockMoveApplyId);
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

        public ResultModel GoBack(UserModel user, int stockMoveId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = stockmoveDAL.Get(user, stockMoveId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockMove stockMove = result.ReturnValue as Model.StockMove;
                    if (stockMove == null || stockMove.StockMoveId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "数据不存在，无法撤返";
                        return result;
                    }

                    result = stockmoveDAL.Goback(user, stockMove);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WorkFlow.DAL.DataSourceDAL dataSourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = dataSourceDAL.SynchronousStatus(user, stockMove);
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

        //移库审核生效	   流水操作：写入 流水类型：移库 流水状态：已生效    库存操作：更新 库存状态：预移库存
        public ResultModel Audit(UserModel user, NFMT.WorkFlow.Model.DataSource dataSource, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.stockmoveDAL.Get(user, dataSource.RowId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockMove stockMove = result.ReturnValue as Model.StockMove;
                    if (stockMove == null || stockMove.StockMoveId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "移库不存在";
                        return result;
                    }

                    //审核，修改数据状态
                    result = this.stockmoveDAL.Audit(user, stockMove, isPass);
                    if (result.ResultStatus != 0)
                        return result;

                    //审核通过
                    if (isPass)
                    {
                        //加载已生效明细
                        DAL.StockMoveDetailDAL stockMoveDetailDAL = new StockMoveDetailDAL();
                        result = stockMoveDetailDAL.Load(user, stockMove.StockMoveId, StatusEnum.已生效);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.StockMoveDetail> details = result.ReturnValue as List<Model.StockMoveDetail>;
                        if (details == null || !details.Any())
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取失败";
                            return result;
                        }

                        DAL.StockDAL stockDAL = new StockDAL();
                        DAL.StockNameDAL stockNameDAL = new StockNameDAL();
                        DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                        Model.Stock stock = null;
                        Model.StockName stockName = null;
                        Model.StockLog stockLog = null;

                        foreach(Model.StockMoveDetail detail in details)
                        {
                            //获取库存
                            result = stockDAL.Get(user, detail.StockId);
                            if (result.ResultStatus != 0)
                                return result;

                            stock = result.ReturnValue as Model.Stock;
                            if (stock == null)
                            {
                                result.ResultStatus = -1;
                                result.Message = "获取库存失败";
                                return result;
                            }

                            //获取业务单号
                            result = stockNameDAL.Get(user,stock.StockNameId);
                            if (result.ResultStatus != 0)
                                return result;

                            stockName = result.ReturnValue as Model.StockName;
                            if (stockName == null)
                            {
                                result.ResultStatus = -1;
                                result.Message = "获取业务单号失败";
                                return result;
                            }

                            //创建库存流水
                            stockLog = new StockLog()
                            {
                                StockId = stock.StockId,
                                StockNameId = stock.StockNameId,
                                RefNo =stockName.RefNo,
                                LogDirection = (int)LogDirectionEnum.In,
                                LogType = (int)LogTypeEnum.移库,
                                //ContractId = 
                                //SubContractId = 
                                LogDate = DateTime.Now,
                                OpPerson = user.EmpId,
                                Bundles = stock.Bundles,
                                GrossAmount = stock.GrossAmount,
                                NetAmount = stock.NetAmount,
                                MUId = stock.UintId,
                                BrandId = stock.BrandId,
                                DeliverPlaceId = stock.DeliverPlaceId,
                                PaperNo = stock.PaperNo,
                                PaperHolder = stock.PaperHolder,
                                CardNo = stock.CardNo,
                                Memo = stock.Memo,
                                LogStatus = StatusEnum.已生效,
                                LogSourceBase = "NFMT",
                                LogSource = "dbo.St_StockMove",
                                SourceId = stockMove.StockMoveId
                            };

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

                            //将库存流水Id反向更新到明细表中
                            detail.StockLogId = stockLogId;
                            result = stockMoveDetailDAL.Update(user, detail);
                            if (result.ResultStatus != 0)
                                return result;

                            //更新库存状态
                            result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.预移库存);
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

        public ResultModel Complete(UserModel user, int stockMoveId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.stockmoveDAL.Get(user, stockMoveId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockMove stockmove = result.ReturnValue as StockMove;
                    if (stockmove == null)
                    {
                        result.Message = "该数据不存在，不能完成";
                        return result;
                    }

                    result = stockmoveDAL.Complete(user, stockmove);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取所有已生效的明细
                    DAL.StockMoveDetailDAL stockMoveDetailDAL = new StockMoveDetailDAL();
                    result = stockMoveDetailDAL.Load(user, stockMoveId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockMoveDetail> details = result.ReturnValue as List<Model.StockMoveDetail>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                    DAL.StockDAL stockDAL = new StockDAL();

                    foreach (Model.StockMoveDetail detail in details)
                    {
                        //明细完成
                        result = stockMoveDetailDAL.Complete(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取移库流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "移库流水获取失败";
                            return result;
                        }

                        //完成移库流水
                        result = stockLogDAL.Complete(user, stockLog);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取库存
                        result = stockDAL.Get(user, detail.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.Stock stock = result.ReturnValue as Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存获取失败";
                            return result;
                        }

                        result = stockDAL.UpdateStockStatusToPrevious(user, stock, StockStatusEnum.在库正常);
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
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        //移库完成撤销	   流水操作：更新 流水状态：已生效		     库存操作：更新 库存状态：预移库存
        public ResultModel CompleteCancel(UserModel user, int stockMoveId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    result = this.stockmoveDAL.Get(user, stockMoveId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockMove stockmove = result.ReturnValue as StockMove;
                    if (stockmove == null)
                    {
                        result.Message = "该数据不存在，不能完成";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = stockmoveDAL.CompleteCancel(user, stockmove);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取所有已生效的明细
                    DAL.StockMoveDetailDAL stockMoveDetailDAL = new StockMoveDetailDAL();
                    result = stockMoveDetailDAL.Load(user, stockMoveId, StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockMoveDetail> details = result.ReturnValue as List<Model.StockMoveDetail>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                    DAL.StockDAL stockDAL = new StockDAL();

                    foreach (Model.StockMoveDetail detail in details)
                    {
                        //明细完成
                        result = stockMoveDetailDAL.CompleteCancel(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取移库流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "移库流水获取失败";
                            return result;
                        }

                        //完成移库流水
                        result = stockLogDAL.CompleteCancel(user, stockLog);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取库存
                        result = stockDAL.Get(user, detail.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.Stock stock = result.ReturnValue as Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存获取失败";
                            return result;
                        }

                        result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.预移库存);
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

        //移库关闭	   流水操作：更新 流水状态：已关闭	  	     库存操作：更新 库存状态：更新至前一状态，如前一状态不存在则更新至在库正常
        public ResultModel Close(UserModel user, int stockMoveId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取移库
                    result = this.stockmoveDAL.Get(user, stockMoveId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.StockMove stockMove = result.ReturnValue as Model.StockMove;
                    if (stockMove == null || stockMove.StockMoveId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "移库不存在";
                        return result;
                    }

                    //移库关闭
                    result = this.stockmoveDAL.Close(user, stockMove);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取所有已完成的明细
                    DAL.StockMoveDetailDAL stockMoveDetailDAL = new StockMoveDetailDAL();
                    result = stockMoveDetailDAL.Load(user, stockMoveId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.StockMoveDetail> details = result.ReturnValue as List<Model.StockMoveDetail>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                    DAL.StockDAL stockDAL = new StockDAL();

                    foreach (Model.StockMoveDetail detail in details)
                    {
                        //明细关闭
                        result = stockMoveDetailDAL.Close(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取移库流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "移库流水获取失败";
                            return result;
                        }

                        //完成移库流水
                        result = stockLogDAL.Close(user, stockLog);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取库存
                        result = stockDAL.Get(user, detail.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.Stock stock = result.ReturnValue as Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存获取失败";
                            return result;
                        }

                        result = stockDAL.UpdateStockStatusToPrevious(user, stock, StockStatusEnum.在库正常);
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

        #endregion
    }
}
