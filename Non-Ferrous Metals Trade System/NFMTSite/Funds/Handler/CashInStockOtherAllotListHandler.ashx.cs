﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// CashInStockOtherAllotListHandler 的摘要说明
    /// </summary>
    public class CashInStockOtherAllotListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 100;
            string orderStr = string.Empty, whereStr = string.Empty;

            int stockLogId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["stockLogId"]) || !int.TryParse(context.Request.QueryString["stockLogId"], out stockLogId) || stockLogId == 0)
            {
                context.Response.Write("获取数据错误");
                context.Response.End();
            }

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
            {
                string sortDataField = context.Request.QueryString["sortdatafield"].Trim();
                string sortOrder = context.Request.QueryString["sortorder"].Trim();

                switch (sortDataField)
                {
                    case "AllotTime":
                        sortDataField = "cia.AllotTime";
                        break;
                    case "AlloterName":
                        sortDataField = "emp.Name";
                        break;
                    case "AllotBala":
                        sortDataField = "cia.AllotBala";
                        break;
                    case "CurrencyName":
                        sortDataField = "cur.CurrencyName";
                        break;
                    case "StatusName":
                        sortDataField = "cia.AllotStatus";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.Funds.BLL.CashInStcokBLL bll = new NFMT.Funds.BLL.CashInStcokBLL();
            NFMT.Common.SelectModel select = bll.GetOtherAllotInStock(pageIndex, pageSize, orderStr,stockLogId);
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