using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// EmpUpdateHandler 的摘要说明
    /// </summary>
    public class EmpUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            context.Response.ContentType = "text/plain";

            int deptId = 0;
            string empCode = context.Request.Form["empCode"];
            string empName = context.Request.Form["empName"];
            string male = context.Request.Form["male"];
            DateTime birthday = NFMT.Common.DefaultValue.DefaultTime;
            string tel = context.Request.Form["tel"];
            string phone = context.Request.Form["phone"];
            int workStatus = 0;
            int id = 0;

            string resultStr = "修改失败";

            if (string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                resultStr = "id未知";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["id"], out id))
            {
                resultStr = "id未知";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(empName))
            {
                resultStr = "员工名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!string.IsNullOrEmpty(context.Request.Form["DeptId"]))
            {
                if (!int.TryParse(context.Request.Form["DeptId"], out deptId))
                {
                    resultStr = "所属部门转换错误";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }
            }

            if (!string.IsNullOrEmpty(context.Request.Form["birthday"]))
            {
                if (!DateTime.TryParse(context.Request.Form["birthday"], out birthday))
                {
                    resultStr = "所属集团转换错误";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }
            }

            if (!string.IsNullOrEmpty(context.Request.Form["workStatus"]))
            {
                if (!int.TryParse(context.Request.Form["workStatus"], out workStatus))
                {
                    resultStr = "员工状态转换错误";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }
            }

            NFMT.User.BLL.EmployeeBLL empBLL = new NFMT.User.BLL.EmployeeBLL();
            NFMT.User.Model.Employee emp = new NFMT.User.Model.Employee()
            {
                EmpId = id,
                DeptId = deptId,
                EmpCode = empCode,
                Name = empName,
                Sex = male.Trim().ToLower() == "true" ? true : false,
                BirthDay = birthday,
                Telephone = tel,
                Phone = phone,
                WorkStatus = workStatus
            };

            result = empBLL.Update(user, emp);
            if (result.ResultStatus == 0)
                result.Message = "修改成功";
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