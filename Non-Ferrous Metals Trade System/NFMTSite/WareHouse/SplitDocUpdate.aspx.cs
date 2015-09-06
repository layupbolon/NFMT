using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class SplitDocUpdate : System.Web.UI.Page
    {
        public NFMT.WareHouse.Model.Stock stock = null;
        public NFMT.WareHouse.Model.StockName stockName = null;
        public NFMT.WareHouse.Model.SplitDoc splitDoc = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            string redirectUrl = string.Format("{0}WareHouse/SplitDocList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            this.hidBDStyleId.Value = ((int)NFMT.Data.StyleEnum.报关状态).ToString();

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 93, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                this.navigation1.Routes.Add("拆单", redirectUrl);
                this.navigation1.Routes.Add("拆单修改", string.Empty);

                int splitDocId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out splitDocId) || splitDocId <= 0)
                    Response.Redirect(redirectUrl);

                NFMT.WareHouse.BLL.SplitDocBLL splitDocBLL = new NFMT.WareHouse.BLL.SplitDocBLL();
                result = splitDocBLL.Get(user, splitDocId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                splitDoc = result.ReturnValue as NFMT.WareHouse.Model.SplitDoc;
                if (splitDoc == null)
                    Response.Redirect(redirectUrl);

                NFMT.WareHouse.BLL.StockBLL stockBLL = new NFMT.WareHouse.BLL.StockBLL();
                result = stockBLL.Get(user, splitDoc.OldStockId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                if (stock == null)
                    Response.Redirect(redirectUrl);

                NFMT.WareHouse.BLL.StockNameBLL stockNameBLL = new NFMT.WareHouse.BLL.StockNameBLL();
                result = stockNameBLL.Get(user, stock.StockNameId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                stockName = result.ReturnValue as NFMT.WareHouse.Model.StockName;
                if (stockName == null)
                    Response.Redirect(redirectUrl);

                this.spStockStatus.InnerText = ((NFMT.WareHouse.StockStatusEnum)stock.StockStatus).ToString();

                //attach
                this.attach1.BusinessIdValue = this.splitDoc.SplitDocId;
            }
        }
    }
}