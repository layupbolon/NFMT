using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// DeptEmpCreateHandler 的摘要说明
    /// </summary>
    public class DeptEmpCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string name = context.Request.QueryString["name"];//姓名模糊查询条件
            bool isHas = false;
            int deptId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["deptId"]))
            {
                if (!int.TryParse(context.Request.QueryString["deptId"], out deptId))
                {
                    context.Response.Write("未选择部门");
                    context.Response.End();
                }
            }
            if (!string.IsNullOrEmpty(context.Request.QueryString["ih"]))
            {
                int intHas = 0;
                if (!int.TryParse(context.Request.QueryString["ih"], out intHas))
                    intHas = 0;
                if (intHas == 1)
                    isHas = true;
                else
                    isHas = false;
            }

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);
            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
            {
                string sortDataField = context.Request.QueryString["sortdatafield"].Trim();
                string sortOrder = context.Request.QueryString["sortorder"].Trim();

                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.User.BLL.EmployeeBLL employeeBLL = new NFMT.User.BLL.EmployeeBLL();
            NFMT.Common.SelectModel select = employeeBLL.GetDeptEmpSelect(pageIndex, pageSize, orderStr, deptId, isHas, name);
            NFMT.Common.ResultModel result = employeeBLL.Load(user, select);
            int totalRows = result.AffectCount;
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("count", result.AffectCount);
            dic.Add("data", dt);

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dic, new Newtonsoft.Json.Converters.DataTableConverter());
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