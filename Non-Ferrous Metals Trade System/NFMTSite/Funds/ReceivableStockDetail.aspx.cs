using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class ReceivableStockDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = string.Format("{0}Funds/ReceivableStockList.aspx", NFMT.Common.DefaultValue.NftmSiteName);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            if (!IsPostBack)
            {
                this.navigation1.Routes.Add("库存收款分配", redirectUrl);
                this.navigation1.Routes.Add("库存收款分配明细", string.Empty);

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
                    NFMT.Data.Model.Currency cyrrency = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(a => a.CurrencyId == receivableAllot.CurrencyId);
                    if (cyrrency != null)
                        this.spanAllotBala.InnerText = receivableAllot.AllotBala.ToString() + cyrrency.CurrencyName;

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