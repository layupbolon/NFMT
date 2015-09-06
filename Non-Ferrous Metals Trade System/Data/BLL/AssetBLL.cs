/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AssetBLL.cs
// 文件功能描述：品种表dbo.Asset业务逻辑类。
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
    /// 品种表dbo.Asset业务逻辑类。
    /// </summary>
    public class AssetBLL : Common.DataBLL
    {
        private AssetDAL assetDAL = new AssetDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(AssetDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AssetBLL()
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
            get { return this.assetDAL; }
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
        /// <param name="assetName"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int status, string assetName)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "a.AssetId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " a.AssetName,a.MUId,a.AssetStatus,a.AssetId,bd.StatusName,mu.MUName as MUName,a.AmountPerHand ";
            select.TableName = " dbo.Asset a left join BDStatusDetail bd on a.AssetStatus=bd.DetailId left join dbo.MeasureUnit mu on a.MUId=mu.MUId and bd.StatusId = 1";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" bd.StatusId = 1 ");

            if (status > 0)
                sb.AppendFormat(" and  a.AssetStatus = {0}", status);
            if (!string.IsNullOrEmpty(assetName))
                sb.AppendFormat(" and a.AssetName like '%{0}%'", assetName);

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
                Model.Asset obj1 = (Model.Asset)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Operate.Get(user, obj.Id);
                    Model.Asset resultobj = (Model.Asset)result.ReturnValue;

                    if (resultobj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能修改";
                        return result;
                    }
                    resultobj.AssetName = obj1.AssetName;
                    resultobj.MUId = obj1.MUId;
                    //resultobj.MisTake = obj1.MisTake;
                    resultobj.AssetStatus = obj1.AssetStatus;
                    resultobj.AmountPerHand = obj1.AmountPerHand;

                    result = assetDAL.Update(user, resultobj);

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
