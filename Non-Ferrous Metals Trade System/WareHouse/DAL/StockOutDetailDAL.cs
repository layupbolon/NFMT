/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockOutDetailDAL.cs
// 文件功能描述：出库明细dbo.St_StockOutDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年12月16日
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
    /// 出库明细dbo.St_StockOutDetail数据交互类。
    /// </summary>
    public class StockOutDetailDAL : ExecOperate, IStockOutDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockOutDetailDAL()
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
            StockOutDetail st_stockoutdetail = (StockOutDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stockoutidpara = new SqlParameter("@StockOutId", SqlDbType.Int, 4);
            stockoutidpara.Value = st_stockoutdetail.StockOutId;
            paras.Add(stockoutidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = st_stockoutdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stockoutdetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stockoutapplydetailidpara = new SqlParameter("@StockOutApplyDetailId", SqlDbType.Int, 4);
            stockoutapplydetailidpara.Value = st_stockoutdetail.StockOutApplyDetailId;
            paras.Add(stockoutapplydetailidpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = st_stockoutdetail.NetAmount;
            paras.Add(netamountpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_stockoutdetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = st_stockoutdetail.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = st_stockoutdetail.Bundles;
            paras.Add(bundlespara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockOutDetail stockoutdetail = new StockOutDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            stockoutdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexStockOutId = dr.GetOrdinal("StockOutId");
            stockoutdetail.StockOutId = Convert.ToInt32(dr[indexStockOutId]);

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                stockoutdetail.DetailStatus = (StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                stockoutdetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockOutApplyDetailId = dr.GetOrdinal("StockOutApplyDetailId");
            if (dr["StockOutApplyDetailId"] != DBNull.Value)
            {
                stockoutdetail.StockOutApplyDetailId = Convert.ToInt32(dr[indexStockOutApplyDetailId]);
            }

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            if (dr["NetAmount"] != DBNull.Value)
            {
                stockoutdetail.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                stockoutdetail.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexGrossAmount = dr.GetOrdinal("GrossAmount");
            if (dr["GrossAmount"] != DBNull.Value)
            {
                stockoutdetail.GrossAmount = Convert.ToDecimal(dr[indexGrossAmount]);
            }

            int indexBundles = dr.GetOrdinal("Bundles");
            if (dr["Bundles"] != DBNull.Value)
            {
                stockoutdetail.Bundles = Convert.ToInt32(dr[indexBundles]);
            }


            return stockoutdetail;
        }

        public override string TableName
        {
            get
            {
                return "St_StockOutDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockOutDetail st_stockoutdetail = (StockOutDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = st_stockoutdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter stockoutidpara = new SqlParameter("@StockOutId", SqlDbType.Int, 4);
            stockoutidpara.Value = st_stockoutdetail.StockOutId;
            paras.Add(stockoutidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = st_stockoutdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stockoutdetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stockoutapplydetailidpara = new SqlParameter("@StockOutApplyDetailId", SqlDbType.Int, 4);
            stockoutapplydetailidpara.Value = st_stockoutdetail.StockOutApplyDetailId;
            paras.Add(stockoutapplydetailidpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = st_stockoutdetail.NetAmount;
            paras.Add(netamountpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_stockoutdetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = st_stockoutdetail.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = st_stockoutdetail.Bundles;
            paras.Add(bundlespara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int stockOutId, Common.StatusEnum status = Common.StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select * from dbo.St_StockOutDetail where StockOutId ={0} and DetailStatus>={1}", stockOutId, (int)status);

                result = Load<Model.StockOutDetail>(user, CommandType.Text, cmdText);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public override ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate)
        {
            Model.StockOutDetail detail = obj as Model.StockOutDetail;
            if (detail != null && operate == OperateEnum.修改 && detail.StockLogId > 0)
            {
                ResultModel result = new ResultModel();
                result.ResultStatus = 0;
                return result;
            }

            return base.AllowOperate(user, obj, operate);
        }

        #endregion
    }
}
