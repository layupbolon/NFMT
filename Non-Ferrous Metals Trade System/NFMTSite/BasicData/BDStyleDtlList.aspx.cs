using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class BDStyleDtlList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 75, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.查询 });

                int pid = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (int.TryParse(Request.QueryString["id"], out pid))
                    {
                        this.btnAdd.HRef = string.Format("BDStyleDtlCreate.aspx?pid={0}", pid);

                        this.navigation1.Routes.Add("类型列表", "BDStyleList.aspx");
                        this.navigation1.Routes.Add("类型明细列表", string.Empty);
                    }
                }
            }
        }
    }
}