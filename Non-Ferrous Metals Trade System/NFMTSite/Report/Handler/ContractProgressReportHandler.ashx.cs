using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Report.Handler
{
    /// <summary>
    /// ContractProgressReportHandler 的摘要说明
    /// </summary>
    public class ContractProgressReportHandler : IHttpHandler
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

            string contractNo = context.Request["cn"];

            int inCorpId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["ici"]) || !int.TryParse(context.Request.QueryString["ici"], out inCorpId))
                inCorpId = 0;

            int outCorpId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["oci"]) || !int.TryParse(context.Request.QueryString["oci"], out outCorpId))
                outCorpId = 0;

            int tradeBorder = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["tb"]) || !int.TryParse(context.Request.QueryString["tb"], out tradeBorder))
                tradeBorder = 0;

            int tradeDirection = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["td"]) || !int.TryParse(context.Request.QueryString["td"], out tradeDirection))
                tradeDirection = 0;
            
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
                    case "TradeDirectionName":
                        sortDataField = "traDir.DetailName";
                        break;
                    case "TradeBorderName":
                        sortDataField = "traBor.DetailName";
                        break;
                    case "pm":
                        sortDataField = "pm.DetailName";
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
                    case "AvgPrice":
                        sortDataField = "pri.AvgPrice";
                        break;
                    case "CurrencyName":
                        sortDataField = "cur.CurrencyName";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = bll.GetContractProgressSelect(pageIndex, pageSize, orderStr,startDate, endDate,contractNo,inCorpId,outCorpId,tradeBorder,tradeDirection);
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