using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.User.Model
{
    public class AuthOption
    {
        public int AuthOptionId { get; internal set; }

        public string OptionCode { get; set; }

        public string OptionName { get; set; }

        public string DataBaseName { get; set; }

        public string TableName { get; set; }

        public int RowId { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreateTime { get; set; }

        public int LastModifyId { get; set; }

        public DateTime LastModifyTime { get; set; }
    }
}
