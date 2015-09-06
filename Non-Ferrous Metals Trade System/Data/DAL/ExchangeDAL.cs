/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ExchangeDAL.cs
// 文件功能描述：dbo.Exchange数据交互类。
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
    /// dbo.Exchange数据交互类。
    /// </summary>
    public class ExchangeDAL : DataOperate, IExchangeDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ExchangeDAL()
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
            Exchange exchange = (Exchange)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ExchangeId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter exchangenamepara = new SqlParameter("@ExchangeName", SqlDbType.VarChar, 50);
            exchangenamepara.Value = exchange.ExchangeName;
            paras.Add(exchangenamepara);

            SqlParameter exchangecodepara = new SqlParameter("@ExchangeCode", SqlDbType.VarChar, 20);
            exchangecodepara.Value = exchange.ExchangeCode;
            paras.Add(exchangecodepara);

            SqlParameter exchangestatuspara = new SqlParameter("@ExchangeStatus", SqlDbType.Int, 4);
            exchangestatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(exchangestatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Exchange exchange = new Exchange();

            int indexExchangeId = dr.GetOrdinal("ExchangeId");
            exchange.ExchangeId = Convert.ToInt32(dr[indexExchangeId]);

            int indexExchangeName = dr.GetOrdinal("ExchangeName");
            exchange.ExchangeName = Convert.ToString(dr[indexExchangeName]);

            int indexExchangeCode = dr.GetOrdinal("ExchangeCode");
            exchange.ExchangeCode = Convert.ToString(dr[indexExchangeCode]);

            int indexExchangeStatus = dr.GetOrdinal("ExchangeStatus");
            exchange.ExchangeStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexExchangeStatus]);

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            exchange.CreatorId = Convert.ToInt32(dr[indexCreatorId]);

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            exchange.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                exchange.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                exchange.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return exchange;
        }

        public override string TableName
        {
            get
            {
                return "Exchange";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Exchange exchange = (Exchange)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter exchangeidpara = new SqlParameter("@ExchangeId", SqlDbType.Int, 4);
            exchangeidpara.Value = exchange.ExchangeId;
            paras.Add(exchangeidpara);

            SqlParameter exchangenamepara = new SqlParameter("@ExchangeName", SqlDbType.VarChar, 50);
            exchangenamepara.Value = exchange.ExchangeName;
            paras.Add(exchangenamepara);

            SqlParameter exchangecodepara = new SqlParameter("@ExchangeCode", SqlDbType.VarChar, 20);
            exchangecodepara.Value = exchange.ExchangeCode;
            paras.Add(exchangecodepara);

            SqlParameter exchangestatuspara = new SqlParameter("@ExchangeStatus", SqlDbType.Int, 4);
            exchangestatuspara.Value = exchange.ExchangeStatus;
            paras.Add(exchangestatuspara);

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
                return 33;
            }
        }

        #endregion
    }
}
