using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class PayApplyContractCreate : System.Web.UI.Page
    {
        public int SubContractId = 0;
        public int PayMatterStyle = 0;
        public int PayModeStyle = 0;
        public int curDeptId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 52, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.录入 });

                this.navigation1.Routes.Add("付款申请列表","PayApplyList.aspx");
                this.navigation1.Routes.Add("付款申请合约列表", "PayApplyContractList.aspx");
                this.navigation1.Routes.Add("付款申请新增--合约关联", string.Empty);

                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"],out this.SubContractId))
                    Response.Redirect("PayApplyContractList.aspx");

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                this.curDeptId = user.DeptId;

                //子合约
                NFMT.Contract.BLL.ContractSubBLL subBll = new NFMT.Contract.BLL.ContractSubBLL();
                NFMT.Common.ResultModel result = subBll.Get(user, this.SubContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect("PayApplyContractList.aspx");
                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.ContractId == 0)
                    Response.Redirect("PayApplyContractList.aspx");

                //合约
                NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
                result = bll.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect("PayApplyContractList.aspx");

                NFMT.Contract.Model.Contract con = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (con == null || con.ContractId == 0)
                    Response.Redirect("PayApplyContractList.aspx");

                //合约信息
                this.spnContractNo.InnerHtml = con.ContractNo;
                this.spnAsset.InnerHtml = NFMT.Data.BasicDataProvider.Assets.First(temp => temp.AssetId == con.AssetId).AssetName;
                NFMT.Data.Model.MeasureUnit muContract = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == con.UnitId);
                this.spnSignAmount.InnerHtml = string.Format("{0}{1}", con.SignAmount.ToString(), muContract.MUName);

                //合约抬头
                NFMT.Contract.BLL.ContractCorporationDetailBLL ccdBll = new NFMT.Contract.BLL.ContractCorporationDetailBLL();

                //内部公司
                result = ccdBll.LoadCorpListByContractId(user, sub.ContractId, true);
                List<NFMT.Contract.Model.ContractCorporationDetail> innerCorps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;

                foreach (NFMT.Contract.Model.ContractCorporationDetail innerCorp in innerCorps)
                {
                    this.spnInCorpNames.InnerHtml += string.Format("[{0}]  ", innerCorp.CorpName);
                }

                //外部公司
                result = ccdBll.LoadCorpListByContractId(user, sub.ContractId, false);
                List<NFMT.Contract.Model.ContractCorporationDetail> outCorps = result.ReturnValue as List<NFMT.Contract.Model.ContractCorporationDetail>;
                foreach (NFMT.Contract.Model.ContractCorporationDetail outCorp in outCorps)
                {
                    this.spnOutCorpNames.InnerHtml += string.Format("[{0}]  ", outCorp.CorpName);
                }

                //子合约信息
                this.spnSubNo.InnerHtml = sub.SubNo;

                NFMT.Data.Model.MeasureUnit muSub = NFMT.Data.BasicDataProvider.MeasureUnits.Single(temp => temp.MUId == sub.UnitId);
                this.spnSubSignAmount.InnerHtml = string.Format("{0}{1}", sub.SignAmount.ToString(), muSub.MUName);
                this.spnPeriodE.InnerHtml = sub.ContractPeriodE.ToShortDateString();

                //局域变量赋值
                this.PayMatterStyle = (int)NFMT.Data.StyleEnum.付款事项;
                this.PayModeStyle = (int)NFMT.Data.StyleEnum.PayMode;
            }
        }
    }
}