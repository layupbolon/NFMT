using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class InvoiceProvisionalCreate : System.Web.UI.Page
    {
        public NFMT.Invoice.InvoiceDirectionEnum invoiceDirection = NFMT.Invoice.InvoiceDirectionEnum.开具;
        public NFMT.Contract.Model.Contract curContract = null;
        public NFMT.Contract.Model.ContractSub curContractSub = null;
        public string currencyName = string.Empty;
        public string SelectedJson = string.Empty;
        public decimal curUnitPrice = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = "InvoiceProvisionalList.aspx";

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 61, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                //获取合约与子合约
                int subId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out subId))
                    Response.Redirect(redirectUrl);

                NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBLL.Get(user, subId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                    Response.Redirect(redirectUrl);

                this.curContractSub = sub;

                NFMT.Data.Model.Currency currency = NFMT.Data.BasicDataProvider.Currencies.FirstOrDefault(temp => temp.CurrencyId == sub.SettleCurrency);
                if (currency != null && currency.CurrencyId > 0)
                    this.currencyName = currency.CurrencyName;

                NFMT.Contract.BLL.ContractBLL conBLL = new NFMT.Contract.BLL.ContractBLL();
                result = conBLL.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                    Response.Redirect(redirectUrl);

                this.curContract = contract;

                if (contract.TradeDirection == (int)NFMT.Contract.TradeDirectionEnum.Buy)
                {
                    invoiceDirection = NFMT.Invoice.InvoiceDirectionEnum.收取;
                }

                //定价合约
                if (contract.PriceMode == (int)NFMT.Contract.PriceModeEnum.Pricing)
                {
                    //获取发票价格明累
                    NFMT.Contract.BLL.ContractPriceBLL priceBLL = new NFMT.Contract.BLL.ContractPriceBLL();
                    result = priceBLL.GetPriceByContractId(user, contract.ContractId);
                    if (result.ResultStatus != 0)
                        Response.Redirect(redirectUrl);

                    NFMT.Contract.Model.ContractPrice contractPrice = result.ReturnValue as NFMT.Contract.Model.ContractPrice;
                    if (contractPrice == null || contractPrice.ContractPriceId <= 0)
                        Response.Redirect(redirectUrl);

                    this.curUnitPrice = contractPrice.FixedPrice;
                }

                this.SelectJson(sub.SubId, invoiceDirection);

                NFMT.Data.Model.MeasureUnit muContract = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == contract.UnitId);
                NFMT.Data.Model.MeasureUnit muSub = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == sub.UnitId);

                this.navigation1.Routes.Add("临票列表", redirectUrl);
                this.navigation1.Routes.Add(string.Format("临票新增", invoiceDirection), string.Empty);

                this.contractExpander1.CurContract = this.curContract;
                this.contractExpander1.CurContractSub = this.curContractSub;
                this.contractExpander1.RedirectUrl = redirectUrl;
            }
        }

        public void SelectJson(int subId, NFMT.Invoice.InvoiceDirectionEnum direction)
        {
            int pageIndex = 1, pageSize = 100;
            string orderStr = string.Empty, whereStr = string.Empty;

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();
            NFMT.Invoice.BLL.BusinessInvoiceBLL bll = new NFMT.Invoice.BLL.BusinessInvoiceBLL();            
            select = bll.GetProvisionalContractStockListSelect(pageIndex, pageSize, orderStr, subId);
            NFMT.Common.ResultModel result = bll.Load(user, select,NFMT.Common.DefaultValue.ClearAuth);

            int totalRows = result.AffectCount;
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

            this.SelectedJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());
        }
    }
}