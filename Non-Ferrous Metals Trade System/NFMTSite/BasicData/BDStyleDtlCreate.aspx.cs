using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class BDStyleDtlCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 75, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                int pid = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
                {
                    if (int.TryParse(Request.QueryString["pid"], out pid))
                    {
                        if (pid == 0)
                            Response.Redirect("BasicData/BDStyleList.aspx");

                        NFMT.Data.Model.BDStyle style = NFMT.Data.BasicDataProvider.BDStyles.Single(temp => temp.BDStyleId == pid);
                        this.spnStyleName.InnerHtml = style.BDStyleName;
                        this.spnStyleStatus.InnerHtml = style.BDStyleStatusName;
                        this.hidStyleId.Value = style.BDStyleId.ToString();

                        this.navigation1.Routes.Add("类型列表", "BDStyleList.aspx");
                        this.navigation1.Routes.Add("类型明细列表", string.Format("BDStyleDtlList.aspx?id={0}", pid));
                        this.navigation1.Routes.Add("类型明细添加", string.Empty);
                    }
                }
            }
        }
    }
}