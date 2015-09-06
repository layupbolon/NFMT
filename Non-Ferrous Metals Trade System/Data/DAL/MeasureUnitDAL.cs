/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：MeasureUnitDAL.cs
// 文件功能描述：dbo.MeasureUnit数据交互类。
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
    /// dbo.MeasureUnit数据交互类。
    /// </summary>
    public class MeasureUnitDAL : DataOperate, IMeasureUnitDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MeasureUnitDAL()
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
            MeasureUnit measureunit = (MeasureUnit)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@MUId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter munamepara = new SqlParameter("@MUName", SqlDbType.VarChar, 20);
            munamepara.Value = measureunit.MUName;
            paras.Add(munamepara);

            SqlParameter baseidpara = new SqlParameter("@BaseId", SqlDbType.Int, 4);
            baseidpara.Value = measureunit.BaseId;
            paras.Add(baseidpara);

            SqlParameter transformratepara = new SqlParameter("@TransformRate", SqlDbType.Decimal, 5);
            transformratepara.Value = measureunit.TransformRate;
            paras.Add(transformratepara);

            SqlParameter mustatuspara = new SqlParameter("@MUStatus", SqlDbType.Int, 4);
            mustatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(mustatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            MeasureUnit measureunit = new MeasureUnit();

            int indexMUId = dr.GetOrdinal("MUId");
            measureunit.MUId = Convert.ToInt32(dr[indexMUId]);

            int indexMUName = dr.GetOrdinal("MUName");
            measureunit.MUName = Convert.ToString(dr[indexMUName]);

            int indexBaseId = dr.GetOrdinal("BaseId");
            if (dr["BaseId"] != DBNull.Value)
            {
                measureunit.BaseId = Convert.ToInt32(dr[indexBaseId]);
            }

            int indexTransformRate = dr.GetOrdinal("TransformRate");
            measureunit.TransformRate = Convert.ToDecimal(dr[indexTransformRate]);

            int indexMUStatus = dr.GetOrdinal("MUStatus");
            measureunit.MUStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexMUStatus]);

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            measureunit.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            measureunit.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                measureunit.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                measureunit.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return measureunit;
        }

        public override string TableName
        {
            get
            {
                return "MeasureUnit";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            MeasureUnit measureunit = (MeasureUnit)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = measureunit.MUId;
            paras.Add(muidpara);

            SqlParameter munamepara = new SqlParameter("@MUName", SqlDbType.VarChar, 20);
            munamepara.Value = measureunit.MUName;
            paras.Add(munamepara);

            SqlParameter baseidpara = new SqlParameter("@BaseId", SqlDbType.Int, 4);
            baseidpara.Value = measureunit.BaseId;
            paras.Add(baseidpara);

            SqlParameter transformratepara = new SqlParameter("@TransformRate", SqlDbType.Decimal, 5);
            transformratepara.Value = measureunit.TransformRate;
            paras.Add(transformratepara);

            SqlParameter mustatuspara = new SqlParameter("@MUStatus", SqlDbType.Int, 4);
            mustatuspara.Value = measureunit.MUStatus;
            paras.Add(mustatuspara);

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
                return 23;
            }
        }

        #endregion
    }
}
