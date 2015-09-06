using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Authority
{
    public class DeptAuth 
    {
        public Common.ResultModel CreateAuthorityStr(Common.UserModel user)
        {
            Common.ResultModel result = new Common.ResultModel();
            
            if (user == null)
            {
                result.ResultStatus = -1;
                result.Message = "用户未登录";
            }

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;

            result.ReturnValue = string.Format(" and mt.CreatorId in (select de1.EmpId from dbo.DeptEmp de1 inner join dbo.DeptEmp de2 on de1.DeptId = de2.DeptId and de2.RefStatus={1} and de2.EmpId={0} where de1.RefStatus={1} group by de1.EmpId)", user.EmpId,readyStatus);

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
