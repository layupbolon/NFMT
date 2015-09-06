/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoApplyDetailDAL.cs
// 文件功能描述：赎回申请单明细dbo.Fin_RepoApplyDetail数据交互类。
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
    /// 赎回申请单明细dbo.Fin_RepoApplyDetail数据交互类。
    /// </summary>
    public partial class RepoApplyDetailDAL : DetailOperate, IRepoApplyDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepoApplyDetailDAL()
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
            RepoApplyDetail fin_repoapplydetail = (RepoApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter repoapplyidpara = new SqlParameter("@RepoApplyId", SqlDbType.Int, 4);
            repoapplyidpara.Value = fin_repoapplydetail.RepoApplyId;
            paras.Add(repoapplyidpara);

            SqlParameter stockdetailidpara = new SqlParameter("@StockDetailId", SqlDbType.Int, 4);
            stockdetailidpara.Value = fin_repoapplydetail.StockDetailId;
            paras.Add(stockdetailidpara);

            SqlParameter pledgeapplyidpara = new SqlParameter("@PledgeApplyId", SqlDbType.Int, 4);
            pledgeapplyidpara.Value = fin_repoapplydetail.PledgeApplyId;
            paras.Add(pledgeapplyidpara);

            SqlParameter repotimepara = new SqlParameter("@RepoTime", SqlDbType.DateTime, 8);
            repotimepara.Value = fin_repoapplydetail.RepoTime;
            paras.Add(repotimepara);

            if (!string.IsNullOrEmpty(fin_repoapplydetail.ContractNo))
            {
                SqlParameter contractnopara = new SqlParameter("@ContractNo", SqlDbType.VarChar, 30);
                contractnopara.Value = fin_repoapplydetail.ContractNo;
                paras.Add(contractnopara);
            }

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = fin_repoapplydetail.NetAmount;
            paras.Add(netamountpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = fin_repoapplydetail.StockId;
            paras.Add(stockidpara);

            if (!string.IsNullOrEmpty(fin_repoapplydetail.RefNo))
            {
                SqlParameter refnopara = new SqlParameter("@RefNo", SqlDbType.VarChar, 30);
                refnopara.Value = fin_repoapplydetail.RefNo;
                paras.Add(refnopara);
            }

            SqlParameter handspara = new SqlParameter("@Hands", SqlDbType.Int, 4);
            handspara.Value = fin_repoapplydetail.Hands;
            paras.Add(handspara);

            SqlParameter pricepara = new SqlParameter("@Price", SqlDbType.Decimal, 9);
            pricepara.Value = fin_repoapplydetail.Price;
            paras.Add(pricepara);

            SqlParameter expiringdatepara = new SqlParameter("@ExpiringDate", SqlDbType.DateTime, 8);
            expiringdatepara.Value = fin_repoapplydetail.ExpiringDate;
            paras.Add(expiringdatepara);

            if (!string.IsNullOrEmpty(fin_repoapplydetail.AccountName))
            {
                SqlParameter accountnamepara = new SqlParameter("@AccountName", SqlDbType.VarChar, 50);
                accountnamepara.Value = fin_repoapplydetail.AccountName;
                paras.Add(accountnamepara);
            }

            if (!string.IsNullOrEmpty(fin_repoapplydetail.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = fin_repoapplydetail.Memo;
                paras.Add(memopara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            RepoApplyDetail repoapplydetail = new RepoApplyDetail();

            repoapplydetail.DetailId = Convert.ToInt32(dr["DetailId"]);

            if (dr["RepoApplyId"] != DBNull.Value)
            {
                repoapplydetail.RepoApplyId = Convert.ToInt32(dr["RepoApplyId"]);
            }

            if (dr["StockDetailId"] != DBNull.Value)
            {
                repoapplydetail.StockDetailId = Convert.ToInt32(dr["StockDetailId"]);
            }

            if (dr["PledgeApplyId"] != DBNull.Value)
            {
                repoapplydetail.PledgeApplyId = Convert.ToInt32(dr["PledgeApplyId"]);
            }

            if (dr["RepoTime"] != DBNull.Value)
            {
                repoapplydetail.RepoTime = Convert.ToDateTime(dr["RepoTime"]);
            }

            if (dr["ContractNo"] != DBNull.Value)
            {
                repoapplydetail.ContractNo = Convert.ToString(dr["ContractNo"]);
            }

            if (dr["NetAmount"] != DBNull.Value)
            {
                repoapplydetail.NetAmount = Convert.ToDecimal(dr["NetAmount"]);
            }

            if (dr["StockId"] != DBNull.Value)
            {
                repoapplydetail.StockId = Convert.ToInt32(dr["StockId"]);
            }

            if (dr["RefNo"] != DBNull.Value)
            {
                repoapplydetail.RefNo = Convert.ToString(dr["RefNo"]);
            }

            if (dr["Hands"] != DBNull.Value)
            {
                repoapplydetail.Hands = Convert.ToInt32(dr["Hands"]);
            }

            if (dr["Price"] != DBNull.Value)
            {
                repoapplydetail.Price = Convert.ToDecimal(dr["Price"]);
            }

            if (dr["ExpiringDate"] != DBNull.Value)
            {
                repoapplydetail.ExpiringDate = Convert.ToDateTime(dr["ExpiringDate"]);
            }

            if (dr["AccountName"] != DBNull.Value)
            {
                repoapplydetail.AccountName = Convert.ToString(dr["AccountName"]);
            }

            if (dr["Memo"] != DBNull.Value)
            {
                repoapplydetail.Memo = Convert.ToString(dr["Memo"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                repoapplydetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }


            return repoapplydetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            RepoApplyDetail repoapplydetail = new RepoApplyDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            repoapplydetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexRepoApplyId = dr.GetOrdinal("RepoApplyId");
            if (dr["RepoApplyId"] != DBNull.Value)
            {
                repoapplydetail.RepoApplyId = Convert.ToInt32(dr[indexRepoApplyId]);
            }

            int indexStockDetailId = dr.GetOrdinal("StockDetailId");
            if (dr["StockDetailId"] != DBNull.Value)
            {
                repoapplydetail.StockDetailId = Convert.ToInt32(dr[indexStockDetailId]);
            }

            int indexPledgeApplyId = dr.GetOrdinal("PledgeApplyId");
            if (dr["PledgeApplyId"] != DBNull.Value)
            {
                repoapplydetail.PledgeApplyId = Convert.ToInt32(dr[indexPledgeApplyId]);
            }

            int indexRepoTime = dr.GetOrdinal("RepoTime");
            if (dr["RepoTime"] != DBNull.Value)
            {
                repoapplydetail.RepoTime = Convert.ToDateTime(dr[indexRepoTime]);
            }

            int indexContractNo = dr.GetOrdinal("ContractNo");
            if (dr["ContractNo"] != DBNull.Value)
            {
                repoapplydetail.ContractNo = Convert.ToString(dr[indexContractNo]);
            }

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            if (dr["NetAmount"] != DBNull.Value)
            {
                repoapplydetail.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                repoapplydetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexRefNo = dr.GetOrdinal("RefNo");
            if (dr["RefNo"] != DBNull.Value)
            {
                repoapplydetail.RefNo = Convert.ToString(dr[indexRefNo]);
            }

            int indexHands = dr.GetOrdinal("Hands");
            if (dr["Hands"] != DBNull.Value)
            {
                repoapplydetail.Hands = Convert.ToInt32(dr[indexHands]);
            }

            int indexPrice = dr.GetOrdinal("Price");
            if (dr["Price"] != DBNull.Value)
            {
                repoapplydetail.Price = Convert.ToDecimal(dr[indexPrice]);
            }

            int indexExpiringDate = dr.GetOrdinal("ExpiringDate");
            if (dr["ExpiringDate"] != DBNull.Value)
            {
                repoapplydetail.ExpiringDate = Convert.ToDateTime(dr[indexExpiringDate]);
            }

            int indexAccountName = dr.GetOrdinal("AccountName");
            if (dr["AccountName"] != DBNull.Value)
            {
                repoapplydetail.AccountName = Convert.ToString(dr[indexAccountName]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                repoapplydetail.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                repoapplydetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }


            return repoapplydetail;
        }

        public override string TableName
        {
            get
            {
                return "Fin_RepoApplyDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            RepoApplyDetail fin_repoapplydetail = (RepoApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = fin_repoapplydetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter repoapplyidpara = new SqlParameter("@RepoApplyId", SqlDbType.Int, 4);
            repoapplyidpara.Value = fin_repoapplydetail.RepoApplyId;
            paras.Add(repoapplyidpara);

            SqlParameter stockdetailidpara = new SqlParameter("@StockDetailId", SqlDbType.Int, 4);
            stockdetailidpara.Value = fin_repoapplydetail.StockDetailId;
            paras.Add(stockdetailidpara);

            SqlParameter pledgeapplyidpara = new SqlParameter("@PledgeApplyId", SqlDbType.Int, 4);
            pledgeapplyidpara.Value = fin_repoapplydetail.PledgeApplyId;
            paras.Add(pledgeapplyidpara);

            SqlParameter repotimepara = new SqlParameter("@RepoTime", SqlDbType.DateTime, 8);
            repotimepara.Value = fin_repoapplydetail.RepoTime;
            paras.Add(repotimepara);

            if (!string.IsNullOrEmpty(fin_repoapplydetail.ContractNo))
            {
                SqlParameter contractnopara = new SqlParameter("@ContractNo", SqlDbType.VarChar, 30);
                contractnopara.Value = fin_repoapplydetail.ContractNo;
                paras.Add(contractnopara);
            }

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = fin_repoapplydetail.NetAmount;
            paras.Add(netamountpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = fin_repoapplydetail.StockId;
            paras.Add(stockidpara);

            if (!string.IsNullOrEmpty(fin_repoapplydetail.RefNo))
            {
                SqlParameter refnopara = new SqlParameter("@RefNo", SqlDbType.VarChar, 30);
                refnopara.Value = fin_repoapplydetail.RefNo;
                paras.Add(refnopara);
            }

            SqlParameter handspara = new SqlParameter("@Hands", SqlDbType.Int, 4);
            handspara.Value = fin_repoapplydetail.Hands;
            paras.Add(handspara);

            SqlParameter pricepara = new SqlParameter("@Price", SqlDbType.Decimal, 9);
            pricepara.Value = fin_repoapplydetail.Price;
            paras.Add(pricepara);

            SqlParameter expiringdatepara = new SqlParameter("@ExpiringDate", SqlDbType.DateTime, 8);
            expiringdatepara.Value = fin_repoapplydetail.ExpiringDate;
            paras.Add(expiringdatepara);

            if (!string.IsNullOrEmpty(fin_repoapplydetail.AccountName))
            {
                SqlParameter accountnamepara = new SqlParameter("@AccountName", SqlDbType.VarChar, 50);
                accountnamepara.Value = fin_repoapplydetail.AccountName;
                paras.Add(accountnamepara);
            }

            if (!string.IsNullOrEmpty(fin_repoapplydetail.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = fin_repoapplydetail.Memo;
                paras.Add(memopara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = fin_repoapplydetail.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel InvalidAll(UserModel user, int repoApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Fin_RepoApplyDetail set DetailStatus = {0} where RepoApplyId = {1}", (int)Common.StatusEnum.已作废, repoApplyId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "作废失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel LoadByRepoApplyId(UserModel user, int repoApplyId, Common.StatusEnum status)
        {
            string sql = string.Format("select * from dbo.Fin_RepoApplyDetail where RepoApplyId = {0} and DetailStatus = {1}", repoApplyId, (int)status);
            return Load<Model.RepoApplyDetail>(user, CommandType.Text, sql);
        }

        public ResultModel UpdateStatus(UserModel user, int repoApplyId, Common.StatusEnum status)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Fin_RepoApplyDetail set DetailStatus = {0} where RepoApplyId = {1}", (int)status, repoApplyId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "修改状态成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "修改状态失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel LoadByRepoApplyId(UserModel user, int repoApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select SUM(Hands) Hands,ContractNo StockContractNo,ExpiringDate,Price from dbo.Fin_RepoApplyDetail where RepoApplyId = {0} and DetailStatus = {1} group by ContractNo,ExpiringDate,Price", repoApplyId, (int)Common.StatusEnum.已生效);
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
