using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockOutApplyUpdateHandler 的摘要说明
    /// </summary>
    public class StockOutApplyUpdateHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int stockOutApplyId = 0;
            string sids = string.Empty;

            if (string.IsNullOrEmpty(context.Request.Form["outApplyId"]))
            {
                result.Message = "出库申请信息错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["outApplyId"], out stockOutApplyId))
            {
                result.Message = "出库申请信息错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string detailStr = context.Request.Form["detail"];
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<NFMT.WareHouse.Model.StockOutApplyDetail> details = new List<NFMT.WareHouse.Model.StockOutApplyDetail>();
            if (!string.IsNullOrEmpty(detailStr))
                details = serializer.Deserialize<List<NFMT.WareHouse.Model.StockOutApplyDetail>>(detailStr);

            int deptId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["deptId"]))
            {
                result.Message = "部门信息错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["deptId"], out deptId))
            {
                result.Message = "部门信息错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            int corpId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["corpId"]) || !int.TryParse(context.Request.Form["corpId"], out corpId) || corpId <= 0)
            {
                result.Message = "申请公司错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            int buyCorpId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["buyCorpId"]) || !int.TryParse(context.Request.Form["buyCorpId"], out buyCorpId) || buyCorpId <= 0)
            {
                result.Message = "收货公司错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string memo = context.Request.Form["memo"];

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.WareHouse.BLL.StockOutApplyBLL applyBLL = new NFMT.WareHouse.BLL.StockOutApplyBLL();
            result = applyBLL.UpdateStockOutApply(user, stockOutApplyId, details, deptId, memo,corpId,buyCorpId);

            if (result.ResultStatus == 0)
                result.Message = "出库申请更新成功";

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            context.Response.Write(jsonStr);
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