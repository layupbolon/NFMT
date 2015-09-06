using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Config.Handler
{
    /// <summary>
    /// WorkFlowHandler 的摘要说明
    /// </summary>
    public class WorkFlowHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string resultStr = "工作任务相关缓存刷新失败";

            try
            {
                NFMT.WorkFlow.FlowOperate.RefreshCondition();
                NFMT.WorkFlow.FlowOperate.RefreshNode();
                resultStr = "工作任务相关缓存刷新成功";
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