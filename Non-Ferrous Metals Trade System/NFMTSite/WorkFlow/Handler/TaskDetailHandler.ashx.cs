using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WorkFlow.Handler
{
    /// <summary>
    /// TaskDetailHandler 的摘要说明
    /// </summary>
    public class TaskDetailHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int taskId = 0;
            if (!string.IsNullOrEmpty(context.Request["tid"]))
                int.TryParse(context.Request["tid"], out taskId);

            int type = 0;
            if (!string.IsNullOrEmpty(context.Request["type"]))
                int.TryParse(context.Request["type"], out type);

            bool hasAttach = false;
            if (!string.IsNullOrEmpty(context.Request["hasAttach"]))
                bool.TryParse(context.Request["hasAttach"], out hasAttach);

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                orderStr = string.Format("{0} {1}", context.Request.QueryString["sortdatafield"].Trim(), context.Request.QueryString["sortorder"].Trim());

            NFMT.WorkFlow.BLL.TaskNodeBLL bll = new NFMT.WorkFlow.BLL.TaskNodeBLL();
            //NFMT.Common.SelectModel select = bll.GetDetailSelectModel(pageIndex, pageSize, orderStr, taskId, type);
            //NFMT.Common.ResultModel result = bll.Load(user, select);
            NFMT.Common.ResultModel result = bll.GetDetailSelectModel(user, taskId, type, hasAttach);

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