using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockReceiptUpdateHandler 的摘要说明
    /// </summary>
    public class StockReceiptUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string stockReceiptStr = context.Request.Form["sr"];
            if (string.IsNullOrEmpty(stockReceiptStr))
            {
                result.Message = "回执不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string detailStr = context.Request.Form["ds"];
            if (string.IsNullOrEmpty(detailStr))
            {
                result.Message = "明细不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.WareHouse.Model.StockReceipt stockReceipt = serializer.Deserialize<NFMT.WareHouse.Model.StockReceipt>(stockReceiptStr);

                char[] splitStr = new char[1];
                splitStr[0] = '|';
                List<NFMT.WareHouse.Model.StockReceiptDetail> details = new List<NFMT.WareHouse.Model.StockReceiptDetail>();
                string[] strs = detailStr.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in strs)
                {
                    var obj = serializer.Deserialize<NFMT.WareHouse.Model.StockReceiptDetail>(s);
                    details.Add(obj);
                }

                NFMT.WareHouse.BLL.StockReceiptBLL bll = new NFMT.WareHouse.BLL.StockReceiptBLL();
                result = bll.Update(user, stockReceipt, details);

                if (result.ResultStatus == 0)
                {
                    result.Message = "仓库回执修改成功";
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