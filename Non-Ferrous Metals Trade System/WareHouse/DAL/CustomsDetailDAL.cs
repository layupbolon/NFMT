/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CustomsDetailDAL.cs
// 文件功能描述：报关明细dbo.St_CustomsDetail数据交互类。
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
    /// 报关明细dbo.St_CustomsDetail数据交互类。
    /// </summary>
    public class CustomsDetailDAL : DetailOperate, ICustomsDetailDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomsDetailDAL()
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
            CustomsDetail st_customsdetail = (CustomsDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@DetailId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter customsidpara = new SqlParameter("@CustomsId", SqlDbType.Int, 4);
            customsidpara.Value = st_customsdetail.CustomsId;
            paras.Add(customsidpara);

            SqlParameter customsapplyidpara = new SqlParameter("@CustomsApplyId", SqlDbType.Int, 4);
            customsapplyidpara.Value = st_customsdetail.CustomsApplyId;
            paras.Add(customsapplyidpara);

            SqlParameter customsapplydetailidpara = new SqlParameter("@CustomsApplyDetailId", SqlDbType.Int, 4);
            customsapplydetailidpara.Value = st_customsdetail.CustomsApplyDetailId;
            paras.Add(customsapplydetailidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_customsdetail.StockId;
            paras.Add(stockidpara);

            SqlParameter grossweightpara = new SqlParameter("@GrossWeight", SqlDbType.Decimal, 9);
            grossweightpara.Value = st_customsdetail.GrossWeight;
            paras.Add(grossweightpara);

            SqlParameter netweightpara = new SqlParameter("@NetWeight", SqlDbType.Decimal, 9);
            netweightpara.Value = st_customsdetail.NetWeight;
            paras.Add(netweightpara);

            SqlParameter customspricepara = new SqlParameter("@CustomsPrice", SqlDbType.Decimal, 9);
            customspricepara.Value = st_customsdetail.CustomsPrice;
            paras.Add(customspricepara);

            SqlParameter deliverplaceidpara = new SqlParameter("@DeliverPlaceId", SqlDbType.Int, 4);
            deliverplaceidpara.Value = st_customsdetail.DeliverPlaceId;
            paras.Add(deliverplaceidpara);

            if (!string.IsNullOrEmpty(st_customsdetail.CardNo))
            {
                SqlParameter cardnopara = new SqlParameter("@CardNo", SqlDbType.VarChar, 50);
                cardnopara.Value = st_customsdetail.CardNo;
                paras.Add(cardnopara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = st_customsdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_customsdetail.StockLogId;
            paras.Add(stocklogidpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            CustomsDetail customsdetail = new CustomsDetail();

            customsdetail.DetailId = Convert.ToInt32(dr["DetailId"]);

            if (dr["CustomsId"] != DBNull.Value)
            {
                customsdetail.CustomsId = Convert.ToInt32(dr["CustomsId"]);
            }

            if (dr["CustomsApplyId"] != DBNull.Value)
            {
                customsdetail.CustomsApplyId = Convert.ToInt32(dr["CustomsApplyId"]);
            }

            if (dr["CustomsApplyDetailId"] != DBNull.Value)
            {
                customsdetail.CustomsApplyDetailId = Convert.ToInt32(dr["CustomsApplyDetailId"]);
            }

            if (dr["StockId"] != DBNull.Value)
            {
                customsdetail.StockId = Convert.ToInt32(dr["StockId"]);
            }

            if (dr["GrossWeight"] != DBNull.Value)
            {
                customsdetail.GrossWeight = Convert.ToDecimal(dr["GrossWeight"]);
            }

            if (dr["NetWeight"] != DBNull.Value)
            {
                customsdetail.NetWeight = Convert.ToDecimal(dr["NetWeight"]);
            }

            if (dr["CustomsPrice"] != DBNull.Value)
            {
                customsdetail.CustomsPrice = Convert.ToDecimal(dr["CustomsPrice"]);
            }

            if (dr["DeliverPlaceId"] != DBNull.Value)
            {
                customsdetail.DeliverPlaceId = Convert.ToInt32(dr["DeliverPlaceId"]);
            }

            if (dr["CardNo"] != DBNull.Value)
            {
                customsdetail.CardNo = Convert.ToString(dr["CardNo"]);
            }

            if (dr["DetailStatus"] != DBNull.Value)
            {
                customsdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr["DetailStatus"]);
            }

            if (dr["StockLogId"] != DBNull.Value)
            {
                customsdetail.StockLogId = Convert.ToInt32(dr["StockLogId"]);
            }


            return customsdetail;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            CustomsDetail customsdetail = new CustomsDetail();

            int indexDetailId = dr.GetOrdinal("DetailId");
            customsdetail.DetailId = Convert.ToInt32(dr[indexDetailId]);

            int indexCustomsId = dr.GetOrdinal("CustomsId");
            if (dr["CustomsId"] != DBNull.Value)
            {
                customsdetail.CustomsId = Convert.ToInt32(dr[indexCustomsId]);
            }

            int indexCustomsApplyId = dr.GetOrdinal("CustomsApplyId");
            if (dr["CustomsApplyId"] != DBNull.Value)
            {
                customsdetail.CustomsApplyId = Convert.ToInt32(dr[indexCustomsApplyId]);
            }

            int indexCustomsApplyDetailId = dr.GetOrdinal("CustomsApplyDetailId");
            if (dr["CustomsApplyDetailId"] != DBNull.Value)
            {
                customsdetail.CustomsApplyDetailId = Convert.ToInt32(dr[indexCustomsApplyDetailId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                customsdetail.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexGrossWeight = dr.GetOrdinal("GrossWeight");
            if (dr["GrossWeight"] != DBNull.Value)
            {
                customsdetail.GrossWeight = Convert.ToDecimal(dr[indexGrossWeight]);
            }

            int indexNetWeight = dr.GetOrdinal("NetWeight");
            if (dr["NetWeight"] != DBNull.Value)
            {
                customsdetail.NetWeight = Convert.ToDecimal(dr[indexNetWeight]);
            }

            int indexCustomsPrice = dr.GetOrdinal("CustomsPrice");
            if (dr["CustomsPrice"] != DBNull.Value)
            {
                customsdetail.CustomsPrice = Convert.ToDecimal(dr[indexCustomsPrice]);
            }

            int indexDeliverPlaceId = dr.GetOrdinal("DeliverPlaceId");
            if (dr["DeliverPlaceId"] != DBNull.Value)
            {
                customsdetail.DeliverPlaceId = Convert.ToInt32(dr[indexDeliverPlaceId]);
            }

            int indexCardNo = dr.GetOrdinal("CardNo");
            if (dr["CardNo"] != DBNull.Value)
            {
                customsdetail.CardNo = Convert.ToString(dr[indexCardNo]);
            }

            int indexDetailStatus = dr.GetOrdinal("DetailStatus");
            if (dr["DetailStatus"] != DBNull.Value)
            {
                customsdetail.DetailStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDetailStatus]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                customsdetail.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }


            return customsdetail;
        }

        public override string TableName
        {
            get
            {
                return "St_CustomsDetail";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            CustomsDetail st_customsdetail = (CustomsDetail)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter detailidpara = new SqlParameter("@DetailId", SqlDbType.Int, 4);
            detailidpara.Value = st_customsdetail.DetailId;
            paras.Add(detailidpara);

            SqlParameter customsidpara = new SqlParameter("@CustomsId", SqlDbType.Int, 4);
            customsidpara.Value = st_customsdetail.CustomsId;
            paras.Add(customsidpara);

            SqlParameter customsapplyidpara = new SqlParameter("@CustomsApplyId", SqlDbType.Int, 4);
            customsapplyidpara.Value = st_customsdetail.CustomsApplyId;
            paras.Add(customsapplyidpara);

            SqlParameter customsapplydetailidpara = new SqlParameter("@CustomsApplyDetailId", SqlDbType.Int, 4);
            customsapplydetailidpara.Value = st_customsdetail.CustomsApplyDetailId;
            paras.Add(customsapplydetailidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_customsdetail.StockId;
            paras.Add(stockidpara);

            SqlParameter grossweightpara = new SqlParameter("@GrossWeight", SqlDbType.Decimal, 9);
            grossweightpara.Value = st_customsdetail.GrossWeight;
            paras.Add(grossweightpara);

            SqlParameter netweightpara = new SqlParameter("@NetWeight", SqlDbType.Decimal, 9);
            netweightpara.Value = st_customsdetail.NetWeight;
            paras.Add(netweightpara);

            SqlParameter customspricepara = new SqlParameter("@CustomsPrice", SqlDbType.Decimal, 9);
            customspricepara.Value = st_customsdetail.CustomsPrice;
            paras.Add(customspricepara);

            SqlParameter deliverplaceidpara = new SqlParameter("@DeliverPlaceId", SqlDbType.Int, 4);
            deliverplaceidpara.Value = st_customsdetail.DeliverPlaceId;
            paras.Add(deliverplaceidpara);

            if (!string.IsNullOrEmpty(st_customsdetail.CardNo))
            {
                SqlParameter cardnopara = new SqlParameter("@CardNo", SqlDbType.VarChar, 50);
                cardnopara.Value = st_customsdetail.CardNo;
                paras.Add(cardnopara);
            }

            SqlParameter detailstatuspara = new SqlParameter("@DetailStatus", SqlDbType.Int, 4);
            detailstatuspara.Value = st_customsdetail.DetailStatus;
            paras.Add(detailstatuspara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_customsdetail.StockLogId;
            paras.Add(stocklogidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        private ResultModel GetStockId(UserModel user, string sql)
        {
            ResultModel result = new ResultModel();

            try
            {
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

                    result.ReturnValue = str;
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                }
                else
                {
                    result.ResultStatus = 0;
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

        public ResultModel GetStockIdForUpGrid(UserModel user, int customId)
        {
            string sql = string.Format("select StockId from dbo.St_CustomsDetail where CustomsId = {0} and DetailStatus = {1}", customId, (int)Common.StatusEnum.已生效);

            return GetStockId(user, sql);
        }

        public ResultModel GetStockIdForDownGrid(UserModel user, int customApplyId)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" select  cad.StockId ");
            sb.Append(" from dbo.St_CustomsApplyDetail cad ");
            sb.AppendFormat(" where cad.CustomsApplyId = {0} and cad.DetailStatus = {1} ", customApplyId, (int)Common.StatusEnum.已生效);
            sb.AppendFormat(" and cad.DetailId not in (select CustomsApplyDetailId from dbo.St_CustomsDetail cd where cd.CustomsApplyId = {0} and cd.DetailStatus >= {1})", customApplyId, (int)Common.StatusEnum.已生效);

            return GetStockId(user, sb.ToString());
        }

        public ResultModel Load(UserModel user, int customId, Common.StatusEnum status = Common.StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.St_CustomsDetail where CustomsId ={0} and DetailStatus>={1}", customId, (int)status);
            return Load<Model.CustomsDetail>(user, CommandType.Text, cmdText);
        }

        #endregion
    }
}
