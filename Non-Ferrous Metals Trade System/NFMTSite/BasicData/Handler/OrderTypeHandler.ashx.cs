using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// OrderTypeHandler 的摘要说明
    /// </summary>
    public class OrderTypeHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            List<NFMT.Data.Model.BDStyleDetail> orderTypes = new List<NFMT.Data.Model.BDStyleDetail>();

            try
            {
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

                List<NFMT.Data.Model.BDStyleDetail> ls = NFMT.Data.BasicDataProvider.StyleDetails;
                int styleId =(int)NFMT.Data.StyleEnum.OrderType;
                orderTypes = ls.Where(temp => temp.BDStyleId == styleId).ToList();

                int typeId = 0;
                if (!string.IsNullOrEmpty(context.Request.QueryString["id"]) && int.TryParse(context.Request.QueryString["id"], out typeId) && typeId>0)
                {
                    if (typeId == 1) 
                    {
                        //制单指令[临票制单、直接终票制单]
                        orderTypes = orderTypes.Where(temp => temp.StyleDetailId == (int)NFMT.Document.OrderTypeEnum.临票制单指令 || temp.StyleDetailId == (int)NFMT.Document.OrderTypeEnum.终票制单指令).ToList();
                    }
                    else if (typeId == 2) 
                    {
                        //[无配货制单]
                        orderTypes = orderTypes.Where(temp => temp.StyleDetailId == (int)NFMT.Document.OrderTypeEnum.无配货临票制单指令 || temp.StyleDetailId == (int)NFMT.Document.OrderTypeEnum.无配货终票制单指令).ToList();
                    }
                    else if (typeId == 3)
                    {
                        //[替临制单]
                        orderTypes = orderTypes.Where(temp => temp.StyleDetailId == (int)NFMT.Document.OrderTypeEnum.替临制单指令).ToList();
                    }
                }
            }
            catch
            {
                orderTypes = new List<NFMT.Data.Model.BDStyleDetail>();
            }

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(orderTypes);
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