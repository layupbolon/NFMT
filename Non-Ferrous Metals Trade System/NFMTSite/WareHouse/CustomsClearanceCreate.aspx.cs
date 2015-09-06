using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class CustomsClearanceCreate : System.Web.UI.Page
    {
        public int customsApplyId = 0;
        public NFMT.WareHouse.Model.CustomsClearanceApply customsClearanceApply = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectURL = string.Format("{0}WareHouse/CustomCanApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 96, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("报关", "CustomsClearanceList.aspx");
                this.navigation1.Routes.Add("可报关申请列表", redirectURL);
                this.navigation1.Routes.Add("报关新增", string.Empty);

                customsApplyId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out customsApplyId))
                    Response.Redirect(redirectURL);

                NFMT.WareHouse.BLL.CustomsDetailBLL customsDetailBLL = new NFMT.WareHouse.BLL.CustomsDetailBLL();
                result = customsDetailBLL.GetStockIdForDownGrid(user, customsApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                this.hidsids.Value = result.ReturnValue.ToString();

                NFMT.WareHouse.BLL.CustomsClearanceApplyBLL customsClearanceApplyBLL = new NFMT.WareHouse.BLL.CustomsClearanceApplyBLL();
                result = customsClearanceApplyBLL.Get(user, customsApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                customsClearanceApply = result.ReturnValue as NFMT.WareHouse.Model.CustomsClearanceApply;
                if (customsClearanceApply == null)
                    Response.Redirect(redirectURL);
            }
        }
    }
}