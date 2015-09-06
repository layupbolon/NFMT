/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockOutApplyDAL.cs
// 文件功能描述：出库申请dbo.St_StockOutApply数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年1月27日
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
    /// 出库申请dbo.St_StockOutApply数据交互类。
    /// </summary>
    public partial class StockOutApplyDAL : ApplyOperate, IStockOutApplyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockOutApplyDAL()
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
            StockOutApply st_stockoutapply = (StockOutApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockOutApplyId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = st_stockoutapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = st_stockoutapply.ContractId;
            paras.Add(contractidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = st_stockoutapply.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = st_stockoutapply.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = st_stockoutapply.NetAmount;
            paras.Add(netamountpara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = st_stockoutapply.Bundles;
            paras.Add(bundlespara);

            SqlParameter unitidpara = new SqlParameter("@UnitId", SqlDbType.Int, 4);
            unitidpara.Value = st_stockoutapply.UnitId;
            paras.Add(unitidpara);

            SqlParameter buycorpidpara = new SqlParameter("@BuyCorpId", SqlDbType.Int, 4);
            buycorpidpara.Value = st_stockoutapply.BuyCorpId;
            paras.Add(buycorpidpara);

            SqlParameter createfrompara = new SqlParameter("@CreateFrom", SqlDbType.Int, 4);
            createfrompara.Value = st_stockoutapply.CreateFrom;
            paras.Add(createfrompara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockOutApply stockoutapply = new StockOutApply();

            int indexStockOutApplyId = dr.GetOrdinal("StockOutApplyId");
            stockoutapply.StockOutApplyId = Convert.ToInt32(dr[indexStockOutApplyId]);

            int indexApplyId = dr.GetOrdinal("ApplyId");
            if (dr["ApplyId"] != DBNull.Value)
            {
                stockoutapply.ApplyId = Convert.ToInt32(dr[indexApplyId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                stockoutapply.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexSubContractId = dr.GetOrdinal("SubContractId");
            if (dr["SubContractId"] != DBNull.Value)
            {
                stockoutapply.SubContractId = Convert.ToInt32(dr[indexSubContractId]);
            }

            int indexGrossAmount = dr.GetOrdinal("GrossAmount");
            if (dr["GrossAmount"] != DBNull.Value)
            {
                stockoutapply.GrossAmount = Convert.ToDecimal(dr[indexGrossAmount]);
            }

            int indexNetAmount = dr.GetOrdinal("NetAmount");
            if (dr["NetAmount"] != DBNull.Value)
            {
                stockoutapply.NetAmount = Convert.ToDecimal(dr[indexNetAmount]);
            }

            int indexBundles = dr.GetOrdinal("Bundles");
            if (dr["Bundles"] != DBNull.Value)
            {
                stockoutapply.Bundles = Convert.ToInt32(dr[indexBundles]);
            }

            int indexUnitId = dr.GetOrdinal("UnitId");
            if (dr["UnitId"] != DBNull.Value)
            {
                stockoutapply.UnitId = Convert.ToInt32(dr[indexUnitId]);
            }

            int indexBuyCorpId = dr.GetOrdinal("BuyCorpId");
            if (dr["BuyCorpId"] != DBNull.Value)
            {
                stockoutapply.BuyCorpId = Convert.ToInt32(dr[indexBuyCorpId]);
            }

            int indexCreateFrom = dr.GetOrdinal("CreateFrom");
            if (dr["CreateFrom"] != DBNull.Value)
            {
                stockoutapply.CreateFrom = Convert.ToInt32(dr[indexCreateFrom]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                stockoutapply.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                stockoutapply.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                stockoutapply.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                stockoutapply.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return stockoutapply;
        }

        public override string TableName
        {
            get
            {
                return "St_StockOutApply";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockOutApply st_stockoutapply = (StockOutApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stockoutapplyidpara = new SqlParameter("@StockOutApplyId", SqlDbType.Int, 4);
            stockoutapplyidpara.Value = st_stockoutapply.StockOutApplyId;
            paras.Add(stockoutapplyidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = st_stockoutapply.ApplyId;
            paras.Add(applyidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = st_stockoutapply.ContractId;
            paras.Add(contractidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = st_stockoutapply.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter grossamountpara = new SqlParameter("@GrossAmount", SqlDbType.Decimal, 9);
            grossamountpara.Value = st_stockoutapply.GrossAmount;
            paras.Add(grossamountpara);

            SqlParameter netamountpara = new SqlParameter("@NetAmount", SqlDbType.Decimal, 9);
            netamountpara.Value = st_stockoutapply.NetAmount;
            paras.Add(netamountpara);

            SqlParameter bundlespara = new SqlParameter("@Bundles", SqlDbType.Int, 4);
            bundlespara.Value = st_stockoutapply.Bundles;
            paras.Add(bundlespara);

            SqlParameter unitidpara = new SqlParameter("@UnitId", SqlDbType.Int, 4);
            unitidpara.Value = st_stockoutapply.UnitId;
            paras.Add(unitidpara);

            SqlParameter buycorpidpara = new SqlParameter("@BuyCorpId", SqlDbType.Int, 4);
            buycorpidpara.Value = st_stockoutapply.BuyCorpId;
            paras.Add(buycorpidpara);

            SqlParameter createfrompara = new SqlParameter("@CreateFrom", SqlDbType.Int, 4);
            createfrompara.Value = st_stockoutapply.CreateFrom;
            paras.Add(createfrompara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
