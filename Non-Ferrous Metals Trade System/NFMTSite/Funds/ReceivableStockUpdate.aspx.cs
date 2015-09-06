using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class ReceivableStockUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = string.Format("{0}Funds/ReceivableStockList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            if (!IsPostBack)
            {
                this.navigation1.Routes.Add("库存收款分配", redirectUrl);
                this.navigation1.Routes.Add("库存收款分配修改", string.Empty);

                int receivableAllotId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out receivableAllotId) || receivableAllotId <= 0)
                    Response.Redirect(redirectUrl);

                this.hidid.Value = receivableAllotId.ToString();

                NFMT.Funds.BLL.StcokReceivableBLL stcokReceivableBLL = new NFMT.Funds.BLL.StcokReceivableBLL();
                NFMT.Funds.BLL.ContractReceivableBLL contractReceivableBLL = new NFMT.Funds.BLL.ContractReceivableBLL();
                NFMT.Common.ResultModel result = stcokReceivableBLL.GetStockId(user, receivableAllotId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                int stockId = (int)result.ReturnValue;
                this.hidStockId.Value = stockId.ToString();

                //库存信息
                NFMT.WareHouse.BLL.StockBLL stockBLL = new NFMT.WareHouse.BLL.StockBLL();
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

                    NFMT.Funds.BLL.ReceivableAllotBLL receivableAllotBLL = new NFMT.Funds.BLL.ReceivableAllotBLL();
                    result = receivableAllotBLL.GetStockAllotAmount(user, stockId);
                    if (result.ResultStatus != 0)
                        Response.Redirect(redirectUrl);

                    this.spanAllotAmount.InnerText = result.ReturnValue == null ? "" : result.ReturnValue.ToString();

                    //分配信息
                    result = receivableAllotBLL.Get(user, receivableAllotId);
                    if (result.ResultStatus != 0)
                        Response.Redirect(redirectUrl);

                    NFMT.Funds.Model.ReceivableAllot receivableAllot = result.ReturnValue as NFMT.Funds.Model.ReceivableAllot;
                    if (receivableAllot != null)
                    {
                        this.txbMemo.Value = receivableAllot.AllotDesc;
                        this.hidcurrencyId.Value = receivableAllot.CurrencyId.ToString();
                        this.hidAllotFrom.Value = receivableAllot.AllotFrom.ToString();

                        if (receivableAllot.AllotFrom == NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.分配来源)["Receivable"].StyleDetailId)
                        {
                            //获取收款登记相关
                            result = stcokReceivableBLL.GetReceIds(user, receivableAllotId);
                            if (result.ResultStatus != 0)
                                Response.Redirect(redirectUrl);

                            this.hidReceIds.Value = result.ReturnValue.ToString();

                            result = stcokReceivableBLL.GetRowsDetail(user, receivableAllotId);
                            if (result.ResultStatus != 0)
                                Response.Redirect(redirectUrl);

                            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

                            this.hidRowDetail.Value = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                        }
                        else if (receivableAllot.AllotFrom == NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.分配来源)["CorpReceivable"].StyleDetailId)
                        {
                            //获取公司收款相关
                            result = contractReceivableBLL.GetCorpRefIds(user, receivableAllotId);
                            if (result.ResultStatus != 0)
                                Response.Redirect(redirectUrl);

                            this.hidCorpRefIds.Value = result.ReturnValue.ToString();

                            result = contractReceivableBLL.GetRowsDetailByCorp(user, receivableAllotId);
                            if (result.ResultStatus != 0)
                                Response.Redirect(redirectUrl);

                            System.Data.DataTable dtCorp = result.ReturnValue as System.Data.DataTable;

                            this.hidRowDetailCorp.Value = Newtonsoft.Json.JsonConvert.SerializeObject(dtCorp);
                        }
                        else if (receivableAllot.AllotFrom == NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.分配来源)["ContractReceivable"].StyleDetailId)
                        {
                            //获取合约收款相关
                            result = stcokReceivableBLL.GetReceIdsForContract(user, receivableAllotId);
                            if (result.ResultStatus != 0)
                                Response.Redirect(redirectUrl);

                            this.hidReceIdsForContract.Value = result.ReturnValue.ToString();

                            result = stcokReceivableBLL.GetRowsDetailForContract(user, receivableAllotId);
                            if (result.ResultStatus != 0)
                                Response.Redirect(redirectUrl);

                            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

                            this.hidRowDetailForContract.Value = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                        }
                    }
                }
            }
        }
    }
}