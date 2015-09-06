using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockOutApplyClose : System.Web.UI.Page
    {
        public int StockOutApplyId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = "StockOutReadyApplyList.aspx";

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 43, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.关闭 });

                this.navigation1.Routes.Add("出库列表", "StockOutList.aspx");
                this.navigation1.Routes.Add("出库申请明细关闭", string.Empty);

                int stockOutApplyId = 0;

                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out stockOutApplyId))
                    Response.Redirect(redirectUrl);

                //当前用户
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                //获取出库申请信息
                NFMT.WareHouse.BLL.StockOutApplyBLL outApplyBLL = new NFMT.WareHouse.BLL.StockOutApplyBLL();
                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();

                NFMT.Common.ResultModel result = outApplyBLL.Get(user, stockOutApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.WareHouse.Model.StockOutApply outApply = result.ReturnValue as NFMT.WareHouse.Model.StockOutApply;
                if (outApply == null || outApply.StockOutApplyId <= 0)
                    Response.Redirect(redirectUrl);

                result = applyBLL.Get(user, outApply.ApplyId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                if (apply == null || apply.ApplyId <= 0)
                    Response.Redirect(redirectUrl);

                NFMT.User.Model.Department dept = NFMT.User.UserProvider.Departments.SingleOrDefault(a => a.DeptId == apply.ApplyDept);
                if (dept == null)
                    Response.Redirect(redirectUrl);

                //出库申请信息赋值
                this.spnApplyDept.InnerHtml = dept.DeptName;
                NFMT.User.Model.Employee emp = NFMT.User.UserProvider.Employees.FirstOrDefault(temp => temp.EmpId == apply.EmpId);
                this.spnApplier.InnerHtml = emp.Name;
                this.spnApplyDate.InnerHtml = apply.ApplyTime.ToShortDateString();
                this.spnApplyMemo.InnerHtml = apply.ApplyDesc;

                //获取子合约信息
                NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
                result = subBLL.Get(user, outApply.SubContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Contract.Model.ContractSub sub = result.ReturnValue as NFMT.Contract.Model.ContractSub;
                if (sub == null || sub.SubId <= 0)
                    Response.Redirect(redirectUrl);

                NFMT.Contract.BLL.ContractBLL contractBLL = new NFMT.Contract.BLL.ContractBLL();
                result = contractBLL.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                    Response.Redirect(redirectUrl);

                //属性赋值
                this.StockOutApplyId = outApply.StockOutApplyId;

                this.contractExpander1.CurContract = contract;
                this.contractExpander1.CurContractSub = sub;
                this.contractExpander1.RedirectUrl = redirectUrl;
            }
        }
    }
}