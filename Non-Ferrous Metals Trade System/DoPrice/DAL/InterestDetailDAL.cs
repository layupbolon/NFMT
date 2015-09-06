/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InterestDetailDAL.cs
// 文件功能描述：利息明细dbo.Pri_InterestDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年3月20日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.DoPrice.Model;
using NFMT.DBUtility;
using NFMT.DoPrice.IDAL;
using NFMT.Common;

namespace NFMT.DoPrice.DAL
{
    /// <summary>
    /// 利息明细dbo.Pri_InterestDetail数据交互类。
    /// </summary>
    public partial class InterestDetailDAL : ExecOperate, IInterestDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InterestDetailDAL()
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
            InterestDetail pri_interestdetail = (InterestDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter interestidpara = new SqlParameter("@InterestId", SqlDbType.Int, 4);
            interestidpara.Value = pri_interestdetail.InterestId;
            paras.Add(interestidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = pri_interestdetail.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = pri_interestdetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = pri_interestdetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = pri_interestdetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter interestamountpara = new SqlParameter("@InterestAmount", SqlDbType.Decimal, 9);
            interestamountpara.Value = pri_interestdetail.InterestAmount;
            paras.Add(interestamountpara);

            SqlParameter pricingunitpara = new SqlParameter("@PricingUnit", SqlDbType.Decimal, 9);
            pricingunitpara.Value = pri_interestdetail.PricingUnit;
            paras.Add(pricingunitpara);

            SqlParameter premiumpara = new SqlParameter("@Premium", SqlDbType.Decimal, 9);
            premiumpara.Value = pri_interestdetail.Premium;
            paras.Add(premiumpara);

            SqlParameter otherpricepara = new SqlParameter("@OtherPrice", SqlDbType.Decimal, 9);
            otherpricepara.Value = pri_interestdetail.OtherPrice;
            paras.Add(otherpricepara);

            SqlParameter interestpricepara = new SqlParameter("@InterestPrice", SqlDbType.Decimal, 9);
            interestpricepara.Value = pri_interestdetail.InterestPrice;
            paras.Add(interestpricepara);

            SqlParameter stockbalapara = new SqlParameter("@StockBala", SqlDbType.Decimal, 9);
            stockbalapara.Value = pri_interestdetail.StockBala;
            paras.Add(stockbalapara);

            SqlParameter intereststartdatepara = new SqlParameter("@InterestStartDate", SqlDbType.DateTime, 8);
            intereststartdatepara.Value = pri_interestdetail.InterestStartDate;
            paras.Add(intereststartdatepara);

            SqlParameter interestenddatepara = new SqlParameter("@InterestEndDate", SqlDbType.DateTime, 8);
            interestenddatepara.Value = pri_interestdetail.InterestEndDate;
            paras.Add(interestenddatepara);

            SqlParameter interestdaypara = new SqlParameter("@InterestDay", SqlDbType.Int, 4);
            interestdaypara.Value = pri_interestdetail.InterestDay;
            paras.Add(interestdaypara);

            SqlParameter interestunitpara = new SqlParameter("@InterestUnit", SqlDbType.Decimal, 9);
            interestunitpara.Value = pri_interestdetail.InterestUnit;
            paras.Add(interestunitpara);

            SqlParameter interestbalapara = new SqlParameter("@InterestBala", SqlDbType.Decimal, 9);
            interestbalapara.Value = pri_interestdetail.InterestBala;
            paras.Add(interestbalapara);

            SqlParameter interesttypepara = new SqlParameter("@InterestType", SqlDbType.Int, 4);
            interesttypepara.Value = pri_interestdetail.InterestType;
            paras.Add(interesttypepara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = pri_interestdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            InterestDetail interestdetail = new InterestDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            interestdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexInterestId = dr.GetOrdinal("InterestId");
            if (dr["InterestId"] != DBNull.Value)
            {
                interestdetail.InterestId = Convert.ToInt32(dr[indexInterestId]);
            }

            int indexSubContractId = dr.GetOrdinal("SubContractId");
            if (dr["SubContractId"] != DBNull.Value)
            {
                interestdetail.SubContractId = Convert.ToInt32(dr[indexSubContractId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                interestdetail.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                interestdetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                interestdetail.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexInterestAmount = dr.GetOrdinal("InterestAmount");
            if (dr["InterestAmount"] != DBNull.Value)
            {
                interestdetail.InterestAmount = Convert.ToDecimal(dr[indexInterestAmount]);
            }

            int indexPricingUnit = dr.GetOrdinal("PricingUnit");
            if (dr["PricingUnit"] != DBNull.Value)
            {
                interestdetail.PricingUnit = Convert.ToDecimal(dr[indexPricingUnit]);
            }

            int indexPremium = dr.GetOrdinal("Premium");
            if (dr["Premium"] != DBNull.Value)
            {
                interestdetail.Premium = Convert.ToDecimal(dr[indexPremium]);
            }

            int indexOtherPrice = dr.GetOrdinal("OtherPrice");
            if (dr["OtherPrice"] != DBNull.Value)
            {
                interestdetail.OtherPrice = Convert.ToDecimal(dr[indexOtherPrice]);
            }

            int indexInterestPrice = dr.GetOrdinal("InterestPrice");
            if (dr["InterestPrice"] != DBNull.Value)
            {
                interestdetail.InterestPrice = Convert.ToDecimal(dr[indexInterestPrice]);
            }

            int indexStockBala = dr.GetOrdinal("StockBala");
            if (dr["StockBala"] != DBNull.Value)
            {
                interestdetail.StockBala = Convert.ToDecimal(dr[indexStockBala]);
            }

            int indexInterestStartDate = dr.GetOrdinal("InterestStartDate");
            if (dr["InterestStartDate"] != DBNull.Value)
            {
                interestdetail.InterestStartDate = Convert.ToDateTime(dr[indexInterestStartDate]);
            }

            int indexInterestEndDate = dr.GetOrdinal("InterestEndDate");
            if (dr["InterestEndDate"] != DBNull.Value)
            {
                interestdetail.InterestEndDate = Convert.ToDateTime(dr[indexInterestEndDate]);
            }

            int indexInterestDay = dr.GetOrdinal("InterestDay");
            if (dr["InterestDay"] != DBNull.Value)
            {
                interestdetail.InterestDay = Convert.ToInt32(dr[indexInterestDay]);
            }

            int indexInterestUnit = dr.GetOrdinal("InterestUnit");
            if (dr["InterestUnit"] != DBNull.Value)
            {
                interestdetail.InterestUnit = Convert.ToDecimal(dr[indexInterestUnit]);
            }

            int indexInterestBala = dr.GetOrdinal("InterestBala");
            if (dr["InterestBala"] != DBNull.Value)
            {
                interestdetail.InterestBala = Convert.ToDecimal(dr[indexInterestBala]);
            }

            int indexInterestType = dr.GetOrdinal("InterestType");
            if (dr["InterestType"] != DBNull.Value)
            {
                interestdetail.InterestType = Convert.ToInt32(dr[indexInterestType]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                interestdetail.DetailStatus = (StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                interestdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                interestdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                interestdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                interestdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return interestdetail;
        }

        public override string TableName
        {
            get
            {
                return "Pri_InterestDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            InterestDetail pri_interestdetail = (InterestDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = pri_interestdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter interestidpara = new SqlParameter("@InterestId", SqlDbType.Int, 4);
            interestidpara.Value = pri_interestdetail.InterestId;
            paras.Add(interestidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = pri_interestdetail.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = pri_interestdetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = pri_interestdetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = pri_interestdetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter interestamountpara = new SqlParameter("@InterestAmount", SqlDbType.Decimal, 9);
            interestamountpara.Value = pri_interestdetail.InterestAmount;
            paras.Add(interestamountpara);

            SqlParameter pricingunitpara = new SqlParameter("@PricingUnit", SqlDbType.Decimal, 9);
            pricingunitpara.Value = pri_interestdetail.PricingUnit;
            paras.Add(pricingunitpara);

            SqlParameter premiumpara = new SqlParameter("@Premium", SqlDbType.Decimal, 9);
            premiumpara.Value = pri_interestdetail.Premium;
            paras.Add(premiumpara);

            SqlParameter otherpricepara = new SqlParameter("@OtherPrice", SqlDbType.Decimal, 9);
            otherpricepara.Value = pri_interestdetail.OtherPrice;
            paras.Add(otherpricepara);

            SqlParameter interestpricepara = new SqlParameter("@InterestPrice", SqlDbType.Decimal, 9);
            interestpricepara.Value = pri_interestdetail.InterestPrice;
            paras.Add(interestpricepara);

            SqlParameter stockbalapara = new SqlParameter("@StockBala", SqlDbType.Decimal, 9);
            stockbalapara.Value = pri_interestdetail.StockBala;
            paras.Add(stockbalapara);

            SqlParameter intereststartdatepara = new SqlParameter("@InterestStartDate", SqlDbType.DateTime, 8);
            intereststartdatepara.Value = pri_interestdetail.InterestStartDate;
            paras.Add(intereststartdatepara);

            SqlParameter interestenddatepara = new SqlParameter("@InterestEndDate", SqlDbType.DateTime, 8);
            interestenddatepara.Value = pri_interestdetail.InterestEndDate;
            paras.Add(interestenddatepara);

            SqlParameter interestdaypara = new SqlParameter("@InterestDay", SqlDbType.Int, 4);
            interestdaypara.Value = pri_interestdetail.InterestDay;
            paras.Add(interestdaypara);

            SqlParameter interestunitpara = new SqlParameter("@InterestUnit", SqlDbType.Decimal, 9);
            interestunitpara.Value = pri_interestdetail.InterestUnit;
            paras.Add(interestunitpara);

            SqlParameter interestbalapara = new SqlParameter("@InterestBala", SqlDbType.Decimal, 9);
            interestbalapara.Value = pri_interestdetail.InterestBala;
            paras.Add(interestbalapara);

            SqlParameter interesttypepara = new SqlParameter("@InterestType", SqlDbType.Int, 4);
            interesttypepara.Value = pri_interestdetail.InterestType;
            paras.Add(interesttypepara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = pri_interestdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
