using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Authority
{
    /// <summary>
    /// 公司权限+购销权限
    /// </summary>
    public class CorpTradeDirectionAuth : NFMT.Common.BasicAuth
    {
        /// <summary>
        /// 通过己方公司，来获取购销数据
        /// 存储过程参数说明
        /// 1：当前操作用户
        /// 2：己方公司序号
        /// 3：购销字段名
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override Common.ResultModel CreateAuthorityStr(Common.UserModel user)
        {
            Common.ResultModel result = new Common.ResultModel();

            if (user == null)
                return result;

            string str = string.Format(" and NFMT_User.dbo.CorpTradeDirectionAuth({0},{1},{2})=1", user.EmpId, this.AuthColumnNames[0], this.AuthColumnNames[1]);
            result.ResultStatus = 0;
            result.ReturnValue = str;

            return result;
        }

        /// <summary>
        /// 权限字段说明
        /// 1：己方公司序号
        /// 2：购销字段名
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
