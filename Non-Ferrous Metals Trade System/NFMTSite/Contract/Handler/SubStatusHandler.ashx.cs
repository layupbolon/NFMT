using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Contract.Handler
{
    /// <summary>
    /// SubStatusHandler 的摘要说明
    /// </summary>
    public class SubStatusHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";
            int id = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["id"], out id) || id <= 0)
            {
                context.Response.Write("序号错误");
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["oi"], out operateId) || operateId <= 0)
            {
                context.Response.Write("操作错误");
                context.Response.End();
            }

            NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.作废:
                    result = subBLL.Invalid(user, id);
                    break;
                case NFMT.Common.OperateEnum.撤返:
                    result = subBLL.GoBack(user, id);
                    break;
                case NFMT.Common.OperateEnum.执行完成:
                    //1:验证付款申请是否全部完成
                    NFMT.Funds.BLL.PayApplyBLL payApplyBLL = new NFMT.Funds.BLL.PayApplyBLL();
                    result = payApplyBLL.CheckContractSubPayApplyConfirm(user, id);
                    if (result.ResultStatus != 0)
                        break;
                    //2:验证收款登记是否全部完成
                    NFMT.Funds.BLL.CashInBLL cashInBLL = new NFMT.Funds.BLL.CashInBLL();
                    result = cashInBLL.CheckContractSubCashInConfirm(user, id);
                    if (result.ResultStatus != 0)
                        break;
                    //3:验证入库登记是否全部完成
                    NFMT.WareHouse.BLL.StockInBLL stockInBLL = new NFMT.WareHouse.BLL.StockInBLL();
                    result = stockInBLL.CheckContractSubStockInConfirm(user, id);
                    if (result.ResultStatus != 0)
                        break;
                    //4:验证出库申请是否全部完成
                    NFMT.WareHouse.BLL.StockOutApplyBLL stockOutApplyBLL = new NFMT.WareHouse.BLL.StockOutApplyBLL();
                    result = stockOutApplyBLL.CheckContractSubStockOutApplyConfirm(user, id);
                    if (result.ResultStatus != 0)
                        break;
                    //5:验证临票是否全部完成
                    //6:验证直接终票是否全部完成
                    //7:验证补零终票是否全部完成
                    NFMT.Invoice.BLL.BusinessInvoiceBLL businessInvoiceBLL = new NFMT.Invoice.BLL.BusinessInvoiceBLL();
                    result = businessInvoiceBLL.CheckContractSubBusinessInvoiceApplyConfirm(user, id);
                    if (result.ResultStatus != 0)
                        break;
                    //8:点价合约验证点价是否全部完成
                    NFMT.DoPrice.BLL.PricingApplyBLL pricingApplyBLL = new NFMT.DoPrice.BLL.PricingApplyBLL();
                    result = pricingApplyBLL.CheckContractSubPricingApplyConfirm(user, id);
                    if (result.ResultStatus != 0)
                        break;

                    result = subBLL.Complete(user, id);
                    break;
                case NFMT.Common.OperateEnum.执行完成撤销:
                    result = subBLL.CompleteCancel(user, id);
                    break;
            }

            if (result.ResultStatus == 0)
                result.Message = string.Format("{0}成功", operateEnum.ToString());

            context.Response.Write(result.Message);
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