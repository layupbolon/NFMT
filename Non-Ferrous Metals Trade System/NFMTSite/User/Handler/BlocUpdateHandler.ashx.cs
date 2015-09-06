using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// BlocUpdateHandler 的摘要说明
    /// </summary>
    public class BlocUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";

            string blocName = context.Request.Form["name"];
            string blocEName = context.Request.Form["ename"];
            string blocFName = context.Request.Form["fname"];
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

            if (string.IsNullOrEmpty(blocName))
            {
                resultStr = "集团名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (string.IsNullOrEmpty(blocEName))
            {
                resultStr = "集团英文名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (string.IsNullOrEmpty(blocFName))
            {
                resultStr = "明细编号不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            NFMT.User.BLL.BlocBLL blocBLL = new NFMT.User.BLL.BlocBLL();

            NFMT.User.Model.Bloc bloc = new NFMT.User.Model.Bloc();
            bloc.BlocId = id;
            bloc.BlocName = blocName;
            bloc.BlocEname = blocEName;
            bloc.BlocFullName = blocFName;

            var result = blocBLL.Update(user, bloc);
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