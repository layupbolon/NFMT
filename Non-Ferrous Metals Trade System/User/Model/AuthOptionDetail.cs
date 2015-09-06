using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.User.Model
{
    public class AuthOptionDetail
    {
        public int DetailId { get; internal set; }

        public int AuthOptionId { get; set; }

        public string DetailCode { get; set; }

        public string DetailName { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreateTime { get; set; }

        public int LastModifyId { get; set; }

        public DateTime LastModifyTime { get; set; }
    }
}
