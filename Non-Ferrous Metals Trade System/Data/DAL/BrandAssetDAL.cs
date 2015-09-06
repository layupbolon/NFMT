/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BrandAssetDAL.cs
// 文件功能描述：dbo.BrandAsset数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Data.Model;
using NFMT.DBUtility;
using NFMT.Data.IDAL;
using NFMT.Common;

namespace NFMT.Data.DAL
{
    /// <summary>
    /// dbo.BrandAsset数据交互类。
    /// </summary>
    public class BrandAssetDAL : DataOperate, IBrandAssetDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BrandAssetDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringBasic;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            BrandAsset brandasset = (BrandAsset)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter brandidpara = new SqlParameter("@BrandId", SqlDbType.Int, 4);
            brandidpara.Value = brandasset.BrandId;
            paras.Add(brandidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = brandasset.AssetId;
            paras.Add(assetidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(refstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            BrandAsset brandasset = new BrandAsset();

            int indexRefId = dr.GetOrdinal("RefId");
            brandasset.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexBrandId = dr.GetOrdinal("BrandId");
            if (dr["BrandId"] != DBNull.Value)
            {
                brandasset.BrandId = Convert.ToInt32(dr[indexBrandId]);
            }

            int indexAssetId = dr.GetOrdinal("AssetId");
            if (dr["AssetId"] != DBNull.Value)
            {
                brandasset.AssetId = Convert.ToInt32(dr[indexAssetId]);
            }

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                brandasset.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRefStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                brandasset.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                brandasset.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                brandasset.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                brandasset.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return brandasset;
        }

        public override string TableName
        {
            get
            {
                return "BrandAsset";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            BrandAsset brandasset = (BrandAsset)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = brandasset.RefId;
            paras.Add(refidpara);

            SqlParameter brandidpara = new SqlParameter("@BrandId", SqlDbType.Int, 4);
            brandidpara.Value = brandasset.BrandId;
            paras.Add(brandidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = brandasset.AssetId;
            paras.Add(assetidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = brandasset.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
