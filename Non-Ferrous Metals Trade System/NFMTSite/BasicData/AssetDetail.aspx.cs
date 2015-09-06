using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class AssetDetail : System.Web.UI.Page
    {
        public NFMT.Data.Model.Asset assrt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 24, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                this.navigation1.Routes.Add("品种管理", string.Format("{0}BasicData/AssetList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("品种明细", string.Empty);

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("AssetList.aspx");
                        NFMT.Data.BLL.AssetBLL currencyBLL = new NFMT.Data.BLL.AssetBLL();
                        var result = currencyBLL.Get(Utility.UserUtility.CurrentUser, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("AssetList.aspx");

                        assrt = result.ReturnValue as NFMT.Data.Model.Asset;
                        if (assrt != null)
                        {
                            this.txbAssetName.InnerText = assrt.AssetName;

                            NFMT.Data.Model.MeasureUnit mu = NFMT.Data.BasicDataProvider.MeasureUnits.SingleOrDefault(a => a.MUId == assrt.MUId);
                            this.txbMUName.InnerText = mu.MUName;

                            this.hidId.Value = Convert.ToString(assrt.AssetId);
                            this.txbAssetStatus.InnerText =Convert.ToString(assrt.AssetStatus);
                        }
                    }
                }
            }
        }

      
    }
}