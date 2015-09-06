using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Config.Handler
{
    /// <summary>
    /// UserHandler 的摘要说明
    /// </summary>
    public class UserHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string resultStr = "用户数据缓存刷新失败";

            try
            {
                NFMT.User.UserProvider.RefreshUser();
                resultStr = "用户数据缓存刷新成功";
            }
            catch (Exception ex)
            {
                resultStr = ex.Message;
            }

            context.Response.Write(resultStr);
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