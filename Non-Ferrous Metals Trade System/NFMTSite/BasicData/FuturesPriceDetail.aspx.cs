using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class FuturesPriceDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                this.navigation1.Routes.Add("期货合约管理", string.Format("{0}BasicData/FuturesCodeList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("期货合约明细", string.Empty);

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("FuturesCodeList.aspx");
                        NFMT.Data.BLL.FuturesCodeBLL fcBLL = new NFMT.Data.BLL.FuturesCodeBLL();
                        var result = fcBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("FuturesCodeList.aspx");

                        NFMT.Data.Model.FuturesCode fc = result.ReturnValue as NFMT.Data.Model.FuturesCode;
                        if (fc != null)
                        {
                            this.Exchage.InnerText = fc.ExchageId.ToString();
                            this.txbCodeSize.InnerText = fc.CodeSize.ToString();
                            this.firstTradeDate.InnerText = fc.FirstTradeDate.ToString("yyyy-MM-dd");
                            this.lastTradeDate.InnerText = fc.LastTradeDate.ToString("yyyy-MM-dd");
                            this.MU.InnerText = fc.MUId.ToString();
                            this.Currency.InnerText = fc.CurrencyId.ToString();
                            this.txbTradeCode.InnerText = fc.TradeCode;
                            this.FuturesCodeStatus.InnerText = fc.FuturesCodeStatus.ToString();



                            this.hidId.Value = fc.FuturesCodeId.ToString();


                        }
                    }
                }
            }
        }
    }
}