using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NFMT.Common
{
    public abstract class ApplyBLL:BaseBLL,IApplyBLL
    {
        public virtual ResultModel Confirm(UserModel user, IModel obj)
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
                        result.Message = "该数据不存在，不能确认完成";
                        return result;
                    }

                    ApplyOperate operate = this.Operate as ApplyOperate;

                    result = operate.Confirm(user, obj);

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
        
        public virtual ResultModel ConfirmCancel(UserModel user, IModel obj)
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
                        result.Message = "该数据不存在，不能确认完成撤销";
                        return result;
                    }

                    ApplyOperate operate = this.Operate as ApplyOperate;

                    result = operate.ConfirmCancel(user, obj);

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
