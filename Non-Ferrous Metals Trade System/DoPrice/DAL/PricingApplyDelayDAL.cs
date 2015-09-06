/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PricingApplyDelayDAL.cs
// 文件功能描述：点价申请延期dbo.Pri_PricingApplyDelay数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年12月8日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.DoPrice.Model;
using NFMT.DBUtility;
using NFMT.DoPrice.IDAL;
using NFMT.Common;

namespace NFMT.DoPrice.DAL
{
    /// <summary>
    /// 点价申请延期dbo.Pri_PricingApplyDelay数据交互类。
    /// </summary>
    public class PricingApplyDelayDAL : ExecOperate, IPricingApplyDelayDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PricingApplyDelayDAL()
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
            PricingApplyDelay pri_pricingapplydelay = (PricingApplyDelay)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DelayId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter pricingapplyidpara = new SqlParameter("@PricingApplyId", SqlDbType.Int, 4);
            pricingapplyidpara.Value = pri_pricingapplydelay.PricingApplyId;
            paras.Add(pricingapplyidpara);

            SqlParameter delayamountpara = new SqlParameter("@DelayAmount", SqlDbType.Decimal, 9);
            delayamountpara.Value = pri_pricingapplydelay.DelayAmount;
            paras.Add(delayamountpara);

            SqlParameter delayfeepara = new SqlParameter("@DelayFee", SqlDbType.Decimal, 9);
            delayfeepara.Value = pri_pricingapplydelay.DelayFee;
            paras.Add(delayfeepara);

            SqlParameter delayqppara = new SqlParameter("@DelayQP", SqlDbType.DateTime, 8);
            delayqppara.Value = pri_pricingapplydelay.DelayQP;
            paras.Add(delayqppara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = (int)Common.StatusEnum.已录入;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            PricingApplyDelay pricingapplydelay = new PricingApplyDelay();

            pricingapplydelay.DelayId = Convert.ToInt32(dr["DelayId"]);

            if (dr["PricingApplyId"] != DBNull.Value)
            {
                pricingapplydelay.PricingApplyId = Convert.ToInt32(dr["PricingApplyId"]);
            }

            if (dr["DelayAmount"] != DBNull.Value)
            {
                pricingapplydelay.DelayAmount = Convert.ToDecimal(dr["DelayAmount"]);
            }

            if (dr["DelayFee"] != DBNull.Value)
            {
                pricingapplydelay.DelayFee = Convert.ToDecimal(dr["DelayFee"]);
            }

            if (dr["DelayQP"] != DBNull.Value)
            {
                pricingapplydelay.DelayQP = Convert.ToDateTime(dr["DelayQP"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                pricingapplydelay.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                pricingapplydelay.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                pricingapplydelay.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                pricingapplydelay.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pricingapplydelay.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return pricingapplydelay;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PricingApplyDelay pricingapplydelay = new PricingApplyDelay();

            int indexDelayId = dr.GetOrdinal("DelayId");
            pricingapplydelay.DelayId = Convert.ToInt32(dr[indexDelayId]);

            int indexPricingApplyId = dr.GetOrdinal("PricingApplyId");
            if (dr["PricingApplyId"] != DBNull.Value)
            {
                pricingapplydelay.PricingApplyId = Convert.ToInt32(dr[indexPricingApplyId]);
            }

            int indexDelayAmount = dr.GetOrdinal("DelayAmount");
            if (dr["DelayAmount"] != DBNull.Value)
            {
                pricingapplydelay.DelayAmount = Convert.ToDecimal(dr[indexDelayAmount]);
            }

            int indexDelayFee = dr.GetOrdinal("DelayFee");
            if (dr["DelayFee"] != DBNull.Value)
            {
                pricingapplydelay.DelayFee = Convert.ToDecimal(dr[indexDelayFee]);
            }

            int indexDelayQP = dr.GetOrdinal("DelayQP");
            if (dr["DelayQP"] != DBNull.Value)
            {
                pricingapplydelay.DelayQP = Convert.ToDateTime(dr[indexDelayQP]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                pricingapplydelay.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                pricingapplydelay.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                pricingapplydelay.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                pricingapplydelay.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pricingapplydelay.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return pricingapplydelay;
        }

        public override string TableName
        {
            get
            {
                return "Pri_PricingApplyDelay";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PricingApplyDelay pri_pricingapplydelay = (PricingApplyDelay)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter delayidpara = new SqlParameter("@DelayId", SqlDbType.Int, 4);
            delayidpara.Value = pri_pricingapplydelay.DelayId;
            paras.Add(delayidpara);

            SqlParameter pricingapplyidpara = new SqlParameter("@PricingApplyId", SqlDbType.Int, 4);
            pricingapplyidpara.Value = pri_pricingapplydelay.PricingApplyId;
            paras.Add(pricingapplyidpara);

            SqlParameter delayamountpara = new SqlParameter("@DelayAmount", SqlDbType.Decimal, 9);
            delayamountpara.Value = pri_pricingapplydelay.DelayAmount;
            paras.Add(delayamountpara);

            SqlParameter delayfeepara = new SqlParameter("@DelayFee", SqlDbType.Decimal, 9);
            delayfeepara.Value = pri_pricingapplydelay.DelayFee;
            paras.Add(delayfeepara);

            SqlParameter delayqppara = new SqlParameter("@DelayQP", SqlDbType.DateTime, 8);
            delayqppara.Value = pri_pricingapplydelay.DelayQP;
            paras.Add(delayqppara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = pri_pricingapplydelay.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetCanDelayWeight(UserModel user, int pricingApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select ISNULL(pa.PricingWeight,0)-ISNULL(pad.DelayAmount,0) from dbo.Pri_PricingApply pa left join (select SUM(ISNULL(DelayAmount,0)) as DelayAmount,PricingApplyId from dbo.Pri_PricingApplyDelay where DetailStatus >= {0} group by PricingApplyId) pad on pa.PricingApplyId = pad.PricingApplyId where pa.PricingApplyId = {1}", (int)Common.StatusEnum.已生效, pricingApplyId);
                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, sql, null);
                decimal canDelayWeight = 0;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && decimal.TryParse(obj.ToString(), out canDelayWeight))
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = canDelayWeight;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
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
