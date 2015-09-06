using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Contract.Handler
{
    /// <summary>
    /// LcListHandler 的摘要说明
    /// </summary>
    public class LcListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 0, pageSize = 10;
            string orderStr = string.Empty;

            int issueBank = 0;
            if (!string.IsNullOrEmpty(context.Request["i"]))
                int.TryParse(context.Request["i"], out issueBank);

            int adviseBank = 0;
            if (!string.IsNullOrEmpty(context.Request["a"]))
                int.TryParse(context.Request["a"], out adviseBank);

            int status = 0;
            if (!string.IsNullOrEmpty(context.Request["s"]))
                int.TryParse(context.Request["s"], out status);

            DateTime datef = NFMT.Common.DefaultValue.DefaultTime;
            if (!string.IsNullOrEmpty(context.Request["df"]))
                DateTime.TryParse(context.Request["df"], out datef);

            DateTime datet = NFMT.Common.DefaultValue.DefaultTime;
            if (!string.IsNullOrEmpty(context.Request["dt"]))
                DateTime.TryParse(context.Request["dt"], out datet);

            if (!string.IsNullOrEmpty(context.Request.QueryString["page"]))
                int.TryParse(context.Request.QueryString["page"].Trim(), out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["rows"]))
                int.TryParse(context.Request.QueryString["rows"].Trim(), out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                orderStr = string.Format("{0} {1}", context.Request.QueryString["sortdatafield"].Trim(), context.Request.QueryString["sortorder"].Trim());

            NFMT.Contract.BLL.LcBLL lcBLL = new NFMT.Contract.BLL.LcBLL();
            NFMT.Common.SelectModel select = lcBLL.GetSelectModel(pageIndex, pageSize, orderStr, issueBank, adviseBank, status, datef, datet);
            NFMT.Common.ResultModel result = lcBLL.Load(user, select);

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