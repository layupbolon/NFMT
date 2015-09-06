/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AttachBLL.cs
// 文件功能描述：附件dbo.Attach业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年6月30日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using System.Linq;
using NFMT.Operate.Model;
using NFMT.Operate.DAL;
using NFMT.Operate.IDAL;
using NFMT.Common;

namespace NFMT.Operate.BLL
{
    /// <summary>
    /// 附件dbo.Attach业务逻辑类。
    /// </summary>
    public class AttachBLL : Common.DataBLL
    {
        private AttachDAL attachDAL = new AttachDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(AttachDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AttachBLL()
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
            get { return this.attachDAL; }
        }
        #endregion

        #region 新增方法

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="attachs">需添加的附件</param>
        /// <param name="id">业务数据id</param>
        /// <param name="operate">业务数据DAL层</param>
        /// <returns></returns>
        public ResultModel AddAttach(UserModel user, List<Model.Attach> attachs, int bussinessDataId, IAttachModel obj, Common.IOperate operate)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (attachs != null && attachs.Any() && bussinessDataId > 0)
                    {
                        foreach (Model.Attach attach in attachs)
                        {
                            //写入附件表
                            attach.AttachStatus = StatusEnum.已生效;
                            result = attachDAL.Insert(user, attach);
                            if (result.ResultStatus != 0)
                                return result;

                            obj.BussinessDataId = bussinessDataId;
                            obj.AttachId = (int)result.ReturnValue;

                            //写入业务附件表
                            result = operate.Insert(user, obj);
                            if (result.ResultStatus != 0)
                                return result;
                        }

                        scope.Complete();
                    }
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

        /// <summary>
        /// 更新附件状态
        /// </summary>
        /// <param name="user"></param>
        /// <param name="attachId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ResultModel UpdateAttachStatus(UserModel user, int attachId, int status)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = attachDAL.UpdateAttachStatus(user, attachId, status);
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

        /// <summary>
        /// 获取附件
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="bussinessId"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public SelectModel GetAttachSelectModel(int pageIndex, int pageSize, string orderStr, int bussinessId, int status, int type)
        {
            SelectModel select = new SelectModel();
            string tableName = string.Empty;
            string bussinessIdName = string.Empty;
            switch (type)
            {
                case (int)NFMT.Operate.AttachType.CashInAttach:
                    tableName = "dbo.Fun_ReceivableAttach";
                    bussinessIdName = "ReceivableId";
                    break;
                case (int)NFMT.Operate.AttachType.ContractAttach:
                    tableName = "dbo.Con_ContractAttach";
                    bussinessIdName = "ContractId";
                    break;
                case (int)NFMT.Operate.AttachType.InvoiceAttach:
                    tableName = "dbo.InvoiceAttach";
                    bussinessIdName = "InvoiceId";
                    break;
                case (int)NFMT.Operate.AttachType.PaymentAttach:
                    tableName = "dbo.Fun_PaymentAttach";
                    bussinessIdName = "AttachId";
                    break;
                case (int)NFMT.Operate.AttachType.StockAttach:
                    tableName = "dbo.St_StocktAttach";
                    bussinessIdName = "StockId";
                    break;
                case (int)NFMT.Operate.AttachType.StockInAttach:
                    tableName = "dbo.St_StockInAttach";
                    bussinessIdName = "StockInId";
                    break;
                case (int)NFMT.Operate.AttachType.StockLogAttach:
                    tableName = "dbo.St_StockLogAttach";
                    bussinessIdName = "StockLogId";
                    break;
                case (int)NFMT.Operate.AttachType.StockOutAttach:
                    tableName = "dbo.St_StockOutAttach";
                    bussinessIdName = "StockOutId";
                    break;
                case (int)NFMT.Operate.AttachType.SplitDocAttach:
                    tableName = "dbo.St_SplitDocAttach";
                    bussinessIdName = "SplitDocId";
                    break;
                case (int)NFMT.Operate.AttachType.SubAttach:
                    tableName = "dbo.Con_ContractSubAttach";
                    bussinessIdName = "SubId";
                    break;
                case (int)NFMT.Operate.AttachType.CustomApplyAttach:
                    tableName = "dbo.St_CustomsApplyAttach";
                    bussinessIdName = "CustomsApplyId";
                    break;
                case (int)NFMT.Operate.AttachType.CustomAttach:
                    tableName = "dbo.St_CustomsAttach";
                    bussinessIdName = "CustomsId";
                    break;
                case (int)NFMT.Operate.AttachType.OrderAttach:
                    tableName = "dbo.Doc_DocumentOrderAttach";
                    bussinessIdName = "OrderId";
                    break;
                case (int)NFMT.Operate.AttachType.PledgeAttach:
                    tableName = "dbo.St_PledgeAttach";
                    bussinessIdName = "PledgeId";
                    break;
                default:
                    return select;
            }

            select.PageIndex = pageIndex;
            select.PageSize = pageSize;
            select.OrderStr = string.IsNullOrEmpty(orderStr) ? "ca.AttachId asc" : orderStr;
            select.ColumnName = "ca.AttachId,a.AttachName,a.CreateTime,a.AttachPath,a.AttachInfo,a.AttachType,a.AttachExt,a.AttachStatus,a.ServerAttachName,0 as [check] ";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendFormat(" {0} ca with (nolock) ", tableName);
            sb.Append(" left join dbo.Attach a with (nolock) on ca.AttachId = a.AttachId ");
            select.TableName = sb.ToString();

            sb.Clear();
            sb.Append(" 1=1 ");

            if (bussinessId > 0)
                sb.AppendFormat(" and ca.{0} = {1} ", bussinessIdName, bussinessId);
            if (status > 0)
                sb.AppendFormat(" and a.AttachStatus = {0} ", status);
            select.WhereStr = sb.ToString();

            return select;
        }

        /// <summary>
        /// 根据业务数据获取附件Id
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public Common.ResultModel GetAttachIds(Common.UserModel user, Common.IModel model)
        {
            Common.ResultModel result = new Common.ResultModel();

            AttachType attachType = this.GetAttachTypeByModel(model);

            if ((int)attachType == 0)
            {
                result.ResultStatus = -1;
                result.Message = "获取附件失败，或无附件";
                return result;
            }

            NFMT.Common.SelectModel select = this.GetAttachSelectModel(1, 200, "ca.AttachId asc", model.Id, (int)Common.StatusEnum.已生效, (int)attachType);
            select.ColumnName = "ca.AttachId";

            return attachDAL.Load(user, select);
        }

        public AttachType GetAttachTypeByModel(Common.IModel model)
        {
            NFMT.Operate.AttachType attachType = 0;

            switch (model.TableName)
            {
                case "dbo.Con_Contract":
                    attachType = AttachType.ContractAttach;
                    break;
                case "dbo.St_StockIn":
                    attachType = AttachType.StockInAttach;
                    break;
                case "dbo.St_StockLog":
                    attachType = AttachType.StockLogAttach;
                    break;
                case "dbo.St_Stock":
                    attachType = AttachType.StockAttach;
                    break;
                case "dbo.St_StockOut":
                    attachType = AttachType.StockOutAttach;
                    break;
                case "dbo.Fun_CashIn":
                    attachType = AttachType.CashInAttach;
                    break;
                case "dbo.Fun_Payment":
                    attachType = AttachType.PaymentAttach;
                    break;
                case "dbo.Invoice":
                    attachType = AttachType.InvoiceAttach;
                    break;
                case "dbo.St_SplitDoc":
                    attachType = AttachType.SplitDocAttach;
                    break;
                case "dbo.Apply":
                    attachType = AttachType.CustomApplyAttach;
                    break;
                case "dbo.St_CustomsClearance":
                    attachType = AttachType.CustomAttach;
                    break;
                case "dbo.Con_ContractSub":
                    attachType = AttachType.SubAttach;
                    break;
                case "dbo.Doc_DocumentOrder":
                    attachType = AttachType.OrderAttach;
                    break;
                case "dbo.St_Pledge":
                    attachType = AttachType.PledgeAttach;
                    break;
                default:
                    attachType = 0;
                    break;
            }

            return attachType;
        }

        public ResultModel GetAttachByAttachIds(UserModel user, string aids)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = attachDAL.GetAttachByAttachIds(user, aids);
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
