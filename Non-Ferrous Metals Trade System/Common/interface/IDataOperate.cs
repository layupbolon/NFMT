/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：IDataOperate.cs
// 文件功能描述：基础类数据操作接口。
// 创建人：pekah.chow
// 创建时间： 2014-04-23
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace NFMT.Common
{
    public interface IDataOperate : IOperate
    {
        /// <summary>
        /// 冻结数据
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">被冻结的数据对象</param>
        /// <returns></returns>
        ResultModel Freeze(UserModel user, IModel obj);

        /// <summary>
        /// 解除冻结
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">被解除冻结的数据对象</param>
        /// <returns></returns>
        ResultModel UnFreeze(UserModel user, IModel obj);
    }
}
