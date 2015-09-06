using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.WareHouse
{
    public enum StockOperateEnum
    {
        入库登记=1,
        入库登记取消=2,
        入库=3,
        出库申请=4,
        出库申请取消 = 5,
        出库=6,
        质押申请=7,
        质押=8,
        回购申请=9,
        回购=10,
        冲销 =99
    }
}
