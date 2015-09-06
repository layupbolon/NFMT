/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SplitDocBLL.cs
// 文件功能描述：拆单dbo.St_SplitDoc业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年10月27日
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
    /// 拆单dbo.St_SplitDoc业务逻辑类。
    /// </summary>
    public class SplitDocBLL : Common.ExecBLL
    {
        private SplitDocDAL splitdocDAL = new SplitDocDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(SplitDocDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SplitDocBLL()
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
            get { return this.splitdocDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int person, DateTime fromDate, DateTime toDate, string refNo, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sp.SplitDocId desc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)NFMT.Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" sp.SplitDocId,e.Name,sp.SplitDocTime,sp.SplitDocStatus,bd.StatusName,sp.OldRefNo  ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_SplitDoc sp ");
            sb.Append(" left join NFMT_User.dbo.Employee e on sp.Spliter = e.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on sp.SplitDocStatus = bd.DetailId and bd.StatusId = {0} ", statusId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (person > 0)
                sb.AppendFormat(" and sp.Spliter = {0} ", person);
            if (status > 0)
                sb.AppendFormat(" and sp.SplitDocStatus = {0} ", status);
            if (!string.IsNullOrEmpty(refNo))
                sb.AppendFormat(" and sp.OldRefNo like '%{0}%' ", refNo);
            if (fromDate > Common.DefaultValue.DefaultTime && toDate >= fromDate)
                sb.AppendFormat(" and sp.SplitDocTime between '{0}' and '{1}' ", fromDate.ToString(), toDate.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Create(UserModel user, int stockId, List<Model.SplitDocDetail> details)
        {
            ResultModel result = new ResultModel();
            DAL.StockDAL stockDAL = new StockDAL();
            DAL.StockNameDAL stockNameDAL = new StockNameDAL();
            DAL.SplitDocDetailDAL splitDocDetailDAL = new SplitDocDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取库存信息
                    result = stockDAL.Get(user, stockId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Stock stock = result.ReturnValue as Model.Stock;
                    if (stock == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取数据失败";
                        return result;
                    }

                    //获取业务单号信息
                    result = stockNameDAL.Get(user, stock.StockNameId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.StockName stockName = result.ReturnValue as Model.StockName;
                    if (stockName == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取数据失败";
                        return result;
                    }

                    //写入拆单主表
                    result = splitdocDAL.Insert(user, new SplitDoc()
                    {
                        Spliter = user.EmpId,
                        SplitDocTime = DateTime.Now,
                        SplitDocStatus = StatusEnum.已录入,
                        OldRefNoId = stock.StockNameId,
                        OldRefNo = stockName.RefNo,
                        OldStockId = stock.StockId
                    });
                    if (result.ResultStatus != 0)
                        return result;

                    int splitDocId = (int)result.ReturnValue;

                    foreach (Model.SplitDocDetail detail in details)
                    {
                        detail.SplitDocId = splitDocId;
                        detail.DetailStatus = StatusEnum.已生效;
                        detail.OldRefNoId = stock.StockNameId;
                        detail.OldStockId = stock.StockId;

                        result = splitDocDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = splitDocId;

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

        public ResultModel Update(UserModel user, int splitDocId, int stockId, List<Model.SplitDocDetail> details)
        {
            ResultModel result = new ResultModel();
            DAL.SplitDocDetailDAL splitDocDetailDAL = new SplitDocDetailDAL();
            DAL.StockDAL stockDAL = new StockDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取拆单信息
                    result = splitdocDAL.Get(user, splitDocId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.SplitDoc splitDoc = result.ReturnValue as Model.SplitDoc;
                    if (splitDoc == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取拆单信息失败";
                        return result;
                    }
                    splitDoc.Spliter = user.EmpId;
                    splitDoc.SplitDocTime = DateTime.Now;

                    //更新拆单信息
                    result = splitdocDAL.Update(user, splitDoc);
                    if (result.ResultStatus != 0)
                        return result;

                    //加载拆单明细
                    result = splitDocDetailDAL.Load(user, splitDocId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.SplitDocDetail> splitDocDetails = result.ReturnValue as List<Model.SplitDocDetail>;
                    if (splitDocDetails == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取拆单明细失败";
                        return result;
                    }

                    //作废原有的拆单明细
                    foreach (Model.SplitDocDetail detail in splitDocDetails)
                    {
                        if (detail.DetailStatus == Common.StatusEnum.已生效)
                            detail.DetailStatus = Common.StatusEnum.已录入;
                        result = splitDocDetailDAL.Invalid(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取库存信息
                    result = stockDAL.Get(user, stockId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Stock stock = result.ReturnValue as Model.Stock;
                    if (stock == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取数据失败";
                        return result;
                    }

                    foreach (Model.SplitDocDetail detail in details)
                    {
                        detail.SplitDocId = splitDocId;
                        detail.DetailStatus = StatusEnum.已生效;
                        detail.OldRefNoId = stock.StockNameId;
                        detail.OldStockId = stock.StockId;

                        result = splitDocDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }
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

        public ResultModel GoBack(UserModel user, int splitDocId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = splitdocDAL.Get(user, splitDocId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.SplitDoc solitDoc = result.ReturnValue as Model.SplitDoc;
                    if (solitDoc == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取数据出错";
                        return result;
                    }

                    result = splitdocDAL.Goback(user, solitDoc);
                    if (result.ResultStatus != 0)
                        return result;

                    //工作流任务关闭
                    WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, solitDoc);
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

        public ResultModel Invalid(UserModel user, int splitDocId)
        {
            ResultModel result = new ResultModel();
            DAL.SplitDocDetailDAL splitDocDetailDAL = new SplitDocDetailDAL();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = splitdocDAL.Get(user, splitDocId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.SplitDoc solitDoc = result.ReturnValue as Model.SplitDoc;
                    if (solitDoc == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取数据出错";
                        return result;
                    }

                    result = splitdocDAL.Invalid(user, solitDoc);
                    if (result.ResultStatus != 0)
                        return result;

                    result = splitDocDetailDAL.Load(user, splitDocId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.SplitDocDetail> details = result.ReturnValue as List<Model.SplitDocDetail>;
                    if (details == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取数据出错";
                        return result;
                    }

                    foreach (Model.SplitDocDetail detail in details)
                    {
                        if (detail.DetailStatus == Common.StatusEnum.已生效)
                            detail.DetailStatus = Common.StatusEnum.已录入;

                        result = splitDocDetailDAL.Invalid(user, detail);
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

        public ResultModel Complete(UserModel user, int splitDocId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                DAL.StockDAL stockDAL = new StockDAL();
                Model.StockLog stockLog = null;
                Model.Stock stock = null;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取拆单
                    result = this.splitdocDAL.Get(user, splitDocId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.SplitDoc splitDoc = result.ReturnValue as SplitDoc;
                    if (splitDoc == null)
                    {
                        result.Message = "该数据不存在，不能完成";
                        return result;
                    }

                    //拆单完成
                    result = splitdocDAL.Complete(user, splitDoc);
                    if (result.ResultStatus != 0)
                        return result;

                    #region 原库存操作

                    //获取流水
                    result = stockLogDAL.Get(user, splitDoc.StockLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    stockLog = result.ReturnValue as Model.StockLog;
                    if (stockLog == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    //流水完成
                    result = stockLogDAL.Complete(user, stockLog);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取库存
                    result = stockDAL.Get(user, splitDoc.OldStockId);
                    if (result.ResultStatus != 0)
                        return result;

                    stock = result.ReturnValue as Model.Stock;
                    if (stock == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    //修改库存状态至 已拆库存
                    result = stockDAL.UpdateStockStatusDirect(stock, StockStatusEnum.已拆库存);
                    if (result.ResultStatus != 0)
                        return result;

                    #endregion

                    #region 明细库存操作

                    //获取所有已生效的明细
                    DAL.SplitDocDetailDAL splitDocDetailDAL = new SplitDocDetailDAL();
                    result = splitDocDetailDAL.Load(user, splitDocId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.SplitDocDetail> details = result.ReturnValue as List<Model.SplitDocDetail>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    foreach (Model.SplitDocDetail detail in details)
                    {
                        //明细完成
                        result = splitDocDetailDAL.Complete(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取拆单流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "拆单流水获取失败";
                            return result;
                        }

                        //完成拆单流水
                        result = stockLogDAL.Complete(user, stockLog);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取库存
                        result = stockDAL.Get(user, stockLog.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        stock = result.ReturnValue as Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存获取失败";
                            return result;
                        }

                        result = stockDAL.UpdateStockStatusDirect(stock, StockStatusEnum.在库正常);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    #endregion

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

        public ResultModel CompleteCancel(UserModel user, int splitDocId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                DAL.StockDAL stockDAL = new StockDAL();
                Model.StockLog stockLog = null;
                Model.Stock stock = null;

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取拆单
                    result = this.splitdocDAL.Get(user, splitDocId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.SplitDoc splitDoc = result.ReturnValue as SplitDoc;
                    if (splitDoc == null)
                    {
                        result.Message = "该数据不存在，不能完成";
                        result.ResultStatus = -1;
                        return result;
                    }

                    //完成撤销
                    result = splitdocDAL.CompleteCancel(user, splitDoc);
                    if (result.ResultStatus != 0)
                        return result;

                    #region 原库存操作

                    //获取流水
                    result = stockLogDAL.Get(user, splitDoc.StockLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    stockLog = result.ReturnValue as Model.StockLog;
                    if (stockLog == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    //流水完成撤销
                    result = stockLogDAL.CompleteCancel(user, stockLog);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取库存
                    result = stockDAL.Get(user, splitDoc.OldStockId);
                    if (result.ResultStatus != 0)
                        return result;

                    stock = result.ReturnValue as Model.Stock;
                    if (stock == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    //修改库存状态至 预拆库存
                    result = stockDAL.UpdateStockStatusDirect(stock, StockStatusEnum.预拆库存);
                    if (result.ResultStatus != 0)
                        return result;

                    #endregion

                    #region 明细库存操作

                    //获取所有已生效的明细
                    DAL.SplitDocDetailDAL splitDocDetailDAL = new SplitDocDetailDAL();
                    result = splitDocDetailDAL.Load(user, splitDocId, StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.SplitDocDetail> details = result.ReturnValue as List<Model.SplitDocDetail>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    foreach (Model.SplitDocDetail detail in details)
                    {
                        //明细完成撤销
                        result = splitDocDetailDAL.CompleteCancel(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取拆单流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "拆单流水获取失败";
                            return result;
                        }

                        //完成拆单流水
                        result = stockLogDAL.CompleteCancel(user, stockLog);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取库存
                        result = stockDAL.Get(user, stockLog.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        stock = result.ReturnValue as Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存获取失败";
                            return result;
                        }

                        result = stockDAL.UpdateStockStatusDirect(stock, StockStatusEnum.新拆库存);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    #endregion

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

        public ResultModel Audit(UserModel user, NFMT.WorkFlow.Model.DataSource dataSource, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.splitdocDAL.Get(user, dataSource.RowId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.SplitDoc splitDoc = result.ReturnValue as Model.SplitDoc;
                    if (splitDoc == null || splitDoc.SplitDocId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "拆单不存在";
                        return result;
                    }

                    //审核，修改数据状态
                    result = this.splitdocDAL.Audit(user, splitDoc, isPass);
                    if (result.ResultStatus != 0)
                        return result;

                    //审核通过
                    if (isPass)
                    {
                        DAL.StockDAL stockDAL = new StockDAL();
                        DAL.StockNameDAL stockNameDAL = new StockNameDAL();
                        DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                        Model.Stock oldStock = null;
                        Model.Stock stock = null;
                        Model.StockLog stockLog = null;
                        
                        #region 获取最新的原库存流水

                        result = stockLogDAL.GetLastStockLogByStockId(user, splitDoc.OldStockId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog lastOldStockLog = result.ReturnValue as Model.StockLog;

                        lastOldStockLog.LogStatus = StatusEnum.已作废;
                        result = stockLogDAL.Update(user, lastOldStockLog);
                        if (result.ResultStatus != 0)
                            return result;

                        #endregion

                        int stockLogId = 0;
                        
                        //获取库存
                        result = stockDAL.Get(user, splitDoc.OldStockId);
                        if (result.ResultStatus != 0)
                            return result;

                        oldStock = result.ReturnValue as Model.Stock;
                        if (oldStock == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取库存失败";
                            return result;
                        }

                        ////创建库存流水
                        //stockLog = new StockLog()
                        //{
                        //    StockId = oldStock.StockId,
                        //    StockNameId = oldStock.StockNameId,
                        //    RefNo = splitDoc.OldRefNo,
                        //    LogDirection = (int)LogDirectionEnum.In,
                        //    LogType = (int)LogTypeEnum.拆单,
                        //    ContractId =lastOldStockLog.ContractId,
                        //    SubContractId = lastOldStockLog.SubContractId,
                        //    LogDate = DateTime.Now,
                        //    OpPerson = user.EmpId,
                        //    Bundles = oldStock.Bundles,
                        //    GrossAmount = oldStock.GrossAmount,
                        //    NetAmount = oldStock.NetAmount,
                        //    MUId = oldStock.UintId,
                        //    BrandId = oldStock.BrandId,
                        //    DeliverPlaceId = oldStock.DeliverPlaceId,
                        //    PaperNo = oldStock.PaperNo,
                        //    PaperHolder = oldStock.PaperHolder,
                        //    CardNo = oldStock.CardNo,
                        //    Memo = oldStock.Memo,
                        //    LogStatus = StatusEnum.已生效,
                        //    LogSourceBase = "NFMT",
                        //    LogSource = "dbo.St_SplitDoc",
                        //    SourceId = splitDoc.SplitDocId
                        //};

                        //result = stockLogDAL.Insert(user, stockLog);
                        //if (result.ResultStatus != 0)
                        //    return result;

                        //stockLogId = 0;
                        //if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out stockLogId) || stockLogId == 0)
                        //{
                        //    result.ResultStatus = -1;
                        //    result.Message = "流水添加失败";
                        //    return result;
                        //}

                        ////将库存流水Id反向更新到拆单主表中
                        //splitDoc.StockLogId = stockLogId;
                        //splitDoc.SplitDocStatus = StatusEnum.已生效;
                        //result = splitdocDAL.Update(user, splitDoc);
                        //if (result.ResultStatus != 0)
                        //    return result;

                        //更新库存状态
                        result = stockDAL.UpdateStockStatus(oldStock, StockStatusEnum.预拆库存);
                        if (result.ResultStatus != 0)
                            return result;

                        #region 写明细库存流水

                        //加载已生效明细
                        DAL.SplitDocDetailDAL splitDocDetailDAL = new SplitDocDetailDAL();
                        result = splitDocDetailDAL.Load(user, splitDoc.SplitDocId, StatusEnum.已生效);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.SplitDocDetail> details = result.ReturnValue as List<Model.SplitDocDetail>;
                        if (details == null || !details.Any())
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取失败";
                            return result;
                        }

                        foreach (Model.SplitDocDetail detail in details)
                        {
                            result = stockNameDAL.Insert(user, new Model.StockName()
                            {
                                RefNo = detail.NewRefNo
                            });

                            if (result.ResultStatus != 0)
                                return result;

                            int newStockNameId = (int)result.ReturnValue;

                            //创建库存
                            stock = new Stock()
                            {
                                StockNameId = newStockNameId,
                                //StockNo
                                StockDate = DateTime.Now,
                                AssetId = detail.AssetId,
                                Bundles = detail.Bundles,
                                GrossAmount = detail.GrossAmount,
                                NetAmount = detail.NetAmount,
                                //ReceiptInGap 
                                //ReceiptOutGap
                                CurGrossAmount = detail.GrossAmount,
                                CurNetAmount = detail.NetAmount,
                                UintId = detail.UnitId,
                                DeliverPlaceId = oldStock.DeliverPlaceId,
                                BrandId = detail.BrandId,
                                CustomsType = oldStock.CustomsType,
                                GroupId = oldStock.GroupId,
                                CorpId = oldStock.CorpId,
                                DeptId = oldStock.DeptId,
                                ProducerId = oldStock.ProducerId,
                                PaperNo = detail.PaperNo,
                                PaperHolder = detail.PaperHolder,
                                PreStatus = StockStatusEnum.新拆库存,
                                StockStatus = StockStatusEnum.新拆库存,
                                CardNo = detail.CardNo,
                                Memo = detail.Memo,
                                StockType = oldStock.StockType
                            };

                            result = stockDAL.Insert(user, stock);
                            if (result.ResultStatus != 0)
                                return result;

                            int stockId = (int)result.ReturnValue;

                            //创建库存流水
                            stockLog = new StockLog()
                            {
                                StockId = stockId,
                                StockNameId = newStockNameId,
                                RefNo = detail.NewRefNo,
                                LogDirection = (int)LogDirectionEnum.In,
                                LogType = (int)LogTypeEnum.拆单,
                                ContractId =lastOldStockLog.ContractId,
                                SubContractId = lastOldStockLog.SubContractId,
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
                                LogSource = "dbo.St_SplitDocDetail",
                                SourceId = detail.DetailId
                            };

                            result = stockLogDAL.Insert(user, stockLog);
                            if (result.ResultStatus != 0)
                                return result;

                            stockLogId = 0;
                            if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out stockLogId) || stockLogId == 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "流水添加失败";
                                return result;
                            }

                            //将库存流水Id反向更新到明细表中
                            detail.StockLogId = stockLogId;
                            result = splitDocDetailDAL.Update(user, detail);
                            if (result.ResultStatus != 0)
                                return result;
                        }

                        #endregion
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

        public ResultModel Close(UserModel user, int splitDocId)
        {
            ResultModel result = new ResultModel();

            try
            {
                DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                DAL.StockDAL stockDAL = new StockDAL();
                Model.StockLog stockLog = null;
                Model.Stock stock = null;

                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取拆单
                    result = this.splitdocDAL.Get(user, splitDocId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.SplitDoc splitDoc = result.ReturnValue as Model.SplitDoc;
                    if (splitDoc == null || splitDoc.SplitDocId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "拆单不存在";
                        return result;
                    }

                    //拆单关闭
                    result = this.splitdocDAL.Close(user, splitDoc);
                    if (result.ResultStatus != 0)
                        return result;

                    #region 原库存操作

                    //获取流水
                    result = stockLogDAL.Get(user, splitDoc.StockLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    stockLog = result.ReturnValue as Model.StockLog;
                    if (stockLog == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    //流水关闭
                    result = stockLogDAL.Close(user, stockLog);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取库存
                    result = stockDAL.Get(user, splitDoc.OldStockId);
                    if (result.ResultStatus != 0)
                        return result;

                    stock = result.ReturnValue as Model.Stock;
                    if (stock == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    //原库存状态更新至前一状态，如前一状态不存在则更新至在库正常
                    result = stockDAL.UpdateStockStatusToPrevious(user, stock, StockStatusEnum.在库正常);
                    if (result.ResultStatus != 0)
                        return result;

                    #endregion

                    #region 明细库存操作

                    //获取所有已生效的明细
                    DAL.SplitDocDetailDAL splitDocDetailDAL = new SplitDocDetailDAL();
                    result = splitDocDetailDAL.Load(user, splitDocId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.SplitDocDetail> details = result.ReturnValue as List<Model.SplitDocDetail>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    foreach (Model.SplitDocDetail detail in details)
                    {
                        //明细关闭
                        result = splitDocDetailDAL.Close(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取拆单流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "拆单流水获取失败";
                            return result;
                        }

                        //关闭拆单流水
                        result = stockLogDAL.Close(user, stockLog);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取库存
                        result = stockDAL.Get(user, stockLog.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        stock = result.ReturnValue as Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存获取失败";
                            return result;
                        }

                        result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.作废库存);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    #endregion

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
