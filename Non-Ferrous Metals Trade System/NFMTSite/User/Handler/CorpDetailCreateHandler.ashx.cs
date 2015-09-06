using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// CorpDetailCreateHandler 的摘要说明
    /// </summary>
    public class CorpDetailCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string corpStr = context.Request.Form["corp"];
            if (string.IsNullOrEmpty(corpStr))
            {
                context.Response.Write("公司主信息不能为空");
                context.Response.End();
            }

            string corpDetailStr = context.Request.Form["corpDetail"];
            if (string.IsNullOrEmpty(corpDetailStr))
            {
                context.Response.Write("公司明细不能为空");
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.User.Model.Corporation corp = serializer.Deserialize<NFMT.User.Model.Corporation>(corpStr);
                NFMT.User.Model.CorporationDetail corpDetail = serializer.Deserialize<NFMT.User.Model.CorporationDetail>(corpDetailStr);
                if (corp == null || corpDetail == null)
                {
                    context.Response.Write("数据错误");
                    context.Response.End();
                }
                NFMT.User.BLL.CorporationDetailBLL bll = new NFMT.User.BLL.CorporationDetailBLL();
                result = bll.Create(user, corp, corpDetail);
                if (result.ResultStatus == 0)
                    result.Message = "客户新增成功";
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