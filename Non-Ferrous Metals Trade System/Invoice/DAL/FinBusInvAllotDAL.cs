/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FinBusInvAllotDAL.cs
// 文件功能描述：业务发票财务发票分配dbo.Inv_FinBusInvAllot数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月24日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Invoice.Model;
using NFMT.DBUtility;
using NFMT.Invoice.IDAL;
using NFMT.Common;

namespace NFMT.Invoice.DAL
{
    /// <summary>
    /// 业务发票财务发票分配dbo.Inv_FinBusInvAllot数据交互类。
    /// </summary>
    public class FinBusInvAllotDAL : ExecOperate, IFinBusInvAllotDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FinBusInvAllotDAL()
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
            FinBusInvAllot inv_finbusinvallot = (FinBusInvAllot)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@AllotId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = inv_finbusinvallot.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = inv_finbusinvallot.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter alloterpara = new SqlParameter("@Alloter", SqlDbType.Int, 4);
            alloterpara.Value = inv_finbusinvallot.Alloter;
            paras.Add(alloterpara);

            SqlParameter allotdatepara = new SqlParameter("@AllotDate", SqlDbType.DateTime, 8);
            allotdatepara.Value = inv_finbusinvallot.AllotDate;
            paras.Add(allotdatepara);

            SqlParameter allotstatuspara = new SqlParameter("@AllotStatus", SqlDbType.Int, 4);
            allotstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(allotstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            FinBusInvAllot finbusinvallot = new FinBusInvAllot();

            int indexAllotId = dr.GetOrdinal("AllotId");
            finbusinvallot.AllotId = Convert.ToInt32(dr[indexAllotId]);

            int indexAllotBala = dr.GetOrdinal("AllotBala");
            if (dr["AllotBala"] != DBNull.Value)
            {
                finbusinvallot.AllotBala = Convert.ToDecimal(dr[indexAllotBala]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                finbusinvallot.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexAlloter = dr.GetOrdinal("Alloter");
            if (dr["Alloter"] != DBNull.Value)
            {
                finbusinvallot.Alloter = Convert.ToInt32(dr[indexAlloter]);
            }

            int indexAllotDate = dr.GetOrdinal("AllotDate");
            if (dr["AllotDate"] != DBNull.Value)
            {
                finbusinvallot.AllotDate = Convert.ToDateTime(dr[indexAllotDate]);
            }

            int indexAllotStatus = dr.GetOrdinal("AllotStatus");
            if (dr["AllotStatus"] != DBNull.Value)
            {
                finbusinvallot.AllotStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexAllotStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                finbusinvallot.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                finbusinvallot.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                finbusinvallot.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                finbusinvallot.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return finbusinvallot;
        }

        public override string TableName
        {
            get
            {
                return "Inv_FinBusInvAllot";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            FinBusInvAllot inv_finbusinvallot = (FinBusInvAllot)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = inv_finbusinvallot.AllotId;
            paras.Add(allotidpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = inv_finbusinvallot.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = inv_finbusinvallot.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter alloterpara = new SqlParameter("@Alloter", SqlDbType.Int, 4);
            alloterpara.Value = inv_finbusinvallot.Alloter;
            paras.Add(alloterpara);

            SqlParameter allotdatepara = new SqlParameter("@AllotDate", SqlDbType.DateTime, 8);
            allotdatepara.Value = inv_finbusinvallot.AllotDate;
            paras.Add(allotdatepara);

            SqlParameter allotstatuspara = new SqlParameter("@AllotStatus", SqlDbType.Int, 4);
            allotstatuspara.Value = inv_finbusinvallot.AllotStatus;
            paras.Add(allotstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetFinanceInvoiceId(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select top 1 FinanceInvoiceId from dbo.Inv_FinBusInvAllotDetail where AllotId = {0} ", allotId);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                int fId = 0;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && int.TryParse(obj.ToString(), out fId) && fId > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = fId;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public override int MenuId
        {
            get
            {
                return 90;
            }
        }

        #endregion
    }
}
