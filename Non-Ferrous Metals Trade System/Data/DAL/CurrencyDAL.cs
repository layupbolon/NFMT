/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CurrencyDAL.cs
// 文件功能描述：币种表dbo.Currency数据交互类。
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
    /// 币种表dbo.Currency数据交互类。
    /// </summary>
    public class CurrencyDAL : DataOperate, ICurrencyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CurrencyDAL()
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
            Currency currency = (Currency)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@CurrencyId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter currencynamepara = new SqlParameter("@CurrencyName", SqlDbType.VarChar, 20);
            currencynamepara.Value = currency.CurrencyName;
            paras.Add(currencynamepara);

            SqlParameter currencystatuspara = new SqlParameter("@CurrencyStatus", SqlDbType.Int, 4);
            currencystatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(currencystatuspara);

            if (!string.IsNullOrEmpty(currency.CurrencyFullName))
            {
                SqlParameter currencyfullnamepara = new SqlParameter("@CurrencyFullName", SqlDbType.VarChar, 20);
                currencyfullnamepara.Value = currency.CurrencyFullName;
                paras.Add(currencyfullnamepara);
            }

            if (!string.IsNullOrEmpty(currency.CurencyShort))
            {
                SqlParameter curencyshortpara = new SqlParameter("@CurencyShort", SqlDbType.VarChar, 20);
                curencyshortpara.Value = currency.CurencyShort;
                paras.Add(curencyshortpara);
            }

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Currency currency = new Currency();

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            currency.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);

            int indexCurrencyName = dr.GetOrdinal("CurrencyName");
            currency.CurrencyName = Convert.ToString(dr[indexCurrencyName]);

            int indexCurrencyStatus = dr.GetOrdinal("CurrencyStatus");
            currency.CurrencyStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexCurrencyStatus]);

            int indexCurrencyFullName = dr.GetOrdinal("CurrencyFullName");
            if (dr["CurrencyFullName"] != DBNull.Value)
            {
                currency.CurrencyFullName = Convert.ToString(dr[indexCurrencyFullName]);
            }

            int indexCurencyShort = dr.GetOrdinal("CurencyShort");
            if (dr["CurencyShort"] != DBNull.Value)
            {
                currency.CurencyShort = Convert.ToString(dr[indexCurencyShort]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            currency.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            currency.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                currency.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                currency.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return currency;
        }

        public override string TableName
        {
            get
            {
                return "Currency";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Currency currency = (Currency)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = currency.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter currencynamepara = new SqlParameter("@CurrencyName", SqlDbType.VarChar, 20);
            currencynamepara.Value = currency.CurrencyName;
            paras.Add(currencynamepara);

            SqlParameter currencystatuspara = new SqlParameter("@CurrencyStatus", SqlDbType.Int, 4);
            currencystatuspara.Value = currency.CurrencyStatus;
            paras.Add(currencystatuspara);

            if (!string.IsNullOrEmpty(currency.CurrencyFullName))
            {
                SqlParameter currencyfullnamepara = new SqlParameter("@CurrencyFullName", SqlDbType.VarChar, 20);
                currencyfullnamepara.Value = currency.CurrencyFullName;
                paras.Add(currencyfullnamepara);
            }

            if (!string.IsNullOrEmpty(currency.CurencyShort))
            {
                SqlParameter curencyshortpara = new SqlParameter("@CurencyShort", SqlDbType.VarChar, 20);
                curencyshortpara.Value = currency.CurencyShort;
                paras.Add(curencyshortpara);
            }

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
                return 25;
            }
        }

        #endregion
    }
}
