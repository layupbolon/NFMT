using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Operate
{
    public enum AttachType
    {
        ContractAttach = 228,
        合约附件 = ContractAttach,

        StockInAttach = 229,
        入库附件 = StockInAttach,

        StockLogAttach = 231,
        库存流水附件 = StockLogAttach,

        StockAttach = 232,
        库存附件 = StockAttach,

        StockOutAttach = 234,
        出库附件 = StockOutAttach,

        CashInAttach = 235,
        收款附件 = CashInAttach,

        PaymentAttach = 236,
        付款附件 = PaymentAttach,

        InvoiceAttach = 238,
        发票附件 = InvoiceAttach,

        SplitDocAttach = 245,
        拆单附件 = SplitDocAttach,

        CustomApplyAttach = 251,
        报关申请附件 = CustomApplyAttach,

        CustomAttach = 252,
        报关附件 = CustomAttach,

        SubAttach= 253,
        子合约附件 = SubAttach,

        OrderAttach = 279,
        制单指令附件 = OrderAttach,

        PledgeAttach = 289,
        质押附件 = PledgeAttach,

        BusinessLiceneseAttach = 317,
        营业执照附件 = BusinessLiceneseAttach,

        TaxAttach = 318,
        税务附件 = TaxAttach,

        OrganizationAttach = 319,
        组织机构代码附件 = OrganizationAttach,

        CertifyAttach = 320,
        证明文件 = CertifyAttach,

        BillAttach = 330,
        单据附件 = BillAttach,

        PurchaseDoubleContractAttach = 342,
        采购双签合同 = PurchaseDoubleContractAttach,

        PurchaseContractAttach = 348,
        采购合同 = PurchaseContractAttach,

        LibraryBillAttach = 349,
        在库提单附件 = LibraryBillAttach,

        WayBillAttach = 350,
        在途提单附件 = WayBillAttach,

        InvoiceScanningAttach = 351,
        发票扫描件 = InvoiceScanningAttach,

        PriceConfirmationAttach = 352,
        点价确认单 = PriceConfirmationAttach,

        ContractStatementAttach = 353,
        合同结算单 = ContractStatementAttach,

        CostBreakdownListAttach = 354,
        费用明细清单 = CostBreakdownListAttach
    }
}
