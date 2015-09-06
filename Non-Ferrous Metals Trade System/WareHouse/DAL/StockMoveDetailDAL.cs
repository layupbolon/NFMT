/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockMoveDetailDAL.cs
// 文件功能描述：移库明细dbo.St_StockMoveDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月28日
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
    /// 移库明细dbo.St_StockMoveDetail数据交互类。
    /// </summary>
    public class StockMoveDetailDAL : DetailOperate, IStockMoveDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockMoveDetailDAL()
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
            StockMoveDetail st_stockmovedetail = (StockMoveDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stockmoveidpara = new SqlParameter("@StockMoveId", SqlDbType.Int, 4);
            stockmoveidpara.Value = st_stockmovedetail.StockMoveId;
            paras.Add(stockmoveidpara);

            SqlParameter movedetailstatuspara = new SqlParameter("@MoveDetailStatus", SqlDbType.Int, 4);
            movedetailstatuspara.Value = st_stockmovedetail.MoveDetailStatus;
            paras.Add(movedetailstatuspara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stockmovedetail.StockId;
            paras.Add(stockidpara);

            if (!string.IsNullOrEmpty(st_stockmovedetail.PaperNo))
            {
                SqlParameter papernopara = new SqlParameter("@PaperNo", SqlDbType.VarChar, 80);
                papernopara.Value = st_stockmovedetail.PaperNo;
                paras.Add(papernopara);
            }

            SqlParameter deliverplaceidpara = new SqlParameter("@DeliverPlaceId", SqlDbType.Int, 4);
            deliverplaceidpara.Value = st_stockmovedetail.DeliverPlaceId;
            paras.Add(deliverplaceidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_stockmovedetail.StockLogId;
            paras.Add(stocklogidpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            StockMoveDetail stockmovedetail = new StockMoveDetail();

            stockmovedetail.DetailId = Convert.ToInt32(dr["DetailId"]);

            if (dr["StockMoveId"] != DBNull.Value)
            {
                stockmovedetail.StockMoveId = Convert.ToInt32(dr["StockMoveId"]);
            }

            if (dr["MoveDetailStatus"] != DBNull.Value)
            {
                stockmovedetail.MoveDetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["MoveDetailStatus"]);
            }

            if (dr["StockId"] != DBNull.Value)
            {
                stockmovedetail.StockId = Convert.ToInt32(dr["StockId"]);
            }

            if (dr["PaperNo"] != DBNull.Value)
            {
                stockmovedetail.PaperNo = Convert.ToString(dr["PaperNo"]);
            }

            if (dr["DeliverPlaceId"] != DBNull.Value)
            {
                stockmovedetail.DeliverPlaceId = Convert.ToInt32(dr["DeliverPlaceId"]);
            }

            if (dr["StockLogId"] != DBNull.Value)
            {
                stockmovedetail.StockLogId = Convert.ToInt32(dr["StockLogId"]);
            }


            return stockmovedetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockMoveDetail stockmovedetail = new StockMoveDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            stockmovedetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexStockMoveId = dr.GetOrdinal("StockMoveId");
            if (dr["StockMoveId"] != DBNull.Value)
            {
                stockmovedetail.StockMoveId = Convert.ToInt32(dr[indexStockMoveId]);
            }

            int indexMoveDetailStatus = dr.GetOrdinal("MoveDetailStatus");
            if (dr["MoveDetailStatus"] != DBNull.Value)
            {
                stockmovedetail.MoveDetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexMoveDetailStatus]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                stockmovedetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexPaperNo = dr.GetOrdinal("PaperNo");
            if (dr["PaperNo"] != DBNull.Value)
            {
                stockmovedetail.PaperNo = Convert.ToString(dr[indexPaperNo]);
            }

            int indexDeliverPlaceId = dr.GetOrdinal("DeliverPlaceId");
            if (dr["DeliverPlaceId"] != DBNull.Value)
            {
                stockmovedetail.DeliverPlaceId = Convert.ToInt32(dr[indexDeliverPlaceId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                stockmovedetail.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }


            return stockmovedetail;
        }

        public override string TableName
        {
            get
            {
                return "St_StockMoveDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockMoveDetail st_stockmovedetail = (StockMoveDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = st_stockmovedetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter stockmoveidpara = new SqlParameter("@StockMoveId", SqlDbType.Int, 4);
            stockmoveidpara.Value = st_stockmovedetail.StockMoveId;
            paras.Add(stockmoveidpara);

            SqlParameter movedetailstatuspara = new SqlParameter("@MoveDetailStatus", SqlDbType.Int, 4);
            movedetailstatuspara.Value = st_stockmovedetail.MoveDetailStatus;
            paras.Add(movedetailstatuspara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stockmovedetail.StockId;
            paras.Add(stockidpara);

            if (!string.IsNullOrEmpty(st_stockmovedetail.PaperNo))
            {
                SqlParameter papernopara = new SqlParameter("@PaperNo", SqlDbType.VarChar, 80);
                papernopara.Value = st_stockmovedetail.PaperNo;
                paras.Add(papernopara);
            }

            SqlParameter deliverplaceidpara = new SqlParameter("@DeliverPlaceId", SqlDbType.Int, 4);
            deliverplaceidpara.Value = st_stockmovedetail.DeliverPlaceId;
            paras.Add(deliverplaceidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_stockmovedetail.StockLogId;
            paras.Add(stocklogidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetStockId(UserModel user, int stockMoveId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("declare @str varchar(200)");
                sb.Append(Environment.NewLine);
                sb.Append("set @str = ' '");
                sb.Append(Environment.NewLine);
                sb.AppendFormat("select @str = @str + CONVERT(varchar,StockId) + ',' from dbo.St_StockMoveDetail where StockMoveId = {0}", stockMoveId);
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
                    result.ReturnValue = obj.ToString();
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

        public ResultModel GetDetailId(UserModel user, int stockMoveId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("declare @str varchar(200)");
                sb.Append(Environment.NewLine);
                sb.Append("set @str = ' '");
                sb.Append(Environment.NewLine);
                sb.AppendFormat("select @str = @str + CONVERT(varchar,DetailId) + ',' from dbo.St_StockMoveDetail where StockMoveId = {0}", stockMoveId);
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
                    result.ReturnValue = obj.ToString();
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

        public ResultModel Load(UserModel user, int stockMoveId, Common.StatusEnum status)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                string sql = string.Format("select * from dbo.St_StockMoveDetail where StockMoveId = {0} and MoveDetailStatus = {1}", stockMoveId, (int)status);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, sql, null);

                if (!dr.HasRows)
                {
                    result.ResultStatus = -1;
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                    return result;
                }

                List<Model.StockMoveDetail> models = new List<Model.StockMoveDetail>();

                int i = 0;
                while (dr.Read())
                {
                    models.Add(CreateModel(dr) as Model.StockMoveDetail);
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
