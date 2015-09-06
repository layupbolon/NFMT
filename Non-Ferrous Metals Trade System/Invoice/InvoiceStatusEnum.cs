/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceStatusEnum.cs
// 文件功能描述：发票状态枚举。
// 创建人：pekah.chow
// 创建时间： 2014-06-12
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Invoice
{
    /// <summary>
    /// 发票状态
    /// </summary>
    public enum InvoiceStatusEnum
    {
        拟收取 = 0,
        收取 = 1,
        拟开出 = 2,
        开出 = 3,
        作废 = 4,
    }
}
