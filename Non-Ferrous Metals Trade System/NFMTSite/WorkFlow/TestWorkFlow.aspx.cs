using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NFMT.Data;

namespace NFMTSite.WorkFlow
{
    public partial class TestWorkFlow : System.Web.UI.Page
    {
        NFMT.WorkFlow.FlowOperate flowOperate = new NFMT.WorkFlow.FlowOperate();
        NFMT.WorkFlow.BLL.TaskNodeBLL taskNodeBLL = new NFMT.WorkFlow.BLL.TaskNodeBLL();
        NFMT.WorkFlow.BLL.TaskOperateLogBLL taskOperateLogBLL = new NFMT.WorkFlow.BLL.TaskOperateLogBLL();

        NFMT.Common.UserModel user = NFMT.WorkFlow.FlowOperate.DefaultUser();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //提交审核
        protected void Button1_Click(object sender, EventArgs e)
        {
            NFMT.WorkFlow.BLL.FlowMasterBLL flowMasterBLL = new NFMT.WorkFlow.BLL.FlowMasterBLL();
            NFMT.WorkFlow.BLL.DataSourceBLL dataSourceBLL = new NFMT.WorkFlow.BLL.DataSourceBLL();
            NFMT.WorkFlow.FlowOperate flowOperate = new NFMT.WorkFlow.FlowOperate();

            NFMT.WorkFlow.Model.FlowMaster flowMaster = flowMasterBLL.Get(user, 8).ReturnValue as NFMT.WorkFlow.Model.FlowMaster;

            NFMT.WorkFlow.Model.DataSource source = new NFMT.WorkFlow.Model.DataSource()
            {
                //DataBaseName = "NFMT",
                TableName = "Area",
                DataStatus = NFMT.Common.StatusEnum.待审核,
                RowId = 1,
                ViewUrl = string.Empty,
                //TaskId = taskid,
                EmpId = 1,
                ApplyTime = DateTime.Now,
                ApplyTitle = string.Empty,
                ApplyMemo = string.Empty,
                ApplyInfo = string.Empty,
                RefusalUrl = string.Format("{0}WorkFlow/Handler/SuccessHandler.ashx",NFMT.Common.DefaultValue.NfmtSiteName),
                SuccessUrl = string.Format("{0}WorkFlow/Handler/SuccessHandler.ashx",NFMT.Common.DefaultValue.NfmtSiteName)
            };           


            //int taskid = Convert.ToInt32(flowOperate.CreateTask(user, flowMaster, source, new NFMT.WorkFlow.Model.Task()
            //{
            //    MasterId = 8,
            //    TaskName = "区域审核",
            //    TaskStatus = NFMT.Common.StatusEnum.待审核
            //}).ReturnValue);          
        }

        //审核
        protected void BtnAudit1_Click(object sender, EventArgs e)
        {
            NFMT.WorkFlow.Model.TaskNode taskNode = taskNodeBLL.Get(user, 15).ReturnValue as NFMT.WorkFlow.Model.TaskNode;
            NFMT.WorkFlow.Model.TaskOperateLog taskOperateLog = new NFMT.WorkFlow.Model.TaskOperateLog()
            {
                TaskNodeId = taskNode.TaskNodeId,
                EmpId = 1,
                Memo = "审核人1审核通过！",
                LogTime = DateTime.Now,
                LogResult = string.Empty
            };
            flowOperate.AuditTaskNode(user, taskNode, taskOperateLog, true);
        }

        protected void BtnAudit2_Click(object sender, EventArgs e)
        {
            NFMT.WorkFlow.Model.TaskNode taskNode = taskNodeBLL.Get(user,16).ReturnValue as NFMT.WorkFlow.Model.TaskNode;
            NFMT.WorkFlow.Model.TaskOperateLog taskOperateLog = new NFMT.WorkFlow.Model.TaskOperateLog()
            {
                TaskNodeId = taskNode.TaskNodeId,
                EmpId = 2,
                Memo = "审核人2审核通过！",
                LogTime = DateTime.Now,
                LogResult = string.Empty
            };
            flowOperate.AuditTaskNode(user, taskNode, taskOperateLog, true);
        }

        protected void BtnAudit21_Click(object sender, EventArgs e)
        {
            NFMT.WorkFlow.Model.TaskNode taskNode = taskNodeBLL.Get(user, 17).ReturnValue as NFMT.WorkFlow.Model.TaskNode;
            NFMT.WorkFlow.Model.TaskOperateLog taskOperateLog = new NFMT.WorkFlow.Model.TaskOperateLog()
            {
                TaskNodeId = taskNode.TaskNodeId,
                EmpId = 2,
                Memo = "审核人21审核不通过！",
                LogTime = DateTime.Now,
                LogResult = string.Empty
            };
            flowOperate.AuditTaskNode(user, taskNode, taskOperateLog, false);
        }

        protected void BtnAudit3_Click(object sender, EventArgs e)
        {
            NFMT.WorkFlow.Model.TaskNode taskNode = taskNodeBLL.Get(user, 14).ReturnValue as NFMT.WorkFlow.Model.TaskNode;
            NFMT.WorkFlow.Model.TaskOperateLog taskOperateLog = new NFMT.WorkFlow.Model.TaskOperateLog()
            {
                TaskNodeId = taskNode.TaskNodeId,
                EmpId =3,
                Memo = "审核人3审核通过！",
                LogTime = DateTime.Now,
                LogResult = string.Empty
            };
            flowOperate.AuditTaskNode(user, taskNode, taskOperateLog, true);
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                //XmlHandler.XmlHandler xmlHandler = new XmlHandler.XmlHandler(FileUpload1.PostedFile.FileName,"",6);

            }
        }
    }
}