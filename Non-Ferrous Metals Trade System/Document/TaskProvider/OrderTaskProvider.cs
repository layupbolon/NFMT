using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Document.TaskProvider
{
    public class OrderTaskProvider : NFMT.WorkFlow.ITaskProvider
    {
        public Common.ResultModel Create(Common.UserModel user, Common.IModel model)
        {
            NFMT.Common.ResultModel result = new Common.ResultModel();

            try
            {
                WorkFlow.Model.Task task = new WorkFlow.Model.Task();
                Model.DocumentOrder order = model as Model.DocumentOrder;

                OrderTypeEnum orderType = (OrderTypeEnum)order.OrderType;
                task.TaskName = string.Format("{0}审核",orderType.ToString("F"));

                NFMT.User.Model.Corporation applyCorp = NFMT.User.UserProvider.Corporations.FirstOrDefault(temp => temp.CorpId == order.ApplyCorp);
                string applyCorpName = string.Empty;
                if (applyCorp != null && applyCorp.CorpId > 0)
                    applyCorpName = applyCorp.CorpName;

                NFMT.Data.Model.MeasureUnit mu = NFMT.Data.BasicDataProvider.MeasureUnits.FirstOrDefault(temp => temp.MUId == order.UnitId);
                string muName = string.Empty;
                if (mu != null && mu.MUId > 0)
                    muName = mu.MUName;

                NFMT.Data.Model.Currency currency = NFMT.Data.BasicDataProvider.Currencies.FirstOrDefault(temp => temp.CurrencyId == order.Currency);
                string currencyName = string.Empty;
                if (currency != null && currency.CurrencyId > 0)
                    currencyName = currency.CurrencyName;

                task.TaskConnext = string.Format("{0} 于 {1} 提交审核。申请公司：[{2}]，客户公司：{3}，净重：{4}{5}，发票金额：{6}{7}", user.EmpName, DateTime.Now.ToString(), applyCorpName,order.BuyerCorpName,order.NetAmount.ToString(),muName,order.InvBala,currencyName);

                result.ReturnValue = task;
                result.ResultStatus = 0;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
