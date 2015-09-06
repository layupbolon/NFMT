/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockOutAttachDAL.cs
// 文件功能描述：出库申请附件dbo.StockOutAttach数据交互类。
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
    /// 出库申请附件dbo.StockOutAttach数据交互类。
    /// </summary>
    public class StockOutAttachDAL : DataOperate, IStockOutAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockOutAttachDAL()
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
            StockOutAttach st_stockoutattach = (StockOutAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockOutAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stockoutidpara = new SqlParameter("@StockOutId", SqlDbType.Int, 4);
            stockoutidpara.Value = st_stockoutattach.StockOutId;
            paras.Add(stockoutidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_stockoutattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockOutAttach stockoutattach = new StockOutAttach();

            int indexStockOutAttachId = dr.GetOrdinal("StockOutAttachId");
            stockoutattach.StockOutAttachId = Convert.ToInt32(dr[indexStockOutAttachId]);

            int indexStockOutId = dr.GetOrdinal("StockOutId");
            if (dr["StockOutId"] != DBNull.Value)
            {
                stockoutattach.StockOutId = Convert.ToInt32(dr[indexStockOutId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                stockoutattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return stockoutattach;
        }

        public override string TableName
        {
            get
            {
                return "St_StockOutAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockOutAttach st_stockoutattach = (StockOutAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stockoutattachidpara = new SqlParameter("@StockOutAttachId", SqlDbType.Int, 4);
            stockoutattachidpara.Value = st_stockoutattach.StockOutAttachId;
            paras.Add(stockoutattachidpara);

            SqlParameter stockoutidpara = new SqlParameter("@StockOutId", SqlDbType.Int, 4);
            stockoutidpara.Value = st_stockoutattach.StockOutId;
            paras.Add(stockoutidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_stockoutattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion
    }
}
