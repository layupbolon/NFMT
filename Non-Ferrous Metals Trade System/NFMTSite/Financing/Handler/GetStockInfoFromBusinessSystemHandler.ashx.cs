using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Financing.Handler
{
    /// <summary>
    /// GetStockInfoFromBusinessSystemHandler 的摘要说明
    /// </summary>
    public class GetStockInfoFromBusinessSystemHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            FinanceService.FinService service = new FinanceService.FinService();
            string json = service.GetStockInfoFromBusinessSystem();
            context.Response.Write(json);
            context.Response.End();
            //if (string.IsNullOrEmpty(json))
            //{
            //    context.Response.Write(string.Empty);
            //    context.Response.End();
            //}

            //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //List<MyStock> myStocks = new List<MyStock>();
            //try
            //{
            //    myStocks = serializer.Deserialize<List<MyStock>>(json);
            //}
            //catch
            //{
                
            //}

            //List<MyStockOut> myStockOuts = new List<MyStockOut>();
            //if (myStocks != null && myStocks.Any())
            //{
            //    myStocks.ForEach(a =>
            //    {
            //        myStockOuts.Add(new MyStockOut
            //        {
            //            RefNo = a.RefNo,
            //            StockIdAndNetAmount = string.Format("{0}{1}{2}", a.StockId, "||", a.NetAmount)
            //        });
            //    });

            //    context.Response.Write(serializer.Serialize(myStockOuts));
            //    context.Response.End();
            //}
            //context.Response.Write(string.Empty);
            //context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class MyStock
        {
            public int StockId { get; set; }
            public string RefNo { get; set; }
            public decimal NetAmount { get; set; }
        }

        public class MyStockOut
        {
            public string RefNo { get; set; }
            public string StockIdAndNetAmount { get; set; }
        }
    }
}