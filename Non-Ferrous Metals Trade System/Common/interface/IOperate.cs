/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：IOperate.cs
// 文件功能描述：操作接口。
// 创建人：pekah.chow
// 创建时间： 2014-04-23
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace NFMT.Common
{
    public interface IOperate
    {
        /// <summary>
        /// 通过Id获取实体对象
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="id">实体对象序号</param>
        /// <returns></returns>
        ResultModel Get(UserModel user, int id);

        /// <summary>
        /// 获取对象实例列表
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <returns></returns>
        ResultModel Load(UserModel user);

        ResultModel Load<T>(UserModel user) where T:class,IModel;

        /// <summary>
        /// 获取对象实例列表
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="select">查询实体对象</param>
        /// <returns></returns>
        ResultModel Load(UserModel user,SelectModel select);

        ResultModel Load(UserModel user, SelectModel select, IAuthority authority);

        /// <summary>
        /// 添加数据至数据库
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">添加对象</param>
        /// <returns></returns>
        ResultModel Insert(UserModel user, IModel obj);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">更新对象</param>
        /// <returns></returns>
        ResultModel Update(UserModel user, IModel obj);

        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">提交审核的数据对象</param>
        /// <returns></returns>
        ResultModel Submit(UserModel user, IModel obj);

        /// <summary>
        /// 审核数据。true为通过，false为不通过
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">审核的数据对象</param>
        /// <param name="isPass">是否通过，true为审核通过，false为审核拒绝</param>
        /// <returns></returns>
        ResultModel Audit(UserModel user, IModel obj, bool isPass);

        /// <summary>
        /// 数据撤返
        /// </summary>
        /// <param name="model">当前操作用户</param>
        /// <param name="obj">撤返的数据对象</param>
        /// <returns></returns>
        ResultModel Goback(UserModel model, IModel obj);

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">作废的数据对象</param>
        /// <returns></returns>
        ResultModel Invalid(UserModel user, IModel obj);

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="obj">关闭的数据对象</param>
        /// <returns></returns>
        ResultModel Close(UserModel user, IModel obj);

        /// <summary>
        /// 返回是否允许对该实例进行相应操作。
        /// </summary>
        /// <param name="obj">判断实例</param>
        /// <param name="operate">操作类型</param>
        /// <returns>返回true则可以进行操作，返回false则不可进行操作。</returns>
        ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate);

        /// <summary>
        /// 数据权限接口
        /// </summary>
        IAuthority Authority { get;}

        /// <summary>
        /// 菜单序号
        /// </summary>
        int MenuId { get; }
    }
}
