using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class PayApplyStockDetail : System.Web.UI.Page
    {
        public int PayMatterStyle = 0;
        public int PayModeStyle = 0;
        public string SelectedJson = string.Empty;
        public NFMT.Funds.Model.PayApply curPayApply = new NFMT.Funds.Model.PayApply();
        public NFMT.Operate.Model.Apply curApply = new NFMT.Operate.Model.Apply();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 52, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.确认完成, NFMT.Common.OperateEnum.确认完成撤销, NFMT.Common.OperateEnum.关闭 });

                this.navigation1.Routes.Add("付款申请列表", "PayApplyList.aspx");
                this.navigation1.Routes.Add("付款申请查看--关联库存", string.Empty);

                this.PayMatterStyle = (int)NFMT.Data.StyleEnum.付款事项;
                this.PayModeStyle = (int)NFMT.Data.StyleEnum.PayMode;

                int applyId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["aid"]) || !int.TryParse(Request.QueryString["aid"], out applyId))
                    applyId = 0;


                int payApplyId = 0;
                if (applyId == 0 && (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out payApplyId)))
                    Response.Redirect("PayApplyList.aspx");              

                //验证付款申请是否存在
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Funds.BLL.PayApplyBLL bll = new NFMT.Funds.BLL.PayApplyBLL();
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                if (applyId > 0)
                    result = bll.GetByApplyId(user, applyId);
                else
                    result = bll.Get(user, payApplyId);

                NFMT.Funds.Model.PayApply payApply = result.ReturnValue as NFMT.Funds.Model.PayApply;
                if (payApply == null || payApply.PayApplyId <= 0)
                    Response.Redirect("PayApplyList.aspx");

                this.curPayApply = payApply;

                //获取主申请
                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, payApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect("PayApplyList.aspx");

                NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null || apply.ApplyId <= 0)
                    Response.Redirect("PayApplyList.aspx");

                this.curApply = apply;
                this.SelectJson(payApply.PayApplyId);

                //审核
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(apply);
                this.hidModel.Value = json;

                result = bll.GetAuditInfo(user, payApply.ApplyId, NFMT.Funds.FundsStyleEnum.库存付款申请);
                if (result.ResultStatus != 0)
                    Response.Redirect("PayApplyList.aspx");

                this.txbAuditInfo.InnerHtml = result.ReturnValue.ToString();
            }
        }

        public void SelectJson(int payApplyId)
        {
            int pageIndex = 1, pageSize = 100;
            string orderStr = string.Empty, whereStr = string.Empty;

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.Funds.BLL.PayApplyBLL bll = new NFMT.Funds.BLL.PayApplyBLL();
            NFMT.Common.SelectModel select = bll.GetSelectedSelect(pageIndex, pageSize, orderStr, payApplyId);
            NFMT.Common.ResultModel result = bll.Load(user, select,new NFMT.Common.BasicAuth());

            int totalRows = result.AffectCount;
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;           

            this.SelectedJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());
        }
    }
}