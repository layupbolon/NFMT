//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NFMTSite_MVC
{
    using System;
    using System.Collections.Generic;
    
    public partial class St_StockReceiptDetail
    {
        public int DetailId { get; set; }
        public int ReceiptId { get; set; }
        public Nullable<int> ContractId { get; set; }
        public Nullable<int> ContractSubId { get; set; }
        public Nullable<int> StockId { get; set; }
        public Nullable<int> StockLogId { get; set; }
        public Nullable<decimal> PreNetAmount { get; set; }
        public Nullable<decimal> ReceiptAmount { get; set; }
        public Nullable<decimal> QtyMiss { get; set; }
        public Nullable<decimal> QtyRate { get; set; }
        public Nullable<int> DetailStatus { get; set; }
        public Nullable<int> CreatorId { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> LastModifyId { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
    }
}
