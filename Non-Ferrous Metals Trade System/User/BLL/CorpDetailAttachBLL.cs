/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorpDetailAttachBLL.cs
// 文件功能描述：客户附件表dbo.CorpDetailAttach业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
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
    /// 客户附件表dbo.CorpDetailAttach业务逻辑类。
    /// </summary>
    public class CorpDetailAttachBLL : Common.ExecBLL
    {
        private CorpDetailAttachDAL corpdetailattachDAL = new CorpDetailAttachDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CorpDetailAttachDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CorpDetailAttachBLL()
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
            get { return this.corpdetailattachDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int detailId)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cda.AttachType desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " cda.CorpDetailAttachId,cda.DetailId,cda.AttachId,cda.AttachType,bd.DetailName, a.AttachName,a.ServerAttachName,a.AttachExt,a.AttachInfo,a.AttachPath,a.CreateTime ";
            select.TableName = " dbo.CorpDetailAttach cda inner join NFMT..Attach a on cda.AttachId = a.AttachId left join NFMT_Basic..BDStyleDetail bd on bd.StyleDetailId = cda.AttachType";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat(" cda.CorpDetailAttachStatus = {0} and a.AttachStatus <> {1} ", (int)Common.StatusEnum.已生效, (int)Common.StatusEnum.已作废);
            if (detailId > 0)
                sb.AppendFormat(" and cda.DetailId = {0}", detailId);

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion
    }
}
