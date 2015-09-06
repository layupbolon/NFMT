using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// DeptEmpDeleteHandler 的摘要说明
    /// </summary>
    public class DeptEmpDeleteHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int deptEmpId = 0;

            if (string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                context.Response.Write("序号错误");
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["id"], out deptEmpId) || deptEmpId <= 0)
            {
                context.Response.Write("序号错误");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.User.Model.DeptEmp deptEmp = new NFMT.User.Model.DeptEmp();
            deptEmp.DeptEmpId = deptEmpId;
            deptEmp.RefStatus = NFMT.Common.StatusEnum.已作废;

            NFMT.User.BLL.DeptEmpBLL bll = new NFMT.User.BLL.DeptEmpBLL();
            NFMT.Common.ResultModel result = bll.Invalid(user, deptEmp);
            if (result.ResultStatus == 0)
                context.Response.Write("操作成功");
            else
                context.Response.Write(result.Message);
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