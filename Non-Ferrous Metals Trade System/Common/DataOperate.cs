/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DataOperate.cs
// 文件功能描述：数据类操作父类。
// 创建人：pekah.chow
// 创建时间： 2014-04-28
----------------------------------------------------------------*/

using NFMT.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace NFMT.Common
{
    public abstract class DataOperate : Operate, IDataOperate
    {
        public ResultModel Freeze(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "冻结对象不能为null";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter idPara = new SqlParameter("@id", SqlDbType.Int, 4);
                idPara.Value = obj.Id;
                paras.Add(idPara);

                SqlParameter statusPara = new SqlParameter("@status", SqlDbType.Int, 4);
                statusPara.Value = (int)StatusEnum.已冻结;
                paras.Add(statusPara);

                SqlParameter lastModifyIdPara = new SqlParameter("@lastModifyId", SqlDbType.Int, 4);
                lastModifyIdPara.Value = user.AccountId;
                paras.Add(lastModifyIdPara);

                int i = SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.StoredProcedure, string.Format("{0}UpdateStatus", obj.TableName), paras.ToArray());

                if (i == 1)
                {
                    result.Message = "冻结成功";
                    result.ResultStatus = 0;
                }
                else
                    result.Message = "冻结失败";

                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel UnFreeze(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "解除冻结对象不能为null";
                    return result;
                }

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter idPara = new SqlParameter("@id", SqlDbType.Int, 4);
                idPara.Value = obj.Id;
                paras.Add(idPara);

                SqlParameter statusPara = new SqlParameter("@status", SqlDbType.Int, 4);
                statusPara.Value = (int)StatusEnum.已生效;
                paras.Add(statusPara);

                SqlParameter lastModifyIdPara = new SqlParameter("@lastModifyId", SqlDbType.Int, 4);
                lastModifyIdPara.Value = user.AccountId;
                paras.Add(lastModifyIdPara);

                int i = SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.StoredProcedure, string.Format("{0}UpdateStatus", obj.TableName), paras.ToArray());

                if (i == 1)
                {
                    result.Message = "解除冻结成功";
                    result.ResultStatus = 0;
                }
                else
                    result.Message = "解除冻结失败";

                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }                

        public override IAuthority Authority
        {
            get { return new BasicAuth(); }
        }

        public override ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate)
        {
            if (operate == OperateEnum.修改)
            {
                ResultModel result = new ResultModel();
                result.ResultStatus = 0;
                return result;
            }

            return base.AllowOperate(user, obj, operate);
        }
    }
}
