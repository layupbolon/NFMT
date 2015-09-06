using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NFMTSite
{
    /// <summary>
    /// 页面基类
    /// 包含用户信息获取、用户操作权限判断、日志等
    /// </summary>
    public abstract partial class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 日志实例
        /// </summary>
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(BasePage));

        /// <summary>
        /// 用户信息实例
        /// 不需要重新赋值
        /// </summary>
        protected new NFMT.Common.UserModel User { get; private set; }

        /// <summary>
        /// 当前菜单ID
        /// </summary>
        protected abstract int MenuId { get; set; }

        /// <summary>
        /// 当前页面【必须】需要的权限
        /// </summary>
        protected abstract List<NFMT.Common.OperateEnum> OperateEnumsMustHave { get; set; }

        /// <summary>
        /// 当前用户拥有的操作权限
        /// </summary>
        protected List<NFMT.Common.OperateEnum> UserOperate { get; private set; }

        protected override void OnInit(EventArgs e)
        {
            User = Utility.UserUtility.CurrentUser;
            if (User == null || User.EmpId <= 0)
            {
                log.ErrorFormat("IP地址：{0}，Cookie已过期，客户端信息：{1}", Request.UserHostName, Request.UserAgent);
                Response.Redirect(string.Format("{0}Login.aspx", NFMT.Common.DefaultValue.NfmtPassPort));
            }

            CheckOperate();
        }

        /// <summary>
        /// 检查页面操作权限
        /// </summary>
        private void CheckOperate()
        {
            Utility.VerificationUtility ver = new Utility.VerificationUtility();
            UserOperate = ver.JudgeUserOperate(this, User, MenuId);

            ver.JudgeOperate(this, MenuId, OperateEnumsMustHave);
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        protected string GetIPAddress()
        {
            string result = String.Empty;

            result = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            // 如果使用代理，获取真实IP 
            if (result != null && result.IndexOf(".", StringComparison.Ordinal) == -1)    //没有“.”肯定是非IPv4格式 
                result = null;
            else if (result != null)
            {
                if (result.IndexOf(",", StringComparison.Ordinal) != -1)
                {
                    //有“,”，估计多个代理。取第一个不是内网的IP。 
                    result = result.Replace(" ", "").Replace("'", "");
                    string[] temparyip = result.Split(",;".ToCharArray());
                    for (int i = 0; i < temparyip.Length; i++)
                    {
                        if (IsIPAddress(temparyip[i])
                            && temparyip[i].Substring(0, 3) != "10."
                            && temparyip[i].Substring(0, 7) != "192.168"
                            && temparyip[i].Substring(0, 7) != "172.16.")
                        {
                            return temparyip[i];    //找到不是内网的地址 
                        }
                    }
                }
                else if (IsIPAddress(result)) //代理即是IP格式 
                    return result;
                else
                    result = null;    //代理中的内容 非IP，取IP 
            }
            if (string.IsNullOrEmpty(result))
                result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(result))
                result = System.Web.HttpContext.Current.Request.UserHostAddress;

            return result;
        }
        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        private bool IsIPAddress(string str1)
        {
            if (string.IsNullOrEmpty(str1) || str1.Length < 7 || str1.Length > 15) return false;

            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }
    }
}