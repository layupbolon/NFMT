using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class EmployeeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 18, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.查询 });

                this.hidBDStyleId.Value = ((int)NFMT.Data.StyleEnum.在职状态).ToString();
            }
        }
    }
}