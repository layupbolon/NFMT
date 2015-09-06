using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class ContractMasterUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 77, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

                int masterId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect("ContractMasterList.aspx");

                if (int.TryParse(Request.QueryString["id"], out masterId))
                {
                    if (masterId == 0)
                        Response.Redirect("ContractMasterList.aspx");

                    NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                    NFMT.Data.Model.ContractMaster master = NFMT.Data.BasicDataProvider.ContractMasters.FirstOrDefault(temp => temp.MasterId == masterId);

                    if(master==null || master.MasterId <=0)
                        Response.Redirect("ContractMasterList.aspx");

                    this.txbMasterEname.Value = master.MasterEname;
                    this.txbMasterName.Value = master.MasterName;
                    this.hidMasterStatus.Value =((int)master.MasterStatus).ToString();

                    this.hidMasterId.Value = master.MasterId.ToString();

                    this.navigation1.Routes.Add("合约模板列表", "ContractMasterList.aspx");
                    this.navigation1.Routes.Add("合约模板修改", string.Empty);
                }
                else
                    Response.Redirect("ContractMasterList.aspx");
            }
        }
    }
}