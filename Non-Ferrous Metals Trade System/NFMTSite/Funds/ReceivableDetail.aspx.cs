using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class ReceivableDetail : System.Web.UI.Page
    {
        public NFMT.Funds.Model.Receivable receivable = new NFMT.Funds.Model.Receivable();

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = string.Format("{0}Funds/ReceivableList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            if (!IsPostBack)
            {
                this.navigation1.Routes.Add("收款登记", redirectUrl);
                this.navigation1.Routes.Add("收款登记明细", string.Empty);

                int receivableId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out receivableId) || receivableId <= 0)
                    Response.Redirect(redirectUrl);

                this.hidid.Value = receivableId.ToString();

                NFMT.Funds.BLL.ReceivableBLL bll = new NFMT.Funds.BLL.ReceivableBLL();
                NFMT.Common.ResultModel result = bll.Get(user, receivableId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                receivable = result.ReturnValue as NFMT.Funds.Model.Receivable;
                if (receivable != null)
                {
                    this.dtReceiveDate.InnerText = receivable.ReceiveDate.ToShortDateString();

                    NFMT.User.Model.Corporation innerCorp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == receivable.ReceivableCorpId);
                    this.ddlReceivableCorpId.InnerText = innerCorp.CorpName;

                    NFMT.Data.Model.Bank recebank = NFMT.Data.BasicDataProvider.Banks.SingleOrDefault(a => a.BankId == receivable.ReceivableBank);
                    this.ddlReceivableBank.InnerText = recebank.BankName;

                    NFMT.Data.Model.BankAccount receAccount = NFMT.Data.BasicDataProvider.BankAccounts.SingleOrDefault(a => a.BankAccId == receivable.ReceivableAccoontId);
                    this.ddlReceivableAccoontId.InnerText = receAccount.AccountNo;

                    this.nbPayBala.InnerText = receivable.PayBala.ToString();

                    NFMT.Data.Model.Currency currency = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(a => a.CurrencyId == receivable.CurrencyId);
                    this.ddlCurrencyId.InnerText = currency.CurrencyName;

                    this.ddlPayCorpId.InnerText = receivable.PayCorpName;

                    this.ddlPayBankId.InnerText = receivable.PayBank;

                    this.ddlPayAccountId.InnerText = receivable.PayAccount;

                    this.txbPayWord.InnerText = receivable.PayWord;

                    this.txtBankLog.InnerText = receivable.BankLog;

                    string json = serializer.Serialize(receivable);
                    this.hidModel.Value = json;
                }
            }
        }
    }
}