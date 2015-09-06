using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Invoice.Handler
{
    /// <summary>
    /// SIDetailListHandler 的摘要说明
    /// </summary>
    public class SIDetailListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int sIId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["sIId"]) || !int.TryParse(context.Request.QueryString["sIId"], out sIId))
                sIId = 0;

            NFMT.Invoice.BLL.SIDetailBLL sIDetailBLL = new NFMT.Invoice.BLL.SIDetailBLL();
            NFMT.Common.ResultModel result = sIDetailBLL.GetSIDetailForUpdate(user, sIId);
            context.Response.ContentType = "application/json; charset=utf-8";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            System.Collections.Generic.Dictionary<string, object> dic = new System.Collections.Generic.Dictionary<string, object>();

            dic.Add("count", dt.Rows.Count);
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