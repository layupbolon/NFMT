/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockExclusiveDAL.cs
// 文件功能描述：库存申请库存排他表dbo.St_StockExclusive数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年12月15日
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
    /// 库存申请库存排他表dbo.St_StockExclusive数据交互类。
    /// </summary>
    public class StockExclusiveDAL : ApplyOperate, IStockExclusiveDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockExclusiveDAL()
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
            StockExclusive st_stockexclusive = (StockExclusive)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ExclusiveId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = st_stockexclusive.ApplyId;
            paras.Add(applyidpara);

            SqlParameter stockapplyidpara = new SqlParameter("@StockApplyId", SqlDbType.Int, 4);
            stockapplyidpara.Value = st_stockexclusive.StockApplyId;
            paras.Add(stockapplyidpara);

            SqlParameter detailapplyidpara = new SqlParameter("@DetailApplyId", SqlDbType.Int, 4);
            detailapplyidpara.Value = st_stockexclusive.DetailApplyId;
            paras.Add(detailapplyidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stockexclusive.StockId;
            paras.Add(stockidpara);

            SqlParameter exclusiveamountpara = new SqlParameter("@ExclusiveAmount", SqlDbType.Decimal, 9);
            exclusiveamountpara.Value = st_stockexclusive.ExclusiveAmount;
            paras.Add(exclusiveamountpara);

            SqlParameter exclusivestatuspara = new SqlParameter("@ExclusiveStatus", SqlDbType.Int, 4);
            exclusivestatuspara.Value = st_stockexclusive.ExclusiveStatus;
            paras.Add(exclusivestatuspara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockExclusive stockexclusive = new StockExclusive();

            int indexExclusiveId = dr.GetOrdinal("ExclusiveId");
            stockexclusive.ExclusiveId = Convert.ToInt32(dr[indexExclusiveId]);

            int indexApplyId = dr.GetOrdinal("ApplyId");
            if (dr["ApplyId"] != DBNull.Value)
            {
                stockexclusive.ApplyId = Convert.ToInt32(dr[indexApplyId]);
            }

            int indexStockApplyId = dr.GetOrdinal("StockApplyId");
            if (dr["StockApplyId"] != DBNull.Value)
            {
                stockexclusive.StockApplyId = Convert.ToInt32(dr[indexStockApplyId]);
            }

            int indexDetailApplyId = dr.GetOrdinal("DetailApplyId");
            if (dr["DetailApplyId"] != DBNull.Value)
            {
                stockexclusive.DetailApplyId = Convert.ToInt32(dr[indexDetailApplyId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                stockexclusive.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexExclusiveAmount = dr.GetOrdinal("ExclusiveAmount");
            if (dr["ExclusiveAmount"] != DBNull.Value)
            {
                stockexclusive.ExclusiveAmount = Convert.ToDecimal(dr[indexExclusiveAmount]);
            }

            int indexExclusiveStatus = dr.GetOrdinal("ExclusiveStatus");
            if (dr["ExclusiveStatus"] != DBNull.Value)
            {
                stockexclusive.ExclusiveStatus = (StatusEnum)Convert.ToInt32(dr[indexExclusiveStatus]);
            }

            return stockexclusive;
        }

        public override string TableName
        {
            get
            {
                return "St_StockExclusive";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockExclusive st_stockexclusive = (StockExclusive)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter exclusiveidpara = new SqlParameter("@ExclusiveId", SqlDbType.Int, 4);
            exclusiveidpara.Value = st_stockexclusive.ExclusiveId;
            paras.Add(exclusiveidpara);

            SqlParameter applyidpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
            applyidpara.Value = st_stockexclusive.ApplyId;
            paras.Add(applyidpara);

            SqlParameter stockapplyidpara = new SqlParameter("@StockApplyId", SqlDbType.Int, 4);
            stockapplyidpara.Value = st_stockexclusive.StockApplyId;
            paras.Add(stockapplyidpara);

            SqlParameter detailapplyidpara = new SqlParameter("@DetailApplyId", SqlDbType.Int, 4);
            detailapplyidpara.Value = st_stockexclusive.DetailApplyId;
            paras.Add(detailapplyidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = st_stockexclusive.StockId;
            paras.Add(stockidpara);

            SqlParameter exclusiveamountpara = new SqlParameter("@ExclusiveAmount", SqlDbType.Decimal, 9);
            exclusiveamountpara.Value = st_stockexclusive.ExclusiveAmount;
            paras.Add(exclusiveamountpara);

            SqlParameter exclusivestatuspara = new SqlParameter("@ExclusiveStatus", SqlDbType.Int, 4);
            exclusivestatuspara.Value = st_stockexclusive.ExclusiveStatus;
            paras.Add(exclusivestatuspara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel InsertOrUpdateStatus(UserModel user, StockExclusive stockExclusive)
        {
            ResultModel result = new ResultModel();

            try
            {
                List<SqlParameter> paras = new List<SqlParameter>();
                SqlParameter detailidpara = new SqlParameter();
                detailidpara.Direction = ParameterDirection.Output;
                detailidpara.SqlDbType = SqlDbType.Int;
                detailidpara.ParameterName = "@ExclusiveId";
                detailidpara.Size = 4;
                paras.Add(detailidpara);

                SqlParameter applyIdpara = new SqlParameter("@ApplyId", SqlDbType.Int, 4);
                applyIdpara.Value = stockExclusive.ApplyId;
                paras.Add(applyIdpara);

                SqlParameter stockApplyIdpara = new SqlParameter("@StockApplyId", SqlDbType.Int, 4);
                stockApplyIdpara.Value = stockExclusive.StockApplyId;
                paras.Add(stockApplyIdpara);

                SqlParameter detailApplyIdpara = new SqlParameter("@DetailApplyId", SqlDbType.Int, 4);
                detailApplyIdpara.Value = stockExclusive.DetailApplyId;
                paras.Add(detailApplyIdpara);

                SqlParameter stockIdpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
                stockIdpara.Value = stockExclusive.StockId;
                paras.Add(stockIdpara);

                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, "InsertOrUpdate_St_StockExclusive", paras.ToArray());

                if (i > 0)
                {
                    result.Message = "操作成功";
                    result.ResultStatus = 0;
                    result.AffectCount = i;
                    result.ReturnValue = detailidpara.Value;
                }
                else
                {
                    result.Message = "操作失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception e)
            {
                result.Message = string.Format("操作失败,{0}", e.Message);
                result.ResultStatus = -1;
            }

            return result;
        }

        public ResultModel Invalid(UserModel user, int applyId, int stockApplyId, string sids)
        {
            ResultModel result = new ResultModel();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            try
            {
                if (string.IsNullOrEmpty(sids))
                    sb.AppendFormat("update dbo.St_StockExclusive set ExclusiveStatus = {0} where ApplyId = {1} and StockApplyId = {2}", (int)NFMT.Common.StatusEnum.已作废, applyId, stockApplyId);
                else
                    sb.AppendFormat("update dbo.St_StockExclusive set ExclusiveStatus = {0} where ApplyId = {1} and StockApplyId = {2} and StockId not in ({3})", (int)NFMT.Common.StatusEnum.已作废, applyId, stockApplyId, sids);

                int i = NFMT.DBUtility.SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.Text, sb.ToString(), null);

                if (i >= 0)
                {
                    result.Message = "操作成功";
                    result.ResultStatus = 0;
                    result.AffectCount = i;
                }
                else
                {
                    result.Message = "操作失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel Get(UserModel user, int applyId, int stockApplyId, int detailId)
        {
            ResultModel result = new ResultModel();

            if (applyId < 1)
            {
                result.Message = "申请不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@applyId", SqlDbType.Int, 4);
            para.Value = applyId;
            paras.Add(para);

            para = new SqlParameter("@stockApplyId", SqlDbType.Int, 4);
            para.Value = stockApplyId;
            paras.Add(para);

            para = new SqlParameter("@detailId", SqlDbType.Int, 4);
            para.Value = detailId;
            paras.Add(para);

            para = new SqlParameter("@status", SqlDbType.Int, 4);
            para.Value = (int)StatusEnum.已生效;
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                string cmdText = "select * from dbo.St_StockExclusive where ApplyId=@applyId and StockApplyId=@stockApplyId and DetailApplyId=@detailId and ExclusiveStatus=@status";

                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, paras.ToArray());

                StockExclusive stockexclusive = new StockExclusive();

                if (dr.Read())
                {
                    int indexExclusiveId = dr.GetOrdinal("ExclusiveId");
                    stockexclusive.ExclusiveId = Convert.ToInt32(dr[indexExclusiveId]);

                    int indexApplyId = dr.GetOrdinal("ApplyId");
                    if (dr["ApplyId"] != DBNull.Value)
                    {
                        stockexclusive.ApplyId = Convert.ToInt32(dr[indexApplyId]);
                    }

                    int indexStockApplyId = dr.GetOrdinal("StockApplyId");
                    if (dr["StockApplyId"] != DBNull.Value)
                    {
                        stockexclusive.StockApplyId = Convert.ToInt32(dr[indexStockApplyId]);
                    }

                    int indexDetailApplyId = dr.GetOrdinal("DetailApplyId");
                    if (dr["DetailApplyId"] != DBNull.Value)
                    {
                        stockexclusive.DetailApplyId = Convert.ToInt32(dr[indexDetailApplyId]);
                    }

                    int indexStockId = dr.GetOrdinal("StockId");
                    if (dr["StockId"] != DBNull.Value)
                    {
                        stockexclusive.StockId = Convert.ToInt32(dr[indexStockId]);
                    }

                    int indexExclusiveStatus = dr.GetOrdinal("ExclusiveStatus");
                    if (dr["ExclusiveStatus"] != DBNull.Value)
                    {
                        stockexclusive.ExclusiveStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr[indexExclusiveStatus].ToString());
                    }

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = stockexclusive;
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

        public ResultModel CheckStockIsInExclusive(UserModel user, int stockId)
        {
            ResultModel result = new ResultModel();

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@stockId", SqlDbType.Int, 4);
            para.Value = stockId;
            paras.Add(para);

            para = new SqlParameter("@status", SqlDbType.Int, 4);
            para.Value = (int)StatusEnum.已生效;
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                string cmdText = "select count(*) from dbo.St_StockExclusive where StockId=@stockId and ExclusiveStatus=@status";

                object obj = SqlHelper.ExecuteScalar(ConnectString, CommandType.Text, cmdText, paras.ToArray());
                int rows = 0;
                if (obj != null && int.TryParse(obj.ToString(), out rows))
                {
                    result.AffectCount = rows;
                    result.Message = "查询成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = rows;
                }
                else
                {
                    result.AffectCount = -1;
                    result.Message = "查询失败";
                    result.ResultStatus = -1;
                    result.ReturnValue = -1;
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

        public ResultModel Load(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select * from dbo.St_StockExclusive where ApplyId={0} and ExclusiveStatus={1}", applyId, (int)Common.StatusEnum.已生效);
                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, cmdText, null, CommandType.Text);

                List<StockExclusive> stockExclusives = new List<StockExclusive>();

                foreach (DataRow dr in dt.Rows)
                {
                    StockExclusive stockexclusive = new StockExclusive();
                    stockexclusive.ExclusiveId = Convert.ToInt32(dr["ExclusiveId"]);

                    if (dr["ApplyId"] != DBNull.Value)
                    {
                        stockexclusive.ApplyId = Convert.ToInt32(dr["ApplyId"]);
                    }
                    if (dr["StockApplyId"] != DBNull.Value)
                    {
                        stockexclusive.StockApplyId = Convert.ToInt32(dr["StockApplyId"]);
                    }
                    if (dr["DetailApplyId"] != DBNull.Value)
                    {
                        stockexclusive.DetailApplyId = Convert.ToInt32(dr["DetailApplyId"]);
                    }
                    if (dr["StockId"] != DBNull.Value)
                    {
                        stockexclusive.StockId = Convert.ToInt32(dr["StockId"]);
                    }
                    if (dr["ExclusiveStatus"] != DBNull.Value)
                    {
                        stockexclusive.ExclusiveStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr["ExclusiveStatus"].ToString());
                    }
                    stockExclusives.Add(stockexclusive);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = stockExclusives;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel LoadByStockId(UserModel user, int stockId,StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from NFMT.dbo.St_StockExclusive where ExclusiveStatus={0} and StockId={1}",(int)status,stockId);

            ResultModel result = Load<Model.StockExclusive>(user, CommandType.Text, cmdText);

            return result;
        }

        #endregion
    }
}
