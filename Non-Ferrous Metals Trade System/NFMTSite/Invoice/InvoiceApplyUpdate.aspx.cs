using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class InvoiceApplyUpdate : System.Web.UI.Page
    {
        public NFMT.Invoice.Model.InvoiceApply invoiceApply;
        public NFMT.Operate.Model.Apply apply;
        public int curDeptId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 121, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("开票申请列表", "InvoiceApplyList.aspx");
                this.navigation1.Routes.Add("开票申请修改", string.Empty);

                string redirectUrl = string.Format("{0}Invoice/InvoiceApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                curDeptId = user.DeptId;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                int invoiceApplyId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out invoiceApplyId) || invoiceApplyId <= 0)
                    Utility.JsUtility.WarmAlert(this.Page, "参数错误", redirectUrl);

                //获取发票申请
                NFMT.Invoice.BLL.InvoiceApplyBLL invoiceApplyBLL = new NFMT.Invoice.BLL.InvoiceApplyBLL();
                result = invoiceApplyBLL.Get(user, invoiceApplyId);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                invoiceApply = result.ReturnValue as NFMT.Invoice.Model.InvoiceApply;
                if (invoiceApply == null)
                    Utility.JsUtility.WarmAlert(this.Page, "获取开票申请失败", redirectUrl);

                //获取主申请
                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, invoiceApply.ApplyId);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null)
                    Utility.JsUtility.WarmAlert(this.Page, "获取申请失败", redirectUrl);

            }
        }
    }
}