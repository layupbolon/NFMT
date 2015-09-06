using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class PayApplyContractDetail : System.Web.UI.Page
    {
        public int PayApplyId = 0;
        public int SubContractId = 0;
        public int PayMatterStyle = 0;
        public int PayModeStyle = 0;
        public NFMT.Funds.Model.PayApply curPayApply = new NFMT.Funds.Model.PayApply();
        public NFMT.Operate.Model.Apply curApply = new NFMT.Operate.Model.Apply();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 52, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.确认完成, NFMT.Common.OperateEnum.确认完成撤销 });

                this.navigation1.Routes.Add("付款申请列表", "PayApplyList.aspx");
                this.navigation1.Routes.Add("付款申请明细--合约关联", string.Empty);

                int applyId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["aid"]) || !int.TryParse(Request.QueryString["aid"], out applyId))
                    applyId = 0;

                if (applyId==0 && (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out this.PayApplyId)))
                    Response.Redirect("PayApplyList.aspx");

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                //获取付款申请
                NFMT.Funds.BLL.PayApplyBLL payApplyBLL = new NFMT.Funds.BLL.PayApplyBLL();
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
                if(applyId>0)
                    result = payApplyBLL.GetByApplyId(user, applyId);
                else
                    result = payApplyBLL.Get(user, this.PayApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect("PayApplyList.aspx");

                NFMT.Funds.Model.PayApply payApply = result.ReturnValue as NFMT.Funds.Model.PayApply;
                if (payApply == null || payApply.PayApplyId <= 0)
                    Response.Redirect("PayApplyList.aspx");

                this.curPayApply = payApply;

                //获取合约关联付款申请信息
                NFMT.Funds.BLL.ContractPayApplyBLL contractPayApplyBLL = new NFMT.Funds.BLL.ContractPayApplyBLL();
                result = contractPayApplyBLL.GetByPayApplyId(user, payApply.PayApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect("PayApplyList.aspx");
                NFMT.Funds.Model.ContractPayApply contractPayApply = result.ReturnValue as NFMT.Funds.Model.ContractPayApply;
                if (contractPayApply == null || contractPayApply.RefId <= 0)
                    Response.Redirect("PayApplyList.aspx");

                //获取主申请
                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, payApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect("PayApplyList.aspx");

                NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null || apply.ApplyId <= 0)
                    Response.Redirect("PayApplyList.aspx");

                this.curApply = apply;

                //子合约序号赋值
                this.SubContractId = contractPayApply.ContractSubId;

                //子合约
                NFMT.Contract.BLL.ContractSubBLL subBll = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBll.Get(user, this.SubContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect("PayApplyList.aspx");
                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.ContractId == 0)
                    Response.Redirect("PayApplyList.aspx");

                //合约
                NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
                result = bll.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect("PayApplyList.aspx");

                NFMT.Contract.Model.Contract con = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (con == null || con.ContractId == 0)
                    Response.Redirect("PayApplyList.aspx");

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

                //审核
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(apply);
                this.hidModel.Value = json;

                result = payApplyBLL.GetAuditInfo(user, payApply.ApplyId, NFMT.Funds.FundsStyleEnum.合约付款申请);
                if (result.ResultStatus != 0)
                    Response.Redirect("PayApplyList.aspx");

                this.txbAuditInfo.InnerHtml = result.ReturnValue.ToString();
            }
        }
    }
}