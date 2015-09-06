using NFMT.Common;
using NFMT.Invoice.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Invoice.DAL
{
    public partial class BusinessInvoiceDetailDAL : ExecOperate, IBusinessInvoiceDetailDAL
    {
        #region 新增方法

        public ResultModel Load(UserModel user, int businessInvoiceId, StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.Inv_BusinessInvoiceDetail where BusinessInvoiceId={0} and DetailStatus ={1}", businessInvoiceId, (int)status);
            return Load<Model.BusinessInvoiceDetail>(user, CommandType.Text, cmdText);
        }

        #endregion
    }
}
