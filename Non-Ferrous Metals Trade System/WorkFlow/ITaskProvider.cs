using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.WorkFlow
{
    public interface ITaskProvider
    {
        NFMT.Common.ResultModel Create(NFMT.Common.UserModel user,NFMT.Common.IModel model);        
    }
}
