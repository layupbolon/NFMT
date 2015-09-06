using System;
using System.Collections.Generic;
using System.Text;
using NFMT.Common;
using System.Data.SqlClient;
using System.Data;
using NFMT.DBUtility;

namespace NFMT.RollBack
{
    public class OperateLogUtility
    {
        public static ResultModel InsertOperate(UserModel user, OperateModel model)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (model == null)
                {
                    result.Message = "新增对象不能为null";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter logIdPara = new SqlParameter();
                logIdPara.Direction = ParameterDirection.Output;
                logIdPara.SqlDbType = SqlDbType.Int;
                logIdPara.ParameterName = "@logId";
                logIdPara.Size = 4;
                paras.Add(logIdPara);

                SqlParameter operateIdPara = new SqlParameter("@operateId", SqlDbType.Int, 4);
                operateIdPara.Value = model.OperateId;
                paras.Add(operateIdPara);

                SqlParameter empIdPara = new SqlParameter("@empId", SqlDbType.Int, 4);
                empIdPara.Value = user.EmpId;
                paras.Add(empIdPara);

                int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringNFMT, CommandType.StoredProcedure, "OperateLogInsert", paras.ToArray());

                if (i == 1)
                {
                    result.ResultStatus = 0;
                    result.AffectCount = i;
                    result.Message = "类型添加成功";
                    result.ReturnValue = logIdPara.Value;
                }
                else
                    result.Message = "类型添加失败";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        private static ResultModel InsertOperateLog(UserModel user, OperateLogModel model)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (model == null)
                {
                    result.Message = "新增对象不能为null";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter logIdPara = new SqlParameter();
                logIdPara.Direction = ParameterDirection.Output;
                logIdPara.SqlDbType = SqlDbType.Int;
                logIdPara.ParameterName = "@logId";
                logIdPara.Size = 4;
                paras.Add(logIdPara);

                SqlParameter operateIdPara = new SqlParameter("@operateId", SqlDbType.Int, 4);
                operateIdPara.Value = model.OperateId;
                paras.Add(operateIdPara);

                SqlParameter empIdPara = new SqlParameter("@empId", SqlDbType.Int, 4);
                empIdPara.Value = user.EmpId;
                paras.Add(empIdPara);

                int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringNFMT, CommandType.StoredProcedure, "OperateLogInsert", paras.ToArray());

                if (i == 1)
                {
                    result.ResultStatus = 0;
                    result.AffectCount = i;
                    result.Message = "操作记录添加成功";
                    result.ReturnValue = logIdPara.Value;
                }
                else
                    result.Message = "操作记录添加失败";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        private static ResultModel InsertOperateLogDetail(UserModel user, OperateLogDetailModel model)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (model == null)
                {
                    result.Message = "新增对象不能为null";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter detailIdPara = new SqlParameter();
                detailIdPara.Direction = ParameterDirection.Output;
                detailIdPara.SqlDbType = SqlDbType.Int;
                detailIdPara.ParameterName = "@detailId";
                detailIdPara.Size = 4;
                paras.Add(detailIdPara);

                SqlParameter logIdPara = new SqlParameter("@logId", SqlDbType.Int, 4);
                logIdPara.Value = model.LogId;
                paras.Add(logIdPara);

                SqlParameter sourceTablePara = new SqlParameter("@sourceTable", SqlDbType.VarChar, 20);
                sourceTablePara.Value = model.SourceTable;
                paras.Add(sourceTablePara);

                SqlParameter rowIdPara = new SqlParameter("@rowId", SqlDbType.Int, 4);
                rowIdPara.Value = model.RowId;
                paras.Add(rowIdPara);

                SqlParameter preVerPara = new SqlParameter("@preVer", SqlDbType.Int, 4);
                preVerPara.Value = model.PreVer;
                paras.Add(preVerPara);

                SqlParameter curVerPara = new SqlParameter("@curVer", SqlDbType.Int, 4);
                curVerPara.Value = model.CurVer;
                paras.Add(curVerPara);

                int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringNFMT, CommandType.StoredProcedure, "OperateLogDetailInsert", paras.ToArray());

                if (i == 1)
                {
                    result.ResultStatus = 0;
                    result.AffectCount = i;
                    result.Message = "操作记录明细添加成功";
                    result.ReturnValue = logIdPara.Value;
                }
                else
                    result.Message = "操作记录明细添加失败";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 添加操作记录
        /// </summary>
        /// <param name="user">当前操作用户</param>
        /// <param name="log">记录实体</param>
        /// <param name="details">记录明细列表</param>
        /// <returns></returns>
        public static ResultModel InsertOperateLog(UserModel user, OperateLogModel log, List<OperateLogDetailModel> details)
        {
            ResultModel result = new ResultModel();

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                result = InsertOperateLog(user, log);
                if (result.ResultStatus != 0)
                    return result;

                foreach (OperateLogDetailModel m in details)
                {
                    result = InsertOperateLogDetail(user, m);
                    if (result.ResultStatus != 0)
                        return result;
                }

                scope.Complete();

                result.AffectCount = 1;
                result.Message = string.Format("操作记录添加成功，明细共{0}条", details.Count);
                result.ResultStatus = 0;
                result.ReturnValue = 1;
            }

            return result;
        }
    }
}
