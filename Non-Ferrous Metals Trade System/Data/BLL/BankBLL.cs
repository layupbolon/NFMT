/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BankBLL.cs
// 文件功能描述：dbo.Bank业务逻辑类。
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
    /// dbo.Bank业务逻辑类。
    /// </summary>
    public class BankBLL : Common.DataBLL
    {
        private BankDAL bankDAL = new BankDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(BankDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BankBLL()
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
            get { return this.bankDAL; }
        }

        #endregion

        #region 新增方法
        
        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int status, string key, int bankeStatus, int capitalType, string bankEname)
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "BankId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " B.BankId,B.BankName,B.BankEname,B.BankFullName,B.BankShort,B.CapitalType,B.BankStatus,bd.StatusName,sd.DetailName,bt.BankName as ParentBankName,B.ParentId,case ISNULL(B.SwitchBack,0) when 0 then '否' when 1 then '是' end as SwitchBack ";
            select.TableName = "  dbo.Bank B left join dbo.BDStatusDetail bd on B.BankStatus = bd.DetailId left join dbo.BDStyleDetail sd on sd.StyleDetailId=B.CapitalType left join Bank bt on bt.BankId=B.ParentId ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" bd.StatusId = 1 ");

            if (status > 0)
                sb.AppendFormat(" and  B.BankStatus = {0}", status);
            if (!string.IsNullOrEmpty(key))
                sb.AppendFormat(" and B.BankFullName like '%{0}%'", key);
            if (capitalType > 0)
                sb.AppendFormat(" and  B.CapitalType = {0}", capitalType);
            if (!string.IsNullOrEmpty(bankEname))
                sb.AppendFormat(" and B.BankEname like '%{0}%'", bankEname);
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
                Model.Bank obj1 = (Model.Bank)obj;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Operate.Get(user, obj.Id);
                    Model.Bank resultobj = (Model.Bank)result.ReturnValue;

                    if (resultobj == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该数据不存在，不能修改";
                        return result;
                    }
                    resultobj.BankName = obj1.BankName;
                    resultobj.BankEname = obj1.BankEname;
                    resultobj.BankFullName = obj1.BankFullName;
                    resultobj.BankShort = obj1.BankShort;
                    resultobj.CapitalType = obj1.CapitalType;
                    resultobj.BankLevel = obj1.BankLevel;
                    resultobj.ParentId = obj1.ParentId;
                    resultobj.BankStatus = obj1.BankStatus;
                    resultobj.SwitchBack = obj1.SwitchBack;

                    result = bankDAL.Update(user, resultobj);

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
