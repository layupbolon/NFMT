/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SummaryApplyStockDAL.cs
// 文件功能描述：制单指令库存关联dbo.Con_SummaryApplyStock_Ref数据交互类。
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
    /// 制单指令库存关联dbo.Con_SummaryApplyStock_Ref数据交互类。
    /// </summary>
    public class SummaryApplyStockDAL : DataOperate, ISummaryApplyStockDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SummaryApplyStockDAL()
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
            SummaryApplyStock con_summaryapplystock_ref = (SummaryApplyStock)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter saidpara = new SqlParameter("@SAId", SqlDbType.Int, 4);
            saidpara.Value = con_summaryapplystock_ref.SAId;
            paras.Add(saidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = con_summaryapplystock_ref.StockId;
            paras.Add(stockidpara);

            SqlParameter stocknameidpara = new SqlParameter("@StockNameId", SqlDbType.Int, 4);
            stocknameidpara.Value = con_summaryapplystock_ref.StockNameId;
            paras.Add(stocknameidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = con_summaryapplystock_ref.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            SummaryApplyStock summaryapplystock = new SummaryApplyStock();

            int indexRefId = dr.GetOrdinal("RefId");
            summaryapplystock.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexSAId = dr.GetOrdinal("SAId");
            if (dr["SAId"] != DBNull.Value)
            {
                summaryapplystock.SAId = Convert.ToInt32(dr[indexSAId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                summaryapplystock.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockNameId = dr.GetOrdinal("StockNameId");
            if (dr["StockNameId"] != DBNull.Value)
            {
                summaryapplystock.StockNameId = Convert.ToInt32(dr[indexStockNameId]);
            }

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                summaryapplystock.RefStatus = Convert.ToInt32(dr[indexRefStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                summaryapplystock.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                summaryapplystock.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                summaryapplystock.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                summaryapplystock.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return summaryapplystock;
        }

        public override string TableName
        {
            get
            {
                return "Con_SummaryApplyStock_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            SummaryApplyStock con_summaryapplystock_ref = (SummaryApplyStock)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = con_summaryapplystock_ref.RefId;
            paras.Add(refidpara);

            SqlParameter saidpara = new SqlParameter("@SAId", SqlDbType.Int, 4);
            saidpara.Value = con_summaryapplystock_ref.SAId;
            paras.Add(saidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = con_summaryapplystock_ref.StockId;
            paras.Add(stockidpara);

            SqlParameter stocknameidpara = new SqlParameter("@StockNameId", SqlDbType.Int, 4);
            stocknameidpara.Value = con_summaryapplystock_ref.StockNameId;
            paras.Add(stocknameidpara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = con_summaryapplystock_ref.RefStatus;
            paras.Add(refstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion

    }
}
