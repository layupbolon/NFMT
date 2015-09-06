/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PaymentStockDetailBLL.cs
// 文件功能描述：库存财务付款明细dbo.Fun_PaymentStockDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月15日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Funds.Model;
using NFMT.Funds.DAL;
using NFMT.Funds.IDAL;
using NFMT.Common;

namespace NFMT.Funds.BLL
{
    /// <summary>
    /// 库存财务付款明细dbo.Fun_PaymentStockDetail业务逻辑类。
    /// </summary>
    public class PaymentStockDetailBLL : Common.ExecBLL
    {
        private PaymentStockDetailDAL paymentstockdetailDAL = new PaymentStockDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PaymentStockDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PaymentStockDetailBLL()
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
            get { return this.paymentstockdetailDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "psd.DetailId desc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" psd.DetailId,a.ApplyNo,sub.SubNo,sn.RefNo,CONVERT(varchar,psd.PayBala)+cur.CurrencyName as PayBala,CONVERT(varchar,psd.FundsBala)+cur.CurrencyName as FundsBala,CONVERT(varchar,psd.VirtualBala)+cur.CurrencyName as VirtualBala,bd.StatusName,psd.DetailStatus ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fun_PaymentStockDetail psd ");
            sb.Append(" left join dbo.Fun_PayApply pa on psd.PayApplyId = pa.PayApplyId ");
            sb.Append(" inner join dbo.Apply a on pa.ApplyId = a.ApplyId ");
            sb.Append(" left join dbo.Con_ContractSub sub on psd.SubId = sub.SubId ");
            sb.Append(" left join dbo.St_Stock st on psd.StockId = st.StockId ");
            sb.Append(" left join dbo.St_StockName sn on st.StockNameId = sn.StockNameId ");
            sb.Append(" left join dbo.Fun_Payment p on psd.PaymentId = p.PaymentId ");
            sb.Append(" left join NFMT_Basic..Currency cur on p.CurrencyId = cur.CurrencyId ");
            sb.AppendFormat(" left join NFMT_Basic..BDStatusDetail bd on bd.DetailId = psd.DetailStatus and bd.StatusId = {0} ", statusId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" psd.SourceFrom = {0} ", (int)PaymenyAllotTypeEnum.合约付款分配);
            if (status > 0)
                sb.AppendFormat(" and psd.DetailStatus = {0} ", status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel Insert(UserModel user, List<Model.PaymentStockDetail> details)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (Model.PaymentStockDetail detail in details)
                    {
                        result = paymentstockdetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (result.ResultStatus == 0)
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

        public ResultModel Invalid(UserModel user, int detailId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = paymentstockdetailDAL.Get(user, detailId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PaymentStockDetail paymentStockDetail = result.ReturnValue as Model.PaymentStockDetail;
                    if (paymentStockDetail == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    result = paymentstockdetailDAL.Invalid(user, paymentStockDetail);
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
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel Update(UserModel user, Model.PaymentStockDetail detail)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = paymentstockdetailDAL.Get(user, detail.DetailId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.PaymentStockDetail resultModel = result.ReturnValue as Model.PaymentStockDetail;
                    if (resultModel == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    resultModel.PayBala = detail.PayBala;
                    resultModel.VirtualBala = detail.VirtualBala;

                    result = paymentstockdetailDAL.Update(user, resultModel);
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
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel LoadByContractDetailId(UserModel user, int contractDetailId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = paymentstockdetailDAL.LoadByContractDetailId(user, contractDetailId);
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

        public ResultModel LoadByPaymentId(UserModel user, int paymentId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = paymentstockdetailDAL.LoadByPaymentId(user, paymentId);
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

        #endregion
    }
}
