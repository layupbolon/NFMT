using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.RollBack
{
    public class OperateModel
    {
        /// <summary>
        /// 操作序号
        /// </summary>
        public int OperateId { get; set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        public string OperateName { get; set; }

        /// <summary>
        /// 操作发生页面名称
        /// </summary>
        public string PageName { get; set; }

        /// <summary>
        /// 操作引发按键名称
        /// </summary>
        public string ButtonName { get; set; }

        /// <summary>
        /// 操作引发事件名称
        /// </summary>
        public string EventName { get; set; }
    }
}
