using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class AreaDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 29, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结,NFMT.Common.OperateEnum.解除冻结 });

                this.navigation1.Routes.Add("区域管理", string.Format("{0}BasicData/AreaList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("区域明细", string.Empty);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("AreaList.aspx");
                        NFMT.Data.BLL.AreaBLL areaBLL = new NFMT.Data.BLL.AreaBLL();
                        var result = areaBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("AreaList.aspx");

                        NFMT.Data.Model.Area area = result.ReturnValue as NFMT.Data.Model.Area;
                        if (area != null)
                        {
                            this.txbAreaName.InnerText = area.AreaName;
                            this.txbAreaFullName.InnerText = area.AreaFullName;
                            this.txbAreaShort.InnerText = area.AreaShort;
                            this.txbAreaCode.InnerText = area.AreaCode;
                            this.txbAreaZip.InnerText = area.AreaZip;

                            NFMT.Data.Model.Area parentArea = NFMT.Data.BasicDataProvider.Areas.SingleOrDefault(a => a.AreaId == area.ParentID);
                            if (parentArea != null)
                                this.txbParentId.InnerHtml = parentArea.AreaName;

                            this.txbAreaStatus.InnerText = area.AreaStatusName.ToString();
                            this.hidId.Value = area.AreaId.ToString();
                        }
                    }
                }
            }
        }
    }
}