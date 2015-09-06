using System;
using System.Web;
using System.Web.UI;
using NFMT.Common;

namespace NFMT.PassPort
{
    public partial class LoginOut : Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

            //页面跳转
            //string redirectUrl = Request.QueryString["redirectUrl"];
            //if (string.IsNullOrEmpty(redirectUrl))
            string redirectUrl = string.Format("{0}login.aspx",NFMT.Common.DefaultValue.NfmtPassPort);
            Response.Redirect(redirectUrl);
        }
    }
}