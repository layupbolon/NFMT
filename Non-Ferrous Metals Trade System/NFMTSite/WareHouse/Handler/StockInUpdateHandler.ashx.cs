using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockInUpdateHandler 的摘要说明
    /// </summary>
    public class StockInUpdateHandler : IHttpHandler
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

                if (stockIn.StockInId <= 0)
                {
                    result.Message = "参数错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                if (stockIn.CorpId <= 0)
                {
                    result.Message = "入账公司错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                if (stockIn.NetAmount <= 0)
                {
                    result.Message = "净重错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                if (stockIn.GrossAmount <= 0 || stockIn.GrossAmount < stockIn.NetAmount)
                {
                    result.Message = "毛重错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                if (stockIn.UintId <= 0)
                {
                    result.Message = "计量单位错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                if (stockIn.AssetId <= 0)
                {
                    result.Message = "品种错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                if (stockIn.DeliverPlaceId <= 0)
                {
                    result.Message = "交货地错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }
                if (stockIn.ProducerId <= 0)
                {
                    result.Message = "生厂商错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                if (stockIn.PaperHolder <= 0)
                {
                    result.Message = "单据保管人错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                if (string.IsNullOrEmpty(stockIn.RefNo))
                {
                    result.Message = "业务单号不可为空";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                NFMT.WareHouse.BLL.StockInBLL bll = new NFMT.WareHouse.BLL.StockInBLL();
                result = bll.StockInUpdate(user, stockIn);
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