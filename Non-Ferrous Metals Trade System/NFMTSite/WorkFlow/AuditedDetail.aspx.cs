using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.WorkFlow
{
    public partial class AuditedDetail : System.Web.UI.Page
    {
        public string viewUrl = string.Empty;
        public List<NFMT.Operate.Model.Attach> attachs;
        public bool hasAttachs = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Utility.VerificationUtility ver = new Utility.VerificationUtility();
                //ver.JudgeOperate(this.Page, 85, new List<NFMT.Common.OperateEnum>() { NFMT.Common.OperateEnum.查询 });

                NFMT.Common.UserModel user = NFMTSite.Utility.UserUtility.CurrentUser;
                string directURL = string.Format("{0}WorkFlow/AuditedList.aspx", NFMT.Common.DefaultValue.NftmSiteName);

                this.navigation1.Routes.Add("已审核任务列表", directURL);
                this.navigation1.Routes.Add("已审核任务明细", string.Empty);

                int taskId = 0;
                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                    Response.Redirect(directURL);
                if (!int.TryParse(Request.QueryString["id"], out taskId))
                    Response.Redirect(directURL);

                this.hidtaskId.Value = taskId.ToString();

                NFMT.WorkFlow.BLL.TaskBLL taskBLL = new NFMT.WorkFlow.BLL.TaskBLL();
                NFMT.Common.ResultModel result = taskBLL.Get(user, taskId);
                if (result.ResultStatus != 0)
                    Response.Redirect(directURL);

                NFMT.WorkFlow.Model.Task task = result.ReturnValue as NFMT.WorkFlow.Model.Task;

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

                NFMT.WorkFlow.BLL.DataSourceBLL dataSourceBLL = new NFMT.WorkFlow.BLL.DataSourceBLL();
                result = dataSourceBLL.Get(user,task.DataSourceId);
                if (result.ResultStatus != 0)
                    Response.Redirect(directURL);

                NFMT.WorkFlow.Model.DataSource dataSource = result.ReturnValue as NFMT.WorkFlow.Model.DataSource;

                if (task != null)
                {
                    this.spTaskName.InnerText = task.TaskName;
                    NFMT.User.Model.Employee emp = NFMT.User.UserProvider.Employees.SingleOrDefault(a => a.EmpId == dataSource.EmpId);
                    if (emp != null)
                        this.spTaskApplyPerson.InnerText = emp.Name;
                    this.spTaskApplyTime.InnerText = dataSource.ApplyTime.ToString();
                    this.spTaskStatus.InnerText = task.TaskStatusName;
                    this.spTaskMemo.InnerText = task.ApplyMemo;

                    this.viewUrl = string.IsNullOrEmpty(dataSource.ViewUrl.Trim()) ? "#" : string.Format("{0}{1}", NFMT.Common.DefaultValue.NfmtSiteName, string.Format(dataSource.ViewUrl, dataSource.RowId));
                }
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