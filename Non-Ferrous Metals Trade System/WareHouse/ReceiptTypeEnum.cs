using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.WareHouse
{
    public enum ReceiptTypeEnum
    {
        StockInReceipt =224,
        入库回执 = StockInReceipt,

        StockOutReceipt =225,
        出库回执 = StockOutReceipt
    }
}
