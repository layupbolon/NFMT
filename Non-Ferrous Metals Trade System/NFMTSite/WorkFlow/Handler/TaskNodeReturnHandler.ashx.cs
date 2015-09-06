using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WorkFlow.Handler
{
    /// <summary>
    /// TaskNodeReturnHandler 的摘要说明
    /// </summary>
    public class TaskNodeReturnHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int taskNodeId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["id"]) || !int.TryParse(context.Request.Form["id"], out taskNodeId) || taskNodeId <= 0)
            {
                result.Message = "序号错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string memo = context.Request.Form["memo"];
            string logResult = context.Request.Form["logResult"];

            NFMT.WorkFlow.BLL.TaskNodeBLL bll = new NFMT.WorkFlow.BLL.TaskNodeBLL();
            result = bll.ReturnHandle(user, new NFMT.WorkFlow.Model.TaskOperateLog()
            {
                TaskNodeId = taskNodeId,
                EmpId = user.EmpId,
                Memo = memo,
                LogTime = DateTime.Now,
                LogResult = logResult
            });

            if (result.ResultStatus == 0)
                result.Message = "操作成功";

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            context.Response.End();
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