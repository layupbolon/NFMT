using NFMT.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WorkFlow
{
    public partial class TaskDetail : System.Web.UI.Page
    {
        public string viewUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Utility.VerificationUtility ver = new Utility.VerificationUtility();
                //ver.JudgeOperate(this.Page, 22, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.查询 });

                NFMT.Common.UserModel user = NFMTSite.Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
                string directURL = string.Format("{0}WorkFlow/TaskList.aspx", NFMT.Common.DefaultValue.NfmtSiteName);

                this.navigation1.Routes.Add("待审核任务列表", directURL);
                this.navigation1.Routes.Add("待审核任务明细", string.Empty);

                int nodeId = 0;
                if (nodeId == 0 && (string.IsNullOrEmpty(Request.QueryString["NodeId"]) || !int.TryParse(Request.QueryString["NodeId"], out nodeId)))
                    Response.Redirect(directURL);

                this.hidId.Value = nodeId.ToString();

                NFMT.WorkFlow.BLL.TaskNodeBLL taskNodeBLL = new NFMT.WorkFlow.BLL.TaskNodeBLL();
                result = taskNodeBLL.Get(user, nodeId);
                if (result.ResultStatus != 0)
                    Response.Redirect(directURL);

                NFMT.WorkFlow.Model.TaskNode taskNode = result.ReturnValue as NFMT.WorkFlow.Model.TaskNode;
                if (taskNode == null || taskNode.TaskNodeId <= 0)
                    Response.Redirect(directURL);

                NFMT.WorkFlow.BLL.TaskBLL taskBLL = new NFMT.WorkFlow.BLL.TaskBLL();

                result = taskBLL.Get(user, taskNode.TaskId);
                if (result.ResultStatus != 0)
                    Response.Redirect(directURL);

                NFMT.WorkFlow.Model.Task task = result.ReturnValue as NFMT.WorkFlow.Model.Task;
                if (task == null || task.TaskId <= 0)
                    Response.Redirect(directURL);

                this.hidtaskId.Value = task.TaskId.ToString();

                if (task.TaskStatus != NFMT.Common.StatusEnum.已生效)
                    Response.Redirect(directURL);//跳至消息过期提醒页面

                NFMT.WorkFlow.BLL.DataSourceBLL dataSourceBLL = new NFMT.WorkFlow.BLL.DataSourceBLL();
                result = dataSourceBLL.Get(user, task.DataSourceId);
                if (result.ResultStatus != 0)
                    Response.Redirect(directURL);

                NFMT.WorkFlow.Model.DataSource dataSource = result.ReturnValue as NFMT.WorkFlow.Model.DataSource;
                if (dataSource == null || dataSource.SourceId <= 0)
                    Response.Redirect(directURL);

                this.spTaskName.InnerText = task.TaskName;
                NFMT.User.Model.Employee emp = NFMT.User.UserProvider.Employees.FirstOrDefault(temp => temp.EmpId == dataSource.EmpId);
                if (emp != null)
                    this.spTaskApplyPerson.InnerText = emp.Name;
                this.spTaskApplyTime.InnerText = dataSource.ApplyTime.ToString();
                this.spTaskMemo.InnerText = task.TaskConnext;
                //this.spTaskStatus.InnerText = task.TaskStatusName;
                //this.spTaskFlowDesc.InnerText = task.FlowDescribtion;

                this.viewUrl = string.IsNullOrEmpty(dataSource.ViewUrl.Trim()) ? "#" : string.Format("{0}{1}", NFMT.Common.DefaultValue.NfmtSiteName, string.Format(dataSource.ViewUrl, dataSource.RowId));
            }
        }
    }
}