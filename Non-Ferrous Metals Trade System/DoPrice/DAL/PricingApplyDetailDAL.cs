/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PricingApplyDetailDAL.cs
// 文件功能描述：点价申请明细dbo.Pri_PricingApplyDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月13日
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
    /// 点价申请明细dbo.Pri_PricingApplyDetail数据交互类。
    /// </summary>
    public class PricingApplyDetailDAL : ApplyOperate, IPricingApplyDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PricingApplyDetailDAL()
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
            PricingApplyDetail pri_pricingapplydetail = (PricingApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter pricingapplyidpara = new SqlParameter("@PricingApplyId", SqlDbType.Int, 4);
            pricingapplyidpara.Value = pri_pricingapplydetail.PricingApplyId;
            paras.Add(pricingapplyidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = pri_pricingapplydetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = pri_pricingapplydetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter pricingweightpara = new SqlParameter("@PricingWeight", SqlDbType.Decimal, 9);
            pricingweightpara.Value = pri_pricingapplydetail.PricingWeight;
            paras.Add(pricingweightpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = pri_pricingapplydetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            PricingApplyDetail pricingapplydetail = new PricingApplyDetail();

            pricingapplydetail.DetailId = Convert.ToInt32(dr["DetailId"]);

            if (dr["PricingApplyId"] != DBNull.Value)
            {
                pricingapplydetail.PricingApplyId = Convert.ToInt32(dr["PricingApplyId"]);
            }

            if (dr["StockId"] != DBNull.Value)
            {
                pricingapplydetail.StockId = Convert.ToInt32(dr["StockId"]);
            }

            if (dr["StockLogId"] != DBNull.Value)
            {
                pricingapplydetail.StockLogId = Convert.ToInt32(dr["StockLogId"]);
            }

            if (dr["PricingWeight"] != DBNull.Value)
            {
                pricingapplydetail.PricingWeight = Convert.ToDecimal(dr["PricingWeight"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                pricingapplydetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                pricingapplydetail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                pricingapplydetail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                pricingapplydetail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pricingapplydetail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return pricingapplydetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            PricingApplyDetail pricingapplydetail = new PricingApplyDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            pricingapplydetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexPricingApplyId = dr.GetOrdinal("PricingApplyId");
            if (dr["PricingApplyId"] != DBNull.Value)
            {
                pricingapplydetail.PricingApplyId = Convert.ToInt32(dr[indexPricingApplyId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                pricingapplydetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                pricingapplydetail.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexPricingWeight = dr.GetOrdinal("PricingWeight");
            if (dr["PricingWeight"] != DBNull.Value)
            {
                pricingapplydetail.PricingWeight = Convert.ToDecimal(dr[indexPricingWeight]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                pricingapplydetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                pricingapplydetail.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                pricingapplydetail.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                pricingapplydetail.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                pricingapplydetail.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return pricingapplydetail;
        }

        public override string TableName
        {
            get
            {
                return "Pri_PricingApplyDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            PricingApplyDetail pri_pricingapplydetail = (PricingApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = pri_pricingapplydetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter pricingapplyidpara = new SqlParameter("@PricingApplyId", SqlDbType.Int, 4);
            pricingapplyidpara.Value = pri_pricingapplydetail.PricingApplyId;
            paras.Add(pricingapplyidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = pri_pricingapplydetail.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = pri_pricingapplydetail.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter pricingweightpara = new SqlParameter("@PricingWeight", SqlDbType.Decimal, 9);
            pricingweightpara.Value = pri_pricingapplydetail.PricingWeight;
            paras.Add(pricingweightpara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = pri_pricingapplydetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel InvalidAll(UserModel user, int pricingApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("update dbo.Pri_PricingApplyDetail set DetailStatus = {0} where PricingApplyId = {1}", (int)Common.StatusEnum.已作废, pricingApplyId);
                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sql, null);
                if (i >= 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "作废成功";
                    result.AffectCount = i;
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "作废失败";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel Load(UserModel user, int pricingApplyId, Common.StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select * from dbo.Pri_PricingApplyDetail where PricingApplyId={0} and DetailStatus>={1}", pricingApplyId, (int)StatusEnum.已生效);
                DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionStringNFMT, cmdText, null, CommandType.Text);

                List<PricingApplyDetail> pricingApplyDetails = new List<PricingApplyDetail>();

                foreach (DataRow dr in dt.Rows)
                {
                    PricingApplyDetail pricingapplydetail = new PricingApplyDetail();
                    pricingapplydetail.DetailId = Convert.ToInt32(dr["DetailId"]);

                    if (dr["PricingApplyId"] != DBNull.Value)
                    {
                        pricingapplydetail.PricingApplyId = Convert.ToInt32(dr["PricingApplyId"]);
                    }
                    if (dr["StockId"] != DBNull.Value)
                    {
                        pricingapplydetail.StockId = Convert.ToInt32(dr["StockId"]);
                    }
                    if (dr["PricingWeight"] != DBNull.Value)
                    {
                        pricingapplydetail.PricingWeight = Convert.ToDecimal(dr["PricingWeight"]);
                    }
                    if (dr["DetailStatus"] != DBNull.Value)
                    {
                        pricingapplydetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
                    }
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        pricingapplydetail.CreatorId = Convert.ToInt32(dr["CreatorId"]);
                    }
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        pricingapplydetail.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    }
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        pricingapplydetail.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    }
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        pricingapplydetail.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    }
                    pricingApplyDetails.Add(pricingapplydetail);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = pricingApplyDetails;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        #endregion
    }
}
