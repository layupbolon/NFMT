/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockMoveAttachDAL.cs
// 文件功能描述：移库附件dbo.StockMoveAttach数据交互类。
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
    /// 移库附件dbo.StockMoveAttach数据交互类。
    /// </summary>
    public class StockMoveAttachDAL : DataOperate, IStockMoveAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockMoveAttachDAL()
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
            StockMoveAttach st_stockmoveattach = (StockMoveAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockMoveAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stockmoveidpara = new SqlParameter("@StockMoveId", SqlDbType.Int, 4);
            stockmoveidpara.Value = st_stockmoveattach.StockMoveId;
            paras.Add(stockmoveidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_stockmoveattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockMoveAttach stockmoveattach = new StockMoveAttach();

            int indexStockMoveAttachId = dr.GetOrdinal("StockMoveAttachId");
            stockmoveattach.StockMoveAttachId = Convert.ToInt32(dr[indexStockMoveAttachId]);

            int indexStockMoveId = dr.GetOrdinal("StockMoveId");
            if (dr["StockMoveId"] != DBNull.Value)
            {
                stockmoveattach.StockMoveId = Convert.ToInt32(dr[indexStockMoveId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                stockmoveattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return stockmoveattach;
        }

        public override string TableName
        {
            get
            {
                return "St_StockMoveAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockMoveAttach st_stockmoveattach = (StockMoveAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stockmoveattachidpara = new SqlParameter("@StockMoveAttachId", SqlDbType.Int, 4);
            stockmoveattachidpara.Value = st_stockmoveattach.StockMoveAttachId;
            paras.Add(stockmoveattachidpara);

            SqlParameter stockmoveidpara = new SqlParameter("@StockMoveId", SqlDbType.Int, 4);
            stockmoveidpara.Value = st_stockmoveattach.StockMoveId;
            paras.Add(stockmoveidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_stockmoveattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion
    }
}
