using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// CorpUpdateHandler 的摘要说明
    /// </summary>
    public class CorpUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            context.Response.ContentType = "text/plain";

            string corpStr = context.Request.Form["corp"];

            if (string.IsNullOrEmpty(corpStr))
            {
                context.Response.Write("公司内容为空");
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.User.Model.Corporation corp = serializer.Deserialize<NFMT.User.Model.Corporation>(corpStr);
                if (corp == null)
                {
                    context.Response.Write("数据错误");
                    context.Response.End();
                }
                NFMT.User.BLL.CorporationBLL bll = new NFMT.User.BLL.CorporationBLL();
                result = bll.Update(user, corp);
                if (result.ResultStatus == 0)
                {
                    result.Message = "修改成功";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
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