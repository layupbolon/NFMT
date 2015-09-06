/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RateDAL.cs
// 文件功能描述：汇率dbo.Rate数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
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
    /// 汇率dbo.Rate数据交互类。
    /// </summary>
    public class RateDAL : DataOperate, IRateDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RateDAL()
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
            Rate rate = (Rate)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RateId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter fromcurrencyidpara = new SqlParameter("@FromCurrencyId", SqlDbType.Int, 4);
            fromcurrencyidpara.Value = rate.FromCurrencyId;
            paras.Add(fromcurrencyidpara);

            SqlParameter tocurrencyidpara = new SqlParameter("@ToCurrencyId", SqlDbType.Int, 4);
            tocurrencyidpara.Value = rate.ToCurrencyId;
            paras.Add(tocurrencyidpara);

            SqlParameter ratevaluepara = new SqlParameter("@RateValue", SqlDbType.Decimal, 9);
            ratevaluepara.Value = rate.RateValue;
            paras.Add(ratevaluepara);

            SqlParameter ratestatuspara = new SqlParameter("@RateStatus", SqlDbType.Int, 4);
            ratestatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(ratestatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Rate rate = new Rate();

            int indexRateId = dr.GetOrdinal("RateId");
            rate.RateId = Convert.ToInt32(dr[indexRateId]);

            int indexFromCurrencyId = dr.GetOrdinal("FromCurrencyId");
            if (dr["FromCurrencyId"] != DBNull.Value)
            {
                rate.FromCurrencyId = Convert.ToInt32(dr[indexFromCurrencyId]);
            }

            int indexToCurrencyId = dr.GetOrdinal("ToCurrencyId");
            if (dr["ToCurrencyId"] != DBNull.Value)
            {
                rate.ToCurrencyId = Convert.ToInt32(dr[indexToCurrencyId]);
            }

            int indexRateValue = dr.GetOrdinal("RateValue");
            if (dr["RateValue"] != DBNull.Value)
            {
                rate.RateValue = Convert.ToDecimal(dr[indexRateValue]);
            }

            int indexRateStatus = dr.GetOrdinal("RateStatus");
            if (dr["RateStatus"] != DBNull.Value)
            {
                rate.RateStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRateStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                rate.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                rate.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                rate.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                rate.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return rate;
        }

        public override string TableName
        {
            get
            {
                return "Rate";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Rate rate = (Rate)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter rateidpara = new SqlParameter("@RateId", SqlDbType.Int, 4);
            rateidpara.Value = rate.RateId;
            paras.Add(rateidpara);

            SqlParameter fromcurrencyidpara = new SqlParameter("@FromCurrencyId", SqlDbType.Int, 4);
            fromcurrencyidpara.Value = rate.FromCurrencyId;
            paras.Add(fromcurrencyidpara);

            SqlParameter tocurrencyidpara = new SqlParameter("@ToCurrencyId", SqlDbType.Int, 4);
            tocurrencyidpara.Value = rate.ToCurrencyId;
            paras.Add(tocurrencyidpara);

            SqlParameter ratevaluepara = new SqlParameter("@RateValue", SqlDbType.Decimal, 9);
            ratevaluepara.Value = rate.RateValue;
            paras.Add(ratevaluepara);

            SqlParameter ratestatuspara = new SqlParameter("@RateStatus", SqlDbType.Int, 4);
            ratestatuspara.Value = rate.RateStatus;
            paras.Add(ratestatuspara);

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
                return 26;
            }
        }

        #endregion

    }
}
