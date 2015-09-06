using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Contract
{
    public partial class ContractInCreateByStocks : System.Web.UI.Page
    {
        public NFMT.Common.UserModel user;
        public NFMT.Contract.BLL.ContractBLL contractBLL = new NFMT.Contract.BLL.ContractBLL();
        public string stockLogIds = string.Empty;
        public int curAssetId = 0;
        public int curTradeBorder = 0;
        public decimal sumGrossAmount = 0;
        public NFMT.Data.Model.Asset curAsset = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 37, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

            user = Utility.UserUtility.CurrentUser;
            string redirectUrl = "SubStockInList.aspx";

            this.hidTradeDirection.Value = ((int)NFMT.Data.StyleEnum.TradeDirection).ToString();
            this.hidTradeBorder.Value = ((int)NFMT.Data.StyleEnum.TradeBorder).ToString();
            this.hidContractLimit.Value = ((int)NFMT.Data.StyleEnum.ContractLimit).ToString();
            this.hidMarginMode.Value = ((int)NFMT.Data.StyleEnum.MarginMode).ToString();
            this.hidValueRateType.Value = ((int)NFMT.Data.StyleEnum.ValueRateType).ToString();
            this.hidDiscountBase.Value = ((int)NFMT.Data.StyleEnum.DiscountBase).ToString();
            this.hidWhoDoPrice.Value = ((int)NFMT.Data.StyleEnum.WhoDoPrice).ToString();
            this.hidSummaryPrice.Value = ((int)NFMT.Data.StyleEnum.SummaryPrice).ToString();

            this.navigation1.Routes.Add("合约列表", "ContractList.aspx");
            this.navigation1.Routes.Add("库存列表", redirectUrl);
            this.navigation1.Routes.Add("合约添加", string.Empty);

            stockLogIds = Request.QueryString["lgs"];
            //库存验证
            if (string.IsNullOrEmpty(stockLogIds))
                Utility.JsUtility.WarmAlert(this, "未选中任务库存", redirectUrl);

            string[] ids = stockLogIds.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
            if (ids== null || ids.Length == 0)
                Utility.JsUtility.WarmAlert(this, "未选中任务库存", redirectUrl);

            int assetId = 0;
            int logDirection = (int)NFMT.WareHouse.LogDirectionEnum.In;//入库流水
            int customsType = 0;           

            NFMT.WareHouse.BLL.StockLogBLL stockLogBLL = new NFMT.WareHouse.BLL.StockLogBLL();
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            foreach (string id in ids)
            {
                int stockLogId = 0;
                if (!int.TryParse(id, out stockLogId) || stockLogId <= 0)
                    Utility.JsUtility.WarmAlert(this, "库存序号错误", redirectUrl);

                result = stockLogBLL.Get(user, stockLogId);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this, result.Message, redirectUrl);

                NFMT.WareHouse.Model.StockLog stockLog = result.ReturnValue as NFMT.WareHouse.Model.StockLog;
                if (stockLog == null || stockLog.StockLogId <= 0)
                    Utility.JsUtility.WarmAlert(this, "库存流水不存在", redirectUrl);

                if (assetId == 0)
                {
                    assetId = stockLog.AssetId;
                    this.curAssetId = assetId;
                }
                if (customsType == 0)
                {
                    customsType = stockLog.CustomsType;
                    if (customsType == (int)NFMT.WareHouse.CustomTypeEnum.关内)
                        curTradeBorder = (int)NFMT.Contract.TradeBorderEnum.内贸;
                    else
                        curTradeBorder = (int)NFMT.Contract.TradeBorderEnum.外贸;
                }

                //比较是否同一品种
                if (assetId != stockLog.AssetId)
                    Utility.JsUtility.WarmAlert(this, "选中库存非同一品种", redirectUrl);

                //比较是否为入库流水
                if (stockLog.LogDirection != logDirection)
                    Utility.JsUtility.WarmAlert(this, "选中库存流水非入库流水，", redirectUrl);

                //比对关内外库存
                if (stockLog.CustomsType != customsType)
                    Utility.JsUtility.WarmAlert(this,"选中库存关境状态不相同",redirectUrl);

                sumGrossAmount += stockLog.GrossAmount;
            }

            NFMT.Data.Model.Asset asset = NFMT.Data.BasicDataProvider.Assets.FirstOrDefault(temp => temp.AssetId == assetId);
            this.curAsset = asset;
        }
    }
}