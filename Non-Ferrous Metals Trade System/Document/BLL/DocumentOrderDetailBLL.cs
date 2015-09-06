/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentOrderDetailBLL.cs
// 文件功能描述：制单指令明细dbo.Doc_DocumentOrderDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Document.Model;
using NFMT.Document.DAL;
using NFMT.Document.IDAL;
using NFMT.Common;

namespace NFMT.Document.BLL
{
    /// <summary>
    /// 制单指令明细dbo.Doc_DocumentOrderDetail业务逻辑类。
    /// </summary>
    public class DocumentOrderDetailBLL : Common.ExecBLL
    {
        private DocumentOrderDetailDAL documentorderdetailDAL = new DocumentOrderDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(DocumentOrderDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public DocumentOrderDetailBLL()
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
            get { return this.documentorderdetailDAL; }
        }
		
        #endregion

        #region 新增方法

        public ResultModel GetByOrderId(UserModel user, int orderId)
        {
            return this.documentorderdetailDAL.GetByOrderId(user, orderId);
        }

        #endregion
    }
}
