using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockInSelectedHandler 的摘要说明
    /// </summary>
    public class StockInSelectedHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;
            
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
            {
                string sortDataField = context.Request.QueryString["sortdatafield"].Trim();
                string sortOrder = context.Request.QueryString["sortorder"].Trim();

                switch (sortDataField)
                {
                    case "RefNo":
                        sortDataField = "si.RefNo";
                        break;
                    case "StockInDate":
                        sortDataField = "si.StockInDate";
                        break;
                    case "GrossWeight":
                        sortDataField = "si.GrossAmount";
                        break;
                    case "AssetName":
                        sortDataField = "ass.AssetName";
                        break;
                    case "BrandName":
                        sortDataField = "bra.BrandName ";
                        break;
                    case "StatusName":
                        sortDataField = "ref.RefStatus";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            int subId = 0;
            int refId = 0;

            if (string.IsNullOrEmpty(context.Request.QueryString["subId"]) || !int.TryParse(context.Request.QueryString["subId"], out subId))
            {
                context.Response.Write("子合约序号错误");
                context.Response.End();
            }

            if(string.IsNullOrEmpty(context.Request.QueryString["refId"]) || !int.TryParse(context.Request.QueryString["refId"],out refId))
                refId =0 ;

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.WareHouse.BLL.ContractStockIn_BLL bll = new NFMT.WareHouse.BLL.ContractStockIn_BLL();
            NFMT.Common.SelectModel select = bll.GetSelectedModel(pageIndex, pageSize, orderStr,subId,refId);
            NFMT.Common.ResultModel result = bll.Load(user, select);

            context.Response.ContentType = "text/plain";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            int totalRows = result.AffectCount;
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("count", totalRows);
            dic.Add("data", dt);

            string postData = Newtonsoft.Json.JsonConvert.SerializeObject(dic, new Newtonsoft.Json.Converters.DataTableConverter());

            context.Response.Write(postData);
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