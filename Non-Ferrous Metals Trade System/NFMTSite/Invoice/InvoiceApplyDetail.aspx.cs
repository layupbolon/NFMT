using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class InvoiceApplyDetail : System.Web.UI.Page
    {
        public NFMT.Invoice.Model.InvoiceApply invoiceApply;
        public NFMT.Operate.Model.Apply apply;
        public int curDeptId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 121, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成, NFMT.Common.OperateEnum.执行完成撤销 });

                this.navigation1.Routes.Add("开票申请列表", "InvoiceApplyList.aspx");
                this.navigation1.Routes.Add("开票申请明细", string.Empty);

                string redirectUrl = string.Format("{0}Invoice/InvoiceApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                curDeptId = user.DeptId;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                NFMT.Invoice.BLL.InvoiceApplyBLL invoiceApplyBLL = new NFMT.Invoice.BLL.InvoiceApplyBLL();
                int applyId = 0;
                int invoiceApplyId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["aid"]) || !int.TryParse(Request.QueryString["aid"], out applyId) || applyId <= 0)
                    applyId = 0;
                else
                {
                    result = invoiceApplyBLL.GetByApplyId(user, applyId);
                    if (result.ResultStatus != 0)
                        Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                    invoiceApply = result.ReturnValue as NFMT.Invoice.Model.InvoiceApply;
                    if (invoiceApply == null)
                        Utility.JsUtility.WarmAlert(this.Page, "获取发票申请错误", redirectUrl);

                    invoiceApplyId = invoiceApply.InvoiceApplyId;
                }

                if (invoiceApplyId == 0)
                {
                    if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out invoiceApplyId) || invoiceApplyId <= 0)
                        Utility.JsUtility.WarmAlert(this.Page, "参数错误", redirectUrl);
                }

                result = invoiceApplyBLL.Get(user, invoiceApplyId);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                invoiceApply = result.ReturnValue as NFMT.Invoice.Model.InvoiceApply;
                if (invoiceApply == null)
                    Utility.JsUtility.WarmAlert(this.Page, "获取开票申请失败", redirectUrl);

                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, invoiceApply.ApplyId);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null)
                    Utility.JsUtility.WarmAlert(this.Page, "获取申请失败", redirectUrl);

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(apply);
                this.hidmodel.Value = json;
            }
        }
    }
}