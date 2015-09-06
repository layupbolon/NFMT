/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PricingPersonDAL.cs
// 文件功能描述：点价权限人dbo.Pri_PricingPerson数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月15日
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
    /// 点价权限人dbo.Pri_PricingPerson数据交互类。
    /// </summary>
    public class PricingPersonDAL : DataOperate, IPricingPersonDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PricingPersonDAL()
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
            PricingPerson pri_pricingperson = (PricingPerson)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@PersoinId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter blocidpara = new SqlParameter("@BlocId", SqlDbType.Int, 4);
            blocidpara.Value = pri_pricingperson.BlocId;
            paras.Add(blocidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = pri_pricingperson.CorpId;
            paras.Add(corpidpara);

            if (!string.IsNullOrEmpty(pri_pricingperson.PricingName))
            {
                SqlParameter pricingnamepara = new SqlParameter("@PricingName", SqlDbType.VarChar, 50);
                pricingnamepara.Value = pri_pricingperson.PricingName;
                paras.Add(pricingnamepara);
            }

            if (!string.IsNullOrEmpty(pri_pricingperson.Job))
            {
                SqlParameter jobpara = new SqlParameter("@Job", SqlDbType.VarChar, 20);
                jobpara.Value = pri_pricingperson.Job;
                paras.Add(jobpara);
            }

            if (!string.IsNullOrEmpty(pri_pricingperson.PricingPhone))
            {
                SqlParameter pricingphonepara = new SqlParameter("@PricingPhone", SqlDbType.VarChar, 20);
                pricingphonepara.Value = pri_pricingperson.PricingPhone;
                paras.Add(pricingphonepara);
            }

            if (!string.IsNullOrEmpty(pri_pricingperson.Phone2))
            {
                SqlParameter phone2para = new SqlParameter("@Phone2", SqlDbType.VarChar, 20);
                phone2para.Value = pri_pricingperson.Phone2;
                paras.Add(phone2para);
            }

            SqlParameter persoinstatuspara = new SqlParameter("@PersoinStatus", SqlDbType.Int, 4);
            persoinstatuspara.Value = pri_pricingperson.PersoinStatus;
            paras.Add(persoinstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PricingPerson pricingperson = new PricingPerson();

            int indexPersoinId = dr.GetOrdinal("PersoinId");
            pricingperson.PersoinId = Convert.ToInt32(dr[indexPersoinId]);

            int indexBlocId = dr.GetOrdinal("BlocId");
            if (dr["BlocId"] != DBNull.Value)
            {
                pricingperson.BlocId = Convert.ToInt32(dr[indexBlocId]);
            }

            int indexCorpId = dr.GetOrdinal("CorpId");
            if (dr["CorpId"] != DBNull.Value)
            {
                pricingperson.CorpId = Convert.ToInt32(dr[indexCorpId]);
            }

            int indexPricingName = dr.GetOrdinal("PricingName");
            if (dr["PricingName"] != DBNull.Value)
            {
                pricingperson.PricingName = Convert.ToString(dr[indexPricingName]);
            }

            int indexJob = dr.GetOrdinal("Job");
            if (dr["Job"] != DBNull.Value)
            {
                pricingperson.Job = Convert.ToString(dr[indexJob]);
            }

            int indexPricingPhone = dr.GetOrdinal("PricingPhone");
            if (dr["PricingPhone"] != DBNull.Value)
            {
                pricingperson.PricingPhone = Convert.ToString(dr[indexPricingPhone]);
            }

            int indexPhone2 = dr.GetOrdinal("Phone2");
            if (dr["Phone2"] != DBNull.Value)
            {
                pricingperson.Phone2 = Convert.ToString(dr[indexPhone2]);
            }

            int indexPersoinStatus = dr.GetOrdinal("PersoinStatus");
            if (dr["PersoinStatus"] != DBNull.Value)
            {
                pricingperson.PersoinStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexPersoinStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                pricingperson.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                pricingperson.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                pricingperson.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pricingperson.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return pricingperson;
        }

        public override string TableName
        {
            get
            {
                return "Pri_PricingPerson";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PricingPerson pri_pricingperson = (PricingPerson)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter persoinidpara = new SqlParameter("@PersoinId", SqlDbType.Int, 4);
            persoinidpara.Value = pri_pricingperson.PersoinId;
            paras.Add(persoinidpara);

            SqlParameter blocidpara = new SqlParameter("@BlocId", SqlDbType.Int, 4);
            blocidpara.Value = pri_pricingperson.BlocId;
            paras.Add(blocidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = pri_pricingperson.CorpId;
            paras.Add(corpidpara);

            if (!string.IsNullOrEmpty(pri_pricingperson.PricingName))
            {
                SqlParameter pricingnamepara = new SqlParameter("@PricingName", SqlDbType.VarChar, 50);
                pricingnamepara.Value = pri_pricingperson.PricingName;
                paras.Add(pricingnamepara);
            }

            if (!string.IsNullOrEmpty(pri_pricingperson.Job))
            {
                SqlParameter jobpara = new SqlParameter("@Job", SqlDbType.VarChar, 20);
                jobpara.Value = pri_pricingperson.Job;
                paras.Add(jobpara);
            }

            if (!string.IsNullOrEmpty(pri_pricingperson.PricingPhone))
            {
                SqlParameter pricingphonepara = new SqlParameter("@PricingPhone", SqlDbType.VarChar, 20);
                pricingphonepara.Value = pri_pricingperson.PricingPhone;
                paras.Add(pricingphonepara);
            }

            if (!string.IsNullOrEmpty(pri_pricingperson.Phone2))
            {
                SqlParameter phone2para = new SqlParameter("@Phone2", SqlDbType.VarChar, 20);
                phone2para.Value = pri_pricingperson.Phone2;
                paras.Add(phone2para);
            }

            SqlParameter persoinstatuspara = new SqlParameter("@PersoinStatus", SqlDbType.Int, 4);
            persoinstatuspara.Value = pri_pricingperson.PersoinStatus;
            paras.Add(persoinstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
