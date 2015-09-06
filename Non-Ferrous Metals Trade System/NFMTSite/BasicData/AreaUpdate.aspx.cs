using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class AreaUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 29, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("区域管理", string.Format("{0}BasicData/AreaList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("区域修改", string.Empty);

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("AreaList.aspx");
                        NFMT.Data.BLL.AreaBLL aBLL = new NFMT.Data.BLL.AreaBLL();
                        var result = aBLL.Get(Utility.UserUtility.CurrentUser, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("AreaList.aspx");

                        NFMT.Data.Model.Area area = result.ReturnValue as NFMT.Data.Model.Area;
                        if (area != null)
                        {
                            this.txbAreaName.Value = area.AreaName;
                            this.txbAreaFullName.Value = area.AreaFullName;
                            this.txbAreaShort.Value = area.AreaShort;
                            this.txbAreaCode.Value = area.AreaCode;
                            this.txbAreaZip.Value = area.AreaZip;

                            this.hidAreaStatus.Value = ((int)area.AreaStatus).ToString();
                            this.hidparentID.Value = area.ParentID.ToString();

                            this.hid.Value = Convert.ToString(area.AreaId);
                        }
                    }
                }
            }
        }
    }
}