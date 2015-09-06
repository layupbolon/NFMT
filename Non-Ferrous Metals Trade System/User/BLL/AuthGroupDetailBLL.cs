/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AuthGroupDetailBLL.cs
// 文件功能描述：员工权限组关联表dbo.AuthGroupDetail业务逻辑类。
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
    /// 员工权限组关联表dbo.AuthGroupDetail业务逻辑类。
    /// </summary>
    public class AuthGroupDetailBLL : Common.DataBLL
    {
        private AuthGroupDetailDAL authgroupdetailDAL = new AuthGroupDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(AuthGroupDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AuthGroupDetailBLL()
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
            get { return this.authgroupdetailDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int empId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "detail.DetailId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("detail.DetailId,e.Name,ag.AuthGroupName,");
            sb.Append("case when ISNULL(ag.AssetId,0)=0 then '全部' else a.AssetName end as AssetName,");
            sb.Append("case when ISNULL(ag.TradeDirection,0)=0 then '全部' else bd1.DetailName end as TradeDirection,");
            sb.Append("case when ISNULL(ag.TradeBorder,0)=0 then '全部' else bd2.DetailName end as TradeBorder,");
            sb.Append("case when ISNULL(ag.ContractInOut,0)=0 then '全部' else bd3.DetailName end as ContractInOut,");
            sb.Append("case when ISNULL(ag.ContractLimit,0)=0 then '全部' else bd4.DetailName end as ContractLimit,");
            sb.Append("case when ISNULL(ag.CorpId,0)=0 then '全部' else c.CorpName end as CorpName,bdd.StatusName");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.AuthGroupDetail detail ");
            sb.Append(" left join NFMT_User.dbo.Employee e on detail.EmpId = e.EmpId ");
            sb.Append(" left join dbo.AuthGroup ag on detail.AuthGroupId = ag.AuthGroupId ");
            sb.Append(" left join NFMT_Basic.dbo.Asset a on ag.AssetId = a.AssetId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail bd1 on ag.TradeDirection = bd1.StyleDetailId and bd1.BDStyleId = {0} ", (int)NFMT.Data.StyleEnum.TradeDirection);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail bd2 on ag.TradeBorder = bd2.StyleDetailId and bd2.BDStyleId = {0} ", (int)NFMT.Data.StyleEnum.TradeBorder);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail bd3 on ag.ContractInOut = bd3.StyleDetailId and bd3.BDStyleId = {0} ", (int)NFMT.Data.StyleEnum.ContractSide);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail bd4 on ag.ContractLimit = bd4.StyleDetailId and bd4.BDStyleId = {0} ", (int)NFMT.Data.StyleEnum.ContractLimit);
            sb.Append(" left join dbo.Corporation c on ag.CorpId = c.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bdd on detail.DetailStatus = bdd.DetailId and bdd.StatusId = {0} ", (int)NFMT.Common.StatusTypeEnum.通用状态);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.AppendFormat(" detail.EmpId = {0} and detail.DetailStatus = {1} ", empId, (int)Common.StatusEnum.已生效);

            select.WhereStr = sb.ToString();

            return select;
        }

        public ResultModel UpdateStauts(UserModel user, int detailid, Common.StatusEnum status)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = authgroupdetailDAL.UpdateStauts(user, detailid, status);
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
