using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// MasterClauseListHandler 的摘要说明
    /// </summary>
    public class MasterClauseListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string clauseText = context.Request.QueryString["t"];
            bool isHas = false;
            int masterId = 0;

            if (!string.IsNullOrEmpty(context.Request.QueryString["mi"]))
            {
                if (!int.TryParse(context.Request.QueryString["mi"], out masterId))
                {
                    context.Response.Write("未选择合约模板");
                    context.Response.End();
                }
            }

            if (!string.IsNullOrEmpty(context.Request.QueryString["ih"]))
            {
                int intHas = 0;
                if (!int.TryParse(context.Request.QueryString["ih"], out intHas))
                    intHas = 0;
                if (intHas == 1)
                    isHas = true;
                else
                    isHas = false;
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
                    case "ClauseText":
                        sortDataField = "cc.ClauseText";
                        break;
                    case "ClauseEnText":
                        sortDataField = "cc.ClauseEnText";
                        break;
                    case "Sort":
                        sortDataField = "ccr.Sort";
                        break;
                    case "IsChose":
                        sortDataField = "ccr.IsChose";
                        break;
                    case "ClauseId":
                        sortDataField = "cc.ClauseId";
                        break;
                    case "RefId":
                        sortDataField = "ccr.RefId";
                        break;
                }


                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Data.BLL.ClauseContractBLL refBLL = new NFMT.Data.BLL.ClauseContractBLL();

            NFMT.Common.SelectModel select = refBLL.GetSelectModel(pageIndex, pageSize, orderStr, masterId, isHas, clauseText);
            NFMT.Common.ResultModel result = refBLL.Load(user, select);
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("count", result.AffectCount);
            dic.Add("data", dt);

            string postData = Newtonsoft.Json.JsonConvert.SerializeObject(dic);
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