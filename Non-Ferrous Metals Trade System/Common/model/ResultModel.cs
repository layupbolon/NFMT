/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ResultModel.cs
// 文件功能描述：返回结果实体。
// 创建人：pekah.chow
// 创建时间： 2014-04-23
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace NFMT.Common
{
    public class ResultModel
    {
        public ResultModel()
        {
            this.AffectCount = -1;
            this.Message = "默认错误";
            this.ResultStatus = -1;
            this.ReturnValue = null;
        }


        /// <summary>
        /// 受影响的行数
        /// </summary>
        public int AffectCount { get; set; }

        /// <summary>
        /// 返回的消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 消息状态。0为完成，其他为错误
        /// </summary>
        public int ResultStatus { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public object ReturnValue { get; set; }
    }
}
