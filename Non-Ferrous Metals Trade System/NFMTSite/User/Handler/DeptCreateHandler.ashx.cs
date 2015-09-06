using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// DeptCreateHandler 的摘要说明
    /// </summary>
    public class DeptCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";

            int corpId = 0;
            string deptCode = context.Request.Form["DeptCode"];
            string deptName = context.Request.Form["DeptName"];
            string deptFullName = context.Request.Form["DeptFullName"];
            string deptShort = context.Request.Form["DeptShort"];
            int deptType = 0;
            int parentLeve = 0;
            int deptLevel = 0;

            string resultStr = "添加失败";
            if (string.IsNullOrEmpty(deptName))
            {
                resultStr = "部门名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!string.IsNullOrEmpty(context.Request.Form["CorpId"]))
            {
                if (!int.TryParse(context.Request.Form["CorpId"], out corpId))
                {
                    resultStr = "所属公司转换错误";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }
            }

            if (!string.IsNullOrEmpty(context.Request.Form["DeptType"]))
            {
                if (!int.TryParse(context.Request.Form["DeptType"], out deptType))
                {
                    resultStr = "部门类型转换错误";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }
            }
        
            if (!string.IsNullOrEmpty(context.Request.Form["ParentLeve"]))
            {
                if (!int.TryParse(context.Request.Form["ParentLeve"], out parentLeve))
                {
                    resultStr = "上级部门转换错误";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }
            }
            if (!string.IsNullOrEmpty(context.Request.Form["DeptLevel"]))
            {
                if (!int.TryParse(context.Request.Form["DeptLevel"], out deptLevel))
                {
                    resultStr = "部门级别转换错误";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }
            }

            NFMT.User.Model.Department dept = new NFMT.User.Model.Department()
            {
                 CorpId = corpId,
                 DeptCode = deptCode,
                 DeptName = deptName,
                 DeptFullName = deptFullName,
                 DeptShort = deptShort,
                 DeptType = deptType,
                 ParentLeve = parentLeve,
                 DeptLevel = deptLevel
            };

            NFMT.User.BLL.DepartmentBLL deptBLL = new NFMT.User.BLL.DepartmentBLL();
            
            var result = deptBLL.Insert(user, dept);
            resultStr = result.Message;

            context.Response.Write(resultStr);
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