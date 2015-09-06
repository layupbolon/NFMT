using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Common
{
    public interface IDataBLL:IBaseBLL
    {
        ResultModel Freeze(UserModel user, IModel obj);

        ResultModel UnFreeze(UserModel user, IModel obj);
    }
}
