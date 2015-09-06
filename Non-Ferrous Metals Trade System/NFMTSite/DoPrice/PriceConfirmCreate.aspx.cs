using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace NFMTSite.DoPrice
{
    public partial class PriceConfirmCreate : System.Web.UI.Page
    {
        public NFMT.Contract.Model.ContractSub sub;
        public NFMT.Contract.Model.Contract contract;
        public NFMT.Data.Model.Currency cur;
        public NFMT.Data.Model.MeasureUnit mu;
        public string strTradeDirection = string.Empty;
        public List<NFMT.Contract.Model.SubCorporationDetail> subInCorpDetails;
        public List<NFMT.Contract.Model.SubCorporationDetail> subOutCorpDetails;

        public decimal lastPayCapital = 0;
        public decimal netWeight = 0;

        public string SelectedJson = string.Empty;

        public decimal RealityAmount = 0;
        public decimal PricingAvg = 0;
        public decimal PremiumAvg = 0;
        public decimal InterestBala = 0;
        public decimal InterestAvg = 0;
        public decimal SettlePrice = 0;
        public decimal SettleBala = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = string.Format("{0}DoPrice/PriceConfirmContractList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 120, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                this.navigation1.Routes.Add("价格确认单列表", string.Format("{0}DoPrice/PriceConfirmList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("点价合约列表", redirectUrl);
                this.navigation1.Routes.Add("价格确认单新增", string.Empty);

                int subId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["subId"]) || !int.TryParse(Request.QueryString["subId"], out subId) || subId <= 0)
                    Utility.JsUtility.WarmAlert(this.Page, "参数错误", redirectUrl);

                //获取子合约
                NFMT.Contract.BLL.ContractSubBLL contractSubBLL = new NFMT.Contract.BLL.ContractSubBLL();
                result = contractSubBLL.Get(user, subId);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                    Utility.JsUtility.WarmAlert(this.Page, "获取子合约失败", redirectUrl);

                NFMT.Contract.BLL.ContractBLL contractBLL = new NFMT.Contract.BLL.ContractBLL();
                result = contractBLL.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                //获取合约
                contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null)
                    Utility.JsUtility.WarmAlert(this.Page, "获取合约失败", redirectUrl);

                this.txbContractNo.Value = contract.OutContractNo;

                if (contract.TradeDirection == (int)NFMT.Contract.TradeDirectionEnum.采购)
                    strTradeDirection = "采购";
                else if (contract.TradeDirection == (int)NFMT.Contract.TradeDirectionEnum.销售)
                    strTradeDirection = "销售";
                else
                    strTradeDirection = string.Empty;

                this.txbTradeDirection.Value = strTradeDirection;

                NFMT.Data.Model.Asset asset = NFMT.Data.BasicDataProvider.Assets.SingleOrDefault(a => a.AssetId == contract.AssetId);
                //this.txbAssetName.Value = asset.AssetName;

                //获取子合约中的币种
                cur = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(a => a.CurrencyId == sub.SettleCurrency);
                if (cur == null)
                    Utility.JsUtility.WarmAlert(this.Page, "获取币种失败", redirectUrl);

                //获取合约单位
                mu = NFMT.Data.BasicDataProvider.MeasureUnits.SingleOrDefault(a => a.MUId == sub.UnitId);
                if (mu == null)
                    Utility.JsUtility.WarmAlert(this.Page, "获取单位失败", redirectUrl);

                //获取合约抬头
                //我方
                NFMT.Contract.BLL.SubCorporationDetailBLL subCorporationDetailBLL = new NFMT.Contract.BLL.SubCorporationDetailBLL();
                result = subCorporationDetailBLL.Load(user, subId, true);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                subInCorpDetails = result.ReturnValue as List<NFMT.Contract.Model.SubCorporationDetail>;
                if (subInCorpDetails == null || !subInCorpDetails.Any())
                    Utility.JsUtility.WarmAlert(this.Page, "获取我方抬头失败", redirectUrl);

                this.txbInCorpId.Value = subInCorpDetails.FirstOrDefault(a => a.IsDefaultCorp == true).CorpName;

                //对方
                result = subCorporationDetailBLL.Load(user, subId, false);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                subOutCorpDetails = result.ReturnValue as List<NFMT.Contract.Model.SubCorporationDetail>;
                if (subInCorpDetails == null || !subInCorpDetails.Any())
                    Utility.JsUtility.WarmAlert(this.Page, "获取我方抬头失败", redirectUrl);

                this.txbOutCorpId.Value = subOutCorpDetails.FirstOrDefault(a => a.IsDefaultCorp == true).CorpName;

                this.SelectJson(sub.SubId);
            }
        }

        public void SelectJson(int subId)
        {
            int pageIndex = 1, pageSize = 100;
            string orderStr = "i.InterestId desc", whereStr = string.Empty;

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();
            NFMT.DoPrice.BLL.PriceConfirmBLL bll = new NFMT.DoPrice.BLL.PriceConfirmBLL();
            select = bll.GetPriceConfirmStockListSelectForCreate(pageIndex, pageSize, orderStr, subId);

            NFMT.Common.ResultModel result = bll.Load(user, select, NFMT.Common.DefaultValue.ClearAuth);
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

            this.SelectedJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());
        }
    }
}