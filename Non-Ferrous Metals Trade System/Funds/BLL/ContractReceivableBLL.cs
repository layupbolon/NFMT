/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractReceivableBLL.cs
// 文件功能描述：收款分配至合约dbo.Fun_ContractReceivable_Ref业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Funds.Model;
using NFMT.Funds.DAL;
using NFMT.Funds.IDAL;
using NFMT.Common;

namespace NFMT.Funds.BLL
{
    /// <summary>
    /// 收款分配至合约dbo.Fun_ContractReceivable_Ref业务逻辑类。
    /// </summary>
    public class ContractReceivableBLL : Common.DataBLL
    {
        private ContractReceivableDAL contractreceivableDAL = new ContractReceivableDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ContractReceivableDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public ContractReceivableBLL()
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
            get { return this.contractreceivableDAL; }
        }
        #endregion

        #region 新增方法

        /// <summary>
        ///  
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="contractNo"></param>
        /// <param name="outCorpId"></param>
        /// <param name="applyTimeBegin"></param>
        /// <param name="applyTimeEnd"></param>
        /// <returns></returns>
        public SelectModel GetCanAllotListSelect(int pageIndex, int pageSize, string orderStr, string contractNo, int outCorpId, DateTime applyTimeBegin, DateTime applyTimeEnd)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cs.SubId desc";
            else
                select.OrderStr = orderStr;

            int tradeDirectionStyleId = (int)NFMT.Data.StyleEnum.贸易方向;
            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;
            int statusType = (int)NFMT.Common.StatusTypeEnum.通用状态;
            int priceModeStyleId = (int)NFMT.Data.StyleEnum.PriceMode;
            int tradeDirection = NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.贸易方向)["Sell"].StyleDetailId;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" cs.SubId,cs.ContractId,cs.ContractDate,c.ContractNo,c.OutContractNo,c.TradeDirection,tradeDirection.DetailName as TradeDirectionName,inCorp.CorpId as InCorpId , inCorp.CorpName as InCorpName,outCorp.CorpId as OutCorpId , outCorp.CorpName as OutCorpName,c.AssetId,ass.AssetName,cs.SignAmount,cs.UnitId,cast(cs.SignAmount as varchar(20))+mu.MUName as ContractWeight,cs.PriceMode,priceMode.DetailName as PriceModeName,cs.SubStatus,subStatus.StatusName");
            sb.Append(",ref.SumBala,CAST(isnull(ref.SumBala,0) as varchar) + cur.CurrencyName as AllotBala,cur.CurrencyId");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Con_ContractSub cs ");
            sb.Append(" left join dbo.Con_SubDetail sd on cs.SubId = sd.SubId ");
            sb.Append(" left join dbo.Con_SubPrice sp on cs.SubId = sp.SubId ");
            sb.Append(" left join dbo.Con_Contract c on cs.ContractId = c.ContractId ");
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail outCorp on c.ContractId = outCorp.ContractId and outCorp.IsInnerCorp= 0 and outCorp.IsDefaultCorp =1 and outCorp.DetailStatus={0} ", readyStatus);
            sb.AppendFormat(" left join dbo.Con_ContractCorporationDetail inCorp on c.ContractId = inCorp.ContractId and inCorp.IsInnerCorp=1 and inCorp.IsDefaultCorp=1 and inCorp.DetailStatus = {0} ", readyStatus);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail tradeDirection on c.TradeDirection = tradeDirection.StyleDetailId and tradeDirection.BDStyleId={0}  ", tradeDirectionStyleId);
            sb.Append(" left join NFMT_Basic.dbo.Asset ass on c.AssetId = ass.AssetId ");
            sb.Append(" left join NFMT_Basic.dbo.MeasureUnit mu on cs.UnitId = mu.MUId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail priceMode on cs.PriceMode = priceMode.StyleDetailId and priceMode.BDStyleId ={0} ", priceModeStyleId);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail subStatus on cs.SubStatus = subStatus.DetailId and SubStatus.StatusId={0} ", statusType);
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cs.SettleCurrency = cur.CurrencyId");

            sb.Append(" left join (select crr.SubContractId,SUM(rad.AllotBala) as SumBala from dbo.Fun_ContractReceivable_Ref crr  ");
            sb.Append(" inner join dbo.Fun_RecAllotDetail rad on crr.DetailId = rad.DetailId and rad.DetailStatus>=20 group by crr.SubContractId) ");
            sb.Append(" as ref on ref.SubContractId = cs.SubId ");

            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" c.TradeDirection = {0} ", tradeDirection);

            if (!string.IsNullOrEmpty(contractNo))
                sb.AppendFormat(" and cs.SubNo like '%{0}%' ", contractNo);
            if (outCorpId > 0)
                sb.AppendFormat(" and outCorp.CorpId = {0} ", outCorpId);
            if (applyTimeBegin > Common.DefaultValue.DefaultTime && applyTimeEnd > applyTimeBegin)
                sb.AppendFormat(" and cs.ContractDate between '{0}' and '{1}' ", applyTimeBegin.ToString(), applyTimeEnd.AddDays(1).ToString());

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel GetSubId(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = contractreceivableDAL.GetSubId(user, allotId);
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

        public ResultModel GetReceIds(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = contractreceivableDAL.GetReceIds(user, allotId);
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

        public ResultModel GetRowsDetail(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = contractreceivableDAL.GetRowsDetail(user, allotId);
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

        public ResultModel GetRowsDetailByCorp(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = contractreceivableDAL.GetRowsDetailByCorp(user, allotId);
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

        public ResultModel GetCorpRefIds(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = contractreceivableDAL.GetCorpRefIds(user, allotId);
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
