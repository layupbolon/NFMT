using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Message.Handler
{
    /// <summary>
    /// MessageListHandler 的摘要说明
    /// </summary>
    public class MessageListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string redirectUrl = string.Format("{0}Default.aspx", NFMT.Common.DefaultValue.NfmtSiteName);
            string jsString = string.Format(" <script type=\"text/javascript\">window.parent.location.href=\"{0}Login.aspx?redirectUrl={1}\";</script>", NFMT.Common.DefaultValue.NfmtPassPort, redirectUrl);

            NFMT.Sms.BLL.SmsBLL bll = new NFMT.Sms.BLL.SmsBLL();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            if (user == null || user.EmpId <= 0)
            {
                context.Response.Write(jsString);
                context.Response.Flush();
            }

            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            string postData = string.Empty;

            result = bll.GetCurrentSms(user);
            if (result.ResultStatus != 0)
            {
                postData = Newtonsoft.Json.JsonConvert.SerializeObject(new Sms() { SmsId = 0 });
                context.Response.Write(postData);
                context.Response.End();
            }
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            postData = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            context.Response.Write(postData);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class Sms
        {
            public int SmsId { get; set; }
        }
    }
}