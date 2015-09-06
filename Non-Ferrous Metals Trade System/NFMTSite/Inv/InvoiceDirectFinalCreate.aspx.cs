using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Inv
{
    public partial class InvoiceDirectFinalCreate : System.Web.UI.Page
    {
        public NFMT.Invoice.InvoiceDirectionEnum invoiceDirection = NFMT.Invoice.InvoiceDirectionEnum.开具;
        public NFMT.Contract.Model.Contract curContract = null;
        public NFMT.Contract.Model.ContractSub curContractSub = null;

        public string currencyName = string.Empty;
        public string SelectedJson = string.Empty;

        public decimal AvgPrice = 0; //均价
        public decimal invoiceBala = 0; //发票金额
        public decimal grossAmount = 0; //毛重
        public decimal netAmount = 0; //净重

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = "InvoiceDirectFinalList.aspx";

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 116, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

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
                    invoiceDirection = NFMT.Invoice.InvoiceDirectionEnum.收取;

                int pageIndex = 1, pageSize = 100;
                string orderStr = string.Empty, whereStr = string.Empty;
                NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();
                NFMT.Invoice.BLL.BusinessInvoiceBLL bll = new NFMT.Invoice.BLL.BusinessInvoiceBLL();

                if (sub.PriceMode == (int)NFMT.Contract.PriceModeEnum.定价)
                {
                    //定价合约

                    //获取子合约价格明细
                    NFMT.Contract.BLL.SubPriceBLL subPriceBLL = new NFMT.Contract.BLL.SubPriceBLL();
                    result = subPriceBLL.GetPriceBySubId(user, sub.SubId);
                    if (result.ResultStatus != 0)
                        Response.Redirect(redirectUrl);

                    NFMT.Contract.Model.SubPrice subPrice = result.ReturnValue as NFMT.Contract.Model.SubPrice;
                    if (subPrice == null || subPrice.SubPriceId <= 0)
                        Response.Redirect(redirectUrl);
                    this.AvgPrice = subPrice.FixedPrice;
                    select = bll.GetDirectFinalStocksSelect(pageIndex, pageSize, orderStr, subId, 0, false, false);
                }
                else
                {
                    select = bll.GetDirectStocksModel(pageIndex, pageSize, orderStr, subId, false, 0);
                }

                result = bll.Load(user, select, NFMT.Common.DefaultValue.ClearAuth);
                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                this.SelectedJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                //if (sub.PriceMode == (int)NFMT.Contract.PriceModeEnum.点价)
                //{
                    decimal sumBala = dt.Select().Sum(temp => Convert.ToDecimal(temp["Bala"]));
                    decimal sumNetAmount = dt.Select().Sum(temp => Convert.ToDecimal(temp["NetAmount"]));

                    if (sumNetAmount == 0)
                        this.WarmAlert("未确认价格，禁止开终票", redirectUrl);
                if (sumNetAmount > 0)
                {
                    this.netAmount = sumNetAmount;
                    this.invoiceBala = Math.Round(sumBala, 2, MidpointRounding.AwayFromZero);
                    this.AvgPrice = Math.Round(this.invoiceBala/sumNetAmount, 4, MidpointRounding.AwayFromZero);
                }
                //}

                NFMT.Data.Model.MeasureUnit muContract = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == contract.UnitId);
                NFMT.Data.Model.MeasureUnit muSub = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == sub.UnitId);

                this.navigation1.Routes.Add("直接终票列表", redirectUrl);
                this.navigation1.Routes.Add(string.Format("直接终票新增", invoiceDirection), string.Empty);

                this.contractExpander1.CurContract = this.curContract;
                this.contractExpander1.CurContractSub = this.curContractSub;
                this.contractExpander1.RedirectUrl = redirectUrl;
            }
        }
    }
}