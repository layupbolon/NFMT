/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AreaDAL.cs
// 文件功能描述：地区表dbo.Area数据交互类。
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
    /// 地区表dbo.Area数据交互类。
    /// </summary>
    public class AreaDAL : DataOperate, IAreaDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AreaDAL()
        {
            //this.auth = new NFMT.Authority.AreaAuth();
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
            Area area = (Area)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@AreaId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter areanamepara = new SqlParameter("@AreaName", SqlDbType.VarChar, 50);
            areanamepara.Value = area.AreaName;
            paras.Add(areanamepara);

            SqlParameter areafullnamepara = new SqlParameter("@AreaFullName", SqlDbType.VarChar, 100);
            areafullnamepara.Value = area.AreaFullName;
            paras.Add(areafullnamepara);

            SqlParameter areashortpara = new SqlParameter("@AreaShort", SqlDbType.VarChar, 80);
            areashortpara.Value = area.AreaShort;
            paras.Add(areashortpara);

            if (!string.IsNullOrEmpty(area.AreaCode))
            {
                SqlParameter areacodepara = new SqlParameter("@AreaCode", SqlDbType.VarChar, 20);
                areacodepara.Value = area.AreaCode;
                paras.Add(areacodepara);
            }

            if (!string.IsNullOrEmpty(area.AreaZip))
            {
                SqlParameter areazippara = new SqlParameter("@AreaZip", SqlDbType.VarChar, 20);
                areazippara.Value = area.AreaZip;
                paras.Add(areazippara);
            }

            SqlParameter arealevelpara = new SqlParameter("@AreaLevel", SqlDbType.Int, 4);
            arealevelpara.Value = area.AreaLevel;
            paras.Add(arealevelpara);

            SqlParameter parentidpara = new SqlParameter("@ParentID", SqlDbType.Int, 4);
            parentidpara.Value = area.ParentID;
            paras.Add(parentidpara);

            SqlParameter areastatuspara = new SqlParameter("@AreaStatus", SqlDbType.Int, 4);
            areastatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(areastatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Area area = new Area();

            int indexAreaId = dr.GetOrdinal("AreaId");
            area.AreaId = Convert.ToInt32(dr[indexAreaId]);

            int indexAreaName = dr.GetOrdinal("AreaName");
            area.AreaName = Convert.ToString(dr[indexAreaName]);

            int indexAreaFullName = dr.GetOrdinal("AreaFullName");
            area.AreaFullName = Convert.ToString(dr[indexAreaFullName]);

            int indexAreaShort = dr.GetOrdinal("AreaShort");
            area.AreaShort = Convert.ToString(dr[indexAreaShort]);

            int indexAreaCode = dr.GetOrdinal("AreaCode");
            if (dr["AreaCode"] != DBNull.Value)
            {
                area.AreaCode = Convert.ToString(dr[indexAreaCode]);
            }

            int indexAreaZip = dr.GetOrdinal("AreaZip");
            if (dr["AreaZip"] != DBNull.Value)
            {
                area.AreaZip = Convert.ToString(dr[indexAreaZip]);
            }

            int indexAreaLevel = dr.GetOrdinal("AreaLevel");
            if (dr["AreaLevel"] != DBNull.Value)
            {
                area.AreaLevel = Convert.ToInt32(dr[indexAreaLevel]);
            }

            int indexParentID = dr.GetOrdinal("ParentID");
            if (dr["ParentID"] != DBNull.Value)
            {
                area.ParentID = Convert.ToInt32(dr[indexParentID]);
            }

            int indexAreaStatus = dr.GetOrdinal("AreaStatus");
            area.AreaStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexAreaStatus]);

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            area.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            area.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                area.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                area.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return area;
        }

        public override string TableName
        {
            get
            {
                return "Area";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Area area = (Area)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter areaidpara = new SqlParameter("@AreaId", SqlDbType.Int, 4);
            areaidpara.Value = area.AreaId;
            paras.Add(areaidpara);

            SqlParameter areanamepara = new SqlParameter("@AreaName", SqlDbType.VarChar, 50);
            areanamepara.Value = area.AreaName;
            paras.Add(areanamepara);

            SqlParameter areafullnamepara = new SqlParameter("@AreaFullName", SqlDbType.VarChar, 100);
            areafullnamepara.Value = area.AreaFullName;
            paras.Add(areafullnamepara);

            SqlParameter areashortpara = new SqlParameter("@AreaShort", SqlDbType.VarChar, 80);
            areashortpara.Value = area.AreaShort;
            paras.Add(areashortpara);

            if (!string.IsNullOrEmpty(area.AreaCode))
            {
                SqlParameter areacodepara = new SqlParameter("@AreaCode", SqlDbType.VarChar, 20);
                areacodepara.Value = area.AreaCode;
                paras.Add(areacodepara);
            }

            if (!string.IsNullOrEmpty(area.AreaZip))
            {
                SqlParameter areazippara = new SqlParameter("@AreaZip", SqlDbType.VarChar, 20);
                areazippara.Value = area.AreaZip;
                paras.Add(areazippara);
            }

            SqlParameter arealevelpara = new SqlParameter("@AreaLevel", SqlDbType.Int, 4);
            arealevelpara.Value = area.AreaLevel;
            paras.Add(arealevelpara);

            SqlParameter parentidpara = new SqlParameter("@ParentID", SqlDbType.Int, 4);
            parentidpara.Value = area.ParentID;
            paras.Add(parentidpara);

            SqlParameter areastatuspara = new SqlParameter("@AreaStatus", SqlDbType.Int, 4);
            areastatuspara.Value = area.AreaStatus;
            paras.Add(areastatuspara);

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
                return 29;
            }
        }

        #endregion
    }
}
