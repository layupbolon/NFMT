using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Common
{
    public interface IExecBLL:IBaseBLL
    {
        ResultModel Complete(UserModel user, IModel obj);

        ResultModel CompleteCancel(UserModel user, IModel obj);
    }
}
