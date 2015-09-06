using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class CashInDetail : System.Web.UI.Page
    {
        public int PayModeStyle = 0;
        public NFMT.Funds.Model.CashIn curCashIn = new NFMT.Funds.Model.CashIn();

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = "CashInList.aspx";

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 55, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.确认完成, NFMT.Common.OperateEnum.确认完成撤销 });

                int cashInId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out cashInId))
                    Response.Redirect(redirectUrl);

                this.navigation1.Routes.Add("收款登记列表", redirectUrl);
                this.navigation1.Routes.Add("收款登记明细", string.Empty);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                NFMT.Funds.BLL.CashInBLL cashInBLL = new NFMT.Funds.BLL.CashInBLL();
                result = cashInBLL.Get(user, cashInId);

                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Funds.Model.CashIn cashIn = result.ReturnValue as NFMT.Funds.Model.CashIn;
                if (cashIn == null || cashIn.CashInId <= 0)
                    Response.Redirect(redirectUrl);

                this.curCashIn = cashIn;

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                this.hidModel.Value = serializer.Serialize(cashIn);

                //attach
                this.attach1.BusinessIdValue = this.curCashIn.CashInId;
                this.PayModeStyle = (int)NFMT.Data.StyleEnum.PayMode;
            }
        }
    }
}