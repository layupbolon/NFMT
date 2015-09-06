/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SplitDocDetailBLL.cs
// 文件功能描述：拆单明细dbo.St_SplitDocDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年10月27日
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
    /// 拆单明细dbo.St_SplitDocDetail业务逻辑类。
    /// </summary>
    public class SplitDocDetailBLL : Common.ExecBLL
    {
        private SplitDocDetailDAL splitdocdetailDAL = new SplitDocDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(SplitDocDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SplitDocDetailBLL()
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
            get { return this.splitdocdetailDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int splitDocId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "sd.DetailId asc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" sd.DetailId,sd.NewRefNo,sd.GrossAmount,sd.NetAmount,sd.UnitId,mu.MUName,sd.AssetId,ass.AssetName,sd.Bundles,sd.BrandId,br.BrandName,sd.PaperNo,sd.PaperHolder,e.Name,sd.CardNo,sd.Memo");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.St_SplitDocDetail sd ");
            //sb.Append(" left join dbo.St_StockName sn on sd.NewRefNoId = sn.StockNameId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on sd.UnitId = mu.MUId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on sd.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.Brand br on sd.BrandId = br.BrandId ");
            sb.Append(" left join NFMT_User.dbo.Employee e on sd.PaperHolder = e.EmpId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" sd.SplitDocId = {0} and sd.DetailStatus >= {1} ", splitDocId, (int)Common.StatusEnum.已生效);

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion
    }
}
