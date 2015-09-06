/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PayApplyAttachBLL.cs
// 文件功能描述：付款申请附件dbo.Fun_PayApplyAttach业务逻辑类。
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
    /// 付款申请附件dbo.Fun_PayApplyAttach业务逻辑类。
    /// </summary>
    public class PayApplyAttachBLL : Common.DataBLL
    {
        private PayApplyAttachDAL payapplyattachDAL = new PayApplyAttachDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PayApplyAttachDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PayApplyAttachBLL()
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
            get { return this.payapplyattachDAL; }
        }
        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int payApplyId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "pa.PayApplyAttachId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " pa.PayApplyAttachId,pa.PayApplyId,pa.AttachId,pa.AttachType,bd.DetailName,a.AttachName,a.ServerAttachName,a.AttachExt,a.AttachInfo,a.AttachPath,a.CreateTime ";
            select.TableName = " dbo.Fun_PayApplyAttach pa inner join NFMT..Attach a on pa.AttachId = a.AttachId left join NFMT_Basic.dbo.BDStyleDetail bd on bd.StyleDetailId = pa.AttachType ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat(" a.AttachStatus <> {0} ", (int)Common.StatusEnum.已作废);
            if (payApplyId > 0)
                sb.AppendFormat(" and pa.PayApplyId = {0}", payApplyId);

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion
    }
}
