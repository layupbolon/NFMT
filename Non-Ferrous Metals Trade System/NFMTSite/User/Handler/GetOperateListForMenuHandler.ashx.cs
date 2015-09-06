using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// GetOperateListForMenuHandler 的摘要说明
    /// </summary>
    public class GetOperateListForMenuHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

                int menuId = 0;
                if (!string.IsNullOrEmpty(context.Request.QueryString["menuId"]))
                    int.TryParse(context.Request.QueryString["menuId"], out menuId);

                int empId = 0;
                if (!string.IsNullOrEmpty(context.Request.QueryString["empId"]))
                    int.TryParse(context.Request.QueryString["empId"], out empId);

                NFMT.User.BLL.MenuBLL bll = new NFMT.User.BLL.MenuBLL();
                result = bll.GetOperateList(user, menuId, empId);

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