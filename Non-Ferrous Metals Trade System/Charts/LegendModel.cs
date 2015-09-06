using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Charts
{
    public class LegendModel
    {
        private List<string> layout;
        private List<string> align;
        private List<string> verticalAlign;
        private List<string> borderWidth;

        public LegendModel()
        {
            layout = new List<string>();
            align = new List<string>();
            verticalAlign = new List<string>();
            borderWidth = new List<string>();
        }

        public List<string> Layout
        {
            get { return this.layout; }
            set { this.layout = value; }
        }
        public List<string> Align
        {
            get { return this.align; }
            set { this.align = value; }
        }
        public List<string> VerticalAlign 
        { 
            get { return this.verticalAlign; }
            set { this.verticalAlign = value; }
        }
        public List<string> BorderWidth 
        {
            get { return this.borderWidth; }
            set { this.borderWidth = value; }
        }

    }
}
