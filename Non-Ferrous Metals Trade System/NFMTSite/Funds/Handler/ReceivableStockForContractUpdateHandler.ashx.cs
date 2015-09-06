using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// ReceivableStockForContractUpdateHandler 的摘要说明
    /// </summary>
    public class ReceivableStockForContractUpdateHandler : IHttpHandler
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

            int sid = 0;
            if (string.IsNullOrEmpty(context.Request.Form["sid"]) || !int.TryParse(context.Request.Form["sid"], out sid) || sid <= 0)
            {
                context.Response.Write("库存信息错误");
                context.Response.End();
            }

            int stockNameId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["stockNameId"]) || !int.TryParse(context.Request.Form["stockNameId"], out stockNameId) || stockNameId <= 0)
            {
                context.Response.Write("业务单号错误");
                context.Response.End();
            }

            int curId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["curId"]) || !int.TryParse(context.Request.Form["curId"], out curId) || curId <= 0)
            {
                context.Response.Write("币种信息错误");
                context.Response.End();
            }

            int allotId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["id"]) || !int.TryParse(context.Request.Form["id"], out allotId) || allotId <= 0)
            {
                context.Response.Write("序号信息错误");
                context.Response.End();
            }

            int allotFrom = 0;
            if (string.IsNullOrEmpty(context.Request.Form["allotFrom"]) || !int.TryParse(context.Request.Form["allotFrom"], out allotFrom) || allotFrom <= 0)
            {
                context.Response.Write("来源信息错误");
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
                            AllotId = allotId,
                            //CorpRefId
                            ContractRefId = refId,
                            //RecId
                            StockId = sid,
                            StockNameId = stockNameId,
                            //AllotBala = bala
                        };
                        stcokReceivables.Add(stcokReceivable);
                    }
                }

                NFMT.Funds.BLL.ReceivableAllotBLL bll = new NFMT.Funds.BLL.ReceivableAllotBLL();
                result = bll.ReceivableStockUpdateForContractHandle(user, stcokReceivables, curId, memo, allotId, allotFrom);
                if (result.ResultStatus == 0)
                    result.Message = "修改成功";

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
            public string CanAllotBala { get; set; }
        }
    }
}