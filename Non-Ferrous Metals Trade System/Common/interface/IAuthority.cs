/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：IAuthority.cs
// 文件功能描述：权限接口。
// 创建人：pekah.chow
// 创建时间： 2014-05-28
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Common
{
    public interface IAuthority
    {
        /// <summary>
        /// 创建权限SQL字符串
        /// </summary>
        /// <param name="user">当前用户</param>
        /// <returns></returns>
        ResultModel CreateAuthorityStr(UserModel user);

        /// <summary>
        /// 操作权限验证
        /// </summary>
        /// <param name="user">当前用户</param>
        /// <param name="operate">操作项</param>
        /// <returns></returns>
        bool OperateAuthority(UserModel user,OperateEnum operate);

        /// <summary>
        /// 权限批配字段列表
        /// </summary>
        List<string> AuthColumnNames { get; }

    }
}
