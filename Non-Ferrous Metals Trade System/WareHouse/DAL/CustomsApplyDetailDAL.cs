/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CustomsApplyDetailDAL.cs
// 文件功能描述：报关申请明细dbo.St_CustomsApplyDetail数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月29日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.WareHouse.Model;
using NFMT.DBUtility;
using NFMT.WareHouse.IDAL;
using NFMT.Common;

namespace NFMT.WareHouse.DAL
{
    /// <summary>
    /// 报关申请明细dbo.St_CustomsApplyDetail数据交互类。
    /// </summary>
    public class CustomsApplyDetailDAL : ApplyOperate, ICustomsApplyDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomsApplyDetailDAL()
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
            CustomsApplyDetail st_customsapplydetail = (CustomsApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter customsapplyidpara = new SqlParameter("@CustomsApplyId", SqlDbType.Int, 4);
            customsapplyidpara.Value = st_customsapplydetail.CustomsApplyId;
            paras.Add(customsapplyidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_customsapplydetail.StockId;
            paras.Add(stockidpara);

            SqlParameter grossweightpara = new SqlParameter("@GrossWeight", SqlDbType.Decimal, 9);
            grossweightpara.Value = st_customsapplydetail.GrossWeight;
            paras.Add(grossweightpara);

            SqlParameter netweightpara = new SqlParameter("@NetWeight", SqlDbType.Decimal, 9);
            netweightpara.Value = st_customsapplydetail.NetWeight;
            paras.Add(netweightpara);

            SqlParameter customspricepara = new SqlParameter("@CustomsPrice", SqlDbType.Decimal, 9);
            customspricepara.Value = st_customsapplydetail.CustomsPrice;
            paras.Add(customspricepara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = st_customsapplydetail.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            CustomsApplyDetail customsapplydetail = new CustomsApplyDetail();

            customsapplydetail.DetailId = Convert.ToInt32(dr["DetailId"]);

            if (dr["CustomsApplyId"] != DBNull.Value)
            {
                customsapplydetail.CustomsApplyId = Convert.ToInt32(dr["CustomsApplyId"]);
            }

            if (dr["StockId"] != DBNull.Value)
            {
                customsapplydetail.StockId = Convert.ToInt32(dr["StockId"]);
            }

            if (dr["GrossWeight"] != DBNull.Value)
            {
                customsapplydetail.GrossWeight = Convert.ToDecimal(dr["GrossWeight"]);
            }

            if (dr["NetWeight"] != DBNull.Value)
            {
                customsapplydetail.NetWeight = Convert.ToDecimal(dr["NetWeight"]);
            }

            if (dr["CustomsPrice"] != DBNull.Value)
            {
                customsapplydetail.CustomsPrice = Convert.ToDecimal(dr["CustomsPrice"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                customsapplydetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }


            return customsapplydetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            CustomsApplyDetail customsapplydetail = new CustomsApplyDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            customsapplydetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexCustomsApplyId = dr.GetOrdinal("CustomsApplyId");
            if (dr["CustomsApplyId"] != DBNull.Value)
            {
                customsapplydetail.CustomsApplyId = Convert.ToInt32(dr[indexCustomsApplyId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                customsapplydetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexGrossWeight = dr.GetOrdinal("GrossWeight");
            if (dr["GrossWeight"] != DBNull.Value)
            {
                customsapplydetail.GrossWeight = Convert.ToDecimal(dr[indexGrossWeight]);
            }

            int indexNetWeight = dr.GetOrdinal("NetWeight");
            if (dr["NetWeight"] != DBNull.Value)
            {
                customsapplydetail.NetWeight = Convert.ToDecimal(dr[indexNetWeight]);
            }

            int indexCustomsPrice = dr.GetOrdinal("CustomsPrice");
            if (dr["CustomsPrice"] != DBNull.Value)
            {
                customsapplydetail.CustomsPrice = Convert.ToDecimal(dr[indexCustomsPrice]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                customsapplydetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }


            return customsapplydetail;
        }

        public override string TableName
        {
            get
            {
                return "St_CustomsApplyDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CustomsApplyDetail st_customsapplydetail = (CustomsApplyDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = st_customsapplydetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter customsapplyidpara = new SqlParameter("@CustomsApplyId", SqlDbType.Int, 4);
            customsapplyidpara.Value = st_customsapplydetail.CustomsApplyId;
            paras.Add(customsapplyidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_customsapplydetail.StockId;
            paras.Add(stockidpara);

            SqlParameter grossweightpara = new SqlParameter("@GrossWeight", SqlDbType.Decimal, 9);
            grossweightpara.Value = st_customsapplydetail.GrossWeight;
            paras.Add(grossweightpara);

            SqlParameter netweightpara = new SqlParameter("@NetWeight", SqlDbType.Decimal, 9);
            netweightpara.Value = st_customsapplydetail.NetWeight;
            paras.Add(netweightpara);

            SqlParameter customspricepara = new SqlParameter("@CustomsPrice", SqlDbType.Decimal, 9);
            customspricepara.Value = st_customsapplydetail.CustomsPrice;
            paras.Add(customspricepara);

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = st_customsapplydetail.DetailStatus;
            paras.Add(detailstatuspara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetStockIdById(UserModel user, int customApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select StockId from dbo.St_CustomsApplyDetail where CustomsApplyId  = {0} and DetailStatus = {1} ", customApplyId, (int)Common.StatusEnum.已生效);

                DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, sql, null, CommandType.Text);
                string str = string.Empty;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        str += dr["StockId"].ToString() + ",";
                    }
                    if (!string.IsNullOrEmpty(str))
                        str = str.Substring(0, str.Length - 1);

                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = str;
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

        public ResultModel Load(UserModel user, int customApplyId, Common.StatusEnum status = Common.StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            SqlDataReader dr = null;

            try
            {
                string cmdText = string.Format("select * from dbo.St_CustomsApplyDetail where CustomsApplyId ={0} and DetailStatus>={1}", customApplyId, (int)status);

               dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, null);

                if(!dr.HasRows)
                {
                    result.ResultStatus = -1;
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                    return result;
                }

                List<Model.CustomsApplyDetail> models = new List<Model.CustomsApplyDetail>();

                int i = 0;
                while (dr.Read())
                {
                    models.Add(CreateModel(dr) as Model.CustomsApplyDetail);
                    i++;
                }

                result.AffectCount = i;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = models;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally{
                if(dr!=null)
                    dr.Dispose();
            }

            return result;
        }

        #endregion
    }
}
