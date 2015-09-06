using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// BlocCreateHandler 的摘要说明
    /// </summary>
    public class BlocCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";

            string blocName = context.Request.Form["name"];
            string blocEName = context.Request.Form["ename"];
            string blocFName = context.Request.Form["fname"];

            string resultStr = "添加失败";

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

            bool isSelf = true;
            NFMT.User.BLL.BlocBLL blocBLL = new NFMT.User.BLL.BlocBLL();
            NFMT.Common.ResultModel result = blocBLL.IsExistSelfBolc(user);
            if (result.ResultStatus != 0)
                isSelf = false;

            NFMT.User.Model.Bloc bloc = new NFMT.User.Model.Bloc()
            {
                BlocName = blocName,
                BlocEname = blocEName,
                BlocFullName = blocFName,
                IsSelf = isSelf
            };
            
            result = blocBLL.Insert(user, bloc);
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