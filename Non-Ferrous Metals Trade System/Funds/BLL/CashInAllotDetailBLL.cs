/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInAllotDetailBLL.cs
// 文件功能描述：收款分配明细dbo.Fun_CashInAllotDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年9月18日
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
    /// 收款分配明细dbo.Fun_CashInAllotDetail业务逻辑类。
    /// </summary>
    public class CashInAllotDetailBLL : Common.ExecBLL
    {
        private CashInAllotDetailDAL cashinallotdetailDAL = new CashInAllotDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CashInAllotDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public CashInAllotDetailBLL()
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
            get { return this.cashinallotdetailDAL; }
        }
		
        #endregion

        #region 新增方法

        public SelectModel GetCurDetailsSelect(int pageIndex, int pageSize, string orderStr,int allotId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();           

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ref.RefId desc";
            else
                select.OrderStr = orderStr;

            int readyStatus = (int)NFMT.Common.StatusEnum.已生效;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append(" ciad.DetailId,ciad.AllotId,ci.CashInId,ci.CashInDate,ci.CashInCorpId,inCorp.CorpName as InCorp ");
            //sb.Append(" ,ci.PayCorpName as OutCorp,ci.CashInBank,ban.BankName as CashInBankName,ci.CurrencyId,cur.CurrencyName ");
            //sb.Append(" ,ci.CashInBala,isnull(ref.SumBala,0) as SumBala,ci.CashInBala - ISNULL(ref.SumBala,0) as LastBala ");
            //sb.Append(" ,isnull(ref.SumBala,0) as AllotBala");
            sb.Append(" ref.RefId as DetailId,ref.AllotId,ci.CashInId,ci.CashInDate,ci.CashInCorpId,inCorp.CorpName as InCorp ");
            sb.Append(" ,ci.PayCorpName as OutCorp,ci.CashInBank,ban.BankName as CashInBankName,ci.CurrencyId,cur.CurrencyName ");
            sb.Append(" ,ci.CashInBala,ref.AllotBala as SumBala,ci.CashInBala - ISNULL(ref.AllotBala,0) as LastBala ");
            sb.Append(" ,isnull(ref.AllotBala,0) as AllotBala ");
            
            select.ColumnName = sb.ToString();

            sb.Clear();
            
            //sb.Append(" dbo.Fun_CashInAllotDetail ciad ");
            //sb.Append(" inner join dbo.Fun_CashIn ci on ciad.CashInId = ci.CashInId ");
            //sb.Append(" left join NFMT_User.dbo.Corporation inCorp on inCorp.CorpId = ci.CashInCorpId ");
            //sb.Append(" left join NFMT_Basic.dbo.Bank ban on ban.BankId = ci.CashInBank ");
            //sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = ci.CurrencyId ");
            //sb.Append(" left join(select CashInId,SUM(AllotBala) as SumBala ");
            //sb.AppendFormat(" from dbo.Fun_CashInAllotDetail where AllotId = {0} and DetailStatus>={1} ", allotId, readyStatus);            
            //sb.Append(" group by CashInId) as ref on ref.CashInId = ciad.CashInId ");

            sb.Append(" dbo.Fun_CashInCorp_Ref ref ");
            sb.Append(" inner join dbo.Fun_CashIn ci on ref.CashInId = ci.CashInId ");
            sb.Append(" left join NFMT_User.dbo.Corporation inCorp on inCorp.CorpId = ci.CashInCorpId ");
            sb.Append(" left join NFMT_Basic.dbo.Bank ban on ban.BankId = ci.CashInBank ");
            sb.Append(" left join NFMT_Basic.dbo.Currency cur on cur.CurrencyId = ci.CurrencyId ");
            
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" ref.DetailStatus>={0} ", readyStatus);
            sb.AppendFormat(" and ref.AllotId = {0} ", allotId);

            select.WhereStr = sb.ToString();

            return select;
        }
        
        #endregion
    }
}
