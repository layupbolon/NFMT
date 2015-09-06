using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockMoveList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 46, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.查询 });

                this.hidBDStyleId.Value = ((int)NFMT.Data.StyleEnum.报关状态).ToString();
            }
        }
    }
}