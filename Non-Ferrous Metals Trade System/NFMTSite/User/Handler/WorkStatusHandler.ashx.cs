using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// WorkStatusHandler 的摘要说明
    /// </summary>
    public class WorkStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            context.Response.ContentType = "text/plain";

            NFMT.User.BLL.EmployeeBLL bll = new NFMT.User.BLL.EmployeeBLL();
            NFMT.Common.ResultModel result = bll.GetWorkStatusList(user);
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            context.Response.Write(jsonStr);
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