using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockOutApplyNotAllotListHandler 的摘要说明
    /// </summary>
    public class StockOutApplyNotAllotListHandler : IHttpHandler
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
                        sortDataField = string.Format("sn.{0}", sortDataField);
                        break;
                    case "StockWeight":
                        sortDataField = "sto.GrossAmount";
                        break;
                    case "StatusName":
                        sortDataField = "sd.StatusName";
                        break;
                    case "CorpName":
                        sortDataField = "cor.CorpName";
                        break;
                    case "AssetName":
                        sortDataField = "ass.AssetName";
                        break;
                    case "BrandName":
                        sortDataField = "bra.BrandName";
                        break;
                    case "LastAmount":
                        sortDataField = "ISNULL(sto.CurNetAmount,0) - ISNULL(soad.ApplyAmount,0)";
                        break;               
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            int contractId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["cid"]))
            {
                context.Response.Write("合约信息错误");
                context.Response.End();
            }
            if (!int.TryParse(context.Request.QueryString["cid"], out contractId) || contractId <= 0)
            {
                context.Response.Write("合约信息错误");
                context.Response.End();
            }

            string sids = context.Request.QueryString["sids"];
            string dids = context.Request.QueryString["dids"];

            int stockOutApplyId = 0;

            if (string.IsNullOrEmpty(context.Request.QueryString["applyId"]) || !int.TryParse(context.Request.QueryString["applyId"], out stockOutApplyId))
                stockOutApplyId = 0;

            string refNo = context.Request.Params["refNo"];
            int ownCorpId = 0;
            if (string.IsNullOrEmpty(context.Request.Params["ownCorpId"]) || !int.TryParse(context.Request.Params["ownCorpId"], out ownCorpId))
                ownCorpId = 0;

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.WareHouse.BLL.StockBLL stockBLL = new NFMT.WareHouse.BLL.StockBLL();
            NFMT.Common.SelectModel select = stockBLL.GetCanSalesSelect(pageIndex, pageSize, orderStr, sids, contractId,stockOutApplyId,dids,refNo,ownCorpId);
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