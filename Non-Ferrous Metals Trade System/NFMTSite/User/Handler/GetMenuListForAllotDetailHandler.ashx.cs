using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// GetMenuListForAllotDetailHandler 的摘要说明
    /// </summary>
    public class GetMenuListForAllotDetailHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                int empId = 0;
                if (!string.IsNullOrEmpty(context.Request.QueryString["empId"]))
                    int.TryParse(context.Request.QueryString["empId"], out empId);

                string menuIds = context.Request.QueryString["menuIds"];

                NFMT.User.BLL.AuthOperateBLL bll = new NFMT.User.BLL.AuthOperateBLL();
                result = bll.GetMenuList(user, empId, menuIds);

                context.Response.ContentType = "application/json; charset=utf-8";
                if (result.ResultStatus != 0)
                {
                    context.Response.Write(result.Message);
                    context.Response.End();
                }

                context.Response.Write(result.ReturnValue);
            }
            catch (Exception e)
            {
                context.Response.Write(e.Message);
            }
            
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