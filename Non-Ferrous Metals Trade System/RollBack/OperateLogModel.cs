using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.RollBack
{
    public class OperateLogModel
    {
        /// <summary>
        /// 记录序号
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// 操作序号
        /// </summary>
        public int OperateId { get; set; }

        /// <summary>
        /// 操作员序号
        /// </summary>
        public int EmpId { get; set; }

        /// <summary>
        /// 操作发生时间
        /// </summary>
        public DateTime OperateTime { get; set; }
    }
}
