using System.Collections.Generic;
using System.ServiceModel;

namespace StockPool
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IStock”。
    [ServiceContract]
    public interface IStock
    {
        [OperationContract]
        bool StockIn(NFMT.Common.UserModel user, NFMT.WareHouse.Model.StockLog stockLog, List<NFMT.WareHouse.Model.StockLogAttach> stockLogAttachs);

        [OperationContract]
        NFMT.Common.ResultModel StockOut(NFMT.Common.UserModel user, NFMT.WareHouse.Model.StockLog stockLog, List<NFMT.WareHouse.Model.StockLogAttach> stockLogAttachs);

        [OperationContract]
        NFMT.Common.ResultModel Reverse(NFMT.Common.UserModel user, NFMT.WareHouse.Model.StockLog stockLog, List<NFMT.WareHouse.Model.StockLogAttach> stockLogAttachs);

        [OperationContract]
        NFMT.Common.ResultModel Pledge(NFMT.Common.UserModel user, NFMT.WareHouse.Model.StockLog stockLog, List<NFMT.WareHouse.Model.StockLogAttach> stockLogAttachs);

        [OperationContract]
        NFMT.Common.ResultModel Repo(NFMT.Common.UserModel user, NFMT.WareHouse.Model.StockLog stockLog, List<NFMT.WareHouse.Model.StockLogAttach> stockLogAttachs);
    }
}
