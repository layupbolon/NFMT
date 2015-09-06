using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Utility
{
    public static class JsUtility
    {
        /// <summary>
        /// 提示错误信息并跳转至指定地址
        /// </summary>
        /// <param name="page">当前页类</param>
        /// <param name="message">提示错误信息</param>
        /// <param name="redirectUrl">指定的跳转地址</param>
        public static void WarmAlert(System.Web.UI.Page page,string message,string redirectUrl)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendLine("<script>");
            sb.AppendFormat("alert(\"{0}\");",message);
            sb.AppendFormat("document.location.href = \"{0}\";", redirectUrl);
            sb.AppendLine("</script>");

            page.Response.Write(sb.ToString());
            page.Response.End();
        }        
    }
}