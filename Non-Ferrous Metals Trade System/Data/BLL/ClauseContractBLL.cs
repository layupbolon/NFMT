/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ClauseContractBLL.cs
// 文件功能描述：模板条款关联表dbo.ClauseContract_Ref业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
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
    /// 模板条款关联表dbo.ClauseContract_Ref业务逻辑类。
    /// </summary>
    public class ClauseContractBLL : Common.DataBLL
    {
        private ClauseContractDAL clausecontractDAL = new ClauseContractDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ClauseContractDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ClauseContractBLL()
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
            get { return this.clausecontractDAL; }
        }

        #endregion

        #region 新增方法

        public SelectModel GetSelectModel(int pageIndex, int pageSize, string orderStr, int masterId, bool isHas = false, string clauseText = "")
        {
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();
            int status = (int)NFMT.Common.StatusEnum.已生效;


            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            if (string.IsNullOrEmpty(orderStr))
                select.OrderStr = "cc.ClauseId desc";
            else
                select.OrderStr = orderStr;

            select.ColumnName = " cc.ClauseText,cc.ClauseEnText,ccr.RefId,cc.ClauseId,ccr.Sort,ccr.IsChose ";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (isHas)
            {
                select.TableName = " dbo.ClauseContract_Ref ccr left join dbo.ContractClause cc on cc.ClauseId = ccr.ClauseId left join dbo.ContractMaster cm on cm.MasterId = ccr.MasterId ";
                sb.AppendFormat(" ccr.RefStatus ={0} and cc.ClauseStatus = {0} ", status);
                sb.AppendFormat(" and ccr.MasterId = {0} ", masterId);
            }
            else
            {
                select.TableName = string.Format(" dbo.ContractClause cc left join dbo.ClauseContract_Ref ccr on ccr.ClauseId = cc.ClauseId and MasterId = {0} and RefStatus = {1}  left join dbo.ContractMaster cm on cm.MasterId = ccr.MasterId ", masterId, status);
                sb.AppendFormat(" ccr.ClauseId is null and cc.ClauseStatus = {0}", status);
                if (!string.IsNullOrEmpty(clauseText))
                    sb.AppendFormat(" and cc.ClauseText like '%{0}%' ", clauseText);
            }

            select.WhereStr = sb.ToString();

            return select;
        }

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            Model.ClauseContract inputObj = (Model.ClauseContract)obj;

            try
            {
                result = clausecontractDAL.Get(user, obj.Id);
                if (result.ResultStatus != 0)
                    return result;

                Model.ClauseContract resultObj = (Model.ClauseContract)result.ReturnValue;

                if (resultObj == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "该数据不存在，不能更新";
                    return result;
                }

                resultObj.IsChose = inputObj.IsChose;
                resultObj.Sort = inputObj.Sort;

                result = clausecontractDAL.Update(user, resultObj);

                if (result.ResultStatus != 0)
                    return result;
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

        public ResultModel Load(UserModel user, Common.StatusEnum status)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = clausecontractDAL.Load(user, status);
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
