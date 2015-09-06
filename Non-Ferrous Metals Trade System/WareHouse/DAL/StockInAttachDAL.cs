/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockInAttachDAL.cs
// 文件功能描述：入库登记附件dbo.St_StockInAttach数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
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
    /// 入库登记附件dbo.St_StockInAttach数据交互类。
    /// </summary>
    public partial class StockInAttachDAL : DataOperate, IStockInAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockInAttachDAL()
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
            StockInAttach st_stockinattach = (StockInAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockInAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stockinidpara = new SqlParameter("@StockInId", SqlDbType.Int, 4);
            stockinidpara.Value = st_stockinattach.StockInId;
            paras.Add(stockinidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_stockinattach.AttachId;
            paras.Add(attachidpara);

            SqlParameter attachtypepara = new SqlParameter("@AttachType", SqlDbType.Int, 4);
            attachtypepara.Value = st_stockinattach.AttachType;
            paras.Add(attachtypepara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockInAttach stockinattach = new StockInAttach();

            int indexStockInAttachId = dr.GetOrdinal("StockInAttachId");
            stockinattach.StockInAttachId = Convert.ToInt32(dr[indexStockInAttachId]);

            int indexStockInId = dr.GetOrdinal("StockInId");
            if (dr["StockInId"] != DBNull.Value)
            {
                stockinattach.StockInId = Convert.ToInt32(dr[indexStockInId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                stockinattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }

            int indexAttachType = dr.GetOrdinal("AttachType");
            if (dr["AttachType"] != DBNull.Value)
            {
                stockinattach.AttachType = Convert.ToInt32(dr[indexAttachType]);
            }


            return stockinattach;
        }

        public override string TableName
        {
            get
            {
                return "St_StockInAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockInAttach st_stockinattach = (StockInAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stockinattachidpara = new SqlParameter("@StockInAttachId", SqlDbType.Int, 4);
            stockinattachidpara.Value = st_stockinattach.StockInAttachId;
            paras.Add(stockinattachidpara);

            SqlParameter stockinidpara = new SqlParameter("@StockInId", SqlDbType.Int, 4);
            stockinidpara.Value = st_stockinattach.StockInId;
            paras.Add(stockinidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_stockinattach.AttachId;
            paras.Add(attachidpara);

            SqlParameter attachtypepara = new SqlParameter("@AttachType", SqlDbType.Int, 4);
            attachtypepara.Value = st_stockinattach.AttachType;
            paras.Add(attachtypepara);


            return paras;
        }

        #endregion
    }
}
