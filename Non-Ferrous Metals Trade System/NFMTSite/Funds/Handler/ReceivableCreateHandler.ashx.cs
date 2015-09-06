using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// ReceivableCreateHandler 的摘要说明
    /// </summary>
    public class ReceivableCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string r = context.Request.Form["Receivable"];
            if (string.IsNullOrEmpty(r))
            {
                context.Response.Write("收款信息不能为空");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Funds.Model.Receivable receivable = serializer.Deserialize<NFMT.Funds.Model.Receivable>(r);
                if (receivable == null)
                {
                    context.Response.Write("收款信息错误");
                    context.Response.End();
                }

                NFMT.Funds.BLL.ReceivableBLL bll = new NFMT.Funds.BLL.ReceivableBLL();
                result = bll.Create(user, receivable);               
            }
            catch (Exception e)
            {
                result.ResultStatus=-1;
                result.Message = e.Message;
            }

            if (result.ResultStatus == 0)
                result.Message = "收款登记新增成功";

            context.Response.Write(result.Message);
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