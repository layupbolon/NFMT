using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// BlocAuditHandler 的摘要说明
    /// </summary>
    public class BlocAuditHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";

            int masterId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["masterId"]) || !int.TryParse(context.Request.Form["masterId"], out masterId) || masterId <= 0)
            {
                context.Response.Write("流程模版序号错误");
                context.Response.End();
            }

            int id = 0;
            if (string.IsNullOrEmpty(context.Request.Form["id"]) || !int.TryParse(context.Request.Form["id"], out id) || id <= 0)
            {
                context.Response.Write("序号错误");
                context.Response.End();
            }

            NFMT.User.BLL.BlocBLL blocBLL = new NFMT.User.BLL.BlocBLL();
            NFMT.Common.ResultModel result = blocBLL.Get(user, id);
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            NFMT.User.Model.Bloc bloc = result.ReturnValue as NFMT.User.Model.Bloc;

            NFMT.WorkFlow.BLL.FlowMasterBLL flowMasterBLL = new NFMT.WorkFlow.BLL.FlowMasterBLL();
            result = flowMasterBLL.Get(user, masterId);
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            NFMT.WorkFlow.Model.FlowMaster flowMaster = result.ReturnValue as NFMT.WorkFlow.Model.FlowMaster;

            NFMT.WorkFlow.Model.DataSource source = new NFMT.WorkFlow.Model.DataSource()
            {
                //DataBaseName = "NFMT_User",
                TableName = "dbo.Bloc",
                DataStatus = NFMT.Common.StatusEnum.待审核,
                RowId = id,//BlocId
                ViewUrl = flowMaster.ViewUrl,
                EmpId = user.EmpId,
                ApplyTime = DateTime.Now,
                ApplyTitle = string.Empty,
                ApplyMemo = string.Empty,
                ApplyInfo = string.Empty,
                RefusalUrl = flowMaster.RefusalUrl,
                SuccessUrl = flowMaster.SuccessUrl,
                DalName = string.IsNullOrEmpty(bloc.DalName) ? "NFMT.User.DAL.BlocDAL" : bloc.DalName,
                AssName = string.IsNullOrEmpty(bloc.AssName) ? "NFMT.User" : bloc.AssName
            };

            NFMT.WorkFlow.Model.Task task = new NFMT.WorkFlow.Model.Task()
            {
                MasterId = masterId,
                TaskName = string.Format("{0} 审核", bloc.BlocName)
            };

            result = blocBLL.Submit(user, new NFMT.User.Model.Bloc() { Id = id });
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            //NFMT.WorkFlow.FlowOperate flowOperate = new NFMT.WorkFlow.FlowOperate();
            //result = flowOperate.CreateTask(user, flowMaster, source, task);
            //if (result.ResultStatus == 0)
            //{
            //    context.Response.Write("提交审核成功");
            //}
            //else
            //{
            //    context.Response.Write(result.Message);
            //}
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}