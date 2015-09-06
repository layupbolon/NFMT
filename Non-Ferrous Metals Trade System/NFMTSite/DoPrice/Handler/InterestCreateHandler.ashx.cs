using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.DoPrice.Handler
{
    /// <summary>
    /// InterestCreateHandler 的摘要说明
    /// </summary>
    public class InterestCreateHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string interestStr = context.Request.Form["Interest"];
            if (string.IsNullOrEmpty(interestStr))
            {
                result.Message = "利息结算信息不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string detailsStr = context.Request.Form["InterestDetail"];
            if (string.IsNullOrEmpty(detailsStr))
            {
                result.Message = "利息结算明细信息不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            bool isSubmitAudit = false;
            if (string.IsNullOrEmpty(context.Request.Form["IsSubmitAudit"]) || !bool.TryParse(context.Request.Form["IsSubmitAudit"], out isSubmitAudit))
                isSubmitAudit = false;

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                NFMT.DoPrice.Model.Interest interest = serializer.Deserialize<NFMT.DoPrice.Model.Interest>(interestStr);

                List<NFMT.DoPrice.Model.InterestDetail> interestDetails = serializer.Deserialize<List<NFMT.DoPrice.Model.InterestDetail>>(detailsStr);

                if (interest == null || interestDetails == null || interestDetails.Count == 0)
                {
                    result.Message = "数据错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                NFMT.DoPrice.BLL.InterestBLL bll = new NFMT.DoPrice.BLL.InterestBLL();
                result = bll.Create(user,interest,interestDetails, isSubmitAudit);
                if (result.ResultStatus == 0)
                {
                    result.Message = "利息结算新增成功";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
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