/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorporationDetailBLL.cs
// 文件功能描述：客户明细dbo.CorporationDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年1月21日
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
    /// 客户明细dbo.CorporationDetail业务逻辑类。
    /// </summary>
    public class CorporationDetailBLL : Common.ExecBLL
    {
        private CorporationDetailDAL corporationdetailDAL = new CorporationDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CorporationDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CorporationDetailBLL()
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
            get { return this.corporationdetailDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel Create(UserModel user, Model.Corporation corporation, Model.CorporationDetail corpDetail)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAL.CorporationDAL corporationDAL = new CorporationDAL();
                    result = corporationDAL.Insert(user, corporation);
                    if (result.ResultStatus != 0)
                        return result;

                    int corpId = (int)result.ReturnValue;

                    corpDetail.CorpId = corpId;

                    result = corporationdetailDAL.Insert(user, corpDetail);
                    if (result.ResultStatus != 0)
                        return result;

                    int detailId = (int)result.ReturnValue;

                    //如果客户为销售商则直接生效 2015/7/2 MKZC
                    if (corpDetail.CorpType == (int)CorpTypeEnum.销货商)
                    {
                        result = corporationdetailDAL.UpdateStatus(user, corpId, detailId);
                        if (result.ResultStatus != 0)
                            return result;
                    }

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

        public ResultModel GetByCorpId(UserModel user, int corpId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = corporationdetailDAL.GetByCorpId(user, corpId);
            }
            catch (Exception ex)
            {
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

        public ResultModel Update(UserModel user, Model.Corporation corporation, Model.CorporationDetail corpDetail)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAL.CorporationDAL corporationDAL = new CorporationDAL();
                    result = corporationDAL.Get(user, corporation.CorpId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Corporation corp = result.ReturnValue as Model.Corporation;
                    if (corp == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    corp.CorpName = corporation.CorpName;
                    corp.TaxPayerId = corporation.TaxPayerId;
                    corp.CorpAddress = corporation.CorpAddress;
                    corp.CorpTel = corporation.CorpTel;
                    corp.CorpEAddress = corporation.CorpEAddress;
                    corp.CorpEName = corporation.CorpEName;
                    corp.CorpFax = corporation.CorpFax;

                    result = corporationDAL.Update(user, corp);
                    if (result.ResultStatus != 0)
                        return result;

                    result = corporationdetailDAL.Get(user, corpDetail.DetailId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CorporationDetail corporationDetail = result.ReturnValue as Model.CorporationDetail;
                    if (corporationDetail == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }
                    corporationDetail.BusinessLicenseCode = corpDetail.BusinessLicenseCode;
                    corporationDetail.RegisteredCapital = corpDetail.RegisteredCapital;
                    corporationDetail.CurrencyId = corpDetail.CurrencyId;
                    corporationDetail.RegisteredDate = corpDetail.RegisteredDate;
                    corporationDetail.CorpProperty = corpDetail.CorpProperty;
                    corporationDetail.BusinessScope = corpDetail.BusinessScope;
                    corporationDetail.TaxRegisteredCode = corpDetail.TaxRegisteredCode;
                    corporationDetail.OrganizationCode = corpDetail.OrganizationCode;
                    corporationDetail.IsChildCorp = corpDetail.IsChildCorp;
                    corporationDetail.CorpType = corpDetail.CorpType;
                    corporationDetail.Memo = corpDetail.Memo;

                    result = corporationdetailDAL.Update(user, corporationDetail);
                    if (result.ResultStatus != 0)
                        return result;

                    if (result.ResultStatus == 0)
                        result.ReturnValue = corpDetail.DetailId;

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

        public ResultModel Invalid(UserModel user, int corpId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = corporationdetailDAL.GetByCorpId(user, corpId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CorporationDetail corporationDetail = result.ReturnValue as Model.CorporationDetail;
                    if (corporationDetail == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    result = corporationdetailDAL.Invalid(user, corporationDetail);
                    if (result.ResultStatus != 0)
                        return result;


                    DAL.CorporationDAL corporationDAL = new CorporationDAL();
                    result = corporationDAL.Get(user, corpId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Corporation corp = result.ReturnValue as Model.Corporation;
                    if (corp == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    result = corporationDAL.Invalid(user, corp);
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

        public ResultModel GoBack(UserModel user, int corpId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = corporationdetailDAL.GetByCorpId(user, corpId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CorporationDetail corporationDetail = result.ReturnValue as Model.CorporationDetail;
                    if (corporationDetail == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    result = corporationdetailDAL.Goback(user,corporationDetail);
                    if (result.ResultStatus != 0)
                        return result;

                    DAL.CorporationDAL corporationDAL = new CorporationDAL();
                    result = corporationDAL.Get(user, corpId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Corporation corp = result.ReturnValue as Model.Corporation;
                    if (corp == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    result = corporationDAL.Goback(user, corp);
                    if (result.ResultStatus != 0)
                        return result;

                    ////工作流任务关闭
                    //WorkFlow.DAL.DataSourceDAL sourceDAL = new WorkFlow.DAL.DataSourceDAL();
                    //result = sourceDAL.SynchronousStatus(user, invoice);
                    //if (result.ResultStatus != 0)
                    //    return result;

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

        public ResultModel Freeze(UserModel user, int corpId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = corporationdetailDAL.GetByCorpId(user, corpId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CorporationDetail corporationDetail = result.ReturnValue as Model.CorporationDetail;
                    if (corporationDetail == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    result = corporationdetailDAL.Freeze(user, corporationDetail);
                    if (result.ResultStatus != 0)
                        return result;


                    DAL.CorporationDAL corporationDAL = new CorporationDAL();
                    result = corporationDAL.Get(user, corpId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Corporation corp = result.ReturnValue as Model.Corporation;
                    if (corp == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    result = corporationDAL.Freeze(user, corp);
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

        public ResultModel UnFreeze(UserModel user, int corpId)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = corporationdetailDAL.GetByCorpId(user, corpId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CorporationDetail corporationDetail = result.ReturnValue as Model.CorporationDetail;
                    if (corporationDetail == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    result = corporationdetailDAL.UnFreeze(user, corporationDetail);
                    if (result.ResultStatus != 0)
                        return result;

                    DAL.CorporationDAL corporationDAL = new CorporationDAL();
                    result = corporationDAL.Get(user, corpId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Corporation corp = result.ReturnValue as Model.Corporation;
                    if (corp == null)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    result = corporationDAL.UnFreeze(user, corp);
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

        public ResultModel Audit(UserModel user, int corpId, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.corporationdetailDAL.GetByCorpId(user, corpId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.CorporationDetail corpDetail = result.ReturnValue as Model.CorporationDetail;
                    if (corpDetail == null || corpDetail.DetailId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "客户不存在";
                        return result;
                    }

                    result = this.corporationdetailDAL.Audit(user, corpDetail, isPass);
                    if (result.ResultStatus != 0)
                        return result;

                    DAL.CorporationDAL corporationDAL = new CorporationDAL();
                    result = corporationDAL.Get(user, corpId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.Corporation corp = result.ReturnValue as Model.Corporation;
                    if (corp == null || corp.CorpId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "公司不存在";
                        return result;
                    }

                    result = corporationDAL.Audit(user, corp, isPass);
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
            return result;
        }

        #endregion
    }
}
