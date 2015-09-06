using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PayContractAllotToStockInvalidHandler 的摘要说明
    /// </summary>
    public class PayContractAllotToStockInvalidHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int detailId = 0;

            if (!int.TryParse(context.Request.Form["id"], out detailId) || detailId <= 0)
            {
                context.Response.Write("序号错误");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            NFMT.Funds.BLL.PaymentStockDetailBLL bll = new NFMT.Funds.BLL.PaymentStockDetailBLL();
            result = bll.Invalid(user, detailId);

            if (result.ResultStatus == 0)
                result.Message = "作废成功";

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