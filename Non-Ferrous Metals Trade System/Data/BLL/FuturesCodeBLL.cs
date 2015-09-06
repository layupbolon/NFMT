/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FuturesCodeBLL.cs
// 文件功能描述：期货合约dbo.FuturesCode业务逻辑类。
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
    /// 期货合约dbo.FuturesCode业务逻辑类。
    /// </summary>
    public class FuturesCodeBLL : Common.DataBLL
    {
        private FuturesCodeDAL futurescodeDAL = new FuturesCodeDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(FuturesCodeDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FuturesCodeBLL()
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
            get { return this.futurescodeDAL; }
        }

        #endregion

        #region 新增方法

        /// <summary>
        /// 获取分页查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="status"></param>
        /// <param name="exchange"></param>
        /// <param name="firstTradeDate"></param>
        /// <param name="lastTradeDate"></param>
        /// <param name="asset"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, DateTime firstTradeDate, DateTime lastTradeDate, int exchageId, int selFuturesCodeStatus)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "fc.FuturesCodeId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " FuturesCodeId,ec.ExchangeName as ExchangeName,FirstTradeDate,LastTradeDate,CONVERT(varchar(20),Convert(int,CodeSize))+mu.MUName as CodeSize,c.CurrencyName as CurrencyName,FuturesCodeStatus,TradeCode,bd.StatusName as StatusName,fc.AssetId,ass.AssetName  ";
            select.TableName = "  dbo.FuturesCode  fc " +
                                                 " inner join dbo.Exchange ec on fc.ExchageId=ec.ExchangeId" +
                                                 " inner join dbo.MeasureUnit mu on fc.MUId=mu.MUId" +
                                                 " inner join dbo.Asset ass on ass.AssetId=fc.AssetId" +
                                                 " inner join dbo.Currency c on fc.CurrencyId =c.CurrencyId" +
                                                 " inner join dbo.BDStatusDetail bd on fc.FuturesCodeStatus=bd.DetailId ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" bd.StatusId = 1 ");

            if (selFuturesCodeStatus > 0)
                sb.AppendFormat(" and FuturesCodeStatus = {0}", selFuturesCodeStatus);
            if (exchageId > 0)
                sb.AppendFormat(" and ExchageId = {0}", exchageId);
            if (firstTradeDate > NFMT.Common.DefaultValue.DefaultTime)
                sb.AppendFormat(" and FirstTradeDate >= '{0}'", firstTradeDate);
            if (lastTradeDate > NFMT.Common.DefaultValue.DefaultTime)
                sb.AppendFormat(" and LastTradeDate <= '{0}'", lastTradeDate);

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion

        #region 重载方法

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                Model.FuturesCode obj1 = (Model.FuturesCode)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, obj.Id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.FuturesCode resultObj = (Model.FuturesCode)result.ReturnValue;

                    if (resultObj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }

                    resultObj.FirstTradeDate = obj1.FirstTradeDate;
                    resultObj.LastTradeDate = obj1.LastTradeDate;
                    resultObj.MUId = obj1.MUId;
                    resultObj.CurrencyId = obj1.CurrencyId;
                    resultObj.FuturesCodeId = obj1.FuturesCodeId;
                    resultObj.ExchageId = obj1.ExchageId;
                    resultObj.CodeSize = obj1.CodeSize;
                    resultObj.TradeCode = obj1.TradeCode;
                    resultObj.FuturesCodeStatus = obj1.FuturesCodeStatus;

                    result = this.Operate.Update(user, resultObj);

                    if (result.ResultStatus != 0)
                        return result;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        #endregion
    }
}
