using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Document
{
    public enum DocumentStatusEnum
    {
        已作废 = 1,
        已关闭 = 10,
        已录入 = 20,
        已撤返 = 25,
        审核拒绝 = 35,
        待审核 = 40,
        已生效 = 50,
        已交单 = 560,
        已承兑 = 570,
        银行退单 = 580,
        已完成 = 80
    }
}
