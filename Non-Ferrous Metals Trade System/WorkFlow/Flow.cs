using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace NFMT.WorkFlow
{
    public class Flow
    {
        /// <summary>
        /// 同步工作流数据撤返
        /// 业务数据撤返调用
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="rowId"></param>
        /// <returns></returns>
        public NFMT.Common.ResultModel SynDataGoBack(NFMT.Common.UserModel user, string tableName, int rowId)
        {
            Common.ResultModel result = new Common.ResultModel();

            try
            {
                result = this.GetSourceId(tableName, rowId);
                if (result.ResultStatus != 0)
                    return result;

                int SourceId = Convert.ToInt32(result.ReturnValue);
                NFMT.WorkFlow.BLL.DataSourceBLL dataSourceBll = new BLL.DataSourceBLL();
                result = dataSourceBll.Get(user, SourceId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WorkFlow.Model.DataSource dataSource = result.ReturnValue as NFMT.WorkFlow.Model.DataSource;

                result = dataSourceBll.GoBack(user, dataSource);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 同步工作流数据作废
        /// 业务数据作废同时调用
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="rowId"></param>
        /// <returns></returns>
        public NFMT.Common.ResultModel SynDataInvalid(NFMT.Common.UserModel user, string tableName, int rowId)
        {
            Common.ResultModel result = new Common.ResultModel();

            try
            {
                result = this.GetSourceId(tableName, rowId);
                if (result.ResultStatus != 0)
                    return result;

                int SourceId = Convert.ToInt32(result.ReturnValue);
                NFMT.WorkFlow.BLL.DataSourceBLL dataSourceBll = new BLL.DataSourceBLL();
                result = dataSourceBll.Get(user, SourceId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WorkFlow.Model.DataSource dataSource = result.ReturnValue as NFMT.WorkFlow.Model.DataSource;

                result = dataSourceBll.Invalid(user, dataSource);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 同步工作流冻结
        /// 业务数据冻结同时调用
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="rowId"></param>
        /// <returns></returns>
        public NFMT.Common.ResultModel SynDataFreeze(NFMT.Common.UserModel user, string tableName, int rowId)
        {
            Common.ResultModel result = new Common.ResultModel();

            try
            {
                result = this.GetSourceId(tableName, rowId);
                if (result.ResultStatus != 0)
                    return result;

                int SourceId = Convert.ToInt32(result.ReturnValue);
                NFMT.WorkFlow.BLL.DataSourceBLL dataSourceBll = new BLL.DataSourceBLL();
                result = dataSourceBll.Get(user, SourceId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WorkFlow.Model.DataSource dataSource = result.ReturnValue as NFMT.WorkFlow.Model.DataSource;

                result = dataSourceBll.Freeze(user, dataSource);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 同步工作流完成
        /// 业务数据在非工作流外审核生效后调用
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="rowId"></param>
        /// <returns></returns>
        public NFMT.Common.ResultModel SynComplete(NFMT.Common.UserModel user, string tableName, int rowId)
        {
            Common.ResultModel result = new Common.ResultModel();

            try
            {
                result = this.GetSourceId(tableName, rowId);
                if (result.ResultStatus != 0)
                    return result;

                int SourceId = Convert.ToInt32(result.ReturnValue);
                NFMT.WorkFlow.BLL.DataSourceBLL dataSourceBll = new BLL.DataSourceBLL();
                result = dataSourceBll.Get(user, SourceId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WorkFlow.Model.DataSource dataSource = result.ReturnValue as NFMT.WorkFlow.Model.DataSource;

                result = dataSourceBll.Close(user, dataSource);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取数据源主键值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="rowId">key值</param>
        /// <returns></returns>
        private NFMT.Common.ResultModel GetSourceId(string tableName, int rowId)
        {
            Common.ResultModel result = new Common.ResultModel();
            try
            {
                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter para = new SqlParameter("@tableName", tableName);
                paras.Add(para);

                para = new SqlParameter("@rowId", rowId);
                paras.Add(para);

                string cmdText = "select SourceId from dbo.DataSource where TableName=@tableName and RowId=@rowId and DataStatus in (2,4)";

                object obj = NFMT.DBUtility.SqlHelper.ExecuteScalar(NFMT.DBUtility.SqlHelper.ConnectionStringNFMT, CommandType.Text, cmdText, paras.ToArray());

                int count;
                if (obj == null)
                {
                    result.Message = "查询出错";
                    return result;
                }
                if (int.TryParse(obj.ToString(), out count))
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = obj;
                    result.Message = "查询成功";
                }
                else
                {
                    result.Message = "查询出错";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
