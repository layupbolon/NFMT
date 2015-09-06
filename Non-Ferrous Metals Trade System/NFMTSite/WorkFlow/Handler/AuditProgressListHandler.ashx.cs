using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WorkFlow.Handler
{
    /// <summary>
    /// AuditProgressListHandler 的摘要说明
    /// </summary>
    public class AuditProgressListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 500;
            string orderStr = string.Empty, whereStr = string.Empty;

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            string baseName = context.Request["b"];
            if (string.IsNullOrEmpty(baseName))
            {
                context.Response.Write("数据库名称参数不能为空");
                context.Response.End();
            }

            string tableCode = context.Request["t"];
            if (string.IsNullOrEmpty(baseName))
            {
                context.Response.Write("表名参数不能为空");
                context.Response.End();
            }

            int rowId = 0;
            if (string.IsNullOrEmpty(context.Request["id"]) || !int.TryParse(context.Request["id"], out rowId) || rowId <= 0)
            {
                context.Response.Write("序号错误");
                context.Response.End();
            }  

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                orderStr = string.Format("{0} {1}", context.Request.QueryString["sortdatafield"].Trim(), context.Request.QueryString["sortorder"].Trim());

            NFMT.WorkFlow.BLL.TaskBLL taskBLL = new NFMT.WorkFlow.BLL.TaskBLL();

            NFMT.Common.ResultModel result = taskBLL.GetAuditProgressSelectModel(user, baseName, tableCode, rowId);

            System.Collections.Generic.Dictionary<string, object> dic = new System.Collections.Generic.Dictionary<string, object>();
            System.Data.DataTable dt = new System.Data.DataTable();
            context.Response.ContentType = "application/json; charset=utf-8";
            if (result.ResultStatus == 0)
                dt = result.ReturnValue as System.Data.DataTable;            

            dic.Add("count", result.AffectCount);
            dic.Add("data", dt);

            string postData = Newtonsoft.Json.JsonConvert.SerializeObject(dic);

            context.Response.Write(postData);
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