using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// SmsTypeStatusHandler 的摘要说明
    /// </summary>
    public class SmsTypeStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int id = 0;
            int operateId = 0;

            if (string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                context.Response.Write("类型明细序号错误");
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["id"], out id) || id <= 0)
            {
                context.Response.Write("类型明细序号错误");
                context.Response.End();
            }

            if (string.IsNullOrEmpty(context.Request.Form["oi"]))
            {
                context.Response.Write("操作错误");
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["oi"], out operateId) || operateId <= 0)
            {
                context.Response.Write("操作错误");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Sms.BLL.SmsTypeBLL bll = new NFMT.Sms.BLL.SmsTypeBLL();
            NFMT.Sms.Model.SmsType smsType = new NFMT.Sms.Model.SmsType();
            smsType.LastModifyId = user.EmpId;
            smsType.SmsTypeId = id;

            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.冻结:
                    result = bll.Freeze(user, smsType);
                    break;
                case NFMT.Common.OperateEnum.解除冻结:
                    result = bll.UnFreeze(user, smsType);
                    break;
            }

            context.Response.Write(result.Message);
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