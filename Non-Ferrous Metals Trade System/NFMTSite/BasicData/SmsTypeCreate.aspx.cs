using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class SmsTypeCreate : System.Web.UI.Page
    {
        private string redirectUrl = string.Format("{0}BasicData/SmsTypeList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 89, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("消息类型管理", redirectUrl);
                this.navigation1.Routes.Add("消息类型新增", string.Empty);
            }
        }
    }
}