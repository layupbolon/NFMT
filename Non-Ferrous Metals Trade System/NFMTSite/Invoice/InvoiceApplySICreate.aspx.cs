using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class InvoiceApplySICreate : System.Web.UI.Page
    {
        public int corpId = 0;
        public string sIIds = string.Empty;
        public int curDeptId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 121, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                corpId = user.CorpId;
                curDeptId = user.DeptId;

                string redirectUrl = string.Format("{0}Invoice/InvoiceApplySIList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

                this.navigation1.Routes.Add("发票申请列表", "InvoiceApplyList.aspx");
                this.navigation1.Routes.Add("可开票价外票列表", redirectUrl);
                this.navigation1.Routes.Add("价外票发票申请新增", string.Empty);

                sIIds = Request.QueryString["sIIds"];
                if (string.IsNullOrEmpty(sIIds))
                    Utility.JsUtility.WarmAlert(this.Page, "参数错误", redirectUrl);                
            }
        }
    }
}