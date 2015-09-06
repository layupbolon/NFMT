using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMT.PassPort.Handler
{
    /// <summary>
    /// LoginHandler 的摘要说明
    /// </summary>
    public class LoginHandler : IHttpHandler
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(LoginHandler));

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string userName = context.Request.Form["u"];
            if (string.IsNullOrEmpty(userName))
            {
                result.Message = "用户名不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string password = context.Request.Form["p"];
            if (string.IsNullOrEmpty(password))
            {
                result.Message = "密码不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                UserService userService = new UserService();
                result = userService.Login(userName, password);

                if (result.ResultStatus == 0)
                {
                    log.InfoFormat("{0}登陆成功", userName);

                    HttpCookie tokenCookie = new HttpCookie(NFMT.Common.DefaultValue.CookieName, result.ReturnValue.ToString())
                    {
                        Domain = NFMT.Common.DefaultValue.Domain,
                        Path = "/",
                        Expires = DateTime.Now.AddMinutes(NFMT.Common.DefaultValue.CacheExpiration)
                    };
                    context.Response.Cookies.Add(tokenCookie);                    
                }
            }
            catch (Exception ex)
            {
                this.log.ErrorFormat(ex.Message);
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            context.Response.End();
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