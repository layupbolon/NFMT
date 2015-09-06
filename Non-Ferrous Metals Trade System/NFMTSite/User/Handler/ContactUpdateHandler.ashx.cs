using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// ContactUpdateHandler 的摘要说明
    /// </summary>
    public class ContactUpdateHandler : IHttpHandler
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
            int id = 0;

            string resultStr = "修改失败";

            if (string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                resultStr = "id未知";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["id"], out id))
            {
                resultStr = "id未知";
                context.Response.Write(resultStr);
                context.Response.End();
            }

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

            NFMT.User.BLL.ContactBLL contactBLL = new NFMT.User.BLL.ContactBLL();

            NFMT.User.Model.Contact contact = new NFMT.User.Model.Contact();
            contact.ContactName = contactName;
            contact.ContactCode = contactCode;
            contact.ContactTel = contactTel;
            contact.ContactFax = contactFax;
            contact.ContactAddress = contactAddress;
            contact.CompanyId = corpId;
            contact.ContactId = id;

            var result = contactBLL.Update(user, contact);
            resultStr = result.Message;

            context.Response.Write(resultStr);
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