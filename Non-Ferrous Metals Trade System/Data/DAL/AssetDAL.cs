/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AssetDAL.cs
// 文件功能描述：品种表dbo.Asset数据交互类。
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
    /// 品种表dbo.Asset数据交互类。
    /// </summary>
    public class AssetDAL : DataOperate, IAssetDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AssetDAL()
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
            Asset asset = (Asset)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@AssetId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter assetnamepara = new SqlParameter("@AssetName", SqlDbType.VarChar, 20);
            assetnamepara.Value = asset.AssetName;
            paras.Add(assetnamepara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = asset.MUId;
            paras.Add(muidpara);

            SqlParameter mistakepara = new SqlParameter("@MisTake", SqlDbType.Decimal, 9);
            mistakepara.Value = asset.MisTake;
            paras.Add(mistakepara);

            SqlParameter amountperhandpara = new SqlParameter("@AmountPerHand", SqlDbType.Int, 4);
            amountperhandpara.Value = asset.AmountPerHand;
            paras.Add(amountperhandpara);

            SqlParameter assetstatuspara = new SqlParameter("@AssetStatus", SqlDbType.Int, 4);
            assetstatuspara.Value = asset.AssetStatus;
            paras.Add(assetstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            Asset asset = new Asset();

            asset.AssetId = Convert.ToInt32(dr["AssetId"]);

            asset.AssetName = Convert.ToString(dr["AssetName"]);

            asset.MUId = Convert.ToInt32(dr["MUId"]);

            if (dr["MisTake"] != DBNull.Value)
            {
                asset.MisTake = Convert.ToDecimal(dr["MisTake"]);
            }

            if (dr["AmountPerHand"] != DBNull.Value)
            {
                asset.AmountPerHand = Convert.ToInt32(dr["AmountPerHand"]);
            }

            asset.AssetStatus = (Common.StatusEnum)Convert.ToInt32(dr["AssetStatus"]);

            asset.CreatorId = Convert.ToInt32(dr["CreatorId"]);

            asset.CreateTime = Convert.ToDateTime(dr["CreateTime"]);

            if (dr["LastModifyId"] != DBNull.Value)
            {
                asset.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                asset.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return asset;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Asset asset = new Asset();

            int indexAssetId = dr.GetOrdinal("AssetId");
            asset.AssetId = Convert.ToInt32(dr[indexAssetId]);

            int indexAssetName = dr.GetOrdinal("AssetName");
            asset.AssetName = Convert.ToString(dr[indexAssetName]);

            int indexMUId = dr.GetOrdinal("MUId");
            asset.MUId = Convert.ToInt32(dr[indexMUId]);

            int indexMisTake = dr.GetOrdinal("MisTake");
            if (dr["MisTake"] != DBNull.Value)
            {
                asset.MisTake = Convert.ToDecimal(dr[indexMisTake]);
            }

            int indexAmountPerHand = dr.GetOrdinal("AmountPerHand");
            if (dr["AmountPerHand"] != DBNull.Value)
            {
                asset.AmountPerHand = Convert.ToInt32(dr[indexAmountPerHand]);
            }

            int indexAssetStatus = dr.GetOrdinal("AssetStatus");
            asset.AssetStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexAssetStatus]);

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            asset.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            asset.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                asset.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                asset.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return asset;
        }

        public override string TableName
        {
            get
            {
                return "Asset";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Asset asset = (Asset)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = asset.AssetId;
            paras.Add(assetidpara);

            SqlParameter assetnamepara = new SqlParameter("@AssetName", SqlDbType.VarChar, 20);
            assetnamepara.Value = asset.AssetName;
            paras.Add(assetnamepara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = asset.MUId;
            paras.Add(muidpara);

            SqlParameter mistakepara = new SqlParameter("@MisTake", SqlDbType.Decimal, 9);
            mistakepara.Value = asset.MisTake;
            paras.Add(mistakepara);

            SqlParameter amountperhandpara = new SqlParameter("@AmountPerHand", SqlDbType.Int, 4);
            amountperhandpara.Value = asset.AmountPerHand;
            paras.Add(amountperhandpara);

            SqlParameter assetstatuspara = new SqlParameter("@AssetStatus", SqlDbType.Int, 4);
            assetstatuspara.Value = asset.AssetStatus;
            paras.Add(assetstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 重写方法

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "更新对象不能为null";
                    return result;
                }

                obj.LastModifyId = user.EmpId;

                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, string.Format("{0}Update", obj.TableName), CreateUpdateParameters(obj).ToArray());

                if (i == 1)
                {
                    result.Message = "更新成功";
                    result.ResultStatus = 0;
                }
                else
                    result.Message = "更新失败";

                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public override int MenuId
        {
            get
            {
                return 24;
            }
        }

        #endregion

        #region 新增方法

        public ResultModel LoadAssetAuth(UserModel user)
        {
            ResultModel result = new ResultModel();

            SqlDataReader dr = null;
            try
            {
                NFMT.Authority.AssetAuth auth = new NFMT.Authority.AssetAuth();
                auth.AuthColumnNames.Add("ass.AssetId");
                result = auth.CreateAuthorityStr(user);
                if (result.ResultStatus != 0)
                    return result;

                string cmdText = string.Format("select * from NFMT_Basic.dbo.Asset ass where 1=1 {0}", result.ReturnValue.ToString());

                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, null);
                List<Model.Asset> models = new List<Model.Asset>();

                int i = 0;
                while (dr.Read())
                {
                    Model.Asset model = this.CreateModel<Model.Asset>(dr);
                    models.Add(model);
                    i++;
                }

                result.AffectCount = i;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = models;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        #endregion
    }
}
