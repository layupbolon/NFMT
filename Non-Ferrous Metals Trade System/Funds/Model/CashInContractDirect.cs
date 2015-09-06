using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Funds.Model
{
    public class CashInContractDirect:CashInContract
    {
        private int allotCorpId = 0;
        public int AllotCorpId
        {
            get { return this.allotCorpId; }
            set { this.allotCorpId = value; }
        }

        public string allotCorp = string.Empty;
        public string AllotCorp
        {
            get { return this.allotCorp; }
            set { this.allotCorp = value; }
        }

        private int stockLogId = 0;
        public int StockLogId
        {
            get { return this.stockLogId; }
            set { this.stockLogId = value; }
        }
    }
}
