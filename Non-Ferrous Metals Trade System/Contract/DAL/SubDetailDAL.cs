/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SubDetailDAL.cs
// 文件功能描述：子合约明细dbo.Con_SubDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月28日
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
    /// 子合约明细dbo.Con_SubDetail数据交互类。
    /// </summary>
    public class SubDetailDAL : ApplyOperate, ISubDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SubDetailDAL()
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
            SubDetail con_subdetail = (SubDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@SubDetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = con_subdetail.SubId;
            paras.Add(subidpara);

            SqlParameter discountbasepara = new SqlParameter("@DiscountBase", SqlDbType.Int, 4);
            discountbasepara.Value = con_subdetail.DiscountBase;
            paras.Add(discountbasepara);

            SqlParameter discounttypepara = new SqlParameter("@DiscountType", SqlDbType.Int, 4);
            discounttypepara.Value = con_subdetail.DiscountType;
            paras.Add(discounttypepara);

            SqlParameter discountratepara = new SqlParameter("@DiscountRate", SqlDbType.Decimal, 9);
            discountratepara.Value = con_subdetail.DiscountRate;
            paras.Add(discountratepara);

            SqlParameter delaytypepara = new SqlParameter("@DelayType", SqlDbType.Int, 4);
            delaytypepara.Value = con_subdetail.DelayType;
            paras.Add(delaytypepara);

            SqlParameter delayratepara = new SqlParameter("@DelayRate", SqlDbType.Decimal, 9);
            delayratepara.Value = con_subdetail.DelayRate;
            paras.Add(delayratepara);

            SqlParameter moreorlesspara = new SqlParameter("@MoreOrLess", SqlDbType.Decimal, 9);
            moreorlesspara.Value = con_subdetail.MoreOrLess;
            paras.Add(moreorlesspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            SubDetail subdetail = new SubDetail();

            int indexSubDetailId = dr.GetOrdinal("SubDetailId");
            subdetail.SubDetailId = Convert.ToInt32(dr[indexSubDetailId]);

            int indexSubId = dr.GetOrdinal("SubId");
            if (dr["SubId"] != DBNull.Value)
            {
                subdetail.SubId = Convert.ToInt32(dr[indexSubId]);
            }

            int indexDiscountBase = dr.GetOrdinal("DiscountBase");
            if (dr["DiscountBase"] != DBNull.Value)
            {
                subdetail.DiscountBase = Convert.ToInt32(dr[indexDiscountBase]);
            }

            int indexDiscountType = dr.GetOrdinal("DiscountType");
            if (dr["DiscountType"] != DBNull.Value)
            {
                subdetail.DiscountType = Convert.ToInt32(dr[indexDiscountType]);
            }

            int indexDiscountRate = dr.GetOrdinal("DiscountRate");
            if (dr["DiscountRate"] != DBNull.Value)
            {
                subdetail.DiscountRate = Convert.ToDecimal(dr[indexDiscountRate]);
            }

            int indexDelayType = dr.GetOrdinal("DelayType");
            if (dr["DelayType"] != DBNull.Value)
            {
                subdetail.DelayType = Convert.ToInt32(dr[indexDelayType]);
            }

            int indexDelayRate = dr.GetOrdinal("DelayRate");
            if (dr["DelayRate"] != DBNull.Value)
            {
                subdetail.DelayRate = Convert.ToDecimal(dr[indexDelayRate]);
            }

            int indexMoreOrLess = dr.GetOrdinal("MoreOrLess");
            if (dr["MoreOrLess"] != DBNull.Value)
            {
                subdetail.MoreOrLess = Convert.ToDecimal(dr[indexMoreOrLess]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                subdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                subdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                subdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                subdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return subdetail;
        }

        public override string TableName
        {
            get
            {
                return "Con_SubDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SubDetail con_subdetail = (SubDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter subdetailidpara = new SqlParameter("@SubDetailId", SqlDbType.Int, 4);
            subdetailidpara.Value = con_subdetail.SubDetailId;
            paras.Add(subdetailidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = con_subdetail.SubId;
            paras.Add(subidpara);

            SqlParameter discountbasepara = new SqlParameter("@DiscountBase", SqlDbType.Int, 4);
            discountbasepara.Value = con_subdetail.DiscountBase;
            paras.Add(discountbasepara);

            SqlParameter discounttypepara = new SqlParameter("@DiscountType", SqlDbType.Int, 4);
            discounttypepara.Value = con_subdetail.DiscountType;
            paras.Add(discounttypepara);

            SqlParameter discountratepara = new SqlParameter("@DiscountRate", SqlDbType.Decimal, 9);
            discountratepara.Value = con_subdetail.DiscountRate;
            paras.Add(discountratepara);

            SqlParameter delaytypepara = new SqlParameter("@DelayType", SqlDbType.Int, 4);
            delaytypepara.Value = con_subdetail.DelayType;
            paras.Add(delaytypepara);

            SqlParameter delayratepara = new SqlParameter("@DelayRate", SqlDbType.Decimal, 9);
            delayratepara.Value = con_subdetail.DelayRate;
            paras.Add(delayratepara);

            SqlParameter moreorlesspara = new SqlParameter("@MoreOrLess", SqlDbType.Decimal, 9);
            moreorlesspara.Value = con_subdetail.MoreOrLess;
            paras.Add(moreorlesspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetDetailBySubId(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            if (subId < 1)
            {
                result.Message = "合约序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@subId", SqlDbType.Int, 4);
            para.Value = subId;
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                string cmdText = "select * from dbo.Con_SubDetail where SubId =@subId";

                dr = SqlHelper.ExecuteReader(this.ConnectString, CommandType.Text, cmdText, paras.ToArray());

                SubDetail subdetail = new SubDetail();

                if (dr.Read())
                {
                    int indexSubDetailId = dr.GetOrdinal("SubDetailId");
                    subdetail.SubDetailId = Convert.ToInt32(dr[indexSubDetailId]);

                    int indexSubId = dr.GetOrdinal("SubId");
                    if (dr["SubId"] != DBNull.Value)
                    {
                        subdetail.SubId = Convert.ToInt32(dr[indexSubId]);
                    }

                    int indexDiscountBase = dr.GetOrdinal("DiscountBase");
                    if (dr["DiscountBase"] != DBNull.Value)
                    {
                        subdetail.DiscountBase = Convert.ToInt32(dr[indexDiscountBase]);
                    }

                    int indexDiscountType = dr.GetOrdinal("DiscountType");
                    if (dr["DiscountType"] != DBNull.Value)
                    {
                        subdetail.DiscountType = Convert.ToInt32(dr[indexDiscountType]);
                    }

                    int indexDiscountRate = dr.GetOrdinal("DiscountRate");
                    if (dr["DiscountRate"] != DBNull.Value)
                    {
                        subdetail.DiscountRate = Convert.ToDecimal(dr[indexDiscountRate]);
                    }

                    int indexDelayType = dr.GetOrdinal("DelayType");
                    if (dr["DelayType"] != DBNull.Value)
                    {
                        subdetail.DelayType = Convert.ToInt32(dr[indexDelayType]);
                    }

                    int indexDelayRate = dr.GetOrdinal("DelayRate");
                    if (dr["DelayRate"] != DBNull.Value)
                    {
                        subdetail.DelayRate = Convert.ToDecimal(dr[indexDelayRate]);
                    }

                    int indexMoreOrLess = dr.GetOrdinal("MoreOrLess");
                    if (dr["MoreOrLess"] != DBNull.Value)
                    {
                        subdetail.MoreOrLess = Convert.ToDecimal(dr[indexMoreOrLess]);
                    }

                    int indexCreatorId = dr.GetOrdinal("CreatorId");
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        subdetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
                    }

                    int indexCreateTime = dr.GetOrdinal("CreateTime");
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        subdetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
                    }

                    int indexLastModifyId = dr.GetOrdinal("LastModifyId");
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        subdetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
                    }

                    int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        subdetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
                    }

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = subdetail;
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
