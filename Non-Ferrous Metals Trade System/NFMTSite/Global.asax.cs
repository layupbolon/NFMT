using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using NFMT.Common;
using NFMTSite.PassPort.User;

namespace NFMTSite
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            //清除Cookie 并调用wcf LoginOut方法以达到清除Cache效果
            HttpCookie cookie = Request.Cookies[DefaultValue.CookieName];
            if (cookie != null)
            {
                string token = cookie.Value;
                UserService userService = new UserService();
                userService.LoginOut(token);

                cookie.Domain = NFMT.Common.DefaultValue.Domain;
                cookie.Expires = DateTime.Now.AddDays(-1);
                Request.Cookies.Add(cookie);
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(Request.Cookies[DefaultValue.CookieName]);
            }
        }
    }
}