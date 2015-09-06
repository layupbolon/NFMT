using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PayApplyStockCreateHandler 的摘要说明
    /// </summary>
    public class PayApplyStockCreateHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string payApplyStr = context.Request.Form["PayApply"];

            if (string.IsNullOrEmpty(payApplyStr))
            {
                result.Message = "付款申请不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string memo = context.Request.Form["memo"];
            int deptId = 0;
            int corpId = 0;

            if (string.IsNullOrEmpty(context.Request.Form["deptId"]) || !int.TryParse(context.Request.Form["deptId"], out deptId))
            {
                result.Message = "未选择申请部门";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (string.IsNullOrEmpty(context.Request.Form["corpId"]) || !int.TryParse(context.Request.Form["corpId"], out corpId))
            {
                result.Message = "未选择申请公司";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string stockStr = context.Request.Form["Stocks"];

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Funds.Model.PayApply payApply = serializer.Deserialize<NFMT.Funds.Model.PayApply>(payApplyStr);

                char[] splitStr = new char[1];
                splitStr[0] = '|';
                List<NFMT.Funds.Model.StockPayApply> stockPayApplies = new List<NFMT.Funds.Model.StockPayApply>();
                string[] strs = stockStr.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in strs)
                {
                    var obj = serializer.Deserialize<NFMT.Funds.Model.StockPayApply>(s);
                    stockPayApplies.Add(obj);
                }

                decimal sumBala = stockPayApplies.Sum(temp => temp.ApplyBala);
                if (sumBala != payApply.ApplyBala)
                {
                    result.Message = "付款申请总计与分项合计金额不相等";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                NFMT.Funds.BLL.PayApplyBLL bll = new NFMT.Funds.BLL.PayApplyBLL();
                result = bll.PayApplyStockCreate(user, payApply,stockPayApplies, memo, deptId,corpId);

                if (result.ResultStatus == 0)
                {
                    result.Message = "付款申请添加成功";
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