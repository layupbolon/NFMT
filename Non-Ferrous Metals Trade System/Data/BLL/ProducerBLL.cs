/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ProducerBLL.cs
// 文件功能描述：生产商dbo.Producer业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月16日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Data.Model;
using NFMT.Data.DAL;
using NFMT.Data.IDAL;
using NFMT.Common;

namespace NFMT.Data.BLL
{
    /// <summary>
    /// 生产商dbo.Producer业务逻辑类。
    /// </summary>
    public class ProducerBLL : Common.DataBLL
    {
        private ProducerDAL producerDAL = new ProducerDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ProducerDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ProducerBLL()
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
            get { return this.producerDAL; }
        }

        #endregion

        #region 新增方法

        /// <summary>
        /// 获取分页查询对象
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="status"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int status, string key, int producerArea)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "P.ProducerId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " P.ProducerId,P.ProducerFullName,P.ProducerName,P.ProducerShort,A.AreaName,BD.StatusName,P.ProducerArea ";
            select.TableName = " dbo.Producer P left join dbo.Area A on P.ProducerArea = A.AreaId left join dbo.BDStatusDetail BD on P.ProducerStatus = BD.DetailId ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" BD.StatusId = 1 ");

            if (status > 0)
                sb.AppendFormat(" and P.ProducerStatus = {0}", status);
            if (!string.IsNullOrEmpty(key))
                sb.AppendFormat(" and P.ProducerName like '%{0}%'", key);
            if (producerArea > 0)
                sb.AppendFormat(" and P.ProducerArea = {0}", producerArea);


            select.WhereStr = sb.ToString();

            return select;
        }

        #endregion

        #region 重载方法

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                Model.Producer obj1 = (Model.Producer)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, obj.Id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Producer resultObj = (Model.Producer)result.ReturnValue;

                    if (resultObj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }

                    resultObj.ProducerName = obj1.ProducerName;
                    resultObj.ProducerFullName = obj1.ProducerFullName;
                    resultObj.ProducerShort = obj1.ProducerShort;
                    resultObj.ProducerId = obj1.ProducerId;
                    resultObj.ProducerArea = obj1.ProducerArea;
                    resultObj.ProducerStatus = obj1.ProducerStatus;

                    result = this.Operate.Update(user, resultObj);

                    if (result.ResultStatus != 0)
                        return result;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
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
