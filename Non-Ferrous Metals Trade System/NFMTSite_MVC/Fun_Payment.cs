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
    
    public partial class Fun_Payment
    {
        public int PaymentId { get; set; }
        public Nullable<int> PayApplyId { get; set; }
        public string PaymentCode { get; set; }
        public Nullable<decimal> PayBala { get; set; }
        public Nullable<decimal> FundsBala { get; set; }
        public Nullable<decimal> VirtualBala { get; set; }
        public Nullable<int> CurrencyId { get; set; }
        public Nullable<int> PayStyle { get; set; }
        public Nullable<int> PayBankId { get; set; }
        public Nullable<int> PayBankAccountId { get; set; }
        public Nullable<int> PayCorp { get; set; }
        public Nullable<int> PayDept { get; set; }
        public Nullable<int> PayEmpId { get; set; }
        public Nullable<System.DateTime> PayDatetime { get; set; }
        public Nullable<int> RecevableCorp { get; set; }
        public Nullable<int> ReceBankId { get; set; }
        public Nullable<int> ReceBankAccountId { get; set; }
        public string ReceBankAccount { get; set; }
        public Nullable<int> PaymentStatus { get; set; }
        public string FlowName { get; set; }
        public string Memo { get; set; }
        public Nullable<int> FundsLogId { get; set; }
        public Nullable<int> CreatorId { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> LastModifyId { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
    }
}
