using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.User
{
    public partial class ContactShare : System.Web.UI.Page
    {
        public int curUserId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 81, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.查询 });

                curUserId = Utility.UserUtility.CurrentUser.EmpId;

                this.hidBDStyleId.Value = ((int)NFMT.Data.StyleEnum.在职状态).ToString();
            }
        }
    }
}