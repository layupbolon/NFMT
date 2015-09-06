using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Invoice
{
    public enum InvoiceTypeEnum
    {
        FinanceInvoice=171,
        财务发票 = FinanceInvoice,

        DirectFinalInvoice=172,
        直接终票 = DirectFinalInvoice,

        ReplaceFinalInvoice=173,
        替临终票 = ReplaceFinalInvoice,

        SuppleFinalInvoice=174,
        补临终票 = SuppleFinalInvoice,

        ProvisionalInvoice=175,
        临时发票 = ProvisionalInvoice,

        SI=176,
        价外票 = SI
    }
}
