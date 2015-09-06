/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockMoveDAL.cs
// 文件功能描述：移库dbo.St_StockMove数据交互类。
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
    /// 移库dbo.St_StockMove数据交互类。
    /// </summary>
    public class StockMoveDAL : ExecOperate, IStockMoveDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockMoveDAL()
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
            StockMove st_stockmove = (StockMove)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockMoveId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stockmoveapplyidpara = new SqlParameter("@StockMoveApplyId", SqlDbType.Int, 4);
            stockmoveapplyidpara.Value = st_stockmove.StockMoveApplyId;
            paras.Add(stockmoveapplyidpara);

            SqlParameter moverpara = new SqlParameter("@Mover", SqlDbType.Int, 4);
            moverpara.Value = st_stockmove.Mover;
            paras.Add(moverpara);

            SqlParameter movetimepara = new SqlParameter("@MoveTime", SqlDbType.DateTime, 8);
            movetimepara.Value = st_stockmove.MoveTime;
            paras.Add(movetimepara);

            SqlParameter movestatuspara = new SqlParameter("@MoveStatus", SqlDbType.Int, 4);
            movestatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(movestatuspara);

            if (!string.IsNullOrEmpty(st_stockmove.MoveMemo))
            {
                SqlParameter movememopara = new SqlParameter("@MoveMemo", SqlDbType.VarChar, 4000);
                movememopara.Value = st_stockmove.MoveMemo;
                paras.Add(movememopara);
            }

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockMove stockmove = new StockMove();

            int indexStockMoveId = dr.GetOrdinal("StockMoveId");
            stockmove.StockMoveId = Convert.ToInt32(dr[indexStockMoveId]);

            int indexStockMoveApplyId = dr.GetOrdinal("StockMoveApplyId");
            stockmove.StockMoveApplyId = Convert.ToInt32(dr[indexStockMoveApplyId]);

            int indexMover = dr.GetOrdinal("Mover");
            if (dr["Mover"] != DBNull.Value)
            {
                stockmove.Mover = Convert.ToInt32(dr[indexMover]);
            }

            int indexMoveTime = dr.GetOrdinal("MoveTime");
            if (dr["MoveTime"] != DBNull.Value)
            {
                stockmove.MoveTime = Convert.ToDateTime(dr[indexMoveTime]);
            }

            int indexMoveStatus = dr.GetOrdinal("MoveStatus");
            if (dr["MoveStatus"] != DBNull.Value)
            {
                stockmove.MoveStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexMoveStatus]);
            }

            int indexMoveMemo = dr.GetOrdinal("MoveMemo");
            if (dr["MoveMemo"] != DBNull.Value)
            {
                stockmove.MoveMemo = Convert.ToString(dr[indexMoveMemo]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                stockmove.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                stockmove.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                stockmove.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                stockmove.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return stockmove;
        }

        public override string TableName
        {
            get
            {
                return "St_StockMove";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockMove st_stockmove = (StockMove)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stockmoveidpara = new SqlParameter("@StockMoveId", SqlDbType.Int, 4);
            stockmoveidpara.Value = st_stockmove.StockMoveId;
            paras.Add(stockmoveidpara);

            SqlParameter stockmoveapplyidpara = new SqlParameter("@StockMoveApplyId", SqlDbType.Int, 4);
            stockmoveapplyidpara.Value = st_stockmove.StockMoveApplyId;
            paras.Add(stockmoveapplyidpara);

            SqlParameter moverpara = new SqlParameter("@Mover", SqlDbType.Int, 4);
            moverpara.Value = st_stockmove.Mover;
            paras.Add(moverpara);

            SqlParameter movetimepara = new SqlParameter("@MoveTime", SqlDbType.DateTime, 8);
            movetimepara.Value = st_stockmove.MoveTime;
            paras.Add(movetimepara);

            SqlParameter movestatuspara = new SqlParameter("@MoveStatus", SqlDbType.Int, 4);
            movestatuspara.Value = st_stockmove.MoveStatus;
            paras.Add(movestatuspara);

            if (!string.IsNullOrEmpty(st_stockmove.MoveMemo))
            {
                SqlParameter movememopara = new SqlParameter("@MoveMemo", SqlDbType.VarChar, 4000);
                movememopara.Value = st_stockmove.MoveMemo;
                paras.Add(movememopara);
            }

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel UpdateStatus(UserModel user, int stockMoveId, int statusId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.St_StockMove set MoveStatus = {0} where StockMoveId = {1}", statusId, stockMoveId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "更新成功";
                    result.AffectCount = i;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "更新失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = string.Format("更新失败,{0}", e.Message);
            }

            return result;
        }

        public ResultModel GetStockMoveIdByApplyId(UserModel user, int stockMoveApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select StockMoveId from NFMT.dbo.St_StockMove where StockMoveApplyId = {0}", stockMoveApplyId);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                int stockMoveId = 0;
                if (obj != null && int.TryParse(obj.ToString(), out stockMoveId))
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = stockMoveId;
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

        public override int MenuId
        {
            get
            {
                return 46;
            }
        }

        #endregion
    }
}
