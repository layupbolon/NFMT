/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockOutApplyDetailDAL.cs
// 文件功能描述：出库申请明细dbo.St_StockOutApplyDetail数据交互类。
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
    /// 出库申请明细dbo.St_StockOutApplyDetail数据交互类。
    /// </summary>
    public class StockOutApplyDetailDAL : ApplyOperate, IStockOutApplyDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockOutApplyDetailDAL()
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
            StockOutApplyDetail st_stockoutapplydetail = (StockOutApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stockoutapplyidpara = new SqlParameter("@StockOutApplyId", SqlDbType.Int, 4);
            stockoutapplyidpara.Value = st_stockoutapplydetail.StockOutApplyId;
            paras.Add(stockoutapplyidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stockoutapplydetail.StockId;
            paras.Add(stockidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = st_stockoutapplydetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = st_stockoutapplydetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = st_stockoutapplydetail.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = st_stockoutapplydetail.NetAmount;
            paras.Add(netamountpara);

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = st_stockoutapplydetail.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = st_stockoutapplydetail.Bundles;
            paras.Add(bundlespara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockOutApplyDetail stockoutapplydetail = new StockOutApplyDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            stockoutapplydetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexStockOutApplyId = dr.GetOrdinal("StockOutApplyId");
            if (dr["StockOutApplyId"] != DBNull.Value)
            {
                stockoutapplydetail.StockOutApplyId = Convert.ToInt32(dr[indexStockOutApplyId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                stockoutapplydetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                stockoutapplydetail.DetailStatus = (StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                stockoutapplydetail.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexSubContractId = dr.GetOrdinal("SubContractId");
            if (dr["SubContractId"] != DBNull.Value)
            {
                stockoutapplydetail.SubContractId = Convert.ToInt32(dr[indexSubContractId]);
            }

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            if (dr["NetAmount"] != DBNull.Value)
            {
                stockoutapplydetail.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
            }

            int indexGrossAmount = dr.GetOrdinal("GrossAmount");
            if (dr["GrossAmount"] != DBNull.Value)
            {
                stockoutapplydetail.GrossAmount = Convert.ToDecimal(dr[indexGrossAmount]);
            }

            int indexBundles = dr.GetOrdinal("Bundles");
            if (dr["Bundles"] != DBNull.Value)
            {
                stockoutapplydetail.Bundles = Convert.ToInt32(dr[indexBundles]);
            }


            return stockoutapplydetail;
        }

        public override string TableName
        {
            get
            {
                return "St_StockOutApplyDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockOutApplyDetail st_stockoutapplydetail = (StockOutApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = st_stockoutapplydetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter stockoutapplyidpara = new SqlParameter("@StockOutApplyId", SqlDbType.Int, 4);
            stockoutapplyidpara.Value = st_stockoutapplydetail.StockOutApplyId;
            paras.Add(stockoutapplyidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stockoutapplydetail.StockId;
            paras.Add(stockidpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = st_stockoutapplydetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = st_stockoutapplydetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = st_stockoutapplydetail.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = st_stockoutapplydetail.NetAmount;
            paras.Add(netamountpara);

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = st_stockoutapplydetail.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = st_stockoutapplydetail.Bundles;
            paras.Add(bundlespara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int outApplyId, Common.StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select * from dbo.St_StockOutApplyDetail where StockOutApplyId={0} and DetailStatus={1} ", outApplyId, (int)status);
                result = Load<Model.StockOutApplyDetail>(user, CommandType.Text, cmdText);               
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel LoadBySubId(UserModel user, int subId, Common.StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select * from dbo.St_StockOutApplyDetail where SubContractId={0} and DetailStatus >={1} ", subId, (int)status);
                result = Load<Model.StockOutApplyDetail>(user, CommandType.Text, cmdText); 
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        #endregion
    }
}
