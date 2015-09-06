/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInCorpDAL.cs
// 文件功能描述：收款分配至公司dbo.Fun_CashInCorp_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年9月19日
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
    /// 收款分配至公司dbo.Fun_CashInCorp_Ref数据交互类。
    /// </summary>
    public class CashInCorpDAL : DetailOperate, ICashInCorpDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CashInCorpDAL()
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
            CashInCorp fun_cashincorp_ref = (CashInCorp)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_cashincorp_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter blocidpara = new SqlParameter("@BlocId", SqlDbType.Int, 4);
            blocidpara.Value = fun_cashincorp_ref.BlocId;
            paras.Add(blocidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = fun_cashincorp_ref.CorpId;
            paras.Add(corpidpara);

            SqlParameter cashinidpara = new SqlParameter("@CashInId", SqlDbType.Int, 4);
            cashinidpara.Value = fun_cashincorp_ref.CashInId;
            paras.Add(cashinidpara);

            SqlParameter issharepara = new SqlParameter("@IsShare", SqlDbType.Bit, 1);
            issharepara.Value = fun_cashincorp_ref.IsShare;
            paras.Add(issharepara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_cashincorp_ref.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已生效;
            paras.Add(detailstatuspara);

            SqlParameter fundslogidpara = new SqlParameter("@FundsLogId", SqlDbType.Int, 4);
            fundslogidpara.Value = fun_cashincorp_ref.FundsLogId;
            paras.Add(fundslogidpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            CashInCorp cashincorp = new CashInCorp();

            cashincorp.RefId = Convert.ToInt32(dr["RefId"]);

            if (dr["AllotId"] != DBNull.Value)
            {
                cashincorp.AllotId = Convert.ToInt32(dr["AllotId"]);
            }

            if (dr["BlocId"] != DBNull.Value)
            {
                cashincorp.BlocId = Convert.ToInt32(dr["BlocId"]);
            }

            if (dr["CorpId"] != DBNull.Value)
            {
                cashincorp.CorpId = Convert.ToInt32(dr["CorpId"]);
            }

            if (dr["CashInId"] != DBNull.Value)
            {
                cashincorp.CashInId = Convert.ToInt32(dr["CashInId"]);
            }

            if (dr["IsShare"] != DBNull.Value)
            {
                cashincorp.IsShare = Convert.ToBoolean(dr["IsShare"]);
            }

            if (dr["AllotBala"] != DBNull.Value)
            {
                cashincorp.AllotBala = Convert.ToDecimal(dr["AllotBala"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                cashincorp.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }

            if (dr["FundsLogId"] != DBNull.Value)
            {
                cashincorp.FundsLogId = Convert.ToInt32(dr["FundsLogId"]);
            }


            return cashincorp;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            CashInCorp cashincorp = new CashInCorp();

            int indexRefId = dr.GetOrdinal("RefId");
            cashincorp.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexAllotId = dr.GetOrdinal("AllotId");
            if (dr["AllotId"] != DBNull.Value)
            {
                cashincorp.AllotId = Convert.ToInt32(dr[indexAllotId]);
            }

            int indexBlocId = dr.GetOrdinal("BlocId");
            if (dr["BlocId"] != DBNull.Value)
            {
                cashincorp.BlocId = Convert.ToInt32(dr[indexBlocId]);
            }

            int indexCorpId = dr.GetOrdinal("CorpId");
            if (dr["CorpId"] != DBNull.Value)
            {
                cashincorp.CorpId = Convert.ToInt32(dr[indexCorpId]);
            }

            int indexCashInId = dr.GetOrdinal("CashInId");
            if (dr["CashInId"] != DBNull.Value)
            {
                cashincorp.CashInId = Convert.ToInt32(dr[indexCashInId]);
            }

            int indexIsShare = dr.GetOrdinal("IsShare");
            if (dr["IsShare"] != DBNull.Value)
            {
                cashincorp.IsShare = Convert.ToBoolean(dr[indexIsShare]);
            }

            int indexAllotBala = dr.GetOrdinal("AllotBala");
            if (dr["AllotBala"] != DBNull.Value)
            {
                cashincorp.AllotBala = Convert.ToDecimal(dr[indexAllotBala]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                cashincorp.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexFundsLogId = dr.GetOrdinal("FundsLogId");
            if (dr["FundsLogId"] != DBNull.Value)
            {
                cashincorp.FundsLogId = Convert.ToInt32(dr[indexFundsLogId]);
            }


            return cashincorp;
        }

        public override string TableName
        {
            get
            {
                return "Fun_CashInCorp_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CashInCorp fun_cashincorp_ref = (CashInCorp)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = fun_cashincorp_ref.RefId;
            paras.Add(refidpara);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_cashincorp_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter blocidpara = new SqlParameter("@BlocId", SqlDbType.Int, 4);
            blocidpara.Value = fun_cashincorp_ref.BlocId;
            paras.Add(blocidpara);

            SqlParameter corpidpara = new SqlParameter("@CorpId", SqlDbType.Int, 4);
            corpidpara.Value = fun_cashincorp_ref.CorpId;
            paras.Add(corpidpara);

            SqlParameter cashinidpara = new SqlParameter("@CashInId", SqlDbType.Int, 4);
            cashinidpara.Value = fun_cashincorp_ref.CashInId;
            paras.Add(cashinidpara);

            SqlParameter issharepara = new SqlParameter("@IsShare", SqlDbType.Bit, 1);
            issharepara.Value = fun_cashincorp_ref.IsShare;
            paras.Add(issharepara);

            SqlParameter allotbalapara = new SqlParameter("@AllotBala", SqlDbType.Decimal, 9);
            allotbalapara.Value = fun_cashincorp_ref.AllotBala;
            paras.Add(allotbalapara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = fun_cashincorp_ref.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter fundslogidpara = new SqlParameter("@FundsLogId", SqlDbType.Int, 4);
            fundslogidpara.Value = fun_cashincorp_ref.FundsLogId;
            paras.Add(fundslogidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int allotId, NFMT.Common.StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.Fun_CashInCorp_Ref ref where ref.DetailStatus>={0} and ref.AllotId ={1}", (int)status, allotId);
            return Load<Model.CashInCorp>(user, CommandType.Text, cmdText);
        }

        public override int MenuId
        {
            get
            {
                return 56;
            }
        }

        public ResultModel InvalidAll(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Fun_CashInCorp_Ref set DetailStatus = {0} where AllotId = {1}", (int)Common.StatusEnum.已作废, allotId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                }
                else
                {
                    result.Message = "作废失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        #endregion
    }
}
