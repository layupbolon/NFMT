﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// CanStockMoveApplyListHandler 的摘要说明
    /// </summary>
    public class CanStockMoveApplyListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            int applyDept = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["a"]))
            {
                if (!int.TryParse(context.Request.QueryString["a"], out applyDept))
                    applyDept = 0;
            }

            DateTime fromDate = NFMT.Common.DefaultValue.DefaultTime;
            if (!string.IsNullOrEmpty(context.Request.QueryString["f"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["f"], out fromDate))
                    fromDate = NFMT.Common.DefaultValue.DefaultTime;
            }

            DateTime toDate = NFMT.Common.DefaultValue.DefaultTime;
            if (!string.IsNullOrEmpty(context.Request.QueryString["t"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["t"], out toDate))
                    toDate = NFMT.Common.DefaultValue.DefaultTime;
            }

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                orderStr = string.Format("{0} {1}", context.Request.QueryString["sortdatafield"].Trim(), context.Request.QueryString["sortorder"].Trim());

            NFMT.WareHouse.BLL.StockMoveApplyBLL bll = new NFMT.WareHouse.BLL.StockMoveApplyBLL();
            NFMT.Common.SelectModel select = bll.GetCanStockMoveApplySelectModel(pageIndex, pageSize, orderStr, applyDept, fromDate, toDate);
            NFMT.Common.ResultModel result = bll.Load(user, select);

            context.Response.ContentType = "application/json; charset=utf-8";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            System.Collections.Generic.Dictionary<string, object> dic = new System.Collections.Generic.Dictionary<string, object>();

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