using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class CorpUpdate : System.Web.UI.Page
    {
        public int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string directUrl = string.Format("{0}User/CorporationList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 16, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("企业管理", directUrl);
                this.navigation1.Routes.Add("企业修改", string.Empty);

                this.hidStyleId.Value = ((int)NFMT.Data.StyleEnum.公司类型).ToString();

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(directUrl);

                if (!int.TryParse(Request.QueryString["id"], out id) || id == 0)
                    Response.Redirect(directUrl);

                this.hidId.Value = id.ToString();

                NFMT.User.BLL.CorporationBLL corpBLL = new NFMT.User.BLL.CorporationBLL();
                var result = corpBLL.Get(user, id);
                if (result.ResultStatus != 0)
                    Response.Redirect(directUrl);

                NFMT.User.Model.Corporation corp = result.ReturnValue as NFMT.User.Model.Corporation;
                if (corp != null)
                {
                    this.hidBlocId.Value = corp.ParentId.ToString();
                    this.txbCorpCode.Value = corp.CorpCode;
                    this.txbCorpName.Value = corp.CorpName;
                    this.txbCorpEName.Value = corp.CorpEName;
                    this.txbTaxPlayer.Value = corp.TaxPayerId.ToString();
                    this.txbCorpFName.Value = corp.CorpFullName;
                    this.txbCorpFEName.Value = corp.CorpFullEName;
                    this.txbCorpAddress.Value = corp.CorpAddress;
                    this.txbCorpEAddress.Value = corp.CorpEAddress;
                    this.txbCorpTel.Value = corp.CorpTel;
                    this.txbCorpFax.Value = corp.CorpFax;
                    this.txbCorpZip.Value = corp.CorpZip;
                    this.hidCorpType.Value = corp.CorpType.ToString();
                }
            }
        }
    }
}