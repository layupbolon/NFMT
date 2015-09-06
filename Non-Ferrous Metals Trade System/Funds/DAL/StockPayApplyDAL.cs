/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockPayApplyDAL.cs
// 文件功能描述：库存付款申请dbo.Fun_StockPayApply_Ref数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年10月10日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.Funds.Model;
using NFMT.DBUtility;
using NFMT.Funds.IDAL;
using NFMT.Common;

namespace NFMT.Funds.DAL
{
    /// <summary>
    /// 库存付款申请dbo.Fun_StockPayApply_Ref数据交互类。
    /// </summary>
    public class StockPayApplyDAL : ApplyOperate, IStockPayApplyDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockPayApplyDAL()
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
            StockPayApply fun_stockpayapply_ref = (StockPayApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@RefId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter payapplyidpara = new SqlParameter("@PayApplyId", SqlDbType.Int, 4);
            payapplyidpara.Value = fun_stockpayapply_ref.PayApplyId;
            paras.Add(payapplyidpara);

            SqlParameter contractrefidpara = new SqlParameter("@ContractRefId", SqlDbType.Int, 4);
            contractrefidpara.Value = fun_stockpayapply_ref.ContractRefId;
            paras.Add(contractrefidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = fun_stockpayapply_ref.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = fun_stockpayapply_ref.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = fun_stockpayapply_ref.ContractId;
            paras.Add(contractidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = fun_stockpayapply_ref.SubId;
            paras.Add(subidpara);

            SqlParameter applybalapara = new SqlParameter("@ApplyBala", SqlDbType.Decimal, 9);
            applybalapara.Value = fun_stockpayapply_ref.ApplyBala;
            paras.Add(applybalapara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = fun_stockpayapply_ref.RefStatus;
            paras.Add(refstatuspara);

            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            StockPayApply stockpayapply = new StockPayApply();

            int indexRefId = dr.GetOrdinal("RefId");
            stockpayapply.RefId = Convert.ToInt32(dr[indexRefId]);

            int indexPayApplyId = dr.GetOrdinal("PayApplyId");
            if (dr["PayApplyId"] != DBNull.Value)
            {
                stockpayapply.PayApplyId = Convert.ToInt32(dr[indexPayApplyId]);
            }

            int indexContractRefId = dr.GetOrdinal("ContractRefId");
            if (dr["ContractRefId"] != DBNull.Value)
            {
                stockpayapply.ContractRefId = Convert.ToInt32(dr[indexContractRefId]);
            }

            int indexStockId = dr.GetOrdinal("StockId");
            if (dr["StockId"] != DBNull.Value)
            {
                stockpayapply.StockId = Convert.ToInt32(dr[indexStockId]);
            }

            int indexStockLogId = dr.GetOrdinal("StockLogId");
            if (dr["StockLogId"] != DBNull.Value)
            {
                stockpayapply.StockLogId = Convert.ToInt32(dr[indexStockLogId]);
            }

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                stockpayapply.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexSubId = dr.GetOrdinal("SubId");
            if (dr["SubId"] != DBNull.Value)
            {
                stockpayapply.SubId = Convert.ToInt32(dr[indexSubId]);
            }

            int indexApplyBala = dr.GetOrdinal("ApplyBala");
            if (dr["ApplyBala"] != DBNull.Value)
            {
                stockpayapply.ApplyBala = Convert.ToDecimal(dr[indexApplyBala]);
            }

            int indexRefStatus = dr.GetOrdinal("RefStatus");
            if (dr["RefStatus"] != DBNull.Value)
            {
                stockpayapply.RefStatus =(Common.StatusEnum)Convert.ToInt32(dr[indexRefStatus]);
            }

            return stockpayapply;
        }

        public override string TableName
        {
            get
            {
                return "Fun_StockPayApply_Ref";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            StockPayApply fun_stockpayapply_ref = (StockPayApply)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter refidpara = new SqlParameter("@RefId", SqlDbType.Int, 4);
            refidpara.Value = fun_stockpayapply_ref.RefId;
            paras.Add(refidpara);

            SqlParameter payapplyidpara = new SqlParameter("@PayApplyId", SqlDbType.Int, 4);
            payapplyidpara.Value = fun_stockpayapply_ref.PayApplyId;
            paras.Add(payapplyidpara);

            SqlParameter contractrefidpara = new SqlParameter("@ContractRefId", SqlDbType.Int, 4);
            contractrefidpara.Value = fun_stockpayapply_ref.ContractRefId;
            paras.Add(contractrefidpara);

            SqlParameter stockidpara = new SqlParameter("@StockId", SqlDbType.Int, 4);
            stockidpara.Value = fun_stockpayapply_ref.StockId;
            paras.Add(stockidpara);

            SqlParameter stocklogidpara = new SqlParameter("@StockLogId", SqlDbType.Int, 4);
            stocklogidpara.Value = fun_stockpayapply_ref.StockLogId;
            paras.Add(stocklogidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = fun_stockpayapply_ref.ContractId;
            paras.Add(contractidpara);

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = fun_stockpayapply_ref.SubId;
            paras.Add(subidpara);

            SqlParameter applybalapara = new SqlParameter("@ApplyBala", SqlDbType.Decimal, 9);
            applybalapara.Value = fun_stockpayapply_ref.ApplyBala;
            paras.Add(applybalapara);

            SqlParameter refstatuspara = new SqlParameter("@RefStatus", SqlDbType.Int, 4);
            refstatuspara.Value = fun_stockpayapply_ref.RefStatus;
            paras.Add(refstatuspara);


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int payApplyId, Common.StatusEnum status = Common.StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.Fun_StockPayApply_Ref where PayApplyId={0} and RefStatus >={1}", payApplyId, (int)status);
                
            return Load<Model.StockPayApply>(user,CommandType.Text,cmdText);
        }

        #endregion
    }
}
