using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Common
{
    public class AuditModel : IModel
    {
        public int Id { get; set; }

        public StatusEnum Status { get; set; }

        public string DataBaseName { get; set; }

        public string TableName { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreateTime { get; set; }

        public int LastModifyId { get; set; }

        public DateTime LastModifyTime { get; set; }

        public string DalName { get; set; }

        public string AssName { get; set; }
    }
}
