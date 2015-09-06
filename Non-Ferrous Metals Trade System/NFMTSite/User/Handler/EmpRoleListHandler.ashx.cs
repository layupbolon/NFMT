using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// EmpRoleListHandler 的摘要说明
    /// </summary>
    public class EmpRoleListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int id = -1,type = -1;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string empName = context.Request.QueryString["name"];

            if (!string.IsNullOrEmpty(context.Request.QueryString["id"]))
                int.TryParse(context.Request["id"], out id);//在职状态查询条件

            if (!string.IsNullOrEmpty(context.Request.QueryString["type"]))
                int.TryParse(context.Request["type"], out type);

            //jqwidgets jqxGrid
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
            NFMT.Common.SelectModel select = employeeBLL.GetEmpRoleSelect(pageIndex, pageSize, orderStr, id, empName,type);
            NFMT.Common.ResultModel result = employeeBLL.Load(user, select);

            context.Response.ContentType = "application/json; charset=utf-8";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            int totalRows = result.AffectCount;
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            Dictionary<string, object> dic = new Dictionary<string, object>();

            //jqwidgets
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