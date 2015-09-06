using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// AuthGroupCanAllotListHandler 的摘要说明
    /// </summary>
    public class AuthGroupCanAllotListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string name = context.Request["name"];

            int assetId = 0;
            if (string.IsNullOrEmpty(context.Request["assetId"]) || !int.TryParse(context.Request["assetId"], out assetId))
                assetId = 0;

            int tradeDirection = 0;
            if (string.IsNullOrEmpty(context.Request["tradeDirection"]) || !int.TryParse(context.Request["tradeDirection"], out tradeDirection))
                tradeDirection = 0;

            int tradeBorder = 0;
            if (string.IsNullOrEmpty(context.Request["tradeBorder"]) || !int.TryParse(context.Request["tradeBorder"], out tradeBorder))
                tradeBorder = 0;

            int contractInOut = 0;
            if (string.IsNullOrEmpty(context.Request["contractInOut"]) || !int.TryParse(context.Request["contractInOut"], out contractInOut))
                contractInOut = 0;

            int contractLimit = 0;
            if (string.IsNullOrEmpty(context.Request["contractLimit"]) || !int.TryParse(context.Request["contractLimit"], out contractLimit))
                contractLimit = 0;

            int corpId = 0;
            if (string.IsNullOrEmpty(context.Request["corpId"]) || !int.TryParse(context.Request["corpId"], out corpId))
                corpId = 0;

            int empId = 0;
            if (string.IsNullOrEmpty(context.Request["empId"]) || !int.TryParse(context.Request["empId"], out empId))
                empId = 0;

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                orderStr = string.Format("{0} {1}", context.Request.QueryString["sortdatafield"].Trim(), context.Request.QueryString["sortorder"].Trim());

            NFMT.User.BLL.AuthGroupBLL bll = new NFMT.User.BLL.AuthGroupBLL();
            NFMT.Common.SelectModel select = bll.GetCanAllotSelectModel(pageIndex, pageSize, orderStr, name, assetId, tradeDirection, tradeBorder, contractInOut, contractLimit, corpId, empId);
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