using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class FuturesCodeDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 34, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

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
                            NFMT.Data.Model.Exchange exchange = NFMT.Data.BasicDataProvider.Exchanges.Single(temp => temp.ExchangeId == fc.ExchageId);
                            this.Exchage.InnerText = exchange.ExchangeName;

                            this.txbCodeSize.InnerText = fc.CodeSize.ToString();
                            this.firstTradeDate.InnerText = fc.FirstTradeDate.ToString("yyyy-MM-dd");
                            this.lastTradeDate.InnerText = fc.LastTradeDate.ToString("yyyy-MM-dd");
                            NFMT.Data.Model.MeasureUnit mu = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == fc.MUId);
                            this.MU.InnerText = mu.MUName;

                            NFMT.Data.Model.Currency currency = NFMT.Data.BasicDataProvider.Currencies.Single(temp => temp.CurrencyId == fc.CurrencyId);

                            this.spnCurrency.InnerText = currency.CurrencyName;
                            this.txbTradeCode.InnerText = fc.TradeCode;
                            this.FuturesCodeStatus.InnerText = fc.StatusName;

                            this.hidId.Value = fc.FuturesCodeId.ToString();


                        }
                    }
                }
            }
        }
    }
}