using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Contract.Handler
{
    /// <summary>
    /// ContractOutAuditHandler 的摘要说明
    /// </summary>
    public class ContractOutAuditHandler : IHttpHandler
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

                NFMT.Contract.BLL.ContractBLL bll = new NFMT.Contract.BLL.ContractBLL();
                NFMT.WareHouse.BLL.StockOutApplyBLL applyBLL = new NFMT.WareHouse.BLL.StockOutApplyBLL();

                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    result = bll.ContractOutAudit(user, obj, isPass);
                    if (result.ResultStatus == 0 && isPass)
                        result = applyBLL.ContractOutAuditStockOperate(user, result.AffectCount,isPass);
                    
                    if (result.ResultStatus == 0)
                    {
                        result.Message = "销售合约审核成功";
                        scope.Complete();
                    }
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