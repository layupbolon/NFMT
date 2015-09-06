using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Contract.Handler
{
    /// <summary>
    /// ContractOutStatusHandler 的摘要说明
    /// </summary>
    public class ContractOutStatusHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            context.Response.ContentType = "text/plain";
            int id = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["id"], out id) || id <= 0)
            {
                context.Response.Write("序号错误");
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["oi"], out operateId) || operateId <= 0)
            {
                context.Response.Write("操作错误");
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            NFMT.Contract.BLL.ContractBLL contractBLL = new NFMT.Contract.BLL.ContractBLL();
            NFMT.WareHouse.BLL.StockOutApplyBLL applyBLL = new NFMT.WareHouse.BLL.StockOutApplyBLL();
           
            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                switch (operateEnum)
                {
                    case NFMT.Common.OperateEnum.作废:
                        result = contractBLL.ContractOutInvalid(user, id);
                        if (result.ResultStatus == 0)
                            result = applyBLL.ContractOutInvalidStockOperate(user, result.AffectCount);
                        break;
                    case NFMT.Common.OperateEnum.撤返:
                        result = contractBLL.ContractOutGoBack(user, id);
                        break;
                    case NFMT.Common.OperateEnum.执行完成:
                        result = contractBLL.ContractOutComplete(user, id);
                        if (result.ResultStatus == 0)
                            result = applyBLL.ContractOutCompleteStockOperate(user, result.AffectCount);
                        break;
                    case NFMT.Common.OperateEnum.执行完成撤销:
                        result = contractBLL.ContractOutCompleteCancel(user, id);
                        break;
                }

                if (result.ResultStatus == 0)
                {
                    result.Message = string.Format("{0}成功", operateEnum.ToString());
                    scope.Complete();
                }
            }

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
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