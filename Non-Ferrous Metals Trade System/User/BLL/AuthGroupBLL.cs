/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AuthGroupBLL.cs
// 文件功能描述：权限组dbo.AuthGroup业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年11月18日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.User.Model;
using NFMT.User.DAL;
using NFMT.User.IDAL;
using NFMT.Common;

namespace NFMT.User.BLL
{
    /// <summary>
    /// 权限组dbo.AuthGroup业务逻辑类。
    /// </summary>
    public class AuthGroupBLL : Common.DataBLL
    {
        private AuthGroupDAL authgroupDAL = new AuthGroupDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(AuthGroupDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AuthGroupBLL()
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
            get { return this.authgroupDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, string name, int assetId, int tradeDirection, int tradeBorder, int contractInOut, int contractLimit, int corpId, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "ag.AuthGroupId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("ag.AuthGroupId,ag.AuthGroupName,");
            sb.Append("case when ISNULL(ag.AssetId,0)=0 then '全部' else a.AssetName end as AssetName,");
            sb.Append("case when ISNULL(ag.TradeDirection,0)=0 then '全部' else bd1.DetailName end as TradeDirection,");
            sb.Append("case when ISNULL(ag.TradeBorder,0)=0 then '全部' else bd2.DetailName end as TradeBorder,");
            sb.Append("case when ISNULL(ag.ContractInOut,0)=0 then '全部' else bd3.DetailName end as ContractInOut,");
            sb.Append("case when ISNULL(ag.ContractLimit,0)=0 then '全部' else bd4.DetailName end as ContractLimit,");
            sb.Append("case when ISNULL(ag.CorpId,0)=0 then '全部' else c.CorpName end as CorpName,bdd.StatusName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.AuthGroup ag ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on ag.AssetId = a.AssetId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail bd1 on ag.TradeDirection = bd1.StyleDetailId and bd1.BDStyleId = {0} ", (int)NFMT.Data.StyleEnum.TradeDirection);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail bd2 on ag.TradeBorder = bd2.StyleDetailId and bd2.BDStyleId = {0} ", (int)NFMT.Data.StyleEnum.TradeBorder);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail bd3 on ag.ContractInOut = bd3.StyleDetailId and bd3.BDStyleId = {0} ", (int)NFMT.Data.StyleEnum.ContractSide);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail bd4 on ag.ContractLimit = bd4.StyleDetailId and bd4.BDStyleId = {0} ", (int)NFMT.Data.StyleEnum.ContractLimit);
            sb.Append(" left join dbo.Corporation c on ag.CorpId = c.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bdd on ag.AuthGroupStatus = bdd.DetailId and bdd.StatusId = {0} ", (int)NFMT.Common.StatusTypeEnum.通用状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (!string.IsNullOrEmpty(name))
                sb.AppendFormat(" and ag.AuthGroupName like '%{0}%' ", name);
            if (assetId > 0)
                sb.AppendFormat(" and (ag.AssetId = {0} or ag.AssetId = 0) ", assetId);
            if (tradeDirection > 0)
                sb.AppendFormat(" and (ag.TradeDirection = {0} or ag.TradeDirection = 0) ", tradeDirection);
            if (tradeBorder > 0)
                sb.AppendFormat(" and (ag.TradeBorder = {0} or ag.TradeBorder = 0) ", tradeBorder);
            if (contractInOut > 0)
                sb.AppendFormat(" and (ag.ContractInOut = {0} or ag.ContractInOut = 0) ", contractInOut);
            if (contractLimit > 0)
                sb.AppendFormat(" and (ag.ContractLimit = {0} or ag.ContractLimit = 0) ", contractLimit);
            if (corpId > 0)
                sb.AppendFormat(" and (ag.CorpId = {0} or ag.CorpId = 0) ", corpId);
            if (status > 0)
                sb.AppendFormat(" and ag.AuthGroupStatus = {0} ", status);

            select.WhereStr = sb.ToString();

            return select;
        }

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    Model.AuthGroup authObj = (Model.AuthGroup)obj;

                    result = authgroupDAL.Get(user, obj.Id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.AuthGroup authGroup = result.ReturnValue as Model.AuthGroup;
                    if (authGroup == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "数据错误";
                        return result;
                    }

                    authGroup.AuthGroupName = authObj.AuthGroupName;
                    authGroup.AssetId = authObj.AssetId;
                    authGroup.TradeDirection = authObj.TradeDirection;
                    authGroup.TradeBorder = authObj.TradeBorder;
                    authGroup.ContractInOut = authObj.ContractInOut;
                    authGroup.ContractLimit = authObj.ContractLimit;
                    authGroup.CorpId = authObj.CorpId;
                    authGroup.AuthGroupStatus = authObj.AuthGroupStatus;

                    result = authgroupDAL.Update(user, authGroup);
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

        public SelectModel GetCanAllotSelectModel(int pageIndex, int pageSize, string orderStr, string name, int assetId, int tradeDirection, int tradeBorder, int contractInOut, int contractLimit, int corpId,int empId)
        {
            SelectModel select = this.GetSelectModel(pageIndex, pageSize, orderStr, name, assetId, tradeDirection, tradeBorder, contractInOut, contractLimit, corpId, 0);

            select.WhereStr += string.Format(" and ag.AuthGroupId not in (select AuthGroupId from dbo.AuthGroupDetail where EmpId = {0} and DetailStatus = {1}) and ag.AuthGroupStatus = {1} ", empId, (int)Common.StatusEnum.已生效);

            return select;
        }

        #endregion
    }
}
