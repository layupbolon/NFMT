using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Inv
{
    public partial class InvoiceSuppleFinalUpdate : System.Web.UI.Page
    {
        public NFMT.Contract.Model.Contract curContract = null;
        public NFMT.Contract.Model.ContractSub curContractSub = null;
        public NFMT.Invoice.Model.BusinessInvoice curFinalInvoice = null;
        public NFMT.Invoice.Model.BusinessInvoice curSuppleInvoice = null;
        public NFMT.Operate.Model.Invoice curInvoice = null;
        public string currencyName = string.Empty;
        public string curUnit = string.Empty;

        public string ReceiptJson = string.Empty;
        public string SuppleJson = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = "InvoiceSuppleFinalList.aspx";

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 64, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
                NFMT.Invoice.BLL.BusinessInvoiceBLL businessInvoiceBLL = new NFMT.Invoice.BLL.BusinessInvoiceBLL();

                //获取补零票
                int suppleId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out suppleId))
                    Response.Redirect(redirectUrl);

                result = businessInvoiceBLL.Get(user, suppleId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Invoice.Model.BusinessInvoice suppleInvoice = result.ReturnValue as NFMT.Invoice.Model.BusinessInvoice;
                if (suppleInvoice == null || suppleInvoice.BusinessInvoiceId <= 0)
                    Response.Redirect(redirectUrl);

                this.curSuppleInvoice = suppleInvoice;

                //获取终票                
                result = businessInvoiceBLL.Get(user, suppleInvoice.RefInvoiceId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Invoice.Model.BusinessInvoice finalInvoice = result.ReturnValue as NFMT.Invoice.Model.BusinessInvoice;
                if (finalInvoice == null || finalInvoice.BusinessInvoiceId <= 0)
                    Response.Redirect(redirectUrl);

                this.curFinalInvoice = finalInvoice;

                //获取主发票
                NFMT.Operate.BLL.InvoiceBLL invoiceBLL = new NFMT.Operate.BLL.InvoiceBLL();
                result = invoiceBLL.Get(user, suppleInvoice.InvoiceId);
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
                NFMT.Data.Model.Currency currency = NFMT.Data.BasicDataProvider.Currencies.FirstOrDefault(temp => temp.CurrencyId == invoice.CurrencyId);
                if (currency != null && currency.CurrencyId > 0)
                    this.currencyName = currency.CurrencyName;
                NFMT.Data.Model.MeasureUnit mu = NFMT.Data.BasicDataProvider.MeasureUnits.FirstOrDefault(temp => temp.MUId == suppleInvoice.MUId);
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

                this.SelectJson(suppleInvoice.RefInvoiceId, suppleInvoice.BusinessInvoiceId);

                this.navigation1.Routes.Add("补零终票列表", redirectUrl);
                this.navigation1.Routes.Add("补零终票修改", string.Empty);

                this.contractExpander1.CurContract = contract;
                this.contractExpander1.CurContractSub = sub;
                this.contractExpander1.RedirectUrl = redirectUrl;
            }
        }

        public void SelectJson(int finalInvoiceId, int suppleInvoiceId)
        {
            int pageIndex = 1, pageSize = 100;
            string orderStr = string.Empty, whereStr = string.Empty;

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();
            NFMT.Invoice.BLL.BusinessInvoiceBLL bll = new NFMT.Invoice.BLL.BusinessInvoiceBLL();

            select = bll.GetSuppleFinalByFinalStockListSelect(pageIndex, pageSize, orderStr, finalInvoiceId, suppleInvoiceId);
            NFMT.Common.ResultModel result = bll.Load(user, select);
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            this.ReceiptJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

            select = bll.GetSuppleFinalByFinalStockListSelect(pageIndex, pageSize, orderStr, finalInvoiceId, suppleInvoiceId, true);
            result = bll.Load(user, select);
            dt = result.ReturnValue as System.Data.DataTable;
            this.SuppleJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());
        }
    }
}