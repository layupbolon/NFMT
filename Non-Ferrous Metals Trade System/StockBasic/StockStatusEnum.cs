using System;
using System.Collections.Generic;
using System.Text;

namespace NFMT.StockBasic
{
    /// <summary>
    /// 库存状态
    /// </summary>
    public enum StockStatusEnum
    {
        作废库存 = 202,
        预入库存 = 210,
        在库正常 = 220,
        新拆库存 = 230,
        预拆库存 = 235,
        已拆库存 = 238,
        预移库存 = 240,
        预押库存 = 250,
        质押库存 = 255,
        预回购库存 = 260,
        预报关库存 = 270,
        预售库存 = 280,
        已售库存 = 285
    }
}
