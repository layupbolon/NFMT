using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class PayApplyUpdate : System.Web.UI.Page
    {
        public int PayMatterStyle = 0;
        public int PayModeStyle = 0;
        public NFMT.Common.UserModel curUser = null;
        public NFMT.Contract.Model.ContractSub curSub = null;
        public NFMT.Operate.Model.Apply curApply = null;
        public NFMT.Funds.Model.PayApply curPayApply = null;
        public string StockDetailsJson = string.Empty;
        public decimal BalancePaymentValue = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            ver.JudgeOperate(this.Page, 52, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.修改 });

            if (!IsPostBack)
            {
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                this.curUser = user;

                this.PayMatterStyle = (int)NFMT.Data.StyleEnum.付款事项;
                this.PayModeStyle = (int)NFMT.Data.StyleEnum.PayMode;

                string redirectUrl = "PayApplyList.aspx";

                this.navigation1.Routes.Add("付款申请列表", redirectUrl);
                this.navigation1.Routes.Add("付款申请修改", string.Empty);

                int applyId = 0 , payApplyId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["aid"]))
                    int.TryParse(Request.QueryString["aid"], out applyId);
                if (applyId<=0 && (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out payApplyId)))
                    Response.Redirect(redirectUrl);

                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                //获取出库申请
                NFMT.Funds.BLL.PayApplyBLL payApplyBLL = new NFMT.Funds.BLL.PayApplyBLL();
                if (applyId > 0)
                    result = payApplyBLL.GetByApplyId(user, applyId);
                else
                    result = payApplyBLL.Get(user, payApplyId);

                if(result.ResultStatus!=0)
                    Response.Redirect(redirectUrl);

                NFMT.Funds.Model.PayApply payApply = result.ReturnValue as NFMT.Funds.Model.PayApply;
                if (payApply == null || payApply.PayApplyId <= 0)
                    Response.Redirect(redirectUrl);

                this.curPayApply = payApply;

                //获取主申请
                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, payApply.ApplyId);
                if(result.ResultStatus!=0)
                    Response.Redirect(redirectUrl);

                NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if(apply == null || apply.ApplyId<=0)
                    Response.Redirect(redirectUrl);

                this.curApply = apply;

                //获取合约付款申请
                NFMT.Funds.BLL.ContractPayApplyBLL contractPayApplyBLL = new NFMT.Funds.BLL.ContractPayApplyBLL();
                result = contractPayApplyBLL.GetByPayApplyId(user, payApply.PayApplyId);
                if(result.ResultStatus!=0)
                    Response.Redirect(redirectUrl);

                NFMT.Funds.Model.ContractPayApply contractPayApply = result.ReturnValue as NFMT.Funds.Model.ContractPayApply;
                if(contractPayApply == null || contractPayApply.RefId<=0)
                    Response.Redirect(redirectUrl);

                //获取子合约
                NFMT.Contract.BLL.ContractSubBLL subBll = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBll.Get(user, contractPayApply.ContractSubId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.ContractId == 0)
                    Response.Redirect(redirectUrl);

                this.curSub = sub;

                //合约
                NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
                result = bll.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId == 0)
                    Response.Redirect(redirectUrl);

                this.contractExpander1.CurContract = contract;
                this.contractExpander1.CurContractSub = sub;
                this.contractExpander1.RedirectUrl = redirectUrl;
               

                NFMT.Common.SelectModel select = payApplyBLL.GetPayApplyStocksSelect(1, 100, "spa.RefId desc", payApply.PayApplyId);
                result = payApplyBLL.Load(user, select,new NFMT.Common.BasicAuth());
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
                if (dt == null)
                    Response.Redirect(redirectUrl);

                this.StockDetailsJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());

                result = payApplyBLL.GetContractBalancePayment(user, sub.SubId, 0);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                BalancePaymentValue = (decimal)result.ReturnValue;
            }
        }
    }
}