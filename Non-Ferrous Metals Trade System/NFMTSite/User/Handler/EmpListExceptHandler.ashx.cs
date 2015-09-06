using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// EmpListExceptHandler 的摘要说明
    /// </summary>
    public class EmpListExceptHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int excepteEmpId = -1, workStatus = 0;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string name = context.Request["name"];//姓名模糊查询条件      
            string empCode = context.Request["empCode"];//员工编号模糊查询条件    

            if (!string.IsNullOrEmpty(context.Request.QueryString["workStatus"]))
                int.TryParse(context.Request.QueryString["workStatus"], out workStatus);//在职状态查询条件

            if (!string.IsNullOrEmpty(context.Request.QueryString["excepteEmpId"]))
                int.TryParse(context.Request.QueryString["excepteEmpId"], out excepteEmpId);

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

            int deptId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["deptId"]))
            {
                if (!int.TryParse(context.Request.QueryString["deptId"], out deptId))
                    deptId = 0;
            }
            NFMT.User.BLL.EmployeeBLL employeeBLL = new NFMT.User.BLL.EmployeeBLL();
            NFMT.Common.SelectModel select = employeeBLL.GetSelectModel(pageIndex, pageSize, orderStr, name, empCode, workStatus, excepteEmpId);
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