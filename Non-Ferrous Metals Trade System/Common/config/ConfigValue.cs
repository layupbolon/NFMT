using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Common
{
    public static class ConfigValue
    {
        private static bool isReuseEmpNo = false;
        /// <summary>
        /// 设置是否可以重用员工号
        /// </summary>
        public static bool IsReuseEmpNo
        {
            get { return isReuseEmpNo; }
            set { isReuseEmpNo = value; }
        }
    }
}
