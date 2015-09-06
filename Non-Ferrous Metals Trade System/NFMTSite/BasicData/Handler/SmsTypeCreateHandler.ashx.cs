using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// SmsTypeCreateHandler 的摘要说明
    /// </summary>
    public class SmsTypeCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string smsTypeStr = context.Request.Form["smsType"];
            if (string.IsNullOrEmpty(smsTypeStr))
            {
                context.Response.Write("类型内容不能为空");
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Sms.Model.SmsType smsType = serializer.Deserialize<NFMT.Sms.Model.SmsType>(smsTypeStr);
                if (smsType == null)
                {
                    context.Response.Write("数据错误");
                    context.Response.End();
                }
                NFMT.Sms.BLL.SmsTypeBLL bll = new NFMT.Sms.BLL.SmsTypeBLL();
                result = bll.Insert(user, smsType);
                if (result.ResultStatus == 0)
                {
                    result.Message = "消息类型新增成功";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
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