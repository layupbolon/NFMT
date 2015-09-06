using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class ReceivableStockCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = string.Format("{0}Funds/ReceivableStockReadyStockList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!IsPostBack)
            {
                this.navigation1.Routes.Add("库存收款分配", string.Format("{0}Funds/ReceivableStockList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("可收款分配库存列表", redirectUrl);
                this.navigation1.Routes.Add("库存收款分配新增", string.Empty);

                int stockId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out stockId) || stockId <= 0)
                    Response.Redirect(redirectUrl);

                this.hidStockId.Value = stockId.ToString();

                NFMT.WareHouse.BLL.StockBLL stockBLL = new NFMT.WareHouse.BLL.StockBLL();
                result = stockBLL.GetCurrencyId(user, stockId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.hidCurrencyId.Value = result.ReturnValue.ToString();

                result = stockBLL.GetStockContractOutCorp(user, stockId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.hidCorps.Value = Newtonsoft.Json.JsonConvert.SerializeObject(result.ReturnValue);

                result = stockBLL.Get(user, stockId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                
                NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                if (stock != null)
                {
                    this.hidStockNameId.Value = stock.StockNameId.ToString();

                    NFMT.WareHouse.BLL.StockNameBLL stockNameBLL = new NFMT.WareHouse.BLL.StockNameBLL();
                    result = stockNameBLL.Get(user, stock.StockNameId);
                    if (result.ResultStatus != 0)
                        Response.Redirect(redirectUrl);
                    NFMT.WareHouse.Model.StockName stockName = result.ReturnValue as NFMT.WareHouse.Model.StockName;

                    if (stockName != null)
                        this.spanRefNo.InnerText = stockName.RefNo;
                    this.spanStockDate.InnerText = stock.StockDate.ToShortDateString();

                    NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == stock.CorpId);
                    if (corp != null)
                        this.spanCorpId.InnerText = corp.CorpName;

                    NFMT.Data.Model.MeasureUnit measureUnit = NFMT.Data.BasicDataProvider.MeasureUnits.SingleOrDefault(a => a.MUId == stock.UintId);
                    if (measureUnit != null)
                        this.spanGrossAmout.InnerText = stock.GrossAmount + measureUnit.MUName;

                    NFMT.Data.Model.Asset asset = NFMT.Data.BasicDataProvider.Assets.SingleOrDefault(a => a.AssetId == stock.AssetId);
                    if (asset != null)
                        this.spanAssetId.InnerText = asset.AssetName;

                    NFMT.Data.Model.Brand brand = NFMT.Data.BasicDataProvider.Brands.SingleOrDefault(a => a.BrandId == stock.BrandId);
                    if (brand != null)
                        this.spanBrandId.InnerText = brand.BrandName;
                }
            }
        }
    }
}