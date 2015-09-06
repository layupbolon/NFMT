/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：XAxisModel.cs
// 文件功能描述：X坐标实体。
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
    public class XAxisModel
    {
        private List<string> categories;
        private List<string> plotLines;
        private List<string> labels;

        public XAxisModel()
        {
            categories = new List<string>();
            plotLines = new List<string>();
            labels = new List<string>();
        }

        /// <summary>
        /// X轴横向列名称
        /// 在未赋值情况下，默认取数据源的列名或Key值
        /// </summary>
        public List<string> Categories
        {
            get 
            {
                if (this.categories.Count == 0)
                {
                    this.categories.Clear();

                    
                }

                return this.categories; 
            }
            set { this.categories = value; }
        }


        /// <summary>
        /// 一条竖线
        /// </summary>
        //public List<string> PlotLines
        //{
        //    get { return this.plotLines; }

        //    set { this.plotLines = value;}
        //}
        /// <summary>
        /// 设置横轴坐标的显示样式 
        /// </summary>
        //public List<string> Labels {
        //    get { return this.labels; }
        //    set { this.labels = value; }

        //}
    }
}
