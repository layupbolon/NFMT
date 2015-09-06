/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StocktAttachDAL.cs
// 文件功能描述：库存附件dbo.StocktAttach数据交互类。
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
    /// 库存附件dbo.StocktAttach数据交互类。
    /// </summary>
    public class StocktAttachDAL : DataOperate, IStocktAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StocktAttachDAL()
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
            StocktAttach st_stocktattach = (StocktAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stocktattach.StockId;
            paras.Add(stockidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_stocktattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StocktAttach stocktattach = new StocktAttach();

            int indexStockAttachId = dr.GetOrdinal("StockAttachId");
            stocktattach.StockAttachId = Convert.ToInt32(dr[indexStockAttachId]);

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                stocktattach.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                stocktattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return stocktattach;
        }

        public override string TableName
        {
            get
            {
                return "St_StocktAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StocktAttach st_stocktattach = (StocktAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stockattachidpara = new SqlParameter("@StockAttachId", SqlDbType.Int, 4);
            stockattachidpara.Value = st_stocktattach.StockAttachId;
            paras.Add(stockattachidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stocktattach.StockId;
            paras.Add(stockidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_stocktattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion
    }
}
