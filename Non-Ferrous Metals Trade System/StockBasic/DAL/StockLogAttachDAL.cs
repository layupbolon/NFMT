using NFMT.Common;
using NFMT.StockBasic.IDAL;
using NFMT.StockBasic.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.StockBasic.DAL
{
    /// <summary>
    /// 流水附件dbo.StockLogAttach数据交互类。
    /// </summary>
    public class StockLogAttachDAL : DataOperate, IStockLogAttachDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockLogAttachDAL()
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
            StockLogAttach st_stocklogattach = (StockLogAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@StockLogAttachId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_stocklogattach.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_stocklogattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockLogAttach stocklogattach = new StockLogAttach();

            int indexStockLogAttachId = dr.GetOrdinal("StockLogAttachId");
            stocklogattach.StockLogAttachId = Convert.ToInt32(dr[indexStockLogAttachId]);

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                stocklogattach.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexAttachId = dr.GetOrdinal("AttachId");
            if (dr["AttachId"] != DBNull.Value)
            {
                stocklogattach.AttachId = Convert.ToInt32(dr[indexAttachId]);
            }


            return stocklogattach;
        }

        public override string TableName
        {
            get
            {
                return "St_StockLogAttach";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockLogAttach st_stocklogattach = (StockLogAttach)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter stocklogattachidpara = new SqlParameter("@StockLogAttachId", SqlDbType.Int, 4);
            stocklogattachidpara.Value = st_stocklogattach.StockLogAttachId;
            paras.Add(stocklogattachidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = st_stocklogattach.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter attachidpara = new SqlParameter("@AttachId", SqlDbType.Int, 4);
            attachidpara.Value = st_stocklogattach.AttachId;
            paras.Add(attachidpara);


            return paras;
        }

        #endregion

    }
}
