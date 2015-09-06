/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsStatus.cs
// 文件功能描述：消息状态枚举。
// 创建人：Eric.Yin
// 创建时间： 2014-09-11
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace NFMT.Sms
{
    /// <summary>
    /// 消息状态枚举
    /// </summary>
    public enum SmsStatusEnum
    {
        无效消息 = 300,
        待处理消息 = 301,
        已处理消息 = 302
    }
}
