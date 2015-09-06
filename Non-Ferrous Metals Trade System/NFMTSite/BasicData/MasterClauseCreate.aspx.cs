using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class MasterClauseCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int masterId = 0;

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect("MasterClauseList.aspx");

                if (!int.TryParse(Request.QueryString["id"], out masterId))
                    Response.Redirect("MasterClauseList.aspx");

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                NFMT.Data.BLL.ContractMasterBLL masterBLL = new NFMT.Data.BLL.ContractMasterBLL();
                NFMT.Common.ResultModel result = masterBLL.Get(user, masterId);
                if(result.ResultStatus!=0)
                    Response.Redirect("MasterClauseList.aspx");

                NFMT.Data.Model.ContractMaster master = result.ReturnValue as NFMT.Data.Model.ContractMaster;

                if (master == null || master.MasterId <= 0)
                    Response.Redirect("MasterClauseList.aspx");

                this.spnMasterEname.InnerHtml = master.MasterEname;
                this.spnMasterName.InnerHtml = master.MasterName;
                this.spnMasterStatus.InnerHtml = master.MasterStatusName;
                this.hidMasterId.Value = master.MasterId.ToString();

                this.navigation1.Routes.Add("合约模板列表", "MasterClauseAllot.aspx");
                this.navigation1.Routes.Add("条款分配", string.Empty);
            }
        }
    }
}