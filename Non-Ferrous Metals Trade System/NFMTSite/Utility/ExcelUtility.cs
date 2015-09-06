using System;
using System.Data;
using System.IO;
using System.Web;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace NFMTSite.Utility
{
    public class ExcelUtility
    {
        public static void ExportExcel(string modelPath, string modelName, DataTable dtSource)
        {
            if (dtSource == null || dtSource.Rows.Count < 1)
                return;

            //创建工作簿对象
            XSSFWorkbook hssfworkbook;

            if (string.IsNullOrEmpty(modelPath) || string.IsNullOrEmpty(modelName))
                hssfworkbook = new XSSFWorkbook();
            else
            {
                //打开模板文件到文件流中
                using (
                    FileStream file = new FileStream(string.Format("{0}{1}.xlsx", modelPath, modelName),
                        FileMode.Open, FileAccess.Read))
                {
                    //将文件流中模板加载到工作簿对象中
                    hssfworkbook = new XSSFWorkbook(file);
                }
            }

            try
            {
                ISheet sheet1 = hssfworkbook.GetSheetAt(0);

                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    var row = sheet1.CreateRow(i + 1);
                    for (int j = 0; j < dtSource.Columns.Count; j++)
                    {
                        string drValue = dtSource.Rows[i][j].ToString();

                        switch (dtSource.Rows[i][j].GetType().ToString())
                        {
                            case "System.String": //字符串类型   
                                //cells.Add(rowIndex, colIndex, drValue);
                                row.CreateCell(j).SetCellValue(drValue);
                                break;
                            case "System.DateTime": //日期类型   
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                //cells.Add(rowIndex, colIndex, dateV, dateStyle);
                                dateV = dateV.Date;
                                ICell cell = row.CreateCell(j);

                                XSSFCellStyle style = (XSSFCellStyle)hssfworkbook.CreateCellStyle();
                                XSSFDataFormat format =
                                    (XSSFDataFormat)hssfworkbook.CreateDataFormat();
                                style.DataFormat = format.GetFormat("yyyy/mm/dd");
                                cell.CellStyle = style;

                                cell.SetCellValue(dateV);
                                break;
                            case "System.Boolean": //布尔型   
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                //cells.Add(rowIndex, colIndex, boolV);
                                row.CreateCell(j).SetCellValue(boolV);
                                break;
                            case "System.Int16": //整型   
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                //cells.Add(rowIndex, colIndex, intV);
                                row.CreateCell(j).SetCellValue(intV);
                                break;
                            case "System.Decimal": //浮点型   
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                //cells.Add(rowIndex, colIndex, doubV);
                                row.CreateCell(j).SetCellValue(doubV);
                                break;
                            case "System.DBNull": //空值处理   
                                //cells.Add(rowIndex, colIndex, null);
                                row.CreateCell(j).SetCellValue(string.Empty);
                                break;
                            default:
                                //cells.Add(rowIndex, colIndex, null);
                                row.CreateCell(j).SetCellValue(string.Empty);
                                break;
                        }
                    }
                }
                //for (int i = 0; i <= dataSource.GetUpperBound(0); i++)
                //{
                //    row = sheet1.CreateRow(i + 1);
                //    for (int j = 0; j <= dataSource.GetUpperBound(1); j++)
                //    {
                //        row.CreateCell(j).SetCellValue(dataSource[i, j]);
                //    }
                //}
                ////强制Excel重新计算表中所有的公式
                //sheet1.ForceFormulaRecalculation = true;
            }
            catch (Exception)
            {
                if (HttpContext.Current.Request.UrlReferrer != null)
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.UrlReferrer.ToString());
            }

            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AppendHeader("Content-Disposition",
                string.Format("attachment;filename={0}.xlsx",
                    string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMdd"), modelName)));
            HttpContext.Current.Response.Clear();

            try
            {
                //写入到客户端 
                MemoryStream ms = new MemoryStream();
                hssfworkbook.Write(ms);
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            }
            catch
            {
                if (HttpContext.Current.Request.UrlReferrer != null)
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.UrlReferrer.ToString());
            }
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}