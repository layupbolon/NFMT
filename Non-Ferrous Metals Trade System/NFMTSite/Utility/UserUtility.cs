using NFMT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Utility
{
    public class UserUtility
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(UserUtility));

        public static UserModel CurrentUser
        {
            get
            {
                string redirectUrl = string.Format("{0}Default.aspx", NFMT.Common.DefaultValue.NfmtSiteName);
                string jsString = string.Format(" <script type=\"text/javascript\">window.parent.location.href=\"{0}Login.aspx?redirectUrl={1}\";</script>", NFMT.Common.DefaultValue.NfmtPassPort, redirectUrl);
                //string redirectUrl = string.Format("{0}Login.aspx",NFMT.Common.DefaultValue.NfmtPassPort);

                try
                {
                    UserModel user = null;
                    HttpCookie userCookie = HttpContext.Current.Request.Cookies[DefaultValue.CookieName];

                    if (userCookie != null)
                    {
                        userCookie.Domain = DefaultValue.Domain;
                        HttpContext.Current.Response.Cookies.Add(userCookie);
                        string token = userCookie.Value;

                        string accountName = string.Empty;

                        PassPort.User.UserService userService = new PassPort.User.UserService();
                        PassPort.User.ResultModel result = userService.CheckLoginStatus(userCookie.Value);

                        if (result != null && result.ResultStatus == 0 && result.ReturnValue != null && !string.IsNullOrEmpty(result.ReturnValue.ToString()))
                            accountName = result.ReturnValue.ToString();

                        if (string.IsNullOrEmpty(accountName))
                        {
                            HttpContext.Current.Response.Clear();
                            HttpContext.Current.Response.Write(jsString);
                            //HttpContext.Current.Response.Redirect(redirectUrl, false);
                            HttpContext.Current.Response.Flush();
                        }

                        user = NFMT.User.UserProvider.GetUserSecurity(userCookie.Value,accountName);
                        if (user == null || user.AccountId <= 0)
                        {
                            HttpContext.Current.Response.Clear();
                            HttpContext.Current.Response.Write(jsString);
                            //HttpContext.Current.Response.Redirect(redirectUrl, false);
                            HttpContext.Current.Response.Flush();
                        }

                        return user;
                    }

                    if (user == null || user.EmpId <= 0)
                    {
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.Write(jsString);
                        //HttpContext.Current.Response.Redirect(redirectUrl, false);
                        HttpContext.Current.Response.Flush();
                    }
                    return user;
                }
                catch (HttpException httpexception)
                {
                    log.ErrorFormat("http错误：{0}，客户端信息：{1}", httpexception.Message, HttpContext.Current.Request != null ? HttpContext.Current.Request.UserAgent : string.Empty);
                    //HttpContext.Current.Response.Redirect(redirectUrl, false);
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Write(jsString);
                    HttpContext.Current.Response.Flush();
                    return null;
                }
                catch(Exception e)
                {
                    log.ErrorFormat("错误:{0}，客户端信息：{1}", e.Message, HttpContext.Current.Request != null ? HttpContext.Current.Request.UserAgent : string.Empty);
                    //HttpContext.Current.Response.Redirect(redirectUrl, false);
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Write(jsString);
                    HttpContext.Current.Response.Flush();
                    return null;
                }
            }
        }
    }
}