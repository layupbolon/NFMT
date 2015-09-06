using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using NFMT.Common;
using NFMT.Data;
using NFMT.Data.Model;
using NFMT.User;
using NFMT.User.BLL;
using NFMT.User.Model;
using NFMTSite.Utility;

namespace NFMTSite.User
{
    public partial class CorpDetail : Page
    {
        public Corporation corp = new Corporation();
        public int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            UserModel user = UserUtility.CurrentUser;
            string directUrl = string.Format("{0}User/CorporationList.aspx", DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                VerificationUtility ver = new VerificationUtility();
                ver.JudgeOperate(Page, 16, new List<OperateEnum>() { OperateEnum.冻结, OperateEnum.解除冻结 });

                navigation1.Routes.Add("企业管理", directUrl);
                navigation1.Routes.Add("企业明细", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(directUrl);

                if (!int.TryParse(Request.QueryString["id"], out id) || id == 0)
                    Response.Redirect(directUrl);

                CorporationBLL corpBLL = new CorporationBLL();
                var result = corpBLL.Get(user, id);
                if (result.ResultStatus != 0)
                    Response.Redirect(directUrl);

                Corporation corporation = result.ReturnValue as Corporation;
                if (corporation != null)
                {
                    Bloc bloc = UserProvider.Blocs.SingleOrDefault(a => a.BlocId == corporation.ParentId);
                    if (bloc != null)
                        ddlBlocId.InnerText = bloc.BlocName;
                    txbCorpCode.InnerText = corporation.CorpCode;
                    txbCorpName.InnerText = corporation.CorpName;
                    txbCorpEName.InnerText = corporation.CorpEName;
                    txbTaxPlayer.InnerText = corporation.TaxPayerId;
                    txbCorpFName.InnerText = corporation.CorpFullName;
                    txbCorpFEName.InnerText = corporation.CorpFullEName;
                    txbCorpAddress.InnerText = corporation.CorpAddress;
                    txbCorpEAddress.InnerText = corporation.CorpEAddress;
                    txbCorpTel.InnerText = corporation.CorpTel;
                    txbCorpFax.InnerText = corporation.CorpFax;
                    txbCorpZip.InnerText = corporation.CorpZip;
                    BDStyleDetail bd = DetailProvider.Details(StyleEnum.CorpType)[corporation.CorpType];
                    if (bd != null)
                        ddlCorpType.InnerText = bd.DetailName;

                    hidId.Value = corporation.CorpId.ToString();

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string json = serializer.Serialize(corporation);
                    hidModel.Value = json;
                }
            }
        }
    }
}