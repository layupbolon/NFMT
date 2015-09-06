using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class ReceivableCorpCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = string.Format("{0}Funds/ReceivableCorpReadyCorpList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                this.navigation1.Routes.Add("公司收款分配", string.Format("{0}Funds/ReceivableCorpList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("可收款分配公司列表", redirectUrl);
                this.navigation1.Routes.Add("公司收款分配新增", string.Empty);

                int corpId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out corpId) || corpId <= 0)
                    Response.Redirect(redirectUrl);

                this.hidCorpId.Value = corpId.ToString();

                NFMT.User.BLL.CorporationBLL corporationBLL = new NFMT.User.BLL.CorporationBLL();
                result = corporationBLL.Get(user, corpId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.User.Model.Corporation corp = result.ReturnValue as NFMT.User.Model.Corporation;
                if (corp != null)
                {
                    this.hidBlocId.Value = corp.ParentId.ToString();

                    NFMT.User.Model.Bloc bloc = NFMT.User.UserProvider.Blocs.SingleOrDefault(a => a.BlocId == corp.ParentId);
                    if (bloc != null)
                        this.spanBlocId.InnerText = bloc.BlocName;

                    this.spanCorpCode.InnerText = corp.CorpCode;
                    this.spanCorpName.InnerText = corp.CorpName;
                    this.spanTaxPlayer.InnerText = corp.TaxPayerId;
                    this.spanCorpAddress.InnerText = corp.CorpAddress;
                    this.spanCorpTel.InnerText = corp.CorpTel;
                    this.spanCorpFax.InnerText = corp.CorpFax;
                    this.spanCorpZip.InnerText = corp.CorpZip;
                }
            }
        }
    }
}