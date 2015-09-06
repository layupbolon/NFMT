using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Common
{
    public enum LogEnum
    {
        出库 = 108,
        出库冲销 = 110,
        入库 = 109,
        入库冲销 = 110,
        移库 = 5,
        移库冲销 = 6,
        质押 = 111,
        质押冲销 = 110,
        回购 = 112,
        回购冲销 = 110,
        收款 = 11,
        收款冲销 = 12,
        付款 = 13,
        付款冲销 = 14,
        开票 = 15,
        开票冲销 = 16,
        收票 = 17,
        收票冲销 = 18,
        拆单 = 19,
        拆单冲销 = 20,
        报关 = 21,
        报关冲销 = 22
    }
}
