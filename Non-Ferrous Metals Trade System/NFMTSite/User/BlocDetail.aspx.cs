using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class BlocDetail : System.Web.UI.Page
    {
        public NFMT.User.Model.Bloc bloc = new NFMT.User.Model.Bloc();
        public int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 15, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                this.navigation1.Routes.Add("集团管理", string.Format("{0}User/BlocList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("集团明细", string.Empty);

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("BlocList.aspx");

                        NFMT.User.BLL.BlocBLL blocBLL = new NFMT.User.BLL.BlocBLL();
                        var result = blocBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("BlocList.aspx");

                        NFMT.User.Model.Bloc bloc = result.ReturnValue as NFMT.User.Model.Bloc;
                        if (bloc != null)
                        {
                            this.txbblocEName.InnerHtml = bloc.BlocEname;
                            this.txbblocFullName.InnerHtml = bloc.BlocFullName;
                            this.txbBlocName.InnerHtml = bloc.BlocName;
                            this.hidId.Value = bloc.BlocId.ToString();
                        }

                        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                        string json = serializer.Serialize(bloc);
                        this.hidModel.Value = json;
                    }
                }
            }
        }
    }
}