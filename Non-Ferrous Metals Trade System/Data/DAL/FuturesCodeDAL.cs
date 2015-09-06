/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FuturesCodeDAL.cs
// 文件功能描述：期货合约dbo.FuturesCode数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月14日
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
    /// 期货合约dbo.FuturesCode数据交互类。
    /// </summary>
    public class FuturesCodeDAL : DataOperate, IFuturesCodeDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FuturesCodeDAL()
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
            FuturesCode futurescode = (FuturesCode)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@FuturesCodeId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter exchageidpara = new SqlParameter("@ExchageId", SqlDbType.Int, 4);
            exchageidpara.Value = futurescode.ExchageId;
            paras.Add(exchageidpara);

            SqlParameter codesizepara = new SqlParameter("@CodeSize", SqlDbType.Decimal, 9);
            codesizepara.Value = futurescode.CodeSize;
            paras.Add(codesizepara);

            SqlParameter firsttradedatepara = new SqlParameter("@FirstTradeDate", SqlDbType.DateTime, 8);
            firsttradedatepara.Value = futurescode.FirstTradeDate;
            paras.Add(firsttradedatepara);

            SqlParameter lasttradedatepara = new SqlParameter("@LastTradeDate", SqlDbType.DateTime, 8);
            lasttradedatepara.Value = futurescode.LastTradeDate;
            paras.Add(lasttradedatepara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = futurescode.MUId;
            paras.Add(muidpara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = futurescode.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = futurescode.AssetId;
            paras.Add(assetidpara);

            SqlParameter futurescodestatuspara = new SqlParameter("@FuturesCodeStatus", SqlDbType.Int, 4);
            futurescodestatuspara.Value = futurescode.FuturesCodeStatus;
            paras.Add(futurescodestatuspara);

            if (!string.IsNullOrEmpty(futurescode.TradeCode))
            {
                SqlParameter tradecodepara = new SqlParameter("@TradeCode", SqlDbType.VarChar, 80);
                tradecodepara.Value = futurescode.TradeCode;
                paras.Add(tradecodepara);
            }

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            FuturesCode futurescode = new FuturesCode();

            int indexFuturesCodeId = dr.GetOrdinal("FuturesCodeId");
            futurescode.FuturesCodeId = Convert.ToInt32(dr[indexFuturesCodeId]);

            int indexExchageId = dr.GetOrdinal("ExchageId");
            if (dr["ExchageId"] != DBNull.Value)
            {
                futurescode.ExchageId = Convert.ToInt32(dr[indexExchageId]);
            }

            int indexCodeSize = dr.GetOrdinal("CodeSize");
            if (dr["CodeSize"] != DBNull.Value)
            {
                futurescode.CodeSize = Convert.ToDecimal(dr[indexCodeSize]);
            }

            int indexFirstTradeDate = dr.GetOrdinal("FirstTradeDate");
            if (dr["FirstTradeDate"] != DBNull.Value)
            {
                futurescode.FirstTradeDate = Convert.ToDateTime(dr[indexFirstTradeDate]);
            }

            int indexLastTradeDate = dr.GetOrdinal("LastTradeDate");
            if (dr["LastTradeDate"] != DBNull.Value)
            {
                futurescode.LastTradeDate = Convert.ToDateTime(dr[indexLastTradeDate]);
            }

            int indexMUId = dr.GetOrdinal("MUId");
            if (dr["MUId"] != DBNull.Value)
            {
                futurescode.MUId = Convert.ToInt32(dr[indexMUId]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                futurescode.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexAssetId = dr.GetOrdinal("AssetId");
            if (dr["AssetId"] != DBNull.Value)
            {
                futurescode.AssetId = Convert.ToInt32(dr[indexAssetId]);
            }

            int indexFuturesCodeStatus = dr.GetOrdinal("FuturesCodeStatus");
            if (dr["FuturesCodeStatus"] != DBNull.Value)
            {
                futurescode.FuturesCodeStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexFuturesCodeStatus]);
            }

            int indexTradeCode = dr.GetOrdinal("TradeCode");
            if (dr["TradeCode"] != DBNull.Value)
            {
                futurescode.TradeCode = Convert.ToString(dr[indexTradeCode]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                futurescode.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                futurescode.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                futurescode.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                futurescode.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return futurescode;
        }

        public override string TableName
        {
            get
            {
                return "FuturesCode";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            FuturesCode futurescode = (FuturesCode)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter futurescodeidpara = new SqlParameter("@FuturesCodeId", SqlDbType.Int, 4);
            futurescodeidpara.Value = futurescode.FuturesCodeId;
            paras.Add(futurescodeidpara);

            SqlParameter exchageidpara = new SqlParameter("@ExchageId", SqlDbType.Int, 4);
            exchageidpara.Value = futurescode.ExchageId;
            paras.Add(exchageidpara);

            SqlParameter codesizepara = new SqlParameter("@CodeSize", SqlDbType.Decimal, 9);
            codesizepara.Value = futurescode.CodeSize;
            paras.Add(codesizepara);

            SqlParameter firsttradedatepara = new SqlParameter("@FirstTradeDate", SqlDbType.DateTime, 8);
            firsttradedatepara.Value = futurescode.FirstTradeDate;
            paras.Add(firsttradedatepara);

            SqlParameter lasttradedatepara = new SqlParameter("@LastTradeDate", SqlDbType.DateTime, 8);
            lasttradedatepara.Value = futurescode.LastTradeDate;
            paras.Add(lasttradedatepara);

            SqlParameter muidpara = new SqlParameter("@MUId", SqlDbType.Int, 4);
            muidpara.Value = futurescode.MUId;
            paras.Add(muidpara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = futurescode.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = futurescode.AssetId;
            paras.Add(assetidpara);

            SqlParameter futurescodestatuspara = new SqlParameter("@FuturesCodeStatus", SqlDbType.Int, 4);
            futurescodestatuspara.Value = futurescode.FuturesCodeStatus;
            paras.Add(futurescodestatuspara);

            if (!string.IsNullOrEmpty(futurescode.TradeCode))
            {
                SqlParameter tradecodepara = new SqlParameter("@TradeCode", SqlDbType.VarChar, 80);
                tradecodepara.Value = futurescode.TradeCode;
                paras.Add(tradecodepara);
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
                return 34;
            }
        }

        #endregion
    }
}
