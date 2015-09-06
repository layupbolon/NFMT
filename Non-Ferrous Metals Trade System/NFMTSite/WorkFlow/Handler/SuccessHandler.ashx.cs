using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Reflection;

namespace NFMTSite.WorkFlow.Handler
{
    /// <summary>
    /// SuccessHandler 的摘要说明
    /// </summary>
    public class SuccessHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //NFMT.Common.UserModel user = context.Session["UserModel"] as NFMT.Common.UserModel;
        //to do list
        NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            if (string.IsNullOrEmpty(context.Request.Form["source"]))
            {
                result.Message = "数据源为空";
                result.ResultStatus = -1;
                context.Response.Write(serializer.Serialize(result));
                context.Response.End();
            }

            bool isPass = false;
            if (string.IsNullOrEmpty(context.Request.Form["ispass"]) || !bool.TryParse(context.Request.Form["ispass"], out isPass))
            {
                result.Message = "审核操作错误";
                result.ResultStatus = -1;
                context.Response.Write(serializer.Serialize(result));
                context.Response.End();
            }

            try
            {
                string jsonData = context.Request.Form["source"];
                var obj = serializer.Deserialize<NFMT.WorkFlow.Model.DataSource>(jsonData);

                NFMT.WorkFlow.BLL.DataSourceBLL bll = new NFMT.WorkFlow.BLL.DataSourceBLL();

                result = bll.BaseAudit(user, obj, isPass);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }

            context.Response.Write(serializer.Serialize(result));
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