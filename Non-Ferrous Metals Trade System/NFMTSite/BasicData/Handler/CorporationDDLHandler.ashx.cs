using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// CorporationDDLHandler 的摘要说明
    /// </summary>
    public class CorporationDDLHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.User.BLL.CorporationBLL bll = new NFMT.User.BLL.CorporationBLL();
            var result = bll.Load(user);

            context.Response.ContentType = "application/json; charset=utf-8";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            List<NFMT.User.Model.Corporation> cp = result.ReturnValue as List<NFMT.User.Model.Corporation>;

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(cp);
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