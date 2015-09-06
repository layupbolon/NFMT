using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Contract.TaskProvider
{
    public class ContractTaskProvider:NFMT.WorkFlow.ITaskProvider
    {
        public NFMT.Common.ResultModel Create(Common.UserModel user, Common.IModel model)
        {
            NFMT.Common.ResultModel result = new Common.ResultModel();

            try
            {
                WorkFlow.Model.Task task = new WorkFlow.Model.Task();
                Model.Contract contract = model as Model.Contract;

                Contract.TradeBorderEnum tradeBorder = (Contract.TradeBorderEnum)contract.TradeBorder;
                Contract.TradeDirectionEnum tradeDirection = (TradeDirectionEnum)contract.TradeDirection;
                task.TaskName = string.Format("{0}{1}合约审核", tradeBorder.ToString("F"), tradeDirection.ToString("F"));

                NFMT.Data.Model.Asset asset = NFMT.Data.BasicDataProvider.Assets.FirstOrDefault(temp => temp.AssetId == contract.AssetId);
                NFMT.Data.Model.MeasureUnit unit = NFMT.Data.BasicDataProvider.MeasureUnits.FirstOrDefault(temp => temp.MUId == contract.UnitId);
                NFMT.Data.Model.Currency currency = NFMT.Data.BasicDataProvider.Currencies.FirstOrDefault(temp => temp.CurrencyId == contract.SettleCurrency);

                task.TaskConnext = string.Format("{0} 于 {1} 提交审核。合约号：{2}，品种：{3}，签订数量：{4}{5}，升贴水：{6}{7}", user.EmpName, DateTime.Now.ToString(), contract.OutContractNo, asset.AssetName, contract.SignAmount, unit.MUName, contract.Premium.ToString("0.00"), currency.CurrencyName);

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
