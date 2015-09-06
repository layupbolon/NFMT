/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractSubDAL.cs
// 文件功能描述：子合约dbo.Con_ContractSub数据交互类。
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
    /// 子合约dbo.Con_ContractSub数据交互类。
    /// </summary>
    public partial class ContractSubDAL : ExecOperate, IContractSubDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractSubDAL()
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
            ContractSub con_contractsub = (ContractSub)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@SubId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contractsub.ContractId;
            paras.Add(contractidpara);

            SqlParameter contractdatepara = new SqlParameter("@ContractDate", SqlDbType.DateTime, 8);
            contractdatepara.Value = con_contractsub.ContractDate;
            paras.Add(contractdatepara);

            SqlParameter tradeborderpara = new SqlParameter("@TradeBorder", SqlDbType.Int, 4);
            tradeborderpara.Value = con_contractsub.TradeBorder;
            paras.Add(tradeborderpara);

            SqlParameter contractlimitpara = new SqlParameter("@ContractLimit", SqlDbType.Int, 4);
            contractlimitpara.Value = con_contractsub.ContractLimit;
            paras.Add(contractlimitpara);

            SqlParameter contractsidepara = new SqlParameter("@ContractSide", SqlDbType.Int, 4);
            contractsidepara.Value = con_contractsub.ContractSide;
            paras.Add(contractsidepara);

            SqlParameter tradedirectionpara = new SqlParameter("@TradeDirection", SqlDbType.Int, 4);
            tradedirectionpara.Value = con_contractsub.TradeDirection;
            paras.Add(tradedirectionpara);

            SqlParameter contractperiodspara = new SqlParameter("@ContractPeriodS", SqlDbType.DateTime, 8);
            contractperiodspara.Value = con_contractsub.ContractPeriodS;
            paras.Add(contractperiodspara);

            SqlParameter contractperiodepara = new SqlParameter("@ContractPeriodE", SqlDbType.DateTime, 8);
            contractperiodepara.Value = con_contractsub.ContractPeriodE;
            paras.Add(contractperiodepara);

            SqlParameter premiumpara = new SqlParameter("@Premium", SqlDbType.Decimal, 9);
            premiumpara.Value = con_contractsub.Premium;
            paras.Add(premiumpara);

            if (!string.IsNullOrEmpty(con_contractsub.SubNo))
            {
                SqlParameter subnopara = new SqlParameter("@SubNo", SqlDbType.VarChar, 80);
                subnopara.Value = con_contractsub.SubNo;
                paras.Add(subnopara);
            }

            if (!string.IsNullOrEmpty(con_contractsub.OutContractNo))
            {
                SqlParameter outcontractnopara = new SqlParameter("@OutContractNo", SqlDbType.VarChar, 200);
                outcontractnopara.Value = con_contractsub.OutContractNo;
                paras.Add(outcontractnopara);
            }

            if (!string.IsNullOrEmpty(con_contractsub.SubName))
            {
                SqlParameter subnamepara = new SqlParameter("@SubName", SqlDbType.VarChar, 200);
                subnamepara.Value = con_contractsub.SubName;
                paras.Add(subnamepara);
            }

            SqlParameter settlecurrencypara = new SqlParameter("@SettleCurrency", SqlDbType.Int, 4);
            settlecurrencypara.Value = con_contractsub.SettleCurrency;
            paras.Add(settlecurrencypara);

            SqlParameter signamountpara = new SqlParameter("@SignAmount", SqlDbType.Decimal, 9);
            signamountpara.Value = con_contractsub.SignAmount;
            paras.Add(signamountpara);

            SqlParameter exeamountpara = new SqlParameter("@ExeAmount", SqlDbType.Decimal, 9);
            exeamountpara.Value = con_contractsub.ExeAmount;
            paras.Add(exeamountpara);

            SqlParameter unitidpara = new SqlParameter("@UnitId", SqlDbType.Int, 4);
            unitidpara.Value = con_contractsub.UnitId;
            paras.Add(unitidpara);

            SqlParameter pricemodepara = new SqlParameter("@PriceMode", SqlDbType.Int, 4);
            pricemodepara.Value = con_contractsub.PriceMode;
            paras.Add(pricemodepara);

            SqlParameter shiptimepara = new SqlParameter("@ShipTime", SqlDbType.DateTime, 8);
            shiptimepara.Value = con_contractsub.ShipTime;
            paras.Add(shiptimepara);

            SqlParameter arrivetimepara = new SqlParameter("@ArriveTime", SqlDbType.DateTime, 8);
            arrivetimepara.Value = con_contractsub.ArriveTime;
            paras.Add(arrivetimepara);

            SqlParameter initqppara = new SqlParameter("@InitQP", SqlDbType.DateTime, 8);
            initqppara.Value = con_contractsub.InitQP;
            paras.Add(initqppara);

            if (!string.IsNullOrEmpty(con_contractsub.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = con_contractsub.Memo;
                paras.Add(memopara);
            }

            SqlParameter deliverystylepara = new SqlParameter("@DeliveryStyle", SqlDbType.Int, 4);
            deliverystylepara.Value = con_contractsub.DeliveryStyle;
            paras.Add(deliverystylepara);

            SqlParameter deliverydatepara = new SqlParameter("@DeliveryDate", SqlDbType.DateTime, 8);
            deliverydatepara.Value = con_contractsub.DeliveryDate;
            paras.Add(deliverydatepara);

            SqlParameter createfrompara = new SqlParameter("@CreateFrom", SqlDbType.Int, 4);
            createfrompara.Value = con_contractsub.CreateFrom;
            paras.Add(createfrompara);

            SqlParameter substatuspara = new SqlParameter("@SubStatus", SqlDbType.Int, 4);
            substatuspara.Value = con_contractsub.SubStatus;
            paras.Add(substatuspara);

            SqlParameter creatoridpara = new SqlParameter("@CreatorId", SqlDbType.Int, 4);
            creatoridpara.Value = obj.CreatorId;
            paras.Add(creatoridpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = con_contractsub.AssetId;
            paras.Add(assetidpara);


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            ContractSub contractsub = new ContractSub();

            int indexSubId = dr.GetOrdinal("SubId");
            contractsub.SubId = Convert.ToInt32(dr[indexSubId]);

            int indexContractId = dr.GetOrdinal("ContractId");
            if (dr["ContractId"] != DBNull.Value)
            {
                contractsub.ContractId = Convert.ToInt32(dr[indexContractId]);
            }

            int indexContractDate = dr.GetOrdinal("ContractDate");
            if (dr["ContractDate"] != DBNull.Value)
            {
                contractsub.ContractDate = Convert.ToDateTime(dr[indexContractDate]);
            }

            int indexTradeBorder = dr.GetOrdinal("TradeBorder");
            if (dr["TradeBorder"] != DBNull.Value)
            {
                contractsub.TradeBorder = Convert.ToInt32(dr[indexTradeBorder]);
            }

            int indexContractLimit = dr.GetOrdinal("ContractLimit");
            if (dr["ContractLimit"] != DBNull.Value)
            {
                contractsub.ContractLimit = Convert.ToInt32(dr[indexContractLimit]);
            }

            int indexContractSide = dr.GetOrdinal("ContractSide");
            if (dr["ContractSide"] != DBNull.Value)
            {
                contractsub.ContractSide = Convert.ToInt32(dr[indexContractSide]);
            }

            int indexTradeDirection = dr.GetOrdinal("TradeDirection");
            if (dr["TradeDirection"] != DBNull.Value)
            {
                contractsub.TradeDirection = Convert.ToInt32(dr[indexTradeDirection]);
            }

            int indexContractPeriodS = dr.GetOrdinal("ContractPeriodS");
            if (dr["ContractPeriodS"] != DBNull.Value)
            {
                contractsub.ContractPeriodS = Convert.ToDateTime(dr[indexContractPeriodS]);
            }

            int indexContractPeriodE = dr.GetOrdinal("ContractPeriodE");
            if (dr["ContractPeriodE"] != DBNull.Value)
            {
                contractsub.ContractPeriodE = Convert.ToDateTime(dr[indexContractPeriodE]);
            }

            int indexPremium = dr.GetOrdinal("Premium");
            if (dr["Premium"] != DBNull.Value)
            {
                contractsub.Premium = Convert.ToDecimal(dr[indexPremium]);
            }

            int indexSubNo = dr.GetOrdinal("SubNo");
            if (dr["SubNo"] != DBNull.Value)
            {
                contractsub.SubNo = Convert.ToString(dr[indexSubNo]);
            }

            int indexOutContractNo = dr.GetOrdinal("OutContractNo");
            if (dr["OutContractNo"] != DBNull.Value)
            {
                contractsub.OutContractNo = Convert.ToString(dr[indexOutContractNo]);
            }

            int indexSubName = dr.GetOrdinal("SubName");
            if (dr["SubName"] != DBNull.Value)
            {
                contractsub.SubName = Convert.ToString(dr[indexSubName]);
            }

            int indexSettleCurrency = dr.GetOrdinal("SettleCurrency");
            if (dr["SettleCurrency"] != DBNull.Value)
            {
                contractsub.SettleCurrency = Convert.ToInt32(dr[indexSettleCurrency]);
            }

            int indexSignAmount = dr.GetOrdinal("SignAmount");
            if (dr["SignAmount"] != DBNull.Value)
            {
                contractsub.SignAmount = Convert.ToDecimal(dr[indexSignAmount]);
            }

            int indexExeAmount = dr.GetOrdinal("ExeAmount");
            if (dr["ExeAmount"] != DBNull.Value)
            {
                contractsub.ExeAmount = Convert.ToDecimal(dr[indexExeAmount]);
            }

            int indexUnitId = dr.GetOrdinal("UnitId");
            if (dr["UnitId"] != DBNull.Value)
            {
                contractsub.UnitId = Convert.ToInt32(dr[indexUnitId]);
            }

            int indexPriceMode = dr.GetOrdinal("PriceMode");
            if (dr["PriceMode"] != DBNull.Value)
            {
                contractsub.PriceMode = Convert.ToInt32(dr[indexPriceMode]);
            }

            int indexShipTime = dr.GetOrdinal("ShipTime");
            if (dr["ShipTime"] != DBNull.Value)
            {
                contractsub.ShipTime = Convert.ToDateTime(dr[indexShipTime]);
            }

            int indexArriveTime = dr.GetOrdinal("ArriveTime");
            if (dr["ArriveTime"] != DBNull.Value)
            {
                contractsub.ArriveTime = Convert.ToDateTime(dr[indexArriveTime]);
            }

            int indexInitQP = dr.GetOrdinal("InitQP");
            if (dr["InitQP"] != DBNull.Value)
            {
                contractsub.InitQP = Convert.ToDateTime(dr[indexInitQP]);
            }

            int indexMemo = dr.GetOrdinal("Memo");
            if (dr["Memo"] != DBNull.Value)
            {
                contractsub.Memo = Convert.ToString(dr[indexMemo]);
            }

            int indexDeliveryStyle = dr.GetOrdinal("DeliveryStyle");
            if (dr["DeliveryStyle"] != DBNull.Value)
            {
                contractsub.DeliveryStyle = Convert.ToInt32(dr[indexDeliveryStyle]);
            }

            int indexDeliveryDate = dr.GetOrdinal("DeliveryDate");
            if (dr["DeliveryDate"] != DBNull.Value)
            {
                contractsub.DeliveryDate = Convert.ToDateTime(dr[indexDeliveryDate]);
            }

            int indexCreateFrom = dr.GetOrdinal("CreateFrom");
            if (dr["CreateFrom"] != DBNull.Value)
            {
                contractsub.CreateFrom = Convert.ToInt32(dr[indexCreateFrom]);
            }

            int indexSubStatus = dr.GetOrdinal("SubStatus");
            if (dr["SubStatus"] != DBNull.Value)
            {
                contractsub.SubStatus = (StatusEnum)Convert.ToInt32(dr[indexSubStatus]);
            }

            int indexCreatorId = dr.GetOrdinal("CreatorId");
            if (dr["CreatorId"] != DBNull.Value)
            {
                contractsub.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
            }

            int indexCreateTime = dr.GetOrdinal("CreateTime");
            if (dr["CreateTime"] != DBNull.Value)
            {
                contractsub.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
            }

            int indexLastModifyId = dr.GetOrdinal("LastModifyId");
            if (dr["LastModifyId"] != DBNull.Value)
            {
                contractsub.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
            }

            int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
            if (dr["LastModifyTime"] != DBNull.Value)
            {
                contractsub.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
            }

            int indexAssetId = dr.GetOrdinal("AssetId");
            if (dr["AssetId"] != DBNull.Value)
            {
                contractsub.AssetId = Convert.ToInt32(dr[indexAssetId]);
            }


            return contractsub;
        }

        public override string TableName
        {
            get
            {
                return "Con_ContractSub";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            ContractSub con_contractsub = (ContractSub)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter subidpara = new SqlParameter("@SubId", SqlDbType.Int, 4);
            subidpara.Value = con_contractsub.SubId;
            paras.Add(subidpara);

            SqlParameter contractidpara = new SqlParameter("@ContractId", SqlDbType.Int, 4);
            contractidpara.Value = con_contractsub.ContractId;
            paras.Add(contractidpara);

            SqlParameter contractdatepara = new SqlParameter("@ContractDate", SqlDbType.DateTime, 8);
            contractdatepara.Value = con_contractsub.ContractDate;
            paras.Add(contractdatepara);

            SqlParameter tradeborderpara = new SqlParameter("@TradeBorder", SqlDbType.Int, 4);
            tradeborderpara.Value = con_contractsub.TradeBorder;
            paras.Add(tradeborderpara);

            SqlParameter contractlimitpara = new SqlParameter("@ContractLimit", SqlDbType.Int, 4);
            contractlimitpara.Value = con_contractsub.ContractLimit;
            paras.Add(contractlimitpara);

            SqlParameter contractsidepara = new SqlParameter("@ContractSide", SqlDbType.Int, 4);
            contractsidepara.Value = con_contractsub.ContractSide;
            paras.Add(contractsidepara);

            SqlParameter tradedirectionpara = new SqlParameter("@TradeDirection", SqlDbType.Int, 4);
            tradedirectionpara.Value = con_contractsub.TradeDirection;
            paras.Add(tradedirectionpara);

            SqlParameter contractperiodspara = new SqlParameter("@ContractPeriodS", SqlDbType.DateTime, 8);
            contractperiodspara.Value = con_contractsub.ContractPeriodS;
            paras.Add(contractperiodspara);

            SqlParameter contractperiodepara = new SqlParameter("@ContractPeriodE", SqlDbType.DateTime, 8);
            contractperiodepara.Value = con_contractsub.ContractPeriodE;
            paras.Add(contractperiodepara);

            SqlParameter premiumpara = new SqlParameter("@Premium", SqlDbType.Decimal, 9);
            premiumpara.Value = con_contractsub.Premium;
            paras.Add(premiumpara);

            if (!string.IsNullOrEmpty(con_contractsub.SubNo))
            {
                SqlParameter subnopara = new SqlParameter("@SubNo", SqlDbType.VarChar, 80);
                subnopara.Value = con_contractsub.SubNo;
                paras.Add(subnopara);
            }

            if (!string.IsNullOrEmpty(con_contractsub.OutContractNo))
            {
                SqlParameter outcontractnopara = new SqlParameter("@OutContractNo", SqlDbType.VarChar, 200);
                outcontractnopara.Value = con_contractsub.OutContractNo;
                paras.Add(outcontractnopara);
            }

            if (!string.IsNullOrEmpty(con_contractsub.SubName))
            {
                SqlParameter subnamepara = new SqlParameter("@SubName", SqlDbType.VarChar, 200);
                subnamepara.Value = con_contractsub.SubName;
                paras.Add(subnamepara);
            }

            SqlParameter settlecurrencypara = new SqlParameter("@SettleCurrency", SqlDbType.Int, 4);
            settlecurrencypara.Value = con_contractsub.SettleCurrency;
            paras.Add(settlecurrencypara);

            SqlParameter signamountpara = new SqlParameter("@SignAmount", SqlDbType.Decimal, 9);
            signamountpara.Value = con_contractsub.SignAmount;
            paras.Add(signamountpara);

            SqlParameter exeamountpara = new SqlParameter("@ExeAmount", SqlDbType.Decimal, 9);
            exeamountpara.Value = con_contractsub.ExeAmount;
            paras.Add(exeamountpara);

            SqlParameter unitidpara = new SqlParameter("@UnitId", SqlDbType.Int, 4);
            unitidpara.Value = con_contractsub.UnitId;
            paras.Add(unitidpara);

            SqlParameter pricemodepara = new SqlParameter("@PriceMode", SqlDbType.Int, 4);
            pricemodepara.Value = con_contractsub.PriceMode;
            paras.Add(pricemodepara);

            SqlParameter shiptimepara = new SqlParameter("@ShipTime", SqlDbType.DateTime, 8);
            shiptimepara.Value = con_contractsub.ShipTime;
            paras.Add(shiptimepara);

            SqlParameter arrivetimepara = new SqlParameter("@ArriveTime", SqlDbType.DateTime, 8);
            arrivetimepara.Value = con_contractsub.ArriveTime;
            paras.Add(arrivetimepara);

            SqlParameter initqppara = new SqlParameter("@InitQP", SqlDbType.DateTime, 8);
            initqppara.Value = con_contractsub.InitQP;
            paras.Add(initqppara);

            if (!string.IsNullOrEmpty(con_contractsub.Memo))
            {
                SqlParameter memopara = new SqlParameter("@Memo", SqlDbType.VarChar, 4000);
                memopara.Value = con_contractsub.Memo;
                paras.Add(memopara);
            }

            SqlParameter deliverystylepara = new SqlParameter("@DeliveryStyle", SqlDbType.Int, 4);
            deliverystylepara.Value = con_contractsub.DeliveryStyle;
            paras.Add(deliverystylepara);

            SqlParameter deliverydatepara = new SqlParameter("@DeliveryDate", SqlDbType.DateTime, 8);
            deliverydatepara.Value = con_contractsub.DeliveryDate;
            paras.Add(deliverydatepara);

            SqlParameter createfrompara = new SqlParameter("@CreateFrom", SqlDbType.Int, 4);
            createfrompara.Value = con_contractsub.CreateFrom;
            paras.Add(createfrompara);

            SqlParameter substatuspara = new SqlParameter("@SubStatus", SqlDbType.Int, 4);
            substatuspara.Value = con_contractsub.SubStatus;
            paras.Add(substatuspara);

            SqlParameter lastmodifyidpara = new SqlParameter("@LastModifyId", SqlDbType.Int, 4);
            lastmodifyidpara.Value = obj.LastModifyId;
            paras.Add(lastmodifyidpara);

            SqlParameter assetidpara = new SqlParameter("@AssetId", SqlDbType.Int, 4);
            assetidpara.Value = con_contractsub.AssetId;
            paras.Add(assetidpara);


            return paras;
        }

        #endregion
    }
}
