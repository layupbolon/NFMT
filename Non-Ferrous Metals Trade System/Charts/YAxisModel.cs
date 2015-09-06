/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：YAxisModel.cs
// 文件功能描述：Y坐标实体。
// 创建人：pekah.chow
// 创建时间： 2014-06-26
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Charts
{
    public class YAxisModel
    {
        private string title;
        private PlotLinesModel plotLines;
        //private string tooltip;
        private List<string> myProperty;

        public YAxisModel() {           
            plotLines = new PlotLinesModel();           
            myProperty = new List<string>();
        }


        /// <summary>
        /// 左侧标题
        /// </summary>
        public string Title 
        { 
            get { return this.title; }
            set { this.title = value; } 
        }
        /// <summary>
        /// 曲线配置（value0，width1,color）
        /// </summary>
        //public PlotLinesModel PlotLines 
        //{ 
        //    get { return this.plotLines; } 
        //    set { this.plotLines = value; }
        //}
        /// <summary>
        /// 鼠标移到图形上时显示的提示框  
        /// </summary>
        //public string Tooltip 
        //{ 
        //    get { return this.tooltip; }
        //    set { this.tooltip = value; }
        
        //}


        //public List<string> MyProperty 
        //{ 
        //    get { return this.myProperty; }
        //    set { this.myProperty = value; }
        //}
    }
}
