using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PayContractAllotStockCreateHandler 的摘要说明
    /// </summary>
    public class PayContractAllotStockCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string detailStr = context.Request.Form["rows"];
            if (string.IsNullOrEmpty(detailStr))
            {
                context.Response.Write("分配信息不能为空");
                context.Response.End();
            }

            string itemStr = context.Request.Form["item"];
            if (string.IsNullOrEmpty(itemStr))
            {
                context.Response.Write("库存财务付款明细信息不能为空");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<NFMT.Funds.Model.PaymentStockDetail> details = serializer.Deserialize<List<NFMT.Funds.Model.PaymentStockDetail>>(detailStr);
                NFMT.Funds.Model.PaymentStockDetail paymentStockDetail = serializer.Deserialize<NFMT.Funds.Model.PaymentStockDetail>(itemStr);

                foreach (NFMT.Funds.Model.PaymentStockDetail detail in details)
                {
                    detail.ContractDetailId = paymentStockDetail.ContractDetailId;
                    detail.PaymentId = paymentStockDetail.PaymentId;
                    detail.ContractId = paymentStockDetail.ContractId;
                    detail.SubId = paymentStockDetail.SubId;
                    detail.PayApplyId = paymentStockDetail.PayApplyId;
                    detail.PayApplyDetailId = paymentStockDetail.PayApplyDetailId;
                    detail.SourceFrom = paymentStockDetail.SourceFrom;
                }

                NFMT.Funds.BLL.PaymentStockDetailBLL bll = new NFMT.Funds.BLL.PaymentStockDetailBLL();
                result = bll.Insert(user, details);

                if (result.ResultStatus == 0)
                    result.Message = "分配新增成功";
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