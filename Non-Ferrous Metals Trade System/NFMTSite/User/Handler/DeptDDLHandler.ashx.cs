using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// DeptDDLHandler 的摘要说明
    /// </summary>
    public class DeptDDLHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int corpId = 0;
            if (!string.IsNullOrEmpty(context.Request["corpId"]))
                int.TryParse(context.Request["corpId"], out corpId);

            NFMT.User.BLL.DepartmentBLL deptBLL = new NFMT.User.BLL.DepartmentBLL();
            var result = deptBLL.GetDeptList(user, corpId);

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