using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class ReceivableUpdate : System.Web.UI.Page
    {
        public NFMT.Funds.Model.Receivable curReceivable = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = string.Format("{0}Funds/ReceivableList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                this.navigation1.Routes.Add("收款登记", redirectUrl);
                this.navigation1.Routes.Add("收款登记修改", string.Empty);

                int receivableId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out receivableId) || receivableId <= 0)
                    Response.Redirect(redirectUrl);

                this.hidid.Value = receivableId.ToString();

                NFMT.Funds.BLL.ReceivableBLL bll = new NFMT.Funds.BLL.ReceivableBLL();
                NFMT.Common.ResultModel result = bll.Get(user, receivableId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Funds.Model.Receivable receivable = result.ReturnValue as NFMT.Funds.Model.Receivable;
                if (receivable != null)
                {
                    this.curReceivable = receivable;
                    //this.hidReceiveDate.Value = receivable.ReceiveDate.ToShortDateString();
                    ////NFMT.User.Model.Corporation innerCorp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == receivable.ReceivableCorpId);
                    //this.hidReceivableCorpId.Value = receivable.ReceivableCorpId.ToString();
                    ////NFMT.Data.Model.Bank recebank = NFMT.Data.BasicDataProvider.Banks.SingleOrDefault(a => a.BankId == receivable.ReceivableBank);
                    //this.hidReceivableBank.Value = receivable.ReceivableBank.ToString();
                    ////NFMT.Data.Model.BankAccount receAccount = NFMT.Data.BasicDataProvider.BankAccounts.SingleOrDefault(a => a.BankAccId == receivable.ReceivableAccoontId);
                    //this.hidReceivableAccoontId.Value = receivable.ReceivableAccoontId.ToString();
                    //this.hidPayBala.Value = receivable.PayBala.ToString();
                    //this.hidCurrencyId.Value = receivable.CurrencyId.ToString();
                    //this.hidPayCorpId.Value = receivable.PayCorpId.ToString();
                    //this.hidPayBankId.Value = receivable.PayBankId.ToString();
                    //this.hidPayAccountId.Value = receivable.PayAccountId.ToString();
                    //this.txbPayWord.Value = receivable.PayWord;
                    //this.txtBankLog.Value = receivable.BankLog;
                    //this.txtPayAccount.Value = receivable.PayAccount;
                    //this.txtPayBank.Value = receivable.PayBank;
                    //this.txtPayCorp.Value = receivable.PayCorpName;
                }
            }
        }
    }
}