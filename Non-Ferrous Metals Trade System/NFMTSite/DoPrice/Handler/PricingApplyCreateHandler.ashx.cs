using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.DoPrice.Handler
{
    /// <summary>
    /// PricingApplyCreateHandler 的摘要说明
    /// </summary>
    public class PricingApplyCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string applyStr = context.Request.Form["apply"];
            if (string.IsNullOrEmpty(applyStr))
            {
                result.Message = "申请信息不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string pricingApplyStr = context.Request.Form["pricingApply"];
            if (string.IsNullOrEmpty(pricingApplyStr))
            {
                result.Message = "点价申请内容不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string detailStr = context.Request.Form["detail"];
            if (string.IsNullOrEmpty(detailStr))
            {
                result.Message = "点价申请明细内容不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Operate.Model.Apply apply = serializer.Deserialize<NFMT.Operate.Model.Apply>(applyStr);
                NFMT.DoPrice.Model.PricingApply pricingApply = serializer.Deserialize<NFMT.DoPrice.Model.PricingApply>(pricingApplyStr);
                List<NFMT.DoPrice.Model.PricingApplyDetail> pricingApplyDetails = serializer.Deserialize<List<NFMT.DoPrice.Model.PricingApplyDetail>>(detailStr);
                if (apply == null || pricingApply == null || pricingApplyDetails == null)
                {
                    result.Message = "数据错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                List<NFMT.DoPrice.Model.PricingApplyDetail> details = new List<NFMT.DoPrice.Model.PricingApplyDetail>();
                foreach (NFMT.DoPrice.Model.PricingApplyDetail detail in pricingApplyDetails)
                {
                    if (detail.PricingWeight != 0)
                        details.Add(detail);
                }

                NFMT.DoPrice.BLL.PricingApplyBLL bll = new NFMT.DoPrice.BLL.PricingApplyBLL();
                result = bll.Create(user, apply, pricingApply, details);
                if (result.ResultStatus == 0)
                {
                    result.Message = "点价申请新增成功";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
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