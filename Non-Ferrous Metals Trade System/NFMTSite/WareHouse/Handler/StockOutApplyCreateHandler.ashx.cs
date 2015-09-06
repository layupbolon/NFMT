using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockOutApplyCreateHandler 的摘要说明
    /// </summary>
    public class StockOutApplyCreateHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            context.Response.ContentType = "text/plain";

            int subId = 0;
            string sids = string.Empty;

            if (string.IsNullOrEmpty(context.Request.Form["subId"]))
            {
                result.Message = "子合约信息错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["subId"], out subId))
            {
                result.Message = "子合约信息错误";
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
            result = applyBLL.CreateStockOutApply(user, subId, details, deptId, memo,corpId,buyCorpId);

            if (result.ResultStatus == 0)
                result.Message = "出库申请新增成功";

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