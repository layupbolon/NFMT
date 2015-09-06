using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class CustomsClearanceApplyUpdate : System.Web.UI.Page
    {
        public NFMT.WareHouse.Model.CustomsClearanceApply customsClearanceApply = null;
        public NFMT.Operate.Model.Apply apply = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectURL = string.Format("{0}WareHouse/CustomsClearanceApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 95, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("报关申请", redirectURL);
                this.navigation1.Routes.Add("报关申请修改", string.Empty);

                int customsApplyId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out customsApplyId))
                    Response.Redirect(redirectURL);

                NFMT.WareHouse.BLL.CustomsClearanceApplyBLL customsClearanceApplyBLL = new NFMT.WareHouse.BLL.CustomsClearanceApplyBLL();
                result = customsClearanceApplyBLL.Get(user, customsApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                customsClearanceApply = result.ReturnValue as NFMT.WareHouse.Model.CustomsClearanceApply;
                if (customsClearanceApply == null)
                    Response.Redirect(redirectURL);

                NFMT.Operate.BLL.ApplyBLL applyBLL =new  NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, customsClearanceApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null)
                    Response.Redirect(redirectURL);

                NFMT.WareHouse.BLL.CustomsApplyDetailBLL customsApplyDetailBLL = new NFMT.WareHouse.BLL.CustomsApplyDetailBLL();
                result = customsApplyDetailBLL.GetStockIdById(user, customsApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                this.hidsids.Value = result.ReturnValue.ToString();

                //attach
                this.attach1.BusinessIdValue = this.customsClearanceApply.ApplyId;
            }
        }
    }
}