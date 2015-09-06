using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Contract
{
    public partial class ContractOutCreate : System.Web.UI.Page
    {
        public NFMT.Common.UserModel user;
        public NFMT.Contract.BLL.ContractBLL contractBLL = new NFMT.Contract.BLL.ContractBLL();
        public string stockIds = string.Empty;
        public int curTradeBorder = 0;
        public decimal sumGrossAmount = 0;
        public NFMT.Data.Model.Asset curAsset = null;
        public string SelectedJson = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 37, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

            user = Utility.UserUtility.CurrentUser;
            string redirectUrl = "ContractOutStockList.aspx";

            this.navigation1.Routes.Add("合约列表", "ContractList.aspx");
            this.navigation1.Routes.Add("库存列表", redirectUrl);
            this.navigation1.Routes.Add("合约添加", string.Empty);

            stockIds = Request.QueryString["ids"];
            //库存验证
            if (string.IsNullOrEmpty(stockIds))
                Utility.JsUtility.WarmAlert(this, "未选中任务库存", redirectUrl);

            string[] ids = stockIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (ids == null || ids.Length == 0)
                Utility.JsUtility.WarmAlert(this, "未选中任务库存", redirectUrl);

            int assetId = 0;
            int customsType = 0;

            NFMT.WareHouse.BLL.StockBLL stockBLL = new NFMT.WareHouse.BLL.StockBLL();
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            NFMT.WareHouse.BLL.StockExclusiveBLL stockExclusiveBLL = new NFMT.WareHouse.BLL.StockExclusiveBLL();

            foreach (string id in ids)
            {
                int stockId = 0;
                if (!int.TryParse(id, out stockId) || stockId <= 0)
                    Utility.JsUtility.WarmAlert(this, "库存序号错误", redirectUrl);

                result = stockBLL.Get(user, stockId);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this, result.Message, redirectUrl);

                NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                if (stock == null || stock.StockId <= 0)
                    Utility.JsUtility.WarmAlert(this, "库存不存在", redirectUrl);

                if (assetId == 0)
                    assetId = stock.AssetId;
                
                if (customsType == 0)
                {
                    customsType = stock.CustomsType;
                    if (customsType == (int)NFMT.WareHouse.CustomTypeEnum.关内)
                        curTradeBorder = (int)NFMT.Contract.TradeBorderEnum.内贸;
                    else
                        curTradeBorder = (int)NFMT.Contract.TradeBorderEnum.外贸;
                }

                //比较是否同一品种
                if (assetId != stock.AssetId)
                    Utility.JsUtility.WarmAlert(this, "选中库存非同一品种", redirectUrl);                

                //比对关内外库存
                if (stock.CustomsType != customsType)
                    Utility.JsUtility.WarmAlert(this, "选中库存关境状态不相同", redirectUrl);

                result = stockExclusiveBLL.LoadByStockId(user, stock.StockId);
                if(result.ResultStatus!=0)
                    Utility.JsUtility.WarmAlert(this, result.Message, redirectUrl);

                List<NFMT.WareHouse.Model.StockExclusive> excs = result.ReturnValue as List<NFMT.WareHouse.Model.StockExclusive>;
                if (excs == null)
                    Utility.JsUtility.WarmAlert(this,"排他表获取失败", redirectUrl);

                decimal excAmount = excs.Sum(temp => temp.ExclusiveAmount);
                sumGrossAmount += (stock.CurNetAmount - excAmount);
            }

            NFMT.Data.Model.Asset asset = NFMT.Data.BasicDataProvider.Assets.FirstOrDefault(temp => temp.AssetId == assetId);
            this.curAsset = asset;

            //
            int pageIndex = 1;
            int pageSize = 100;
            string orderStr = string.Empty;
            NFMT.WareHouse.BLL.StockLogBLL stockLogBLL = new NFMT.WareHouse.BLL.StockLogBLL();
            NFMT.Common.SelectModel select = stockLogBLL.GetContractOutStockSelect(pageIndex, pageSize, orderStr, string.Empty, NFMT.Common.DefaultValue.DefaultTime, NFMT.Common.DefaultValue.DefaultTime, stockIds);
            result = stockLogBLL.Load(user, select, NFMT.Common.DefaultValue.ClearAuth);
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            this.SelectedJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

        }
    }
}