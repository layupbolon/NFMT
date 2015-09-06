using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NFMT.Common;
using NFMT.Contract.BLL;
using NFMT.Contract.Model;
using NFMT.Data;
using NFMT.Funds;
using NFMT.Funds.BLL;
using NFMT.Funds.Model;
using NFMT.Operate.Model;
using NFMTSite.Utility;
using ApplyBLL = NFMT.Operate.BLL.ApplyBLL;

namespace NFMTSite.Funds
{
    public partial class PayApplyDetail : Page
    {
        public int PayMatterStyle = 0;
        public int PayModeStyle = 0;
        public UserModel curUser = null;
        public ContractSub curSub = null;
        public Apply curApply = null;
        public PayApply curPayApply = null;
        public string StockDetailsJson = string.Empty;
        public string PaymentJson = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificationUtility ver = new VerificationUtility();
            ver.JudgeOperate(this.Page, 52, new List<OperateEnum>() { OperateEnum.修改 });

            if (!IsPostBack)
            {
                UserModel user = UserUtility.CurrentUser;
                this.curUser = user;

                this.PayMatterStyle = (int)StyleEnum.付款事项;
                this.PayModeStyle = (int)StyleEnum.PayMode;

                string redirectUrl = "PayApplyList.aspx";

                this.navigation1.Routes.Add("付款申请列表", redirectUrl);
                this.navigation1.Routes.Add("付款申请明细", string.Empty);

                int applyId = 0, payApplyId = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["aid"]))
                    int.TryParse(Request.QueryString["aid"], out applyId);
                if (applyId <= 0 && (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out payApplyId)))
                    this.WarmAlert("付款申请序号错误", redirectUrl);

                ResultModel result = new ResultModel();

                //获取付款申请
                PayApplyBLL payApplyBLL = new PayApplyBLL();
                if (applyId > 0)
                    result = payApplyBLL.GetByApplyId(user, applyId);
                else
                    result = payApplyBLL.Get(user, payApplyId);

                if (result.ResultStatus != 0)
                    this.WarmAlert("获取付款申请失败", redirectUrl);

                PayApply payApply = result.ReturnValue as PayApply;
                if (payApply == null || payApply.PayApplyId <= 0)
                    this.WarmAlert("获取付款申请失败", redirectUrl);

                this.curPayApply = payApply;

                //获取主申请
                ApplyBLL applyBLL = new ApplyBLL();
                result = applyBLL.Get(user, payApply.ApplyId);
                if (result.ResultStatus != 0)
                    this.WarmAlert("获取主申请失败", redirectUrl);

                Apply apply = result.ReturnValue as Apply;
                if (apply == null || apply.ApplyId <= 0)
                    this.WarmAlert("获取主申请失败", redirectUrl);

                this.curApply = apply;

                //获取合约付款申请
                ContractPayApplyBLL contractPayApplyBLL = new ContractPayApplyBLL();
                result = contractPayApplyBLL.GetByPayApplyId(user, payApply.PayApplyId);
                if (result.ResultStatus != 0)
                    this.WarmAlert("获取合约失败", redirectUrl);

                ContractPayApply contractPayApply = result.ReturnValue as ContractPayApply;
                if (contractPayApply == null || contractPayApply.RefId <= 0)
                    this.WarmAlert("获取合约失败", redirectUrl);

                //获取子合约
                ContractSubBLL subBll = new ContractSubBLL();
                result = subBll.Get(user, contractPayApply.ContractSubId);
                if (result.ResultStatus != 0)
                    this.WarmAlert("获取子合约失败", redirectUrl);
                ContractSub sub = result.ReturnValue as ContractSub;
                if (sub == null || sub.ContractId == 0)
                    this.WarmAlert("获取子合约失败", redirectUrl);

                this.curSub = sub;

                //合约
                ContractBLL bll = new ContractBLL();
                result = bll.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    this.WarmAlert("获取合约失败", redirectUrl);

                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId == 0)
                    this.WarmAlert("获取合约失败", redirectUrl);

                this.contractExpander1.CurContract = contract;
                this.contractExpander1.CurContractSub = sub;
                this.contractExpander1.RedirectUrl = redirectUrl;

                SelectModel select = payApplyBLL.GetPayApplyStocksSelect(1, 100, "spa.RefId desc", payApply.PayApplyId);
                result = payApplyBLL.Load(user, select, DefaultValue.ClearAuth);
                if (result.ResultStatus != 0)
                    this.WarmAlert("获取付款库存列表失败", redirectUrl);

                DataTable dt = result.ReturnValue as DataTable;
                if (dt == null)
                    this.WarmAlert("获取付款库存列表失败", redirectUrl);

                this.StockDetailsJson = JsonConvert.SerializeObject(dt, new DataTableConverter());

                //付款明细
                PaymentBLL paymentBLL = new PaymentBLL();
                select = paymentBLL.GetSelectModel(1, 100, "pay.PaymentId desc", DefaultValue.DefaultTime, DefaultValue.DefaultTime, 0, 0, (int)StatusEnum.已生效, payApply.PayApplyId);
                result = paymentBLL.Load(user, select, DefaultValue.ClearAuth);
                if (result.ResultStatus != 0)
                    this.WarmAlert("获取付款明细失败", redirectUrl);

                dt = result.ReturnValue as DataTable;
                if (dt == null)
                    this.WarmAlert("获取付款明细失败", redirectUrl);
                this.PaymentJson = JsonConvert.SerializeObject(dt, new DataTableConverter());

                //审核实体
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(apply);
                this.hidModel.Value = json;

                FundsStyleEnum fundsStyle = (FundsStyleEnum)payApply.PayApplySource;
                result = payApplyBLL.GetAuditInfo(user, payApply.ApplyId, fundsStyle);
                if (result.ResultStatus != 0)
                    this.WarmAlert("获取付款申请审核信息失败", redirectUrl);

                //this.txbAuditInfo.InnerHtml = result.ReturnValue.ToString();

                if (apply.ApplyStatus != StatusEnum.已录入 && apply.ApplyStatus != StatusEnum.待审核 && apply.ApplyStatus != StatusEnum.审核拒绝
                    && apply.ApplyStatus != StatusEnum.已撤返)
                {
                    //this.jqxAuditInfoExpander.Visible = false;
                }
                else if (apply.ApplyStatus == StatusEnum.已生效 || apply.ApplyStatus == StatusEnum.已完成)
                {

                }
            }
        }
    }
}