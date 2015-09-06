using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.DoPrice.Handler
{
    /// <summary>
    /// StopLossApplyCreateHandler 的摘要说明
    /// </summary>
    public class StopLossApplyCreateHandler : IHttpHandler
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

            string stopLossApplyStr = context.Request.Form["stopLossApply"];
            if (string.IsNullOrEmpty(stopLossApplyStr))
            {
                result.Message = "止损申请内容不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string detailStr = context.Request.Form["detail"];

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Operate.Model.Apply apply = serializer.Deserialize<NFMT.Operate.Model.Apply>(applyStr);
                NFMT.DoPrice.Model.StopLossApply stopLossApply = serializer.Deserialize<NFMT.DoPrice.Model.StopLossApply>(stopLossApplyStr);

                List<StopLossInfo> stopLossInfos = null;
                if (!string.IsNullOrEmpty(detailStr))
                    stopLossInfos = serializer.Deserialize<List<StopLossInfo>>(detailStr);

                if (apply == null || stopLossApply == null)
                {
                    result.Message = "数据错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                List<NFMT.DoPrice.Model.StopLossApplyDetail> details = new List<NFMT.DoPrice.Model.StopLossApplyDetail>();
                if (stopLossInfos != null)
                {
                    foreach (StopLossInfo info in stopLossInfos)
                    {
                        if (info.StopLossWeight <= 0) continue;

                        details.Add(new NFMT.DoPrice.Model.StopLossApplyDetail()
                            {
                                //StopLossApplyId
                                //ApplyId
                                PricingDetailId = info.DetailId,
                                StockId = info.StockId,
                                StockLogId = info.StockLogId,
                                StopLossWeight = info.StopLossWeight,
                                DetailStatus = NFMT.Common.StatusEnum.已生效
                            });
                    }
                }

                NFMT.DoPrice.BLL.StopLossApplyBLL bll = new NFMT.DoPrice.BLL.StopLossApplyBLL();
                result = bll.Create(user, apply, stopLossApply, details);
                if (result.ResultStatus == 0)
                {
                    result.Message = "止损申请新增成功";
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

        public class StopLossInfo
        {
            public int DetailId { get; set; }
            public int StockId { get; set; }
            public int StockLogId { get; set; }
            public decimal StopLossWeight { get; set; }
        }
    }
}