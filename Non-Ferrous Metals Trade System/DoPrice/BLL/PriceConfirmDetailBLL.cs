/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PriceConfirmDetailBLL.cs
// 文件功能描述：价格确认明细dbo.Pri_PriceConfirmDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年1月26日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.DoPrice.Model;
using NFMT.DoPrice.DAL;
using NFMT.DoPrice.IDAL;
using NFMT.Common;

namespace NFMT.DoPrice.BLL
{
    /// <summary>
    /// 价格确认明细dbo.Pri_PriceConfirmDetail业务逻辑类。
    /// </summary>
    public class PriceConfirmDetailBLL : Common.ExecBLL
    {
        private PriceConfirmDetailDAL priceconfirmdetailDAL = new PriceConfirmDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PriceConfirmDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PriceConfirmDetailBLL()
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
            get { return this.priceconfirmdetailDAL; }
        }

        #endregion

        #region 新增方法

        public Common.SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr,int priceConfirmId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "detail.DetailId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("detail.DetailId,detail.PriceConfirmId,detail.InterestStartDate,detail.InterestEndDate,CONVERT(varchar,detail.InterestDay) + '天' as InterestDayName,detail.InterestDay,detail.InterestUnit,CONVERT(varchar,detail.InterestUnit)+mu.MUName as InterestUnitName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Pri_PriceConfirmDetail detail ");
            sb.Append(" left join dbo.Pri_PriceConfirm pc on detail.PriceConfirmId = pc.PriceConfirmId ");
            sb.Append(" left join dbo.Con_Contract con on pc.ContractId = con.ContractId ");
            sb.Append(" left join NFMT_Basic..MeasureUnit mu on con.UnitId = mu.MUId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" detail.PriceConfirmId = {0} and detail.DetailStatus = {1} ", priceConfirmId, (int)Common.StatusEnum.已生效);
            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion
    }
}
