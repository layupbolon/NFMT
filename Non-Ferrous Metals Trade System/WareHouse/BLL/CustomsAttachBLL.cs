/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CustomsAttachBLL.cs
// 文件功能描述：报关附件dbo.St_CustomsAttach业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年10月29日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.WareHouse.Model;
using NFMT.WareHouse.DAL;
using NFMT.WareHouse.IDAL;
using NFMT.Common;

namespace NFMT.WareHouse.BLL
{
    /// <summary>
    /// 报关附件dbo.St_CustomsAttach业务逻辑类。
    /// </summary>
    public class CustomsAttachBLL : Common.ExecBLL
    {
        private CustomsAttachDAL customsattachDAL = new CustomsAttachDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CustomsAttachDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public CustomsAttachBLL()
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
            get { return this.customsattachDAL; }
        }
		
        #endregion
    }
}
