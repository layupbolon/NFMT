/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BrandDAL.cs
// 文件功能描述：dbo.Brand数据交互类。
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
    /// dbo.Brand数据交互类。
    /// </summary>
    public class BrandDAL : DataOperate, IBrandDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BrandDAL()
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
            Brand brand = (Brand)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@BrandId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter produceridpara = new SqlParameter("@ProducerId", SqlDbType.Int, 4);
            produceridpara.Value = brand.ProducerId;
            paras.Add(produceridpara);

            if (!string.IsNullOrEmpty(brand.BrandName))
            {
                SqlParameter brandnamepara = new SqlParameter("@BrandName", SqlDbType.VarChar, 80);
                brandnamepara.Value = brand.BrandName;
                paras.Add(brandnamepara);
            }

            if (!string.IsNullOrEmpty(brand.BrandFullName))
            {
                SqlParameter brandfullnamepara = new SqlParameter("@BrandFullName", SqlDbType.VarChar, 400);
                brandfullnamepara.Value = brand.BrandFullName;
                paras.Add(brandfullnamepara);
            }

            if (!string.IsNullOrEmpty(brand.BrandInfo))
            {
                SqlParameter brandinfopara = new SqlParameter("@BrandInfo", SqlDbType.VarChar, 800);
                brandinfopara.Value = brand.BrandInfo;
                paras.Add(brandinfopara);
            }

            SqlParameter brandstatuspara = new SqlParameter("@BrandStatus", SqlDbType.Int, 4);
            brandstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(brandstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Brand brand = new Brand();

            int indexBrandId = dr.GetOrdinal("BrandId");
            brand.BrandId = Convert.ToInt32(dr[indexBrandId]);

            int indexProducerId = dr.GetOrdinal("ProducerId");
            if (dr["ProducerId"] != DBNull.Value)
            {
                brand.ProducerId = Convert.ToInt32(dr[indexProducerId]);
            }

            int indexBrandName = dr.GetOrdinal("BrandName");
            if (dr["BrandName"] != DBNull.Value)
            {
                brand.BrandName = Convert.ToString(dr[indexBrandName]);
            }

            int indexBrandFullName = dr.GetOrdinal("BrandFullName");
            if (dr["BrandFullName"] != DBNull.Value)
            {
                brand.BrandFullName = Convert.ToString(dr[indexBrandFullName]);
            }

            int indexBrandInfo = dr.GetOrdinal("BrandInfo");
            if (dr["BrandInfo"] != DBNull.Value)
            {
                brand.BrandInfo = Convert.ToString(dr[indexBrandInfo]);
            }

            int indexBrandStatus = dr.GetOrdinal("BrandStatus");
            if (dr["BrandStatus"] != DBNull.Value)
            {
                brand.BrandStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexBrandStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                brand.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                brand.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                brand.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                brand.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return brand;
        }

        public override string TableName
        {
            get
            {
                return "Brand";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Brand brand = (Brand)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter brandidpara = new SqlParameter("@BrandId", SqlDbType.Int, 4);
            brandidpara.Value = brand.BrandId;
            paras.Add(brandidpara);

            SqlParameter produceridpara = new SqlParameter("@ProducerId", SqlDbType.Int, 4);
            produceridpara.Value = brand.ProducerId;
            paras.Add(produceridpara);

            if (!string.IsNullOrEmpty(brand.BrandName))
            {
                SqlParameter brandnamepara = new SqlParameter("@BrandName", SqlDbType.VarChar, 80);
                brandnamepara.Value = brand.BrandName;
                paras.Add(brandnamepara);
            }

            if (!string.IsNullOrEmpty(brand.BrandFullName))
            {
                SqlParameter brandfullnamepara = new SqlParameter("@BrandFullName", SqlDbType.VarChar, 400);
                brandfullnamepara.Value = brand.BrandFullName;
                paras.Add(brandfullnamepara);
            }

            if (!string.IsNullOrEmpty(brand.BrandInfo))
            {
                SqlParameter brandinfopara = new SqlParameter("@BrandInfo", SqlDbType.VarChar, 800);
                brandinfopara.Value = brand.BrandInfo;
                paras.Add(brandinfopara);
            }

            SqlParameter brandstatuspara = new SqlParameter("@BrandStatus", SqlDbType.Int, 4);
            brandstatuspara.Value = brand.BrandStatus;
            paras.Add(brandstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 重载方法

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
                return 32;
            }
        }

        #endregion
    }
}
