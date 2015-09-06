using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BDStyleDtlFreezeHandler 的摘要说明
    /// </summary>
    public class BDStyleDtlFreezeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string resultStr = "更新失败";

            int detailId = 0;
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            if (!int.TryParse(context.Request.Form["detailId"], out detailId))
            {
                context.Response.Write("明细序号未知");
                context.Response.End();
            }

            NFMT.Data.Model.BDStyleDetail detail = new NFMT.Data.Model.BDStyleDetail();
            detail.StyleDetailId = detailId;

            NFMT.Data.BLL.BDStyleDetailBLL bll = new NFMT.Data.BLL.BDStyleDetailBLL();
            NFMT.Common.ResultModel result = bll.Freeze(user, detail);
            resultStr = result.Message;

            context.Response.Write(resultStr);
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