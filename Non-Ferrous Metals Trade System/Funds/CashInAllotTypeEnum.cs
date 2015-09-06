using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Funds
{
    public enum CashInAllotTypeEnum
    {
        Corp = 246,
        收款分配至公司= Corp,

        Contract =247,
        收款分配至合约 = Contract,

        Stock = 248,
        收款分配至库存 = Stock,

        Invoice = 249,
        收款分配至发票 = Invoice
    }
}
