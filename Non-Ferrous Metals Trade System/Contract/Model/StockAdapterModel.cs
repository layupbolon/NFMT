using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Contract.Model
{
    public class StockAdapterModel
    {
        private int stockId = 0;
        private int bundles = 0;
        private decimal netAmount = 0;

        public int StockId
        {
            get { return this.stockId; }
            set { this.stockId = value; }
        }

        public int Bundles
        {
            get { return this.bundles; }
            set { this.bundles = value; }
        }

        public decimal NetAmount
        {
            get { return this.netAmount; }
            set { this.netAmount = value; }
        }
    }
}
