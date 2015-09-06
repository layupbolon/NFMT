using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WareHouse
{
    public partial class StockOutDetail : System.Web.UI.Page
    {
        public int StockOutApplyId = 0;
        public int StockOutId = 0;
        public string DetailStr = string.Empty;
        public NFMT.WareHouse.Model.StockOut stockOut = new NFMT.WareHouse.Model.StockOut();

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = "StockOutList.aspx";

            if (!IsPostBack)
            {
                Utility.VerificationUtility ver = new Utility.VerificationUtility();
                ver.JudgeOperate(this.Page, 44, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.提交审核, NFMT.Common.OperateEnum.作废, NFMT.Common.OperateEnum.撤返, NFMT.Common.OperateEnum.执行完成, NFMT.Common.OperateEnum.执行完成撤销 });

                this.navigation1.Routes.Add("出库列表", redirectUrl);
                this.navigation1.Routes.Add("出库明细", string.Empty);

                int stockOutId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out stockOutId))
                    Response.Redirect(redirectUrl);

                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
                //当前用户
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                //获取出库信息
                NFMT.WareHouse.BLL.StockOutBLL stockOutBLL = new NFMT.WareHouse.BLL.StockOutBLL();
                result = stockOutBLL.Get(user, stockOutId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);

                stockOut = result.ReturnValue as NFMT.WareHouse.Model.StockOut;
                if (stockOut == null || stockOut.StockOutId <= 0)
                    Response.Redirect(redirectUrl);

                //出库信息赋值
                this.txbMemo.Value = stockOut.Memo;

                //获取出库明细信息
                NFMT.WareHouse.BLL.StockOutDetailBLL stockOutDetailBLL = new NFMT.WareHouse.BLL.StockOutDetailBLL();
                result = stockOutDetailBLL.Load(user, stockOutId);
                if (result.ResultStatus != 0)
                    Response.Redirect(redirectUrl);
                List<NFMT.WareHouse.Model.StockOutDetail> stockOutDetails = result.ReturnValue as List<NFMT.WareHouse.Model.StockOutDetail>;
                for (int i = 0; i < stockOutDetails.Count; i++)
                {
                    NFMT.WareHouse.Model.StockOutDetail d = stockOutDetails[i];
                    DetailStr += d.StockOutApplyDetailId.ToString();
                    if (i != stockOutDetails.Count - 1)
                        DetailStr += ",";
                }

                //获取出库申请信息
                NFMT.WareHouse.BLL.StockOutApplyBLL outApplyBLL = new NFMT.WareHouse.BLL.StockOutApplyBLL();
                NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();

                result = outApplyBLL.Get(user, stockOut.StockOutApplyId);
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

                this.contractExpander1.CurContract = contract;
                this.contractExpander1.CurContractSub = sub;
                this.contractExpander1.RedirectUrl = redirectUrl;

                //属性赋值
                this.StockOutApplyId = outApply.StockOutApplyId;
                this.StockOutId = stockOut.StockOutId;

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(stockOut);
                this.hidModel.Value = json;

                //attach
                this.attach1.BusinessIdValue = this.StockOutId;
            }
        }
    }
}