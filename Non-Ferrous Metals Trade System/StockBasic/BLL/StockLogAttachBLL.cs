using NFMT.Common;
using NFMT.StockBasic.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.StockBasic.BLL
{
    /// <summary>
    /// 流水附件dbo.StockLogAttach业务逻辑类。
    /// </summary>
    public class StockLogAttachBLL : Common.DataBLL
    {
        private StockLogAttachDAL stocklogattachDAL = new StockLogAttachDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StockLogAttachDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockLogAttachBLL()
        {
        }

        #endregion

        #region 数据库操作

        protected override log4net.ILog Log
        {
            get { return this.log; }
        }

        public override IOperate Operate
        {
            get { return this.stocklogattachDAL; }
        }
        #endregion
    }
}
