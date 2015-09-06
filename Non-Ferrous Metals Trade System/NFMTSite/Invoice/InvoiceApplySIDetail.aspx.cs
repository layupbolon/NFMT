using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Invoice
{
    public partial class InvoiceApplySIDetail : System.Web.UI.Page
    {
        public int corpId = 0;
        public int curDeptId;
        public string sIIds = string.Empty;
        public int invoiceApplyId = 0;
        public NFMT.Invoice.Model.InvoiceApply invoiceApply;
        public NFMT.Operate.Model.Apply apply;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 121, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成, NFMT.Common.OperateEnum.执行完成撤销 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                corpId = user.CorpId;
                curDeptId = user.DeptId;

                string redirectUrl = string.Format("{0}Invoice/InvoiceApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

                this.navigation1.Routes.Add("发票申请列表", redirectUrl);
                this.navigation1.Routes.Add("价外票发票申请明细", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out invoiceApplyId) || invoiceApplyId <= 0)
                    this.WarmAlert("参数错误", redirectUrl);

                NFMT.Invoice.BLL.InvoiceApplySIDetailBLL invoiceApplySIDetailBLL = new NFMT.Invoice.BLL.InvoiceApplySIDetailBLL();
                NFMT.Common.ResultModel result = invoiceApplySIDetailBLL.GetSIIds(user, invoiceApplyId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                sIIds = result.ReturnValue.ToString();

                NFMT.Invoice.BLL.InvoiceApplyBLL invoiceApplyBLL = new NFMT.Invoice.BLL.InvoiceApplyBLL();
                result = invoiceApplyBLL.Get(user, invoiceApplyId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                invoiceApply = result.ReturnValue as NFMT.Invoice.Model.InvoiceApply;

                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, invoiceApply.ApplyId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                apply = result.ReturnValue as NFMT.Operate.Model.Apply;

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(apply);
                this.hidmodel.Value = json;
            }
        }
    }
}