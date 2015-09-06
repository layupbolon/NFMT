/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorporationBLL.cs
// 文件功能描述：公司dbo.Corporation业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年5月4日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.User.Model;
using NFMT.User.DAL;
using NFMT.User.IDAL;
using NFMT.Common;

namespace NFMT.User.BLL
{
    /// <summary>
    /// 公司dbo.Corporation业务逻辑类。
    /// </summary>
    public class CorporationBLL : Common.DataBLL
    {
        private CorporationDAL corporationDAL = new CorporationDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CorporationDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CorporationBLL()
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
            get { return this.corporationDAL; }
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
        /// <param name="CorpCode"></param>
        /// <param name="CorpName"></param>
        /// <param name="blocId"></param>
        /// <returns></returns>
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int status, string CorpCode, string CorpName, int blocId, int isSelf = -1)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "C.CorpId desc";
            else
                select.OrderStr = orderStr;

            int bDStyleId = (int)NFMT.Data.StyleEnum.公司类型;
            int bDStatusId = (int)NFMT.Common.StatusTypeEnum.通用状态;
            int corpType = (int)NFMT.Data.StyleEnum.客户类型;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            select.ColumnName = string.Format(" C.CorpId,B.BlocName,C.CorpName,C.CorpEName,C.TaxPayerId,C.CorpFullName,C.CorpFullEName,C.CorpAddress,C.CorpStatus,case when ISNULL(detail.CorpType,0) <> 0 then BD2.DetailName else BD.DetailName end as DetailName,BDD2.StatusName,case C.IsSelf when 1 then '己方公司' when 0 then '非己方公司' else '' end as IsSelf ");

            sb.Append(" dbo.Corporation C left join dbo.Bloc B on C.ParentId = B.BlocId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail BD on BD.StyleDetailId = C.CorpType and BD.BDStyleId ={0} ", bDStyleId);
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStatusDetail BDD2 on BDD2.DetailId = C.CorpStatus and BDD2.StatusId= {0} ", bDStatusId);
            sb.Append(" left join dbo.CorporationDetail detail on C.CorpId = detail.CorpId ");
            sb.AppendFormat(" left join NFMT_Basic.dbo.BDStyleDetail BD2 on BD2.StyleDetailId = detail.CorpType and BD2.BDStyleId ={0} ", corpType);
            select.TableName = sb.ToString();

            sb.Clear();

            sb.Append(" 1=1 ");

            if (blocId > 0)
                sb.AppendFormat(" and C.ParentId = {0} ", blocId);
            if (status > 0)
                sb.AppendFormat(" and C.CorpStatus = {0} ", status);
            if (!string.IsNullOrEmpty(CorpCode))
                sb.AppendFormat(" and C.CorpCode like '%{0}%' ", CorpCode);
            if (!string.IsNullOrEmpty(CorpName))
                sb.AppendFormat(" and C.CorpName like '%{0}%' ", CorpName);

            if (isSelf == 0 || isSelf == 1)
                sb.AppendFormat(" and C.IsSelf = {0}", isSelf);

            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 获取企业列表(用于绑定下拉框)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Type">1表示包含“全部”；2表示不包含“全部”</param>
        /// <returns></returns>
        public ResultModel GetCorpList(UserModel user, int isSelf)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.corporationDAL.GetCorpList(user, isSelf);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public ResultModel LoadAuthSelfCorp(UserModel user)
        {
            return this.corporationDAL.LoadAuthSelfCorp(user);
        }

        #endregion

        #region 重载方法

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                Model.Corporation obj1 = (Model.Corporation)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Get(user, obj.Id);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Corporation resultObj = (Model.Corporation)result.ReturnValue;

                    if (resultObj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能更新";
                        return result;
                    }

                    resultObj.ParentId = obj1.ParentId;
                    resultObj.CorpCode = obj1.CorpCode;
                    resultObj.CorpName = obj1.CorpName;
                    resultObj.CorpEName = obj1.CorpEName;
                    resultObj.TaxPayerId = obj1.TaxPayerId;
                    resultObj.CorpFullName = obj1.CorpFullName;
                    resultObj.CorpFullEName = obj1.CorpFullEName;
                    resultObj.CorpAddress = obj1.CorpAddress;
                    resultObj.CorpEAddress = obj1.CorpEAddress;
                    resultObj.CorpTel = obj1.CorpTel;
                    resultObj.CorpFax = obj1.CorpFax;
                    resultObj.CorpZip = obj1.CorpZip;
                    resultObj.CorpType = obj1.CorpType;

                    if (resultObj.ParentId != 0)
                    {
                        DAL.BlocDAL blocDAL = new BlocDAL();
                        result = blocDAL.Get(user, resultObj.ParentId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.Bloc bloc = result.ReturnValue as Model.Bloc;

                        resultObj.IsSelf = bloc.IsSelf;
                    }

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
