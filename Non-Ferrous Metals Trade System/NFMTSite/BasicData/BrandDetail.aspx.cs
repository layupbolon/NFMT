using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class BrandDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 32, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                this.navigation1.Routes.Add("品牌管理", string.Format("{0}BasicData/BrandList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("品牌明细", string.Empty);

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("BrandList.aspx");
                        NFMT.Data.BLL.BrandBLL brandBLL = new NFMT.Data.BLL.BrandBLL();
                        var result = brandBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", string.Format("<script> alert({0})</script>", result.Message));

                        NFMT.Data.Model.Brand b = result.ReturnValue as NFMT.Data.Model.Brand;
                        if (b != null)
                        {
                            this.producerName.InnerText = b.ProducerName;
                            this.txbBrandName.InnerText = b.BrandName;
                            this.txbBrandInfo.InnerText = b.BrandInfo;
                            this.txbBrandFullName.InnerText = b.BrandFullName;
                            this.BrandStatusName.InnerText = b.BrandStatusName;

                            this.hidId.Value = b.BrandId.ToString();


                        }
                    }
                }
            }
        }
    }
}