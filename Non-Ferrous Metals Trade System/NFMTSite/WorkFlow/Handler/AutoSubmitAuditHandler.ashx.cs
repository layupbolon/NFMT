using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WorkFlow.Handler
{
    /// <summary>
    /// AutoSubmitAuditHandler 的摘要说明
    /// </summary>
    public class AutoSubmitAuditHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string modelStr = context.Request.Form["model"];
            if (string.IsNullOrEmpty(modelStr))
            {
                result.Message = "实体不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            int masterId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["masterId"]) || !int.TryParse(context.Request.Form["masterId"], out masterId) || masterId <= 0)
            {
                result.Message = "审核模版序号错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Common.IModel model = null;
                NFMT.WorkFlow.ITaskProvider taskProvider = null;                

                switch (masterId)
                {
                    case (int)NFMT.WorkFlow.MasterEnum.合约审核:
                        taskProvider = new NFMT.Contract.TaskProvider.ContractTaskProvider();
                        model = serializer.Deserialize<NFMT.Contract.Model.Contract>(modelStr);
                        break;
                    case (int)NFMT.WorkFlow.MasterEnum.子合约审核:
                        taskProvider = new NFMT.Contract.TaskProvider.SubTaskProvider();
                        model = serializer.Deserialize<NFMT.Contract.Model.ContractSub>(modelStr);
                        break;
                    case (int)NFMT.WorkFlow.MasterEnum.采购主子合约审核:
                        taskProvider = new NFMT.Contract.TaskProvider.ContractTaskProvider();
                        model = serializer.Deserialize<NFMT.Contract.Model.Contract>(modelStr);
                        break;
                    case (int)NFMT.WorkFlow.MasterEnum.销售合约审核:
                        taskProvider = new NFMT.Contract.TaskProvider.ContractTaskProvider();
                        model = serializer.Deserialize<NFMT.Contract.Model.Contract>(modelStr);
                        break;
                    case (int)NFMT.WorkFlow.MasterEnum.入库登记审核:
                        taskProvider = new NFMT.WareHouse.TaskProvider.StockInTaskProvider();
                        model = serializer.Deserialize<NFMT.WareHouse.Model.StockIn>(modelStr);
                        break;
                    case (int)NFMT.WorkFlow.MasterEnum.预入库审核:
                        taskProvider = new NFMT.WareHouse.TaskProvider.StockInTaskProvider();
                        model = serializer.Deserialize<NFMT.WareHouse.Model.StockIn>(modelStr);
                        break;
                    case (int)NFMT.WorkFlow.MasterEnum.付款申请:
                        taskProvider = new NFMT.Funds.TaskProvider.PayApplyTaskProvider();
                        NFMT.Funds.Model.PayApply payApply = serializer.Deserialize<NFMT.Funds.Model.PayApply>(modelStr);
                        NFMT.Operate.BLL.ApplyBLL applyBLL = new NFMT.Operate.BLL.ApplyBLL();
                        result = applyBLL.Get(user, payApply.ApplyId);
                        if (result.ResultStatus != 0)
                        {
                            result.Message = "获取申请错误";
                            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                            context.Response.End();
                        }
                        model = result.ReturnValue as NFMT.Common.IModel;
                        break;
                    case (int)NFMT.WorkFlow.MasterEnum.出库审核:
                        taskProvider = new NFMT.WareHouse.TaskProvider.StockOutTaskProvider();
                        model = serializer.Deserialize<NFMT.WareHouse.Model.StockOut>(modelStr);
                        break;
                    case (int)NFMT.WorkFlow.MasterEnum.预入库转正式审核:
                        taskProvider = new NFMT.WareHouse.TaskProvider.PreToRealTaskProvider();
                        model = serializer.Deserialize<NFMT.WareHouse.Model.Stock>(modelStr);
                        break;
                    case (int)NFMT.WorkFlow.MasterEnum.直接终票审核:
                        taskProvider = new NFMT.Invoice.TaskProvider.InvoiceDirectFinalTaskProvider();
                        model = serializer.Deserialize<NFMT.Operate.Model.Invoice>(modelStr);
                        break;
                    case (int)NFMT.WorkFlow.MasterEnum.财务发票审核:
                        taskProvider = new NFMT.Invoice.TaskProvider.FinanceInvoiceTaskProvider();
                        model = serializer.Deserialize<NFMT.Operate.Model.Invoice>(modelStr);
                        break;
                    case (int)NFMT.WorkFlow.MasterEnum.库存净重回执审核:
                        taskProvider = new NFMT.WareHouse.TaskProvider.StockReceiptTaskProvider();
                        model = serializer.Deserialize<NFMT.WareHouse.Model.StockReceipt>(modelStr);
                        break;
                    case (int)NFMT.WorkFlow.MasterEnum.价外票审核:
                        taskProvider = new NFMT.Invoice.TaskProvider.SITaskProvider();
                        model = serializer.Deserialize<NFMT.Operate.Model.Invoice>(modelStr);
                        break;
                    default:
                        break;
                }

                if (model == null)
                {
                    result.Message = "数据错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                NFMT.WorkFlow.AutoSubmit submit = new NFMT.WorkFlow.AutoSubmit();
                result = submit.Submit(user, model, taskProvider, (NFMT.WorkFlow.MasterEnum)masterId);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
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