using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.BasicData
{
    public partial class ContractMasterView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 77, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.冻结, NFMT.Common.OperateEnum.解除冻结 });

                int masterId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect("ContractMasterList.aspx");

                if (int.TryParse(Request.QueryString["id"], out masterId))
                {
                    if (masterId == 0)
                        Response.Redirect("ContractMasterList.aspx");

                    NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                    NFMT.Data.BLL.ContractMasterBLL bll = new NFMT.Data.BLL.ContractMasterBLL();
                    NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
                    result = bll.Get(user, masterId);
                    if (result.ResultStatus != 0)
                        Response.Redirect("ContractMasterList.aspx");
                    NFMT.Data.Model.ContractMaster master = result.ReturnValue as NFMT.Data.Model.ContractMaster;

                    if (master == null || master.MasterId <= 0)
                        Response.Redirect("ContractMasterList.aspx");

                    this.spnMasterEname.InnerHtml = master.MasterEname;
                    this.spnMasterName.InnerHtml = master.MasterName;
                    this.spnMasterStatus.InnerHtml = master.MasterStatusName;
                    this.hidMasterId.Value = master.MasterId.ToString();

                    this.navigation1.Routes.Add("合约模板列表", "ContractMasterList.aspx");
                    this.navigation1.Routes.Add("合约模板明细", string.Empty);
                }
                else
                    Response.Redirect("ContractMasterList.aspx");
            }
        }
    }
}