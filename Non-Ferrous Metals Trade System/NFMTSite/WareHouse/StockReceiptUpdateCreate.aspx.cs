using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockReceiptUpdateCreate : System.Web.UI.Page
    {
        public NFMT.WareHouse.Model.Stock stock = null;
        public NFMT.WareHouse.Model.StockName stockName = null;
        public NFMT.WareHouse.Model.StockReceiptDetail stockReceiptDetail = null;
        public int detailId = 0;
        public int stockId = 0;
        public int stockLogId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            string redirectUrl = string.Format("{0}WareHouse/StockReceiptUpdateList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            this.hidBDStyleId.Value = ((int)NFMT.Data.StyleEnum.报关状态).ToString();

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 131, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("库存净重回执修改列表", redirectUrl);
                this.navigation1.Routes.Add("库存净重回执修改", string.Empty);

                detailId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out detailId) || detailId <= 0)
                    this.WarmAlert("参数错误", redirectUrl);

                stockId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["stockId"]) || !int.TryParse(Request.QueryString["stockId"], out stockId) || stockId <= 0)
                    this.WarmAlert("参数错误", redirectUrl);

                stockLogId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["stockLogId"]) || !int.TryParse(Request.QueryString["stockLogId"], out stockLogId) || stockLogId <= 0)
                    this.WarmAlert("参数错误", redirectUrl);
                
                NFMT.WareHouse.BLL.StockBLL stockBLL = new NFMT.WareHouse.BLL.StockBLL();
                result = stockBLL.Get(user, stockId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                if (stock == null)
                    this.WarmAlert("获取库存出错", redirectUrl);

                NFMT.WareHouse.BLL.StockNameBLL stockNameBLL = new NFMT.WareHouse.BLL.StockNameBLL();
                result = stockNameBLL.Get(user, stock.StockNameId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                stockName = result.ReturnValue as NFMT.WareHouse.Model.StockName;
                if (stockName == null)
                    this.WarmAlert("获取业务单号出错", redirectUrl);

                NFMT.WareHouse.BLL.StockReceiptDetailBLL stockReceiptDetailBLL = new NFMT.WareHouse.BLL.StockReceiptDetailBLL();
                result = stockReceiptDetailBLL.Get(user, detailId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                stockReceiptDetail = result.ReturnValue as NFMT.WareHouse.Model.StockReceiptDetail;
                if (stockName == null)
                    this.WarmAlert("获取仓库回执出错", redirectUrl);

                this.spStockStatus.InnerText = ((NFMT.WareHouse.StockStatusEnum)stock.StockStatus).ToString();
            }
        }
    }
}