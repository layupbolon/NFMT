using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Contract.Handler
{
    /// <summary>
    /// LcCreateHandler 的摘要说明
    /// </summary>
    public class LcCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";

            if (string.IsNullOrEmpty(context.Request.Form["issueBank"]))
            {
                context.Response.Write("开证行不能为空");
                context.Response.End();
            }
            int issueBank = 0;
            if (!int.TryParse(context.Request.Form["issueBank"], out issueBank))
            {
                context.Response.Write("开证行转换失败");
                context.Response.End();
            }


            if (string.IsNullOrEmpty(context.Request.Form["adviseBank"]))
            {
                context.Response.Write("通知行不能为空");
                context.Response.End();
            }
            int adviseBank = 0;
            if (!int.TryParse(context.Request.Form["adviseBank"], out adviseBank))
            {
                context.Response.Write("通知行转换失败");
                context.Response.End();
            }


            if (string.IsNullOrEmpty(context.Request.Form["issueDate"]))
            {
                context.Response.Write("开证日期不能为空");
                context.Response.End();
            }
            DateTime issueDate = NFMT.Common.DefaultValue.DefaultTime;
            if (!DateTime.TryParse(context.Request.Form["issueDate"], out issueDate))
            {
                context.Response.Write("开证日期转换失败");
                context.Response.End();
            }


            if (string.IsNullOrEmpty(context.Request.Form["futureDay"]))
            {
                context.Response.Write("远期天数不能为空");
                context.Response.End();
            }
            int futureDay = 0;
            if (!int.TryParse(context.Request.Form["futureDay"], out futureDay))
            {
                context.Response.Write("远期天数转换失败");
                context.Response.End();
            }


            if (string.IsNullOrEmpty(context.Request.Form["lcBala"]))
            {
                context.Response.Write("信用证金额不能为空");
                context.Response.End();
            }
            decimal lcBala = 0;
            if (!decimal.TryParse(context.Request.Form["lcBala"], out lcBala))
            {
                context.Response.Write("信用证金额转换失败");
                context.Response.End();
            }


            if (string.IsNullOrEmpty(context.Request.Form["currency"]))
            {
                context.Response.Write("信用证币种不能为空");
                context.Response.End();
            }
            int currency = 0;
            if (!int.TryParse(context.Request.Form["currency"], out currency))
            {
                context.Response.Write("信用证币种转换失败");
                context.Response.End();
            }

            string resultStr = "添加失败";

            NFMT.Contract.Model.Lc lc = new NFMT.Contract.Model.Lc()
            {
                IssueBank = issueBank,
                AdviseBank = adviseBank,
                IssueDate = issueDate,
                FutureDay = futureDay,
                LcBala = lcBala,
                Currency = currency
            };

            NFMT.Contract.BLL.LcBLL lcBLL = new NFMT.Contract.BLL.LcBLL();
            var result = lcBLL.Insert(user, lc);
            resultStr = result.Message;

            context.Response.Write(resultStr);
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