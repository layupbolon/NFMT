/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BrandBLL.cs
// 文件功能描述：dbo.Brand业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年5月4日
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
    /// dbo.Brand业务逻辑类。
    /// </summary>
    public class BrandBLL : Common.DataBLL
    {
        private BrandDAL brandDAL = new BrandDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(BrandDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BrandBLL()
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
            get { return this.brandDAL; }
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
        /// <param name="producerName"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int brandStatus, string key, int producerName)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "BrandId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "  BrandId,b.ProducerId,pd.ProducerName as ProducerName,BrandName,BrandFullName,BrandInfo,BrandStatus,bd.StatusName as BrandStatusName ";
            select.TableName = " Brand b  inner join dbo.BDStatusDetail bd on b.BrandStatus=bd.DetailId inner join dbo.Producer pd on b.ProducerId=pd.ProducerId ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" bd.StatusId = 1 ");

            if (producerName > 0)
                sb.AppendFormat(" and  b.ProducerId= {0}", producerName);
            if (brandStatus > 0)
                sb.AppendFormat(" and BrandStatus = {0}", brandStatus);
            if (!string.IsNullOrEmpty(key))
                sb.AppendFormat(" and BrandName like '%{0}%'", key);

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
                Model.Brand obj1 = (Model.Brand)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, obj.Id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Brand resultObj = (Model.Brand)result.ReturnValue;

                    if (resultObj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }

                    resultObj.BrandName = obj1.BrandName;
                    resultObj.BrandFullName = obj1.BrandFullName;
                    resultObj.BrandInfo = obj1.BrandInfo;
                    resultObj.BrandId = obj1.BrandId;
                    resultObj.ProducerId = obj1.ProducerId;
                    resultObj.BrandStatus = obj1.BrandStatus;

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
