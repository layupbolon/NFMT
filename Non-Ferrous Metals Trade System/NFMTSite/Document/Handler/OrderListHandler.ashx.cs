using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Document.Handler
{
    /// <summary>
    /// OrderListHandler 的摘要说明
    /// </summary>
    public class OrderListHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int status = -1, outerCorp = -1;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty;
            bool isReady = false;
            string contractNo = context.Request["cn"];

            DateTime beginDate = NFMT.Common.DefaultValue.DefaultTime;
            DateTime endDate = NFMT.Common.DefaultValue.DefaultTime;

            if (!string.IsNullOrEmpty(context.Request["db"]))
            {
                if (!DateTime.TryParse(context.Request["db"], out beginDate))
                    beginDate = NFMT.Common.DefaultValue.DefaultTime;
            }
            if (!string.IsNullOrEmpty(context.Request["de"]))
            {
                if (!DateTime.TryParse(context.Request["de"], out endDate))
                    endDate = NFMT.Common.DefaultValue.DefaultTime;
                else
                    endDate.AddDays(1);
            }

            if (!string.IsNullOrEmpty(context.Request["s"]))
                int.TryParse(context.Request["s"], out status);

            if (!string.IsNullOrEmpty(context.Request["ci"]))
                int.TryParse(context.Request["ci"], out outerCorp);//状态查询条件

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (string.IsNullOrEmpty(context.Request.QueryString["re"]) || !bool.TryParse(context.Request.QueryString["re"], out isReady))
                isReady = false;

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
            {
                string sortDataField = context.Request.QueryString["sortdatafield"].Trim();
                string sortOrder = context.Request.QueryString["sortorder"].Trim();

                switch (sortDataField)
                {
                    case "OrderNo":
                        sortDataField = "do.OrderNo";
                        break;
                    case "OrderTypeName":
                        sortDataField = "do.OrderType";
                        break;
                    case "ApplyCorpName":
                        sortDataField = "appCorp.CorpName";
                        break;
                    case "BuyCorpName":
                        sortDataField = "buyCorp.CorpName";
                        break;
                    case "SubNo":
                        sortDataField = "cs.SubNo";
                        break;
                    case "AssetName":
                        sortDataField = "ass.AssetName";
                        break;
                    case "NetAmount":
                        sortDataField = "do.NetAmount";
                        break;
                    case "MUName":
                        sortDataField = "mu.MUName";
                        break;
                    case "EmpName":
                        sortDataField = "emp.Name";
                        break;
                    case "StatusName":
                        sortDataField = "do.OrderStatus";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Document.BLL.DocumentOrderBLL bll = new NFMT.Document.BLL.DocumentOrderBLL();
            NFMT.Common.SelectModel select = bll.GetSelectModel(pageIndex, pageSize, orderStr, beginDate, endDate, contractNo, outerCorp, status,isReady);
            NFMT.Common.ResultModel result = bll.Load(user, select);

            context.Response.ContentType = "text/plain";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("count", result.AffectCount);
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