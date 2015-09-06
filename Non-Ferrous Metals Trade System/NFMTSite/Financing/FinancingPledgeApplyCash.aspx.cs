using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using NFMT.Common;
using NFMT.Finance.BLL;
using NFMT.Finance.Model;
using NFMT.User;
using NFMT.WorkFlow.BLL;
using NFMT.WorkFlow.Model;
using NFMTSite.Utility;

namespace NFMTSite.Financing
{
    public partial class FinancingPledgeApplyCash : BasePage
    {
        int menuId = (int)MenuEnum.质押申请单;
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

        public PledgeApply CurPledgeApply;
        public int taskNodeId = 0;
        public bool hasData = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string redirectUrl = "FinancingPledgeApplyList.aspx";

                int id = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out id))
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

                PledgeApplyBLL pledgeApplyBLL = new PledgeApplyBLL();
                result = pledgeApplyBLL.Get(User, id);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                CurPledgeApply = result.ReturnValue as PledgeApply;

                PledgeApplyCashDetailBLL pledgeApplyCashDetailBLL = new PledgeApplyCashDetailBLL();
                result = pledgeApplyCashDetailBLL.LoadByPledgeApplyId(User, id);
                if (result.ResultStatus != 0)
                    this.WarmAlert(result.Message, redirectUrl);

                List<PledgeApplyCashDetail> pledgeApplyCashDetails = result.ReturnValue as List<PledgeApplyCashDetail>;
                if (pledgeApplyCashDetails != null && pledgeApplyCashDetails.Any())
                    hasData = true;
            }
        }
    }
}