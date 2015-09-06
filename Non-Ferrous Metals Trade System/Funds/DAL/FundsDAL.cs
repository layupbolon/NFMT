/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FundsDAL.cs
// 文件功能描述：资金dbo.Fun_Funds数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Funds.Model;
using NFMT.DBUtility;
using NFMT.Funds.IDAL;
using NFMT.Common;

namespace NFMT.Funds.DAL
{
    /// <summary>
    /// 资金dbo.Fun_Funds数据交互类。
    /// </summary>
    public class FundsDAL : DataOperate, IFundsDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FundsDAL()
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
            Model.Funds fun_funds = (Model.Funds)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@FundsId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter groupidpara = new SqlParameter("@GroupId", SqlDbType.Int, 4);
            groupidpara.Value = fun_funds.GroupId;
            paras.Add(groupidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = fun_funds.CorpId;
            paras.Add(corpidpara);

            SqlParameter fundsvaluepara = new SqlParameter("@FundsValue", SqlDbType.Decimal, 9);
            fundsvaluepara.Value = fun_funds.FundsValue;
            paras.Add(fundsvaluepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_funds.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter fundstypepara = new SqlParameter("@FundsType", SqlDbType.Int, 4);
            fundstypepara.Value = fun_funds.FundsType;
            paras.Add(fundstypepara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Model.Funds funds = new Model.Funds();

            int indexFundsId = dr.GetOrdinal("FundsId");
            funds.FundsId = Convert.ToInt32(dr[indexFundsId]);

            int indexGroupId = dr.GetOrdinal("GroupId");
            if (dr["GroupId"] != DBNull.Value)
            {
                funds.GroupId = Convert.ToInt32(dr[indexGroupId]);
            }

            int indexCorpId = dr.GetOrdinal("CorpId");
            if (dr["CorpId"] != DBNull.Value)
            {
                funds.CorpId = Convert.ToInt32(dr[indexCorpId]);
            }

            int indexFundsValue = dr.GetOrdinal("FundsValue");
            if (dr["FundsValue"] != DBNull.Value)
            {
                funds.FundsValue = Convert.ToDecimal(dr[indexFundsValue]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                funds.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexFundsType = dr.GetOrdinal("FundsType");
            if (dr["FundsType"] != DBNull.Value)
            {
                funds.FundsType = Convert.ToInt32(dr[indexFundsType]);
            }


            return funds;
        }

        public override string TableName
        {
            get
            {
                return "Fun_Funds";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Model.Funds fun_funds = (Model.Funds)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter fundsidpara = new SqlParameter("@FundsId", SqlDbType.Int, 4);
            fundsidpara.Value = fun_funds.FundsId;
            paras.Add(fundsidpara);

            SqlParameter groupidpara = new SqlParameter("@GroupId", SqlDbType.Int, 4);
            groupidpara.Value = fun_funds.GroupId;
            paras.Add(groupidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = fun_funds.CorpId;
            paras.Add(corpidpara);

            SqlParameter fundsvaluepara = new SqlParameter("@FundsValue", SqlDbType.Decimal, 9);
            fundsvaluepara.Value = fun_funds.FundsValue;
            paras.Add(fundsvaluepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_funds.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter fundstypepara = new SqlParameter("@FundsType", SqlDbType.Int, 4);
            fundstypepara.Value = fun_funds.FundsType;
            paras.Add(fundstypepara);


            return paras;
        }

        #endregion

        ///// <summary>
        ///// 资金更新
        ///// </summary>
        ///// <param name="user">用户</param>
        ///// <param name="fundsLog">资金流水</param>
        ///// <param name="handleType">操作类型。如"+"，"-"</param>
        ///// <returns></returns>
        //public ResultModel FundsUpdate(UserModel user, Model.FundsLog fundsLog, string handleType)
        //{
        //    ResultModel result = new ResultModel();

        //    try
        //    {
        //        string sql = string.Format("update dbo.Fun_Funds set FundsValue = FundsValue {0} {1} where CorpId = {2} and CurrencyId = {3} and FundsType = {4}", handleType, fundsLog.FundsValue, fundsLog.CorpId, fundsLog.CurrencyId, fundsLog.FundsType);
        //        int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(NFMT.DBUtility.SqlHelper.ConnectionStringNFMT, CommandType.Text, sql, null);

        //        if (i > 0)
        //        {
        //            result.ResultStatus = 0;
        //            result.Message = "资金处理成功";
        //            result.AffectCount = i;
        //        }
        //        else
        //        {
        //            result.ResultStatus = -1;
        //            result.Message = "资金处理失败";
        //            result.AffectCount = i;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        result.ResultStatus = -1;
        //        result.Message = string.Format("资金处理失败,{0}", e.Message);
        //    }

        //    return result;
        //}

    }
}
