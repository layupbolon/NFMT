using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Document.Handler
{
    /// <summary>
    /// DocumentUpdateHandler 的摘要说明
    /// </summary>
    public class DocumentUpdateHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            string documentStr = context.Request.Form["document"];
            string docStocksStr = context.Request.Form["docStocks"];

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            NFMT.Document.Model.Document document = serializer.Deserialize<NFMT.Document.Model.Document>(documentStr);
            List<NFMT.Document.Model.DocumentStock> docStocks = serializer.Deserialize<List<NFMT.Document.Model.DocumentStock>>(docStocksStr);
            
            NFMT.Document.BLL.DocumentBLL bll = new NFMT.Document.BLL.DocumentBLL();
            result = bll.Update(user, document, docStocks);

            if (result.ResultStatus == 0)
                result.Message = "制单修改成功";

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