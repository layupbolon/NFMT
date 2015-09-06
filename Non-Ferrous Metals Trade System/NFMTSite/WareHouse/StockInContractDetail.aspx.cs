using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockInContractDetail : System.Web.UI.Page
    {
        public NFMT.WareHouse.Model.StockIn curStockIn = null;
        public string curContractJson = string.Empty;
        public NFMT.WareHouse.Model.ContractStockIn curContractStockIn = null;

        public string CustomTypeName = string.Empty;
        public string CorpName = string.Empty;
        public string AssetName = string.Empty;
        public string MUName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 41, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返 });

                string redirectUrl = "StockInContractList.aspx";

                this.navigation1.Routes.Add("入库分配列表", "StockInContractList.aspx");
                this.navigation1.Routes.Add("入库分配明细", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(redirectUrl);

                int refId = 0;
                if (!int.TryParse(Request.QueryString["id"], out refId))
                    Response.Redirect(redirectUrl);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
                //获取入库分配
                NFMT.WareHouse.BLL.ContractStockIn_BLL contractStockInBLL = new NFMT.WareHouse.BLL.ContractStockIn_BLL();
                result = contractStockInBLL.Get(user, refId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.WareHouse.Model.ContractStockIn contractStockIn = result.ReturnValue as NFMT.WareHouse.Model.ContractStockIn;
                if (contractStockIn == null || contractStockIn.RefId <= 0)
                    Response.Redirect(redirectUrl);

                this.curContractStockIn = contractStockIn;

                //获取入库登记
                NFMT.WareHouse.BLL.StockInBLL stockInBLL = new NFMT.WareHouse.BLL.StockInBLL();
                result = stockInBLL.Get(user, contractStockIn.StockInId);

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
                NFMT.Common.SelectModel select = bll.GetContractSelect(1, 100, "si.StockInId desc", stockIn.StockInId);
                result = bll.Load(user, select);
                int totalRows = result.AffectCount;
                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                this.curContractJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(contractStockIn);
                this.hidModel.Value = json;
            }
        }
    }
}