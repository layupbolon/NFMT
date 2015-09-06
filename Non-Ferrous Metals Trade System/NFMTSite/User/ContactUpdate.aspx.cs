using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class ContactUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string directUrl = string.Format("{0}User/ContactList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 20, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("联系人管理", directUrl);
                this.navigation1.Routes.Add("联系人修改", string.Empty);


                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(directUrl);

                int id = 0;
                if (!int.TryParse(Request.QueryString["id"], out id) || id == 0)
                    Response.Redirect(directUrl);

                this.hidId.Value = id.ToString();

                NFMT.User.BLL.ContactBLL contactBLL = new NFMT.User.BLL.ContactBLL();
                var result = contactBLL.Get(user, id);
                if (result.ResultStatus != 0)
                    Response.Redirect(directUrl);

                NFMT.User.Model.Contact contact = result.ReturnValue as NFMT.User.Model.Contact;
                if (contact != null)
                {
                    this.txbContactName.Value = contact.ContactName;
                    this.txbContactCode.Value = contact.ContactCode;
                    this.txbContactTel.Value = contact.ContactTel;
                    this.txbContactFax.Value = contact.ContactFax;
                    this.txbContactAddress.Value = contact.ContactAddress;

                    this.hidCorpId.Value = contact.CompanyId.ToString();
                }
            }
        }
    }
}