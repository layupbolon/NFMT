using NFMT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NFMTService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IContractOutService”。
    [ServiceContract]
    public interface IContractOutService
    {
        [OperationContract]
        ResultModel ContractOutCreateStockOperate(UserModel user, int contractId, int subId, int assetId, int unitId, int outCorpId, int tradeBorder, List<NFMT.WareHouse.Model.StockOutApplyDetail> details, bool isSubmitAudit);
        
        [OperationContract]
        ResultModel ContractOutGoBackStockOperate(UserModel user, int subId);

        [OperationContract]
        ResultModel ContractOutInvalidStockOperate(UserModel user, int subId);

        [OperationContract]
        ResultModel ContractOutSubmitStockOperate(UserModel user, int subId);

        [OperationContract]
        ResultModel ContractOutCompleteStockOperate(UserModel user, int subId);

        [OperationContract]
        ResultModel ContractOutAuditStockOperate(UserModel user, int subId, bool isPass);
    }
}
