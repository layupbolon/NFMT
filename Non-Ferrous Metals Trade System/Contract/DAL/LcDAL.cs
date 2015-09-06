/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：LcDAL.cs
// 文件功能描述：信用证dbo.Con_Lc数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月23日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Contract.Model;
using NFMT.DBUtility;
using NFMT.Contract.IDAL;
using NFMT.Common;

namespace NFMT.Contract.DAL
{
    /// <summary>
    /// 信用证dbo.Con_Lc数据交互类。
    /// </summary>
    public class LcDAL : DataOperate, ILcDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public LcDAL()
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
            Lc con_lc = (Lc)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@LcId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter issuebankpara = new SqlParameter("@IssueBank", SqlDbType.Int, 4);
            issuebankpara.Value = con_lc.IssueBank;
            paras.Add(issuebankpara);

            SqlParameter advisebankpara = new SqlParameter("@AdviseBank", SqlDbType.Int, 4);
            advisebankpara.Value = con_lc.AdviseBank;
            paras.Add(advisebankpara);

            SqlParameter issuedatepara = new SqlParameter("@IssueDate", SqlDbType.DateTime, 8);
            issuedatepara.Value = con_lc.IssueDate;
            paras.Add(issuedatepara);

            SqlParameter futuredaypara = new SqlParameter("@FutureDay", SqlDbType.Int, 4);
            futuredaypara.Value = con_lc.FutureDay;
            paras.Add(futuredaypara);

            SqlParameter lcbalapara = new SqlParameter("@LcBala", SqlDbType.Decimal, 9);
            lcbalapara.Value = con_lc.LcBala;
            paras.Add(lcbalapara);

            SqlParameter currencypara = new SqlParameter("@Currency", SqlDbType.Int, 4);
            currencypara.Value = con_lc.Currency;
            paras.Add(currencypara);

            SqlParameter lcstatuspara = new SqlParameter("@LCStatus", SqlDbType.Int, 4);
            lcstatuspara.Value = (int)StatusEnum.已录入;
            paras.Add(lcstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Lc lc = new Lc();

            int indexLcId = dr.GetOrdinal("LcId");
            lc.LcId = Convert.ToInt32(dr[indexLcId]);

            int indexIssueBank = dr.GetOrdinal("IssueBank");
            if (dr["IssueBank"] != DBNull.Value)
            {
                lc.IssueBank = Convert.ToInt32(dr[indexIssueBank]);
            }

            int indexAdviseBank = dr.GetOrdinal("AdviseBank");
            if (dr["AdviseBank"] != DBNull.Value)
            {
                lc.AdviseBank = Convert.ToInt32(dr[indexAdviseBank]);
            }

            int indexIssueDate = dr.GetOrdinal("IssueDate");
            if (dr["IssueDate"] != DBNull.Value)
            {
                lc.IssueDate = Convert.ToDateTime(dr[indexIssueDate]);
            }

            int indexFutureDay = dr.GetOrdinal("FutureDay");
            if (dr["FutureDay"] != DBNull.Value)
            {
                lc.FutureDay = Convert.ToInt32(dr[indexFutureDay]);
            }

            int indexLcBala = dr.GetOrdinal("LcBala");
            if (dr["LcBala"] != DBNull.Value)
            {
                lc.LcBala = Convert.ToDecimal(dr[indexLcBala]);
            }

            int indexCurrency = dr.GetOrdinal("Currency");
            if (dr["Currency"] != DBNull.Value)
            {
                lc.Currency = Convert.ToInt32(dr[indexCurrency]);
            }

            int indexLCStatus = dr.GetOrdinal("LCStatus");
            if (dr["LCStatus"] != DBNull.Value)
            {
                lc.LCStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexLCStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                lc.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                lc.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                lc.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                lc.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return lc;
        }

        public override string TableName
        {
            get
            {
                return "Con_Lc";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Lc con_lc = (Lc)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter lcidpara = new SqlParameter("@LcId", SqlDbType.Int, 4);
            lcidpara.Value = con_lc.LcId;
            paras.Add(lcidpara);

            SqlParameter issuebankpara = new SqlParameter("@IssueBank", SqlDbType.Int, 4);
            issuebankpara.Value = con_lc.IssueBank;
            paras.Add(issuebankpara);

            SqlParameter advisebankpara = new SqlParameter("@AdviseBank", SqlDbType.Int, 4);
            advisebankpara.Value = con_lc.AdviseBank;
            paras.Add(advisebankpara);

            SqlParameter issuedatepara = new SqlParameter("@IssueDate", SqlDbType.DateTime, 8);
            issuedatepara.Value = con_lc.IssueDate;
            paras.Add(issuedatepara);

            SqlParameter futuredaypara = new SqlParameter("@FutureDay", SqlDbType.Int, 4);
            futuredaypara.Value = con_lc.FutureDay;
            paras.Add(futuredaypara);

            SqlParameter lcbalapara = new SqlParameter("@LcBala", SqlDbType.Decimal, 9);
            lcbalapara.Value = con_lc.LcBala;
            paras.Add(lcbalapara);

            SqlParameter currencypara = new SqlParameter("@Currency", SqlDbType.Int, 4);
            currencypara.Value = con_lc.Currency;
            paras.Add(currencypara);

            SqlParameter lcstatuspara = new SqlParameter("@LCStatus", SqlDbType.Int, 4);
            lcstatuspara.Value = con_lc.LCStatus;
            paras.Add(lcstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public override int MenuId
        {
            get
            {
                return 86;
            }
        }

        #endregion
    }
}
