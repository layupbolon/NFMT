/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeApplyStockDetailDAL.cs
// 文件功能描述：质押申请单实货明细dbo.Fin_PledgeApplyStockDetail数据交互类。
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
    /// 质押申请单实货明细dbo.Fin_PledgeApplyStockDetail数据交互类。
    /// </summary>
    public partial class PledgeApplyStockDetailDAL : DetailOperate, IPledgeApplyStockDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PledgeApplyStockDetailDAL()
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
            PledgeApplyStockDetail fin_pledgeapplystockdetail = (PledgeApplyStockDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockDetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter pledgeapplyidpara = new SqlParameter("@PledgeApplyId", SqlDbType.Int, 4);
            pledgeapplyidpara.Value = fin_pledgeapplystockdetail.PledgeApplyId;
            paras.Add(pledgeapplyidpara);

            if (!string.IsNullOrEmpty(fin_pledgeapplystockdetail.ContractNo))
            {
                SqlParameter contractnopara = new SqlParameter("@ContractNo", SqlDbType.VarChar, 30);
                contractnopara.Value = fin_pledgeapplystockdetail.ContractNo;
                paras.Add(contractnopara);
            }

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = fin_pledgeapplystockdetail.NetAmount;
            paras.Add(netamountpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = fin_pledgeapplystockdetail.StockId;
            paras.Add(stockidpara);

            if (!string.IsNullOrEmpty(fin_pledgeapplystockdetail.RefNo))
            {
                SqlParameter refnopara = new SqlParameter("@RefNo", SqlDbType.VarChar, 30);
                refnopara.Value = fin_pledgeapplystockdetail.RefNo;
                paras.Add(refnopara);
            }

            if (!string.IsNullOrEmpty(fin_pledgeapplystockdetail.Deadline))
            {
                SqlParameter deadlinepara = new SqlParameter("@Deadline", SqlDbType.VarChar, 20);
                deadlinepara.Value = fin_pledgeapplystockdetail.Deadline;
                paras.Add(deadlinepara);
            }

            SqlParameter handspara = new SqlParameter("@Hands", SqlDbType.Int, 4);
            handspara.Value = fin_pledgeapplystockdetail.Hands;
            paras.Add(handspara);

            if (!string.IsNullOrEmpty(fin_pledgeapplystockdetail.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = fin_pledgeapplystockdetail.Memo;
                paras.Add(memopara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            PledgeApplyStockDetail pledgeapplystockdetail = new PledgeApplyStockDetail();

            pledgeapplystockdetail.StockDetailId = Convert.ToInt32(dr["StockDetailId"]);

            if (dr["PledgeApplyId"] != DBNull.Value)
            {
                pledgeapplystockdetail.PledgeApplyId = Convert.ToInt32(dr["PledgeApplyId"]);
            }

            if (dr["ContractNo"] != DBNull.Value)
            {
                pledgeapplystockdetail.ContractNo = Convert.ToString(dr["ContractNo"]);
            }

            if (dr["NetAmount"] != DBNull.Value)
            {
                pledgeapplystockdetail.NetAmount = Convert.ToDecimal(dr["NetAmount"]);
            }

            if (dr["StockId"] != DBNull.Value)
            {
                pledgeapplystockdetail.StockId = Convert.ToInt32(dr["StockId"]);
            }

            if (dr["RefNo"] != DBNull.Value)
            {
                pledgeapplystockdetail.RefNo = Convert.ToString(dr["RefNo"]);
            }

            if (dr["Deadline"] != DBNull.Value)
            {
                pledgeapplystockdetail.Deadline = Convert.ToString(dr["Deadline"]);
            }

            if (dr["Hands"] != DBNull.Value)
            {
                pledgeapplystockdetail.Hands = Convert.ToInt32(dr["Hands"]);
            }

            if (dr["Memo"] != DBNull.Value)
            {
                pledgeapplystockdetail.Memo = Convert.ToString(dr["Memo"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                pledgeapplystockdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }


            return pledgeapplystockdetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PledgeApplyStockDetail pledgeapplystockdetail = new PledgeApplyStockDetail();

            int indexStockDetailId = dr.GetOrdinal("StockDetailId");
            pledgeapplystockdetail.StockDetailId = Convert.ToInt32(dr[indexStockDetailId]);

            int indexPledgeApplyId = dr.GetOrdinal("PledgeApplyId");
            if (dr["PledgeApplyId"] != DBNull.Value)
            {
                pledgeapplystockdetail.PledgeApplyId = Convert.ToInt32(dr[indexPledgeApplyId]);
            }

            int indexContractNo = dr.GetOrdinal("ContractNo");
            if (dr["ContractNo"] != DBNull.Value)
            {
                pledgeapplystockdetail.ContractNo = Convert.ToString(dr[indexContractNo]);
            }

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            if (dr["NetAmount"] != DBNull.Value)
            {
                pledgeapplystockdetail.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                pledgeapplystockdetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexRefNo = dr.GetOrdinal("RefNo");
            if (dr["RefNo"] != DBNull.Value)
            {
                pledgeapplystockdetail.RefNo = Convert.ToString(dr[indexRefNo]);
            }

            int indexDeadline = dr.GetOrdinal("Deadline");
            if (dr["Deadline"] != DBNull.Value)
            {
                pledgeapplystockdetail.Deadline = Convert.ToString(dr[indexDeadline]);
            }

            int indexHands = dr.GetOrdinal("Hands");
            if (dr["Hands"] != DBNull.Value)
            {
                pledgeapplystockdetail.Hands = Convert.ToInt32(dr[indexHands]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                pledgeapplystockdetail.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                pledgeapplystockdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }


            return pledgeapplystockdetail;
        }

        public override string TableName
        {
            get
            {
                return "Fin_PledgeApplyStockDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PledgeApplyStockDetail fin_pledgeapplystockdetail = (PledgeApplyStockDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stockdetailidpara = new SqlParameter("@StockDetailId", SqlDbType.Int, 4);
            stockdetailidpara.Value = fin_pledgeapplystockdetail.StockDetailId;
            paras.Add(stockdetailidpara);

            SqlParameter pledgeapplyidpara = new SqlParameter("@PledgeApplyId", SqlDbType.Int, 4);
            pledgeapplyidpara.Value = fin_pledgeapplystockdetail.PledgeApplyId;
            paras.Add(pledgeapplyidpara);

            if (!string.IsNullOrEmpty(fin_pledgeapplystockdetail.ContractNo))
            {
                SqlParameter contractnopara = new SqlParameter("@ContractNo", SqlDbType.VarChar, 30);
                contractnopara.Value = fin_pledgeapplystockdetail.ContractNo;
                paras.Add(contractnopara);
            }

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = fin_pledgeapplystockdetail.NetAmount;
            paras.Add(netamountpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = fin_pledgeapplystockdetail.StockId;
            paras.Add(stockidpara);

            if (!string.IsNullOrEmpty(fin_pledgeapplystockdetail.RefNo))
            {
                SqlParameter refnopara = new SqlParameter("@RefNo", SqlDbType.VarChar, 30);
                refnopara.Value = fin_pledgeapplystockdetail.RefNo;
                paras.Add(refnopara);
            }

            if (!string.IsNullOrEmpty(fin_pledgeapplystockdetail.Deadline))
            {
                SqlParameter deadlinepara = new SqlParameter("@Deadline", SqlDbType.VarChar, 20);
                deadlinepara.Value = fin_pledgeapplystockdetail.Deadline;
                paras.Add(deadlinepara);
            }

            SqlParameter handspara = new SqlParameter("@Hands", SqlDbType.Int, 4);
            handspara.Value = fin_pledgeapplystockdetail.Hands;
            paras.Add(handspara);

            if (!string.IsNullOrEmpty(fin_pledgeapplystockdetail.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = fin_pledgeapplystockdetail.Memo;
                paras.Add(memopara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = fin_pledgeapplystockdetail.DetailStatus;
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
                string sql = string.Format("update dbo.Fin_PledgeApplyStockDetail set DetailStatus = {0} where PledgeApplyId = {1}", (int)Common.StatusEnum.已作废, pledgeApplyId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.Message = "作废成功";
                    result.ResultStatus = 0;
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
            string sql = string.Format("select * from dbo.Fin_PledgeApplyStockDetail where PledgeApplyId = {0} and DetailStatus = {1}", pledgeApplyId, (int)status);
            return Load<Model.PledgeApplyStockDetail>(user, CommandType.Text, sql);
        }

        public ResultModel UpdateDetailStatus(UserModel user, int pledgeApplyId,NFMT.Common.StatusEnum status)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Fin_PledgeApplyStockDetail set DetailStatus = {0} where PledgeApplyId = {1}", (int)status, pledgeApplyId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.Message = "更新成功";
                    result.ResultStatus = 0;
                }
                else
                {
                    result.Message = "更新失败";
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

        #endregion
    }
}
