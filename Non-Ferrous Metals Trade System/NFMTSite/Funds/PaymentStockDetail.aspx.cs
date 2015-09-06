using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class PaymentStockDetail : System.Web.UI.Page
    {
        public NFMT.Funds.Model.Payment curPayment = null;
        public NFMT.Funds.Model.PayApply curPayApply = null;
        public NFMT.Operate.Model.Apply curApply = null;
        public int PayMatterStyle = 0;
        public int PayModeStyle = 0;
        public string SelectedJson = string.Empty;
        public NFMT.Funds.Model.PaymentVirtual curPaymentVirtual = new NFMT.Funds.Model.PaymentVirtual();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 53, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成, NFMT.Common.OperateEnum.执行完成撤销 });

                this.navigation1.Routes.Add("财务付款列表", "PaymentList.aspx");
                this.navigation1.Routes.Add("财务付款修改--库存关联", string.Empty);

                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                //获取财务付款
                int paymentId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out paymentId))
                    Response.Redirect("PaymentList.aspx");
                NFMT.Funds.BLL.PaymentBLL paymentBLL = new NFMT.Funds.BLL.PaymentBLL();
                result = paymentBLL.Get(user, paymentId);
                if (result.ResultStatus != 0)
                    Response.Redirect("PaymentList.aspx");
                NFMT.Funds.Model.Payment payment = result.ReturnValue as NFMT.Funds.Model.Payment;
                if (payment == null || payment.PaymentId <= 0)
                    Response.Redirect("PaymentList.aspx");

                this.curPayment = payment;

                //获取虚拟付款
                if (payment.VirtualBala > 0)
                {
                    NFMT.Funds.BLL.PaymentVirtualBLL paymentVirtualBLL = new NFMT.Funds.BLL.PaymentVirtualBLL();
                    result = paymentVirtualBLL.GetByPaymentId(user, payment.PaymentId);
                    if (result.ResultStatus != 0)
                        Response.Redirect("PaymentList.aspx");
                    NFMT.Funds.Model.PaymentVirtual paymentVirtual = result.ReturnValue as NFMT.Funds.Model.PaymentVirtual;
                    if (paymentVirtual == null)
                        Response.Redirect("PaymentList.aspx");

                    this.curPaymentVirtual = paymentVirtual;
                }

                //获取付款申请
                NFMT.Funds.BLL.PayApplyBLL payApplyBLL = new NFMT.Funds.BLL.PayApplyBLL();
                result = payApplyBLL.Get(user, payment.PayApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect("PaymentList.aspx");

                NFMT.Funds.Model.PayApply payApply = result.ReturnValue as NFMT.Funds.Model.PayApply;
                if (payApply == null || payApply.PayApplyId <= 0)
                    Response.Redirect("PaymentList.aspx");

                this.curPayApply = payApply;

                //获取主申请
                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                result = applyBLL.Get(user, payApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect("PaymentList.aspx");

                NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null || apply.ApplyId <= 0)
                    Response.Redirect("PaymentList.aspx");

                this.curApply = apply;

                this.SelectJson(payApply.PayApplyId, payment.PaymentId);

                //付款申请信息
                this.spnApplyDate.InnerHtml = apply.ApplyTime.ToShortDateString();

                NFMT.User.Model.Department applyDept = NFMT.User.UserProvider.Departments.SingleOrDefault(temp => temp.DeptId == apply.ApplyDept);
                if (applyDept != null && applyDept.DeptId > 0)
                    this.spnApplyDept.InnerHtml = applyDept.DeptName;

                NFMT.User.Model.Corporation recCorp = NFMT.User.UserProvider.Corporations.SingleOrDefault(temp => temp.CorpId == payApply.RecCorpId);
                if (recCorp != null && recCorp.CorpId > 0)
                {
                    this.spnRecCorp.InnerHtml = recCorp.CorpName;
                    this.spnRecCorpFullName.InnerHtml = recCorp.CorpFullName;
                }

                NFMT.Data.Model.Bank recBank = NFMT.Data.BasicDataProvider.Banks.SingleOrDefault(temp => temp.BankId == payApply.RecBankId);
                if (recBank != null && recBank.BankId > 0)
                    this.spnBank.InnerHtml = recBank.BankName;

                this.spnBankAccount.InnerHtml = payApply.RecBankAccount;
                this.spnApplyBala.InnerHtml = payApply.ApplyBala.ToString();

                NFMT.Data.Model.Currency cur = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(temp => temp.CurrencyId == payApply.CurrencyId);
                if (cur != null && cur.CurrencyId > 0)
                    this.spnCurrency.InnerHtml = cur.CurrencyName;

                this.spnPayDeadline.InnerHtml = payApply.PayDeadline.ToShortDateString();

                NFMT.Data.Model.BDStyleDetail payMatter = NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.付款事项)[payApply.PayMatter];
                if (payMatter != null && payMatter.StyleDetailId > 0)
                    this.spnPayMatter.InnerHtml = payMatter.DetailName;

                NFMT.Data.Model.BDStyleDetail payMode = NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.付款方式)[payApply.PayMode];
                if (payMode != null && payMode.StyleDetailId > 0)
                    this.spnPayMode.InnerHtml = payMode.DetailName;

                this.spnMemo.InnerHtml = apply.ApplyDesc;
                this.spnSpecialDesc.InnerHtml = payApply.SpecialDesc;

                this.PayMatterStyle = (int)NFMT.Data.StyleEnum.付款事项;
                this.PayModeStyle = (int)NFMT.Data.StyleEnum.PayMode;

                //审核
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(payment);
                this.hidModel.Value = json;
            }
        }

        public void SelectJson(int payApplyId, int paymentId)
        {
            int pageIndex = 1, pageSize = 100;
            string orderStr = string.Empty, whereStr = string.Empty;

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.Funds.BLL.PayApplyBLL bll = new NFMT.Funds.BLL.PayApplyBLL();
            NFMT.Common.SelectModel select = bll.GetPaymentStockCreateSelect(pageIndex, pageSize, orderStr, payApplyId, paymentId);
            NFMT.Common.ResultModel result = bll.Load(user, select,new NFMT.Common.BasicAuth());

            int totalRows = result.AffectCount;
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

            this.SelectedJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, new Newtonsoft.Json.Converters.DataTableConverter());
        }
    }
}