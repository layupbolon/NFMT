using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockInAuditHandler 的摘要说明
    /// </summary>
    public class StockInAuditHandler : IHttpHandler
    {       
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            if (string.IsNullOrEmpty(context.Request.Form["source"]))
            {
                result.Message = "数据源为空";
                result.ResultStatus = -1;
                context.Response.Write(serializer.Serialize(result));
                context.Response.End();
            }

            bool isPass = false;
            if (string.IsNullOrEmpty(context.Request.Form["ispass"]) || !bool.TryParse(context.Request.Form["ispass"],out isPass))
            {
                result.Message = "审核结果错误";
                result.ResultStatus = -1;
                context.Response.Write(serializer.Serialize(result));
                context.Response.End();
            }

            try
            {
                string jsonData = context.Request.Form["source"];
                var obj = serializer.Deserialize<NFMT.WorkFlow.Model.DataSource>(jsonData);

                NFMT.WareHouse.BLL.StockInBLL bll = new NFMT.WareHouse.BLL.StockInBLL();
                result = bll.Audit(user, obj, isPass);
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