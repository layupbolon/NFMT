/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FuturesPriceBLL.cs
// 文件功能描述：期货价格表dbo.FuturesPrice业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年5月4日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Data.Model;
using NFMT.Data.DAL;
using NFMT.Data.IDAL;
using NFMT.Common;

namespace NFMT.Data.BLL
{
    /// <summary>
    /// 期货价格表dbo.FuturesPrice业务逻辑类。
    /// </summary>
    public class FuturesPriceBLL : Common.DataBLL
    {
        private FuturesPriceDAL futurespriceDAL = new FuturesPriceDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(FuturesPriceDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FuturesPriceBLL()
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
            get { return this.futurespriceDAL; }
        }

        #endregion

        /// <summary>
        /// 获取分页查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="status"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, DateTime tradeDate, DateTime deliverDate, string tradeCode)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "FPId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "   FPId,TradeDate,DeliverDate,TradeCode,SettlePrice ";
            select.TableName = " dbo.FuturesPrice ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" 1=1 ");

            //if (tradeDate!=null)
            //    sb.AppendFormat(" and TradeDate = '{0}'", tradeDate);
            //if (deliverDate!=null)
            //    sb.AppendFormat(" and DeliverDate = '{0}'", deliverDate);
            //if (!string.IsNullOrEmpty(tradeCode))
            //    sb.AppendFormat(" and TradeCode = '{0}'", tradeCode);

            select.WhereStr = sb.ToString();

            return select;
        }
        
    }
}
