using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class CashInAllotCorpCreate : System.Web.UI.Page
    {
        public NFMT.User.Model.Corporation curCorp = null;

        public string curSelectedStr = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = "CashInAllotList.aspx";
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 56, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("公司收款分配", redirectUrl);
                this.navigation1.Routes.Add("公司收款分配新增", string.Empty);

                int corpId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out corpId) || corpId <= 0)
                    Response.Redirect(redirectUrl);

                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                //公司信息                
                NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == corpId);
                if (corp == null || corp.CorpId <= 0)
                    Response.Redirect(redirectUrl);

                this.curCorp = corp;

                this.curSelectedStr = "{}";
            }
        }
    }
}