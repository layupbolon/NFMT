/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockMoveApplyDAL.cs
// 文件功能描述：移库申请dbo.StockMoveApply数据交互类。
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
    /// 移库申请dbo.StockMoveApply数据交互类。
    /// </summary>
    public class StockMoveApplyDAL : ApplyOperate, IStockMoveApplyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockMoveApplyDAL()
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
            StockMoveApply st_stockmoveapply = (StockMoveApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockMoveApplyId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = st_stockmoveapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockMoveApply stockmoveapply = new StockMoveApply();

            int indexStockMoveApplyId = dr.GetOrdinal("StockMoveApplyId");
            stockmoveapply.StockMoveApplyId = Convert.ToInt32(dr[indexStockMoveApplyId]);

            int indexApplyId = dr.GetOrdinal("ApplyId");
            if (dr["ApplyId"] != DBNull.Value)
            {
                stockmoveapply.ApplyId = Convert.ToInt32(dr[indexApplyId]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                stockmoveapply.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                stockmoveapply.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                stockmoveapply.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                stockmoveapply.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return stockmoveapply;
        }

        public override string TableName
        {
            get
            {
                return "St_StockMoveApply";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockMoveApply st_stockmoveapply = (StockMoveApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stockmoveapplyidpara = new SqlParameter("@StockMoveApplyId", SqlDbType.Int, 4);
            stockmoveapplyidpara.Value = st_stockmoveapply.StockMoveApplyId;
            paras.Add(stockmoveapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = st_stockmoveapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetStockMoveApplyByApplyId(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                string sql = string.Format("select * from dbo.St_StockMoveApply where ApplyId = {0}", applyId);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, sql, null);

                Model.StockMoveApply model = new StockMoveApply();

                if (dr.Read())
                {
                    model = CreateModel(dr) as StockMoveApply;

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = model;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
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

        public override int MenuId
        {
            get
            {
                return 45;
            }
        }

        #endregion
    }
}
