/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ChartModel.cs
// 文件功能描述：ChartModel实体。
// 创建人：pekah.chow
// 创建时间： 2014-06-26
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace NFMT.Charts
{
    [Serializable()]
    public class ChartModel
    {
        private string title;
        private string subTitle;
        private XAxisModel xAxis;
        private YAxisModel yAxis;
        private string tooltip;
        private DataTable sourceTable;
        private Dictionary<string, NFMT.Common.IModel> series;
        private LegendModel legend;


        public ChartModel()
        {
            xAxis = new XAxisModel();
            yAxis = new YAxisModel();
            sourceTable = new DataTable();
            series = new Dictionary<string, NFMT.Common.IModel>();
            legend = new LegendModel();
        }

        /// <summary>
        /// 主标题
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle
        {
            get { return this.subTitle; }
            set { this.subTitle = value; }
        }
        /// <summary>
        /// X横轴的数据  
        /// </summary>
        public XAxisModel XAxis
        {
            get { return this.xAxis; }
            set { this.xAxis = value; }
        }
        /// <summary>
        /// Y轴的数据  
        /// </summary>
        public YAxisModel YAxis
        {
            get { return this.yAxis; }
            set { this.yAxis = value; }
        }
        /// <summary>
        /// 鼠标移到图形上时显示的提示框
        /// </summary>
        public string Tooltip
        {
            get { return this.tooltip; }
            set { this.tooltip = value; }
        }

        public System.Data.DataTable SourceTable
        {
            get { return this.sourceTable; }
            set { this.sourceTable = value; }
        }

        /// <summary>
        /// 数据
        /// </summary>
        //public Dictionary<string, Common.IModel> Series
        //{
        //    get { return this.series; }
        //    set { this.series = value; }
        //}
        /// <summary>
        /// legend 图例选项(设置highcharts图例（legend）属性靠右)
        /// </summary>
        //public LegendModel Legend
        //{
        //    get { return this.legend; }
        //    set { this.legend = value; }
        //}
    }
}
