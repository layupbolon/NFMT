using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockInContractCreate : System.Web.UI.Page
    {
        public NFMT.WareHouse.Model.StockIn curStockIn = null;
        public string curContractJson = string.Empty;
        public int curSubContractId = 0;
        public string curBrandName = string.Empty;

        public string CustomTypeName = string.Empty;
        public string CorpName = string.Empty;
        public string AssetName = string.Empty;
        public string MUName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 41, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.查询 });

                string redirectUrl = "StockInContractList.aspx";

                this.navigation1.Routes.Add("入库分配列表", "StockInContractList.aspx");
                this.navigation1.Routes.Add("合约列表", "StockInNoContractList.aspx");
                this.navigation1.Routes.Add("入库分配", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(redirectUrl);

                int stockInId = 0;
                if (!int.TryParse(Request.QueryString["id"], out stockInId))
                    Response.Redirect(redirectUrl);

                //获取入库登记 
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                NFMT.WareHouse.BLL.StockInBLL stockInBLL = new NFMT.WareHouse.BLL.StockInBLL();
                result = stockInBLL.Get(user, stockInId);

                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.WareHouse.Model.StockIn stockIn = result.ReturnValue as NFMT.WareHouse.Model.StockIn;
                if (stockIn == null || stockIn.StockInId <= 0)
                    Response.Redirect(redirectUrl);

                this.curStockIn = stockIn;

                NFMT.WareHouse.CustomTypeEnum customsType = (NFMT.WareHouse.CustomTypeEnum)stockIn.CustomType;
                this.CustomTypeName = customsType.ToString("F");

                NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == stockIn.CorpId);
                if (corp != null)
                    this.CorpName = corp.CorpName;

                NFMT.Data.Model.Asset asset = NFMT.Data.BasicDataProvider.Assets.FirstOrDefault(temp => temp.AssetId == stockIn.AssetId);
                if (asset != null)
                    this.AssetName = asset.AssetName;

                NFMT.Data.Model.MeasureUnit unit = NFMT.Data.BasicDataProvider.MeasureUnits.FirstOrDefault(temp => temp.MUId == stockIn.UintId);
                if (unit != null)
                    this.MUName = unit.MUName;

                NFMT.WareHouse.BLL.ContractStockIn_BLL bll = new NFMT.WareHouse.BLL.ContractStockIn_BLL();
                NFMT.Common.SelectModel select = bll.GetContractSelect(1, 100, "si.StockInId desc",stockIn.StockInId);
                result = bll.Load(user, select);
                int totalRows = result.AffectCount;
                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                this.curContractJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                if (result.AffectCount > 0)
                {
                    //合约存在，即修改状态
                    System.Data.DataRow dr = dt.Rows[0];
                    if (dr["ContractId"] == DBNull.Value || !int.TryParse(dr["ContractId"].ToString(), out this.curSubContractId))
                        Response.Redirect(redirectUrl);
                }

                NFMT.Data.Model.Brand brand =  NFMT.Data.BasicDataProvider.Brands.FirstOrDefault(temp => temp.BrandId == this.curStockIn.BrandId);
                if (brand != null && brand.BrandId > 0)
                    this.curBrandName = brand.BrandName;
            }
        }
    }
}