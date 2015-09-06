using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PayApplyUpdateHandler 的摘要说明
    /// </summary>
    public class PayApplyUpdateHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string applyStr = context.Request.Form["Apply"];
            if (string.IsNullOrEmpty(applyStr))
            {
                result.Message = "申请不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string payApplyStr = context.Request.Form["PayApply"];
            if (string.IsNullOrEmpty(payApplyStr))
            {
                result.Message = "付款申请不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string stockAppsStr = context.Request.Form["StockApps"];

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                NFMT.Operate.Model.Apply apply = serializer.Deserialize<NFMT.Operate.Model.Apply>(applyStr);
                NFMT.Funds.Model.PayApply payApply = serializer.Deserialize<NFMT.Funds.Model.PayApply>(payApplyStr);
                List<NFMT.Funds.Model.StockPayApply> stockDetails = new List<NFMT.Funds.Model.StockPayApply>();
                if (!string.IsNullOrEmpty(stockAppsStr))
                    stockDetails = serializer.Deserialize<List<NFMT.Funds.Model.StockPayApply>>(stockAppsStr);

                NFMT.Funds.BLL.PayApplyBLL bll = new NFMT.Funds.BLL.PayApplyBLL();
                result = bll.PayApplyUpdate(user, apply, payApply, stockDetails);

                if (result.ResultStatus == 0)
                {
                    result.Message = "付款申请修改成功";
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