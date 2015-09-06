using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class FuturesCodeUpdate : System.Web.UI.Page
    {
        public NFMT.Data.Model.FuturesCode curFuturesCode = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 34, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("期货合约管理", string.Format("{0}BasicData/FuturesCodeList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("期货合约修改", string.Empty);

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("FuturesCodeList.aspx");

                        NFMT.Data.BLL.FuturesCodeBLL bBLL = new NFMT.Data.BLL.FuturesCodeBLL();
                        var result = bBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("FuturesCodeList.aspx");

                        NFMT.Data.Model.FuturesCode fC = result.ReturnValue as NFMT.Data.Model.FuturesCode;
                        if (fC != null)
                        {
                           // this.hidBirthday.Value = emp.BirathDay.ToString("yyyy-MM-dd");
                            this.txbCodeSize.Value = fC.CodeSize.ToString();
                            this.txbTradeCode.Value = fC.TradeCode.ToString();

                            this.hidFirstTradeDate.Value = fC.FirstTradeDate.ToString("yyyy-MM-dd");
                            this.hidLastTradeDate.Value = fC.LastTradeDate.ToString("yyyy-MM-dd");
                            this.hidMU.Value = fC.MUId.ToString();
                            this.hidCurrency.Value = fC.CurrencyId.ToString();
                            this.hidExchage.Value = fC.ExchageId.ToString();

                            this.hidFuturesCodeStatus.Value = ((int)fC.FuturesCodeStatus).ToString();

                            this.curFuturesCode = fC;
                        }
                    }
                }
            }
        }
    }
}