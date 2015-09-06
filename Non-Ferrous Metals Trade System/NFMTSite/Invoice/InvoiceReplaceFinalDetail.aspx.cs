using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class InvoiceReplaceFinalDetail : System.Web.UI.Page
    {
        public NFMT.Invoice.InvoiceDirectionEnum invoiceDirection = NFMT.Invoice.InvoiceDirectionEnum.开具;
        public NFMT.Operate.Model.Invoice curInvoice = null;
        public NFMT.Invoice.Model.BusinessInvoice curReplaceInvoice = null;

        public string currencyName = string.Empty;
        public string SelectedJson = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = "InvoiceReplaceFinalList.aspx";

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 63, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成, NFMT.Common.OperateEnum.执行完成撤销 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                //获取替临终票
                int replaceId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out replaceId))
                    Response.Redirect(redirectUrl);

                NFMT.Invoice.BLL.BusinessInvoiceBLL businessInvoiceBLL = new NFMT.Invoice.BLL.BusinessInvoiceBLL();
                result = businessInvoiceBLL.Get(user, replaceId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Invoice.Model.BusinessInvoice replaceInvoice = result.ReturnValue as NFMT.Invoice.Model.BusinessInvoice;
                if (replaceInvoice == null || replaceInvoice.BusinessInvoiceId <= 0)
                    Response.Redirect(redirectUrl);

                this.curReplaceInvoice = replaceInvoice;

                //获取发票主体
                NFMT.Operate.BLL.InvoiceBLL invoiceBLL = new NFMT.Operate.BLL.InvoiceBLL();
                result = invoiceBLL.Get(user, replaceInvoice.InvoiceId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Operate.Model.Invoice invoice = result.ReturnValue as NFMT.Operate.Model.Invoice;
                if (invoice == null || invoice.InvoiceId <= 0)
                    Response.Redirect(redirectUrl);

                this.curInvoice = invoice;

                //获取合约与子合约
                NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBLL.Get(user, replaceInvoice.SubContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                    Response.Redirect(redirectUrl);

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

                if (contract.TradeDirection == (int)NFMT.Contract.TradeDirectionEnum.Buy)
                    invoiceDirection = NFMT.Invoice.InvoiceDirectionEnum.收取;

                this.SelectJson(replaceInvoice, invoiceDirection);

                NFMT.Data.Model.MeasureUnit muContract = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == contract.UnitId);

                this.navigation1.Routes.Add("替临终票列表", redirectUrl);
                this.navigation1.Routes.Add("替临终票明细", string.Empty);

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(invoice);
                this.hidModel.Value = json;

                this.contractExpander1.CurContract = contract;
                this.contractExpander1.CurContractSub = sub;
                this.contractExpander1.RedirectUrl = redirectUrl;                
            }
        }

        public void SelectJson(NFMT.Invoice.Model.BusinessInvoice replaceInvoice, NFMT.Invoice.InvoiceDirectionEnum direction)
        {
            int pageIndex = 1, pageSize = 100;
            string orderStr = string.Empty, whereStr = string.Empty;

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();
            NFMT.Invoice.BLL.BusinessInvoiceBLL bll = new NFMT.Invoice.BLL.BusinessInvoiceBLL();
            
            select = bll.GetReplaceFinalByProvisionalStockListSelect(pageIndex, pageSize, orderStr, replaceInvoice.RefInvoiceId, replaceInvoice.BusinessInvoiceId,true);
            
            NFMT.Common.ResultModel result = bll.Load(user, select,NFMT.Common.DefaultValue.ClearAuth);

            int totalRows = result.AffectCount;
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

            this.SelectedJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());
        }
    }
}