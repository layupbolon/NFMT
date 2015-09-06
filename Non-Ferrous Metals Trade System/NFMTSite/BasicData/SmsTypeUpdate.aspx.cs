using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class SmsTypeUpdate : System.Web.UI.Page
    {
        public int id = 0;
        public int status = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 89, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("消息类型管理", string.Format("{0}BasicData/SmsTypeList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("消息类型修改", string.Empty);

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("SmsTypeList.aspx");
                        NFMT.Sms.BLL.SmsTypeBLL smsTypeBLL = new NFMT.Sms.BLL.SmsTypeBLL();
                        var result = smsTypeBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("SmsTypeList.aspx");

                        NFMT.Sms.Model.SmsType smsType = result.ReturnValue as NFMT.Sms.Model.SmsType;
                        if (smsType != null)
                        {
                            this.txbTypeName.Value = smsType.TypeName;

                            this.txbListUrl.Value = smsType.ListUrl;

                            this.txbViewUrl.Value = smsType.ViewUrl;
                            this.status = (int)smsType.SmsTypeStatus;
                        }
                    }
                }
            }
        }
    }
}