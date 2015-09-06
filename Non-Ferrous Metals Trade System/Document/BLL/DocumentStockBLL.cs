/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentStockBLL.cs
// 文件功能描述：制单库存明细dbo.Doc_DocumentStock业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年11月20日
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
    /// 制单库存明细dbo.Doc_DocumentStock业务逻辑类。
    /// </summary>
    public class DocumentStockBLL : Common.ExecBLL
    {
        private DocumentStockDAL documentstockDAL = new DocumentStockDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(DocumentStockDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public DocumentStockBLL()
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
            get { return this.documentstockDAL; }
        }
		
        #endregion
    }
}
