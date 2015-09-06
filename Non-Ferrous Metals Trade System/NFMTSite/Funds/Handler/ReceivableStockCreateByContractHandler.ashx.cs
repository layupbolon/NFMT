using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// ReceivableStockCreateByContractHandler 的摘要说明
    /// </summary>
    public class ReceivableStockCreateByContractHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string r = context.Request.Form["Allot"];
            if (string.IsNullOrEmpty(r))
            {
                context.Response.Write("分配信息不能为空");
                context.Response.End();
            }
            string memo = context.Request.Form["memo"];

            int stockId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["sid"]) || !int.TryParse(context.Request.Form["sid"], out stockId) || stockId <= 0)
            {
                context.Response.Write("库存信息错误");
                context.Response.End();
            }

            int stockNameId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["stockNameId"]) || !int.TryParse(context.Request.Form["stockNameId"], out stockNameId) || stockNameId <= 0)
            {
                context.Response.Write("业务单号信息错误");
                context.Response.End();
            }

            int curId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["curId"]) || !int.TryParse(context.Request.Form["curId"], out curId) || curId <= 0)
            {
                context.Response.Write("币种信息错误");
                context.Response.End();
            }

            int allotFrom = 0;
            if (string.IsNullOrEmpty(context.Request.Form["allotFrom"]) || !int.TryParse(context.Request.Form["allotFrom"], out allotFrom) || allotFrom <= 0)
            {
                context.Response.Write("分配来源错误");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<AllotInfo> allotInfos = serializer.Deserialize<List<AllotInfo>>(r);
                if (allotInfos == null)
                {
                    context.Response.Write("分配信息错误");
                    context.Response.End();
                }

                List<NFMT.Funds.Model.StcokReceivable> stcokReceivables = new List<NFMT.Funds.Model.StcokReceivable>();
                foreach (var item in allotInfos)
                {
                    int refId = 0;
                    decimal bala = 0;
                    if (!string.IsNullOrEmpty(item.CanAllotBala)
                        && !string.IsNullOrEmpty(item.RefId)
                        && int.TryParse(item.RefId, out refId)
                        && decimal.TryParse(item.CanAllotBala, out bala))
                    {
                        NFMT.Funds.Model.StcokReceivable stcokReceivable = new NFMT.Funds.Model.StcokReceivable()
                        {
                            //AllotId
                            //CorpRefId
                            ContractRefId = refId,
                            //RecId
                            StockId = stockId,
                            StockNameId = stockNameId,
                            //AllotBala = bala
                        };
                        stcokReceivables.Add(stcokReceivable);
                    }
                }

                NFMT.Funds.BLL.ReceivableAllotBLL bll = new NFMT.Funds.BLL.ReceivableAllotBLL();
                result = bll.ReceivableStockCreateByContractHandle(user, stcokReceivables, curId, memo, allotFrom);
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            context.Response.Write(result.Message);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class AllotInfo
        {
            public string RefId { get; set; }
            //public string CorpCode { get; set; }
            public string CanAllotBala { get; set; }
        }
    }
}