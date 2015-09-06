using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Operate
{
    public enum ApplyType
    {
        StockOutApply = 148,
        StockMoveApply = 149,
        PledgeApply = 152,
        RepoApply = 153,
        PayApply =160,
        RecAllot =161,
        PricingApply= 240,
        StopLossApply = 244,
        CustomApply = 250,
        InvoiceApply = 321,
        出库申请 = StockOutApply,
        移库申请 = StockMoveApply,
        质押申请 = PledgeApply,
        回购申请 = RepoApply,
        付款申请 = PayApply,
        收款分配 = RecAllot,
        点价申请 = PricingApply,
        止损申请 = StopLossApply,
        报关申请 = CustomApply,
        开票申请 = InvoiceApply
    }
}
