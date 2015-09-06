using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class MeasureUnitDetail : System.Web.UI.Page
    {
        public NFMT.Data.Model.MeasureUnit mu;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 23, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                this.navigation1.Routes.Add("计量单位管理", string.Format("{0}BasicData/MeasureUnitList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("计量单位明细", string.Empty);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                int id = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out id))
                    {
                        if (id == 0)
                            Response.Redirect("MeasureUnitList.aspx");
                        NFMT.Data.BLL.MeasureUnitBLL muBLL = new NFMT.Data.BLL.MeasureUnitBLL();
                        var result = muBLL.Get(user, id);
                        if (result.ResultStatus != 0)
                            Response.Redirect("MeasureUnitList.aspx");

                        mu = result.ReturnValue as NFMT.Data.Model.MeasureUnit;
                    }
                }
            }
        }
    }
}