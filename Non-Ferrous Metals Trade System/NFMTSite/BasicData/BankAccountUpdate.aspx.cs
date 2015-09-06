using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class BankAccountUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 28, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("银行账户管理", string.Format("{0}BasicData/BankAccountList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("银行账户修改", string.Empty);

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("BankAccountList.aspx");
                        NFMT.Data.BLL.BankAccountBLL baBLL = new NFMT.Data.BLL.BankAccountBLL();
                        var result = baBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("BankAccountList.aspx");

                        NFMT.Data.Model.BankAccount ba = result.ReturnValue as NFMT.Data.Model.BankAccount;

                        if (ba != null)
                        {
                            this.hidNo.Value = id.ToString();

                            this.hidbankId.Value = ba.BankId.ToString();
                            this.hidcompanyId.Value = ba.CompanyId.ToString();
                            this.hidcurrencyId.Value = ba.CurrencyId.ToString();

                            this.txbBankAccDesc.Value = ba.BankAccDesc;
                            this.txbAccountNo.Value = ba.AccountNo;
                            this.hidStatus.Value = ((int)ba.BankAccStatus).ToString();
                        }
                    }
                }
            }
        }
    }
}