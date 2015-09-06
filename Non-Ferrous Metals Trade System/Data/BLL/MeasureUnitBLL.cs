/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：MeasureUnitBLL.cs
// 文件功能描述：dbo.MeasureUnit业务逻辑类。
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
    /// dbo.MeasureUnit业务逻辑类。
    /// </summary>
    public class MeasureUnitBLL : Common.DataBLL
    {
        private MeasureUnitDAL measureunitDAL = new MeasureUnitDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(MeasureUnitDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MeasureUnitBLL()
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
            get { return this.measureunitDAL; }
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
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int status, string key)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "mu.MUId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = "  mu.MUId,mu.MUName,mu.BaseId,bmu.MUName as BaseName,mu.TransformRate,mu.MUStatus,BD.StatusName as MUStatusName ";
            select.TableName = " dbo.MeasureUnit mu left join dbo.MeasureUnit bmu on mu.BaseId = bmu.MUId left join dbo.BDStatusDetail BD on BD.DetailId = mu.MUStatus ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("  BD.StatusId = 1  ");

            if (status > 0)
                sb.AppendFormat(" and mu.MUStatus = {0}", status);
            if (!string.IsNullOrEmpty(key))
                sb.AppendFormat(" and mu.MUName like '%{0}%'", key);

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
                Model.MeasureUnit obj1 = (Model.MeasureUnit)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Operate.Get(user, obj.Id);
                    Model.MeasureUnit resultobj = (Model.MeasureUnit)result.ReturnValue;

                    if (resultobj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能修改";
                        return result;
                    }
                    resultobj.MUName = obj1.MUName;
                    resultobj.BaseId = obj1.BaseId;
                    resultobj.TransformRate = obj1.TransformRate;
                    resultobj.MUStatus = obj1.MUStatus;

                    result = measureunitDAL.Update(user, resultobj);

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
