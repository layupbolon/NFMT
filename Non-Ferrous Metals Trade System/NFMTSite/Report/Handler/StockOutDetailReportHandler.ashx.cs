using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Report.Handler
{
    /// <summary>
    /// StockOutDetailReportHandler 的摘要说明
    /// </summary>
    public class StockOutDetailReportHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            int stockOutApplyId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["soai"]) || !int.TryParse(context.Request.QueryString["soai"], out stockOutApplyId))
                stockOutApplyId = 0;         

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
                    case "LogTypeName":
                        sortDataField = "lt.DetailName";
                        break;
                    case "RefNo":
                        sortDataField = "sn.RefNo";
                        break;
                    case "PaperNo":
                        sortDataField = "sto.PaperNo";
                        break;
                    case "AssetName":
                        sortDataField = "ass.AssetName";
                        break;
                    case "BrandName":
                        sortDataField = "bra.BrandName";
                        break;
                    case "CustomsTypeName":
                        sortDataField = "cus.DetailName";
                        break;
                    case "DPName":
                        sortDataField = "dp.DPName";
                        break;
                    case "GrossAmount":
                        sortDataField = "sl.GrossAmount";
                        break;
                    case "NetAmount":
                        sortDataField = "sl.NetAmount";
                        break;
                    case "MUName":
                        sortDataField = "mu.MUName";
                        break;
                    case "CardNo":
                        sortDataField = "sto.CardNo";
                        break;
                    case "InCorpName":
                        sortDataField = "inCorp.CorpName";
                        break;
                    case "OutCorpName":
                        sortDataField = "outCorp.CorpName";
                        break;
                    case "SubNo":
                        sortDataField = "cs.SubNo";
                        break;
                    case "TradeDirectionName":
                        sortDataField = "td.DetailName";
                        break;
                    case "AvgPrice":
                        sortDataField = "pri.AvgPrice";
                        break;
                    case "CurrencyName":
                        sortDataField = "cur.CurrencyName";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.WareHouse.BLL.StockOutApplyBLL bll = new NFMT.WareHouse.BLL.StockOutApplyBLL();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = bll.GetStockOutDetailReportSelect(pageIndex, pageSize, orderStr, stockOutApplyId);
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