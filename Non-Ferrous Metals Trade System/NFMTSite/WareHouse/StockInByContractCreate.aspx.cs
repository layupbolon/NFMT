using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockInByContractCreate : System.Web.UI.Page
    {
        public int SubId = 0;
        public int curDeptId = 0;
        public NFMT.Contract.Model.ContractSub curSub = null;
        public NFMT.Contract.Model.Contract curContract = null;
        public NFMT.Common.UserModel curUser = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            this.curUser = user;

            string redirectUrl = string.Format("{0}WareHouse/StockInByContract.aspx", NFMT.Common.DefaultValue.NftmSiteName);

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 41, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("入库登记", string.Format("{0}WareHouse/StockInList.aspx", NFMT.Common.DefaultValue.NftmSiteName));
                this.navigation1.Routes.Add("合约入库", redirectUrl);
                this.navigation1.Routes.Add("入库登记新增", string.Empty);

                this.hidBDStyleId.Value = ((int)NFMT.Data.StyleEnum.报关状态).ToString();

                NFMT.User.UserSecurity security = user as NFMT.User.UserSecurity;
                this.curDeptId = security.Dept.DeptId;

                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(redirectUrl);

                int id = 0;//子合约Id
                if (!int.TryParse(Request.QueryString["id"], out id) || id == 0)
                    Response.Redirect(redirectUrl);

                this.hidContractSubId.Value = id.ToString();

                NFMT.Contract.BLL.ContractSubBLL contractSubBLL = new NFMT.Contract.BLL.ContractSubBLL();
                NFMT.Common.ResultModel result = contractSubBLL.Get(user, id);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.ContractSub contractSub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (contractSub == null)
                    Response.Redirect(redirectUrl);

                this.curSub = contractSub;

                int contractId = contractSub.ContractId;
                NFMT.Contract.BLL.ContractBLL contractBLL = new NFMT.Contract.BLL.ContractBLL();
                result = contractBLL.Get(user, contractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null)
                    Response.Redirect(redirectUrl);

                this.curContract = contract;

                this.hidAssetId.Value = contract.AssetId.ToString();
                this.hidMUId.Value = contract.UnitId.ToString();

                this.SubId = contractSub.SubId;

                this.contractExpander1.CurContract = contract;
                this.contractExpander1.CurContractSub = contractSub;
                this.contractExpander1.RedirectUrl = redirectUrl;

            }
        }
    }
}