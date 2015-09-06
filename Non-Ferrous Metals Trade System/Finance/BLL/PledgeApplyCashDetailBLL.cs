/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeApplyCashDetailBLL.cs
// 文件功能描述：质押申请单期货头寸明细dbo.Fin_PledgeApplyCashDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Finance.Model;
using NFMT.Finance.DAL;
using NFMT.Finance.IDAL;
using NFMT.Common;

namespace NFMT.Finance.BLL
{
    /// <summary>
    /// 质押申请单期货头寸明细dbo.Fin_PledgeApplyCashDetail业务逻辑类。
    /// </summary>
    public class PledgeApplyCashDetailBLL : Common.ExecBLL
    {
        private PledgeApplyCashDetailDAL pledgeapplycashdetailDAL = new PledgeApplyCashDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PledgeApplyCashDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PledgeApplyCashDetailBLL()
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
            get { return this.pledgeapplycashdetailDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int pledgeApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "detail.CashDetailId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" detail.*,bd.StatusName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fin_PledgeApplyCashDetail detail ");
            sb.Append(" left join NFMT_Basic..BDStatusDetail bd on bd.DetailId = detail.DetailStatus ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" detail.PledgeApplyId = {0} and detail.DetailStatus >= {1} ", pledgeApplyId, (int)Common.StatusEnum.已作废);

            select.WhereStr = sb.ToString();

            return select;
        }

        public SelectModel GetCashSelectModel(int pageIndex, int pageSize, string orderStr, int pledgeApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "psd.ContractNo desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" psd.ContractNo as StockContractNo,psd.Deadline,SUM(Hands) as Hands,bank.BankName as AccountName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Fin_PledgeApply pa ");
            sb.AppendFormat(" inner join dbo.Fin_PledgeApplyStockDetail psd on pa.PledgeApplyId = psd.PledgeApplyId and psd.DetailStatus ={0} ", (int)Common.StatusEnum.已生效);
            sb.Append(" left join NFMT_Basic..Bank bank on pa.FinancingBankId = bank.BankId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" pa.PledgeApplyId ={0} group by psd.ContractNo,psd.Deadline,bank.BankName ", pledgeApplyId);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel LoadByPledgeApplyId(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = pledgeapplycashdetailDAL.LoadByPledgeApplyId(user, pledgeApplyId, StatusEnum.已生效);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        #endregion
    }
}
