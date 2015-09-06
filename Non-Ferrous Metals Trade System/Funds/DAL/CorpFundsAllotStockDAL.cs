/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorpFundsAllotStockDAL.cs
// 文件功能描述：公司款分配至库存dbo.Fun_CorpFundsAllotStock_Ref数据交互类。
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
    /// 公司款分配至库存dbo.Fun_CorpFundsAllotStock_Ref数据交互类。
    /// </summary>
    public class CorpFundsAllotStockDAL : DataOperate, ICorpFundsAllotStockDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CorpFundsAllotStockDAL()
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
            CorpFundsAllotStock fun_corpfundsallotstock_ref = (CorpFundsAllotStock)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_corpfundsallotstock_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter receidpara = new SqlParameter("@ReceId", SqlDbType.Int, 4);
            receidpara.Value = fun_corpfundsallotstock_ref.ReceId;
            paras.Add(receidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = fun_corpfundsallotstock_ref.StockId;
            paras.Add(stockidpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_corpfundsallotstock_ref.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_corpfundsallotstock_ref.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            CorpFundsAllotStock corpfundsallotstock = new CorpFundsAllotStock();

            int indexRefId = dr.GetOrdinal("RefId");
            corpfundsallotstock.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexAllotId = dr.GetOrdinal("AllotId");
            if (dr["AllotId"] != DBNull.Value)
            {
                corpfundsallotstock.AllotId = Convert.ToInt32(dr[indexAllotId]);
            }

            int indexReceId = dr.GetOrdinal("ReceId");
            if (dr["ReceId"] != DBNull.Value)
            {
                corpfundsallotstock.ReceId = Convert.ToInt32(dr[indexReceId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                corpfundsallotstock.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexAllotBala = dr.GetOrdinal("AllotBala");
            if (dr["AllotBala"] != DBNull.Value)
            {
                corpfundsallotstock.AllotBala = Convert.ToDecimal(dr[indexAllotBala]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                corpfundsallotstock.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                corpfundsallotstock.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                corpfundsallotstock.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                corpfundsallotstock.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                corpfundsallotstock.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return corpfundsallotstock;
        }

        public override string TableName
        {
            get
            {
                return "Fun_CorpFundsAllotStock_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CorpFundsAllotStock fun_corpfundsallotstock_ref = (CorpFundsAllotStock)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = fun_corpfundsallotstock_ref.RefId;
            paras.Add(refidpara);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_corpfundsallotstock_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter receidpara = new SqlParameter("@ReceId", SqlDbType.Int, 4);
            receidpara.Value = fun_corpfundsallotstock_ref.ReceId;
            paras.Add(receidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = fun_corpfundsallotstock_ref.StockId;
            paras.Add(stockidpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_corpfundsallotstock_ref.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_corpfundsallotstock_ref.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
