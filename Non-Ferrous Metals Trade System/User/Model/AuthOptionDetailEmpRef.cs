/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AuthOptionDetailEmpRef.cs
// 文件功能描述：dbo.AuthOptionDetailEmp_Ref实体类。
// 创建人：pekah.chow
// 创建时间： 2014年6月24日
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.User.Model
{
    public class AuthOptionDetailEmpRef
    {
        public int RefId { get; internal set; }

        public int EmpId { get; set; }

        public int DetailId { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreateTime { get; set; }

        public int LastModifyId { get; set; }

        public DateTime LastModifyTime { get; set; }
    }
}
