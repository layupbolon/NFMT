using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// SplitDocStockListHandler 的摘要说明
    /// </summary>
    public class SplitDocStockListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int corpId = -1;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            DateTime stockDateBegin = NFMT.Common.DefaultValue.DefaultTime;//context.Request["stockDateBegin"];//入库日期开始查询条件
            DateTime stockDateEnd = NFMT.Common.DefaultValue.DefaultTime;// context.Request["stockDateEnd"];//入库日期结束查询条件

            if (!string.IsNullOrEmpty(context.Request["sdb"]))
            {
                if (!DateTime.TryParse(context.Request["sdb"], out stockDateBegin))
                    stockDateBegin = NFMT.Common.DefaultValue.DefaultTime;
            }

            if (!string.IsNullOrEmpty(context.Request["sde"]))
            {
                if (!DateTime.TryParse(context.Request["sde"], out stockDateEnd))
                    stockDateEnd = NFMT.Common.DefaultValue.DefaultTime;
                else
                    stockDateEnd.AddDays(1);
            }

            string stockName = context.Request["sn"];//业务单号模糊查询

            if (!string.IsNullOrEmpty(context.Request["ci"]))
                int.TryParse(context.Request["ci"], out corpId);//所属公司查询条件

            int stockStatus = 0;
            if (!string.IsNullOrEmpty(context.Request["s"]))
                int.TryParse(context.Request["s"], out stockStatus);

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
                    case "StockDate":
                        sortDataField = string.Format("sto.{0}", sortDataField);
                        break;
                    case "RefNo":
                        sortDataField = string.Format("sn.{0}", sortDataField);
                        break;
                    case "CorpName":
                        sortDataField = "cor.CorpName";
                        break;
                    case "AssetName":
                        sortDataField = "ass.AssetName";
                        break;
                    case "StockWeight":
                        sortDataField = "sto.GrossAmount";
                        break;
                    case "BrandName":
                        sortDataField = "bra.BrandName";
                        break;
                    case "CustomsTypeName":
                        sortDataField = "sto.CustomsType";
                        break;
                    case "StatusName":
                        sortDataField = "sd.StatusName";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.WareHouse.BLL.StockBLL stockBLL = new NFMT.WareHouse.BLL.StockBLL();
            NFMT.Common.SelectModel select = stockBLL.GetStockListSelectForSplitDoc(pageIndex, pageSize, orderStr, stockName, stockDateBegin, stockDateEnd, corpId, stockStatus);
            NFMT.Common.ResultModel result = stockBLL.Load(user, select);

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