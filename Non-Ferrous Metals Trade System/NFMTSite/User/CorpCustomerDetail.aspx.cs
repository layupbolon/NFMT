using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class CorpCustomerDetail : System.Web.UI.Page
    {
        public NFMT.User.Model.Corporation corporation;
        public NFMT.User.Model.CorporationDetail corpDetail;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string redirectUrl = string.Format("{0}User/CorporationList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 16, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                this.navigation1.Routes.Add("企业管理", redirectUrl);
                this.navigation1.Routes.Add("客户明细", string.Empty);

                NFMT.User.BLL.CorporationDetailBLL corporationDetailBLL = new NFMT.User.BLL.CorporationDetailBLL();
                //int detailId = 0;
                int corpId = 0;
                //if (!string.IsNullOrEmpty(Request.QueryString["detailId"]) && int.TryParse(Request.QueryString["detailId"], out detailId))
                //{
                //    result = corporationDetailBLL.Get(user, detailId);
                //    NFMT.User.Model.CorporationDetail corporationDetail = result.ReturnValue as NFMT.User.Model.CorporationDetail;
                //    if (corporationDetail == null)
                //        Response.Redirect(redirectUrl);
                //    corpId = corporationDetail.CorpId;
                //}
                
                //if (detailId == 0)
                //{
                    if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out corpId))
                        Response.Redirect(redirectUrl);
                //}

                NFMT.User.BLL.CorporationBLL corporationBLL = new NFMT.User.BLL.CorporationBLL();
                result = corporationBLL.Get(user, corpId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                corporation = result.ReturnValue as NFMT.User.Model.Corporation;
                if (corporation == null)
                    Response.Redirect(redirectUrl);

                result = corporationDetailBLL.GetByCorpId(user, corpId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                corpDetail = result.ReturnValue as NFMT.User.Model.CorporationDetail;
                if (corpDetail == null)
                    Response.Redirect(redirectUrl);

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(corporation);
                this.hidModel.Value = json;
            }
        }
    }
}