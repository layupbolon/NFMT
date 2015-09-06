using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Funds
{
    public enum PayMatterEnum
    {
        合约定金 = 1,
        预付款保证金 = 2,
        货款 = 3,
        价外费用 = 4,
        退款 = 5,
        尾款 = 6,
        背靠背 = 337,
        货物质押采购首次不带票 = 338,
        货物质押采购带票 = 339,
        库存商品采购 = 340,
        营业费用 = 341
    }
}
