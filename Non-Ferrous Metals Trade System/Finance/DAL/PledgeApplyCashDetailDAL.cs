/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeApplyCashDetailDAL.cs
// 文件功能描述：质押申请单期货头寸明细dbo.Fin_PledgeApplyCashDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Finance.Model;
using NFMT.DBUtility;
using NFMT.Finance.IDAL;
using NFMT.Common;

namespace NFMT.Finance.DAL
{
    /// <summary>
    /// 质押申请单期货头寸明细dbo.Fin_PledgeApplyCashDetail数据交互类。
    /// </summary>
    public partial class PledgeApplyCashDetailDAL : ExecOperate, IPledgeApplyCashDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PledgeApplyCashDetailDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringFinance;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            PledgeApplyCashDetail fin_pledgeapplycashdetail = (PledgeApplyCashDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@CashDetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter pledgeapplyidpara = new SqlParameter("@PledgeApplyId", SqlDbType.Int, 4);
            pledgeapplyidpara.Value = fin_pledgeapplycashdetail.PledgeApplyId;
            paras.Add(pledgeapplyidpara);

            if (!string.IsNullOrEmpty(fin_pledgeapplycashdetail.StockContractNo))
            {
                SqlParameter stockcontractnopara = new SqlParameter("@StockContractNo", SqlDbType.VarChar, 30);
                stockcontractnopara.Value = fin_pledgeapplycashdetail.StockContractNo;
                paras.Add(stockcontractnopara);
            }

            if (!string.IsNullOrEmpty(fin_pledgeapplycashdetail.Deadline))
            {
                SqlParameter deadlinepara = new SqlParameter("@Deadline", SqlDbType.VarChar, 20);
                deadlinepara.Value = fin_pledgeapplycashdetail.Deadline;
                paras.Add(deadlinepara);
            }

            SqlParameter handspara = new SqlParameter("@Hands", SqlDbType.Int, 4);
            handspara.Value = fin_pledgeapplycashdetail.Hands;
            paras.Add(handspara);

            SqlParameter pricepara = new SqlParameter("@Price", SqlDbType.Decimal, 9);
            pricepara.Value = fin_pledgeapplycashdetail.Price;
            paras.Add(pricepara);

            SqlParameter expiringdatepara = new SqlParameter("@ExpiringDate", SqlDbType.DateTime, 8);
            expiringdatepara.Value = fin_pledgeapplycashdetail.ExpiringDate;
            paras.Add(expiringdatepara);

            if (!string.IsNullOrEmpty(fin_pledgeapplycashdetail.AccountName))
            {
                SqlParameter accountnamepara = new SqlParameter("@AccountName", SqlDbType.VarChar, 50);
                accountnamepara.Value = fin_pledgeapplycashdetail.AccountName;
                paras.Add(accountnamepara);
            }

            if (!string.IsNullOrEmpty(fin_pledgeapplycashdetail.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = fin_pledgeapplycashdetail.Memo;
                paras.Add(memopara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            PledgeApplyCashDetail pledgeapplycashdetail = new PledgeApplyCashDetail();

            pledgeapplycashdetail.CashDetailId = Convert.ToInt32(dr["CashDetailId"]);

            if (dr["PledgeApplyId"] != DBNull.Value)
            {
                pledgeapplycashdetail.PledgeApplyId = Convert.ToInt32(dr["PledgeApplyId"]);
            }

            if (dr["StockContractNo"] != DBNull.Value)
            {
                pledgeapplycashdetail.StockContractNo = Convert.ToString(dr["StockContractNo"]);
            }

            if (dr["Deadline"] != DBNull.Value)
            {
                pledgeapplycashdetail.Deadline = Convert.ToString(dr["Deadline"]);
            }

            if (dr["Hands"] != DBNull.Value)
            {
                pledgeapplycashdetail.Hands = Convert.ToInt32(dr["Hands"]);
            }

            if (dr["Price"] != DBNull.Value)
            {
                pledgeapplycashdetail.Price = Convert.ToDecimal(dr["Price"]);
            }

            if (dr["ExpiringDate"] != DBNull.Value)
            {
                pledgeapplycashdetail.ExpiringDate = Convert.ToDateTime(dr["ExpiringDate"]);
            }

            if (dr["AccountName"] != DBNull.Value)
            {
                pledgeapplycashdetail.AccountName = Convert.ToString(dr["AccountName"]);
            }

            if (dr["Memo"] != DBNull.Value)
            {
                pledgeapplycashdetail.Memo = Convert.ToString(dr["Memo"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                pledgeapplycashdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }


            return pledgeapplycashdetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PledgeApplyCashDetail pledgeapplycashdetail = new PledgeApplyCashDetail();

            int indexCashDetailId = dr.GetOrdinal("CashDetailId");
            pledgeapplycashdetail.CashDetailId = Convert.ToInt32(dr[indexCashDetailId]);

            int indexPledgeApplyId = dr.GetOrdinal("PledgeApplyId");
            if (dr["PledgeApplyId"] != DBNull.Value)
            {
                pledgeapplycashdetail.PledgeApplyId = Convert.ToInt32(dr[indexPledgeApplyId]);
            }

            int indexStockContractNo = dr.GetOrdinal("StockContractNo");
            if (dr["StockContractNo"] != DBNull.Value)
            {
                pledgeapplycashdetail.StockContractNo = Convert.ToString(dr[indexStockContractNo]);
            }

            int indexDeadline = dr.GetOrdinal("Deadline");
            if (dr["Deadline"] != DBNull.Value)
            {
                pledgeapplycashdetail.Deadline = Convert.ToString(dr[indexDeadline]);
            }

            int indexHands = dr.GetOrdinal("Hands");
            if (dr["Hands"] != DBNull.Value)
            {
                pledgeapplycashdetail.Hands = Convert.ToInt32(dr[indexHands]);
            }

            int indexPrice = dr.GetOrdinal("Price");
            if (dr["Price"] != DBNull.Value)
            {
                pledgeapplycashdetail.Price = Convert.ToDecimal(dr[indexPrice]);
            }

            int indexExpiringDate = dr.GetOrdinal("ExpiringDate");
            if (dr["ExpiringDate"] != DBNull.Value)
            {
                pledgeapplycashdetail.ExpiringDate = Convert.ToDateTime(dr[indexExpiringDate]);
            }

            int indexAccountName = dr.GetOrdinal("AccountName");
            if (dr["AccountName"] != DBNull.Value)
            {
                pledgeapplycashdetail.AccountName = Convert.ToString(dr[indexAccountName]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                pledgeapplycashdetail.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                pledgeapplycashdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }


            return pledgeapplycashdetail;
        }

        public override string TableName
        {
            get
            {
                return "Fin_PledgeApplyCashDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PledgeApplyCashDetail fin_pledgeapplycashdetail = (PledgeApplyCashDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter cashdetailidpara = new SqlParameter("@CashDetailId", SqlDbType.Int, 4);
            cashdetailidpara.Value = fin_pledgeapplycashdetail.CashDetailId;
            paras.Add(cashdetailidpara);

            SqlParameter pledgeapplyidpara = new SqlParameter("@PledgeApplyId", SqlDbType.Int, 4);
            pledgeapplyidpara.Value = fin_pledgeapplycashdetail.PledgeApplyId;
            paras.Add(pledgeapplyidpara);

            if (!string.IsNullOrEmpty(fin_pledgeapplycashdetail.StockContractNo))
            {
                SqlParameter stockcontractnopara = new SqlParameter("@StockContractNo", SqlDbType.VarChar, 30);
                stockcontractnopara.Value = fin_pledgeapplycashdetail.StockContractNo;
                paras.Add(stockcontractnopara);
            }

            if (!string.IsNullOrEmpty(fin_pledgeapplycashdetail.Deadline))
            {
                SqlParameter deadlinepara = new SqlParameter("@Deadline", SqlDbType.VarChar, 20);
                deadlinepara.Value = fin_pledgeapplycashdetail.Deadline;
                paras.Add(deadlinepara);
            }

            SqlParameter handspara = new SqlParameter("@Hands", SqlDbType.Int, 4);
            handspara.Value = fin_pledgeapplycashdetail.Hands;
            paras.Add(handspara);

            SqlParameter pricepara = new SqlParameter("@Price", SqlDbType.Decimal, 9);
            pricepara.Value = fin_pledgeapplycashdetail.Price;
            paras.Add(pricepara);

            SqlParameter expiringdatepara = new SqlParameter("@ExpiringDate", SqlDbType.DateTime, 8);
            expiringdatepara.Value = fin_pledgeapplycashdetail.ExpiringDate;
            paras.Add(expiringdatepara);

            if (!string.IsNullOrEmpty(fin_pledgeapplycashdetail.AccountName))
            {
                SqlParameter accountnamepara = new SqlParameter("@AccountName", SqlDbType.VarChar, 50);
                accountnamepara.Value = fin_pledgeapplycashdetail.AccountName;
                paras.Add(accountnamepara);
            }

            if (!string.IsNullOrEmpty(fin_pledgeapplycashdetail.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = fin_pledgeapplycashdetail.Memo;
                paras.Add(memopara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = fin_pledgeapplycashdetail.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel InvalidAll(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Fin_PledgeApplyCashDetail set DetailStatus = {0} where PledgeApplyId = {1}", (int)Common.StatusEnum.已作废, pledgeApplyId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                }
                else
                {
                    result.Message = "作废失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        public ResultModel LoadByPledgeApplyId(UserModel user, int pledgeApplyId, Common.StatusEnum status)
        {
            string sql = string.Format("select * from dbo.Fin_PledgeApplyCashDetail where PledgeApplyId = {0} and DetailStatus = {1}", pledgeApplyId, (int)status);
            return Load<Model.PledgeApplyCashDetail>(user, CommandType.Text, sql);
        }

        public ResultModel UpdateStatus(UserModel user, int pledgeApplyId, Common.StatusEnum status)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Fin_PledgeApplyCashDetail set DetailStatus = {0} where PledgeApplyId = {1}", (int)status, pledgeApplyId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                }
                else
                {
                    result.Message = "作废失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        public ResultModel LoadByPledgeApplyId(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select SUM(Hands) Hands,StockContractNo,ExpiringDate,Price from dbo.Fin_PledgeApplyCashDetail where PledgeApplyId = {0} and DetailStatus = {1} group by StockContractNo,ExpiringDate,Price", pledgeApplyId, (int)Common.StatusEnum.已生效);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt == null || dt.Rows.Count < 1)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
                else
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = dt;
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        #endregion
    }
}
