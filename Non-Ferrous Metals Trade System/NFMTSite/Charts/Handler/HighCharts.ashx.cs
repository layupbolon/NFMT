using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NFMTSite.Charts
{
    /// <summary>
    /// HighCharts 的摘要说明
    /// </summary>
    public class HighCharts : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            List<NFMT.Charts.ChartModel> list = new List<NFMT.Charts.ChartModel>();
            NFMT.Charts.ChartModel model = new NFMT.Charts.ChartModel();
            //X横向轴说明数据
            model.XAxis.Categories.Add("一月");
            model.XAxis.Categories.Add("二月");
            model.XAxis.Categories.Add("三月");
            model.XAxis.Categories.Add("四月");
            model.XAxis.Categories.Add("五月");
            model.XAxis.Categories.Add("六月");
            model.XAxis.Categories.Add("七月");
            model.XAxis.Categories.Add("八月");
            model.XAxis.Categories.Add("九月");
            model.XAxis.Categories.Add("十月");
            model.XAxis.Categories.Add("十一月");
            model.XAxis.Categories.Add("十二月");

            //主标题
            model.Title ="2013年迈科入库表";
            //model.Title.X.Add("-20");

            model.SubTitle = "副标题";

            //Y轴数据
            model.YAxis.Title ="重量（吨)";
            model.Tooltip = "吨";
            //model.YAxis.PlotLines.Value.Add("0");
            //model.YAxis.PlotLines.Width.Add("1");
            //model.YAxis.PlotLines.Color.Add("#808080");

            //提示框数据单位
            //model.Tooltip.ValueSuffix.Add("吨");

            //图形选项配置数据
            //model.Legend.Layout.Add("vertical");
            //model.Legend.Align.Add("right");
            //model.Legend.VerticalAlign.Add("middle");
            //model.Legend.BorderWidth.Add("0");

            //线性数据
            //model.Series["铜"]["12000, 12300, 26000, 15822, 15201, 18005, 25001, 25410, 12546, 21542, 15800, 16500"];

            //model.Series.Add("铜");
            //model.Series.Add("铝");
            //model.Series.Add("锌");
            //model.Series.Add("其他");
            //model.Series.Add("12000, 12300, 26000, 15822, 15201, 18005, 25001, 25410, 12546, 21542, 15800, 16500");
            //model.Series.Add("2000, 1300, 2000, 5822, 5201, 8005, 5001, 5410, 2546, 1542, 5800, 6500");
            //model.Series.Add("1500, 2300, 6000, 1822, 1201, 1005, 2001, 1410, 1546, 5542, 5800, 2500");
            //model.Series.Add("5841, 2800, 1200, 1422, 800, 2000, 1200, 1810, 1500, 5181, 2800, 5500");

            //NFMT.Data.DAL.BDStyleDAL dal = new NFMT.Data.DAL.BDStyleDAL();

            //for (int i = 15; i < 17; i++)
            //{
            //    NFMT.Common.IModel bDStyle = dal.Get(NFMT.Common.DefaultValue.SysUser, i).ReturnValue as NFMT.Common.IModel;
            //    model.Series.Add(i.ToString(), bDStyle);
            //}
            //model.Series.Add("Name:铝", "Data:12000, 12300, 26000, 15822, 15201, 18005, 25001, 25410, 12546, 21542, 15800, 16500");
            //model.Series.Add("Name:锌", "Data:12000, 12300, 26000, 15822, 15201, 18005, 25001, 25410, 12546, 21542, 15800, 16500");
            //model.Series.Add("Name:其他", "Data:12000, 12300, 26000, 15822, 15201, 18005, 25001, 25410, 12546, 21542, 15800, 16500");


            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataColumn column = new System.Data.DataColumn("asset");
            dt.Columns.Add(column);

            column = new System.Data.DataColumn("一月");
            dt.Columns.Add(column);
            column = new System.Data.DataColumn("二月");
            dt.Columns.Add(column);
            column = new System.Data.DataColumn("三月");
            dt.Columns.Add(column);
            column = new System.Data.DataColumn("四月");
            dt.Columns.Add(column);
            column = new System.Data.DataColumn("五月");
            dt.Columns.Add(column);
            column = new System.Data.DataColumn("六月");
            dt.Columns.Add(column);
            column = new System.Data.DataColumn("七月");
            dt.Columns.Add(column);
            column = new System.Data.DataColumn("八月");
            dt.Columns.Add(column);
            column = new System.Data.DataColumn("九月");
            dt.Columns.Add(column);
            column = new System.Data.DataColumn("十月");
            dt.Columns.Add(column);
            column = new System.Data.DataColumn("十一月");
            dt.Columns.Add(column);
            column = new System.Data.DataColumn("十二月");
            dt.Columns.Add(column);

            System.Random random = new Random();
            System.Data.DataRow row = null;
            
            row = dt.NewRow();
            row[0] = "铜";
            for (int i = 1; i < 13; i++)
            {
                row[i] = random.Next(10000, 30000);             
            }
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[0] = "锌";
            for (int i = 1; i < 13; i++)
            {
                row[i] = random.Next(2000, 20000);          
            }
            dt.Rows.Add(row);

            dt.TableName = "Series";
            model.SourceTable = dt;

            string postData = NFMT.Charts.ChartJson.CreateJosn(model);
            context.Response.Write(postData);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}