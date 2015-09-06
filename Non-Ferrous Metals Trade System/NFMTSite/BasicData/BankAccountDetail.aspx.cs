using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class BankAccountDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 28, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                this.navigation1.Routes.Add("银行账户管理", string.Format("{0}BasicData/BankAccountList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("账户管理明细", string.Empty);
                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("BankAccountList.aspx");
                        NFMT.Data.BLL.BankAccountBLL bkBLL = new NFMT.Data.BLL.BankAccountBLL();
                        var result = bkBLL.Get(Utility.UserUtility.CurrentUser, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("BankAccountList.aspx");

                        NFMT.Data.Model.BankAccount ba = result.ReturnValue as NFMT.Data.Model.BankAccount;
                        if (ba != null)
                        {

                            this.hidId.Value = ba.BankAccId.ToString();
                            NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == ba.CompanyId);
                            if (corp == null)
                                Response.Redirect("BankAccountList.aspx");
                            this.txbCorpName.InnerText = corp.CorpName.ToString();
                            NFMT.Data.Model.Bank bank = NFMT.Data.BasicDataProvider.Banks.SingleOrDefault(a => a.BankId == ba.BankId);
                            if (bank == null)
                                Response.Redirect("BankAccountList.aspx");
                            this.txbBankName.InnerText = bank.BankName.ToString();
                            this.txbAccountNo.InnerText = ba.AccountNo.ToString();
                            NFMT.Data.Model.Currency currency = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(a => a.CurrencyId == ba.CurrencyId);
                            if (currency == null)
                                Response.Redirect("BankAccountList.aspx");
                            this.txbCurrencyName.InnerText = currency.CurrencyName.ToString();
                            this.txbBankAccDesc.InnerText = ba.BankAccDesc.ToString();
                            this.txbStatusName.InnerText = ba.BankAccStatusName.ToString();

                        }
                    }
                }
            }
        }
    }
}