using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockReceiptCreate : System.Web.UI.Page
    {
        public NFMT.Contract.Model.Contract curContract = null;
        public NFMT.Contract.Model.ContractSub curContractSub = null;
        public string currencyName = string.Empty;
        public string MUName = string.Empty;
        public string curReceiptingJson = string.Empty;
        public string tradeDirString = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = "StockReceiptList.aspx";

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 88, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                //获取合约与子合约
                int subId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out subId))
                    Response.Redirect(redirectUrl);

                NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBLL.Get(user, subId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                    Response.Redirect(redirectUrl);

                this.curContractSub = sub;

                //合约币种
                NFMT.Data.Model.Currency currency = NFMT.Data.BasicDataProvider.Currencies.FirstOrDefault(temp => temp.CurrencyId == sub.SettleCurrency);
                if (currency != null && currency.CurrencyId > 0)
                    this.currencyName = currency.CurrencyName;
                //合约计量单位
                NFMT.Data.Model.MeasureUnit mu = NFMT.Data.BasicDataProvider.MeasureUnits.FirstOrDefault(temp => temp.MUId == sub.UnitId);
                if (mu != null && mu.MUId > 0)
                    this.MUName = mu.MUName;

                NFMT.Contract.BLL.ContractBLL conBLL = new NFMT.Contract.BLL.ContractBLL();
                result = conBLL.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                //主合约
                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                    Response.Redirect(redirectUrl);

                this.curContract = contract;

                this.contractExpander1.CurContract = contract;
                this.contractExpander1.CurContractSub = sub;
                this.contractExpander1.RedirectUrl = redirectUrl;

                this.navigation1.Routes.Add("回执列表", redirectUrl);
                this.navigation1.Routes.Add("合约列表", "StockReceiptContractList.aspx");
                this.navigation1.Routes.Add("回执新增", string.Empty);

                tradeDirString = ((NFMT.Contract.TradeDirectionEnum)contract.TradeDirection).ToString();
                
                //ReceiptingStockList Json
                NFMT.WareHouse.BLL.StockReceiptBLL bll = new NFMT.WareHouse.BLL.StockReceiptBLL();

                int receiptId = 0;
                string sids = "0";
                NFMT.Common.SelectModel select = bll.GetReceiptingStockListSelect(1, 100, "sto.StockId desc", sub.SubId, receiptId, sids);
                result = bll.Load(user, select);
                int totalRows = result.AffectCount;
                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                this.curReceiptingJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());
                
            }
        }
        
    }
}