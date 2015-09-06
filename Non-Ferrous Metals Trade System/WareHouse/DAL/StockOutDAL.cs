/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockOutDAL.cs
// 文件功能描述：出库dbo.St_StockOut数据交互类。
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
    /// 出库dbo.St_StockOut数据交互类。
    /// </summary>
    public partial class StockOutDAL : ExecOperate, IStockOutDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockOutDAL()
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
            StockOut st_stockout = (StockOut)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockOutId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stockoutapplyidpara = new SqlParameter("@StockOutApplyId", SqlDbType.Int, 4);
            stockoutapplyidpara.Value = st_stockout.StockOutApplyId;
            paras.Add(stockoutapplyidpara);

            SqlParameter executorpara = new SqlParameter("@Executor", SqlDbType.Int, 4);
            executorpara.Value = st_stockout.Executor;
            paras.Add(executorpara);

            SqlParameter confirmorpara = new SqlParameter("@Confirmor", SqlDbType.Int, 4);
            confirmorpara.Value = st_stockout.Confirmor;
            paras.Add(confirmorpara);

            SqlParameter stockouttimepara = new SqlParameter("@StockOutTime", SqlDbType.DateTime, 8);
            stockouttimepara.Value = st_stockout.StockOutTime;
            paras.Add(stockouttimepara);

            SqlParameter grosstamountpara = new SqlParameter("@GrosstAmount", SqlDbType.Decimal, 9);
            grosstamountpara.Value = st_stockout.GrosstAmount;
            paras.Add(grosstamountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = st_stockout.NetAmount;
            paras.Add(netamountpara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = st_stockout.Bundles;
            paras.Add(bundlespara);

            SqlParameter unitpara = new SqlParameter("@Unit", SqlDbType.Int, 4);
            unitpara.Value = st_stockout.Unit;
            paras.Add(unitpara);

            SqlParameter stockoperatetypepara = new SqlParameter("@StockOperateType", SqlDbType.Int, 4);
            stockoperatetypepara.Value = st_stockout.StockOperateType;
            paras.Add(stockoperatetypepara);

            if (!string.IsNullOrEmpty(st_stockout.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = st_stockout.Memo;
                paras.Add(memopara);
            }

            SqlParameter stockoutstatuspara = new SqlParameter("@StockOutStatus", SqlDbType.Int, 4);
            stockoutstatuspara.Value = st_stockout.StockOutStatus;
            paras.Add(stockoutstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockOut stockout = new StockOut();

            int indexStockOutId = dr.GetOrdinal("StockOutId");
            stockout.StockOutId = Convert.ToInt32(dr[indexStockOutId]);

            int indexStockOutApplyId = dr.GetOrdinal("StockOutApplyId");
            if (dr["StockOutApplyId"] != DBNull.Value)
            {
                stockout.StockOutApplyId = Convert.ToInt32(dr[indexStockOutApplyId]);
            }

            int indexExecutor = dr.GetOrdinal("Executor");
            if (dr["Executor"] != DBNull.Value)
            {
                stockout.Executor = Convert.ToInt32(dr[indexExecutor]);
            }

            int indexConfirmor = dr.GetOrdinal("Confirmor");
            if (dr["Confirmor"] != DBNull.Value)
            {
                stockout.Confirmor = Convert.ToInt32(dr[indexConfirmor]);
            }

            int indexStockOutTime = dr.GetOrdinal("StockOutTime");
            if (dr["StockOutTime"] != DBNull.Value)
            {
                stockout.StockOutTime = Convert.ToDateTime(dr[indexStockOutTime]);
            }

            int indexGrosstAmount = dr.GetOrdinal("GrosstAmount");
            if (dr["GrosstAmount"] != DBNull.Value)
            {
                stockout.GrosstAmount = Convert.ToDecimal(dr[indexGrosstAmount]);
            }

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            if (dr["NetAmount"] != DBNull.Value)
            {
                stockout.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
            }

            int indexBundles = dr.GetOrdinal("Bundles");
            if (dr["Bundles"] != DBNull.Value)
            {
                stockout.Bundles = Convert.ToInt32(dr[indexBundles]);
            }

            int indexUnit = dr.GetOrdinal("Unit");
            if (dr["Unit"] != DBNull.Value)
            {
                stockout.Unit = Convert.ToInt32(dr[indexUnit]);
            }

            int indexStockOperateType = dr.GetOrdinal("StockOperateType");
            if (dr["StockOperateType"] != DBNull.Value)
            {
                stockout.StockOperateType = Convert.ToInt32(dr[indexStockOperateType]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                stockout.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexStockOutStatus = dr.GetOrdinal("StockOutStatus");
            if (dr["StockOutStatus"] != DBNull.Value)
            {
                stockout.StockOutStatus = (StatusEnum)Convert.ToInt32(dr[indexStockOutStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                stockout.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                stockout.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                stockout.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                stockout.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return stockout;
        }

        public override string TableName
        {
            get
            {
                return "St_StockOut";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockOut st_stockout = (StockOut)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stockoutidpara = new SqlParameter("@StockOutId", SqlDbType.Int, 4);
            stockoutidpara.Value = st_stockout.StockOutId;
            paras.Add(stockoutidpara);

            SqlParameter stockoutapplyidpara = new SqlParameter("@StockOutApplyId", SqlDbType.Int, 4);
            stockoutapplyidpara.Value = st_stockout.StockOutApplyId;
            paras.Add(stockoutapplyidpara);

            SqlParameter executorpara = new SqlParameter("@Executor", SqlDbType.Int, 4);
            executorpara.Value = st_stockout.Executor;
            paras.Add(executorpara);

            SqlParameter confirmorpara = new SqlParameter("@Confirmor", SqlDbType.Int, 4);
            confirmorpara.Value = st_stockout.Confirmor;
            paras.Add(confirmorpara);

            SqlParameter stockouttimepara = new SqlParameter("@StockOutTime", SqlDbType.DateTime, 8);
            stockouttimepara.Value = st_stockout.StockOutTime;
            paras.Add(stockouttimepara);

            SqlParameter grosstamountpara = new SqlParameter("@GrosstAmount", SqlDbType.Decimal, 9);
            grosstamountpara.Value = st_stockout.GrosstAmount;
            paras.Add(grosstamountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = st_stockout.NetAmount;
            paras.Add(netamountpara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = st_stockout.Bundles;
            paras.Add(bundlespara);

            SqlParameter unitpara = new SqlParameter("@Unit", SqlDbType.Int, 4);
            unitpara.Value = st_stockout.Unit;
            paras.Add(unitpara);

            SqlParameter stockoperatetypepara = new SqlParameter("@StockOperateType", SqlDbType.Int, 4);
            stockoperatetypepara.Value = st_stockout.StockOperateType;
            paras.Add(stockoperatetypepara);

            if (!string.IsNullOrEmpty(st_stockout.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = st_stockout.Memo;
                paras.Add(memopara);
            }

            SqlParameter stockoutstatuspara = new SqlParameter("@StockOutStatus", SqlDbType.Int, 4);
            stockoutstatuspara.Value = st_stockout.StockOutStatus;
            paras.Add(stockoutstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
