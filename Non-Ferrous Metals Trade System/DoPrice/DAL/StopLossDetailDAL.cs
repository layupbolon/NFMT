/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StopLossDetailDAL.cs
// 文件功能描述：止损明细表dbo.Pri_StopLossDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月23日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.DoPrice.Model;
using NFMT.DBUtility;
using NFMT.DoPrice.IDAL;
using NFMT.Common;

namespace NFMT.DoPrice.DAL
{
    /// <summary>
    /// 止损明细表dbo.Pri_StopLossDetail数据交互类。
    /// </summary>
    public class StopLossDetailDAL : ExecOperate, IStopLossDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StopLossDetailDAL()
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
            StopLossDetail pri_stoplossdetail = (StopLossDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stoplossidpara = new SqlParameter("@StopLossId", SqlDbType.Int, 4);
            stoplossidpara.Value = pri_stoplossdetail.StopLossId;
            paras.Add(stoplossidpara);

            SqlParameter stoplossapplyidpara = new SqlParameter("@StopLossApplyId", SqlDbType.Int, 4);
            stoplossapplyidpara.Value = pri_stoplossdetail.StopLossApplyId;
            paras.Add(stoplossapplyidpara);

            SqlParameter stoplossapplydetailidpara = new SqlParameter("@StopLossApplyDetailId", SqlDbType.Int, 4);
            stoplossapplydetailidpara.Value = pri_stoplossdetail.StopLossApplyDetailId;
            paras.Add(stoplossapplydetailidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = pri_stoplossdetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = pri_stoplossdetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter stoplossweightpara = new SqlParameter("@StopLossWeight", SqlDbType.Decimal, 9);
            stoplossweightpara.Value = pri_stoplossdetail.StopLossWeight;
            paras.Add(stoplossweightpara);

            SqlParameter avgpricepara = new SqlParameter("@AvgPrice", SqlDbType.Decimal, 9);
            avgpricepara.Value = pri_stoplossdetail.AvgPrice;
            paras.Add(avgpricepara);

            SqlParameter stoplosstimepara = new SqlParameter("@StopLossTime", SqlDbType.DateTime, 8);
            stoplosstimepara.Value = pri_stoplossdetail.StopLossTime;
            paras.Add(stoplosstimepara);

            SqlParameter stoplosserpara = new SqlParameter("@StopLosser", SqlDbType.Int, 4);
            stoplosserpara.Value = pri_stoplossdetail.StopLosser;
            paras.Add(stoplosserpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = pri_stoplossdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StopLossDetail stoplossdetail = new StopLossDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            stoplossdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexStopLossId = dr.GetOrdinal("StopLossId");
            if (dr["StopLossId"] != DBNull.Value)
            {
                stoplossdetail.StopLossId = Convert.ToInt32(dr[indexStopLossId]);
            }

            int indexStopLossApplyId = dr.GetOrdinal("StopLossApplyId");
            if (dr["StopLossApplyId"] != DBNull.Value)
            {
                stoplossdetail.StopLossApplyId = Convert.ToInt32(dr[indexStopLossApplyId]);
            }

            int indexStopLossApplyDetailId = dr.GetOrdinal("StopLossApplyDetailId");
            if (dr["StopLossApplyDetailId"] != DBNull.Value)
            {
                stoplossdetail.StopLossApplyDetailId = Convert.ToInt32(dr[indexStopLossApplyDetailId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                stoplossdetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                stoplossdetail.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexStopLossWeight = dr.GetOrdinal("StopLossWeight");
            if (dr["StopLossWeight"] != DBNull.Value)
            {
                stoplossdetail.StopLossWeight = Convert.ToDecimal(dr[indexStopLossWeight]);
            }

            int indexAvgPrice = dr.GetOrdinal("AvgPrice");
            if (dr["AvgPrice"] != DBNull.Value)
            {
                stoplossdetail.AvgPrice = Convert.ToDecimal(dr[indexAvgPrice]);
            }

            int indexStopLossTime = dr.GetOrdinal("StopLossTime");
            if (dr["StopLossTime"] != DBNull.Value)
            {
                stoplossdetail.StopLossTime = Convert.ToDateTime(dr[indexStopLossTime]);
            }

            int indexStopLosser = dr.GetOrdinal("StopLosser");
            if (dr["StopLosser"] != DBNull.Value)
            {
                stoplossdetail.StopLosser = Convert.ToInt32(dr[indexStopLosser]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                stoplossdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                stoplossdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                stoplossdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                stoplossdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                stoplossdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return stoplossdetail;
        }

        public override string TableName
        {
            get
            {
                return "Pri_StopLossDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StopLossDetail pri_stoplossdetail = (StopLossDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = pri_stoplossdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter stoplossidpara = new SqlParameter("@StopLossId", SqlDbType.Int, 4);
            stoplossidpara.Value = pri_stoplossdetail.StopLossId;
            paras.Add(stoplossidpara);

            SqlParameter stoplossapplyidpara = new SqlParameter("@StopLossApplyId", SqlDbType.Int, 4);
            stoplossapplyidpara.Value = pri_stoplossdetail.StopLossApplyId;
            paras.Add(stoplossapplyidpara);

            SqlParameter stoplossapplydetailidpara = new SqlParameter("@StopLossApplyDetailId", SqlDbType.Int, 4);
            stoplossapplydetailidpara.Value = pri_stoplossdetail.StopLossApplyDetailId;
            paras.Add(stoplossapplydetailidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = pri_stoplossdetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = pri_stoplossdetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter stoplossweightpara = new SqlParameter("@StopLossWeight", SqlDbType.Decimal, 9);
            stoplossweightpara.Value = pri_stoplossdetail.StopLossWeight;
            paras.Add(stoplossweightpara);

            SqlParameter avgpricepara = new SqlParameter("@AvgPrice", SqlDbType.Decimal, 9);
            avgpricepara.Value = pri_stoplossdetail.AvgPrice;
            paras.Add(avgpricepara);

            SqlParameter stoplosstimepara = new SqlParameter("@StopLossTime", SqlDbType.DateTime, 8);
            stoplosstimepara.Value = pri_stoplossdetail.StopLossTime;
            paras.Add(stoplosstimepara);

            SqlParameter stoplosserpara = new SqlParameter("@StopLosser", SqlDbType.Int, 4);
            stoplosserpara.Value = pri_stoplossdetail.StopLosser;
            paras.Add(stoplosserpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = pri_stoplossdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetCanStopLossApplyDetailIds(UserModel user, int stopLossId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select DetailId from dbo.Pri_StopLossApplyDetail detail ");
                sb.AppendFormat(" where detail.StopLossApplyId = (select StopLossApplyId from dbo.Pri_StopLoss where StopLossId = {0}) and detail.DetailStatus <> {1} ", stopLossId, (int)Common.StatusEnum.已作废);
                sb.AppendFormat(" and detail.DetailId not in (select StopLossApplyDetailId from dbo.Pri_StopLossDetail a where a.StopLossId = {0} and a.DetailStatus <> {1})", stopLossId, (int)Common.StatusEnum.已作废);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sb.ToString(), null, CommandType.Text);
                if (dt != null && dt.Rows.Count >= 0)
                {
                    string str = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        str += dr["DetailId"] + ",";
                    }
                    if (!string.IsNullOrEmpty(str))
                        str = str.Substring(0, str.Length - 1);

                    result.ResultStatus = 0;
                    result.ReturnValue = str;
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
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel GetStopLossApplyDetailIds(UserModel user, int stopLossId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select StopLossApplyDetailId from dbo.Pri_StopLossDetail where StopLossId = {0} and DetailStatus >= {1}", stopLossId, (int)Common.StatusEnum.已生效);
                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string str = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        str += dr["StopLossApplyDetailId"] + ",";
                    }
                    if (!string.IsNullOrEmpty(str))
                        str = str.Substring(0, str.Length - 1);

                    result.ResultStatus = 0;
                    result.ReturnValue = str;
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
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel InvalidAll(UserModel user, int stopLossId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Pri_StopLossDetail set DetailStatus = {0} where StopLossId ={1} ", (int)Common.StatusEnum.已作废, stopLossId);
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
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel Load(UserModel user, int stopLossId, Common.StatusEnum status = Common.StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            SqlDataReader dr = null;

            try
            {
                string cmdText = string.Format("select * from dbo.Pri_StopLossDetail where StopLossId ={0} and DetailStatus>={1}", stopLossId, (int)status);

                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, null);

                List<Model.StopLossDetail> stopLossDetails = new List<Model.StopLossDetail>();

                int i = 0;
                while (dr.Read())
                {
                    stopLossDetails.Add(CreateModel(dr) as Model.StopLossDetail);
                    i++;
                }

                result.AffectCount = i;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = stopLossDetails;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null) dr.Dispose();
            }

            return result;
        }

        #endregion
    }
}
