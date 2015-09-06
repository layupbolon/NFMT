using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Charts
{
    public class TitleModel
    {
        private List<string> text;
        private List<string> x;

        public TitleModel() 
        {
            text = new List<string>();
            x = new List<string>();
        }

        /// <summary>
        /// 主标题名称
        /// </summary>
        public List<string> Text { get { return this.text; } set { this.text = value; } }
        /// <summary>
        /// 大小
        /// </summary>
        public List<string> X { get { return this.x; } set { this.x = value; } }
    }
}
