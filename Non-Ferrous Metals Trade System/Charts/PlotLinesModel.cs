using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Charts
{
    public class PlotLinesModel
    {
        private List<string> value;
        private List<string> width;
        private List<string> color;

        public PlotLinesModel() 
        {
            value = new List<string>();
            width = new List<string>();
            color = new List<string>();
        }

        public List<string> Value { get { return this.value; } set { this.value = value; } }
        public List<string> Width { get { return this.width; } set { this.width = value; } }
        public List<string> Color { get { return this.color; } set { this.color = value; } }
    }
}
