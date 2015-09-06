using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// ContactCreateHandler 的摘要说明
    /// </summary>
    public class ContactCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";

            string contactName = context.Request.Form["contactName"];
            string contactCode = context.Request.Form["contactCode"];
            string contactTel = context.Request.Form["contactTel"];
            string contactFax = context.Request.Form["contactFax"];
            string contactAddress = context.Request.Form["contactAddress"];
            int corpId = 0;

            string resultStr = "添加失败";

            if (string.IsNullOrEmpty(contactName))
            {
                resultStr = "联系人姓名不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!string.IsNullOrEmpty(context.Request.Form["corpId"]))
            {
                if (!int.TryParse(context.Request.Form["corpId"], out corpId))
                {
                    resultStr = "联系人公司转换错误";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }
            }

            NFMT.User.Model.Contact contact = new NFMT.User.Model.Contact()
            {
                 ContactName = contactName,
                 ContactCode = contactCode,
                 ContactTel = contactTel,
                 ContactFax = contactFax,
                 ContactAddress = contactAddress,
                 CompanyId = corpId
            };

            NFMT.User.BLL.ContactBLL contactBLL = new NFMT.User.BLL.ContactBLL();
            var result = contactBLL.Insert(user, contact);
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