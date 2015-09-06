/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsParameterBLL.cs
// 文件功能描述：消息类型构造参数dbo.Sm_SmsParameter业务逻辑类。
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
    /// 消息类型构造参数dbo.Sm_SmsParameter业务逻辑类。
    /// </summary>
    public class SmsParameterBLL : Common.DataBLL
    {
        private SmsParameterDAL smsparameterDAL = new SmsParameterDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(SmsParameterDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SmsParameterBLL()
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
            get { return this.smsparameterDAL; }
        }
        #endregion
    }
}
