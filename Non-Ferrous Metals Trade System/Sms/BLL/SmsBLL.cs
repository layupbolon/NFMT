/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsBLL.cs
// 文件功能描述：消息dbo.Sm_Sms业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年9月11日
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
    /// 消息dbo.Sm_Sms业务逻辑类。
    /// </summary>
    public class SmsBLL : Common.ExecBLL
    {
        private SmsDAL smsDAL = new SmsDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(SmsDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SmsBLL()
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
            get { return this.smsDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel GetCurrentSms(UserModel user)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = smsDAL.GetCurrentSms(user);
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }
            return result;
        }

        public ResultModel ReadSms(UserModel user, string smsId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = smsDAL.ReadSms(user, smsId);
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 添加消息(此方法有事务)
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="sms">消息内容</param>
        /// <param name="smsDetails">消息明细</param>
        /// <returns></returns>
        public ResultModel AddSms(UserModel user, Model.Sms sms, List<Model.SmsDetail> smsDetails)
        {
            ResultModel result = new ResultModel();
            DAL.SmsDetailDAL smsDetailDAL = new SmsDetailDAL();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = smsDAL.Insert(user, sms);
                    if (result.ResultStatus != 0)
                        return result;

                    int smsId = (int)result.ReturnValue;

                    foreach (Model.SmsDetail detail in smsDetails)
                    {
                        detail.SmsId = smsId;
                        result = smsDetailDAL.Insert(user, detail);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                    if (result.ResultStatus == 0)
                        result.Message = "消息添加成功";

                    scope.Complete();
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
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

        public Common.SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, UserModel user)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "s.SmsId desc";
            else
                select.OrderStr = orderStr;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" s.SmsId,s.SmsHead,s.SmsBody,s.SmsRelTime,s.SmsStatus,s.SmsLevel,st.TypeName,st.ListUrl,st.ViewUrl,s.SourceId ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Sm_Sms s ");
            sb.Append(" left join dbo.Sm_SmsType st on s.SmsTypeId = st.SmsTypeId ");
            sb.Append(" left join dbo.Sm_SmsDetail sd on s.SmsId = sd.SmsId ");
            
            select.TableName = sb.ToString();

            sb.Clear();

            sb.AppendFormat(" s.SmsStatus = {0} and st.SmsTypeStatus = {1} and sd.EmpId = {2} ", (int)SmsStatusEnum.待处理消息, (int)Common.StatusEnum.已生效, user.EmpId);

            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion
    }
}
