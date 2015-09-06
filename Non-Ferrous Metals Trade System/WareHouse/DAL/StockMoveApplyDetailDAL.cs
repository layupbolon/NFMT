/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockMoveApplyDetailDAL.cs
// 文件功能描述：回购申请库存明细dbo.StockMoveApplyDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
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
    /// 回购申请库存明细dbo.StockMoveApplyDetail数据交互类。
    /// </summary>
    public class StockMoveApplyDetailDAL : DataOperate, IStockMoveApplyDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockMoveApplyDetailDAL()
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
            StockMoveApplyDetail st_stockmoveapplydetail = (StockMoveApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stockmoveapplyidpara = new SqlParameter("@StockMoveApplyId", SqlDbType.Int, 4);
            stockmoveapplyidpara.Value = st_stockmoveapplydetail.StockMoveApplyId;
            paras.Add(stockmoveapplyidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stockmoveapplydetail.StockId;
            paras.Add(stockidpara);

            if (!string.IsNullOrEmpty(st_stockmoveapplydetail.PaperNo))
            {
                SqlParameter papernopara = new SqlParameter("@PaperNo", SqlDbType.VarChar, 80);
                papernopara.Value = st_stockmoveapplydetail.PaperNo;
                paras.Add(papernopara);
            }

            SqlParameter deliverplaceidpara = new SqlParameter("@DeliverPlaceId", SqlDbType.Int, 4);
            deliverplaceidpara.Value = st_stockmoveapplydetail.DeliverPlaceId;
            paras.Add(deliverplaceidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            StockMoveApplyDetail stockmoveapplydetail = new StockMoveApplyDetail();

            stockmoveapplydetail.DetailId = Convert.ToInt32(dr["DetailId"]);

            stockmoveapplydetail.StockMoveApplyId = Convert.ToInt32(dr["StockMoveApplyId"]);

            if (dr["StockId"] != DBNull.Value)
            {
                stockmoveapplydetail.StockId = Convert.ToInt32(dr["StockId"]);
            }

            if (dr["PaperNo"] != DBNull.Value)
            {
                stockmoveapplydetail.PaperNo = Convert.ToString(dr["PaperNo"]);
            }

            if (dr["DeliverPlaceId"] != DBNull.Value)
            {
                stockmoveapplydetail.DeliverPlaceId = Convert.ToInt32(dr["DeliverPlaceId"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                stockmoveapplydetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }


            return stockmoveapplydetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockMoveApplyDetail stockmoveapplydetail = new StockMoveApplyDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            stockmoveapplydetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexStockMoveApplyId = dr.GetOrdinal("StockMoveApplyId");
            stockmoveapplydetail.StockMoveApplyId = Convert.ToInt32(dr[indexStockMoveApplyId]);

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                stockmoveapplydetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexPaperNo = dr.GetOrdinal("PaperNo");
            if (dr["PaperNo"] != DBNull.Value)
            {
                stockmoveapplydetail.PaperNo = Convert.ToString(dr[indexPaperNo]);
            }

            int indexDeliverPlaceId = dr.GetOrdinal("DeliverPlaceId");
            if (dr["DeliverPlaceId"] != DBNull.Value)
            {
                stockmoveapplydetail.DeliverPlaceId = Convert.ToInt32(dr[indexDeliverPlaceId]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                stockmoveapplydetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }


            return stockmoveapplydetail;
        }

        public override string TableName
        {
            get
            {
                return "St_StockMoveApplyDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockMoveApplyDetail st_stockmoveapplydetail = (StockMoveApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = st_stockmoveapplydetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter stockmoveapplyidpara = new SqlParameter("@StockMoveApplyId", SqlDbType.Int, 4);
            stockmoveapplyidpara.Value = st_stockmoveapplydetail.StockMoveApplyId;
            paras.Add(stockmoveapplyidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stockmoveapplydetail.StockId;
            paras.Add(stockidpara);

            if (!string.IsNullOrEmpty(st_stockmoveapplydetail.PaperNo))
            {
                SqlParameter papernopara = new SqlParameter("@PaperNo", SqlDbType.VarChar, 80);
                papernopara.Value = st_stockmoveapplydetail.PaperNo;
                paras.Add(papernopara);
            }

            SqlParameter deliverplaceidpara = new SqlParameter("@DeliverPlaceId", SqlDbType.Int, 4);
            deliverplaceidpara.Value = st_stockmoveapplydetail.DeliverPlaceId;
            paras.Add(deliverplaceidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = st_stockmoveapplydetail.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetStockId(UserModel user, int stockMoveApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("declare @str varchar(200)");
                sb.Append(Environment.NewLine);
                sb.Append("set @str = ' '");
                sb.Append(Environment.NewLine);
                sb.AppendFormat("select @str = @str + CONVERT(varchar,StockId) + ',' from dbo.St_StockMoveApplyDetail where StockMoveApplyId = {0} and DetailStatus ={1}", stockMoveApplyId, (int)Common.StatusEnum.已生效);
                sb.Append(Environment.NewLine);
                sb.Append("if LEN(LTRIM(@str)) > 1");
                sb.Append(Environment.NewLine);
                sb.Append("	select SUBSTRING(@str,1,LEN(@str)-1)");
                sb.Append(Environment.NewLine);
                sb.Append("else");
                sb.Append(Environment.NewLine);
                sb.Append("	select ''");

                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sb.ToString(), null);
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                {
                    result.Message = "获取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = obj.ToString().Trim();
                }
                else
                {
                    result.Message = "获取失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = string.Format("获取失败,{0}", e.Message);
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel InsertOrUpdateStatus(UserModel user, int stockMoveApplyId, int stockId)
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

                SqlParameter stockmoveapplyidpara = new SqlParameter("@stockMoveApplyId", SqlDbType.Int, 4);
                stockmoveapplyidpara.Value = stockMoveApplyId;
                paras.Add(stockmoveapplyidpara);

                SqlParameter stockidpara = new SqlParameter("@stockId", SqlDbType.Int, 4);
                stockidpara.Value = stockId;
                paras.Add(stockidpara);

                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, "InsertOrUpdate_St_StockMoveApplyDetail", paras.ToArray());

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

        public ResultModel Invalid(UserModel user, int stockMoveApplyId, string sids)
        {
            ResultModel result = new ResultModel();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            try
            {
                if (string.IsNullOrEmpty(sids))
                    sb.AppendFormat("update dbo.St_StockMoveApplyDetail set DetailStatus = {0} where StockMoveApplyId = {1}", (int)NFMT.Common.StatusEnum.已作废, stockMoveApplyId);
                else
                    sb.AppendFormat("update dbo.St_StockMoveApplyDetail set DetailStatus = {0} where StockMoveApplyId = {1} and StockId not in ({2})", (int)NFMT.Common.StatusEnum.已作废, stockMoveApplyId, sids);

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

        public ResultModel InvalidAll(UserModel user, int stockMoveApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.St_StockMoveApplyDetail set DetailStatus = {0} where StockMoveApplyId = {1}", (int)Common.StatusEnum.已作废, stockMoveApplyId);
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

        public ResultModel Load(UserModel user,int stockMoveApplyId,Common.StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                string sql = string.Format("select * from dbo.St_StockMoveApplyDetail where StockMoveApplyId = {0} and DetailStatus = {1}", stockMoveApplyId, (int)status);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, sql, null);

                List<Model.StockMoveApplyDetail> models = new List<Model.StockMoveApplyDetail>();

                int i = 0;
                while (dr.Read())
                {
                    models.Add(CreateModel<Model.StockMoveApplyDetail>(dr));
                    i++;
                }

                result.AffectCount = i;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = models;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        #endregion
    }
}
