/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AreaBLL.cs
// 文件功能描述：地区表dbo.Area业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年5月4日
----------------------------------------------------------------*/

using System;
using System.Transactions;
using NFMT.Data.Model;
using NFMT.Data.DAL;
using NFMT.Common;

namespace NFMT.Data.BLL
{
    /// <summary>
    /// 地区表dbo.Area业务逻辑类。
    /// </summary>
    public class AreaBLL : Common.DataBLL
    {
        private AreaDAL areaDAL = new AreaDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(AreaDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AreaBLL()
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
            get { return this.areaDAL; }
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
            SelectModel select = new SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            @select.OrderStr = string.IsNullOrEmpty(orderStr) ? "A.AreaId desc" : orderStr;

            select.ColumnName = "  a.AreaId as AreaId,a.AreaName as AreaName,a.AreaFullName as AreaFullName,a.AreaShort as AreaShort,a.AreaCode as AreaCode,a.AreaStatus as AreaStatus,a.AreaZip as AreaZip,bd.StatusName as StatusName,at.AreaName as atAreaName ";
            select.TableName = " dbo.Area a left join dbo.BDStatusDetail bd on bd.DetailId=a.AreaStatus left join Area at on at.AreaId=a.ParentID ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" bd.StatusId = 1 ");

            if (status > 0)
                sb.AppendFormat(" and a.AreaStatus = {0}", status);
            if (!string.IsNullOrEmpty(key))
                sb.AppendFormat(" and a.AreaName like '%{0}%'", key);

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
                Model.Area obj1 = (Model.Area)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, obj.Id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Area resultObj = (Model.Area)result.ReturnValue;

                    if (resultObj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }
                    resultObj.AreaName = obj1.AreaName;
                    resultObj.AreaId = obj1.AreaId;
                    resultObj.AreaFullName = obj1.AreaFullName;
                    resultObj.AreaShort = obj1.AreaShort;
                    resultObj.AreaCode = obj1.AreaCode;
                    resultObj.AreaZip = obj1.AreaZip;
                    resultObj.ParentID = obj1.ParentID;
                    resultObj.AreaStatus = obj1.Status;

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
