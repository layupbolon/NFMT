using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NFMT.Common;

namespace FinancingService.DAL
{
    public class UserUtility
    {
        public static UserModel CurrentUser
        {
            get
            {
                string redirectUrl = string.Format("{0}Default.aspx", NFMT.Common.DefaultValue.NfmtSiteName);
                string jsString = string.Format(" <script type=\"text/javascript\">window.parent.location.href=\"{0}Login.aspx?redirectUrl={1}\";</script>", NFMT.Common.DefaultValue.NfmtPassPort, redirectUrl);

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
                        if (result != null && result.ReturnValue != null)
                            accountName = result.ReturnValue.ToString();

                        if (string.IsNullOrEmpty(accountName))
                        {
                            HttpContext.Current.Response.Write(jsString);
                            HttpContext.Current.Response.End();
                        }

                        user = NFMT.User.UserProvider.GetUserSecurity(userCookie.Value, accountName);

                        if (user == null || user.AccountId <= 0)
                        {
                            HttpContext.Current.Response.Write(jsString);
                            HttpContext.Current.Response.End();
                        }

                        return user;
                    }

                    if (user == null || user.EmpId <= 0)
                    {
                        HttpContext.Current.Response.Write(jsString);
                        HttpContext.Current.Response.End();
                    }

                    return user;
                }
                catch
                {
                    HttpContext.Current.Response.Write(jsString);
                    HttpContext.Current.Response.End();
                    return null;
                }
            }
        }
    }
}