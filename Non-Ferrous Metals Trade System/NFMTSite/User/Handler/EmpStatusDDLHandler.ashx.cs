using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// EmpStatusDDLHandler 的摘要说明
    /// </summary>
    public class EmpStatusDDLHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = NFMT.Common.DefaultValue.SysUser;//Utility.UserUtility.CurrentUser;

            int type = 1;
            if (!string.IsNullOrEmpty(context.Request["type"]))
                int.TryParse(context.Request["type"], out type);

            NFMT.User.BLL.EmployeeBLL empBLL = new NFMT.User.BLL.EmployeeBLL();
            var result = empBLL.GetEmpWorkStatusList(user, type);

            context.Response.ContentType = "application/json; charset=utf-8";
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