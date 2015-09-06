using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class PledgeDetail : System.Web.UI.Page
    {
        public int pledgeId = 0;
        public NFMT.WareHouse.Model.Pledge pledge = new NFMT.WareHouse.Model.Pledge();

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string redirectUrl = string.Format("{0}WareHouse/PledgeList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 48, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成, NFMT.Common.OperateEnum.执行完成撤销 });

                this.navigation1.Routes.Add("质押", redirectUrl);
                this.navigation1.Routes.Add("质押明细", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out pledgeId) || pledgeId <= 0)
                    Response.Redirect(redirectUrl);

                this.hidid.Value = pledgeId.ToString();

                NFMT.WareHouse.BLL.PledgeBLL pledgeBLL = new NFMT.WareHouse.BLL.PledgeBLL();
                NFMT.Common.ResultModel result = pledgeBLL.Get(user, pledgeId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                pledge = result.ReturnValue as NFMT.WareHouse.Model.Pledge;
                if (pledge == null)
                    Response.Redirect(redirectUrl);

                NFMT.Data.Model.Bank bank = NFMT.Data.BasicDataProvider.Banks.SingleOrDefault(a => a.BankId == pledge.PledgeBank);
                if (bank == null)
                    Response.Redirect(redirectUrl);
                this.ddlPledgeBank.InnerText = bank.BankName;

                NFMT.User.Model.Employee emp = NFMT.User.UserProvider.Employees.SingleOrDefault(a => a.EmpId == pledge.Pledger);
                if (emp == null)
                    Response.Redirect(redirectUrl);
                this.ddlPledger.InnerText = emp.Name;

                this.txbMoveMemo.InnerText = pledge.Memo;

                NFMT.WareHouse.BLL.PledgeDetialBLL pledgeDetialBLL = new NFMT.WareHouse.BLL.PledgeDetialBLL();
                result = pledgeDetialBLL.GetStockIds(user, pledgeId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                this.hidsids.Value = result.ReturnValue.ToString();

                string json = serializer.Serialize(pledge);
                this.hidModel.Value = json;

                //attach
                this.attach1.BusinessIdValue = this.pledgeId;
            }
        }
    }
}