/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractSubQPDAL.cs
// 文件功能描述：子合约QPdbo.Con_ContractSubQP数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
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
    /// 子合约QPdbo.Con_ContractSubQP数据交互类。
    /// </summary>
    public class ContractSubQPDAL : DataOperate, IContractSubQPDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractSubQPDAL()
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
            ContractSubQP con_contractsubqp = (ContractSubQP)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@QPId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = con_contractsubqp.SubId;
            paras.Add(subidpara);

            SqlParameter curqppara = new SqlParameter("@CurQP", SqlDbType.Int, 4);
            curqppara.Value = con_contractsubqp.CurQP;
            paras.Add(curqppara);

            SqlParameter initamountpara = new SqlParameter("@InitAmount", SqlDbType.Decimal, 9);
            initamountpara.Value = con_contractsubqp.InitAmount;
            paras.Add(initamountpara);

            SqlParameter chgedamountpara = new SqlParameter("@ChgedAmount", SqlDbType.Decimal, 9);
            chgedamountpara.Value = con_contractsubqp.ChgedAmount;
            paras.Add(chgedamountpara);

            SqlParameter qpchgdatepara = new SqlParameter("@QPChgDate", SqlDbType.Int, 4);
            qpchgdatepara.Value = con_contractsubqp.QPChgDate;
            paras.Add(qpchgdatepara);

            SqlParameter qpfrompara = new SqlParameter("@QPFrom", SqlDbType.Int, 4);
            qpfrompara.Value = con_contractsubqp.QPFrom;
            paras.Add(qpfrompara);

            SqlParameter thisqpfeebalapara = new SqlParameter("@ThisQPFeeBala", SqlDbType.Decimal, 9);
            thisqpfeebalapara.Value = con_contractsubqp.ThisQPFeeBala;
            paras.Add(thisqpfeebalapara);

            SqlParameter qpmemopara = new SqlParameter("@QPMemo", SqlDbType.VarChar, 200);
            qpmemopara.Value = con_contractsubqp.QPMemo;
            paras.Add(qpmemopara);

            SqlParameter totalinitfeebalapara = new SqlParameter("@TotalInitFeeBala", SqlDbType.Decimal, 9);
            totalinitfeebalapara.Value = con_contractsubqp.TotalInitFeeBala;
            paras.Add(totalinitfeebalapara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractSubQP contractsubqp = new ContractSubQP();

            int indexQPId = dr.GetOrdinal("QPId");
            contractsubqp.QPId = Convert.ToInt32(dr[indexQPId]);

            int indexSubId = dr.GetOrdinal("SubId");
            if (dr["SubId"] != DBNull.Value)
            {
                contractsubqp.SubId = Convert.ToInt32(dr[indexSubId]);
            }

            int indexCurQP = dr.GetOrdinal("CurQP");
            if (dr["CurQP"] != DBNull.Value)
            {
                contractsubqp.CurQP = Convert.ToInt32(dr[indexCurQP]);
            }

            int indexInitAmount = dr.GetOrdinal("InitAmount");
            contractsubqp.InitAmount = Convert.ToDecimal(dr[indexInitAmount]);

            int indexChgedAmount = dr.GetOrdinal("ChgedAmount");
            contractsubqp.ChgedAmount = Convert.ToDecimal(dr[indexChgedAmount]);

            int indexQPChgDate = dr.GetOrdinal("QPChgDate");
            contractsubqp.QPChgDate = Convert.ToInt32(dr[indexQPChgDate]);

            int indexQPFrom = dr.GetOrdinal("QPFrom");
            contractsubqp.QPFrom = Convert.ToInt32(dr[indexQPFrom]);

            int indexThisQPFeeBala = dr.GetOrdinal("ThisQPFeeBala");
            contractsubqp.ThisQPFeeBala = Convert.ToDecimal(dr[indexThisQPFeeBala]);

            int indexQPMemo = dr.GetOrdinal("QPMemo");
            contractsubqp.QPMemo = Convert.ToString(dr[indexQPMemo]);

            int indexTotalInitFeeBala = dr.GetOrdinal("TotalInitFeeBala");
            contractsubqp.TotalInitFeeBala = Convert.ToDecimal(dr[indexTotalInitFeeBala]);

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                contractsubqp.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                contractsubqp.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                contractsubqp.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                contractsubqp.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return contractsubqp;
        }

        public override string TableName
        {
            get
            {
                return "Con_ContractSubQP";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractSubQP con_contractsubqp = (ContractSubQP)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter qpidpara = new SqlParameter("@QPId", SqlDbType.Int, 4);
            qpidpara.Value = con_contractsubqp.QPId;
            paras.Add(qpidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = con_contractsubqp.SubId;
            paras.Add(subidpara);

            SqlParameter curqppara = new SqlParameter("@CurQP", SqlDbType.Int, 4);
            curqppara.Value = con_contractsubqp.CurQP;
            paras.Add(curqppara);

            SqlParameter initamountpara = new SqlParameter("@InitAmount", SqlDbType.Decimal, 9);
            initamountpara.Value = con_contractsubqp.InitAmount;
            paras.Add(initamountpara);

            SqlParameter chgedamountpara = new SqlParameter("@ChgedAmount", SqlDbType.Decimal, 9);
            chgedamountpara.Value = con_contractsubqp.ChgedAmount;
            paras.Add(chgedamountpara);

            SqlParameter qpchgdatepara = new SqlParameter("@QPChgDate", SqlDbType.Int, 4);
            qpchgdatepara.Value = con_contractsubqp.QPChgDate;
            paras.Add(qpchgdatepara);

            SqlParameter qpfrompara = new SqlParameter("@QPFrom", SqlDbType.Int, 4);
            qpfrompara.Value = con_contractsubqp.QPFrom;
            paras.Add(qpfrompara);

            SqlParameter thisqpfeebalapara = new SqlParameter("@ThisQPFeeBala", SqlDbType.Decimal, 9);
            thisqpfeebalapara.Value = con_contractsubqp.ThisQPFeeBala;
            paras.Add(thisqpfeebalapara);

            SqlParameter qpmemopara = new SqlParameter("@QPMemo", SqlDbType.VarChar, 200);
            qpmemopara.Value = con_contractsubqp.QPMemo;
            paras.Add(qpmemopara);

            SqlParameter totalinitfeebalapara = new SqlParameter("@TotalInitFeeBala", SqlDbType.Decimal, 9);
            totalinitfeebalapara.Value = con_contractsubqp.TotalInitFeeBala;
            paras.Add(totalinitfeebalapara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
