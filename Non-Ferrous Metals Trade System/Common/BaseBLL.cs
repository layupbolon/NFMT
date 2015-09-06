using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Excel = Microsoft.Office.Interop.Excel;

namespace NFMT.Common
{
    public abstract class BaseBLL : IBaseBLL
    {
        protected abstract log4net.ILog Log { get; }

        public abstract IOperate Operate { get; }

        public virtual ResultModel Insert(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.Operate.Insert(user, obj);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }
            return result;
        }

        public virtual ResultModel Get(UserModel user, int id)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.Operate.Get(user, id);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public virtual ResultModel Load(UserModel user)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.Operate.Load(user);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }
            return result;
        }

        public virtual ResultModel Load<T>(UserModel user)
            where T:class,IModel
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.Operate.Load<T>(user);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }
            return result;
        }

        public virtual ResultModel Load(UserModel user, SelectModel select)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.Operate.Load(user, select);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }
            return result;
        }

        public virtual ResultModel Load(UserModel user, SelectModel select,IAuthority authority)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.Operate.Load(user, select,authority);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }
            return result;
        }

        public virtual ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.Get(user, obj.Id);
                if (result.ResultStatus != 0)
                    return result;

                IModel resultObj = (IModel)result.ReturnValue;

                if (resultObj == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "该数据不存在，不能更新";
                    return result;
                }
                result = this.Operate.Update(user, obj);

                if (result.ResultStatus != 0)
                    return result;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public virtual ResultModel Submit(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.Operate.Get(user, obj.Id);
                if (result.ResultStatus != 0)
                    return result;

                obj = (IModel)result.ReturnValue;

                if (obj == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "该数据不存在，不能提交审核";
                    return result;
                }

                result = this.Operate.Submit(user, obj);

                if (result.ResultStatus != 0)
                    return result;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public virtual ResultModel Audit(UserModel user, IModel obj, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.Get(user, obj.Id);
                if (result.ResultStatus != 0)
                    return result;

                obj = (IModel)result.ReturnValue;

                if (obj == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "该数据不存在，不能审核";
                    return result;
                }

                result = this.Operate.Audit(user, obj, isPass);

                if (result.ResultStatus != 0)
                    return result;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public virtual ResultModel GoBack(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.Operate.Get(user, obj.Id);
                obj = (IModel)result.ReturnValue;

                if (obj == null)
                {
                    result.Message = "该数据不存在，不能撤返";
                    return result;
                }

                result = this.Operate.Goback(user, obj);

                if (result.ResultStatus != 0)
                    return result;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public virtual ResultModel Invalid(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.Operate.Get(user, obj.Id);
                obj = (IModel)result.ReturnValue;

                if (obj == null)
                {
                    result.Message = "该数据不存在，不能作废";
                    return result;
                }

                result = this.Operate.Invalid(user, obj);

                if (result.ResultStatus != 0)
                    return result;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        public virtual ResultModel Close(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.Operate.Get(user, obj.Id);
                obj = (IModel)result.ReturnValue;

                if (obj == null)
                {
                    result.Message = "该数据不存在，不能关闭";
                    return result;
                }

                result = this.Operate.Close(user, obj);

                if (result.ResultStatus != 0)
                    return result;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="source">数据源,DataTable</param>
        /// <param name="modelPath">模板文件夹路径</param>
        /// <param name="filePath">新文件存放路径</param>
        /// <param name="reportType">报表类型</param>
        /// <returns>新文件全称</returns>
        public virtual string CreateExcel(System.Data.DataTable source, string modelPath, string filePath,ReportType reportType)
        {
            string newPath = string.Empty;
            Excel.Application app = null;
            Excel.Workbook tworkbook = null;
            System.IO.FileInfo mode = null;

            try
            {
                newPath = filePath + Guid.NewGuid() + ".xlsx";

                //调用的模板文件
                mode = new System.IO.FileInfo(string.Format("{0}{1}.xlsx", modelPath, reportType.ToString("F")));
                //mode.IsReadOnly = false;

                app = new Excel.Application();
                if (app == null)
                {
                    return string.Empty;
                }
                app.Application.DisplayAlerts = false;
                app.Visible = false;

                if (mode.Exists)
                {
                    Object missing = System.Reflection.Missing.Value;

                    app.Workbooks.Add(missing);
                    //调用模板
                    tworkbook = app.Workbooks.Open(mode.FullName, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                    Excel.Worksheet tworksheet = (Excel.Worksheet)tworkbook.Sheets[1];
                    Excel.Range r = tworksheet.get_Range("A2", missing);

                    string[,] objData = null;

                    if (objData != null && objData.Length > 0)
                    {
                        r = r.get_Resize(objData.GetLength(0), objData.GetLength(1));
                        r.Value = objData;
                    }

                    tworksheet.SaveAs(newPath, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                }
            }
            catch (Exception ex)
            {
                this.Log.ErrorFormat("导出excel错误，{0}", ex.Message);
            }
            finally
            {
                tworkbook.Close(false, mode.FullName, System.Reflection.Missing.Value);
                app.Workbooks.Close();
                app.Quit();
            }

            return newPath;
        }

        //public virtual string[,] SetExcelRangeData(System.Data.DataTable source)
        //{
        //    string[,] objData = new string[source.Rows.Count, 13];

        //    for (int i = 0; i < source.Rows.Count; i++)
        //    {
        //        System.Data.DataRow dr = source.Rows[i];

        //        objData[i, 0] = ((DateTime)dr["StockDate"]).ToShortDateString();
        //        objData[i, 1] = dr["CorpName"].ToString();
        //        objData[i, 2] = dr["RefNo"].ToString();
        //        objData[i, 3] = dr["AssetName"].ToString();
        //        objData[i, 4] = dr["CurGrossAmount"].ToString();
        //        objData[i, 5] = dr["CurNetAmount"].ToString();
        //        objData[i, 6] = dr["MUName"].ToString();
        //        objData[i, 7] = dr["BrandName"].ToString();
        //        objData[i, 8] = dr["DPName"].ToString();
        //        objData[i, 9] = dr["PaperNo"].ToString();
        //        objData[i, 10] = dr["CardNo"].ToString();
        //        objData[i, 11] = dr["CustomsTypeName"].ToString();
        //        objData[i, 12] = dr["StatusName"].ToString();
        //    }

        //    return objData;
        //}

        public virtual System.Data.DataTable SetExcelRangeData(System.Data.DataTable source)
        {
            return source;
        }
    }
}
