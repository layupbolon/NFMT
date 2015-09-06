using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.RollBack
{
    public class OperateLogDetailModel
    {
        /// <summary>
        /// 操作记录明细序号
        /// </summary>
        public int DetailId { get; set; }

        /// <summary>
        /// 操作记录序号
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// 操作影响表名
        /// </summary>
        public string SourceTable { get; set; }

        /// <summary>
        /// 操作影响数据序号
        /// </summary>
        public int RowId { get; set; }

        /// <summary>
        /// 前一版本号
        /// </summary>
        public int PreVer { get; set; }

        /// <summary>
        /// 当前版本号
        /// </summary>
        public int CurVer { get; set; }

    }
}
