using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockMoveCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = string.Format("{0}WareHouse/CanStockMoveApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 46, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("移库", string.Format("{0}WareHouse/StockMoveList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("可移库申请列表", redirectUrl);
                this.navigation1.Routes.Add("移库新增", string.Empty);

                int stockMoveApplyId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out stockMoveApplyId) || stockMoveApplyId <= 0)
                    Response.Redirect(redirectUrl);

                this.hidid.Value = stockMoveApplyId.ToString();
            }
        }
    }
}