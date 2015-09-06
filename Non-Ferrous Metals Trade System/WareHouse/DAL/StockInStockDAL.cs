/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockInStockDAL.cs
// 文件功能描述：入库登记库存关联dbo.St_StockInStock_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月11日
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
    /// 入库登记库存关联dbo.St_StockInStock_Ref数据交互类。
    /// </summary>
    public partial class StockInStockDAL : ExecOperate, IStockInStockDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockInStockDAL()
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
            StockInStock st_stockinstock_ref = (StockInStock)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stockinidpara = new SqlParameter("@StockInId", SqlDbType.Int, 4);
            stockinidpara.Value = st_stockinstock_ref.StockInId;
            paras.Add(stockinidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stockinstock_ref.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_stockinstock_ref.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = st_stockinstock_ref.RefStatus;
            paras.Add(refstatuspara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockInStock stockinstock = new StockInStock();

            int indexRefId = dr.GetOrdinal("RefId");
            stockinstock.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexStockInId = dr.GetOrdinal("StockInId");
            if (dr["StockInId"] != DBNull.Value)
            {
                stockinstock.StockInId = Convert.ToInt32(dr[indexStockInId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                stockinstock.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                stockinstock.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                stockinstock.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRefStatus]);
            }


            return stockinstock;
        }

        public override string TableName
        {
            get
            {
                return "St_StockInStock_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockInStock st_stockinstock_ref = (StockInStock)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = st_stockinstock_ref.RefId;
            paras.Add(refidpara);

            SqlParameter stockinidpara = new SqlParameter("@StockInId", SqlDbType.Int, 4);
            stockinidpara.Value = st_stockinstock_ref.StockInId;
            paras.Add(stockinidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stockinstock_ref.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_stockinstock_ref.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = st_stockinstock_ref.RefStatus;
            paras.Add(refstatuspara);


            return paras;
        }

        #endregion
        
    }
}
