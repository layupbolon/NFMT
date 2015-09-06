using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Invoice.Handler
{
    /// <summary>
    /// SIListHandler 的摘要说明
    /// </summary>
    public class SIListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string invName = context.Request.QueryString["i"];

            int corp = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["c"]))
            {
                if (!int.TryParse(context.Request.QueryString["c"], out corp))
                    corp = 0;
            }

            int corpOut = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["co"]))
            {
                if (!int.TryParse(context.Request.QueryString["co"], out corpOut))
                    corpOut = 0;
            }

            DateTime fromdate = NFMT.Common.DefaultValue.DefaultTime;
            if (!string.IsNullOrEmpty(context.Request.QueryString["f"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["f"], out fromdate))
                    fromdate = NFMT.Common.DefaultValue.DefaultTime;
            }

            DateTime todate = NFMT.Common.DefaultValue.DefaultTime;
            if (!string.IsNullOrEmpty(context.Request.QueryString["t"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["t"], out todate))
                    todate = NFMT.Common.DefaultValue.DefaultTime;
            }

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                orderStr = string.Format("{0} {1}", context.Request.QueryString["sortdatafield"].Trim(), context.Request.QueryString["sortorder"].Trim());

            NFMT.Invoice.BLL.SIBLL bll = new NFMT.Invoice.BLL.SIBLL();
            NFMT.Common.SelectModel select = bll.GetSelectModel(pageIndex, pageSize, orderStr, invName, corp, corpOut, fromdate, todate);
            NFMT.Common.ResultModel result = bll.Load(user, select);

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