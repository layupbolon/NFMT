using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockInCreateHandler 的摘要说明
    /// </summary>
    public class StockInCreateHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            context.Response.ContentType = "text/plain";

            string s = context.Request.Form["StockIn"];
            if (string.IsNullOrEmpty(s))
            {
                result.Message = "入库信息不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            int isInsertRef = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["IsInsertRef"]))
            {
                if (!int.TryParse(context.Request.Form["IsInsertRef"], out isInsertRef))
                {
                    result.Message = "参数错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }
            }
            else
            {
                result.Message = "参数错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string isSubmitAuditStr = context.Request.Form["IsSubmitAudit"];
            bool isSubmitAudit = false;
            if (string.IsNullOrEmpty(isSubmitAuditStr) || !bool.TryParse(isSubmitAuditStr, out isSubmitAudit))
                isSubmitAudit = false;

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.WareHouse.Model.StockIn stockIn = serializer.Deserialize<NFMT.WareHouse.Model.StockIn>(s);
                if (stockIn == null)
                {
                    result.Message = "入库信息错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }
                
                int subId = 0;
                if (isInsertRef == 1)
                {
                    string strSubId = context.Request.Form["ContractSubId"];
                    if (string.IsNullOrEmpty(strSubId) || !int.TryParse(strSubId, out subId) || subId == 0)
                    {
                        result.Message = "子合约序号错误";
                        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                        context.Response.End();
                    }
                }
                
                NFMT.WareHouse.BLL.StockInBLL bll = new NFMT.WareHouse.BLL.StockInBLL();
                result = bll.Create(user, stockIn, subId, isSubmitAudit);
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            if (result.ResultStatus == 0)
                result.Message = "入库登记成功";

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