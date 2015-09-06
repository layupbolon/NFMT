using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class PayApplyInvoiceDetail : System.Web.UI.Page
    {
        public int PayMatterStyle = 0;
        public int PayModeStyle = 0;
        public NFMT.Funds.Model.PayApply curPayApply = new NFMT.Funds.Model.PayApply();
        public NFMT.Operate.Model.Apply curApply = new NFMT.Operate.Model.Apply();
        public string JsonStr = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 52, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.确认完成, NFMT.Common.OperateEnum.确认完成撤销,NFMT.Common.OperateEnum.关闭 });

                string redirectUrl = "PayApplyList.aspx";

                this.navigation1.Routes.Add("付款申请列表", redirectUrl);
                this.navigation1.Routes.Add("付款申请明细--关联发票", string.Empty);

                this.PayMatterStyle = (int)NFMT.Data.StyleEnum.付款事项;
                this.PayModeStyle = (int)NFMT.Data.StyleEnum.PayMode;

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                int applyId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["aid"]) || !int.TryParse(Request.QueryString["aid"], out applyId))
                    applyId = 0;

                int payApplyId = 0;
                if (applyId == 0 && (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out payApplyId)))
                    Response.Redirect(redirectUrl);

                //获取付款申请
                NFMT.Funds.BLL.PayApplyBLL payApplyBLL = new NFMT.Funds.BLL.PayApplyBLL();
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                if (applyId > 0)
                    result = payApplyBLL.GetByApplyId(user, applyId);
                else
                    result = payApplyBLL.Get(user, payApplyId);

                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.curPayApply = result.ReturnValue as NFMT.Funds.Model.PayApply;
                if (this.curPayApply == null || this.curPayApply.PayApplyId <= 0)
                    Response.Redirect(redirectUrl);

                //获取主申请
                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, this.curPayApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.curApply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (this.curApply == null || this.curApply.ApplyId <= 0)
                    Response.Redirect(redirectUrl);

                NFMT.Common.SelectModel select = payApplyBLL.GetInvoiceListByApplySelect(1, 100, "si.SIId desc", this.curPayApply.PayApplyId);
                result = payApplyBLL.Load(user, select,new NFMT.Common.BasicAuth());
                int totalRows = result.AffectCount;
                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                this.JsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                //审核
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(this.curApply);
                this.hidModel.Value = json;
            }
        }
    }
}