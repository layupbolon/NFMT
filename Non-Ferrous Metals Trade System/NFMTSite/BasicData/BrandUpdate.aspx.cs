using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class BrandUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 32, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("品牌管理", string.Format("{0}BasicData/BrandList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("品牌修改", string.Empty);

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("BrandList.aspx");

                        NFMT.Data.BLL.BrandBLL bBLL = new NFMT.Data.BLL.BrandBLL();
                        var result = bBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("BrandList.aspx");

                        NFMT.Data.Model.Brand brand = result.ReturnValue as NFMT.Data.Model.Brand;
                        if (brand != null)
                        {
                            this.txbBrandName.Value = brand.BrandName;
                            this.txbBrandFullName.Value = brand.BrandFullName;
                            this.txbBrandInfo.Value = brand.BrandInfo;

                            this.hidproducerName.Value = brand.ProducerId.ToString();
                            this.hidBrandStatusName.Value = ((int)brand.BrandStatus).ToString();
                        }
                    }
                }
            }
        }
    }
}