using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Report.Handler
{
    /// <summary>
    /// StockLogReportHandler 的摘要说明
    /// </summary>
    public class StockLogReportHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            DateTime startDate = NFMT.Common.DefaultValue.DefaultTime;
            DateTime endDate = NFMT.Common.DefaultValue.DefaultTime;

            if (string.IsNullOrEmpty(context.Request["sd"]) || !DateTime.TryParse(context.Request["sd"], out startDate))
                startDate = NFMT.Common.DefaultValue.DefaultTime;

            if (string.IsNullOrEmpty(context.Request["ed"]) || !DateTime.TryParse(context.Request["ed"], out endDate))
                endDate = NFMT.Common.DefaultValue.DefaultTime;
            else
                endDate = endDate.AddDays(1);

            string refNo = context.Request["rn"];

            int logType = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["lt"]) || !int.TryParse(context.Request.QueryString["lt"], out logType))
                logType = 0;

            int customsType = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["ct"]) || !int.TryParse(context.Request.QueryString["ct"], out customsType))
                customsType = 0;

            int assetId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["ass"]) || !int.TryParse(context.Request.QueryString["ass"], out assetId))
                assetId = 0;

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

            NFMT.WareHouse.BLL.StockLogBLL bll = new NFMT.WareHouse.BLL.StockLogBLL();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = bll.GetStockLogReportSelect(pageIndex, pageSize, orderStr, refNo, logType, customsType,assetId, startDate, endDate);
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