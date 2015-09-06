/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CustomsClearanceDAL.cs
// 文件功能描述：报关dbo.St_CustomsClearance数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月29日
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
    /// 报关dbo.St_CustomsClearance数据交互类。
    /// </summary>
    public class CustomsClearanceDAL : ExecOperate, ICustomsClearanceDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomsClearanceDAL()
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
            CustomsClearance st_customsclearance = (CustomsClearance)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@CustomsId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter customsapplyidpara = new SqlParameter("@CustomsApplyId", SqlDbType.Int, 4);
            customsapplyidpara.Value = st_customsclearance.CustomsApplyId;
            paras.Add(customsapplyidpara);

            SqlParameter customserpara = new SqlParameter("@Customser", SqlDbType.Int, 4);
            customserpara.Value = st_customsclearance.Customser;
            paras.Add(customserpara);

            SqlParameter customscorpidpara = new SqlParameter("@CustomsCorpId", SqlDbType.Int, 4);
            customscorpidpara.Value = st_customsclearance.CustomsCorpId;
            paras.Add(customscorpidpara);

            SqlParameter customsdatepara = new SqlParameter("@CustomsDate", SqlDbType.DateTime, 8);
            customsdatepara.Value = st_customsclearance.CustomsDate;
            paras.Add(customsdatepara);

            SqlParameter customsnamepara = new SqlParameter("@CustomsName", SqlDbType.Int, 4);
            customsnamepara.Value = st_customsclearance.CustomsName;
            paras.Add(customsnamepara);

            SqlParameter grossweightpara = new SqlParameter("@GrossWeight", SqlDbType.Decimal, 9);
            grossweightpara.Value = st_customsclearance.GrossWeight;
            paras.Add(grossweightpara);

            SqlParameter netweightpara = new SqlParameter("@NetWeight", SqlDbType.Decimal, 9);
            netweightpara.Value = st_customsclearance.NetWeight;
            paras.Add(netweightpara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = st_customsclearance.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter customspricepara = new SqlParameter("@CustomsPrice", SqlDbType.Decimal, 9);
            customspricepara.Value = st_customsclearance.CustomsPrice;
            paras.Add(customspricepara);

            SqlParameter tariffratepara = new SqlParameter("@TariffRate", SqlDbType.Decimal, 9);
            tariffratepara.Value = st_customsclearance.TariffRate;
            paras.Add(tariffratepara);

            SqlParameter addedvalueratepara = new SqlParameter("@AddedValueRate", SqlDbType.Decimal, 9);
            addedvalueratepara.Value = st_customsclearance.AddedValueRate;
            paras.Add(addedvalueratepara);

            SqlParameter otherfeespara = new SqlParameter("@OtherFees", SqlDbType.Decimal, 9);
            otherfeespara.Value = st_customsclearance.OtherFees;
            paras.Add(otherfeespara);

            if (!string.IsNullOrEmpty(st_customsclearance.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = st_customsclearance.Memo;
                paras.Add(memopara);
            }

            SqlParameter customsstatuspara = new SqlParameter("@CustomsStatus", SqlDbType.Int, 4);
            customsstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(customsstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            CustomsClearance customsclearance = new CustomsClearance();

            customsclearance.CustomsId = Convert.ToInt32(dr["CustomsId"]);

            if (dr["CustomsApplyId"] != DBNull.Value)
            {
                customsclearance.CustomsApplyId = Convert.ToInt32(dr["CustomsApplyId"]);
            }

            if (dr["Customser"] != DBNull.Value)
            {
                customsclearance.Customser = Convert.ToInt32(dr["Customser"]);
            }

            if (dr["CustomsCorpId"] != DBNull.Value)
            {
                customsclearance.CustomsCorpId = Convert.ToInt32(dr["CustomsCorpId"]);
            }

            if (dr["CustomsDate"] != DBNull.Value)
            {
                customsclearance.CustomsDate = Convert.ToDateTime(dr["CustomsDate"]);
            }

            if (dr["CustomsName"] != DBNull.Value)
            {
                customsclearance.CustomsName = Convert.ToInt32(dr["CustomsName"]);
            }

            if (dr["GrossWeight"] != DBNull.Value)
            {
                customsclearance.GrossWeight = Convert.ToDecimal(dr["GrossWeight"]);
            }

            if (dr["NetWeight"] != DBNull.Value)
            {
                customsclearance.NetWeight = Convert.ToDecimal(dr["NetWeight"]);
            }

            if (dr["CurrencyId"] != DBNull.Value)
            {
                customsclearance.CurrencyId = Convert.ToInt32(dr["CurrencyId"]);
            }

            if (dr["CustomsPrice"] != DBNull.Value)
            {
                customsclearance.CustomsPrice = Convert.ToDecimal(dr["CustomsPrice"]);
            }

            if (dr["TariffRate"] != DBNull.Value)
            {
                customsclearance.TariffRate = Convert.ToDecimal(dr["TariffRate"]);
            }

            if (dr["AddedValueRate"] != DBNull.Value)
            {
                customsclearance.AddedValueRate = Convert.ToDecimal(dr["AddedValueRate"]);
            }

            if (dr["OtherFees"] != DBNull.Value)
            {
                customsclearance.OtherFees = Convert.ToDecimal(dr["OtherFees"]);
            }

            if (dr["Memo"] != DBNull.Value)
            {
                customsclearance.Memo = Convert.ToString(dr["Memo"]);
            }

            if (dr["CustomsStatus"] != DBNull.Value)
            {
                customsclearance.CustomsStatus = (Common.StatusEnum)Convert.ToInt32(dr["CustomsStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                customsclearance.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                customsclearance.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                customsclearance.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                customsclearance.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return customsclearance;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            CustomsClearance customsclearance = new CustomsClearance();

            int indexCustomsId = dr.GetOrdinal("CustomsId");
            customsclearance.CustomsId = Convert.ToInt32(dr[indexCustomsId]);

            int indexCustomsApplyId = dr.GetOrdinal("CustomsApplyId");
            if (dr["CustomsApplyId"] != DBNull.Value)
            {
                customsclearance.CustomsApplyId = Convert.ToInt32(dr[indexCustomsApplyId]);
            }

            int indexCustomser = dr.GetOrdinal("Customser");
            if (dr["Customser"] != DBNull.Value)
            {
                customsclearance.Customser = Convert.ToInt32(dr[indexCustomser]);
            }

            int indexCustomsCorpId = dr.GetOrdinal("CustomsCorpId");
            if (dr["CustomsCorpId"] != DBNull.Value)
            {
                customsclearance.CustomsCorpId = Convert.ToInt32(dr[indexCustomsCorpId]);
            }

            int indexCustomsDate = dr.GetOrdinal("CustomsDate");
            if (dr["CustomsDate"] != DBNull.Value)
            {
                customsclearance.CustomsDate = Convert.ToDateTime(dr[indexCustomsDate]);
            }

            int indexCustomsName = dr.GetOrdinal("CustomsName");
            if (dr["CustomsName"] != DBNull.Value)
            {
                customsclearance.CustomsName = Convert.ToInt32(dr[indexCustomsName]);
            }

            int indexGrossWeight = dr.GetOrdinal("GrossWeight");
            if (dr["GrossWeight"] != DBNull.Value)
            {
                customsclearance.GrossWeight = Convert.ToDecimal(dr[indexGrossWeight]);
            }

            int indexNetWeight = dr.GetOrdinal("NetWeight");
            if (dr["NetWeight"] != DBNull.Value)
            {
                customsclearance.NetWeight = Convert.ToDecimal(dr[indexNetWeight]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                customsclearance.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexCustomsPrice = dr.GetOrdinal("CustomsPrice");
            if (dr["CustomsPrice"] != DBNull.Value)
            {
                customsclearance.CustomsPrice = Convert.ToDecimal(dr[indexCustomsPrice]);
            }

            int indexTariffRate = dr.GetOrdinal("TariffRate");
            if (dr["TariffRate"] != DBNull.Value)
            {
                customsclearance.TariffRate = Convert.ToDecimal(dr[indexTariffRate]);
            }

            int indexAddedValueRate = dr.GetOrdinal("AddedValueRate");
            if (dr["AddedValueRate"] != DBNull.Value)
            {
                customsclearance.AddedValueRate = Convert.ToDecimal(dr[indexAddedValueRate]);
            }

            int indexOtherFees = dr.GetOrdinal("OtherFees");
            if (dr["OtherFees"] != DBNull.Value)
            {
                customsclearance.OtherFees = Convert.ToDecimal(dr[indexOtherFees]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                customsclearance.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexCustomsStatus = dr.GetOrdinal("CustomsStatus");
            if (dr["CustomsStatus"] != DBNull.Value)
            {
                customsclearance.CustomsStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexCustomsStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                customsclearance.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                customsclearance.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                customsclearance.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                customsclearance.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return customsclearance;
        }

        public override string TableName
        {
            get
            {
                return "St_CustomsClearance";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CustomsClearance st_customsclearance = (CustomsClearance)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter customsidpara = new SqlParameter("@CustomsId", SqlDbType.Int, 4);
            customsidpara.Value = st_customsclearance.CustomsId;
            paras.Add(customsidpara);

            SqlParameter customsapplyidpara = new SqlParameter("@CustomsApplyId", SqlDbType.Int, 4);
            customsapplyidpara.Value = st_customsclearance.CustomsApplyId;
            paras.Add(customsapplyidpara);

            SqlParameter customserpara = new SqlParameter("@Customser", SqlDbType.Int, 4);
            customserpara.Value = st_customsclearance.Customser;
            paras.Add(customserpara);

            SqlParameter customscorpidpara = new SqlParameter("@CustomsCorpId", SqlDbType.Int, 4);
            customscorpidpara.Value = st_customsclearance.CustomsCorpId;
            paras.Add(customscorpidpara);

            SqlParameter customsdatepara = new SqlParameter("@CustomsDate", SqlDbType.DateTime, 8);
            customsdatepara.Value = st_customsclearance.CustomsDate;
            paras.Add(customsdatepara);

            SqlParameter customsnamepara = new SqlParameter("@CustomsName", SqlDbType.Int, 4);
            customsnamepara.Value = st_customsclearance.CustomsName;
            paras.Add(customsnamepara);

            SqlParameter grossweightpara = new SqlParameter("@GrossWeight", SqlDbType.Decimal, 9);
            grossweightpara.Value = st_customsclearance.GrossWeight;
            paras.Add(grossweightpara);

            SqlParameter netweightpara = new SqlParameter("@NetWeight", SqlDbType.Decimal, 9);
            netweightpara.Value = st_customsclearance.NetWeight;
            paras.Add(netweightpara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = st_customsclearance.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter customspricepara = new SqlParameter("@CustomsPrice", SqlDbType.Decimal, 9);
            customspricepara.Value = st_customsclearance.CustomsPrice;
            paras.Add(customspricepara);

            SqlParameter tariffratepara = new SqlParameter("@TariffRate", SqlDbType.Decimal, 9);
            tariffratepara.Value = st_customsclearance.TariffRate;
            paras.Add(tariffratepara);

            SqlParameter addedvalueratepara = new SqlParameter("@AddedValueRate", SqlDbType.Decimal, 9);
            addedvalueratepara.Value = st_customsclearance.AddedValueRate;
            paras.Add(addedvalueratepara);

            SqlParameter otherfeespara = new SqlParameter("@OtherFees", SqlDbType.Decimal, 9);
            otherfeespara.Value = st_customsclearance.OtherFees;
            paras.Add(otherfeespara);

            if (!string.IsNullOrEmpty(st_customsclearance.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = st_customsclearance.Memo;
                paras.Add(memopara);
            }

            SqlParameter customsstatuspara = new SqlParameter("@CustomsStatus", SqlDbType.Int, 4);
            customsstatuspara.Value = st_customsclearance.CustomsStatus;
            paras.Add(customsstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public override int MenuId
        {
            get
            {
                return 96;
            }
        }

        #endregion
    }
}
