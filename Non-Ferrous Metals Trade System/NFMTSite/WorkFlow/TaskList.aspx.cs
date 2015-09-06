using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WorkFlow
{
    public partial class TaskList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Utility.VerificationUtility ver = new Utility.VerificationUtility();
                //ver.JudgeOperate(this.Page, 22, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.查询 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                this.hidEmpId.Value = user.EmpId.ToString();
            }
        }
    }
}