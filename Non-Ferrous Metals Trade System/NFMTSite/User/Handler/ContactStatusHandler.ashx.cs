﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// ContactStatusHandler 的摘要说明
    /// </summary>
    public class ContactStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";
            int id = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["id"], out id) || id <= 0)
            {
                context.Response.Write("序号错误");
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["oi"], out operateId) || operateId <= 0)
            {
                context.Response.Write("操作错误");
                context.Response.End();
            }

            NFMT.User.BLL.ContactBLL bll = new NFMT.User.BLL.ContactBLL();
            NFMT.User.Model.Contact contact = new NFMT.User.Model.Contact()
            {
                 ContactId = id,
                 LastModifyId = user.EmpId
            };

            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.作废:
                    result = bll.Invalid(user, contact);
                    break;
                case NFMT.Common.OperateEnum.撤返:
                    result = bll.GoBack(user, contact);
                    break;
                case NFMT.Common.OperateEnum.冻结:
                    result = bll.Freeze(user, contact);
                    break;
                case NFMT.Common.OperateEnum.解除冻结:
                    result = bll.UnFreeze(user, contact);
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