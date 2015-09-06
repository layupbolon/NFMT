using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using NFMT.Common;
using NFMTSite.PassPort.User;

namespace NFMTSite.Module
{
    public class UserModule:IHttpModule
    {
        private const int MAX_QUEUE_LENGTH = 5000;
        private const int MAX_EQUAL = 500;
        private static Queue<string> queue = new Queue<string>();

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.AcquireRequestState += new EventHandler(Application_AcquireRequestState);
        }

        public void Application_AcquireRequestState(object sender,EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpContext context = app.Context;

            string str = context.Request.AcceptTypes[0];

            //判断请求类型
            

            //判断页面是否需要登录验证

            //获取Cookie，并进行用户验证

            //未登录用户操作
            //记录未登录用户IP与浏览器信息
            //判断该IP用户是否恶意访问

            //已登录用户

            

            string IP = context.Request.UserHostAddress;
            string browser = context.Request.Browser.Browser;
            //string net = context.Request.Browser.ClrVersion.

            if (context.Request.Cookies[DefaultValue.CookieName] != null)
            {
                HttpCookie cookie = context.Request.Cookies[DefaultValue.CookieName];
                cookie.Domain = "maikegroup.com";
                cookie.Expires = DateTime.Now.AddMinutes(DefaultValue.CacheExpiration);
                context.Response.Cookies.Add(cookie);

                PassPort.User.UserService userService = new PassPort.User.UserService();
                PassPort.User.ResultModel result = userService.CheckLoginStatus(cookie.Value);
                if (result.ResultStatus != 0)
                {
                    context.Response.Redirect("Login.aspx");
                    //if (JudgeDanger(IP, browser))
                    //{
                    //    throw new NotImplementedException();
                    //}
                }
            }
            //else
            //{
            //    if (JudgeDanger(IP, browser))
            //    {
            //        throw new NotImplementedException();
            //    }
            //}
        }

        /// <summary>
        /// 判断是否恶意访问
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="browser"></param>
        /// <returns></returns>
        private bool JudgeDanger(string IP, string browser)
        {
            queue.Enqueue(IP);

            if (queue.Count > MAX_QUEUE_LENGTH)
                queue.Dequeue();

            int i = queue.Count(a => a.Equals(IP));

            if (i > MAX_EQUAL)
                return true;

            return false;
        }
    }
}