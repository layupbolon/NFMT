using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class ExchangeDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 33, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                this.navigation1.Routes.Add("交易所管理", string.Format("{0}BasicData/ExchangeList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("交易所明细", string.Empty);

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("ExchangeList.aspx");
                        NFMT.Data.BLL.ExchangeBLL exchangeBLL = new NFMT.Data.BLL.ExchangeBLL();
                        var result = exchangeBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("ExchangeList.aspx");

                        NFMT.Data.Model.Exchange ec = result.ReturnValue as NFMT.Data.Model.Exchange;
                        if (ec != null)
                        {
                            this.txbExchangeName.InnerText = ec.ExchangeName;
                            this.txbExchangeCode.InnerText = ec.ExchangeCode;
                            this.StatusName.InnerText = ec.ExchangeStatusName;

                            this.hidId.Value = ec.ExchangeId.ToString();


                        }
                    }
                }
            }
        }
    }
}