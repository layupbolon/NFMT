using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Inv
{
    public partial class InvoiceProvisionalDetail : System.Web.UI.Page
    {
        public NFMT.Invoice.InvoiceDirectionEnum invoiceDirection = NFMT.Invoice.InvoiceDirectionEnum.开具;
        public NFMT.Contract.Model.Contract curContract = null;
        public NFMT.Contract.Model.ContractSub curContractSub = null;
        public NFMT.Operate.Model.Invoice curInvoice = null;
        public NFMT.Invoice.Model.BusinessInvoice curBusinessInvoice = null;

        public string currencyName = string.Empty;
        public string SelectedJson = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = "InvoiceProvisionalList.aspx";

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 115, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成, NFMT.Common.OperateEnum.执行完成撤销 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                int invoiceId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["iid"]) || !int.TryParse(Request.QueryString["iid"], out invoiceId))
                    invoiceId = 0;

                //获取直接终票
                int businessInvoiceId = 0;
                if (invoiceId == 0 && (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out businessInvoiceId)))
                    Response.Redirect(redirectUrl);
                NFMT.Invoice.BLL.BusinessInvoiceBLL businessInvoiceBLL = new NFMT.Invoice.BLL.BusinessInvoiceBLL();

                if (invoiceId > 0)
                    result = businessInvoiceBLL.GetByInvoiceId(user, invoiceId);
                else
                    result = businessInvoiceBLL.Get(user, businessInvoiceId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Invoice.Model.BusinessInvoice businessInvoice = result.ReturnValue as NFMT.Invoice.Model.BusinessInvoice;
                if (businessInvoice == null || businessInvoice.BusinessInvoiceId <= 0)
                    Response.Redirect(redirectUrl);

                this.curBusinessInvoice = businessInvoice;

                //获取发票主体
                NFMT.Operate.BLL.InvoiceBLL invoiceBLL = new NFMT.Operate.BLL.InvoiceBLL();
                result = invoiceBLL.Get(user, businessInvoice.InvoiceId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Operate.Model.Invoice invoice = result.ReturnValue as NFMT.Operate.Model.Invoice;
                if (invoice == null || invoice.InvoiceId <= 0)
                    Response.Redirect(redirectUrl);

                this.curInvoice = invoice;

                //获取合约与子合约
                NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBLL.Get(user, businessInvoice.SubContractId);
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

                this.SelectJson(businessInvoice, invoiceDirection);

                NFMT.Data.Model.MeasureUnit muContract = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == contract.UnitId);
                NFMT.Data.Model.MeasureUnit muSub = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == sub.UnitId);

                this.navigation1.Routes.Add("临票列表", redirectUrl);
                this.navigation1.Routes.Add(string.Format("临票明细", invoiceDirection), string.Empty);

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(invoice);
                this.hidModel.Value = json;

                this.contractExpander1.CurContract = this.curContract;
                this.contractExpander1.CurContractSub = this.curContractSub;
                this.contractExpander1.RedirectUrl = redirectUrl;
            }
        }

        public void SelectJson(NFMT.Invoice.Model.BusinessInvoice businessInvoice, NFMT.Invoice.InvoiceDirectionEnum direction)
        {
            int pageIndex = 1, pageSize = 100;
            string orderStr = string.Empty, whereStr = string.Empty;

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();
            NFMT.Invoice.BLL.BusinessInvoiceBLL bll = new NFMT.Invoice.BLL.BusinessInvoiceBLL();

            select = bll.GetProvisionalContractStockListSelect(pageIndex, pageSize, orderStr, businessInvoice.SubContractId, businessInvoice.BusinessInvoiceId, false, true);

            NFMT.Common.ResultModel result = bll.Load(user, select, NFMT.Common.DefaultValue.ClearAuth);

            int totalRows = result.AffectCount;
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

            this.SelectedJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());
        }
    }
}