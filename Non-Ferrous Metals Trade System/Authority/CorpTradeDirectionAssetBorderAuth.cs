using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Authority
{
    /// <summary>
    /// 公司权限+购销权限+品种权限+内外贸权限
    /// </summary>
    public class CorpTradeDirectionAssetBorderAuth 
    {
        public Common.ResultModel CreateAuthorityStr(Common.UserModel user)
        {
            throw new NotImplementedException();
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
