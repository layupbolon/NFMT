/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockMoveApplyAttachDAL.cs
// 文件功能描述：移库申请附件dbo.StockMoveApplyAttach数据交互类。
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
    /// 移库申请附件dbo.StockMoveApplyAttach数据交互类。
    /// </summary>
    public class StockMoveApplyAttachDAL : DataOperate, IStockMoveApplyAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockMoveApplyAttachDAL()
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
            StockMoveApplyAttach st_stockmoveapplyattach = (StockMoveApplyAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockMoveApplyAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stockmoveapplyidpara = new SqlParameter("@StockMoveApplyId", SqlDbType.Int, 4);
            stockmoveapplyidpara.Value = st_stockmoveapplyattach.StockMoveApplyId;
            paras.Add(stockmoveapplyidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_stockmoveapplyattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockMoveApplyAttach stockmoveapplyattach = new StockMoveApplyAttach();

            int indexStockMoveApplyAttachId = dr.GetOrdinal("StockMoveApplyAttachId");
            stockmoveapplyattach.StockMoveApplyAttachId = Convert.ToInt32(dr[indexStockMoveApplyAttachId]);

            int indexStockMoveApplyId = dr.GetOrdinal("StockMoveApplyId");
            if (dr["StockMoveApplyId"] != DBNull.Value)
            {
                stockmoveapplyattach.StockMoveApplyId = Convert.ToInt32(dr[indexStockMoveApplyId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                stockmoveapplyattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return stockmoveapplyattach;
        }

        public override string TableName
        {
            get
            {
                return "St_StockMoveApplyAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockMoveApplyAttach st_stockmoveapplyattach = (StockMoveApplyAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stockmoveapplyattachidpara = new SqlParameter("@StockMoveApplyAttachId", SqlDbType.Int, 4);
            stockmoveapplyattachidpara.Value = st_stockmoveapplyattach.StockMoveApplyAttachId;
            paras.Add(stockmoveapplyattachidpara);

            SqlParameter stockmoveapplyidpara = new SqlParameter("@StockMoveApplyId", SqlDbType.Int, 4);
            stockmoveapplyidpara.Value = st_stockmoveapplyattach.StockMoveApplyId;
            paras.Add(stockmoveapplyidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_stockmoveapplyattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion

    }
}
