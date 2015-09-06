/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInAllotDAL.cs
// 文件功能描述：收款分配dbo.Fun_CashInAllot数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月18日
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
    /// 收款分配dbo.Fun_CashInAllot数据交互类。
    /// </summary>
    public partial class CashInAllotDAL : ExecOperate, ICashInAllotDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CashInAllotDAL()
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
            CashInAllot fun_cashinallot = (CashInAllot)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@AllotId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_cashinallot.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter allottypepara = new SqlParameter("@AllotType", SqlDbType.Int, 4);
            allottypepara.Value = fun_cashinallot.AllotType;
            paras.Add(allottypepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_cashinallot.CurrencyId;
            paras.Add(currencyidpara);

            if (!string.IsNullOrEmpty(fun_cashinallot.AllotDesc))
            {
                SqlParameter allotdescpara = new SqlParameter("@AllotDesc", SqlDbType.VarChar, 400);
                allotdescpara.Value = fun_cashinallot.AllotDesc;
                paras.Add(allotdescpara);
            }

            SqlParameter alloterpara = new SqlParameter("@Alloter", SqlDbType.Int, 4);
            alloterpara.Value = fun_cashinallot.Alloter;
            paras.Add(alloterpara);

            SqlParameter allottimepara = new SqlParameter("@AllotTime", SqlDbType.DateTime, 8);
            allottimepara.Value = fun_cashinallot.AllotTime;
            paras.Add(allottimepara);

            SqlParameter allotstatuspara = new SqlParameter("@AllotStatus", SqlDbType.Int, 4);
            allotstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(allotstatuspara);

            SqlParameter allotfrompara = new SqlParameter("@AllotFrom", SqlDbType.Int, 4);
            allotfrompara.Value = fun_cashinallot.AllotFrom;
            paras.Add(allotfrompara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            CashInAllot cashinallot = new CashInAllot();

            int indexAllotId = dr.GetOrdinal("AllotId");
            cashinallot.AllotId = Convert.ToInt32(dr[indexAllotId]);

            int indexAllotBala = dr.GetOrdinal("AllotBala");
            if (dr["AllotBala"] != DBNull.Value)
            {
                cashinallot.AllotBala = Convert.ToDecimal(dr[indexAllotBala]);
            }

            int indexAllotType = dr.GetOrdinal("AllotType");
            if (dr["AllotType"] != DBNull.Value)
            {
                cashinallot.AllotType = Convert.ToInt32(dr[indexAllotType]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                cashinallot.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexAllotDesc = dr.GetOrdinal("AllotDesc");
            if (dr["AllotDesc"] != DBNull.Value)
            {
                cashinallot.AllotDesc = Convert.ToString(dr[indexAllotDesc]);
            }

            int indexAlloter = dr.GetOrdinal("Alloter");
            if (dr["Alloter"] != DBNull.Value)
            {
                cashinallot.Alloter = Convert.ToInt32(dr[indexAlloter]);
            }

            int indexAllotTime = dr.GetOrdinal("AllotTime");
            if (dr["AllotTime"] != DBNull.Value)
            {
                cashinallot.AllotTime = Convert.ToDateTime(dr[indexAllotTime]);
            }

            int indexAllotStatus = dr.GetOrdinal("AllotStatus");
            if (dr["AllotStatus"] != DBNull.Value)
            {
                cashinallot.AllotStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexAllotStatus]);
            }

            int indexAllotFrom = dr.GetOrdinal("AllotFrom");
            if (dr["AllotFrom"] != DBNull.Value)
            {
                cashinallot.AllotFrom = Convert.ToInt32(dr[indexAllotFrom]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                cashinallot.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                cashinallot.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                cashinallot.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                cashinallot.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return cashinallot;
        }

        public override string TableName
        {
            get
            {
                return "Fun_CashInAllot";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CashInAllot fun_cashinallot = (CashInAllot)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_cashinallot.AllotId;
            paras.Add(allotidpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_cashinallot.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter allottypepara = new SqlParameter("@AllotType", SqlDbType.Int, 4);
            allottypepara.Value = fun_cashinallot.AllotType;
            paras.Add(allottypepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_cashinallot.CurrencyId;
            paras.Add(currencyidpara);

            if (!string.IsNullOrEmpty(fun_cashinallot.AllotDesc))
            {
                SqlParameter allotdescpara = new SqlParameter("@AllotDesc", SqlDbType.VarChar, 400);
                allotdescpara.Value = fun_cashinallot.AllotDesc;
                paras.Add(allotdescpara);
            }

            SqlParameter alloterpara = new SqlParameter("@Alloter", SqlDbType.Int, 4);
            alloterpara.Value = fun_cashinallot.Alloter;
            paras.Add(alloterpara);

            SqlParameter allottimepara = new SqlParameter("@AllotTime", SqlDbType.DateTime, 8);
            allottimepara.Value = fun_cashinallot.AllotTime;
            paras.Add(allottimepara);

            SqlParameter allotstatuspara = new SqlParameter("@AllotStatus", SqlDbType.Int, 4);
            allotstatuspara.Value = fun_cashinallot.AllotStatus;
            paras.Add(allotstatuspara);

            SqlParameter allotfrompara = new SqlParameter("@AllotFrom", SqlDbType.Int, 4);
            allotfrompara.Value = fun_cashinallot.AllotFrom;
            paras.Add(allotfrompara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

    }
}
