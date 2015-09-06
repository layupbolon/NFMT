using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Invoice.Handler
{
    /// <summary>
    /// SIUpdateHandler 的摘要说明
    /// </summary>
    public class SIUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string invoiceStr = context.Request.Form["invoice"];
            if (string.IsNullOrEmpty(invoiceStr))
            {
                context.Response.Write("发票不能为空");
                context.Response.End();
            }

            string SIstr = context.Request.Form["SI"];
            if (string.IsNullOrEmpty(SIstr))
            {
                context.Response.Write("价外票内容不能为空");
                context.Response.End();
            }

            string SIDetailstr = context.Request.Form["SIDetail"];
            if (string.IsNullOrEmpty(SIDetailstr))
            {
                context.Response.Write("价外票明细内容不能为空");
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Operate.Model.Invoice invoice = serializer.Deserialize<NFMT.Operate.Model.Invoice>(invoiceStr);
                NFMT.Invoice.Model.SI si = serializer.Deserialize<NFMT.Invoice.Model.SI>(SIstr);
                List<NFMT.Invoice.Model.SIDetail> siDetails = serializer.Deserialize<List<NFMT.Invoice.Model.SIDetail>>(SIDetailstr);
                if (invoice == null || si == null || siDetails == null)
                {
                    context.Response.Write("数据错误");
                    context.Response.End();
                }

                NFMT.Invoice.BLL.SIBLL bll = new NFMT.Invoice.BLL.SIBLL();
                result = bll.Update(user, invoice, si, siDetails);
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