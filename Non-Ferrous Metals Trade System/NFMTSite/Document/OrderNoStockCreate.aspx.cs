using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Document
{
    public partial class OrderNoStockCreate : System.Web.UI.Page
    {
        public NFMT.Contract.Model.ContractSub curSub = null;
        public NFMT.Contract.Model.Contract curContract = null;
        public NFMT.Data.Model.Currency curCurrency = null;
        public int curDeptId = 0;
        public string curMUName = string.Empty;
        public NFMT.Data.Model.Asset curAsset = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.navigation1.Routes.Add("制单指令列表", "OrderList.aspx");
                this.navigation1.Routes.Add("可制单合约列表", "OrderContractList.aspx");
                this.navigation1.Routes.Add("制单指令新增", string.Empty);

                string redirectUrl = "OrderList.aspx";

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(redirectUrl);

                int subId = 0;
                if (!int.TryParse(Request.QueryString["id"], out subId))
                    Response.Redirect(redirectUrl);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                this.curDeptId = user.DeptId;

                NFMT.Contract.BLL.ContractSubBLL subBll = new NFMT.Contract.BLL.ContractSubBLL();
                NFMT.Common.ResultModel result = subBll.Get(user, subId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.ContractId == 0)
                    Response.Redirect(redirectUrl);
                this.curSub = sub;

                NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
                result = bll.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.Contract con = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (con == null || con.ContractId == 0)
                    Response.Redirect(redirectUrl);
                this.curContract = con;

                NFMT.Data.Model.Asset ass = NFMT.Data.BasicDataProvider.Assets.First(temp => temp.AssetId == con.AssetId);
                this.curAsset = ass;

                NFMT.Data.Model.MeasureUnit muContract = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == con.UnitId);
                this.curMUName = muContract.MUName;

                NFMT.Data.Model.Currency cur = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(temp => temp.CurrencyId == sub.SettleCurrency);
                this.curCurrency = cur;

                NFMT.Data.Model.MeasureUnit muSub = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == sub.UnitId);

                this.contractExpander1.CurContract = this.curContract;
                this.contractExpander1.CurContractSub = this.curSub;
                this.contractExpander1.RedirectUrl = redirectUrl;    
            }
        }
    }
}