using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BDStyleDtlCreateHandler 的摘要说明
    /// </summary>
    public class BDStyleDtlCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string detailCode = context.Request.Form["code"];
            string detailName = context.Request.Form["name"];
            int styleId = 0;

            string resultStr = "添加失败";

            if (string.IsNullOrEmpty(context.Request.Form["pid"]))
            {
                resultStr = "父类型未知";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["pid"], out styleId))
            {
                resultStr = "父类型未知";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (string.IsNullOrEmpty(detailCode))
            {
                resultStr = "明细编号不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (string.IsNullOrEmpty(detailName))
            {
                resultStr = "明细名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            NFMT.Data.BLL.BDStyleDetailBLL bll = new NFMT.Data.BLL.BDStyleDetailBLL();
            NFMT.Data.Model.BDStyleDetail detail = new NFMT.Data.Model.BDStyleDetail();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            detail.CreatorId = user.EmpId;
            detail.BDStyleId = styleId;
            detail.DetailCode = detailCode;
            detail.DetailName = detailName;
            detail.DetailStatus = NFMT.Common.StatusEnum.已录入;

            NFMT.Common.ResultModel result = bll.Insert(user, detail);
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