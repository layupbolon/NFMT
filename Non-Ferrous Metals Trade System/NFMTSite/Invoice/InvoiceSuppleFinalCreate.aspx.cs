using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class InvoiceSuppleFinalCreate : System.Web.UI.Page
    {
        public NFMT.Contract.Model.Contract curContract = null;
        public NFMT.Contract.Model.ContractSub curContractSub = null;
        public NFMT.Invoice.Model.BusinessInvoice curFinalInvoice = null;
        public NFMT.Operate.Model.Invoice curInvoice = null;
        public NFMT.Invoice.InvoiceDirectionEnum invoiceDirection = NFMT.Invoice.InvoiceDirectionEnum.开具;
        public string currencyName = string.Empty;
        public string curUnit = string.Empty;
        public string SelectedJson = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = "InvoiceSuppleFinalList.aspx";

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 64, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                //获取终票
                int finalId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out finalId))
                    Response.Redirect(redirectUrl);

                NFMT.Invoice.BLL.BusinessInvoiceBLL businessInvoiceBLL = new NFMT.Invoice.BLL.BusinessInvoiceBLL();
                result = businessInvoiceBLL.Get(user, finalId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Invoice.Model.BusinessInvoice finalInvoice = result.ReturnValue as NFMT.Invoice.Model.BusinessInvoice;
                if (finalInvoice == null || finalInvoice.BusinessInvoiceId <= 0)
                    Response.Redirect(redirectUrl);

                this.curFinalInvoice = finalInvoice;

                //获取主发票
                NFMT.Operate.BLL.InvoiceBLL invoiceBLL = new NFMT.Operate.BLL.InvoiceBLL();
                result = invoiceBLL.Get(user, finalInvoice.InvoiceId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Operate.Model.Invoice invoice = result.ReturnValue as NFMT.Operate.Model.Invoice;
                if (invoice == null || invoice.InvoiceId <= 0)
                    Response.Redirect(redirectUrl);

                this.curInvoice = invoice;

                //获取合约与子合约
                NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBLL.Get(user, finalInvoice.SubContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                    Response.Redirect(redirectUrl);

                this.curContractSub = sub;

                //币种和计量单位
                NFMT.Data.Model.Currency currency = NFMT.Data.BasicDataProvider.Currencies.FirstOrDefault(temp => temp.CurrencyId == sub.SettleCurrency);
                if (currency != null && currency.CurrencyId > 0)
                    this.currencyName = currency.CurrencyName;
                NFMT.Data.Model.MeasureUnit mu = NFMT.Data.BasicDataProvider.MeasureUnits.FirstOrDefault(temp => temp.MUId == sub.UnitId);
                if (mu != null && mu.MUId > 0)
                    this.curUnit = mu.MUName;


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

                this.SelectJson(finalInvoice.BusinessInvoiceId);

                this.navigation1.Routes.Add("补零终票列表", redirectUrl);
                this.navigation1.Routes.Add("补零终票新增", string.Empty);

                this.contractExpander1.CurContract = contract;
                this.contractExpander1.CurContractSub = sub;
                this.contractExpander1.RedirectUrl = redirectUrl;
            }
        }

        public void SelectJson(int finalInvoiceId)
        {
            int pageIndex = 1, pageSize = 100;
            string orderStr = string.Empty, whereStr = string.Empty;

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();
            NFMT.Invoice.BLL.BusinessInvoiceBLL bll = new NFMT.Invoice.BLL.BusinessInvoiceBLL();

            select = bll.GetSuppleFinalByFinalStockListSelect(pageIndex, pageSize, orderStr, finalInvoiceId,0);
            NFMT.Common.ResultModel result = bll.Load(user, select,NFMT.Common.DefaultValue.ClearAuth);
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

            this.SelectedJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());
        }
    }
}