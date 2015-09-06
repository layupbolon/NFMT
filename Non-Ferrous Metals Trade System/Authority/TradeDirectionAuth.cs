using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Authority
{
    /// <summary>
    /// 购销权限
    /// </summary>
    public class TradeDirectionAuth:NFMT.Common.BasicAuth
    {
        /// <summary>
        /// 购销权限
        /// </summary>
        /// <param name="user">当前用户</param>
        /// <returns></returns>
        public override Common.ResultModel CreateAuthorityStr(Common.UserModel user)
        {
            Common.ResultModel result = new Common.ResultModel();

            if (user == null)
                return result;

            string str = string.Format(" and NFMT_User.dbo.TradeDirectionAuth({0},{1})=1", user.EmpId, this.AuthColumnNames[0]);
            result.ResultStatus = 0;
            result.ReturnValue = str;

            return result;
        }

        /// <summary>
        /// 权限字段说明
        /// 1：购销字段名
        /// </summary>
        public override List<string> AuthColumnNames
        {
            get
            {
                return base.AuthColumnNames;
            }
        }
    }
}
