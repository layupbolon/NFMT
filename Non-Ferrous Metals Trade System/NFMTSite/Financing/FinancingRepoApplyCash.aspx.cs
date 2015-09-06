using System;
using System.Collections.Generic;
using System.Web.UI;
using NFMT.Common;
using NFMT.Finance.BLL;
using NFMT.Finance.Model;
using NFMT.User;
using NFMT.WorkFlow.BLL;
using NFMT.WorkFlow.Model;
using NFMTSite.FinanceService;
using NFMTSite.Utility;
using PledgeApply = NFMT.Finance.Model.PledgeApply;
using StatusEnum = NFMT.Common.StatusEnum;
using UserModel = NFMT.Common.UserModel;

namespace NFMTSite.Financing
{
    public partial class FinancingRepoApplyCash : BasePage
    {
        int menuId = (int)MenuEnum.赎回申请单;
        protected override int MenuId
        {
            get { return menuId; }
            set { menuId = value; }
        }

        List<OperateEnum> operateEnumsMustHave = new List<OperateEnum>() { OperateEnum.查询 };
        protected override List<OperateEnum> OperateEnumsMustHave
        {
            get { return operateEnumsMustHave; }
            set { operateEnumsMustHave = value; }
        }

        public RepoApply CurRepoApply;
        public PledgeApply CurPledgeApply;
        public string selectedJsonUp = string.Empty;
        public string selectedJsonDown = string.Empty;
        public int repoApplyId = 0;
        public int taskNodeId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string redirectUrl = "FinancingRepoApplyList.aspx";

                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out repoApplyId) || repoApplyId <= 0)
                    this.WarmAlert("参数错误", redirectUrl);

                if (string.IsNullOrEmpty(Request.QueryString["taskNodeId"]) || !int.TryParse(Request.QueryString["taskNodeId"], out taskNodeId))
                    this.WarmAlert("参数错误", redirectUrl);

                TaskNodeBLL taskNodeBLL = new TaskNodeBLL();
                ResultModel result = taskNodeBLL.Get(User, taskNodeId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                TaskNode taskNode = result.ReturnValue as TaskNode;
                if (taskNode == null || taskNode.TaskNodeId <= 0 || taskNode.NodeStatus != StatusEnum.待审核 || taskNode.EmpId != User.EmpId)
                    Response.Redirect(redirectUrl);

                TaskBLL taskBLL = new TaskBLL();
                result = taskBLL.Get(User, taskNode.TaskId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                Task task = result.ReturnValue as Task;
                if (task.TaskStatus != StatusEnum.已生效)
                    Response.Redirect(redirectUrl);

                RepoApplyBLL repoApplyBLL = new RepoApplyBLL();
                result = repoApplyBLL.Get(User, repoApplyId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                CurRepoApply = result.ReturnValue as RepoApply;
                if (CurRepoApply == null || CurRepoApply.RepoApplyId <= 0)
                    this.WarmAlert("获取赎回申请单错误", redirectUrl);

                PledgeApplyBLL pledgeApplyBLL = new PledgeApplyBLL();
                result = pledgeApplyBLL.Get(User, CurRepoApply.PledgeApplyId);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                CurPledgeApply = result.ReturnValue as PledgeApply;
                if (CurPledgeApply == null || CurPledgeApply.PledgeApplyId <= 0)
                    this.WarmAlert("获取质押申请单错误", redirectUrl);

                FinService service = new FinService();
                selectedJsonUp = service.GetFinPledgeApplyStockDetailForRepoList(1, 500, "pasd.StockDetailId desc", CurPledgeApply.PledgeApplyId);

                selectedJsonDown = service.GetFinRepoApplyStockDetailForUpdateDown(1, 500, "rad.StockDetailId desc", CurPledgeApply.PledgeApplyId, repoApplyId);
            }
        }
    }
}