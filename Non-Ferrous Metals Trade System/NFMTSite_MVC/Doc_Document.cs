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
    
    public partial class Doc_Document
    {
        public int DocumentId { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<System.DateTime> DocumentDate { get; set; }
        public Nullable<int> DocEmpId { get; set; }
        public Nullable<System.DateTime> PresentDate { get; set; }
        public Nullable<int> Presenter { get; set; }
        public Nullable<System.DateTime> AcceptanceDate { get; set; }
        public Nullable<int> Acceptancer { get; set; }
        public string Meno { get; set; }
        public Nullable<int> DocumentStatus { get; set; }
        public Nullable<int> CreatorId { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> LastModifyId { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
    }
}
