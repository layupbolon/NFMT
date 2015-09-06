using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Authority
{
    /// <summary>
    /// 库存权限=内外贸权限+品种权限+公司权限
    /// </summary>
    public class StockAuth : NFMT.Common.BasicAuth
    {
        public override Common.ResultModel CreateAuthorityStr(Common.UserModel user)
        {
            Common.ResultModel result = new Common.ResultModel();

            if (user == null)
                return result;

            if (this.AuthColumnNames.Count == 0)
                this.AuthColumnNames.Add("sto.StockId");

            string str = string.Format(" and NFMT_User.dbo.StockAuth({0},{1})=1", user.EmpId, this.AuthColumnNames[0]);
            result.ResultStatus = 0;
            result.ReturnValue = str;

            return result;
        }

        /// <summary>
        /// 存储过程参数
        /// 1：库存序号字段名
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
