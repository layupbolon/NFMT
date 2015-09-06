/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorpFundsAllotContractDAL.cs
// 文件功能描述：集团或公司款分配至合约dbo.Fun_CorpFundsAllotContract_Ref数据交互类。
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
    /// 集团或公司款分配至合约dbo.Fun_CorpFundsAllotContract_Ref数据交互类。
    /// </summary>
    public class CorpFundsAllotContractDAL : DataOperate, ICorpFundsAllotContractDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CorpFundsAllotContractDAL()
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
            CorpFundsAllotContract fun_corpfundsallotcontract_ref = (CorpFundsAllotContract)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_corpfundsallotcontract_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter receidpara = new SqlParameter("@ReceId", SqlDbType.Int, 4);
            receidpara.Value = fun_corpfundsallotcontract_ref.ReceId;
            paras.Add(receidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = fun_corpfundsallotcontract_ref.ContractId;
            paras.Add(contractidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = fun_corpfundsallotcontract_ref.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter allotgroupidpara = new SqlParameter("@AllotGroupId", SqlDbType.Int, 4);
            allotgroupidpara.Value = fun_corpfundsallotcontract_ref.AllotGroupId;
            paras.Add(allotgroupidpara);

            SqlParameter allotcorpidpara = new SqlParameter("@AllotCorpId", SqlDbType.Int, 4);
            allotcorpidpara.Value = fun_corpfundsallotcontract_ref.AllotCorpId;
            paras.Add(allotcorpidpara);

            SqlParameter fundsvaluepara = new SqlParameter("@FundsValue", SqlDbType.Decimal, 9);
            fundsvaluepara.Value = fun_corpfundsallotcontract_ref.FundsValue;
            paras.Add(fundsvaluepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_corpfundsallotcontract_ref.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            CorpFundsAllotContract corpfundsallotcontract = new CorpFundsAllotContract();

            int indexRefId = dr.GetOrdinal("RefId");
            corpfundsallotcontract.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexAllotId = dr.GetOrdinal("AllotId");
            if (dr["AllotId"] != DBNull.Value)
            {
                corpfundsallotcontract.AllotId = Convert.ToInt32(dr[indexAllotId]);
            }

            int indexReceId = dr.GetOrdinal("ReceId");
            if (dr["ReceId"] != DBNull.Value)
            {
                corpfundsallotcontract.ReceId = Convert.ToInt32(dr[indexReceId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                corpfundsallotcontract.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexSubContractId = dr.GetOrdinal("SubContractId");
            if (dr["SubContractId"] != DBNull.Value)
            {
                corpfundsallotcontract.SubContractId = Convert.ToInt32(dr[indexSubContractId]);
            }

            int indexAllotGroupId = dr.GetOrdinal("AllotGroupId");
            if (dr["AllotGroupId"] != DBNull.Value)
            {
                corpfundsallotcontract.AllotGroupId = Convert.ToInt32(dr[indexAllotGroupId]);
            }

            int indexAllotCorpId = dr.GetOrdinal("AllotCorpId");
            if (dr["AllotCorpId"] != DBNull.Value)
            {
                corpfundsallotcontract.AllotCorpId = Convert.ToInt32(dr[indexAllotCorpId]);
            }

            int indexFundsValue = dr.GetOrdinal("FundsValue");
            if (dr["FundsValue"] != DBNull.Value)
            {
                corpfundsallotcontract.FundsValue = Convert.ToDecimal(dr[indexFundsValue]);
            }

            int indexCurrencyId = dr.GetOrdinal("CurrencyId");
            if (dr["CurrencyId"] != DBNull.Value)
            {
                corpfundsallotcontract.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                corpfundsallotcontract.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                corpfundsallotcontract.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                corpfundsallotcontract.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                corpfundsallotcontract.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return corpfundsallotcontract;
        }

        public override string TableName
        {
            get
            {
                return "Fun_CorpFundsAllotContract_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CorpFundsAllotContract fun_corpfundsallotcontract_ref = (CorpFundsAllotContract)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = fun_corpfundsallotcontract_ref.RefId;
            paras.Add(refidpara);

            SqlParameter allotidpara = new SqlParameter("@AllotId", SqlDbType.Int, 4);
            allotidpara.Value = fun_corpfundsallotcontract_ref.AllotId;
            paras.Add(allotidpara);

            SqlParameter receidpara = new SqlParameter("@ReceId", SqlDbType.Int, 4);
            receidpara.Value = fun_corpfundsallotcontract_ref.ReceId;
            paras.Add(receidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = fun_corpfundsallotcontract_ref.ContractId;
            paras.Add(contractidpara);

            SqlParameter subcontractidpara = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
            subcontractidpara.Value = fun_corpfundsallotcontract_ref.SubContractId;
            paras.Add(subcontractidpara);

            SqlParameter allotgroupidpara = new SqlParameter("@AllotGroupId", SqlDbType.Int, 4);
            allotgroupidpara.Value = fun_corpfundsallotcontract_ref.AllotGroupId;
            paras.Add(allotgroupidpara);

            SqlParameter allotcorpidpara = new SqlParameter("@AllotCorpId", SqlDbType.Int, 4);
            allotcorpidpara.Value = fun_corpfundsallotcontract_ref.AllotCorpId;
            paras.Add(allotcorpidpara);

            SqlParameter fundsvaluepara = new SqlParameter("@FundsValue", SqlDbType.Decimal, 9);
            fundsvaluepara.Value = fun_corpfundsallotcontract_ref.FundsValue;
            paras.Add(fundsvaluepara);

            SqlParameter currencyidpara = new SqlParameter("@CurrencyId", SqlDbType.Int, 4);
            currencyidpara.Value = fun_corpfundsallotcontract_ref.CurrencyId;
            paras.Add(currencyidpara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
