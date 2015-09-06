/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockInAttachBLL.cs
// 文件功能描述：入库登记附件dbo.StockInAttach业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.WareHouse.Model;
using NFMT.WareHouse.DAL;
using NFMT.WareHouse.IDAL;
using NFMT.Common;

namespace NFMT.WareHouse.BLL
{
    /// <summary>
    /// 入库登记附件dbo.StockInAttach业务逻辑类。
    /// </summary>
    public class StockInAttachBLL : Common.ExecBLL
    {
        private StockInAttachDAL stockinattachDAL = new StockInAttachDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StockInAttachDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockInAttachBLL()
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
            get { return this.stockinattachDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int stockInId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sia.AttachType desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " sia.StockInAttachId,sia.StockInId,sia.AttachId,sia.AttachType,bd.DetailName, a.AttachName,a.ServerAttachName,a.AttachExt,a.AttachInfo,a.AttachPath,a.CreateTime ";
            select.TableName = " dbo.St_StockInAttach sia inner join NFMT..Attach a on sia.AttachId = a.AttachId left join NFMT_Basic..BDStyleDetail bd on bd.StyleDetailId = sia.AttachType";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat(" a.AttachStatus <> {0} ", (int)Common.StatusEnum.已作废);
            if (stockInId > 0)
                sb.AppendFormat(" and sia.StockInId = {0}", stockInId);

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion
    }
}
