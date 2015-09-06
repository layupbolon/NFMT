using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.DoPrice.Handler
{
    /// <summary>
    /// PriceConfirmUpdateHandler 的摘要说明
    /// </summary>
    public class PriceConfirmUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string priceConfirmStr = context.Request.Form["priceConfirm"];
            if (string.IsNullOrEmpty(priceConfirmStr))
            {
                result.Message = "价格确认单信息不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string rowsStr = context.Request.Form["rows"];
            if (string.IsNullOrEmpty(rowsStr))
            {
                result.Message = "价格确认单明细信息不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.DoPrice.Model.PriceConfirm priceConfirm = serializer.Deserialize<NFMT.DoPrice.Model.PriceConfirm>(priceConfirmStr);
                List<NFMT.DoPrice.Model.PriceConfirmDetail> details = serializer.Deserialize<List<NFMT.DoPrice.Model.PriceConfirmDetail>>(rowsStr);
                if (priceConfirm == null || details == null || !details.Any())
                {
                    result.Message = "数据错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                foreach (NFMT.DoPrice.Model.PriceConfirmDetail detail in details)
                {
                    detail.SettlePrice = priceConfirm.SettlePrice;
                    detail.SettleBala = detail.SettlePrice * detail.ConfirmAmount;
                }

                NFMT.DoPrice.BLL.PriceConfirmBLL bll = new NFMT.DoPrice.BLL.PriceConfirmBLL();
                result = bll.Update(user, priceConfirm, details);
                if (result.ResultStatus == 0)
                {
                    result.Message = "价格确认单修改成功";
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