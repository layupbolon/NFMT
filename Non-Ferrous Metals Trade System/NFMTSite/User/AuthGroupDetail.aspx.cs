using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class AuthGroupDetail : System.Web.UI.Page
    {
        public NFMT.User.Model.AuthGroup authGroup = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 99, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                string redirectUrl = string.Format("{0}User/AuthGroupList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                this.navigation1.Routes.Add("权限组管理", redirectUrl);
                this.navigation1.Routes.Add("权限组明细", string.Empty);

                this.hidContractInOut.Value = ((int)NFMT.Data.StyleEnum.ContractSide).ToString();
                this.hidContractLimit.Value = ((int)NFMT.Data.StyleEnum.ContractLimit).ToString();
                this.hidTradeBorder.Value = ((int)NFMT.Data.StyleEnum.TradeBorder).ToString();
                this.hidTradeDirection.Value = ((int)NFMT.Data.StyleEnum.TradeDirection).ToString();

                int authGroupId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out authGroupId) || authGroupId <= 0)
                    Response.Redirect(redirectUrl);

                NFMT.User.BLL.AuthGroupBLL authGroupBLL = new NFMT.User.BLL.AuthGroupBLL();
                result = authGroupBLL.Get(user, authGroupId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                authGroup = result.ReturnValue as NFMT.User.Model.AuthGroup;
                if (authGroup == null)
                    Response.Redirect(redirectUrl);
            }
        }
    }
}