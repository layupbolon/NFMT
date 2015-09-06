using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class BankUpdate : System.Web.UI.Page
    {
        public NFMT.Data.Model.Bank bank;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 27, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("银行管理", string.Format("{0}BasicData/BankList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("银行修改", string.Empty);

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("BankList.aspx");

                        NFMT.Data.BLL.BankBLL bBLL = new NFMT.Data.BLL.BankBLL();
                        var result = bBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("BankList.aspx");

                        bank = result.ReturnValue as  NFMT.Data.Model.Bank;

                        int styleId = (int)NFMT.Data.StyleEnum.CapitalType;
                        this.hidStyleId.Value = styleId.ToString();
                    }
                }
            }
        }
    }
}