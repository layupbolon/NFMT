using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// EmpCreateHandler 的摘要说明
    /// </summary>
    public class EmpCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                context.Response.ContentType = "text/plain";

                string empStr = context.Request.Form["Employee"];
                if (string.IsNullOrEmpty(empStr))
                {
                    context.Response.Write("员工信息不能为空");
                    context.Response.End();
                }

                string accountStr = context.Request.Form["account"];
                if (string.IsNullOrEmpty(empStr))
                {
                    context.Response.Write("员工账号密码信息不能为空");
                    context.Response.End();
                }

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.User.Model.Employee emp = serializer.Deserialize<NFMT.User.Model.Employee>(empStr);
                NFMT.User.Model.Account account = serializer.Deserialize<NFMT.User.Model.Account>(accountStr);

                if (emp.DeptId <= 0)
                {
                    context.Response.Write("未选择部门");
                    context.Response.End();
                }

                if (string.IsNullOrEmpty(emp.EmpCode))
                {
                    context.Response.Write("未填写员工编号");
                    context.Response.End();
                }

                if (string.IsNullOrEmpty(emp.Name))
                {
                    context.Response.Write("员工名称不能为空");
                    context.Response.End();
                }

                NFMT.User.BLL.EmployeeBLL empBLL = new NFMT.User.BLL.EmployeeBLL();
                result = empBLL.CreateHandler(user, emp, account);

                if (result.ResultStatus == 0)
                    result.Message = "员工新增成功";
                
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.ResultStatus = -1;
            }

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