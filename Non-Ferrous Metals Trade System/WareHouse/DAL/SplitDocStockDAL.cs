/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SplitDocStockDAL.cs
// 文件功能描述：拆单库存关联表dbo.St_SplitDocStock_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月28日
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
    /// 拆单库存关联表dbo.St_SplitDocStock_Ref数据交互类。
    /// </summary>
    public class SplitDocStockDAL : DataOperate, ISplitDocStockDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SplitDocStockDAL()
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
            SplitDocStock st_splitdocstock_ref = (SplitDocStock)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter splitdocdetailidpara = new SqlParameter("@SplitDocDetailId", SqlDbType.Int, 4);
            splitdocdetailidpara.Value = st_splitdocstock_ref.SplitDocDetailId;
            paras.Add(splitdocdetailidpara);

            SqlParameter newrefnoidpara = new SqlParameter("@NewRefNoId", SqlDbType.Int, 4);
            newrefnoidpara.Value = st_splitdocstock_ref.NewRefNoId;
            paras.Add(newrefnoidpara);

            SqlParameter newstockidpara = new SqlParameter("@NewStockId", SqlDbType.Int, 4);
            newstockidpara.Value = st_splitdocstock_ref.NewStockId;
            paras.Add(newstockidpara);

            SqlParameter oldrefnoidpara = new SqlParameter("@OldRefNoId", SqlDbType.Int, 4);
            oldrefnoidpara.Value = st_splitdocstock_ref.OldRefNoId;
            paras.Add(oldrefnoidpara);

            SqlParameter oldstockidpara = new SqlParameter("@OldStockId", SqlDbType.Int, 4);
            oldstockidpara.Value = st_splitdocstock_ref.OldStockId;
            paras.Add(oldstockidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = st_splitdocstock_ref.RefStatus;
            paras.Add(refstatuspara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            SplitDocStock splitdocstock = new SplitDocStock();

            splitdocstock.RefId = Convert.ToInt32(dr["RefId"]);

            if (dr["SplitDocDetailId"] != DBNull.Value)
            {
                splitdocstock.SplitDocDetailId = Convert.ToInt32(dr["SplitDocDetailId"]);
            }

            if (dr["NewRefNoId"] != DBNull.Value)
            {
                splitdocstock.NewRefNoId = Convert.ToInt32(dr["NewRefNoId"]);
            }

            if (dr["NewStockId"] != DBNull.Value)
            {
                splitdocstock.NewStockId = Convert.ToInt32(dr["NewStockId"]);
            }

            if (dr["OldRefNoId"] != DBNull.Value)
            {
                splitdocstock.OldRefNoId = Convert.ToInt32(dr["OldRefNoId"]);
            }

            if (dr["OldStockId"] != DBNull.Value)
            {
                splitdocstock.OldStockId = Convert.ToInt32(dr["OldStockId"]);
            }

            if (dr["RefStatus"] != DBNull.Value)
            {
                splitdocstock.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr["RefStatus"]);
            }


            return splitdocstock;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            SplitDocStock splitdocstock = new SplitDocStock();

            int indexRefId = dr.GetOrdinal("RefId");
            splitdocstock.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexSplitDocDetailId = dr.GetOrdinal("SplitDocDetailId");
            if (dr["SplitDocDetailId"] != DBNull.Value)
            {
                splitdocstock.SplitDocDetailId = Convert.ToInt32(dr[indexSplitDocDetailId]);
            }

            int indexNewRefNoId = dr.GetOrdinal("NewRefNoId");
            if (dr["NewRefNoId"] != DBNull.Value)
            {
                splitdocstock.NewRefNoId = Convert.ToInt32(dr[indexNewRefNoId]);
            }

            int indexNewStockId = dr.GetOrdinal("NewStockId");
            if (dr["NewStockId"] != DBNull.Value)
            {
                splitdocstock.NewStockId = Convert.ToInt32(dr[indexNewStockId]);
            }

            int indexOldRefNoId = dr.GetOrdinal("OldRefNoId");
            if (dr["OldRefNoId"] != DBNull.Value)
            {
                splitdocstock.OldRefNoId = Convert.ToInt32(dr[indexOldRefNoId]);
            }

            int indexOldStockId = dr.GetOrdinal("OldStockId");
            if (dr["OldStockId"] != DBNull.Value)
            {
                splitdocstock.OldStockId = Convert.ToInt32(dr[indexOldStockId]);
            }

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                splitdocstock.RefStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexRefStatus]);
            }


            return splitdocstock;
        }

        public override string TableName
        {
            get
            {
                return "St_SplitDocStock_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SplitDocStock st_splitdocstock_ref = (SplitDocStock)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = st_splitdocstock_ref.RefId;
            paras.Add(refidpara);

            SqlParameter splitdocdetailidpara = new SqlParameter("@SplitDocDetailId", SqlDbType.Int, 4);
            splitdocdetailidpara.Value = st_splitdocstock_ref.SplitDocDetailId;
            paras.Add(splitdocdetailidpara);

            SqlParameter newrefnoidpara = new SqlParameter("@NewRefNoId", SqlDbType.Int, 4);
            newrefnoidpara.Value = st_splitdocstock_ref.NewRefNoId;
            paras.Add(newrefnoidpara);

            SqlParameter newstockidpara = new SqlParameter("@NewStockId", SqlDbType.Int, 4);
            newstockidpara.Value = st_splitdocstock_ref.NewStockId;
            paras.Add(newstockidpara);

            SqlParameter oldrefnoidpara = new SqlParameter("@OldRefNoId", SqlDbType.Int, 4);
            oldrefnoidpara.Value = st_splitdocstock_ref.OldRefNoId;
            paras.Add(oldrefnoidpara);

            SqlParameter oldstockidpara = new SqlParameter("@OldStockId", SqlDbType.Int, 4);
            oldstockidpara.Value = st_splitdocstock_ref.OldStockId;
            paras.Add(oldstockidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = st_splitdocstock_ref.RefStatus;
            paras.Add(refstatuspara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel GetBySplitDocDetailId(UserModel user,int splitDocDetailId)
        {
            ResultModel result = new ResultModel();
            SqlDataReader dr = null;

            try
            {
                string sql = string.Format("select * from dbo.St_SplitDocStock_Ref where SplitDocDetailId = {0}", splitDocDetailId);
                dr = NFMT.DBUtility.SqlHelper.ExecuteReader(this.ConnectString, CommandType.Text, sql, null);

                IModel model = null;

                if (dr.Read())
                {
                    model = CreateModel(dr);

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = model;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
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
