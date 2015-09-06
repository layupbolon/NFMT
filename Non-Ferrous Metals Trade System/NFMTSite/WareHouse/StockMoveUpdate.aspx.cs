using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockMoveUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = string.Format("{0}WareHouse/StockMoveList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 46, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("移库", redirectUrl);
                this.navigation1.Routes.Add("移库修改", string.Empty);

                int stockMoveId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out stockMoveId) || stockMoveId <= 0)
                    Response.Redirect(redirectUrl);

                this.hidid.Value = stockMoveId.ToString();

                NFMT.WareHouse.BLL.StockMoveBLL stockMoveBLL = new NFMT.WareHouse.BLL.StockMoveBLL();
                NFMT.Common.ResultModel result = stockMoveBLL.Get(user, stockMoveId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.WareHouse.Model.StockMove stockMove = result.ReturnValue as NFMT.WareHouse.Model.StockMove;
                if (stockMove == null)
                    Response.Redirect(redirectUrl);

                this.hidStockMoveApplyId.Value = stockMove.StockMoveApplyId.ToString();
                //this.hidDPId.Value = stockMove.DeliverPlaceId.ToString();
                this.txbMoveMemo.Value = stockMove.MoveMemo;
            }
        }
    }
}