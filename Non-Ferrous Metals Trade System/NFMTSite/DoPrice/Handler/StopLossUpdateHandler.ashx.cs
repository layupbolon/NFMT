using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.DoPrice.Handler
{
    /// <summary>
    /// StopLossUpdateHandler 的摘要说明
    /// </summary>
    public class StopLossUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string stopLossStr = context.Request.Form["stopLoss"];
            if (string.IsNullOrEmpty(stopLossStr))
            {
                result.Message = "止损信息不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string detailStr = context.Request.Form["detail"];

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.DoPrice.Model.StopLoss stopLoss = serializer.Deserialize<NFMT.DoPrice.Model.StopLoss>(stopLossStr);

                List<StopLossInfo> stopLossDetails = null;

                if (!string.IsNullOrEmpty(detailStr))
                    stopLossDetails = serializer.Deserialize<List<StopLossInfo>>(detailStr);
                if (stopLoss == null)
                {
                    result.Message = "数据错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                List<NFMT.DoPrice.Model.StopLossDetail> details = new List<NFMT.DoPrice.Model.StopLossDetail>();
                if (stopLossDetails != null && stopLossDetails.Any())
                {
                    foreach (StopLossInfo info in stopLossDetails)
                    {
                        if (info.StopLossWeight <= 0) continue;

                        details.Add(new NFMT.DoPrice.Model.StopLossDetail()
                        {
                            StopLossId = stopLoss.StopLossId,
                            StopLossApplyId = stopLoss.StopLossApplyId,
                            StopLossApplyDetailId = info.DetailId,
                            StockId = info.DetailId,
                            StockLogId = info.StockLogId,
                            StopLossWeight = info.StopLossWeight,
                            AvgPrice = stopLoss.AvgPrice,
                            StopLossTime = DateTime.Now,
                            StopLosser = user.EmpId,
                            DetailStatus = NFMT.Common.StatusEnum.已生效
                        });
                    }
                }

                NFMT.DoPrice.BLL.StopLossBLL bll = new NFMT.DoPrice.BLL.StopLossBLL();
                result = bll.Update(user, stopLoss, details);
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