using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// ContactShareHandler 的摘要说明
    /// </summary>
    public class ContactShareHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            string id = context.Request.Form["id"];
            string contactIds = context.Request.Form["cid"];

            if (string.IsNullOrEmpty(contactIds))
            {
                context.Response.Write("必须选择需要共享的联系人");
                context.Response.End();
            }

            int empId = 0;

            if (!int.TryParse(id, out empId) || empId == 0)
            {
                context.Response.Write("员工转换出错");
                context.Response.End();
            }

            string[] strs = contactIds.Split('|');
            List<NFMT.User.Model.EmployeeContact> listempContact = new List<NFMT.User.Model.EmployeeContact>();
            foreach (string s in strs)
            {
                int contactId = 0;
                if (!int.TryParse(s, out contactId) || contactId == 0)
                {
                    context.Response.Write("选择联系人出错");
                    context.Response.End();
                }
                NFMT.User.Model.EmployeeContact empContact = new NFMT.User.Model.EmployeeContact()
                {
                    ContactId = contactId,
                    EmpId = empId,
                    //to do list
                };
                listempContact.Add(empContact);
            }

            NFMT.User.BLL.EmployeeContactBLL bll = new NFMT.User.BLL.EmployeeContactBLL();
            NFMT.Common.ResultModel result = bll.InsertRange(user, listempContact);
            if (result.ResultStatus == 0)
                context.Response.Write("共享成功");
            else
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