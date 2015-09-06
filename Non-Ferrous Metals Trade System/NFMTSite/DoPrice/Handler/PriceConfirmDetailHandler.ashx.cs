using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.DoPrice.Handler
{
    /// <summary>
    /// PriceConfirmDetailHandler 的摘要说明
    /// </summary>
    public class PriceConfirmDetailHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            int cid = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["cid"]))
            {
                if (!int.TryParse(context.Request.QueryString["cid"], out cid))
                    cid = 0;
            }

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
                    case "DetailId":
                        sortDataField = "detail.DetailId";
                        break;
                    case "PriceConfirmId":
                        sortDataField = "detail.PriceConfirmId";
                        break;
                    case "InterestStartDate":
                        sortDataField = "detail.InterestStartDate";
                        break;
                    case "InterestEndDate":
                        sortDataField = "detail.InterestEndDate";
                        break;
                    case "InterestDayName":
                    case "InterestDay":
                        sortDataField = "detail.InterestDay";
                        break;
                    case "InterestUnit":
                    case "InterestUnitName":
                        sortDataField = "detail.InterestUnit";
                        break;
                    default: break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.DoPrice.BLL.PriceConfirmDetailBLL bll = new NFMT.DoPrice.BLL.PriceConfirmDetailBLL();
            NFMT.Common.SelectModel select = bll.GetSelectModel(pageIndex, pageSize, orderStr, cid);
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