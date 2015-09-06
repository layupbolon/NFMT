using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
	public partial class CurrencyDetail : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 25, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                this.navigation1.Routes.Add("币种管理", string.Format("{0}BasicData/CurrencyList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("币种明细", string.Empty);

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("ContactList.aspx");
                        NFMT.Data.BLL.CurrencyBLL currencyBLL = new NFMT.Data.BLL.CurrencyBLL();
                        var result = currencyBLL.Get(Utility.UserUtility.CurrentUser, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("ContactList.aspx");

                        NFMT.Data.Model.Currency currency = result.ReturnValue as NFMT.Data.Model.Currency;
                        if (currency != null)
                        {
                            this.txbCurencyShort.InnerText = currency.CurencyShort;
                            this.txbCurrencyFullName.InnerText = currency.CurrencyFullName;
                            this.txbCurrencyName.InnerText = currency.CurrencyName;
                            this.txbCurrencyStatus.InnerText =Convert.ToString(currency.CurrencyStatus);
                            this.hidId.Value = currency.CurrencyId.ToString();
                        }
                    }
                }
            }
		}
	}
}