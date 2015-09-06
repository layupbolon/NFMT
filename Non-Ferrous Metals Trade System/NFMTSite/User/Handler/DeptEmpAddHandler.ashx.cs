using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// DeptEmpAddHandler 的摘要说明
    /// </summary>
    public class DeptEmpAddHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int empId = 0;
            int deptId = 0;

            if (string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                context.Response.Write("员工序号错误");
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["id"], out empId) || empId <= 0)
            {
                context.Response.Write("员工序号错误");
                context.Response.End();
            }

            if (string.IsNullOrEmpty(context.Request.Form["did"]))
            {
                context.Response.Write("部门序号错误");
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["did"], out deptId) || deptId <= 0)
            {
                context.Response.Write("部门序号错误");
                context.Response.End();
            }

            NFMT.User.Model.DeptEmp deptEmp = new NFMT.User.Model.DeptEmp();
            deptEmp.DeptId = deptId;
            deptEmp.EmpId = empId;
            deptEmp.CreatorId = user.EmpId;
            deptEmp.RefStatus = NFMT.Common.StatusEnum.已生效;

            NFMT.User.BLL.DeptEmpBLL bll = new NFMT.User.BLL.DeptEmpBLL();
            NFMT.Common.ResultModel result = bll.Insert(user, deptEmp);

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