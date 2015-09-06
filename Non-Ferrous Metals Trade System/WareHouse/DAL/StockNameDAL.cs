/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockNameDAL.cs
// 文件功能描述：业务单号表dbo.St_StockName数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月30日
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
    /// 业务单号表dbo.St_StockName数据交互类。
    /// </summary>
    public class StockNameDAL : DataOperate, IStockNameDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockNameDAL()
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
            StockName st_stockname = (StockName)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockNameId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(st_stockname.RefNo))
            {
                SqlParameter refnopara = new SqlParameter("@RefNo", SqlDbType.VarChar, 50);
                refnopara.Value = st_stockname.RefNo;
                paras.Add(refnopara);
            }


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockName stockname = new StockName();

            int indexStockNameId = dr.GetOrdinal("StockNameId");
            stockname.StockNameId = Convert.ToInt32(dr[indexStockNameId]);

            int indexRefNo = dr.GetOrdinal("RefNo");
            if (dr["RefNo"] != DBNull.Value)
            {
                stockname.RefNo = Convert.ToString(dr[indexRefNo]);
            }


            return stockname;
        }

        public override string TableName
        {
            get
            {
                return "St_StockName";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockName st_stockname = (StockName)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stocknameidpara = new SqlParameter("@StockNameId", SqlDbType.Int, 4);
            stocknameidpara.Value = st_stockname.StockNameId;
            paras.Add(stocknameidpara);

            if (!string.IsNullOrEmpty(st_stockname.RefNo))
            {
                SqlParameter refnopara = new SqlParameter("@RefNo", SqlDbType.VarChar, 50);
                refnopara.Value = st_stockname.RefNo;
                paras.Add(refnopara);
            }


            return paras;
        }

        #endregion
    }
}
