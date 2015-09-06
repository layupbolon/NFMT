/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractDAL.cs
// 文件功能描述：合约dbo.Con_Contract数据交互类。
// 创建人：CodeSmith
// 创建时间： 2015年1月20日
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
    /// 合约dbo.Con_Contract数据交互类。
    /// </summary>
    public partial class ContractDAL : ExecOperate, IContractDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractDAL()
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
            Model.Contract con_contract = (Model.Contract)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@ContractId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(con_contract.ContractNo))
            {
                SqlParameter contractnopara = new SqlParameter("@ContractNo", SqlDbType.VarChar, 20);
                contractnopara.Value = con_contract.ContractNo;
                paras.Add(contractnopara);
            }

            if (!string.IsNullOrEmpty(con_contract.OutContractNo))
            {
                SqlParameter outcontractnopara = new SqlParameter("@OutContractNo", SqlDbType.VarChar, 200);
                outcontractnopara.Value = con_contract.OutContractNo;
                paras.Add(outcontractnopara);
            }

            SqlParameter contractdatepara = new SqlParameter("@ContractDate", SqlDbType.DateTime, 8);
            contractdatepara.Value = con_contract.ContractDate;
            paras.Add(contractdatepara);

            if (!string.IsNullOrEmpty(con_contract.ContractName))
            {
                SqlParameter contractnamepara = new SqlParameter("@ContractName", SqlDbType.VarChar, 80);
                contractnamepara.Value = con_contract.ContractName;
                paras.Add(contractnamepara);
            }

            SqlParameter tradeborderpara = new SqlParameter("@TradeBorder", SqlDbType.Int, 4);
            tradeborderpara.Value = con_contract.TradeBorder;
            paras.Add(tradeborderpara);

            SqlParameter contractlimitpara = new SqlParameter("@ContractLimit", SqlDbType.Int, 4);
            contractlimitpara.Value = con_contract.ContractLimit;
            paras.Add(contractlimitpara);

            SqlParameter contractsidepara = new SqlParameter("@ContractSide", SqlDbType.Int, 4);
            contractsidepara.Value = con_contract.ContractSide;
            paras.Add(contractsidepara);

            SqlParameter tradedirectionpara = new SqlParameter("@TradeDirection", SqlDbType.Int, 4);
            tradedirectionpara.Value = con_contract.TradeDirection;
            paras.Add(tradedirectionpara);

            SqlParameter havegoodsflowpara = new SqlParameter("@HaveGoodsFlow", SqlDbType.Int, 4);
            havegoodsflowpara.Value = con_contract.HaveGoodsFlow;
            paras.Add(havegoodsflowpara);

            SqlParameter contractperiodspara = new SqlParameter("@ContractPeriodS", SqlDbType.DateTime, 8);
            contractperiodspara.Value = con_contract.ContractPeriodS;
            paras.Add(contractperiodspara);

            SqlParameter contractperiodepara = new SqlParameter("@ContractPeriodE", SqlDbType.DateTime, 8);
            contractperiodepara.Value = con_contract.ContractPeriodE;
            paras.Add(contractperiodepara);

            SqlParameter premiumpara = new SqlParameter("@Premium", SqlDbType.Decimal, 9);
            premiumpara.Value = con_contract.Premium;
            paras.Add(premiumpara);

            SqlParameter initqppara = new SqlParameter("@InitQP", SqlDbType.DateTime, 8);
            initqppara.Value = con_contract.InitQP;
            paras.Add(initqppara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = con_contract.AssetId;
            paras.Add(assetidpara);

            SqlParameter settlecurrencypara = new SqlParameter("@SettleCurrency", SqlDbType.Int, 4);
            settlecurrencypara.Value = con_contract.SettleCurrency;
            paras.Add(settlecurrencypara);

            SqlParameter signamountpara = new SqlParameter("@SignAmount", SqlDbType.Decimal, 9);
            signamountpara.Value = con_contract.SignAmount;
            paras.Add(signamountpara);

            SqlParameter exeamountpara = new SqlParameter("@ExeAmount", SqlDbType.Decimal, 9);
            exeamountpara.Value = con_contract.ExeAmount;
            paras.Add(exeamountpara);

            SqlParameter unitidpara = new SqlParameter("@UnitId", SqlDbType.Int, 4);
            unitidpara.Value = con_contract.UnitId;
            paras.Add(unitidpara);

            SqlParameter pricemodepara = new SqlParameter("@PriceMode", SqlDbType.Int, 4);
            pricemodepara.Value = con_contract.PriceMode;
            paras.Add(pricemodepara);

            if (!string.IsNullOrEmpty(con_contract.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = con_contract.Memo;
                paras.Add(memopara);
            }

            SqlParameter deliverystylepara = new SqlParameter("@DeliveryStyle", SqlDbType.Int, 4);
            deliverystylepara.Value = con_contract.DeliveryStyle;
            paras.Add(deliverystylepara);

            SqlParameter deliverydatepara = new SqlParameter("@DeliveryDate", SqlDbType.DateTime, 8);
            deliverydatepara.Value = con_contract.DeliveryDate;
            paras.Add(deliverydatepara);

            SqlParameter createfrompara = new SqlParameter("@CreateFrom", SqlDbType.Int, 4);
            createfrompara.Value = con_contract.CreateFrom;
            paras.Add(createfrompara);

            SqlParameter contractstatuspara = new SqlParameter("@ContractStatus", SqlDbType.Int, 4);
            contractstatuspara.Value = con_contract.ContractStatus;
            paras.Add(contractstatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            Model.Contract contract = new Model.Contract();

            int indexContractId = dr.GetOrdinal("ContractId");
            contract.ContractId = Convert.ToInt32(dr[indexContractId]);

            int indexContractNo = dr.GetOrdinal("ContractNo");
            if (dr["ContractNo"] != DBNull.Value)
            {
                contract.ContractNo = Convert.ToString(dr[indexContractNo]);
            }

            int indexOutContractNo = dr.GetOrdinal("OutContractNo");
            if (dr["OutContractNo"] != DBNull.Value)
            {
                contract.OutContractNo = Convert.ToString(dr[indexOutContractNo]);
            }

            int indexContractDate = dr.GetOrdinal("ContractDate");
            if (dr["ContractDate"] != DBNull.Value)
            {
                contract.ContractDate = Convert.ToDateTime(dr[indexContractDate]);
            }

            int indexContractName = dr.GetOrdinal("ContractName");
            if (dr["ContractName"] != DBNull.Value)
            {
                contract.ContractName = Convert.ToString(dr[indexContractName]);
            }

            int indexTradeBorder = dr.GetOrdinal("TradeBorder");
            if (dr["TradeBorder"] != DBNull.Value)
            {
                contract.TradeBorder = Convert.ToInt32(dr[indexTradeBorder]);
            }

            int indexContractLimit = dr.GetOrdinal("ContractLimit");
            if (dr["ContractLimit"] != DBNull.Value)
            {
                contract.ContractLimit = Convert.ToInt32(dr[indexContractLimit]);
            }

            int indexContractSide = dr.GetOrdinal("ContractSide");
            if (dr["ContractSide"] != DBNull.Value)
            {
                contract.ContractSide = Convert.ToInt32(dr[indexContractSide]);
            }

            int indexTradeDirection = dr.GetOrdinal("TradeDirection");
            if (dr["TradeDirection"] != DBNull.Value)
            {
                contract.TradeDirection = Convert.ToInt32(dr[indexTradeDirection]);
            }

            int indexHaveGoodsFlow = dr.GetOrdinal("HaveGoodsFlow");
            if (dr["HaveGoodsFlow"] != DBNull.Value)
            {
                contract.HaveGoodsFlow = Convert.ToInt32(dr[indexHaveGoodsFlow]);
            }

            int indexContractPeriodS = dr.GetOrdinal("ContractPeriodS");
            if (dr["ContractPeriodS"] != DBNull.Value)
            {
                contract.ContractPeriodS = Convert.ToDateTime(dr[indexContractPeriodS]);
            }

            int indexContractPeriodE = dr.GetOrdinal("ContractPeriodE");
            if (dr["ContractPeriodE"] != DBNull.Value)
            {
                contract.ContractPeriodE = Convert.ToDateTime(dr[indexContractPeriodE]);
            }

            int indexPremium = dr.GetOrdinal("Premium");
            if (dr["Premium"] != DBNull.Value)
            {
                contract.Premium = Convert.ToDecimal(dr[indexPremium]);
            }

            int indexInitQP = dr.GetOrdinal("InitQP");
            if (dr["InitQP"] != DBNull.Value)
            {
                contract.InitQP = Convert.ToDateTime(dr[indexInitQP]);
            }

            int indexAssetId = dr.GetOrdinal("AssetId");
            if (dr["AssetId"] != DBNull.Value)
            {
                contract.AssetId = Convert.ToInt32(dr[indexAssetId]);
            }

            int indexSettleCurrency = dr.GetOrdinal("SettleCurrency");
            if (dr["SettleCurrency"] != DBNull.Value)
            {
                contract.SettleCurrency = Convert.ToInt32(dr[indexSettleCurrency]);
            }

            int indexSignAmount = dr.GetOrdinal("SignAmount");
            if (dr["SignAmount"] != DBNull.Value)
            {
                contract.SignAmount = Convert.ToDecimal(dr[indexSignAmount]);
            }

            int indexExeAmount = dr.GetOrdinal("ExeAmount");
            if (dr["ExeAmount"] != DBNull.Value)
            {
                contract.ExeAmount = Convert.ToDecimal(dr[indexExeAmount]);
            }

            int indexUnitId = dr.GetOrdinal("UnitId");
            if (dr["UnitId"] != DBNull.Value)
            {
                contract.UnitId = Convert.ToInt32(dr[indexUnitId]);
            }

            int indexPriceMode = dr.GetOrdinal("PriceMode");
            if (dr["PriceMode"] != DBNull.Value)
            {
                contract.PriceMode = Convert.ToInt32(dr[indexPriceMode]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                contract.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexDeliveryStyle = dr.GetOrdinal("DeliveryStyle");
            if (dr["DeliveryStyle"] != DBNull.Value)
            {
                contract.DeliveryStyle = Convert.ToInt32(dr[indexDeliveryStyle]);
            }

            int indexDeliveryDate = dr.GetOrdinal("DeliveryDate");
            if (dr["DeliveryDate"] != DBNull.Value)
            {
                contract.DeliveryDate = Convert.ToDateTime(dr[indexDeliveryDate]);
            }

            int indexCreateFrom = dr.GetOrdinal("CreateFrom");
            if (dr["CreateFrom"] != DBNull.Value)
            {
                contract.CreateFrom = Convert.ToInt32(dr[indexCreateFrom]);
            }

            int indexContractStatus = dr.GetOrdinal("ContractStatus");
            if (dr["ContractStatus"] != DBNull.Value)
            {
                contract.ContractStatus = (StatusEnum)Convert.ToInt32(dr[indexContractStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                contract.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                contract.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                contract.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                contract.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }


            return contract;
        }

        public override string TableName
        {
            get
            {
                return "Con_Contract";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            Model.Contract con_contract = (Model.Contract)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contract.ContractId;
            paras.Add(contractidpara);

            if (!string.IsNullOrEmpty(con_contract.ContractNo))
            {
                SqlParameter contractnopara = new SqlParameter("@ContractNo", SqlDbType.VarChar, 20);
                contractnopara.Value = con_contract.ContractNo;
                paras.Add(contractnopara);
            }

            if (!string.IsNullOrEmpty(con_contract.OutContractNo))
            {
                SqlParameter outcontractnopara = new SqlParameter("@OutContractNo", SqlDbType.VarChar, 200);
                outcontractnopara.Value = con_contract.OutContractNo;
                paras.Add(outcontractnopara);
            }

            SqlParameter contractdatepara = new SqlParameter("@ContractDate", SqlDbType.DateTime, 8);
            contractdatepara.Value = con_contract.ContractDate;
            paras.Add(contractdatepara);

            if (!string.IsNullOrEmpty(con_contract.ContractName))
            {
                SqlParameter contractnamepara = new SqlParameter("@ContractName", SqlDbType.VarChar, 80);
                contractnamepara.Value = con_contract.ContractName;
                paras.Add(contractnamepara);
            }

            SqlParameter tradeborderpara = new SqlParameter("@TradeBorder", SqlDbType.Int, 4);
            tradeborderpara.Value = con_contract.TradeBorder;
            paras.Add(tradeborderpara);

            SqlParameter contractlimitpara = new SqlParameter("@ContractLimit", SqlDbType.Int, 4);
            contractlimitpara.Value = con_contract.ContractLimit;
            paras.Add(contractlimitpara);

            SqlParameter contractsidepara = new SqlParameter("@ContractSide", SqlDbType.Int, 4);
            contractsidepara.Value = con_contract.ContractSide;
            paras.Add(contractsidepara);

            SqlParameter tradedirectionpara = new SqlParameter("@TradeDirection", SqlDbType.Int, 4);
            tradedirectionpara.Value = con_contract.TradeDirection;
            paras.Add(tradedirectionpara);

            SqlParameter havegoodsflowpara = new SqlParameter("@HaveGoodsFlow", SqlDbType.Int, 4);
            havegoodsflowpara.Value = con_contract.HaveGoodsFlow;
            paras.Add(havegoodsflowpara);

            SqlParameter contractperiodspara = new SqlParameter("@ContractPeriodS", SqlDbType.DateTime, 8);
            contractperiodspara.Value = con_contract.ContractPeriodS;
            paras.Add(contractperiodspara);

            SqlParameter contractperiodepara = new SqlParameter("@ContractPeriodE", SqlDbType.DateTime, 8);
            contractperiodepara.Value = con_contract.ContractPeriodE;
            paras.Add(contractperiodepara);

            SqlParameter premiumpara = new SqlParameter("@Premium", SqlDbType.Decimal, 9);
            premiumpara.Value = con_contract.Premium;
            paras.Add(premiumpara);

            SqlParameter initqppara = new SqlParameter("@InitQP", SqlDbType.DateTime, 8);
            initqppara.Value = con_contract.InitQP;
            paras.Add(initqppara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = con_contract.AssetId;
            paras.Add(assetidpara);

            SqlParameter settlecurrencypara = new SqlParameter("@SettleCurrency", SqlDbType.Int, 4);
            settlecurrencypara.Value = con_contract.SettleCurrency;
            paras.Add(settlecurrencypara);

            SqlParameter signamountpara = new SqlParameter("@SignAmount", SqlDbType.Decimal, 9);
            signamountpara.Value = con_contract.SignAmount;
            paras.Add(signamountpara);

            SqlParameter exeamountpara = new SqlParameter("@ExeAmount", SqlDbType.Decimal, 9);
            exeamountpara.Value = con_contract.ExeAmount;
            paras.Add(exeamountpara);

            SqlParameter unitidpara = new SqlParameter("@UnitId", SqlDbType.Int, 4);
            unitidpara.Value = con_contract.UnitId;
            paras.Add(unitidpara);

            SqlParameter pricemodepara = new SqlParameter("@PriceMode", SqlDbType.Int, 4);
            pricemodepara.Value = con_contract.PriceMode;
            paras.Add(pricemodepara);

            if (!string.IsNullOrEmpty(con_contract.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = con_contract.Memo;
                paras.Add(memopara);
            }

            SqlParameter deliverystylepara = new SqlParameter("@DeliveryStyle", SqlDbType.Int, 4);
            deliverystylepara.Value = con_contract.DeliveryStyle;
            paras.Add(deliverystylepara);

            SqlParameter deliverydatepara = new SqlParameter("@DeliveryDate", SqlDbType.DateTime, 8);
            deliverydatepara.Value = con_contract.DeliveryDate;
            paras.Add(deliverydatepara);

            SqlParameter createfrompara = new SqlParameter("@CreateFrom", SqlDbType.Int, 4);
            createfrompara.Value = con_contract.CreateFrom;
            paras.Add(createfrompara);

            SqlParameter contractstatuspara = new SqlParameter("@ContractStatus", SqlDbType.Int, 4);
            contractstatuspara.Value = con_contract.ContractStatus;
            paras.Add(contractstatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);


            return paras;
        }

        #endregion
    }
}
