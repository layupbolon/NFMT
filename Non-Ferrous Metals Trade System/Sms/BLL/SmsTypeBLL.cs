/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsTypeBLL.cs
// 文件功能描述：消息类型dbo.Sm_SmsType业务逻辑类。
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
    /// 消息类型dbo.Sm_SmsType业务逻辑类。
    /// </summary>
    public class SmsTypeBLL : Common.DataBLL
    {
        private SmsTypeDAL smstypeDAL = new SmsTypeDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(SmsTypeDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SmsTypeBLL()
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
            get { return this.smstypeDAL; }
        }

        #endregion

        #region 重载方法

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {

                    Model.SmsType obj1 = (Model.SmsType)obj;
                    result = this.Get(user, obj.Id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.SmsType resultObj = (Model.SmsType)result.ReturnValue;

                    if (resultObj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }

                    resultObj.TypeName = obj1.TypeName;
                    resultObj.ListUrl = obj1.ListUrl;
                    resultObj.ViewUrl = obj1.ViewUrl;
                    resultObj.SmsTypeStatus = obj1.SmsTypeStatus;

                    result = this.Operate.Update(user, resultObj);

                    if (result.ResultStatus != 0)
                        return result;

                    scope.Complete();
                }

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

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, string typeName, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "SmsTypeId asc";
            else
                select.OrderStr = orderStr;

            int statusId = (int)Common.StatusTypeEnum.通用状态;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" SmsTypeId,TypeName,ListUrl,ViewUrl,SmsTypeStatus,bd.StatusName ");
            select.ColumnName = sb.ToString();

            sb.Clear();
            sb.Append(" dbo.Sm_SmsType st  ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail bd on st.SmsTypeStatus = bd.DetailId and bd.StatusId = {0} ", statusId);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(typeName))
                sb.AppendFormat(" and TypeName like '%{0}%' ", typeName);
            if (status > 0)
                sb.AppendFormat(" and SmsTypeStatus = {0} ", status);
            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion
    }
}
