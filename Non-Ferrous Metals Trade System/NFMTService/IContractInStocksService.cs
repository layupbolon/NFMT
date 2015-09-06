using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NFMTService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IContractInStocksService”。
    [ServiceContract]
    public interface IContractInStocksService
    {
        [OperationContract]
        NFMT.Common.ResultModel InsertStockInContract(NFMT.Common.UserModel user, int contractId, int subId, List<int> stockLogIds);

        [OperationContract]
        NFMT.Common.ResultModel InvalidStockContract(NFMT.Common.UserModel user, int contractId);

        [OperationContract]
        NFMT.Common.ResultModel ContractStockInComplete(NFMT.Common.UserModel user, int contractId);

        [OperationContract]
        NFMT.Common.ResultModel ContractStockInCompleteCancel(NFMT.Common.UserModel user, int contractId);
    }
}
