/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BasicAuth.cs
// 文件功能描述：权限基类，基类权限无限制。
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
    public class BasicAuth : Common.IAuthority
    {
        private List<string> authColumnNames = new List<string>();

        public virtual Common.ResultModel CreateAuthorityStr(Common.UserModel user)
        {
            Common.ResultModel result = new Common.ResultModel();

            result.ResultStatus = 0;
            result.Message = "";
            result.ReturnValue = string.Empty;

            return result;
        }

        public virtual bool OperateAuthority(UserModel user, OperateEnum operate)
        {
            return true;
        }

        public virtual List<string> AuthColumnNames
        {
            get { return this.authColumnNames; }
        }
    }
}
