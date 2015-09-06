using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
	public partial class CurrencyUpdate : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 25, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("币种管理", string.Format("{0}BasicData/CurrencyList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("币种修改", string.Empty);

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("CurrencyList.aspx");
                        NFMT.Data.BLL.CurrencyBLL cyBLL = new NFMT.Data.BLL.CurrencyBLL();
                        var result = cyBLL.Get(Utility.UserUtility.CurrentUser, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("CurrencyList.aspx");

                        NFMT.Data.Model.Currency cy = result.ReturnValue as NFMT.Data.Model.Currency;
                        if (cy != null)
                        {
                            this.txbCurrencyName.Value = cy.CurrencyName;
                            this.txbCurrencyShort.Value = cy.CurencyShort;
                            this.txbCurrencyFullName.Value = cy.CurrencyFullName;

                            this.txbCurrencyStatus.Value = ((int)cy.CurrencyStatus).ToString();

                            this.hid.Value = Convert.ToString(cy.CurrencyId);
                        }
                    }
                }
            }
		}
	}
}