/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeBLL.cs
// 文件功能描述：质押dbo.Pledge业务逻辑类。
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
    /// 质押dbo.Pledge业务逻辑类。
    /// </summary>
    public class PledgeBLL : Common.ExecBLL
    {
        private PledgeDAL pledgeDAL = new PledgeDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PledgeDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PledgeBLL()
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
            get { return this.pledgeDAL; }
        }

        #endregion

        #region 新增方法

        /// <summary>
        /// 数据撤返
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="pledge">Pledge对象</param>
        /// <returns></returns>
        public ResultModel GoBack(UserModel user, int pledgeId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, pledgeId);
                    if (result.ResultStatus != 0)
                        return result;
                    Model.Pledge pledge = result.ReturnValue as Pledge;

                    if (pledge == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能撤返";
                        return result;
                    }

                    if (pledge.Status != StatusEnum.待审核 && pledge.Status != StatusEnum.已生效)
                    {
                        result.ResultStatus = -1;
                        result.Message = "非待审核或已生效状态的数据不允许撤返";
                        return result;
                    }

                    //获取质押申请实体
                    DAL.PledgeApplyDAL pledgeApplyDAL = new PledgeApplyDAL();
                    result = pledgeApplyDAL.Get(user, pledge.PledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PledgeApply pledgeApply = result.ReturnValue as Model.PledgeApply;

                    //获取申请实体
                    NFMT.Operate.DAL.ApplyDAL applyDAl = new Operate.DAL.ApplyDAL();
                    result = applyDAl.Get(user, pledgeApply.ApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                    if (apply.Status == Common.StatusEnum.已关闭 || apply.Status == Common.StatusEnum.已完成)
                    {
                        result.ResultStatus = -1;
                        result.Message = "质押对应的申请已完成或已关闭，不能进行撤返操作";
                        return result;
                    }

                    result = pledgeDAL.Goback(user, pledge);
                    if (result.ResultStatus != 0)
                        return result;

                    if (pledge.Status == StatusEnum.待审核)
                    {
                        //同步工作流状态
                        NFMT.WorkFlow.BLL.DataSourceBLL dataSourceBLL = new WorkFlow.BLL.DataSourceBLL();
                        result = dataSourceBLL.SynchronousStatus(user, pledge);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    NFMT.WareHouse.DAL.PledgeDetialDAL pledgeDetialDAL = new DAL.PledgeDetialDAL();
                    result = pledgeDetialDAL.GetStockId(user, pledgeId);
                    if (result.ResultStatus != 0)
                        return result;

                    if (!string.IsNullOrEmpty(result.ReturnValue.ToString()))
                    {
                        NFMT.WareHouse.DAL.StockDAL stockDAL = new StockDAL();
                        NFMT.WareHouse.DAL.StockNameDAL stockNameDAL = new StockNameDAL();
                        NFMT.WareHouse.DAL.StockLogDAL stockLogDAL = new StockLogDAL();

                        NFMT.WareHouse.Model.StockName stockName = new StockName();
                        NFMT.WareHouse.Model.Stock stock = new Stock();
                        NFMT.WareHouse.Model.StockLog stockLog = new StockLog();

                        foreach (string s in result.ReturnValue.ToString().Split(','))
                        {
                            result = stockDAL.Get(user, Convert.ToInt32(s));
                            if (result.ResultStatus != 0)
                                return result;
                            stock = result.ReturnValue as Model.Stock;

                            result = stockNameDAL.Get(user, stock.StockNameId);
                            if (result.ResultStatus != 0)
                                return result;
                            stockName = result.ReturnValue as Model.StockName;

                            ////创建stockLog对象
                            //stockLog = new StockLog()
                            //{
                            //    StockId = stock.StockId,
                            //    StockNameId = stock.StockNameId,
                            //    RefNo = stockName.RefNo,
                            //    //LogDirection = 
                            //    LogType = LogEnum.质押冲销,
                            //    //ContractId = ,
                            //    //SubContractId = ,
                            //    LogDate = DateTime.Now,
                            //    OpPerson = user.EmpId,
                            //    Bundles = stock.Bundles,
                            //    GrossAmount = stock.GrossAmount,
                            //    NetAmount = stock.NetAmount,
                            //    MUId = stock.UintId,
                            //    BrandId = stock.BrandId,
                            //    DeliverPlaceId = stock.DeliverPlaceId,
                            //    PaperNo = stock.PaperNo,
                            //    PaperHolder = stock.PaperHolder,
                            //    CardNo = stock.CardNo,
                            //    Memo = stock.Memo,
                            //    LogStatus = StatusEnum.已生效,
                            //    LogSourceBase = "NFMT",
                            //    LogSource = "dbo.St_Pledge",
                            //    SourceId = pledgeId
                            //};

                            //result = stockLogDAL.Insert(user, stockLog);
                            //if (result.ResultStatus != 0)
                            //    return result;

                            result = stockDAL.UpdateStockStatusToPrevious(user, stock);
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

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int pledge, int status, int bank, int dept)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "p.PledgeId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" p.PledgeId,e.Name,p.PledgeTime,b.BankName,p.PledgeStatus,p.Memo,bd.StatusName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" NFMT.dbo.St_Pledge p left join NFMT_User.dbo.Employee e on p.Pledger = e.EmpId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank b on p.PledgeBank = b.BankId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on p.PledgeStatus = bd.DetailId and StatusId = {0} ", statusId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1");
            if (status > 0)
                sb.AppendFormat(" and p.PledgeStatus = {0} ", status);
            if (pledge > 0)
                sb.AppendFormat(" and p.Pledger = {0} ", pledge);
            if (bank > 0)
                sb.AppendFormat(" and p.PledgeBank = {0} ", bank);
            if (dept > 0)
                sb.AppendFormat(" and p.PledgeDept = {0} ", dept);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetPledgeStockInfoSelect(int pageIndex, int pageSize, string orderStr, int pledgeApplyId, string sids)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "st.StockId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " pad.DetailId,st.StockId,stn.RefNo,st.PaperNo,a.AssetName,convert(varchar,st.GrossAmount) + mu.MUName as GrossAmount,CONVERT(varchar,st.NetAmount) + mu.MUName as NetAmount,pad.ApplyQty,pad.UintId,pad.PledgePrice,pad.CurrencyId,cur.CurrencyName,dp.DPName,st.CardNo,bra.BrandName ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_PledgeApplyDetail pad ");
            sb.Append(" left join dbo.St_Stock st on pad.StockId = st.StockId ");
            sb.Append(" left join dbo.St_StockName stn on st.StockNameId = stn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on a.AssetId = st.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = st.UintId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = pad.CurrencyId");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = st.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = st.BrandId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" pad.PledgeApplyId = {0} and pad.DetailStatus = {1}", pledgeApplyId, (int)Common.StatusEnum.已生效);

            if (string.IsNullOrEmpty(sids.Trim()))
                sb.Append(" and 1=2 ");
            else
                sb.AppendFormat(" and st.StockId in ({0}) ", sids);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel PledgeCreateHandle(UserModel user, Model.Pledge pledge, List<Model.PledgeDetial> details)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = pledgeDAL.Insert(user, pledge);
                    if (result.ResultStatus != 0)
                        return result;

                    int pledgeId = (int)result.ReturnValue;

                    DAL.PledgeDetialDAL pledgeDetialDAL = new PledgeDetialDAL();
                    foreach (Model.PledgeDetial detail in details)
                    {
                        detail.PledgeId = pledgeId;
                        result = pledgeDetialDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (result.ResultStatus == 0)
                        result.ReturnValue = pledgeId;
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
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel PledgeUpdateHandle(UserModel user, Model.Pledge pledge, List<Model.PledgeDetial> details)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = pledgeDAL.Get(user, pledge.PledgeId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Pledge pledgeRes = result.ReturnValue as Model.Pledge;
                    if (pledgeRes == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }
                    pledgeRes.Pledger = user.EmpId;
                    pledgeRes.PledgeTime = DateTime.Now;
                    pledgeRes.PledgeBank = pledge.PledgeBank;
                    pledgeRes.Memo = pledge.Memo;

                    result = pledgeDAL.Update(user, pledgeRes);
                    if (result.ResultStatus != 0)
                        return result;

                    DAL.PledgeDetialDAL pledgeDetialDAL = new PledgeDetialDAL();
                    result = pledgeDetialDAL.InvalidAll(user, pledge.PledgeId);
                    if (result.ResultStatus != 0)
                        return result;

                    foreach (Model.PledgeDetial detail in details)
                    {
                        detail.PledgeId = pledge.PledgeId;
                        result = pledgeDetialDAL.Insert(user, detail);
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
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Invalid(UserModel user, int pledgeId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = pledgeDAL.Get(user, pledgeId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Pledge pledge = result.ReturnValue as Model.Pledge;
                    if (pledge == null)
                    {
                        result.Message = "不存在质押";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = pledgeDAL.Invalid(user, pledge);
                    if (result.ResultStatus != 0)
                        return result;

                    DAL.PledgeDetialDAL pledgeDetialDAL = new PledgeDetialDAL();
                    result = pledgeDetialDAL.GetDetailId(user, pledgeId);
                    if (result.ResultStatus != 0)
                        return result;

                    if (!string.IsNullOrEmpty(result.ReturnValue.ToString()))
                    {
                        foreach (string s in result.ReturnValue.ToString().Split(','))
                        {
                            result = pledgeDetialDAL.Invalid(user, new Model.PledgeDetial() { Id = Convert.ToInt32(s) });
                            if (result.ResultStatus != 0)
                                return result;
                        }
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

        public ResultModel PledgeHandle(UserModel user, NFMT.WorkFlow.Model.DataSource dataSource, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.pledgeDAL.Get(user, dataSource.RowId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Pledge pledge = result.ReturnValue as Model.Pledge;
                    if (pledge == null || pledge.PledgeId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "质押不存在";
                        return result;
                    }

                    //审核，修改数据状态
                    result = this.pledgeDAL.Audit(user, pledge, isPass);
                    if (result.ResultStatus != 0)
                        return result;

                    //审核通过
                    if (isPass)
                    {
                        //加载已生效明细
                        DAL.PledgeDetialDAL pledgeDetialDAL = new PledgeDetialDAL();
                        result = pledgeDetialDAL.Load(user, pledge.PledgeId, StatusEnum.已生效);
                        if (result.ResultStatus != 0)
                            return result;

                        List<Model.PledgeDetial> details = result.ReturnValue as List<Model.PledgeDetial>;
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

                        foreach (Model.PledgeDetial detail in details)
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
                                LogDirection = (int)LogDirectionEnum.Out,
                                LogType = (int)LogTypeEnum.质押,
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
                                LogSource = "dbo.St_Pledge",
                                SourceId = pledge.PledgeId
                            };

                            result = stockLogDAL.Insert(user, stockLog);
                            if (result.ResultStatus != 0)
                                return result;

                            int stockLogId = 0;
                            if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out stockLogId) || stockLogId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "流水添加失败";
                                return result;
                            }

                            //将库存流水Id反向更新到明细表中
                            detail.StockLogId = stockLogId;
                            result = pledgeDetialDAL.Update(user, detail);
                            if (result.ResultStatus != 0)
                                return result;

                            //更新库存状态
                            result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.预押库存);
                            if (result.ResultStatus != 0)
                                return result;
                        }
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

        public ResultModel Complete(UserModel user, int pledgeId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //验证质押
                    result = pledgeDAL.Get(user, pledgeId);
                    if (result.ResultStatus != 0)
                        return result;

                    Pledge pledge = result.ReturnValue as Pledge;
                    if (pledge == null || pledge.PledgeId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能进行执行完成操作";
                        return result;
                    }

                    //执行完成
                    result = pledgeDAL.Complete(user, pledge);
                    if (result.ResultStatus != 0)
                        return result;

                    //加载明细
                    PledgeDetialDAL pledgeDetialDAL = new PledgeDetialDAL();
                    result = pledgeDetialDAL.Load(user, pledge.PledgeId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.PledgeDetial> details = result.ReturnValue as List<Model.PledgeDetial>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取质押明细失败";
                        return result;
                    }

                    //加载对应质押申请
                    PledgeApplyDAL pledgeApplyDAL = new PledgeApplyDAL();
                    result = pledgeApplyDAL.Get(user, pledge.PledgeApplyId);
                    if (result.ResultStatus != 0)
                        return result;

                    PledgeApply pledgeApply = result.ReturnValue as PledgeApply;
                    if (pledgeApply == null || pledgeApply.PledgeApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "对应质押申请加载失败";
                        return result;
                    }

                    //明细执行完成
                    //更新库存状态至质押库存
                    StockDAL stockDAL = new StockDAL();
                    StockLogDAL stockLogDAL = new StockLogDAL();
                    StockNameDAL stockNameDAL = new StockNameDAL();

                    foreach (Model.PledgeDetial detail in details)
                    {
                        //明细完成
                        result = pledgeDetialDAL.Complete(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取质押流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "质押流水获取失败";
                            return result;
                        }

                        //完成质押流水
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

                        result = stockDAL.UpdateStockStatusDirect(stock, StockStatusEnum.质押库存);
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

        public SelectModel GetSelectModelByPledgeApplyId(int pageIndex, int pageSize, string orderStr, int pledgeApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "p.PledgeId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" p.PledgeId,e.Name,p.PledgeTime,b.BankName,p.Memo,bd.StatusName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" NFMT.dbo.St_Pledge p left join NFMT_User.dbo.Employee e on p.Pledger = e.EmpId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank b on p.PledgeBank = b.BankId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on p.PledgeStatus = bd.DetailId and StatusId = {0} ", statusId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" p.PledgeApplyId = {0} ", pledgeApplyId);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetPledgeSelect(int pageIndex, int pageSize, string orderStr, string sids, int pledgeApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sto.StockId asc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "sto.StockId,stn.RefNo,sto.PaperNo,a.AssetName,convert(varchar,sto.GrossAmount) + mu.MUName as GrossAmount,CONVERT(varchar,sto.NetAmount) + mu.MUName as NetAmount,sto.GrossAmount as ApplyQty ,sto.UintId,dp.DPName,bra.BrandName,sto.CardNo ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.St_Stock sto left join dbo.St_StockName stn on sto.StockNameId = stn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on a.AssetId = sto.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on mu.MUId = sto.UintId ");
            sb.Append(" left join NFMT_Basic.dbo.DeliverPlace dp on dp.DPId = sto.DeliverPlaceId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand bra on bra.BrandId = sto.BrandId ");
            select.TableName = sb.ToString();

            sb.Clear();
            int status = (int)StockStatusEnum.质押库存;
            sb.AppendFormat(" sto.StockId not in (select StockId from NFMT.dbo.St_PledgeApplyDetail where DetailStatus <> {0} ", (int)
Common.StatusEnum.已作废);
            if (pledgeApplyId > 0)
                sb.AppendFormat(" and PledgeApplyId <> {0}", pledgeApplyId);
            sb.Append(")");
            sb.AppendFormat(" and sto.StockStatus < {0} and sto.StockStatus > 0 ", status);
            //sb.AppendFormat(" and sto.StockId not in (select StockId from dbo.St_StockExclusive where ExclusiveStatus = {1})", status, (int)Common.StatusEnum.已生效);

            if (!string.IsNullOrEmpty(sids))
                sb.AppendFormat(" and sto.StockId not in ({0})", sids);
            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel CompleteCancel(UserModel user, int pledgeId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.pledgeDAL.Get(user, pledgeId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Pledge pledge = result.ReturnValue as Pledge;
                    if (pledge == null)
                    {
                        result.Message = "该数据不存在，不能完成撤销";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = pledgeDAL.CompleteCancel(user, pledge);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取所有已生效的明细
                    DAL.PledgeDetialDAL pledgeDetialDAL = new PledgeDetialDAL();
                    result = pledgeDetialDAL.Load(user, pledgeId, StatusEnum.已完成);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.PledgeDetial> details = result.ReturnValue as List<Model.PledgeDetial>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                    DAL.StockDAL stockDAL = new StockDAL();

                    foreach (Model.PledgeDetial detail in details)
                    {
                        //明细完成
                        result = pledgeDetialDAL.CompleteCancel(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取质押流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "质押流水获取失败";
                            return result;
                        }

                        //完成质押流水
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

                        result = stockDAL.UpdateStockStatusDirect(stock, StockStatusEnum.预押库存);
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

        public ResultModel Close(UserModel user, int pledgeId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope())
                {
                    //获取质押
                    result = this.pledgeDAL.Get(user, pledgeId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Pledge pledge = result.ReturnValue as Model.Pledge;
                    if (pledge == null || pledge.PledgeId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "质押不存在";
                        return result;
                    }

                    //质押关闭
                    result = this.pledgeDAL.Close(user, pledge);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取所有已完成的明细
                    DAL.PledgeDetialDAL pledgeDetialDAL = new PledgeDetialDAL();
                    result = pledgeDetialDAL.Load(user, pledgeId, StatusEnum.已生效);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.PledgeDetial> details = result.ReturnValue as List<Model.PledgeDetial>;
                    if (details == null || !details.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    DAL.StockLogDAL stockLogDAL = new StockLogDAL();
                    DAL.StockDAL stockDAL = new StockDAL();

                    foreach (Model.PledgeDetial detail in details)
                    {
                        //明细关闭
                        result = pledgeDetialDAL.Close(user, detail);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取质押流水
                        result = stockLogDAL.Get(user, detail.StockLogId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.StockLog stockLog = result.ReturnValue as Model.StockLog;
                        if (stockLog == null || stockLog.StockLogId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "质押流水获取失败";
                            return result;
                        }

                        //完成质押流水
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

                        if (stock.PreStatus == StockStatusEnum.质押库存)
                            stock.PreStatus = StockStatusEnum.在库正常;

                        result = stockDAL.UpdateStockStatusToPrevious(user, stock);
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