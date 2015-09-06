/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockOutApplyAttachDAL.cs
// 文件功能描述：出库申请附件dbo.StockOutApplyAttach数据交互类。
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
    /// 出库申请附件dbo.StockOutApplyAttach数据交互类。
    /// </summary>
    public class StockOutApplyAttachDAL : DataOperate, IStockOutApplyAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockOutApplyAttachDAL()
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
            StockOutApplyAttach st_stockoutapplyattach = (StockOutApplyAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockOutApplyAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stockoutapplyidpara = new SqlParameter("@StockOutApplyId", SqlDbType.Int, 4);
            stockoutapplyidpara.Value = st_stockoutapplyattach.StockOutApplyId;
            paras.Add(stockoutapplyidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_stockoutapplyattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockOutApplyAttach stockoutapplyattach = new StockOutApplyAttach();

            int indexStockOutApplyAttachId = dr.GetOrdinal("StockOutApplyAttachId");
            stockoutapplyattach.StockOutApplyAttachId = Convert.ToInt32(dr[indexStockOutApplyAttachId]);

            int indexStockOutApplyId = dr.GetOrdinal("StockOutApplyId");
            if (dr["StockOutApplyId"] != DBNull.Value)
            {
                stockoutapplyattach.StockOutApplyId = Convert.ToInt32(dr[indexStockOutApplyId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                stockoutapplyattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return stockoutapplyattach;
        }

        public override string TableName
        {
            get
            {
                return "St_StockOutApplyAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockOutApplyAttach st_stockoutapplyattach = (StockOutApplyAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stockoutapplyattachidpara = new SqlParameter("@StockOutApplyAttachId", SqlDbType.Int, 4);
            stockoutapplyattachidpara.Value = st_stockoutapplyattach.StockOutApplyAttachId;
            paras.Add(stockoutapplyattachidpara);

            SqlParameter stockoutapplyidpara = new SqlParameter("@StockOutApplyId", SqlDbType.Int, 4);
            stockoutapplyidpara.Value = st_stockoutapplyattach.StockOutApplyId;
            paras.Add(stockoutapplyidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_stockoutapplyattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion
    }
}
