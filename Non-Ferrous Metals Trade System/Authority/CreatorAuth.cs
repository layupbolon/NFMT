using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Authority
{
    public class CreatorAuth
    {
        public Common.ResultModel CreateAuthorityStr(Common.UserModel user)
        {
            Common.ResultModel result = new Common.ResultModel();

            if (user == null)
            {
                result.ResultStatus = -1;
                result.Message = "用户未登录";
            }

            result.ResultStatus = -1;
            result.ReturnValue = string.Format(" and mt.CreatorId = {0}",user.EmpId);

            return result;
        }

        public bool OperateAuthority(Common.UserModel user, Common.OperateEnum operate)
        {
            throw new NotImplementedException();
        }


        public List<string> AuthColumnName
        {
            get { throw new NotImplementedException(); }
        }
    }
}
