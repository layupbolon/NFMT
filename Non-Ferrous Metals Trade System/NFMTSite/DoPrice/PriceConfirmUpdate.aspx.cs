using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.DoPrice
{
    public partial class PriceConfirmUpdate : System.Web.UI.Page
    {
        public NFMT.DoPrice.Model.PriceConfirm priceConfirm;
        public NFMT.Contract.Model.ContractSub sub;
        public NFMT.Contract.Model.Contract contract;
        public NFMT.Data.Model.Currency cur;
        public NFMT.Data.Model.MeasureUnit mu;
        public string strTradeDirection = string.Empty;
        public List<NFMT.Contract.Model.SubCorporationDetail> subInCorpDetails;
        public List<NFMT.Contract.Model.SubCorporationDetail> subOutCorpDetails;

        public string SelectedJsonUp = string.Empty;
        public string SelectedJsonDown = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = string.Format("{0}DoPrice/PriceConfirmList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 120, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                this.navigation1.Routes.Add("价格确认单列表", redirectUrl);
                this.navigation1.Routes.Add("价格确认单修改", string.Empty);

                int priceConfirmId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out priceConfirmId) || priceConfirmId <= 0)
                    Utility.JsUtility.WarmAlert(this.Page, "参数错误", redirectUrl);

                NFMT.DoPrice.BLL.PriceConfirmBLL priceConfirmBLL = new NFMT.DoPrice.BLL.PriceConfirmBLL();
                result = priceConfirmBLL.Get(user, priceConfirmId);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                priceConfirm = result.ReturnValue as NFMT.DoPrice.Model.PriceConfirm;
                if (priceConfirm == null)
                    Utility.JsUtility.WarmAlert(this.Page, "获取价格确认单失败", redirectUrl);

                int subId = priceConfirm.SubId;

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

                this.txbInCorpId.Value = subInCorpDetails.SingleOrDefault(a => a.IsDefaultCorp == true).CorpName;

                //对方
                result = subCorporationDetailBLL.Load(user, subId, false);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                subOutCorpDetails = result.ReturnValue as List<NFMT.Contract.Model.SubCorporationDetail>;
                if (subInCorpDetails == null || !subInCorpDetails.Any())
                    Utility.JsUtility.WarmAlert(this.Page, "获取我方抬头失败", redirectUrl);

                this.txbOutCorpId.Value = subOutCorpDetails.SingleOrDefault(a => a.IsDefaultCorp == true).CorpName;

                SelectJson(sub.SubId, priceConfirmId);
            }
        }

        public void SelectJson(int subId,int priceConfirmId)
        {
            int pageIndex = 1, pageSize = 100;
            string orderStr = string.Empty, whereStr = string.Empty;

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            NFMT.DoPrice.BLL.PriceConfirmBLL bll = new NFMT.DoPrice.BLL.PriceConfirmBLL();

            select = bll.GetPriceConfirmStockListSelectForUpdateUp(pageIndex, pageSize, orderStr, subId, priceConfirmId);
            result = bll.Load(user, select, NFMT.Common.DefaultValue.ClearAuth);
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

            this.SelectedJsonUp = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

            select = bll.GetPriceConfirmStockListSelectForUpdateDown(pageIndex, pageSize, orderStr, subId, priceConfirmId);
            result = bll.Load(user, select, NFMT.Common.DefaultValue.ClearAuth);
            dt = result.ReturnValue as System.Data.DataTable;

            this.SelectedJsonDown = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());
        }
    }
}