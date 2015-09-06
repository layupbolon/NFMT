using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.DoPrice.TaskProvider
{
    public class PriceConfirmTaskProvider : NFMT.WorkFlow.ITaskProvider
    {
        public Common.ResultModel Create(Common.UserModel user, Common.IModel model)
        {
            NFMT.Common.ResultModel result = new Common.ResultModel();

            try
            {
                WorkFlow.Model.Task task = new WorkFlow.Model.Task();
                Model.PriceConfirm priceConfirm = model as Model.PriceConfirm;

                task.TaskName = "价格确认单审核";

                NFMT.Contract.BLL.ContractBLL contractBLL = new Contract.BLL.ContractBLL();
                result = contractBLL.Get(user, priceConfirm.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                //获取合约
                NFMT.Contract.Model.Contract contract = result.ReturnValue as NFMT.Contract.Model.Contract;
                if (contract == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取合约失败";
                    return result;
                }
                //获取币种
                NFMT.Data.Model.Currency cur = NFMT.Data.BasicDataProvider.Currencies.SingleOrDefault(a => a.CurrencyId == contract.SettleCurrency);
                if (cur == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取币种失败";
                    return result;
                }
                //获取单位
                NFMT.Data.Model.MeasureUnit mu = NFMT.Data.BasicDataProvider.MeasureUnits.SingleOrDefault(a => a.MUId == contract.UnitId);
                if (mu == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取单位失败";
                    return result;
                }

                task.TaskConnext = string.Format("{0} 于 {1} 提交审核。合约号：{2}，价格：{3}{4}/{5}", user.EmpName, DateTime.Now.ToString(), contract.OutContractNo, priceConfirm.SettlePrice, cur.CurrencyName, mu.MUName);

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
