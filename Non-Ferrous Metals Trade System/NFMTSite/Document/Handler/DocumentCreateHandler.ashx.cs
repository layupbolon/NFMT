using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Document.Handler
{
    /// <summary>
    /// DocumentCreateHandler 的摘要说明
    /// </summary>
    public class DocumentCreateHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            string documentStr = context.Request.Form["document"];
            string docStocksStr = context.Request.Form["docStocks"];
            string isSubmitAuditStr = context.Request.Form["IsSubmitAudit"];

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            NFMT.Document.Model.Document document = serializer.Deserialize<NFMT.Document.Model.Document>(documentStr);
            List<NFMT.Document.Model.DocumentStock> docStocks = serializer.Deserialize<List<NFMT.Document.Model.DocumentStock>>(docStocksStr);

            bool isSubmitAudit = false;
            if (string.IsNullOrEmpty(isSubmitAuditStr) || !bool.TryParse(isSubmitAuditStr, out isSubmitAudit))
                isSubmitAudit = false;

            NFMT.Document.BLL.DocumentBLL bll = new NFMT.Document.BLL.DocumentBLL();
            result = bll.Create(user, document, docStocks, isSubmitAudit);

            if (result.ResultStatus == 0)
                result.Message = "制单新增成功";

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            context.Response.Write(jsonStr);
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