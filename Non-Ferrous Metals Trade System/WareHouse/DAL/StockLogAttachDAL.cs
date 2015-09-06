/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockLogAttachDAL.cs
// 文件功能描述：流水附件dbo.StockLogAttach数据交互类。
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
    /// 流水附件dbo.StockLogAttach数据交互类。
    /// </summary>
    public class StockLogAttachDAL : DataOperate, IStockLogAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockLogAttachDAL()
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
            StockLogAttach st_stocklogattach = (StockLogAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockLogAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_stocklogattach.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_stocklogattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockLogAttach stocklogattach = new StockLogAttach();

            int indexStockLogAttachId = dr.GetOrdinal("StockLogAttachId");
            stocklogattach.StockLogAttachId = Convert.ToInt32(dr[indexStockLogAttachId]);

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                stocklogattach.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                stocklogattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return stocklogattach;
        }

        public override string TableName
        {
            get
            {
                return "St_StockLogAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockLogAttach st_stocklogattach = (StockLogAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stocklogattachidpara = new SqlParameter("@StockLogAttachId", SqlDbType.Int, 4);
            stocklogattachidpara.Value = st_stocklogattach.StockLogAttachId;
            paras.Add(stocklogattachidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_stocklogattach.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_stocklogattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion

    }
}
