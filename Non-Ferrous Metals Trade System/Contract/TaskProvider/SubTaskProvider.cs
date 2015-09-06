using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Contract.TaskProvider
{
    public class SubTaskProvider : NFMT.WorkFlow.ITaskProvider
    {
        public NFMT.Common.ResultModel Create(Common.UserModel user, Common.IModel model)
        {
            NFMT.Common.ResultModel result = new Common.ResultModel();

            try
            {
                WorkFlow.Model.Task task = new WorkFlow.Model.Task();
                Model.ContractSub sub = model as Model.ContractSub;

                DAL.ContractDAL dal = new DAL.ContractDAL();
                result = dal.Get(user, sub.ContractId);
                if (result.ResultStatus != 0)
                    return result;

                Model.Contract contract = result.ReturnValue as Model.Contract;
                if (contract == null || contract.ContractId <= 0)
                {
                    result.Message = "主合约不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                Contract.TradeBorderEnum tradeBorder = (Contract.TradeBorderEnum)contract.TradeBorder;
                Contract.TradeDirectionEnum tradeDirection = (TradeDirectionEnum)contract.TradeDirection;
                task.TaskName = string.Format("{0}{1}子合约审核", tradeBorder.ToString("F"), tradeDirection.ToString("F"));

                NFMT.Data.Model.Asset asset = NFMT.Data.BasicDataProvider.Assets.FirstOrDefault(temp => temp.AssetId == contract.AssetId);
                NFMT.Data.Model.MeasureUnit unit = NFMT.Data.BasicDataProvider.MeasureUnits.FirstOrDefault(temp => temp.MUId == contract.UnitId);
                NFMT.Data.Model.Currency currency = NFMT.Data.BasicDataProvider.Currencies.FirstOrDefault(temp => temp.CurrencyId == sub.SettleCurrency);

                task.TaskConnext = string.Format("{0} 于 {1} 提交审核。合约号：{2}，品种：{3}，签订数量：{4}{5}，升贴水：{6}{7}", user.EmpName, DateTime.Now.ToString(), sub.OutContractNo, asset.AssetName, sub.SignAmount, unit.MUName, sub.Premium.ToString("0.00"), currency.CurrencyName);

                result.ResultStatus = 0;
                result.ReturnValue = task;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }

            return result;
        }
    }
}
