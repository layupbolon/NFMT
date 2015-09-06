using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.DoPrice.Handler
{
    /// <summary>
    /// PricingCreateHandler 的摘要说明
    /// </summary>
    public class PricingCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string pricingStr = context.Request.Form["pricing"];
            if (string.IsNullOrEmpty(pricingStr))
            {
                result.Message = "点价信息不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string detailStr = context.Request.Form["detail"];

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.DoPrice.Model.Pricing pricing = serializer.Deserialize<NFMT.DoPrice.Model.Pricing>(pricingStr);

                List<NFMT.DoPrice.Model.PricingApplyDetail> pricingApplyDetails = new List<NFMT.DoPrice.Model.PricingApplyDetail>();

                if (!string.IsNullOrEmpty(detailStr))
                    pricingApplyDetails = serializer.Deserialize<List<NFMT.DoPrice.Model.PricingApplyDetail>>(detailStr);
                if (pricing == null || pricingApplyDetails == null)
                {
                    result.Message = "数据错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                List<NFMT.DoPrice.Model.PricingDetail> details = new List<NFMT.DoPrice.Model.PricingDetail>();
                decimal sumWeight = 0;
                foreach (NFMT.DoPrice.Model.PricingApplyDetail detail in pricingApplyDetails)
                {
                    NFMT.DoPrice.Model.PricingDetail pricingDetail = new NFMT.DoPrice.Model.PricingDetail()
                    {
                        PricingApplyId = pricing.PricingApplyId,
                        PricingApplyDetailId = detail.DetailId,
                        StockId = detail.StockId,
                        StockLogId = detail.StockLogId,
                        PricingWeight = detail.PricingWeight,
                        AvgPrice = pricing.AvgPrice,
                        PricingTime = DateTime.Now,
                        Pricinger = user.EmpId
                    };
                    details.Add(pricingDetail);

                    sumWeight += detail.PricingWeight;
                }

                if (sumWeight > 0)
                    pricing.PricingWeight = sumWeight;

                pricing.PricingTime = DateTime.Now;
                pricing.Pricinger = user.EmpId;
                pricing.FinalPrice = pricing.DelayFee + pricing.Spread + pricing.OtherFee + pricing.AvgPrice;

                NFMT.DoPrice.BLL.PricingBLL bll = new NFMT.DoPrice.BLL.PricingBLL();
                result = bll.Create(user, pricing, details);
                if (result.ResultStatus == 0)
                {
                    result.Message = "新增成功";
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