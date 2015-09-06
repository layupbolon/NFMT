using System;
using System.Web;
using System.Web.UI;
using NFMT.Common;

namespace NFMT.PassPort
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public ResultModel CheckLogin(string accName, string passWord)
        {
            //登录事件中，写Cookie ,调用wcf的Login方法来写Cache
            UserService userService = new UserService();
            ResultModel result = userService.Login(accName, passWord);

            if (result.ResultStatus == 0)
            {
                HttpCookie tokenCookie = new HttpCookie(DefaultValue.CookieName, result.ReturnValue.ToString())
                {
                    Domain = DefaultValue.Domain,
                    Path = "/",
                    //Expires = DateTime.Now.AddMinutes(DefaultValue.CacheExpiration)
                };
                Response.Cookies.Add(tokenCookie);
            }

            return result;
        }

        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {
            string redirectUrl = DefaultValue.NfmtSiteName;

            string userName = txbUserName.Value.Trim();
            string passWord = txbPassWord.Value.Trim();

            if (string.IsNullOrEmpty(userName))
            {
                ClientScript.RegisterStartupScript(GetType(), "usernameempty", "<script>alert('用户名不能为空');</script>");
                txbUserName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(passWord))
            {
                ClientScript.RegisterStartupScript(GetType(), "passWordempty", "<script>alert('密码不能为空');</script>");
                txbPassWord.Focus();
                return;
            }

            ResultModel result = CheckLogin(userName, passWord);

            if (result.ResultStatus == 0)
            {

                if (Request.QueryString["redirectUrl"] != null)
                    redirectUrl = Request.QueryString["redirectUrl"].Trim();
                //string site = NFMT.Common.DefaultValue.NfmtSiteName;
                //char[] ss = site.ToCharArray();
                //if (ss[ss.Length - 1] == '/')
                //    site = site.Substring(0, site.Length - 1);

                Response.Redirect(redirectUrl);
            }
            else
            {
                result.Message = "用户名或密码错误";
                ClientScript.RegisterStartupScript(GetType(), "error", string.Format("<script>alert('{0}');</script>", result.Message));
            }
        }
    }
}