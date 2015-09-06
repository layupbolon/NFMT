using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class CashInAllotMainDetail : System.Web.UI.Page
    {
        public NFMT.Funds.Model.CashInAllot cashInAllot;
        public NFMT.Funds.Model.CashInCorp cashInCorp;
        public NFMT.Funds.Model.CashInContract cashInContract;
        public List<NFMT.Funds.Model.CashInStcok> cashInStocks;
        public string contractGirdInfo = string.Empty;
        public string stockGridInfo = string.Empty;
        public NFMT.Funds.Model.CashIn cashIn;
        public string InCorpName = string.Empty;
        public string InBankName = string.Empty;
        public string InBankAccountNo = string.Empty;
        public string CurrencyName = string.Empty;
        public string upSIIds = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = "CashInAllotMainList.aspx";

            if (!IsPostBack)
            {
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 122, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成, NFMT.Common.OperateEnum.执行完成撤销 });

                this.navigation1.Routes.Add("收款分配", redirectUrl);
                this.navigation1.Routes.Add("收款分配明细", string.Empty);

                int allotId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out allotId) || allotId <= 0)
                    this.WarmAlert("序号错误", redirectUrl);

                //获取收款分配
                NFMT.Funds.BLL.CashInAllotBLL cashInAllotBLL = new NFMT.Funds.BLL.CashInAllotBLL();
                result = cashInAllotBLL.Get(user, allotId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                cashInAllot = result.ReturnValue as NFMT.Funds.Model.CashInAllot;
                if (cashInAllot == null)
                    this.WarmAlert("获取收款分配失败", redirectUrl);

                NFMT.Common.StatusEnum status = NFMT.Common.StatusEnum.已生效;
                if (cashInAllot.AllotStatus == NFMT.Common.StatusEnum.已完成)
                    status = NFMT.Common.StatusEnum.已完成;
                else if(cashInAllot.AllotStatus == NFMT.Common.StatusEnum.已作废)
                    status = NFMT.Common.StatusEnum.已作废;
                else if (cashInAllot.AllotStatus == NFMT.Common.StatusEnum.已关闭)
                    status = NFMT.Common.StatusEnum.已关闭;

                //获取收款分配至公司
                NFMT.Funds.BLL.CashInCorpBLL cashInCorpBLL = new NFMT.Funds.BLL.CashInCorpBLL();
                result = cashInCorpBLL.Load(user, allotId, status);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                List<NFMT.Funds.Model.CashInCorp> cashInCorps = result.ReturnValue as List<NFMT.Funds.Model.CashInCorp>;
                if (cashInCorps == null || !cashInCorps.Any())
                    this.WarmAlert("获取收款分配至公司失败", redirectUrl);

                cashInCorp = cashInCorps.FirstOrDefault();

                //获取收款分配至合约
                NFMT.Funds.BLL.CashInContractBLL cashInContractBLL = new NFMT.Funds.BLL.CashInContractBLL();
                result = cashInContractBLL.GetByAllot(user, allotId, status);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                cashInContract = result.ReturnValue as NFMT.Funds.Model.CashInContract;
                if (cashInContract == null)
                    this.WarmAlert("获取收款分配至合约失败", redirectUrl);

                //获取收款分配至库存
                NFMT.Funds.BLL.CashInStcokBLL cashInStockBLL = new NFMT.Funds.BLL.CashInStcokBLL();
                result = cashInStockBLL.LoadByAllot(user, allotId, status);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                cashInStocks = result.ReturnValue as List<NFMT.Funds.Model.CashInStcok>;
                if (cashInStocks == null || !cashInStocks.Any())
                    this.WarmAlert("获取收款分配至库存失败", redirectUrl);

                //获取合约列表
                NFMT.Contract.BLL.ContractSubBLL contractSubBLL = new NFMT.Contract.BLL.ContractSubBLL();
                NFMT.Common.SelectModel select = contractSubBLL.GetListSelect(1, 200, string.Empty, cashInContract.SubContractId);
                result = contractSubBLL.Load(user, select);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                int totalRows = result.AffectCount;
                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

                this.contractGirdInfo = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                //获取库存列表
                result = cashInStockBLL.GetStockInfoByAlotId(user, allotId, status);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                totalRows = result.AffectCount;
                dt = result.ReturnValue as System.Data.DataTable;

                this.stockGridInfo = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                //获取收款登记信息
                NFMT.Funds.BLL.CashInBLL cashInBLL = new NFMT.Funds.BLL.CashInBLL();
                result = cashInBLL.Get(user, cashInCorp.CashInId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                cashIn = result.ReturnValue as NFMT.Funds.Model.CashIn;
                if (cashIn == null)
                    this.WarmAlert("获取收款登记失败", redirectUrl);

                //初始化
                InitCashInInfo(cashIn, redirectUrl);

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(cashInAllot);
                this.hidModel.Value = json;

                NFMT.Funds.BLL.CashInInvoiceBLL cashInInvoiceBLL = new NFMT.Funds.BLL.CashInInvoiceBLL();
                result = cashInInvoiceBLL.GetSIIdsbyAllotId(user, allotId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                upSIIds = result.ReturnValue != null ? result.ReturnValue.ToString() : string.Empty;
            }
        }

        private void InitCashInInfo(NFMT.Funds.Model.CashIn cashIn, string redirectUrl)
        {
            NFMT.User.Model.Corporation inCorp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == cashIn.CashInCorpId);
            if (inCorp == null)
                Utility.JsUtility.WarmAlert(this.Page, "获取收款公司失败", redirectUrl);
            InCorpName = inCorp.CorpName;

            NFMT.Data.Model.Bank inBank = NFMT.Data.BasicDataProvider.Banks.SingleOrDefault(a => a.BankId == cashIn.CashInBank);
            if (inBank == null)
                Utility.JsUtility.WarmAlert(this.Page, "获取收款银行失败", redirectUrl);
            InBankName = inBank.BankName;

            NFMT.Data.Model.BankAccount inBankAccount = NFMT.Data.BasicDataProvider.BankAccounts.SingleOrDefault(a => a.BankAccId == cashIn.CashInAccoontId);
            if (inBankAccount == null)
                Utility.JsUtility.WarmAlert(this.Page, "获取收款银行账号失败", redirectUrl);
            InBankAccountNo = inBankAccount.AccountNo;

            NFMT.Data.Model.Currency currency = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(a => a.CurrencyId == cashIn.CurrencyId);
            if (currency == null)
                Utility.JsUtility.WarmAlert(this.Page, "获取币种失败", redirectUrl);
            CurrencyName = currency.CurrencyName;
        }
    }
}