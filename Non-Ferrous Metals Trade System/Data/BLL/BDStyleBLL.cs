/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BDStyleBLL.cs
// 文件功能描述：dbo.BDStyle业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年4月22日
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
    /// dbo.BDStyle业务逻辑类。
    /// </summary>
    public class BDStyleBLL : Common.DataBLL
    {
        private BDStyleDAL bDStyleDAL = new BDStyleDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(BDStyleBLL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BDStyleBLL()
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
            get { return this.bDStyleDAL; }
        }

        #endregion

        /// <summary>
        /// 获取BDStyleDetail集合,返回DetailCollection
        /// </summary>
        /// <param name="style">类型</param>
        /// <returns></returns>
        public ResultModel Load(StyleEnum style)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = bDStyleDAL.Load(style);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("加载类型失败，失败原因：{0}", result.Message);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("加载基础类型,执行结果：{0}", result.Message);
            }
            return result;
        }
    }
}
