using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class InvoiceApplyCreate : System.Web.UI.Page
    {
        public int corpId = 0;
        public string bids = string.Empty;
        public int curDeptId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 121, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                curDeptId = user.DeptId;
                string redirectUrl = string.Format("{0}Invoice/InvoiceApplyBusInvList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

                this.navigation1.Routes.Add("发票申请列表", "InvoiceApplyList.aspx");
                this.navigation1.Routes.Add("业务发票列表", redirectUrl);
                this.navigation1.Routes.Add("发票申请新增", string.Empty);

                bids = Request.QueryString["busInvIds"];
                if (string.IsNullOrEmpty(bids))
                    Utility.JsUtility.WarmAlert(this.Page, "参数错误", redirectUrl);

                corpId = user.CorpId;
            }
        }
    }
}