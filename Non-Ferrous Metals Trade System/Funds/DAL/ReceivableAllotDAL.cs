/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ReceivableAllotDAL.cs
// 文件功能描述：收款分配dbo.Fun_ReceivableAllot数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
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
    /// 收款分配dbo.Fun_ReceivableAllot数据交互类。
    /// </summary>
    public class ReceivableAllotDAL : ApplyOperate , IReceivableAllotDAL
    {
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public ReceivableAllotDAL()
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
            ReceivableAllot fun_receivableallot = (ReceivableAllot)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ReceivableAllotId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_receivableallot.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter allottypepara = new SqlParameter("@AllotType", SqlDbType.Int, 4);
            allottypepara.Value = fun_receivableallot.AllotType;
            paras.Add(allottypepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_receivableallot.CurrencyId;
            paras.Add(currencyidpara);

            if (!string.IsNullOrEmpty(fun_receivableallot.AllotDesc))
            {
                SqlParameter allotdescpara = new SqlParameter("@AllotDesc", SqlDbType.VarChar, 400);
                allotdescpara.Value = fun_receivableallot.AllotDesc;
                paras.Add(allotdescpara);
            }

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = fun_receivableallot.EmpId;
            paras.Add(empidpara);

            SqlParameter allottimepara = new SqlParameter("@AllotTime", SqlDbType.DateTime, 8);
            allottimepara.Value = fun_receivableallot.AllotTime;
            paras.Add(allottimepara);

            SqlParameter allotstatuspara = new SqlParameter("@AllotStatus", SqlDbType.Int, 4);
            allotstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(allotstatuspara);

            SqlParameter allotfrompara = new SqlParameter("@AllotFrom", SqlDbType.Int, 4);
            allotfrompara.Value = fun_receivableallot.AllotFrom;
            paras.Add(allotfrompara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ReceivableAllot receivableallot = new ReceivableAllot();

            int indexReceivableAllotId = dr.GetOrdinal("ReceivableAllotId");
            receivableallot.ReceivableAllotId = Convert.ToInt32(dr[indexReceivableAllotId]);

            int indexAllotBala = dr.GetOrdinal("AllotBala");
            if (dr["AllotBala"] != DBNull.Value)
            {
                receivableallot.AllotBala = Convert.ToDecimal(dr[indexAllotBala]);
            }

            int indexAllotType = dr.GetOrdinal("AllotType");
            if (dr["AllotType"] != DBNull.Value)
            {
                receivableallot.AllotType = Convert.ToInt32(dr[indexAllotType]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                receivableallot.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexAllotDesc = dr.GetOrdinal("AllotDesc");
            if (dr["AllotDesc"] != DBNull.Value)
            {
                receivableallot.AllotDesc = Convert.ToString(dr[indexAllotDesc]);
            }

            int indexEmpId = dr.GetOrdinal("EmpId");
            if (dr["EmpId"] != DBNull.Value)
            {
                receivableallot.EmpId = Convert.ToInt32(dr[indexEmpId]);
            }

            int indexAllotTime = dr.GetOrdinal("AllotTime");
            if (dr["AllotTime"] != DBNull.Value)
            {
                receivableallot.AllotTime = Convert.ToDateTime(dr[indexAllotTime]);
            }

            int indexAllotStatus = dr.GetOrdinal("AllotStatus");
            if (dr["AllotStatus"] != DBNull.Value)
            {
                receivableallot.AllotStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexAllotStatus]);
            }

            int indexAllotFrom = dr.GetOrdinal("AllotFrom");
            if (dr["AllotFrom"] != DBNull.Value)
            {
                receivableallot.AllotFrom = Convert.ToInt32(dr[indexAllotFrom]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                receivableallot.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                receivableallot.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                receivableallot.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                receivableallot.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return receivableallot;
        }

        public override string TableName
        {
            get
            {
                return "Fun_ReceivableAllot";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ReceivableAllot fun_receivableallot = (ReceivableAllot)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter receivableallotidpara = new SqlParameter("@ReceivableAllotId", SqlDbType.Int, 4);
            receivableallotidpara.Value = fun_receivableallot.ReceivableAllotId;
            paras.Add(receivableallotidpara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_receivableallot.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter allottypepara = new SqlParameter("@AllotType", SqlDbType.Int, 4);
            allottypepara.Value = fun_receivableallot.AllotType;
            paras.Add(allottypepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_receivableallot.CurrencyId;
            paras.Add(currencyidpara);

            if (!string.IsNullOrEmpty(fun_receivableallot.AllotDesc))
            {
                SqlParameter allotdescpara = new SqlParameter("@AllotDesc", SqlDbType.VarChar, 400);
                allotdescpara.Value = fun_receivableallot.AllotDesc;
                paras.Add(allotdescpara);
            }

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = fun_receivableallot.EmpId;
            paras.Add(empidpara);

            SqlParameter allottimepara = new SqlParameter("@AllotTime", SqlDbType.DateTime, 8);
            allottimepara.Value = fun_receivableallot.AllotTime;
            paras.Add(allottimepara);

            SqlParameter allotstatuspara = new SqlParameter("@AllotStatus", SqlDbType.Int, 4);
            allotstatuspara.Value = fun_receivableallot.AllotStatus;
            paras.Add(allotstatuspara);

            SqlParameter allotfrompara = new SqlParameter("@AllotFrom", SqlDbType.Int, 4);
            allotfrompara.Value = fun_receivableallot.AllotFrom;
            paras.Add(allotfrompara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetSubContractAllotAmount(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select CONVERT(varchar,SUM(ISNULL(AllotBala,0))) +c.CurrencyName ");
                sb.Append(" from dbo.Fun_ReceivableAllot a left join NFMT_Basic.dbo.Currency c on a.CurrencyId =c.CurrencyId");
                sb.Append(" group by AllotBala,c.CurrencyName,ReceivableAllotId ");
                sb.AppendFormat(" having ReceivableAllotId in (select AllotId from  dbo.Fun_ContractReceivable_Ref where SubContractId ={0})", subId);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sb.ToString(), null);
                if (obj != null)
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = obj;
                }
                else
                {
                    result.Message = "获取失败";
                    result.ResultStatus = 0;
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }
            return result;
        }

        public ResultModel GetStockAllotAmount(UserModel user, int stockId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select CONVERT(varchar,SUM(ISNULL(AllotBala,0))) +c.CurrencyName ");
                sb.Append(" from dbo.Fun_ReceivableAllot a left join NFMT_Basic.dbo.Currency c on a.CurrencyId =c.CurrencyId");
                sb.Append(" group by AllotBala,c.CurrencyName,ReceivableAllotId ");
                sb.AppendFormat(" having ReceivableAllotId in (select AllotId from  dbo.Fun_StcokReceivable_Ref where StockId ={0})", stockId);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sb.ToString(), null);
                if (obj != null)
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = obj;
                }
                else
                {
                    result.Message = "获取失败";
                    result.ResultStatus = 0;
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }
            return result;
        }

        #endregion
    }
}
