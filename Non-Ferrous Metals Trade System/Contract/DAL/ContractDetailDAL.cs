/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractDetailDAL.cs
// 文件功能描述：合约明细dbo.Con_ContractDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月24日
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
    /// 合约明细dbo.Con_ContractDetail数据交互类。
    /// </summary>
    public class ContractDetailDAL : ApplyOperate, IContractDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractDetailDAL()
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
            ContractDetail con_contractdetail = (ContractDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ContractDetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contractdetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter discountbasepara = new SqlParameter("@DiscountBase", SqlDbType.Int, 4);
            discountbasepara.Value = con_contractdetail.DiscountBase;
            paras.Add(discountbasepara);

            SqlParameter discounttypepara = new SqlParameter("@DiscountType", SqlDbType.Int, 4);
            discounttypepara.Value = con_contractdetail.DiscountType;
            paras.Add(discounttypepara);

            SqlParameter discountratepara = new SqlParameter("@DiscountRate", SqlDbType.Decimal, 9);
            discountratepara.Value = con_contractdetail.DiscountRate;
            paras.Add(discountratepara);

            SqlParameter delaytypepara = new SqlParameter("@DelayType", SqlDbType.Int, 4);
            delaytypepara.Value = con_contractdetail.DelayType;
            paras.Add(delaytypepara);

            SqlParameter delayratepara = new SqlParameter("@DelayRate", SqlDbType.Decimal, 9);
            delayratepara.Value = con_contractdetail.DelayRate;
            paras.Add(delayratepara);

            SqlParameter moreorlesspara = new SqlParameter("@MoreOrLess", SqlDbType.Decimal, 9);
            moreorlesspara.Value = con_contractdetail.MoreOrLess;
            paras.Add(moreorlesspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractDetail contractdetail = new ContractDetail();

            int indexContractDetailId = dr.GetOrdinal("ContractDetailId");
            contractdetail.ContractDetailId = Convert.ToInt32(dr[indexContractDetailId]);

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                contractdetail.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexDiscountBase = dr.GetOrdinal("DiscountBase");
            if (dr["DiscountBase"] != DBNull.Value)
            {
                contractdetail.DiscountBase = Convert.ToInt32(dr[indexDiscountBase]);
            }

            int indexDiscountType = dr.GetOrdinal("DiscountType");
            if (dr["DiscountType"] != DBNull.Value)
            {
                contractdetail.DiscountType = Convert.ToInt32(dr[indexDiscountType]);
            }

            int indexDiscountRate = dr.GetOrdinal("DiscountRate");
            if (dr["DiscountRate"] != DBNull.Value)
            {
                contractdetail.DiscountRate = Convert.ToDecimal(dr[indexDiscountRate]);
            }

            int indexDelayType = dr.GetOrdinal("DelayType");
            if (dr["DelayType"] != DBNull.Value)
            {
                contractdetail.DelayType = Convert.ToInt32(dr[indexDelayType]);
            }

            int indexDelayRate = dr.GetOrdinal("DelayRate");
            if (dr["DelayRate"] != DBNull.Value)
            {
                contractdetail.DelayRate = Convert.ToDecimal(dr[indexDelayRate]);
            }

            int indexMoreOrLess = dr.GetOrdinal("MoreOrLess");
            if (dr["MoreOrLess"] != DBNull.Value)
            {
                contractdetail.MoreOrLess = Convert.ToDecimal(dr[indexMoreOrLess]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                contractdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                contractdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                contractdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                contractdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return contractdetail;
        }

        public override string TableName
        {
            get
            {
                return "Con_ContractDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractDetail con_contractdetail = (ContractDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter contractdetailidpara = new SqlParameter("@ContractDetailId", SqlDbType.Int, 4);
            contractdetailidpara.Value = con_contractdetail.ContractDetailId;
            paras.Add(contractdetailidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contractdetail.ContractId;
            paras.Add(contractidpara);

            SqlParameter discountbasepara = new SqlParameter("@DiscountBase", SqlDbType.Int, 4);
            discountbasepara.Value = con_contractdetail.DiscountBase;
            paras.Add(discountbasepara);

            SqlParameter discounttypepara = new SqlParameter("@DiscountType", SqlDbType.Int, 4);
            discounttypepara.Value = con_contractdetail.DiscountType;
            paras.Add(discounttypepara);

            SqlParameter discountratepara = new SqlParameter("@DiscountRate", SqlDbType.Decimal, 9);
            discountratepara.Value = con_contractdetail.DiscountRate;
            paras.Add(discountratepara);

            SqlParameter delaytypepara = new SqlParameter("@DelayType", SqlDbType.Int, 4);
            delaytypepara.Value = con_contractdetail.DelayType;
            paras.Add(delaytypepara);

            SqlParameter delayratepara = new SqlParameter("@DelayRate", SqlDbType.Decimal, 9);
            delayratepara.Value = con_contractdetail.DelayRate;
            paras.Add(delayratepara);

            SqlParameter moreorlesspara = new SqlParameter("@MoreOrLess", SqlDbType.Decimal, 9);
            moreorlesspara.Value = con_contractdetail.MoreOrLess;
            paras.Add(moreorlesspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetDetailByContractId(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();

            if (contractId < 1)
            {
                result.Message = "合约序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@contractId", SqlDbType.Int, 4);
            para.Value = contractId;
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                string cmdText = string.Format("select * from dbo.Con_ContractDetail where ContractId ={0}", contractId);

                dr = SqlHelper.ExecuteReader(this.ConnectString, CommandType.Text, cmdText, paras.ToArray());

                ContractDetail contractdetail = new ContractDetail();

                if (dr.Read())
                {
                    int indexContractDetailId = dr.GetOrdinal("ContractDetailId");
                    contractdetail.ContractDetailId = Convert.ToInt32(dr[indexContractDetailId]);

                    int indexContractId = dr.GetOrdinal("ContractId");
                    if (dr["ContractId"] != DBNull.Value)
                    {
                        contractdetail.ContractId = Convert.ToInt32(dr[indexContractId]);
                    }

                    int indexDiscountBase = dr.GetOrdinal("DiscountBase");
                    if (dr["DiscountBase"] != DBNull.Value)
                    {
                        contractdetail.DiscountBase = Convert.ToInt32(dr[indexDiscountBase]);
                    }

                    int indexDiscountType = dr.GetOrdinal("DiscountType");
                    if (dr["DiscountType"] != DBNull.Value)
                    {
                        contractdetail.DiscountType = Convert.ToInt32(dr[indexDiscountType]);
                    }

                    int indexDiscountRate = dr.GetOrdinal("DiscountRate");
                    if (dr["DiscountRate"] != DBNull.Value)
                    {
                        contractdetail.DiscountRate = Convert.ToDecimal(dr[indexDiscountRate]);
                    }

                    int indexDelayType = dr.GetOrdinal("DelayType");
                    if (dr["DelayType"] != DBNull.Value)
                    {
                        contractdetail.DelayType = Convert.ToInt32(dr[indexDelayType]);
                    }

                    int indexDelayRate = dr.GetOrdinal("DelayRate");
                    if (dr["DelayRate"] != DBNull.Value)
                    {
                        contractdetail.DelayRate = Convert.ToDecimal(dr[indexDelayRate]);
                    }

                    int indexMoreOrLess = dr.GetOrdinal("MoreOrLess");
                    if (dr["MoreOrLess"] != DBNull.Value)
                    {
                        contractdetail.MoreOrLess = Convert.ToDecimal(dr[indexMoreOrLess]);
                    }

                    int indexCreatorId = dr.GetOrdinal("CreatorId");
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        contractdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
                    }

                    int indexCreateTime = dr.GetOrdinal("CreateTime");
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        contractdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
                    }

                    int indexLastModifyId = dr.GetOrdinal("LastModifyId");
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        contractdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
                    }

                    int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        contractdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
                    }

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = contractdetail;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        #endregion

    }
}
