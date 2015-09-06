using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Financing.Handler
{
    /// <summary>
    /// HandleStockInfo 的摘要说明
    /// </summary>
    public class HandleStockInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            string text = context.Request.Form["text"];
            if (string.IsNullOrEmpty(text))
            {
                result.ResultStatus = -1;
                result.Message = "参数错误";
                context.Response.Write(serializer.Serialize(result));
                context.Response.End();
            }

            List<StockInfo> stockInfos = new List<StockInfo>();

            try
            {
                foreach (string str in text.Split('\n'))
                {
                    if (string.IsNullOrEmpty(str) || str.Split('\t').Length < 5) continue;

                    stockInfos.Add(new StockInfo()
                    {
                        ContractNo = str.Split('\t')[0],
                        NetAmount = Convert.ToDecimal(str.Split('\t')[1]),
                        RefNo = str.Split('\t')[2],
                        Deadline = str.Split('\t')[3],
                        Memo = str.Split('\t')[4]
                    });
                }
                if (stockInfos.Any())
                {
                    result.ResultStatus = 0;
                    result.Message = "处理成功";
                    result.ReturnValue = serializer.Serialize(stockInfos);
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "处理失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            context.Response.Write(serializer.Serialize(result));
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class StockInfo
        {
            public string ContractNo { get; set; }
            public decimal NetAmount { get; set; }
            public string RefNo { get; set; }
            public string Deadline { get; set; }
            public string Memo { get; set; }
        }
    }
}