using NFMT.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WorkFlow
{
    public partial class TaskDetailWithAttach : System.Web.UI.Page
    {
        public string viewUrl = string.Empty;
        public NFMT.WorkFlow.Model.DataSource dataSource;
        public int attachTypeId = 0;
        public List<NFMT.Operate.Model.Attach> attachs;
        public int nodeType = 29;
        public bool hasAttachs = false;

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

                int nodeId = 0;//taskNodeId
                if (nodeId == 0 && (string.IsNullOrEmpty(Request.QueryString["NodeId"]) || !int.TryParse(Request.QueryString["NodeId"], out nodeId)))
                    Response.Redirect(directURL);

                this.hidId.Value = nodeId.ToString();

                //获取任务节点
                NFMT.WorkFlow.BLL.TaskNodeBLL taskNodeBLL = new NFMT.WorkFlow.BLL.TaskNodeBLL();
                result = taskNodeBLL.Get(user, nodeId);
                if (result.ResultStatus != 0)
                    Response.Redirect(directURL);

                NFMT.WorkFlow.Model.TaskNode taskNode = result.ReturnValue as NFMT.WorkFlow.Model.TaskNode;
                if (taskNode == null || taskNode.TaskNodeId <= 0)
                    Response.Redirect(directURL);

                if (taskNode.NodeStatus != NFMT.Common.StatusEnum.待审核 || taskNode.EmpId != user.EmpId)
                    Response.Redirect(directURL);

                //获取节点
                NFMT.WorkFlow.BLL.NodeBLL nodeBLL = new NFMT.WorkFlow.BLL.NodeBLL();
                result = nodeBLL.Get(user, taskNode.NodeId);
                if (result.ResultStatus == 0)
                {
                    NFMT.WorkFlow.Model.Node node = result.ReturnValue as NFMT.WorkFlow.Model.Node;
                    if (node == null || node.NodeId <= 0)
                        Response.Redirect(directURL);

                    nodeType = node.NodeType;
                }

                //获取任务
                NFMT.WorkFlow.BLL.TaskBLL taskBLL = new NFMT.WorkFlow.BLL.TaskBLL();
                result = taskBLL.Get(user, taskNode.TaskId);
                if (result.ResultStatus != 0)
                    Response.Redirect(directURL);

                NFMT.WorkFlow.Model.Task task = result.ReturnValue as NFMT.WorkFlow.Model.Task;
                if (task == null || task.TaskId <= 0 || task.TaskStatus != NFMT.Common.StatusEnum.已生效)
                    Response.Redirect(directURL);

                //获取任务附件
                NFMT.WorkFlow.BLL.TaskAttachBLL taskAttachBLL = new NFMT.WorkFlow.BLL.TaskAttachBLL();
                result = taskAttachBLL.GetTaskAttachByTaskId(user, task.TaskId);
                if (result.ResultStatus != 0)
                    Response.Redirect(directURL);

                List<NFMT.WorkFlow.Model.TaskAttach> taskAttachs = result.ReturnValue as List<NFMT.WorkFlow.Model.TaskAttach>;
                if (taskAttachs == null && !taskAttachs.Any())
                    Response.Redirect(directURL);

                string aids = string.Empty;
                foreach (NFMT.WorkFlow.Model.TaskAttach taskAttach in taskAttachs)
                {
                    aids += taskAttach.AttachId + ",";
                }
                if (!string.IsNullOrEmpty(aids))
                    aids = aids.Substring(0, aids.Length - 1);

                //获取任务附件
                NFMT.Operate.BLL.AttachBLL attachBLL = new NFMT.Operate.BLL.AttachBLL();
                result = attachBLL.GetAttachByAttachIds(user, aids);
                if (result.ResultStatus != 0)
                    Response.Redirect(directURL);

                attachs = result.ReturnValue as List<NFMT.Operate.Model.Attach>;
                if (attachs != null && attachs.Any())
                    hasAttachs = true;

                this.hidtaskId.Value = task.TaskId.ToString();

                //if (task.TaskStatus != NFMT.Common.StatusEnum.已生效)
                //    Response.Redirect(directURL);

                //获取数据源
                NFMT.WorkFlow.BLL.DataSourceBLL dataSourceBLL = new NFMT.WorkFlow.BLL.DataSourceBLL();
                result = dataSourceBLL.Get(user, task.DataSourceId);
                if (result.ResultStatus != 0)
                    Response.Redirect(directURL);

                dataSource = result.ReturnValue as NFMT.WorkFlow.Model.DataSource;
                if (dataSource == null || dataSource.SourceId <= 0)
                    Response.Redirect(directURL);


                //判断该任务节点是否需要操作，如有，则跳转到相应界面
                NFMT.WorkFlow.BLL.TaskOperateBLL taskOperateBLL = new NFMT.WorkFlow.BLL.TaskOperateBLL();
                result = taskOperateBLL.GetByTaskNodeId(user, taskNode.TaskNodeId);
                if (result.ResultStatus == 0)
                {
                    NFMT.WorkFlow.Model.TaskOperate taskOperate = result.ReturnValue as NFMT.WorkFlow.Model.TaskOperate;
                    try
                    {
                        Response.Redirect(string.Format(taskOperate.OperateUrl, dataSource.RowId.ToString(), taskNode.TaskNodeId));
                    }
                    catch (Exception ex)
                    {
                        this.WarmAlert(ex.Message, "#");
                    }                    
                }

                this.spTaskName.InnerText = task.TaskName;
                NFMT.User.Model.Employee emp = NFMT.User.UserProvider.Employees.FirstOrDefault(temp => temp.EmpId == dataSource.EmpId);
                if (emp != null)
                    this.spTaskApplyPerson.InnerText = emp.Name;
                this.spTaskApplyTime.InnerText = dataSource.ApplyTime.ToString();
                this.spTaskMemo.InnerText = task.TaskConnext;
                //this.spTaskStatus.InnerText = task.TaskStatusName;
                //this.spTaskFlowDesc.InnerText = task.FlowDescribtion;

                this.viewUrl = string.IsNullOrEmpty(dataSource.ViewUrl.Trim()) ? "#" : string.Format("{0}{1}", NFMT.Common.DefaultValue.NfmtSiteName, string.Format(dataSource.ViewUrl, dataSource.RowId));

                NFMT.Operate.AttachType attachType = attachBLL.GetAttachTypeByModel(new NFMT.Common.AuditModel() { TableName = dataSource.TableCode });
                attachTypeId = (int)attachType;
            }
        }

        public string GetDatafields(List<NFMT.Operate.Model.Attach> attachs)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (attachs != null && attachs.Any())
            {
                foreach (NFMT.Operate.Model.Attach attach in attachs)
                {
                    sb.Append("{");
                    sb.AppendFormat("name:\"{0}\",type:\"{1}\"", attach.AttachName, "bool");
                    sb.Append("},");
                }
            }
            if (!string.IsNullOrEmpty(sb.ToString()))
                sb = sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public string GetColumns(List<NFMT.Operate.Model.Attach> attachs)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (attachs != null && attachs.Any())
            {
                foreach (NFMT.Operate.Model.Attach attach in attachs)
                {
                    sb.Append("{");
                    sb.AppendFormat("text:\"{0}\",datafield:\"{1}\",columngroup: \"{2}\", threestatecheckbox: true, columntype: \"checkbox\", cellsalign: \"center\", align: \"center\"", attach.AttachName, attach.AttachName, "AttachDetails");
                    sb.Append("},");
                }
            }
            if (!string.IsNullOrEmpty(sb.ToString()))
                sb = sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }
    }
}