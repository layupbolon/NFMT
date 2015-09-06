/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoBLL.cs
// 文件功能描述：回购dbo.Repo业务逻辑类。
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
    /// 回购dbo.Repo业务逻辑类。
    /// </summary>
    public class RepoBLL : Common.ExecBLL
    {
        private RepoDAL repoDAL = new RepoDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(RepoDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepoBLL()
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
            get { return this.repoDAL; }
        }
        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int status, int repoer)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "r.RepoId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" r.RepoId,e.Name,r.RepoerTime,r.RepoStatus,bd.StatusName,r.Memo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT.dbo.St_Repo r ");
            sb.Append(" left join NFMT_User.dbo.Employee e on r.Repoer = e.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on r.RepoStatus = bd.DetailId and StatusId = {0} ", statusId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (status > 0)
                sb.AppendFormat(" and r.RepoStatus = {0} ", status);
            if (repoer > 0)
                sb.AppendFormat(" and r.Repoer = {0} ", repoer);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel RepoUpdateHandle(UserModel user, int repoId, string sids, string memo)
        {
            ResultModel result = new ResultModel();
            DAL.RepoDetailDAL repoDetailDAL = new RepoDetailDAL();
            NFMT.WareHouse.DAL.StockDAL stockDAL = new StockDAL();
            NFMT.WareHouse.Model.Stock stock = new Stock();
            NFMT.WareHouse.DAL.RepoApplyDetailDAL repoApplyDetailDAL = new RepoApplyDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = repoDAL.Get(user, repoId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Repo repo = result.ReturnValue as Model.Repo;
                    if (repo == null)
                    {
                        result.ResultStatus = -1;
                        return result;
                    }

                    repo.Memo = memo;

                    //更新回购
                    result = repoDAL.Update(user, repo);
                    if (result.ResultStatus != 0)
                        return result;

                    //作废所有该回购下的明细
                    result = repoDetailDAL.InvalidAll(user, repoId);
                    if (result.ResultStatus != 0)
                        return result;

                    string[] splits = sids.Split(',');
                    if (splits != null && splits.Length > 0)
                    {
                        foreach (string str in splits)
                        {
                            //获取库存信息
                            result = stockDAL.Get(user, Convert.ToInt32(str));
                            if (result.ResultStatus != 0)
                                return result;

                            stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                            if (stock == null)
                            {
                                result.ResultStatus = -1;
                                return result;
                            }

                            result = repoApplyDetailDAL.GetDetailId(user, repo.RepoApplyId, Convert.ToInt32(str));
                            if (result.ResultStatus != 0)
                                return result;

                            int detailId = (int)result.ReturnValue;

                            //写入质押明细表
                            result = repoDetailDAL.Insert(user, new RepoDetail()
                            {
                                RepoId = repoId,
                                RepoApplyDetailId = detailId,
                                StockId = Convert.ToInt32(str),
                                RepoWeight = stock.GrossAmount,
                                Unit = stock.UintId
                            });
                            if (result.ResultStatus != 0)
                                return result;
                        }
                        scope.Complete();
                    }
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

        public ResultModel RepoCreateHandle(UserModel user, int repoApplyId, string memo, string sids)
        {
            ResultModel result = new ResultModel();
            if (string.IsNullOrEmpty(sids))
            {
                result.Message = "参数错误";
                return result;
            }

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //写入质押表
                    result = repoDAL.Insert(user, new Model.Repo()
                    {
                        RepoApplyId = repoApplyId,
                        Repoer = user.EmpId,
                        RepoerTime = DateTime.Now,
                        Memo = memo
                    });
                    if (result.ResultStatus != 0)
                        return result;

                    int RepoId = (int)result.ReturnValue;

                    string[] splits = sids.Split(',');
                    if (splits != null && splits.Length > 0)
                    {
                        NFMT.WareHouse.DAL.StockDAL stockDAL = new StockDAL();
                        NFMT.WareHouse.Model.Stock stock = new Stock();
                        NFMT.WareHouse.DAL.RepoDetailDAL repoDetailDAL = new RepoDetailDAL();
                        NFMT.WareHouse.DAL.RepoApplyDetailDAL repoApplyDetailDAL = new RepoApplyDetailDAL();
                        foreach (string str in splits)
                        {
                            //获取库存信息
                            result = stockDAL.Get(user, Convert.ToInt32(str));
                            if (result.ResultStatus != 0)
                                return result;

                            stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                            if (stock == null)
                            {
                                result.ResultStatus = -1;
                                return result;
                            }

                            result = repoApplyDetailDAL.GetDetailId(user, repoApplyId, Convert.ToInt32(str));
                            if (result.ResultStatus != 0)
                                return result;

                            int detailId = (int)result.ReturnValue;

                            //写入质押明细表
                            result = repoDetailDAL.Insert(user, new RepoDetail()
                            {
                                RepoId = RepoId,
                                RepoApplyDetailId = detailId,
                                StockId = Convert.ToInt32(str),
                                RepoWeight = stock.GrossAmount,
                                Unit = stock.UintId
                            });
                            if (result.ResultStatus != 0)
                                return result;
                        }
                        scope.Complete();
                    }
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

        public ResultModel GoBack(UserModel user, int repoId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证回购
                    result = repoDAL.Get(user, repoId);
                    if (result.ResultStatus != 0)
                        return result;

                    Repo resultObj = result.ReturnValue as Repo;

                    if (resultObj == null || resultObj.RepoId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能撤返";
                        return result;
                    }

                    //撤返回购
                    result = repoDAL.Goback(user, resultObj);
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

        public ResultModel Invalid(UserModel user, int repoId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = repoDAL.Get(user, repoId);
                    if (result.ResultStatus != 0)
                        return result;

                    Repo resultObj = result.ReturnValue as Repo;

                    if (resultObj == null || resultObj.RepoId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能作废";
                        return result;
                    }
                    if (resultObj.Status < StatusEnum.已录入 || resultObj.Status > StatusEnum.审核拒绝)
                    {
                        result.ResultStatus = -1;
                        result.Message = "数据不允许作废";
                        return result;
                    }

                    result = repoDAL.Invalid(user, resultObj);
                    if (result.ResultStatus != 0)
                        return result;

                    RepoDetailDAL repoDetailDAL = new RepoDetailDAL();
                    result = repoDetailDAL.InvalidAll(user, resultObj.RepoId);
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
                    log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Complete(UserModel user, int repoId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.repoDAL.Get(user, repoId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Repo Repo = result.ReturnValue as Repo;
                    if (Repo == null)
                    {
                        result.Message = "该数据不存在，不能完成";
                        return result;
                    }

                    result = repoDAL.Complete(user, Repo);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取所有已生效的明细
                    DAL.RepoDetailDAL repoDetailDAL = new RepoDetailDAL();
                    result = repoDetailDAL.Load(user, repoId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.RepoDetail> details = result.ReturnValue as List<Model.RepoDetail>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                    DAL.StockDAL stockDAL = new StockDAL();

                    foreach (Model.RepoDetail detail in details)
                    {
                        //明细完成
                        result = repoDetailDAL.Complete(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取回购流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "回购流水获取失败";
                            return result;
                        }

                        //完成回购流水
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

                        result = stockDAL.UpdateStockStatusDirect(stock, StockStatusEnum.在库正常);
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

        public ResultModel CompleteCancel(UserModel user, int repoId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.repoDAL.Get(user, repoId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Repo repo = result.ReturnValue as Repo;
                    if (repo == null)
                    {
                        result.Message = "该数据不存在，不能完成";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = repoDAL.CompleteCancel(user, repo);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取所有已生效的明细
                    DAL.RepoDetailDAL repoDetailDAL = new RepoDetailDAL();
                    result = repoDetailDAL.Load(user, repoId, StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.RepoDetail> details = result.ReturnValue as List<Model.RepoDetail>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                    DAL.StockDAL stockDAL = new StockDAL();

                    foreach (Model.RepoDetail detail in details)
                    {
                        //明细完成
                        result = repoDetailDAL.CompleteCancel(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取回购流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "回购流水获取失败";
                            return result;
                        }

                        //完成回购流水
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

                        result = stockDAL.UpdateStockStatusDirect(stock, StockStatusEnum.预回购库存);
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

        public SelectModel GetSelectModelForApplyDetail(int pageIndex, int pageSize, string orderStr, int repoApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "r.RepoId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" r.RepoId,e.Name,r.RepoerTime,r.RepoStatus,bd.StatusName,r.Memo ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT.dbo.St_Repo r ");
            sb.Append(" left join NFMT_User.dbo.Employee e on r.Repoer = e.EmpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on r.RepoStatus = bd.DetailId and StatusId = {0} ", statusId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (repoApplyId > 0)
                sb.AppendFormat(" and r.RepoApplyId = {0} ", repoApplyId);
            else
                sb.Append(" and 1=2 ");

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Audit(UserModel user, NFMT.WorkFlow.Model.DataSource dataSource, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.repoDAL.Get(user, dataSource.RowId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Repo repo = result.ReturnValue as Model.Repo;
                    if (repo == null || repo.RepoId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "回购不存在";
                        return result;
                    }

                    //审核，修改数据状态
                    result = this.repoDAL.Audit(user, repo, isPass);
                    if (result.ResultStatus != 0)
                        return result;

                    //审核通过
                    if (isPass)
                    {
                        //加载已生效明细
                        DAL.RepoDetailDAL repoDetailDAL = new RepoDetailDAL();
                        result = repoDetailDAL.Load(user, repo.RepoId, StatusEnum.已生效);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.RepoDetail> details = result.ReturnValue as List<Model.RepoDetail>;
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

                        foreach (Model.RepoDetail detail in details)
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
                            result = stockNameDAL.Get(user, stock.StockNameId);
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
                                RefNo = stockName.RefNo,
                                LogDirection = (int)LogDirectionEnum.In,
                                LogType = (int)LogTypeEnum.回购,
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
                                LogSource = "dbo.St_Repo",
                                SourceId = repo.RepoId
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
                            result = repoDetailDAL.Update(user, detail);
                            if (result.ResultStatus != 0)
                                return result;

                            //更新库存状态
                            result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.预回购库存);
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

        public ResultModel Close(UserModel user, int repoId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取回购
                    result = this.repoDAL.Get(user, repoId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Repo repo = result.ReturnValue as Model.Repo;
                    if (repo == null || repo.RepoId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "回购不存在";
                        return result;
                    }

                    //回购关闭
                    result = this.repoDAL.Close(user, repo);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取所有已完成的明细
                    DAL.RepoDetailDAL repoDetailDAL = new RepoDetailDAL();
                    result = repoDetailDAL.Load(user, repoId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.RepoDetail> details = result.ReturnValue as List<Model.RepoDetail>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                    DAL.StockDAL stockDAL = new StockDAL();

                    foreach (Model.RepoDetail detail in details)
                    {
                        //明细关闭
                        result = repoDetailDAL.Close(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取回购流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "回购流水获取失败";
                            return result;
                        }

                        //完成回购流水
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

                        result = stockDAL.UpdateStockStatusToPrevious(user, stock, StockStatusEnum.质押库存);
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

        public SelectModel GetRepoStockInfoSelect(int pageIndex, int pageSize, string orderStr, int repoApplyId, string sids)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "st.StockId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " pad.DetailId,st.StockId,stn.RefNo,st.PaperNo,a.AssetName,convert(varchar,st.GrossAmount) + mu.MUName as GrossAmount,CONVERT(varchar,st.NetAmount) + mu.MUName as NetAmount,dp.DPName,bra.BrandName,st.CardNo ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_RepoApplyDetail pad ");
            sb.Append(" left join dbo.St_Stock st on pad.StockId = st.StockId ");
            sb.Append(" left join dbo.St_StockName stn on st.StockNameId = stn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on a.AssetId = st.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = st.UintId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = st.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = st.BrandId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" pad.RepoApplyId = {0} and pad.DetailStatus = {1}", repoApplyId, (int)Common.StatusEnum.已生效);

            if (string.IsNullOrEmpty(sids.Trim()))
                sb.Append(" and 1=2 ");
            else
                sb.AppendFormat(" and st.StockId in ({0}) ", sids);

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion
    }
}
