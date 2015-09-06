/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Wf_DataSourceDAL.cs
// 文件功能描述：数据源表dbo.Wf_DataSource数据交互类。
// 创建人：CodeSmith
// 创建时间： 2014年7月11日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using NFMT.WorkFlow.Model;
using NFMT.DBUtility;
using NFMT.WorkFlow.IDAL;
using NFMT.Common;
using System.Linq;

namespace NFMT.WorkFlow.DAL
{
    /// <summary>
    /// 数据源表dbo.Wf_DataSource数据交互类。
    /// </summary>
    public class DataSourceDAL : DataOperate, IDataSourceDAL
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataSourceDAL()
        {
        }

        #endregion

        #region 数据库操作

        public override string ConnectString
        {
            get
            {
                return NFMT.DBUtility.SqlHelper.ConnectionStringWorkFlow;
            }
        }

        public override List<SqlParameter> CreateInsertParameters(IModel obj, ref SqlParameter returnValue)
        {
            DataSource wf_datasource = (DataSource)obj;

            List<SqlParameter> paras = new List<SqlParameter>();
            returnValue.Direction = ParameterDirection.Output;
            returnValue.SqlDbType = SqlDbType.Int;
            returnValue.ParameterName = "@SourceId";
            returnValue.Size = 4;
            paras.Add(returnValue);

            if (!string.IsNullOrEmpty(wf_datasource.BaseName))
            {
                SqlParameter basenamepara = new SqlParameter("@BaseName", SqlDbType.VarChar, 200);
                basenamepara.Value = wf_datasource.BaseName;
                paras.Add(basenamepara);
            }

            if (!string.IsNullOrEmpty(wf_datasource.TableCode))
            {
                SqlParameter tablecodepara = new SqlParameter("@TableCode", SqlDbType.VarChar, 50);
                tablecodepara.Value = wf_datasource.TableCode;
                paras.Add(tablecodepara);
            }

            SqlParameter datastatuspara = new SqlParameter("@DataStatus", SqlDbType.Int, 4);
            datastatuspara.Value = wf_datasource.DataStatus;
            paras.Add(datastatuspara);

            SqlParameter rowidpara = new SqlParameter("@RowId", SqlDbType.Int, 4);
            rowidpara.Value = wf_datasource.RowId;
            paras.Add(rowidpara);

            if (!string.IsNullOrEmpty(wf_datasource.DalName))
            {
                SqlParameter dalnamepara = new SqlParameter("@DalName", SqlDbType.VarChar, 80);
                dalnamepara.Value = wf_datasource.DalName;
                paras.Add(dalnamepara);
            }

            if (!string.IsNullOrEmpty(wf_datasource.AssName))
            {
                SqlParameter assnamepara = new SqlParameter("@AssName", SqlDbType.VarChar, 50);
                assnamepara.Value = wf_datasource.AssName;
                paras.Add(assnamepara);
            }

            if (!string.IsNullOrEmpty(wf_datasource.ViewUrl))
            {
                SqlParameter viewurlpara = new SqlParameter("@ViewUrl", SqlDbType.VarChar, 400);
                viewurlpara.Value = wf_datasource.ViewUrl;
                paras.Add(viewurlpara);
            }

            if (!string.IsNullOrEmpty(wf_datasource.RefusalUrl))
            {
                SqlParameter refusalurlpara = new SqlParameter("@RefusalUrl", SqlDbType.VarChar, 800);
                refusalurlpara.Value = wf_datasource.RefusalUrl;
                paras.Add(refusalurlpara);
            }

            if (!string.IsNullOrEmpty(wf_datasource.SuccessUrl))
            {
                SqlParameter successurlpara = new SqlParameter("@SuccessUrl", SqlDbType.VarChar, 800);
                successurlpara.Value = wf_datasource.SuccessUrl;
                paras.Add(successurlpara);
            }

            if (!string.IsNullOrEmpty(wf_datasource.ConditionUrl))
            {
                SqlParameter conditionurlpara = new SqlParameter("@ConditionUrl", SqlDbType.VarChar, 800);
                conditionurlpara.Value = wf_datasource.ConditionUrl;
                paras.Add(conditionurlpara);
            }

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = wf_datasource.EmpId;
            paras.Add(empidpara);

            SqlParameter applytimepara = new SqlParameter("@ApplyTime", SqlDbType.DateTime, 8);
            applytimepara.Value = wf_datasource.ApplyTime;
            paras.Add(applytimepara);

            if (!string.IsNullOrEmpty(wf_datasource.ApplyTitle))
            {
                SqlParameter applytitlepara = new SqlParameter("@ApplyTitle", SqlDbType.VarChar, 400);
                applytitlepara.Value = wf_datasource.ApplyTitle;
                paras.Add(applytitlepara);
            }

            if (!string.IsNullOrEmpty(wf_datasource.ApplyMemo))
            {
                SqlParameter applymemopara = new SqlParameter("@ApplyMemo", SqlDbType.VarChar, 4000);
                applymemopara.Value = wf_datasource.ApplyMemo;
                paras.Add(applymemopara);
            }

            if (!string.IsNullOrEmpty(wf_datasource.ApplyInfo))
            {
                SqlParameter applyinfopara = new SqlParameter("@ApplyInfo", SqlDbType.VarChar, 4000);
                applyinfopara.Value = wf_datasource.ApplyInfo;
                paras.Add(applyinfopara);
            }


            return paras;
        }

        public override IModel CreateModel(SqlDataReader dr)
        {
            DataSource datasource = new DataSource();

            int indexSourceId = dr.GetOrdinal("SourceId");
            datasource.SourceId = Convert.ToInt32(dr[indexSourceId]);

            int indexBaseName = dr.GetOrdinal("BaseName");
            if (dr["BaseName"] != DBNull.Value)
            {
                datasource.BaseName = Convert.ToString(dr[indexBaseName]);
            }

            int indexTableCode = dr.GetOrdinal("TableCode");
            if (dr["TableCode"] != DBNull.Value)
            {
                datasource.TableCode = Convert.ToString(dr[indexTableCode]);
            }

            int indexDataStatus = dr.GetOrdinal("DataStatus");
            if (dr["DataStatus"] != DBNull.Value)
            {
                datasource.DataStatus = (Common.StatusEnum)Convert.ToInt32(dr[indexDataStatus]);
            }

            int indexRowId = dr.GetOrdinal("RowId");
            if (dr["RowId"] != DBNull.Value)
            {
                datasource.RowId = Convert.ToInt32(dr[indexRowId]);
            }

            int indexDalName = dr.GetOrdinal("DalName");
            if (dr["DalName"] != DBNull.Value)
            {
                datasource.DalName = Convert.ToString(dr[indexDalName]);
            }

            int indexAssName = dr.GetOrdinal("AssName");
            if (dr["AssName"] != DBNull.Value)
            {
                datasource.AssName = Convert.ToString(dr[indexAssName]);
            }

            int indexViewUrl = dr.GetOrdinal("ViewUrl");
            if (dr["ViewUrl"] != DBNull.Value)
            {
                datasource.ViewUrl = Convert.ToString(dr[indexViewUrl]);
            }

            int indexRefusalUrl = dr.GetOrdinal("RefusalUrl");
            if (dr["RefusalUrl"] != DBNull.Value)
            {
                datasource.RefusalUrl = Convert.ToString(dr[indexRefusalUrl]);
            }

            int indexSuccessUrl = dr.GetOrdinal("SuccessUrl");
            if (dr["SuccessUrl"] != DBNull.Value)
            {
                datasource.SuccessUrl = Convert.ToString(dr[indexSuccessUrl]);
            }

            int indexConditionUrl = dr.GetOrdinal("ConditionUrl");
            if (dr["ConditionUrl"] != DBNull.Value)
            {
                datasource.ConditionUrl = Convert.ToString(dr[indexConditionUrl]);
            }

            int indexEmpId = dr.GetOrdinal("EmpId");
            if (dr["EmpId"] != DBNull.Value)
            {
                datasource.EmpId = Convert.ToInt32(dr[indexEmpId]);
            }

            int indexApplyTime = dr.GetOrdinal("ApplyTime");
            if (dr["ApplyTime"] != DBNull.Value)
            {
                datasource.ApplyTime = Convert.ToDateTime(dr[indexApplyTime]);
            }

            int indexApplyTitle = dr.GetOrdinal("ApplyTitle");
            if (dr["ApplyTitle"] != DBNull.Value)
            {
                datasource.ApplyTitle = Convert.ToString(dr[indexApplyTitle]);
            }

            int indexApplyMemo = dr.GetOrdinal("ApplyMemo");
            if (dr["ApplyMemo"] != DBNull.Value)
            {
                datasource.ApplyMemo = Convert.ToString(dr[indexApplyMemo]);
            }

            int indexApplyInfo = dr.GetOrdinal("ApplyInfo");
            if (dr["ApplyInfo"] != DBNull.Value)
            {
                datasource.ApplyInfo = Convert.ToString(dr[indexApplyInfo]);
            }


            return datasource;
        }

        public override string TableName
        {
            get
            {
                return "Wf_DataSource";
            }
        }

        public override List<SqlParameter> CreateUpdateParameters(IModel obj)
        {
            DataSource wf_datasource = (DataSource)obj;

            List<SqlParameter> paras = new List<SqlParameter>();

            SqlParameter sourceidpara = new SqlParameter("@SourceId", SqlDbType.Int, 4);
            sourceidpara.Value = wf_datasource.SourceId;
            paras.Add(sourceidpara);

            if (!string.IsNullOrEmpty(wf_datasource.BaseName))
            {
                SqlParameter basenamepara = new SqlParameter("@BaseName", SqlDbType.VarChar, 200);
                basenamepara.Value = wf_datasource.BaseName;
                paras.Add(basenamepara);
            }

            if (!string.IsNullOrEmpty(wf_datasource.TableCode))
            {
                SqlParameter tablecodepara = new SqlParameter("@TableCode", SqlDbType.VarChar, 50);
                tablecodepara.Value = wf_datasource.TableCode;
                paras.Add(tablecodepara);
            }

            SqlParameter datastatuspara = new SqlParameter("@DataStatus", SqlDbType.Int, 4);
            datastatuspara.Value = wf_datasource.DataStatus;
            paras.Add(datastatuspara);

            SqlParameter rowidpara = new SqlParameter("@RowId", SqlDbType.Int, 4);
            rowidpara.Value = wf_datasource.RowId;
            paras.Add(rowidpara);

            if (!string.IsNullOrEmpty(wf_datasource.DalName))
            {
                SqlParameter dalnamepara = new SqlParameter("@DalName", SqlDbType.VarChar, 80);
                dalnamepara.Value = wf_datasource.DalName;
                paras.Add(dalnamepara);
            }

            if (!string.IsNullOrEmpty(wf_datasource.AssName))
            {
                SqlParameter assnamepara = new SqlParameter("@AssName", SqlDbType.VarChar, 50);
                assnamepara.Value = wf_datasource.AssName;
                paras.Add(assnamepara);
            }

            if (!string.IsNullOrEmpty(wf_datasource.ViewUrl))
            {
                SqlParameter viewurlpara = new SqlParameter("@ViewUrl", SqlDbType.VarChar, 400);
                viewurlpara.Value = wf_datasource.ViewUrl;
                paras.Add(viewurlpara);
            }

            if (!string.IsNullOrEmpty(wf_datasource.RefusalUrl))
            {
                SqlParameter refusalurlpara = new SqlParameter("@RefusalUrl", SqlDbType.VarChar, 800);
                refusalurlpara.Value = wf_datasource.RefusalUrl;
                paras.Add(refusalurlpara);
            }

            if (!string.IsNullOrEmpty(wf_datasource.SuccessUrl))
            {
                SqlParameter successurlpara = new SqlParameter("@SuccessUrl", SqlDbType.VarChar, 800);
                successurlpara.Value = wf_datasource.SuccessUrl;
                paras.Add(successurlpara);
            }

            if (!string.IsNullOrEmpty(wf_datasource.ConditionUrl))
            {
                SqlParameter conditionurlpara = new SqlParameter("@ConditionUrl", SqlDbType.VarChar, 800);
                conditionurlpara.Value = wf_datasource.ConditionUrl;
                paras.Add(conditionurlpara);
            }

            SqlParameter empidpara = new SqlParameter("@EmpId", SqlDbType.Int, 4);
            empidpara.Value = wf_datasource.EmpId;
            paras.Add(empidpara);

            SqlParameter applytimepara = new SqlParameter("@ApplyTime", SqlDbType.DateTime, 8);
            applytimepara.Value = wf_datasource.ApplyTime;
            paras.Add(applytimepara);

            if (!string.IsNullOrEmpty(wf_datasource.ApplyTitle))
            {
                SqlParameter applytitlepara = new SqlParameter("@ApplyTitle", SqlDbType.VarChar, 400);
                applytitlepara.Value = wf_datasource.ApplyTitle;
                paras.Add(applytitlepara);
            }

            if (!string.IsNullOrEmpty(wf_datasource.ApplyMemo))
            {
                SqlParameter applymemopara = new SqlParameter("@ApplyMemo", SqlDbType.VarChar, 4000);
                applymemopara.Value = wf_datasource.ApplyMemo;
                paras.Add(applymemopara);
            }

            if (!string.IsNullOrEmpty(wf_datasource.ApplyInfo))
            {
                SqlParameter applyinfopara = new SqlParameter("@ApplyInfo", SqlDbType.VarChar, 4000);
                applyinfopara.Value = wf_datasource.ApplyInfo;
                paras.Add(applyinfopara);
            }


            return paras;
        }

        #endregion

        #region 新增方法

        public ResultModel DataSourceComplete(UserModel user, Model.DataSource dataSource)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (dataSource == null)
                {
                    result.Message = "确认完成对象不能为null";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter idPara = new SqlParameter("@id", SqlDbType.Int, 4);
                idPara.Value = dataSource.Id;
                paras.Add(idPara);

                SqlParameter statusPara = new SqlParameter("@status", SqlDbType.Int, 4);
                statusPara.Value = (int)StatusEnum.已完成;
                paras.Add(statusPara);

                SqlParameter lastModifyIdPara = new SqlParameter("@lastModifyId", SqlDbType.Int, 4);
                lastModifyIdPara.Value = user.AccountId;
                paras.Add(lastModifyIdPara);

                int i = SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.StoredProcedure, string.Format("{0}UpdateStatus", "dbo.Wf_DataSource"), paras.ToArray());

                if (i == 1)
                {
                    result.Message = "确认完成成功";
                    result.ResultStatus = 0;
                }
                else
                    result.Message = "确认完成失败";

                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        //public override ResultModel Freeze(UserModel user, IModel obj)
        //{
        //    ResultModel result = new ResultModel();

        //    try
        //    {
        //        if (obj == null)
        //        {
        //            result.Message = "冻结对象不能为null";
        //            return result;
        //        }

        //        List<SqlParameter> paras = new List<SqlParameter>();

        //        SqlParameter idPara = new SqlParameter("@id", SqlDbType.Int, 4);
        //        idPara.Value = obj.Id;
        //        paras.Add(idPara);

        //        SqlParameter statusPara = new SqlParameter("@status", SqlDbType.Int, 4);
        //        statusPara.Value = (int)StatusEnum.已冻结;
        //        paras.Add(statusPara);

        //        SqlParameter lastModifyIdPara = new SqlParameter("@lastModifyId", SqlDbType.Int, 4);
        //        lastModifyIdPara.Value = user.AccountId;
        //        paras.Add(lastModifyIdPara);

        //        int i = SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.StoredProcedure, string.Format("{0}UpdateStatus", obj.TableName), paras.ToArray());

        //        if (i == 1)
        //        {
        //            result.Message = "冻结成功";
        //            result.ResultStatus = 0;
        //        }
        //        else
        //            result.Message = "冻结失败";

        //        result.AffectCount = i;
        //        result.ReturnValue = i;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = ex.Message;
        //    }

        //    return result;
        //}

        public ResultModel CheckHasSource(DataSource source)
        {
            ResultModel result = new ResultModel();

            try
            {
                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter para = new SqlParameter("@tableName", source.TableCode);
                paras.Add(para);

                para = new SqlParameter("@rowId", source.RowId);
                paras.Add(para);

                string cmdText = string.Format("select count(*) from NFMT_WorkFlow.dbo.Wf_DataSource where TableCode=@tableName and RowId=@rowId and DataStatus in ({0},{1})", (int)NFMT.Common.StatusEnum.已完成, (int)NFMT.Common.StatusEnum.待审核);

                object obj = SqlHelper.ExecuteScalar(ConnectString, CommandType.Text, cmdText, paras.ToArray());

                int count;
                if (obj == null)
                {
                    result.Message = "查询出错";
                    return result;
                }
                if (int.TryParse(obj.ToString(), out count))
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = count;
                    result.Message = "查询成功";
                    result.AffectCount = count;
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

        public ResultModel GetDataSource(UserModel user, IModel model)
        {
            ResultModel result = new ResultModel();


            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@BaseName", SqlDbType.VarChar);
            para.Value = model.DataBaseName;
            paras.Add(para);

            para = new SqlParameter("@TableCode", SqlDbType.VarChar);
            para.Value = model.TableName;
            paras.Add(para);

            para = new SqlParameter("@RowId", SqlDbType.Int);
            para.Value = model.Id;
            paras.Add(para);

            para = new SqlParameter("@status", SqlDbType.Int);
            para.Value = StatusEnum.待审核;
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                string sql = string.Format("select * from NFMT_WorkFlow.dbo.Wf_DataSource where BaseName = @BaseName and TableCode = @TableCode and RowId = @RowId  and DataStatus =@status", model.DataBaseName, model.TableName, model.Id);
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, sql, paras.ToArray());

                DataSource datasource = new DataSource();

                if (dr.Read())
                {
                    int indexSourceId = dr.GetOrdinal("SourceId");
                    datasource.SourceId = Convert.ToInt32(dr[indexSourceId]);

                    int indexBaseName = dr.GetOrdinal("BaseName");
                    if (dr["BaseName"] != DBNull.Value)
                    {
                        datasource.BaseName = Convert.ToString(dr[indexBaseName]);
                    }

                    int indexTableCode = dr.GetOrdinal("TableCode");
                    if (dr["TableCode"] != DBNull.Value)
                    {
                        datasource.TableCode = Convert.ToString(dr[indexTableCode]);
                    }

                    int indexDataStatus = dr.GetOrdinal("DataStatus");
                    if (dr["DataStatus"] != DBNull.Value)
                    {
                        datasource.DataStatus = (Common.StatusEnum)Enum.Parse(typeof(Common.StatusEnum), dr[indexDataStatus].ToString());
                    }

                    int indexRowId = dr.GetOrdinal("RowId");
                    if (dr["RowId"] != DBNull.Value)
                    {
                        datasource.RowId = Convert.ToInt32(dr[indexRowId]);
                    }

                    int indexDalName = dr.GetOrdinal("DalName");
                    if (dr["DalName"] != DBNull.Value)
                    {
                        datasource.DalName = Convert.ToString(dr[indexDalName]);
                    }

                    int indexAssName = dr.GetOrdinal("AssName");
                    if (dr["AssName"] != DBNull.Value)
                    {
                        datasource.AssName = Convert.ToString(dr[indexAssName]);
                    }

                    int indexViewUrl = dr.GetOrdinal("ViewUrl");
                    if (dr["ViewUrl"] != DBNull.Value)
                    {
                        datasource.ViewUrl = Convert.ToString(dr[indexViewUrl]);
                    }

                    int indexRefusalUrl = dr.GetOrdinal("RefusalUrl");
                    if (dr["RefusalUrl"] != DBNull.Value)
                    {
                        datasource.RefusalUrl = Convert.ToString(dr[indexRefusalUrl]);
                    }

                    int indexSuccessUrl = dr.GetOrdinal("SuccessUrl");
                    if (dr["SuccessUrl"] != DBNull.Value)
                    {
                        datasource.SuccessUrl = Convert.ToString(dr[indexSuccessUrl]);
                    }

                    int indexConditionUrl = dr.GetOrdinal("ConditionUrl");
                    if (dr["ConditionUrl"] != DBNull.Value)
                    {
                        datasource.ConditionUrl = Convert.ToString(dr[indexConditionUrl]);
                    }

                    int indexEmpId = dr.GetOrdinal("EmpId");
                    if (dr["EmpId"] != DBNull.Value)
                    {
                        datasource.EmpId = Convert.ToInt32(dr[indexEmpId]);
                    }

                    int indexApplyTime = dr.GetOrdinal("ApplyTime");
                    if (dr["ApplyTime"] != DBNull.Value)
                    {
                        datasource.ApplyTime = Convert.ToDateTime(dr[indexApplyTime]);
                    }

                    int indexApplyTitle = dr.GetOrdinal("ApplyTitle");
                    if (dr["ApplyTitle"] != DBNull.Value)
                    {
                        datasource.ApplyTitle = Convert.ToString(dr[indexApplyTitle]);
                    }

                    int indexApplyMemo = dr.GetOrdinal("ApplyMemo");
                    if (dr["ApplyMemo"] != DBNull.Value)
                    {
                        datasource.ApplyMemo = Convert.ToString(dr[indexApplyMemo]);
                    }

                    int indexApplyInfo = dr.GetOrdinal("ApplyInfo");
                    if (dr["ApplyInfo"] != DBNull.Value)
                    {
                        datasource.ApplyInfo = Convert.ToString(dr[indexApplyInfo]);
                    }

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = datasource;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }

            return result;
        }

        public ResultModel SynchronousStatus(UserModel user, IModel model)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.GetDataSource(user, model);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WorkFlow.Model.DataSource source = result.ReturnValue as NFMT.WorkFlow.Model.DataSource;
                if (source == null || source.SourceId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "工作流无对应任务可撤返";
                    return result;
                }

                source.Status = StatusEnum.已录入;

                result = this.Invalid(user, source);
                if (result.ResultStatus != 0)
                    return result;

                DAL.TaskDAL taskDAL = new TaskDAL();
                result = taskDAL.GetTaskByDataSourceId(user, source.SourceId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WorkFlow.Model.Task task = result.ReturnValue as NFMT.WorkFlow.Model.Task;

                task.Status = StatusEnum.已录入;
                result = taskDAL.Invalid(user, task);
                if (result.ResultStatus != 0)
                    return result;

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel GetDataSourceByTaskNodeId(UserModel user, int taskNodeId)
        {
            ResultModel result = new ResultModel();

            try
            {
                string sql = string.Format("select * from dbo.Wf_DataSource where SourceId = (select DataSourceId from dbo.Wf_Task where TaskId = (select TaskId from dbo.Wf_TaskNode where TaskNodeId = {0}))", taskNodeId);
                result = Load<Model.DataSource>(user, CommandType.Text, sql);
                if (result.AffectCount > 0) {
                    result.ResultStatus = 0;
                    result.Message = "获取成功";
                    List<Model.DataSource> dataSources = result.ReturnValue as List<Model.DataSource>;
                    result.ReturnValue = dataSources.First();
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }
            return result;
        }

        #endregion
    }
}
