using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockMoveApplyUpdate : System.Web.UI.Page
    {

        public NFMT.Operate.Model.Apply apply;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 45, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                string redirectURL = string.Format("{0}WareHouse/StockMoveApplyList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

                this.navigation1.Routes.Add("移库申请", redirectURL);
                this.navigation1.Routes.Add("移库申请修改", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(redirectURL);

                int stockMoveApplyId = 0;
                if (!int.TryParse(Request.QueryString["id"], out stockMoveApplyId))
                    Response.Redirect(redirectURL);

                this.hidstockMoveApplyId.Value = stockMoveApplyId.ToString();

                NFMT.WareHouse.BLL.StockMoveApplyDetailBLL stockMoveApplyDetailBLL = new NFMT.WareHouse.BLL.StockMoveApplyDetailBLL();
                NFMT.Common.ResultModel result = stockMoveApplyDetailBLL.GetStockIds(user, stockMoveApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                this.hidsids.Value = result.ReturnValue.ToString();

                NFMT.WareHouse.BLL.StockMoveApplyBLL stockMoveApplyBLL = new NFMT.WareHouse.BLL.StockMoveApplyBLL();
                result = stockMoveApplyBLL.Get(user, stockMoveApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                NFMT.WareHouse.Model.StockMoveApply stockMoveApply = result.ReturnValue as NFMT.WareHouse.Model.StockMoveApply;
                if (stockMoveApply == null)
                    Response.Redirect(redirectURL);

                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, stockMoveApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectURL);

                apply = result.ReturnValue as NFMT.Operate.Model.Apply;

                this.hidDeptId.Value = apply.ApplyDept.ToString();
                this.txbMemo.Value = apply.ApplyDesc;

            }
        }
    }
}