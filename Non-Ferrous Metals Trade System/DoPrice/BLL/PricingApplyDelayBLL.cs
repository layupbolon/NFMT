/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PricingApplyDelayBLL.cs
// 文件功能描述：点价申请延期dbo.Pri_PricingApplyDelay业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年12月8日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.DoPrice.Model;
using NFMT.DoPrice.DAL;
using NFMT.DoPrice.IDAL;
using NFMT.Common;

namespace NFMT.DoPrice.BLL
{
    /// <summary>
    /// 点价申请延期dbo.Pri_PricingApplyDelay业务逻辑类。
    /// </summary>
    public class PricingApplyDelayBLL : Common.ExecBLL
    {
        private PricingApplyDelayDAL pricingapplydelayDAL = new PricingApplyDelayDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PricingApplyDelayDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PricingApplyDelayBLL()
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
            get { return this.pricingapplydelayDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, string subNo, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pad.DelayId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" pad.DelayId,a.ApplyNo,con.ContractNo,sub.SubNo,CONVERT(varchar,pa.PricingWeight)+ mu.MUName as PricingApplyWeight,CONVERT(varchar,pad.DelayAmount)+mu.MUName as DelayAmount,CONVERT(varchar,pad.DelayFee)+cur.CurrencyName as DelayFee,pad.DelayQP,bd.StatusName,pad.DetailStatus ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_PricingApplyDelay pad ");
            sb.Append(" left join dbo.Pri_PricingApply pa on pad.PricingApplyId = pa.PricingApplyId ");
            sb.Append(" left join dbo.Apply a on pa.ApplyId = a.ApplyId ");
            sb.Append(" left join dbo.Con_Contract con on pa.ContractId = con.ContractId ");
            sb.Append(" left join dbo.Con_ContractSub sub on pa.SubContractId = sub.SubId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on pa.MUId = mu.MUId ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on pa.CurrencyId = cur.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on pad.DetailStatus = bd.DetailId and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (!string.IsNullOrEmpty(subNo))
                sb.AppendFormat(" and (sub.SubNo like '%{0}%' or con.ContractNo like '%{0}%')", subNo);
            if (status > 0)
                sb.AppendFormat(" and pad.DetailStatus = {0} ", status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                Model.PricingApplyDelay delay = (Model.PricingApplyDelay)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = pricingapplydelayDAL.Get(user, obj.Id);
                    Model.PricingApplyDelay delayRes = result.ReturnValue as Model.PricingApplyDelay;
                    if (delayRes == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在";
                        return result;
                    }

                    delayRes.DelayAmount = delay.DelayAmount;
                    delayRes.DelayFee = delay.DelayFee;
                    delayRes.DelayQP = delay.DelayQP;

                    result = pricingapplydelayDAL.Update(user, delayRes);
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
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Goback(UserModel user, int delayId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取点价延期
                    result = pricingapplydelayDAL.Get(user, delayId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.DoPrice.Model.PricingApplyDelay pricingApplyDelay = result.ReturnValue as NFMT.DoPrice.Model.PricingApplyDelay;
                    if (pricingApplyDelay == null || pricingApplyDelay.DelayId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "点价延期验证失败";
                        return result;
                    }

                    result = pricingapplydelayDAL.Goback(user, pricingApplyDelay);
                    if (result.ResultStatus != 0)
                        return result;

                    //工作流任务关闭
                    WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    result = sourceDAL.SynchronousStatus(user, pricingApplyDelay);
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

        public ResultModel Invalid(UserModel user, int delayId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (System.Transactions.TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取点价延期
                    result = pricingapplydelayDAL.Get(user, delayId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.DoPrice.Model.PricingApplyDelay pricingApplyDelay = result.ReturnValue as NFMT.DoPrice.Model.PricingApplyDelay;
                    if (pricingApplyDelay == null || pricingApplyDelay.DelayId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "点价延期验证失败";
                        return result;
                    }

                    result = pricingapplydelayDAL.Invalid(user, pricingApplyDelay);
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

        public ResultModel Complete(UserModel user, int delayId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取点价延期
                    result = pricingapplydelayDAL.Get(user, delayId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.DoPrice.Model.PricingApplyDelay pricingApplyDelay = result.ReturnValue as NFMT.DoPrice.Model.PricingApplyDelay;
                    if (pricingApplyDelay == null || pricingApplyDelay.DelayId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "点价延期验证失败";
                        return result;
                    }

                    //将延期信息更新到点价申请上
                    DAL.PricingApplyDAL pricingApplyDAL = new PricingApplyDAL();
                    result = pricingApplyDAL.UpdateDelayInfo(user, pricingApplyDelay);
                    if (result.ResultStatus != 0)
                        return result;

                    result = pricingapplydelayDAL.Complete(user, pricingApplyDelay);
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

        public ResultModel CompleteCancel(UserModel user, int delayId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取点价延期
                    result = pricingapplydelayDAL.Get(user, delayId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.DoPrice.Model.PricingApplyDelay pricingApplyDelay = result.ReturnValue as NFMT.DoPrice.Model.PricingApplyDelay;
                    if (pricingApplyDelay == null || pricingApplyDelay.DelayId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "点价延期验证失败";
                        return result;
                    }

                    DAL.PricingApplyDAL pricingApplyDAL = new PricingApplyDAL();
                    result = pricingApplyDAL.UpdateCancelDelayInfo(user, pricingApplyDelay);
                    if (result.ResultStatus != 0)
                        return result;

                    result = pricingapplydelayDAL.CompleteCancel(user, pricingApplyDelay);
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

        public ResultModel GetCanDelayWeight(UserModel user, int pricingApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = pricingapplydelayDAL.GetCanDelayWeight(user, pricingApplyId);
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

        public SelectModel GetSelectModelByPricingApplyId(int pageIndex, int pageSize, string orderStr, int pricingApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pad.DelayId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " pad.DelayId,CONVERT(varchar,pad.DelayAmount)+ mu.MUName as DelayAmountName,pad.DelayAmount,CONVERT(varchar,pad.DelayFee) + cur.CurrencyName as DelayFeeName,pad.DelayFee,pad.DelayQP  ";

            select.TableName = " dbo.Pri_PricingApplyDelay pad left join dbo.Pri_PricingApply pa on pad.PricingApplyId = pa.PricingApplyId left join NFMT_Basic.dbo.MeasureUnit mu on pa.MUId = mu.MUId left join NFMT_Basic.dbo.Currency cur on pa.CurrencyId = cur.CurrencyId";

            select.WhereStr = string.Format(" pad.DetailStatus = {0} and pad.PricingApplyId = {1} ", (int)Common.StatusEnum.已完成, pricingApplyId);

            return select;
        }

        #endregion
    }
}
