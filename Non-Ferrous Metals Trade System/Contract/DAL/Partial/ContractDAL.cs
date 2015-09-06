using NFMT.Common;
using NFMT.Contract.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Contract.DAL
{
    public partial class ContractDAL : ExecOperate, IContractDAL
    {
        #region 新增方法

        public override IAuthority Authority
        {
            get
            {
                return new NFMT.Authority.ContractAuth();
            }
        }

        public override int MenuId
        {
            get
            {
                return 37;
            }
        }

        public override ResultModel Submit(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();
            Model.Contract contract = obj as Model.Contract;
            if (contract.CreateFrom == (int)NFMT.Common.CreateFromEnum.采购合约库存创建 || contract.CreateFrom == (int)NFMT.Common.CreateFromEnum.销售合约库存创建)
            {
                result = base.Submit(user, obj);
                if (result.ResultStatus != 0)
                    return result;

                DAL.ContractSubDAL subDAL = new ContractSubDAL();
                result = subDAL.GetSubByContractId(user, contract.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.ContractSub sub = result.ReturnValue as Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "子合约获取失败";
                    return result;
                }

                if (sub.SubStatus != StatusEnum.待审核)
                {
                    result = subDAL.Submit(user, sub);
                    if (result.ResultStatus != 0)
                        return result;
                }

                if (result.ResultStatus == 0)
                    result.Message = "合约提交审核成功";
            }
            else
                result = base.Submit(user, obj);

            return result;
        }

        public ResultModel GetContractDetail(UserModel user, int contractId, TradeDirectionEnum tradeDirection)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Data.DataTable dt = NFMT.DBUtility.SqlHelper.ExecuteDataTable(this.ConnectString, GetSQLString(contractId, tradeDirection), null, System.Data.CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    result.ReturnValue = dt;
                }
                else
                {
                    result.ResultStatus = -1;
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

        private string GetSQLString(int contractId, TradeDirectionEnum tradeDirection)
        {
            System.Text.StringBuilder sb = new StringBuilder();

            switch (tradeDirection)
            {
                case TradeDirectionEnum.采购:
                    sb.Append("select AssetName,BrandName,Format,OriginPlace,SUM(GrossAmount) GrossAmount,Price,DPAddress from ( ");
                    sb.AppendFormat("select csi.RefId,asset.AssetName,brand.BrandName,si.Format,si.OriginPlace,si.GrossAmount,case con.PriceMode when {0} then ISNULL(cp.AlmostPrice,0) when {1} then ISNULL(cp.FixedPrice,0) else 0 end as Price,dp.DPAddress ", (int)PriceModeEnum.点价, (int)PriceModeEnum.定价);
                    sb.Append("from dbo.St_ContractStockIn_Ref csi ");
                    sb.AppendFormat("inner join dbo.St_StockIn si on csi.StockInId = si.StockInId and si.StockInStatus>={0} ", (int)Common.StatusEnum.已生效);
                    sb.Append("left join dbo.Con_Contract con on con.ContractId = csi.ContractId ");
                    sb.Append("left join NFMT_Basic..Asset asset on si.AssetId = asset.AssetId ");
                    sb.Append("left join NFMT_Basic..Brand brand on si.BrandId = brand.BrandId ");
                    sb.Append("left join NFMT..Con_ContractPrice cp on con.ContractId = cp.ContractId ");
                    sb.AppendFormat("left join NFMT..Pri_PriceConfirm pc on con.ContractId = pc.ContractId and pc.PriceConfirmStatus >={0} ", (int)Common.StatusEnum.已生效);
                    sb.Append("left join NFMT_Basic..DeliverPlace dp on si.DeliverPlaceId = dp.DPId ");
                    sb.AppendFormat("where csi.ContractId = {0} and csi.RefStatus >={1} ", contractId, (int)Common.StatusEnum.已生效);
                    sb.Append(") as t group by AssetName,BrandName,Format,OriginPlace,Price,DPAddress");
                    break;
                case TradeDirectionEnum.销售:
                    sb.Append("select AssetName,BrandName,Format,OriginPlace,SUM(GrossAmount) GrossAmount,Price,DPAddress from ( ");
                    sb.AppendFormat("select sad.DetailId,asset.AssetName,brand.BrandName,st.Format,st.OriginPlace,sad.GrossAmount,case con.PriceMode when {0} then ISNULL(cp.AlmostPrice,0) when {1} then ISNULL(cp.FixedPrice,0) else 0 end as Price,dp.DPAddress ", (int)PriceModeEnum.点价, (int)PriceModeEnum.定价);
                    sb.Append("from dbo.St_StockOutApplyDetail sad ");
                    sb.Append(" left join NFMT..St_Stock st on sad.StockId = st.StockId ");
                    sb.Append(" left join NFMT..Con_Contract con on sad.ContractId = con.ContractId ");
                    sb.Append(" left join NFMT_Basic..Asset asset on st.AssetId = asset.AssetId ");
                    sb.Append(" left join NFMT_Basic..Brand brand on st.BrandId = brand.BrandId ");
                    sb.Append(" left join NFMT..Con_ContractPrice cp on con.ContractId = cp.ContractId ");
                    sb.Append(" left join NFMT_Basic..DeliverPlace dp on st.DeliverPlaceId = dp.DPId ");
                    sb.AppendFormat(" where sad.DetailStatus>={0} and sad.ContractId = {1} ", (int)Common.StatusEnum.已生效, contractId);
                    sb.Append(" ) as t group by AssetName,BrandName,Format,OriginPlace,Price,DPAddress ");
                    break;
                default: break;
            }

            return sb.ToString();
        }

        #endregion
    }
}
