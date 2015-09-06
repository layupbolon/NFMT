using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WorkFlow.Handler
{
    /// <summary>
    /// AuditedListHandler 的摘要说明
    /// </summary>
    public class AuditedListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int empId = -1;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string key = context.Request["k"];//任务名称模糊搜索

            if (!string.IsNullOrEmpty(context.Request.QueryString["uid"]))
                int.TryParse(context.Request.QueryString["uid"], out empId);

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                orderStr = string.Format("{0} {1}", context.Request.QueryString["sortdatafield"].Trim(), context.Request.QueryString["sortorder"].Trim());

            NFMT.WorkFlow.BLL.TaskBLL taskBLL = new NFMT.WorkFlow.BLL.TaskBLL();
            NFMT.Common.SelectModel select = taskBLL.GetAuditedModel(pageIndex, pageSize, orderStr, key, empId);
            NFMT.Common.ResultModel result = taskBLL.Load(new NFMT.Common.UserModel(), select);

            context.Response.ContentType = "application/json; charset=utf-8";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            System.Collections.Generic.Dictionary<string, object> dic = new System.Collections.Generic.Dictionary<string, object>();

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