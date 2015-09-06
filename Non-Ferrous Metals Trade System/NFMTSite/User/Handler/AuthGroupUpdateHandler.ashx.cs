using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// AuthGroupUpdateHandler 的摘要说明
    /// </summary>
    public class AuthGroupUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string authGroupStr = context.Request.Form["authGroup"];
            if (string.IsNullOrEmpty(authGroupStr))
            {
                context.Response.Write("权限组信息不能为空");
                context.Response.End();
            }
            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.User.Model.AuthGroup authGroup = serializer.Deserialize<NFMT.User.Model.AuthGroup>(authGroupStr);
                if (authGroup == null)
                {
                    context.Response.Write("数据错误");
                    context.Response.End();
                }
                NFMT.User.BLL.AuthGroupBLL bll = new NFMT.User.BLL.AuthGroupBLL();
                result = bll.Update(user, authGroup);
                if (result.ResultStatus == 0)
                {
                    result.Message = "权限组修改成功";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
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