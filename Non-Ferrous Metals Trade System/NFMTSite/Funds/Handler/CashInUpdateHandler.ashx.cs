using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// CashInUpdateHandler 的摘要说明
    /// </summary>
    public class CashInUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string r = context.Request.Form["CashIn"];
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
                NFMT.Funds.Model.CashIn cashIn = serializer.Deserialize<NFMT.Funds.Model.CashIn>(r);
                if (cashIn == null)
                {
                    context.Response.Write("收款信息错误");
                    context.Response.End();
                }

                NFMT.Funds.BLL.CashInBLL bll = new NFMT.Funds.BLL.CashInBLL();
                result = bll.Update(user, cashIn);
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            if (result.ResultStatus == 0)
                result.Message = "收款修改成功";

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