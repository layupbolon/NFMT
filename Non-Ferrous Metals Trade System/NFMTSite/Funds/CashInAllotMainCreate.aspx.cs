using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class CashInAllotMainCreate : System.Web.UI.Page
    {
        public NFMT.Funds.Model.CashIn cashIn = new NFMT.Funds.Model.CashIn();
        public string InCorpName = string.Empty;
        public string InBankName = string.Empty;
        public string InBankAccountNo = string.Empty;
        public string CurrencyName = string.Empty;
        public decimal CanAllotBala = 0;
        public string sIIds = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = "CashInAllotMainReadyCashInList.aspx";

            if (!IsPostBack)
            {
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 122, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("收款分配", "CashInAllotMainList.aspx");
                this.navigation1.Routes.Add("收款登记列表", redirectUrl);
                this.navigation1.Routes.Add("收款分配新增", string.Empty);

                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                int cashInId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out cashInId) || cashInId <= 0)
                    Utility.JsUtility.WarmAlert(this.Page, "参数错误", redirectUrl);

                //获取收款
                NFMT.Funds.BLL.CashInBLL cashInBLL = new NFMT.Funds.BLL.CashInBLL();
                result = cashInBLL.Get(user, cashInId);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                cashIn = result.ReturnValue as NFMT.Funds.Model.CashIn;
                if (cashIn == null)
                    Utility.JsUtility.WarmAlert(this.Page, "获取收款出错", redirectUrl);

                //初始化
                InitCashInInfo(cashIn, redirectUrl);

                NFMT.Funds.BLL.CashInAllotBLL cashInAllotBLL = new NFMT.Funds.BLL.CashInAllotBLL();
                result = cashInAllotBLL.GetCanAllotBala(user, cashInId, false);
                if (result.ResultStatus != 0)
                    Utility.JsUtility.WarmAlert(this.Page, result.Message, redirectUrl);

                CanAllotBala = (decimal)result.ReturnValue;

                NFMT.Invoice.BLL.SIBLL sIBLL = new NFMT.Invoice.BLL.SIBLL();
                result = sIBLL.GetSIIdsByCustomCorpId(user, cashIn.PayCorpId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                sIIds = result.ReturnValue != null ? result.ReturnValue.ToString() : string.Empty;
            }
        }

        private void InitCashInInfo(NFMT.Funds.Model.CashIn cashIn,string redirectUrl)
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