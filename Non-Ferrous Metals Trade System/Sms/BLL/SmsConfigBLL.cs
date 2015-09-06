/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsConfigBLL.cs
// 文件功能描述：消息配置dbo.Sm_SmsConfig业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年6月27日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Sms.Model;
using NFMT.Sms.DAL;
using NFMT.Sms.IDAL;
using NFMT.Common;

namespace NFMT.Sms.BLL
{
    /// <summary>
    /// 消息配置dbo.Sm_SmsConfig业务逻辑类。
    /// </summary>
    public class SmsConfigBLL : Common.DataBLL
    {
        private SmsConfigDAL smsconfigDAL = new SmsConfigDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(SmsConfigDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SmsConfigBLL()
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
            get { return this.smsconfigDAL; }
        }
        #endregion
    }
}
