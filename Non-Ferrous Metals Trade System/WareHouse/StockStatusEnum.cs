/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockStatusEnum.cs
// 文件功能描述：库存状态枚举。
// 创建人：pekah.chow
// 创建时间： 2014-06-12
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.WareHouse
{
    /// <summary>
    /// 库存状态
    /// </summary>
    public enum StockStatusEnum
    {
        作废库存 = 202,
        流程中库存 = 205,
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
