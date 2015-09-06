using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Charts
{
    public class TooltipModel
    {
        private List<string> valueSuffix;

        public TooltipModel() 
        {
            valueSuffix = new List<string>();
        }

        /// <summary>
        /// 提示框单位
        /// </summary>
        public List<string> ValueSuffix { get { return this.valueSuffix; } set { this.valueSuffix = value;} }
    }
}
