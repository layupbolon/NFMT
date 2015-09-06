using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockOutApplyCreate : System.Web.UI.Page
    {
        public NFMT.Contract.Model.ContractSub curSub = null;
        public NFMT.Contract.Model.Contract curContract = null;
        public NFMT.Data.Model.Currency curCurrency = null;
        public int curDeptId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 43, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });
                
                this.navigation1.Routes.Add("出库申请列表", "StockOutApplyList.aspx");
                this.navigation1.Routes.Add("可配货子合约列表", "StockOutApplyContractList.aspx");
                this.navigation1.Routes.Add("子合约配货", string.Empty);

                string redirectUrl = "StockOutApplyContractList.aspx";

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

                this.contractExpander1.CurContract = this.curContract;
                this.contractExpander1.CurContractSub = this.curSub;
                this.contractExpander1.RedirectUrl = redirectUrl;               
            }
        }
    }
}