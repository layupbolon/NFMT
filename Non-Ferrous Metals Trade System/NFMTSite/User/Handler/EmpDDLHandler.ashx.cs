using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// EmpDDLHandler 的摘要说明
    /// </summary>
    public class EmpDDLHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.User.BLL.EmployeeBLL bll = new NFMT.User.BLL.EmployeeBLL();
            var result = bll.Load<NFMT.User.Model.Employee>(user);

            context.Response.ContentType = "application/json; charset=utf-8";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            List<NFMT.User.Model.Employee> dt = result.ReturnValue as List<NFMT.User.Model.Employee>;
            dt.OrderBy(temp => temp.Name);


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