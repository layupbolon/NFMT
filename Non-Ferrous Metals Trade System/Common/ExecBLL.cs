using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NFMT.Common
{
    public abstract class ExecBLL:BaseBLL,IExecBLL
    {
        public ResultModel Complete(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Operate.Get(user, obj.Id);
                    obj = (IModel)result.ReturnValue;

                    if (obj == null)
                    {
                        result.Message = "该数据不存在，不能执行完成";
                        return result;
                    }

                    ExecOperate operate = this.Operate as ExecOperate;

                    result = operate.Complete(user, obj);

                    if (result.ResultStatus != 0)
                        return result;

                    scope.Complete();
                }
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

        public ResultModel CompleteCancel(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = this.Operate.Get(user, obj.Id);
                    obj = (IModel)result.ReturnValue;

                    if (obj == null)
                    {
                        result.Message = "该数据不存在，不能执行完成撤销";
                        return result;
                    }

                    ExecOperate operate = this.Operate as ExecOperate;

                    result = operate.CompleteCancel(user, obj);

                    if (result.ResultStatus != 0)
                        return result;

                    scope.Complete();
                }
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
    }
}
