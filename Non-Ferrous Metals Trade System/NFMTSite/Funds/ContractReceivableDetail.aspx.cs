using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class ContractReceivableDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = string.Format("{0}Funds/ContractReceivableList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 57, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成, NFMT.Common.OperateEnum.执行完成撤销 });

                this.navigation1.Routes.Add("合约收款分配", redirectUrl);
                this.navigation1.Routes.Add("合约收款分配明细", string.Empty);

                int receivableAllotId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out receivableAllotId) || receivableAllotId <= 0)
                    Response.Redirect(redirectUrl);

                this.hidid.Value = receivableAllotId.ToString();

                NFMT.Funds.BLL.ReceivableAllotBLL receivableAllotBLL = new NFMT.Funds.BLL.ReceivableAllotBLL();
                NFMT.Common.ResultModel result = receivableAllotBLL.Get(user, receivableAllotId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Funds.Model.ReceivableAllot receivableAllot = result.ReturnValue as NFMT.Funds.Model.ReceivableAllot;
                if (receivableAllot != null)
                {
                    NFMT.Data.Model.Currency currency = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(a => a.CurrencyId == receivableAllot.CurrencyId);
                    if (currency != null)
                        this.spanAllotBala.InnerText = receivableAllot.AllotBala.ToString() + currency.CurrencyName;

                    this.spanAllotDesc.InnerText = receivableAllot.AllotDesc;

                    NFMT.User.Model.Employee employee = NFMT.User.UserProvider.Employees.SingleOrDefault(a => a.EmpId == receivableAllot.EmpId);
                    if (employee != null)
                        this.spanEmpId.InnerText = employee.Name;

                    this.spanAllotTime.InnerText = receivableAllot.AllotTime.ToString();

                    string json = serializer.Serialize(receivableAllot);
                    this.hidModel.Value = json;
                }
            }
        }
    }
}