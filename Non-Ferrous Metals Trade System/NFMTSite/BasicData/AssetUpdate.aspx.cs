using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class AssetUpdate : System.Web.UI.Page
    {
        public NFMT.Data.Model.Asset asset;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 24, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("品种管理", string.Format("{0}BasicData/AssetList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("品种修改", string.Empty);

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("AssetList.aspx");
                        NFMT.Data.BLL.AssetBLL cyBLL = new NFMT.Data.BLL.AssetBLL();
                        var result = cyBLL.Get(Utility.UserUtility.CurrentUser, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("AssetList.aspx");

                        asset = result.ReturnValue as NFMT.Data.Model.Asset;
                        if (asset != null)
                        {
                            this.txbAssetName.Value = asset.AssetName;

                            this.HidNo.Value = asset.MUId.ToString();

                            this.hid.Value = Convert.ToString(asset.AssetId);

                            this.txbAssetStatus.Value = ((int)asset.AssetStatus).ToString();
                        }
                    }
                }
            }
        }
    }
}

