using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Authority
{
    /// <summary>
    /// 合约权限
    /// </summary>
    public class ContractAuth :NFMT.Common.BasicAuth
    {        
        public override Common.ResultModel CreateAuthorityStr(Common.UserModel user)
        {
            Common.ResultModel result = new Common.ResultModel();

            if (user == null)
                return result;

            if (this.AuthColumnNames.Count == 0)
                this.AuthColumnNames.Add("con.ContractId");

            string str = string.Format(" and NFMT_User.dbo.ContractAuth({0},{1})=1", user.EmpId, this.AuthColumnNames[0]);
            result.ResultStatus = 0;
            result.ReturnValue = str;

            return result;
        }

        public override bool OperateAuthority(Common.UserModel user, Common.OperateEnum operate)
        {
            return true;
            //NFMT.User.DAL.RoleMenuOperateDAL dal = new User.DAL.RoleMenuOperateDAL();
            //return dal.AuthorityOperate(user, operate, NFMT.User.MenuEnum.Contract);
        }

        /// <summary>
        /// 权限函数参数列表
        /// 1:合约序号字段名
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
