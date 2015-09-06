using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Document
{
    public enum OrderTypeEnum
    {
        临票制单指令 = 277,
        终票制单指令 = 278,
        无配货临票制单指令 = 285,
        无配货终票制单指令 = 287,
        替临制单指令 = 288
    }
}
