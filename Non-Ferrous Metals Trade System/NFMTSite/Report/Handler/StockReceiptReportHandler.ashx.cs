using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Report.Handler
{
    /// <summary>
    /// StockReceiptReportHandler 的摘要说明
    /// </summary>
    public class StockReceiptReportHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            DateTime startDate = NFMT.Common.DefaultValue.DefaultTime;
            DateTime endDate = NFMT.Common.DefaultValue.DefaultTime;

            string refNo = context.Request.QueryString["refNo"];

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
                    case "RefId":
                        sortDataField = "sis.RefId";
                        break;
                    case "AssetName":
                        sortDataField = "ass.AssetName";
                        break;
                    case "RefNo":
                        sortDataField = "sn.RefNo";
                        break;
                    case "PaperNo":
                        sortDataField = "st.PaperNo";
                        break;
                    case "CardNo":
                        sortDataField = "st.CardNo";
                        break;
                    case "StockDate":
                        sortDataField = "st.StockDate";
                        break;
                    case "OutContractNo":
                        sortDataField = "sub.OutContractNo";
                        break;
                    case "InCorpName":
                        sortDataField = "inCorp.CorpName";
                        break;
                    case "OutCorpName":
                        sortDataField = "outCorp.CorpName";
                        break;
                    case "MUName":
                        sortDataField = "mu.MUName";
                        break;
                    case "inGapAmount":
                        sortDataField = "inLog.GapAmount";
                        break;
                    case "inNetAmount":
                    case "ReceiptInGap":
                    case "inRate":
                        sortDataField = "inLog.NetAmount";
                        break;
                    case "outNetAmount":
                        sortDataField = "outLog.NetAmount";
                        break;
                    case "outGapAmount":
                        sortDataField = "outLog.GapAmount";
                        break;
                    case "outRate":
                    case "ReceiptOutGap":
                    case "ProfitOrLoss":
                        sortDataField = "outLog.ReceiptOutGap";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.WareHouse.BLL.StockReceiptBLL bll = new NFMT.WareHouse.BLL.StockReceiptBLL();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = bll.GetReceiptReportSelect(pageIndex, pageSize, orderStr, assetId, refNo);
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