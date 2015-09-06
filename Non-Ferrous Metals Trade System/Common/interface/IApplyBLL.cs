using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Common
{
    public interface IApplyBLL:IBaseBLL
    {
        ResultModel Confirm(UserModel user, IModel obj);

        ResultModel ConfirmCancel(UserModel user, IModel obj);
    }
}
