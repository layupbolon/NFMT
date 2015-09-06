/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SplitDocDAL.cs
// 文件功能描述：拆单dbo.St_SplitDoc数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月27日
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
    /// 拆单dbo.St_SplitDoc数据交互类。
    /// </summary>
    public class SplitDocDAL : ExecOperate, ISplitDocDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SplitDocDAL()
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
            SplitDoc st_splitdoc = (SplitDoc)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@SplitDocId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter spliterpara = new SqlParameter("@Spliter", SqlDbType.Int, 4);
            spliterpara.Value = st_splitdoc.Spliter;
            paras.Add(spliterpara);

            SqlParameter splitdoctimepara = new SqlParameter("@SplitDocTime", SqlDbType.DateTime, 8);
            splitdoctimepara.Value = st_splitdoc.SplitDocTime;
            paras.Add(splitdoctimepara);

            SqlParameter splitdocstatuspara = new SqlParameter("@SplitDocStatus", SqlDbType.Int, 4);
            splitdocstatuspara.Value = st_splitdoc.SplitDocStatus;
            paras.Add(splitdocstatuspara);

            SqlParameter oldrefnoidpara = new SqlParameter("@OldRefNoId", SqlDbType.Int, 4);
            oldrefnoidpara.Value = st_splitdoc.OldRefNoId;
            paras.Add(oldrefnoidpara);

            if (!string.IsNullOrEmpty(st_splitdoc.OldRefNo))
            {
                SqlParameter oldrefnopara = new SqlParameter("@OldRefNo", SqlDbType.VarChar, 50);
                oldrefnopara.Value = st_splitdoc.OldRefNo;
                paras.Add(oldrefnopara);
            }

            SqlParameter oldstockidpara = new SqlParameter("@OldStockId", SqlDbType.Int, 4);
            oldstockidpara.Value = st_splitdoc.OldStockId;
            paras.Add(oldstockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_splitdoc.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(DataRow dr)
        {
            SplitDoc splitdoc = new SplitDoc();

            splitdoc.SplitDocId = Convert.ToInt32(dr["SplitDocId"]);

            if (dr["Spliter"] != DBNull.Value)
            {
                splitdoc.Spliter = Convert.ToInt32(dr["Spliter"]);
            }

            if (dr["SplitDocTime"] != DBNull.Value)
            {
                splitdoc.SplitDocTime = Convert.ToDateTime(dr["SplitDocTime"]);
            }

            if (dr["SplitDocStatus"] != DBNull.Value)
            {
                splitdoc.SplitDocStatus = (Common.StatusEnum)Convert.ToInt32(dr["SplitDocStatus"]);
            }

            if (dr["OldRefNoId"] != DBNull.Value)
            {
                splitdoc.OldRefNoId = Convert.ToInt32(dr["OldRefNoId"]);
            }

            if (dr["OldRefNo"] != DBNull.Value)
            {
                splitdoc.OldRefNo = Convert.ToString(dr["OldRefNo"]);
            }

            if (dr["OldStockId"] != DBNull.Value)
            {
                splitdoc.OldStockId = Convert.ToInt32(dr["OldStockId"]);
            }

            if (dr["StockLogId"] != DBNull.Value)
            {
                splitdoc.StockLogId = Convert.ToInt32(dr["StockLogId"]);
            }

            if (dr["CreatorId"] != DBNull.Value)
            {
                splitdoc.CreatorId = Convert.ToInt32(dr["CreatorId"]);
            }

            if (dr["CreateTime"] != DBNull.Value)
            {
                splitdoc.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            }

            if (dr["LastModifyId"] != DBNull.Value)
            {
                splitdoc.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
            }

            if (dr["LastModifyTime"] != DBNull.Value)
            {
                splitdoc.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
            }


            return splitdoc;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            SplitDoc splitdoc = new SplitDoc();

            int indexSplitDocId = dr.GetOrdinal("SplitDocId");
            splitdoc.SplitDocId = Convert.ToInt32(dr[indexSplitDocId]);

            int indexSpliter = dr.GetOrdinal("Spliter");
            if (dr["Spliter"] != DBNull.Value)
            {
                splitdoc.Spliter = Convert.ToInt32(dr[indexSpliter]);
            }

            int indexSplitDocTime = dr.GetOrdinal("SplitDocTime");
            if (dr["SplitDocTime"] != DBNull.Value)
            {
                splitdoc.SplitDocTime = Convert.ToDateTime(dr[indexSplitDocTime]);
            }

            int indexSplitDocStatus = dr.GetOrdinal("SplitDocStatus");
            if (dr["SplitDocStatus"] != DBNull.Value)
            {
                splitdoc.SplitDocStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexSplitDocStatus]);
            }

            int indexOldRefNoId = dr.GetOrdinal("OldRefNoId");
            if (dr["OldRefNoId"] != DBNull.Value)
            {
                splitdoc.OldRefNoId = Convert.ToInt32(dr[indexOldRefNoId]);
            }

            int indexOldRefNo = dr.GetOrdinal("OldRefNo");
            if (dr["OldRefNo"] != DBNull.Value)
            {
                splitdoc.OldRefNo = Convert.ToString(dr[indexOldRefNo]);
            }

            int indexOldStockId = dr.GetOrdinal("OldStockId");
            if (dr["OldStockId"] != DBNull.Value)
            {
                splitdoc.OldStockId = Convert.ToInt32(dr[indexOldStockId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                splitdoc.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                splitdoc.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                splitdoc.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                splitdoc.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                splitdoc.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return splitdoc;
        }

        public override string TableName
        {
            get
            {
                return "St_SplitDoc";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SplitDoc st_splitdoc = (SplitDoc)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter splitdocidpara = new SqlParameter("@SplitDocId", SqlDbType.Int, 4);
            splitdocidpara.Value = st_splitdoc.SplitDocId;
            paras.Add(splitdocidpara);

            SqlParameter spliterpara = new SqlParameter("@Spliter", SqlDbType.Int, 4);
            spliterpara.Value = st_splitdoc.Spliter;
            paras.Add(spliterpara);

            SqlParameter splitdoctimepara = new SqlParameter("@SplitDocTime", SqlDbType.DateTime, 8);
            splitdoctimepara.Value = st_splitdoc.SplitDocTime;
            paras.Add(splitdoctimepara);

            SqlParameter splitdocstatuspara = new SqlParameter("@SplitDocStatus", SqlDbType.Int, 4);
            splitdocstatuspara.Value = st_splitdoc.SplitDocStatus;
            paras.Add(splitdocstatuspara);

            SqlParameter oldrefnoidpara = new SqlParameter("@OldRefNoId", SqlDbType.Int, 4);
            oldrefnoidpara.Value = st_splitdoc.OldRefNoId;
            paras.Add(oldrefnoidpara);

            if (!string.IsNullOrEmpty(st_splitdoc.OldRefNo))
            {
                SqlParameter oldrefnopara = new SqlParameter("@OldRefNo", SqlDbType.VarChar, 50);
                oldrefnopara.Value = st_splitdoc.OldRefNo;
                paras.Add(oldrefnopara);
            }

            SqlParameter oldstockidpara = new SqlParameter("@OldStockId", SqlDbType.Int, 4);
            oldstockidpara.Value = st_splitdoc.OldStockId;
            paras.Add(oldstockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_splitdoc.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

        #region 新增方法

        public override int MenuId
        {
            get
            {
                return 93;
            }
        }

        public override ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate)
        {
            return new ResultModel()
            {
                ResultStatus = 0
            };
        }

        #endregion
    }
}
