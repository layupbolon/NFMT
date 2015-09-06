using NFMT.Common;
using NFMT.WareHouse.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.WareHouse.DAL
{
    /// <summary>
    /// 出库dbo.St_StockOut数据交互类。
    /// </summary>
    public partial class StockOutDAL : ExecOperate, IStockOutDAL
    {
        #region 新增方法

        public override int MenuId
        {
            get
            {
                return 44;
            }
        }

        #endregion
    }
}
