using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BDStyleDtlUpdateHandler 的摘要说明
    /// </summary>
    public class BDStyleDtlUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string resultStr = "更新失败";

            int detailId = 0;
            string detailCode = context.Request.Form["code"];
            string detailName = context.Request.Form["name"];

            if (string.IsNullOrEmpty(context.Request.Form["detailId"]))
            {
                context.Response.Write("明细序号未知");
                context.Response.End();
            }
            if (string.IsNullOrEmpty(detailCode))
            {
                context.Response.Write("明细编号不能为空");
                context.Response.End();
            }
            if (string.IsNullOrEmpty(detailName))
            {
                context.Response.Write("明细名称不能为空");
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["detailId"], out detailId))
            {
                context.Response.Write("明细序号未知");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.Data.Model.BDStyleDetail detail = new NFMT.Data.Model.BDStyleDetail();
            detail.StyleDetailId = detailId;
            detail.DetailCode = detailCode;
            detail.DetailName = detailName;

            int status = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["status"]))
                int.TryParse(context.Request.Form["status"], out status);

            if (status <= 0)
            {
                context.Response.Write("明细状态未知");
                context.Response.End();
            }

            NFMT.Common.StatusEnum st = (NFMT.Common.StatusEnum)status;

            detail.DetailStatus = st;
            NFMT.Data.BLL.BDStyleDetailBLL bll = new NFMT.Data.BLL.BDStyleDetailBLL();
            NFMT.Common.ResultModel result = bll.Update(user, detail);
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