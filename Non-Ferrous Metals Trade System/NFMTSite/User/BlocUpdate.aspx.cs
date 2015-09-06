using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class BlocUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string directURL = string.Format("{0}User/BlocList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 15, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("集团管理", directURL);
                this.navigation1.Routes.Add("集团修改", string.Empty);
                
                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(directURL);

                int id = 0;
                if (!int.TryParse(Request.QueryString["id"], out id) || id == 0)
                    Response.Redirect(directURL);

                this.hidId.Value = id.ToString();

                NFMT.User.BLL.BlocBLL blocBLL = new NFMT.User.BLL.BlocBLL();
                var result = blocBLL.Get(user, id);
                if (result.ResultStatus != 0)
                    Response.Redirect(directURL);

                NFMT.User.Model.Bloc bloc = result.ReturnValue as NFMT.User.Model.Bloc;
                if (bloc != null)
                {
                    this.txbblocEName.Value = bloc.BlocEname;
                    this.txbblocFullName.Value = bloc.BlocFullName;
                    this.txbBlocName.Value = bloc.BlocName;
                    //this.hidtext.Value = bloc.IsSelf ? "true" : "false";
                }


            }
        }
    }
}