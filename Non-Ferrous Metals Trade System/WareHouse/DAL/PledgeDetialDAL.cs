/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeDetialDAL.cs
// 文件功能描述：质押明细dbo.St_PledgeDetial数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年8月4日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.WareHouse.Model;
using NFMT.DBUtility;
using NFMT.WareHouse.IDAL;
using NFMT.Common;

namespace NFMT.WareHouse.DAL
{
    /// <summary>
    /// 质押明细dbo.St_PledgeDetial数据交互类。
    /// </summary>
    public class PledgeDetialDAL : DetailOperate, IPledgeDetialDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PledgeDetialDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringNFMT;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            PledgeDetial st_pledgedetial = (PledgeDetial)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter pledgeidpara = new SqlParameter("@PledgeId", SqlDbType.Int, 4);
            pledgeidpara.Value = st_pledgedetial.PledgeId;
            paras.Add(pledgeidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);

            SqlParameter pledgeapplydetailidpara = new SqlParameter("@PledgeApplyDetailId", SqlDbType.Int, 4);
            pledgeapplydetailidpara.Value = st_pledgedetial.PledgeApplyDetailId;
            paras.Add(pledgeapplydetailidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_pledgedetial.StockId;
            paras.Add(stockidpara);

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = st_pledgedetial.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter unitpara = new SqlParameter("@Unit", SqlDbType.Int, 4);
            unitpara.Value = st_pledgedetial.Unit;
            paras.Add(unitpara);

            SqlParameter pledgepricepara = new SqlParameter("@PledgePrice", SqlDbType.Decimal, 9);
            pledgepricepara.Value = st_pledgedetial.PledgePrice;
            paras.Add(pledgepricepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = st_pledgedetial.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_pledgedetial.StockLogId;
            paras.Add(stocklogidpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            PledgeDetial pledgedetial = new PledgeDetial();

            pledgedetial.DetailId = Convert.ToInt32(dr["DetailId"]);

            if (dr["PledgeId"] != DBNull.Value)
            {
                pledgedetial.PledgeId = Convert.ToInt32(dr["PledgeId"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                pledgedetial.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }

            if (dr["PledgeApplyDetailId"] != DBNull.Value)
            {
                pledgedetial.PledgeApplyDetailId = Convert.ToInt32(dr["PledgeApplyDetailId"]);
            }

            if (dr["StockId"] != DBNull.Value)
            {
                pledgedetial.StockId = Convert.ToInt32(dr["StockId"]);
            }

            if (dr["GrossAmount"] != DBNull.Value)
            {
                pledgedetial.GrossAmount = Convert.ToDecimal(dr["GrossAmount"]);
            }

            if (dr["Unit"] != DBNull.Value)
            {
                pledgedetial.Unit = Convert.ToInt32(dr["Unit"]);
            }

            if (dr["PledgePrice"] != DBNull.Value)
            {
                pledgedetial.PledgePrice = Convert.ToDecimal(dr["PledgePrice"]);
            }

            if (dr["CurrencyId"] != DBNull.Value)
            {
                pledgedetial.CurrencyId = Convert.ToInt32(dr["CurrencyId"]);
            }

            if (dr["StockLogId"] != DBNull.Value)
            {
                pledgedetial.StockLogId = Convert.ToInt32(dr["StockLogId"]);
            }


            return pledgedetial;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PledgeDetial pledgedetial = new PledgeDetial();

            int indexDetailId = dr.GetOrdinal("DetailId");
            pledgedetial.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexPledgeId = dr.GetOrdinal("PledgeId");
            if (dr["PledgeId"] != DBNull.Value)
            {
                pledgedetial.PledgeId = Convert.ToInt32(dr[indexPledgeId]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                pledgedetial.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexPledgeApplyDetailId = dr.GetOrdinal("PledgeApplyDetailId");
            if (dr["PledgeApplyDetailId"] != DBNull.Value)
            {
                pledgedetial.PledgeApplyDetailId = Convert.ToInt32(dr[indexPledgeApplyDetailId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                pledgedetial.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexGrossAmount = dr.GetOrdinal("GrossAmount");
            if (dr["GrossAmount"] != DBNull.Value)
            {
                pledgedetial.GrossAmount = Convert.ToDecimal(dr[indexGrossAmount]);
            }

            int indexUnit = dr.GetOrdinal("Unit");
            if (dr["Unit"] != DBNull.Value)
            {
                pledgedetial.Unit = Convert.ToInt32(dr[indexUnit]);
            }

            int indexPledgePrice = dr.GetOrdinal("PledgePrice");
            if (dr["PledgePrice"] != DBNull.Value)
            {
                pledgedetial.PledgePrice = Convert.ToDecimal(dr[indexPledgePrice]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                pledgedetial.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                pledgedetial.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }


            return pledgedetial;
        }

        public override string TableName
        {
            get
            {
                return "St_PledgeDetial";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PledgeDetial st_pledgedetial = (PledgeDetial)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = st_pledgedetial.DetailId;
            paras.Add(detailidpara);

            SqlParameter pledgeidpara = new SqlParameter("@PledgeId", SqlDbType.Int, 4);
            pledgeidpara.Value = st_pledgedetial.PledgeId;
            paras.Add(pledgeidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = st_pledgedetial.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter pledgeapplydetailidpara = new SqlParameter("@PledgeApplyDetailId", SqlDbType.Int, 4);
            pledgeapplydetailidpara.Value = st_pledgedetial.PledgeApplyDetailId;
            paras.Add(pledgeapplydetailidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_pledgedetial.StockId;
            paras.Add(stockidpara);

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = st_pledgedetial.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter unitpara = new SqlParameter("@Unit", SqlDbType.Int, 4);
            unitpara.Value = st_pledgedetial.Unit;
            paras.Add(unitpara);

            SqlParameter pledgepricepara = new SqlParameter("@PledgePrice", SqlDbType.Decimal, 9);
            pledgepricepara.Value = st_pledgedetial.PledgePrice;
            paras.Add(pledgepricepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = st_pledgedetial.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_pledgedetial.StockLogId;
            paras.Add(stocklogidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetStockId(UserModel user, int pledgeId)
        {
            ResultModel result = new ResultModel();
            string str = string.Empty;

            try
            {
                string sql = string.Format("select StockId from NFMT.dbo.St_PledgeDetial where PledgeId = {0} and DetailStatus <> {1}", pledgeId, (int)Common.StatusEnum.已作废);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        str += dr["StockId"].ToString() + ",";
                    }
                    if (!string.IsNullOrEmpty(str))
                        str = str.Substring(0, str.Length - 1);

                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = str;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel InsertOrUpdateStatus(UserModel user, int pledgeId, int stockId)
        {
            ResultModel result = new ResultModel();

            try
            {
                List<SqlParameter> paras = new List<SqlParameter>();
                SqlParameter detailidpara = new SqlParameter();
                detailidpara.Direction = ParameterDirection.Output;
                detailidpara.SqlDbType = SqlDbType.Int;
                detailidpara.ParameterName = "@DetailId";
                detailidpara.Size = 4;
                paras.Add(detailidpara);

                SqlParameter pledgeIdpara = new SqlParameter("@pledgeId", SqlDbType.Int, 4);
                pledgeIdpara.Value = pledgeId;
                paras.Add(pledgeIdpara);

                SqlParameter stockidpara = new SqlParameter("@stockId", SqlDbType.Int, 4);
                stockidpara.Value = stockId;
                paras.Add(stockidpara);

                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, "InsertOrUpdate_St_PledgeDetial", paras.ToArray());

                if (i > 0)
                {
                    result.Message = "操作成功";
                    result.ResultStatus = 0;
                    result.AffectCount = i;
                    result.ReturnValue = detailidpara.Value;
                }
                else
                {
                    result.Message = "操作失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = string.Format("操作失败,{0}", e.Message);
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel Invalid(UserModel user, int pledgeId, string sids)
        {
            ResultModel result = new ResultModel();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            try
            {
                if (string.IsNullOrEmpty(sids))
                    sb.AppendFormat("update dbo.St_PledgeDetial set DetailStatus = {0} where PledgeId = {1}", (int)NFMT.Common.StatusEnum.已作废, pledgeId);
                else
                    sb.AppendFormat("update dbo.St_PledgeDetial set DetailStatus = {0} where PledgeId = {1} and StockId not in ({2})", (int)NFMT.Common.StatusEnum.已作废, pledgeId, sids);

                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sb.ToString(), null);

                if (i >= 0)
                {
                    result.Message = "操作成功";
                    result.ResultStatus = 0;
                    result.AffectCount = i;
                }
                else
                {
                    result.Message = "操作失败";
                    result.ResultStatus = -1;
                }

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel GetDetailId(UserModel user, int pledgeId)
        {
            ResultModel result = new ResultModel();
            string str = string.Empty;

            try
            {
                string sql = string.Format("select DetailId from NFMT.dbo.St_PledgeDetial where PledgeId = {0} and DetailStatus <> {1}", pledgeId, (int)Common.StatusEnum.已作废);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        str += dr["DetailId"].ToString() + ",";
                    }
                    if (!string.IsNullOrEmpty(str))
                        str = str.Substring(0, str.Length - 1);

                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = str;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel InvalidAll(UserModel user, int pledgeId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.St_PledgeDetial set DetailStatus = {0} where PledgeId = {1}", (int)Common.StatusEnum.已作废, pledgeId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                    result.AffectCount = i;
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

        public ResultModel GetDetailId(UserModel user, int pledgeId, int stockId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select DetailId from dbo.St_PledgeApplyDetail where PledgeApplyId = (select PledgeApplyId from dbo.St_Pledge where PledgeId = {0}) and StockId = {1}", pledgeId, stockId);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                int detailId = 0;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && int.TryParse(obj.ToString(), out detailId))
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = detailId;
                    result.Message = "获取成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel Load(UserModel user, int pledgeId, Common.StatusEnum status = Common.StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.St_PledgeDetial where PledgeId ={0} and DetailStatus>={1}", pledgeId, (int)status);
            return Load<Model.PledgeDetial>(user, CommandType.Text, cmdText);
        }

        #endregion
    }
}
