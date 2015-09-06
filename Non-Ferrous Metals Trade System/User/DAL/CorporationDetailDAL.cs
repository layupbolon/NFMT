/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorporationDetailDAL.cs
// 文件功能描述：客户明细dbo.CorporationDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.User.Model;
using NFMT.DBUtility;
using NFMT.User.IDAL;
using NFMT.Common;

namespace NFMT.User.DAL
{
    /// <summary>
    /// 客户明细dbo.CorporationDetail数据交互类。
    /// </summary>
    public partial class CorporationDetailDAL : DataOperate, ICorporationDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CorporationDetailDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringUser;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            CorporationDetail corporationdetail = (CorporationDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = corporationdetail.CorpId;
            paras.Add(corpidpara);

            if (!string.IsNullOrEmpty(corporationdetail.BusinessLicenseCode))
            {
                SqlParameter businesslicensecodepara = new SqlParameter("@BusinessLicenseCode", SqlDbType.VarChar, 80);
                businesslicensecodepara.Value = corporationdetail.BusinessLicenseCode;
                paras.Add(businesslicensecodepara);
            }

            SqlParameter registeredcapitalpara = new SqlParameter("@RegisteredCapital", SqlDbType.Decimal, 9);
            registeredcapitalpara.Value = corporationdetail.RegisteredCapital;
            paras.Add(registeredcapitalpara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = corporationdetail.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter registereddatepara = new SqlParameter("@RegisteredDate", SqlDbType.DateTime, 8);
            registereddatepara.Value = corporationdetail.RegisteredDate;
            paras.Add(registereddatepara);

            if (!string.IsNullOrEmpty(corporationdetail.CorpProperty))
            {
                SqlParameter corppropertypara = new SqlParameter("@CorpProperty", SqlDbType.VarChar, 200);
                corppropertypara.Value = corporationdetail.CorpProperty;
                paras.Add(corppropertypara);
            }

            if (!string.IsNullOrEmpty(corporationdetail.BusinessScope))
            {
                SqlParameter businessscopepara = new SqlParameter("@BusinessScope", SqlDbType.VarChar, 500);
                businessscopepara.Value = corporationdetail.BusinessScope;
                paras.Add(businessscopepara);
            }

            if (!string.IsNullOrEmpty(corporationdetail.TaxRegisteredCode))
            {
                SqlParameter taxregisteredcodepara = new SqlParameter("@TaxRegisteredCode", SqlDbType.VarChar, 200);
                taxregisteredcodepara.Value = corporationdetail.TaxRegisteredCode;
                paras.Add(taxregisteredcodepara);
            }

            if (!string.IsNullOrEmpty(corporationdetail.OrganizationCode))
            {
                SqlParameter organizationcodepara = new SqlParameter("@OrganizationCode", SqlDbType.VarChar, 200);
                organizationcodepara.Value = corporationdetail.OrganizationCode;
                paras.Add(organizationcodepara);
            }

            SqlParameter ischildcorppara = new SqlParameter("@IsChildCorp", SqlDbType.Bit, 1);
            ischildcorppara.Value = corporationdetail.IsChildCorp;
            paras.Add(ischildcorppara);

            if (!string.IsNullOrEmpty(corporationdetail.CorpZip))
            {
                SqlParameter corpzippara = new SqlParameter("@CorpZip", SqlDbType.VarChar, 20);
                corpzippara.Value = corporationdetail.CorpZip;
                paras.Add(corpzippara);
            }

            SqlParameter corptypepara = new SqlParameter("@CorpType", SqlDbType.Int, 4);
            corptypepara.Value = corporationdetail.CorpType;
            paras.Add(corptypepara);

            SqlParameter isselfpara = new SqlParameter("@IsSelf", SqlDbType.Bit, 1);
            isselfpara.Value = corporationdetail.IsSelf;
            paras.Add(isselfpara);

            if (!string.IsNullOrEmpty(corporationdetail.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = corporationdetail.Memo;
                paras.Add(memopara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = corporationdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            CorporationDetail corporationdetail = new CorporationDetail();

            corporationdetail.DetailId = Convert.ToInt32(dr["DetailId"]);

            if (dr["CorpId"] != DBNull.Value)
            {
                corporationdetail.CorpId = Convert.ToInt32(dr["CorpId"]);
            }

            if (dr["BusinessLicenseCode"] != DBNull.Value)
            {
                corporationdetail.BusinessLicenseCode = Convert.ToString(dr["BusinessLicenseCode"]);
            }

            if (dr["RegisteredCapital"] != DBNull.Value)
            {
                corporationdetail.RegisteredCapital = Convert.ToDecimal(dr["RegisteredCapital"]);
            }

            if (dr["CurrencyId"] != DBNull.Value)
            {
                corporationdetail.CurrencyId = Convert.ToInt32(dr["CurrencyId"]);
            }

            if (dr["RegisteredDate"] != DBNull.Value)
            {
                corporationdetail.RegisteredDate = Convert.ToDateTime(dr["RegisteredDate"]);
            }

            if (dr["CorpProperty"] != DBNull.Value)
            {
                corporationdetail.CorpProperty = Convert.ToString(dr["CorpProperty"]);
            }

            if (dr["BusinessScope"] != DBNull.Value)
            {
                corporationdetail.BusinessScope = Convert.ToString(dr["BusinessScope"]);
            }

            if (dr["TaxRegisteredCode"] != DBNull.Value)
            {
                corporationdetail.TaxRegisteredCode = Convert.ToString(dr["TaxRegisteredCode"]);
            }

            if (dr["OrganizationCode"] != DBNull.Value)
            {
                corporationdetail.OrganizationCode = Convert.ToString(dr["OrganizationCode"]);
            }

            if (dr["IsChildCorp"] != DBNull.Value)
            {
                corporationdetail.IsChildCorp = Convert.ToBoolean(dr["IsChildCorp"]);
            }

            if (dr["CorpZip"] != DBNull.Value)
            {
                corporationdetail.CorpZip = Convert.ToString(dr["CorpZip"]);
            }

            if (dr["CorpType"] != DBNull.Value)
            {
                corporationdetail.CorpType = Convert.ToInt32(dr["CorpType"]);
            }

            if (dr["IsSelf"] != DBNull.Value)
            {
                corporationdetail.IsSelf = Convert.ToBoolean(dr["IsSelf"]);
            }

            if (dr["Memo"] != DBNull.Value)
            {
                corporationdetail.Memo = Convert.ToString(dr["Memo"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                corporationdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                corporationdetail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                corporationdetail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                corporationdetail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                corporationdetail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return corporationdetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            CorporationDetail corporationdetail = new CorporationDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            corporationdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexCorpId = dr.GetOrdinal("CorpId");
            if (dr["CorpId"] != DBNull.Value)
            {
                corporationdetail.CorpId = Convert.ToInt32(dr[indexCorpId]);
            }

            int indexBusinessLicenseCode = dr.GetOrdinal("BusinessLicenseCode");
            if (dr["BusinessLicenseCode"] != DBNull.Value)
            {
                corporationdetail.BusinessLicenseCode = Convert.ToString(dr[indexBusinessLicenseCode]);
            }

            int indexRegisteredCapital = dr.GetOrdinal("RegisteredCapital");
            if (dr["RegisteredCapital"] != DBNull.Value)
            {
                corporationdetail.RegisteredCapital = Convert.ToDecimal(dr[indexRegisteredCapital]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                corporationdetail.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexRegisteredDate = dr.GetOrdinal("RegisteredDate");
            if (dr["RegisteredDate"] != DBNull.Value)
            {
                corporationdetail.RegisteredDate = Convert.ToDateTime(dr[indexRegisteredDate]);
            }

            int indexCorpProperty = dr.GetOrdinal("CorpProperty");
            if (dr["CorpProperty"] != DBNull.Value)
            {
                corporationdetail.CorpProperty = Convert.ToString(dr[indexCorpProperty]);
            }

            int indexBusinessScope = dr.GetOrdinal("BusinessScope");
            if (dr["BusinessScope"] != DBNull.Value)
            {
                corporationdetail.BusinessScope = Convert.ToString(dr[indexBusinessScope]);
            }

            int indexTaxRegisteredCode = dr.GetOrdinal("TaxRegisteredCode");
            if (dr["TaxRegisteredCode"] != DBNull.Value)
            {
                corporationdetail.TaxRegisteredCode = Convert.ToString(dr[indexTaxRegisteredCode]);
            }

            int indexOrganizationCode = dr.GetOrdinal("OrganizationCode");
            if (dr["OrganizationCode"] != DBNull.Value)
            {
                corporationdetail.OrganizationCode = Convert.ToString(dr[indexOrganizationCode]);
            }

            int indexIsChildCorp = dr.GetOrdinal("IsChildCorp");
            if (dr["IsChildCorp"] != DBNull.Value)
            {
                corporationdetail.IsChildCorp = Convert.ToBoolean(dr[indexIsChildCorp]);
            }

            int indexCorpZip = dr.GetOrdinal("CorpZip");
            if (dr["CorpZip"] != DBNull.Value)
            {
                corporationdetail.CorpZip = Convert.ToString(dr[indexCorpZip]);
            }

            int indexCorpType = dr.GetOrdinal("CorpType");
            if (dr["CorpType"] != DBNull.Value)
            {
                corporationdetail.CorpType = Convert.ToInt32(dr[indexCorpType]);
            }

            int indexIsSelf = dr.GetOrdinal("IsSelf");
            if (dr["IsSelf"] != DBNull.Value)
            {
                corporationdetail.IsSelf = Convert.ToBoolean(dr[indexIsSelf]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                corporationdetail.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                corporationdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                corporationdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                corporationdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                corporationdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                corporationdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return corporationdetail;
        }

        public override string TableName
        {
            get
            {
                return "CorporationDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CorporationDetail corporationdetail = (CorporationDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = corporationdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = corporationdetail.CorpId;
            paras.Add(corpidpara);

            if (!string.IsNullOrEmpty(corporationdetail.BusinessLicenseCode))
            {
                SqlParameter businesslicensecodepara = new SqlParameter("@BusinessLicenseCode", SqlDbType.VarChar, 80);
                businesslicensecodepara.Value = corporationdetail.BusinessLicenseCode;
                paras.Add(businesslicensecodepara);
            }

            SqlParameter registeredcapitalpara = new SqlParameter("@RegisteredCapital", SqlDbType.Decimal, 9);
            registeredcapitalpara.Value = corporationdetail.RegisteredCapital;
            paras.Add(registeredcapitalpara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = corporationdetail.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter registereddatepara = new SqlParameter("@RegisteredDate", SqlDbType.DateTime, 8);
            registereddatepara.Value = corporationdetail.RegisteredDate;
            paras.Add(registereddatepara);

            if (!string.IsNullOrEmpty(corporationdetail.CorpProperty))
            {
                SqlParameter corppropertypara = new SqlParameter("@CorpProperty", SqlDbType.VarChar, 200);
                corppropertypara.Value = corporationdetail.CorpProperty;
                paras.Add(corppropertypara);
            }

            if (!string.IsNullOrEmpty(corporationdetail.BusinessScope))
            {
                SqlParameter businessscopepara = new SqlParameter("@BusinessScope", SqlDbType.VarChar, 500);
                businessscopepara.Value = corporationdetail.BusinessScope;
                paras.Add(businessscopepara);
            }

            if (!string.IsNullOrEmpty(corporationdetail.TaxRegisteredCode))
            {
                SqlParameter taxregisteredcodepara = new SqlParameter("@TaxRegisteredCode", SqlDbType.VarChar, 200);
                taxregisteredcodepara.Value = corporationdetail.TaxRegisteredCode;
                paras.Add(taxregisteredcodepara);
            }

            if (!string.IsNullOrEmpty(corporationdetail.OrganizationCode))
            {
                SqlParameter organizationcodepara = new SqlParameter("@OrganizationCode", SqlDbType.VarChar, 200);
                organizationcodepara.Value = corporationdetail.OrganizationCode;
                paras.Add(organizationcodepara);
            }

            SqlParameter ischildcorppara = new SqlParameter("@IsChildCorp", SqlDbType.Bit, 1);
            ischildcorppara.Value = corporationdetail.IsChildCorp;
            paras.Add(ischildcorppara);

            if (!string.IsNullOrEmpty(corporationdetail.CorpZip))
            {
                SqlParameter corpzippara = new SqlParameter("@CorpZip", SqlDbType.VarChar, 20);
                corpzippara.Value = corporationdetail.CorpZip;
                paras.Add(corpzippara);
            }

            SqlParameter corptypepara = new SqlParameter("@CorpType", SqlDbType.Int, 4);
            corptypepara.Value = corporationdetail.CorpType;
            paras.Add(corptypepara);

            SqlParameter isselfpara = new SqlParameter("@IsSelf", SqlDbType.Bit, 1);
            isselfpara.Value = corporationdetail.IsSelf;
            paras.Add(isselfpara);

            if (!string.IsNullOrEmpty(corporationdetail.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = corporationdetail.Memo;
                paras.Add(memopara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = corporationdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel UpdateStatus(UserModel user, int corpId, int detailId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Corporation set CorpStatus = {0} where CorpId = {1};update dbo.CorporationDetail set DetailStatus = {0} where DetailId = {2}", (int)Common.StatusEnum.已生效, corpId, detailId);

                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 1)
                {
                    result.ResultStatus = 0;
                    result.Message = "更新成功";
                }
                else
                {
                    result.Message = "更新失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        #endregion
    }
}
