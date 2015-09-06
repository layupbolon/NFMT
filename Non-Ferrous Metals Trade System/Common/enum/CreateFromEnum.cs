using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Common
{
    /// <summary>
    /// 创建来源枚举
    /// </summary>
    public enum CreateFromEnum
    {
        采购合约库存创建 = 308,
        销售合约库存创建 = 310,
        合约直接创建 = 311,
        出库申请同合约创建 = 312,
        出库申请中制单申请创建 = 313,
        出库申请直接创建 = 314,
        制单指令创建 = 324
    }
}
