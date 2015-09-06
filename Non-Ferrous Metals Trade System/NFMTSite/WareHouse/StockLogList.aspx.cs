using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockLogList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.navigation1.Routes.Add("库存查看", "StockList.aspx");
                this.navigation1.Routes.Add("库存流水查看", string.Empty);

                int stockId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect("StockList.aspx");
                if (!int.TryParse(Request.QueryString["id"], out stockId))
                    Response.Redirect("StockList.aspx");

                this.hidStockId.Value = stockId.ToString();

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                NFMT.WareHouse.BLL.StockBLL stockBLL = new NFMT.WareHouse.BLL.StockBLL();
                NFMT.Common.ResultModel result = stockBLL.Get(user, stockId);
                if (result.ResultStatus != 0)
                    Response.Redirect("StockList.aspx");

                NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                if(stock==null)
                    Response.Redirect("StockList.aspx");
                this.spnStockDate.InnerHtml = stock.StockDate.ToShortDateString();

                NFMT.WareHouse.BLL.StockNameBLL stockNameBLL = new NFMT.WareHouse.BLL.StockNameBLL();
                result = stockNameBLL.Get(user, stock.StockNameId);
                if (result.ResultStatus != 0)
                    Response.Redirect("StockList.aspx");

                NFMT.WareHouse.Model.StockName stockName = result.ReturnValue as NFMT.WareHouse.Model.StockName;
                if(stockName==null)
                    Response.Redirect("StockList.aspx");

                this.spnStockName.InnerHtml = stockName.RefNo;
                NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.SingleOrDefault(temp => temp.CorpId == stock.CorpId);
                if(corp!=null && corp.CorpId>0)
                    this.spnOwnCorp.InnerHtml = corp.CorpName;

                NFMT.Data.DetailCollection col = NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.CustomType);
                if (col != null && col.Count != 0)
                {
                    NFMT.Data.Model.BDStyleDetail detail = col[stock.CustomsType];
                    if (detail != null)
                        this.spnCustomType.InnerHtml = detail.DetailName;
                }

                NFMT.Data.Model.Asset ass = NFMT.Data.BasicDataProvider.Assets.SingleOrDefault(temp => temp.AssetId == stock.AssetId);
                if(ass!=null && ass.AssetId>0)
                    this.spnAsset.InnerHtml = ass.AssetName;

                NFMT.Data.Model.Brand bra = NFMT.Data.BasicDataProvider.Brands.SingleOrDefault(temp => temp.BrandId == stock.BrandId);
                if(bra!=null && bra.BrandId>0)
                    this.spnBrand.InnerHtml = bra.BrandName;

                NFMT.Data.Model.MeasureUnit mu = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == stock.UintId);
                this.spnStockAmount.InnerHtml = string.Format("{0}{1}", stock.GrossAmount.ToString(), mu.MUName);

                this.spnStockStatus.InnerHtml = stock.StockStatusName;
            }
        }
    }
}