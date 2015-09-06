/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ExceOperate.cs
// 文件功能描述：执行类操作父类。
// 创建人：pekah.chow
// 创建时间： 2014-08-07
----------------------------------------------------------------*/

using NFMT.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Common
{
    public abstract class ExecOperate : Operate, IExecOperate
    {
        public virtual ResultModel Complete(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "执行完成对象不能为null";
                    return result;
                }

                result = AllowOperate(user, obj, OperateEnum.执行完成);
                if (result.ResultStatus != 0)
                    return result;

                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter idPara = new SqlParameter("@id", SqlDbType.Int, 4);
                idPara.Value = obj.Id;
                paras.Add(idPara);

                SqlParameter statusPara = new SqlParameter("@status", SqlDbType.Int, 4);
                statusPara.Value = (int)StatusEnum.已完成;
                paras.Add(statusPara);

                SqlParameter lastModifyIdPara = new SqlParameter("@lastModifyId", SqlDbType.Int, 4);
                lastModifyIdPara.Value = user.AccountId;
                paras.Add(lastModifyIdPara);

                int i = SqlHelper.ExecuteNonQuery(this.ConnectString, CommandType.StoredProcedure, string.Format("{0}UpdateStatus", obj.TableName), paras.ToArray());

                if (i == 1)
                {
                    result.Message = "执行完成成功";
                    result.ResultStatus = 0;
                }
                else
                    result.Message = "执行完成失败";

                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public virtual ResultModel CompleteCancel(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "执行完成撤销对象不能为null";
                    return result;
                }

                result = AllowOperate(user, obj, OperateEnum.执行完成撤销);
                if (result.ResultStatus != 0)
                    return result;

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
                    result.Message = "执行完成撤销成功";
                    result.ResultStatus = 0;
                }
                else
                    result.Message = "执行完成撤销失败";

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
    }
}
