/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：IExecOperate.cs
// 文件功能描述：执行类数据操作接口。
// 创建人：pekah.chow
// 创建时间： 2014-08-07
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace NFMT.Common
{
    public interface IExecOperate:IOperate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        ResultModel Complete(UserModel user, IModel obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        ResultModel CompleteCancel(UserModel user, IModel obj);
    }
}
