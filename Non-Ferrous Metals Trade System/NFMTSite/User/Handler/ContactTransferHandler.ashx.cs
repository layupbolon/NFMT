using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// ContactTransferHandler 的摘要说明
    /// </summary>
    public class ContactTransferHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            string id = context.Request.Form["id"];
            string contactIds = context.Request.Form["cid"];
            string ECIDS = context.Request.Form["ecIDs"];

            if (string.IsNullOrEmpty(contactIds))
            {
                context.Response.Write("必须选择需要共享的联系人");
                context.Response.End();
            }

            if (string.IsNullOrEmpty(ECIDS))
            {
                context.Response.Write("必须选择需要共享的联系人");
                context.Response.End();
            }

            int empId = 0;
            int oldEmpId = 0;

            if (!int.TryParse(id, out empId) || empId == 0)
            {
                context.Response.Write("员工转换出错");
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["oldId"], out oldEmpId) || oldEmpId == 0)
            {
                context.Response.Write("员工转换出错");
                context.Response.End();
            }

            string[] strs = contactIds.Split('|');
            string[] ecs = ECIDS.Split('|');
            List<NFMT.User.Model.EmployeeContact> listInsert = new List<NFMT.User.Model.EmployeeContact>();

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
                listInsert.Add(empContact);
            }

            List<NFMT.User.Model.EmployeeContact> listInvalid = new List<NFMT.User.Model.EmployeeContact>();
            foreach (string s in ecs)
            {
                int ec = 0;
                if (!int.TryParse(s, out ec) || ec == 0)
                {
                    context.Response.Write("选择联系人出错");
                    context.Response.End();
                }
                NFMT.User.Model.EmployeeContact empContact = new NFMT.User.Model.EmployeeContact()
                {
                    ECId = ec
                };
                listInvalid.Add(empContact);
            }

            NFMT.User.BLL.EmployeeContactBLL bll = new NFMT.User.BLL.EmployeeContactBLL();
            NFMT.Common.ResultModel result = bll.ContactTransferHandler(user, listInsert, listInvalid);
            if (result.ResultStatus == 0)
                context.Response.Write("转移成功");
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