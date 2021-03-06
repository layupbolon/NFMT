﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Authority
{
    public class CorpAuth : NFMT.Common.BasicAuth
    {
        public override Common.ResultModel CreateAuthorityStr(Common.UserModel user)
        {
            Common.ResultModel result = new Common.ResultModel();

            if (user == null)
                return result;

            if (this.AuthColumnNames.Count == 0)
                this.AuthColumnNames.Add("innerCorp.CorpId");

            string str = string.Format(" and NFMT_User.dbo.CorpAuth({0},{1})=1", user.EmpId, this.AuthColumnNames[0]);
            result.ResultStatus = 0;
            result.ReturnValue = str;

            return result;
        }
    }
}
