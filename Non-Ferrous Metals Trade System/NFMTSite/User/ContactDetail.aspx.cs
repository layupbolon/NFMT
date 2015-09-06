using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class ContactDetail : System.Web.UI.Page
    {
        public NFMT.User.Model.Contact contact = new NFMT.User.Model.Contact();
        public int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 20, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                this.navigation1.Routes.Add("联系人管理", string.Format("{0}User/ContactList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("联系人明细", string.Empty);

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("ContactList.aspx");

                        NFMT.User.BLL.ContactBLL contactBLL = new NFMT.User.BLL.ContactBLL();
                        var result = contactBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("ContactList.aspx");

                        NFMT.User.Model.Contact contact = result.ReturnValue as NFMT.User.Model.Contact;
                        if (contact != null)
                        {
                            this.txbContactName.InnerText = contact.ContactName;
                            this.txbContactCode.InnerText = contact.ContactCode;
                            this.txbContactTel.InnerText = contact.ContactTel;
                            this.txbContactFax.InnerText = contact.ContactFax;
                            this.txbContactAddress.InnerText = contact.ContactAddress;
                            this.ddlCorpId.InnerText = contact.CorpName;
                            this.ContactStatus.InnerText = contact.ContactStatusName;

                            this.hidId.Value = contact.ContactId.ToString();

                            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                            string json = serializer.Serialize(contact);
                            this.hidModel.Value = json;
                        }
                    }
                }
            }
        }
    }
}