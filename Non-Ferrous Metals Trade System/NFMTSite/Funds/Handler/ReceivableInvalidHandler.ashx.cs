﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// ReceivableInvalidHandler 的摘要说明
    /// </summary>
    public class ReceivableInvalidHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int receivableId = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                if (!int.TryParse(context.Request.Form["id"], out receivableId))
                {
                    context.Response.Write("参数错误");
                    context.Response.End();
                }
            }

            NFMT.Funds.BLL.ReceivableBLL bll = new NFMT.Funds.BLL.ReceivableBLL();
            result = bll.Invalid(user, receivableId);

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