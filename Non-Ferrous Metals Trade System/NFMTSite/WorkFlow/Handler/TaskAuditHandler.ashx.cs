using NFMT.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WorkFlow.Handler
{
    /// <summary>
    /// TaskAuditHandler 的摘要说明
    /// </summary>
    public class TaskAuditHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string logResult = context.Request.Form["logResult"];
            string isPasspara = context.Request.Form["isPass"];
            string memo = context.Request.Form["memo"];
            int id = 0;

            if (string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                result.Message = "序号错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["id"], out id) || id <= 0)
            {
                result.Message = "序号错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            bool isPass;
            if (!bool.TryParse(isPasspara, out isPass))
            {
                result.Message = "转换错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string aids = context.Request.Form["aids"];

            NFMT.WorkFlow.BLL.TaskNodeBLL taskNodeBLL = new NFMT.WorkFlow.BLL.TaskNodeBLL();
            result = taskNodeBLL.AuditTaskNode(user, id, isPass, memo, logResult, aids);
            if (result.ResultStatus == 0)
                result.Message = "操作成功";

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
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