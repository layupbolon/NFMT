using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Config.Handler
{
    /// <summary>
    /// BasicDataHandler 的摘要说明
    /// </summary>
    public class BasicDataHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string resultStr = "基础数据缓存刷新失败";

            try
            {
                NFMT.Data.BasicDataProvider.RefreshBasicData();
                NFMT.Data.DetailProvider.RefreshBDStyle();
                resultStr = "基础数据缓存刷新成功";
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