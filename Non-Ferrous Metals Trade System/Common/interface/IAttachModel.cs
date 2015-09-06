using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Common
{
    public interface IAttachModel : IModel
    {
        /// <summary>
        /// 附件序号
        /// </summary>
        int AttachId { get; set; }

        /// <summary>
        /// 业务数据序号
        /// </summary>
        int BussinessDataId { get; set; }
    }
}
