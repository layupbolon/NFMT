using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Contract
{
    public partial class LcDetail : System.Web.UI.Page
    {
        public NFMT.Contract.Model.Lc auditProgressModel = new NFMT.Contract.Model.Lc();
        public int lcId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectURL = string.Format("{0}Contract/LcList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                this.navigation1.Routes.Add("信用证管理", redirectURL);
                this.navigation1.Routes.Add("信用证明细", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(redirectURL);

                if (!int.TryParse(Request.QueryString["id"], out lcId))
                    Response.Redirect(redirectURL);

                this.hidId.Value = lcId.ToString();

                NFMT.Contract.BLL.LcBLL lcBLL = new NFMT.Contract.BLL.LcBLL();
                NFMT.Common.ResultModel result = lcBLL.Get(user, lcId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                NFMT.Contract.Model.Lc lc = result.ReturnValue as NFMT.Contract.Model.Lc;
                if (lc != null)
                {
                    NFMT.Data.Model.Bank aviseBank = NFMT.Data.BasicDataProvider.Banks.FirstOrDefault(temp => temp.BankId == lc.AdviseBank);
                    if (aviseBank != null && aviseBank.BankId > 0)
                        this.ddlAdviseBank.InnerText = aviseBank.BankName;//lc.AviseBankName.ToString();

                    NFMT.Data.Model.Currency currency = NFMT.Data.BasicDataProvider.Currencies.FirstOrDefault(temp => temp.CurrencyId == lc.Currency);
                    if (currency != null && currency.CurrencyId > 0)
                        this.ddlCurrency.InnerText = currency.CurrencyName;//lc.CurrencyName.ToString();

                    this.nbFutureDay.InnerText = lc.FutureDayName.ToString();

                    NFMT.Data.Model.Bank issueBank = NFMT.Data.BasicDataProvider.Banks.FirstOrDefault(temp => temp.BankId == lc.IssueBank);
                    if (issueBank != null && issueBank.BankId > 0)
                        this.ddlIssueBank.InnerText = issueBank.BankName;//lc.IssueBankName.ToString();
                    this.dtIssueDate.InnerText = lc.IssueDate.ToShortDateString();
                    this.nbLcBala.InnerText = lc.LcBalaName.ToString();

                    NFMT.Common.IModel model = lc;
                    string json = serializer.Serialize(model);
                    this.hidModel.Value = json;
                }
            }
        }

    }
}