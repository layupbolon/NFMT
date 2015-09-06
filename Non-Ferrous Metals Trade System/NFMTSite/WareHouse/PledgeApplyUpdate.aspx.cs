using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class PledgeApplyUpdate : System.Web.UI.Page
    {
        public int curDeptId = Utility.UserUtility.CurrentUser.DeptId;
        public NFMT.Operate.Model.Apply apply;
        public NFMT.WareHouse.Model.PledgeApply pledgeApply;
        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectURL = string.Format("{0}WareHouse/PledgeApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 47, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("质押申请", redirectURL);
                this.navigation1.Routes.Add("质押申请修改", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(redirectURL);

                int pledgeApplyId = 0;
                if (!int.TryParse(Request.QueryString["id"], out pledgeApplyId))
                    Response.Redirect(redirectURL);

                this.hidpledgeApplyId.Value = pledgeApplyId.ToString();

                NFMT.WareHouse.BLL.PledgeApplyDetailBLL pledgeApplyDetailBLL = new NFMT.WareHouse.BLL.PledgeApplyDetailBLL();
                NFMT.Common.ResultModel result = pledgeApplyDetailBLL.GetStockIds(user, pledgeApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                this.hidsids.Value = result.ReturnValue.ToString();

                NFMT.WareHouse.BLL.PledgeApplyBLL pledgeApplyBLL = new NFMT.WareHouse.BLL.PledgeApplyBLL();
                result = pledgeApplyBLL.GetPledgeApplyDetails(user, pledgeApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                this.hidDetails.Value = Newtonsoft.Json.JsonConvert.SerializeObject(result.ReturnValue);
                
                result = pledgeApplyBLL.Get(user, pledgeApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                pledgeApply = result.ReturnValue as NFMT.WareHouse.Model.PledgeApply;
                if (pledgeApply == null)
                    Response.Redirect(redirectURL);

                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, pledgeApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                apply = result.ReturnValue as NFMT.Operate.Model.Apply;

                //this.hidDeptId.Value = apply.ApplyDept.ToString();
                this.txbMemo.Value = apply.ApplyDesc;
            }
        }
    }
}