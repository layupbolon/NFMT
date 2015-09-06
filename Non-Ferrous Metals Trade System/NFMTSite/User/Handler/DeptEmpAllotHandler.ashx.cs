using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// DeptEmpAllotHandler 的摘要说明
    /// </summary>
    public class DeptEmpAllotHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string deptIdString = context.Request.Form["did"];
            string empIdsString = context.Request.Form["eids"];

            if (string.IsNullOrEmpty(deptIdString))
            {
                context.Response.Write("必须选择部门");
                context.Response.End();
            }

            if (string.IsNullOrEmpty(empIdsString))
            {
                context.Response.Write("必须选择员工");
                context.Response.End();
            }

            int deptId = 0;
            List<int> empIds = new List<int>();

            if (!int.TryParse(deptIdString, out deptId) || deptId == 0)
            {
                context.Response.Write("选择部门出错");
                context.Response.End();
            }

            NFMT.User.Model.Department dept = new NFMT.User.Model.Department();
            dept.DeptId = deptId;

            string[] strs = empIdsString.Split('|');
            List<NFMT.User.Model.Employee> emps = new List<NFMT.User.Model.Employee>();
            foreach (string s in strs)
            {
                int empId = 0;
                if (!int.TryParse(s, out empId) || empId == 0)
                {
                    context.Response.Write("选择员工出错");
                    context.Response.End();
                }

                NFMT.User.Model.Employee emp = new NFMT.User.Model.Employee();
                emp.EmpId = empId;
                emps.Add(emp);

                empIds.Add(empId);
            }

            NFMT.User.BLL.DeptEmpBLL bll = new NFMT.User.BLL.DeptEmpBLL();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();//bll.DeptEmpAllot(user, dept, emps);

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