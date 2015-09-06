/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsStatusEnum.cs
// 文件功能描述：在职状态枚举。
// 创建人：Eric.Yin
// 创建时间： 2014-09-17
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace NFMT.User
{
    /// <summary>
    /// 在职状态枚举
    /// </summary>
    public enum WorkStatusEnum
    {
        在职 = 401,
        离职 = 402,
        停职 = 403,
        退休 = 404
    }
}
