/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：IApplyOperate.cs
// 文件功能描述：申请类数据操作接口。
// 创建人：pekah.chow
// 创建时间： 2014-04-23
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace NFMT.Common
{
    public interface IApplyOperate : IOperate
    {
        /// <summary>
        /// 执行完成确认
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">执行完成确认的数据对象</param>
        /// <returns></returns>
        ResultModel Confirm(UserModel user, IModel obj);

        /// <summary>
        /// 部分执行完成确认
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">部分执行完成确认的数据对象</param>
        /// <returns></returns>
        ResultModel PartiallyConfirm(UserModel user, IModel obj);

        /// <summary>
        /// 取消执行完成确认
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">取消执行完成确认的数据对象</param>
        /// <returns></returns>
        ResultModel ConfirmCancel(UserModel user, IModel obj);
    }
}
