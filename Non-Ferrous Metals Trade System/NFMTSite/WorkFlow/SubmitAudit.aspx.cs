using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WorkFlow
{
    public partial class SubmitAudit : System.Web.UI.Page
    {
        public string curTitle = string.Empty;
        public string curContent = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string redirectUrl = "#";
                int mid =0;
                if (string.IsNullOrEmpty(Request.QueryString["mid"]) || !int.TryParse(Request.QueryString["mid"], out mid) || mid <= 0)
                    Response.Redirect(redirectUrl);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                //获取模板
                NFMT.WorkFlow.BLL.FlowMasterBLL fowMasterBLL = new NFMT.WorkFlow.BLL.FlowMasterBLL();
                result = fowMasterBLL.Get(user, mid);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.WorkFlow.Model.FlowMaster flowMaster = result.ReturnValue as NFMT.WorkFlow.Model.FlowMaster;

                //模板标题
                if (flowMaster != null)
                    this.curTitle = flowMaster.ViewTitle;
                //模板内容
            }
        }
    }
}