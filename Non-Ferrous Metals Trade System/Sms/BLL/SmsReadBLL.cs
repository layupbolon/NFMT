/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsReadBLL.cs
// 文件功能描述：消息已读dbo.Sm_SmsRead业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年9月9日
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
    /// 消息已读dbo.Sm_SmsRead业务逻辑类。
    /// </summary>
    public class SmsReadBLL : Common.ExecBLL
    {
        private SmsReadDAL smsreadDAL = new SmsReadDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(SmsReadDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SmsReadBLL()
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
            get { return this.smsreadDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel ReadSms(UserModel user, int smsId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = smsreadDAL.ReadSms(user, smsId);
            }
            catch (Exception ex)
            {
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
