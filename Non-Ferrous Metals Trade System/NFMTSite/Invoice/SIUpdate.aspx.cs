using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class SIUpdate : System.Web.UI.Page
    {
        public int deptId = 0;
        public int invoiceId = 0;
        public int SIId = 0;
        public string styleId = ((int)NFMT.Data.StyleEnum.发票内容).ToString();
        public int invoiceType = (int)NFMT.Invoice.InvoiceTypeEnum.价外票;
        public int invoiceDirection = NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.发票方向)["Issue"].StyleDetailId;
        public string InvoiceName = string.Empty;
        public DateTime InvoiceDate = NFMT.Common.DefaultValue.DefaultTime;
        public decimal InvoiceBala = 0;
        public int CurrencyId = 0;
        public int OutCorpId = 0;
        public int InCorpId = 0;
        public string Memo = string.Empty;
        public int ChangeCurrencyId = 0;
        public decimal ChangeRate = 0;
        public decimal ChangeBala = 0;
        public int PayDept = 0;
        
        string redirectUrl = string.Format("{0}Invoice/SIList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
        NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
        NFMT.Operate.BLL.InvoiceBLL invoiceBLL = new NFMT.Operate.BLL.InvoiceBLL();
        NFMT.Invoice.BLL.SIBLL sIBLL = new NFMT.Invoice.BLL.SIBLL();
        NFMT.Invoice.BLL.SIDetailBLL sIDetailBLL = new NFMT.Invoice.BLL.SIDetailBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 65, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("价外票", redirectUrl);
                this.navigation1.Routes.Add("价外票修改", string.Empty);

                deptId = user.DeptId;

                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out invoiceId) || invoiceId <= 0)
                    Response.Redirect(redirectUrl);

                //获取invoice
                result = invoiceBLL.Get(user, invoiceId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Operate.Model.Invoice invoice = result.ReturnValue as NFMT.Operate.Model.Invoice;
                if (invoice == null)
                    Response.Redirect(redirectUrl);

                //通过invoiceId获取SI
                result = sIBLL.GetSIbyInvoiceId(user, invoiceId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Invoice.Model.SI si = result.ReturnValue as NFMT.Invoice.Model.SI;
                if (si == null)
                    Response.Redirect(redirectUrl);

                SIId = si.SIId;

                //获取价外票明细
                result = sIDetailBLL.GetSIDetailForUpdate(user, si.SIId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.hidDetails.Value = Newtonsoft.Json.JsonConvert.SerializeObject(result.ReturnValue);

                InvoiceName = invoice.InvoiceName;
                InvoiceDate = invoice.InvoiceDate;
                InvoiceBala = invoice.InvoiceBala;
                CurrencyId = invoice.CurrencyId;
                OutCorpId = invoice.OutCorpId;
                InCorpId = invoice.InCorpId;
                Memo = invoice.Memo;

                ChangeCurrencyId = si.ChangeCurrencyId;
                ChangeRate = si.ChangeRate;
                ChangeBala = si.ChangeBala;
                PayDept = si.PayDept;

                //attach
                this.attach1.BusinessIdValue = invoice.InvoiceId;
            }
        }
    }
}