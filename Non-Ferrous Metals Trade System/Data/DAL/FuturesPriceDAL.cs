/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FuturesPriceDAL.cs
// 文件功能描述：期货价格表dbo.FuturesPrice数据交互类。
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
    /// 期货价格表dbo.FuturesPrice数据交互类。
    /// </summary>
    public class FuturesPriceDAL : DataOperate, IFuturesPriceDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FuturesPriceDAL()
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
            FuturesPrice futuresprice = (FuturesPrice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@FPId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter tradedatepara = new SqlParameter("@TradeDate", SqlDbType.DateTime, 8);
            tradedatepara.Value = futuresprice.TradeDate;
            paras.Add(tradedatepara);

            SqlParameter tradecodepara = new SqlParameter("@TradeCode", SqlDbType.VarChar, 80);
            tradecodepara.Value = futuresprice.TradeCode;
            paras.Add(tradecodepara);

            SqlParameter deliverdatepara = new SqlParameter("@DeliverDate", SqlDbType.DateTime, 8);
            deliverdatepara.Value = futuresprice.DeliverDate;
            paras.Add(deliverdatepara);

            SqlParameter settlepricepara = new SqlParameter("@SettlePrice", SqlDbType.Decimal, 9);
            settlepricepara.Value = futuresprice.SettlePrice;
            paras.Add(settlepricepara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            FuturesPrice futuresprice = new FuturesPrice();

            int indexFPId = dr.GetOrdinal("FPId");
            futuresprice.FPId = Convert.ToInt32(dr[indexFPId]);

            int indexTradeDate = dr.GetOrdinal("TradeDate");
            futuresprice.TradeDate = Convert.ToDateTime(dr[indexTradeDate]);

            int indexTradeCode = dr.GetOrdinal("TradeCode");
            futuresprice.TradeCode = Convert.ToString(dr[indexTradeCode]);

            int indexDeliverDate = dr.GetOrdinal("DeliverDate");
            futuresprice.DeliverDate = Convert.ToDateTime(dr[indexDeliverDate]);

            int indexSettlePrice = dr.GetOrdinal("SettlePrice");
            if (dr["SettlePrice"] != DBNull.Value)
            {
                futuresprice.SettlePrice = Convert.ToDecimal(dr[indexSettlePrice]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            futuresprice.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            futuresprice.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                futuresprice.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                futuresprice.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return futuresprice;
        }

        public override string TableName
        {
            get
            {
                return "FuturesPrice";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            FuturesPrice futuresprice = (FuturesPrice)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter fpidpara = new SqlParameter("@FPId", SqlDbType.Int, 4);
            fpidpara.Value = futuresprice.FPId;
            paras.Add(fpidpara);

            SqlParameter tradedatepara = new SqlParameter("@TradeDate", SqlDbType.DateTime, 8);
            tradedatepara.Value = futuresprice.TradeDate;
            paras.Add(tradedatepara);

            SqlParameter tradecodepara = new SqlParameter("@TradeCode", SqlDbType.VarChar, 80);
            tradecodepara.Value = futuresprice.TradeCode;
            paras.Add(tradecodepara);

            SqlParameter deliverdatepara = new SqlParameter("@DeliverDate", SqlDbType.DateTime, 8);
            deliverdatepara.Value = futuresprice.DeliverDate;
            paras.Add(deliverdatepara);

            SqlParameter settlepricepara = new SqlParameter("@SettlePrice", SqlDbType.Decimal, 9);
            settlepricepara.Value = futuresprice.SettlePrice;
            paras.Add(settlepricepara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
