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
    
    public partial class St_StockMove
    {
        public int StockMoveId { get; set; }
        public int StockMoveApplyId { get; set; }
        public Nullable<int> Mover { get; set; }
        public Nullable<System.DateTime> MoveTime { get; set; }
        public Nullable<int> MoveStatus { get; set; }
        public string MoveMemo { get; set; }
        public Nullable<int> CreatorId { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> LastModifyId { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
    }
}
