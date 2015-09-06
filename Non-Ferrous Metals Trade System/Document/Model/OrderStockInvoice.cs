using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Document.Model
{
    /// <summary>
    /// 制单指令库存发票明细实体
    /// </summary>
    [Serializable]
    public class OrderStockInvoice
    {
        private int stockId = 0;
        private decimal applyWeight = 0;
        private decimal invoiceBala = 0;
        private string invoiceNo = string.Empty;
        private string refNo = string.Empty;

        public OrderStockInvoice() { }

        public int StockId
        {
            get { return this.stockId; }
            set { this.stockId = value; }
        }

        public decimal ApplyWeight
        {
            get { return this.applyWeight; }
            set { this.applyWeight = value; }
        }

        public decimal InvoiceBala
        {
            get { return this.invoiceBala; }
            set { this.invoiceBala = value; }
        }

        public string InvoiceNo
        {
            get { return this.invoiceNo; }
            set { this.invoiceNo = value; }
        }

        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }

    }

    [Serializable]
    public class OrderReplaceStock : OrderStockInvoice
    {
        private int detailId = 0;
        public int DetailId 
        {
            get { return this.detailId; }
            set { this.detailId = value; }
        }
    }
}
