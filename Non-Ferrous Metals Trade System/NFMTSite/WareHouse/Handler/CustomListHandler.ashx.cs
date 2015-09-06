using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// CustomListHandler 的摘要说明
    /// </summary>
    public class CustomListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            int person = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["p"]))
            {
                if (!int.TryParse(context.Request.QueryString["p"], out person))
                    person = 0;
            }

            int corpId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["c"]))
            {
                if (!int.TryParse(context.Request.QueryString["c"], out corpId))
                    corpId = 0;
            }

            int assetId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["a"]))
            {
                if (!int.TryParse(context.Request.QueryString["a"], out assetId))
                    assetId = 0;
            }

            DateTime fromDate = NFMT.Common.DefaultValue.DefaultTime;
            if (!string.IsNullOrEmpty(context.Request.QueryString["f"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["f"], out fromDate))
                    fromDate = NFMT.Common.DefaultValue.DefaultTime;
            }

            DateTime toDate = NFMT.Common.DefaultValue.DefaultTime;
            if (!string.IsNullOrEmpty(context.Request.QueryString["t"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["t"], out toDate))
                    toDate = NFMT.Common.DefaultValue.DefaultTime;
            }

            int status = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["s"]))
            {
                if (!int.TryParse(context.Request.QueryString["s"], out status))
                    status = 0;
            }

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                orderStr = string.Format("{0} {1}", context.Request.QueryString["sortdatafield"].Trim(), context.Request.QueryString["sortorder"].Trim());

            NFMT.WareHouse.BLL.CustomsClearanceBLL bll = new NFMT.WareHouse.BLL.CustomsClearanceBLL();
            NFMT.Common.SelectModel select = bll.GetSelectModel(pageIndex, pageSize, orderStr, person, corpId, assetId, fromDate, toDate, status);
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