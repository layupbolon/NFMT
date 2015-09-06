using System;
using System.Collections.Generic;
using System.Web.UI;
using NFMT.Common;
using NFMT.Data;
using NFMT.WareHouse.BLL;
using NFMT.WareHouse.Model;
using NFMTSite.Utility;

namespace NFMTSite.WareHouse
{
    public partial class PreToRealCreate : Page
    {
        public Stock stock = null;
        public StockName stockName = null;
        //public string stockJson = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            UserModel user = UserUtility.CurrentUser;
            ResultModel result = new ResultModel();
            string redirectUrl = string.Format("{0}WareHouse/PreToRealList.aspx", DefaultValue.NftmSiteName);

            this.hidBDStyleId.Value = ((int)StyleEnum.报关状态).ToString();

            if (!IsPostBack)
            {
                VerificationUtility ver = new VerificationUtility();
                ver.JudgeOperate(this.Page, 129, new List<OperateEnum>() { OperateEnum.录入 });

                this.navigation1.Routes.Add("预入库转正式库存列表", redirectUrl);
                this.navigation1.Routes.Add("预入库转正式库存新增", string.Empty);

                int stockId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out stockId) || stockId <= 0)
                    Response.Redirect(redirectUrl);

                StockBLL stockBLL = new StockBLL();
                result = stockBLL.Get(user, stockId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                stock = result.ReturnValue as Stock;
                if (stock == null)
                    Response.Redirect(redirectUrl);

                StockNameBLL stockNameBLL = new StockNameBLL();
                result = stockNameBLL.Get(user, stock.StockNameId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                stockName = result.ReturnValue as StockName;
                if (stockName == null)
                    Response.Redirect(redirectUrl);

                //this.spStockStatus.InnerText = ((NFMT.WareHouse.StockStatusEnum)stock.StockStatus).ToString();

                //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                //this.stockJson = serializer.Serialize(stock);
            }
        }
    }
}