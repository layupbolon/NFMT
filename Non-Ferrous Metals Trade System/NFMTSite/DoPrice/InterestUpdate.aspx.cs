using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.DoPrice
{
    public partial class InterestUpdate : System.Web.UI.Page
    {
        public NFMT.DoPrice.Model.Interest curInterest = null;
        public NFMT.Data.Model.Currency curCurrency = null;
        public NFMT.Data.Model.MeasureUnit curMeasureUnit = null;
        public NFMT.Contract.Model.ContractSub curSub = null;
        public NFMT.Data.Model.Asset curAsset = null;
        public NFMT.Contract.TradeDirectionEnum curTradeDirection =  NFMT.Contract.TradeDirectionEnum.Buy;

        public NFMT.Contract.Model.SubCorporationDetail curInCorp = null;
        public NFMT.Contract.Model.SubCorporationDetail curOutCorp = null;

        public string curJson = string.Empty;
        public string othJson = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 120, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                string redirectUrl = "InterestList.aspx";
                this.navigation1.Routes.Add("利息结算列表", redirectUrl);
                this.navigation1.Routes.Add("利息结算修改", string.Empty);

                int interestId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out interestId) || interestId <= 0)
                    this.Page.WarmAlert("参数错误", redirectUrl);

                //获取当前结息
                NFMT.DoPrice.BLL.InterestBLL interestBLL = new NFMT.DoPrice.BLL.InterestBLL();
                result = interestBLL.Get(user, interestId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                NFMT.DoPrice.Model.Interest interest = result.ReturnValue as NFMT.DoPrice.Model.Interest;
                if (interest == null || interest.Id <= 0)
                    this.WarmAlert("利息结算不存在",redirectUrl);

                this.curInterest = interest;

                //获取币种
                NFMT.Data.Model.Currency currency = NFMT.Data.BasicDataProvider.Currencies.FirstOrDefault(temp => temp.CurrencyId == interest.CurrencyId);
                if (currency == null || currency.CurrencyId <= 0)
                    this.WarmAlert("币种不存在", redirectUrl);

                this.curCurrency = currency;

                //获取重量单位
                NFMT.Data.Model.MeasureUnit measureUnit = NFMT.Data.BasicDataProvider.MeasureUnits.FirstOrDefault(temp => temp.MUId == interest.Unit);
                if (measureUnit == null || measureUnit.MUId <= 0)
                    this.WarmAlert("重量单位不存在", redirectUrl);

                this.curMeasureUnit = measureUnit;

                //获取子合约
                NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBLL.Get(user,interest.SubContractId);
                if(result.ResultStatus!=0)
                    this.WarmAlert("子合约不存在",redirectUrl);
                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                    this.WarmAlert("子合约不存在",redirectUrl);

                this.curSub = sub;

                //品种
                NFMT.Data.Model.Asset asset = NFMT.Data.BasicDataProvider.Assets.FirstOrDefault(temp => temp.AssetId == sub.AssetId);
                if (asset == null || asset.AssetId <= 0)
                    this.WarmAlert("品种获取失败",redirectUrl);

                this.curAsset = asset;

                NFMT.Contract.TradeDirectionEnum tradeDirection = (NFMT.Contract.TradeDirectionEnum)sub.TradeDirection;
                this.curTradeDirection = tradeDirection;

                NFMT.Contract.BLL.SubCorporationDetailBLL corpBLL = new NFMT.Contract.BLL.SubCorporationDetailBLL();
                //我方公司
                result = corpBLL.Load(user, sub.SubId, true);
                if (result.ResultStatus != 0)
                    this.WarmAlert("我方公司获取失败",redirectUrl);

                List<NFMT.Contract.Model.SubCorporationDetail> inCorps = result.ReturnValue as List<NFMT.Contract.Model.SubCorporationDetail>;
                if (inCorps == null || inCorps.Count == 0)
                    this.WarmAlert("我方公司获取失败",redirectUrl);

                NFMT.Contract.Model.SubCorporationDetail inCorp = inCorps.FirstOrDefault(temp => temp.IsDefaultCorp);
                if(inCorp== null || inCorp.CorpId<=0)
                    this.WarmAlert("我方公司获取失败", redirectUrl);

                this.curInCorp = inCorp;

                //对方公司
                result = corpBLL.Load(user, sub.SubId, false);
                if (result.ResultStatus != 0)
                    this.WarmAlert("对方公司获取失败", redirectUrl);

                List<NFMT.Contract.Model.SubCorporationDetail> outCorps = result.ReturnValue as List<NFMT.Contract.Model.SubCorporationDetail>;
                if (outCorps == null || outCorps.Count == 0)
                    this.WarmAlert("对方公司获取失败",redirectUrl);

                NFMT.Contract.Model.SubCorporationDetail outCorp = outCorps.FirstOrDefault(temp => temp.IsDefaultCorp);
                if(outCorp == null || outCorp.CorpId<=0)
                    this.WarmAlert("对方公司获取失败", redirectUrl);

                this.curOutCorp = outCorp;

                //json
                int pageIndex = 1, pageSize = 100;
                string orderStr = string.Empty, whereStr = string.Empty;
                NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

                select = interestBLL.GetCurDetailsSelect(pageIndex, pageSize, orderStr,sub.SubId,interest.InterestId);
                result = interestBLL.Load(user, select, NFMT.Common.DefaultValue.ClearAuth);
                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                this.curJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                select = interestBLL.GetOthDetailsSelect(pageIndex, pageSize, orderStr, sub.SubId, interest.InterestId);
                result = interestBLL.Load(user, select, NFMT.Common.DefaultValue.ClearAuth);
                dt = result.ReturnValue as System.Data.DataTable;
                this.othJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());
            }
        }
    }
}