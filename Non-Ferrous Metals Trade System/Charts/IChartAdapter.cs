using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Charts
{
    public interface IChartAdapter
    {
        string AdapterJson(object obj);
    }
}
