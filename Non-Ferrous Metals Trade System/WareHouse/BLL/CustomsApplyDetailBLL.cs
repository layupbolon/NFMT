/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CustomsApplyDetailBLL.cs
// 文件功能描述：报关申请明细dbo.St_CustomsApplyDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年10月29日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.WareHouse.Model;
using NFMT.WareHouse.DAL;
using NFMT.WareHouse.IDAL;
using NFMT.Common;

namespace NFMT.WareHouse.BLL
{
    /// <summary>
    /// 报关申请明细dbo.St_CustomsApplyDetail业务逻辑类。
    /// </summary>
    public class CustomsApplyDetailBLL : Common.ApplyBLL
    {
        private CustomsApplyDetailDAL customsapplydetailDAL = new CustomsApplyDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CustomsApplyDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomsApplyDetailBLL()
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
            get { return this.customsapplydetailDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel GetStockIdById(UserModel user, int customApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = customsapplydetailDAL.GetStockIdById(user, customApplyId);
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

        #endregion
    }
}
