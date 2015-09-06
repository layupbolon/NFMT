using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Contract.Handler
{
    /// <summary>
    /// ContractInAuditHandler 的摘要说明
    /// </summary>
    public class ContractInAuditHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            context.Response.ContentType = "text/plain";

            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            if (string.IsNullOrEmpty(context.Request.Form["source"]))
            {
                result.Message = "数据源为空";
                result.ResultStatus = -1;
                context.Response.Write(serializer.Serialize(result));
                context.Response.End();
            }

            bool isPass = false;
            if (string.IsNullOrEmpty(context.Request.Form["ispass"]) || !bool.TryParse(context.Request.Form["ispass"], out isPass))
            {
                result.Message = "审核结果错误";
                result.ResultStatus = -1;
                context.Response.Write(serializer.Serialize(result));
                context.Response.End();
            }

            try
            {
                string jsonData = context.Request.Form["source"];
                var obj = serializer.Deserialize<NFMT.WorkFlow.Model.DataSource>(jsonData);

                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
                    result = bll.ContractInAudit(user, obj, isPass);
                    if (result.ResultStatus == 0)
                    {
                        int contractId = (int)result.ReturnValue;
                        int subId = result.AffectCount;

                        NFMT.WareHouse.BLL.StockLogBLL stockLogBLL = new NFMT.WareHouse.BLL.StockLogBLL();
                        result = stockLogBLL.ContractInAuditStockLogOperate(user, contractId, subId);
                    }

                    if (result.ResultStatus == 0)
                        scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }

            context.Response.Write(serializer.Serialize(result));
            context.Response.End();
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