/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DeliverPlaceBLL.cs
// 文件功能描述：交货地dbo.DeliverPlace业务逻辑类。
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
    /// 交货地dbo.DeliverPlace业务逻辑类。
    /// </summary>
    public class DeliverPlaceBLL : Common.DataBLL
    {
        private DeliverPlaceDAL deliverplaceDAL = new DeliverPlaceDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(DeliverPlaceDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeliverPlaceBLL()
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
            get { return this.deliverplaceDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int areaId, int corpId, string dPName, int status)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "dp.DPId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " dp.DPId,style.DetailName,a.AreaName,c.CorpName,dp.DPName,dp.DPFullName,dp.DPAddress,dp.DPEAddress,dp.DPTel,dp.DPContact,dp.DPFax,dp.DPStatus,bd.StatusName ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" dbo.DeliverPlace dp ");
            sb.Append(" left join dbo.Area a on dp.DPArea = a.AreaId ");
            sb.Append(" left join NFMT_User.dbo.Corporation c on dp.DPCompany = c.CorpId ");
            sb.AppendFormat(" left join dbo.BDStatusDetail bd on dp.DPStatus = bd.DetailId and bd.StatusId = {0} ", (int)Common.StatusTypeEnum.通用状态);
            sb.AppendFormat(" left join dbo.BDStyleDetail style on dp.DPType = style.StyleDetailId and BDStyleId = {0} ", (int)NFMT.Data.StyleEnum.交货地类型);
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (areaId > 0)
                sb.AppendFormat(" and dp.DPArea = {0}", areaId);
            if (corpId > 0)
                sb.AppendFormat(" and dp.DPCompany = {0}", corpId);
            if (!string.IsNullOrEmpty(dPName))
                sb.AppendFormat(" and dp.DPName like '%{0}%'", dPName);
            if (status > 0)
                sb.AppendFormat(" and dp.DPStatus = {0}", status);
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
                Model.DeliverPlace obj1 = (Model.DeliverPlace)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, obj.Id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.DeliverPlace resultObj = (Model.DeliverPlace)result.ReturnValue;

                    if (resultObj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }
                    resultObj.DPType = obj1.DPType;
                    resultObj.DPArea = obj1.DPArea;
                    resultObj.DPCompany = obj1.DPCompany;
                    resultObj.DPName = obj1.DPName;
                    resultObj.DPFullName = obj1.DPFullName;
                    resultObj.DPStatus = obj1.DPStatus;
                    resultObj.DPAddress = obj1.DPAddress;
                    resultObj.DPEAddress = obj1.DPEAddress;
                    resultObj.DPTel = obj1.DPTel;
                    resultObj.DPContact = obj1.DPContact;
                    resultObj.DPFax = obj1.DPFax;

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
