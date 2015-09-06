using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class DeliverPlaceUpdate : System.Web.UI.Page
    {
        public NFMT.Data.Model.DeliverPlace deliverPlace = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectStr = string.Format("{0}BasicData/DeliverPlaceList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 97, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("交货地管理", redirectStr);
                this.navigation1.Routes.Add("交货地修改", string.Empty);

                this.hidBDStyleId.Value = ((int)NFMT.Data.StyleEnum.交货地类型).ToString();

                int dPId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out dPId) || dPId <= 0)
                    Response.Redirect(redirectStr);

                NFMT.Data.BLL.DeliverPlaceBLL deliverPlaceBLL = new NFMT.Data.BLL.DeliverPlaceBLL();
                result = deliverPlaceBLL.Get(user, dPId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectStr);

                deliverPlace = result.ReturnValue as NFMT.Data.Model.DeliverPlace;
                if (deliverPlace == null)
                    Response.Redirect(redirectStr);
            }
        }
    }
}